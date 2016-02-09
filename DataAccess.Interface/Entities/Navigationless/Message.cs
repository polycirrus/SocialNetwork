using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Interface.Entities.Navigationless
{
    public class Message
    {
        public int Id { get; set; }

        public int FromId { get; set; }
        public int ToId { get; set; }

        public string Body { get; set; }
    }
}
