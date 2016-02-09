using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ninject.Modules;
using Ninject.Web.Common;
using SocialNetwork.BL.Interface.Services;
using SocialNetwork.BL.Services;
using SocialNetwork.DataAccess.EntityFramework;
using SocialNetwork.DataAccess.EntityFramework.Repositories;
using SocialNetwork.DataAccess.EntityFramework.UOW;
using SocialNetwork.DataAccess.Interface;
using SocialNetwork.DataAccess.Interface.Repositories;

namespace SocialNetwork.Infrastructure
{
    public class ResolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<Context>().InRequestScope();
            Bind<IUnitOfWorkFactory>().To<UnitOfWorkFactory>();

            Bind<IUserRepository>().To<UserRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<ICountryRepository>().To<CountryRepository>();
            Bind<IMessageRepository>().To<MessageRepository>();

            //Bind<IUserService>().To<UserService>();
            Bind<IAccountService>().To<AccountService>().InSingletonScope();

            Bind<IMapper>().ToConstant(AutoMapperConfiguration.Mapper);
        }
    }
}
