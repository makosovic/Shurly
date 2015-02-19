using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Shurly.Core.WebApi.Serialization;

namespace Shurly.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new ShurlyJsonSerializerSettings();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Account",
                routeTemplate: "account",
                defaults: new { controller = "Shurly", action = "Account", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Register",
                routeTemplate: "register",
                defaults: new { controller = "Shurly", action = "Register", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Statistics",
                routeTemplate: "statistic/{accountId}",
                defaults: new { controller = "Shurly", action = "Statistic" }
            );

            config.Routes.MapHttpRoute(
                name: "ShurlyRedirect",
                routeTemplate: "{shortUrl}",
                defaults: new { controller = "Shurly", action = "Get" }
            );
        }
    }
}
