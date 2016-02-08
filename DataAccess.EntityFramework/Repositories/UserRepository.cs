using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dal = SocialNetwork.DataAccess.Interface.Entities;
using SocialNetwork.DataAccess.Interface.Repositories;
using SocialNetwork.DataAccess.EntityFramework.Mappers;
using SocialNetwork.DataAccess.EntityFramework.Entities;

namespace SocialNetwork.DataAccess.EntityFramework.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbContext context;

        public UserRepository(DbContext context)
        {
            this.context = context;
        }

        public void Add(Dal.User entity)
        {
            context.Set<User>().Add(entity.ToEfModel());
            context.SaveChanges();
        }

        public void AddOrUpdate(Dal.User entity)
        {
            var users = context.Set<User>();

            if (users.Any(user => user.Email == entity.Email))
                Update(entity);
            else
                Add(entity);
        }

        public void Delete(Dal.User entity)
        {
            context.Set<User>().Remove(entity.ToEfModel());
        }

        public Dal.User GetByEmail(string email)
        {
            return context.Set<User>()
                .SingleOrDefault(user => user.Email == email)
                ?.ToDalModel();
        }

        public Dal.User GetById(int id)
        {
            return context.Set<User>()
                .SingleOrDefault(user => user.Id == id)
                ?.ToDalModel();
        }

        public void Update(Dal.User entity)
        {
            context.Entry(entity.ToEfModel()).State = EntityState.Modified;
        }
    }
}
