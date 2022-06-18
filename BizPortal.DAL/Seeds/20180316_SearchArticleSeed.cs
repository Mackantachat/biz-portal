using BizPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.Seeds
{
    public class _20180316_SearchArticleSeed
    {
        static ApplicationUser _creator = null;
        public static void Seed(BizPortal.DAL.ApplicationDbContext context, ApplicationUser creator)
        {
            _creator = creator;
            var aList = new List<Article>();

            Dictionary<string, Tag> cacheTags = new Dictionary<string, Tag>();
            ArticleTranslation trans = null;
            Article article = null;
            string tags = "";

            var section = context.Sections.Where(s => s.SectionSysName == "Business Section").First();
            var category = context.Categories.Where(c => c.CategorySysName == "Informations").First();
            
            #region Initialize logo files

            var fList = new FileUpload[]
            {
                createFileUpload("article_retail.png", "image/png"),
                createFileUpload("article_restaurant.png", "image/png"),
                createFileUpload("article_utilities.png", "image/png"),
                createFileUpload("article_starting_business.png", "image/png"),
                createFileUpload("article_retail_page.png", "image/png"),
                createFileUpload("article_restaurant_page.png", "image/png"),
                createFileUpload("article_business_landing.png", "image/png"),
            };

            // Clear existing images            
            //List<string> filenames = fList.Select(f => f.FileSysName).ToList();
            //var existingFiles = context.FileUploads.Where(o => filenames.Contains(o.FileSysName)).ToList();
            //foreach(var f in existingFiles)
            //{
            //    var ar = context.Articles.FirstOrDefault(o => o.ThumbnailID == f.FileUploadID);
            //    if (ar != null) ar.Thumbnail = null;

            //    context.FileUploads.Remove(f);
            //}            
            //context.SaveChanges(false);

            context.FileUploads.AddOrUpdate(a => a.FileSysName, fList);
            context.SaveChanges(false);

            #endregion

            #region Article "Landing"            
            article = new Article
            {
                ArticleSysName = "business landing",
                Unlisted = false,
                SectionID = section.SectionID,
                CategoryID = category.CategoryID,
                FinishPublishing = null,
                Reads = 0,
                Published = true,

                PreviousArticleID = null,
                NextArticleID = null,

                CreatedUserID = creator.Id,
                CreatedDate = DateTime.Now,

                Thumbnail = context.FileUploads.First(o => o.FileSysName == "article_business_landing.png")
            };

            trans = new ArticleTranslation();
            trans.Article = article;
            trans.ArticleBody = "#GOTO=~/th/Business";
            trans.ArticleIntroText = "แหล่งรวมข้อมูลที่เป็นประโยชน์ต่อผู้ที่ต้องการขอใบอนุญาต เช่น ข้อมูลรายประเภทธุรกิจ ข้อมูลการติดต่อราชการตามวงจรธุรกิจ ตลอดจนหัวข้องานบริการที่มีคนเรียกดูมากที่สุด เป็นต้น";
            trans.ArticleName = "ก่อนเริ่มต้นธุรกิจคุณจำเป็นต้องติดต่อราชการเรื่องใดบ้าง";
            trans.LanguageID = 1;

            article.ArticleTranslations = new HashSet<ArticleTranslation>() { trans };

            tags = "รายประเภท, ประเภทธุรกิจ, อาหาร, ค้าปลีก, กาแฟ, Co Working, โรงแรม, รีสอร์ท, เริ่มต้นธุรกิจ, ก่อสร้าง, ขอน้ำ, ขอไฟ, สาธารณูปโภค, จ้างงาน, จดทะเบียนทรัพย์สิน, ที่ดิน, ภาษี, สินเชื่อ, ส่งออก, นำเข้า, BOI, สิทธิบัตร, ผู้ถือหุ้น, ยื่นฟ้อง, ปิดกิจการ, ล้มละลาย";
            AddArticleTags(context, article, tags, cacheTags);

            aList.Add(article);
            #endregion

            #region Article "Restaurant"            
            article = new Article
            {
                ArticleSysName = "restaurant page",
                Unlisted = false,
                SectionID = section.SectionID,
                CategoryID = category.CategoryID,
                FinishPublishing = null,
                Reads = 0,
                Published = true,

                PreviousArticleID = null,
                NextArticleID = null,

                CreatedUserID = creator.Id,
                CreatedDate = DateTime.Now,

                Thumbnail = context.FileUploads.First(o => o.FileSysName == "article_restaurant_page.png")
            };

            trans = new ArticleTranslation();
            trans.Article = article;
            trans.ArticleBody = "#GOTO=~/th/Business/Restaurant";
            trans.ArticleIntroText = "ธุรกิจร้านอาหาร คือ ธุรกิจที่มีการทำอาหาร เครื่องดื่ม ไอศกรีม ขนมหรือเค้ก โดยมีการขายและ ให้บริการ เช่น เสิร์ฟให้กินที่ร้าน ตักเองจากชั้นวาง ห่อ หรือสั่งให้จัดส่งถึงบ้าน ทั้งนี้ไม่รวมโรงงานผลิตอาหารที่มีเครื่องจักรอุตสาหกรรม";
            trans.ArticleName = "ธุรกิจร้านอาหารคืออะไร";
            trans.LanguageID = 1;

            article.ArticleTranslations = new HashSet<ArticleTranslation>() { trans };

            tags = "รายประเภท, อาหาร, เครื่องดื่ม, ไอศครีม, ไอศกรีม, ไอติม, ขนม, เค้ก, ร้านเค้ก, ข้าวแกง, ตามสั่ง, ซื้อกลับบ้าน, ร้านอาหาร, กับข้าว, สะสมอาหาร, ของกิน, ของหวาน, ของคาว, ก๋วยเตี๋ยว, เหล้า, คาราโอเกะ, ข้าวราดแกง";
            AddArticleTags(context, article, tags, cacheTags);

            aList.Add(article);
            #endregion

            #region Article "retail page"            
            article = new Article
            {
                ArticleSysName = "retail page",
                Unlisted = false,
                SectionID = section.SectionID,
                CategoryID = category.CategoryID,
                FinishPublishing = null,
                Reads = 0,
                Published = true,

                PreviousArticleID = null,
                NextArticleID = null,

                CreatedUserID = creator.Id,
                CreatedDate = DateTime.Now,

                Thumbnail = context.FileUploads.First(o => o.FileSysName == "article_retail_page.png")
            };

            trans = new ArticleTranslation();
            trans.Article = article;
            trans.ArticleBody = "#GOTO=~/th/Business/Retail";
            trans.ArticleIntroText = "ธุรกิจที่มีการขายสินค้าเพื่อให้ผู้ซื้อนำไปอุปโภคบริโภคโดยตรง และผู้ขายไม่ได้ผลิตหรือแปรรูปสินค้านั้นเอง";
            trans.ArticleName = "ธุรกิจร้านค้าปลีกคืออะไร​";
            trans.LanguageID = 1;

            article.ArticleTranslations = new HashSet<ArticleTranslation>() { trans };

            tags = "ขายตรง, ตัวแทน, ตลาดแบบตรง, ขายผ่านเน็ต, อินเตอร์เน็ต, ขายของ, สินค้า, ออนไลน์, อินเตอร์เน็ท, Internet, ค้าปลึก, แพ็ค, ค้าขาย, โชว์ห่วย, โชห่วย, ซื้อมาขายไป, ขายปลีก, ขายของออนไลน์, ร้านค้าออนไลน์";
            AddArticleTags(context, article, tags, cacheTags);

            aList.Add(article);
            #endregion
            
            #region Article "Permit Selection: Starting Business"            
            article = new Article
            {
                ArticleSysName = "Permit Selection: Starting Business",
                Unlisted = false,
                SectionID = section.SectionID,
                CategoryID = category.CategoryID,
                FinishPublishing = null,
                Reads = 0,
                Published = true,

                PreviousArticleID = null,
                NextArticleID = null,

                CreatedUserID = creator.Id,
                CreatedDate = DateTime.Now,

                Thumbnail = context.FileUploads.First(o => o.FileSysName == "article_starting_business.png")
            };

            trans = new ArticleTranslation();
            trans.Article = article;
            trans.ArticleBody = "#GOTO=~/th/Business/PermitList#starting-business";
            trans.ArticleIntroText = "รวบรวมรายการใบอนุญาตที่สำคัญ สำหรับผู้ที่ต้องการเริ่มต้นธุรกิจ โดยสามารถเลือกขอใบอนุญาตที่ต้องการได้ทันที เช่น ขอจดทะเบียนนิติบุคคล ขอขึ้นทะเบียนนายจ้างลูกจ้าง เป็นต้น";
            trans.ArticleName = "รายการใบอนุญาตที่สำคัญ สำหรับผู้ที่ต้องการเริ่มต้นธุรกิจ";
            trans.LanguageID = 1;

            article.ArticleTranslations = new HashSet<ArticleTranslation>() { trans };

            tags = "ทะเบียนนิติบุคคล, ทะเบียนพานิชย์, ทะเบียนนายจ้าง, ทะเบียนภาษีมูลค่าเพิ่ม";
            AddArticleTags(context, article, tags, cacheTags);

            aList.Add(article);
            #endregion
            
            #region Article "Permit Selection: utilities"            
            article = new Article
            {
                ArticleSysName = "Permit Selection: utilities",
                Unlisted = false,
                SectionID = section.SectionID,
                CategoryID = category.CategoryID,
                FinishPublishing = null,
                Reads = 0,
                Published = true,

                PreviousArticleID = null,
                NextArticleID = null,

                CreatedUserID = creator.Id,
                CreatedDate = DateTime.Now,

                Thumbnail = context.FileUploads.First(o => o.FileSysName == "article_utilities.png")
            };

            trans = new ArticleTranslation();
            trans.Article = article;
            trans.ArticleBody = "#GOTO=~/th/Business/PermitList#utilities";
            trans.ArticleIntroText = "รวบรวมรายการใบอนุญาตที่จำเป็นต้องขอ สำหรับผู้ที่ต้องการขอติดตั้งสาธารณูปโภค อาทิ ขอติดตั้งไฟฟ้า ประปา โทรศัพท์ และ อินเทอร์เน็ต เป็นต้น โดยสามารถทำการเลือกขอใบอนุญาตผ่านระบบได้ทันที";
            trans.ArticleName = "รายการใบอนุญาตที่จำเป็นต้องขอ สำหรับผู้ที่ต้องการขอติดตั้งสาธารณูปโภค";
            trans.LanguageID = 1;

            article.ArticleTranslations = new HashSet<ArticleTranslation>() { trans };

            tags = "ไฟฟ้า, น้ำประปา, โทรศัพท์, อินเทอร์เน็ต";
            AddArticleTags(context, article, tags, cacheTags);

            aList.Add(article);
            #endregion
            
            #region Article "Permit Selection: restaurant"            
            article = new Article
            {
                ArticleSysName = "Permit Selection: restaurant",
                Unlisted = false,
                SectionID = section.SectionID,
                CategoryID = category.CategoryID,
                FinishPublishing = null,
                Reads = 0,
                Published = true,

                PreviousArticleID = null,
                NextArticleID = null,

                CreatedUserID = creator.Id,
                CreatedDate = DateTime.Now,

                Thumbnail = context.FileUploads.First(o => o.FileSysName == "article_restaurant.png")
            };

            trans = new ArticleTranslation();
            trans.Article = article;
            trans.ArticleBody = "#GOTO=~/th/Business/PermitList#restaurant";
            trans.ArticleIntroText = "เหมาะสำหรับผู้ที่ต้องการขอใบอนุญาตประกอบธุรกิจร้านอาหาร โดยสามารถเลือกขอใบอนุญาตผ่านระบบที่ต้องการได้มากกว่า 1 ใบอนุญาตในครั้งเดียว หรือ หากไม่แน่ใจว่าธุรกิจของท่านต้องขอใบอนุญาตอะไรบ้าง สามารถใช้ Smart Quiz เพื่อให้ระบบช่วยแนะนำใบอนุญาตที่เกี่ยวข้องกับธุรกิจร้านอาหารของท่าน พร้อมทำการขออนุญาตผ่านระบบได้ทันที";
            trans.ArticleName = "ใบอนุญาตประกอบธุรกิจร้านอาหาร";
            trans.LanguageID = 1;

            article.ArticleTranslations = new HashSet<ArticleTranslation>() { trans };

            tags = "สะสมอาหาร, อันตรายต่อสุขภาพ, สุรา, ยาสูบ, สถานบริการ, อาหาร, เครื่องดื่ม, บริการ, วีดิทัศน์, คาราโอเกะ";
            AddArticleTags(context, article, tags, cacheTags);

            aList.Add(article);
            #endregion
            
            #region Article "Permit Selection: retail"            
            article = new Article
            {
                ArticleSysName = "Permit Selection: retail",
                Unlisted = false,
                SectionID = section.SectionID,
                CategoryID = category.CategoryID,
                FinishPublishing = null,
                Reads = 0,
                Published = true,

                PreviousArticleID = null,
                NextArticleID = null,

                CreatedUserID = creator.Id,
                CreatedDate = DateTime.Now,

                Thumbnail = context.FileUploads.First(o => o.FileSysName == "article_retail.png")
            };

            trans = new ArticleTranslation();
            trans.Article = article;
            trans.ArticleBody = "#GOTO=~/th/Business/PermitList#retail";
            trans.ArticleIntroText = "เหมาะสำหรับผู้ที่ต้องการขอใบอนุญาตประกอบธุรกิจร้านค้าปลีก โดยสามารถเลือกขอใบอนุญาตผ่านระบบที่ต้องการได้มากกว่า 1 ใบอนุญาตในครั้งเดียว หรือ หากไม่แน่ใจว่าธุรกิจของท่านต้องขอใบอนุญาตอะไรบ้าง สามารถใช้ Smart Quiz เพื่อให้ระบบช่วยแนะนำใบอนุญาตที่เกี่ยวข้องกับธุรกิจร้านค้าปลีกของท่าน พร้อมทำการขออนุญาตผ่านระบบได้ทันที";
            trans.ArticleName = "ใบอนุญาตประกอบธุรกิจร้านค้าปลีก";
            trans.LanguageID = 1;

            article.ArticleTranslations = new HashSet<ArticleTranslation>() { trans };

            tags = "สะสมอาหาร, สุรา, ยาสูบ, สถานบริการ, อาหารสัตว์, สัตว์, ซากสัตว์, ขายตรง, ตลาดแบบตรง, ปุ๋ย, เมล็ดพันธุ์, วีดิทัศน์, ยาแผนปัจจุบัน, เครื่องสำอาง, ภาพยนตร์";
            AddArticleTags(context, article, tags, cacheTags);

            aList.Add(article);
            #endregion

            
            foreach(var ar in aList)
            {
                // Remove existing article first
                var objArList = context.Articles.Where(o => o.ArticleSysName == ar.ArticleSysName).ToList();
                foreach(var objAr in objArList)
                {
                    if (objAr != null)
                    {
                        context.ArticleTranslations.RemoveRange(context.ArticleTranslations.Where(o => o.ArticleID == objAr.ArticleID));
                        context.ArticleTagMappings.RemoveRange(context.ArticleTagMappings.Where(o => o.ArticleID == objAr.ArticleID));
                        context.Articles.Remove(objAr);
                    }
                }                
            }
            
            context.Articles.AddRange(aList);
            context.SaveChanges(false);

        }


        private static void AddArticleTags(BizPortal.DAL.ApplicationDbContext context, Article article, string tags, Dictionary<string, Tag> cacheTags)
        {
            if (tags == null) tags = "";
            string[] splitted = tags.Split(',');

            Tag tag = null;
            foreach(string t in splitted)
            {
                string strTag = t.Trim();
                if (string.IsNullOrEmpty(strTag)) continue;

                // Look in cache first
                tag = cacheTags.ContainsKey(strTag) ? cacheTags[strTag] : null;
                if (tag == null)
                {
                    // Then look in database
                    tag = context.Tags.FirstOrDefault(o => o.TagName == strTag);
                    if (tag == null)
                    {
                        // This is a new tag
                        tag = new Tag() { TagName = strTag, CreatedUserID = _creator.Id, CreatedDate = DateTime.Now };
                    }

                    cacheTags.Add(tag.TagName, tag);
                }

                ArticleTagMapping mapping = new ArticleTagMapping();
                mapping.Article = article;
                mapping.Tag = tag;
                mapping.LanguageID = 1;
                context.ArticleTagMappings.Add(mapping);
            }
        }

        private static FileUpload createFileUpload(string filename, string contentType)
        {
            return new FileUpload
            {
                FileSysName = filename,
                FileName = filename,
                AbsolutePath = "~/Uploads/apps/logos/" + filename,
                ContentType = contentType,
                ContentLength = 111,
                TemporaryStatus = false,
                CreatedDate = DateTime.Now,
                CreatedUserID = _creator.Id,
                UpdatedUserID = _creator.Id,
                UpdatedDate = DateTime.Now,
            };
        }
    }
}
