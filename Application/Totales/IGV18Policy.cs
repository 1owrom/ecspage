using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ecspage.Application.Contracts;

namespace ecspage.Application.Totales
{
    public class IGV18Policy : IImpuestoPolicy
    {
        public decimal Rate => 0.18m;
        public decimal Calcular(decimal baseImponible)
            => decimal.Round(baseImponible * Rate, 2);
    }
}