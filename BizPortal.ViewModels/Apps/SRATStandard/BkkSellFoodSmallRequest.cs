using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.SRATStandard
{
    public class BkkSellFoodSmallRequest
    {
        public string Id { get; set; }
        public string ApplicationNo { get; set; }

        /// <summary>
        /// เขียนที่ Biz Portal
        /// </summary>
        public string WroteAt { get; set; }
        /// <summary>
        /// ApplicationRequestNumber
        /// </summary>
        public string BizId { get; set; }
        /// <summary>
        /// ApplicationRequestID
        /// </summary>
        public string BizGuid { get; set; }
        /// <summary>
        /// วันที่ยื่น
        /// </summary>
        public DateTime SubmitDate { get; set; }
        /// <summary>
        /// ข้อมูลผู้ยื่น
        /// ผู้ยื่นแบบบุคคลธรรมดา PersonApplicantDto
        /// ผู้ยื่นแบบนิติบุคคล JuristicPersonApplicantDto
        /// </summary>
        public IApplicant Applicant;
        /// <summary>
        /// ที่อยู่หลัก
        /// เช่น สถานที่ก่อสร้าง สถานประกอบการ
        /// </summary>
        public Address Address { get; set; }
        public GeneralInfo General { get; set; }
        public SaleCollectionInfo SaleCollectionInfo { get; set; }
        public FileMetaData[] ApplicationAttachments { get; set; }

        public BkkSellFoodSmallRequest()
        {
            General = new GeneralInfo();
            SaleCollectionInfo = new SaleCollectionInfo();
        }
    }
    public class GeneralInfo
    {
        public ApplicantInfomation ApplicantInfo { get; set; }
        public ShopInformation ShopInfo { get; set; }

        public class ApplicantInfomation
        {
            public string ApplicantType { get; set; }
        }

        public class ShopInformation
        {
            public string ShopNameTH { get; set; }
            public string ShopNameEN { get; set; }
            public string ShopAddress { get; set; }
            public string ShopTelephone { get; set; }
            public string ShopMobile { get; set; }
            public string ShopFax { get; set; }
            public string ShopEmail { get; set; }
            public string ShopLatLong { get; set; }
            public Double Area { get; set; }
            public string OwnershipType { get; set; }
        }
    }
    public class SaleCollectionInfo
    {
        public SaleCollectionDataInfo SaleCollectionData { get; set; }
        public ShopCrewsDataInfo[] ShopCrewsData { get; set; }
        public ShopManagerDataInfo ShopManagerData { get; set; }

        public class SaleCollectionDataInfo
        {
            public string ApplicationType { get; set; }
            public string LicenseType { get; set; }
            public string ShopType { get; set; }
            public string Purpose { get; set; }
            public string FoodType { get; set; }
        }
        public class ShopCrewsDataInfo
        {
            public string ShopCrews_Name { get; set; }
            public string ShopCrews_Position { get; set; }
        }
        public class ShopManagerDataInfo
        {
            public string ShopManager { get; set; }
            public string ShopManagerAge { get; set; }
            public string ShopManagerNationality { get; set; }
            public string ShopManagerAddress { get; set; }
            public string ShopManagerTelephone { get; set; }
            public string ShopManagerFax { get; set; }

        }
    }
}
