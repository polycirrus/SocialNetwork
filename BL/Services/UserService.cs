using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BL.Interface.Services;
using SocialNetwork.BL.Interface.Entities;
using SocialNetwork.DataAccess.Interface.Repositories;
using SocialNetwork.DataAccess.Interface;
using SocialNetwork.BL.Mappers;

namespace SocialNetwork.BL.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private IUnitOfWorkFactory unitOfWorkFactory;

        public UserService(IUserRepository userRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.userRepository = userRepository;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public void CreateOrUpdate(User user)
        {
            ExecuteInUow(() =>
            {
                userRepository.AddOrUpdate(user.ToDataModel());
            });
        }

        public void Create(User user)
        {
            ExecuteInUow(() =>
            {
                userRepository.Add(user.ToDataModel());
            });
        }

        public void Delete(User user)
        {
            ExecuteInUow(() =>
            {
                userRepository.Delete(user.ToDataModel());
            });
        }

        public User FindById(int id)
        {
            return ExecuteInUow(() =>
            {
                return userRepository.GetById(id)?.ToBlModel();
            });
        }

        public User FindByEmail(string email)
        {
            return ExecuteInUow(() =>
            {
                return userRepository.GetByEmail(email)?.ToBlModel();
            });
        }

        public void Update(User user)
        {
            ExecuteInUow(() =>
            {
                userRepository.Update(user.ToDataModel());
            });
        }

        private void ExecuteInUow(Action action)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                action();
                unitOfWork.Commit();
            }
        }

        private T ExecuteInUow<T>(Func<T> func)
        {
            T result;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                result = func();
                unitOfWork.Commit();
            }

            return result;
        }
    }
}
