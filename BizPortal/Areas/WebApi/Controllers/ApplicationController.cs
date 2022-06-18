using BizPortal.ViewModels;
using BizPortal.ViewModels.Select2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Newtonsoft.Json;
using BizPortal.Models;

namespace BizPortal.Areas.WebApi.Controllers
{
    public class ApplicationController : ApiControllerBase
    {
        [HttpGet]
        [Route("api/Application/GroupOptions")]
        public object GroupOptions(string query)
        {
            query = !string.IsNullOrEmpty(query) ? query.ToLower() : string.Empty;
            Select2GroupOpt[] results = DB.Applications
                .Include(o => o.ApplicationSysName)
                .GroupBy(o => new { o.ApplicationID, o.ApplicationSysName })
                .OrderBy(o => o.Key.ApplicationID)
                .Select(o => new Select2GroupOpt()
                {
                    Text = o.Key.ApplicationSysName,
                    Children = o.Where(c => c.ApplicationID.ToString().ToLower().Contains(query))
                    .OrderBy(c => c.ApplicationID)
                    .Select(c => new Select2Opt()
                    {
                        ID = c.ApplicationID.ToString(),
                        Text = c.ApplicationSysName
                    })
                }).ToArray();

            return new { results };
        }

        [HttpPost]
        [Route("api/Application/List")]
        public DataTablesResult<ApplicationViewModel> List(ApplicationDataTables dataTables)
        {
            DataTablesResult<ApplicationViewModel> result = new DataTablesResult<ApplicationViewModel>();

            var query = DB.Applications.Where(o => !o.IsDeleted).AsQueryable();
            result.Draw = dataTables.Draw;
            result.RecordsTotal = result.RecordsFiltered = query.Count(); // Set default number of records. TotalDisplayRecords must be set again after using filter
            if (!string.IsNullOrEmpty(dataTables.SearchKeyword))
            {
                dataTables.SearchKeyword = dataTables.SearchKeyword.ToLower();
                query = query.Where(o =>
                    o.ApplicationSysName.ToLower().Contains(dataTables.SearchKeyword)
                    || o.ApplicationTranslations.Where(t => t.ApplicationName.ToLower().Contains(dataTables.SearchKeyword)).Any());
            }

            result.RecordsFiltered = query.Count(); // Update number of TotalDisplayRecords.

            var selectedQuery = query.Select(o => new ApplicationViewModel()
            {
                ApplicationID = o.ApplicationID,
                ApplicationSysName = o.ApplicationSysName,
                OrgSysName = o.Organization.OrgSysName
            });


            result.Data = dataTables.GenerateSearchQuery<ApplicationViewModel>(selectedQuery, "ApplicationSysName").ToList();
            return result;
        }
    }
}
