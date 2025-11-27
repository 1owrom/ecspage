using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecspage.Infrastructure.Abstractions
{
    public interface IClienteRepository
    {
        List<(int Id, string Nombre, string Ruc, string Email, string Direccion)> Listar();
        (int Id, string Nombre, string Ruc, string Email, string Direccion)? Obtener(int id);
        int Crear(string nombre, string ruc, string? email, string? direccion);
    }

}
