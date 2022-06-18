using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizPortal.ViewModels;
using BizPortal.Models;
using Mapster;
using System.Linq.Dynamic;
using EGA.Owin.Security.Utils.Extensions;
using EGA.Owin.Security.EGAOpenID;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Globalization;
using BizPortal.Utils.Annotations;

namespace BizPortal.Controllers
{
    [FilterUser]
    public class HomeController : ControllerBase
    {
        public ActionResult Index(string showMsg = null, string returnUrl = null)
        {
            List<CategoryView> model = DB.CategoryViews
                .Where(e => e.TwoLetterISOLanguageName == this.CurrentCulture && !e.Unlisted)
                .OrderBy(e => e.Ordering).ToList();

            if (!string.IsNullOrEmpty(showMsg))
            {
                string message = string.Empty;
                if (showMsg == ShowMsgType.Unauthorized.GetEnumStringValue())
                {
                    message = BizPortal.Resources.Global.MSG_WARNING_UNAUTHORIZED;
                    notifyMsg(NotifyMsgType.Warning, message, returnUrl);
                }
                else if (showMsg == ShowMsgType.InvalidAccount.GetEnumStringValue())
                {
                    message = BizPortal.Resources.Global.MSG_WARNING_INVALID_ACCOUNT;
                    notifyMsg(NotifyMsgType.Warning, message, returnUrl);
                }
                else if (showMsg == ShowMsgType.ProfileUpdated.GetEnumStringValue())
                {
                    message = BizPortal.Resources.Global.MSG_SUCCESS_PROFILE_UPDATED;
                    notifyMsg(NotifyMsgType.Success, message, returnUrl);
                }
                else if (showMsg == ShowMsgType.FailedToUpdate.GetEnumStringValue())
                {
                    message = BizPortal.Resources.Global.MSG_ERROR_FAILED_TO_UPDATE;
                    notifyMsg(NotifyMsgType.Error, message, returnUrl);
                }
                else if (showMsg == ShowMsgType.ApplicationNotFound.GetEnumStringValue())
                {
                    message = BizPortal.Resources.Global.MSG_ERROR_APPLICATION_NOT_FOUND;
                    notifyMsg(NotifyMsgType.Error, message, returnUrl);
                }
                else if (showMsg == ShowMsgType.NeedValidate.GetEnumStringValue())
                {
                    message = BizPortal.Resources.Global.MSG_ERROR_ACCOUNT_NEED_VALIDATE;
                    notifyMsg(NotifyMsgType.Warning, message, returnUrl);
                }
                /*else if (showMsg == ShowMsgType.NeedEmailAndPhone.GetEnumStringValue())
                {
                    message = "กรุณาระบุ Email และ เบอร์ติดต่อ";
                    notifyMsg(NotifyMsgType.Warning, message, returnUrl);
                }*/


                return RedirectToAction("Index");
            }

            if (User.Identity.IsAuthenticated)
            {
                var flashNews = DB.ArticleViews
                    .Where(o => o.CategorySysName == "Flash News"
                        && (o.StartPublishing == null || o.StartPublishing <= DateTime.Now)
                        && (o.FinishPublishing == null || o.FinishPublishing >= DateTime.Now))
                    .Select(o => new { o.ArticleID, o.ArticleBody, o.CreatedDate, o.UpdatedDate })
                    .FirstOrDefault();
                if (flashNews != null)
                {
                    var ts = flashNews.UpdatedDate != null ? (UnixTimestamp)flashNews.UpdatedDate.Value : (UnixTimestamp)flashNews.CreatedDate;
                    if (Request.Cookies["FNSHOWN"] == null || Request.Cookies["FNSHOWN"].Value != ts.ToString())
                    {
                        var cookie = new HttpCookie("FNSHOWN", ts.SecondsSinceEpoch.ToString());
                        cookie.Expires = DateTime.Today.AddDays(1);
                        Response.Cookies.Set(cookie);
                        //ViewBag.FlashNews = flashNews.ArticleBody;
                        return RedirectToAction("Article", "Home", new { area = string.Empty, id = flashNews.ArticleID });
                    }
                }
            }

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MaintenanceMessage"]))
            {
                var startDate = DateTime.ParseExact(ConfigurationManager.AppSettings["MaintenanceStartDate"], "yyyy-MM-dd hh:mm", CultureInfo.InvariantCulture);
                var endDate = DateTime.ParseExact(ConfigurationManager.AppSettings["MaintenanceEndDate"], "yyyy-MM-dd hh:mm", CultureInfo.InvariantCulture);
                var ts = DateTime.Today.ToString("yyyy-MM-dd");
                var now = DateTime.Now;

                bool.TryParse(ConfigurationManager.AppSettings["MaintenanceAlwayShow"], out bool isAlwayShow);

                if ((now >= startDate && now <= endDate)  && (Request.Cookies["MAINTENANCESHOWN"] == null || Request.Cookies["MAINTENANCESHOWN"].Value != ts) || isAlwayShow)
                {
                    var cookie = new HttpCookie("MAINTENANCESHOWN", ts);
                    cookie.Expires = DateTime.Today.AddDays(1);
                    Response.Cookies.Set(cookie);

                    notifyMsg(NotifyMsgType.Info, ConfigurationManager.AppSettings["MaintenanceMessage"], "");
                }
            }
            
            return View(model);
        }



