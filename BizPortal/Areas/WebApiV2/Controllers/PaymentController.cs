using BizPortal.Areas.WebApi.Controllers;
using BizPortal.ViewModels;
using BizPortal.ViewModels.Select2;
using EGA.WS;
using GreatFriends.ThaiBahtText;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nut;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using System.Web;
using EGA.EGA_CentralLog.Util;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class PaymentController : ApiControllerBase, IProgress<int>
    {
        [HttpGet]
        [Route("Api/v2/Payment/Catalogs")]
        public object Catalogs(string query = "")
        {
            var catalogs = DB.PaymentCatalogs.Where(e => (string.IsNullOrEmpty(query) ? true : e.Description.Contains(query)) && !e.IsDeleted)
                                             .Select(e => new PaymentCatalogSelect2Opt
                                             {
                                                 ID = e.PaymentCatalogCode,
                                                 Text = e.Description
                                             })
                                             .ToList();

            return new { results = catalogs };
        }

        [HttpGet]
        [Route("Api/v2/Payment/HomeCostCenters")]
        public object CostCenters(string cCode, string query = "")
        {
            var costCenters = DB.PaymentHomeCostCenters.Where(e => e.CostCenterCode.Substring(0, 5) == cCode && (string.IsNullOrEmpty(query) ? true : e.Description.Contains(query)) && !e.IsDeleted)
                                                       .Select(e => new PaymentCostCenterSelect2Opt
                                                       {
                                                           ID = e.CostCenterCode,
                                                           Text = e.Description
                                                       })
                                                       .ToList();

            return new { results = costCenters };
        }

        [HttpPost]
        [Route("Api/v2/Payment/UpdateStatus")]
        public object UpdateStatus(BillPaymentUpdateStatus model)
        {
            var url = ConfigurationManager.AppSettings["PMT2"];
            var trans = new List<PaymentTransactionStatus>();
            var updateTrans = new List<PaymentTransaction>();

            if (!string.IsNullOrEmpty(model.IdentityID) && model.ApplicationRequestID.HasValue)
            {
                var paidTrans = PaymentTransaction.List().Where(e => e.ApplicationRequestId == model.ApplicationRequestID && e.PaymentGatewayRef1 != null && e.Status == (int)CGDPaymentStatus.Success).ToList();

                if (paidTrans != null)
                {
                    return paidTrans;
                }
                else
                {
                    trans = PaymentTransaction.List()
                                              .Where(e => e.ApplicationRequestId == model.ApplicationRequestID && e.PaymentGatewayRef1 != null && e.Status != (int)CGDPaymentStatus.Success)
                                              .GroupBy(e => e.CostCenterCode)
                                              .Select(e => new PaymentTransactionStatus { CostCenterCode = e.Key, UpdatedDate = e.Min(s => s.UpdatedDate) })
                                              .ToList();
                }
            }
            else if (model.Token == ConfigurationManager.AppSettings["PMT2Token"])
            {
                trans = PaymentTransaction.List()
                                          .Where(e => e.ApplicationRequestId != null && e.PaymentGatewayRef1 != null && e.Status != (int)CGDPaymentStatus.Success)
                                          .GroupBy(e => e.CostCenterCode)
                                          .Select(e => new PaymentTransactionStatus { CostCenterCode = e.Key, UpdatedDate = e.Min(s => s.UpdatedDate) })
                                          .ToList();
            }
            else 
            {
                throw new BadRequestException();
            }

            var statusTrans = new List<BillPaymentReturnStatusViewModel>();

            foreach (var tran in trans)
            {
                for (var i = 0; i < ((DateTime.Now - tran.UpdatedDate).TotalDays + 1); i++)
                {
                    var args = new Dictionary<string, string> {
                            { "CostCenterCode", tran.CostCenterCode},
                            { "PaymentDate", tran.UpdatedDate.AddDays(i).ToString("yyyy-MM-ddTHH:mm:ss.ffzzz", new CultureInfo("en"))},
                            { "PaymentStatus", ((int)CGDCheckPaymentStatus.All).ToString()}
                        };

                    var result = Api.Get(url, args, ContentType.ApplicationJson);

                    if (result.HasValues && result["returnCode"].ToString() == "000")
                    {
                        var responseData = result.ToObject<BillpaymentManageResponseViewModel<BillPaymentReturnStatusViewModel>>();

                        if (responseData.billPaymentReturnData != null)
                        {
                            statusTrans.AddRange(responseData.billPaymentReturnData);
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(model.IdentityID) && model.ApplicationRequestID.HasValue)
            {
                updateTrans = PaymentTransaction.List().Where(e => e.ApplicationRequestId == model.ApplicationRequestID && e.PaymentGatewayRef1 != null && e.Status != (int)CGDPaymentStatus.Success).ToList();
            }
            else 
            {
                updateTrans = PaymentTransaction.List().Where(e => e.PaymentGatewayRef1 != null && e.Status != (int)CGDPaymentStatus.Success).ToList();
            }

            foreach (var update in updateTrans)
            {
                var status = statusTrans.Where(e => e.cgdRef1 == update.PaymentGatewayRef1 && (e.responseCode == ((int)CGDInvoiceStatus.OnlineConfirm).ToString() || e.responseCode == ((int)CGDInvoiceStatus.Reconcile).ToString() || e.responseCode == ((int)CGDInvoiceStatus.RecreiptCreated).ToString())).FirstOrDefault();

                if (status != null)
                {
                    // update payment transaction
                    update.ConfirmPaidDate = status.paymentDate;
                    update.Status = (int)CGDPaymentStatus.Success;
                    update.UpdatedDate = DateTime.Now;
                    update.Description = "suceessfully";
                    update.Update();
                }
                else
                {
                    update.Status = (int)CGDPaymentStatus.Pending;
                    update.Description = "pending";
                    update.UpdatedDate = DateTime.Now;
                    update.Update();
                }
            }



            return updateTrans;
        }

        [HttpPost]
        [Route("Api/V2/payment/pdf")]
        public async Task<HttpResponseMessage> PDF(BillPaymentReturnDataViewModel model)
        {
            var printData = new JObject();
            var printBarcode = new JObject();

            printData["OrgNameTH"] = "สำนักงานทดสอบ";
            printData["OrgNameEN"] = "Test Organization";
            printData["OrgPhoneNumber"] = "021234567";
            printData["HomeCostCenter"] = "กองคลัง";
            printData["Name"] = "ทดสอบ ระบบ";
            printData["InvoiceStartDate"] = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th"));
            printData["InvoiceEndDate"] = DateTime.Now.AddDays(30).ToString("dd MMMM yyyy เวลา hh:mm น.", new CultureInfo("th"));
            printData["InvoiceCreateDate"] = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th"));
            printData["BillPaymentCode"] = model.billPaymentCode;
            printData["BillerID"] = model.refNo1;
            printData["RefNo1"] = model.refNo1;
            printData["RefNo2"] = model.refNo2;
            printData["RefNo3"] = model.refNo3;
            printData["CGDRef1"] = model.cgdRef1;
            printData["CGDRef2"] = model.cgdRef2;
            printData["CGDRef3"] = model.cgdRef3;
            printData["Amount"] = model.amount.ToString("N2");
            printData["AmountThai"] = model.amount.ThaiBahtText();
            printData["AmountEng"] = model.amount.ToText("eur").Replace("euros", "baht").Replace("zero eurocent", "net").Replace("eurocents", "satang").Replace("eurncent", "satang");
            printBarcode["barcode"] = QRAndBarCodeHelper.FormatOutput(QRAndBarCodeHelper.GenerateBarCodeBase64(model.barcodeString));
            printBarcode["qrcode"] = QRAndBarCodeHelper.FormatOutput(QRAndBarCodeHelper.GenerateQRCodeBase64(model.qrCodeString));
            printData["Images"] = printBarcode;
            printData["Barcodetxt"] = Regex.Replace(Regex.Replace(model.barcodeString, @"\r", " "), @"\v", "");

            var count = 1;
            printData["ItemName" + count] = "ทดสอบ";
            printData["ItemAmount" + count] = model.amount.ToString("N2");
            count = count + 1;

            for (var i = count; i <= 5; i++)
            {
                printData["ItemName" + i] = " ";
                printData["ItemAmount" + i] = " ";
            }

            var arg = new Dictionary<string, string> { { "FileID", ConfigurationValues.PDF_INVOICE_FILEID }, { "Content", JsonConvert.SerializeObject(printData).Replace("\"\"", "\"-\"") } };
            var initResult = Api.Call("/report/init", HttpVerb.PUT, arg);
            var printToken = initResult["Result"].ToString();
            var responsePrintData = Api.Get(string.Format("/report/render" + "?key={0}&type=pdf", printToken));

            //เก็บ billpayment (pdf) ใส่ file server
            if (responsePrintData != null && responsePrintData["List"] != null)
            {
                var serviceTokenPath = ConfigurationManager.AppSettings["FileServiceUploadTokenPath"];
                var serviceUploadPath = ConfigurationManager.AppSettings["FileServicePath"];
                var consumerKey = ConfigurationManager.AppSettings["FileConsumerKey"];
                var secret = ConfigurationManager.AppSettings["FileConsumerSecret"];

                using (var stream = new MemoryStream())
                {
                    var byteData = QRAndBarCodeHelper.Base64urlStringToByte(QRAndBarCodeHelper.Base64urlStringToBase64String(responsePrintData["List"][0].ToString()));

                    stream.Write(byteData, 0, byteData.Length);
                    stream.Position = 0;

                    var result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(stream.ToArray())
                    };
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "ใบเเจ้งชำระเงิน.pdf"
                    };
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    return result;
                }
            }
            else
            {
                throw new Exception("ไม่สามารถสร้างไฟล์ Bill Payment ได้");
            }
        }

        public async Task<(FileItem fileMetadata, string[] paymentGatewayRef)> GenerateCGDBill(BillpaymentParamViewModel model)
        {
            try
            {
                var bills = new List<BillpaymentManageViewModel>();

                int order = 1;

                foreach (var catalog in model.PaymentCatalog)
                {
                    bills.Add(new BillpaymentManageViewModel
                    {
                        order = order,
                        costCenterCode = catalog.HomeCostcenterCode,
                        catalogCode = catalog.CatalogCode,
                        catalogDesc = "",
                        invoiceCreateDate = model.InvoiceCreateDate,
                        invoiceEndDate = model.InvoiceEndDate,
                        invoiceStartDate = model.InvoiceStartDate,
                        amount = catalog.Amount,
                        refNo1 = model.RefNo1,
                        refNo2 = "",
                        refNo3 = "",
                        citizenNo = model.CitizenNo,
                        titleName = model.TitleName,
                        firstName = model.FirstName,
                        lastName = model.LastName,
                        middleName = model.MiddleName,
                        businessNo = model.BusinessNo,
                        businessName = model.BusinessName,
                        gDepartmentCode = "",
                        gCostCenterCode = "",
                        taxID = "",
                        personGroupName = "",
                        tradingParter = "",
                        houseNo = model.HouseNo,
                        buildingName = model.BuildingName,
                        moo = model.Moo,
                        soi = model.Soi,
                        road = model.Road,
                        tambonCode = (model.ProvinceCode + model.AmphurCode + model.TambonCode).PadRight(8, '0'),
                        amphurCode = (model.ProvinceCode + model.AmphurCode).PadRight(8, '0'),
                        provinceCode = (model.ProvinceCode).PadRight(8, '0'),
                        postcode = model.PostCode,
                        mobileNo = model.MobileNo,
                        canSendToMobile = "N",
                        email = model.Email,
                        canSendToEmail = model.CanSendToEmail ? "Y" : "N",
                        billPaymentGroupingCode = model.PersonType == (int)CGDPersonType.Citizen ? "G1" : "G2",
                        personType = model.PersonType,
                        sendDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffzzz", new CultureInfo("en")),
                        dataFlag = "A"
                    });
                    order++;
                }

                // ยิงไปหา CGD

                var postedData = "";

                if (System.Configuration.ConfigurationManager.AppSettings["TestMode"] == "true")
                {
                    var request = new
                    {
                        username = model.UserName,
                        password = model.Password,
                        billPaymentData = bills
                    };

                    postedData = JsonConvert.SerializeObject(request); 
                }
                else 
                {
                    postedData = JsonConvert.SerializeObject(bills);
                }

                var result = Api.Call(ConfigurationManager.AppSettings["PMT1"], HttpVerb.POST, new Dictionary<string, string>(), postedData, ContentType.ApplicationJson);

                // สร้าง Bill payment 
                if (result.HasValues && result["returnCode"].ToString() == "000" && result["billPaymentReturnData"].HasValues)
                {
                    var responseData = result.ToObject<BillpaymentManageResponseViewModel<BillPaymentReturnDataViewModel>>();
                    var billData = responseData.billPaymentReturnData.OrderBy(e => e.order).LastOrDefault();
                    var printData = new JObject();
                    var printBarcode = new JObject();
                    var InvoiceCreateDate = DateTime.ParseExact(model.InvoiceCreateDate, "yyyy-MM-ddTHH:mm:ss.ffzzz", new CultureInfo("en"));
                    var InvoiceStartDate = DateTime.ParseExact(model.InvoiceStartDate, "yyyy-MM-ddTHH:mm:ss.ffzzz", new CultureInfo("en"));
                    var InvoiceEndDate = DateTime.ParseExact(model.InvoiceEndDate, "yyyy-MM-ddTHH:mm:ss.ffzzz", new CultureInfo("en"));
                    var fileMetadata = (FileItem)null;
                    var totalAmount = model.PaymentCatalog.Sum(e => e.Amount);

                    printData["OrgNameTH"] = model.OrgNameTH;
                    printData["OrgNameEN"] = model.OrgNameEN;
                    printData["OrgPhoneNumber"] = model.OrgPhoneNumber;
                    printData["HomeCostCenter"] = string.Join(", ", model.PaymentCatalog.Select(e => e.HomeCostcenterName).Distinct().ToList());
                    printData["Name"] = model.PersonType == (int)CGDPersonType.Citizen ? string.Format("{0}{1}", model.TitleName, string.Join(" ", model.FirstName, model.MiddleName, model.LastName)) : model.BusinessName;
                    printData["InvoiceStartDate"] = InvoiceStartDate.ToString("dd MMMM yyyy", new CultureInfo("th"));
                    printData["InvoiceEndDate"] = InvoiceEndDate.ToString("dd MMMM yyyy", new CultureInfo("th"));
                    printData["InvoiceCreateDate"] = InvoiceCreateDate.ToString("dd MMMM yyyy", new CultureInfo("th"));
                    printData["BillPaymentCode"] = billData.billPaymentCode;
                    printData["BillerID"] = billData.billerID;
                    printData["RefNo1"] = billData.refNo1;
                    printData["RefNo2"] = billData.refNo2;
                    printData["RefNo3"] = billData.refNo3;
                    printData["CGDRef1"] = billData.cgdRef1;
                    printData["CGDRef2"] = billData.cgdRef2;
                    printData["CGDRef3"] = billData.cgdRef3;
                    printData["Amount"] = totalAmount.ToString("N2");
                    printData["AmountThai"] = totalAmount.ThaiBahtText();
                    printData["AmountEng"] = totalAmount.ToText("eur").Replace("euros", "baht").Replace("zero eurocent", "net").Replace("eurocents", "satang").Replace("eurncent", "satang");
                    printBarcode["barcode"] = QRAndBarCodeHelper.FormatOutput(QRAndBarCodeHelper.GenerateBarCodeBase64(billData.barcodeString));
                    printBarcode["qrcode"] = QRAndBarCodeHelper.FormatOutput(QRAndBarCodeHelper.GenerateQRCodeBase64(billData.qrCodeString));
                    printData["Images"] = printBarcode;
                    printData["Barcodetxt"] = Regex.Replace(billData.barcodeString, @"\r", " ");

                    var count = 1;
                    foreach (var catalog in model.PaymentCatalog)
                    {
                        printData["ItemName" + count] = catalog.CatalogName;
                        printData["ItemAmount" + count] = catalog.Amount.ToString("N2");
                        count = count + 1;
                    }

                    for (var i = count; i <= 5; i++)
                    {
                        printData["ItemName" + i] = " ";
                        printData["ItemAmount" + i] = " ";
                    }

                    var arg = new Dictionary<string, string> { { "FileID", ConfigurationValues.PDF_INVOICE_FILEID }, { "Content", JsonConvert.SerializeObject(printData).Replace("\"\"", "\"-\"") } };
                    var initResult = Api.Call("/report/init", HttpVerb.PUT, arg);
                    var printToken = initResult["Result"].ToString();
                    var responsePrintData = Api.Get(string.Format("/report/render" + "?key={0}&type=pdf", printToken));

                    //เก็บ billpayment (pdf) ใส่ file server
                    if (responsePrintData != null && responsePrintData["List"] != null)
                    {
                        var serviceTokenPath = ConfigurationManager.AppSettings["FileServiceUploadTokenPath"];
                        var serviceUploadPath = ConfigurationManager.AppSettings["FileServicePath"];
                        var consumerKey = ConfigurationManager.AppSettings["FileConsumerKey"];
                        var secret = ConfigurationManager.AppSettings["FileConsumerSecret"];

                        using (var stream = new MemoryStream())
                        {
                            var byteData = QRAndBarCodeHelper.Base64urlStringToByte(QRAndBarCodeHelper.Base64urlStringToBase64String(responsePrintData["List"][0].ToString()));

                            stream.Write(byteData, 0, byteData.Length);
                            stream.Position = 0;

                            var responseStream = new MemoryStream(stream.ToArray());
                            var fileInfoJson = JsonConvert.SerializeObject(new
                            {
                                Name = billData.refNo1,
                                ContentType = "application/pdf",
                                Size = responseStream.Length,
                                IsPublic = false
                            });
                            var token = FileHelper.RequestAccessToken(serviceTokenPath, consumerKey, secret, fileInfoJson);
                            fileMetadata = await FileHelper.UploadFile(serviceUploadPath, token, consumerKey, secret, fileInfoJson, responseStream, this);
                        }
                    }
                    else
                    {
                        throw new Exception("ไม่สามารถสร้างไฟล์ Bill Payment ได้");
                    }

                    // เก็บ payment transaction
                    PaymentTransaction.Add(new PaymentTransaction
                    {
                        IdentityId = model.PersonType == (int)CGDPersonType.Citizen ? model.CitizenNo : model.BusinessNo,
                        ApplicationRequestId = model.ApplicationRequestID,
                        ApplicationRequestNumber = model.ApplicationRequestNumber,
                        CostCenterCode = billData.costCenterCode,
                        PaymentGatewayRef1 = billData.cgdRef1,
                        PaymentGatewayRef2 = billData.cgdRef2,
                        PaymentGatewayRef3 = billData.cgdRef3,
                        Amount = totalAmount,
                        Status = (int)CGDPaymentStatus.Pending,
                        Description = "pending"
                    });

                    return (fileMetadata, new string[] { billData.cgdRef1, billData.cgdRef2, billData.cgdRef3 });
                }
                else
                {
                    var errorCode = result["returnCode"].ToString();
                    var errorMessage = "ไม่สามารถสร้าง Bill Payment จากกรมบัญชีกลางได้";

                    switch (errorCode)
                    {
                        case "001":
                            errorMessage = errorMessage + " เนื่องจาก Password ไม่ถูกต้อง";
                            break;
                        case "002":
                            errorMessage = errorMessage + " เนื่องจาก Username ไม่ถูกต้อง";
                            break;
                        case "003":
                            errorMessage = errorMessage + " เนื่องจากรหัสศูนย์ต้นทุนไม่ถูกต้อง";
                            break;
                        case "004":
                            errorMessage = errorMessage + " เนื่องจากรหัสรายการรับชำระไม่ถูกต้อง";
                            break;
                        case "005":
                            errorMessage = errorMessage + " เนื่องจากรายละเอียดเพิ่มเติมของรายการรับชำระยาวเกินกว่าที่กำหนด (50 ตัวอักษร";
                            break;
                        case "006":
                            errorMessage = errorMessage + " เนื่องจากวันที่เริ่มต้นที่สามารถชำระเงินได้ไม่ถูกต้อง";
                            break;
                        case "007":
                            errorMessage = errorMessage + " เนื่องจากวันที่ครบกำหนดชำระเงินไม่ถูกต้อง";
                            break;
                        case "008":
                            errorMessage = errorMessage + " เนื่องจากจำนวนเงินที่ต้องชำระรวมไม่ถูกต้อง";
                            break;
                        case "009":
                            errorMessage = errorMessage + " เนื่องจากรหัสอ้างอิง 1 ไม่ถูกต้อง";
                            break;
                        case "010":
                            errorMessage = errorMessage + " เนื่องจากรหัสอ้างอิง 2 ไม่ถูกต้อง";
                            break;
                        case "011":
                            errorMessage = errorMessage + " เนื่องจากหมายเลขบัตรประชาชนไม่ถูกต้อง";
                            break;
                        case "012":
                            errorMessage = errorMessage + " เนื่องจากชื่อจริงยาวเกินกว่าที่กำหนด (30 ตัวอักษร)";
                            break;
                        case "013":
                            errorMessage = errorMessage + " เนื่องจากชื่อกลางยาวเกินกว่าที่กำหนด (30 ตัวอักษร)";
                            break;
                        case "014":
                            errorMessage = errorMessage + " เนื่องจากนามสกุลยาวเกินกว่าที่กำหนด (30 ตัวอักษร)";
                            break;
                        case "015":
                            errorMessage = errorMessage + " เนื่องจากเลขนิติบุคคลไม่ถูกต้อง";
                            break;
                        case "016":
                            errorMessage = errorMessage + " เนื่องจากชื่อนิติบุคคลยาวเกินกว่าที่กำหนด (1000 ตัวอักษร)";
                            break;
                        case "017":
                            errorMessage = errorMessage + " เนื่องจากรหัสกรมไม่ถูกต้อง";
                            break;
                        case "018":
                            errorMessage = errorMessage + " เนื่องจากหมายเลขประจำตัวผู้เสียภาษีอากรไม่ถูกต้อง";
                            break;
                        case "019":
                            errorMessage = errorMessage + " เนื่องจากบ้านเลขที่ไม่ถูกต้อง";
                            break;
                        case "020":
                            errorMessage = errorMessage + " เนื่องจากที่อยู่ไม่ถูกต้อง";
                            break;
                        case "021":
                            errorMessage = errorMessage + " เนื่องจากเบอร์โทรศัพท์ไม่ถูกต้อง";
                            break;
                        case "022":
                            errorMessage = errorMessage + " เนื่องจาก Email ไม่ถูกต้อง";
                            break;
                        case "023":
                            errorMessage = errorMessage + " เนื่องจาก Control code ไม่ถูกต้อง";
                            break;
                        case "999":
                            errorMessage = errorMessage + " เนื่องจากระบบขัดข้อง";
                            break;
                        default:
                            break;
                    }

                    var log = new ActivityLog { Type = "GenerateCGDBill", Path = "/cgd/payment/dev/bill/create", Data = new { PostData = postedData, ResponseData = result } };
                    log.Create();

                    throw new Exception(errorMessage);
                }
            }
            catch (Exception ex)
            {
                // เก็บ payment transaction
                PaymentTransaction.Add(new PaymentTransaction
                {
                    IdentityId = model.PersonType == (int)CGDPersonType.Citizen ? model.CitizenNo : model.BusinessNo,
                    ApplicationRequestId = model.ApplicationRequestID,
                    ApplicationRequestNumber = model.ApplicationRequestNumber,
                    CostCenterCode = model.PaymentCatalog.Select(e => e.HomeCostcenterCode).FirstOrDefault(),
                    Status = (int)CGDPaymentStatus.Failed,
                    Description = ex.ToString()
                });

                throw new Exception(ex.Message);
            }
        }

        public void Report(int value)
        {
            //throw new NotImplementedException();
        }

        public class PaymentTransactionStatus 
        {
            public string CostCenterCode { get; set; }

            public DateTime UpdatedDate { get; set; }
        }
    }
}
