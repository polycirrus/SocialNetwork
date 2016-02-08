using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SocialNetwork.BL.Interface.Services;
using SocialNetwork.MvcPresentation.Mappers;

namespace SocialNetwork.MvcPresentation.Identity
{
    public class UserStore : IUserStore<IdentityUser, int>, IUserRoleStore<IdentityUser, int>, IUserPasswordStore<IdentityUser, int>, IUserEmailStore<IdentityUser, int>
    {
        private IUserService userService;

        public UserStore()
        {
            userService = System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>();
        }

        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(IdentityUser user)
        {
            userService.Create(user.ToBlModel());

            return Task.FromResult(true);
        }

        public Task DeleteAsync(IdentityUser user)
        {
            userService.Delete(user.ToBlModel());

            return Task.FromResult(true);
        }

        public void Dispose()
        {
        }

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            var user = userService.FindByEmail(email);

            return Task.FromResult(user?.ToIdentity());
        }

        public Task<IdentityUser> FindByIdAsync(int userId)
        {
            var user = userService.FindById(userId);

            return Task.FromResult(user?.ToIdentity());
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            var user = userService.FindByEmail(userName);

            return Task.FromResult(user?.ToIdentity());
        }

        public Task<string> GetEmailAsync(IdentityUser user)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            return Task.FromResult(true);
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            var blUser = userService.FindById(user.Id);

            return Task.FromResult(blUser.Password);
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(IdentityUser user, string email)
        {
            return Task.FromResult(true);
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            return Task.FromResult(true);
        }

        //called by UserManager whenever new user with password is created,
        //so user might be new
        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            //var blUserModel = user.ToBlModel();
            //blUserModel.Password = passwordHash;
            //userService.CreateOrUpdate(blUserModel);

            user.PasswordHash = passwordHash;

            return Task.FromResult(true);
        }

        public Task UpdateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }
    }
}