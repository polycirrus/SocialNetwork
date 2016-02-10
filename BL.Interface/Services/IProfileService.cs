using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BL.Interface.Entities;

namespace SocialNetwork.BL.Interface.Services
{
    public interface IProfileService
    {
        Profile GetProfile(int id);
        void UpdateProfile(Profile updatedProfile);

        ICollection<Profile> SearchByName(string searchString);
        ICollection<Profile> SearchByCountry(string countryName);
        ICollection<Profile> SearchByBio(string searchString);

        void AddToFriends(int userId, int friendId);
        void RemoveFromFriends(int userId, int friendId);
        ICollection<Profile> ReceivedFriendRequests(int id);
        ICollection<Profile> SentFriendRequest(int id);
        ICollection<Profile> Friends(int id);
        FriendStatus GetFriendStatus(int userId, int friendId);

        Country GetCountryById(int id);
        Country GetCountryByName(string name);
        ICollection<Country> GetAllCountries();
    }
}
