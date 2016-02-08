using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DataAccess.Interface;
using System.Data.Entity;

namespace SocialNetwork.DataAccess.EntityFramework.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext context;
        private DbContextTransaction transaction;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
            transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
            context.SaveChanges();
            transaction.Commit();
        }

        public void Dispose()
        {
            transaction.Dispose();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }
    }
}
