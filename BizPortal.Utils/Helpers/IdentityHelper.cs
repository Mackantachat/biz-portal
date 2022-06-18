using EGA.Owin.Security.EGAOpenID;
using EGA.WS;
using Newtonsoft.Json.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public static class IdentityHelper
    {
        private const int repeatTimes = 3;
        private const int delayMilliseconds = 500;

        public static string GetValue(this IIdentity identity, string claimName)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(EGAOpenIDAttributeExchangeType.UserType);

            return claim?.Value ?? string.Empty;
        }

        public static JObject GetCitizenProfile(string citizenID)
        {
            var api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("CitizenID", citizenID);

            for (int i = 0; i < repeatTimes; i++)
            {
                JObject profile = api.Get("/dopa/personal/profile/normal", args);
                if (profile != null && profile.HasValues)
                {
                    return profile;
                }

                System.Threading.Thread.Sleep(delayMilliseconds);
            }

            return null;
        }

        public static JObject GetJuristicProfile(string juristicID)
        {
            var api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("JuristicID", juristicID);

            for (int i = 0; i < repeatTimes; i++)
            {
                try
                {
                    JObject profile = api.Get("/dbd/v3/juristic", args);
                    if (profile != null && profile.HasValues)
                    {
                        return profile;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                    throw ex;
                };

                System.Threading.Thread.Sleep(delayMilliseconds);
            }

            return null;
        }

        public static string GetUserType(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(EGAOpenIDAttributeExchangeType.UserType);

            return claim?.Value ?? string.Empty;
        }

        public static string GetEmail(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.Email);

            return claim?.Value ?? string.Empty;
        }

        public static string GetBirthDate(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(OpenIdAttributeExchangeType.BirthDate);

            return claim?.Value ?? string.Empty;
        }

        public static OpenIdProfile GetOpenIdProfile(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(OpenIdAttributeExchangeType.LoginProviderId);
            var userName = claimsIdentity.FindFirst(OpenIdAttributeExchangeType.LoginProviderUserName);
            var citizenId = claimsIdentity.FindFirst(EGAOpenIDAttributeExchangeType.CitizenID);
            var juristicId = claimsIdentity.FindFirst(EGAOpenIDAttributeExchangeType.JuristicID);


            return new OpenIdProfile
            {
               UserID = userId?.Value ?? string.Empty,
               UserName = userName?.Value ?? string.Empty,
               IdentityID = citizenId?.Value ?? juristicId?.Value ?? string.Empty
            };
        }


        public static Address GetAddress(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var houseNumber = claimsIdentity.FindFirst(OpenIdAttributeExchangeType.HouseNumber);
            var villageName = claimsIdentity.FindFirst(OpenIdAttributeExchangeType.VillageName);
            var moo = claimsIdentity.FindFirst(OpenIdAttributeExchangeType.Moo);
            var road = claimsIdentity.FindFirst(OpenIdAttributeExchangeType.Road);
            var soi = claimsIdentity.FindFirst(OpenIdAttributeExchangeType.Soi);
            var subDistrict = claimsIdentity.FindFirst(OpenIdAttributeExchangeType.SubDistrict);
            var district = claimsIdentity.FindFirst(OpenIdAttributeExchangeType.District);
            var province = claimsIdentity.FindFirst(OpenIdAttributeExchangeType.Province);
            var postCode = claimsIdentity.FindFirst(OpenIdAttributeExchangeType.PostCode);
            var geoCode = claimsIdentity.FindFirst(OpenIdAttributeExchangeType.GeoCode);

            return new Address
            {
                HouseNumber = houseNumber?.Value ?? string.Empty,
                VillageName = villageName?.Value ?? string.Empty,
                Moo = moo?.Value ?? string.Empty,
                Road = road?.Value ?? string.Empty,
                Soi = soi?.Value ?? string.Empty,
                SubDistrict = subDistrict?.Value ?? string.Empty,
                District = district?.Value ?? string.Empty,
                Province = province?.Value ?? string.Empty,
                PostCode = postCode?.Value ?? string.Empty,
                GeoCode = geoCode?.Value ?? string.Empty
            };
        }

        //public static bool GetAllowAge(DateTime dateObj) 
        //{
        //    if ((DateTime.Now.Year - dateObj.Year) < 20) { return false; }
        //    else
        //    {
        //        if (DateTime.Now.Month < dateObj.Month) { return false; }
        //        else if (DateTime.Now.Month > dateObj.Month) { return true; }
        //        else
        //        {
        //            DateTime dateAndTime = DateTime.Now;
        //            DateTime date = dateAndTime.Date;

        //            DateTime userDateAndTime = new DateTime(DateTime.Now.Year, dateObj.Month, dateObj.Day);
        //            DateTime userDate = userDateAndTime.Date;

        //            if (date < userDate) { return false; }
        //            else
        //            {
        //                return true;
        //            }
        //        }

        //    }
        //}

        public static bool GetAllowAge(DateTime dateObj)
        {

            DateTime zeroTime = new DateTime(1, 1, 1);
            DateTime dateAndTime = DateTime.Now.Date;
            DateTime onlyDate = dateAndTime.Date;
            int year = 0;
            int day = 0;
            int month = 0;
            TimeSpan dtSpan = onlyDate - dateObj.Date;
            year = (zeroTime + dtSpan).Year - 1;
            month = (zeroTime + dtSpan).Month - 1;
            day = (zeroTime + dtSpan).Day - 1;

            if (year > 20)
            {
                return true;
            }
            else if (year < 20)
            {
                return false;
            }
            else
            {
                if (year == 20 && month == 0 && day == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public static class OpenIdAttributeExchangeType
    {
        public const string LoginProvider = "http://www.egov.go.th/2016/identifier/loginprovider";

        public const string LoginProviderId = "http://www.egov.go.th/2016/identifier/loginproviderid";

        public const string LoginProviderUserName = "http://www.egov.go.th/2016/identifier/loginprovideusername";

        public const string BirthDate = "http://www.egov.go.th/2016/identifier/birthdate";

        public const string FullNameEN = "http://www.egov.go.th/2016/identifier/fullnameen";

        public const string PrefixEN = "http://www.egov.go.th/2016/identifier/prefixen";

        public const string FirstEN = "http://www.egov.go.th/2016/identifier/firsten";

        public const string LastEN = "http://www.egov.go.th/2016/identifier/lasten";

        public const string HouseNumber = "http://www.egov.go.th/2016/identifier/housenumber";

        public const string VillageName = "http://www.egov.go.th/2016/identifier/villagename";

        public const string Moo = "http://www.egov.go.th/2016/identifier/moo";

        public const string Road = "http://www.egov.go.th/2016/identifier/road";

        public const string Soi = "http://www.egov.go.th/2016/identifier/soi";

        public const string SubDistrict = "http://www.egov.go.th/2016/identifier/subdistrict";

        public const string District = "http://www.egov.go.th/2016/identifier/district";

        public const string Province = "http://www.egov.go.th/2016/identifier/province";

        public const string PostCode = "http://www.egov.go.th/2016/identifier/postcode";

        public const string GeoCode = "http://www.egov.go.th/2016/identifier/geocode";

        //public const string CardAdressFull = "http://www.egov.go.th/2016/identifier/cardaddressfull";

        //public const string CardAddressStreet1 = "http://www.egov.go.th/2016/identifier/cardadressstreet1";

        //public const string CardAddressStreet2 = "http://www.egov.go.th/2016/identifier/cardadressstreet2";

        //public const string CardAddressSubdistrict = "http://www.egov.go.th/2016/identifier/cardAddresssubdistrict";

        //public const string CardAddressDistrict = "http://www.egov.go.th/2016/identifier/cardaddressdistrict";

        //public const string CardAddressProvince = "http://www.egov.go.th/2016/identifier/cardaddressprovince";

        //public const string CardAddressCountry = "http://www.egov.go.th/2016/identifier/cardaddresscountry";

        //public const string CardAddressZipcode = "http://www.egov.go.th/2016/identifier/cardaddresszipcode";
    }

    public class Address
    {
        public string HouseNumber { get; set; }

        public string VillageName { get; set; }

        public string Moo { get; set; }

        public string Road { get; set; }

        public string Soi { get; set; }

        public string SubDistrict { get; set; }

        public string District { get; set; }

        public string Province { get; set; }

        public string PostCode { get; set; }

        public string GeoCode { get; set; }
    }

    public class OpenIdProfile
    {
        public string UserID { get; set; }

        public string UserName { get; set; }

        public string IdentityID { get; set; }

    }
}
