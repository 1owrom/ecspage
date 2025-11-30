using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ecspage.Application.Contracts;
using ecspage.Infrastructure.Abstractions;
using Microsoft.Data.SqlClient;

namespace ecspage.Infrastructure.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly IConnectionFactory _factory;
        public FacturaRepository(IConnectionFactory factory) => _factory = factory;

        private static SqlParameter DDec(string name, decimal value) =>
            new SqlParameter(name, SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = value };

        public int InsertarFactura(int clienteId, DateTime fecha, string estado,
            decimal subtotal, decimal impuesto, decimal total, DbTransaction tx)
        {
            var cmd = tx.Connection!.CreateCommand();
            cmd.Transaction = tx;

            cmd.CommandText = @"
                INSERT INTO Facturas (IdCliente, FechaEmision, Estado, Subtotal, Impuesto, Total)
                OUTPUT INSERTED.IdFactura
                VALUES (@c, @f, @e, @s, @i, @t)";

            cmd.Parameters.Add(new SqlParameter("@c", SqlDbType.Int) { Value = clienteId });
            cmd.Parameters.Add(new SqlParameter("@f", SqlDbType.Date) { Value = fecha.Date });
            cmd.Parameters.Add(new SqlParameter("@e", SqlDbType.VarChar, 10) { Value = estado });
            cmd.Parameters.Add(DDec("@s", subtotal));
            cmd.Parameters.Add(DDec("@i", impuesto));
            cmd.Parameters.Add(DDec("@t", total));

            return (int)cmd.ExecuteScalar()!;
        }

        public void InsertarDetalle(int facturaId, int productoId, decimal cantidad, decimal precio, DbTransaction tx)
        {
            var cmd = tx.Connection!.CreateCommand();
            cmd.Transaction = tx;

            cmd.CommandText = @"
                INSERT INTO DetallesFactura (IdFactura, IdProducto, Cantidad, PrecioUnitario)
                VALUES (@f, @p, @c, @pr)";

            cmd.Parameters.Add(new SqlParameter("@f", SqlDbType.Int) { Value = facturaId });
            cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.Int) { Value = productoId });
            cmd.Parameters.Add(DDec("@c", cantidad));
            cmd.Parameters.Add(DDec("@pr", precio));
            cmd.ExecuteNonQuery();
        }

        public void Anular(int facturaId)
        {
            using var cn = _factory.Create();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = "UPDATE Facturas SET Estado = 'Anulada' WHERE IdFactura = @id";
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = facturaId });
            cmd.ExecuteNonQuery();
        }

        public FacturaDTO? Obtener(int facturaId)
        {
            using var cn = _factory.Create();

            // Maestro
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT
                        f.IdFactura         AS Id,
                        c.Nombre            AS ClienteNombre,
                        f.FechaEmision      AS Fecha,
                        f.Estado            AS Estado,
                        f.Subtotal          AS Subtotal,
                        f.Impuesto          AS Impuesto,
                        f.Total             AS Total,
                        c.RucDni            AS ClienteRuc,
                        c.Email             AS ClienteEmail,
                        c.Direccion         AS ClienteDireccion,
                        f.Serie             AS Serie
                    FROM Facturas f
                    INNER JOIN Clientes c ON c.IdCliente = f.IdCliente
                    WHERE f.IdFactura = @id";
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = facturaId });

                using var rd = cmd.ExecuteReader();
                if (!rd.Read()) return null;

                var dto = new FacturaDTO
                {
                    Id = rd.GetInt32(0),
                    ClienteNombre = rd.GetString(1),
                    Fecha = rd.GetDateTime(2),
                    Estado = rd.GetString(3),
                    Subtotal = rd.GetDecimal(4),
                    Impuesto = rd.GetDecimal(5),
                    Total = rd.GetDecimal(6),
                    ClienteRuc = rd.IsDBNull(7) ? null : rd.GetString(7),
                    ClienteEmail = rd.IsDBNull(8) ? null : rd.GetString(8),
                    ClienteDireccion = rd.IsDBNull(9) ? null : rd.GetString(9),
                    Serie = rd.IsDBNull(10) ? string.Empty : rd.GetString(10),
                    Detalles = new List<FacturaDetalleDTO>()
                };

                rd.Close();

                using var cmd2 = cn.CreateCommand();
                cmd2.CommandText = @"
                    SELECT
                        p.Nombre         AS Producto,
                        d.Cantidad       AS Cantidad,
                        d.PrecioUnitario AS Precio,
                        d.Importe        AS Importe
                    FROM DetallesFactura d
                    INNER JOIN Productos p ON p.IdProducto = d.IdProducto
                    WHERE d.IdFactura = @id";
                cmd2.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = facturaId });

                using var rd2 = cmd2.ExecuteReader();
                while (rd2.Read())
                {
                    dto.Detalles.Add(new FacturaDetalleDTO(
                        rd2.GetString(0),
                        rd2.GetDecimal(1),
                        rd2.GetDecimal(2),
                        rd2.GetDecimal(3)
                    ));
                }

                return dto;
            }
        }

        public void ActualizarFactura(int idFactura, int nuevoClienteId, string nuevoEstado)
        {
            using var cn = _factory.Create();
            using var cmd = cn.CreateCommand();

            cmd.CommandText = @"
        UPDATE Facturas
        SET 
            IdCliente = @cli,
            Estado = @est
        WHERE IdFactura = @id";

            cmd.Parameters.Add(new SqlParameter("@cli", SqlDbType.Int) { Value = nuevoClienteId });
            cmd.Parameters.Add(new SqlParameter("@est", SqlDbType.VarChar, 10) { Value = nuevoEstado });
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = idFactura });

            cmd.ExecuteNonQuery();
        }


        public List<FacturaDTO> Listar(FiltroFacturas filtro)
        {
            using var cn = _factory.Create();
            using var cmd = cn.CreateCommand();

            var sql = @"
                SELECT
                    f.IdFactura     AS Id,
                    f.Serie         AS Serie,
                    c.Nombre        AS ClienteNombre,
                    f.FechaEmision  AS Fecha,
                    f.Estado        AS Estado,
                    f.Subtotal      AS Subtotal,
                    f.Impuesto      AS Impuesto,
                    f.Total         AS Total
                FROM Facturas f
                INNER JOIN Clientes c ON c.IdCliente = f.IdCliente
                WHERE 1 = 1";

            if (filtro.ClienteId.HasValue)
            {
                sql += " AND c.IdCliente = @cli";
                cmd.Parameters.Add(new SqlParameter("@cli", SqlDbType.Int) { Value = filtro.ClienteId.Value });
            }

            if (!string.IsNullOrWhiteSpace(filtro.Estado))
            {
                sql += " AND f.Estado = @est";
                cmd.Parameters.Add(new SqlParameter("@est", SqlDbType.VarChar, 10) { Value = filtro.Estado! });
            }

            if (filtro.Desde.HasValue)
            {
                sql += " AND f.FechaEmision >= @d";
                cmd.Parameters.Add(new SqlParameter("@d", SqlDbType.Date) { Value = filtro.Desde.Value.Date });
            }

            if (filtro.Hasta.HasValue)
            {
                sql += " AND f.FechaEmision <= @h";
                cmd.Parameters.Add(new SqlParameter("@h", SqlDbType.Date) { Value = filtro.Hasta.Value.Date });
            }

            sql += " ORDER BY f.FechaEmision DESC";
            cmd.CommandText = sql;

            using var rd = cmd.ExecuteReader();
            var list = new List<FacturaDTO>();
            while (rd.Read())
            {
                var dto = new FacturaDTO
                {
                    Id = rd.GetInt32(0),
                    Serie = rd.IsDBNull(1) ? string.Empty : rd.GetString(1),
                    ClienteNombre = rd.GetString(2),
                    Fecha = rd.GetDateTime(3),
                    Estado = rd.GetString(4),
                    Subtotal = rd.GetDecimal(5),
                    Impuesto = rd.GetDecimal(6),
                    Total = rd.GetDecimal(7),
                    Detalles = new List<FacturaDetalleDTO>()
                };

                list.Add(dto);
            }
            return list;
        }
    }
}
