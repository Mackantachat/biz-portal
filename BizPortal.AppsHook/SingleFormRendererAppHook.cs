using System;
using System.Collections.Generic;
using BizPortal.ViewModels.V2;
using BizPortal.DAL.MongoDB;
using Newtonsoft.Json.Linq;
using EGA.WS;
using System.Configuration;
using BizPortal.DAL;
using BizPortal.Models;
using MongoDB.Driver;
using System.Linq;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels.SingleForm;
using System.Text.RegularExpressions;
using BizPortal.Integrated;
using BizPortal.ViewModels.ControlData;
using RestSharp;
using Newtonsoft.Json;
using Mapster;
using BizPortal.AppsHook.StandardAPI;
using EGA.EGA_Development.Util.MailV2.Data;
using EGA.EGA_FileService.Util;
using EGA.EGA_FileService.Util.Models;
using System.Threading.Tasks;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using System.Globalization;
using GreatFriends.ThaiBahtText;
using QRCoder;
using System.Drawing;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using FileItem = EGA.EGA_FileService.Util.Models.FileItem;

namespace BizPortal.AppsHook
{
    public abstract class SingleFormRendererAppHook : IAppsHook, IProgress<int>
    {
        private ApplicationDbContext _db;
        private EGAWSAPI _api;

        private string serviceTokenPath = ConfigurationManager.AppSettings["FileServiceUploadTokenPath"];
        private string serviceUploadPath = ConfigurationManager.AppSettings["FileServicePath"];
        private string consumerKey = ConfigurationManager.AppSettings["FileConsumerKey"];
        private string secret = ConfigurationManager.AppSettings["FileConsumerSecret"];

        public EGAWSAPI Api
        {
            get
            {
                if (_api == null)
                {
                    _api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
                }

                return _api;
            }
        }

        public ApplicationDbContext DB
        {
            get
            {
                if (_db == null)
                    _db = new ApplicationDbContext();
                return _db;// ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            }
            protected set
            {
                _db = value;
            }
        }

        public virtual string DetailViewName
        {
            get
            {
                return ViewNameForAgentConst.NEW_FLOW;
            }
        }

        public virtual string TrackDetailViewName
        {
            get
            {
                return ViewNameForUserConst.NEW_FLOW;
            }
        }

        public bool ShowPermitSummaryInSingleFormConfirmScreen
        {
            get
            {
                return true;
            }
        }

        public virtual bool PermitCanBeDeliveredOnPayment
        {
            get
            {
                return false;
            }
        }

        public virtual bool AllowFreeDocument { get; } = true;

        public virtual bool HasOrgPdfForm { get; } = false;

        public virtual string PrintFormTitle
        {
            get
            {
                return null;
            }
        }

        public virtual string PrintFormHeaderRight
        {
            get
            {
                return null;
            }
        }

        public abstract InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request);

        protected Dictionary<string, object> GenerateStandardFormDataCreate(ApplicationRequestViewModel model, ApplicationRequestEntity request)
        {
            ApplicationRequestStandardDataCreate standardFormData = new ApplicationRequestStandardDataCreate();
            TypeAdapter.Adapt(request, standardFormData);
            return new Dictionary<string, object>
            {
                { "formData", standardFormData }
            };
        }

        protected Dictionary<string, object> GenerateStandardFormData(ApplicationRequestViewModel model, ApplicationRequestEntity request)
        {
            ApplicationRequestStandardData standardFormData = new ApplicationRequestStandardData();
            TypeAdapter.Adapt(request, standardFormData);
            return new Dictionary<string, object>
            {
                { "formData", standardFormData }
            };
        }

        public class StandardApiFileInfo_Base64
        {
            public string Name;
            public string Content;
            public string FileName;
            public string ContentType;
            public string Description;
        }

        public class StandardApiFileInfo
        {
            public string Name;
            public byte[] Content;
            public string FileName;
            public string ContentType;
        }
        protected IRestResponse SendStandardAPIRequest(string endPointUrl, object data, List<StandardApiFileInfo> files)
        {
            var client = new RestClient(endPointUrl);
            var request = new RestRequest();
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.Method = Method.POST;
            request.AddParameter("data", JsonConvert.SerializeObject(data), "application/json", ParameterType.RequestBody);
            foreach (var file in files)
            {
                request.AddFile(file.Name, file.Content, file.FileName, file.ContentType);
            }
            return client.Execute(request);
        }

