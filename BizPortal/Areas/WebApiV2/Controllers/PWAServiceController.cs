using BizPortal.Areas.WebApi.Controllers;
using BizPortal.ViewModels.Select2;
using BizPortal.ViewModels.Apps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class PWAServiceController : ApiControllerBase
    {
        [Route("Api/v2/PWA/Branches")]
        [HttpGet]
        public object Branches(int? pid = null)
        {
            var client = new RestClient("https://customer-application.pwa.co.th");
            var request = new RestRequest("ws/province/{pid}", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            pid = pid ?? 0;
            request.AddUrlSegment("pid", pid.ToString());
            IRestResponse resp = client.Execute(request);

            List<Select2Opt> opts = new List<Select2Opt>();

            if (resp.StatusCode == HttpStatusCode.OK)
            {
                Dictionary<string, PWABranch> branches = JsonConvert.DeserializeObject<Dictionary<string, PWABranch>>(resp.Content);
                foreach (var branch in branches)
                {
                    opts.Add(new Select2Opt() { ID = branch.Value.ID, Text = branch.Value.Name });
                }

                //JArray results = JArray.Parse(resp.Content);

                //for (int i = 0; i < results.Count; i++)
                //{
                //    var item = results[i];
                //    opts.Add(new Select2Opt() { ID = item["id"].ToString(), Text = item["name"].ToString() });
                //}
            }

            return new { results = opts };
        }
    }
}
