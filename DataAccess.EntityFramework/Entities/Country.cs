using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.EntityFramework.Entities
{
    public class Country : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<User> Inhabitants { get; set; }
    }
}
