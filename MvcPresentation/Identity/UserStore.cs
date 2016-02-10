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
    public class UserStore : IUserStore<IdentityUser, int>, IUserRoleStore<IdentityUser, int>, 
        IUserPasswordStore<IdentityUser, int>, IUserEmailStore<IdentityUser, int>, 
        IUserLockoutStore<IdentityUser, int>, IUserTwoFactorStore<IdentityUser, int>
    {
        private IAccountService accountService => DependencyResolver.Current.GetService<IAccountService>();
        
        public void Dispose()
        {
        }

        #region User

        public Task CreateAsync(IdentityUser user)
        {
            //userService.Create(user.ToBlModel());
            //var createdUser = userService.FindByEmail(user.UserName);
            //user.Id = createdUser.Id;

            accountService.Create(user.ToBlModel());
            var account = accountService.FindByEmail(user.UserName);
            user.Id = account.Id;

            return Task.FromResult(true);
        }

        public Task DeleteAsync(IdentityUser user)
        {
            //userService.Delete(user.ToBlModel());

            accountService.Delete(user.ToBlModel());

            return Task.FromResult(true);
        }

        public Task<IdentityUser> FindByIdAsync(int userId)
        {
            //var user = userService.FindById(userId);
            var user = accountService.FindById(userId);

            return Task.FromResult(user?.ToIdentity());
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            //var user = userService.FindByEmail(userName);
            var user = accountService.FindByEmail(userName);

            return Task.FromResult(user?.ToIdentity());
        }

        #endregion

        #region Password

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            //var blUser = userService.FindById(user.Id);
            var blUser = accountService.FindById(user.Id);

            return Task.FromResult(blUser.Password);
        }

        //called by UserManager whenever new user with password is created,
        //so user might be new
        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            //var blUserModel = user.ToBlModel();
            //blUserModel.Password = passwordHash;
            //userService.CreateOrUpdate(blUserModel);

            user.PasswordHash = passwordHash;
            if (user.Id != default(int))
            {
                //not a new user
                accountService.SetPassword(user.Id, passwordHash);
            }

            return Task.FromResult(true);
        }

        #endregion

        #region Role

        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            accountService.AddToRole(user.ToBlModel(), roleName);

            return Task.FromResult(true);
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            var roles = accountService.GetRoles(user.ToBlModel());

            return Task.FromResult(roles);
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            var result = accountService.IsInRole(user.ToBlModel(), roleName);

            return Task.FromResult(result);
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            accountService.RemoveFromRole(user.ToBlModel(), roleName);

            return Task.FromResult(true);
        }

        #endregion

        #region Email

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            //var user = userService.FindByEmail(email);
            var user = accountService.FindByEmail(email);

            return Task.FromResult(user?.ToIdentity());
        }

        public Task<string> GetEmailAsync(IdentityUser user)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetEmailAsync(IdentityUser user, string email)
        {
            accountService.SetEmail(user.ToBlModel(), email);
            user.UserName = email;

            return Task.FromResult(true);
        }

        #endregion

        #region Stubs

        #region TwoFactor

        public Task SetTwoFactorEnabledAsync(IdentityUser user, bool enabled)
        {
            return Task.FromResult(true);
        }

        public Task<bool> GetTwoFactorEnabledAsync(IdentityUser user)
        {
            return Task.FromResult(false);
        }

        #endregion

        #region Lockout

        public Task<int> GetAccessFailedCountAsync(IdentityUser user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(IdentityUser user)
        {
            return Task.FromResult(false);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(IdentityUser user)
        {
            return Task.FromResult(DateTimeOffset.Now);
        }

        public Task<int> IncrementAccessFailedCountAsync(IdentityUser user)
        {
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(IdentityUser user)
        {
            return Task.FromResult(true);
        }

        public Task SetLockoutEnabledAsync(IdentityUser user, bool enabled)
        {
            return Task.FromResult(true);
        }

        public Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset lockoutEnd)
        {
            return Task.FromResult(true);
        }

        #endregion

        #region Email

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            return Task.FromResult(true);
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            return Task.FromResult(true);
        }

        #endregion

        #region Password

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            return Task.FromResult(true);
        }

        #endregion

        #region User

        public Task UpdateAsync(IdentityUser user)
        {
            return Task.FromResult(true);
        }

        #endregion

        #endregion
    }
}