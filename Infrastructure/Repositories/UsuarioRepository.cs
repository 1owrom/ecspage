using ecspage.Application.Contracts;
using ecspage.Infrastructure.Abstractions;
using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecspage.Infrastructure.Repositories
{
    internal class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConnectionFactory _factory;

        public UsuarioRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public Usuario ObtenerPorUsuario(String usuario)
        {
            using var conn = _factory.Create();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                SELECT Id, Usuario, Contrasena
                FROM Usuarios
                WHERE Usuario = @usuario";

            cmd.Parameters.Add(new SqlParameter("@usuario",usuario));

            using var reader = cmd.ExecuteReader();

            if (reader.Read()) return null;

            return new Usuario
            {
                Id = (int)reader["Id"],
                UsuarioNombre = reader["Usuario"].ToString(),
                Contrasena = reader["Contrasena"].ToString()
            }; 
        }
    }
}
