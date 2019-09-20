using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TrackingSystem.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //ContainerConfig.RegisterContainer(config);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
