using BizPortal.Areas.WebApi.Controllers;
using System.Configuration;
using System.Web.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class SWHController : ApiControllerBase
    {

        public class VATCheck
        {
            public string IdentityID { get; set; }
        }



        [Route("Api/v2/SWH/CheckVAT")]
        [HttpPost]
        public object CheckVAT(VATCheck vatCheck)
        {
            //List<string> example = new List<string>();
            //example.AddRange(new string[] { "1857280264124", "0923560000213" });

            string RESULT = "TRUE";
            bool VALUE = true;

            //if (example.Contains(vatCheck.IdentityID.Trim()))
            //{
            //    RESULT = "TRUE";
            //    VALUE = true;
            //}
            return new
            {
                RESULT,
                VALUE,
            };
        }
    }
}