        public virtual Dictionary<string, string> TranslateKeyValue(ApplicationRequestViewModel model)
        {
            return new Dictionary<string, string>();
        }

        protected AppHookInfo GenerateAppsHookData(AppsHookResult result, AppsStage stage, string message,
            string data = null, string sentdata = null, bool schedule = false, DateTime? scheduleDate = null, int scueduleCount = 1)
        {
            AppHookInfo info = new AppHookInfo()
            {
                Result = result.ToString(),
                ExceuteDate = DateTime.Now,
                Schedule = schedule,
                ScheduleCount = schedule ? scueduleCount : 0,
                ScheduleDate = schedule ? scheduleDate != null ? scheduleDate : DateTime.Now.AddHours(scueduleCount) : null,
                Message = message,
                Data = data,
                SentData = sentdata,
                AppsStage = stage.ToString()
            };

            return info;
        }

        public RenderDataResult RenderData(RenderStage stage, ApplicationRequestViewModel model)
        {
            // Not used!
            return null;
        }

        public SingleFormAppsViewModel[] RenderDataSingleForm(ApplicationRequestEntity request, dynamic ViewBag)
        {
            string[] apps = DB.Applications
                .Where(x => x.ApplicationID == request.ApplicationID)
                .Select(x => x.ApplicationSysName)
                .ToArray();

            List<SingleFormAppsViewModel> supportApps = new List<SingleFormAppsViewModel>();

            List<FormSection> sections = new List<FormSection>();

            // Frontis: Get data based on form revision.
            FormSectionGroup[] sectionGroups = null;
            // เอา request.FormRevisionCode ไป query ใน FormSectionGroupRevision 
            if (FormSectionGroup.TryGetAllSectionGroupRevision(apps, request.FormRevisionCode, request.IdentityType, out sectionGroups))
            {
                //TODO: ถ้าเจอให้เอา config จาก FormSectionXXXRevision ทั้งหมด (ยังไม่ได้ทำ)
                var groups = sectionGroups.Where(o => !o.SearchPage).ToArray();
                foreach (var group in groups)
                {
                    sections.AddRange(FormSection.GetSectionsByRevision(group.SectionGroup, request.FormRevisionCode, apps, request.IdentityType).OrderBy(o => o.Ordering));
                }

                List<FormSectionRow> sectionRows = new List<FormSectionRow>();
                foreach (var section in sections)
                {
                    sectionRows.AddRange(FormSectionRow.GetSectionRowsByRevision(section.Section, request.FormRevisionCode, apps, request.IdentityType));
                }

                ViewBag.SectionGroups = groups.ToArray();
                ViewBag.Sections = sections.ToArray();
                ViewBag.SectionRows = sectionRows.ToArray();
            }
            else
            {
                // ไม่เจอให้เอา config จาก FormSectionXXX (คือใช้ code block นี้แหละ)
                var groups = FormSectionGroup.GetAllSectionGroup(apps);
                groups = groups.Where(o => !o.SearchPage).ToList();
                foreach (var group in groups)
                {
                    sections.AddRange(FormSection.GetSections(group.SectionGroup, apps, request.IdentityType).OrderBy(o => o.Ordering));
                }

                List<FormSectionRow> sectionRows = new List<FormSectionRow>();
                foreach (var section in sections)
                {
                    sectionRows.AddRange(FormSectionRow.GetSectionRows(section.Section, apps, request.IdentityType));
                }

                ViewBag.SectionGroups = groups.ToArray();
                ViewBag.Sections = sections.ToArray();
                ViewBag.SectionRows = sectionRows.ToArray();
            }


            Dictionary<string, object> defaults = ViewBag.Defaults;
            if (defaults == null)
            {
                defaults = new Dictionary<string, object>();
            }

            var appPrefillAll = MongoFactory.GetAppPrefillAnswerCollection()
                .AsQueryable()
                .Where(o => apps.Contains(o.AppSystemName))
                .ToArray();
            foreach (var appPrefill in appPrefillAll)
            {
                foreach (var prefill in appPrefill.Prefill)
                {
                    var key = prefill.SectionName != null ? (prefill.SectionName + "::" + prefill.ControlName) : prefill.ControlName;
                    if (defaults.ContainsKey(key))
                    {
                        defaults[key] = prefill.ControlPrefill;
                    }
                    else
                    {
                        defaults.Add(key, prefill.ControlPrefill);
                    }
                }
            }

            var singleFormData = GetData(request, sections);

            // Begin - เพิ่มข้อมูลทั่วไปของนิติบุคคล - สรอ.
            if (request.IdentityType == UserTypeEnum.Juristic)
            {
                var generalInfo = singleFormData.SectionData.Where(o => o.SectionName == "GENERAL_INFORMATION").SingleOrDefault();
                if (generalInfo != null && generalInfo.FormData != null && generalInfo.FormData.Count > 0)
                {
                    if (generalInfo.FormData.ContainsKey("COMPANY_NAME_TH"))
                    {
                        if (defaults.ContainsKey("COMPANY_NAME_TH"))
                        {
                            defaults.Remove("COMPANY_NAME_TH");
                        }
                        defaults.Add("COMPANY_NAME_TH", generalInfo.FormData.TryGetString("COMPANY_NAME_TH"));
                    }

                    if (generalInfo.FormData.ContainsKey("COMPANY_NAME_EN"))
                    {
                        if (defaults.ContainsKey("COMPANY_NAME_EN"))
                        {
                            defaults.Remove("COMPANY_NAME_EN");
                        }
                        defaults.Add("COMPANY_NAME_EN", generalInfo.FormData.TryGetString("COMPANY_NAME_EN"));
                    }

                    if (generalInfo.FormData.ContainsKey("IDENTITY_ID"))
                    {
                        if (defaults.ContainsKey("IDENTITY_ID"))
                        {
                            defaults.Remove("IDENTITY_ID");
                        }
                        defaults.Add("IDENTITY_ID", generalInfo.FormData.TryGetString("IDENTITY_ID"));
                    }

                    if (generalInfo.FormData.ContainsKey("REGISTER_DATE"))
                    {
                        if (defaults.ContainsKey("REGISTER_DATE"))
                        {
                            defaults.Remove("REGISTER_DATE");
                        }
                        defaults.Add("REGISTER_DATE", generalInfo.FormData.TryGetString("REGISTER_DATE"));
                    }

                    if (generalInfo.FormData.ContainsKey("REGISTER_CAPITAL"))
                    {
                        if (defaults.ContainsKey("REGISTER_CAPITAL"))
                        {
                            defaults.Remove("REGISTER_CAPITAL");
                        }
                        defaults.Add("REGISTER_CAPITAL", generalInfo.FormData.TryGetString("REGISTER_CAPITAL"));
                    }
                    if (generalInfo.FormData.ContainsKey("REGISTER_CAPITAL_PAID"))
                    {
                        if (defaults.ContainsKey("REGISTER_CAPITAL_PAID"))
                        {
                            defaults.Remove("REGISTER_CAPITAL_PAID");
                        }
                        defaults.Add("REGISTER_CAPITAL_PAID", generalInfo.FormData.TryGetString("REGISTER_CAPITAL_PAID"));
                    }
                }

                if (singleFormData != null && singleFormData.SectionData != null && singleFormData.SectionData.Count > 0)
                {
                    var provinces = GeoService.Provinces(string.Empty);
                    var provinceID = request.IdentityID.Substring(1, 2);
                    var regisProvince = provinces.Where(o => o.ID == provinceID).SingleOrDefault();
                    if (regisProvince != null)
                    {
                        if (defaults.ContainsKey("REGISTER_PROVINCE"))
                        {
                            defaults.Remove("REGISTER_PROVINCE");
                        }
                        defaults.Add("REGISTER_PROVINCE", new AddressControlData() { Province = regisProvince });
                    }


                }
            }
            // End - เพิ่มข้อมูลทั่วไปของนิติบุคคล - สรอ.

            ViewBag.Defaults = defaults;
            ViewBag.SingleFormData = singleFormData;

            AppFiles = MongoFactory.GetSingleFormAppFileCollection()
                .AsQueryable()
                .Where(o => apps[0] == o.AppSysName)
                .SelectMany(o => o.Files).Distinct().ToArray();
            return supportApps.ToArray();
        }

