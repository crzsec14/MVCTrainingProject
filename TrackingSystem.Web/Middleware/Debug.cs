using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AppFunc = System.Func<
    System.Collections.Generic.IDictionary<string, object>,
    System.Threading.Tasks.Task>;

namespace TrackingSystem.Web.Middleware
{
    public class Debug
    {
        AppFunc _next;
        DebugOptions _options;
        public Debug(AppFunc next, DebugOptions options)
        {
            _next = next;
            _options = options;

            if (_options.OnIncomingRequest == null)
                _options.OnIncomingRequest = (ctx) => {
                    Console.WriteLine("Incoming Request: " + ctx.Request.Path);
                };

            if (_options.OnOutgoingRequest == null)
                _options.OnOutgoingRequest = (ctx) =>
                {
                    Console.WriteLine("Outgoing Request: " + ctx.Request.Path);
                };
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var ctx = new OwinContext(environment);

            _options.OnIncomingRequest(ctx);
            await _next(environment);
            _options.OnOutgoingRequest(ctx);
        }
    }
}