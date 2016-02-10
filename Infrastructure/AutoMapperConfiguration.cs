using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ef = SocialNetwork.DataAccess.EntityFramework.Entities;
using Data = SocialNetwork.DataAccess.Interface.Entities;
using BLL = SocialNetwork.BL.Interface.Entities;

namespace SocialNetwork.Infrastructure
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration Configuration { get; private set; }
        public static IMapper Mapper { get; private set; }

        static AutoMapperConfiguration()
        {
            Configuration = new MapperConfiguration((configuration) =>
            {
                //Entity Framework -> DAL interface
                configuration.CreateMap<Ef.User, Data.User>();
                configuration.CreateMap<Ef.Role, Data.Role>();
                configuration.CreateMap<Ef.Country, Data.Country>();
                configuration.CreateMap<Ef.Message, Data.Message>();

                configuration.CreateMap<Ef.User, Data.Navigationless.User>();
                configuration.CreateMap<Ef.Role, Data.Navigationless.Role>();
                configuration.CreateMap<Ef.Country, Data.Navigationless.Country>();
                configuration.CreateMap<Ef.Message, Data.Navigationless.Message>();

                //DAL interface -> EF
                configuration.CreateMap<Data.User, Ef.User>();
                configuration.CreateMap<Data.Role, Ef.Role>();
                configuration.CreateMap<Data.Country, Ef.Country>();
                configuration.CreateMap<Data.Message, Ef.Message>();

                configuration.CreateMap<Data.Navigationless.User, Ef.User>();
                configuration.CreateMap<Data.Navigationless.Role, Ef.Role>();
                configuration.CreateMap<Data.Navigationless.Country, Ef.Country>();
                configuration.CreateMap<Data.Navigationless.Message, Ef.Message>();

                //DAL
                configuration.CreateMap<Data.Navigationless.Country, Data.Country>();

                //BLL -> DAL
                configuration.CreateMap<BLL.Account, Data.User>();
                configuration.CreateMap<BLL.Profile, Data.User>();
                configuration.CreateMap<BLL.Country, Data.Country>();
                configuration.CreateMap<BLL.Country, Data.Navigationless.Country>();

                //DAL -> BLL
                configuration.CreateMap<Data.User, BLL.Account>();
                configuration.CreateMap<Data.User, BLL.Profile>();
                configuration.CreateMap<Data.Navigationless.User, BLL.Profile>();
                configuration.CreateMap<Data.Country, BLL.Country>();
                configuration.CreateMap<Data.Navigationless.Country, BLL.Country>();
                configuration.CreateMap<Data.FriendStatus, BLL.FriendStatus>();
            });

            Mapper = Configuration.CreateMapper();
        }
    }

    //public class MapperContainer : IMapperContainer
    //{
    //    private static IMapper mapper;

    //    static MapperContainer()
    //    {
    //        var configuration = new MapperConfiguration(cfg =>
    //        {
    //            cfg.CreateMap<Ef.User, Data.User>();
    //            cfg.CreateMap<Ef.Role, Data.Role>();
    //            cfg.CreateMap<Ef.Country, Data.Country>();
    //            cfg.CreateMap<Ef.Message, Data.Message>();
    //        });
    //        mapper = configuration.CreateMapper();
    //    }

    //    public IMapper Mapper => mapper;
    //}

    //public interface IMapperContainer
    //{
    //    IMapper Mapper { get; }
    //}
}
