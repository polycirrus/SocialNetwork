using SocialNetwork.DataAccess.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.EntityFramework
{
    public class Context : DbContext
    {
        public Context() : base("name=SocialNetworkMsSqlDatabase")
        {
        }

        DbSet<User> Users { get; set; }
    }
}
