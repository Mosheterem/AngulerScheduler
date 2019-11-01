using System;
using System.Collections.Generic;
using System.Text;
using scheduler.EF.Model;
using scheduler.Repository.IRepositories;
using scheduler.Repository.Repositories;

namespace scheduler.Repository.Infrastructure
{
    public class UnitOfWork :IUnitOfWork
    {
        public SchedulerContext db;


        public UnitOfWork()
        {
            this.db = new SchedulerContext();

        }
       
        private IAppontmentRepository _appontmentRepository;

        ////Getter Method For Repositories
        public IAppontmentRepository AppontmentRepository
        {
            get
            {
                return _appontmentRepository = _appontmentRepository ?? new AppontmentRepository(db);
            }
        }

      // public IAppontmentRepository AppontmentRepository => throw new NotImplementedException();

        public void Commit()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();

        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
