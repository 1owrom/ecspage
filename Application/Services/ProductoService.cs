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
        // Nuevo cambio para gestionar agregar producto
        public Result Crear(string nombre, decimal precio, int stock, out int nuevoId)
        {
            nuevoId = 0;

            if (string.IsNullOrWhiteSpace(nombre))
                return Result.Fail("El nombre del producto es obligatorio.");

            if (precio < 0)
                return Result.Fail("El precio no puede ser negativo.");

            if (stock < 0)
                return Result.Fail("El stock no puede ser negativo.");

            _repo.Insertar(nombre.Trim(), precio, stock, out nuevoId);
            return Result.Ok();
        }
        public Result Actualizar(int idProducto, string nombre, decimal precio, int stock, bool activo)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return Result.Fail("El nombre del producto es obligatorio.");

            if (precio < 0)
                return Result.Fail("El precio no puede ser negativo.");

            if (stock < 0)
                return Result.Fail("El stock no puede ser negativo.");

            _repo.Actualizar(idProducto, nombre.Trim(), precio, stock, activo);
            return Result.Ok();
        }
        public Result Eliminar(int idProducto)
        {
            _repo.Eliminar(idProducto);
            return Result.Ok();
        }
    }
}
