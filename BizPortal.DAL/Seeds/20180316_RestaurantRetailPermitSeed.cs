using BizPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.Seeds
{
    class _20180316_RestaurantRetailPermitSeed
    {
        static ApplicationUser user;
        const string BKK_ONLY = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
        public static void Seed(BizPortal.DAL.ApplicationDbContext context, ApplicationUser creater)
        {
            user = creater;
            //var oList = new Organization[]
            //{
            //    new Organization
            //    {
            //        OrgCode = "03006000",
            //        OrgSysName = "กรมสรรพสามิต กระทรวงการคลัง",
            //        MinistryCode = "99",
            //        DepartmentCode = "99",
            //        DivisionCode = "999"
            //    },
            //    new Organization
            //    {
            //        OrgCode = "13020000",
            //        OrgSysName = "สำนักงานเขต กรุงเทพมหานคร",
            //        MinistryCode = "99",
            //        DepartmentCode = "99",
            //        DivisionCode = "999"
            //    },
            //    new Organization
            //    {
            //        OrgCode = "21007006",
            //        OrgSysName = "กองบัญชาการตำรวจนครบาล",
            //        MinistryCode = "99",
            //        DepartmentCode = "99",
            //        DivisionCode = "999"
            //    },
            //    new Organization
            //    {
            //        OrgCode = "16005000",
            //        OrgSysName = "กรมส่งเสริมวัฒนธรรม",
            //        MinistryCode = "99",
            //        DepartmentCode = "99",
            //        DivisionCode = "999"
            //    },
            //    new Organization
            //    {
            //        OrgCode = "07008000",
            //        OrgSysName = "กรมวิชาการเกษตร",
            //        MinistryCode = "99",
            //        DepartmentCode = "99",
            //        DivisionCode = "999"
            //    },
            //    new Organization
            //    {
            //        OrgCode = "07006000",
            //        OrgSysName = "กรมปศุสัตว์",
            //        MinistryCode = "99",
            //        DepartmentCode = "99",
            //        DivisionCode = "999"
            //    },
            //    new Organization
            //    {
            //        OrgCode = "01004000",
            //        OrgSysName = "สำนักงานคณะกรรมการคุ้มครองผู้บริโภค",
            //        MinistryCode = "99",
            //        DepartmentCode = "99",
            //        DivisionCode = "999"
            //    },
            //    new Organization
            //    {
            //        OrgCode = "19010000",
            //        OrgSysName = "สำนักงานคณะกรรมการอาหารและยา",
            //        MinistryCode = "99",
            //        DepartmentCode = "99",
            //        DivisionCode = "999"
            //    },
            //    new Organization
            //    {
            //        OrgCode = "12007000",
            //        OrgSysName = "กรมพัฒนาธุรกิจการค้า",
            //        MinistryCode = "99",
            //        DepartmentCode = "99",
            //        DivisionCode = "999"
            //    },
            //    new Organization
            //    {
            //        OrgCode = "11005000",
            //        OrgSysName = "กรมธุรกิจพลังงาน",
            //        MinistryCode = "99",
            //        DepartmentCode = "99",
            //        DivisionCode = "999"
            //    },
            //};
            //context.Organizations.AddOrUpdate(o => o.OrgCode, oList);
            //context.SaveChanges(false);

            //var oTList = new OrganizationTranslation[]
            //{
            //    new OrganizationTranslation
            //    {
            //        OrgName = "กรมสรรพสามิต",
            //        Abbreviation = "",
            //        LanguageID = 1,
            //        OrgCode = "03006000",
            //        Address = "1488 ถนนนครไชยศรี ดุสิต กรุงเทพมหานคร 10300",
            //    },
            //    new OrganizationTranslation
            //    {
            //        OrgName = "สำนักงานเขต กรุงเทพมหานคร",
            //        Abbreviation = "BMA Health",
            //        LanguageID = 1,
            //        OrgCode = "13020000",
            //        Address = "",
            //    },
            //    new OrganizationTranslation
            //    {
            //        OrgName = "Bangkok Health Department",
            //        Abbreviation = "BMA Health",
            //        LanguageID = 2,
            //        OrgCode = "13020000",
            //        Address = "",
            //    },
            //    new OrganizationTranslation
            //    {
            //        OrgName = "กรมสรรพสามิต (en)",
            //        Abbreviation = "",
            //        LanguageID = 2,
            //        OrgCode = "03006000",
            //        Address = "1488 ถนนนครไชยศรี ดุสิต กรุงเทพมหานคร 10300",
            //    },
            //    new OrganizationTranslation
            //    {
            //        OrgName = "กรมพัฒนาธุรกิจการค้า",
            //        Abbreviation = "dbd",
            //        LanguageID = 1,
            //        OrgCode = "12007000",
            //        Address = "",
            //    },
            //    new OrganizationTranslation
            //    {
            //        OrgName = "กรมวิชาการเกษตร",
            //        Abbreviation = "",
            //        LanguageID = 1,
            //        OrgCode = "07008000",
            //        Address = "50 ถนนพหลโยธิน. แขวงลาดยาว. เขตจตุจักร. กรุงเทพฯ 10900",
            //    },
            //    new OrganizationTranslation
            //    {
            //        OrgName = "กรมวิชาการเกษตร (en)",
            //        Abbreviation = "",
            //        LanguageID = 2,
            //        OrgCode = "07008000",
            //        Address = "50 ถนนพหลโยธิน. แขวงลาดยาว. เขตจตุจักร. กรุงเทพฯ 10900",
            //    },
            //    new OrganizationTranslation
            //    {
            //        OrgName = "กรมปศุสัตว์",
            //        Abbreviation = "",
            //        LanguageID = 1,
            //        OrgCode = "07006000",
            //        Address = "",
            //    },
            //    new OrganizationTranslation
            //    {
            //        OrgName = "กรมปศุสัตว์ (en)",
            //        Abbreviation = "",
            //        LanguageID = 2,
            //        OrgCode = "07006000",
            //        Address = "",
            //    },
            //    new OrganizationTranslation
            //    {
            //        OrgName = "สำนักงานคณะกรรมการคุ้มครองผู้บริโภค",
            //        Abbreviation = "",
            //        LanguageID = 1,
            //        OrgCode = "01004000",
            //        Address = "ศูนย์ราชการเฉลิมพระเกียรติ 80 พรรษา 5 ธันวาคม 2550 อาคารรัฐประศาสนภักดี (อาคาร B) ชั้น 5 ถนนแจ้งวัฒนะ ทุ่งสองห้อง หลักสี่ กรุงเทพมหานคร 10210",
            //    },
            //    new OrganizationTranslation
            //    {
            //        OrgName = "สำนักงานคณะกรรมการคุ้มครองผู้บริโภค (en)",
            //        Abbreviation = "",
            //        LanguageID = 2,
            //        OrgCode = "01004000",
            //        Address = "ศูนย์ราชการเฉลิมพระเกียรติ 80 พรรษา 5 ธันวาคม 2550 อาคารรัฐประศาสนภักดี (อาคาร B) ชั้น 5 ถนนแจ้งวัฒนะ ทุ่งสองห้อง หลักสี่ กรุงเทพมหานคร 10210",
            //    },
            //    new OrganizationTranslation
            //    {
            //        OrgName = "กรมธุรกิจพลังงาน",
            //        Abbreviation = "",
            //        LanguageID = 1,
            //        OrgCode = "11005000",
            //        Address = "",
            //    },
            //    new OrganizationTranslation
            //    {
            //        OrgName = "กรมธุรกิจพลังงาน (en)",
            //        Abbreviation = "",
            //        LanguageID = 2,
            //        OrgCode = "11005000",
            //        Address = "",
            //    },
            //};
            //context.OrganizationTranslations.AddOrUpdate(o => new { o.OrgCode, o.LanguageID }, oTList);
            //context.SaveChanges(false);

            var fList = new FileUpload[]
            {
                createFileUpload("bangkok.jpg", "image/jpeg"),
                createFileUpload("tax.png", "image/png"),
                createFileUpload("agri.gif", "image/gif"),
                createFileUpload("buyer_protect.PNG", "image/png"),
                createFileUpload("culture.png", "image/png"),
                createFileUpload("drug.png", "image/png"),
                createFileUpload("livestock.png", "image/png"),
                createFileUpload("police.jpg", "image/jpeg"),
                createFileUpload("dbd.png", "image/png"),
                createFileUpload("energy.png", "image/png"),
            };

            context.FileUploads.AddOrUpdate(a => a.FileSysName, fList);
            context.SaveChanges(false);

            var aList = new Application[]
            {
                createApplication(
                    "ขอใบอนุญาตขายสุรา",
                    "03006000",
                    null,
                    "BizPortal.AppsHook.ExciseSellAlcoholAppHook",
                    true,
                    "https://info.go.th/#!/th/search/48626/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%82%E0%B8%B2%E0%B8%A2%E0%B8%AA%E0%B8%B8%E0%B8%A3%E0%B8%B2%20%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B9%80%E0%B8%A0%E0%B8%97%E0%B8%97%E0%B8%B5%E0%B9%88%203%20%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B9%80%E0%B8%A0%E0%B8%97%E0%B8%97%E0%B8%B5%E0%B9%88%204%20(%E0%B8%A2%E0%B8%81%E0%B9%80%E0%B8%A7%E0%B9%89%E0%B8%99%E0%B9%81%E0%B8%AD%E0%B8%A5%E0%B8%81%E0%B8%AD%E0%B8%AE%E0%B8%AD%E0%B8%A5%E0%B9%8C)%C2%A0(%E0%B8%A1%E0%B8%B2%E0%B8%95%E0%B8%A3%E0%B8%B2%2017)/",
                    context.FileUploads.Where(f => f.FileSysName == "tax.png").First(),
                    1,
                    330,
                    7700,
                    CostType.Range,
                    null,
                    null
                ),
                createApplication(
                    "ขอหนังสือรับรองการแจ้งจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (ไม่เกิน 200 ตร.ม.)",
                    "22000000",
                    null,
                    "BizPortal.AppsHook.BKKSellFoodSmallAppHook",
                    true,
                    "https://info.go.th/#!/th/search/8598/%E0%B8%AA%E0%B8%B0%E0%B8%AA%E0%B8%A1%E0%B8%AD%E0%B8%B2%E0%B8%AB%E0%B8%B2%E0%B8%A3/",
                    context.FileUploads.Where(f => f.FileSysName == "bangkok.jpg").First(),
                    7,
                    100,
                    1000,
                    CostType.Range,
                    BKK_ONLY,
                    BKK_ONLY
                ),
                createApplication(
                    "ขอใบอนุญาตจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (เกิน 200 ตร.ม.)",
                    "22000000",
                    null,
                    "BizPortal.AppsHook.BKKSellFoodLargeAppHook",
                    true,
                    "https://info.go.th/#!/th/search/3871/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%88%E0%B8%B1%E0%B8%94%E0%B8%95%E0%B8%B1%E0%B9%89%E0%B8%87%E0%B8%AA%E0%B8%96%E0%B8%B2%E0%B8%99%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%88%E0%B8%B3%E0%B8%AB%E0%B8%99%E0%B9%88%E0%B8%B2%E0%B8%A2%E0%B8%AD%E0%B8%B2%E0%B8%AB%E0%B8%B2%E0%B8%A3%E0%B8%AB%E0%B8%A3%E0%B8%B7%E0%B8%AD%E0%B8%AA%E0%B8%96%E0%B8%B2%E0%B8%99%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%AA%E0%B8%B0%E0%B8%AA%E0%B8%A1%E0%B8%AD%E0%B8%B2%E0%B8%AB%E0%B8%B2%E0%B8%A3%20(%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B9%83%E0%B8%AB%E0%B8%A1%E0%B9%88)/",
                    context.FileUploads.Where(f => f.FileSysName == "bangkok.jpg").First(),
                    30,
                    2000,
                    3000,
                    CostType.Range,
                    BKK_ONLY,
                    BKK_ONLY
                ),
                createApplication(
                    "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ",
                    "22000000",
                    null,
                    "BizPortal.AppsHook.BKKDangerFoodAppHook",
                    true,
                    "https://info.go.th/#!/th/search/5014/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B9%80%E0%B8%9B%E0%B9%87%E0%B8%99%E0%B8%AD%E0%B8%B1%E0%B8%99%E0%B8%95%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B8%95%E0%B9%88%E0%B8%AD%E0%B8%AA%E0%B8%B8%E0%B8%82%E0%B8%A0%E0%B8%B2%E0%B8%9E%20(%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B9%83%E0%B8%AB%E0%B8%A1%E0%B9%88)/",
                    context.FileUploads.Where(f => f.FileSysName == "bangkok.jpg").First(),
                    30,
                    1000,
                    10000,
                    CostType.Range,
                    null,
                    null
                ),
                createApplication(
                    "ขอใบอนุญาตจำหน่ายสินค้าในที่หรือทางสาธารณะ",
                    "22000000",
                    null,
                    "BizPortal.AppsHook.BKKSellFoodOnFootpathAppHook",
                    true,
                    "https://info.go.th/#!/th/search/3880/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%80%E0%B8%9B%E0%B9%87%E0%B8%99%E0%B8%9C%E0%B8%B9%E0%B9%89%E0%B8%88%E0%B8%B3%E0%B8%AB%E0%B8%99%E0%B9%88%E0%B8%B2%E0%B8%A2%E0%B8%AA%E0%B8%B4%E0%B8%99%E0%B8%84%E0%B9%89%E0%B8%B2%E0%B9%83%E0%B8%99%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%AB%E0%B8%A3%E0%B8%B7%E0%B8%AD%E0%B8%97%E0%B8%B2%E0%B8%87%E0%B8%AA%E0%B8%B2%E0%B8%98%E0%B8%B2%E0%B8%A3%E0%B8%93%E0%B8%B0(%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B9%83%E0%B8%AB%E0%B8%A1%E0%B9%88)/",
                    context.FileUploads.Where(f => f.FileSysName == "bangkok.jpg").First(),
                    30,
                    100,
                    null,
                    CostType.Fixed,
                    "หมายเหตุ: สำหรับพื้นที่กรุงเทพมหานคร ไม่สามารถขอใบอนุญาตนี้ได้แล้ว",
                    "หมายเหตุ: สำหรับพื้นที่กรุงเทพมหานคร ไม่สามารถขอใบอนุญาตนี้ได้แล้ว"
                ),
                createApplication(
                    "ขอใบอนุญาตตั้งสถานบริการในท้องที่กรุงเทพมหานคร",
                    "21007006",
                    null,
                    null,
                    false,
                    "https://info.go.th/#!/th/search/8655/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%95%E0%B8%B1%E0%B9%89%E0%B8%87%E0%B8%AA%E0%B8%96%E0%B8%B2%E0%B8%99%E0%B8%9A%E0%B8%A3%E0%B8%B4%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%83%E0%B8%99%E0%B8%97%E0%B9%89%E0%B8%AD%E0%B8%87%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%81%E0%B8%A3%E0%B8%B8%E0%B8%87%E0%B9%80%E0%B8%97%E0%B8%9E%E0%B8%A1%E0%B8%AB%E0%B8%B2%E0%B8%99%E0%B8%84%E0%B8%A3/",
                    context.FileUploads.Where(f => f.FileSysName == "police.jpg").First(),
                    90,
                    10000,
                    50000,
                    CostType.Range,
                    "<span style=\"color: red; \">นอกจากขอใบอนุญาตตั้งสถานบริการฯ แล้ว คุณต้องขอจดทะเบียนสรรพสามิตกับกรมสรรพสามิตด้วย</span>",
                    "<span style=\"color: red; \">นอกจากขอใบอนุญาตตั้งสถานบริการฯ แล้ว คุณต้องขอจดทะเบียนสรรพสามิตกับกรมสรรพสามิตด้วย</span>"
                ),
                createApplication(
                    "ขอใบอนุญาตประกอบกิจการร้านวีดิทัศน์ (คาราโอเกะ)",
                    "16005000",
                    "http://movie.culture.go.th/",
                    null,
                    false,
                    "https://www.info.go.th/#!/th/search/6629/%E0%B8%82%E0%B8%AD%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%A3%E0%B9%89%E0%B8%B2%E0%B8%99%E0%B8%A7%E0%B8%B5%E0%B8%94%E0%B8%B4%E0%B8%97%E0%B8%B1%E0%B8%A8%E0%B8%99%E0%B9%8C%20(%E0%B8%84%E0%B8%B2%E0%B8%A3%E0%B8%B2%E0%B9%82%E0%B8%AD%E0%B9%80%E0%B8%81%E0%B8%B0)/",
                    context.FileUploads.Where(f => f.FileSysName == "culture.png").First(),
                    46,
                    1000,
                    3000,
                    CostType.Range,
                    null,
                    null
                ),
                createApplication(
                    "ขอใบอนุญาตจำหน่ายปุ๋ย",
                    "07008000",
                    "http://e-fert.doa.go.th:21003/",
                    null,
                    false,
                    "https://info.go.th/#!/th/search/7969/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%AD%E0%B8%AD%E0%B8%81%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%82%E0%B8%B2%E0%B8%A2%E0%B8%9B%E0%B8%B8%E0%B9%8B%E0%B8%A2/",
                    context.FileUploads.Where(f => f.FileSysName == "agri.gif").First(),
                    1,
                    200,
                    null,
                    CostType.Fixed,
                    null,
                    null
                ),
                createApplication(
                    "ขอใบอนุญาตจำหน่ายเมล็ดพันธุ์ควบคุมเพื่อการค้า",
                    "07008000",
                    "http://e-seed.doa.go.th:21004/",
                    null,
                    false,
                    "https://info.go.th/#!/th/search/7969/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%AD%E0%B8%AD%E0%B8%81%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%82%E0%B8%B2%E0%B8%A2%E0%B8%9B%E0%B8%B8%E0%B9%8B%E0%B8%A2/",
                    context.FileUploads.Where(f => f.FileSysName == "agri.gif").First(),
                    1,
                    200,
                    null,
                    CostType.Fixed,
                    null,
                    null
                ),
                createApplication(
                    "ขอใบอนุญาตขายอาหารสัตว์ควบคุมเฉพาะ",
                    "07006000",
                    "https://req1.dld.go.th/req/index.jsp",
                    "BizPortal.AppsHook.DLDSellAnimalFoodAppHook",
                    false,
                    "https://info.go.th/#!/th/search/11091/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%82%E0%B8%B2%E0%B8%A2%E0%B8%AD%E0%B8%B2%E0%B8%AB%E0%B8%B2%E0%B8%A3%E0%B8%AA%E0%B8%B1%E0%B8%95%E0%B8%A7%E0%B9%8C%E0%B8%84%E0%B8%A7%E0%B8%9A%E0%B8%84%E0%B8%B8%E0%B8%A1%E0%B9%80%E0%B8%89%E0%B8%9E%E0%B8%B2%E0%B8%B0/",
                    context.FileUploads.Where(f => f.FileSysName == "livestock.png").First(),
                    3,
                    100,
                    300,
                    CostType.Range,
                    null,
                    null
                ),
                createApplication(
                    "ขอใบอนุญาตทำการค้าหรือหากำไรในลักษณะคนกลางซึ่งสัตว์",
                    "07006000",
                    "https://emovereq.dld.go.th/",
                    "BizPortal.AppsHook.DLDSellAnimalAppHook",
                    false,
                    "https://info.go.th/#!/th/search/9729/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B9%89%E0%B8%97%E0%B8%B3%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%84%E0%B9%89%E0%B8%B2%E0%B8%AA%E0%B8%B1%E0%B8%95%E0%B8%A7%E0%B9%8C%E0%B8%AB%E0%B8%A3%E0%B8%B7%E0%B8%AD%E0%B8%8B%E0%B8%B2%E0%B8%81%E0%B8%AA%E0%B8%B1%E0%B8%95%E0%B8%A7%E0%B9%8C%20(%E0%B8%A3.10)/",
                    context.FileUploads.Where(f => f.FileSysName == "livestock.png").First(),
                    1,
                    300,
                    8600,
                    CostType.Range,
                    "ค่าธรรมเนียมขึ้นอยู่กับลักษณะการทำการค้า เช่น ทำการค้าทั่วประเทศ = 1,000 บาท ทำการค้าแบบนำเข้า = 5,000 บาท",
                    "ค่าธรรมเนียมขึ้นอยู่กับลักษณะการทำการค้า เช่น ทำการค้าทั่วประเทศ = 1,000 บาท ทำการค้าแบบนำเข้า = 5,000 บาท"
                ),
                createApplication(
                    "ขอใบอนุญาตทำการค้าหรือหากำไรในลักษณะคนกลางซึ่งซากสัตว์",
                    "07006000",
                    "https://emovereq.dld.go.th/",
                    "BizPortal.AppsHook.DLDSellCarcassAppHook",
                    false,
                    "https://info.go.th/#!/th/search/9729/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B9%89%E0%B8%97%E0%B8%B3%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%84%E0%B9%89%E0%B8%B2%E0%B8%AA%E0%B8%B1%E0%B8%95%E0%B8%A7%E0%B9%8C%E0%B8%AB%E0%B8%A3%E0%B8%B7%E0%B8%AD%E0%B8%8B%E0%B8%B2%E0%B8%81%E0%B8%AA%E0%B8%B1%E0%B8%95%E0%B8%A7%E0%B9%8C%20(%E0%B8%A3.10)/",
                    context.FileUploads.Where(f => f.FileSysName == "livestock.png").First(),
                    1,
                    100,
                    1590,
                    CostType.Range,
                    "ค่าธรรมเนียมขึ้นอยู่กับลักษณะการทำการค้า เช่น ทำการค้าทั่วประเทศ = 240 บาท ทำการค้าแบบนำเข้า = 1,000 บาท",
                    "ค่าธรรมเนียมขึ้นอยู่กับลักษณะการทำการค้า เช่น ทำการค้าทั่วประเทศ = 240 บาท ทำการค้าแบบนำเข้า = 1,000 บาท"
                ),
                createApplication(
                    "ขอใบอนุญาตประกอบกิจการให้เช่า แลกเปลี่ยน หรือจำหน่ายภาพยนตร์",
                    "16005000",
                    "http://movie.culture.go.th/",
                    null,
                    false,
                    "https://info.go.th/#!/th/search/6658/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%83%E0%B8%AB%E0%B9%89%E0%B9%80%E0%B8%8A%E0%B9%88%E0%B8%B2%20%E0%B9%81%E0%B8%A5%E0%B8%81%E0%B9%80%E0%B8%9B%E0%B8%A5%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%99%20%E0%B8%AB%E0%B8%A3%E0%B8%B7%E0%B8%AD%E0%B8%88%E0%B8%B3%E0%B8%AB%E0%B8%99%E0%B9%88%E0%B8%B2%E0%B8%A2%E0%B8%A0%E0%B8%B2%E0%B8%9E%E0%B8%A2%E0%B8%99%E0%B8%95%E0%B8%A3%E0%B9%8C/",
                    context.FileUploads.Where(f => f.FileSysName == "culture.png").First(),
                    16,
                    500,
                    3000,
                    CostType.Range,
                    null,
                    null
                ),
                createApplication(
                    "ขอใบอนุญาตประกอบกิจการให้เช่า แลกเปลี่ยน หรือจำหน่ายวีดิทัศน์",
                    "16005000",
                    "http://movie.culture.go.th/",
                    null,
                    false,
                    "https://info.go.th/#!/th/search/6716/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%83%E0%B8%AB%E0%B9%89%E0%B9%80%E0%B8%8A%E0%B9%88%E0%B8%B2%20%E0%B9%81%E0%B8%A5%E0%B8%81%E0%B9%80%E0%B8%9B%E0%B8%A5%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%99%20%E0%B8%AB%E0%B8%A3%E0%B8%B7%E0%B8%AD%E0%B8%88%E0%B8%B3%E0%B8%AB%E0%B8%99%E0%B9%88%E0%B8%B2%E0%B8%A2%E0%B8%A7%E0%B8%B5%E0%B8%94%E0%B8%B4%E0%B8%97%E0%B8%B1%E0%B8%A8%E0%B8%99%E0%B9%8C/",
                    context.FileUploads.Where(f => f.FileSysName == "culture.png").First(),
                    16,
                    500,
                    3000,
                    CostType.Range,
                    null,
                    null
                ),
                createApplication(
                    "ขอจดทะเบียนการประกอบธุรกิจขายตรง",
                    "01004000",
                    null,
                    "BizPortal.AppsHook.OCPBDirectSellAppHook",
                    true,
                    "https://info.go.th/#!/th/search/8864/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%88%E0%B8%94%E0%B8%97%E0%B8%B0%E0%B9%80%E0%B8%9A%E0%B8%B5%E0%B8%A2%E0%B8%99%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%82%E0%B8%B2%E0%B8%A2%E0%B8%95%E0%B8%A3%E0%B8%87%20(%E0%B8%88%E0%B8%94%E0%B8%97%E0%B8%B0%E0%B9%80%E0%B8%9A%E0%B8%B5%E0%B8%A2%E0%B8%99%E0%B9%83%E0%B8%AB%E0%B8%A1%E0%B9%88)/",
                    context.FileUploads.Where(f => f.FileSysName == "buyer_protect.PNG").First(),
                    68,
                    0,
                    null,
                    CostType.Fixed,
                    null,
                    "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอจดทะเบียนการประกอบธุรกิจขายตรงได้</span>"
                ),
                createApplication(
                    "ขอจดทะเบียนการประกอบธุรกิจตลาดแบบตรง",
                    "01004000",
                    null,
                    "BizPortal.AppsHook.OCPBDirectMarketingAppHook",
                    true,
                    "https://info.go.th/#!/th/search/9074/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%88%E0%B8%94%E0%B8%97%E0%B8%B0%E0%B9%80%E0%B8%9A%E0%B8%B5%E0%B8%A2%E0%B8%99%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%95%E0%B8%A5%E0%B8%B2%E0%B8%94%E0%B9%81%E0%B8%9A%E0%B8%9A%E0%B8%95%E0%B8%A3%E0%B8%87%20(%E0%B8%88%E0%B8%94%E0%B8%97%E0%B8%B0%E0%B9%80%E0%B8%9A%E0%B8%B5%E0%B8%A2%E0%B8%99%E0%B9%83%E0%B8%AB%E0%B8%A1%E0%B9%88)/",
                    context.FileUploads.Where(f => f.FileSysName == "buyer_protect.PNG").First(),
                    68,
                    0,
                    null,
                    CostType.Fixed,
                    null,
                    null
                ),
                createApplication(
                    "ขอใบอนุญาตขายยาสูบ",
                    "03006000",
                    null,
                    "BizPortal.AppsHook.ExciseSellTobaccoAppHook",
                    true,
                    "https://info.go.th/#!/th/search/5182/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%82%E0%B8%B2%E0%B8%A2%E0%B8%A2%E0%B8%B2%E0%B8%AA%E0%B8%B9%E0%B8%9A%E0%B8%8A%E0%B8%99%E0%B8%B4%E0%B8%94%E0%B8%9A%E0%B8%B8%E0%B8%AB%E0%B8%A3%E0%B8%B5%E0%B9%88%E0%B8%8B%E0%B8%B4%E0%B8%81%E0%B8%B2%E0%B9%81%E0%B8%A3%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B9%80%E0%B8%A0%E0%B8%97%203/",
                    context.FileUploads.Where(f => f.FileSysName == "tax.png").First(),
                    1,
                    100,
                    3500,
                    CostType.Range,
                    null,
                    null
                ),
                createApplication(
                    "ขอจดแจ้งรายละเอียดการผลิตเพื่อขายหรือนำเข้าเพื่อขายเครื่องสำอางควบคุม",
                    "19010000",
                    "https://privus.fda.moph.go.th",
                    null,
                    false,
                    "https://info.go.th/#!/th/search/205/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%88%E0%B8%94%E0%B9%81%E0%B8%88%E0%B9%89%E0%B8%87%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B8%A5%E0%B8%B0%E0%B9%80%E0%B8%AD%E0%B8%B5%E0%B8%A2%E0%B8%94%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%9C%E0%B8%A5%E0%B8%B4%E0%B8%95%E0%B9%80%E0%B8%9E%E0%B8%B7%E0%B9%88%E0%B8%AD%E0%B8%82%E0%B8%B2%E0%B8%A2%E0%B8%AB%E0%B8%A3%E0%B8%B7%E0%B8%AD%E0%B8%99%E0%B8%B3%E0%B9%80%E0%B8%82%E0%B9%89%E0%B8%B2%E0%B9%80%E0%B8%9E%E0%B8%B7%E0%B9%88%E0%B8%AD%E0%B8%82%E0%B8%B2%E0%B8%A2%E0%B9%80%E0%B8%84%E0%B8%A3%E0%B8%B7%E0%B9%88%E0%B8%AD%E0%B8%87%E0%B8%AA%E0%B8%B3%E0%B8%AD%E0%B8%B2%E0%B8%87%E0%B8%84%E0%B8%A7%E0%B8%9A%E0%B8%84%E0%B8%B8%E0%B8%A1/",
                    context.FileUploads.Where(f => f.FileSysName == "drug.png").First(),
                    3,
                    1000,
                    3000,
                    CostType.Range,
                    null,
                    null
                ),
                createApplication(
                    "ขอใบอนุญาตขายยาแผนปัจจุบัน",
                    "19010000",
                    null,
                    null,
                    false,
                    null,
                    context.FileUploads.Where(f => f.FileSysName == "drug.png").First(),
                    10,
                    2000,
                    null,
                    CostType.Fixed,
                    null,
                    null
                ),
                createApplication(
                    "จดทะเบียนพาณิชย์",
                    "12007000",
                    null,
                    null,
                    false,
                    "https://www.info.go.th/#!/th/search/329/%E0%B8%88%E0%B8%94%E0%B8%97%E0%B8%B0%E0%B9%80%E0%B8%9A%E0%B8%B5%E0%B8%A2%E0%B8%99%E0%B8%9E%E0%B8%B2%E0%B8%93%E0%B8%B4%E0%B8%8A%E0%B8%A2%E0%B9%8C/",
                    context.FileUploads.Where(f => f.FileSysName == "dbd.png").First(),
                    null,
                    50,
                    null,
                    CostType.Fixed,
                    null,
                    null
                ),
                createApplication(
                    "ขอใบรับแจ้ง/แจ้งแก้ไขเปลี่ยนแปลง/แจ้งโอน การประกอบกิจการสถานที่เก็บรักษาก๊าซปิโตรเลียมเหลว ประเภทสถานที่ใช้ ลักษณะที่สอง",
                    "11005000",
                    null,
                    null,
                    false,
                    "https://info.go.th/#!/th/search/6947/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%AD%E0%B8%AD%E0%B8%81%E0%B9%83%E0%B8%9A%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%81%E0%B8%88%E0%B9%89%E0%B8%87%252F%E0%B9%81%E0%B8%88%E0%B9%89%E0%B8%87%E0%B9%81%E0%B8%81%E0%B9%89%E0%B9%84%E0%B8%82%E0%B9%80%E0%B8%9B%E0%B8%A5%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%99%E0%B9%81%E0%B8%9B%E0%B8%A5%E0%B8%87%252F%E0%B9%81%E0%B8%88%E0%B9%89%E0%B8%87%E0%B9%82%E0%B8%AD%E0%B8%99%20%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%AA%E0%B8%96%E0%B8%B2%E0%B8%99%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B9%80%E0%B8%81%E0%B9%87%E0%B8%9A%E0%B8%A3%E0%B8%B1%E0%B8%81%E0%B8%A9%E0%B8%B2%E0%B8%81%E0%B9%8A%E0%B8%B2%E0%B8%8B%E0%B8%9B%E0%B8%B4%E0%B9%82%E0%B8%95%E0%B8%A3%E0%B9%80%E0%B8%A5%E0%B8%B5%E0%B8%A2%E0%B8%A1%E0%B9%80%E0%B8%AB%E0%B8%A5%E0%B8%A7%20%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B9%80%E0%B8%A0%E0%B8%97%E0%B8%AA%E0%B8%96%E0%B8%B2%E0%B8%99%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B9%83%E0%B8%8A%E0%B9%89%20%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%A9%E0%B8%93%E0%B8%B0%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%AA%E0%B8%AD%E0%B8%87/",
                    context.FileUploads.Where(f => f.FileSysName == "energy.png").First(),
                    1,
                    0,
                    null,
                    CostType.Fixed,
                    null,
                    null
                ),
                createApplication(
                    "ขอใบอนุญาตประกอบกิจการ สถานที่เก็บรักษาก๊าซปิโตรเลียมเหลวประเภทสถานที่ใช้ ลักษณะที่สาม กรณีใช้ถังเก็บและจ่ายก๊าซปิโตรเลียมเหลว แบบทรงกระบอก",
                    "11005000",
                    null,
                    null,
                    false,
                    "https://info.go.th/#!/th/search/46214/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%AD%E0%B8%AD%E0%B8%81%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%20%E0%B8%AA%E0%B8%96%E0%B8%B2%E0%B8%99%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B9%80%E0%B8%81%E0%B9%87%E0%B8%9A%E0%B8%A3%E0%B8%B1%E0%B8%81%E0%B8%A9%E0%B8%B2%E0%B8%81%E0%B9%8A%E0%B8%B2%E0%B8%8B%E0%B8%9B%E0%B8%B4%E0%B9%82%E0%B8%95%E0%B8%A3%E0%B9%80%E0%B8%A5%E0%B8%B5%E0%B8%A2%E0%B8%A1%E0%B9%80%E0%B8%AB%E0%B8%A5%E0%B8%A7%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B9%80%E0%B8%A0%E0%B8%97%E0%B8%AA%E0%B8%96%E0%B8%B2%E0%B8%99%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B9%83%E0%B8%8A%E0%B9%89%20%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%A9%E0%B8%93%E0%B8%B0%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%AA%E0%B8%B2%E0%B8%A1%20%E0%B8%81%E0%B8%A3%E0%B8%93%E0%B8%B5%E0%B9%83%E0%B8%8A%E0%B9%89%E0%B8%96%E0%B8%B1%E0%B8%87%E0%B9%80%E0%B8%81%E0%B9%87%E0%B8%9A%E0%B9%81%E0%B8%A5%E0%B8%B0%E0%B8%88%E0%B9%88%E0%B8%B2%E0%B8%A2%E0%B8%81%E0%B9%8A%E0%B8%B2%E0%B8%8B%E0%B8%81%E0%B9%8A%E0%B8%B2%E0%B8%8B%E0%B8%9B%E0%B8%B4%E0%B9%82%E0%B8%95%E0%B8%A3%E0%B9%80%E0%B8%A5%E0%B8%B5%E0%B8%A2%E0%B8%A1%E0%B9%80%E0%B8%AB%E0%B8%A5%E0%B8%A7%20%E0%B9%81%E0%B8%9A%E0%B8%9A%E0%B8%97%E0%B8%A3%E0%B8%87%E0%B8%81%E0%B8%A3%E0%B8%B0%E0%B8%9A%E0%B8%AD%E0%B8%81%20(%E0%B8%A3%E0%B8%B0%E0%B8%A2%E0%B8%B0%E0%B8%97%E0%B8%B5%E0%B9%88%201%20:%20%E0%B8%82%E0%B8%B1%E0%B9%89%E0%B8%99%E0%B8%95%E0%B8%AD%E0%B8%99%E0%B8%AD%E0%B8%AD%E0%B8%81%E0%B8%84%E0%B8%B3%E0%B8%AA%E0%B8%B1%E0%B9%88%E0%B8%87%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B8%84%E0%B8%B3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95)/",
                    context.FileUploads.Where(f => f.FileSysName == "energy.png").First(),
                    45,
                    0,
                    null,
                    CostType.Fixed,
                    null,
                    null
                ),
            };
            context.Applications.AddOrUpdate(a => a.ApplicationSysName, aList);
            context.SaveChanges(false);
            var aDangerFoodList = createDangerFoodApp(context.FileUploads.Where(f => f.FileSysName == "bangkok.jpg").First());

            context.Applications.AddOrUpdate(a => a.ApplicationSysName, aDangerFoodList.ToArray());
            context.SaveChanges(false);

            var aTList = new List<ApplicationTranslation>();
            foreach (var app in aList)
            {
                int appID = context.Applications.Where(a => a.ApplicationSysName == app.ApplicationSysName).First().ApplicationID;
                aTList.Add(new ApplicationTranslation
                {
                    ApplicationID = appID,
                    ApplicationName = app.ApplicationSysName,
                    ApplicationDetail = "<p><br></p>",
                    LanguageID = 1,
                    ApprovedMailMessage = "<p><br></p>",
                    RejectedMailMessage = "<p><br></p>",
                    SubmitMailMessage = "<p><br></p>",
                });
                aTList.Add(new ApplicationTranslation
                {
                    ApplicationID = appID,
                    ApplicationName = app.ApplicationSysName + " (en)",
                    ApplicationDetail = "<p><br></p>",
                    LanguageID = 2,
                    ApprovedMailMessage = "<p><br></p>",
                    RejectedMailMessage = "<p><br></p>",
                    SubmitMailMessage = "<p><br></p>",
                });
            }
            foreach (var app in aDangerFoodList)
            {
                int appID = context.Applications.Where(a => a.ApplicationSysName == app.ApplicationSysName).First().ApplicationID;
                aTList.Add(new ApplicationTranslation
                {
                    ApplicationID = appID,
                    ApplicationName = app.ApplicationSysName,
                    ApplicationDetail = "<p><br></p>",
                    LanguageID = 1,
                    ApprovedMailMessage = "<p><br></p>",
                    RejectedMailMessage = "<p><br></p>",
                    SubmitMailMessage = "<p><br></p>",
                });
                aTList.Add(new ApplicationTranslation
                {
                    ApplicationID = appID,
                    ApplicationName = app.ApplicationSysName + " (en)",
                    ApplicationDetail = "<p><br></p>",
                    LanguageID = 2,
                    ApprovedMailMessage = "<p><br></p>",
                    RejectedMailMessage = "<p><br></p>",
                    SubmitMailMessage = "<p><br></p>",
                });
            }
            context.ApplicationTranslations.AddOrUpdate(a => new { a.ApplicationID, a.LanguageID }, aTList.ToArray());
            context.SaveChanges(false);

        }

        public const int APP_DANGER_FOOD_CNT = 26;
        public const string APP_DANGER_PREFIX = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";

        private static List<Application> createDangerFoodApp(FileUpload logo)
        {
            List<Application> list = new List<Application>();
            for (int i = 0; i < APP_DANGER_FOOD_CNT; i++)
            {
                var appSysName = APP_DANGER_PREFIX + GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_ITEM_C" + (i + 1), "Apps_SmartQuiz");
                list.Add(
                    createApplication(
                        appSysName,
                        "22000000",
                        null,
                        "BizPortal.AppsHook.BKKDangerFoodAppHook",
                        true,
                        "https://info.go.th/#!/th/search/5014/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B9%80%E0%B8%9B%E0%B9%87%E0%B8%99%E0%B8%AD%E0%B8%B1%E0%B8%99%E0%B8%95%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B8%95%E0%B9%88%E0%B8%AD%E0%B8%AA%E0%B8%B8%E0%B8%82%E0%B8%A0%E0%B8%B2%E0%B8%9E%20(%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B9%83%E0%B8%AB%E0%B8%A1%E0%B9%88)/",
                        logo,
                        30,
                        1000,
                        10000,
                        CostType.Range,
                        BKK_ONLY,
                        BKK_ONLY
                    )
                );
            }
            return list;
        }
        private static string GetResourceWordWithDefault(string name, string resourceName, string defaultValue = "", string culture = "")
        {
            try
            {
                var type = typeof(BizPortal.Resources.Global);
                var assembly = type.Assembly;
                ResourceManager rm = new ResourceManager(string.Format("{0}.{1}", type.Namespace, resourceName), assembly);
                if (string.IsNullOrEmpty(culture))
                {
                    string word = rm.GetString(name);
                    if (!string.IsNullOrEmpty(word))
                    {
                        return word;
                    }
                    else
                    {
                        return defaultValue;
                    }
                }
                else
                {
                    string word = rm.GetString(name, CultureInfo.CreateSpecificCulture(culture));
                    if (!string.IsNullOrEmpty(word))
                    {
                        return word;
                    }
                    else
                    {
                        return defaultValue;
                    }
                }
            }
            catch
            {
                return defaultValue;
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
                CreatedUserID = user.Id,
                UpdatedUserID = user.Id,
                UpdatedDate = DateTime.Now,
            };
        }

        enum CostType
        {
            Fixed,
            Range,
            StartAt
        }
        private static Application createApplication(string appSysName, string orgCode, string appUrl, string appHookClassName, bool enable, string hbUrl, FileUpload logo,
            int? opDays, decimal? cost, decimal? cost2, CostType costType, string remark, string citizenRemark)
        {
            return new Application
            {
                ApplicationSysName = appSysName,
                OrgCode = orgCode,
                ApplicationUrl = appUrl,
                CitizenApplicationUrl = appUrl,
                MultipleRequestSupport = true,
                AppsHookClassName = appHookClassName,
                SingleFormEnabled = true,
                RequestAtOrg = !enable,
                CitizenRequestAtOrg = !enable,
                HandbookUrl = hbUrl,
                CitizenHandbookUrl = hbUrl,
                LogoFileID = null, //logo.FileUploadID,
                OperatingDays = opDays,
                CitizenOperatingDays = opDays,
                OperatingCost = cost,
                CitizenOperatingCost = cost,
                OperatingCost2 = cost2,
                CitizenOperatingCost2 = cost2,
                OperatingCostType = costType.ToString(),
                CitizenOperatingCostType = costType.ToString(),
                ShowRemark = remark != null,
                Remark = remark,
                CitizenShowRemark = citizenRemark != null,
                CitizenRemark = citizenRemark,
                CreatedDate = DateTime.Now,
                CreatedUserID = user.Id,
                UpdatedUserID = user.Id,
                UpdatedDate = DateTime.Now,
            };
        }
    }
}
