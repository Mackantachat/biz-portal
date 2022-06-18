using BizPortal.Areas.WebApi.Controllers;
using BizPortal.ViewModels.Select2;
using EGA.WS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class TOTServiceController : ApiControllerBase
    {
        [Route("Api/v2/TOT/Services")]
        public object GetServices()
        {
            List<Select2Opt> options = new List<Select2Opt>();
            JArray jArray = Api.Get<JArray>("/tot/bizportal/services", ContentType.ApplicationJson);
            if (jArray != null)
            {
                foreach (var item in jArray)
                {
                    Select2Opt opt = new Select2Opt();
                    opt.ID = item["totservice_code"].ToString();
                    if (CurrentCulture == "th")
                    {
                        opt.Text = item["totservice_desc_thai"].ToString();
                    }
                    else
                    {
                        opt.Text = item["totservice_desc_eng"].ToString();
                    }
                    options.Add(opt);
                }
            }
            return new { results = options.ToArray() };
        }

        [Route("Api/v2/TOT/Branches")]
        public object GetBranches(float latitude, float longitude)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("startlat", latitude.ToString());
            args.Add("startlng", longitude.ToString());
            JArray branches = Api.Get<JArray>("/tot/bizportal/getlocation", args, ContentType.ApplicationJson);
            return branches;
        }
    }
}
