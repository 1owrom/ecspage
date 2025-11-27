using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ecspage.Application.Contracts;

namespace ecspage.Application.Totales
{
    public class SinDescuentoPolicy : IDescuentoPolicy
    {
        public decimal Aplicar(decimal monto) => monto;
    }
}