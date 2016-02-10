using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.BL.Interface.Services;
using SocialNetwork.MvcPresentation.Models;
using SocialNetwork.MvcPresentation.Mappers;

namespace SocialNetwork.MvcPresentation.Helpers
{
    public static class ProfileHelpers
    {
        public static string GetEmail(int id)
        {
            var service = DependencyResolver.Current.GetService<IAccountService>();
            return service.FindById(id).Email;
        }

        public static List<CountryViewModel> GetAllCountries()
        {
            var service = DependencyResolver.Current.GetService<IProfileService>();
            return service.GetAllCountries().Select(blCountry => blCountry.ToViewModel()).ToList();
        }

        public static string GetCountryName(int id)
        {
            var service = DependencyResolver.Current.GetService<IProfileService>();
            return service.GetCountryById(id).Name;
        }
    }
}