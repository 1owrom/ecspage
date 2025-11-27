using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace ecspage.Application.Contracts
{
    public interface IFacturaService
    {
        Result CrearFactura(CrearFacturaCommand cmd);
        Result AnularFactura(int facturaId);
        FacturaDTO? ObtenerFactura(int facturaId);
        List<FacturaDTO> Listar(FiltroFacturas filtro);
    }

    public interface IProductoService
    {
        List<(int Id, string Nombre, decimal Precio, int Stock)> ListarActivos();
        Result VerificarStock(IReadOnlyList<CrearFacturaDetalleCmd> detalles);
    }

    public interface IInvoiceExporter
    {
        Result ExportToPdf(int facturaId, string filePath);
    }
    public interface IClienteService
    {
        IEnumerable<ClienteDTO> Listar();
        Result Crear(string nombre, string rucDni, string? email, string? direccion, out int nuevoId);
    }
    public interface IDetalleFacturaService
    {
        IEnumerable<FacturaDetalleDTO> ListarPorFactura(int idFactura);
    }


}