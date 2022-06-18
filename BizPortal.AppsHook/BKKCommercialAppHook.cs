using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using BizPortal.ViewModels.Apps.DBDStandard;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;
using MongoDB.Driver;
using EGA.WS;
using BizPortal.Utils.Helpers;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using BizPortal.Utils.Extensions;

namespace BizPortal.AppsHook
{
    public class BKKCommercialAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 50;
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = serverMapPathFunc("~/Uploads/apps/bkk/20.1_form_tp_noLock.pdf");
            PDFFieldValue field;
            List<PDFFieldValue> model = new List<PDFFieldValue>();

            string content = Newtonsoft.Json.JsonConvert.SerializeObject(req);

            model.Add(new PDFFieldValue() { FieldName = "TypeChange", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            model.Add(new PDFFieldValue() { FieldName = "TypeCancel", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            var generalInfo = req.Data["GENERAL_INFORMATION"].Data;
            decimal decimalVal;

            #region Section 1-2

            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                // [1]
                if (req.Data.ContainsKey("APP_COMMERCIAL_REGISTRATION_SECTION_PART"))
                {
                    if (req.Data["APP_COMMERCIAL_REGISTRATION_SECTION_PART"].Data["APP_COMMERCIAL_REGISTRATION_TYPE_OPTION"] == "APP_COMMERCIAL_REGISTRATION_TYPE_PARTNERSHIP")
                    {
                        #region ห้างหุ้นส่วน
                        model.Add(new PDFFieldValue() { FieldName = "IdentityName", Value = req.Data["INFORMATION_STORE"].Data["INFORMATION_STORE_NAME_TH"] });

                        // Finding first director
                        if (req.Data.ContainsKey("APP_COMMERCIAL_DIRECTOR_SECTION"))
                        {
                            int total = int.Parse(req.Data["APP_COMMERCIAL_DIRECTOR_SECTION"].Data["APP_COMMERCIAL_DIRECTOR_SECTION_TOTAL"]);
                            if (total > 0)
                            {
                                var dirInfo = req.Data["APP_COMMERCIAL_DIRECTOR_SECTION"].Data;
                                model.Add(new PDFFieldValue() { FieldName = "Age", Value = dirInfo["APP_COMMERCIAL_DIRECTOR_SECTION_AGE_0"] });
                                model.Add(new PDFFieldValue() { FieldName = "Nationality", Value = dirInfo["APP_COMMERCIAL_DIRECTOR_SECTION_NATIONALITY_0"] });
                            }
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

                        model.Add(new PDFFieldValue() { FieldName = "Fax", Value = addrInfo["ADDRESS_FAX_INFORMATION_STORE__ADDRESS"] });

                        #endregion
                    }
                    else
                    {
                        #region บุคคลธรรมดา
                        field = new PDFFieldValue() { FieldName = "IdentityName" };
                        field.Value = string.Format("{0}{1} {2}", generalInfo["DROPDOWN_CITIZEN_TITLE_TEXT"], generalInfo["CITIZEN_NAME"], generalInfo["CITIZEN_LASTNAME"]);
                        model.Add(field);

                        model.Add(new PDFFieldValue() { FieldName = "Age", Value = generalInfo["GENERAL_INFORMATION__CITIZEN_AGE"] });
                        model.Add(new PDFFieldValue() { FieldName = "Origin", Value = generalInfo["DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE_TEXT"] });
                        model.Add(new PDFFieldValue() { FieldName = "Nationality", Value = generalInfo["DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT"] });

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

                        model.Add(new PDFFieldValue() { FieldName = "Fax", Value = addrInfo["ADDRESS_FAX_CITIZEN_ADDRESS"] });
                        #endregion
                    }
                }

                // [2]
                if (req.Data.ContainsKey("APP_COMMERCIAL_REGISTRATION_SECTION_PART") && req.Data.ContainsKey("INFORMATION_STORE"))
                {
                    model.Add(new PDFFieldValue() { FieldName = "CompanyNameTH", Value = req.Data["INFORMATION_STORE"].Data["INFORMATION_STORE_NAME_TH"] });
                    model.Add(new PDFFieldValue() { FieldName = "CompanyNameEN", Value = req.Data["INFORMATION_STORE"].Data["INFORMATION_STORE_NAME_EN"] });
                }
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                model.Add(new PDFFieldValue() { FieldName = "IdentityName", Value = req.IdentityName });

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

                model.Add(new PDFFieldValue() { FieldName = "Fax", Value = addrInfo["ADDRESS_FAX_JURISTIC_HQ_ADDRESS"] });

                // [2]
                model.Add(new PDFFieldValue() { FieldName = "CompanyNameTH", Value = generalInfo["COMPANY_NAME_TH"] });
                model.Add(new PDFFieldValue() { FieldName = "CompanyNameEN", Value = generalInfo["COMPANY_NAME_EN"] });
            }

            #endregion

            #region Section 3
            if (req.Data.ContainsKey("APP_COMMERCIAL_REGISTRATION_SECTION")) 
            {
                var regisInfo = req.Data["APP_COMMERCIAL_REGISTRATION_SECTION"].Data;
                int totalRegis = int.Parse(regisInfo["APP_COMMERCIAL_REGISTRATION_SECTION_TOTAL"]);
                for (int i = 0; i < totalRegis && i < 4; i++)
                {
                    model.Add(new PDFFieldValue() { FieldName = "RegistrationType" + (i + 1), Value = regisInfo["APP_COMMERCIAL_REGISTRATION_SECTION_TYPE_" + i] });
                }
            }

            if (req.Data.ContainsKey("APP_COMMERCIAL_REGISTRATION_SECTION_PART")) 
            {
                var regisPart = req.Data["APP_COMMERCIAL_REGISTRATION_SECTION_PART"].Data;
                decimalVal = StringHelper.ToDecimal(regisPart["APP_COMMERCIAL_REGISTRATION_SECTION_REGISTERED_CAPITAL"], 0);
                model.Add(new PDFFieldValue() { FieldName = "Capital", Value = decimalVal.ToString("#,##0") });
                model.Add(new PDFFieldValue() { FieldName = "CapitalText", Value = regisPart["APP_COMMERCIAL_REGISTRATION_SECTION_REGISTERED_CAPITAL_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "EstablishDate", Value = regisPart["APP_COMMERCIAL_REGISTRATION_SECTION_START_DATE"] });
                model.Add(new PDFFieldValue() { FieldName = "RegisterDate", Value = regisPart["APP_COMMERCIAL_REGISTRATION_SECTION_REGISTER_DATE"] });
            }

            #endregion

            #region Section 5: Head Quater

            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                var addrInfo = req.Data["INFORMATION_STORE"].Data;
                model.Add(new PDFFieldValue() { FieldName = "HQAddress", Value = addrInfo["ADDRESS_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQBuilding", Value = addrInfo["ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQRoomNumber", Value = addrInfo["ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQFloor", Value = addrInfo["ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQMoo", Value = addrInfo["ADDRESS_MOO_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQSoi", Value = addrInfo["ADDRESS_SOI_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQRoad", Value = addrInfo["ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQTumbol", Value = addrInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "HQAmphur", Value = addrInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "HQProvince", Value = addrInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "HQPostcode", Value = addrInfo["ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"] });

                model.Add(new PDFFieldValue() { FieldName = "HQTelephone", Value = addrInfo["ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"] });
                if (!string.IsNullOrEmpty(addrInfo["ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"]))
                    model.First(o => o.FieldName == "HQTelephone").Value += " ext. " + addrInfo["ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"];

                model.Add(new PDFFieldValue() { FieldName = "HQFax", Value = addrInfo["ADDRESS_FAX_INFORMATION_STORE__ADDRESS"] });
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                var addrInfo = req.Data["JURISTIC_ADDRESS_INFORMATION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "HQAddress", Value = addrInfo["ADDRESS_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQBuilding", Value = addrInfo["ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQRoomNumber", Value = addrInfo["ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQFloor", Value = addrInfo["ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQMoo", Value = addrInfo["ADDRESS_MOO_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQSoi", Value = addrInfo["ADDRESS_SOI_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQRoad", Value = addrInfo["ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"] });
                model.Add(new PDFFieldValue() { FieldName = "HQTumbol", Value = addrInfo["ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "HQAmphur", Value = addrInfo["ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "HQProvince", Value = addrInfo["ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "HQPostcode", Value = addrInfo["ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"] });

                model.Add(new PDFFieldValue() { FieldName = "HQTelephone", Value = addrInfo["ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS"] });
                if (!string.IsNullOrEmpty(addrInfo["ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS"]))
                    model.First(o => o.FieldName == "HQTelephone").Value += " ext. " + addrInfo["ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS"];

                model.Add(new PDFFieldValue() { FieldName = "HQFax", Value = addrInfo["ADDRESS_FAX_JURISTIC_HQ_ADDRESS"] });
            }

            #endregion

            #region Section 6. Director

            if (req.Data.ContainsKey("APP_COMMERCIAL_DIRECTOR_SECTION")) 
            {
                var directorInfo = req.Data["APP_COMMERCIAL_DIRECTOR_SECTION"].Data;
                int totalDirector = int.Parse(directorInfo["APP_COMMERCIAL_DIRECTOR_SECTION_TOTAL"]);
                if (totalDirector > 0)
                {
                    model.Add(new PDFFieldValue() { FieldName = "MgrName", Value = directorInfo["APP_COMMERCIAL_DIRECTOR_SECTION_FIRSTNAME_0"] + " " + directorInfo["APP_COMMERCIAL_DIRECTOR_SECTION_LASTNAME_0"] });
                    model.Add(new PDFFieldValue() { FieldName = "MgrAge", Value = directorInfo["APP_COMMERCIAL_DIRECTOR_SECTION_AGE_0"] });
                    model.Add(new PDFFieldValue() { FieldName = "MgrNationality", Value = directorInfo["APP_COMMERCIAL_DIRECTOR_SECTION_NATIONALITY_0"] });
                    model.Add(new PDFFieldValue() { FieldName = "MgrAddress", Value = directorInfo["ADDRESS_APP_COMMERCIAL_DIRECTOR_SECTION_ADDRESS_0"] });
                    model.Add(new PDFFieldValue() { FieldName = "MgrMoo", Value = directorInfo["ADDRESS_MOO_APP_COMMERCIAL_DIRECTOR_SECTION_ADDRESS_0"] });
                    model.Add(new PDFFieldValue() { FieldName = "MgrSoi", Value = directorInfo["ADDRESS_SOI_APP_COMMERCIAL_DIRECTOR_SECTION_ADDRESS_0"] });
                    model.Add(new PDFFieldValue() { FieldName = "MgrRoad", Value = directorInfo["ADDRESS_ROAD_APP_COMMERCIAL_DIRECTOR_SECTION_ADDRESS_0"] });
                    model.Add(new PDFFieldValue() { FieldName = "MgrTumbol", Value = directorInfo["ADDRESS_TUMBOL_APP_COMMERCIAL_DIRECTOR_SECTION_ADDRESS_TEXT_0"] });
                    model.Add(new PDFFieldValue() { FieldName = "MgrAmphur", Value = directorInfo["ADDRESS_AMPHUR_APP_COMMERCIAL_DIRECTOR_SECTION_ADDRESS_TEXT_0"] });
                    model.Add(new PDFFieldValue() { FieldName = "MgrProvince", Value = directorInfo["ADDRESS_PROVINCE_APP_COMMERCIAL_DIRECTOR_SECTION_ADDRESS_TEXT_0"] });
                    model.Add(new PDFFieldValue() { FieldName = "MgrTelephone", Value = directorInfo["ADDRESS_TELEPHONE_APP_COMMERCIAL_DIRECTOR_SECTION_ADDRESS_0"] });
                    if (!string.IsNullOrEmpty(directorInfo["ADDRESS_TELEPHONE_EXT_APP_COMMERCIAL_DIRECTOR_SECTION_ADDRESS_0"]))
                        model.First(o => o.FieldName == "MgrTelephone").Value += " ext. " + directorInfo["ADDRESS_TELEPHONE_EXT_APP_COMMERCIAL_DIRECTOR_SECTION_ADDRESS_0"];

                    model.Add(new PDFFieldValue() { FieldName = "MgrFax", Value = directorInfo["ADDRESS_FAX_APP_COMMERCIAL_DIRECTOR_SECTION_ADDRESS_0"] });
                }
            }
            #endregion

            #region Section 11 ห้างหุ้นส่วน/Partner
            if (req.Data.ContainsKey("APP_COMMERCIAL_PARTNER_SECTION_PART"))
            {
                var partnerPart = req.Data["APP_COMMERCIAL_PARTNER_SECTION_PART"].Data;
                int totalPartner = int.Parse(partnerPart["APP_COMMERCIAL_PARTNER_SECTION_PART_TOTAL"]);

                model.Add(new PDFFieldValue() { FieldName = "HolderTotal", Value = partnerPart["APP_COMMERCIAL_PARTNER_SECTION_PART_TOTAL"] });
                for (int i = 0; i < totalPartner && i < 3; i++)
                {
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Name", i + 1), Value = partnerPart["APP_COMMERCIAL_PARTNER_SECTION_FIRSTNAME_" + i] + " " + partnerPart["APP_COMMERCIAL_PARTNER_SECTION_LASTNAME_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Age", i + 1), Value = partnerPart["APP_COMMERCIAL_PARTNER_SECTION_AGE_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Origin", i + 1), Value = partnerPart["APP_COMMERCIAL_PARTNER_SECTION_RACE_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Nationality", i + 1), Value = partnerPart["APP_COMMERCIAL_PARTNER_SECTION_NATIONALITY_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Address", i + 1), Value = partnerPart["ADDRESS_APP_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Moo", i + 1), Value = partnerPart["ADDRESS_MOO_APP_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Soi", i + 1), Value = partnerPart["ADDRESS_SOI_APP_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Road", i + 1), Value = partnerPart["ADDRESS_ROAD_APP_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Tumbol", i + 1), Value = partnerPart["ADDRESS_TUMBOL_APP_COMMERCIAL_PARTNER_SECTION_ADDRESS_TEXT_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Amphur", i + 1), Value = partnerPart["ADDRESS_AMPHUR_APP_COMMERCIAL_PARTNER_SECTION_ADDRESS_TEXT_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Province", i + 1), Value = partnerPart["ADDRESS_PROVINCE_APP_COMMERCIAL_PARTNER_SECTION_ADDRESS_TEXT_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Telephone", i + 1), Value = partnerPart["ADDRESS_AMPHUR_APP_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Fax", i + 1), Value = partnerPart["ADDRESS_AMPHUR_APP_COMMERCIAL_PARTNER_SECTION_ADDRESS_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Share", i + 1), Value = StringHelper.ToDecimal(partnerPart["APP_COMMERCIAL_PARTNER_SECTION_SHARE_" + i], 0).ToString("#,##0") });
                    model.Add(new PDFFieldValue() { FieldName = string.Format("Holder{0}Amount", i + 1), Value = StringHelper.ToDecimal(partnerPart["APP_COMMERCIAL_PARTNER_SECTION_AMOUNT_" + i], 0).ToString("#,##0") });
                }
            }

            #endregion

            #region Section 12  Company holder
            if (req.Data.ContainsKey("APP_COMMERCIAL_COMPANY_SECTION"))
            {
                var comInfo = req.Data["APP_COMMERCIAL_COMPANY_SECTION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "StockCapital", Value = StringHelper.ToDecimal(comInfo["APP_COMMERCIAL_COMPANY_SECTION_REGISTERED_CAPITAL"], 0).ToString("#,##0") });
                model.Add(new PDFFieldValue() { FieldName = "StockTotal", Value = StringHelper.ToDecimal(comInfo["APP_COMMERCIAL_COMPANY_SECTION_EACH_SHARE"], 0).ToString("#,##0") });
                model.Add(new PDFFieldValue() { FieldName = "StockPrice", Value = StringHelper.ToDecimal(comInfo["APP_COMMERCIAL_COMPANY_SECTION_PRICE_SHARE"], 0).ToString("#,##0.00") });
            }


            if (req.Data.ContainsKey("APP_COMMERCIAL_COMPANY_SECTION_PART"))
            {
                var comPart = req.Data["APP_COMMERCIAL_COMPANY_SECTION_PART"].Data;
                int totalComp = int.Parse(comPart["APP_COMMERCIAL_COMPANY_SECTION_PART_TOTAL"]);
                for (int i = 0; i < totalComp && i < 4; i++)
                {
                    model.Add(new PDFFieldValue() { FieldName = "StockNationality" + (i + 1), Value = comPart["APP_COMMERCIAL_COMPANY_SECTION_NATIONALITY_" + i] });
                    model.Add(new PDFFieldValue() { FieldName = "StockTotal" + (i + 1), Value = StringHelper.ToDecimal(comPart["APP_COMMERCIAL_COMPANY_SECTION_OWNED_SHARE_" + i], 0).ToString("#,##0") });
                }
            }


            #endregion

            if (req.Data.ContainsKey("APP_COMMERCIAL_OTHER_SECTION"))
            {
                model.Add(new PDFFieldValue() { FieldName = "OtherText", Value = req.Data["APP_COMMERCIAL_OTHER_SECTION"].Data["APP_COMMERCIAL_OTHER_SECTION_TEXT"] });
            }

            PDFConfig cfg = new PDFConfig() { FontName = "Angsima.ttf", FontSize = 14 };
            var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model, cfg);

            return bytes;
        }

        public override bool HasOrgPdfForm
        {
            get
            {
                return true;
            }
        }

        public override bool IsEnabledChat()
        {
            return true;
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            try
            {
                var tResult = base.Invoke(stage, model, appHookInfo, ref request);

                if (stage == AppsStage.UserCreate)
                {
                    var standardData = GenerateStandardFormDataCreate(model, request);
                    var files = new List<StandardApiFileInfo_Base64>();

                    foreach (var upload in request.UploadedFiles)
                    {
                        foreach (var f in upload.Files)
                        {
                            var fileMetaModel = new BizPortal.ViewModels.V2.FileMetadata
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

                            var file = new StandardApiFileInfo_Base64
                            {
                                Name = f.FileTypeCode,
                                Content = Convert.ToBase64String(fileMetaModel.GetBytes()),
                                FileName = f.FileName,
                                ContentType = f.ContentType,
                                Description = Description,
                            };

                            files.Add(file);
                        }
                    }

                    string regisUrl = ConfigurationManager.AppSettings["BKK_COMMERCIAL_WS_URL"];

                    var post = new
                    {
                        Data = standardData,
                        Files = files,
                    };
                    var jsonPost = JsonConvert.SerializeObject(post);

                    Api.OnCheckingApplicationError += (ex) =>
                    {
                        tResult.Exception = ex;
                        var egaEx = ex as EGAWSAPIException;
                        if (egaEx != null)
                        {
                            var message = string.Format("{0}: {1}", (int)egaEx.HttpStatusCode, egaEx.ResponseData["Message"].ToString());
                            tResult.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, message, egaEx.ResponseData.ToString(), jsonPost);
                            tResult.Message = egaEx.ResponseData["Message"].ToString();
                        }
                        else
                        {
                            tResult.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, ex.Message, ex.StackTrace, jsonPost);
                            tResult.Message = ex.Message;
                        }
                    };

                    var result = Api.Call(regisUrl, EGA.WS.HttpVerb.POST, null, jsonPost, EGA.WS.ContentType.ApplicationJson);
                    if (result != null && false)
                    {
                        if (result.HasValues && result["responseStatus"].ToString() == "OK")
                        {

                        }
                        else
                        {
                            string error = "Unable to request to " + regisUrl + ".";
                            tResult.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, result.ToString(), jsonPost, true);
                            throw new Exception(error);
                        }
                    }
                }
                else if (stage == AppsStage.UserUpdate)
                {
                    var standardData = GenerateStandardFormData(model, request);
                    var files = new List<StandardApiFileInfo_Base64>();

                    var UploadedFiles = request.UploadedFiles.LastOrDefault().Files;

                    foreach (var f in UploadedFiles)
                    {
                        var fileMetaModel = new BizPortal.ViewModels.V2.FileMetadata
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

                        string FileName = "";

                        if (f.Extras.ContainsKey("REQUEST_FILE_NAME"))
                        {
                            FileName = f.Extras["REQUEST_FILE_NAME"].ToString();
                        }
                        else
                        {
                            FileName = f.FileName;
                        }

                        var file = new StandardApiFileInfo_Base64
                        {
                            Name = FileName,
                            Content = Convert.ToBase64String(fileMetaModel.GetBytes()),
                            FileName = f.FileName,
                            ContentType = f.ContentType
                        };

                        files.Add(file);
                    }

                    string regisUrl = ConfigurationManager.AppSettings["BKK_COMMERCIAL_WS_URL"];

                    var post = new
                    {
                        Data = standardData,
                        Files = files,
                    };
                    var jsonPost = JsonConvert.SerializeObject(post);
                    Api.OnCheckingApplicationError += (ex) =>
                    {
                        tResult.Exception = ex;
                        var egaEx = ex as EGAWSAPIException;
                        if (egaEx != null)
                        {
                            var message = string.Format("{0}: {1}", (int)egaEx.HttpStatusCode, egaEx.ResponseData["Message"].ToString());
                            tResult.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, message, egaEx.ResponseData.ToString(), jsonPost);
                            tResult.Message = egaEx.ResponseData["Message"].ToString();
                        }
                        else
                        {
                            tResult.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, ex.Message, ex.StackTrace, jsonPost);
                            tResult.Message = ex.Message;
                        }
                    };

                    var result = Api.Call(regisUrl, EGA.WS.HttpVerb.POST, null, jsonPost, EGA.WS.ContentType.ApplicationJson);
                    if (result != null && false)
                    {
                        if (result.HasValues && result["responseStatus"].ToString() == "OK")
                        {

                        }
                        else
                        {
                            string error = "Unable to request to " + regisUrl + ".";
                            tResult.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, result.ToString(), jsonPost, true);
                            throw new Exception(error);
                        }
                    }
                }
                return tResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
