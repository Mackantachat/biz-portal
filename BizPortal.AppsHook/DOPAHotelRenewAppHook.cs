﻿using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels.SingleForm;
using System;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;

namespace BizPortal.AppsHook
{
    public class DOPAHotelRenewAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            decimal fee = 0;
            var sec = sectionData.Where(x => x.SectionName == "APP_HOTEL_RENEW_SECTION").FirstOrDefault();
            //var area = int.Parse(sec.FormData["APP_HOTEL_ROOM"].ToString());
            if (sec != null)
            {
                var area = int.Parse(sec.FormData["APP_HOTEL_ROOM"].ToString());
                if (sec.FormData.TryGetString("DROPDOWN_APP_HOTEL_TYPE", "") == "01")
                {
                    fee += 5000 + (area * 40);
                }
                else if (sec.FormData.TryGetString("DROPDOWN_APP_HOTEL_TYPE", "") == "02")
                {
                    fee += 10000 + (area * 40);
                }
                else if (sec.FormData.TryGetString("DROPDOWN_APP_HOTEL_TYPE", "") == "03")
                {
                    fee += 15000 + (area * 40);
                }
                else if (sec.FormData.TryGetString("DROPDOWN_APP_HOTEL_TYPE", "") == "04")
                {
                    fee += 20000 + (area * 40);
                }
            }

            return fee;
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = serverMapPathFunc("~/Uploads/apps/dopa/25.2_hotel_renew.pdf");
            PDFFieldValue field;
            List<PDFFieldValue> model = new List<PDFFieldValue>();

            string content = Newtonsoft.Json.JsonConvert.SerializeObject(req);

            var generalInfo = req.Data["GENERAL_INFORMATION"].Data;
            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = string.Format("{0}{1} {2}", generalInfo["DROPDOWN_CITIZEN_TITLE_TEXT"], generalInfo["CITIZEN_NAME"], generalInfo["CITIZEN_LASTNAME"]);
                model.Add(field);
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = generalInfo["COMPANY_NAME_TH"];
                model.Add(field);
            }
            
            if (req.Data.ContainsKey("INFORMATION_STORE"))
            {
                var storeInfo = req.Data["INFORMATION_STORE"].Data;
                model.Add(new PDFFieldValue() { FieldName = "StoreName", Value = storeInfo["INFORMATION_STORE_NAME_TH"] });
            }

            PDFConfig cfg = new PDFConfig() { FontName = "THSarabunNew.ttf", FontSize = 14 };
            var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model, cfg);

            return bytes;
        }

        public override bool HasOrgPdfForm
        {
            get
            {
                return false;
            }
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
    }
}
