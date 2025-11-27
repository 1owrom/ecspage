using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecspage.Application.Contracts
{
    public interface IImpuestoPolicy
    {
        decimal Rate { get; }
        decimal Calcular(decimal baseImponible);
    }
    public interface IDescuentoPolicy
    {
        decimal Aplicar(decimal monto);
    }
    public interface ITotalesCalculator
    {
        (decimal Subtotal, decimal Impuesto, decimal Total) Calcular(
            IEnumerable<(decimal Cantidad, decimal Precio)> items,
            bool precioIncluyeIGV
        );
    }
}

