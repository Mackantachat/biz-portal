using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.SingleForm;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using System.Globalization;

namespace BizPortal.AppsHook
{
    public class DLTNonRegularTruckRenewAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return null;
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = serverMapPathFunc("~/Uploads/apps/dlt/app39_renew.pdf");
            PDFFieldValue field;
            List<PDFFieldValue> model = new List<PDFFieldValue>();

            //string content = Newtonsoft.Json.JsonConvert.SerializeObject(req);

            model.Add(new PDFFieldValue() { FieldName = "CreatedDay", Value = req.CreatedDate.ToString("dd", CultureInfo.CreateSpecificCulture("th-TH")) });
            model.Add(new PDFFieldValue() { FieldName = "CreatedMonth", Value = req.CreatedDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("th-TH")) });
            model.Add(new PDFFieldValue() { FieldName = "CreatedYear", Value = req.CreatedDate.ToString("yyyy", CultureInfo.CreateSpecificCulture("th-TH")) });

            #region Register name and Address
            var generalInfo = req.Data["GENERAL_INFORMATION"].Data;

            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = string.Format("{0}{1} {2}", generalInfo.TryGetString("DROPDOWN_CITIZEN_TITLE_TEXT", ""), generalInfo.TryGetString("CITIZEN_NAME", ""), generalInfo.TryGetString("CITIZEN_LASTNAME", ""));
                model.Add(field);

                // store address
                if (req.Data.ContainsKey("INFORMATION_STORE"))
                {
                    var addrInfo = req.Data["INFORMATION_STORE"].Data;

                    model.Add(new PDFFieldValue() { FieldName = "StoreName", Value = addrInfo.TryGetString("INFORMATION_STORE_NAME_TH") });
                                        
                    string addr = GetSingleLineStoreAddress(addrInfo);
                    model.Add(new PDFFieldValue() { FieldName = "StoreAddress", Value = addr });

                }

            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = generalInfo.TryGetString("COMPANY_NAME_TH", "");
                model.Add(field);

                model.Add(new PDFFieldValue() { FieldName = "StoreName", Value = generalInfo.TryGetString("COMPANY_NAME_TH") });

                // register address
                if (req.Data.ContainsKey("JURISTIC_ADDRESS_INFORMATION"))
                {
                    var addrInfo = req.Data["JURISTIC_ADDRESS_INFORMATION"].Data;

                    string addr = GetSingleLineJuristicAddress(addrInfo);
                    model.Add(new PDFFieldValue() { FieldName = "StoreAddress", Value = addr });

                }
            }
            #endregion

            if (req.Data.ContainsKey("APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_INFO_SECTION"))
            {
                string provinceList = "";
                if (req.Data["APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_INFO_SECTION"].Data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_INFO_SECTION_TRANSPORT_IN_OPTION") == "KINGDOM")
                {
                    provinceList = "ทั่วราชอาณาจักร";
                }
                else if (req.Data.ContainsKey("APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_TRANSPORT_PROVINCE_SECTION"))
                {
                    var provinceData = req.Data["APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_TRANSPORT_PROVINCE_SECTION"].Data;
                    int total = int.Parse(provinceData.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_TRANSPORT_PROVINCE_SECTION_TOTAL", "0"));
                    for (int i = 0; i < total; i++)
                    {
                        if (provinceList != "") provinceList += ", ";
                        provinceList += provinceData.TryGetString("AJAX_DROPDOWN_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_TRANSPORT_PROVINCE_SECTION_PROVINCE_TEXT_" + i);
                    }
                }

                field = new PDFFieldValue() { FieldName = "ProvinceList" };
                field.Value = provinceList;
                model.Add(field);
            }

            if (req.Data.ContainsKey("APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2"))
            {
                var data = req.Data["APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2"].Data;
                model.Add(new PDFFieldValue() { FieldName = "PermitNo", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_RENEW_PERMIT") });
                model.Add(new PDFFieldValue() { FieldName = "PermitExpired", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_EXPIRE_DATE") });
            }

            PDFConfig cfg = new PDFConfig() { FontName = "Angsima.ttf", FontSize = 14 };
            var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model, cfg);

            return bytes;
        }

        private static string GetSingleLineStoreAddress(Dictionary<string, string> data)
        {
            string addr = data.TryGetString("ADDRESS_INFORMATION_STORE__ADDRESS");

            if (data.TryGetString("ADDRESS_MOO_INFORMATION_STORE__ADDRESS") != "") addr += string.Format(" หมู่ {0} ", data.TryGetString("ADDRESS_MOO_INFORMATION_STORE__ADDRESS"));
            //if (data.TryGetString("ADDRESS_SOI_INFORMATION_STORE__ADDRESS") != "") addr += string.Format(" ซอย{0} ", data.TryGetString("ADDRESS_SOI_INFORMATION_STORE__ADDRESS"));
            if (data.TryGetString("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS") != "") addr += string.Format(" อาคาร{0} ", data.TryGetString("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"));
            if (data.TryGetString("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS") != "") addr += string.Format(" ห้อง {0} ", data.TryGetString("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS"));
            if (data.TryGetString("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS") != "") addr += string.Format(" ชั้น {0} ", data.TryGetString("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS"));
            if (data.TryGetString("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS") != "") addr += string.Format(" ถนน{0} ", data.TryGetString("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"));
            if (data.TryGetString("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT") != "") addr += string.Format(" ตำบล{0} ", data.TryGetString("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"));
            if (data.TryGetString("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT") != "") addr += string.Format(" อำเภอ{0} ", data.TryGetString("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"));
            if (data.TryGetString("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT") != "") addr += string.Format("  {0} ", data.TryGetString("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"));
            if (data.TryGetString("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS") != "") addr += string.Format(" {0} ", data.TryGetString("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"));
            return addr;
        }

        private static string GetSingleLineJuristicAddress(Dictionary<string, string> data)
        {
            string addr = data.TryGetString("ADDRESS_JURISTIC_HQ_ADDRESS");

            if (data.TryGetString("ADDRESS_MOO_JURISTIC_HQ_ADDRESS") != "") addr += string.Format(" หมู่ {0} ", data.TryGetString("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"));
            //if (data.TryGetString("ADDRESS_SOI_JURISTIC_HQ_ADDRESS") != "") addr += string.Format(" ซอย{0} ", data.TryGetString("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"));
            if (data.TryGetString("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS") != "") addr += string.Format(" อาคาร{0} ", data.TryGetString("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS"));
            if (data.TryGetString("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS") != "") addr += string.Format(" ห้อง {0} ", data.TryGetString("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS"));
            if (data.TryGetString("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS") != "") addr += string.Format(" ชั้น {0} ", data.TryGetString("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS"));
            if (data.TryGetString("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS") != "") addr += string.Format(" ถนน{0} ", data.TryGetString("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"));
            if (data.TryGetString("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT") != "") addr += string.Format(" ตำบล{0} ", data.TryGetString("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT"));
            if (data.TryGetString("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT") != "") addr += string.Format(" อำเภอ{0} ", data.TryGetString("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT"));
            if (data.TryGetString("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT") != "") addr += string.Format("  {0} ", data.TryGetString("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT"));
            if (data.TryGetString("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS") != "") addr += string.Format(" {0} ", data.TryGetString("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"));

            return addr;
        }

        public override bool HasOrgPdfForm
        {
            get
            {
                return true;
            }
        }
    }
}
