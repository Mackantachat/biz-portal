using BizPortal.AppsHook;
using BizPortal.Areas.WebApi.Controllers;
using V2 = BizPortal.Areas.WebApiV2.Controllers;
using BizPortal.DAL;
using BizPortal.DAL.MongoDB;
using BizPortal.Extensions;
using BizPortal.Models;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using BizPortal.ViewModels.V2;
using EGA.EGA_Development.Util.MailV2.Data;
using EGA.EGA_FileService.Util;
using EGA.EGA_FileService.Util.Models;
using Mapster;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;


namespace BizPortal.Areas.WebApiV3.Controllers
{
    public class ApplicationsController : ApiControllerBase, IProgress<int>
    {
        public object ApplicationRequestID { get; private set; }

        public void Report(int value)
        {

        }

        [HttpPost]
        [Route("Api/V3/Applications/Requests")]
        public async Task<ResponseData<object>> Requests(ApplicationRequestViewModel model)
        {
            var ctrl = new V2.ApplicationsController();
            return await ctrl.Requests(new ApplicationRequestViewModel());
        }

    }
}
