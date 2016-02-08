using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DataAccess.Interface.Entities;

namespace SocialNetwork.DataAccess.Interface.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void AddOrUpdate(User entity);
        User GetByEmail(string email);
    }
}
