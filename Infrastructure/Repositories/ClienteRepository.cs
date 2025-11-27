using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ecspage.Infrastructure.Abstractions;
using Microsoft.Data.SqlClient;

namespace ecspage.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConnectionFactory _factory;
        public ClienteRepository(IConnectionFactory factory) => _factory = factory;

        public List<(int Id, string Nombre, string Ruc, string Email, string Direccion)> Listar()
        {
            using var cn = _factory.Create();
            using var cmd = cn.CreateCommand();

            cmd.CommandText = @"
                SELECT
                    c.IdCliente  AS Id,
                    c.Nombre     AS Nombre,
                    c.RucDni     AS Ruc,
                    c.Email      AS Email,
                    c.Direccion  AS Direccion
                FROM Clientes c
                ORDER BY c.Nombre";
            using var rd = cmd.ExecuteReader();

            var list = new List<(int, string, string, string, string)>();
            while (rd.Read())
                list.Add((rd.GetInt32(0), rd.GetString(1), rd.GetString(2),
                          rd.IsDBNull(3) ? "" : rd.GetString(3),
                          rd.IsDBNull(4) ? "" : rd.GetString(4)));
            return list;
        }

        public (int Id, string Nombre, string Ruc, string Email, string Direccion)? Obtener(int id)
        {
            using var cn = _factory.Create();
            using var cmd = cn.CreateCommand();

            cmd.CommandText = @"
                SELECT
                    c.IdCliente  AS Id,
                    c.Nombre     AS Nombre,
                    c.RucDni     AS Ruc,
                    c.Email      AS Email,
                    c.Direccion  AS Direccion
                FROM Clientes c
                WHERE c.IdCliente = @id";
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });

            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;

            return (rd.GetInt32(0), rd.GetString(1), rd.GetString(2),
                    rd.IsDBNull(3) ? "" : rd.GetString(3),
                    rd.IsDBNull(4) ? "" : rd.GetString(4));
        }
        public int Crear(string nombre, string rucDni, string? email, string? direccion)
        {
            using var cn = _factory.Create();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Clientes (Nombre, RucDni, Email, Direccion)
                OUTPUT INSERTED.IdCliente
                VALUES (@n, @r, @e, @d)";
            cmd.Parameters.Add(new SqlParameter("@n", SqlDbType.NVarChar, 200) { Value = nombre });
            cmd.Parameters.Add(new SqlParameter("@r", SqlDbType.VarChar, 15) { Value = rucDni });
            cmd.Parameters.Add(new SqlParameter("@e", SqlDbType.NVarChar, 200) { Value = (object?)email ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@d", SqlDbType.NVarChar, 300) { Value = (object?)direccion ?? DBNull.Value });

            try
            {
                return (int)cmd.ExecuteScalar()!;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Ya existe un cliente con ese RUC/DNI.", ex);
            }
        }
    }
}

