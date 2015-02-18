using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Shurly.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Account",
                routeTemplate: "api/v1/account",
                defaults: new { controller = "Shurly", action = "Post", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Register",
                routeTemplate: "api/v1/register",
                defaults: new { controller = "Shurly", action = "Post", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Statistics",
                routeTemplate: "api/v1/statistic/{id}",
                defaults: new { controller = "Shurly", action = "Get" }
            );

            config.Routes.MapHttpRoute(
                name: "ShurlyRedirect",
                routeTemplate: "api/v1/{shortUrl}",
                defaults: new { controller = "Shurly", action = "Get" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { controller = "Shurly", id = RouteParameter.Optional }
            );
        }
    }
}
