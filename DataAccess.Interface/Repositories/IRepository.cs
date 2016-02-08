using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Interface.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(T entity);
        T GetById(int id);
        void Update(T entity);
    }
}
