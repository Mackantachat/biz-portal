using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels.StandardApi;
using System.Configuration;
using Newtonsoft.Json;
using static BizPortal.ViewModels.Apps.EXCISEStandard.ExciseRequest;
using EGA.WS;
using System.Net;
using System.IO;

namespace BizPortal.AppsHook
{
    class ExciseSellAlcoholAppHook : StoreBaseAppHook
    {
        public override bool PermitCanBeDeliveredOnPayment
        {
            get
            {
                return true;
            }
        }

        private Dictionary<string, decimal> feeMap1 = new Dictionary<string, decimal>
        {
            { "SELL_ALCOHOL_PRODUCTION_TYPE__LOCAL1", 5500 },
            { "SELL_ALCOHOL_PRODUCTION_TYPE__LOCAL2", 5500 },
            { "SELL_ALCOHOL_PRODUCTION_TYPE__FACTORY", 5500 },
            { "SELL_ALCOHOL_PRODUCTION_TYPE__OTHER", 5500 },
        };

        private Dictionary<string, decimal> feeMap2 = new Dictionary<string, decimal>
        {
            { "SELL_ALCOHOL_SELL_TYPE__VAT_FREE", 2200 },
            { "SELL_ALCOHOL_SELL_TYPE__VAT_REGISTERED", 2200 },
            { "SELL_ALCOHOL_SELL_TYPE__VAT_NOT_REGISTERED", 330 },
        };

        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            var sec = sectionData.Where(x => x.SectionName == "SELL_ALCOHOL_INFO").FirstOrDefault();
            if (sec != null)
            {
                decimal fee = 0;
                if (sec.FormData.TryGetString("SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_GE10L", "") == "true")
                {
                    fee += feeMap1[sec.FormData["SELL_ALCOHOL_PRODUCTION_TYPE_OPTION"].ToString()];
                }
                if (sec.FormData.TryGetString("SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L", "") == "true")
                {
                    fee += feeMap2[sec.FormData["SELL_ALCOHOL_SELL_TYPE_OPTION"].ToString()];
                }
                //fee = fee * (decimal)1.10;
                return fee;
            }
            throw new Exception("ไม่พบข้อมูลการจำหน่ายสุราใน SingleFormRequest");
        }


