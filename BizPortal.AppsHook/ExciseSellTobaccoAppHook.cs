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
    class ExciseSellTobaccoAppHook : StoreBaseAppHook
    {
        public override bool PermitCanBeDeliveredOnPayment
        {
            get
            {
                return true;
            }
        }
        private Dictionary<string, decimal> feeWholeSale = new Dictionary<string, decimal>
        {
            { "SELL_ALCOHOL_PRODUCTION_TYPE_SELL_ALCOHOL_PRODUCTION_TYPE__LOCAL1", 1200 },
            { "SELL_ALCOHOL_PRODUCTION_TYPE_SELL_ALCOHOL_PRODUCTION_TYPE__LOCAL2", 600 },
            { "SELL_ALCOHOL_PRODUCTION_TYPE_SELL_ALCOHOL_PRODUCTION_TYPE__FACTORY", 5000 },
            { "SELL_ALCOHOL_PRODUCTION_TYPE_SELL_ALCOHOL_PRODUCTION_TYPE__OTHER", 5000 },
        };
        private Dictionary<string, decimal> feeMap2 = new Dictionary<string, decimal>
        {
            { "SELL_ALCOHOL_SELL_TYPE_SELL_ALCOHOL_SELL_TYPE__VAT_FREE", 2000 },
            { "SELL_ALCOHOL_SELL_TYPE_SELL_ALCOHOL_SELL_TYPE__VAT_REGISTERED", 2000 },
            { "SELL_ALCOHOL_SELL_TYPE_SELL_ALCOHOL_SELL_TYPE__VAT_NOT_REGISTERED", 300 },
        };
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            decimal fee = 0;
            var sec = sectionData.Where(x => x.SectionName == "SELL_TOBACCO_INFO").FirstOrDefault();
            if (sec != null)
            {
                if (sec.FormData.TryGetString("SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_WHOLE_SELL", "") == "true")
                {
                    if (sec.FormData.TryGetString("SELL_TOBACCO_TOBACCO_TYPE_SELL_TOBACCO_TOBACCO_TYPE_CIGARATE", "") == "true")
                    {
                        fee += 1200;
                    }
                    if (sec.FormData.TryGetString("SELL_TOBACCO_TOBACCO_TYPE_SELL_TOBACCO_TOBACCO_TYPE_TOBACCO", "") == "true")
                    {
                        if (sec.FormData.TryGetString("SELL_TOBACCO_SELL_TYPE_OPTION", "") == "SELL_TOBACCO_SELL_TYPE_TOBACCO")
                        {
                            fee += 100;
                        }
                        if (sec.FormData.TryGetString("SELL_TOBACCO_SELL_TYPE_OPTION", "") == "SELL_TOBACCO_SELL_TYPE_OTHER")
                        {
                            fee += 500;
                        }
                    }
                }
                if (sec.FormData.TryGetString("SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_RETAIL", "") == "true")
                {
                    if (sec.FormData.TryGetString("SELL_TOBACCO_TOBACCO_TYPE_SELL_TOBACCO_TOBACCO_TYPE_CIGARATE", "") == "true")
                    {
                        if (sec.FormData.TryGetString("SELL_TOBACCO_STORE_TYPE_OPTION", "") == "SELL_TOBACCO_STORE_TYPE_DUTY_FREE")
                        {
                            fee += 500;
                        }
                        if (sec.FormData.TryGetString("SELL_TOBACCO_STORE_TYPE_OPTION", "") == "SELL_TOBACCO_STORE_TYPE_VAT")
                        {
                            fee += 500;
                        }
                        if (sec.FormData.TryGetString("SELL_TOBACCO_STORE_TYPE_OPTION", "") == "SELL_TOBACCO_STORE_TYPE_NO_VAT")
                        {
                            fee += 100;
                        }
                    }
                    if (sec.FormData.TryGetString("SELL_TOBACCO_TOBACCO_TYPE_SELL_TOBACCO_TOBACCO_TYPE_TOBACCO", "") == "true")
                    {
                        fee += 100;
                    }
                }
            }

            return fee;
            //throw new Exception("ไม่พบข้อมูลการจำหน่ายสุราใน SingleFormRequest");
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
            string src = serverMapPathFunc("~/Uploads/apps/exise/tobacco/tobacco_permit_template.pdf");
            PDFFieldValue field;
            List<PDFFieldValue> model = new List<PDFFieldValue>();
            
            #region 1. ชื่อผู้ขออนุญาติ

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

            // Default state of checkbox is checked, assign value only when we need to uncheck.
            if (!(req.IdentityType == UserTypeEnum.Citizen))
                model.Add(new PDFFieldValue() { FieldName = "RequestAsCitizen", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(req.IdentityType == UserTypeEnum.Juristic))
                model.Add(new PDFFieldValue() { FieldName = "RequestAsJuristic", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (req.IdentityType == UserTypeEnum.Citizen
                || req.IdentityType == UserTypeEnum.Juristic)
                model.Add(new PDFFieldValue() { FieldName = "RequestAsOther", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });


            string identity = req.Data["GENERAL_INFORMATION"].Data["IDENTITY_ID"];
            int digit = 0;
            for (int i = 0; i < identity.Length && digit <= 13; i++)
            {
                if (!IsNumeric(identity.Substring(i, 1))) continue;

                digit++;
                model.Add(new PDFFieldValue() { FieldName = "Identity" + digit, Value = identity.Substring(i, 1), FontSize = 13 });

            }

            string excise = req.Data["SELL_TOBACCO_INFO"].Data["SELL_TOBACCO_EXCISE_ID"];
            digit = 0;
            for (int i = 0; i < excise.Length && digit <= 17; i++)
            {
                if (!IsNumeric(excise.Substring(i, 1))) continue;

                digit++;
                model.Add(new PDFFieldValue() { FieldName = "Excise" + digit, Value = excise.Substring(i, 1), FontSize = 13 });

            }


            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                model.Add(new PDFFieldValue() { FieldName = "HomeNumber", Value = req.Data["GENERAL_INFORMATION"].Data["CITIZEN_HOUSEHOLD_REGISTRATION_ID"] });

                var addrInfo = req.Data["CITIZEN_ADDRESS_INFORMATION"].Data;                
                model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo.GetValue("ADDRESS_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo.GetValue("ADDRESS_BUILDING_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo.GetValue("ADDRESS_ROOMNO_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo.GetValue("ADDRESS_FLOOR_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo.GetValue("ADDRESS_MOO_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo.GetValue("ADDRESS_SOI_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo.GetValue("ADDRESS_ROAD_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo.GetValue("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo.GetValue("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo.GetValue("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo.GetValue("ADDRESS_POSTCODE_CITIZEN_ADDRESS") });

                model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.GetValue("ADDRESS_TELEPHONE_CITIZEN_ADDRESS") });
                if (!string.IsNullOrEmpty(addrInfo.GetValue("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS")))
                    model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo.GetValue("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS");

                model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo.GetValue("CITIZEN_EMAIL") });
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                var addrInfo = req.Data["JURISTIC_ADDRESS_INFORMATION"].Data;
                // ??? model.Add(new PDFFieldValue() { FieldName = "HomeNumber", Value = addrInfo.GetValue(""] });
                model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo.GetValue("ADDRESS_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo.GetValue("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo.GetValue("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo.GetValue("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo.GetValue("ADDRESS_MOO_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo.GetValue("ADDRESS_SOI_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo.GetValue("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo.GetValue("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo.GetValue("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo.GetValue("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo.GetValue("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS") });

                model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.GetValue("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS") });
                if (!string.IsNullOrEmpty(addrInfo.GetValue("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS")))
                    model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo.GetValue("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS");

                model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo.GetValue("JURISTIC_EMAIL") });
            }



            #endregion

            #region 2. สถานที่ขายยาสูบ
            var storeInfo = req.Data["INFORMATION_STORE"].Data;
            model.Add(new PDFFieldValue() { FieldName = "StoreName", Value = storeInfo["INFORMATION_STORE_NAME_TH"] });            
            model.Add(new PDFFieldValue() { FieldName = "StoreNumber", Value = storeInfo["INFORMATION_STORE_HOUSEHOLD_REGISTRATION_ID"] });
            model.Add(new PDFFieldValue() { FieldName = "StoreAddress", Value = storeInfo["ADDRESS_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "StoreBuilding", Value = storeInfo["ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "StoreRoomNumber", Value = storeInfo["ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "StoreFloor", Value = storeInfo["ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "StoreMoo", Value = storeInfo["ADDRESS_MOO_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "StoreSoi", Value = storeInfo["ADDRESS_SOI_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "StoreRoad", Value = storeInfo["ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"] });
            model.Add(new PDFFieldValue() { FieldName = "StoreTumbol", Value = storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "StoreAmphur", Value = storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "StoreProvince", Value = storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"] });
            model.Add(new PDFFieldValue() { FieldName = "StorePostcode", Value = storeInfo["ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"] });

            model.Add(new PDFFieldValue() { FieldName = "StoreTelephone", Value = storeInfo["ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"] });
            if (!string.IsNullOrEmpty(storeInfo["ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"]))
                model.First(o => o.FieldName == "StoreTelephone").Value += " ext. " + storeInfo["ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"];

            model.Add(new PDFFieldValue() { FieldName = "StoreEmail", Value = storeInfo["INFORMATION_STORE_EMAIL"] });
            #endregion

            #region 3. มีความประสงค์ขออนุญาตขายยาสูบ
            var tobaccoInfo = req.Data["SELL_TOBACCO_INFO"].Data;
            if (!IsTrueValue(tobaccoInfo["SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_WHOLE_SELL"]))
                model.Add(new PDFFieldValue() { FieldName = "Option1", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!IsTrueValue(tobaccoInfo["SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_RETAIL"]))
                model.Add(new PDFFieldValue() { FieldName = "Option2", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            //if (!IsTrueValue(tobaccoInfo["SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_OTHER"]))
            //    model.Add(new PDFFieldValue() { FieldName = "Option3", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            // ขายยาสูบประเภทที่ 1  Whole sell
            if (!(IsTrueValue(tobaccoInfo["SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_WHOLE_SELL"]) && IsTrueValue(tobaccoInfo["SELL_TOBACCO_TOBACCO_TYPE_SELL_TOBACCO_TOBACCO_TYPE_CIGARATE"])))
                model.Add(new PDFFieldValue() { FieldName = "Option11", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(tobaccoInfo["SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_WHOLE_SELL"]) && IsTrueValue(tobaccoInfo["SELL_TOBACCO_TOBACCO_TYPE_SELL_TOBACCO_TOBACCO_TYPE_TOBACCO"])))
                model.Add(new PDFFieldValue() { FieldName = "Option12", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(tobaccoInfo["SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_WHOLE_SELL"]) && IsTrueValue(tobaccoInfo["SELL_TOBACCO_TOBACCO_TYPE_SELL_TOBACCO_TOBACCO_TYPE_TOBACCO"]) && tobaccoInfo["SELL_TOBACCO_SELL_TYPE_OPTION"] == "SELL_TOBACCO_SELL_TYPE_TOBACCO"))
                model.Add(new PDFFieldValue() { FieldName = "Option121", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(tobaccoInfo["SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_WHOLE_SELL"]) && IsTrueValue(tobaccoInfo["SELL_TOBACCO_TOBACCO_TYPE_SELL_TOBACCO_TOBACCO_TYPE_TOBACCO"]) && tobaccoInfo["SELL_TOBACCO_SELL_TYPE_OPTION"] == "SELL_TOBACCO_SELL_TYPE_OTHER"))
                model.Add(new PDFFieldValue() { FieldName = "Option122", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });



            // ขายยาสูบประเภทที่ 2  Retail sell
            if (!(IsTrueValue(tobaccoInfo["SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_RETAIL"]) && IsTrueValue(tobaccoInfo["SELL_TOBACCO_TOBACCO_TYPE_SELL_TOBACCO_TOBACCO_TYPE_CIGARATE"])))
                model.Add(new PDFFieldValue() { FieldName = "Option21", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(tobaccoInfo["SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_RETAIL"]) && IsTrueValue(tobaccoInfo["SELL_TOBACCO_TOBACCO_TYPE_SELL_TOBACCO_TOBACCO_TYPE_CIGARATE"])
                    && tobaccoInfo["SELL_TOBACCO_STORE_TYPE_OPTION"] == "SELL_TOBACCO_STORE_TYPE_DUTY_FREE"))
                model.Add(new PDFFieldValue() { FieldName = "Option211", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(tobaccoInfo["SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_RETAIL"]) && IsTrueValue(tobaccoInfo["SELL_TOBACCO_TOBACCO_TYPE_SELL_TOBACCO_TOBACCO_TYPE_CIGARATE"])
                    && tobaccoInfo["SELL_TOBACCO_STORE_TYPE_OPTION"] == "SELL_TOBACCO_STORE_TYPE_VAT"))
                model.Add(new PDFFieldValue() { FieldName = "Option212", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(tobaccoInfo["SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_RETAIL"]) && IsTrueValue(tobaccoInfo["SELL_TOBACCO_TOBACCO_TYPE_SELL_TOBACCO_TOBACCO_TYPE_CIGARATE"])
                    && tobaccoInfo["SELL_TOBACCO_STORE_TYPE_OPTION"] == "SELL_TOBACCO_STORE_TYPE_NO_VAT"))
                model.Add(new PDFFieldValue() { FieldName = "Option213", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (!(IsTrueValue(tobaccoInfo["SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_RETAIL"]) && IsTrueValue(tobaccoInfo["SELL_TOBACCO_TOBACCO_TYPE_SELL_TOBACCO_TOBACCO_TYPE_TOBACCO"])))
                model.Add(new PDFFieldValue() { FieldName = "Option22", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });


            #endregion

            #region 4. เอกสารประกอบการพิจารณา
            model.Add(new PDFFieldValue() { FieldName = "Doc411", Value = CountDocumentText(req, "ID_CARD_COPY") });
            model.Add(new PDFFieldValue() { FieldName = "Doc412", Value = CountDocumentText(req, "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY") });
            model.Add(new PDFFieldValue() { FieldName = "Doc413", Value = "0" });
            model.Add(new PDFFieldValue() { FieldName = "Doc414", Value = CountDocumentText(req, "VAT_REGISTRATION") });
            model.Add(new PDFFieldValue() { FieldName = "Doc415", Value = CountDocumentText(req, "INFORMATION_STORE_RENTAL_CONTRACT") });
            model.Add(new PDFFieldValue() { FieldName = "Doc416", Value = CountDocumentText(req, "SELL_TOBACCO_STORE_LAYOUT") });

            model.Add(new PDFFieldValue() { FieldName = "Doc421", Value = "0" });
            #endregion



            var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model);
            return bytes;

            /*PdfReader reader = new PdfReader(src);
            var ms = new MemoryStream();
            PdfStamper stamper = new PdfStamper(reader, ms);
            var name = req.Data["GENERAL_INFORMATION"].Data["CITIZEN_NAME"] + " " + req.Data["GENERAL_INFORMATION"].Data["CITIZEN_LASTNAME"];
            stamper.AcroFields.SetField("name_th", name);
            stamper.FormFlattening = true;
            stamper.Close();
            
            return ms.ToArray();*/
        }
        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            //หลังจากทำ Biz-api เสร็จแล้วจะต้องนำโค้ดชุดนี้ออก
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
            string tobAppReqId = string.Empty;
            

            try
            {
                switch (stage)
                {
                    case AppsStage.UserUpdate:

                        List<ExciseMetaDataREquest> listReqdocuments = new List<ExciseMetaDataREquest>();
                        var up_ = model.UploadedFiles.ToArray();

                        if (stage == AppsStage.UserUpdate)
                        {
                            up_ = model.UploadedFiles.Where(x => x.Description == "REQUESTED_FILE").ToArray().OrderByDescending(x => x.CreatedDate).Take(1).ToArray();
                        }

                        foreach (var upload in up_)
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

                            documentList = listReqdocuments

                        };
                        var Req = new ExciseFile()
                        {
                            RequestData = post
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
                                cusHouseno = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_HOUSEHOLD_REGISTRATION_ID").Replace("-","");
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
                        tobAppReqId = model.ApplicationRequestID.ToString();
                        facTitleCode = "";
                        facName = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH");
                        facSurname = "";
                        facHouseno = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_HOUSEHOLD_REGISTRATION_ID").Replace("-","");
                        facBuildname = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS");
                        cusIsic= model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("AJAX_DROPDOWN_EXCISE_BUSINESS_TYPE");
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

                        /*
                        //น่าจะต้องเป็น radio และดึงข้อมูลจาก service
                        cusIsic = "1"; //model.Data.TryGetData("SELL_ALCOHOL_INFO").ThenGetStringData("SELL_ALCOHOL_BUSINESS_TYPE_SELL_ALCOHOL_BUSINESS_RESTAURANT");
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
                        */
                        //cusIsic = model.Data.TryGetData("SELL_TOBACCO_INFO").ThenGetStringData("AJAX_DROPDOWN_SELL_ALCOHOL_BUSINESS_TYPE");
                        tobType1Cig = model.Data.TryGetData("SELL_TOBACCO_INFO").ThenGetStringData("SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_WHOLE_SELL") == "true" ? "1" : "";
                        if (model.Data.TryGetData("SELL_TOBACCO_INFO").ThenGetStringData("SELL_TOBACCO_SELL_TYPE_OPTION")== "SELL_TOBACCO_SELL_TYPE_TOBACCO")
                            tobType1Oth = "1";
                        else if (model.Data.TryGetData("SELL_TOBACCO_INFO").ThenGetStringData("SELL_TOBACCO_SELL_TYPE_OPTION") == "SELL_TOBACCO_SELL_TYPE_OTHER")
                            tobType1Oth = "2";
                        //tobType2Cig = model.Data.TryGetData("SELL_TOBACCO_INFO").ThenGetStringData("SELL_TOBACCO_TYPE_SELL_TOBACCO_TYPE_RETAIL")=="true" ? "1":"";




                        tobType2Oth = "";
                        if (model.Data.TryGetData("SELL_TOBACCO_INFO").ThenGetStringData("SELL_TOBACCO_STORE_TYPE_OPTION") == "SELL_TOBACCO_STORE_TYPE_DUTY_FREE")
                        {
                            tobType2Cig = "1";
                            tobType2Oth = "1";
                        }
                        else if (model.Data.TryGetData("SELL_TOBACCO_INFO").ThenGetStringData("SELL_TOBACCO_STORE_TYPE_OPTION") == "SELL_TOBACCO_STORE_TYPE_VAT")
                        {
                            tobType2Cig = "2";
                            tobType2Oth = "1";
                        }
                        else if (model.Data.TryGetData("SELL_TOBACCO_INFO").ThenGetStringData("SELL_TOBACCO_STORE_TYPE_OPTION") == "SELL_TOBACCO_STORE_TYPE_NO_VAT")
                        {
                            tobType2Cig = "3";
                            tobType2Oth = "1";
                        }

                        //REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD  ขออนุญาตเองโดยเจ้าของกิจการ
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE มอบอำนาจให้ผู้อื่นดำเนินการแทน

                        //CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE


                        List<ExciseMetaData> listdocuments = new List<ExciseMetaData>();
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
                                fileType = fileType == "0" ? "501" : fileType;
                                var file = new ExciseMetaData
                                {
                                    docCode = fileType,//"501",// fileTypeCode,
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
                            isicOther = null,
                            liqType1 = "",
                            liqAppReqId = "",
                            liqType2 = "",
                           
                            cardAppReqId = "",
                            cardType1 = "",
                            cardType2 = "",
                            


                            tobAppReqId = tobAppReqId,
                            tobType1Cig = tobType1Cig,                       
                            tobType1Oth = tobType1Oth,                        
                            tobType2Cig = tobType2Cig,
                            tobType2Oth = tobType2Oth,
                        
                           

                            documentList = listdocuments

                        };
                        var Request = new Excise()
                        {
                            RequestData = postinformations
                        };


                        string regisUrl = ConfigurationManager.AppSettings["EXCISE_WS_URL_REQUEST"];
                        var jsonPost = Newtonsoft.Json.JsonConvert.SerializeObject(Request,
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

        public override bool IsEnabledChat()
        {
            return true;
        }
    }
}
