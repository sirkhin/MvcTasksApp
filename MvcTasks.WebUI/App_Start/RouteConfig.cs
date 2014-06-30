using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTasks.WebUI.Infrastructure;

namespace MvcTasks.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
        "",
        new
        {
            controller = "Home",
            action = "TaskList",
            category = (string)null,
            page = 1
        }
      );

            routes.MapRoute(null,
              "Page{page}",
              new { controller = "Home", action = "TaskList", category = (string)null },
              new { page = @"\d+" }
            );

            routes.MapRoute(null,
              "{category}",
              new { controller = "Home", action = "TaskList", page = 1 }
            );

            routes.MapRoute(null,
              "{category}/Page{page}",
              new { controller = "Home", action = "TaskList" },
              new { page = @"\d+" }
            );

            routes.MapRoute(null,
              "{name}",
              new { controller = "User", action = "Index", uniqueName = (string)null}
            );

            routes.MapRoute(null, "{controller}/{action}/{id}");

            routes.MapRoute(null, "{controller}/{action}");

        }
    }
}