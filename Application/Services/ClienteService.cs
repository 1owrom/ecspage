using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ecspage.Infrastructure.Abstractions;
using ecspage.Application.Contracts;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repo;
    public ClienteService(IClienteRepository repo) => _repo = repo;

    public IEnumerable<ClienteDTO> Listar() =>
        _repo.Listar().Select(c => new ClienteDTO
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Ruc = c.Ruc,
            Email = c.Email,
            Direccion = c.Direccion
        });

    public Result Crear(string nombre, string ruc, string? email, string? dir, out int nuevoId)
    {
        nuevoId = 0;
        if (string.IsNullOrWhiteSpace(nombre)) return Result.Fail("Nombre requerido.");
        if (!(ruc.All(char.IsDigit) && (ruc.Length == 8 || ruc.Length == 11)))
            return Result.Fail("RUC/DNI inválido (8 u 11 dígitos).");

        try { nuevoId = _repo.Crear(nombre, ruc, email, dir); return Result.Ok("Cliente creado."); }
        catch (InvalidOperationException ex) { return Result.Fail(ex.Message); } // RUC duplicado (unique) cambio xxx
    }
}
