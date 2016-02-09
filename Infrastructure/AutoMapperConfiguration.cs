using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ef = SocialNetwork.DataAccess.EntityFramework.Entities;
using Data = SocialNetwork.DataAccess.Interface.Entities;

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
                configuration.CreateMap<Ef.User, Data.User>();
                configuration.CreateMap<Ef.Role, Data.Role>();
                configuration.CreateMap<Ef.Country, Data.Country>();
                configuration.CreateMap<Ef.Message, Data.Message>();

                configuration.CreateMap<Ef.User, Data.Navigationless.User>();
                configuration.CreateMap<Ef.Role, Data.Navigationless.Role>();
                configuration.CreateMap<Ef.Country, Data.Navigationless.Country>();
                configuration.CreateMap<Ef.Message, Data.Navigationless.Message>();

                //configuration.CreateMap<Data.User, Ef.User>();
                //configuration.CreateMap<Data.Role, Ef.Role>();
                //configuration.CreateMap<Data.Country, Ef.Country>();
                //configuration.CreateMap<Data.Message, Ef.Message>();
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
