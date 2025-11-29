using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using ecspage.Infrastructure.Abstractions;
using Microsoft.Data.SqlClient;

namespace ecspage.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IConnectionFactory _factory;
        public ProductoRepository(IConnectionFactory factory) => _factory = factory;

        public List<(int Id, string Nombre, decimal Precio, int Stock)> ListarActivos()
        {
            using var cn = _factory.Create();
            using var cmd = cn.CreateCommand();

            cmd.CommandText = @"
                SELECT
                    p.IdProducto     AS Id,
                    p.Nombre         AS Nombre,
                    p.PrecioUnitario AS Precio,
                    p.Stock          AS Stock
                FROM Productos p
                WHERE p.Activo = 1
                ORDER BY p.Nombre";
            using var rd = cmd.ExecuteReader();

            var list = new List<(int, string, decimal, int)>();
            while (rd.Read())
                list.Add((rd.GetInt32(0), rd.GetString(1), rd.GetDecimal(2), rd.GetInt32(3)));
            return list;
        }

        public (int Id, string Nombre, decimal Precio, int Stock)? Obtener(int id)
        {
            using var cn = _factory.Create();
            using var cmd = cn.CreateCommand();

            cmd.CommandText = @"
                SELECT
                    p.IdProducto     AS Id,
                    p.Nombre         AS Nombre,
                    p.PrecioUnitario AS Precio,
                    p.Stock          AS Stock
                FROM Productos p
                WHERE p.IdProducto = @id";
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });

            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;

            return (rd.GetInt32(0), rd.GetString(1), rd.GetDecimal(2), rd.GetInt32(3));
        }

        public void DescontarStock(int productoId, int cantidad, DbTransaction tx)
        {
            var cmd = tx.Connection!.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = @"
                UPDATE Productos
                SET Stock = Stock - @cant
                WHERE IdProducto = @id";
            cmd.Parameters.Add(new SqlParameter("@cant", SqlDbType.Int) { Value = cantidad });
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = productoId });
            cmd.ExecuteNonQuery();
        }

        // Nuevo cambio para gestionar agregar producto
        public void Insertar(string nombre, decimal precio, int stock, out int nuevoId)
        {
            using var cn = _factory.Create();
            using var cmd = cn.CreateCommand();

            cmd.CommandText = @"
        INSERT INTO Productos (Nombre, PrecioUnitario, Stock, Activo)
        VALUES (@n, @p, @s, 1);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            cmd.Parameters.Add(new SqlParameter("@n", nombre));
            cmd.Parameters.Add(new SqlParameter("@p", precio));
            cmd.Parameters.Add(new SqlParameter("@s", stock));

            nuevoId = (int)cmd.ExecuteScalar();
        }

        public void Actualizar(int idProducto, string nombre, decimal precio, int stock, bool activo)
        {
            using var cn = _factory.Create();
            using var cmd = cn.CreateCommand();

            cmd.CommandText = @"
        UPDATE Productos
        SET Nombre=@n,
            PrecioUnitario=@p,
            Stock=@s,
            Activo=@a
        WHERE IdProducto=@id";

            cmd.Parameters.Add(new SqlParameter("@n", nombre));
            cmd.Parameters.Add(new SqlParameter("@p", precio));
            cmd.Parameters.Add(new SqlParameter("@s", stock));
            cmd.Parameters.Add(new SqlParameter("@a", activo));
            cmd.Parameters.Add(new SqlParameter("@id", idProducto));

            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int idProducto)
        {
            using var cn = _factory.Create();
            using var cmd = cn.CreateCommand();

            cmd.CommandText = @"
        UPDATE Productos
        SET Activo = 0
        WHERE IdProducto=@id";

            cmd.Parameters.Add(new SqlParameter("@id", idProducto));
            cmd.ExecuteNonQuery();
        }

    }
}
