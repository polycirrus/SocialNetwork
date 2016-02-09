using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigationless = SocialNetwork.DataAccess.Interface.Entities.Navigationless;

namespace SocialNetwork.DataAccess.Interface.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public int FromId { get; set; }
        public virtual Navigationless.User From { get; set; }
        public int ToId { get; set; }
        public virtual Navigationless.User To { get; set; }

        public string Body { get; set; }
    }
}
