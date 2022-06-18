using BizPortal.Areas.WebApi.Controllers;
using BizPortal.ViewModels.Select2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class PEAServiceController : ApiControllerBase
    {
        [Route("Api/v2/PEA/TSIC")]
        public object GetTSIC(string query = "")
        {
            List<Select2Opt> options = new List<Select2Opt>();

            PEAServices.PeaCosServiceSoap service = (PEAServices.PeaCosServiceSoap)new PEAServices.PeaCosServiceSoapClient();
            var response = service.Select_Tsic(new PEAServices.Select_TsicRequest()
            {
                Body = new PEAServices.Select_TsicRequestBody()
                {
                    username = "bizportal",
                    Tsic_Tgsc_Code = "",
                    Tsic_Tgsc_Name = query
                }
            });

            if (response.Body.Select_TsicResult.tsic_List != null)
            {
                foreach (var tsic in response.Body.Select_TsicResult.tsic_List)
                {
                    Select2Opt opt = new Select2Opt();

                    opt.ID = tsic.Tsic_Tgsc_Code;
                    opt.Text = tsic.Tsic_Tgsc_Name;

                    options.Add(opt);
                }
            }

            return new { results = options.ToArray() };
        }
    }
}
