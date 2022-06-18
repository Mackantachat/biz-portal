using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EGA.Owin.Security.EGAOpenID;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using System.ComponentModel;

namespace BizPortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserType { get; set; }

        [ForeignKey("Organization")]
        public string OrgCode { get; set; }

        [StringLength(450)]
        public string FullName { get; set; }

        [StringLength(450)]
        public string Prefix { get; set; }

        [StringLength(450)]
        public string Firstname { get; set; }

        [StringLength(450)]
        public string Lastname { get; set; }

        [StringLength(450)]
        public string CitizenID { get; set; }

        [StringLength(450)]
        public string JuristicID { get; set; }

        [StringLength(450)]
        public string PassportID { get; set; }

        [StringLength(450)]
        public string AuthToken { get; set; }

        [StringLength(450)]
        public string LastestLoginProvider { get; set; }

        public virtual Organization Organization { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, string> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            var user = manager.FindById(userIdentity.GetUserId());
            userIdentity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.FullName, user.FullName ?? string.Empty));
            userIdentity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.UserType, user.UserType ?? string.Empty));
            userIdentity.AddClaim(new Claim(WellKnownAttributes.Name.Prefix, user.Prefix ?? string.Empty));
            userIdentity.AddClaim(new Claim(WellKnownAttributes.Name.First, user.Firstname ?? string.Empty));
            userIdentity.AddClaim(new Claim(WellKnownAttributes.Name.Last, user.Lastname ?? string.Empty));

            string displayName = user.UserName;

            switch (user.UserType)
            {
                case "Citizen":
                    userIdentity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.CitizenID, user.CitizenID ?? string.Empty));
                    displayName = user.CitizenID ?? user.UserName;
                    break;
                case "JuristicPerson":
                    userIdentity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.JuristicID, user.JuristicID ?? string.Empty));
                    displayName = user.JuristicID ?? user.UserName;
                    break;
                case "Foreigner":
                    userIdentity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.PassportID, user.PassportID ?? string.Empty));
                    displayName = user.PassportID ?? user.UserName;
                    break;
                case "GovernmentAgent":
                    userIdentity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.CitizenID, user.CitizenID ?? string.Empty));
                    userIdentity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.OrganizationID, user.OrgCode ?? string.Empty));
                    displayName = user.CitizenID ?? user.UserName;
                    break;
            }

            if (string.IsNullOrEmpty(displayName))
            {
                displayName = user.UserName;
            }

            userIdentity.AddClaim(new Claim(EGAOpenIDAttributeExchangeType.AuthToken, user.AuthToken ?? string.Empty));
            userIdentity.AddClaim(new Claim("UserDisplayName", displayName));

            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        [StringLength(450)]
        public string Description { get; set; }

        public int Order { get; set; }
    }
}