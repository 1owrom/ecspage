using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace ecspage.Infrastructure.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        DbConnection Connection { get; }
        DbTransaction? Transaction { get; }
        void Begin();
        void Commit();
        void Rollback();
    }
}

