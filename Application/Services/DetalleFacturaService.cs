using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecspage.Application.Contracts;
using ecspage.Infrastructure.Abstractions;

public class DetalleFacturaService : IDetalleFacturaService
{
    private readonly IFacturaRepository _facturas;
    public DetalleFacturaService(IFacturaRepository facturas) => _facturas = facturas;

    public IEnumerable<FacturaDetalleDTO> ListarPorFactura(int idFactura)
        => _facturas.Obtener(idFactura)?.Detalles ?? Enumerable.Empty<FacturaDetalleDTO>();
}
