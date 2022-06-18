using BizPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.Seeds
{
    class _20190911_BusinessList
    {
        public static void Seed(BizPortal.DAL.ApplicationDbContext context, ApplicationUser creater)
        {
            List<BusinessGroup> businessGroups = new List<BusinessGroup>()
            {
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.RESTAURANT,
                    BusinessNameTH = "ธุรกิจร้านอาหารและเครื่องดื่ม",
                    BusinessNameEN = "Restaurant Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.RETAIL,
                    BusinessNameTH = "ธุรกิจร้านค้าปลีก",
                    BusinessNameEN = "Retail Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.UTILITIES,
                    BusinessNameTH = "ขอใช้สาธารณูปโภค",
                    BusinessNameEN = "Utilities Request"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.STARTINGBUSINESS,
                    BusinessNameTH = "เริ่มต้นธุรกิจ",
                    BusinessNameEN = "Starting Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.HOTEL,
                    BusinessNameTH = "ธุรกิจรีสอร์ทขนาดเล็ก/โรงแรม",
                    BusinessNameEN = "Resort And Hotel Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.COFFEESHOP,
                    BusinessNameTH = "ธุรกิจ Co-Working Space",
                    BusinessNameEN = "Co-Working Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.FITNESS,
                    BusinessNameTH = "ธุรกิจฟิตเนส",
                    BusinessNameEN = "Fitness Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.VETERINARY,
                    BusinessNameTH = "ธุรกิจสถานพยาบาลสัตว์",
                    BusinessNameEN = "Veterinary Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.BEAUTYCLINIC,
                    BusinessNameTH = "ธุรกิจคลินิกเสริมความงาม (แบบไม่ค้างคืน)",
                    BusinessNameEN = "Beauty Clinic Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.COSMETICS,
                    BusinessNameTH = "ธุรกิจผลิตครีมบำรุง เครื่องสำอาง น้ำหอม",
                    BusinessNameEN = "Cosmetics Production Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.SPA,
                    BusinessNameTH = "ธุรกิจสปา",
                    BusinessNameEN = "Spa Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.CONSTRUCTION,
                    BusinessNameTH = "ธุรกิจก่อสร้างและรับเหมาก่อสร้าง",
                    BusinessNameEN = "Construction Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.ELECTRONIC,
                    BusinessNameTH = "ธุรกิจซ่อมและขายอุปกรณ์อิเล็กทรอนิกส์",
                    BusinessNameEN = "Electronic Equipment Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.CARCARE,
                    BusinessNameTH = "ธุรกิจคาร์แคร์",
                    BusinessNameEN = "Car Care Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.ORGANICFARMING,
                    BusinessNameTH = "ธุรกิจเกษตรปลอดสารพิษ",
                    BusinessNameEN = "Organic Agriculture Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.MEDICALTOOLS,
                    BusinessNameTH = "ธุรกิจอุปกรณ์เครื่องมือทางการแพทย์",
                    BusinessNameEN = "Medical Tools Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.TOURISM,
                    BusinessNameTH = "ธุรกิจการท่องเที่ยว",
                    BusinessNameEN = "Tourism Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.LOGISTICS,
                    BusinessNameTH = "ธุรกิจขนส่ง Logistics",
                    BusinessNameEN = "Logistics Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.FINANCE,
                    BusinessNameTH = "ธุรกิจทางการเงิน",
                    BusinessNameEN = "Financial Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.EDUCATION,
                    BusinessNameTH = "ธุรกิจการศึกษา",
                    BusinessNameEN = "Education Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.LEGAL,
                    BusinessNameTH = "ธุรกิจให้คำปรึกษาด้านกฎหมายและบัญชี",
                    BusinessNameEN = "Legal And Accounting Advising Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.SOFTWARE,
                    BusinessNameTH = "ธุรกิจการพัฒนาแอปพลิเคชัน ซอฟต์แวร์ ซื้อมา-ขายไป และการให้บริการ",
                    BusinessNameEN = "Software Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.ELDERLYCARE,
                    BusinessNameTH = "ธุรกิจการให้บริการสถานดูแลผู้ป่วย ผู้สูงอายุ/ จัดหาผู้ดูแล",
                    BusinessNameEN = "Elderly Care Center Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.SMALLAGRICULTURAL,
                    BusinessNameTH = "ธุรกิจแปรรูปสินค้าเกษตรขนาดเล็ก",
                    BusinessNameEN = "Processing Small Agricultural Products Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.ENERGY,
                    BusinessNameTH = "ธุรกิจผลิตพลังงานสำรอง และพลังงานทดแทน และขายกระแสไฟฟ้าให้ภาครัฐ",
                    BusinessNameEN = "Energy Business"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.ONLINECOSMETIC,
                    BusinessNameTH = "ธุรกิจขายสินค้า online (ด้านเครื่องสำอาง)",
                    BusinessNameEN = "Online Marketing Business (Cosmetic)"
                },
                new BusinessGroup()
                {
                    BusinessSysName = BusinessGroupID.ECOMCLOTHES,
                    BusinessNameTH = "ธุรกิจ e-Commerce (ด้านเสื้อผ้า)",
                    BusinessNameEN = "E-Commerce Business (Clothes)"
                },
            };

            foreach (BusinessGroup businessGroup in businessGroups)
            {
                var item = context.BusinessGroups.Where(x => x.BusinessSysName == businessGroup.BusinessSysName).FirstOrDefault();
                if (item == null)
                {
                    businessGroup.CreatedDate = DateTime.Now;
                    businessGroup.UpdatedDate = DateTime.Now;
                }
                else
                {
                    businessGroup.ID = item.ID;
                    businessGroup.CreatedDate = item.CreatedDate;
                    businessGroup.UpdatedDate = DateTime.Now;
                }
            }
            context.BusinessGroups.AddOrUpdate(x => x.ID, businessGroups.ToArray());
            context.SaveChanges(false);
        }

        public class BusinessGroupID
        {
            /// <summary>
            /// ธุรกิจร้านอาหารและเครื่องดื่ม
            /// </summary>
            public const string RESTAURANT = "RESTAURANT";
            /// <summary>
            /// ธุรกิจร้านค้าปลีก
            /// </summary>
            public const string RETAIL = "RETAIL";
            /// <summary>
            /// ขอใช้สาธารณูปโภค
            /// </summary>
            public const string UTILITIES = "UTILITIES";
            /// <summary>
            /// เริ่มต้นธุรกิจ
            /// </summary>
            public const string STARTINGBUSINESS = "STARTINGBUSINESS";
            /// <summary>
            /// ธุรกิจรีสอร์ทขนาดเล็ก/โรงแรม
            /// </summary>
            public const string HOTEL = "HOTEL";
            /// <summary>
            /// ธุรกิจ Co-Working Space
            /// </summary>
            public const string COFFEESHOP = "COFFEESHOP";
            /// <summary>
            /// ธุรกิจฟิตเนส
            /// </summary>
            public const string FITNESS = "FITNESS";
            /// <summary>
            /// ธุรกิจสถานพยาบาลสัตว์
            /// </summary>
            public const string VETERINARY = "VETERINARY";
            /// <summary>
            /// ธุรกิจคลินิกเสริมความงาม (แบบไม่ค้างคืน)
            /// </summary>
            public const string BEAUTYCLINIC = "BEAUTYCLINIC";
            /// <summary>
            /// ธุรกิจผลิตครีมบำรุง เครื่องสำอาง น้ำหอม
            /// </summary>
            public const string COSMETICS = "COSMETICS";
            /// <summary>
            /// ธุรกิจสปา
            /// </summary>
            public const string SPA = "SPA";
            /// <summary>
            /// ธุรกิจก่อสร้างและรับเหมาก่อสร้าง
            /// </summary>
            public const string CONSTRUCTION = "CONSTRUCTION";
            /// <summary>
            /// ธุรกิจซ่อมและขายอุปกรณ์อิเล็กทรอนิกส์
            /// </summary>
            public const string ELECTRONIC = "ELECTRONIC";
            /// <summary>
            /// ธุรกิจคาร์แคร์
            /// </summary>
            public const string CARCARE = "CARCARE";
            /// <summary>
            /// ธุรกิจเกษตรปลอดสารพิษ
            /// </summary>
            public const string ORGANICFARMING = "ORGANICFARMING";
            /// <summary>
            /// ธุรกิจอุปกรณ์เครื่องมือทางการแพทย์
            /// </summary>
            public const string MEDICALTOOLS = "MEDICALTOOLS";
            /// <summary>
            /// ธุรกิจการท่องเที่ยว
            /// </summary>
            public const string TOURISM = "TOURISM";
            /// <summary>
            /// ธุรกิจขนส่ง Logistics
            /// </summary>
            public const string LOGISTICS = "LOGISTICS";
            /// <summary>
            /// ธุรกิจทางการเงิน
            /// </summary>
            public const string FINANCE = "FINANCE";
            /// <summary>
            /// ธุรกิจการศึกษา
            /// </summary>
            public const string EDUCATION = "EDUCATION";
            /// <summary>
            /// ธุรกิจให้คำปรึกษาด้านกฎหมายและบัญชี
            /// </summary>
            public const string LEGAL = "LEGAL";
            /// <summary>
            /// ธุรกิจการพัฒนาแอปพลิเคชัน ซอฟต์แวร์ ซื้อมา-ขายไป และการให้บริการ
            /// </summary>
            public const string SOFTWARE = "SOFTWARE";
            /// <summary>
            /// ธุรกิจการให้บริการสถานดูแลผู้ป่วย ผู้สูงอายุ/ จัดหาผู้ดูแล
            /// </summary>
            public const string ELDERLYCARE = "ELDERLYCARE";
            /// <summary>
            /// ธุรกิจแปรรูปสินค้าเกษตรขนาดเล็ก
            /// </summary>
            public const string SMALLAGRICULTURAL = "SMALLAGRICULTURAL";
            /// <summary>
            /// ธุรกิจผลิตพลังงานสำรอง และพลังงานทดแทน และขายกระแสไฟฟ้าให้ภาครัฐ
            /// </summary>
            public const string ENERGY = "ENERGY";
            /// <summary>
            /// ธุรกิจขายสินค้า online (ด้านเครื่องสำอาง)
            /// </summary>
            public const string ONLINECOSMETIC = "ONLINECOSMETIC";
            /// <summary>
            /// ธุรกิจ e-Commerce (ด้านเสื้อผ้า)
            /// </summary>
            public const string ECOMCLOTHES = "ECOMCLOTHES";

        }
    }
}
