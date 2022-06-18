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
using BizPortal.ViewModels.V2;
using BizPortal.ViewModels.Apps.DLTStandard;
using System.Configuration;
using Newtonsoft.Json;
using EGA.WS;
using RestSharp;
using System.Net;
using BizPortal.Utils;
using BizPortal.Integrated;

namespace BizPortal.AppsHook
{
    public class DLTNonRegularTruckNewAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 1500;
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = serverMapPathFunc("~/Uploads/apps/dlt/app39_new.pdf");
            PDFFieldValue field;
            List<PDFFieldValue> model = new List<PDFFieldValue>();

            string content = Newtonsoft.Json.JsonConvert.SerializeObject(req);

            model.Add(new PDFFieldValue() { FieldName = "Date", Value = req.CreatedDate.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th-TH")) });

            var generalInfo = req.Data["GENERAL_INFORMATION"].Data;

            #region Register name and Address
            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = string.Format("{0}{1} {2}", generalInfo.TryGetString("DROPDOWN_CITIZEN_TITLE_TEXT", ""), generalInfo.TryGetString("CITIZEN_NAME", ""), generalInfo.TryGetString("CITIZEN_LASTNAME", ""));
                model.Add(field);

                if (req.Data.ContainsKey("CITIZEN_ADDRESS_INFORMATION"))
                {
                    var addrInfo = req.Data["CITIZEN_ADDRESS_INFORMATION"].Data;
                    string addr = GetSingleLineCitizenAddress(addrInfo);
                    model.Add(new PDFFieldValue() { FieldName = "Address2", Value = addr });
                }

                field = new PDFFieldValue() { FieldName = "RegisterName" };
                field.Value = string.Format("{0}{1} {2}", generalInfo.TryGetString("DROPDOWN_CITIZEN_TITLE_TEXT", ""), generalInfo.TryGetString("CITIZEN_NAME", ""), generalInfo.TryGetString("CITIZEN_LASTNAME", ""));
                model.Add(field);

                if (req.Data.ContainsKey("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION"))
                {
                    model.Add(new PDFFieldValue() { FieldName = "RegisterNationality", Value = req.Data["APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION"].Data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_NATIONALITY", "") });
                }


                // register address
                if (req.Data.ContainsKey("INFORMATION_STORE"))
                {
                    var addrInfo = req.Data["INFORMATION_STORE"].Data;
                    string addr = GetSingleLineStoreAddress(addrInfo);
                    model.Add(new PDFFieldValue() { FieldName = "HeadOffice", Value = addr });

                    model.Add(new PDFFieldValue() { FieldName = "HeadOfficeTel", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS", "") });
                    if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", "")))
                        model.First(o => o.FieldName == "HeadOfficeTel").Value += " ext. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", "");
                }
            }
            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                field = new PDFFieldValue() { FieldName = "IdentityName" };
                field.Value = generalInfo.TryGetString("COMPANY_NAME_TH", "");
                model.Add(field);

                field = new PDFFieldValue() { FieldName = "RegisterName" };
                field.Value = generalInfo.TryGetString("COMPANY_NAME_TH", "");
                model.Add(field);

                // register address
                if (req.Data.ContainsKey("JURISTIC_ADDRESS_INFORMATION"))
                {
                    var addrInfo = req.Data["JURISTIC_ADDRESS_INFORMATION"].Data;
                    string addr = GetSingleLineJuristicAddress(addrInfo);

                    model.Add(new PDFFieldValue() { FieldName = "Address2", Value = addr });
                    model.Add(new PDFFieldValue() { FieldName = "HeadOffice", Value = addr });

                    model.Add(new PDFFieldValue() { FieldName = "HeadOfficeTel", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS", "") });
                    if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS", "")))
                        model.First(o => o.FieldName == "HeadOfficeTel").Value += " ext. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS", "");
                }
            }
            #endregion

            // ไม่ขนส่งผู้โดยสาร
            model.Add(new PDFFieldValue() { FieldName = "OptHasPassenger", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            if (req.Data.ContainsKey("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION"))
            {
                string provinceList = "";
                if (req.Data["APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION"].Data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_TRANSPORT_IN_OPTION") == "KINGDOM")
                {
                    provinceList = "ทั่วราชอาณาจักร";
                }
                else if (req.Data.ContainsKey("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION"))
                {
                    var provinceData = req.Data["APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION"].Data;
                    int total = int.Parse(provinceData.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION_TOTAL", "0"));
                    for (int i = 0; i < total; i++)
                    {
                        if (provinceList != "") provinceList += ", ";
                        provinceList += provinceData.TryGetString("AJAX_DROPDOWN_APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION_PROVINCE_TEXT_" + i);
                    }
                }

                field = new PDFFieldValue() { FieldName = "ProvinceList" };
                field.Value = provinceList;
                model.Add(field);
            }

            if (req.Data.ContainsKey("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2"))
            {
                var data = req.Data["APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2"].Data;

                model.Add(new PDFFieldValue() { FieldName = "TotalTruck", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_CAR_TOTAL_AMOUNT") });
                model.Add(new PDFFieldValue() { FieldName = "TotalType1", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_1") });
                model.Add(new PDFFieldValue() { FieldName = "TotalType2", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_2") });
                model.Add(new PDFFieldValue() { FieldName = "TotalType3", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_3") });
                model.Add(new PDFFieldValue() { FieldName = "TotalType4", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_4") });
                model.Add(new PDFFieldValue() { FieldName = "TotalType5", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_5") });
                model.Add(new PDFFieldValue() { FieldName = "TotalType6", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_6") });
                model.Add(new PDFFieldValue() { FieldName = "TotalType7", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_7") });
                model.Add(new PDFFieldValue() { FieldName = "TotalType8", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_8") });
                model.Add(new PDFFieldValue() { FieldName = "TotalType9", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_9") });

                model.Add(new PDFFieldValue() { FieldName = "TotalDriver", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_DRIVER_TOTAL_AMOUNT") });

                model.Add(new PDFFieldValue() { FieldName = "AcquireWithIn", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_WITHIN_TIME") });
            }

            if (req.Data.ContainsKey("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANFER_PLACE_SECTION"))
            {
                var data = req.Data["APP_TRANSPORT_NON_REGULAR_TRUCK_TRANFER_PLACE_SECTION"].Data;

                // ไม่ขนส่งผู้โดยสาร
                model.Add(new PDFFieldValue() { FieldName = "OptTransferPassenger", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

                // ไม่ขนส่งสัตว์
                if (!(data.TryGetBool("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANFER_PLACE_SECTION_TRANFER_TRANFER_ANIMAL")))
                    model.Add(new PDFFieldValue() { FieldName = "OptTransferAnimal", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

                // ไม่ขนส่งสิ่งของ
                if (!(data.TryGetBool("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANFER_PLACE_SECTION_TRANFER_TRANFER_OTHER")))
                    model.Add(new PDFFieldValue() { FieldName = "OptTransferObject", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (req.Data.ContainsKey("APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION"))
            {
                var data = req.Data["APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION"].Data;
                int placeTotal = int.Parse(data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_TOTAL", "0"));

                if (placeTotal > 0)
                {
                    string addr = data.TryGetString("ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0");

                    if (data.TryGetString("ADDRESS_MOO_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0") != "") addr += string.Format(" หมู่ {0} ", data.TryGetString("ADDRESS_MOO_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0"));
                    //if (data.TryGetString("ADDRESS_SOI_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0") != "") addr += string.Format(" ซอย{0} ", data.TryGetString("ADDRESS_SOI_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0"));
                    if (data.TryGetString("ADDRESS_BUILDING_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0") != "") addr += string.Format(" อาคาร{0} ", data.TryGetString("ADDRESS_BUILDING_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0"));
                    if (data.TryGetString("ADDRESS_ROOMNO_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0") != "") addr += string.Format(" ห้อง {0} ", data.TryGetString("ADDRESS_ROOMNO_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0"));
                    if (data.TryGetString("ADDRESS_FLOOR_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0") != "") addr += string.Format(" ชั้น {0} ", data.TryGetString("ADDRESS_FLOOR_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0"));
                    if (data.TryGetString("ADDRESS_ROAD_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0") != "") addr += string.Format(" ถนน{0} ", data.TryGetString("ADDRESS_ROAD_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0"));
                    if (data.TryGetString("ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_TEXT_0") != "") addr += string.Format(" ตำบล{0} ", data.TryGetString("ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_TEXT_0"));
                    if (data.TryGetString("ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_TEXT_0") != "") addr += string.Format(" อำเภอ{0} ", data.TryGetString("ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_TEXT_0"));
                    if (data.TryGetString("ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_TEXT_0") != "") addr += string.Format("  {0} ", data.TryGetString("ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_TEXT_0"));
                    if (data.TryGetString("ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0") != "") addr += string.Format(" {0} ", data.TryGetString("ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION_ADDRESS_0"));

                    if (placeTotal > 1) addr += " และที่อื่นๆ";

                    model.Add(new PDFFieldValue() { FieldName = "TransferPlace", Value = addr });
                }
            }

            if (req.Data.ContainsKey("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION"))
            {
                var data = req.Data["APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION"].Data;
                int total = int.Parse(data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_TOTAL", "0"));

                if (total > 0)
                {
                    string addr = data.TryGetString("ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_0");

                    if (data.TryGetString("ADDRESS_MOO_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_0") != "") addr += string.Format(" หมู่ {0} ", data.TryGetString("ADDRESS_MOO_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_0"));
                    if (data.TryGetString("ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_0") != "") addr += string.Format(" {0} ", data.TryGetString("ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_0"));
                    if (data.TryGetString("ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_0") != "") addr += string.Format(" {0} ", data.TryGetString("ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_0"));
                    if (data.TryGetString("ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_0") != "") addr += string.Format("  {0} ", data.TryGetString("ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_0"));

                    model.Add(new PDFFieldValue() { FieldName = "Garage1", Value = addr });

                    if (total > 1)
                    {
                        addr = data.TryGetString("ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_1");

                        if (data.TryGetString("ADDRESS_MOO_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_1") != "") addr += string.Format(" หมู่ {0} ", data.TryGetString("ADDRESS_MOO_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_1"));
                        if (data.TryGetString("ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_1") != "") addr += string.Format(" {0} ", data.TryGetString("ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_1"));
                        if (data.TryGetString("ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_1") != "") addr += string.Format(" {0} ", data.TryGetString("ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_1"));
                        if (data.TryGetString("ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_1") != "") addr += string.Format("  {0} ", data.TryGetString("ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_1"));

                        if (!string.IsNullOrEmpty(addr)) model.Add(new PDFFieldValue() { FieldName = "Garage2", Value = "และ " + addr });
                    }
                }
            }

            if (req.Data.ContainsKey("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2"))
            {
                var data = req.Data["APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2"].Data;

                model.Add(new PDFFieldValue() { FieldName = "GarageCapacity", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_CAR_AMOUNT") });
                model.Add(new PDFFieldValue() { FieldName = "TotalCurrentTruck", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_CAR_CURRENT") });

                if (data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_READY_OPTION") == "WITHIN")
                {
                    model.Add(new PDFFieldValue() { FieldName = "StartDate", Value = data.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_DATETIME") });
                }
                else
                {
                    model.Add(new PDFFieldValue() { FieldName = "StartDate", Value = "วันที่ได้รับใบอนุญาต" });
                }
            }

            // ข้อมูลรถในกิจการ
            if (req.Data.ContainsKey("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION"))
            {
                var truckList = req.Data["APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION"].Data;
                int total = int.Parse(truckList.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_TOTAL", "0"));
                for (int i = 0; i < total; i++)
                {
                    model.Add(new PDFFieldValue() { FieldName = string.Format("CarList{0}1", i + 1), Value = truckList.TryGetString("ARR_IDX_" + i) });

                    if (truckList.TryGetString("DROPDOWN_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_REQUEST_TYPE_" + i, "") == "HAVE_CAR")
                    {
                        model.Add(new PDFFieldValue() { FieldName = string.Format("CarList{0}2", i + 1), Value = truckList.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_LICENSE_" + i) });
                        model.Add(new PDFFieldValue() { FieldName = string.Format("CarList{0}3", i + 1), Value = truckList.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_CASSIS_" + i) });
                        model.Add(new PDFFieldValue() { FieldName = string.Format("CarList{0}4", i + 1), Value = truckList.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_ENGINE_" + i) });
                        model.Add(new PDFFieldValue() { FontSize = 12, FieldName = string.Format("CarList{0}5", i + 1), Value = truckList.TryGetString("DROPDOWN_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_BRAND_TEXT_" + i) });
                    }
                    else
                    {
                        model.Add(new PDFFieldValue() { FieldName = string.Format("CarList{0}2", i + 1), Value = "อยู่ในหลักการ\n(" + truckList.TryGetString("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_AMOUNT_" + i) + " คัน)" });
                    }

                    model.Add(new PDFFieldValue() { FieldName = string.Format("CarList{0}6", i + 1), Value = truckList.TryGetString("DROPDOWN_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_TYPE_TEXT_" + i) });
                }
            }

            PDFConfig cfg = new PDFConfig() { FontName = "Angsima.ttf", FontSize = 14 };
            var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model, cfg);

            return bytes;
        }

        private static string GetSingleLineCitizenAddress(Dictionary<string, string> data)
        {
            string addr = data.TryGetString("ADDRESS_CITIZEN_ADDRESS");

            if (data.TryGetString("ADDRESS_MOO_CITIZEN_ADDRESS") != "") addr += string.Format(" หมู่ {0} ", data.TryGetString("ADDRESS_MOO_CITIZEN_ADDRESS"));
            //if (data.TryGetString("ADDRESS_SOI_CITIZEN_ADDRESS") != "") addr += string.Format(" ซอย{0} ", data.TryGetString("ADDRESS_SOI_CITIZEN_ADDRESS"));
            if (data.TryGetString("ADDRESS_BUILDING_CITIZEN_ADDRESS") != "") addr += string.Format(" อาคาร{0} ", data.TryGetString("ADDRESS_BUILDING_CITIZEN_ADDRESS"));
            if (data.TryGetString("ADDRESS_ROOMNO_CITIZEN_ADDRESS") != "") addr += string.Format(" ห้อง {0} ", data.TryGetString("ADDRESS_ROOMNO_CITIZEN_ADDRESS"));
            if (data.TryGetString("ADDRESS_FLOOR_CITIZEN_ADDRESS") != "") addr += string.Format(" ชั้น {0} ", data.TryGetString("ADDRESS_FLOOR_CITIZEN_ADDRESS"));
            if (data.TryGetString("ADDRESS_ROAD_CITIZEN_ADDRESS") != "") addr += string.Format(" ถนน{0} ", data.TryGetString("ADDRESS_ROAD_CITIZEN_ADDRESS"));
            if (data.TryGetString("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT") != "") addr += string.Format(" ตำบล{0} ", data.TryGetString("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT"));
            if (data.TryGetString("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT") != "") addr += string.Format(" อำเภอ{0} ", data.TryGetString("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT"));
            if (data.TryGetString("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT") != "") addr += string.Format("  {0} ", data.TryGetString("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT"));
            if (data.TryGetString("ADDRESS_POSTCODE_CITIZEN_ADDRESS") != "") addr += string.Format(" {0} ", data.TryGetString("ADDRESS_POSTCODE_CITIZEN_ADDRESS"));

            return addr;
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

            #region API-API
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;

            try
            {
                switch (stage)
                {
                    case AppsStage.UserCreate:
                        {
                            var post = new BusinessData();
                            var appList = new List<Application>();
                            var app = new Application()
                            {
                                bizAppId = model.ApplicationID.ToString(),
                                bizReqNo = model.ApplicationRequestID.ToString(),
                                bizReqDateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", new CultureInfo("en")),
                                reqNo = model.ApplicationRequestNumber,
                                company_name_th = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH"),
                                company_name_en = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_EN"),
                                company_type = "70",
                                operator_type = DLTUtility.GetOwnerType().FirstOrDefault(x => x.Value == model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("INFORMATION__REQUEST_AS_OPTION")).Key.ToString(),
                                is_yourself = DLTUtility.GetRequrestType().FirstOrDefault(x => x.Value == model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION")).Key.ToString(),
                                //is_same_address = model.Data.TryGetData("INFORMATION_STORE").ThenGetIntData("INFORMATION_STORE__USE_CITIZEN_ADDRESS_INFORMATION_STORE__USE_CITIZEN_ADDRESS__TRUE").ToString(),
                                email = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL"),
                                to_all_provinces = DLTUtility.GetTransportIn().FirstOrDefault(x => x.Value == model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_TRANSPORT_IN_OPTION")).Key.ToString(),
                                has_stop_point = DLTUtility.GetTransportPlace().FirstOrDefault(x => x.Value == model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANFER_PLACE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANFER_PLACE_SECTION_HAVE_TRANFER_OPTION")).Key.ToString(),
                                transfer_animal = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANFER_PLACE_SECTION").ThenGetIntData("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANFER_PLACE_SECTION_TRANFER_TRANFER_ANIMAL").ToString(),
                                transfer_goods = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANFER_PLACE_SECTION").ThenGetIntData("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANFER_PLACE_SECTION_TRANFER_TRANFER_OTHER").ToString(),
                                can_operate_on_approval = DLTUtility.GetReady().FirstOrDefault(x => x.Value == model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_READY_OPTION")).Key.ToString(),
                                days_to_fill_fleet = DLTUtility.GetReady().FirstOrDefault(x => x.Value == model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_READY_OPTION")).Key == 0?model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_WITHIN_TIME"): "0",
                                total_trucks_by_regulation = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_CAR_CURRENT"),
                                reason = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REASON"),
                                note = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_NOTE"),
                                num_truck1 = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_1"),
                                num_truck2 = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_2"),
                                num_truck3 = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_3"),
                                num_truck4 = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_4"),
                                num_truck5 = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_5"),
                                num_truck6 = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_6"),
                                num_truck7 = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_7"),
                                num_truck8 = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_8"),
                                num_truck9 = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_9"),
                                // cnt_car10 = "0",
                                total_trucks = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_CAR_TOTAL_AMOUNT"),
                                total_drivers = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_DRIVER_TOTAL_AMOUNT"),
                                storage_area = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_AREA_METER"),
                                // num_trucks_stored = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_CAR_AMOUNT"),
                                address_landno_1 = "",//model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS"),
                                address_no_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS"),
                                address_moo_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS"),
                                address_village_1 = "",//model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS"),
                                address_lane_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS"),
                                address_building_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"),
                                address_room_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS"),
                                address_floor_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS"),
                                address_road_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"),
                                tumbol_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"),
                                amphoe_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"),
                                province_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"),
                                postal_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"),
                                phone_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOBILE_INFORMATION_STORE__ADDRESS"),
                                tel_no_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                                tel_no_ext_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"),
                                fax_no_1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS"),
                                lat = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_LAT_INFORMATION_STORE__ADDRESS"),
                                lon = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_LNG_INFORMATION_STORE__ADDRESS"),
                            };

                            if (app.can_operate_on_approval == DLTUtility.GetReady().FirstOrDefault(x => x.Value == "WITHIN").Key.ToString())
                            {
                                app.operation_date = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2").ThenGetDateStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_DATETIME");
                            }
                            if (app.operator_type == DLTUtility.GetOwnerType().FirstOrDefault(x => x.Value == "บุคคลธรรมดา").Key.ToString())
                            {
                                app.title_name_th = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_TITLE_TEXT");
                                app.name_th = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_NAME");
                                app.surname_th = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME");
                                app.title_name_en = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_TITLE_EN_TEXT");
                                app.name_en = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_FIRSTNAME_EN");
                                app.surname_en = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME_EN");
                                app.birthdate = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetDateStringData("BIRTH_DATE");
                                app.nationality = "ไทย"; //NationalityService.GetNationalityText(model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY")),
                                app.citizen_no = model.IdentityID.ToString();
                                app.address_no = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_CITIZEN_ADDRESS");
                                app.address_moo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS");
                                app.address_lane = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS");
                                app.address_building = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_CITIZEN_ADDRESS");
                                app.address_room = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROOMNO_CITIZEN_ADDRESS");
                                app.address_floor = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_CITIZEN_ADDRESS");
                                app.address_road = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS");
                                app.tel_no = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS");
                                app.tel_no_ext = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS");
                                app.fax_no = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_CITIZEN_ADDRESS");
                                app.tumbol = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT");
                                app.amphoe = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT");
                                app.province = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT");
                                app.postal = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS");
                            }
                            else
                            {
                                app.org_name_th = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_TH");
                                app.org_name_en = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_EN");
                                app.org_type = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_INFORMATION__JURISTIC_TYPE");
                                app.org_no = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("IDENTITY_ID");
                                app.org_regis_date = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetDateStringData("REGISTER_DATE");
                                app.org_email = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL");
                                app.address_no = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS");
                                app.address_moo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS");
                                app.address_lane = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS");
                                app.address_building = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS");
                                app.address_room = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS");
                                app.address_floor = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS");
                                app.address_road = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS");
                                app.tel_no = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS");
                                app.tel_no_ext = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS");
                                app.fax_no = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_JURISTIC_HQ_ADDRESS");
                                app.tumbol = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT");
                                app.amphoe = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT");
                                app.province = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT");
                                app.postal = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS");
                                app.regis_capital = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REGIS_CAPITAL");
                                app.regis_date = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION").ThenGetDateStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REGIS_DATE");
                            }
                            if (app.province_1 == "กรุงเทพมหานคร")
                            {
                                app.amphoe_1 = app.amphoe_1.Replace("เขต", "");
                            }
                            if (app.province== "กรุงเทพมหานคร")
                            {
                                app.amphoe = app.amphoe.Replace("เขต", "");
                            }
                            var tranList = new List<Tran>();
                            var tranTotal = int.Parse(model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION_TOTAL"));
                            if (tranTotal > 0)
                            {
                                for (int i = 0; i < tranTotal; i++)
                                {
                                    var tran = new Tran()
                                    {
                                        province_t = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION_PROVINCE_TEXT_" + i),
                                    };
                                    tranList.Add(tran);
                                }
                            }
                            app.trans = tranList.ToList();
                            var truckList = new List<Truck>();
                            var truckTotal = int.Parse(model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_TOTAL"));
                            if (truckTotal > 0)
                            {
                                for (int i = 0; i < truckTotal; i++)
                                {
                                    var chk_license_no = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_LICENSE_" + i);
                                    if (chk_license_no != "")
                                    {
                                        var truck = new Truck()
                                        {
                                            license_no = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_LICENSE_" + i),
                                            register_pro_th = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_PROVINCE_TEXT_" + i),
                                            chassis_no = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_CASSIS_" + i),
                                            engine_no = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_ENGINE_" + i),
                                            truck_type = DLTUtility.GetCarType().FirstOrDefault(x => x.Value == model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION").ThenGetStringData("DROPDOWN_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_TYPE_" + i)).Key.ToString(),
                                           // truck_type_sub = "",//model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_LICENSE_" + i),
                                            truck_category = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION").ThenGetStringData("DROPDOWN_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_BRAND_TEXT_" + i),
                                        };
                                        truckList.Add(truck);
                                    }
                                }
                            }
                            app.truck = truckList.ToList();
                            var placeList = new List<Place>();
                            var placeTotal = int.Parse(model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_TOTAL"));
                            if (placeTotal > 0)
                            {
                                for (int i = 0; i < placeTotal; i++)
                                {
                                    var place = new Place()
                                    {
                                        land_id = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_LAND_ID_" + i),
                                        land_no = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_LAND_NO_" + i),
                                        land_code = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_LAND_CODE_" + i),
                                        address_no = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        address_moo = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_MOO_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        address_village = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_VILLAGE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        address_lane = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_SOI_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        address_building = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        address_room = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        address_floor = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_FLOOR_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        address_road = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        tumbol = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_" + i),
                                        amphoe = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_" + i),
                                        province = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_TEXT_" + i),
                                        postal = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        tel_no = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        tel_no_ext = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        fax_no = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_FAX_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        email = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_EMAIL_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS" + i),
                                        lat = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_LAT_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        lon = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_LNG_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        address_type = DLTUtility.GetAddressType().FirstOrDefault(x => x.Value == model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN_OPTION_" + i)).Key.ToString(),
                                        phone =  model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_MOBILE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i),
                                        area = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA_" + i),
                                        //num_trucks_stored = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_CNT_TRUCK_STORE_" + i),
                                        address_owner = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_OWNER_" + i),
                                        address_area_doc = "",//model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_LNG_INFORMATION_STORE__ADDRESS"),
                                        address_area_unit = "",// model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_LNG_INFORMATION_STORE__ADDRESS"),
                                        rent_type = DLTUtility.GetOwnerType().FirstOrDefault(x => x.Value == model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE_OPTION__RADIO_TEXT_" + i)).Key.ToString(),
                                        rai = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA_RAI_" + i),
                                        nkan = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA_NKAN_" + i),
                                        tarankwa = model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA_TARANKWA_" + i),
                                    };
                                    if (model.Data.TryGetData("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_" + i) == "10")
                                    {
                                        place.amphoe = place.amphoe.Replace("เขต", "");
                                    }
                                    placeList.Add(place);
                                }
                            }
                            app.place = placeList.ToList();
                            // Model data
                            appList.Add(app);
                            post.application = appList.ToList();

                            //string BaseUrl = ConfigurationManager.AppSettings["DLT_REGIS_WS_URL"];//ConfigurationManager.AppSettings[""];
                            var jsonPost = JsonConvert.SerializeObject(post,
                                Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                }); // Serialize model data to JSON
              

                            string regis = ConfigurationManager.AppSettings["DLT_REGIS_WS_URL"];


                            var client = new RestClient(regis);
                            var request_ = new RestRequest(Method.POST);

                            request_.AddHeader("content-type", "multipart/form-data");
                            request_.AddParameter("application", jsonPost);
                            IRestResponse response = client.Execute(request_);

                            //IRestResponse resp = client.Execute(request_);
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                DLTInfo resp = JsonConvert.DeserializeObject<DLTInfo>(response.Content);
                                if (resp.status == "0")
                                {
                                    var postFile = new BusinessDataFile();
                                    var appListFile = new List<ApplicationFile>();

                                    var appfile = new ApplicationFile();
                                    appfile.app_token = resp.app_token;
                                    var attachList = new List<File>();
                                    //int file_index = 1;
                                    foreach (FileGroup group in model.UploadedFiles)
                                    {
                                        foreach (var item in group.Files)
                                        {
                                            //var description = item.Extras.ContainsKey("FILETYPENAME") ? item.Extras["FILETYPENAME"].ToString() : string.Empty;

                                            string fileType = DLTUtility.GetFileTypeDLTRef().FirstOrDefault(x => item.FileTypeCode.Contains(x.Key)).Value;
                                            var attach = new File()
                                            {
                                                file_name64 = item.GetBased64String(),
                                                file_type = item.ContentType,
                                                //description = description,
                                                file_name = item.FileName,
                                                file_topic = fileType

                                            };
                                            attachList.Add(attach);
                                            // break;
                                        }

                                    }
                                    appfile.file = attachList.ToList();
                                    appListFile.Add(appfile);
                                    postFile.application_file = appListFile.ToList();
                                    var jsonPostFile = JsonConvert.SerializeObject(postFile,
                               Newtonsoft.Json.Formatting.None,
                               new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore
                               });
                                    string regisFile = ConfigurationManager.AppSettings["DLT_REGIS_FILE_WS_URL"];

                                    var clientfile = new RestClient(regisFile);
                                    var requestfile_ = new RestRequest(Method.POST);
                                 
                                    requestfile_.AddHeader("content-type", "multipart/form-data");
                                    requestfile_.AddParameter("application_file", jsonPostFile);
                                    IRestResponse respfile = clientfile.Execute(requestfile_);
                                    if (respfile.StatusCode == HttpStatusCode.OK)

                                    {
                                        DLTInfo respfile1 = JsonConvert.DeserializeObject<DLTInfo>(respfile.Content);

                                        if (respfile1.status == "0")
                                        {
                                            Dictionary<string, string> respData = new Dictionary<string, string>()
                                            {
                                                { "app_token", resp.app_token.ToString() }
                                            };

                                            if (request.Data.ContainsKey("DLT_RESPONSE_DATA"))
                                            {
                                                request.Data.Remove("DLT_RESPONSE_DATA");
                                            }
                                            request.Data.Add("DLT_RESPONSE_DATA", new ApplicationRequestDataGroupEntity()
                                            {
                                                GroupName = "DLT_RESPONSE_DATA",
                                                Data = respData
                                            });
                                            result.Success = true;
                                            result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, "", result.Success.ToString(), jsonPost);
                                        }
                                        else
                                        {
                                            string error = "เกิดข้อผิดพลาด" + respfile1.error + ".";
                                            result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                            throw new Exception(error);
                                        }
                                    }
                                }
                                else
                                {
                                    string error = "เกิดข้อผิดพลาด" + resp.error + ".";
                                    result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                    throw new Exception(error);
                                }
                            }
                            else
                            {
                                string error = "Unable to request to " + regis + ".";
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                throw new Exception(error);
                            }
                        }
                        break;

                    case AppsStage.UserUpdate:
                        {
                            if (model.Status == ApplicationStatusV2Enum.CHECK || model.Status == ApplicationStatusV2Enum.PENDING)
                            {
                                var postFile = new BusinessDataFile();
                                var appListFile = new List<ApplicationFile>();

                                var appfile = new ApplicationFile();
                                appfile.app_token = model.Data.TryGetData("DLT_RESPONSE_DATA").ThenGetStringData("app_token");
                                appfile.message = model.Remark;
                                var attachList = new List<File>();
                                #region [Attachs]
                              
                                // int file_index = 1;
                                var requestedFiles = model.UploadedFiles.Where(o => o.Description == "REQUESTED_FILE").OrderByDescending(o => o.CreatedDate).FirstOrDefault();
                                if (requestedFiles != null && requestedFiles.Files != null && requestedFiles.Files.Count > 0)
                                {
                                    foreach (var item in requestedFiles.Files)
                                    {

                                        //// var seqNo = item.Extras.ContainsKey("SEQNO") ? item.Extras["SEQNO"].ToString() : string.Empty;
                                        //var fileTypeDesc = item.Extras.ContainsKey("FILETYPEDESC") ? item.Extras["FILETYPEDESC"].ToString() : string.Empty;
                                        var fileType = item.Extras.ContainsKey("FILETYPE") ? item.Extras["FILETYPE"].ToString() : "13";
                                 
                                        var attach = new File()
                                        {
                                            file_name64 = item.GetBased64String(),
                                            file_type = item.ContentType,
                                            //description = description,
                                            file_name = item.FileName,
                                            file_topic = fileType

                                        };
                                        attachList.Add(attach);
                                        // break;
                                    }
                                }
                                appfile.file = attachList.ToList();
                                appListFile.Add(appfile);
                                postFile.application_file = appListFile.ToList();
                                var jsonPostFile = JsonConvert.SerializeObject(postFile,
                                   Newtonsoft.Json.Formatting.None,
                                   new JsonSerializerSettings
                                   {
                                       NullValueHandling = NullValueHandling.Ignore
                                   });
                                string regisFile = ConfigurationManager.AppSettings["DLT_REGIS_FILE_WS_URL"];

                                var clientfile = new RestClient(regisFile);
                                var requestfile_ = new RestRequest(Method.POST);
                                requestfile_.AddHeader("content-type", "multipart/form-data");
                                requestfile_.AddParameter("application_file", jsonPostFile);
                                IRestResponse respfile = clientfile.Execute(requestfile_);
                                if (respfile.StatusCode == HttpStatusCode.OK)
                                {
                                    DLTInfo respfile1 = JsonConvert.DeserializeObject<DLTInfo>(respfile.Content);

                                    if (respfile1.status == "0")
                                    {
                                        result.Success = true;
                                    }
                                    else
                                    {
                                        string error = "เกิดข้อผิดพลาด" + respfile1.error + ".";
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPostFile, true);
                                        throw new Exception(error);
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                result.Success = true;
                            }
                        }
                        break;
                    case AppsStage.ApiUpdate:
                    case AppsStage.None:
                    case AppsStage.AgentUpdate:
               
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

            return result;
#endregion
        }

    }

    public class DLTInfo
    {
        public string status { get; set; }
        public string error { get; set; }
        public string app_token
        {
            get; set;
        }
    }
}
