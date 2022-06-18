using BizPortal.Models;
using BizPortal.ViewModels;
using BizPortal.ViewModels.Select2;
using Mapster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BizPortal.Areas.Manage.Controllers
{
    [Authorize(Roles = ConfigurationValues.ROLES_ADMIN_NAME + "," +
        ConfigurationValues.ROLES_OPDC_ADMIN_NAME + "," +
        ConfigurationValues.ROLES_MODULATOR_NAME)]
    public class ArticleController : ManageControllerBase
    {
        // GET: Manage/Article
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = PageNameBackendEnum.ARTICLE.GetEnumStringValue();
            return View();
        }

        public ActionResult Add()
        {
            List<SelectListItem> sections = DB.Sections.Where(o => !o.IsDeleted).OrderBy(o => o.SectionSysName).Select(o => new SelectListItem() { Value = o.SectionID.ToString(), Text = o.SectionSysName }).ToList();
            sections.Insert(0, new SelectListItem() { Value = string.Empty, Text = BizPortal.Resources.Global.DDL_SELECT });
            ViewBag.Sections = new SelectList(sections, "Value", "Text");

            List<SelectListItem> application = DB.Applications.Where(o => !o.IsDeleted).OrderBy(o => o.ApplicationSysName).Select(o => new SelectListItem() { Value = o.ApplicationID.ToString(), Text = o.ApplicationSysName }).ToList();
            application.Insert(0, new SelectListItem() { Value = string.Empty, Text = BizPortal.Resources.Application.APPLICATION_LIST_DEFAULT });
            ViewBag.Aapplication = new SelectList(application, "Value", "Text");

            List<SelectListItem> categories = new List<SelectListItem>();
            categories.Add(new SelectListItem() { Value = string.Empty, Text = BizPortal.Resources.Article.DDL_CATEGORY_DEFAULT });
            ViewBag.Categories = new SelectList(categories, "Value", "Text");

            ArticleViewModel model = new ArticleViewModel()
            {
                Published = true
            };
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Article article = TypeAdapter.Adapt<ArticleViewModel, Article>(model);
                    article.ArticleTranslations = new HashSet<ArticleTranslation>();

                    ArticleTranslation translate = new ArticleTranslation()
                    {
                        LanguageID = DB.CurrentLanguageID.Value
                    };
                    TypeAdapter.Adapt<ArticleViewModel, ArticleTranslation>(model, translate);
                    article.ArticleTranslations.Add(translate);

                    if (model.Tags != null)
                    {
                        string[] idString = model.Tags;
                        foreach (var item in idString)
                        {
                            Tag tag = null;
                            if (item.StartsWith("$new_"))
                            {
                                tag = new Tag();
                                tag.TagName = item.Replace("$new_", string.Empty);
                            }
                            else
                            {
                                int tagID = int.Parse(item);
                                tag = DB.Tags.Where(o => o.TagID == tagID).Single();
                            }

                            ArticleTagMapping mapping = new ArticleTagMapping();
                            mapping.Article = article;
                            mapping.Tag = tag;
                            mapping.LanguageID = DB.CurrentLanguageID.Value;
                            DB.ArticleTagMappings.Add(mapping);
                        }
                    }

                    if (!string.IsNullOrEmpty(model.ApplicationID))
                    {
                        string[] idString = model.ApplicationID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in idString)
                        {
                            Application app = null;

                            int appID = int.Parse(item);
                            app = DB.Applications.Where(o => o.ApplicationID == appID).Single();


                            ArticleApplicationMapping mappingApps = new ArticleApplicationMapping();
                            mappingApps.Application = app;
                            mappingApps.Article = article;
                            DB.ArticleApplicationMappings.Add(mappingApps);
                        }
                    }

                    DB.Articles.Add(article);
                    DB.SaveChanges();
                    notifyMsg(NotifyMsgType.Success, Resources.Article.MSG_ADD_SUCCESS);

                    return RedirectToAction("Edit", new { id = article.ArticleID });
                }
                catch (Exception ex)
                {
                    notifyMsg(NotifyMsgType.Success, Resources.Article.MSG_ADD_FAILED);
                }
            }

            ViewBag.Sections = new SelectList(GetSectionList(), "Value", "Text", model.SectionID);
            ViewBag.Aapplication = new SelectList(GetApplicationlist(), "Value", "Text", model.ApplicationID);
            ViewBag.Categories = new SelectList(GetCategoryList(), "Value", "Text", model.CategoryID);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            int idApp = DB.ArticleApplicationMappings.Where(o => o.ArticleID == id).Select(o => o.ApplicationID).FirstOrDefault();
            ArticleViewModel model = DB.Articles
                .Where(o => o.ArticleID == id && !o.IsDeleted)
                .Select(o => new ArticleViewModel()
                {
                    ArticleID = o.ArticleID,
                    ArticleSysName = o.ArticleSysName,
                    Unlisted = o.Unlisted,
                    SectionID = o.SectionID,
                    CategoryID = o.CategoryID,
                    ApplicationID = idApp.ToString(),
                    StartPublishing = o.StartPublishing,
                    FinishPublishing = o.FinishPublishing,
                    Published = o.Published,
                    ThumbnailID = o.ThumbnailID,
                    PreviousArticleID = o.PreviousArticleID,
                    NextArticleID = o.NextArticleID
                }).SingleOrDefault();


            if (model == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var tran = DB.ArticleTranslations.Where(o => o.ArticleID == id && o.LanguageID == DB.CurrentLanguageID)
                .Select(o => new { o.ArticleName, o.ArticleBody, o.ArticleIntroText }).SingleOrDefault();
            if (tran != null)
            {
                model.ArticleName = tran.ArticleName;
                model.ArticleBody = tran.ArticleBody;
                model.ArticleIntroText = tran.ArticleIntroText;
            }

            if (model.PreviousArticleID != null)
            {
                ViewBag.PreviousArticleSysName = DB.Articles.Where(o => o.ArticleID == model.PreviousArticleID.Value).Select(o => o.ArticleSysName).Single();
            }
            if (model.NextArticleID != null)
            {
                ViewBag.NextArticleSysName = DB.Articles.Where(o => o.ArticleID == model.NextArticleID.Value).Select(o => o.ArticleSysName).Single();
            }

            ViewBag.Sections = new SelectList(GetSectionList(), "Value", "Text", model.SectionID);
            ViewBag.Aapplication = new SelectList(GetApplicationlist(), "Value", "Text", model.ApplicationID);
            ViewBag.Categories = new SelectList(GetCategoryList(), "Value", "Text", model.CategoryID);

            ViewBag.Tags = JsonConvert.SerializeObject(DB.ArticleTagMappings.Where(o => o.ArticleID == model.ArticleID && o.LanguageID == DB.CurrentLanguageID.Value).Select(o => new Select2Opt() { ID = o.TagID.ToString(), Text = o.Tag.TagName }).ToArray());
            ViewBag.App = JsonConvert.SerializeObject(DB.ArticleApplicationMappings.Where(o => o.ArticleID == model.ArticleID).Select(o => new Select2Opt() { ID = o.ApplicationID.ToString(), Text = o.Application.ApplicationSysName }).ToArray()); ;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    Article article = DB.Articles.Where(o => o.ArticleID == id && !o.IsDeleted).SingleOrDefault();


                    if (article == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }


                    TypeAdapter.Adapt<ArticleViewModel, Article>(model, article);
                    DB.Entry(article).State = System.Data.Entity.EntityState.Modified;

                    List<ArticleApplicationMapping> articleApp = DB.ArticleApplicationMappings.Where(o => o.ArticleID == id).ToList();
                    if (articleApp != null)
                    {
                        foreach (var app in articleApp)
                        {
                            DB.ArticleApplicationMappings.Remove(app);
                        }

                    }

                    ArticleTranslation translate = DB.ArticleTranslations.Where(o => o.ArticleID == id && o.LanguageID == DB.CurrentLanguageID.Value).SingleOrDefault();

                    if (translate == null)
                    {
                        translate = new ArticleTranslation();
                        article.ArticleTranslations = new HashSet<ArticleTranslation>();
                        article.ArticleTranslations.Add(translate);
                        translate.LanguageID = DB.CurrentLanguageID.Value;
                    }
                    else
                    {
                        DB.Entry(translate).State = System.Data.Entity.EntityState.Modified;
                    }

                    TypeAdapter.Adapt<ArticleViewModel, ArticleTranslation>(model, translate);

                    List<ArticleTagMapping> oldMappings = DB.ArticleTagMappings.Where(o => o.ArticleID == model.ArticleID && o.LanguageID == DB.CurrentLanguageID.Value).ToList();
                    DB.ArticleTagMappings.RemoveRange(oldMappings);

                    if (model.Tags != null)
                    {
                        string[] idString = model.Tags;
                        foreach (var item in idString)
                        {
                            Tag tag = null;
                            if (item.StartsWith("$new_"))
                            {
                                tag = new Tag();
                                tag.TagName = item.Replace("$new_", string.Empty);
                            }
                            else
                            {
                                int tagID = int.Parse(item);
                                tag = DB.Tags.Where(o => o.TagID == tagID).Single();
                            }

                            ArticleTagMapping mapping = new ArticleTagMapping();
                            mapping.Article = article;
                            mapping.LanguageID = DB.CurrentLanguageID.Value;
                            mapping.Tag = tag;
                            DB.ArticleTagMappings.Add(mapping);
                        }
                    }

                    if (!string.IsNullOrEmpty(model.ApplicationID))
                    {
                        string[] idString = model.ApplicationID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in idString)
                        {
                            Application app = null;
                            int appID = int.Parse(item);
                            app = DB.Applications.Where(o => o.ApplicationID == appID).Single();

                            ArticleApplicationMapping newMapping = new ArticleApplicationMapping
                            {
                                ArticleID = model.ArticleID.Value,
                                ApplicationID = appID
                            };

                            DB.ArticleApplicationMappings.Add(newMapping);

                        }
                    }
                    DB.SaveChanges();

                    notifyMsg(NotifyMsgType.Success, Resources.Article.MSG_EDIT_SUCCESS);

                }
                catch (Exception ex)
                {
                    notifyMsg(NotifyMsgType.Error, Resources.Article.MSG_EDIT_FAILED);
                }

                return RedirectToAction("Edit", new { id = id });
            }

            ViewBag.Sections = new SelectList(GetSectionList(), "Value", "Text", model.SectionID);
            ViewBag.Aapplication = new SelectList(GetApplicationlist(), "Value", "Text", model.ApplicationID);
            ViewBag.Categories = new SelectList(GetCategoryList(), "Value", "Text", model.CategoryID);

            return View(model);
        }
    }
}