using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.Repository.IRepositories
{
    public interface IGeneric<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Get(long id);

        void Insert(T entity);
        void Update(T entity);
        void UpdateInfo(int id, T entity);
        void Delete(int id);
        void Save();
    }
}
