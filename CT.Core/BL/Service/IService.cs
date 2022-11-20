using CT.Core.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.BL.Service
{
    public interface IService : IDisposable
    {
        IUnitOfWork UnitOfWork { get; set; }
    }
}
