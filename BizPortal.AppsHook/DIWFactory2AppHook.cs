using System;
using System.Collections.Generic;
using System.Linq;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using BizPortal.Utils.Helpers;
using MongoDB.Driver;

namespace BizPortal.AppsHook
{
    public class DIWFactory2AppHook : StoreBaseAppHook
    {
        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.Success = true;

            if (stage == AppsStage.UserCreate)
            {
                if (request.AppSysName == AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW)
                {
                    var storeInfo_search = request.Data["FACTORY_CLASS_2_SEARCH"].Data;
                    var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
                    var type2request = repo.Where(x => x.ApplicationRequestNumber == storeInfo_search["AJAX_DROPDOWN_BOOK_ID_OPINION_TEXT"]).FirstOrDefault();
                    var storeInfo = type2request.Data["APP_FACTORY_TYPE2_PLANT_LOCATION_INFO"].Data;
                    if (storeInfo.ContainsKey("ADDRESS_PROVINCE_FACTORY_TYPE2_AREA_INFORMATION_FORM") &&
                        storeInfo.ContainsKey("ADDRESS_AMPHUR_FACTORY_TYPE2_AREA_INFORMATION_FORM") &&
                        storeInfo.ContainsKey("ADDRESS_TUMBOL_FACTORY_TYPE2_AREA_INFORMATION_FORM"))
                    {
                        request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_FACTORY_TYPE2_AREA_INFORMATION_FORM"]);
                        request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_FACTORY_TYPE2_AREA_INFORMATION_FORM"]);
                        request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_FACTORY_TYPE2_AREA_INFORMATION_FORM"]);

                        request.Province = (storeInfo["ADDRESS_PROVINCE_FACTORY_TYPE2_AREA_INFORMATION_FORM_TEXT"]);
                        request.Amphur = (storeInfo["ADDRESS_AMPHUR_FACTORY_TYPE2_AREA_INFORMATION_FORM_TEXT"]);
                        request.Tumbol = (storeInfo["ADDRESS_TUMBOL_FACTORY_TYPE2_AREA_INFORMATION_FORM_TEXT"]);
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "ไม่พบข้อมูลตำบล อำเภอ จังหวัดของที่ตั้งโรงงาน";
                    }
                }
                else if (request.AppSysName == AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT)
                {
                    var storeInfo = request.Data["APP_FACTORY_CLASS_2_SECTION_PLANT_LOCATION_INFO"].Data;
                   
                    //if change factory is True
                    var plantInfo = request.Data["APP_FACTORY_CLASS_2_SECTION_INFO_EDIT"].Data;
                    bool c = Convert.ToBoolean(plantInfo["APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX"]);
                    if(c)
                    {
                        var valOpt3 = request.Data["APP_FACTORY_CLASS_2_OPT_3"].Data;
                        //var province = Convert.ToString(valOpt3["ADDRESS_PROVINCE_APP_FACTORY_CLASS_2_ADDRESS_TEXT"]); // จังหวัด
                        //var district = Convert.ToString(valOpt3["ADDRESS_AMPHUR_APP_FACTORY_CLASS_2_ADDRESS_TEXT"]); // อำเภอ
                        //var subDistrict = Convert.ToString(valOpt3["ADDRESS_TUMBOL_APP_FACTORY_CLASS_2_ADDRESS_TEXT"]); // ตำบล

                        request.ProvinceID = int.Parse(valOpt3["ADDRESS_PROVINCE_APP_FACTORY_CLASS_2_ADDRESS"]);
                        request.AmphurID = int.Parse(valOpt3["ADDRESS_AMPHUR_APP_FACTORY_CLASS_2_ADDRESS"]);
                        request.TumbolID = int.Parse(valOpt3["ADDRESS_TUMBOL_APP_FACTORY_CLASS_2_ADDRESS"]);

                        request.Province = (valOpt3["ADDRESS_PROVINCE_APP_FACTORY_CLASS_2_ADDRESS_TEXT"]);
                        request.Amphur = (valOpt3["ADDRESS_AMPHUR_APP_FACTORY_CLASS_2_ADDRESS_TEXT"]);
                        request.Tumbol = (valOpt3["ADDRESS_TUMBOL_APP_FACTORY_CLASS_2_ADDRESS_TEXT"]);

                    }
                    else
                    {
                       if (storeInfo.ContainsKey("ADDRESS_PROVINCE_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO") &&
                       storeInfo.ContainsKey("ADDRESS_AMPHUR_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO") &&
                       storeInfo.ContainsKey("ADDRESS_TUMBOL_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO"))
                        {
                            request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO"]);
                            request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO"]);
                            request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO"]);

                            request.Province = (storeInfo["ADDRESS_PROVINCE_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_TEXT"]);
                            request.Amphur = (storeInfo["ADDRESS_AMPHUR_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_TEXT"]);
                            request.Tumbol = (storeInfo["ADDRESS_TUMBOL_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_TEXT"]);
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "ไม่พบข้อมูลตำบล อำเภอ จังหวัดของที่ตั้งโรงงาน";
                        }
                    }


                }
                else if (request.AppSysName == AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL)
                {
                    var storeInfo = request.Data["APP_FACTORY_CLASS_2_SECTION_LOCATION_CANCEL"].Data;
                    if (storeInfo.ContainsKey("ADDRESS_PROVINCE_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_CANCEL") &&
                        storeInfo.ContainsKey("ADDRESS_AMPHUR_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_CANCEL") &&
                        storeInfo.ContainsKey("ADDRESS_TUMBOL_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_CANCEL"))
                    {
                        request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_CANCEL"]);
                        request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_CANCEL"]);
                        request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_CANCEL"]);

                        request.Province = (storeInfo["ADDRESS_PROVINCE_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_CANCEL_TEXT"]);
                        request.Amphur = (storeInfo["ADDRESS_AMPHUR_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_CANCEL_TEXT"]);
                        request.Tumbol = (storeInfo["ADDRESS_TUMBOL_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_CANCEL_TEXT"]);
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "ไม่พบข้อมูลตำบล อำเภอ จังหวัดของที่ตั้งโรงงาน";
                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = "AppHook ไม่ถูกต้อง";
                }

            }

            return result;
        }

        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
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

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = string.Empty;
            var generalInfo = req.Data["GENERAL_INFORMATION"].Data;
            string strDate = generalInfo["INFORMATION_HEADER__REQUEST_DATE"].ToString();
            string[] strDateArr = strDate.Split('/');
            string dd = strDateArr[0];
            string mm = strDateArr[1];
            string strYY = strDateArr[2];
            string[] yyArr = strYY.Split(' ');
            string yy = yyArr[0];