        public abstract decimal? CalculateFee(List<ISectionData> sectionData);

        protected string[] AppFiles = null;
        Regex multipleFileNamePattern = new Regex("^(.+)([\\-_]([0-9])+)$");
        Regex freeDocPattern = new Regex("^FREE_DOC\\-[0-9]+$");
        public bool IsRelatedFile(string fileTypeCode)
        {
            if (fileTypeCode == "" || fileTypeCode == "ADDITIONAL_DOC" || fileTypeCode == "OTHER_DOC"  || freeDocPattern.IsMatch(fileTypeCode))
            {
                if (!AllowFreeDocument)
                {
                    return false;
                }
                return true;
            }

            if (multipleFileNamePattern.IsMatch(fileTypeCode))
            {
                Match m = multipleFileNamePattern.Match(fileTypeCode);

                var filenameMultiple = m.Groups[1].Value;
                if (AppFiles.Contains(filenameMultiple))
                    return true;
            }
            return AppFiles.Contains(fileTypeCode);
        }

        public List<FileMetadataEntity> GetRelatedFiles(List<FileMetadataEntity> files)
        {
            List<FileMetadataEntity> relatedFiles = new List<FileMetadataEntity>();
            foreach (var file in files)
            {
                if (IsRelatedFile(file.FileTypeCode))
                {
                    relatedFiles.Add(file);
                }
            }
            return relatedFiles;
        }

