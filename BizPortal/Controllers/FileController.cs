using BizPortal.DAL.MongoDB;
using BizPortal.Models;
using BizPortal.Models.Reports;
using BizPortal.Service;
using BizPortal.Utils;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BizPortal.Controllers
{
    public class FileController : ControllerBase
    {
        private EGA.WS.EGAWSAPI _api;

        public EGA.WS.EGAWSAPI Api
        {
            get
            {
                if (_api == null)
                {
                    _api = EGA.WS.EGAWSAPI.CreateInstance(
                        System.Configuration.ConfigurationManager.AppSettings["ConsumerKey"],
                        System.Configuration.ConfigurationManager.AppSettings["ConsumerSecret"]);
                }

                return _api;
            }
        }

        [HttpGet]
        public ActionResult GetV2(string id, string fid, Guid? rid, Guid? tid, string name = "")
        {
            if (fid != null)
            {
                id = fid;
            }
            FileMetadataEntity file;
            if (rid.HasValue)
            {
                file = FileMetadataEntity.Get(id, rid.Value);
            }
            else
            {
                file = FileMetadataEntity.GetWithTransaction(id, tid.Value);

                if (file == null && User.Identity.IsAuthenticated)
                {
                    return GetByMetadata(id, name, "");
                }
            }

            Func<BizPortal.ViewModels.V2.FileMetadata, ActionResult> valFilePermission = (metadata) =>
            {
                if (User.Identity.IsAuthenticated
                    && (User.IsInRole(ConfigurationValues.ROLES_OPDC_AGENT_NAME) || User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME) || User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME) || User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME) || User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME)))
                {
                    // กรณีเป็น Admin หรือ หน่วยงาน สามารถเปิดไฟล์ได้เลย
                    return File(metadata.GetBytes(), metadata.ContentType ?? "application/octet-stream"/*, metadata.FileName*/);
                }
                else
                {
                    if (metadata.Extras != null && metadata.Extras.ContainsKey("OWNER_IDENT_ID"))
                    {
                        // กรณีระบุเลข 13 หลักของเจ้าของไฟล์
                        if (User.Identity.IsAuthenticated && metadata.Extras["OWNER_IDENT_ID"] == IdentityID)
                        {
                            // มีสิทธิ์เปิดไฟล์
                            return File(metadata.GetBytes(), metadata.ContentType ?? "application/octet-stream"/*, metadata.FileName*/);
                        }
                        else
                        {
                            return HttpNotFound();
                        }
                    }
                    else
                    {
                        // กรณีไม่ระบบเลข 13 หลักของเจ้าของไฟล์ ให้เปิดไฟล์ได้เลย
                        return File(metadata.GetBytes(), metadata.ContentType ?? "application/octet-stream"/*, metadata.FileName*/);
                    }
                }
            };

            if (file != null)
            {
                var metadata = new BizPortal.ViewModels.V2.FileMetadata() { FileID = file.FileID, FileName = file.FileName, ContentType = file.ContentType, Extras = new Dictionary<string, string>() };
                if (file.Extras != null)
                {
                    foreach (var extra in file.Extras)
                    {
                        metadata.Extras.Add(extra.Key, extra.Value != null ? extra.Value.ToString() : string.Empty);
                    }
                }

                if (file.ContentType == "file/url" && !string.IsNullOrEmpty(file.FileURL))
                {
                    return Redirect(file.FileURL);
                }
                else
                {
                    return valFilePermission(metadata);
                }
            }
            else
            {
                var request = ApplicationRequestEntity.Get(rid.Value);

                if (request != null)
                {
                    if (request.GovFiles != null && request.GovFiles.Count > 0)
                    {
                        file = request.GovFiles.Where(o => o.FileID == id).FirstOrDefault();
                        if (file != null)
                        {
                            var metadata = new BizPortal.ViewModels.V2.FileMetadata() { FileID = file.FileID, FileName = file.FileName, ContentType = file.ContentType, Extras = new Dictionary<string, string>() };
                            if (file.Extras != null)
                            {
                                foreach (var extra in file.Extras)
                                {
                                    metadata.Extras.Add(extra.Key, extra.Value != null ? extra.Value.ToString() : string.Empty);
                                }
                            }

                            return valFilePermission(metadata);
                        }
                    }

                    if (request.EPermitFiles != null && request.EPermitFiles.Count > 0)
                    {
                        file = request.EPermitFiles.Where(o => o.FileID == id).FirstOrDefault();
                        if (file != null)
                        {
                            var metadata = new BizPortal.ViewModels.V2.FileMetadata() { FileID = file.FileID, FileName = file.FileName, ContentType = file.ContentType, Extras = new Dictionary<string, string>() };
                            if (file.Extras != null)
                            {
                                foreach (var extra in file.Extras)
                                {
                                    metadata.Extras.Add(extra.Key, extra.Value != null ? extra.Value.ToString() : string.Empty);
                                }
                            }

                            return valFilePermission(metadata);
                        }
                    }

                    if (request.BillPaymentFiles != null && request.BillPaymentFiles.Count > 0)
                    {
                        file = request.BillPaymentFiles.Where(o => o.FileID == id).FirstOrDefault();
                        if (file != null)
                        {
                            var metadata = new BizPortal.ViewModels.V2.FileMetadata() { FileID = file.FileID, FileName = file.FileName, ContentType = file.ContentType, Extras = new Dictionary<string, string>() };
                            if (file.Extras != null)
                            {
                                foreach (var extra in file.Extras)
                                {
                                    metadata.Extras.Add(extra.Key, extra.Value != null ? extra.Value.ToString() : string.Empty);
                                }
                            }

                            return valFilePermission(metadata);
                        }
                    }
                }

                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult GetByMetadata(string id, string fileName, string contentType, string fileUrl = "")
        {
            if (contentType == "file/url")
            {
                return Redirect(!string.IsNullOrEmpty(fileUrl) ? fileUrl : fileName);
            }
            else
            {
                var metadata = new BizPortal.ViewModels.V2.FileMetadata() { FileID = id, FileName = fileName, ContentType = contentType };

                if (string.IsNullOrEmpty(metadata.ContentType))
                {
                    if (fileName.Contains("pdf"))
                    {
                        metadata.ContentType = "application/pdf";
                    }
                    else if (fileName.Contains("jpg") || fileName.Contains("jpeg"))
                    {
                        metadata.ContentType = "image/jpeg";
                    }
                    else if (fileName.Contains("png"))
                    {
                        metadata.ContentType = "image/png";
                    }
                    else
                    {
                        metadata.ContentType = "application/octet-stream";
                    }
                }

                return File(metadata.GetBytes(), metadata.ContentType);
            }
        }

        [HttpGet]
        public ActionResult GetThumbnail(int? id = null, int? cid = null)
        {
            FileUpload file = DB.FileUploads.Where(o => o.FileUploadID == id && !o.IsDeleted).SingleOrDefault();
            byte[] fileBytes = null;

            if (file != null && System.IO.File.Exists(Server.MapPath(Url.Content(file.AbsolutePath))))
            {
                fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(Url.Content(file.AbsolutePath)));
                Response.AddHeader("Content-Disposition", "filename=" + file.FileName);
            }
            else
            {
                var categoryID = cid != null ? cid.Value : 0;
                switch (categoryID)
                {
                    case 1:
                        fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(Url.Content("~/Content/Images/businessLogo/menu_02.jpg")));
                        break;
                    case 3:
                        fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(Url.Content("~/Content/Images/businessLogo/menu_04.jpg")));
                        break;
                    case 4:
                        fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(Url.Content("~/Content/Images/businessLogo/menu_01.jpg")));
                        break;
                    case 5:
                        fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(Url.Content("~/Content/Images/businessLogo/menu_05.jpg")));
                        break;
                    case 2:
                    default:
                        fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(Url.Content("~/Content/Images/businessLogo/menu_03.jpg")));
                        break;
                }
                Response.AddHeader("Content-Disposition", "filename=thumbnail.jpg");
            }

            if (fileBytes == null)
                return HttpNotFound();

            return new FileContentResult(fileBytes, MediaTypeNames.Image.Jpeg);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            FileUpload file = DB.FileUploads.Where(o => o.FileUploadID == id && !o.IsDeleted).SingleOrDefault();

            if (file == null || !System.IO.File.Exists(Server.MapPath(Url.Content(file.AbsolutePath))))
            {
                return HttpNotFound();
            }

            // Local File
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(Url.Content(file.AbsolutePath)));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.FileName);
        }

        [HttpGet]
        public void GetWS(string consumerKey, string id)
        {
            var data = BizPortal.Utils.EgaContentStore.Get(consumerKey, id);
            var file = JsonConvert.DeserializeObject<FileUploadResponseViewModel>(data.ToString());

            if (file == null)
            {
                Response.StatusCode = 404;
                Response.End();
            }

            byte[] strDecode = Base64UrlStringHelper.Base64urlStringToByte(file.Data);

            Response.Clear();
            Response.ContentType = file.FileType ?? "application/octet-stream";
            Response.AddHeader("Content-Disposition", "filename=" + file.FileName);
            Response.BinaryWrite(strDecode);
        }

        public ActionResult GetPredoc(string docName, string id = null)
        {
            try
            {
                var predocService = new PredocService();
                var predoc = predocService.GetPredoc(User, docName, id);
                return File(predoc.Content, predoc.MimeType);
            }
            catch (PredocService.UnknownPredocException)
            {
                return HttpNotFound();
            }
            catch (PredocService.PredocNotFoundException)
            {
                return HttpNotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return new HttpUnauthorizedResult();
            }
        }

        public ActionResult CheckPredocFile(string docName, string id = null)
        {
            PredocService.PredocInfo predoc = null;
            bool isError = false;
            try
            {
                //Convert.ToInt16("s"); //สำหรับ test error
                var predocService = new PredocService();
                predoc = predocService.GetPredoc(User, docName, id);
            }
            catch (PredocService.UnknownPredocException)
            {
                isError = true;
            }
            catch (PredocService.PredocNotFoundException)
            {
                isError = true;
            }
            catch (UnauthorizedAccessException)
            {
                return new HttpUnauthorizedResult();
            }
            catch (Exception)
            {
                isError = true;
            }

            string statusToReturn = "success";
            if (isError)
            {
                statusToReturn = "error";
            }

            return Json(new
            {
                status = statusToReturn

            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadPaymentNote(string appReqId)
        {
            Guid appReqIdGuid = new Guid(appReqId);
            ApplicationRequestEntity app = ApplicationRequestEntity.Get(appReqIdGuid);
            BillPaymentFormOSSPDFModel model = new BillPaymentFormOSSPDFModel
            {
                ApplicationNumber = app.ApplicationRequestNumber,
                IsJuristic = app.IdentityType == UserTypeEnum.Juristic,
                IdentityID = app.IdentityID,
                DueDate = app.DueDateForPayFee,
                IPAddress = app.SourceIPAddress,
                OrganizationName = !string.IsNullOrEmpty(app.PaymentMethodOrgDetail) ? app.PaymentMethodOrgDetail : app.OrgNameTH,
                RequestDateTime = app.CreatedDate,
                TaxID = app.IdentityID,
                PaymentChannel = app.GetPaymentChannel(),
                CompanyName = app.IdentityName,
                CustomerName = app.GetRequestorInfo().Name,
                DocumentTitle = app.PermitName,
                PaymentAddress = app.GetPaymentAddress(),
                OrgTel = !string.IsNullOrEmpty(app.PaymentMethodOrgTel) ? app.PaymentMethodOrgTel : System.Configuration.ConfigurationManager.AppSettings["OrgTel"],
                PaymentItems = new List<BillPaymentFormOSSPDFModel.PaymentItem>
                {
                    new BillPaymentFormOSSPDFModel.PaymentItem
                    {
                        Sequence = 1,
                        Title = app.PermitName,
                        Amount = app.Fee.Value,
                    }
                }
            };
            if (app.PermitDeliveryType == "BY_MAIL")
            {
                model.PaymentItems.Add(new BillPaymentFormOSSPDFModel.PaymentItem
                {
                    Sequence = 2,
                    Title = "EMS",
                    Amount = app.EMSFee.HasValue ? app.EMSFee.Value : 0,
                });
            }
            var pdfBytes = iTextReportHelper.GetBillPaymentOSSReport(model);
            return File(pdfBytes, "application/pdf");
        }

        [HttpGet]
        public ActionResult DownloadBillPayment(string appReqId, int ems = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(appReqId))
                {
                    return HttpNotFound();
                }
                Guid appReqIdGuid = new Guid(appReqId);
                ApplicationRequestEntity app = ApplicationRequestEntity.Get(appReqIdGuid);
                if (app != null
                    && app.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE)
                {
                    BillPaymentFormPDFModel model = new BillPaymentFormPDFModel
                    {
                        CustomerName = app.IdentityName,
                        DueDate = app.DueDateForPayFee,
                        Prefix = "1",
                        ProductCode = "55555",
                        //RefCode1 = "0123456789123",
                        RefCode1 = "8888888888888",
                        //RefCode2 = "3219876530862",
                        RefCode2 = "9999999999999",
                        ServiceCode = "22",
                        TaxID = app.IdentityID,
                        PaymentItems = new List<BillPaymentFormPDFModel.PaymentItem>(),
                    };
                    model.PaymentItems.Add(new BillPaymentFormPDFModel.PaymentItem
                    {
                        Sequence = 1,
                        Title = app.PermitName,
                        Amount = app.Fee.Value,
                    });
                    if (app.PermitDeliveryType == "BY_MAIL")
                    {
                        model.PaymentItems.Add(new BillPaymentFormPDFModel.PaymentItem
                        {
                            Sequence = 2,
                            Title = "EMS",
                            Amount = 50,
                        });
                    }
                    else if (ems > 0)
                    {
                        model.PaymentItems.Add(new BillPaymentFormPDFModel.PaymentItem
                        {
                            Sequence = 2,
                            Title = "EMS",
                            Amount = ems,
                        });
                    }
                    var pdfBytes = iTextReportHelper.GetBillPaymentReport(model);
                    return File(pdfBytes, "application/pdf");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        public ActionResult DownloadAllDoc(string appReqId)
        {
            if (string.IsNullOrEmpty(appReqId))
            {
                return HttpNotFound();
            }
            Guid appReqIdGuid = new Guid(appReqId);
            ApplicationRequestEntity app = ApplicationRequestEntity.Get(appReqIdGuid);
            string FileName = app.IdentityID + " - " + app.IdentityName;

            List<SourceFile> ListSourceFile = new List<SourceFile>();

            if (app.UploadedFiles != null || app.UploadedFiles.Count > 0)
            {
                foreach (var group in app.UploadedFiles)
                {
                    if (group.Files != null || group.Files.Count > 0)
                    {
                        foreach (var file in group.Files)
                        {
                            FileMetadataEntity Metadata = FileMetadataEntity.Get(file.FileID, appReqIdGuid);
                            var metadata = new BizPortal.ViewModels.V2.FileMetadata() { FileID = file.FileID, FileName = file.FileName, ContentType = file.ContentType, Extras = new Dictionary<string, string>() };
                            if (file.Extras != null)
                            {
                                foreach (var extra in file.Extras)
                                {
                                    metadata.Extras.Add(extra.Key, extra.Value != null ? extra.Value.ToString() : string.Empty);
                                }
                            }
                            SourceFile SourceFile = new SourceFile()
                            {
                                Name = file.FileName,
                                FileBytes = metadata.GetBytes(),
                            };
                            ListSourceFile.Add(SourceFile);
                        }
                    }
                }
            }

            // the output bytes of the zip
            byte[] fileBytes = null;

            // create a working memory stream
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                // create a zip
                using (System.IO.Compression.ZipArchive zip = new System.IO.Compression.ZipArchive(memoryStream, System.IO.Compression.ZipArchiveMode.Create, true))
                {
                    // interate through the source files
                    foreach (var SourceFile in ListSourceFile)
                    {
                        // add the item name to the zip
                        System.IO.Compression.ZipArchiveEntry zipItem = zip.CreateEntry(SourceFile.Name);
                        // add the item bytes to the zip entry by opening the original file and copying the bytes 
                        using (System.IO.MemoryStream originalFileMemoryStream = new System.IO.MemoryStream(SourceFile.FileBytes))
                        {
                            using (System.IO.Stream entryStream = zipItem.Open())
                            {
                                originalFileMemoryStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
                fileBytes = memoryStream.ToArray();
            }

            //Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".zip");
            return File(fileBytes, "application/zip", FileName + ".zip");
        }

        [HttpGet]
        public ActionResult DownloadEDocument(string applicationRequestId, string documentId)
        {
            try
            {
                var request = ApplicationRequestEntity.Get(Guid.Parse(applicationRequestId));

                if (request != null)
                {
                    var signing = EDocumentEntity.Get(documentId);

                    if (signing != null)
                    {
                        if (!string.IsNullOrEmpty(signing.DocumentUrl))
                        {
                            return Redirect(signing.DocumentUrl);
                        }
                        else
                        {
                            var edCtrl = new BizPortal.Utils.EDocument(request.ApplicationID);
                            var edocData = edCtrl.DownloadDocument(signing.DocumentID);

                            return File(edocData, "application/pdf");
                        }
                    }
                    else if (!string.IsNullOrEmpty(documentId)) 
                    {
                        var edCtrl = new BizPortal.Utils.EDocument(request.ApplicationID);
                        var edocData = edCtrl.DownloadDocument(documentId);

                        return File(edocData, "application/pdf");
                    }
                    else
                    {
                        throw new Exception("ไม่พบข้อมูลคำร้อง");
                    }
                }
                else 
                {
                    throw new Exception("ไม่พบเอกสาร");
                }
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public async Task<JObject> Upload(string fileInfo)
        {
            var fileServiceUrl = ConfigurationManager.AppSettings["FileServicePath"];
            var fileUploadAccessTokenUrl = ConfigurationManager.AppSettings["FileServiceUploadTokenPath"];
            var fileConsumerKey = ConfigurationManager.AppSettings["FileConsumerKey"];
            var fileSecret = ConfigurationManager.AppSettings["FileConsumerSecret"];
            var plu = new PLUpload(System.Web.HttpContext.Current);
            var result = await plu.ProcessUploadToFileService(fileServiceUrl, fileUploadAccessTokenUrl, fileConsumerKey, fileSecret, fileInfo);
            return result;
        }

        public class SourceFile
        {
            public string Name { get; set; }
            public Byte[] FileBytes { get; set; }
        }
    }
}