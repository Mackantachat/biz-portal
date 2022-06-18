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

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class EXCISEController : ApiControllerBase
    {
        private static string getAccessToken(string ConsumerKey, string ConsumerSecret, string AgentID)
        {
            string DGA_WS_URL = ConfigurationManager.AppSettings["DGA_WS_URL"].ToString();
            string token = string.Empty;
            String reqTokenUrl = String.Format("{0}/ws/auth/validate?ConsumerSecret={1}&AgentID={2}", DGA_WS_URL, ConsumerSecret, AgentID);
            HttpWebRequest getTokenReq = WebRequest.Create(reqTokenUrl) as HttpWebRequest;
            getTokenReq.Headers.Add("Consumer-Key", ConsumerKey);
            try
            {
                using (HttpWebResponse oAuthResp = getTokenReq.GetResponse() as HttpWebResponse)
                {
                    StreamReader sReader = new StreamReader(oAuthResp.GetResponseStream());
                    string tokenResponse = sReader.ReadToEnd();
                    token = (string)JObject.Parse(tokenResponse)["Result"];
                }
            }
            catch (Exception e)
            {
                token = string.Empty;
            }
            return token;
        }

        [HttpGet]
        [Route("Api/v2/EXCISE/Cuslsic")]
        public object GetCuslsic(string query = "")
        {
            string DGA_WS_URL = ConfigurationManager.AppSettings["DGA_WS_URL"].ToString();
            string EXCISE_WS_URL_CUSLSIC = ConfigurationManager.AppSettings["EXCISE_WS_URL_CUSLSIC"].ToString();
            string ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
            string ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
            string egovToken = getAccessToken(ConsumerKey, ConsumerSecret, "BizAPI");
            string encryptetext = string.Empty;
            string decrypttext = string.Empty;
            string result = string.Empty;

            var jsonPost = JsonConvert.SerializeObject(new
            {
                RequestData = new
                {
                    isicCode = query
                }
            },
            Newtonsoft.Json.Formatting.None,
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            HttpWebRequest httpWebRequest = WebRequest.Create(DGA_WS_URL + "/ws" + EXCISE_WS_URL_CUSLSIC) as HttpWebRequest;
            httpWebRequest.Headers.Add("Consumer-Key", ConsumerKey);
            httpWebRequest.Headers.Add("Token", egovToken);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonPost);
                streamWriter.Flush();
                streamWriter.Close();
            }

            Stream requestStream = httpWebRequest.GetRequestStream();
            requestStream.Close();
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = myHttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(responseStream);
            result = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            responseStream.Close();
            myHttpWebResponse.Close();

            var resultToObject = JsonConvert.DeserializeObject<Cuslsic>(result);

            var options = new List<Select2Opt>();

            if (resultToObject.ResponseCode == "OK" && resultToObject.ResponseMessage == "SUCCESS")
            {
                foreach (var item in resultToObject.ResponseData.isicList)
                {
                    if (!options.Any(e => e.ID == item.isicCode.ToString()))
                    {
                        options.Add(new Select2Opt
                        {
                            ID = item.isicCode.ToString(),
                            Text = item.isicDesc.ToString()
                        });
                    }
                }

            }

            return new { results = options.ToArray() };

        }


        public class Cuslsic
        {
            public string ResponseCode { get; set; }
            public string ResponseMessage { get; set; }
            public CuslsicList ResponseData { get; set; }
        }

        public class CuslsicList
        {
            public List<CuslsicDetail> isicList { get; set; }
        }

        public class CuslsicDetail
        {
            public string isicCode { get; set; }
            public string isicDesc { get; set; }
            public string status { get; set; }
        }
    }
}
