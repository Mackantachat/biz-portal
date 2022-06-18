using BizPortal.Utils.Annotations;
using BizPortal.ViewModels.Apps;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EGA.Owin.Security.Utils.Extensions;
using System.Security.Claims;
using System.Configuration;

namespace BizPortal.Areas.Apps.Controllers
{
    [AuthorizeUser(OpenIDUserType = "JuristicPerson")]
    public class VATController : AppsControllerBase
    {
        string statusUrl = ConfigurationManager.AppSettings["VAT_STATUS_WS_URL"];
        // GET: Apps/VAT
        public ActionResult Index()
        {
            JuristicProfile model = null;
            try
            {
                Dictionary<string, string> vatArgs = new Dictionary<string, string>();
                vatArgs.Add("TaxID", IdentityID);
                var vatStatus = Api.Get(statusUrl, vatArgs);
                if (vatStatus.HasValues && vatStatus["responseStatus"].ToString() == "OK" && vatStatus["responseData"]["VatStatus"].ToString() == "N"
                    && vatStatus["responseData"]["VatApr"].ToString() != "0" && vatStatus["responseData"]["VatApr"].ToString() != "1")
                {
                    try
                    {
                        Dictionary<string, string> args = new Dictionary<string, string>();
                        args.Add("JuristicID", IdentityID);
                        var profile = Api.Get("/dbd/v3/juristic", args);

                        if (profile.Count > 0)
                        {
                            model = profile.ToObject<JuristicProfile>();
                        }
                        else
                        {
                            ViewBag.errorType = "NO_CONTENT";
                            return View("Info", ViewBag);
                        }
                    }
                    catch (Exception e)
                    {
                        ViewBag.errorType = "DBD_ERROR";
                        return View("Info", ViewBag);
                    }
                }
                else
                {
                    ViewBag.errorType = "VAT_REQUESTED";
                    return View("Info", ViewBag);
                }
            }
            catch (Exception e)
            {
                ViewBag.errorType = "RD_ERROR";
                return View("Info", ViewBag);
            }

            return View("Agreement", model);
        }

        public ActionResult Form()
        {
            var email = User.Identity.GetClaimValueOrDefault(ClaimTypes.Email);
            var phone = User.Identity.GetClaimValueOrDefault(ClaimTypes.HomePhone);

            JuristicProfile model = null;

            Dictionary<string, string> vatArgs = new Dictionary<string, string>();
            vatArgs.Add("TaxID", IdentityID);
            var vatStatus = Api.Get(statusUrl, vatArgs);
            if (vatStatus.HasValues && vatStatus["responseStatus"].ToString() == "OK" && vatStatus["responseData"]["VatStatus"].ToString() == "N"
                && vatStatus["responseData"]["VatApr"].ToString() != "0" && vatStatus["responseData"]["VatApr"].ToString() != "1")
            {
                try
                {
                    Dictionary<string, string> args = new Dictionary<string, string>();
                    args.Add("JuristicID", IdentityID);
                    var profile = Api.Get("/dbd/v3/juristic", args);

                    if (profile.Count > 0)
                    {
                        model = profile.ToObject<JuristicProfile>();
                        if (model.AddressInformations[0].Phone == null && phone != null)
                        {
                            model.AddressInformations[0].Phone = phone;
                        }


                        DateTime regDate = DateTime.MinValue;
                        if (DateTime.TryParseExact(model.RegisterDate, "yyyy-MM-dd+hh:mm", CultureInfo.CreateSpecificCulture("en"), DateTimeStyles.None, out regDate))
                        {
                            if (CurrentCulture == "th")
                                model.RegisterDate = regDate.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH"));
                            else
                                model.RegisterDate = regDate.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("en"));
                        }
                    }
                    else
                    {
                        ViewBag.errorType = "NO_CONTENT";
                        return View("Info", ViewBag);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.errorType = "DBD_ERROR";
                    return View("Info", ViewBag);
                }
            }
            else
            {
                ViewBag.errorType = "VAT_REQUESTED";
                return View("Info", ViewBag);
            }

            ViewBag.Email = email;
            return View("Index", model);
        }

    }
}