        protected SingleFormRequestViewModel GetData(ApplicationRequestEntity request, List<FormSection> sections)
        {
            Regex arrOfFormPattern = new Regex("(.*)_([0-9]+)");
            SingleFormRequestViewModel data = new SingleFormRequestViewModel();
            data.IdentityID = request.IdentityID;
            data.IdentityType = request.IdentityType;
            data.SectionData = new List<SingleFormSectionDataViewModel>();
            foreach (var sec in sections)
            {
                SingleFormSectionDataViewModel m = new SingleFormSectionDataViewModel();
                m.SectionName = sec.Section;
                m.Type = sec.Type.ToString();
                m.FormData = new Dictionary<string, object>();
                m.ArrayData = new List<Dictionary<string, object>>();
                if (request.Data.ContainsKey(m.SectionName.ToUpper()))
                {
                    var reqSecData = request.Data[m.SectionName.ToUpper()];
                    if (sec.Type == SectionType.Form)
                    {
                        foreach (var kv in reqSecData.Data)
                        {
                            m.FormData.Add(kv.Key, kv.Value);
                            if (kv.Key == "INFORMATION__REQUEST_AS_OPTION")
                            {
                                switch (kv.Value)
                                {
                                    case "นิติบุคคล": m.FormData[kv.Key] = "INFORMATION__REQUEST_AS_JURISTIC"; break;
                                    case "บุคคลธรรมดา": m.FormData[kv.Key] = "INFORMATION__REQUEST_AS_CITIZEN"; break;
                                }
                            }
                        }
                    }
                    else
                    {

                        Dictionary<int, Dictionary<string, object>> tempData = new Dictionary<int, Dictionary<string, object>>();
                        int arrCnt = 0;
                        //if (reqSecData.Data.ContainsKey(m.SectionName + "_TOTAL") && int.TryParse(reqSecData.Data[m.SectionName + "_TOTAL"], out arrCnt))
                        {
                            foreach (var kv in reqSecData.Data)
                            {
                                if (arrOfFormPattern.IsMatch(kv.Key))
                                {
                                    var match = arrOfFormPattern.Match(kv.Key);
                                    var dataKey = match.Groups[1].Value;
                                    var index = int.Parse(match.Groups[2].Value);
                                    if (!tempData.ContainsKey(index))
                                    {
                                        tempData.Add(index, new Dictionary<string, object>());
                                    }
                                    tempData[index].Add(dataKey, kv.Value);
                                }
                            }
                            int i = 0;
                            while (tempData.ContainsKey(i))
                            {
                                m.ArrayData.Add(tempData[i]);
                                i++;
                            }
                        }
                    }
                }
                data.SectionData.Add(m);
            }
            return data;
        }

        public virtual decimal CalculateEMSFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public virtual byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc) => null;

        public virtual bool IsEnabledChat()
        {
            return false;
        }

        public virtual bool IsEnabledExportData(ApplicationStatusV2Enum status)
        {
            return false;
        }


        public virtual bool InvokeSingleForm(Guid trid, string currentSectionGroup, ref SingleFormRequestViewModel model)
        {
            return true;
        }

        public virtual FileMetadataEntity InvokeFilePreDoc(string IdentityID, string FileTypeCode)
        {
            return null;
        }

