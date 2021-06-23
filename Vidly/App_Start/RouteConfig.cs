using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Conventional baed routing
            //routes.MapRoute(
            //    "MovieByReleaseDate",
            //    "movie/released/{year}/{month}",
            //    new { Controller = "Movie", Action = "ByReleasedDate" },
            //    new {year=@"\d{4}",month=@"\d{2}" }//specifying constrains that input parameter should follow
            //    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
