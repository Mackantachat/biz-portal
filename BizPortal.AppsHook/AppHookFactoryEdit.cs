using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;
using iTextSharp.text.pdf;
using System.IO;
using BizPortal.Utils.Helpers;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;

namespace BizPortal.AppsHook
{
    class AppHookFactoryEdit : StoreBaseAppHook
    {
        public override bool HasOrgPdfForm
        {
            get
            {
                return true;
            }
        }

        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return null;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = serverMapPathFunc("~/Uploads/apps/factory/Fac_Edit.pdf");
            List<PDFFieldValue> model = new List<PDFFieldValue>();

            var generalInfo = req.Data["GENERAL_INFORMATION"].Data;
            string strDate = generalInfo["INFORMATION_HEADER__REQUEST_DATE"].ToString();
            string[] strDateArr = strDate.Split('/');
            string dd = strDateArr[0];
            string mm = strDateArr[1];
            string strYY = strDateArr[2];
            string[] yyArr = strYY.Split(' ');
            string yy = yyArr[0];
            model.Add(new PDFFieldValue() { FieldName = "txtDay", Value = dd }); ;
            model.Add(new PDFFieldValue() { FieldName = "txtYear", Value = yy }); ;
            model.Add(new PDFFieldValue() { FieldName = "txtMonth", Value = CastToMonth(mm) });

            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                model.Add(new PDFFieldValue() { FieldName = "txtName", Value = string.Format("{0} {1} {2}", generalInfo.ThenGetStringData("DROPDOWN_CITIZEN_TITLE_TEXT"), generalInfo.ThenGetStringData("CITIZEN_NAME"), generalInfo.ThenGetStringData("CITIZEN_LASTNAME")) });
                model.Add(new PDFFieldValue() { FieldName = "txtAge", Value = generalInfo.ThenGetStringData("GENERAL_INFORMATION__CITIZEN_AGE") });
                model.Add(new PDFFieldValue() { FieldName = "txtNational", Value = generalInfo.ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "txtOfficePlace", Value = "-----------------------" });

                var citiInfo = req.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION");

