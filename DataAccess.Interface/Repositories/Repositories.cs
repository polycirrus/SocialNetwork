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
        void AddToFriends(int userId, int friendId);
        void RemoveFromFriends(int userId, int friendId);
        FriendStatus GetFriendStatus(int userId, int friendId);
        ICollection<User> GetAllFriends(int userId);
    }

    public interface IRoleRepository : IRepository<Role>
    {
        void RemoveFromRole(Role role, int userId);
    }

    public interface ICountryRepository : IRepository<Country>
    {
    }

    public interface IMessageRepository : IRepository<Message>
    {
    }
}
