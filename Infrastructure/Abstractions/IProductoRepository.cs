using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecspage.Infrastructure.Abstractions
{
    public interface IProductoRepository
    {
        List<(int Id, string Nombre, decimal Precio, int Stock)> ListarActivos();
        (int Id, string Nombre, decimal Precio, int Stock)? Obtener(int id);
        void DescontarStock(int productoId, int cantidad, DbTransaction tx);
    }
}
