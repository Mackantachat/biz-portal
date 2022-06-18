using BizPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BizPortal.Areas.WebApi.Controllers
{
    public class SectionController : ApiControllerBase
    {
        [HttpPost]
        [Route("api/Section/List")]
        public DataTablesResult<SectionViewModel> List(SectionDataTables dataTables)
        {
            DataTablesResult<SectionViewModel> result = new DataTablesResult<SectionViewModel>();

            var query = DB.SectionTranslations.Where(o => !o.Section.IsDeleted && o.Language.TwoLetterISOLanguageName == dataTables.Lang).AsQueryable();
            result.Draw = dataTables.Draw;
            result.RecordsTotal = result.RecordsFiltered = query.Count(); // Set default number of records. TotalDisplayRecords must be set again after using filter
            if (!string.IsNullOrEmpty(dataTables.SearchKeyword))
            {
                dataTables.SearchKeyword = dataTables.SearchKeyword.ToLower();
                query = query.Where(o => o.SectionName.ToLower().Contains(dataTables.SearchKeyword));
            }

            result.RecordsFiltered = query.Count(); // Update number of TotalDisplayRecords.

            var selectedQuery = query.Select(o => new SectionViewModel()
            {
                SectionID = o.SectionID,
                SectionName = o.SectionName,
                Published = o.Section.Published,
                Ordering = o.Section.Ordering,
                ThumbnailID = o.Section.ThumbnailID
            });
            result.Data = dataTables.GenerateSearchQuery<SectionViewModel>(selectedQuery, "SectionName").ToList();

            return result;
        }
    }
}
