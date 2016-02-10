using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SocialNetwork.MvcPresentation.Helpers;

namespace SocialNetwork.MvcPresentation.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Bio")]
        public string Bio { get; set; }
        
        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        public string FullName
        {
            get
            {
                if (FirstName != null)
                {
                    if (LastName != null)
                        return $"{FirstName} {LastName}";
                    return FirstName;
                }
                if (LastName != null)
                    return LastName;

                return ProfileHelpers.GetEmail(Id); 
            }
        }
    }

    public class CountryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}