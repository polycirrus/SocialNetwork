using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.BL.Interface.Entities;
using SocialNetwork.BL.Interface.Services;
using SocialNetwork.DataAccess.Interface;
using SocialNetwork.DataAccess.Interface.Repositories;
using Data = SocialNetwork.DataAccess.Interface.Entities;

namespace SocialNetwork.BL.Services
{
    public class AccountService : IAccountService
    {
        private IUserRepository userRepository;
        private IRoleRepository roleRepository;
        private IUnitOfWorkFactory uowFactory;
        private IMapper mapper;

        public AccountService(IUserRepository userRepository, IRoleRepository roleRepository, 
            IUnitOfWorkFactory uowFactory, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.uowFactory = uowFactory;
            this.mapper = mapper;
        }

        public void AddToRole(Account account, string roleName)
        {
            throw new NotImplementedException();
        }

        public void Create(Account newAccount)
        {
            ExecuteInUow(() =>
            {
                ValidateNewAccount(newAccount);

                var newUser = mapper.Map<Account, Data.User>(newAccount);
                userRepository.InsertOrUpdate(newUser);
            });
        }

        public void Delete(Account account)
        {
            ExecuteInUow(()
                => userRepository.Delete(account.Id));
        }

        public Account FindByEmail(string email)
        {
            var user = ExecuteInUow(()
                => userRepository.All.FirstOrDefault(dbUser => dbUser.Email == email));

            if (user == null)
                return null;
            return mapper.Map<Data.User, Account>(user);
        }

        public Account FindById(int id)
        {
            var user = ExecuteInUow(()
                => userRepository.Find(id));

            if (user == null)
                return null;
            return mapper.Map<Data.User, Account>(user);
        }

        public IList<string> GetRoles(Account account)
        {
            var roles = ExecuteInUow(() 
                => userRepository.Find(account.Id)?.Roles
                ?.Select(role => role.Name).ToList());

            return roles;
        }

        public bool IsInRole(Account account, string roleName)
        {
            return ExecuteInUow(() =>
            {
                var user = userRepository.Find(account.Id);

                if (user?.Roles == null)
                    return false;
                return user.Roles.Any(role => role.Name == roleName);
            });
        }

        public void RemoveFromRole(Account account, string roleName)
        {
            ExecuteInUow(() =>
            {
                var role = roleRepository.All.FirstOrDefault(dbRole => dbRole.Name == roleName);
                if (role == null)
                    throw new ArgumentException("Role with the specified name does not exist.");

                roleRepository.RemoveFromRole(role, account.Id);
            });
        }

        public void SetEmail(Account account, string email)
        {
            ExecuteInUow(() =>
            {
                ValidateUniqueEmail(email);

                var user = userRepository.Find(account.Id);
                if (user == null)
                    throw new ArgumentException("User with the specified Id does not exist.");

                user.Email = email;
                userRepository.InsertOrUpdate(user);
            });
        }

        public void SetPassword(int id, string password)
        {
            ExecuteInUow(() =>
            {
                var user = userRepository.Find(id);
                if (user == null)
                    throw new ArgumentException("User with the specified Id does not exist.");

                user.Password = password;
                userRepository.InsertOrUpdate(user);
            });
        }

        private void ValidateNewAccount(Account newAccount)
        {
            if (newAccount?.Email == null || newAccount?.Password == null)
                throw new ArgumentNullException();

            ValidateUniqueEmail(newAccount.Email);
        }

        private void ValidateUniqueEmail(string email)
        {
            if (userRepository.All.Where(user => user.Email == email).Count() > 0)
                throw new ArgumentException($"E-mail address \'{email}\" has been taken.");
        }

        #region UoW helpers

        private void ExecuteInUow(Action action)
        {
            using (var unitOfWork = uowFactory.Create())
            {
                action();

                try
                {
                    unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    throw new Exception("An error occured while commiting a transaction.", e);
                }
            }
        }

        private T ExecuteInUow<T>(Func<T> func)
        {
            T result;

            using (var unitOfWork = uowFactory.Create())
            {
                result = func();

                try
                {
                    unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    throw new Exception("An error occured while commiting a transaction.", e);
                }
            }

            return result;
        }

        #endregion
    }
}
