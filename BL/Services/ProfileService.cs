using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BL.Interface.Entities;
using SocialNetwork.BL.Interface.Services;
using SocialNetwork.DataAccess.Interface.Repositories;
using SocialNetwork.DataAccess.Interface;
using Data = SocialNetwork.DataAccess.Interface.Entities;

namespace SocialNetwork.BL.Services
{
    public class ProfileService : IProfileService
    {
        private IUserRepository userRepository;
        private ICountryRepository countryRepository;
        private IUnitOfWorkFactory uowFactory;
        private AutoMapper.IMapper mapper;

        public ProfileService(IUserRepository userRepository, ICountryRepository countryRepository,
            IUnitOfWorkFactory uowFactory, AutoMapper.IMapper mapper)
        {
            this.userRepository = userRepository;
            this.countryRepository = countryRepository;
            this.uowFactory = uowFactory;
            this.mapper = mapper;
        }

        #region Profile

        public Profile GetProfile(int id)
        {
            var user = ExecuteInUow(()
                => userRepository.Find(id));

            if (user == null)
                return null;
            return mapper.Map<Data.User, Profile>(user);
        }

        public void UpdateProfile(Profile updatedProfile)
        {
            ExecuteInUow(() =>
            {
                var dbUser = userRepository.Find(updatedProfile.Id);
                if (dbUser == null)
                    throw new ArgumentException("User with the specified Id does not exist.");

                MergeProfileIntoUser(dbUser, updatedProfile);

                userRepository.InsertOrUpdate(dbUser);
            });
        }

        #endregion

        #region Friends

        public void AddToFriends(int userId, int friendId)
        {
            ExecuteInUow(()
                => userRepository.AddToFriends(userId, friendId));
        }

        public ICollection<Profile> Friends(int id)
        {
            return ExecuteInUow(()
                => userRepository.GetAllFriends(id).Select(user => mapper.Map<Data.User, Profile>(user)).ToList());
        }

        public ICollection<Profile> ReceivedFriendRequests(int id)
        {
            return ExecuteInUow(()
                => userRepository.All.Where(user => user.Friends.Any(friend => friend.Id == id))
                .Select(user => mapper.Map<Data.User, Profile>(user)).ToList());
        }

        public void RemoveFromFriends(int userId, int friendId)
        {
            ExecuteInUow(()
                => userRepository.RemoveFromFriends(userId, friendId));
        }

        public ICollection<Profile> SentFriendRequest(int id)
        {
            return ExecuteInUow(()
                => userRepository.Find(id).Friends
                .Select(user => mapper.Map<Data.Navigationless.User, Profile>(user)).ToList());
        }

        public FriendStatus GetFriendStatus(int userId, int friendId)
        {
            return ExecuteInUow(()
                => mapper.Map<Data.FriendStatus, FriendStatus>(userRepository.GetFriendStatus(userId, friendId)));
        }

        #endregion

        #region Search

        public ICollection<Profile> SearchByBio(string searchString)
        {
            return ExecuteInUow(()
                => userRepository.All
                .Where(user => user.Bio != null && user.Bio.Contains(searchString))
                .Select(user => mapper.Map<Data.User, Profile>(user)).ToList());
        }

        public ICollection<Profile> SearchByCountry(string countryName)
        {
            return ExecuteInUow(()
                => userRepository.All
                .Where(user => user.Country != null && user.Country.Name == countryName)
                .Select(user => mapper.Map<Data.User, Profile>(user)).ToList());
        }

        public ICollection<Profile> SearchByName(string searchString)
        {
            return ExecuteInUow(()
                => userRepository.All
                .Where(user => (user.FirstName != null && user.FirstName.Contains(searchString))
                    || (user.LastName != null && user.LastName.Contains(searchString)) 
                    || (user.FirstName != null && user.LastName != null 
                    && (user.FirstName + " " + user.LastName).Contains(searchString)))
                .Select(user => mapper.Map<Data.User, Profile>(user)).ToList());
        }

        #endregion

        #region Country

        public Country GetCountryById(int id)
        {
            var country = ExecuteInUow(()
                => countryRepository.Find(id));

            if (country == null)
                return null;
            return mapper.Map<Data.Country, Country>(country);
        }

        public Country GetCountryByName(string name)
        {
            var country = ExecuteInUow(()
                => countryRepository.All.FirstOrDefault(dbCountry => dbCountry.Name == name));

            if (country == null)
                return null;
            return mapper.Map<Data.Country, Country>(country);
        }

        public ICollection<Country> GetAllCountries()
        {
            return ExecuteInUow(() =>
            {
                var countries = countryRepository.All.ToList();
                var dalCountries = countries.Select(dbCountry => mapper.Map<Data.Country, Country>(dbCountry));
                return dalCountries.ToList();
            });
        }

        #endregion

        #region Private

        private void ExecuteInUow(Action action)
        {
            using (var unitOfWork = uowFactory.Create())
            {
                action();

                try
                {
                    unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    throw new Exception("An error occured while commiting a transaction.", e);
                }
            }
        }

        private T ExecuteInUow<T>(Func<T> func)
        {
            T result;

            using (var unitOfWork = uowFactory.Create())
            {
                result = func();

                try
                {
                    unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    throw new Exception("An error occured while commiting a transaction.", e);
                }
            }

            return result;
        }

        private void MergeProfileIntoUser(Data.User user, Profile profile)
        {
            user.Bio = !string.IsNullOrWhiteSpace(profile.Bio) ? profile.Bio : null;
            user.DateOfBirth = profile.DateOfBirth;
            user.FirstName = !string.IsNullOrWhiteSpace(profile.FirstName) ? profile.FirstName : null;
            user.LastName = !string.IsNullOrWhiteSpace(profile.LastName) ? profile.LastName : null;

            if (profile.Country != null)
            {
                user.Country = mapper.Map<Country, Data.Navigationless.Country>(profile.Country);
                user.CountryId = profile.Country.Id;
            }
            else if (profile.CountryId != null)
            {
                user.CountryId = profile.CountryId;
                user.Country = mapper.Map<Data.Country, Data.Navigationless.Country>(countryRepository.Find(profile.CountryId.Value));
            }
            else
            {
                user.CountryId = null;
                user.Country = null;
            }
        }

        #endregion
    }
}
