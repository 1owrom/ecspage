using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecspage.Application.Contracts;
using ecspage.Infrastructure.Abstractions;

namespace ecspage.Application.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IFacturaRepository _facturas;
        private readonly IProductoRepository _productos;
        private readonly ITotalesCalculator _calc;

        public FacturaService(IUnitOfWork uow, IFacturaRepository facturas, IProductoRepository productos, ITotalesCalculator calc)
        {
            _uow = uow; _facturas = facturas; _productos = productos; _calc = calc;
        }

        public Result CrearFactura(CrearFacturaCommand cmd)
        {
            if (cmd.Detalles == null || cmd.Detalles.Count == 0)
                return Result.Fail("La factura debe tener al menos un ítem.");

            var items = cmd.Detalles.Select(d => (d.Cantidad, d.PrecioUnitario));
            var (sub, igv, tot) = _calc.Calcular(items, cmd.PrecioIncluyeIGV);

            try
            {
                _uow.Begin();

                var id = _facturas.InsertarFactura(cmd.ClienteId, cmd.FechaEmision, cmd.Estado, sub, igv, tot, _uow.Transaction!);

                foreach (var d in cmd.Detalles)
                {
                    _facturas.InsertarDetalle(id, d.ProductoId, d.Cantidad, d.PrecioUnitario, _uow.Transaction!);
                    _productos.DescontarStock(d.ProductoId, (int)d.Cantidad, _uow.Transaction!);
                }

                _uow.Commit();
                return Result.Ok($"Factura F00{id} emitida.");
            }
            catch (System.Exception ex)
            {
                _uow.Rollback();
                return Result.Fail("No se pudo crear la factura: " + ex.Message);
            }
        }

        public Result AnularFactura(int facturaId)
        {
            try { _facturas.Anular(facturaId); return Result.Ok("Factura anulada."); }
            catch (System.Exception ex) { return Result.Fail("No se pudo anular: " + ex.Message); }
        }
        public FacturaDTO? ObtenerFactura(int facturaId) => _facturas.Obtener(facturaId);
        public List<FacturaDTO> Listar(FiltroFacturas filtro) => _facturas.Listar(filtro);


        public Result EditarFactura(int idFactura, int nuevoClienteId, string nuevoEstado)
        {
            try
            {
                _facturas.ActualizarFactura(idFactura, nuevoClienteId, nuevoEstado);
                return Result.Ok("Factura actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return Result.Fail("No se pudo actualizar la factura: " + ex.Message);
            }
        }

    }

}
