using BizPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mvc = System.Web.Mvc;

namespace BizPortal.Areas.WebApi.Controllers
{
    public class CategoryController : ApiControllerBase
    {
        [HttpPost]
        [Route("api/Category/List")]
        public DataTablesResult<CategoryViewModel> List(CategoryDataTables dataTables)
        {
            DataTablesResult<CategoryViewModel> result = new DataTablesResult<CategoryViewModel>();

            var query = DB.CategoryTranslations.Where(o => !o.Category.IsDeleted && o.Language.TwoLetterISOLanguageName == dataTables.Lang).AsQueryable();
            result.Draw = dataTables.Draw;
            result.RecordsTotal = result.RecordsFiltered = query.Count(); // Set default number of records. TotalDisplayRecords must be set again after using filter
            if (!string.IsNullOrEmpty(dataTables.SearchKeyword))
            {
                dataTables.SearchKeyword = dataTables.SearchKeyword.ToLower();
                query = query.Where(o => o.CategoryName.ToLower().Contains(dataTables.SearchKeyword));
            }
            if (dataTables.SectionID != null)
            {
                query = query.Where(o => o.Category.SectionID == dataTables.SectionID);
            }

            result.RecordsFiltered = query.Count(); // Update number of TotalDisplayRecords.

            var selectedQuery = query.Select(o => new CategoryViewModel()
            {
                CategoryID = o.CategoryID,
                CategoryName = o.CategoryName,
                Published = o.Category.Published,
                Ordering = o.Category.Ordering,
                ThumbnailID = o.Category.ThumbnailID,
                SectionID = o.Category.Section.SectionID,
                SectionName = o.Category.Section.SectionTranslations.Where(s => s.Language.TwoLetterISOLanguageName == dataTables.Lang).Select(s => s.SectionName).FirstOrDefault(),
            });
            result.Data = dataTables.GenerateSearchQuery<CategoryViewModel>(selectedQuery, "CategoryName").ToList();

            return result;
        }

        [HttpGet]
        [Route("api/Category/Options/{sectionId}")]
        public List<Mvc.SelectListItem> Option(int sectionId)
        {
            var categories = DB.Categories.Where(o => !o.IsDeleted && o.SectionID == sectionId).OrderBy(o => o.CategorySysName).Select(o => new Mvc.SelectListItem() { Value = o.CategoryID.ToString(), Text = o.CategorySysName }).ToList();
            return categories;
        }
    }
}
