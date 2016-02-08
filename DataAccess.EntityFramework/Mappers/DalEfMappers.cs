using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal = SocialNetwork.DataAccess.Interface.Entities;
using SocialNetwork.DataAccess.EntityFramework.Entities;

namespace SocialNetwork.DataAccess.EntityFramework.Mappers
{
    public static class DalEfMappers
    {
        public static Dal.User ToDalModel(this User user)
        {
            return new Dal.User()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Name,
                Password = user.Password
            };
        }

        public static User ToEfModel(this Dal.User user)
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
