using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.BL.Interface.Services;
using SocialNetwork.MvcPresentation.Mappers;
using Microsoft.AspNet.Identity;

namespace SocialNetwork.MvcPresentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("View", "Profile", new { id = User.Identity.GetUserId<int>() });
            else
                return RedirectToAction("Login", "Account");
        }
    }
}