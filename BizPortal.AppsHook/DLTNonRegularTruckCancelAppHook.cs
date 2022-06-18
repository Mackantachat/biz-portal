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
    public class DLTNonRegularTruckCancelAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return null;
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = serverMapPathFunc("~/Uploads/apps/dlt/app39_edit.pdf");    // แก้ไขกับยกเลิกใช้ฟอร์มเดียวกัน
            PDFFieldValue field;
            List<PDFFieldValue> model = new List<PDFFieldValue>();

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

                DateTime birthDate = DateTime.Today;
                string birthDateStr = generalInfo.TryGetString("BIRTH_DATE");
                if (DateTime.TryParseExact(birthDateStr, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th-TH"), DateTimeStyles.None, out birthDate))
                {
                    DateTime deBirthDate = new DateTime(birthDate.Year - 543, birthDate.Month, birthDate.Day); // to D.E.
                    int yearDiff = new DateTime(DateTime.Today.Ticks - deBirthDate.Ticks).Year; // Roughtly estimation
                    if (yearDiff > 500) yearDiff -= 543;
                    model.Add(new PDFFieldValue() { FieldName = "Age", Value = yearDiff.ToString() });
                }

                if (req.Data.ContainsKey("CITIZEN_ADDRESS_INFORMATION"))
                {
                    var addrInfo = req.Data["CITIZEN_ADDRESS_INFORMATION"].Data;
                    model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo.TryGetString("ADDRESS_CITIZEN_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo.TryGetString("ADDRESS_MOO_CITIZEN_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo.TryGetString("ADDRESS_SOI_CITIZEN_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo.TryGetString("ADDRESS_BUILDING_CITIZEN_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Room", Value = addrInfo.TryGetString("ADDRESS_ROOMNO_CITIZEN_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo.TryGetString("ADDRESS_FLOOR_CITIZEN_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo.TryGetString("ADDRESS_ROAD_CITIZEN_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo.TryGetString("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT") });
                    model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo.TryGetString("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT") });
                    model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo.TryGetString("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT") });
                    model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_CITIZEN_ADDRESS") });

                    if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS", "")))
                        model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS", "");

                }

            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = generalInfo.TryGetString("COMPANY_NAME_TH", "");
                model.Add(field);

                // register address
                if (req.Data.ContainsKey("JURISTIC_ADDRESS_INFORMATION"))
                {
                    var addrInfo = req.Data["JURISTIC_ADDRESS_INFORMATION"].Data;
                    model.Add(new PDFFieldValue() { FieldName = "Address", Value = addrInfo.TryGetString("ADDRESS_JURISTIC_HQ_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Moo", Value = addrInfo.TryGetString("ADDRESS_MOO_JURISTIC_HQ_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Soi", Value = addrInfo.TryGetString("ADDRESS_SOI_JURISTIC_HQ_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Building", Value = addrInfo.TryGetString("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Room", Value = addrInfo.TryGetString("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Floor", Value = addrInfo.TryGetString("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Road", Value = addrInfo.TryGetString("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS") });
                    model.Add(new PDFFieldValue() { FieldName = "Province", Value = addrInfo.TryGetString("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT") });
                    model.Add(new PDFFieldValue() { FieldName = "Amphur", Value = addrInfo.TryGetString("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT") });
                    model.Add(new PDFFieldValue() { FieldName = "Tumbol", Value = addrInfo.TryGetString("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT") });
                    model.Add(new PDFFieldValue() { FieldName = "Telephone", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS") });

                    if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS", "")))
                        model.First(o => o.FieldName == "Telephone").Value += " ext. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS", "");

                }
            }
            #endregion

            #region Store information
            if (req.Data.ContainsKey("INFORMATION_STORE"))
            {
                var addrInfo = req.Data["INFORMATION_STORE"].Data;

                model.Add(new PDFFieldValue() { FieldName = "StoreName", Value = addrInfo.TryGetString("INFORMATION_STORE_NAME_TH") });

                model.Add(new PDFFieldValue() { FieldName = "SAddress", Value = addrInfo.TryGetString("ADDRESS_INFORMATION_STORE__ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "SMoo", Value = addrInfo.TryGetString("ADDRESS_MOO_INFORMATION_STORE__ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "SSoi", Value = addrInfo.TryGetString("ADDRESS_SOI_INFORMATION_STORE__ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "SBuilding", Value = addrInfo.TryGetString("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "SRoom", Value = addrInfo.TryGetString("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "SFloor", Value = addrInfo.TryGetString("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "SRoad", Value = addrInfo.TryGetString("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "SProvince", Value = addrInfo.TryGetString("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "SAmphur", Value = addrInfo.TryGetString("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "STumbol", Value = addrInfo.TryGetString("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "STelephone", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS") });

                if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", "")))
                    model.First(o => o.FieldName == "STelephone").Value += " ext. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", "");
            }
            #endregion

            if (req.Data.ContainsKey("APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL_INFO_SECTION"))
                model.Add(new PDFFieldValue() { FieldName = "Reason2", Value = req.Data["APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL_INFO_SECTION"].Data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL_INFO_SECTION_PURPOSE") });

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
    }
}
