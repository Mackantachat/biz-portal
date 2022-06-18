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
    public class TSICsController : ApiControllerBase
    {
        [HttpGet]
        [Route("Api/v2/TSICs/Categories")]
        public object GetCategories(string query = "")
        {
            query = !string.IsNullOrEmpty(query) ? query : "";

            Select2Opt[] options;

            options = DB.TSICcategories
                .Where(o => o.CategoryName.Contains(query))
                .Select(o => new Select2Opt() { ID = o.CategoryCode, Text = o.CategoryCode + ": " + o.CategoryName })
                .OrderBy(o => o.ID)
                .ToArray();

            return new { results = options.ToArray() };
        }

        [HttpGet]
        [Route("Api/v2/TSICs/Categories/{id}/SubCategories")]
        public object GetSubCategories(string id, string query = "")
        {
            query = !string.IsNullOrEmpty(query) ? query : "";

            Select2Opt[] options;

            options = DB.TSICsubCategories
                .Where(o => o.TSICcategory.CategoryCode == id && o.SubCategoryName.Contains(query))
                .Select(o => new Select2Opt() { ID = o.SubCategoryCode, Text = o.SubCategoryCode + ": " + o.SubCategoryName })
                .OrderBy(o => o.ID)
                .ToArray();

            return new { results = options.ToArray() };
        }

        [HttpGet]
        [Route("Api/v2/TSICs/SubCategories/{id}/Groups")]
        public object GetGroups(string id, string query = "")
        {
            query = !string.IsNullOrEmpty(query) ? query : "";

            Select2Opt[] options;

            options = DB.TSICgroups
                .Where(o => o.TSICsubCategory.SubCategoryCode == id && o.GroupName.Contains(query))
                .Select(o => new Select2Opt() { ID = o.GroupCode, Text = o.GroupCode + ": " + o.GroupName })
                .OrderBy(o => o.ID)
                .ToArray();

            return new { results = options.ToArray() };
        }

        [HttpGet]
        [Route("Api/v2/TSICs/Groups/{id}/SubGroups")]
        public object GetSubGroups(string id, string query = "")
        {
            query = !string.IsNullOrEmpty(query) ? query : "";

            Select2Opt[] options;

            options = DB.TSICsubGroups
                .Where(o => o.TSICgroup.GroupCode == id && o.SubGroupName.Contains(query))
                .Select(o => new Select2Opt() { ID = o.SubGroupCode, Text = o.SubGroupCode + ": " + o.SubGroupName })
                .OrderBy(o => o.ID)
                .ToArray();

            return new { results = options.ToArray() };
        }

        [HttpGet]
        [Route("Api/v2/TSICs/SubGroups/{id}/Codes")]
        public object GetCodes(string id, string query = "")
        {
            query = !string.IsNullOrEmpty(query) ? query : "";

            Select2Opt[] options;

            options = DB.TSICcodes
                .Where(o => o.TSICsubGroup.SubGroupCode == id && o.TSICName.Contains(query))
                .Select(o => new TSICSelect2Opt() { ID = o.TSICNumber, Text = o.TSICNumber + ": " + o.TSICName, Multiple = o.TSICMultiple })
                .OrderBy(o => o.ID)
                .ToArray();

            return new { results = options.ToArray() };
        }
    }
}
