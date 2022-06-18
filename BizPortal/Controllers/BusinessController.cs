using BizPortal.Utils.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizPortal.ViewModels;
using BizPortal.Resources.PermitResource;
using Org.BouncyCastle.Asn1.Crmf;
using System.Net;
using System.IO;

namespace BizPortal.Controllers
{
    [FilterUser]
    public class BusinessController : ControllerBase
    {
        // GET: Business
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Restaurant()
        {
            return View();
        }

        public ActionResult Retail()
        {
            return View();
        }

        public ActionResult PermitList()
        {
            
            return View();
        }

        public ActionResult PermitListRenew()
        {
            return View();
        }

        public ActionResult PermitListEdit()
        {
            return View();
        }

        public ActionResult AgriproductProcessing() 
        {
            return View();
        }

        [Authorize]
        public ActionResult PermitAll()
        {
            string whiteList = ConfigurationManager.AppSettings["Test.PermitAll.WhiteList"] + "";
            string[] identities = whiteList.Split(',');

            if (!identities.Any(o => o.Trim() == "*") && !identities.Any(o => o.Trim() == IdentityID))
                return new HttpUnauthorizedResult();

            return View();
        }

        public ActionResult PermitListCancel()
        {
            return View();
        }

        public ActionResult CoffeeShop()
        {
            return View();
        }

        public ActionResult Hotel()
        {
            return View();
        }

        public ActionResult Logistics()
        {
            return View();
        }

        public ActionResult MedicineTool() 
        {
            return View();
        }

        public ActionResult PatientService() 
        {
            return View();
        }

        public ActionResult CosmeticShortTerm() 
        {
            return View();
        }

        public ActionResult Spa() 
        {
            return View();
        }

        public ActionResult PetMedicine() 
        {
            return View();
        }

        public ActionResult Construction() 
        {
            return View();
        }

        public ActionResult ElectronicSell()
        {
            return View();
        }

        public ActionResult Fitness()
        {
            return View();
        }

        public ActionResult CarCare()
        {
            return View();
        }

        public ActionResult AccountAndLawConsult()
        {
            return View();
        }

        public ActionResult Cosmetic()
        {
            return View();
        }

        public ActionResult Tourism() 
        {
            return View();
        }

        public ActionResult SoftwareHouse()
        {
            return View();
        }

        public ActionResult CosmeticOnline() 
        {
            return View();
        }

        public ActionResult FabricECommerce()
        {
            return View();
        }

        public ActionResult SparPower()
        {
            return View();
        }

        public ActionResult Education()
        {
            return View();
        }

        public ActionResult SEC() 
        {
            return View();
        }

        public ActionResult OrganicPlant() 
        {
            return View();
        }

