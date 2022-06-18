using BizPortal.Areas.WebApi.Controllers;
using System.Configuration;
using System.Web.Http;
using Newtonsoft.Json;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class HSSController : ApiControllerBase
    {
        public class Spa
        {
            public string SpaNameTh { get; set; }
            public string ProvinceId { get; set; }
        }

        public class Manager
        {
            public string IdCard { get; set; }
        }

        public class Personal
        {
            public string IdCard { get; set; }
        }

        public class DocID
        {
            public string Doc_ID { get; set; }
        }

        [HttpPost]
        [Route("Api/v2/HSS/SPA/CheckSpaName")]
        public object CheckManager(Spa spa)
        {
            string RESULT = "FALSE";
            string VALUE = "ไม่พบข้อมูล";
            if (User.Identity.IsAuthenticated)
            {
                if (spa != null)
                {
                    var Data = new
                    {
                        spa.SpaNameTh,
                        spa.ProvinceId,
                    };

                    var response = Api.Call(ConfigurationManager.AppSettings["HSS_HEALTHCARE_WS_URL_CHECK_SPA_NAME"], EGA.WS.HttpVerb.POST, null, JsonConvert.SerializeObject(Data), EGA.WS.ContentType.ApplicationJson);
                    if(response != null && response.HasValues)
                    {
                        RESULT = response["RESULT"].ToString();

                        if (response["RESULT"].ToString() == "True")
                        {
                            VALUE = response["VALUE"].ToString();
                        }
                    }
                }
            }
            return new {
                RESULT,
                VALUE,
            };
        }

        [HttpPost]
        [Route("Api/v2/HSS/SPA/CheckManager")]
        public object CheckManager(Manager manager)
        {
            string RESULT = "FALSE";
            string VALUE = "ไม่พบข้อมูล";
            if (User.Identity.IsAuthenticated)
            {
                if (manager != null)
                {
                    var Data = new
                    {
                        manager.IdCard,
                    };

                    var response = Api.Call(ConfigurationManager.AppSettings["HSS_HEALTHCARE_WS_URL_CHECK_MANAGER"], EGA.WS.HttpVerb.POST, null, JsonConvert.SerializeObject(Data), EGA.WS.ContentType.ApplicationJson);
                    if(response != null && response.HasValues)
                    {
                        RESULT = response["RESULT"].ToString();

                        if (response["RESULT"].ToString() == "True")
                        {
                            VALUE = response["VALUE"].ToString();
                        }
                    }
                }
            }
            return new {
                RESULT,
                VALUE,
            };
        }

        [HttpPost]
        [Route("Api/v2/HSS/SPA/CheckPersonal")]
        public object CheckPersonal(Personal personal)
        {
            string RESULT = "FALSE";
            string VALUE = "ไม่พบข้อมูล";
            if (User.Identity.IsAuthenticated)
            {
                if (personal != null)
                {
                    var Data = new
                    {
                        personal.IdCard,
                    };

                    var response = Api.Call(ConfigurationManager.AppSettings["HSS_HEALTHCARE_WS_URL_CHECK_PERSONAL"], EGA.WS.HttpVerb.POST, null, JsonConvert.SerializeObject(Data), EGA.WS.ContentType.ApplicationJson);
                    if (response != null && response.HasValues)
                    {
                        RESULT = response["RESULT"].ToString();

                        if (response["RESULT"].ToString() == "True")
                        {
                            VALUE = response["VALUE"].ToString();
                        }
                    }
                }
            }
            return new
            {
                RESULT,
                VALUE,
            };
        }

        [Route("Api/v2/HSS/SPA/ValidateDocID")]
        [HttpPost]
        public object ValidateDocID(DocID DocID)
        {

            string RESULT = "TRUE";
            bool VALUE = true;

            return new
            {
                RESULT,
                VALUE,
            };
        }

        [Route("Api/v2/HSS/SPA/ValidateDocID2")]
        [HttpPost]
        public object ValidateDocID2(DocID DocID)
        {

            string RESULT = "TRUE";
            bool VALUE = true;

            return new
            {
                RESULT,
                VALUE,
            };
        }

        [Route("Api/v2/HSS/SPA/ValidateDocID3")]
        [HttpPost]
        public object ValidateDocID3(DocID DocID)
        {

            string RESULT = "TRUE";
            bool VALUE = true;

            return new
            {
                RESULT,
                VALUE,
            };
        }

    }
}
