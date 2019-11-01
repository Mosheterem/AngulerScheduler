using scheduler.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.Repository.Infrastructure
{
   public interface IUnitOfWork:IDisposable
    {
        IAppontmentRepository AppontmentRepository { get; }

        void Commit();
        void Rollback();
    }
}
