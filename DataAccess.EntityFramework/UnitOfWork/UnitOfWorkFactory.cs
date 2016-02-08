using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SocialNetwork.DataAccess.Interface;

namespace SocialNetwork.DataAccess.EntityFramework.UOW
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private DbContext context;

        public UnitOfWorkFactory(DbContext context)
        {
            this.context = context;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(context);
        }
    }
}
