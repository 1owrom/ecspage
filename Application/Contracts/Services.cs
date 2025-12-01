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
        Result EditarFactura(int idFactura, int nuevoClienteId, string nuevoEstado);

    }

    public interface IProductoService
    {
        List<(int Id, string Nombre, decimal Precio, int Stock)> ListarActivos();
        Result VerificarStock(IReadOnlyList<CrearFacturaDetalleCmd> detalles);
        // Nuevo formulario para gestionar productos
        Result Crear(string nombre, decimal precio, int stock, out int nuevoId);
        Result Actualizar(int idProducto, string nombre, decimal precio, int stock, bool activo);
        Result Eliminar(int idProducto);
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