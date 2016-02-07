﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace SocialNetwork.MvcPresentation.Identity
{
    public class IdentityRole : IRole<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}