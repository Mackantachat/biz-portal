using BizPortal.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace BizPortal.DAL.Seeds
{
    public class _20190104_OrganizationSeed
    {
        public static void Seed(BizPortal.DAL.ApplicationDbContext context, ApplicationUser adminUser)
        {
            var organizations = new List<Organization>
            {
                new Organization
                {
                    OrgCode = "01004000",
                    MinistryCode ="01",
                    DepartmentCode = "004",
                    DivisionCode = "000",
                    OrgSysName = "สำนักงานคณะกรรมการคุ้มครองผู้บริโภค",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.ocpb.go.th"
                },
                new Organization
                {
                    OrgCode = "01011000",
                    MinistryCode ="01",
                    DepartmentCode = "011",
                    DivisionCode = "000",
                    OrgSysName = "สำนักงานคณะกรรมการการพัฒนาระบบราชการ",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.opdc.go.th"
                },
                new Organization
                {
                    OrgCode = "03006000",
                    MinistryCode ="03",
                    DepartmentCode = "006",
                    DivisionCode = "000",
                    OrgSysName = "กรมสรรพสามิต",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.excise.go.th"
                },
                new Organization
                {
                    OrgCode = "03007000",
                    MinistryCode = "03",
                    DepartmentCode = "007",
                    DivisionCode = "000",
                    OrgSysName = "กรมสรรพากร",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.rd.go.th/publish/index.html"
                },
                new Organization
                {
                    OrgCode = "06003000",
                    MinistryCode = "06",
                    DepartmentCode = "003",
                    DivisionCode = "000",
                    OrgSysName = "กรมพัฒนาสังคมและสวัสดิการ",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.dsdw.go.th/"
                },
                new Organization
                {
                    OrgCode = "07006000",
                    MinistryCode = "07",
                    DepartmentCode = "006",
                    DivisionCode = "000",
                    OrgSysName = "กรมปศุสัตว์",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.dld.go.th"
                },
                new Organization
                {
                    OrgCode = "07008000",
                    MinistryCode = "07",
                    DepartmentCode = "008",
                    DivisionCode = "000",
                    OrgSysName = "กรมวิชาการเกษตร",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.doa.go.th"
                },
                new Organization
                {
                    OrgCode = "10008000",
                    MinistryCode = "10",
                    DepartmentCode = "008",
                    DivisionCode = "000",
                    OrgSysName = "บริษัท ทีโอที จำกัด (มหาชน) (ส่วนกลาง)",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.tot.co.th/"
                },
                new Organization
                {
                    OrgCode = "10009000",
                    MinistryCode = "10",
                    DepartmentCode = "009",
                    DivisionCode = "000",
                    OrgSysName = "สำนักงานรัฐบาลอิเล็กทรอนิกส์(องค์การมหาชน)",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.ega.or.th"
                },
                new Organization
                {
                    OrgCode = "11005000",
                    MinistryCode ="11",
                    DepartmentCode = "005",
                    DivisionCode = "000",
                    OrgSysName = "กรมธุรกิจพลังงาน",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.doeb.go.th"
                },
                new Organization
                {
                    OrgCode = "12007000",
                    MinistryCode ="12",
                    DepartmentCode = "007",
                    DivisionCode = "000",
                    OrgSysName = "กรมพัฒนาธุรกิจการค้า",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.dbd.go.th"
                },
                new Organization
                {
                    OrgCode = "12007003",
                    MinistryCode ="12",
                    DepartmentCode = "007",
                    DivisionCode = "003",
                    OrgSysName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดเชียงราย",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.dbd.go.th"
                },
                new Organization
                {
                    OrgCode = "12007016",
                    MinistryCode ="12",
                    DepartmentCode = "007",
                    DivisionCode = "016",
                    OrgSysName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดชลบุรี",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.dbd.go.th"
                },
                new Organization
                {
                    OrgCode = "12007029",
                    MinistryCode ="12",
                    DepartmentCode = "007",
                    DivisionCode = "029",
                    OrgSysName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดนนทบุรี",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.dbd.go.th"
                },
                new Organization
                {
                    OrgCode = "12007033",
                    MinistryCode ="12",
                    DepartmentCode = "007",
                    DivisionCode = "033",
                    OrgSysName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดบุรีรัมย์",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.dbd.go.th"
                },
                new Organization
                {
                    OrgCode = "12007050",
                    MinistryCode ="12",
                    DepartmentCode = "007",
                    DivisionCode = "050",
                    OrgSysName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดระนอง",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.dbd.go.th"
                },
                new Organization
                {
                    OrgCode = "12007078",
                    MinistryCode ="12",
                    DepartmentCode = "007",
                    DivisionCode = "078",
                    OrgSysName = "กองพัฒนาระบบงานสารสนเทศ",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.dbd.go.th"
                },
                new Organization
                {
                    OrgCode = "12007079",
                    MinistryCode ="12",
                    DepartmentCode = "007",
                    DivisionCode = "079",
                    OrgSysName = "กองบริการจดทะเบียนธุรกิจ (ส่วนจดทะเบียนธุรกิจกลาง)",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.dbd.go.th"
                },
                new Organization
                {
                    OrgCode = "12007080",
                    MinistryCode ="12",
                    DepartmentCode = "007",
                    DivisionCode = "080",
                    OrgSysName = "สำนักงานพัฒนาธุรกิจการค้าเขต 1",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.dbd.go.th"
                },
                new Organization
                {
                    OrgCode = "12007081",
                    MinistryCode ="12",
                    DepartmentCode = "007",
                    DivisionCode = "081",
                    OrgSysName = "สำนักงานพัฒนาธุรกิจการค้าเขต 2",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.dbd.go.th"
                },
                new Organization
                {
                    OrgCode = "13003000",
                    MinistryCode ="13",
                    DepartmentCode = "003",
                    DivisionCode = "000",
                    OrgSysName = "กรมการปกครอง",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.dopa.go.th"
                },
                new Organization
                {
                    OrgCode = "13007000",
                    MinistryCode = "13",
                    DepartmentCode = "007",
                    DivisionCode = "000",
                    OrgSysName = "กรมโยธาธิการและผังเมือง",
                    LogoUrl = "https://www.egov.go.th/FilesUpload\\GovernmentAgenCy\\14085710470577.png",
                    Url = "http://www.dpt.go.th"
                },
                new Organization
                {
                    OrgCode = "13008000",
                    MinistryCode ="13",
                    DepartmentCode = "008",
                    DivisionCode = "000",
                    OrgSysName = "กรมส่งเสริมการปกครองท้องถิ่น",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.dla.go.th/"
                },
                new Organization
                {
                    OrgCode = "13009000",
                    MinistryCode = "13",
                    DepartmentCode = "009",
                    DivisionCode = "000",
                    OrgSysName = "การประปานครหลวง",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.mwa.co.th"
                },
                new Organization
                {
                    OrgCode = "13010000",
                    MinistryCode = "13",
                    DepartmentCode = "010",
                    DivisionCode = "000",
                    OrgSysName = "การประปาส่วนภูมิภาค",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.pwa.co.th"
                },
                new Organization
                {
                    OrgCode = "13011000",
                    MinistryCode = "13",
                    DepartmentCode = "011",
                    DivisionCode = "000",
                    OrgSysName = "การไฟฟ้านครหลวง",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.mea.or.th"
                },
                new Organization
                {
                    OrgCode = "13012000",
                    MinistryCode = "13",
                    DepartmentCode = "012",
                    DivisionCode = "000",
                    OrgSysName = "การไฟฟ้าส่วนภูมิภาค",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.pea.co.th/"
                },
                new Organization
                {
                    OrgCode = "15006000",
                    MinistryCode = "15",
                    DepartmentCode = "006",
                    DivisionCode = "000",
                    OrgSysName = "Social Security Office",
                    LogoUrl ="",
                    Url ="http://www.sso.go.th/"
                },
                new Organization
                 {
                    OrgCode = "16005000",
                    MinistryCode ="16",
                    DepartmentCode = "005",
                    DivisionCode = "000",
                    OrgSysName = "กรมส่งเสริมวัฒนธรรม",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.culture.go.th"
                 },
                new Organization
                {
                    OrgCode = "19007000",
                    MinistryCode ="19",
                    DepartmentCode = "007",
                    DivisionCode = "000",
                    OrgSysName = "กรมสนับสนุนบริการสุขภาพ",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.moph.go.th/"
                },
                new Organization
                {
                    OrgCode = "19010000",
                    MinistryCode ="19",
                    DepartmentCode = "010",
                    DivisionCode = "000",
                    OrgSysName = "สำนักงานคณะกรรมการอาหารและยา",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.fda.moph.go.th"
                },
                new Organization
                {
                    OrgCode = "21007006",
                    MinistryCode = "21",
                    DepartmentCode = "007",
                    DivisionCode = "006",
                    OrgSysName = "กองบัญชาการตำรวจนครบาล",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="https://metro.police.go.th/"
                },
                new Organization
                {
                    OrgCode = "21037000",
                    MinistryCode ="21",
                    DepartmentCode = "037",
                    DivisionCode = "000",
                    OrgSysName = "สำนักงานคณะกรรมการกิจการกระจายเสียง กิจการโทรทัศน์ และกิจการโทรคมนาคมแห่งชาติ",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url = "http://www.ntc.or.th/"
                },
                new Organization
                 {
                    OrgCode = "21043000",
                    MinistryCode ="21",
                    DepartmentCode = "043",
                    DivisionCode = "000",
                    OrgSysName = "องค์กรปกครองส่วนท้องถิ่นและองค์กรปกครองพิเศษ",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url =""
                 },
                new Organization
                {
                    OrgCode = "22000000",
                    MinistryCode = "22",
                    DepartmentCode = "000",
                    DivisionCode = "000",
                    OrgSysName = "กรุงเทพมหานคร",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.bangkok.go.th"
                },
                new Organization
                {
                    OrgCode = "21044000",
                    MinistryCode = "21",
                    DepartmentCode = "044",
                    DivisionCode = "000",
                    OrgSysName = "สภาทนายความ ในพระบรมราชูปถัมภ์",
                    LogoUrl ="https://www.egov.go.th/FilesUpload/GovernmentAgenCy/07066217021164.png",
                    Url ="https://www.lawyerscouncil.or.th"
                },
                new Organization
                {
                    OrgCode = "11003000",
                    MinistryCode = "11",
                    DepartmentCode = "003",
                    DivisionCode = "000",
                    OrgSysName = "กรมพัฒนาพลังงานทดแทนและอนุรักษ์พลังงาน",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.dede.go.th"
                },
                new Organization
                {
                    OrgCode = "08003000",
                    MinistryCode = "08",
                    DepartmentCode = "003",
                    DivisionCode = "000",
                    OrgSysName = "กรมการขนส่งทางบก",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.dlt.go.th"
                },
                new Organization
                {
                    OrgCode = "21032000",
                    MinistryCode = "21",
                    DepartmentCode = "032",
                    DivisionCode = "000",
                    OrgSysName = "สำนักงานคณะกรรมการกำกับหลักทรัพย์และตลาดหลักทรัพย์",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.sec.or.th"
                },
                new Organization
                {
                    OrgCode = "05004000",
                    MinistryCode = "05",
                    DepartmentCode = "004",
                    DivisionCode = "000",
                    OrgSysName = "กรมการท่องเที่ยว",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="https://www.dot.go.th"
                },
                new Organization
                {
                    OrgCode = "12014000",
                    MinistryCode = "12",
                    DepartmentCode = "014",
                    DivisionCode = "000",
                    OrgSysName = "สภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.fap.or.th"
                },
                 new Organization
                {
                    OrgCode = "20003000",
                    MinistryCode = "20",
                    DepartmentCode = "003",
                    DivisionCode = "000",
                    OrgSysName = "กรมโรงงานอุตสาหกรรม",
                    LogoUrl ="https://www.egov.go.th/images/icon/service/service-default-icon.png",
                    Url ="http://www.diw.go.th"
                },
            };

            context.Organizations.AddOrUpdate(o => o.OrgCode, organizations.ToArray());
            context.SaveChanges(false);

            var organizationTranslations = new List<OrganizationTranslation>
            {
                //สำนักงานคณะกรรมการคุ้มครองผู้บริโภค
                new OrganizationTranslation
                {
                    OrgCode = "01004000",
                    OrgName = "สำนักงานคณะกรรมการคุ้มครองผู้บริโภค",
                    Abbreviation ="สคบ",
                    Address ="ศูนย์ราชการเฉลิมพระเกียรติ ๘๐ พรรษา ๕ ธันวาคม ๒๕๕๐ อาคาร B ชั้น ๕ ถนนแจ้งวัฒนะ แขวงทุ่งสองห้อง เขตหลักสี่ กรุงเทพฯ ๑๐๒๑๐",
                    LanguageID = 1
                },
                new OrganizationTranslation
                 {
                    OrgCode = "01004000",
                    OrgName = "Office of The Consumer Protection Board",
                    Abbreviation ="สคบ",
                    Address ="ศูนย์ราชการเฉลิมพระเกียรติ ๘๐ พรรษา ๕ ธันวาคม ๒๕๕๐ อาคาร B ชั้น ๕ ถนนแจ้งวัฒนะ แขวงทุ่งสองห้อง เขตหลักสี่ กรุงเทพฯ ๑๐๒๑๐",
                    LanguageID = 2
                 },

                //กรมสรรพสามิต
                new OrganizationTranslation
                 {
                    OrgCode = "03006000",
                    OrgName = "กรมสรรพสามิต",
                    Abbreviation ="",
                    Address ="1488 ถ.นครไชยศรี ดุสิต กทม. 10300  โทรศัพท์ : 02-2415600-18  แฟกซ์ : 02-2411030",
                    LanguageID = 1
                 },
                new OrganizationTranslation
                 {
                    OrgCode = "03006000",
                    OrgName = "The Excise Department",
                    Abbreviation ="",
                    Address ="1488 ถ.นครไชยศรี ดุสิต กทม. 10300  โทรศัพท์ : 02-2415600-18  แฟกซ์ : 02-2411030",
                    LanguageID = 2
                 },

                //สำนักงานคณะกรรมการการพัฒนาระบบราชการ
                new OrganizationTranslation
                 {
                    OrgCode = "01011000",
                    OrgName = "สำนักงานคณะกรรมการการพัฒนาระบบราชการ",
                    Abbreviation ="ก.พ.ร.",
                    Address ="59/1 ถ.พิษณุโลก แขวงดุสิต เขตดุสิต กทม.10300  โทรศัพท์ : 02-2813333  แฟกซ์ : 02-2814937  อีเมล์ : administrator@opdc.go.th",
                    LanguageID = 1
                 },
                new OrganizationTranslation
                 {
                    OrgCode = "01011000",
                    OrgName = "Office of the Public Sector Development Commission",
                    Abbreviation ="OPDC",
                    Address ="59/1 ถ.พิษณุโลก แขวงดุสิต เขตดุสิต กทม.10300  โทรศัพท์ : 02-2813333  แฟกซ์ : 02-2814937  อีเมล์ : administrator@opdc.go.th",
                    LanguageID = 2
                 },

                //กรมสรรพากร
                new OrganizationTranslation
                {
                    OrgCode = "03007000",
                    OrgName = "กรมสรรพากร",
                    Abbreviation ="",
                    Address ="90 อาคารกรมสรรพากร ซ.พหลโยธิน 7 ถ.พหลโยธิน แขวงสามเสนใน เขตพญาไท กทม. 10400  โทรศัพท์ : 02-6173000-9  แฟกซ์ : 02-6173729",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "03007000",
                    OrgName = "The Revenue Department",
                    Abbreviation ="RD",
                    Address =" 90 Soi Phaholyothin 7 , Phaholyothin Rd. , Bangkok 10400 Tel: 1161 sotoso@rd.go.th",
                    LanguageID = 2
                },

                //กรมพัฒนาสังคมและสวัสดิการ
                new OrganizationTranslation
                {
                    OrgCode = "06003000",
                    OrgName = "กรมพัฒนาสังคมและสวัสดิการ",
                    Abbreviation ="",
                    Address ="1034 ถ.กรุงเกษม แขวงมหานาค เขตป้อมปราบศัตรูพ่าย กทม. 10100",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "06003000",
                    OrgName = "Dept. of Social Development and Welfare",
                    Abbreviation ="dsdw",
                    Address ="1034 ถ.กรุงเกษม แขวงมหานาค เขตป้อมปราบศัตรูพ่าย กทม. 10100",
                    LanguageID = 2
                },

                //กรมปศุสัตว์
                new OrganizationTranslation
                {
                    OrgCode = "07006000",
                    OrgName = "กรมปศุสัตว์",
                    Abbreviation ="",
                    Address ="69/1 ถ.พญาไท เขตราชเทวี กทม. 10400 โทรศัพท์ : 02-6534925",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "07006000",
                    OrgName = "Department of Livestock Development",
                    Abbreviation ="",
                    Address ="69/1 Phayathai road, Ratchathewi district, Bangkok 10400 Tel: 02-6534425",
                    LanguageID = 2
                },

                //กรมวิชาการเกษตร
                new OrganizationTranslation
                {
                    OrgCode = "07008000",
                    OrgName = "กรมวิชาการเกษตร",
                    Abbreviation ="",
                    Address ="50 ถนนพหลโยธิน. แขวงลาดยาว. เขตจตุจักร. กรุงเทพฯ 10900",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "07008000",
                    OrgName = "Department of Agriculture",
                    Abbreviation ="",
                    Address ="50 ถนนพหลโยธิน. แขวงลาดยาว. เขตจตุจักร. กรุงเทพฯ 10900",
                    LanguageID = 2
                },

                //บริษัท ทีโอที จำกัด (มหาชน) (ส่วนกลาง)
                new OrganizationTranslation
                {
                    OrgCode = "10008000",
                    OrgName = "บริษัท ทีโอที จำกัด (มหาชน) (ส่วนกลาง)",
                    Abbreviation ="ทีโอที",
                    Address ="89/2 หมู่ 3 ถนนแจ้งวัฒนะ แขวงทุ่งสองห้อง เขตหลักสี่ กรุงเทพ 10210  โทรศัพท์ : 1100",
                    LanguageID = 1
                 },
                new OrganizationTranslation
                 {
                    OrgCode = "10008000",
                    OrgName = "TOT Public Company Limited",
                    Abbreviation ="TOT",
                    Address ="89/2 หมู่ 3 ถนนแจ้งวัฒนะ แขวงทุ่งสองห้อง เขตหลักสี่ กรุงเทพ 10210  โทรศัพท์ : 1100",
                    LanguageID = 2
                 },

                //สํานักงานพัฒนารัฐบาลดิจิทัล (องค์การมหาชน) (สพร.)
                new OrganizationTranslation
                {
                    OrgCode = "10009000",
                    OrgName = "สํานักงานพัฒนารัฐบาลดิจิทัล (องค์การมหาชน) (สพร.)",
                    Abbreviation ="สพร.",
                    Address ="สำนักงานรัฐบาลอิเล็กทรอนิกส์ (องค์การมหาชน)(สรอ.) ชั้น 17 อาคารบางกอกไทยทาวเวอร์ 108 ถนนรางน้ำ แขวงถนนพญาไท เขตราชเทวี กรุงเทพฯ 10400",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "10009000",
                    OrgName = "Digital Government Development Agency (Public Organization)",
                    Abbreviation ="DGA",
                    Address ="17 th Floor, Bangkok Thai Tower Building 108 Rangnam Rd. Phayathai, Ratchatewi, Bangkok 10400",
                    LanguageID = 2
                },

                //กรมธุรกิจพลังงาน
                new OrganizationTranslation
                 {
                    OrgCode = "11005000",
                    OrgName = "กรมธุรกิจพลังงาน",
                    Abbreviation ="ธพ.",
                    Address ="555/2 อาคารเอ็นเนอร์ยี่ คอมเพล็กซ์ ชั้น 19 ถ.วิภาวดีรังสิต แขวงจตุจักร เขตจตุจักร กรุงเทพฯ 10900  โทรศัพท์ : 0 2794 4000",
                    LanguageID = 1
                 },
                new OrganizationTranslation
                 {
                    OrgCode = "11005000",
                    OrgName = "Department of Energy Business, Ministry of Energy",
                    Abbreviation ="ธพ.",
                    Address ="555/2 อาคารเอ็นเนอร์ยี่ คอมเพล็กซ์ ชั้น 19 ถ.วิภาวดีรังสิต แขวงจตุจักร เขตจตุจักร กรุงเทพฯ 10900  โทรศัพท์ : 0 2794 4000",
                    LanguageID = 2
                 },

                //กรมพัฒนาธุรกิจการค้า
                new OrganizationTranslation
                 {
                    OrgCode = "12007000",
                    OrgName = "กรมพัฒนาธุรกิจการค้า",
                    Abbreviation ="DBD",
                    Address ="44/100 ม.1 ถ.นนทบุรี อ.เมือง จ.นนทบุรี 11000  โทรศัพท์ : 02-5475036-50  แฟกซ์ : 02-5474459",
                    LanguageID = 1
                 },
                new OrganizationTranslation
                 {
                    OrgCode = "12007000",
                    OrgName = "Department of Business Development",
                    Abbreviation ="DBD",
                    Address ="44/100 ม.1 ถ.นนทบุรี อ.เมือง จ.นนทบุรี 11000  โทรศัพท์ : 02-5475036-50  แฟกซ์ : 02-5474459",
                    LanguageID = 2
                 },

                //สำนักงานพัฒนาธุรกิจการค้าจังหวัดเชียงราย
                new OrganizationTranslation
                 {
                    OrgCode = "12007003",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดเชียงราย",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าจังหวัดเชียงราย ศาลากลางจังหวัดเชียงรายชั้น 3 ถ.แม่ฟ้าหลวง ต.ริมกก อ.เมือง จ.เชียงราย 57100 โทร. 0 5374 4115-6 โทรสาร. 0 5374 4116 อีเมล. chiangrai@dbd.go.th",
                    LanguageID = 1

                 },
                new OrganizationTranslation
                 {
                    OrgCode = "12007003",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดเชียงราย (en)",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าจังหวัดเชียงราย ศาลากลางจังหวัดเชียงรายชั้น 3 ถ.แม่ฟ้าหลวง ต.ริมกก อ.เมือง จ.เชียงราย 57100 โทร. 0 5374 4115-6 โทรสาร. 0 5374 4116 อีเมล. chiangrai@dbd.go.th",
                    LanguageID = 2

                 },

                //สำนักงานพัฒนาธุรกิจการค้าจังหวัดชลบุรี
                new OrganizationTranslation
                 {
                    OrgCode = "12007016",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดชลบุรี",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าจังหวัดชลบุรี 43/16 ถ.โรงพยาบาลเก่า ต.บางปลาสร้อย อ.เมือง จ.ชลบุรี 20000 โทร. 0 3827 9169 , 0 3827 4812 โทรสาร. 0 3827 9521 อีเมล. chonburi@dbd.go.th",
                    LanguageID = 1
                 },
                new OrganizationTranslation
                 {
                    OrgCode = "12007016",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดชลบุรี (en)",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าจังหวัดชลบุรี 43/16 ถ.โรงพยาบาลเก่า ต.บางปลาสร้อย อ.เมือง จ.ชลบุรี 20000 โทร. 0 3827 9169 , 0 3827 4812 โทรสาร. 0 3827 9521 อีเมล. chonburi@dbd.go.th",
                    LanguageID = 2
                 },

                //สำนักงานพัฒนาธุรกิจการค้าจังหวัดนนทบุรี
                new OrganizationTranslation
                 {
                    OrgCode = "12007029",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดนนทบุรี",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าจังหวัดนนทบุรี 15 ซ.รัตนาธิเบศร์ 6 ต.บางกระสอ อ.เมือง จ.นนทบุรี 11000 โทร. 0 2591 7882 , 0 2589 7958 โทรสาร. 0 2580 1071 อีเมล. nonthaburi@dbd.go.th",
                    LanguageID = 1
                 },
                new OrganizationTranslation
                 {
                    OrgCode = "12007029",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดนนทบุรี (en)",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าจังหวัดนนทบุรี 15 ซ.รัตนาธิเบศร์ 6 ต.บางกระสอ อ.เมือง จ.นนทบุรี 11000 โทร. 0 2591 7882 , 0 2589 7958 โทรสาร. 0 2580 1071 อีเมล. nonthaburi@dbd.go.th",
                    LanguageID = 2
                 },

                //สำนักงานพัฒนาธุรกิจการค้าจังหวัดบุรีรัมย์
                new OrganizationTranslation
                {
                    OrgCode = "12007033",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดบุรีรัมย์",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าจังหวัดบุรีรัมย์ ศูนย์ราชการจังหวัด ชั้น 2 เลขที่ 1159 เขากระโดง ต.เสม็ด อ.เมือง จ.บุรีรัมย์ 31000 โทร. 0 4466 6539  โทรสาร. 0 4466 6540 อีเมล. buriram@dbd.go.th",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "12007033",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดบุรีรัมย์ (en)",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าจังหวัดบุรีรัมย์ ศูนย์ราชการจังหวัด ชั้น 2 เลขที่ 1159 เขากระโดง ต.เสม็ด อ.เมือง จ.บุรีรัมย์ 31000 โทร. 0 4466 6539  โทรสาร. 0 4466 6540 อีเมล. buriram@dbd.go.th",
                    LanguageID = 2
                },

                //สำนักงานพัฒนาธุรกิจการค้าจังหวัดระนอง
                new OrganizationTranslation
                {
                    OrgCode = "12007050",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดระนอง",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าจังหวัดระนอง 15/16 ถ.ลุวัง ต.เขานิเวศน์ อ.เมือง จ.ระนอง 85000 โทร. 0 7782 3743 , 0 7782 1674 โทรสาร. 0 7782 1674 อีเมล.  ranong@dbd.go.th",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "12007050",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าจังหวัดระนอง (en)",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าจังหวัดระนอง 15/16 ถ.ลุวัง ต.เขานิเวศน์ อ.เมือง จ.ระนอง 85000 โทร. 0 7782 3743 , 0 7782 1674 โทรสาร. 0 7782 1674 อีเมล.  ranong@dbd.go.th",
                    LanguageID = 2
                },

                //สำนักงานพัฒนาธุรกิจการค้าจังหวัดระนอง
                new OrganizationTranslation
                {
                    OrgCode = "12007078",
                    OrgName = "กองพัฒนาระบบงานสารสนเทศ",
                    Abbreviation ="",
                    Address ="กรมพัฒนาธุรกิจการค้า 563 ถนน นนทบุรี ตำบลบางกระสอ อำเภอเมือง จังหวัดนนทบุรี 11000 แฟกซ์: 02-547-4459 อีเมล: computer@dbd.go.th ",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "12007078",
                    OrgName = "กองพัฒนาระบบงานสารสนเทศ (en)",
                    Abbreviation ="",
                    Address ="กรมพัฒนาธุรกิจการค้า 563 ถนน นนทบุรี ตำบลบางกระสอ อำเภอเมือง จังหวัดนนทบุรี 11000 แฟกซ์: 02-547-4459 อีเมล: computer@dbd.go.th ",
                    LanguageID = 2
                },

                //กองบริการจดทะเบียนธุรกิจ (ส่วนจดทะเบียนธุรกิจกลาง)
                new OrganizationTranslation
                {
                    OrgCode = "12007079",
                    OrgName = "กองบริการจดทะเบียนธุรกิจ (ส่วนจดทะเบียนธุรกิจกลาง)",
                    Abbreviation ="",
                    Address ="กรมพัฒนาธุรกิจการค้า 563 ถนน นนทบุรี ตำบลบางกระสอ อำเภอเมือง จังหวัดนนทบุรี 11000 แฟกซ์: 02-547-4459 อีเมล: computer@dbd.go.th ",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "12007079",
                    OrgName = "กองบริการจดทะเบียนธุรกิจ (ส่วนจดทะเบียนธุรกิจกลาง) (en)",
                    Abbreviation ="",
                    Address ="กรมพัฒนาธุรกิจการค้า 563 ถนน นนทบุรี ตำบลบางกระสอ อำเภอเมือง จังหวัดนนทบุรี 11000 แฟกซ์: 02-547-4459 อีเมล: computer@dbd.go.th ",
                    LanguageID = 2
                },

                //สำนักงานพัฒนาธุรกิจการค้าเขต 1
                new OrganizationTranslation
                {
                    OrgCode = "12007080",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าเขต 1",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าเขต 1 (ปิ่นเกล้า) อาคารธนาลงกรณ์ ทาวเวอร์ ชั้น 14 ถนนบรมราชชนนี แขวงบางบำหรุ เขตบางพลัด กทม. 10700 โทร. 0 2446 8160-1, 67 , 69 FAX. 0 2446 8191",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "12007080",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าเขต 1 (en)",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าเขต 1 (ปิ่นเกล้า) อาคารธนาลงกรณ์ ทาวเวอร์ ชั้น 14 ถนนบรมราชชนนี แขวงบางบำหรุ เขตบางพลัด กทม. 10700 โทร. 0 2446 8160-1, 67 , 69 FAX. 0 2446 8191",
                    LanguageID = 2
                },

                //สำนักงานพัฒนาธุรกิจการค้าเขต 2
                new OrganizationTranslation
                {
                    OrgCode = "12007081",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าเขต 2",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าเขต 2 (พหลโยธิน) อาคารเลขที่ 78/13 ถนนพระราม 6 (สี่แยกประดิพัทธ์) เขตพญาไท กรุงเทพฯ 10400 โทร. 0 2618 3340-41,45 FAX. 0 2618 3343-4",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "12007081",
                    OrgName = "สำนักงานพัฒนาธุรกิจการค้าเขต 2 (en)",
                    Abbreviation ="",
                    Address ="สำนักงานพัฒนาธุรกิจการค้าเขต 2 (พหลโยธิน) อาคารเลขที่ 78/13 ถนนพระราม 6 (สี่แยกประดิพัทธ์) เขตพญาไท กรุงเทพฯ 10400 โทร. 0 2618 3340-41,45 FAX. 0 2618 3343-4",
                    LanguageID = 2
                },

                //กรมการปกครอง
                new OrganizationTranslation
                {
                    OrgCode = "13003000",
                    OrgName = "กรมการปกครอง",
                    Abbreviation ="ปค.",
                    Address ="กรมการปกครอง ถนนอัษฎางค์ แขวงวัดราชบพิธ เขตพระนคร กทม. 10200 โทร. 0-2221-0151-8 Call Center . 1548",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "13003000",
                    OrgName = "Department Of Provincial Administration",
                    Abbreviation ="ปค.",
                    Address ="กรมการปกครอง ถนนอัษฎางค์ แขวงวัดราชบพิธ เขตพระนคร กทม. 10200 โทร. 0-2221-0151-8 Call Center . 1548",
                    LanguageID = 2
                },

                //กรมโยธาธิการและผังเมือง
                new OrganizationTranslation
                {
                    OrgCode = "13007000",
                    OrgName = "กรมโยธาธิการและผังเมือง",
                    Abbreviation = "",
                    Address = "218/1 ถ.พระรามที่6 แขวงสามเสนใน เขตพญาไท กทม.10400  โทรศัพท์ : 02-2994000  แฟกซ์ : 02-2730895",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "13007000",
                    OrgName = "Department of Public Works and Town & Country Planning",
                    Abbreviation = "",
                    Address = "218/1 ถ.พระรามที่6 แขวงสามเสนใน เขตพญาไท กทม.10400  โทรศัพท์ : 02-2994000  แฟกซ์ : 02-2730895",
                    LanguageID = 2
                },

                //กรมส่งเสริมการปกครองท้องถิ่น
                new OrganizationTranslation
                {
                    OrgCode = "13008000",
                    OrgName = "กรมส่งเสริมการปกครองท้องถิ่น",
                    Abbreviation ="",
                    Address ="วังสวนสุนันทา ถ.ราชสีมา แขวงวชิระ เขตดุสิต กทม.10300  โทรศัพท์ : 02-2419014  แฟกซ์ : 02-2419055",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "13008000",
                    OrgName = "The Department of Local Administration",
                    Abbreviation ="dla",
                    Address ="วังสวนสุนันทา ถ.ราชสีมา แขวงวชิระ เขตดุสิต กทม.10300  โทรศัพท์ : 02-2419014  แฟกซ์ : 02-2419055",
                    LanguageID = 2
                },

                //การประปานครหลวง
                new OrganizationTranslation
                 {
                    OrgCode = "13009000",
                    OrgName = "การประปานครหลวง",
                    Abbreviation ="กปน",
                    Address ="400 ถ.ประชาชื่น แขงทุ่งสองห้อง เขตหลักสี่ กทม.10210  โทรศัพท์ : 02-5040123  แฟกซ์ : 02-5039490",
                    LanguageID = 1

                 },
                new OrganizationTranslation
                 {
                    OrgCode = "13009000",
                    OrgName = "The Metropolitan Waterworks Authority",
                    Abbreviation ="MWA",
                    Address ="400 ถ.ประชาชื่น แขงทุ่งสองห้อง เขตหลักสี่ กทม.10210  โทรศัพท์ : 02-5040123  แฟกซ์ : 02-5039490",
                    LanguageID = 2

                 },

                //การประปาส่วนภูมิภาค
                new OrganizationTranslation
                 {
                    OrgCode = "13010000",
                    OrgName = "การประปาส่วนภูมิภาค",
                    Abbreviation ="กปภ",
                    Address ="72 ซ.แจ้งวัฒนะ 1 ถ.แจ้งวัฒนะ เขตหลักสี่ กทม.10210  โทรศัพท์ : 02-5511020  แฟกซ์ : 02-5511239",
                    LanguageID = 1

                 },
                new OrganizationTranslation
                 {
                    OrgCode = "13010000",
                    OrgName = "The Provincial Waterworks Authority",
                    Abbreviation ="pwa",
                    Address ="72 ซ.แจ้งวัฒนะ 1 ถ.แจ้งวัฒนะ เขตหลักสี่ กทม.10210  โทรศัพท์ : 02-5511020  แฟกซ์ : 02-5511239",
                    LanguageID = 2

                 },

                //การไฟฟ้านครหลวง
                new OrganizationTranslation
                 {
                    OrgCode = "13011000",
                    OrgName = "การไฟฟ้านครหลวง",
                    Abbreviation ="กฟน",
                    Address ="30 ซ.ชิดลม ถ.เพลินจิต แขวงลุมพนี เขตปทุมวัน กทม.10330  โทรศัพท์ : 02-2549550  แฟกซ์ : 02-2531424",
                    LanguageID = 1

                 },
                new OrganizationTranslation
                 {
                    OrgCode = "13011000",
                    OrgName = "The Metropolitan Electricity Authority",
                    Abbreviation ="MEA",
                    Address ="30 ซ.ชิดลม ถ.เพลินจิต แขวงลุมพนี เขตปทุมวัน กทม.10330  โทรศัพท์ : 02-2549550  แฟกซ์ : 02-2531424",
                    LanguageID = 2

                 },

                //การไฟฟ้าส่วนภูมิภาค
                new OrganizationTranslation
                 {
                    OrgCode = "13012000",
                    OrgName = "การไฟฟ้าส่วนภูมิภาค",
                    Abbreviation ="กฟภ",
                    Address ="200 ถ. งามวงศ์วาน เขต จตุจักร กรุงเทพฯ 10900  โทรศัพท์ : 02-5890100-1",
                    LanguageID = 1

                 },
                new OrganizationTranslation
                 {
                    OrgCode = "13012000",
                    OrgName = "The Provincial Electricity Authority",
                    Abbreviation ="PEA",
                    Address ="200 ถ. งามวงศ์วาน เขต จตุจักร กรุงเทพฯ 10900  โทรศัพท์ : 02-5890100-1",
                    LanguageID = 2

                 },

                //สํานักงานประกันสังคม
                new OrganizationTranslation
                 {
                    OrgCode = "15006000",
                    OrgName = "สํานักงานประกันสังคม",
                    Abbreviation ="สปส",
                    Address ="",
                    LanguageID = 1

                 },
                new OrganizationTranslation
                 {
                    OrgCode = "15006000",
                    OrgName = "Social Security Office",
                    Abbreviation ="SSO",
                    Address ="",
                    LanguageID = 2

                 },

                //กรมส่งเสริมวัฒนธรรม
                new OrganizationTranslation
                {
                    OrgCode = "16005000",
                    OrgName = "กรมส่งเสริมวัฒนธรรม",
                    Abbreviation ="สวธ",
                    Address ="ถ.รัชดาภิเษก เขตห้วยขวาง กทม.10320  โทรศัพท์ : 02-2470013-9  แฟกซ์ : 02-6452961",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "16005000",
                    OrgName = "Department of Cultural Promotion",
                    Abbreviation ="สวธ",
                    Address ="ถ.รัชดาภิเษก เขตห้วยขวาง กทม.10320  โทรศัพท์ : 02-2470013-9  แฟกซ์ : 02-6452961",
                    LanguageID = 2
                },

                //กรมสนับสนุนบริการสุขภาพ
                new OrganizationTranslation
                 {
                    OrgCode = "19007000",
                    OrgName = "กรมสนับสนุนบริการสุขภาพ",
                    Abbreviation ="",
                    Address ="ถ.ติวานนท์ อ.เมือง จ.นนทบุรี 11000",
                    LanguageID = 1
                 },
                new OrganizationTranslation
                 {
                    OrgCode = "19007000",
                    OrgName = "Department of Health Service Support",
                    Abbreviation ="",
                    Address ="ถ.ติวานนท์ อ.เมือง จ.นนทบุรี 11000",
                    LanguageID = 2
                 },

                //สำนักงานคณะกรรมการอาหารและยา
                new OrganizationTranslation
                {
                    OrgCode = "19010000",
                    OrgName = "สำนักงานคณะกรรมการอาหารและยา",
                    Abbreviation ="อย.",
                    Address ="ถ.ติวานนท์ อ.เมือง จ.นนทบุรี 11000  โทรศัพท์ : 02-5907000  แฟกซ์ : 02-5901776",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "19010000",
                    OrgName = "Food and Drug Administration",
                    Abbreviation ="อย.",
                    Address ="ถ.ติวานนท์ อ.เมือง จ.นนทบุรี 11000  โทรศัพท์ : 02-5907000  แฟกซ์ : 02-5901776",
                    LanguageID = 2
                },

                 //กองบัญชาการตำรวจนครบาล
                new OrganizationTranslation
                {
                    OrgCode = "21007006",
                    OrgName = "กองบัญชาการตำรวจนครบาล",
                    Abbreviation ="",
                    Address ="",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "21007006",
                    OrgName = "Metropolitan Police Bureau",
                    Abbreviation ="",
                    Address ="",
                    LanguageID = 2
                },

                //สำนักงานคณะกรรมการกิจการกระจายเสียง กิจการโทรทัศน์ และกิจการโทรคมนาคมแห่งชาติ
                new OrganizationTranslation
                {
                    OrgCode = "21037000",
                    OrgName = "สำนักงานคณะกรรมการกิจการกระจายเสียง กิจการโทรทัศน์ และกิจการโทรคมนาคมแห่งชาติ",
                    Abbreviation ="กสทช.",
                    Address ="87 ถนนพหลโยธิน ซอย 8 \r\nแขวงสามเสนใน เขตพญาไท กทม 10400 \r\nโทรศัพท์  02-670-8888",
                    LanguageID = 1
                },
                new OrganizationTranslation
                {
                    OrgCode = "21037000",
                    OrgName = "Office of the National Broadcasting and Telecommunications Commission",
                    Abbreviation ="กสทช.",
                    Address ="87 ถนนพหลโยธิน ซอย 8 \r\nแขวงสามเสนใน เขตพญาไท กทม 10400 \r\nโทรศัพท์  02-670-8888",
                    LanguageID = 2
                },

                //องค์กรปกครองส่วนท้องถิ่นและองค์กรปกครองพิเศษ
                new OrganizationTranslation
                 {
                    OrgCode = "21043000",
                    OrgName = "องค์กรปกครองส่วนท้องถิ่นและองค์กรปกครองพิเศษ",
                    Abbreviation ="",
                    Address ="",
                    LanguageID = 1

                 },
                new OrganizationTranslation
                 {
                    OrgCode = "21043000",
                    OrgName = "องค์กรปกครองส่วนท้องถิ่นและองค์กรปกครองพิเศษ",
                    Abbreviation ="",
                    Address ="",
                    LanguageID = 2

                 },

                //กรุงเทพมหานคร
                new OrganizationTranslation
                {
                    OrgCode = "22000000",
                    OrgName = "กรุงเทพมหานคร",
                    Abbreviation ="กทม",
                    Address ="173 ถนนดินสอ แขวงเสาชิงช้า เขตพระนคร กรุงเทพฯ 10200",
                    LanguageID = 1
                },
                new OrganizationTranslation
                 {
                    OrgCode = "22000000",
                    OrgName = "Bangkok Metropolitan Administration",
                    Abbreviation ="BKK",
                    Address ="173 ถนนดินสอ แขวงเสาชิงช้า เขตพระนคร กรุงเทพฯ 10200",
                    LanguageID = 2
                 },

                //สภาทนายความ ในพระบรมราชูปถัมภ์
                new OrganizationTranslation
                {
                    OrgCode = "21044000",
                    OrgName = "สภาทนายความ ในพระบรมราชูปถัมภ์",
                    Abbreviation ="",
                    Address ="249 ถนนพหลโยธิน แขวงอนุสาวรีย์ เขตบางเขน กรุงเทพมหานคร 10220",
                    LanguageID = 1
                },
                new OrganizationTranslation
                 {
                    OrgCode = "21044000",
                    OrgName = "Lawyers Council Under the Royal Patronage",
                    Abbreviation ="",
                    Address ="249 ถนนพหลโยธิน แขวงอนุสาวรีย์ เขตบางเขน กรุงเทพมหานคร 10220",
                    LanguageID = 2
                 },

                //กรมพัฒนาพลังงานทดแทนและอนุรักษ์พลังงาน
                new OrganizationTranslation
                {
                    OrgCode = "11003000",
                    OrgName = "กรมพัฒนาพลังงานทดแทนและอนุรักษ์พลังงาน",
                    Abbreviation ="พพ.",
                    Address ="17 ถ.พระรามที่1 แขวงรองเมือง เขตปทุมวัน กทม.10330  โทรศัพท์ : 02-2230021-9  แฟกซ์ : 02-2261416",
                    LanguageID = 1
                },
                new OrganizationTranslation
                 {
                    OrgCode = "11003000",
                    OrgName = "Department of Alternative Energy Development and Efficiency ",
                    Abbreviation ="DEDE",
                    Address ="17 Rama 1 Rd, Kasatsuk Bridge, Pathumwan, Bangkok 10330 Tel. : 02-2230021-9  Fax : 02-2261416",
                    LanguageID = 2
                 },

                //กรมการขนส่งทางบก
                new OrganizationTranslation
                {
                    OrgCode = "08003000",
                    OrgName = "กรมการขนส่งทางบก",
                    Abbreviation ="ขบ.",
                    //Address ="1032 ถ.พหลโยธิน จตุจักร กทม. 10900  โทรศัพท์ : 02-2718888  แฟกซ์ : 02-271880",
                    Address ="1.กรมการขนส่งทางบก(กรุงเทพมหานคร): ส่วนประกอบการขนส่งสินค้า สำนักงานขนส่งสินค้า  2.สำนักงานขนส่งจังหวัด(ส่วนภูมิภาค): สำนักงานขนส่งจังหวัดที่ยื่นประกอบการ",
                    LanguageID = 1
                },
                new OrganizationTranslation
                 {
                    OrgCode = "08003000",
                    OrgName = "Department of Land Transport",
                    Abbreviation ="DLT",
                    //Address ="Chom Phon 1032 Phaholyothin Road, Chatuchak, Bangkok 10900 Tel. : 02-2718888  Fax : 02-271880",
                    Address ="1.กรมการขนส่งทางบก(กรุงเทพมหานคร): ส่วนประกอบการขนส่งสินค้า สำนักงานขนส่งสินค้า  2.สำนักงานขนส่งจังหวัด(ส่วนภูมิภาค): สำนักงานขนส่งจังหวัดที่ยื่นประกอบการ",
                    LanguageID = 2
                 },

                //สำนักงานคณะกรรมการกำกับหลักทรัพย์และตลาดหลักทรัพย์
                new OrganizationTranslation
                {
                    OrgCode = "21032000",
                    OrgName = "สำนักงานคณะกรรมการกำกับหลักทรัพย์และตลาดหลักทรัพย์",
                    Abbreviation ="กลต",
                    Address ="333/3 ถนนวิภาวดีรังสิต แขวงจอมพล เขตจตุจักร กรุงเทพมหานคร 10900",
                    LanguageID = 1
                },
                new OrganizationTranslation
                 {
                    OrgCode = "21032000",
                    OrgName = "The Securities and Exchange Commission, Thailand",
                    Abbreviation ="SEC",
                    Address ="333/3 Vibhavadi-Rangsit Road, Chomphon, Chatuchak Bangkok 10900",
                    LanguageID = 2
                 },

                //กรมการท่องเที่ยว
                new OrganizationTranslation
                {
                    OrgCode = "05004000",
                    OrgName = "กรมการท่องเที่ยว",
                    Abbreviation ="กทท",
                    Address ="สนามกีฬาแห่งชาติ ถนนพระรามที่ 1 แขวงวังใหม่ เขตปทุมวัน กรุงเทพฯ 10330",
                    LanguageID = 1
                },
                new OrganizationTranslation
                 {
                    OrgCode = "05004000",
                    OrgName = "Department of Tourism",
                    Abbreviation ="DOT",
                    Address ="สนามกีฬาแห่งชาติ ถนนพระรามที่ 1 แขวงวังใหม่ เขตปทุมวัน กรุงเทพฯ 10330",
                    LanguageID = 2
                 },

                //สภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์
                new OrganizationTranslation
                {
                    OrgCode = "12014000",
                    OrgName = "สภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์",
                    Abbreviation ="",
                    Address ="อาคารสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ เลขที่ 133 ถนนสุขุมวิท 21 (อโศก) แขวงคลองเตยเหนือ เขตวัฒนา กรุงเทพฯ 10110 โทรศัพท์ 0-2685-2500 โทรสาร 0-2685-2501",
                    LanguageID = 1
                },
                new OrganizationTranslation
                 {
                    OrgCode = "12014000",
                    OrgName = "Federetion of Accounting Professions Under The Royal Patronage of His Majesty The King",
                    Abbreviation ="TFAC",
                    Address ="Federation of Accounting Professions Building 133 Sukhumvit 21 Road, Wattana Dist., Bangkok 10110 Tel: (+66)02-685-2500 Fax: (+66)02-685-2501",
                    LanguageID = 2
                 },

                //กรมโรงงานอุตสาหกรรม 
                new OrganizationTranslation
                {
                    OrgCode = "20003000",
                    OrgName = "กรมโรงงานอุตสาหกรรม ",
                    Abbreviation ="",
                    Address ="75/6 ถ.พระรามที่ 6 แขวงทุ่งพญาไท เขตราชเทวี กรุงเทพฯ 10400 โทร. 66-(02)-202-4000 และ 3967 โทรสาร 66-(02)-354-3390",
                    LanguageID = 1
                },
                new OrganizationTranslation
                 {
                    OrgCode = "20003000",
                    OrgName = "Department of Industrial works",
                    Abbreviation ="DIW",
                    Address ="75/6 RAMA VI ROAD, RATCHATHEWI, BANGKOK 10400 TEL. 66-(0)2-202-4000,4014 FAX. 66-(0)2-354-3390",
                    LanguageID = 2
                 }
            };

            context.OrganizationTranslations.AddOrUpdate(o => new { o.OrgCode, o.LanguageID }, organizationTranslations.ToArray());
            context.SaveChanges(false);
        }
    }
}
