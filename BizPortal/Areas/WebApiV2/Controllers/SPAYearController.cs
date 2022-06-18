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
    public class SPAYearController : ApiControllerBase
    {

        [Route("Api/v2/SPAYear/GetYearDDL")]
        [HttpGet]
        public object GetYearDDL()
        {

            List<Select2Opt> options = new List<Select2Opt>();

            options.Add(new Select2Opt()
            {
                ID = (DateTime.Now.Year + 543).ToString()
                ,
                Text = (DateTime.Now.Year + 543).ToString()
            });

            options.Add(new Select2Opt()
            {
                ID = (DateTime.Now.Year + 543 + 1).ToString()
                ,
                Text = (DateTime.Now.Year + 543 + 1).ToString()
            });

            return new { results = options.ToArray() };
        }

    }
}
