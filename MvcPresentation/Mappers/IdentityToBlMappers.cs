using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.BL.Interface.Entities;
using SocialNetwork.MvcPresentation.Identity;

namespace SocialNetwork.MvcPresentation.Mappers
{
    public static class IdentityBlMappers
    {
        public static User ToBlModel(this IdentityUser identityUser)
        {
            return new User()
            {
                Id = identityUser.Id,
                Name = identityUser.UserName,
                Password = identityUser.PasswordHash
            };
        }

        public static IdentityUser ToIdentity(this User blUser)
        {
            return new IdentityUser()
            {
                Id = blUser.Id,
                UserName = blUser.Name,
            };
        }
    }
}