using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecspage.Application.Contracts;
using ecspage.Infrastructure.Abstractions;

namespace ecspage.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repo;
        public ProductoService(IProductoRepository repo) => _repo = repo;

        public List<(int Id, string Nombre, decimal Precio, int Stock)> ListarActivos()
            => _repo.ListarActivos();

        public Result VerificarStock(IReadOnlyList<CrearFacturaDetalleCmd> detalles)
        {
            foreach (var d in detalles)
            {
                var p = _repo.Obtener(d.ProductoId);
                if (p is null) return Result.Fail($"Producto {d.ProductoId} no existe.");
                if (d.Cantidad > p.Value.Stock)
                    return Result.Fail($"Stock insuficiente para '{p.Value.Nombre}'. Disponible: {p.Value.Stock}");
            }
            return Result.Ok();
        }
    }
}
