using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.EntityFramework.Entities
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public int FromId { get; set; }
        public virtual User From { get; set; }
        [Required]
        public int ToId { get; set; }
        public virtual User To { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
