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
    [Authorize(Roles = ConfigurationValues.ROLES_ADMIN_NAME + "," +
        ConfigurationValues.ROLES_OPDC_ADMIN_NAME + "," +
        ConfigurationValues.ROLES_MODULATOR_NAME)]
    public class ArticleController : ApiControllerBase
    {
        [HttpGet]
        [Route("api/Article/GroupOptions")]
        public object GroupOptions(string query = null)
        {
            query = !string.IsNullOrEmpty(query) ? query.ToLower() : string.Empty;
            Select2GroupOpt[] results = DB.Articles
                .Include(o => o.Section)
                .Include(o => o.Category)
                .GroupBy(o => new { o.Section.SectionSysName, o.Category.CategorySysName })
                .OrderBy(o => o.Key.SectionSysName).ThenBy(o => o.Key.CategorySysName)
                .Select(o => new Select2GroupOpt()
                {
                    Text = o.Key.SectionSysName + " > " + o.Key.CategorySysName,
                    Children = o.Where(c => c.ArticleSysName.ToLower().Contains(query))
                    .OrderBy(c => c.ArticleSysName)
                    .Select(c => new Select2Opt()
                    {
                        ID = c.ArticleID.ToString(),
                        Text = c.ArticleSysName
                    })
                }).ToArray();

            return new { results };
        }

        [HttpPost]
        [Route("api/Article/List")]
        public DataTablesResult<ArticleViewModel> List(ArticleDataTables dataTables)
        {
            DataTablesResult<ArticleViewModel> result = new DataTablesResult<ArticleViewModel>();

            var query = DB.Articles.Where(o => !o.IsDeleted).AsQueryable();
            result.Draw = dataTables.Draw;
            result.RecordsTotal = result.RecordsFiltered = query.Count(); // Set default number of records. TotalDisplayRecords must be set again after using filter
            if (!string.IsNullOrEmpty(dataTables.SearchKeyword))
            {
                dataTables.SearchKeyword = dataTables.SearchKeyword.ToLower();
                query = query.Where(o =>
                    o.ArticleSysName.ToLower().Contains(dataTables.SearchKeyword)
                    || o.ArticleTranslations.Where(t => t.ArticleName.ToLower().Contains(dataTables.SearchKeyword)).Any());
            }
            if (dataTables.SectionID != null)
            {
                query = query.Where(o => o.SectionID == dataTables.SectionID);
            }
            if (dataTables.CategoryID != null)
            {
                query = query.Where(o => o.CategoryID == dataTables.CategoryID);
            }


            result.RecordsFiltered = query.Count(); // Update number of TotalDisplayRecords.

            var selectedQuery = query.Select(o => new ArticleViewModel()
            {
                ArticleID = o.ArticleID,
                ArticleSysName = o.ArticleSysName,
                SectionSysName = o.Section.SectionSysName,
                CategorySysName = o.Category.CategorySysName,
                Unlisted = o.Unlisted,
                Ordering = o.Ordering.HasValue ? o.Ordering.Value : 0
            });


            result.Data = dataTables.GenerateSearchQuery<ArticleViewModel>(selectedQuery, "Ordering").ToList();
            return result;
        }

        // PUT: api/Article/5/unlist/true|false or api/Article/5,6,7,8/unlist/true|false
        [HttpPut]
        [Route("api/Article/{id}/unlist/{unlist}")]
        public ResponseData<object> Put(string id, bool unlist)
        {
            string[] idString = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] idInt = Array.ConvertAll(idString, int.Parse);

            ResponseData<object> response = new ResponseData<object>();
            DB.ArticleSetUnlist(idInt, unlist);
            DB.SaveChanges();

            response.Type = ResultDataType.Success;
            response.Data = new { ArticleID = idInt, Unlisted = unlist };
            response.Message = unlist ? Resources.Article.MSG_SET_UNLIST_SUCCESS : Resources.Article.MSG_SET_LIST_SUCCESS;

            return response;
        }

        // DELETE: api/Article/5 or api/Accounts/5,6,7,8
        [HttpDelete]
        [Route("api/Article/{id}")]
        public ResponseData<object> Delete(string id)
        {
            string[] idString = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] idInt = Array.ConvertAll(idString, int.Parse);

            ResponseData<object> response = new ResponseData<object>();
            DB.ArticleDelete(idInt);

            List<ArticleApplicationMapping> mapDelete = DB.ArticleApplicationMappings.Where(o => idInt.Contains(o.ArticleID)).ToList();
            foreach (ArticleApplicationMapping map in mapDelete)
            {
                DB.ArticleApplicationMappings.Remove(map);
            }

            DB.SaveChanges();

            response.Type = ResultDataType.Success;
            response.Data = new { ArticleID = idInt };
            response.Message = Resources.Article.MSG_DELETED;

            return response;
        }

        [HttpPost]
        [Route("api/Article/Order")]
        public ResponseData<object> Order(OrderRequest model)
        {
            List<OrderJsonModel> ordering = JsonConvert.DeserializeObject<List<OrderJsonModel>>(model.Orders).OrderBy(o => o.id).ToList();
            int[] ids = ordering.Select(s => s.id).ToArray();
            int[] orders = ordering.Select(s => s.order).ToArray();

            ResponseData<object> response = new ResponseData<object>();
            DB.ArticleSetOrderList(ids, orders);
            DB.SaveChanges();

            response.Type = ResultDataType.Success;
            response.Message = Resources.Article.MSG_UPDATE_ORDER_SUCESS;

            return response;
        }

        [HttpPost]
        [Route("api/Article/Published")]
        [AllowAnonymous]
        public object Published(ArticleSearchViewModel model)
        {
            int totalRecordNoFilter = 0;
            int totalRecord = 0;
            var query = DB.ArticleViews
                .Where(e => e.TwoLetterISOLanguageName == this.CurrentCulture && e.Published && e.Unlisted == model.UnlistedVisible)
                .AsQueryable();

            totalRecordNoFilter = query.Count();

            //filter
            if (model.CategoryId.HasValue && model.CategoryId != 0)
            {
                query = query.Where(o => o.CategoryID == model.CategoryId).AsQueryable();
            }

            if (!string.IsNullOrEmpty(model.SearchKeyword))
            {
                // Also search Tag
                query = query.Where(o => o.ArticleName.Contains(model.SearchKeyword)
                                    || o.CategoryName.Contains(model.SearchKeyword)
                                    || o.ArticleIntroText.Contains(model.SearchKeyword)
                                    || o.TagName.ToLower().Contains(model.SearchKeyword.ToLower())).AsQueryable();
            }

            if (!string.IsNullOrEmpty(model.SearchTag))
            {
                string tag = model.SearchTag.ToLower();
                query = query.Where(o => o.TagName.ToLower().Contains(tag)).AsQueryable();
            }

            //order
            query = query.OrderBy(o => o.Ordering);
            //query = query.OrderBy(model.Ordering + (!model.IsAsc ? " descending" : ""));

            //if (isAsc)
            //{
            //    query = query.OrderBy(o => o.Ordering).AsQueryable();
            //}
            //else
            //{
            //    query = query.OrderByDescending(o => o.Ordering).AsQueryable();
            //}

            // set total record
            totalRecord = query.Count();

            //paging
            query = query.Skip((model.PageNo - 1) * model.PageLength).Take(model.PageLength).AsQueryable();

            var response = new
            {
                model.PageNo,
                model.PageLength,
                TotalData = totalRecordNoFilter,
                TotalDisplayData = totalRecord,
                Data = query.Select(o => new
                {
                    o.ArticleID,
                    o.ThumbnailID,
                    o.CategoryID,
                    o.ArticleName,
                    o.ArticleIntroText,
                    o.TagName
                })
            };

            return response;
        }



        [HttpPost]
        [Route("api/Article/SearchPublished")]
        [AllowAnonymous]
        public object SearchPublished(ArticleSearchViewModel model)
        {
            int totalRecordNoFilter = 0;
            int totalRecord = 0;

            if (string.IsNullOrEmpty(model.Language)) model.Language = this.CurrentCulture;

            if (model.PageNo <= 0) model.PageNo = 1;
            if (model.PageLength <= 0) model.PageLength = 10;

            var query = DB.ArticleViews
                .Where(e => e.TwoLetterISOLanguageName == model.Language && e.Published && e.Unlisted == false)
                .AsQueryable();

            totalRecordNoFilter = query.Count();

            //filter
            if (model.CategoryId.HasValue && model.CategoryId != 0)
            {
                query = query.Where(o => o.CategoryID == model.CategoryId).AsQueryable();
            }

            if (!string.IsNullOrEmpty(model.SearchKeyword))
            {
                query = query.Where(o => o.ArticleName.Contains(model.SearchKeyword)
                                        || o.ArticleIntroText.Contains(model.SearchKeyword)
                                        || o.TagName.Contains(model.SearchKeyword)
                                        || o.CategoryName.Contains(model.SearchKeyword)).AsQueryable();

            }

            if (!string.IsNullOrEmpty(model.SearchTag))
            {
                string tag = model.SearchTag.ToLower();
                query = query.Where(o => o.TagName.ToLower().Contains(tag)).AsQueryable();
            }

            //order
            query = query.OrderBy(o => o.Ordering);

            // set total record
            totalRecord = query.Count();

            // Reset page number to 1 if starting index exceed total record
            if ((model.PageNo - 1) * model.PageLength + 1 > totalRecord)
            {
                model.PageNo = 1;
            }


            //paging
            query = query.Skip((model.PageNo - 1) * model.PageLength).Take(model.PageLength).AsQueryable();

            var result = query.Select(o => new
            {
                o.ArticleID,
                o.CategoryID,
                o.ThumbnailID,
                o.CategoryName,
                o.ArticleName,
                o.ArticleIntroText,
                o.TagName
            }).ToList();

            var response = new
            {
                model.PageNo,
                model.PageLength,
                TotalData = totalRecordNoFilter,
                TotalFilteredData = totalRecord,
                Data = result.Select(o => new
                {
                    o.ArticleID,
                    ArticleUrl = Url.Content(string.Format("~/{0}/Home/Article/{1}", model.Language, o.ArticleID)),
                    ThumbnailUrl = Url.Content(string.Format("~/{0}/File/GetThumbnail/{1}?cid={2}", model.Language, o.ThumbnailID, o.CategoryID)),
                    o.CategoryID,
                    o.CategoryName,
                    o.ArticleName,
                    o.ArticleIntroText,
                    o.TagName
                })
            };

            return response;
        }
    }
}
