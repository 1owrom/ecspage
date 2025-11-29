using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecspage.Infrastructure.Abstractions
{
    internal class Usuario
    {
        public int Id { get; set; }
        public string UsuarioNombre { get; set; }
        public string Contrasena { get; set; }
    }
}
