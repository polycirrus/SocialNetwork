using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SocialNetwork.BL.Interface.Services;
using SocialNetwork.MvcPresentation.Mappers;
using SocialNetwork.MvcPresentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.MvcPresentation.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public ProfileController()
        {
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public IProfileService ProfileService => DependencyResolver.Current.GetService<IProfileService>();

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (!(User.Identity.GetUserId<int>() == id || User.IsInRole("Admin")))
            {
                return RedirectToAction("Index", "Home");
            }

            var service = ProfileService;
            
            var profile = service.GetProfile(id);
            if (profile == null)
                return RedirectToAction("Index", "Home");

            var profileViewModel = profile.ToViewModel();

            var countries = service.GetAllCountries().Select(blCountry => blCountry.ToViewModel()).ToList();
            var countryList = countries.Select(country
                => new SelectListItem()
                {
                    Text = country.Name,
                    Value = country.Id.ToString()
                });
            ViewBag.CountryList = countryList;

            return View(profileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileViewModel model)
        {
            if (!(User.Identity.GetUserId<int>() == model.Id || User.IsInRole("Admin")))
            {
                return RedirectToAction("Index", "Home");
            }

            var service = ProfileService;

            if (ModelState.IsValid)
            {
                service.UpdateProfile(model.ToBlModel());
                return RedirectToAction("View", new { id = model.Id });
            }

            return View(model);
        }

        public ActionResult View(int id)
        {
            var service = ProfileService;

            var profile = service.GetProfile(id);
            if (profile == null)
                return RedirectToAction("Index", "Home");

            var selfId = User.Identity.GetUserId<int>();

            var friendStatus = service.GetFriendStatus(selfId, id);

            bool editOptions = selfId == id || User.IsInRole("Admin");
            bool self = selfId == id;

            ViewBag.FriendStatus = friendStatus;
            ViewBag.EditOptions = editOptions;
            ViewBag.Self = self;

            return View(profile.ToViewModel());
        }

        public ActionResult AddFriend(int id)
        {
            var selfId = User.Identity.GetUserId<int>();

            var service = ProfileService;
            service.AddToFriends(selfId, id);

            return RedirectToAction("View", new { id = id });
        }

        public ActionResult RemoveFriend(int id)
        {
            var selfId = User.Identity.GetUserId<int>();

            var service = ProfileService;
            service.RemoveFromFriends(selfId, id);

            return RedirectToAction("View", new { id = id });
        }
    }
}