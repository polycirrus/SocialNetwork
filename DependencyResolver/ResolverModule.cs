using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Ninject.Modules;
using Ninject.Web.Common;
using SocialNetwork.BL.Interface.Services;
using SocialNetwork.BL.Services;
using SocialNetwork.DataAccess.EntityFramework;
using SocialNetwork.DataAccess.EntityFramework.Repositories;
using SocialNetwork.DataAccess.EntityFramework.UOW;
using SocialNetwork.DataAccess.Interface.Repositories;
using SocialNetwork.DataAccess.Interface;

namespace SocialNetwork.DependencyResolver
{
    public class ResolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<Context>().InRequestScope();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUnitOfWorkFactory>().To<UnitOfWorkFactory>();
            //Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            Bind<IUserService>().To<UserService>();
        }
    }
}
