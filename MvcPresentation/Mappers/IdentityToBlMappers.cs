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
        public static Account ToBlModel(this IdentityUser identityUser)
        {
            return new Account()
            {
                Id = identityUser.Id,
                Email = identityUser.UserName,
                Password = identityUser.PasswordHash
            };
        }

        public static IdentityUser ToIdentity(this Account account)
        {
            return new IdentityUser()
            {
                Id = account.Id,
                UserName = account.Email,
                PasswordHash = account.Password
            };
        }
    }
}