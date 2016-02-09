using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.DataAccess.Interface.Repositories;
using Dal = SocialNetwork.DataAccess.Interface.Entities;
using SocialNetwork.DataAccess.EntityFramework.Entities;

namespace SocialNetwork.DataAccess.EntityFramework.Repositories
{
    public class RoleRepository : MappedRepository<Role, Dal.Role>, IRoleRepository
    {
        public RoleRepository(DbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public void RemoveFromRole(Dal.Role role, int userId)
        {
            var dbRole = dataSet.Find(role.Id);
            if (dbRole == null)
                throw new ArgumentException("Role does not exist.");

            var user = context.Set<User>().Find(userId);
            if (user == null)
                throw new ArgumentException("User with the specified Id does not exist.");

            dbRole.Users?.Remove(user);
            Save();
        }
    }
}
