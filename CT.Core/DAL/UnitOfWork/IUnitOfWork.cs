using CT.Core.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository PersonRepository { get; set; }
        void BeginTransaction(bool autoDetectChangesEnabled = true);
        void CommitTransaction(bool autoDetectChangesEnabled = true);
        void RollbackTransaction(bool autoDetectChangesEnabled = true);
        void CommitOrFlush();
    }
}
