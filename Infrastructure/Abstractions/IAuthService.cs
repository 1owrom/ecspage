using ecspage.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecspage.Infrastructure.Abstractions
{
    internal interface IAuthService
    {
        Usuario Login(string usuario, string contrasena);
    }
}
