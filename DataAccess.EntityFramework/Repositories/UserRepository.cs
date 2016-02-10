using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Dal = SocialNetwork.DataAccess.Interface.Entities;
using SocialNetwork.DataAccess.Interface.Repositories;
//using SocialNetwork.DataAccess.EntityFramework.Mappers;
using SocialNetwork.DataAccess.EntityFramework.Entities;
using AutoMapper;

namespace SocialNetwork.DataAccess.EntityFramework.Repositories
{
    public class UserRepository : MappedRepository<User, Dal.User>, IUserRepository
    {
        public UserRepository(DbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public void AddToFriends(int userId, int friendId)
        {
            var user = dataSet.Find(userId);
            if (user == null)
                throw new ArgumentException("User with the specified user Id does not exist.");
            var friend = dataSet.Find(friendId);
            if (friend == null)
                throw new ArgumentException("User with the specified friend Id does not exist.");

            if (user.Friends == null)
                user.Friends = new List<User>();
            user.Friends.Add(friend);
            Save();
        }

        public ICollection<Dal.User> GetAllFriends(int userId)
        {
            var user = dataSet.Find(userId);
            if (user == null)
                throw new ArgumentException("User with the specified user Id does not exist.");

            var friends = user.Friends.Where(friend => friend.Friends.Contains(user));

            return friends.Select(friend => mapper.Map<User, Dal.User>(friend)).ToList();
        }

        public Dal.FriendStatus GetFriendStatus(int userId, int friendId)
        {
            var user = dataSet.Find(userId);
            if (user == null)
                throw new ArgumentException("User with the specified user Id does not exist.");
            var friend = dataSet.Find(friendId);
            if (friend == null)
                throw new ArgumentException("User with the specified friend Id does not exist.");

            bool direct = user.Friends.Contains(friend);
            bool reverse = friend.Friends.Contains(user);

            if (direct && reverse)
                return Dal.FriendStatus.Friends;
            else if (direct)
                return Dal.FriendStatus.SentRequest;
            else if (reverse)
                return Dal.FriendStatus.ReceivedRequest;
            else
                return Dal.FriendStatus.None;
        }

        public void RemoveFromFriends(int userId, int friendId)
        {
            var user = dataSet.Find(userId);
            if (user == null)
                throw new ArgumentException("User with the specified user Id does not exist.");
            var friend = dataSet.Find(friendId);
            if (friend == null)
                throw new ArgumentException("User with the specified friend Id does not exist.");

            user.Friends.Remove(friend);
            Save();
        }
    }
}
