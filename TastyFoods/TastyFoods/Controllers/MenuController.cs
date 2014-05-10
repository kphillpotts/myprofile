using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TastyFoods.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/

        public ActionResult Index()
        {
            ViewBag.CanonicalUrl = SEO.Menu_Index_Canonical();
            ViewBag.Title = SEO.Menu_Index_Title();
            ViewBag.Description = SEO.Menu_Index_Description();
            ViewBag.Keywords = SEO.Menu_Index_Keywords();

            return View();
        }

    }
}
