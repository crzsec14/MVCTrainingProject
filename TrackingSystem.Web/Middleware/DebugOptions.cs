using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingSystem.Web.Middleware
{
    public class DebugOptions
    {
        public Action<IOwinContext> OnIncomingRequest { get; set; }
        public Action<IOwinContext> OnOutgoingRequest { get; set; }
    }
}