        public SingleFormSectionDataViewModel GetSectionData(SingleFormRequestViewModel model, string sectionName, SectionType sectionType)
        {
            var sectionData = model.SectionData.Where(e => e.SectionName == sectionName && e.Type == sectionType.ToString()).FirstOrDefault();

            if (sectionData == null)
            {
                sectionData = new SingleFormSectionDataViewModel
                {
                    SectionName = sectionName,
                    Type = sectionType.ToString()
                };
                model.SectionData.Add(sectionData);
            }

            return sectionData;
        }

        public virtual string GenerateRequestData(Guid ApplicationRequestID)
        {
            return string.Empty;
        }

        public virtual JObject GenerateELicenseData(Guid ApplicationRequestID)
        {
            return null;
        }

        public virtual JObject GenerateEReceiptData(Guid ApplicationRequestID)
        {
            return null;
        }

        public virtual ApplicationRequestTransactionEntity GenerateBizReceipt(ApplicationRequestEntity request, ApplicationRequestTransactionEntity trans, ApplicationUser user)
        {
            #region --------- Generate PDF Here ---------
            int running = 1;
            var lastOrgReceipt = ReceiptRunningTransaction.GetLastByOrgCode(request.OrgCode);
            if (lastOrgReceipt != null)
            {
                running = lastOrgReceipt.RunningNumber + 1;
            }
            byte[] pdfByteArray = FillPDFFieldValue(request, running, user);
            #endregion

            string fileArray = Convert.ToBase64String(pdfByteArray);
            using (var fileMS = new System.IO.MemoryStream(Convert.FromBase64String(fileArray)))
            {

                #region --------- Upload file to server and save file info to permit request ---------
                var fileInfo = new
                {
                    Name = request.ApplicationRequestNumber + ".pdf",
                    ContentType = "application/pdf",
                    Size = fileMS.Length,
                    IsPublic = false
                };

                string fileInfoJson = JsonConvert.SerializeObject(fileInfo);
                string token = ServiceHelper.RequestAccessToken(serviceTokenPath, consumerKey, secret, fileInfoJson);

                var uploaded = ServiceHelper.UploadFile(serviceUploadPath, token, consumerKey, secret, fileInfoJson, fileMS, this).Result;
                var fileItem = new FileMetadataEntity();
                TypeAdapter.Adapt<FileItem, FileMetadataEntity>(uploaded, fileItem);

                if (fileItem.Extras == null)
                {
                    fileItem.Extras = new Dictionary<string, object>();
                }
                fileItem.GovDocumentName = "ใบเสร็จรับเงิน";

                if (fileItem.Extras.ContainsKey("OWNER_IDENT_ID"))
                {
                    fileItem.Extras["OWNER_IDENT_ID"] = request.IdentityID;
                }
                else
                {
                    fileItem.Extras.Add("OWNER_IDENT_ID", request.IdentityID);
                }

                if (trans.GovFiles == null) { trans.GovFiles = new List<FileMetadataEntity>(); }

                trans.GovFiles.Add(fileItem);
                #endregion

                #region ---------  Insert receipt transaction to sql server --------- 
                ReceiptRunningTransaction receiptModel = ReceiptRunningTransaction.GetByApplicationRequestNumber(request.ApplicationRequestNumber);

                // create receipt transaction
                if (receiptModel == null)
                {
                    receiptModel = new ReceiptRunningTransaction();
                    receiptModel.FileId = fileItem.FileID;
                    receiptModel.Filename = fileInfo.Name;
                    receiptModel.OrgCode = request.OrgCode;
                    receiptModel.ApplicationRequestNumber = request.ApplicationRequestNumber;
                    receiptModel.RunningNumber = running;
                    receiptModel.ActiveFlag = true;
                    receiptModel.CreateDate = DateTime.Now;
                    MongoFactory.GetReceiptRunningTransactionCollection().InsertOne(receiptModel);
                }
                #endregion
            };

            //request = trans.Save();
            return trans;
        }

