using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace SocialNetwork.MvcPresentation.Identity
{
    public class RoleStore : IRoleStore<IdentityRole, int>
    {
        public Task CreateAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> FindByIdAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}