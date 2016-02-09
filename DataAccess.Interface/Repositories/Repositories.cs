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
    }

    public interface IRoleRepository : IRepository<Role>
    {
    }

    public interface ICountryRepository : IRepository<Country>
    {
    }

    public interface IMessageRepository : IRepository<Message>
    {
    }
}
