using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.BL.Interface.Entities;
using SocialNetwork.MvcPresentation.Models;

namespace SocialNetwork.MvcPresentation.Mappers
{
    public static class ViewBlMappers
    {
        public static ProfileViewModel ToViewModel(this Profile source)
        {
            return new ProfileViewModel()
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Bio = source.Bio,
                DateOfBirth = source.DateOfBirth,
                CountryId = source.Country?.Id
            };
        }

        public static CountryViewModel ToViewModel(this Country source)
        {
            return new CountryViewModel()
            {
                Id = source.Id,
                Name = source.Name
            };
        }

        public static Profile ToBlModel(this ProfileViewModel source)
        {
            return new Profile()
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Bio = source.Bio,
                DateOfBirth = source.DateOfBirth,
                CountryId = source.CountryId
            };
        }
    }
}