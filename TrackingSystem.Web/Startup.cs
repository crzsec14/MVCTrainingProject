using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using TrackingSystem.Web.Middleware;

namespace TrackingSystem.Web
{
    public partial class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            app.UseDebug(new DebugOptions
            {
                OnIncomingRequest = (ctx) =>
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    ctx.Environment["DebugStopwatch"] = watch;
                },
                OnOutgoingRequest = (ctx) =>
                {
                    var watch = (Stopwatch)ctx.Environment["DebugStopwatch"];
                    watch.Stop();
                    Console.WriteLine("Request took: " + watch.ElapsedMilliseconds + " ms");
                }

            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookies",
                LoginPath = new Microsoft.Owin.PathString("/Auth/Login")
            });

            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AppId = "457564831514889",
                AppSecret = "52cbca79256e6655ee1d2028e4068af4",
                SignInAsAuthenticationType = "ApplicationCookies"
            });

            app.Use(async (ctx, next) =>
            {
                if (ctx.Authentication.User.Identity.IsAuthenticated)
                    Console.WriteLine("User: " + ctx.Authentication.User.Identity.Name);
                else
                    Console.WriteLine("User is not authenticated");
                await next();

            });

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);

            app.Map("/nancy", mappedApp => { mappedApp.UseNancy(); });

            //app.UseNancy(conf => {
            //    conf.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound);
            //});

            //app.Use(async(ctx, next) => {
            //    await ctx.Response.WriteAsync("Hello Madepaker!");
            //});
        }        
    }    
}
