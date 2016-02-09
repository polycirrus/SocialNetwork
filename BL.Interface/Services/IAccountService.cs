using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BL.Interface.Entities;

namespace SocialNetwork.BL.Interface.Services
{
    public interface IAccountService
    {
        void Create(Account account);
        void Delete(Account account);
        Account FindById(int id);

        void SetPassword(int id, string password);

        void AddToRole(Account account, string roleName);
        IList<string> GetRoles(Account account);
        bool IsInRole(Account account, string roleName);
        void RemoveFromRole(Account account, string roleName);

        Account FindByEmail(string email);
        void SetEmail(Account account, string email);
    }
}
