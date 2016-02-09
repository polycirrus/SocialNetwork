using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SocialNetwork.DataAccess.Interface.Repositories;
using SocialNetwork.DataAccess.EntityFramework.Entities;

namespace SocialNetwork.DataAccess.EntityFramework.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class, IEntity
    {
        private DbContext context;
        private DbSet<T> dataSet;

        public Repository(DbContext context)
        {
            this.context = context;
            dataSet = context.Set<T>();
        }

        public IQueryable<T> All
            => dataSet; 

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dataSet;
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return query;
        }

        public void Delete(int id)
        {
            var toDelete = dataSet.Find(id);
            dataSet.Remove(toDelete);
        }

        public T Find(int id)
        {
            return dataSet.Find(id);
        }

        public void InsertOrUpdate(T model, string user)
        {
            if (model.Id == default(int))
            {
                // New entity
                dataSet.Add(model);
                return;
            }
            // Existing entity
            context.Entry(model).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
