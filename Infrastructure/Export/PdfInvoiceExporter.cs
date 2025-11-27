using System;
using System.IO;

using ecspage.Application.Contracts;
using ecspage.Infrastructure.Abstractions;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ecspage.Infrastructure.Export
{
    public class PdfInvoiceExporter : IInvoiceExporter
    {
        private readonly IFacturaRepository _facturas;
        public PdfInvoiceExporter(IFacturaRepository facturas) => _facturas = facturas;

        public Result ExportToPdf(int facturaId, string filePath)
        {
            try
            {
                var f = _facturas.Obtener(facturaId);
                if (f == null) return Result.Fail("No se encontró la factura.");

                var doc = new Document(PageSize.A4, 40f, 40f, 40f, 40f);
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                var titulo = $"FACTURA {(string.IsNullOrWhiteSpace(f.Serie) ? f.Id.ToString() : f.Serie)}";
                doc.Add(new Paragraph(titulo) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 12f });

                doc.Add(new Paragraph($"Cliente:     {f.ClienteNombre}"));
                doc.Add(new Paragraph($"RUC/DNI:     {f.ClienteRuc ?? "-"}"));
                doc.Add(new Paragraph($"Email:       {f.ClienteEmail ?? "-"}"));
                doc.Add(new Paragraph($"Dirección:   {f.ClienteDireccion ?? "-"}"));
                doc.Add(new Paragraph($"Fecha:       {f.Fecha:dd/MM/yyyy}"));
                doc.Add(new Paragraph($"Estado:      {f.Estado}"));
                doc.Add(new Paragraph(" "));

                var table = new PdfPTable(4) { WidthPercentage = 100f };
                table.SetWidths(new float[] { 50f, 15f, 15f, 20f });

                AddHeader(table, "Producto");
                AddHeader(table, "Cantidad");
                AddHeader(table, "P. Unitario (S/)");
                AddHeader(table, "Subtotal (S/)");

                foreach (var d in f.Detalles)
                {
                    table.AddCell(new Phrase(d.Producto));
                    table.AddCell(new Phrase(d.Cantidad.ToString("N2")));
                    table.AddCell(new Phrase(d.Precio.ToString("N2")));
                    table.AddCell(new Phrase(d.Importe.ToString("N2")));
                }

                doc.Add(table);
                doc.Add(new Paragraph(" "));

                doc.Add(new Paragraph($"Subtotal: S/ {f.Subtotal:N2}"));
                doc.Add(new Paragraph($"Impuesto: S/ {f.Impuesto:N2}"));
                doc.Add(new Paragraph($"TOTAL:    S/ {f.Total:N2}"));

                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("Documento generado por Sistema de Facturación ECS")
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 12f
                });

                doc.Close();
                return Result.Ok("PDF generado correctamente.");
            }
            catch (Exception ex)
            {
                return Result.Fail("Error al generar PDF: " + ex.Message);
            }
        }
        private static void AddHeader(PdfPTable t, string text)
        {
            var cell = new PdfPCell(new Phrase(text))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5f
            };
            t.AddCell(cell);
        }
    }
}
