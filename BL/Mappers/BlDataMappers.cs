using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data = SocialNetwork.DataAccess.Interface.Entities;
using SocialNetwork.BL.Interface.Entities;

namespace SocialNetwork.BL.Mappers
{
    public static class BlDataMappers
    {
        public static Data.User ToDataModel(this User user)
        {
            return new Data.User()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Name,
                Password = user.Password
            };
        }

        public static User ToBlModel(this Data.User user)
        {
            return new User()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Name,
                Password = user.Password
            };
        }
    }
}
