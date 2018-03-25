using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CandyShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.config");

            routes.MapRoute(
                name: "DefaultPage",
                url: "",
                defaults: new { controller = "User", action = "ShowHomePage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "User",
                url: "User",
                defaults: new { controller = "User", action = "ShowHomePage", id = UrlParameter.Optional }
             );

            routes.MapRoute(
                name: "Manager",
                url: "Manager",
                   defaults: new { controller = "Manager", action = "ShowHomePage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
