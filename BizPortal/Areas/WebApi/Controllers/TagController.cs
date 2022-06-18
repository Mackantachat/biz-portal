using BizPortal.ViewModels.Select2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BizPortal.Areas.WebApi.Controllers
{
    public class TagController : ApiControllerBase
    {
        [HttpGet]
        [Route("api/Tag/Options")]
        public object Options(string query = null)
        {
            query = !string.IsNullOrEmpty(query) ? query.ToLower() : string.Empty;
            Select2Opt[] results = DB.Tags.Where(o => o.TagName.Contains(query)).Select(o => new Select2Opt()
                {
                    ID = o.TagID.ToString(),
                    Text = o.TagName
                }).ToArray();
            return new { results };
        }
    }
}
