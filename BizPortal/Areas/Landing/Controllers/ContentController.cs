using BizPortal.Models;
using BizPortal.ViewModels;
using EGA.Owin.Security.EGAOpenID;
using EGA.Owin.Security.Utils.Extensions;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Areas.Landing.Controllers
{
    public class ContentController : BizPortal.Controllers.ControllerBase
    {
        // GET: Landing/Content
        public ActionResult View(int? id)
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
    }
}