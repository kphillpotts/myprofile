using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheCubby.Controllers
{
    public class HomeController : Controller
    {
         public ActionResult Index()
        {
            ViewBag.Admin = false;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Admin = false;
            return View();
        }

        public ActionResult Blog()
        {
            ViewBag.Admin = false;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Admin = false;
            return View();
        }

        public ActionResult Admin()
        {
            ViewBag.Admin = true;
            return View();
        }
    }
}