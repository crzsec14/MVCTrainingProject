using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Security;
using Nancy.Owin;

namespace TrackingSystem.Web.Modules
{
    public class NancyApiModule : NancyModule
    {
        public NancyApiModule()
        {
            this.RequiresMSOwinAuthentication();

            Get("/nancy", x =>
            {
                var env = Context.GetOwinEnvironment();
                var user = Context.GetMSOwinUser();
                return "Hello from nancy! You requested: " + env["owin.RequestPathBase"] + env["owin.RequestPath"] + "<br /><br />User: " + user.Identity.Name;
            });
        }
    }
}