using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TastyFoods.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CanonicalUrl = SEO.Home_Index_Canonical();
            ViewBag.Title = SEO.Home_Index_Title();
            ViewBag.Description = SEO.Home_Index_Description();
            ViewBag.Keywords = SEO.Home_Index_Keywords();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.CanonicalUrl = SEO.Home_About_Canonical();
            ViewBag.Title = SEO.Home_About_Title();
            ViewBag.Description = SEO.Home_About_Description();
            ViewBag.Keywords = SEO.Home_About_Keywords();

            return View();
        }

        public ActionResult Gallery()
        {
            ViewBag.CanonicalUrl = SEO.Home_Gallery_Canonical();
            ViewBag.Title = SEO.Home_Gallery_Title();
            ViewBag.Description = SEO.Home_Gallery_Description();
            ViewBag.Keywords = SEO.Home_Gallery_Keywords();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.CanonicalUrl = SEO.Home_Contact_Canonical();
            ViewBag.Title = SEO.Home_Contact_Title();
            ViewBag.Description = SEO.Home_Contact_Description();
            ViewBag.Keywords = SEO.Home_Contact_Keywords();

            return View();
        }

        public ActionResult PageNotFound()
        {
            ViewBag.IsNoIndexNoFollow = "YES";

            ViewBag.CanonicalUrl = SEO.Home_PageNotFound_Canonical();
            ViewBag.Title = SEO.Home_PageNotFound_Title();
            ViewBag.Description = SEO.Home_PageNotFound_Description();
            ViewBag.Keywords = SEO.Home_PageNotFound_Keywords();

            return View();
        }

    }
}
