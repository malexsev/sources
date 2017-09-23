using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cure.WebSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "NewsMore",
                url: "News/More/{skiprecords}",
                defaults: new { controller = "News", action = "More", skiprecords = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "NewsDetails",
                url: "News/{alias}",
                defaults: new { controller = "News", action = "Details" }
            );
            routes.MapRoute(
                name: "BiblioDetails",
                url: "Biblio/{alias}",
                defaults: new { controller = "Biblio", action = "Details" }
            );

            routes.MapRoute(
                name: "Home",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
