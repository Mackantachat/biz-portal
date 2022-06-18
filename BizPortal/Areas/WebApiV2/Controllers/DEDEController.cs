using BizPortal.Areas.WebApi.Controllers;
using BizPortal.ViewModels.Select2;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class DEDEController : ApiControllerBase
    {
        //private static readonly string DEDE_HOST = ConfigurationManager.AppSettings["DEDE_HOST_WS_URL"];

        //[Route("Api/v2/DEDE/getFuelsDropdownData")]
        //[HttpGet]
        //public object getFuelsDropdownData()
        //{
        //    Select2Opt[] result = null;

        //    if (result == null)
        //    {
        //        var client = new RestClient(DEDE_HOST);
        //        var request = new RestRequest("/" + ConfigurationManager.AppSettings["DEDE_FUEL_MASTERDATA_WS_URL"]);
        //        request.AddHeader("Accept", "application/json");
        //        request.AddHeader("Content-Type", "application/json");
        //        request.Method = Method.GET;

        //        IRestResponse response = client.Execute(request);

        //        if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
        //        {
        //            JObject jRsult = JObject.Parse(response.Content);
        //            JArray jArray = (JArray)jRsult["data"];
        //            result = new Select2Opt[jArray.Count];
        //            for (int i = 0; i < jArray.Count; i++)
        //            {
        //                result[i] = new Select2Opt()
        //                {
        //                    ID = jArray[i]["id"].ToString(),
        //                    Text = jArray[i]["name"].ToString()
        //                };
        //            }
        //        }
        //    }
        //    return new { results = result };
        //}

        //[Route("Api/v2/DEDE/getPlantsDropdownData")]
        //[HttpGet]
        //public object getPlantsDropdownData()
        //{
        //    Select2Opt[] result = null;

        //    if (result == null)
        //    {
        //        var client = new RestClient(DEDE_HOST);
        //        var request = new RestRequest("/" + ConfigurationManager.AppSettings["DEDE_PLANT_MASTERDATA_WS_URL"]);
        //        request.AddHeader("Accept", "application/json");
        //        request.AddHeader("Content-Type", "application/json");
        //        request.Method = Method.GET;

        //        IRestResponse response = client.Execute(request);

        //        if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
        //        {
        //            JObject jRsult = JObject.Parse(response.Content);
        //            JArray jArray = (JArray)jRsult["data"];
        //            result = new Select2Opt[jArray.Count];
        //            for (int i = 0; i < jArray.Count; i++)
        //            {
        //                result[i] = new Select2Opt()
        //                {
        //                    ID = jArray[i]["id"].ToString(),
        //                    Text = jArray[i]["name"].ToString()
        //                };
        //            }
        //        }
        //    }
        //    return new { results = result };
        //}

        private List<Select2Opt> RawToSelect2Options(dynamic raw, string dataNode, string idField, string textField)
        {
            List<Select2Opt> options = new List<Select2Opt>();

            JObject result = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(raw.results.ToString());
            JArray rawItems = (JArray)result[dataNode];

            foreach (JObject rawItem in rawItems)
            {
                options.Add(new Select2Opt()
                {
                    ID = rawItem[idField] + "",
                    Text = rawItem[textField] + "",
                });
            }

            return options;
        }

        [Route("Api/v2/DEDE/fuels")]
        [HttpGet]
        public object fuels()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_FUEL_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/fuelOptions")]
        [HttpGet]
        public object fuelOptions(string pCode)
        {
            List<Select2Opt> data = RawToSelect2Options(fuels(), "data", "id", "name");
            List<Select2Opt> results = new List<Select2Opt>();
            if (pCode == "7")
            {
                results = data;
            } else
            {
                foreach (var item in data)
                {
                    if (item.ID != "10")
                    {
                        results.Add(item);
                    }
                }
            }

            return new { results };
        }

        [Route("Api/v2/DEDE/plants")]
        [HttpGet]
        public object plants()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_PLANT_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/plantOptions")]
        [HttpGet]
        public object plantOptions()
        {
            return new { results = RawToSelect2Options(plants(), "data", "id", "name") };
        }

        [Route("Api/v2/DEDE/regions")]
        [HttpGet]
        public object regions()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_REGIONS_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/provinces")]
        [HttpGet]
        public object provinces()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_PROVINCE_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/provinceUnderRegions")]
        [HttpGet]
        public object provinceUnderRegions()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_PROVINCE_UNDER_REGION_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/city")]
        [HttpGet]
        public object city()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_CITY_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/cityUnderProvinces")]
        [HttpGet]
        public object cityUnderProvinces()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_CITY_UNDER_PROVINCE_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/districts")]
        [HttpGet]
        public object districts()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_DISTRICT_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/districtUnderCities")]
        [HttpGet]
        public object districtUnderCities()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_DISTRICT_UNDER_CITY_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/usertypes")]
        [HttpGet]
        public object usertypes()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_USER_TYPE_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/gentypes")]
        [HttpGet]
        public object gentypes()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_GEN_TYPE_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/genTypeOptions")]
        [HttpGet]
        public object genTypeOptions()
        {
            return new { results = RawToSelect2Options(gentypes(), "data", "id", "name") };
        }

        [Route("Api/v2/DEDE/solars")]
        [HttpGet]
        public object solars()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_SOLAR_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/solarOptions")]
        [HttpGet]
        public object solarOptions()
        {
            return new { results = RawToSelect2Options(solars(), "data", "id", "name") };
        }

        [Route("Api/v2/DEDE/solartypes")]
        [HttpGet]
        public object solartypes()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_SOLAR_TYPE_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/solarTypeOptions")]
        [HttpGet]
        public object solarTypeOptions()
        {
            return new { results = RawToSelect2Options(solartypes(), "data", "id", "solar_type") };
        }

        [Route("Api/v2/DEDE/permits")]
        [HttpGet]
        public object permits()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_PERMIT_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/companyPrefixs")]
        [HttpGet]
        public object companyPrefixs()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_COMPANY_PREFIX_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/purposes")]
        [HttpGet]
        public object purposes()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_PURPOSE_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/documents")]
        [HttpGet]
        public object documents()
        {
            var url = ConfigurationManager.AppSettings["DEDE_MASTERDATA_DOCUMENT_WS_URL"];
            var result = Api.Get(url, EGA.WS.ContentType.ApplicationJson);
            return new { results = result };
        }

        [Route("Api/v2/DEDE/permitsOwnerOptions")]
        [HttpGet]
        public object permitsOwnerOptions(string identity)
        {
            return new
            {
                results = new List<Select2Opt>()
                {
                    new Select2Opt()
                    {
                        ID = "1",
                        Text = "Permit 1"
                    },
                    new Select2Opt()
                    {
                        ID = "2",
                        Text = "Permit 2"
                    }
                }
            };
            //return new { results = RawToSelect2Options(documents(), "data", "id", "name") };
        }
    }
}