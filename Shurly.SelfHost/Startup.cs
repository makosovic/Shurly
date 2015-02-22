using System.Web.Http;
using Owin;
using Shurly.Core.WebApi.Serialization;

namespace Shurly.SelfHost
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Formatters.JsonFormatter.SerializerSettings = new ShurlyJsonSerializerSettings();
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

            config.Routes.MapHttpRoute(
                name: "Home",
                routeTemplate: "",
                defaults: new { controller = "Home", action = "Get" }
            );

            appBuilder.UseWebApi(config);
        }
    } 
}
