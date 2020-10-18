using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASP.NET_WhatWasRead
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Books", action = "List" } //page = 1, category = null, accepts filter via querystring
                );
            routes.MapRoute(
                name: "PageRoute",
                url: "books/list/page{page}",
                defaults: new { controller = "Books", action = "List"}, //page = 1, category = null, accepts filter via querystring
                constraints: new { page = @"\d+" }
                );
            routes.MapRoute(
                name: "BooksIdAction",
                url: "books/{id}/{action}",
                defaults: new { controller = "Books", action = "Details" },
                constraints: new { id = @"\d+" }
                );
            routes.MapRoute(
                name: "CategoryPageRoute",
                url: "books/list/{category}/page{page}",
                defaults: new { controller = "Books", action = "List", page = 1}, //page = 1, category = null, accepts filter via querystring
                constraints: new { page = @"\d+" }
                );
            routes.MapRoute(null, "{controller}/{action}");

        }
    }
}
