using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Interface.Repositories
{
    public interface IRepository<T>
    {
        //void Add(T entity);
        //void Delete(T entity);
        //T GetById(int id);
        //void Update(T entity);

        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Find(int id);
        void InsertOrUpdate(T model, string user);
        void Delete(int id);
        void Save();
    }
}
