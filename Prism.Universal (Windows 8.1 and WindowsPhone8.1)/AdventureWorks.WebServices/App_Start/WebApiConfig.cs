// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using WebApiProxy.Server.MetadataGenerator;

namespace AdventureWorks.WebServices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            if (config == null || config.Routes == null) return;

            var route = config.Routes.CreateRoute(
                routeTemplate: "api/meta",
                defaults: new HttpRouteValueDictionary("route"),
                constraints: null,
                dataTokens: null,
                handler: new WebApiDescriptionHandler(config) { InnerHandler = new HttpControllerDispatcher(config) });
            config.Routes.Add("meta", route);

            config.Routes.MapHttpRoute(
                name: "ShippingMethodApi",
                routeTemplate: "api/shippingmethod/{action}",
                defaults: new { controller = "shippingmethod", action = "defaultAction" },
                constraints: new { action = @"[^\d]+" });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
