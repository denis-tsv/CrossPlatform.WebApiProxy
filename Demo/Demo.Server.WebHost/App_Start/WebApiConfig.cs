using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using WebApiProxy.Server.MetadataGenerator;

namespace Demo.Server.WebHost
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
           
            var route = config.Routes.CreateRoute(
                routeTemplate: "api/meta",
                defaults: new HttpRouteValueDictionary("route"),
                constraints: null,
                dataTokens: null,
                handler: new WebApiDescriptionHandler(config) { InnerHandler = new HttpControllerDispatcher(config) });
            config.Routes.Add("meta", route);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
