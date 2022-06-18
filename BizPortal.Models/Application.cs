using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizPortal.Models
{
    public class Application : ManipulateModel
    {
        public Application()
        {
            ApplicationTranslations = new HashSet<ApplicationTranslation>();
            SigningPersons = new HashSet<SigningPerson>();
            SigningPositions = new HashSet<SigningPosition>();
            SigningExtendedDatas = new HashSet<SigningExtendedData>();
        }

        public int ApplicationID { get; set; }

        public string ApplicationType { get; set; }

        [Required, StringLength(450)]
        public string ApplicationSysName { get; set; }

        [Required, ForeignKey("Organization")]
        public string OrgCode { get; set; }

        [StringLength(1000)]
        public string ApplicationUrl { get; set; }

        [StringLength(1000)]
        public string AppsHookClassName { get; set; }

        /// <summary>
        /// รองรับการส่งคำร้องได้หลายครั้ง
        /// </summary>
        public bool MultipleRequestSupport { get; set; }

        /// <summary>
        /// For OAuth
        /// </summary>
        public Guid? ConsumerKey { get; set; }

        public string UrlCallBack { get; set; }

        public string ParamCallBack { get; set; }

        public bool SingleFormEnabled { get; set; }

        public bool RequestAtOrg { get; set; }
        public bool CitizenRequestAtOrg { get; set; }

        /// <summary>
        /// ลิ้งค์เชื่อมโยงไปยังหน้า INFO.GO.TH
        /// </summary>
        public string HandbookUrl { get; set; }

        /// <summary>
        /// โลโก้ของ App
        /// </summary>
        [ForeignKey("LogoFileUpload")]
        public int? LogoFileID { get; set; }

        public string OperatingDayType { get; set; }

        /// <summary>
        /// จำนวนวันทำการ
        /// </summary>
        public int? OperatingDays { get; set; }

        public int? OperatingDays2 { get; set; }

        public string OperatingCostType { get; set; }

        /// <summary>
        /// ค่าใช้จ่าย
        /// </summary>
        public decimal? OperatingCost { get; set; }

        public decimal? OperatingCost2 { get; set; }

        public bool ShowRemark { get; set; }

        public string Remark { get; set; }

        public string CitizenApplicationUrl { get; set; }

        public string CitizenHandbookUrl { get; set; }

        public string CitizenOperatingDayType { get; set; }

        public int? CitizenOperatingDays { get; set; }

        public int? CitizenOperatingDays2 { get; set; }

        public string CitizenOperatingCostType { get; set; }

        public decimal? CitizenOperatingCost { get; set; }

        public decimal? CitizenOperatingCost2 { get; set; }

        public bool CitizenShowRemark { get; set; }

        public string CitizenRemark { get; set; }

        public bool TemporaryDisable { get; set; }

        public string TemporaryRemark { get; set; }

        public string FileOwner { get; set; }

        public virtual FileUpload LogoFileUpload { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual ICollection<ApplicationTranslation> ApplicationTranslations { get; set; }

        public string StatusSequence { get; set; }

        public bool NoDocument { get; set; }

        public bool EnableCGDPayment { get; set; }

        public bool IsEnableELicense { get; set; }

        public string ELicenseConsumerKey { get; set; }

        public string ELicenseSecret { get; set; }

        public string ELicenseXMLStandard { get; set; }

        public string SigningType { get; set; }

        public string SigningDocumentType { get; set; }

        public string SigningDocumentCitizenTemplateID { get; set; }

        public string SigningDocumentJuristicTemplateID { get; set; }

        public virtual ICollection<SigningPerson> SigningPersons { get; set; }

        public virtual ICollection<SigningPosition> SigningPositions { get; set; }

        public virtual ICollection<SigningExtendedData> SigningExtendedDatas { get; set; }
    }
}
