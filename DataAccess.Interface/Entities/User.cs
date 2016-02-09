using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigationless = SocialNetwork.DataAccess.Interface.Entities.Navigationless;

namespace SocialNetwork.DataAccess.Interface.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Navigationless.Role> Roles { get; set; }

        public virtual ICollection<Navigationless.User> Friends { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Bio { get; set; }

        public int? CountryId { get; set; }
        public virtual Navigationless.Country Country { get; set; }

        public virtual ICollection<Navigationless.Message> MessagesFrom { get; set; }
        public virtual ICollection<Navigationless.Message> MessagesTo { get; set; }
    }
}
