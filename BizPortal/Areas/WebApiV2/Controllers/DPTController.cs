using BizPortal.Areas.WebApi.Controllers;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Select2;
using EGA.WS;
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
    public class DPTController : ApiControllerBase
    {
        [HttpGet]
        [Route("Api/v2/DPT/ResponsibleArea")]
        public object ResponsibleArea(string pCode, string aCode, string tCode, string moo = "")
        {
            var options = new List<Select2Opt>();
            string geocode = string.Empty;
            int moo_int = 0;
            int.TryParse(moo, out moo_int);
            if (String.IsNullOrEmpty(moo))
            {
                geocode = pCode + aCode + tCode;
            }
            else
            {
                if (moo_int < 10)
                    geocode = pCode + aCode + tCode + "0" + moo;
                else
                    geocode = pCode + aCode + tCode + moo;
            }

            var args = new Dictionary<string, string> { { "geocode", geocode }};
            var response = Api.Call(ConfigurationManager.AppSettings["DPT_BUILDING_RESPONSIBLE_AREA_WS_URL"], HttpVerb.GET, args, "", ContentType.ApplicationJson);

            if (response.HasValues && response["Result"].HasValues && response["Result"]["Data"].HasValues)
            {
                foreach (var item in response["Result"]["Data"])
                {
                    if (item["Organizations"].HasValues) //&& !options.Any(e => e.ID == item["Organization"]["Code"].ToString()))
                    {
                        foreach (var org in item["Organizations"])
                        {
                            if (!options.Any(e => e.ID == org["Code"].ToString()))
                            {
                                options.Add(new Select2Opt
                                {
                                    ID = org["Code"].ToString(),
                                    Text = org["Name"].ToString()
                                });
                            }
                        }
                    }
                }
            }

            return new { results = options.ToArray() };

        }

        [HttpGet]
        [Route("Api/v2/DPT/A1License")]
        public object A1License(string orgCode, string licenseNo, string releasedDate)
        {
            var url = ConfigurationManager.AppSettings["DPT_A1LICENSE_CHECK_WS_URL"];
            var args = new Dictionary<string, string>
            {
                { "orgcode", orgCode },
                { "licenseNo", licenseNo },
                { "releasedDate", releasedDate }
            };

            var response = Api.Get(url, args, ContentType.ApplicationJson);
            if (response.HasValues && response["Result"] != null && response["Result"].HasValues)
            {
                return new { Success = true };
            }

            return new { Success = false };
        }
    }
}
