using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TastyFoods
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // HOME ---------------------------------------------------------------------------//

            routes.MapRoute("HOME_INDEX_CANONICAL", SEO.HOME_INDEX_CANONICAL, new { controller = "Home", action = "Index" });

            // ABOUT --------------------------------------------------------------------------//

            routes.MapRoute("HOME_ABOUT_CANONICAL", SEO.HOME_ABOUT_CANONICAL, new { controller = "Home", action = "About" });

            // GALLERY ------------------------------------------------------------------------//

            routes.MapRoute("HOME_GALLERY_CANONICAL", SEO.HOME_GALLERY_CANONICAL, new { controller = "Home", action = "Gallery" });

            // CONTACT ------------------------------------------------------------------------//

            routes.MapRoute("HOME_CONTACT_CANONICAL", SEO.HOME_CONTACT_CANONICAL, new { controller = "Home", action = "Contact" });

            // PAGE NOT FOUND -----------------------------------------------------------------//

            routes.MapRoute("HOME_PAGENOTFOUND_CANONICAL", SEO.HOME_PAGENOTFOUND_CANONICAL, new { controller = "Home", action = "PageNotFound" });

            // MENU ---------------------------------------------------------------------------//

            routes.MapRoute("MENU_INDEX_CANONICAL", SEO.MENU_INDEX_CANONICAL, new { controller = "Menu", action = "Index" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}