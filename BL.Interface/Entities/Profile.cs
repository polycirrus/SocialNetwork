using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BL.Interface.Entities
{
    public class Profile
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Bio { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }
    }
}
