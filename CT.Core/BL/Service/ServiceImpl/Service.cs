using CT.Core.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.BL.Service.ServiceImpl
{
    public abstract class Service : IService
    {
        #region "Field"

        private bool disposed = false;
        private IUnitOfWork unitOfWork;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitOfWork;
            }
            set
            {
                unitOfWork = value;
            }
        }

        #endregion

        #region "Constructor"
        public Service()
        {
        }
        public Service(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region "Dispose(bool disposing)"

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (UnitOfWork != null)
                        UnitOfWork.Dispose();
                }
            }
            this.disposed = true;
        }

        #endregion

        #region "Dispose()"

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