        private byte[] FillPDFFieldValue(ApplicationRequestEntity req, int runningNo, ApplicationUser user)
        {
            try
            {
                DateTime now = DateTime.Now;
                string runningCode = req.OrgCode + "-" + now.ToString("yy") + now.ToString("MM") + runningNo.ToString("D4");

                string src = System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/apps/receipt/receipt.pdf");
                PDFFieldValue field;

                List<PDFFieldValue> model = new List<PDFFieldValue>();
                UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                //string url = helper.Action("Receipt", "Track", new RouteValueDictionary(new { applicationRequestNumber = req.ApplicationRequestNumber }), HttpContext.Current.Request.Url.Scheme);
                string encodeUrl = helper.BizAction("Receipt", "Track", new { applicationRequestNumber = req.ApplicationRequestNumber }, true);

                // ------------Test QR on local--------------
                bool applyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);
                if (!applyDomainRoute)
                {
                    encodeUrl = helper.AbsolutePath(encodeUrl);
                }
                // ------------------------------------------

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(encodeUrl, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                string qr64str = Convert.ToBase64String(stream.ToArray());

                PDFFieldValue qrField = new PDFFieldValue()
                {
                    FieldName = "QR_CODE",
                    FieldType = PDFFieldValue.eFieldType.Image,
                    Value = qr64str
                };
                model.Add(qrField);

                field = new PDFFieldValue()
                {
                    FieldName = "RUNNING_NO",
                    Value = req.OrgCode + "-" + now.ToString("yy") + now.ToString("MM") + runningNo.ToString("D4")
                };
                model.Add(field);

                field = new PDFFieldValue()
                {
                    FieldName = "PUBLISH_DATE",
                    Value = now.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th-TH"))
                };
                model.Add(field);

                field = new PDFFieldValue()
                {
                    FieldName = "ORG_NAME",
                    Value = req.OrgNameTH + "\n" + req.OrgAddress
                };
                model.Add(field);


                field = new PDFFieldValue()
                {
                    FieldName = "IDENTITY_NAME",
                    Value = req.IdentityName
                };
                model.Add(field);

                field = new PDFFieldValue()
                {
                    FieldName = "IDENTITY_ID",
                    Value = req.IdentityType == UserTypeEnum.Citizen ? Regex.Replace(req.IdentityID, @"(\d{1})(\d{4})(\d{5})(\d{2})(\d{1})", "$1-$2-$3-$4-$5")
                            : req.IdentityType == UserTypeEnum.Juristic ? Regex.Replace(req.IdentityID, @"(\d{1})(\d{2})(\d{1})(\d{3})(\d{5})(\d{1})", "$1-$2-$3-$4-$5-$6")
                            : req.IdentityID
                };
                model.Add(field);

                #region All Fee
                decimal total = 0;
                if (req.Fee.HasValue)
                {
                    field = new PDFFieldValue()
                    {
                        FieldName = "NO_1",
                        Value = "1"
                    };
                    model.Add(field);
                    field = new PDFFieldValue()
                    {
                        FieldName = "DESC_1",
                        Value = req.PermitName
                    };
                    model.Add(field);
                    field = new PDFFieldValue()
                    {
                        FieldName = "AMOUNT_1",
                        Value = string.Format("{0:n}", req.Fee.Value)
                    };
                    model.Add(field);
                    total += req.Fee.Value;
                }
                if (req.PermitDeliveryType == "BY_MAIL" && req.EMSFeePaymentType == "USER" && req.EMSFee.HasValue)
                {
                    field = new PDFFieldValue()
                    {
                        FieldName = "NO_2",
                        Value = "2"
                    };
                    model.Add(field);
                    field = new PDFFieldValue()
                    {
                        FieldName = "DESC_2",
                        Value = "ค่าบริการส่ง EMS"
                    };
                    model.Add(field);
                    field = new PDFFieldValue()
                    {
                        FieldName = "AMOUNT_2",
                        Value = string.Format("{0:n}", req.EMSFee.Value)
                    };
                    model.Add(field);
                    total += req.EMSFee.Value;
                }

                field = new PDFFieldValue()
                {
                    FieldName = "TOTAL",
                    Value = string.Format("{0:n}", total)
                };
                model.Add(field);

                field = new PDFFieldValue()
                {
                    FieldName = "TOTAL_LETTER",
                    Value = total.ThaiBahtText(usesEt: UsesEt.Always)
                };
                model.Add(field);
                #endregion

                field = new PDFFieldValue()
                {
                    FieldName = "PUBLISH_BY",
                    Value = user.Firstname + "  " + user.Lastname
                };
                model.Add(field);

                field = new PDFFieldValue()
                {
                    FieldName = "CREATE_BY",
                    Value = string.Format("Biz Portal {0}", now.ToString("dd/MM/yy hh:mm:ss"))
                };
                model.Add(field);

                var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model, new PDFConfig() { FontName = "THSarabunNew.ttf", FontSize = 12 });

                return bytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Report(int value)
        {

        }
    }
}