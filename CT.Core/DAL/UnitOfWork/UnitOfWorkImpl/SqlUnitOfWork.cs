using CT.Core.DAL.Entity;
using CT.Core.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.DAL.UnitOfWork.UnitOfWorkImpl
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly CTEntities context;
        public IPersonRepository PersonRepository { get; set; }
        private bool disposed = false;

        private DbContextTransaction transaction;

        #region "Constructor"
        public SqlUnitOfWork(CTEntities context)
        {
            this.context = context;
        }
        #endregion

        #region "CommitOrFlushToDatabase"
        public void CommitOrFlush()
        {
            context.SaveChanges(); 
        }
        #endregion

        #region "BeginTransaction"
        public void BeginTransaction(bool autoDetectChangesEnabled = true)
        {
            this.context.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;
            this.transaction = this.context.Database.BeginTransaction();
        }
        #endregion

        #region "CommitTransaction"
        public void CommitTransaction(bool autoDetectChangesEnabled = true)
        {
            if (!autoDetectChangesEnabled)
            {
                this.context.Configuration.AutoDetectChangesEnabled = true;
            }
            this.transaction.Commit();
        }
        #endregion

        #region "RollbackTransaction"
        public void RollbackTransaction(bool autoDetectChangesEnabled = true)
        {
            if (!autoDetectChangesEnabled)
            {
                this.context.Configuration.AutoDetectChangesEnabled = true;
            }
            this.transaction.Rollback();
        }
        #endregion               

        #region "Dispose(bool disposing)"
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (context != null)
                        context.Dispose();
                    if (transaction != null)
                        transaction.Dispose();
                }
            }
            this.disposed = true;
        }
        #endregion

        #region "Dispose"
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
