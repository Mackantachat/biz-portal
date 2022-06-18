using BizPortal.Utils.JsonConverter;
using BizPortal.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BizPortal.ViewModels
{
    [Bind(Include = "ArticleID, ArticleSysName, ArticleName, ArticleBody, ArticleIntroText, Unlisted, SectionID, CategoryID, ApplicationID, StartPublishing, FinishPublishing, PreviousArticleID, NextArticleID, Tags, Published, Ordering, ThumbnailID")]
    public class ArticleViewModel : CmsViewModel
    {
        public int? ArticleID { get; set; }

        [Required, StringLength(450)]
        [Display(Name = "ARTICLE_SYS_NAME", ResourceType = typeof(Resources.Article))]
        public string ArticleSysName { get; set; }

        [Required, StringLength(450)]
        [Display(Name = "ARTICLE_NAME", ResourceType = typeof(Resources.Article))]
        public string ArticleName { get; set; }

        [Display(Name = "ARTICLE_BODY", ResourceType = typeof(Resources.Article))]
        public string ArticleBody { get; set; }

        [StringLength(1000)]
        [Display(Name = "ARTICLE_INTRO_TEXT", ResourceType = typeof(Resources.Article))]
        public string ArticleIntroText { get; set; }

        [Display(Name = "ARTICLE_UNLIST", ResourceType = typeof(Resources.Article))]
        /// <summary>
        /// ไม่ถูกแสดงในลิสต์
        /// </summary>
        public bool Unlisted { get; set; }

        [Required]
        [Display(Name = "SECTION_NAME", ResourceType = typeof(Resources.Section))]
        public int? SectionID { get; set; }

        [Required]
        [Display(Name = "CATEGORY_NAME", ResourceType = typeof(Resources.Category))]
        public int? CategoryID { get; set; }


        [Display(Name = "APPLICATION_NAME", ResourceType = typeof(Resources.Application))]
        public string ApplicationID { get; set; }

        [Display(Name = "ARTICLE_START_PUBLISHING", ResourceType = typeof(Resources.Article))]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm tt", ApplyFormatInEditMode = true)]
        public DateTime? StartPublishing { get; set; }

        [Display(Name = "ARTICLE_FINISH_PUBLISHING", ResourceType = typeof(Resources.Article))]
        public DateTime? FinishPublishing { get; set; }

        public int Reads { get; set; }

        [Display(Name = "ARTICLE_PREVIOUS", ResourceType = typeof(Resources.Article))]
        public int? PreviousArticleID { get; set; }

        public string PreviousArticleName { get; set; }

        [Display(Name = "ARTICLE_NEXT", ResourceType = typeof(Resources.Article))]
        public int? NextArticleID { get; set; }

        public string NextArticleName { get; set; }

        public string[] Tags { get; set; }


        #region [Display Only]
        public string SectionSysName { get; set; }

        public string CategorySysName { get; set; }
        #endregion
    }
}