                model.Add(new PDFFieldValue() { FieldName = "txtNbr",       Value = citiInfo.ThenGetStringData("ADDRESS_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "txtMoo",           Value = citiInfo.ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "txtSOI",         Value = citiInfo.ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "txtStreet",        Value = citiInfo.ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "txtSubDistrict",   Value = citiInfo.ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "txtDistrict",      Value = citiInfo.ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "txtProvince",      Value = citiInfo.ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "txtTell",          Value = citiInfo.ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS") });
                if (!string.IsNullOrEmpty(citiInfo.ThenGetStringData("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS")))
                    model.First(o => o.FieldName == "txtTell").Value += " ต่อ " + citiInfo.ThenGetStringData("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS");
                model.Add(new PDFFieldValue() { FieldName = "txtCanel", Value = citiInfo.ThenGetStringData("CITIZEN_CANEL") });
                model.Add(new PDFFieldValue() { FieldName = "txtRiver", Value = citiInfo.ThenGetStringData("CITIZEN_RIVER") });
            }
            else
            {
                var addrInfo = req.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION");
                model.Add(new PDFFieldValue() { FieldName = "txtNbr", Value = addrInfo.ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "txtMoo", Value = addrInfo.ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "txtSOI", Value = addrInfo.ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "txtStreet", Value = addrInfo.ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS") });
                model.Add(new PDFFieldValue() { FieldName = "txtSubDistrict", Value = addrInfo.ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "txtDistrict", Value = addrInfo.ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "txtProvince", Value = addrInfo.ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT") });
                model.Add(new PDFFieldValue() { FieldName = "txtTell", Value = addrInfo.ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS") });
                if (!string.IsNullOrEmpty(addrInfo.ThenGetStringData("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS")))
                    model.First(o => o.FieldName == "txtTell").Value += " ต่อ " + addrInfo.ThenGetStringData("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS");
                model.Add(new PDFFieldValue() { FieldName = "txtName", Value = generalInfo.ThenGetStringData("COMPANY_NAME_TH") });
                model.Add(new PDFFieldValue() { FieldName = "txtAge", Value = "-" });
                model.Add(new PDFFieldValue() { FieldName = "txtRace", Value = "-" });
                model.Add(new PDFFieldValue() { FieldName = "txtPlace", Value = "---------" });
                model.Add(new PDFFieldValue() { FieldName = "txtCanel", Value = addrInfo.ThenGetStringData("JURISTIC_CANEL") });
                model.Add(new PDFFieldValue() { FieldName = "txtRiver", Value = addrInfo.ThenGetStringData("JURISTIC_RIVER") });
            }

            // ข้อมูลเฉพาะ
            var plantInfo = req.Data["APP_FACTORY_CLASS_2_SECTION_INFO_EDIT"].Data;
            bool a = Convert.ToBoolean(plantInfo["APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_CHECKBOX"]);
            bool b = Convert.ToBoolean(plantInfo["APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX"]);
            bool c = Convert.ToBoolean(plantInfo["APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX"]);
            bool d = Convert.ToBoolean(plantInfo["APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_DETAIL"]);
            string str = string.Empty;
            string strDetail = string.Empty;
            if (a)
            {
                str = str + "เปลี่ยนแปลงชื่อ-นามสกุลผู้ประกอบกิจการโรงงาน,";
                var valOpt1 = req.Data["APP_FACTORY_CLASS_2_OPT_1"].Data;
                strDetail = strDetail + "เปลี่ยนแปลงชื่อ-นามสกุลผู้ประกอบกิจการโรงงานเป็น : " + valOpt1["APP_FACTORY_2_LOCATION_CHANGING"].ToString() + ",";
            }

            if (b)
            {
                str = str + "เปลี่ยนแปลงที่ตั้งสำนักงานใหญ่,";
                var valOpt2 = req.Data["APP_FACTORY_CLASS_2_OPT_2"].Data;

                var nbr = Convert.ToString(valOpt2["ADDRESS_APP_FACTORY_CLASS_2_ADDRESS_2"]); // เลขที่
                var moo = Convert.ToString(valOpt2["ADDRESS_MOO_APP_FACTORY_CLASS_2_ADDRESS_2"]); // หมู่ที่
                var village = Convert.ToString(valOpt2["ADDRESS_VILLAGE_APP_FACTORY_CLASS_2_ADDRESS_2"]); // หมู่บ้าน
                var soi = Convert.ToString(valOpt2["ADDRESS_SOI_APP_FACTORY_CLASS_2_ADDRESS_2"]); // ตรอก/ซอย
                var building = Convert.ToString(valOpt2["ADDRESS_BUILDING_APP_FACTORY_CLASS_2_ADDRESS_2"]); // อาคาร
                var roomNbr = Convert.ToString(valOpt2["ADDRESS_ROOMNO_APP_FACTORY_CLASS_2_ADDRESS_2"]); // ห้องเลขที่
                var floor = Convert.ToString(valOpt2["ADDRESS_FLOOR_APP_FACTORY_CLASS_2_ADDRESS_2"]); // ชั้น
                var street = Convert.ToString(valOpt2["ADDRESS_ROAD_APP_FACTORY_CLASS_2_ADDRESS_2"]); // ถนน
                var province = Convert.ToString(valOpt2["ADDRESS_PROVINCE_APP_FACTORY_CLASS_2_ADDRESS_2_TEXT"]); // จังหวัด
                var district = Convert.ToString(valOpt2["ADDRESS_AMPHUR_APP_FACTORY_CLASS_2_ADDRESS_2_TEXT"]); // อำเภอ
                var subDistrict = Convert.ToString(valOpt2["ADDRESS_TUMBOL_APP_FACTORY_CLASS_2_ADDRESS_2_TEXT"]); // ตำบล
                var tell = Convert.ToString(valOpt2["ADDRESS_TELEPHONE_APP_FACTORY_CLASS_2_ADDRESS_2"]); // โทรศัพท์
                var tellConcat = Convert.ToString(valOpt2["ADDRESS_TELEPHONE_EXT_APP_FACTORY_CLASS_2_ADDRESS_2"]); // ต่อ
                var fax = Convert.ToString(valOpt2["ADDRESS_FAX_APP_FACTORY_CLASS_2_ADDRESS_2"]); // โทรสาร
                strDetail = strDetail + "เปลี่ยนแปลงที่ตั้งสำนักงานใหญ่เป็น :";

                if (moo.Equals(string.Empty))
                {
                    moo = "-";
                }

                if (village.Equals(string.Empty))
                {
                    village = "-";
                }

                if (soi.Equals(string.Empty))
                {
                    soi = "-";
                }

                if (building.Equals(string.Empty))
                {
                    building = "-";
                }

                if (roomNbr.Equals(string.Empty))
                {
                    roomNbr = "-";
                }

                if (floor.Equals(string.Empty))
                {
                    floor = "-";
                }

                if (street.Equals(string.Empty))
                {
                    street = "-";
                }

                if (tellConcat.Equals(string.Empty))
                {
                    tellConcat = "-";
                }

                if (fax.Equals(string.Empty))
                {
                    fax = "-";
                }
                strDetail = strDetail + "เปลี่ยนแปลงที่ตั้งสำนักงานใหญ่เป็น : " + string.Format("เลขที่ : {0} หมู่ที่ : {1} หมู่บ้าน : {2} ตรอก/ซอย : {3}" +
                    "อาคาร : {4} ห้องเลขที่ : {5} ชั้น : {6} ถนน : {7} จังหวัด : {8} อำเภอ : {9} ตำบล : {10} โทรศัพท์ : {11} ต่อ : {12} โทรสาร : {13},"
                    , nbr, moo, village, soi, building, roomNbr, floor, street, province, district, subDistrict, tell, tellConcat, fax);
            }
            if (c)
            {
                str = str + "เปลี่ยนแปลงที่ตั้งโรงงาน,";
                var valOpt3 = req.Data["APP_FACTORY_CLASS_2_OPT_3"].Data;
                var nbr = Convert.ToString(valOpt3["ADDRESS_MOO_APP_FACTORY_CLASS_2_ADDRESS"]); // เลขที่
                var moo = Convert.ToString(valOpt3["ADDRESS_MOO_APP_FACTORY_CLASS_2_ADDRESS"]); // หมู่ที่
                var village = Convert.ToString(valOpt3["ADDRESS_VILLAGE_APP_FACTORY_CLASS_2_ADDRESS"]); // หมู่บ้าน
                var soi = Convert.ToString(valOpt3["ADDRESS_SOI_APP_FACTORY_CLASS_2_ADDRESS"]); // ตรอก/ซอย
                var building = Convert.ToString(valOpt3["ADDRESS_BUILDING_APP_FACTORY_CLASS_2_ADDRESS"]); // อาคาร
                var roomNbr = Convert.ToString(valOpt3["ADDRESS_ROOMNO_APP_FACTORY_CLASS_2_ADDRESS"]); // ห้องเลขที่
                var floor = Convert.ToString(valOpt3["ADDRESS_FLOOR_APP_FACTORY_CLASS_2_ADDRESS"]); // ชั้น
                var street = Convert.ToString(valOpt3["ADDRESS_ROAD_APP_FACTORY_CLASS_2_ADDRESS"]); // ถนน
                var province = Convert.ToString(valOpt3["ADDRESS_PROVINCE_APP_FACTORY_CLASS_2_ADDRESS_TEXT"]); // จังหวัด
                var district = Convert.ToString(valOpt3["ADDRESS_AMPHUR_APP_FACTORY_CLASS_2_ADDRESS_TEXT"]); // อำเภอ
                var subDistrict = Convert.ToString(valOpt3["ADDRESS_TUMBOL_APP_FACTORY_CLASS_2_ADDRESS_TEXT"]); // ตำบล
                var tell = Convert.ToString(valOpt3["ADDRESS_TELEPHONE_APP_FACTORY_CLASS_2_ADDRESS"]); // โทรศัพท์
                var tellConcat = Convert.ToString(valOpt3["ADDRESS_TELEPHONE_EXT_APP_FACTORY_CLASS_2_ADDRESS"]); // ต่อ
                var fax = Convert.ToString(valOpt3["ADDRESS_FAX_APP_FACTORY_CLASS_2_ADDRESS"]); // โทรสาร
                strDetail = strDetail + "เปลี่ยนแปลงที่ตั้งโรงงานเป็น : " + string.Format("เลขที่ : {0} หมู่ที่ : {1} หมู่บ้าน : {2} ตรอก/ซอย : {3}" +
                    "อาคาร : {4} ห้องเลขที่ : {5} ชั้น : {6} ถนน : {7} จังหวัด : {8} อำเภอ : {9} ตำบล : {10} โทรศัพท์ : {11} ต่อ : {12} โทรสาร : {13},"
                    , nbr, moo, village, soi, building, roomNbr, floor, street, province, district, subDistrict, tell, tellConcat, fax);
            }
            if (d)
            {
                var valOpt4 = req.Data["APP_FACTORY_CLASS_2_OPT_4"].Data;
                strDetail = strDetail + Convert.ToString(valOpt4["APP_FACTORY_CLASS_2_EDIT_CHANGING_DETAIL_TEXTBOX"]) + ",";
            }

            model.Add(new PDFFieldValue() { FieldName = "txtOther", Value = str.Substring(0, (str.Length - 1)) });
            model.Add(new PDFFieldValue() { FieldName = "txtDetail", Value = strDetail.Substring(0, (strDetail.Length - 1)) });

            //PDFConfig cfg = new PDFConfig() { FontName = "THSarabunNew.ttf", FontSize = 12 };
            var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model);
            return bytes;
        }

        private string CastToMonth(string strM)
        {
            switch (strM) {
                case "1":
                    return "มกราคม";
                case "2":
                    return "กุมภาพันธ์";
                case "3":
                    return "มีนาคม";
                case "4":
                    return "เมษายน";
                case "5":
                    return "พฤษภาคม";
                case "6":
                    return "มิถุนายน";
                case "7":
                    return "กรกฎาคม";
                case "8":
                    return "สิงหาคม";
                case "9":
                    return "กันยายน";
                case "10":
                    return "ตุลาคม";
                case "11":
                    return "พฤศจิกายน";
                case "12":
                    return "ธันวาคม";
            }
            return null;
        }
    }
}