        public override bool HasOrgPdfForm
        {
            get
            {
                return true;
            }
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {

            //TODO: Generate pdf here
            string src = serverMapPathFunc("~/Uploads/apps/exise/alcohol/alcohol_permit_template.pdf");
            PDFFieldValue field;
            List<PDFFieldValue> model = new List<PDFFieldValue>();

            // พื้นที่สาขา
            //model.Add(new PDFFieldValue() { FieldName = "Branch", Value = "???" });

            #region 1. ข้อมูลผู้ประสงค์จะขายสุรา

            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = string.Format("{0}{1} {2}", req.Data["GENERAL_INFORMATION"].Data["DROPDOWN_CITIZEN_TITLE_TEXT"], req.Data["GENERAL_INFORMATION"].Data["CITIZEN_NAME"], req.Data["GENERAL_INFORMATION"].Data["CITIZEN_LASTNAME"]);
                model.Add(field);
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = req.Data["GENERAL_INFORMATION"].Data["COMPANY_NAME_TH"];
                model.Add(field);
            }


            string identity = req.Data["GENERAL_INFORMATION"].Data["IDENTITY_ID"];
            int digit = 0;
            for (int i = 0; i < identity.Length && digit <= 13; i++)
            {
                if (!IsNumeric(identity.Substring(i, 1))) continue;

                digit++;
                model.Add(new PDFFieldValue() { FieldName = "Identity" + digit, Value = identity.Substring(i, 1), FontSize = 13 });

            }

            string excise = req.Data["SELL_ALCOHOL_INFO"].Data["SELL_ALCOHOL_EXCISE_ID"];
            digit = 0;
            for (int i = 0; i < excise.Length && digit <= 17; i++)
            {
                if (!IsNumeric(excise.Substring(i, 1))) continue;

                digit++;
                model.Add(new PDFFieldValue() { FieldName = "Excise" + digit, Value = excise.Substring(i, 1), FontSize = 13 });

            }

            var addrInfo = req.Data["INFORMATION_STORE"].Data;
            model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo["ADDRESS_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo["ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo["ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo["ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo["ADDRESS_MOO_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo["ADDRESS_SOI_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo["ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo["ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"] });

            model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo["ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"] });
            if (!string.IsNullOrEmpty(addrInfo["ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"]))
                model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo["ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"];

            model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo["INFORMATION_STORE_EMAIL"] });
            model.Add(new PDFFieldValue() { FieldName = "Fax", Value = addrInfo["ADDRESS_FAX_INFORMATION_STORE__ADDRESS"] });

            /*
            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                var addrInfo = req.Data["CITIZEN_ADDRESS_INFORMATION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo["ADDRESS_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo["ADDRESS_BUILDING_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo["ADDRESS_ROOMNO_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo["ADDRESS_FLOOR_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo["ADDRESS_MOO_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo["ADDRESS_SOI_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo["ADDRESS_ROAD_CITIZEN_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo["ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo["ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo["ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo["ADDRESS_POSTCODE_CITIZEN_ADDRESS"] });

                model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo["ADDRESS_TELEPHONE_CITIZEN_ADDRESS"] });
                if (!string.IsNullOrEmpty(addrInfo["ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS"]))
                    model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo["ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS"];

                model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo["CITIZEN_EMAIL"] });
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                var addrInfo = req.Data["JURISTIC_ADDRESS_INFORMATION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo["ADDRESS_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo["ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo["ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo["ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo["ADDRESS_MOO_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo["ADDRESS_SOI_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo["ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo["ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo["ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo["ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo["ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"] });

                model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo["ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS"] });
                if (!string.IsNullOrEmpty(addrInfo["ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS"]))
                    model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo["ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS"];

                model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo["JURISTIC_EMAIL"] });
            }
            */

            #endregion

            #region 2. วัตถุประสงค์ของการยื่นคำขอรับใบอนุญาต

            var alcoholInfo = req.Data["SELL_ALCOHOL_INFO"].Data;

            // ขายสุราประเภท 1
            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_GE10L"]))
                model.Add(new PDFFieldValue() { FieldName = "Option21", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_GE10L"])
                    && alcoholInfo["SELL_ALCOHOL_PRODUCTION_TYPE_OPTION"] == "SELL_ALCOHOL_PRODUCTION_TYPE__LOCAL1"))
                model.Add(new PDFFieldValue() { FieldName = "Option211", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_GE10L"])
                    && alcoholInfo["SELL_ALCOHOL_PRODUCTION_TYPE_OPTION"] == "SELL_ALCOHOL_PRODUCTION_TYPE__LOCAL2"))
                model.Add(new PDFFieldValue() { FieldName = "Option212", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_GE10L"])
                    && alcoholInfo["SELL_ALCOHOL_PRODUCTION_TYPE_OPTION"] == "SELL_ALCOHOL_PRODUCTION_TYPE__FACTORY"))
                model.Add(new PDFFieldValue() { FieldName = "Option213", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_GE10L"])
                    && alcoholInfo["SELL_ALCOHOL_PRODUCTION_TYPE_OPTION"] == "SELL_ALCOHOL_PRODUCTION_TYPE__OTHER"))
                model.Add(new PDFFieldValue() { FieldName = "Option214", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });


            // ขายสุราประเภท 2
            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L"]))
                model.Add(new PDFFieldValue() { FieldName = "Option22", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L"])
                    && alcoholInfo["SELL_ALCOHOL_SELL_TYPE_OPTION"] == "SELL_ALCOHOL_SELL_TYPE__VAT_FREE"))
                model.Add(new PDFFieldValue() { FieldName = "Option221", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L"])
                    && alcoholInfo["SELL_ALCOHOL_SELL_TYPE_OPTION"] == "SELL_ALCOHOL_SELL_TYPE__VAT_REGISTERED"))
                model.Add(new PDFFieldValue() { FieldName = "Option222", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L"])
                    && alcoholInfo["SELL_ALCOHOL_SELL_TYPE_OPTION"] == "SELL_ALCOHOL_SELL_TYPE__VAT_REGISTERED")
            {
                string vat = alcoholInfo["SELL_ALCOHOL_VAT_ID"];
                digit = 0;
                for (int i = 0; i < vat.Length && digit <= 17; i++)
                {
                    if (!IsNumeric(vat.Substring(i, 1))) continue;

                    digit++;
                    model.Add(new PDFFieldValue() { FieldName = "Vat" + digit, Value = vat.Substring(i, 1), FontSize = 13 });
                }
            }

            if (!(IsTrueValue(alcoholInfo["SELL_ALCOHOL_OBJECTIVE_SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L"])
                    && alcoholInfo["SELL_ALCOHOL_SELL_TYPE_OPTION"] == "SELL_ALCOHOL_SELL_TYPE__VAT_NOT_REGISTERED"))
                model.Add(new PDFFieldValue() { FieldName = "Option223", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });


            // ลักษณะของสถานประกอบการขายสุรา
            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_PRODUCER"]))
                model.Add(new PDFFieldValue() { FieldName = "Option31", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_DUTY_FREE"]))
                model.Add(new PDFFieldValue() { FieldName = "Option32", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_RESTAURANT"]))
                model.Add(new PDFFieldValue() { FieldName = "Option33", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_DEPARTMENT_STORE"]))
                model.Add(new PDFFieldValue() { FieldName = "Option34", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_ASSOCIATION"]))
                model.Add(new PDFFieldValue() { FieldName = "Option35", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_PUB"]))
                model.Add(new PDFFieldValue() { FieldName = "Option36", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_CONVENIENT_STORE"]))
                model.Add(new PDFFieldValue() { FieldName = "Option37", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_OTHER"]))
                model.Add(new PDFFieldValue() { FieldName = "Option38", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            model.Add(new PDFFieldValue() { FieldName = "Option38Other", Value = alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_OTHER_INFO"] });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_BROTHEL"]))
                model.Add(new PDFFieldValue() { FieldName = "Option39", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(alcoholInfo["SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_GROCERY"]))
                model.Add(new PDFFieldValue() { FieldName = "Option310", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            #endregion



            var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model);
            return bytes;
        }

        private static string CountDocumentText(ApplicationRequestEntity req, string fileTypeCode)
        {
            int count = 0;
            if (req.UploadedFiles != null)
            {
                foreach (var fg in req.UploadedFiles)
                {
                    if (fg.Files == null) continue;

                    count += fg.Files.Count(o => o.FileTypeCode == fileTypeCode);
                }
            }

            return count.ToString();
        }

        private static bool IsTrueValue(string data)
        {
            return ("" + data).ToLower() == "true";
        }

        private static bool IsNumeric(string str)
        {
            int i = 0;
            return int.TryParse(str, out i);
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            // หลังจากทำ Biz-api เสร็จแล้วจะต้องนำโค้ดชุดนี้ออก
            if (request.Data.ContainsKey("INFORMATION_STORE"))
            {
                var storeInfo = request.Data["INFORMATION_STORE"].Data;
                if (storeInfo.ContainsKey("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"))
                {
                    request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"]);
                    request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS"]);
                    request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS"]);

                    request.Province = (storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"]);
                    request.Amphur = (storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"]);
                    request.Tumbol = (storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"]);
                }
            }

            // TODO: Updated on 2020-05-08, do not submit data to third-party.
            return new InvokeResult() { Success = true };

            #region biz-api code
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;
            string nid = string.Empty;

            string regId = string.Empty;
            string cusType = string.Empty;
            string cusTitleCode = string.Empty;
            string cusName = string.Empty;
            string cusSurname = string.Empty;
            string cusHouseno = string.Empty;
            string cusBuildname = string.Empty;
            string cusFloorno = string.Empty;
            string cusRoomno = string.Empty;
            string cusVillage = string.Empty;
            string cusAddrno = string.Empty;
            string cusMoono = string.Empty;
            string cusSoiname = string.Empty;
            string cusThnname = string.Empty;
            string cusTambolCode = string.Empty;
            string cusPoscode = string.Empty;
            string cusTelno = string.Empty;
            string cusEmail = string.Empty;
            string facTitleCode = string.Empty;
            string facName = string.Empty;
            string facSurname = string.Empty;
            string facHouseno = string.Empty;
            string facBuildname = string.Empty;
            string facFloorno = string.Empty;
            string facRoomno = string.Empty;
            string facVillage = string.Empty;
            string facAddrno = string.Empty;
            string facMoono = string.Empty;
            string facSoiname = string.Empty;
            string facThnname = string.Empty;
            string facTambolCode = string.Empty;
            string facPoscode = string.Empty;
            string facTelno = string.Empty;
            string facEmail = string.Empty;
            string latitude = string.Empty;
            string longitude = string.Empty;
            string authType = string.Empty;
            string addrType = string.Empty;
            string ownerType = string.Empty;
            string cusIsic = string.Empty;
            string isicOther = string.Empty;
            string liqType1 = string.Empty;
            string liqType1Id = string.Empty;
            string liqType2 = string.Empty;
            string liqType2Id = string.Empty;
            string tobType1Cig = string.Empty;
            string tobType1CigId = string.Empty;
            string tobType1Oth = string.Empty;
            string tobType1OthId = string.Empty;
            string tobType2Cig = string.Empty;
            string tobType2CigId = string.Empty;
            string tobType2Oth = string.Empty;
            string tobType2OthId = string.Empty;
            string cardType1 = string.Empty;
            string cardType1Id = string.Empty;
            string cardType2 = string.Empty;
            string cardType2Id = string.Empty;
            string liqAppReqId = string.Empty;


            try
            {
                switch (stage)
                {
                    case AppsStage.UserUpdate:


                        List<ExciseMetaDataREquest> listReqdocuments = new List<ExciseMetaDataREquest>();
                        var up = model.UploadedFiles.ToArray();

                        if (stage == AppsStage.UserUpdate)
                        {
                            up = model.UploadedFiles.Where(x => x.Description == "REQUESTED_FILE").ToArray().OrderByDescending(x => x.CreatedDate).Take(1).ToArray();
                        }

                        foreach (var upload in up)
                        {
                            foreach (var f in upload.Files)
                            {
                                var fileMetaModel = new FileMetadata
                                {
                                    FileID = f.FileID,
                                    FileName = f.FileName,
                                    ContentType = f.ContentType,
                                    Extras = new Dictionary<string, string>()
                                };

                                if (f.Extras != null)
                                {
                                    foreach (var extra in f.Extras)
                                    {
                                        fileMetaModel.Extras.Add(extra.Key, extra.Value != null ? extra.Value.ToString() : string.Empty);
                                    }
                                }

                                string Description = "";

                                if (f.Extras.ContainsKey("DISPLAYNAME"))
                                {
                                    Description = f.Extras["DISPLAYNAME"].ToString();
                                }

                                var fileTypeCode = (upload.Description == "REQUESTED_FILE" && f.Extras.ContainsKey("FILETYPENAME")) ? f.Extras["FILETYPENAME"].DefaultString() : f.FileTypeCode;
                                string fileType = Utils.EXCIRSEUtility.GetFileTypeRef().FirstOrDefault(x => f.FileTypeCode.Contains(x.Value)).Key.ToString();

                                var file = new ExciseMetaDataREquest
                                {
                                    appReqId = model.ApplicationRequestID.ToString(),
                                    docCode = "501",// fileTypeCode,
                                    attachBase64 = Convert.ToBase64String(fileMetaModel.GetBytes()),
                                    attachFileName = f.FileName,

                                    //ContentType = f.ContentType,
                                    //Description = Description == "" ? fileTypeCode : Description,
                                    //Name = fileTypeCode,
                                    //Content = Convert.ToBase64String(fileMetaModel.GetBytes()),
                                    //FileName = f.FileName,
                                    //ContentType = f.ContentType,
                                    //Description = Description == "" ? fileTypeCode : Description,
                                };

                                listReqdocuments.Add(file);

                            }
                        }
                        var post = new ExciseRequestFile()
                        {

                          documentList= listReqdocuments

                        };
                        var Req = new ExciseFile()
                        {
                            RequestData= post
                        };


                        string regisUrl_ = ConfigurationManager.AppSettings["EXCISE_WS_URL_POSTREQUESTFILE"];
                        var jsonPost_ = JsonConvert.SerializeObject(Req,
                        Newtonsoft.Json.Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });

                                           
                        Api.OnCheckingApplicationError += (ex) =>
                        {
                            result.Exception = ex;
                            var egaEx = ex as EGAWSAPIException;
                            if (egaEx != null)
                            {
                                var message = string.Format("{0}: {1}", (int)egaEx.HttpStatusCode, egaEx.ResponseData["Message"].ToString());
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, message, egaEx.ResponseData.ToString(), jsonPost_);
                                result.Message = egaEx.ResponseData["Message"].ToString();
                            }
                            else
                            {
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, ex.Message, ex.StackTrace, jsonPost_);
                                result.Message = ex.Message;
                            }
                        };

                        var response_ = Api.Call(regisUrl_, HttpVerb.POST, null, jsonPost_, ContentType.ApplicationJson);



                        if (response_.HasValues && response_["ResponseCode"].ToString() == "OK")
                        {
                            result.Success = true;
                        }
                        else
                        {
                            result.Success = false;
                        }


                        break;
                    case AppsStage.UserCreate:
                        switch (model.IdentityType)
                        {
                            case UserTypeEnum.Citizen:
                                nid = model.IdentityID.ToString();
                                cusType = "1";
                                cusHouseno = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_HOUSEHOLD_REGISTRATION_ID").Replace("-", "");
                                cusTitleCode = "00" + model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_TITLE");
                                cusName = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_NAME");
                                cusSurname = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME");

                                cusAddrno = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_CITIZEN_ADDRESS");
                                cusSoiname = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS");
                                cusMoono = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS");
                                //road = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS");
                                cusThnname = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT");
                                cusTambolCode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS");
                                cusTambolCode += model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS");
                                cusTambolCode += model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS");

                                cusPoscode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS");
                                cusTelno = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS");
                                if (!string.IsNullOrEmpty(model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS")))
                                    cusTelno = cusTelno + " ต่อ " + model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS");
                                cusEmail = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("CITIZEN_EMAIL");
                                addrType = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION");
                                authType = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                                break;
                            case UserTypeEnum.Juristic:
                                nid = model.IdentityID.ToString();
                                cusType = "2";




                                cusTitleCode = "9999";
                                cusName = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_TH");
                                cusSurname = "";
                                cusHouseno = "";
                                cusBuildname = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS");
                                cusFloorno = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS");
                                cusRoomno = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS");
                                cusVillage = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS");
                                cusAddrno = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS");
                                cusMoono = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS");
                                cusSoiname = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS");
                                cusThnname = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT");
                                cusTambolCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS") + model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS") + model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS");
                                cusPoscode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS");
                                cusTelno = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS");
                                if (!string.IsNullOrEmpty(model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS")))
                                    cusTelno = cusTelno + " ต่อ " + model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS");
                                cusEmail = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS");
                                addrType = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_BUILDING_TYPE_OPTION");
                                authType = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                                break;
                            default:
                                cusType = "3";
                                break;
                        }
                        regId = model.Data.TryGetData("SELL_ALCOHOL_INFO").ThenGetStringData("SELL_ALCOHOL_EXCISE_ID").Replace("-", "");
                        liqAppReqId = model.ApplicationRequestID.ToString();
                        facTitleCode = "";
                        facName = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH");
                        facSurname = "";
                        facHouseno = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_HOUSEHOLD_REGISTRATION_ID").Replace("-", "");
                        cusIsic = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("AJAX_DROPDOWN_EXCISE_BUSINESS_TYPE");

                        facBuildname = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS");
                        facFloorno = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS");
                        facRoomno = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS");
                        facVillage = string.Empty;
                        facAddrno = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS");
                        facMoono = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS");
                        facSoiname = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS");
                        facThnname = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT");
                        facTambolCode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS");
                        facTambolCode += model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS");
                        facTambolCode += model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS");
                        facPoscode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS");
                        facTelno = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS");
                        if (facTelno.Equals("-")) facTelno = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOBILE_INFORMATION_STORE__ADDRESS");
                        if (string.IsNullOrEmpty(model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS")))
                            facTelno = facTelno + " ต่อ " + model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS");
                        facEmail = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS");
                        latitude = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_LAT_INFORMATION_STORE__ADDRESS"); ;
                        longitude = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_LNG_INFORMATION_STORE__ADDRESS"); ;
                        // authType = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                        if (authType == "REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER" || authType == "REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD") authType = "1";
                        else authType = "2";
                        // REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER
                        //addrType = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION");
                        if (addrType == "INFORMATION_STORE_BUILDING_TYPE_OPTION__OWNED")
                        {
                            addrType = "1";
                            ownerType = "1";
                        }
                        else if (addrType == "INFORMATION_STORE_BUILDING_TYPE_OPTION__RENT")
                        {
                            addrType = "2";
                            ownerType = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION");
                            if (ownerType == "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION__JURISTIC") ownerType = "1";
                            else if (ownerType == "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION__CITIZEN") ownerType = "2";
                            else if (ownerType == "INFORMATION_STORE_BUILDING_TYPE_OPTION__GOVERNMENT") ownerType = "3";
                            else if (ownerType == "INFORMATION_STORE_BUILDING_TYPE_OPTION__ROYAL") ownerType = "4";
                        }
                        else if (addrType == "INFORMATION_STORE_BUILDING_TYPE_OPTION__RENT_FREE") addrType = "3";

                        //น่าจะต้องเป็น radio และดึงข้อมูลจาก service
                        //cusIsic = "1"; //model.Data.TryGetData("SELL_ALCOHOL_INFO").ThenGetStringData("SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_RESTAURANT");
                        //cusIsic = model.Data.TryGetData("SELL_ALCOHOL_INFO").ThenGetStringData("AJAX_DROPDOWN_SELL_ALCOHOL_BUSINESS_TYPE");
                        isicOther = "";
                        liqType1 = model.Data.TryGetData("SELL_ALCOHOL_INFO").ThenGetStringData("SELL_ALCOHOL_PRODUCTION_TYPE_OPTION");
                        if (liqType1 == "SELL_ALCOHOL_PRODUCTION_TYPE__LOCAL1") liqType1 = "1";
                        else if (liqType1 == "SELL_ALCOHOL_PRODUCTION_TYPE__LOCAL2") liqType1 = "2";
                        else if (liqType1 == "SELL_ALCOHOL_PRODUCTION_TYPE__FACTORY") liqType1 = "3";
                        else if (liqType1 == "SELL_ALCOHOL_PRODUCTION_TYPE__OTHER") liqType1 = "4";
                        liqType1Id = model.ApplicationRequestID.ToString();


                        liqType2 = model.Data.TryGetData("SELL_ALCOHOL_INFO").ThenGetStringData("SELL_ALCOHOL_SELL_TYPE_OPTION");
                        if (liqType2 == "SELL_ALCOHOL_SELL_TYPE__VAT_FREE") liqType2 = "1";
                        else if (liqType2 == "SELL_ALCOHOL_SELL_TYPE__VAT_REGISTERED") liqType2 = "2";
                        else if (liqType2 == "SELL_ALCOHOL_SELL_TYPE__VAT_NOT_REGISTERED") liqType2 = "3";
                        liqType2Id = model.ApplicationRequestID.ToString();





                        //REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD  ขออนุญาตเองโดยเจ้าของกิจการ
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE มอบอำนาจให้ผู้อื่นดำเนินการแทน

                        //CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE


                        List<ExciseMetaData> listdocuments = new List<ExciseMetaData>();
                        var up_ = model.UploadedFiles.ToArray();

                        if (stage == AppsStage.UserUpdate)
                        {
                            up = model.UploadedFiles.Where(x => x.Description == "REQUESTED_FILE").ToArray().OrderByDescending(x => x.CreatedDate).Take(1).ToArray();
                        }

                        foreach (var upload in up_)
                        {
                            foreach (var f_ in upload.Files)
                            {
                                var fileMetaModel = new FileMetadata
                                {
                                    FileID = f_.FileID,
                                    FileName = f_.FileName,
                                    ContentType = f_.ContentType,
                                    Extras = new Dictionary<string, string>()
                                };

                                if (f_.Extras != null)
                                {
                                    foreach (var extra in f_.Extras)
                                    {
                                        fileMetaModel.Extras.Add(extra.Key, extra.Value != null ? extra.Value.ToString() : string.Empty);
                                    }
                                }

                                string Description = "";

                                if (f_.Extras.ContainsKey("DISPLAYNAME"))
                                {
                                    Description = f_.Extras["DISPLAYNAME"].ToString();
                                }

                                var fileTypeCode = (upload.Description == "REQUESTED_FILE" && f_.Extras.ContainsKey("FILETYPENAME")) ? f_.Extras["FILETYPENAME"].DefaultString() : f_.FileTypeCode;
                                string fileType = Utils.EXCIRSEUtility.GetFileTypeRef().FirstOrDefault(x => f_.FileTypeCode.Contains(x.Value)).Key.ToString();
                                fileType = fileType == "0" ? "501" : fileType;
                                var file = new ExciseMetaData
                                {
                                    docCode = fileType,//"501",// fileTypeCode,
                                    attachBase64 = Convert.ToBase64String(fileMetaModel.GetBytes()),
                                    attachFileName = f_.FileName,

                                    //ContentType = f.ContentType,
                                    //Description = Description == "" ? fileTypeCode : Description,
                                    //Name = fileTypeCode,
                                    //Content = Convert.ToBase64String(fileMetaModel.GetBytes()),
                                    //FileName = f.FileName,
                                    //ContentType = f.ContentType,
                                    //Description = Description == "" ? fileTypeCode : Description,
                                };

                                listdocuments.Add(file);

                            }
                        }



                        var postinformations = new ExciseDetail()
                        {

                            nid = nid,
                            regId = regId,
                            cusType = cusType,
                            cusTitleCode = cusTitleCode,
                            cusName = cusName,
                            cusSurname = cusSurname,
                            cusHouseno = cusHouseno,
                            cusBuildname = cusBuildname,
                            cusFloorno = cusFloorno,
                            cusRoomno = cusRoomno,
                            cusVillage = cusVillage,
                            cusAddrno = cusAddrno,
                            cusMoono = cusMoono,
                            cusSoiname = cusSoiname,
                            cusThnname = cusThnname,
                            cusTambolCode = cusTambolCode,
                            cusPoscode = cusPoscode,
                            cusTelno = cusTelno,
                            cusEmail = cusEmail,
                            facTitleCode = facTitleCode,
                            facName = facName,
                            facSurname = facSurname,
                            facHouseno = facHouseno,
                            facBuildname = facBuildname,
                            facFloorno = facFloorno,
                            facRoomno = facRoomno,
                            facVillage = facVillage,
                            facAddrno = facAddrno,
                            facMoono = facMoono,
                            facSoiname = facSoiname,
                            facThnname = facThnname,
                            facTambolCode = facTambolCode,
                            facPoscode = facPoscode,
                            facTelno = facTelno,
                            facEmail = facEmail,
                            latitude = latitude,
                            longitude = longitude,
                            authType = authType,
                            addrType = addrType,
                            ownerType = ownerType,
                            cusIsic = cusIsic,
                            isicOther = isicOther,
                            liqType1 = liqType1,
                            liqAppReqId = liqAppReqId,
                            liqType2 = liqType2,
                            tobType1Cig = tobType1Cig,
                            tobAppReqId = "",
                            tobType1Oth = tobType1Oth,
                            //tobType1OthId = tobType1OthId,
                            tobType2Cig = tobType2Cig,
                            tobType2Oth = tobType2Oth,
                            //tobType2OthId = tobType2OthId,
                            cardAppReqId = "",
                            cardType1 = cardType1,
                            cardType2 = cardType2,
                            documentList = listdocuments

                        };
                        var Request = new Excise()
                        {
                            RequestData = postinformations
                        };


                        string regisUrl = ConfigurationManager.AppSettings["EXCISE_WS_URL_REQUEST"];
                        var jsonPost = JsonConvert.SerializeObject(Request,
                        Newtonsoft.Json.Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });

                        string DGA_WS_URL = ConfigurationManager.AppSettings["DGA_WS_URL"];
                        /*string xx = DGA_WS_URL + "/ws" + regisUrl;
                        //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://164.115.9.184:90/BizPortalServicesUAT/lic/LicFrm0120");
                        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(DGA_WS_URL + "/ws" + regisUrl);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";
                        httpWebRequest.Headers.Add("Consumer-Key", Api.ConsumerKey);
                        httpWebRequest.Headers.Add("Token", Api.AccessToken);
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            streamWriter.Write(jsonPost);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }

                        using (var response_ = httpWebRequest.GetResponse() as HttpWebResponse)
                        {
                            if (httpWebRequest.HaveResponse && response_ != null)
                            {
                                using (var reader = new StreamReader(response_.GetResponseStream()))
                                {
                                    var res = reader.ReadToEnd();
                                    if (response_.StatusCode == HttpStatusCode.OK)
                                    {
                                        result.Success = true;
                                    }
                                    else
                                    {
                                        result.Success = false;
                                    }
                                }
                            }
                        }
                        */




                        Api.OnCheckingApplicationError += (ex) =>
                        {
                            result.Exception = ex;
                            var egaEx = ex as EGAWSAPIException;
                            if (egaEx != null)
                            {
                                var message = string.Format("{0}: {1}", (int)egaEx.HttpStatusCode, egaEx.ResponseData["Message"].ToString());
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, message, egaEx.ResponseData.ToString(), jsonPost);
                                result.Message = egaEx.ResponseData["Message"].ToString();
                            }
                            else
                            {
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, ex.Message, ex.StackTrace, jsonPost);
                                result.Message = ex.Message;
                            }
                        };

                        var response = Api.Call(regisUrl, HttpVerb.POST, null, jsonPost, ContentType.ApplicationJson);



                        if (response.HasValues && response["ResponseCode"].ToString() == "OK")
                        {
                            result.Success = true;
                        }
                        else
                        {
                            result.Success = false;
                        }

                        break;
                    case AppsStage.None:
                    case AppsStage.AgentUpdate:
                    case AppsStage.ApiUpdate:
                    default:
                        result.Success = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Exception = ex;

                result.Success = false;
            }



            //result.Success = true;
            return result;

            #endregion
        }

        //public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        //{
        //    var tResult = base.Invoke(stage, model, appHookInfo, ref request);

        //    var standardData = GenerateStandardFormData(model, request);
        //    var files = new List<StandardApiFileInfo>();
        //    foreach (var upload in request.UploadedFiles)
        //    {
        //        foreach (var f in upload.Files)
        //        {
        //            if (f.Extras != null && f.Extras.ContainsKey("PREDOC") && f.Extras["PREDOC"].ToString() == "true")
        //            {
        //                //TODO: Get predoc file here
        //                //files.Add(file);
        //            }
        //            else
        //            {
        //                var fileMetaModel = new BizPortal.ViewModels.V2.FileMetadata
        //                {
        //                    FileID = f.FileID,
        //                    FileName = f.FileName,
        //                    ContentType = f.ContentType,
        //                    Extras = new Dictionary<string, string>()
        //                };
        //                if (f.Extras != null)
        //                {
        //                    foreach (var extra in f.Extras)
        //                    {
        //                        fileMetaModel.Extras.Add(extra.Key, extra.Value != null ? extra.Value.ToString() : string.Empty);
        //                    }
        //                }
        //                var file = new StandardApiFileInfo
        //                {
        //                    Name = f.FileTypeCode,
        //                    Content = fileMetaModel.GetBytes(),
        //                    FileName = f.FileName,
        //                    ContentType = f.ContentType
        //                };
        //                files.Add(file);
        //            }
        //        }

        //    }
        //    var result = SendStandardAPIRequest("http://localhost:45598/th/test/TestStandardApi", standardData, files);
        //    return tResult;
        //}

        public override bool IsEnabledChat()
        {
            return true;
        }




    }
}
