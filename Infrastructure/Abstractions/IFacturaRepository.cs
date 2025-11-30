using ecspage.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecspage.Infrastructure.Abstractions
{
    public interface IFacturaRepository
    {
        int InsertarFactura(int clienteId, DateTime fecha, string estado,
                            decimal subtotal, decimal impuesto, decimal total, DbTransaction tx);

        void InsertarDetalle(int facturaId, int productoId, decimal cantidad, decimal precio, DbTransaction tx);

        void Anular(int facturaId);

        void ActualizarFactura(int idFactura, int nuevoClienteId, string nuevoEstado);


        FacturaDTO? Obtener(int facturaId);

        List<FacturaDTO> Listar(FiltroFacturas filtro);
    }
}
