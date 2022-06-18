using BizPortal.Integrated.Models;
using EGA.WS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Integrated
{
    public class EGovServices
    {
        //private string _egovBaseUrl;
        //private RestClient _client;

        public EGovServices()//(string egovBaseUrl)
        {
            //_egovBaseUrl = egovBaseUrl;
            //_client = new RestClient(_egovBaseUrl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgCode">8 digits of number.</param>
        /// <param name="langCode">en-US, th-TH</param>
        /// <returns></returns>
        public Organization GetOrganizationByOrgCode(string orgCode, string langCode)
        {
            EGAWSAPI api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);

            //var request = new RestRequest("informationservice/REST/Organization/Get", Method.GET);
            //request.AddQueryParameter("lc", langCode);
            //request.AddQueryParameter("oc", orgCode);

            //var response = _client.Execute(request);
            //if (response.StatusCode != System.Net.HttpStatusCode.OK)
            //    return null;

            //Organization org = JsonConvert.DeserializeObject<Organization>(response.Content);
            //return org;

            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("lc", langCode);
            args.Add("oc", orgCode);
            try
            {
                JObject json = api.Get("/ega/infosvc/organization/get", args);
                return json.ToObject<Organization>();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="langCode">en-US, th-TH</param>
        /// <returns></returns>
        public Organization[] GetOrganizations(string langCode)
        {
            try
            {
                //string servicePath = "http://172.17.17.65/informationservice/REST/Organization/List?lc="+langCode;
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(servicePath);
                //request.ContentType = "application/json; charset=utf-8;";
                //request.Method = "GET";
                //JArray json = null;
                //string resp;
                //using (StreamReader stream = new StreamReader(request.GetResponse().GetResponseStream()))
                //{
                //    resp = stream.ReadToEnd();
                //}
                //json = JArray.Parse(resp);

                //return json.ToObject<Organization[]>();

                EGAWSAPI api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);

                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("lc", langCode);

                JObject json = api.Get("/ega/infosvc/organization/list", args);

                return json["Result"].ToObject<Organization[]>();
            }
            catch
            {
                return null;
            }
        }
    }
}
