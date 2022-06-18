﻿using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.V2;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using BizPortal.Utils.Helpers;

namespace BizPortal.AppsHook
{
    public class NBTCRadioRenewAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            decimal fee = 0;
            var sec = sectionData.Where(x => x.SectionName == "APP_RADIO_RENEW_SECTION").FirstOrDefault();
            if (sec != null)
            {
                if (sec.FormData.TryGetString("APP_RADIO_SECTION_RENEW_TYPE_OPTION", "") == "1")
                {
                    fee += 1070;
                }
                else if(sec.FormData.TryGetString("APP_RADIO_SECTION_RENEW_TYPE_OPTION", "") == "2")
                {
                    fee += 535;
                }
            }
            return fee;
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = serverMapPathFunc("~/Uploads/apps/nbct/28.1_nbct_radio.pdf");
            PDFFieldValue field;
            List<PDFFieldValue> model = new List<PDFFieldValue>();

            //string content = Newtonsoft.Json.JsonConvert.SerializeObject(req);

            #region Section 1

            model.Add(new PDFFieldValue() { FieldName = "Opt1Sell", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            model.Add(new PDFFieldValue() { FieldName = "Opt1Fix", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (req.Data.ContainsKey("APP_RADIO_RENEW_SECTION"))
            {
                if (req.Data["APP_RADIO_RENEW_SECTION"].Data.TryGetString("APP_RADIO_SECTION_RENEW_TYPE_OPTION", "") == "1") model.Add(new PDFFieldValue() { FieldName = "RenewType", Value = "ค้า" });
                if (req.Data["APP_RADIO_RENEW_SECTION"].Data.TryGetString("APP_RADIO_SECTION_RENEW_TYPE_OPTION", "") == "2") model.Add(new PDFFieldValue() { FieldName = "RenewType", Value = "ซ่อมแซม" });
            }

            var generalInfo = req.Data["GENERAL_INFORMATION"].Data;

            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                model.Add(new PDFFieldValue() { FieldName = "PersonFirstName", Value = generalInfo.TryGetString("CITIZEN_NAME", "") });
                model.Add(new PDFFieldValue() { FieldName = "PersonLastName", Value = generalInfo.TryGetString("CITIZEN_LASTNAME", "") });
                model.Add(new PDFFieldValue() { FieldName = "Nationality", Value = generalInfo.TryGetString("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT", "") });

                model.Add(new PDFFieldValue() { FieldName = "PersonBirthDate", Value = generalInfo.TryGetString("BIRTH_DATE", "") });

                string identity = generalInfo["IDENTITY_ID"];
                FillIdentityDigits(model, identity, "Identity", 13);

                var storeInfo = req.Data["INFORMATION_STORE"].Data;
                if (storeInfo.TryGetString("INFORMATION_STORE_COMMERCE_REGISTRATION_CITIZEN_OPTION", "") == "INFORMATION_STORE_COMMERCE_REGISTRATION_CITIZEN_OPTION__YES")
                {
                    // ใช่ ฉันได้จดทะเบียนพาณิชย์
                    var addrInfo = req.Data["INFORMATION_STORE"].Data;
                    model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo.TryGetString("ADDRESS_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo.TryGetString("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo.TryGetString("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo.TryGetString("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo.TryGetString("ADDRESS_MOO_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo.TryGetString("ADDRESS_SOI_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo.TryGetString("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo.TryGetString("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo.TryGetString("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo.TryGetString("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo.TryGetString("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS", "") });

                    model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS", "") });

                    model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS", "") });
                    if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", "")))
                        model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", "");

                    model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo.TryGetString("INFORMATION_STORE_EMAIL", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Fax", Value = addrInfo.TryGetString("ADDRESS_FAX_INFORMATION_STORE__ADDRESS", "") });
                }
                else
                {
                    // ไม่ใช่ ฉันไม่ได้จดทะเบียนพาณิชย์
                    var addrInfo = req.Data["CITIZEN_ADDRESS_INFORMATION"].Data;
                    model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo.TryGetString("ADDRESS_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo.TryGetString("ADDRESS_BUILDING_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo.TryGetString("ADDRESS_ROOMNO_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo.TryGetString("ADDRESS_FLOOR_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo.TryGetString("ADDRESS_MOO_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo.TryGetString("ADDRESS_SOI_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo.TryGetString("ADDRESS_ROAD_CITIZEN_ADDRESS", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo.TryGetString("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo.TryGetString("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo.TryGetString("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT", "") });
                    model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo.TryGetString("ADDRESS_POSTCODE_CITIZEN_ADDRESS", "") });

                    model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_CITIZEN_ADDRESS", "") });
                    if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS", "")))
                        model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS", "");

                    model.Add(new PDFFieldValue() { FieldName = "Fax", Value = addrInfo.TryGetString("ADDRESS_FAX_CITIZEN_ADDRESS", "") });
                }
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                model.Add(new PDFFieldValue() { FieldName = "JuName", Value = generalInfo.TryGetString("COMPANY_NAME_TH", "") });
                model.Add(new PDFFieldValue() { FieldName = "JuID", Value = generalInfo.TryGetString("IDENTITY_ID", "") });
                model.Add(new PDFFieldValue() { FieldName = "JuRegisterDate", Value = generalInfo.TryGetString("REGISTER_DATE", "") });

                var addrInfo = req.Data["INFORMATION_STORE"].Data;
                model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo.TryGetString("ADDRESS_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo.TryGetString("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "RoomNumber", Value = addrInfo.TryGetString("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo.TryGetString("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo.TryGetString("ADDRESS_MOO_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo.TryGetString("ADDRESS_SOI_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo.TryGetString("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo.TryGetString("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo.TryGetString("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo.TryGetString("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "Postcode", Value = addrInfo.TryGetString("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS", "") });

                model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS", "") });
                if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", "")))
                    model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", "");

                model.Add(new PDFFieldValue() { FieldName = "Email", Value = addrInfo.TryGetString("INFORMATION_STORE_EMAIL", "") });
                model.Add(new PDFFieldValue() { FieldName = "Fax", Value = addrInfo.TryGetString("ADDRESS_FAX_INFORMATION_STORE__ADDRESS", "") });
            }

            #endregion

            #region Section 2

            if (req.Data.ContainsKey("INFORMATION_STORE"))
            {
                var storeInfo = req.Data["INFORMATION_STORE"].Data;
                model.Add(new PDFFieldValue() { FieldName = "StoreName", Value = storeInfo.TryGetString("INFORMATION_STORE_NAME_TH", "") });
            }

            // Uncheck First license checkboxes
            model.Add(new PDFFieldValue() { FieldName = "OptNew", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            model.Add(new PDFFieldValue() { FieldName = "OptDocID", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            model.Add(new PDFFieldValue() { FieldName = "OptDocCommerce", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            model.Add(new PDFFieldValue() { FieldName = "OptDocJuristic", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            
            if (req.Data.ContainsKey("APP_RADIO_RENEW_SECTION"))
            {
                string renewID = req.Data["APP_RADIO_RENEW_SECTION"].Data.TryGetString("APP_RADIO_RENEW_ID", "");
                FillIdentityDigits(model, renewID, "PrevID", 8, true);
            }

            // Representative
            if (!(req.Data.ContainsKey("REQUESTOR_INFORMATION__HEADER") 
                    && ((req.IdentityType == UserTypeEnum.Citizen && req.Data["REQUESTOR_INFORMATION__HEADER"].Data.TryGetString("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION", "") == "REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE"))
                        || (req.IdentityType == UserTypeEnum.Juristic && req.Data["REQUESTOR_INFORMATION__HEADER"].Data.TryGetString("REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION", "") == "REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE")))
            {
                model.Add(new PDFFieldValue() { FieldName = "OptDocRep", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
                model.Add(new PDFFieldValue() { FieldName = "OptDocOwnerID", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
                model.Add(new PDFFieldValue() { FieldName = "OptDocRepID", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            #endregion


            PDFConfig cfg = new PDFConfig() { FontName = "Angsima.ttf", FontSize = 12 };
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
    }
}
