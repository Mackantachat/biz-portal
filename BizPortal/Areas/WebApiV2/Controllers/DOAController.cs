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
using System.Net.Http.Headers;
using System.Configuration;
using static BizPortal.ViewModels.Apps.DOAStandard.DOAPlants;
using System.IO;
using EGA.WS;

using BizPortal.Utils.Helpers;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using BizPortal.AppsHook;
using BizPortal.ViewModels.SingleForm;
using BizPortal.DAL.MongoDB;
using EGA.Owin.Security.Utils.Extensions;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class DOAController : ApiControllerBase
    {

        //[Route("Api/v2/ORGANICPLANT/Plant_")]
        //[HttpGet]
        //public object Plant()
        //{
        //    var client = new RestClient("http://organic.doa.go.th/restapi/organic_api_v100/getPlant");
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("Accept", "application/json");
        //    request.AddHeader("Content-Type", "application/json");
        //    IRestResponse resp = client.Execute(request);

        //    List<Select2Opt> opts = new List<Select2Opt>();

        //    if (resp.StatusCode == HttpStatusCode.OK)
        //    {
        //        RootObject opt = JsonConvert.DeserializeObject<RootObject>(resp.Content.ToString());
        //        foreach (var obj in opt.result)
        //        {
        //            opts.Add(new Select2Opt() { ID = obj.id, Text = obj.name });
        //        }

        //    }
        //    return new { results = opts };
        //}

        //[Route("Api/v2/ORGANICPLANT/Plant2_")]
        //[HttpGet]
        //public object Plant2()
        //{
        //    var client = new RestClient("http://organic.doa.go.th/restapi/organic_api_v100/getPlantType");
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("Accept", "application/json");
        //    request.AddHeader("Content-Type", "application/json");
        //    IRestResponse resp = client.Execute(request);

        //    List<Select2Opt> opts2 = new List<Select2Opt>();

        //    if (resp.StatusCode == HttpStatusCode.OK)
        //    {
        //        RootObject2 opt2 = JsonConvert.DeserializeObject<RootObject2>(resp.Content.ToString());
        //        foreach (var obj in opt2.RESULT)
        //        {
        //            opts2.Add(new Select2Opt() { ID = obj.ID, Text = obj.NAME });
        //        }

        //    }
        //    return new { results = opts2 };
        //}




        //รายการใบรับรองตามIdentityID
        [HttpGet]
        [Route("Api/v2/ORGANICPLANT/certificatelist")]
        public object GetCertificate()
        {
            GetCertificate data = new GetCertificate();
            data.ConsumerKey = ConfigurationManager.AppSettings["DOA_CONSUMERKEY"];

            data.idcard =  IdentityID;
            List<Select2Opt> opts2 = new List<Select2Opt>();
            if (data != null)
                {
                    var Data = new
                    {
                        data.ConsumerKey,
                        data.idcard
                        
                    };
                string ws_url = ConfigurationManager.AppSettings["DOA_REQUEST_CERTIFICATE_WS_URL"];
                //ws_url = "/doa/request/certificatelist";
                    var doa_response = Api.Call(ws_url, EGA.WS.HttpVerb.POST, null, JsonConvert.SerializeObject(Data), EGA.WS.ContentType.ApplicationJson);
                    if (doa_response != null && doa_response.HasValues)
                    {
                    if ( Convert.ToBoolean(doa_response["status"]) && doa_response["result"]!=null)
                    {
                        List<ListCertificate> lsc = JsonConvert.DeserializeObject<List<ListCertificate>>(doa_response["result"].ToString());
                        if (doa_response != null && doa_response.HasValues)
                        {
                            foreach (var obj in lsc)
                            {
                                opts2.Add(new Select2Opt() { ID = obj.certificateNumber, Text = obj.certificateNumber });
                            }
                        }
                    }
                }
                }

            #region "Frontis: For test purpose only. Delete me once everything is done."
            bool isTestMode = (ConfigurationManager.AppSettings["TestMode"] + "").ToLower() == "true";
            if (isTestMode && data.idcard == "1100800933211")
            {
                opts2 = new List<Select2Opt>()
                {
                    new Select2Opt() { ID = "TAS-55265", Text = "TAS-55265" },
                    new Select2Opt() { ID = "TAS-55266", Text = "TAS-55266" }
                };
            }
            #endregion

            return new { results = opts2 };


        }
     

        //ข้อมูลชนิดพืช
        [HttpGet]
        [Route("Api/v2/ORGANICPLANT/Plant")]
        public object Plants(string query = "", string typeid = "")
        {
            string doa_ws_url = ConfigurationManager.AppSettings["DOA_MASTERDATA_PLANTS_WS_URL"];
            List<Select2Opt> opts2 = new List<Select2Opt>();
            var doa_response = Api.Get<JArray>(doa_ws_url, new Dictionary<string, string> { }, ContentType.ApplicationJson);
            List<Plants> plants = JsonConvert.DeserializeObject<List<Plants>>(doa_response.ToString());
            if (doa_response != null && doa_response.HasValues)
            {
                if (!string.IsNullOrEmpty(query))
                {
                    plants = plants.Where(x => x.NAME.Contains(query)).ToList();
                }

                if (!string.IsNullOrEmpty(typeid))
                {
                    plants = plants.Where(x => x.TYPEID == typeid).ToList();
                }

                foreach (var obj in plants)
                {
                    opts2.Add(new Select2Opt() { ID = obj.ID, Text = obj.NAME });
                }
            }
            return new { results = opts2 };
        }
        //ข้อมูลชนิดแปลง        
        [Route("Api/v2/ORGANICPLANT/Plant2")]
        [HttpGet]
        public object PlantTypes(string query = "")
        {
            string doa_ws_url = ConfigurationManager.AppSettings["DOA_MASTERDATA_PLANTTYPES_WS_URL"];
            List<Select2Opt> opts2 = new List<Select2Opt>();
            var doa_response = Api.Get<JArray>(doa_ws_url, new Dictionary<string, string> { }, ContentType.ApplicationJson);
            List<TypePlants> plants = JsonConvert.DeserializeObject<List<TypePlants>>(doa_response.ToString());
            if (doa_response != null && doa_response.HasValues)
            {
                if (!string.IsNullOrEmpty(query))
                {
                    plants = plants.Where(x => x.NAME.Contains(query)).ToList();
                }
                foreach (var obj in plants)
                {
                    opts2.Add(new Select2Opt() { ID = obj.ID, Text = obj.NAME });
                }
            }
            return new { results = opts2 };
        }

    }

    public class CreateAGPDF : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return null;
        }

        public override bool HasOrgPdfForm
        {
            get
            {
                return true;
            }
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            return null;
        }
    }

    /*
            //ข้อมูลชนิดพืช
            [Route("Api/v2/ORGANICPLANT/Plant")]
            [HttpGet]
            public object Plants(string typid = "")
            {
                string doa_ws_url = ConfigurationManager.AppSettings["DOA_MASTERDATA_PLANTS_WS_URL"];
                List<Select2Opt> opts2 = new List<Select2Opt>();
                var doa_response = Api.Get<JArray>(doa_ws_url, new Dictionary<string, string> { }, ContentType.ApplicationJson);
                List<Plants> plants = JsonConvert.DeserializeObject<List<Plants>>(doa_response.ToString());
                if (doa_response != null && doa_response.HasValues)
                {
                    if (!string.IsNullOrEmpty(typid))
                    {
                        plants = plants.Where(x => x.NAME.Contains(typid)).ToList();
                    }

                    foreach (var obj in plants)
                    {
                        opts2.Add(new Select2Opt() { ID = obj.ID, Text = obj.NAME });
                    }
                }
                return new { results = opts2 };
            }

            //ข้อมูลชนิดแปลง        
            [Route("Api/v2/ORGANICPLANT/Plant2")]
            [HttpGet]
            public object PlantTypes()
            {
                string doa_ws_url = ConfigurationManager.AppSettings["DOA_MASTERDATA_PLANTTYPES_WS_URL"];
                List<Select2Opt> opts2 = new List<Select2Opt>();
                var doa_response = Api.Get<JArray>(doa_ws_url, new Dictionary<string, string> { }, ContentType.ApplicationJson);
                List<TypePlants> plants = JsonConvert.DeserializeObject<List<TypePlants>>(doa_response.ToString());
                if (doa_response != null && doa_response.HasValues)
                {

                    foreach (var obj in plants)
                    {
                        opts2.Add(new Select2Opt() { ID = obj.ID, Text = obj.NAME });
                    }
                }
                return new { results = opts2 };
            }
    */
}
