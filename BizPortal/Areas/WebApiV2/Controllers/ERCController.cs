using BizPortal.Areas.WebApi.Controllers;
using BizPortal.Utils.Extensions;
using EGA.WS;
using BizPortal.ViewModels.Select2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;
using BizPortal.th.go.moph.fda.privus;
using EGA.Owin.Security.Utils.Extensions;
using RestSharp;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class ERCController : ApiControllerBase
    {
        [HttpGet]
        [Route("Api/v2/ERC/Prefix")]
        public object GetPrefix(string query = "")
        {
            RestClient client = new RestClient("http://data.eformthai.com/uprefix");
            RestRequest request = new RestRequest(Method.GET);

            var options = new List<Select2Opt>();

            IRestResponse response = client.Execute(request);
            if (response != null && response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                var resultToObject = JsonConvert.DeserializeObject<Data>(response.Content);
                foreach (var item in resultToObject.data)
                {
                    if (!options.Any(e => e.ID == item.id.ToString()))
                    {
                        options.Add(new Select2Opt
                        {
                            ID = item.id.ToString(),
                            Text = item.name.ToString()
                        });
                    }
                }
            }

            return new { results = options.ToArray() };

        }

        [HttpGet]
        [Route("Api/v2/ERC/Purpose")]
        public object GetPurpose(string query = "")
        {
            RestClient client = new RestClient("http://data.eformthai.com/purpose");
            RestRequest request = new RestRequest(Method.GET);

            var options = new List<Select2Opt>();

            IRestResponse response = client.Execute(request);
            if (response != null && response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                var resultToObject = JsonConvert.DeserializeObject<Data>(response.Content);
                foreach (var item in resultToObject.data)
                {
                    if (item.is_biz == "1") // check condition is biz dga
                    {        
                    if (!options.Any(e => e.ID == item.id.ToString()))
                    {
                        options.Add(new Select2Opt
                        {
                            ID = item.id.ToString(),
                            Text = item.name.ToString()
                        });
                    }
                    }
                }
            }

            return new { results = options.ToArray() };

        }


        public class Data
        {
            public List<DataList> data { get; set; }
        }

        public class DataList
        {
            public string id { get; set; }
            public string name { get; set; }
            public string is_biz { get; set; }
        }
    }
}