            List<PDFFieldValue> model = new List<PDFFieldValue>();
            model.Add(new PDFFieldValue() { FieldName = "txtDay", Value = dd }); ;
            model.Add(new PDFFieldValue() { FieldName = "txtYear", Value = yy }); ;
            model.Add(new PDFFieldValue() { FieldName = "txtMonth", Value = CastToMonth(mm) });


            if (req.ApplicationName.Equals("ขอแก้ไขการแจ้งประกอบกิจการโรงงานจำพวกที่ 2"))
            {
                src = serverMapPathFunc("~/Uploads/apps/factory/Fac_Edit.pdf");               

                if (req.IdentityType == UserTypeEnum.Citizen)
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtName", Value = string.Format("{0} {1} {2}", generalInfo["DROPDOWN_CITIZEN_TITLE_TEXT"], generalInfo["CITIZEN_NAME"], generalInfo["CITIZEN_LASTNAME"]) });
                    model.Add(new PDFFieldValue() { FieldName = "txtAge", Value = generalInfo["GENERAL_INFORMATION__CITIZEN_AGE"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtNational", Value = generalInfo["DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtOfficePlace", Value = "-----------------------" });

                    var citiInfo = req.Data["CITIZEN_ADDRESS_INFORMATION"].Data;
                    model.Add(new PDFFieldValue() { FieldName = "txtNbr", Value = citiInfo["ADDRESS_CITIZEN_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtMoo", Value = citiInfo["ADDRESS_MOO_CITIZEN_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtSOI", Value = citiInfo["ADDRESS_SOI_CITIZEN_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtStreet", Value = citiInfo["ADDRESS_ROAD_CITIZEN_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtSubDistrict", Value = citiInfo["ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtDistrict", Value = citiInfo["ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtProvince", Value = citiInfo["ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtTell", Value = citiInfo["ADDRESS_TELEPHONE_CITIZEN_ADDRESS"] });
                    if (!string.IsNullOrEmpty(citiInfo["ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS"]))
                        model.First(o => o.FieldName == "txtTell").Value += " ต่อ " + citiInfo["ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS"];
                    model.Add(new PDFFieldValue() { FieldName = "txtCanel", Value = citiInfo["CITIZEN_CANEL"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtRiver", Value = citiInfo["CITIZEN_RIVER"] });
                }
                else
                {
                    var addrInfo = req.Data["JURISTIC_ADDRESS_INFORMATION"].Data;
                    model.Add(new PDFFieldValue() { FieldName = "txtNbr", Value = addrInfo["ADDRESS_JURISTIC_HQ_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtMoo", Value = addrInfo["ADDRESS_MOO_JURISTIC_HQ_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtSOI", Value = addrInfo["ADDRESS_SOI_JURISTIC_HQ_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtStreet", Value = addrInfo["ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtSubDistrict", Value = addrInfo["ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtDistrict", Value = addrInfo["ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtProvince", Value = addrInfo["ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtTell", Value = addrInfo["ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS"] });
                    if (!string.IsNullOrEmpty(addrInfo["ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS"]))
                        model.First(o => o.FieldName == "txtTell").Value += " ต่อ " + addrInfo["ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS"];
                    model.Add(new PDFFieldValue() { FieldName = "txtName", Value = generalInfo["COMPANY_NAME_TH"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtAge", Value = "-" });
                    model.Add(new PDFFieldValue() { FieldName = "txtRace", Value = "-" });
                    model.Add(new PDFFieldValue() { FieldName = "txtPlace", Value = "---------" });
                    model.Add(new PDFFieldValue() { FieldName = "txtCanel", Value = addrInfo["JURISTIC_CANEL"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtRiver", Value = addrInfo["JURISTIC_RIVER"] });
                }

                // ข้อมูลเฉพาะ
                var plantInfo = req.Data["APP_FACTORY_CLASS_2_SECTION_INFO_EDIT"].Data;
                bool a = Convert.ToBoolean(plantInfo["APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_CHECKBOX"]);
                bool b = Convert.ToBoolean(plantInfo["APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX"]);
                bool c = Convert.ToBoolean(plantInfo["APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX"]);
                bool d = Convert.ToBoolean(plantInfo["APP_FACTORY_CLASS_2_EDIT_CHANGING_APP_FACTORY_CLASS_2_EDIT_CHANGING_DETAIL"]);
                model.Add(new PDFFieldValue() { FieldName = "txtFacName", Value = plantInfo["APP_FACTORY_CLASS_2_FACT_NAME"] });
                model.Add(new PDFFieldValue() { FieldName = "txtFacID", Value = plantInfo["FACTORY_CLASS_2_ID"] });
                string str = string.Empty;
                string strDetail = string.Empty;

                var storeInfo = req.Data["APP_FACTORY_CLASS_2_SECTION_PLANT_LOCATION_INFO"].Data;
                model.Add(new PDFFieldValue() { FieldName = "txtLocat", Value = storeInfo["ADDRESS_AMPHUR_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "txtLocatOffice", Value = storeInfo["ADDRESS_PROVINCE_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_TEXT"] });

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
            }
            if (req.ApplicationName.Equals("ขอแจ้งเลิกประกอบกิจการโรงงานจำพวกที่ 2"))
            {
                src = serverMapPathFunc("~/Uploads/apps/factory/Fac_Cancel.pdf");

                if (req.IdentityType == UserTypeEnum.Citizen)
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtName", Value = string.Format("{0} {1} {2}", generalInfo["DROPDOWN_CITIZEN_TITLE_TEXT"], generalInfo["CITIZEN_NAME"], generalInfo["CITIZEN_LASTNAME"]) });
                    model.Add(new PDFFieldValue() { FieldName = "txtAge", Value = generalInfo["GENERAL_INFORMATION__CITIZEN_AGE"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtNational", Value = generalInfo["DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtOfficePlace", Value = "-----------------------" });

                    var citiInfo = req.Data["CITIZEN_ADDRESS_INFORMATION"].Data;
                    model.Add(new PDFFieldValue() { FieldName = "txtNbr", Value = citiInfo["ADDRESS_CITIZEN_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtMoo", Value = citiInfo["ADDRESS_MOO_CITIZEN_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtSOI", Value = citiInfo["ADDRESS_SOI_CITIZEN_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtStreet", Value = citiInfo["ADDRESS_ROAD_CITIZEN_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtSubDistrict", Value = citiInfo["ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtDistrict", Value = citiInfo["ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtProvince", Value = citiInfo["ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtTell", Value = citiInfo["ADDRESS_TELEPHONE_CITIZEN_ADDRESS"] });
                    if (!string.IsNullOrEmpty(citiInfo["ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS"]))
                        model.First(o => o.FieldName == "txtTell").Value += " ต่อ " + citiInfo["ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS"];
                    model.Add(new PDFFieldValue() { FieldName = "txtCanel", Value = citiInfo["CITIZEN_CANEL"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtRiver", Value = citiInfo["CITIZEN_RIVER"] });
                }
                else
                {
                    var addrInfo = req.Data["JURISTIC_ADDRESS_INFORMATION"].Data;
                    model.Add(new PDFFieldValue() { FieldName = "txtNbr", Value = addrInfo["ADDRESS_JURISTIC_HQ_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtMoo", Value = addrInfo["ADDRESS_MOO_JURISTIC_HQ_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtSOI", Value = addrInfo["ADDRESS_SOI_JURISTIC_HQ_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtStreet", Value = addrInfo["ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtSubDistrict", Value = addrInfo["ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtDistrict", Value = addrInfo["ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtProvince", Value = addrInfo["ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtTell", Value = addrInfo["ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS"] });
                    if (!string.IsNullOrEmpty(addrInfo["ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS"]))
                        model.First(o => o.FieldName == "txtTell").Value += " ต่อ " + addrInfo["ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS"];
                    model.Add(new PDFFieldValue() { FieldName = "txtName", Value = generalInfo["COMPANY_NAME_TH"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtAge", Value = "-" });
                    model.Add(new PDFFieldValue() { FieldName = "txtRace", Value = "-" });
                    model.Add(new PDFFieldValue() { FieldName = "txtPlace", Value = "---------" });
                    model.Add(new PDFFieldValue() { FieldName = "txtCanel", Value = addrInfo["JURISTIC_CANEL"] });
                    model.Add(new PDFFieldValue() { FieldName = "txtRiver", Value = addrInfo["JURISTIC_RIVER"] });
                }

                // ข้อมูลเฉพาะ
                string strDate2 = DateTime.Now.Date.ToString();
                string[] strDateArr2 = strDate2.Split('/');
                string dd2 = strDateArr2[0];
                string mm2 = strDateArr2[1];
                string strYY2 = strDateArr2[2];
                string[] yyArr2 = strYY2.Split(' ');
                string yy2 = yyArr2[0];
                var reFromDate = dd2 + " " + (CastToMonth(mm2)) + " " + yy2;

                model.Add(new PDFFieldValue() { FieldName = "txtCurrentDate", Value = reFromDate }) ;
                var facInfo = req.Data["APP_FACTORY_CLASS_2_SECTION_CANCEL_REQUEST"].Data;
                model.Add(new PDFFieldValue() { FieldName = "txtFacName", Value = facInfo["APP_FACTORY_CLASS_2_FACT_NAME"] });
                model.Add(new PDFFieldValue() { FieldName = "txtFacID", Value = facInfo["APP_FACTORY_CLASS_2_ID"] });
                model.Add(new PDFFieldValue() { FieldName = "txtDetail", Value = facInfo["APP_FACTORY_CLASS_2_CANCEL_REASON"] });

                var storeInfo = req.Data["APP_FACTORY_CLASS_2_SECTION_LOCATION_CANCEL"].Data;
                model.Add(new PDFFieldValue() { FieldName = "txtLocat", Value = storeInfo["ADDRESS_AMPHUR_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_CANCEL_TEXT"] });
                model.Add(new PDFFieldValue() { FieldName = "txtLocatOffice", Value = storeInfo["ADDRESS_PROVINCE_APP_FACTORY_CLASS_2_PLANT_LOCATION_INFO_CANCEL_TEXT"] });
            }

            //PDFConfig cfg = new PDFConfig() { FontName = "THSarabunNew.ttf", FontSize = 12 };
            var bytes = iTextPDFFormFieldsHelper.ApplyPDFFieldValues(src, model);
            return bytes;
        }

        private string CastToMonth(string strM)
        {
            switch (strM)
            {
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
