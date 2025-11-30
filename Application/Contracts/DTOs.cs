using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecspage.Application.Contracts
{
    public record CrearFacturaDetalleCmd(int ProductoId, decimal Cantidad, decimal PrecioUnitario);

    public record CrearFacturaCommand(
        int ClienteId,
        DateTime FechaEmision,
        string Estado,
        bool PrecioIncluyeIGV,
        IReadOnlyList<CrearFacturaDetalleCmd> Detalles
    );
    public class FacturaDetalleDTO
    {
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }

        public FacturaDetalleDTO(string producto, decimal cantidad, decimal precio, decimal importe)
        {
            Producto = producto;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
        }
    }

    public class FacturaDTO
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Serie { get; set; } = "";
        public string ClienteNombre { get; set; } = "";
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = "";
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public List<FacturaDetalleDTO> Detalles { get; set; } = new();
        public string? ClienteRuc { get; set; }
        public string? ClienteEmail { get; set; }
        public string? ClienteDireccion { get; set; }
    }
    public record FiltroFacturas(int? ClienteId, string? Estado, DateTime? Desde, DateTime? Hasta);
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Ruc { get; set; } = "";
        public string? Email { get; set; }
        public string? Direccion { get; set; }
    }
    // Registrar producto (Datos)
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
    }


}

