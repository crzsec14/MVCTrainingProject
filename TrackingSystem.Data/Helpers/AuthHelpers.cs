using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TrackingSystem.Data.Helpers
{
    public static class AuthHelpers
    {
        public static void MapUserIdentityToClaims(IDictionary<string, string> userIdentity, ClaimsIdentity identity)
        {
            identity.AddClaim(new Claim("Id", userIdentity["Id"]));
            identity.AddClaim(new Claim(ClaimTypes.Role, userIdentity["Role"]));
            identity.AddClaim(new Claim("Name", userIdentity["Name"]));
            identity.AddClaim(new Claim("UserID", userIdentity["UserID"]));
            identity.AddClaim(new Claim("AuthenticationType", userIdentity["AuthenticationType"]));
            identity.AddClaim(new Claim("Capability", userIdentity["Capability"]));
        }
    }
}
