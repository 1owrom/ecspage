using ecspage.Application.Contracts;
using ecspage.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecspage.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _repo;

        public AuthService(IUsuarioRepository repo) 
        {
            _repo = repo;
        }

        public Usuario Login(string usuario, string contrasena) 
        {
            var user = _repo.ObtenerPorUsuario(usuario);
            if (user == null) return null;

            if (user.Contrasena != contrasena) return null;

            return user;
        }
    }
}
