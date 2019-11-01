using scheduler.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using scheduler.EF.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace scheduler.Repository.Repositories
{
    public class Generic<T> : IGeneric<T> where T : class
    {

        public SchedulerContext context;
        public Generic()
        {
            context = new SchedulerContext(); //new AppLoginDbContext();
        }
        protected virtual SchedulerContext Context
        {
            get { return context; }
            set { context = value; }
        }
        public Generic(SchedulerContext context)
        {
            Context = context;
        }
        //public AbstractRepository(AayushDBContext context)
        //{
        //    this.context = context;
        //    entities = context.Set<T>();
        //}
        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }
        public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }
        public T Get(long id)
        {
            return Context.Set<T>().Find(id);
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            //Context.Add(entity);
            context.Set<T>().Add(entity);
            //Context.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Entry(entity).State = EntityState.Modified;
            //Context.SaveChanges();
        }
        public virtual void UpdateInfo(int id, T updateEntity)
        {
            var entity = Get(id);
            context.Entry(entity).CurrentValues.SetValues(updateEntity);
            context.Entry(entity).State = EntityState.Modified;

            //context.Set<T>().Remove(entity);
            //context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            //if (id == null)
            //{
            //    throw new ArgumentNullException("entity");
            //}
            var entity = Get(id);
            context.Set<T>().Remove(entity);
            //Context.Remove(entity);
            //Context.SaveChanges();
        }
        public virtual void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("entity");
            }
        }
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);

        }


    }
}
