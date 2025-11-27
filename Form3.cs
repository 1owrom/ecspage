using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using ecspage.Bootstrap;
using ecspage.Application.Contracts;

namespace ecspage
{
    public partial class FormDetallesFactura : System.Windows.Forms.Form
    {
        private readonly int _idFactura;
        private IFacturaService _facturas;

        public FormDetallesFactura(int idFactura)
        {
            InitializeComponent();
            _idFactura = idFactura;
            Load += FormDetallesFactura_Load;
        }

        private void FormDetallesFactura_Load(object? sender, EventArgs e)
        {
            try
            {
                _facturas = AppHost.Get<IFacturaService>();
                CargarDetallesFactura();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo iniciar la vista de detalle:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void CargarDetallesFactura()
        {
            try
            {
                panelPreview.Controls.Clear();
                panelPreview.Visible = true;

                var f = _facturas.ObtenerFactura(_idFactura);
                if (f == null)
                {
                    MessageBox.Show("Factura no encontrada.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                // ===== Encabezado =====
                int y = 20;
                void AddLabel(string text, bool bold = false)
                {
                    var lbl = new Label
                    {
                        Text = text,
                        Font = new Font("Segoe UI", bold ? 11 : 10, bold ? FontStyle.Bold : FontStyle.Regular),
                        Location = new Point(20, y),
                        AutoSize = true
                    };
                    panelPreview.Controls.Add(lbl);
                    y += 22;
                }

                AddLabel($"Factura {(!string.IsNullOrWhiteSpace(f.Serie) ? f.Serie : $"N° {f.Id}")}", true);
                AddLabel($"Estado: {f.Estado}");
                AddLabel($"Fecha de Emisión: {f.Fecha:dd/MM/yyyy}");

                AddLabel("Cliente", true);
                AddLabel($"Nombre  : {f.ClienteNombre}");
                AddLabel($"RUC/DNI : {f.ClienteRuc ?? "-"}");
                AddLabel($"Email   : {f.ClienteEmail ?? "-"}");
                AddLabel($"Dirección: {f.ClienteDireccion ?? "-"}");

                AddLabel($"Subtotal: S/ {f.Subtotal:N2}");
                AddLabel($"Impuesto: S/ {f.Impuesto:N2}");
                AddLabel($"Total   : S/ {f.Total:N2}", true);

                // ===== Grilla de detalles =====
                var dt = new DataTable();
                dt.Columns.Add("Producto", typeof(string));
                dt.Columns.Add("Cantidad", typeof(decimal));
                dt.Columns.Add("PrecioUnitario", typeof(decimal));
                dt.Columns.Add("Subtotal", typeof(decimal)); // <- subtotal por producto

                foreach (var d in f.Detalles)
                    dt.Rows.Add(d.Producto, d.Cantidad, d.Precio, d.Importe);

                var dgv = new DataGridView
                {
                    DataSource = dt,
                    Location = new Point(20, y + 10),
                    Size = new Size(1040, 400),
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false
                };

                dgv.DataBindingComplete += (s, e) =>
                {
                    // Encabezados
                    dgv.Columns["Producto"].HeaderText = "Producto";
                    dgv.Columns["Cantidad"].HeaderText = "Cantidad";
                    dgv.Columns["PrecioUnitario"].HeaderText = "P. Unitario (S/)";
                    dgv.Columns["Subtotal"].HeaderText = "Subtotal (S/)";

                    // Formatos
                    dgv.Columns["Cantidad"].DefaultCellStyle.Format = "N2";
                    dgv.Columns["PrecioUnitario"].DefaultCellStyle.Format = "N2";
                    dgv.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
                };

                // Prefijo S/ solo visual en monetarias
                dgv.CellFormatting += (s, e) =>
                {
                    if (e.Value == null) return;
                    var col = dgv.Columns[e.ColumnIndex].Name;
                    if (col == "PrecioUnitario" || col == "Subtotal")
                    {
                        if (decimal.TryParse(e.Value.ToString(), out var val))
                        {
                            e.Value = $"S/ {val:N2}";
                            e.FormattingApplied = true;
                        }
                    }
                };

                panelPreview.Controls.Add(dgv);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el detalle:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e) => Close();
    }
}
