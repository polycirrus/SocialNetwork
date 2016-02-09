using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BL.Interface.Entities;

namespace SocialNetwork.BL.Interface.Services
{
    public interface IUserService
    {
        void Create(User user);
        void CreateOrUpdate(User user);
        void Delete(User user);
        User FindById(int id);
        User FindByEmail(string email);
        void Update(User user);

        void Test();
    }
}
