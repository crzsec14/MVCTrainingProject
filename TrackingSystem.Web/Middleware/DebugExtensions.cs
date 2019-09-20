using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingSystem.Web.Middleware
{
    public static class DebugExtensions
    {
        public static void UseDebug(this IAppBuilder app, DebugOptions options = null)
        {
            if (options == null)
                options = new DebugOptions();

            app.Use<Debug>(options);

        }
    }
}