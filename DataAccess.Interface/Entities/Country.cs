﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigationless = SocialNetwork.DataAccess.Interface.Entities.Navigationless;

namespace SocialNetwork.DataAccess.Interface.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Navigationless.User> Inhabitants { get; set; }
    }
}
