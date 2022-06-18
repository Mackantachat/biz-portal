using BizPortal.AppsHook;
using BizPortal.DAL.MongoDB;
using BizPortal.Models.Reports;
using BizPortal.ViewModels;
using EGA.WS;
using GreatFriends.ThaiBahtText;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nut;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZXing.Common;

namespace BizPortal.Controllers
{
    public class TestController : ControllerBase
    {
        // GET: Test
        public string Index()
        {
            return Server.MapPath("~/Uploads");
        }

        [HttpPost]
        public ActionResult TestStandardApi()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                var a = 0;
            }

            return Json(new { success = true });
        }

        public ActionResult TestPrintFormPdf(Guid id)
        {
            var model = ApplicationRequestEntity.Get(id);
            string content = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            ViewBag.AppsHookClassName = DB.Applications.Where(o => o.ApplicationID == model.ApplicationID).Select(o => o.AppsHookClassName).SingleOrDefault();
            var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", ViewBag.AppsHookClassName).Unwrap();
            if (hook.HasOrgPdfForm && !AppHookFilter.IsDisableOrgPdfForm(model.PermitName))
            {

                var b = hook.GetOrgPdfFormContent(model, Server.MapPath);
                return File(b, "application/pdf");
            }
            return null;
        }

        public ActionResult TestPrintFormPdfAppHook(string hookName)
        {
            ViewBag.AppsHookClassName = hookName;
            var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", ViewBag.AppsHookClassName).Unwrap();
            if (hook.HasOrgPdfForm)
            {

                var b = hook.GetOrgPdfFormContent(null, Server.MapPath);
                return File(b, "application/pdf");
            }
            return null;
        }

        public ActionResult ConfirmationFormJuristicPDF()
        {
            #region Mockup data
            Models.Reports.ConfirmationFormJuristicPDFModel result = new Models.Reports.ConfirmationFormJuristicPDFModel();
            result.CompanyName = "บริษัท จาง กง สี";
            result.CompanyID = "012345678910";
            result.CompanyVatID = "110-100-1001-11-1";
            result.RequestorName = "นายจาง กวางสำลี";
            result.RequestDateTime = new DateTime(2017, 11, 7, 9, 0, 0);
            result.IsNormal = (Request["IsNormal"] == "true");
            result.IPAddress = "192.168.1.111";
            result.Requests = new List<Models.Reports.ConfirmationFormJuristicPDFModel.ApplicationRequest>();

            Models.Reports.ConfirmationFormJuristicPDFModel.ApplicationRequest d1 = new Models.Reports.ConfirmationFormJuristicPDFModel.ApplicationRequest();
            d1.RequestID = "012345678910";
            d1.ApplicationName = "ขอหนังสือรับรองการแจ้งจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (ไม่เกิน 200 ตร.ม.)";
            d1.OrgName = "สำนักงานเขตดุสิต กรุงเทพมหานคร";
            d1.Duration = 7;

            Models.Reports.ConfirmationFormJuristicPDFModel.ApplicationRequest d2 = new Models.Reports.ConfirmationFormJuristicPDFModel.ApplicationRequest();
            d2.RequestID = "012345678911";
            d2.ApplicationName = "ขอใบอนุญาตขายสุรา";
            d2.OrgName = "กรมสรรพสามิต";
            d2.Duration = 1;
            result.Requests.Add(d1);
            result.Requests.Add(d2);

            for (int i = 0; i < 5; i++)
            {
                d2 = new Models.Reports.ConfirmationFormJuristicPDFModel.ApplicationRequest();
                d2.RequestID = "012345678911";
                d2.ApplicationName = "ขอใบอนุญาตขายสุรา";
                d2.OrgName = "กรมสรรพสามิต";
                d2.Duration = 1;
                result.Requests.Add(d2);
            }

            #endregion


            var b = Utils.Helpers.iTextReportHelper.GetConfirmationFormJuristicReport(result);
            Response.AddHeader("content-disposition", "attachment;filename=Confirmation Form Juristic.pdf");
            return File(b, "application/pdf");

        }

        public ActionResult BillPaymentFormPDF()
        {
            #region Mockup data
            BillPaymentFormPDFModel model = new BillPaymentFormPDFModel();
            model.ProductCode = "47865";
            model.Prefix = "1";
            model.TaxID = "1234567890123";
            model.ServiceCode = "10";
            model.RefCode1 = "0123456789123";
            model.RefCode2 = "3219876530862";
            model.DueDate = DateTime.Parse("2017-10-16");
            model.CustomerName = "นายมั่งมี สุขใจ";

            model.PaymentItems.Add(new BillPaymentFormPDFModel.PaymentItem()
            {
                Sequence = 1,
                Title = "หนังสือรับรองการแจ้งจัดตั้งสถานที่จำหน่ายอาหารหรือสถานที่สะสมอาหาร",
                Amount = 301
            });

            model.PaymentItems.Add(new BillPaymentFormPDFModel.PaymentItem()
            {
                Sequence = 2,
                Title = "EMS",
                Amount = 50
            });

            model.PaymentItems.Add(new BillPaymentFormPDFModel.PaymentItem()
            {
                Sequence = 3,
                Title = "Something",
                Amount = 25
            });
            #endregion


            var b = Utils.Helpers.iTextReportHelper.GetBillPaymentReport(model);
            Response.AddHeader("content-disposition", "attachment;filename=Bill Payment.pdf");
            return File(b, "application/pdf");
        }

        public ActionResult BillPaymentFormOSSPDF()
        {
            #region Mockup data
            BillPaymentFormOSSPDFModel model = new BillPaymentFormOSSPDFModel();
            model.CompanyName = "บ้านเคโอ จำกัด";
            model.CustomerName = "นายพิทักษ์ มโนภัยสิทธากุล";
            model.IdentityID = "0205559001340";
            model.TaxID = "1234567890123";
            model.RequestDateTime = DateTime.Parse("2017-10-16");
            model.IPAddress = "125.25.162.175";
            model.OrganizationName = "สำนักงานเขต กรุงเทพมหานคร";
            model.DueDate = DateTime.Parse("2017-12-24");
            model.DocumentTitle = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: น้ำพริก น้ำจิ้มชนิดต่างๆ ";
            model.ApplicationNumber = "J601219001";
            model.PaymentChannel = "ศูนย์รับคำขออนุญาต (OSS)";
            model.PaymentAddress = "59/1 ถนนพิษณุโลก แขวงดุสิต เขตดุสิต กรุงเทพฯ 10300";

            model.PaymentItems.Add(new BillPaymentFormOSSPDFModel.PaymentItem()
            {
                Sequence = 1,
                Title = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: น้ำพริก น้ำจิ้มชนิดต่างๆ",
                Amount = 10000
            });

            model.PaymentItems.Add(new BillPaymentFormOSSPDFModel.PaymentItem()
            {
                Sequence = 2,
                Title = "ใบอนุญาตขายน้ำอัดลม",
                Amount = 2500
            });

            #endregion


            var b = Utils.Helpers.iTextReportHelper.GetBillPaymentOSSReport(model);
            Response.AddHeader("content-disposition", "attachment;filename=Bill Payment OSS.pdf");
            return File(b, "application/pdf");
        }

        public ActionResult BillPaymentFormCGD()
        {
            var model = new BillpaymentParamViewModel
            {
                PersonType = 1,
                CitizenNo = "1234567890123",
                TitleName = "นาย",
                FirstName = "สมรักษ์",
                MiddleName = "",
                LastName = "คำดี",
                BusinessNo = "12345",
                BusinessName = "ทดสอบ",
                MobileNo = "021234567",
                Email = "test@test.mail.com",
                CanSendToEmail = false,
                RefNo1 = "12345",
                InvoiceCreateDate = DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en")),
                InvoiceStartDate = DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en")),
                InvoiceEndDate = DateTime.Now.AddDays(30).ToString("yyyyMMdd", new CultureInfo("en")),
                PaymentCatalog = new List<BillpaymentCatalogViewModel>
                {
                    new BillpaymentCatalogViewModel
                    {
                        Amount = 500,
                        CatalogCode = "1",
                        CatalogName = "ค่าบริการ",
                        HomeCostcenterCode = "",
                        HomeCostcenterName = "กองกลางคลัง"
                    }
                }
            };

            var name = string.Format("{0}{1}", model.TitleName, string.Join(" ", model.FirstName, model.MiddleName, model.LastName));
            var amount = model.PaymentCatalog.Select(e => e.Amount).DefaultIfEmpty(0).Sum();
            var createDate = model.InvoiceCreateDate;
            var startDate = model.InvoiceStartDate;
            var endDate = model.InvoiceEndDate;
            var refNo1 = model.RefNo1;
            var barcode = "099400015951015611129000139343061122914400000";
            var qrcode = "099400015951015611129000139343061122914400000";

            DateTime InvoiceStartDate = DateTime.MinValue.Date;
            DateTime.TryParseExact(startDate, "yyyyMMdd", new CultureInfo("en"), DateTimeStyles.None, out InvoiceStartDate);

            DateTime InvoiceEndDate = DateTime.MinValue.Date;
            DateTime.TryParseExact(endDate, "yyyyMMdd", new CultureInfo("en"), DateTimeStyles.None, out InvoiceEndDate);

            DateTime InvoiceCreateDate = DateTime.MinValue.Date;
            DateTime.TryParseExact(createDate, "yyyyMMdd", new CultureInfo("en"), DateTimeStyles.None, out InvoiceCreateDate);

            var barcodeBase64 = FormatOutput(GenerateBarCodeBase64(barcode));
            var qrcodeBase64 = FormatOutput(GenerateQRCodeBase64(qrcode));

            var _PrintData = new JObject();
            var _PrintBarcode = new JObject();

            _PrintBarcode["barcode"] = barcodeBase64;
            _PrintBarcode["qrcode"] = qrcodeBase64;
            _PrintData["Images"] = _PrintBarcode;
            _PrintData["barcodetxt"] = barcode;
            _PrintData["Name"] = name;
            _PrintData["InvoiceStartDate"] = InvoiceStartDate.ToString("dd MMMM yyyy", new CultureInfo("th"));
            _PrintData["InvoiceEndDate"] = InvoiceEndDate.ToString("dd MMMM yyyy เวลา hh:mm", new CultureInfo("th"));
            _PrintData["InvoiceCreateDate"] = InvoiceCreateDate.ToString("dd MMMM yyyy", new CultureInfo("th"));
            _PrintData["RefNo1"] = refNo1;
            _PrintData["RefNo2"] = "-";
            _PrintData["Amount"] = amount.ToString("N2");
            _PrintData["AmountThai"] = amount.ThaiBahtText();
            _PrintData["AmountEng"] = amount.ToText("eur").Replace("euros", "baht").Replace("zero eurocent", "net").Replace("eurocents", "satang").Replace("eurncent", "satang");

            //ที่อยู่ดึงจาก mongo
            _PrintData["HouseNo"] = "123";
            _PrintData["BuildingName"] = "-";
            _PrintData["Moo"] = "-";
            _PrintData["Soi"] = "-";
            _PrintData["Road"] = "-";
            _PrintData["TambonCode"] = "-";
            _PrintData["AmphurCode"] = "-";
            _PrintData["ProvinceCode"] = "-";
            _PrintData["PostCode"] = "-";

            var count = 1;
            foreach (var catalog in model.PaymentCatalog)
            {
                _PrintData["ItemName" + count] = catalog.CatalogName;
                _PrintData["ItemAmount" + count] = catalog.Amount.ToString("N2");
                count = count + 1;
            }

            for (var i = count; i <= 5; i++)
            {
                _PrintData["ItemName" + i] = " ";
                _PrintData["ItemAmount" + i] = " ";
            }

            var content = JsonConvert.SerializeObject(_PrintData).Replace("\"\"", "\"-\"");

            //biz ได้รับ ref code มาทำ billpayment             
            var arg = new Dictionary<string, string>();
            arg.Add("FileID", ConfigurationValues.PDF_INVOICE_FILEID);
            arg.Add("Content", content);
            var result = Api.Call("/report/init", HttpVerb.PUT, arg);
            var printToken = result["Result"].ToString();

            var resPrintData = Api.Get(string.Format("/report/render" + "?key={0}&type=pdf", printToken));
            var printResponse = "";

            if (resPrintData["List"] != null)
            {
                printResponse = resPrintData["List"][0].ToString();
            }

            var stream = new MemoryStream();
            var byteArray = Base64urlStringToByte(Base64urlStringToBase64String(printResponse));

            stream.Write(byteArray, 0, byteArray.Length);
            stream.Position = 0;

            var responseStream = new MemoryStream(stream.ToArray());

            return File(responseStream, "application/pdf");
        }

        #region[Helper]
        private string GenerateBarCodeBase64(string data)
        {
            var writer = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    PureBarcode = true,
                    Height = 120,
                    Width = 900,
                    Margin = 0
                }
            };
            var barcode = writer.Write(data);
            MemoryStream stream = new MemoryStream();
            barcode.Save(stream, ImageFormat.Bmp);
            byte[] barcodeBytes = stream.ToArray();

            string encode = ByteToBase64urlString(barcodeBytes);
            return encode;
        }

        private string GenerateQRCodeBase64(string data)
        {
            var writer = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    PureBarcode = true,
                    Height = 120,
                    Width = 120,
                    Margin = 0
                }
            };
            var barcode = writer.Write(data);
            MemoryStream stream = new MemoryStream();
            barcode.Save(stream, ImageFormat.Bmp);
            byte[] barcodeBytes = stream.ToArray();

            string encode = ByteToBase64urlString(barcodeBytes);
            return encode;
        }

        private static string EmptyString = "-";

        private static string FormatOutput(object s)
        {
            string output = Convert.ToString(s);
            if (string.IsNullOrEmpty(output))
            {
                return EmptyString;
            }
            else
            {
                output = output.Trim();
                if (string.IsNullOrEmpty(output))
                    return EmptyString;
                return output;
            }
        }

        private string ByteToBase64urlString(byte[] input)
        {
            StringBuilder result = new StringBuilder(Convert.ToBase64String(input).TrimEnd('='));
            result.Replace('+', '-');
            result.Replace('/', '_');
            return result.ToString();
        }

        private string Base64urlStringToBase64String(string base64urlstring)
        {
            byte[] convertData = Base64urlStringToByte(base64urlstring);
            return Convert.ToBase64String(convertData);
        }

        private static byte[] Base64urlStringToByte(string base64ForUrlInput)
        {
            int padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));
            StringBuilder result = new StringBuilder(base64ForUrlInput, base64ForUrlInput.Length + padChars);
            result.Append(String.Empty.PadRight(padChars, '='));
            result.Replace('-', '+');
            result.Replace('_', '/');
            return Convert.FromBase64String(result.ToString());
        }
        #endregion
    }
}