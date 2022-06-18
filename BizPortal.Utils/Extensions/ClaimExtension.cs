using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using System.Web;
using Microsoft.Owin.Security;

namespace BizPortal.Utils.Extensions
{
    public static class ClaimExtension
    {
        public static void UpdateClaimValue(this IIdentity identity, Claim[] claims)
        {
            ClaimsIdentity claimIden = (ClaimsIdentity)identity;

            foreach (var claim in claims)
            {
                Claim oldClaim = identity.GetClaim(claim.Type);
                if (oldClaim != null)
                    claimIden.TryRemoveClaim(oldClaim); // Try to remove claim
                claimIden.AddClaim(claim); // Add claim to identity
            }

            // Rewrite identity to context
            HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
            IAuthenticationManager authenticationManager = context.GetOwinContext().Authentication;
            authenticationManager.SignOut(identity.AuthenticationType);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, claimIden);
        }

        public static void UpdateClaimValue(this IIdentity identity, string claimType, string value)
        {
            Claim claim = new Claim(claimType, value);
            identity.UpdateClaimValue(new Claim[] { claim });
        }

        public static Claim GetClaim(this IIdentity identity, string claimType)
        {
            Claim claim = null;

            ClaimsIdentity claimIden = identity as ClaimsIdentity;
            if (claimIden == null)
                return null;

            if (claimIden.HasClaim(o => o.Type == claimType))
            {
                claim = claimIden.FindFirst(claimType);
            }

            return claim;
        }

        /// <summary>
        /// Claim types can be find in System.Security.Claims.ClaimTypes or RabbitAcc.Models.ApplicationClaimTypes
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="claimType"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetClaimValueOrDefault(this IIdentity identity, string claimType, string defaultValue = "")
        {
            ClaimsIdentity claimIden = identity as ClaimsIdentity;
            if (claimIden == null)
                return null;

            string value = defaultValue;
            if (claimIden.HasClaim(o => o.Type == claimType))
            {
                var claimValue = claimIden.FindFirstValue(claimType).Trim();
                if (!string.IsNullOrEmpty(claimValue))
                    value = claimValue;
            }

            return value;
        }

        public static int GetClaimIntValue(this IIdentity identity, string claimType)
        {
            return int.Parse(GetClaimValueOrDefault(identity, claimType, "0"));
        }

        public static int? GetClaimNullableIntValue(this IIdentity identity, string claimType)
        {
            string result = GetClaimValueOrDefault(identity, claimType, "null");
            if (string.IsNullOrEmpty(result) || result == "null")
                return null;
            return int.Parse(result);
        }

        public static long GetClaimLongValue(this IIdentity identity, string claimType)
        {
            return long.Parse(GetClaimValueOrDefault(identity, claimType, "0"));
        }

        public static bool GetClaimBooleanValue(this IIdentity identity, string claimType)
        {
            return bool.Parse(GetClaimValueOrDefault(identity, claimType, "false"));
        }
    }
}
