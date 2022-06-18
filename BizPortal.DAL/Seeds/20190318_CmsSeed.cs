using BizPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;

namespace BizPortal.DAL.Seeds
{
    public class _20190318_CmsSeed
    {
        public static void Seed(BizPortal.DAL.ApplicationDbContext context, ApplicationUser adminUser)
        {
            //List<Section> sections = new List<Section>()
            //            {
            //                new Section()
            //                {
            //                    SectionSysName = "Business Section",
            //                    Published = true,
            //                    Ordering = 1,
            //                    CreatedUserID = adminUser.Id,
            //                    CreatedDate = DateTime.Now
            //                },
            //                new Section()
            //                {
            //                    SectionSysName = "Portal Section",
            //                    Published = true,
            //                    Ordering = 0,
            //                    CreatedUserID = adminUser.Id,
            //                    CreatedDate = DateTime.Now
            //                }
            //            };
            //context.Sections.AddOrUpdate(o => o.SectionSysName, sections.ToArray());
            //context.SaveChanges(false);

            //List<SectionTranslation> sectionTrans = new List<SectionTranslation>()
            //{
            //    new SectionTranslation()
            //    {
            //        LanguageID = languages.Where(o => o.TwoLetterISOLanguageName == "th").Select(o => o.LanguageID).Single(),
            //        SectionID = sections.Where(o => o.SectionSysName == "Business Section").Select(o => o.SectionID).Single(),
            //        SectionName = "หมวดธุรกิจ"
            //    },
            //    new SectionTranslation()
            //    {
            //        LanguageID = languages.Where(o => o.TwoLetterISOLanguageName == "en").Select(o => o.LanguageID).Single(),
            //        SectionID = sections.Where(o => o.SectionSysName == "Business Section").Select(o => o.SectionID).Single(),
            //        SectionName = "Business Section"
            //    },

            //    new SectionTranslation()
            //    {
            //        LanguageID = languages.Where(o => o.TwoLetterISOLanguageName == "th").Select(o => o.LanguageID).Single(),
            //        SectionID = sections.Where(o => o.SectionSysName == "Portal Section").Select(o => o.SectionID).Single(),
            //        SectionName = "หมวด Portal"
            //    },
            //    new SectionTranslation()
            //    {
            //        LanguageID = languages.Where(o => o.TwoLetterISOLanguageName == "en").Select(o => o.LanguageID).Single(),
            //        SectionID = sections.Where(o => o.SectionSysName == "Portal Section").Select(o => o.SectionID).Single(),
            //        SectionName = "Portal Section"
            //    }
            //};
            //context.SectionTranslations.AddOrUpdate(o => new { o.SectionID, o.LanguageID }, sectionTrans.ToArray());
            //context.SaveChanges(false);

            //List<Models.FileUpload> cateThumbnails = new List<Models.FileUpload>()
            //{
            //    new Models.FileUpload()
            //    {
            //        FileSysName = "menu_01.jpg",
            //        FileName = "menu_01.jpg",
            //        AbsolutePath = "~/Uploads/category-thumbnail/default/menu_01.jpg",
            //        ContentType = "image/jpeg",
            //        CreatedUserID = adminUser.Id,
            //        CreatedDate = DateTime.Now
            //    },
            //    new Models.FileUpload()
            //    {
            //        FileSysName = "menu_02.jpg",
            //        FileName = "menu_02.jpg",
            //        AbsolutePath = "~/Uploads/category-thumbnail/default/menu_02.jpg",
            //        ContentType = "image/jpeg",
            //        CreatedUserID = adminUser.Id,
            //        CreatedDate = DateTime.Now
            //    },
            //    new Models.FileUpload()
            //    {
            //        FileSysName = "menu_03.jpg",
            //        FileName = "menu_03.jpg",
            //        AbsolutePath = "~/Uploads/category-thumbnail/default/menu_03.jpg",
            //        ContentType = "image/jpeg",
            //        CreatedUserID = adminUser.Id,
            //        CreatedDate = DateTime.Now
            //    },
            //    new Models.FileUpload()
            //    {
            //        FileSysName = "menu_04.jpg",
            //        FileName = "menu_04.jpg",
            //        AbsolutePath = "~/Uploads/category-thumbnail/default/menu_04.jpg",
            //        ContentType = "image/jpeg",
            //        CreatedUserID = adminUser.Id,
            //        CreatedDate = DateTime.Now
            //    },
            //    new Models.FileUpload()
            //    {
            //        FileSysName = "menu_05.jpg",
            //        FileName = "menu_05.jpg",
            //        AbsolutePath = "~/Uploads/category-thumbnail/default/menu_05.jpg",
            //        ContentType = "image/jpeg",
            //        CreatedUserID = adminUser.Id,
            //        CreatedDate = DateTime.Now
            //    }
            //};
            //context.FileUploads.AddOrUpdate(o => o.FileSysName, cateThumbnails.ToArray());
            //context.SaveChanges(false);


            var sections = context.Sections.Where(e => !e.IsDeleted).ToList();
            var languages = context.Languages.ToList();

            var categories = new List<Category>
            {
                new Category
                {
                    CategorySysName = "Thailand Informations",
                    SectionID = sections.Where(e => e.SectionSysName == "Business Section").Select(e=>e.SectionID).FirstOrDefault(),
                    Published = true,
                    Ordering = 1,
                    CreatedUserID = adminUser.Id,
                    CreatedDate = DateTime.Now
                }
            };

            context.Categories.AddOrUpdate(e => new { e.CategorySysName }, categories.ToArray());
            context.SaveChanges(false);

            var categoryTranslations = new List<CategoryTranslation>
            {
                new CategoryTranslation
                {
                    CategoryID = categories.Where(e=>e.CategorySysName == "Thailand Informations").Select(e=>e.CategoryID).FirstOrDefault(),
                    CategoryName = "ภาพรวมประเทศไทย",
                    LanguageID = languages.Where(o => o.TwoLetterISOLanguageName == "th").Select(o => o.LanguageID).FirstOrDefault(),
                    Description = "ภาพรวมประเทศไทย",
                },
                new CategoryTranslation
                {
                    CategoryID = categories.Where(e=>e.CategorySysName == "Thailand Informations").Select(e=>e.CategoryID).FirstOrDefault(),
                    CategoryName = "Thailand Informations",
                    LanguageID = languages.Where(o => o.TwoLetterISOLanguageName == "en").Select(o => o.LanguageID).FirstOrDefault(),
                    Description ="Thailand Informations",
                }
            };

            context.CategoryTranslations.AddOrUpdate(e => new { e.CategoryName, e.LanguageID }, categoryTranslations.ToArray());
            context.SaveChanges(false);
        }
    }
}