        public ActionResult NeedProfile(string showMsg = null, string returnUrl = null)
        {

            ViewBag.CitizenOpenIDUrl= ConfigurationManager.AppSettings["CitizenOpenIDUrl"]+"Citizen/Account/Edit";
            return View();
        }

            public ActionResult Search(string SearchKeyword)
        {
            return RedirectToAction("Category", new { id = "", keyword = SearchKeyword, area = "" });
        }

        public ActionResult Category(int? id, string keyword = "", string tag = "")
        {
            var categories = DB.CategoryViews
                .Where(e => e.TwoLetterISOLanguageName == this.CurrentCulture && !e.Unlisted)
                .OrderBy(e => e.Ordering).ToDictionary(e => e.CategoryName, e => e.CategoryID);
            ViewBag.CategoryId = id;
            ViewBag.CategoryName = categories.FirstOrDefault(e => e.Value == id).Key;
            ViewBag.CategorySelectList = new SelectList(categories, "Value", "Key", id);
            ViewBag.SearchKeyword = keyword;
            ViewBag.SearchTag = tag;
            return View();
        }

        /*[HttpPost]
        public ActionResult ArticleList(ArticleSearchViewModel model)
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
                query = query.Where(o => o.ArticleName.Contains(model.SearchKeyword) || o.CategoryName.Contains(model.SearchKeyword)).AsQueryable();
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

            return Json(response);
        }*/

        public ActionResult Article(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var article = DB.ArticleViews.Where(e => e.ArticleID == id && e.Published == true && e.TwoLetterISOLanguageName == this.CurrentCulture).SingleOrDefault();

            if (article == null)
            {
                return HttpNotFound();
            }
            
            ArticleViewModel model = TypeAdapter.Adapt<ArticleView, ArticleViewModel>(article);
            //model.Tags = article.TagName;

            string[] tags = DB.ArticleTagMappings.Where(o => o.ArticleID == model.ArticleID && o.LanguageID == DB.CurrentLanguageID.Value).Select(o => o.Tag.TagName).ToArray();
            ViewBag.Tags = string.Join(",", tags);

            #region Check if this article link to external url
            string externalUrl = GetExternalUrl(model);
            if (!string.IsNullOrEmpty(externalUrl))
            {
                return Redirect(externalUrl);
            }
            #endregion

            if (model.NextArticleID.HasValue)
            {
                model.NextArticleName = DB.ArticleViews.Where(e => e.ArticleID == model.NextArticleID && e.TwoLetterISOLanguageName == this.CurrentCulture).Select(e => e.ArticleName).SingleOrDefault();
                if (string.IsNullOrEmpty(model.NextArticleName))
                {
                    model.NextArticleID = null;
                }
            }

            if (model.PreviousArticleID.HasValue)
            {
                model.PreviousArticleName = DB.ArticleViews.Where(e => e.ArticleID == model.PreviousArticleID && e.TwoLetterISOLanguageName == this.CurrentCulture).Select(e => e.ArticleName).SingleOrDefault();
                if (string.IsNullOrEmpty(model.PreviousArticleName))
                {
                    model.PreviousArticleName = null;
                }
            }

            ViewBag.CategoryId = article.CategoryID;
            ViewBag.CategoryName = article.CategoryName;
            ViewBag.Title = article.ArticleName;

            string juristic = "นิติบุคคล";
            if (CurrentCulture == "en")
                juristic = "Juristic";

            Dictionary<string, string> merges = new Dictionary<string, string>()
            {
                { "[[JURISTIC_NAME]]", juristic },
                { "[[LANGUAGE]]", CurrentCulture }
            };
            if (User.Identity.IsAuthenticated 
                && User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.UserType) == UserTypeEnum.Juristic.GetEnumStringValue())
            {
                merges["[[JURISTIC_NAME]]"] = User.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.FullName, juristic);
            }
            // Merge content
            foreach (var merge in merges)
            {
                //model.ArticleName = model.ArticleName.Replace(merge.Key, merge.Value);
                //model.ArticleIntroText = model.ArticleIntroText.Replace(merge.Key, merge.Value);
                model.ArticleBody = model.ArticleBody.Replace(merge.Key, merge.Value);
            }

            return View(model);
        }

        private string GetExternalUrl(ArticleViewModel article)
        {
            if (article.ArticleBody.StartsWith("#GOTO="))
            {
                string firstLine = article.ArticleBody.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0];
                return firstLine.Replace("#GOTO=", "");
            }
            return null;
        }
    }
}

