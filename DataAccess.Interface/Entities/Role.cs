using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigationless = SocialNetwork.DataAccess.Interface.Entities.Navigationless;

namespace SocialNetwork.DataAccess.Interface.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Navigationless.User> Users { get; set; }
    }
}
