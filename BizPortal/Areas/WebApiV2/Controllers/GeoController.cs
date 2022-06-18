using BizPortal.Areas.WebApi.Controllers;
using BizPortal.Integrated;
using BizPortal.ViewModels.Select2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class GeoController : ApiControllerBase
    {
        [HttpGet]
        [Route("Api/v2/Geo/Provinces")]
        public object Provinces(string query = "", string isMetro = "", bool minimumWage = false)
        {
            var provinces = GeoService.Provinces(query, isMetro, CurrentCulture).ToArray();

            if (minimumWage)
            {
                var wages = DB.SSOMinimumWages.ToList();
                foreach (var province in provinces)
                {
                    province.MinimumWage = wages.Where(o => o.ProvinceCode == province.ID).Select(o => o.MinimumWage).DefaultIfEmpty(0).Single();
                }
            }

            return new { results = provinces };
        }

        [HttpGet]
        [Route("Api/v2/Geo/Amphurs")]
        public object Amphurs(string pCode, string query = "")
        {
            return new { results = GeoService.Amphurs(pCode, query, CurrentCulture).ToArray() };
        }

        [HttpGet]
        [Route("Api/v2/Geo/Tambols")]
        public object Tambols(string pCode, string aCode, string query = "")
        {
            return new { results = GeoService.Tambols(pCode, aCode, query, CurrentCulture).ToArray() };
        }

        [HttpGet]
        [Route("Api/v2/Geo/GetProvinceId")]
        public object GetProvinceId(string query = "")
        {
            return new { results = GeoService.GetProvinceId(query).FirstOrDefault().ID };
        }

        [HttpGet]
        [Route("Api/v2/Geo/GetAmphurId")]
        public object GetAmphurId(string pCode, string query = "")
        {
            return new { results = GeoService.Amphurs(pCode, query).FirstOrDefault().ID };
        }

        [HttpGet]
        [Route("Api/v2/Geo/GetTambolId")]
        public object GetTambolId(string pCode, string aCode, string query = "")
        {
            return new { results = GeoService.Tambols(pCode, aCode, query).FirstOrDefault()?.ID };
        }

        [HttpGet]
        [Route("Api/v2/Geo/GetProvinceText")]
        public object GetProvinceText(string pCode)
        {
            return new { results = GeoService.GetProvinceText(pCode) };
        }

        [HttpGet]
        [Route("Api/v2/Geo/GetAmphurText")]
        public object GetAmphurText(string pCode, string aCode)
        {
            return new { results = GeoService.GetAmphurText(pCode, aCode) };
        }

        [HttpGet]
        [Route("Api/v2/Geo/GetTambolText")]
        public object GetTambolText(string pCode, string aCode, string tCode)
        {
            return new { results = GeoService.GetTambolText(pCode, aCode, tCode) };
        }

        [HttpGet]
        [Route("Api/v2/MWA/GetBranch")]
        public object GetMWABranch(string lat = "", string lng = "", string pcode = "", string acode = "", string tcode = "")
        {
            Select2Opt result = null;

            if (result == null && !string.IsNullOrEmpty(lat) && !string.IsNullOrEmpty(lng))
            {
                var client = new RestClient("https://gisonline.mwa.co.th");
                var request = new RestRequest("/1125/rest/convert_latlng_to_UTM47.php");
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddQueryParameter("lat", lat);
                request.AddQueryParameter("lng", lng);
                request.Method = Method.GET;

                //string json = "{ ProvinceCode: \"10\" }";
                //json = JsonConvert.SerializeObject(new { ProvinceCode = 10 });

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
                {
                    JObject xyObj = JObject.Parse(response.Content);

                    // Clear request params
                    request.Parameters.RemoveRange(2, 2);

                    request.Resource = "/1125/rest/identify_javascript.php";
                    request.AddQueryParameter("x", xyObj["Easting"].ToString());
                    request.AddQueryParameter("y", xyObj["Northing"].ToString());
                    response = client.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
                    {
                        JObject branch = JObject.Parse(response.Content);
                        foreach (var item in (JArray)branch["results"])
                        {
                            var attr = item["attributes"];
                            if (attr["DISTRICT_I"] != null)
                            {
                                result = new Select2Opt()
                                {
                                    ID = attr["DISTRICT_I"].ToString(),
                                    Text = attr["NAME"].ToString()
                                };
                                break;
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(pcode) && !string.IsNullOrEmpty(acode) && !string.IsNullOrEmpty(tcode))
            {
                if (acode.Length == 1)
                {
                    acode = acode.PadLeft(2, '0');
                }
                if (tcode.Length == 1)
                {
                    tcode = tcode.PadLeft(2, '0');
                }
                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("ProvinceID", pcode);
                args.Add("AmphurID", acode);
                args.Add("DistrictID", tcode);

                if (result != null)
                {
                    args.Add("BranchID", result.ID);
                }

                string branchUrl = ConfigurationManager.AppSettings["MWA_BRANCH_WS_URL"];
                var branch = Api.Get(branchUrl, args);
                if (int.Parse(branch["Total"].ToString()) >= 1)
                {
                    JArray branches = (JArray)branch["ResultList"];
                    var bObj = branches[0];
                    result = new Select2Opt()
                    {
                        ID = bObj["BranchCode"].ToString(),
                        Text = bObj["BranchName"].ToString()
                    };
                }

            }

            return result;
        }

        [HttpGet]
        [Route("Api/v2/Geo/PostalCodes")]
        public object PostalCodes(string geoCode)
        {
            var codes = DB.MoiPostalCodes.Where(o => o.GeoCode == geoCode).Select(o => o.PostalCode).ToArray();
            return codes;
        }

        #region Get Province, Amphur, Tumbol English

        [HttpGet]
        [Route("Api/v2/Geo/ProvincesEN")]
        public object ProvincesEN(string query = "", string isMetro = "", bool minimumWage = false)
        {
            var provinces = GeoService.Provinces(query, isMetro, "en").ToArray();           
            if (minimumWage)
            {
                var wages = DB.SSOMinimumWages.ToList();
                foreach (var province in provinces)
                {
                    province.MinimumWage = wages.Where(o => o.ProvinceCode == province.ID).Select(o => o.MinimumWage).DefaultIfEmpty(0).Single();
                }
            }

            return new { results = provinces };
        }

        [HttpGet]
        [Route("Api/v2/Geo/AmphursEN")]
        public object AmphursEN(string pCode, string query = "")
        {
            return new { results = GeoService.Amphurs(pCode, query, "en").ToArray() };
        }

        [HttpGet]
        [Route("Api/v2/Geo/TambolsEN")]
        public object TambolsEN(string pCode, string aCode, string query = "")
        {
            return new { results = GeoService.Tambols(pCode, aCode, query, "en").ToArray() };
        }

        #endregion

    }

}
