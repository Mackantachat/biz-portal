using BizPortal.DAL.MongoDB;
using BizPortal.Models;
using BizPortal.ViewModels.Select2;
using EGA.WS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Integrated
{
    public class GeoService
    {
        public static List<ProvinceSelect2Opt> Provinces(string query, string isMetro = "", string culture = "th")
        {
            /*var client = new RestClient("http://172.17.17.65/");
            var request = new RestRequest("InformationService/REST/GeoService/ListProvince");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.Method = Method.POST;

            IRestResponse response = client.Execute(request);

            List<Select2Opt> results = new List<Select2Opt>();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                JArray provinces = JArray.Parse(response.Content);
                foreach (var province in provinces)
                {
                    Select2Opt opt = new Select2Opt();
                    opt.ID = province["Province_Code"].ToString();
                    if (culture.ToLower() == "en")
                    {
                        opt.Text = province["Province_Name_En"].ToString();
                    }
                    if (string.IsNullOrEmpty(opt.Text))
                    {
                        opt.Text = province["Province_Name_Th"].ToString();
                    }
                    results.Add(opt);
                }

                if (isMetro == "Metro")
                {
                    results = results.Where(o => new string[] { "10", "11", "12" }.Contains(o.ID)).ToList();
                }
                else if (isMetro == "Provin")
                {
                    results = results.Where(o => !(new string[] { "10", "11", "12" }.Contains(o.ID))).ToList();
                }
            }*/

            List<ProvinceSelect2Opt> results = new List<ProvinceSelect2Opt>();
            EGAWSAPI api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
            var provinces = api.Post<JArray>("/ega/informationservice/gerservice/listprovince", null, ContentType.ApplicationJson);
            if (provinces != null)
            {
                foreach (var province in provinces)
                {
                    ProvinceSelect2Opt opt = new ProvinceSelect2Opt();
                    opt.ID = province["Province_Code"].ToString();
                    if (culture.ToLower() == "en")
                    {
                        opt.Text = province["Province_Name_En"].ToString();
                    }
                    if (string.IsNullOrEmpty(opt.Text))
                    {
                        opt.Text = province["Province_Name_Th"].ToString();
                    }
                    results.Add(opt);
                }

                if (isMetro == ProvinceType.Metro.ToString())
                {
                    results = results.Where(o => new string[] { "10", "11", "12" }.Contains(o.ID)).ToList();
                }
                else if (isMetro == ProvinceType.Provin.ToString())
                {
                    results = results.Where(o => !(new string[] { "10", "11", "12" }.Contains(o.ID))).ToList();
                }
                else if (isMetro == ProvinceType.BKK.ToString())
                {
                    results = results.Where(o => new string[] { "10" }.Contains(o.ID)).ToList();
                }
                else if (isMetro == ProvinceType.NotBKK.ToString())
                {
                    results = results.Where(o => !(new string[] { "10" }.Contains(o.ID))).ToList();
                }
            }
            results = results.Where(o => o.Text.Contains(string.IsNullOrEmpty(query) ? string.Empty : query)).ToList();
            //results = results.OrderBy(item => item.Text).ToList();
            return results;
        }

        public static List<Select2Opt> Amphurs(string pCode, string query, string culture = "th")
        {
            /*//--// Frontis Stub
            List<Select2Opt> resultStub = new List<Select2Opt>();
            resultStub.Add(new Select2Opt { ID = "10", Text = "หลักสี่" });
            return resultStub;

            /*
            var client = new RestClient("http://172.17.17.65/");
            var request = new RestRequest("InformationService/REST/GeoService/ListAmphur");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(new { ProvinceCode = pCode }), ParameterType.RequestBody);
            request.Method = Method.POST;

            //string json = "{ ProvinceCode: \"10\" }";
            //json = JsonConvert.SerializeObject(new { ProvinceCode = 10 });

            IRestResponse response = client.Execute(request);

            List<Select2Opt> results = new List<Select2Opt>();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                JArray amphurs = JArray.Parse(response.Content);
                foreach (var amphur in amphurs)
                {
                    Select2Opt opt = new Select2Opt();
                    opt.ID = amphur["Amphur_Code"].ToString();
                    if (culture.ToLower() == "en")
                    {
                        opt.Text = amphur["Amphur_Name_En"].ToString();
                    }
                    if (string.IsNullOrEmpty(opt.Text))
                    {
                        opt.Text = amphur["Amphur_Name_Th"].ToString();
                    }
                    results.Add(opt);
                }
            }*/

            List<Select2Opt> results = new List<Select2Opt>();
            EGAWSAPI api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
            var amphurs = api.Call<JArray>("/ega/informationservice/gerservice/listamphur", HttpVerb.POST, null, JsonConvert.SerializeObject(new { ProvinceCode = pCode }), ContentType.ApplicationJson);
            if (amphurs != null)
            {
                foreach (var amphur in amphurs)
                {
                    Select2Opt opt = new Select2Opt();
                    opt.ID = amphur["Amphur_Code"].ToString();
                    if (culture.ToLower() == "en")
                    {
                        opt.Text = amphur["Amphur_Name_En"].ToString();
                    }
                    if (string.IsNullOrEmpty(opt.Text))
                    {
                        opt.Text = amphur["Amphur_Name_Th"].ToString();
                    }
                    results.Add(opt);
                }
            }

            return results.Where(o => o.Text.Contains(string.IsNullOrEmpty(query) ? string.Empty : query)).ToList();
        }

        public static List<Select2Opt> Tambols(string pCode, string aCode, string query, string culture = "th")
        {
            /*//--// Frontis Stub
            List<Select2Opt> resultStub = new List<Select2Opt>();
            resultStub.Add(new Select2Opt { ID = "10", Text = "ทุ่งสองห้อง" });
            return resultStub;

            /*var client = new RestClient("http://172.17.17.65/");
            var request = new RestRequest("InformationService/REST/GeoService/ListTambol");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(new { ProvinceCode = pCode, AmphurCode = aCode }), ParameterType.RequestBody);
            request.Method = Method.POST;

            //string json = "{ ProvinceCode: \"10\" }";
            //json = JsonConvert.SerializeObject(new { ProvinceCode = 10 });

            IRestResponse response = client.Execute(request);

            List<Select2Opt> results = new List<Select2Opt>();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                JArray amphurs = JArray.Parse(response.Content);
                foreach (var amphur in amphurs)
                {
                    Select2Opt opt = new Select2Opt();
                    opt.ID = amphur["Tambol_Code"].ToString();
                    if (culture.ToLower() == "en")
                    {
                        opt.Text = amphur["Tambol_Name_En"].ToString();
                    }
                    if (string.IsNullOrEmpty(opt.Text))
                    {
                        opt.Text = amphur["Tambol_Name_Th"].ToString();
                    }
                    results.Add(opt);
                }
            }*/

            List<Select2Opt> results = new List<Select2Opt>();
            EGAWSAPI api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
            var tumbols = api.Call<JArray>("/ega/informationservice/gerservice/listtumbol ", HttpVerb.POST, null, JsonConvert.SerializeObject(new { ProvinceCode = pCode, AmphurCode = aCode }), ContentType.ApplicationJson);
            if (tumbols != null)
            {
                foreach (var tumbol in tumbols)
                {
                    Select2Opt opt = new Select2Opt();
                    opt.ID = tumbol["Tambol_Code"].ToString();
                    if (culture.ToLower() == "en")
                    {
                        opt.Text = tumbol["Tambol_Name_En"].ToString();
                    }
                    if (string.IsNullOrEmpty(opt.Text))
                    {
                        opt.Text = tumbol["Tambol_Name_Th"].ToString();
                    }
                    results.Add(opt);
                }
            }

            return results.Where(o => o.Text.Contains(string.IsNullOrEmpty(query) ? string.Empty : query)).ToList();
        }

        public static List<Select2Opt> GetProvinceId(string query = "", string culture = "th")
        {
            List<Select2Opt> results = new List<Select2Opt>();
            EGAWSAPI api = EGAWSAPI.CreateChatInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
            var provinces = api.Post<JArray>("/ega/informationservice/gerservice/listprovince", null, ContentType.ApplicationJson);
            if (provinces != null)
            {
                foreach (var province in provinces)
                {
                    Select2Opt opt = new Select2Opt();
                    opt.ID = province["Province_Code"].ToString();
                    if (culture.ToLower() == "en")
                    {
                        opt.Text = province["Province_Name_En"].ToString();
                    }
                    if (string.IsNullOrEmpty(opt.Text))
                    {
                        opt.Text = province["Province_Name_Th"].ToString();
                    }
                    results.Add(opt);
                }
            }

            return results.Where(o => o.Text.Contains(query)).ToList();
        }

        public static string GetProvinceText(string pCode = "", string culture = "th")
        {
            List<Select2Opt> results = new List<Select2Opt>();
            EGAWSAPI api = EGAWSAPI.CreateChatInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
            var provinces = api.Post<JArray>("/ega/informationservice/gerservice/listprovince", null, ContentType.ApplicationJson);
            if (provinces != null)
            {
                foreach (var province in provinces)
                {
                    Select2Opt opt = new Select2Opt();
                    opt.ID = province["Province_Code"].ToString();
                    if (culture.ToLower() == "en")
                    {
                        opt.Text = province["Province_Name_En"].ToString();
                    }
                    if (string.IsNullOrEmpty(opt.Text))
                    {
                        opt.Text = province["Province_Name_Th"].ToString();
                    }
                    results.Add(opt);
                }
            }

            var item = results.Where(o => o.ID == pCode).SingleOrDefault();
            return item != null ? item.Text : null;
        }

        public static string GetAmphurText(string pCode, string aCode, string culture = "th")
        {
            List<Select2Opt> results = new List<Select2Opt>();
            EGAWSAPI api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
            var amphurs = api.Call<JArray>("/ega/informationservice/gerservice/listamphur", HttpVerb.POST, null, JsonConvert.SerializeObject(new { ProvinceCode = pCode }), ContentType.ApplicationJson);
            if (amphurs != null)
            {
                foreach (var amphur in amphurs)
                {
                    Select2Opt opt = new Select2Opt();
                    opt.ID = amphur["Amphur_Code"].ToString();
                    if (culture.ToLower() == "en")
                    {
                        opt.Text = amphur["Amphur_Name_En"].ToString();
                    }
                    if (string.IsNullOrEmpty(opt.Text))
                    {
                        opt.Text = amphur["Amphur_Name_Th"].ToString();
                    }
                    results.Add(opt);
                }
            }

            var item = results.Where(o => o.ID == aCode).SingleOrDefault();
            return item != null ? item.Text : null;
        }

        public static string GetTambolText(string pCode, string aCode, string tCode, string culture = "th")
        {
            List<Select2Opt> results = new List<Select2Opt>();
            EGAWSAPI api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
            var tumbols = api.Call<JArray>("/ega/informationservice/gerservice/listtumbol ", HttpVerb.POST, null, JsonConvert.SerializeObject(new { ProvinceCode = pCode, AmphurCode = aCode }), ContentType.ApplicationJson);
            if (tumbols != null)
            {
                foreach (var tumbol in tumbols)
                {
                    Select2Opt opt = new Select2Opt();
                    opt.ID = tumbol["Tambol_Code"].ToString();
                    if (culture.ToLower() == "en")
                    {
                        opt.Text = tumbol["Tambol_Name_En"].ToString();
                    }
                    if (string.IsNullOrEmpty(opt.Text))
                    {
                        opt.Text = tumbol["Tambol_Name_Th"].ToString();
                    }
                    results.Add(opt);
                }
            }

            var item = results.Where(o => o.ID == tCode).SingleOrDefault();
            return item != null ? item.Text : null;
        }
        public static ResultData<DEDEGeoData> GetDEDEGeoData(string StringCodeOrId, string CodeOrId)
        {
            string DEDEGeoUrl = ConfigurationManager.AppSettings["DEDEGeoServiceIP"];
            ResultData<DEDEGeoData> ReturnResult = new ResultData<DEDEGeoData>();

            try
            {
                var BaseUrl = DEDEGeoUrl;

                var url = string.Format(BaseUrl, StringCodeOrId, CodeOrId);

                var client = new RestClient(url);
                var response = client.Execute<List<DEDEGeoData>>(new RestRequest());
                var result = response.Data;


                ReturnResult.Result = result;
                ReturnResult.Status = "success";

            }
            catch (Exception ex)
            {
                ReturnResult.Result = null;
                ReturnResult.Status = "error";
            }


            return ReturnResult;
        }




    }
}
