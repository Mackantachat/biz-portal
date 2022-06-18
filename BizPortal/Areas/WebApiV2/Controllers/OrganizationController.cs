using BizPortal.Integrated;
using BizPortal.ViewModels.Select2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    [Authorize]
    public class OrganizationController : ApiController
    {
        //public object Get()
        //{
        //    EGovServices svc = new EGovServices();
        //    var orgs = svc.GetOrganizations("th-TH");
        //    var results = orgs.Select(o => new Select2Opt() { ID = o.Code, Text = o.Name }).ToArray();
        //    return new { results };
        //}
    }
}
