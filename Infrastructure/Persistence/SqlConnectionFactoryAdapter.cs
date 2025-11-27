using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Common;

namespace ecspage.Infrastructure.Persistence
{
    public class SqlConnectionFactoryAdapter : ecspage.Infrastructure.Abstractions.IConnectionFactory
    {
        public DbConnection Create()
        {
            var c = conexion.GetConnection();
            if (c.State != System.Data.ConnectionState.Open)
                c.Open();
            return c;
        }
    }
}


