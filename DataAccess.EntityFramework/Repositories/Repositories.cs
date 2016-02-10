using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.DataAccess.Interface.Repositories;
using Dal = SocialNetwork.DataAccess.Interface.Entities;
using SocialNetwork.DataAccess.EntityFramework.Entities;

namespace SocialNetwork.DataAccess.EntityFramework.Repositories
{
    public class CountryRepository : MappedRepository<Country, Dal.Country>, ICountryRepository
    {
        public CountryRepository(DbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }
    }

    public class MessageRepository : MappedRepository<Message, Dal.Message>, IMessageRepository
    {
        public MessageRepository(DbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }
    }
}
