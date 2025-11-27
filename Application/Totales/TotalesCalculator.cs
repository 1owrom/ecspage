using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecspage.Application.Contracts;

namespace ecspage.Application.Totales
{
    public class TotalesCalculator : ITotalesCalculator
    {
        private readonly IImpuestoPolicy _impuesto;
        private readonly IDescuentoPolicy _descuento;

        public TotalesCalculator(IImpuestoPolicy impuesto, IDescuentoPolicy descuento)
        {
            _impuesto = impuesto;
            _descuento = descuento;
        }

        public (decimal Subtotal, decimal Impuesto, decimal Total) Calcular(
            IEnumerable<(decimal Cantidad, decimal Precio)> items,
            bool precioIncluyeIGV)
        {
            var suma = items.Sum(i => i.Cantidad * i.Precio);

            if (precioIncluyeIGV)
            {
                var baseImp = decimal.Round(suma / (1 + _impuesto.Rate), 2);
                baseImp = _descuento.Aplicar(baseImp);
                var igv = _impuesto.Calcular(baseImp);
                return (baseImp, igv, baseImp + igv);
            }
            else
            {
                var baseImp = _descuento.Aplicar(suma);
                var igv = _impuesto.Calcular(baseImp);
                return (baseImp, igv, baseImp + igv);
            }
        }
    }
}