        [HttpGet]
        public ActionResult BDBBusinessList(string parmBType)
        {
            string bType = parmBType;
            bType = bType.ToUpper();

            BusinessListViewModel model = new BusinessListViewModel();

            model.AppModel = BusinessDataTable.GetBusnessDataTable(bType);
            model.AppModel = BusinessDataTable.GetSecretKey(model.AppModel);

            switch (bType)
            {
                case "RESTAURANT":
                    ViewBag.Title = "ธุรกิจร้านอาหารและเครื่องดื่ม";
                    break;
                case "RETAIL":
                    ViewBag.Title = "ธุรกิจร้านค้าปลีก";
                    break;
                case "COFFEESHOP":
                    ViewBag.Title = "ธุรกิจ Co-working space";
                    break;
                case "HOTEL":
                    ViewBag.Title = "ธุรกิจโรงแรมและรีสอร์ตขนาดเล็ก";
                    break;
                case "AGP":
                    ViewBag.Title = "ธุรกิจแปรรูปสินค้าเกษตรขนาดเล็ก";
                    break;
                case "LOGISTICS":
                    ViewBag.Title = "ธุรกิจขนส่ง Logistics (ขนส่งสินค้า)";
                    break;
                case "MEDICTOOL":
                    ViewBag.Title = "ธุรกิจอุปกรณ์เครื่องมือทางการแพทย์";
                    break;
                case "PATIENTSERVICE":
                    ViewBag.Title = "ธุรกิจสถานพยาบาล";
                    break;
                case "COSMETICSHORTTERM":
                    ViewBag.Title = "ธุรกิจคลินิกเสริมความงาม (แบบไม่ค้างคืน)";
                    break;
                case "SPA":
                    ViewBag.Title = "กิจกรรมสปาและการนวด";
                    break;
                case "PETMEDICINE":
                    ViewBag.Title = "ธุรกิจสถานพยาบาลสัตว์";
                    break;
                case "CONSTRUCTION":
                    ViewBag.Title = "ธุรกิจก่อสร้างและรับเหมาก่อสร้าง";
                    break;
                case "ELECTRONICSELL":
                    ViewBag.Title = "ธุรกิจซ่อมและขายอุปกรณ์อิเล็กทรอนิกส์";
                    break;
                case "FITNESS":
                    ViewBag.Title = "ธุรกิจฟิตเนส";
                    break;
                case "CARCARE":
                    ViewBag.Title = "ธุรกิจคาร์แคร์";
                    break;
                case "ACCOUNTANDLAWCONSULT":
                    ViewBag.Title = "ธุรกิจให้คำปรึกษาด้านกฎหมายและบัญชี";
                    break;
                case "COSMETIC":
                    ViewBag.Title = "ธุรกิจผลิตครีมบำรุง เครื่องสำอาง น้ำหอม";
                    break;
                case "TOURISM":
                    ViewBag.Title = "ธุรกิจการท่องเที่ยว";
                    break;
                case "SOFTWAREHOUSE":
                    ViewBag.Title = "ธุรกิจพัฒนาแอปพลิเคชัน ซอฟต์แวร์ซื้อมา-ขายไป";
                    break;
                case "COSMETICONLINE":
                    ViewBag.Title = "ธุรกิจขายสินค้า online (ด้านเครื่องสำอาง)";
                    break;
                case "FABRICECOMMERCE":
                    ViewBag.Title = "ธุรกิจ e-Commerce (ด้านเสื้อผ้า)";
                    break;
                case "SPARPOWER":
                    ViewBag.Title = "ธุรกิจพลังงานสำรอง พลังงานทดแทนและขายกระแสไฟฟ้าให้ภาครัฐ";
                    break;
                case "EDUCATION":
                    ViewBag.Title = "ธุรกิจการศึกษา";
                    break;
                case "SEC":
                    ViewBag.Title = "ธุรกิจทางการเงิน";
                    break;
                case "ORGANICPLANT":
                    ViewBag.Title = "ธุรกิจเกษตรปลอดสารพิษ";
                    break;
                default:
                    return RedirectToAction("Index","Home");
            }

            ViewBag.ReturnUrl = string.Format("/th/Business/BDBBusinessList?parmBType={0}", parmBType);

            return View(model);
        }

        [HttpGet]
        public ActionResult DBDHtmlRender(string tsicID) 
        {
            System.Text.StringBuilder strKey = new System.Text.StringBuilder();
            byte[] plainTextBye;
            string strInsertKey2 = string.Empty;
            string strParm = string.Empty;
            string htmlText = string.Empty;
            string strBase64 = string.Empty;

            strKey.Append("b6oYHyU6g4fGSTYu"); // Key1
            strKey.Append("No2usIEJs1BUlqR6HV0S"); // SALT
            strKey.Append(tsicID); // Tsic
            strKey.Append("dga"); // User
            strKey.Append("b6oYHyU6g4fGSTYu"); // Key1

            plainTextBye = System.Text.Encoding.UTF8.GetBytes(strKey.ToString());

            strBase64 = Convert.ToBase64String(plainTextBye);
            strInsertKey2 = strBase64.Substring(0,3);
            strParm = string.Format("{0}{1}{2}",strInsertKey2, "EY4lS3NA1",strBase64.Replace(strInsertKey2,string.Empty));

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://datawarehouse.dbd.go.th/business/profile?code={0}", strParm));
            httpWebRequest.Method = "POST";
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                htmlText = reader.ReadToEnd();
                reader.Close();
            }

            return Content(htmlText, "text/html");
        }
    }
}