using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.MvcPresentation.Models
{
    public class ProfileViewModel
    {
        [Display(Name="Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Bio")]
        public string Bio { get; set; }
    }
}