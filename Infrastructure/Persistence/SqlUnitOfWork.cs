using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

using ecspage.Infrastructure.Abstractions;

namespace ecspage.Infrastructure.Persistence
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly IConnectionFactory _factory;
        public DbConnection Connection { get; }
        public DbTransaction? Transaction { get; private set; }

        public SqlUnitOfWork(IConnectionFactory factory)
        {
            _factory = factory;
            Connection = _factory.Create();
        }
        public void Begin() => Transaction = Connection.BeginTransaction();
        public void Commit() { Transaction?.Commit(); Transaction = null; }
        public void Rollback() { Transaction?.Rollback(); Transaction = null; }
        public void Dispose() { Transaction?.Dispose(); Connection?.Dispose(); }
    }
}


