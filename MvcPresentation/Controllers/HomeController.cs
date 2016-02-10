using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.BL.Interface.Services;

namespace SocialNetwork.MvcPresentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var profileService = DependencyResolver.Current.GetService<IProfileService>();
            profileService.GetAllCountries();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}