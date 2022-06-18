
using ExpressiveAnnotations.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BizPortal.ViewModels
{
    public class ApplicationViewModel
    {
        public int? ApplicationID { get; set; }

        [Required, StringLength(450)]
        [Display(Name = "APPLICATION_SYS_NAME", ResourceType = typeof(Resources.Application))]
        public string ApplicationSysName { get; set; }

        [Required(AllowEmptyStrings = false), StringLength(450)]
        [Display(Name = "ORG_CODE", ResourceType = typeof(Resources.Application))]
        public string OrgCode { get; set; }

        [StringLength(1000)]
        [Display(Name = "APPLICATION_URL", ResourceType = typeof(Resources.Application))]
        public string ApplicationUrl { get; set; }

        [StringLength(1000)]
        [Display(Name = "APPLICATION_URL", ResourceType = typeof(Resources.Application))]
        public string CitizenApplicationUrl { get; set; }

        [Display(Name = "APPLICATION_CONSUMER_KEY", ResourceType = typeof(Resources.Application))]
        public Guid? ConsumerKey { get; set; }

        [Display(Name = "APPLICATION_SINGLE_FORM_ENABLED", ResourceType = typeof(Resources.Application))]
        public bool SingleFormEnabled { get; set; }

        /// <summary>
        /// ลิ้งค์เชื่อมโยงไปยังหน้า INFO.GO.TH
        /// </summary>
        [Display(Name = "APPLICATION_HANDBOOK_URL", ResourceType = typeof(Resources.Application))]
        public string HandbookUrl { get; set; }

        /// <summary>
        /// ลิ้งค์เชื่อมโยงไปยังหน้า INFO.GO.TH
        /// </summary>
        [Display(Name = "APPLICATION_HANDBOOK_URL", ResourceType = typeof(Resources.Application))]
        public string CitizenHandbookUrl { get; set; }

        /// <summary>
        /// โลโก้ของ App
        /// </summary>
        [Display(Name = "APPLICATION_LOGO", ResourceType = typeof(Resources.Application))]
        public int? LogoFileID { get; set; }

        /// <summary>
        /// จำนวนวันทำการ
        /// </summary>
        [Display(Name = "APPLICATION_OPERATING_DAYS", ResourceType = typeof(Resources.Application))]
        [RequiredIf("OperatingDayType != null", ErrorMessageResourceName = "VAL_APPLICATION_OPERATING_DAY_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]
        public int? OperatingDays { get; set; }

        /// <summary>
        /// จำนวนวันทำการ
        /// </summary>
        [Display(Name = "APPLICATION_OPERATING_DAYS", ResourceType = typeof(Resources.Application))]
        [RequiredIf("CitizenOperatingDayType != null", ErrorMessageResourceName = "VAL_APPLICATION_OPERATING_DAY_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]
        public int? CitizenOperatingDays { get; set; }

        public Guid? FileUploadRefCode { get; set; }

        public string FileUploadName { get; set; }

        [Display(Name = "LABEL_OPERATION_COST_TYPE", ResourceType = typeof(Resources.Application))]
        public string OperatingCostType { get; set; }


        [Display(Name = "LABEL_OPERATION_DAY_TYPE", ResourceType = typeof(Resources.Application))]
        public string OperatingDayType { get; set; }

        /// <summary>
        /// ค่าใช้จ่าย
        /// </summary>
        [Display(Name = "APPLICATION_OPERATING_COST", ResourceType = typeof(Resources.Application))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "###0.00")]
        [RequiredIf("OperatingCostType != null", ErrorMessageResourceName = "VAL_APPLICATION_OPERATING_COST_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]
        public decimal? OperatingCost { get; set; }


        [Display(Name = "LABEL_SHOW_REMARK", ResourceType = typeof(Resources.Application))]
        public bool ShowRemark { get; set; }

        [Display(Name = "LABEL_REMARK", ResourceType = typeof(Resources.Application))]
        [RequiredIf("ShowRemark == true", ErrorMessageResourceName = "VAL_REMARK_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]
        public string Remark { get; set; }

        /// <summary>
        /// ค่าใช้จ่าย 2
        /// </summary>
        [Display(Name = "APPLICATION_OPERATING_COST2", ResourceType = typeof(Resources.Application))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "###0.00")]
        [RequiredIf("OperatingCostType == 'Range'", ErrorMessageResourceName = "VAL_APPLICATION_OPERATING_COST2_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]
        [AssertThat("OperatingCostType != 'Range' || OperatingCost2 > OperatingCost", ErrorMessageResourceName = "VAL_COST2_MUST_GREATER_THAN_COST", ErrorMessageResourceType = typeof(Resources.Application))]
        public decimal? OperatingCost2 { get; set; }

        /// <summary>
        /// จำนวนวันทำการ 2
        /// </summary>
        [Display(Name = "APPLICATION_OPERATING_DAYS2", ResourceType = typeof(Resources.Application))]
        [RequiredIf("OperatingDayType == 'Range'", ErrorMessageResourceName = "VAL_APPLICATION_OPERATING_DAY2_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]
        [AssertThat("OperatingDayType != 'Range' || OperatingDays2 > OperatingDays", ErrorMessageResourceName = "VAL_DAY2_MUST_GREATER_THAN_COST", ErrorMessageResourceType = typeof(Resources.Application))]
        public int? OperatingDays2 { get; set; }

        /// <summary>
        /// จำนวนวันทำการ 2
        /// </summary>
        [Display(Name = "APPLICATION_OPERATING_DAYS2", ResourceType = typeof(Resources.Application))]
        [RequiredIf("CitizenOperatingDayType == 'Range'", ErrorMessageResourceName = "VAL_APPLICATION_OPERATING_DAY2_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]
        [AssertThat("CitizenOperatingDayType != 'Range' || CitizenOperatingDays2 > CitizenOperatingDays", ErrorMessageResourceName = "VAL_DAY2_MUST_GREATER_THAN_COST", ErrorMessageResourceType = typeof(Resources.Application))]
        public int? CitizenOperatingDays2 { get; set; }


        [Display(Name = "LABEL_OPERATION_COST_TYPE", ResourceType = typeof(Resources.Application))]
        public string CitizenOperatingCostType { get; set; }


        [Display(Name = "LABEL_OPERATION_DAY_TYPE", ResourceType = typeof(Resources.Application))]
        public string CitizenOperatingDayType { get; set; }

        /// <summary>
        /// ค่าใช้จ่าย
        /// </summary>
        [Display(Name = "APPLICATION_OPERATING_COST", ResourceType = typeof(Resources.Application))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "###0.00")]
        [RequiredIf("CitizenOperatingCostType != null", ErrorMessageResourceName = "VAL_APPLICATION_OPERATING_COST_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]
        public decimal? CitizenOperatingCost { get; set; }

        /// <summary>
        /// ค่าใช้จ่าย 2
        /// </summary>
        [Display(Name = "APPLICATION_OPERATING_COST2", ResourceType = typeof(Resources.Application))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "###0.00")]
        [RequiredIf("CitizenOperatingCostType == 'Range'", ErrorMessageResourceName = "VAL_APPLICATION_OPERATING_COST2_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]
        [AssertThat("CitizenOperatingCostType != 'Range' || CitizenOperatingCost2 > CitizenOperatingCost", ErrorMessageResourceName = "VAL_COST2_MUST_GREATER_THAN_COST", ErrorMessageResourceType = typeof(Resources.Application))]
        public decimal? CitizenOperatingCost2 { get; set; }

        [Display(Name = "LABEL_SHOW_REMARK", ResourceType = typeof(Resources.Application))]
        public bool CitizenShowRemark { get; set; }

        [Display(Name = "LABEL_REMARK", ResourceType = typeof(Resources.Application))]
        [RequiredIf("CitizenShowRemark == true", ErrorMessageResourceName = "VAL_REMARK_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]
        public string CitizenRemark { get; set; }

        [Display(Name = "TEMPORARY_DISABLE", ResourceType = typeof(Resources.Application))]
        public bool TemporaryDisable { get; set; }

        [Display(Name = "TEMPORARY_REMARK", ResourceType = typeof(Resources.Application))]
        [RequiredIf("TemporaryDisable == true", ErrorMessageResourceName = "VAL_REMARK_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]
        public string TemporaryRemark { get; set; }

        public string StatusSequence { get; set; }

        #region [Multi-language Data]

        [Required, StringLength(450)]
        [Display(Name = "APPLICATION_NAME", ResourceType = typeof(Resources.Application))]
        public string ApplicationName { get; set; }

        [StringLength(1000)]
        [Display(Name = "APPLICATION_DETAIL", ResourceType = typeof(Resources.Application))]
        public string ApplicationDetail { get; set; }

        [Display(Name = "APPLICATION_APPROVE_MAIL_MSG", ResourceType = typeof(Resources.Application))]
        public string ApprovedMailMessage { get; set; }

        [Display(Name = "APPLICATION_REJECT_MAIL_MSG", ResourceType = typeof(Resources.Application))]
        public string RejectedMailMessage { get; set; }

        [Display(Name = "APPLICATION_SUBMIT_MAIL_MSG", ResourceType = typeof(Resources.Application))]
        public string SubmitMailMessage { get; set; }

        [Display(Name = "APPLICATION_HOOK", ResourceType = typeof(Resources.Application))]
        [StringLength(1000)]
        public string AppsHookClassName { get; set; }

        public bool IsEnableELicense { get; set; }

      
        [Display(Name = "SIGNING_TYPE", ResourceType = typeof(Resources.Application))]
        //  [RequiredIf("IsEnableELicense == true", ErrorMessageResourceName = "VAL_SIGNING_TYPE_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]

        public string SigningType { get; set; }


        [Display(Name = "SIGNING_DOCUMENT_TYPE", ResourceType = typeof(Resources.Application))]
        // [RequiredIf("IsEnableELicense == true", ErrorMessageResourceName = "VAL_SIGNING_DOC_TYPE_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]

        public string SigningDocumentType { get; set; }


        [Display(Name = "SIGNING_DOCUMENT_CITIZEN_TEMPLATE_ID", ResourceType = typeof(Resources.Application))]
      //  [RequiredIf("SigningDocumentType == 'Template'", ErrorMessageResourceName = "VAL_SIGNING_DOC_TEMPLATE_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]

        public string SigningDocumentCitizenTemplateID { get; set; }

        [Display(Name = "SIGNING_DOCUMENT_JURISTIC_TEMPLATE_ID", ResourceType = typeof(Resources.Application))]
        //  [RequiredIf("SigningDocumentType == 'Template'", ErrorMessageResourceName = "VAL_SIGNING_DOC_TEMPLATE_REQUIRED", ErrorMessageResourceType = typeof(Resources.Application))]

        public string SigningDocumentJuristicTemplateID { get; set; }
        [Display(Name = "SigningPersons", ResourceType = typeof(Resources.Application))]

        //[JsonProperty("SigningPersons")]   
        public ICollection<SigningPersonViewModel> SigningPersons { get; set; }

        
        #endregion

        #region [Display Only]

        public string OrgSysName { get; set; }

        #endregion


       




       


    }
}
