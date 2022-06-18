using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Apps;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using EGA.WS;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using System.Web;
using static BizPortal.ViewModels.Apps.DOAStandard.DOAPlants;
using Newtonsoft.Json.Linq;
using BizPortal.Integrated;

namespace BizPortal.AppsHook
{
    public class DOAOrganicPlantReNewAppHook : SingleFormRendererAppHook
    {

        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public override bool IsEnabledChat() => false;

        public override bool InvokeSingleForm(Guid trid, string currentSectionGroup, ref SingleFormRequestViewModel model)
        {
            var result = true;
            var darftData = SingleFormRequestEntity.Get(model.IdentityID);
            string certificateNumber = string.Empty;

            var formdata = model.SectionData.Where(e => e.SectionName == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_SEARCH_SEARCH_SECTION").Select(e => e.FormData).FirstOrDefault();
            if (formdata != null)
            {
                certificateNumber = formdata["AJAX_DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_SEARCH_SEARCH_SECTION_LICENSE_RENEW"].ToString();
            }

            if (currentSectionGroup == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_SEARCH")
            {
                var singleformReq = new SingleFormRequestEntity();
                singleformReq.DeleteSections(model.IdentityID, "APP_ORGANIC_PLANT_PRODUCTION_RENEW", new string[] { "APP_ORGANIC_PLANT_PRODUCTION_RENEW_SEARCH_SEARCH_SECTION", "" });
            }
            else if (currentSectionGroup == "INFORMATION" || currentSectionGroup == "APP_ORGANIC_PLANT_PRODUCTION_RENEW")
            {
                var cinfo = getCertificateInfo(model.IdentityID, certificateNumber);
                string DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE = string.Empty;
                string DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE_TEXT = string.Empty;
                if (string.IsNullOrEmpty(cinfo.responseModel.farmer_group_id))
                {
                    DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE = "STANDALONE";
                    DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE_TEXT = "แปลงเดียว/รายเดียว";
                }
                else
                {
                    DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE = "GROUP";
                    DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE_TEXT = "รายกลุ่ม";
                }
                var OrganicInfo = GetSectionData(model, "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION", SectionType.Form);
                OrganicInfo.FormData = new Dictionary<string, object>
                {
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TEXT","ต่ออายุ"},
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE},
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE_TEXT",DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE_TEXT},
                };



                if (cinfo.responseModel.idcardType == "personal")
                {
                    string ptel = cinfo.responseModel.tel;
                    string[] parrTel = ptel.Split(new[] { "ต่อ" }, StringSplitOptions.None);
                    string ptelext = string.Empty;
                    if (parrTel.Count() > 1)
                    {
                        ptel = parrTel[0];
                        ptelext = parrTel[1];
                    }
                    var CITIZEN_ADDRESS_INFORMATION = GetSectionData(model, "CITIZEN_ADDRESS_INFORMATION", SectionType.Form);
                    CITIZEN_ADDRESS_INFORMATION.FormData = new Dictionary<string, object>
                    {
                        {"ADDRESS_CITIZEN_ADDRESS" , cinfo.responseModel.village_no},
                        {"ADDRESS_MOO_CITIZEN_ADDRESS" ,cinfo.responseModel.moo},
                        {"ADDRESS_SOI_CITIZEN_ADDRESS" , cinfo.responseModel.lane},
                        {"ADDRESS_BUILDING_CITIZEN_ADDRESS" , string.Empty},
                        {"ADDRESS_ROOMNO_CITIZEN_ADDRESS" ,string.Empty},
                        {"ADDRESS_FLOOR_CITIZEN_ADDRESS" ,string.Empty},
                        {"ADDRESS_ROAD_CITIZEN_ADDRESS" , cinfo.responseModel.road},
                        {"ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT" , GeoService.GetProvinceText(cinfo.responseModel.province_id)},
                        {"ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT" , GeoService.GetAmphurText(cinfo.responseModel.province_id,cinfo.responseModel.amphur_id)},
                        {"ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT" , GeoService.GetTambolText(cinfo.responseModel.province_id,cinfo.responseModel.amphur_id,cinfo.responseModel.district_id)},
                        {"ADDRESS_POSTCODE_CITIZEN_ADDRESS" , cinfo.responseModel.postcode},
                        {"ADDRESS_TELEPHONE_CITIZEN_ADDRESS" ,ptel},
                        {"ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS" , ptelext},
                        {"ADDRESS_FAX_CITIZEN_ADDRESS" , cinfo.responseModel.fax},
                        {"ADDRESS_PROVINCE_CITIZEN_ADDRESS" , cinfo.responseModel.province_id},
                        {"ADDRESS_AMPHUR_CITIZEN_ADDRESS" , cinfo.responseModel.amphur_id},
                        {"ADDRESS_TUMBOL_CITIZEN_ADDRESS" , cinfo.responseModel.district_id},
                        //{"CITIZEN_EMAIL" , "za@dga.or.th" }
                    };
                }

                if (cinfo.responseModel.idcardType == "company")
                {
                    string jtel = cinfo.responseModel.tel;
                    string[] jarrTel = jtel.Split(new[] { "ต่อ" }, StringSplitOptions.None);
                    string jtelext = string.Empty;
                    if (jarrTel.Count() > 1)
                    {
                        jtel = jarrTel[0];
                        jtelext = jarrTel[1];
                    }
                    var JURISTIC_ADDRESS_INFORMATION = GetSectionData(model, "JURISTIC_ADDRESS_INFORMATION", SectionType.Form);
                    JURISTIC_ADDRESS_INFORMATION.FormData = new Dictionary<string, object>
                    {
                        {"ADDRESS_JURISTIC_HQ_ADDRESS" , cinfo.responseModel.village_no},
                        {"ADDRESS_MOO_JURISTIC_HQ_ADDRESS" , cinfo.responseModel.moo},
                        {"ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS" , ""},
                        {"ADDRESS_SOI_JURISTIC_HQ_ADDRESS" , cinfo.responseModel.lane},
                        {"ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS" , ""},
                        {"ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS" , ""},
                        {"ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS" , ""},
                        {"ADDRESS_ROAD_JURISTIC_HQ_ADDRESS" , cinfo.responseModel.road},
                        {"ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS" , cinfo.responseModel.province_id},
                        {"ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT" ,GeoService.GetProvinceText(cinfo.responseModel.province_id)},
                        {"ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS" , cinfo.responseModel.amphur_id},
                        {"ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT" , GeoService.GetAmphurText(cinfo.responseModel.province_id,cinfo.responseModel.amphur_id)},
                        {"ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS" , cinfo.responseModel.district_id},
                        {"ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT" , GeoService.GetTambolText(cinfo.responseModel.province_id,cinfo.responseModel.amphur_id,cinfo.responseModel.district_id)},
                        {"ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS" , cinfo.responseModel.postcode},
                        {"ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS" , jtel},
                        {"ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS" , jtelext},
                        {"ADDRESS_FAX_JURISTIC_HQ_ADDRESS" , cinfo.responseModel.fax},
                        {"ADDRESS_MOBILE_JURISTIC_HQ_ADDRESS" , cinfo.responseModel.mobile},
                        {"ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS",cinfo.responseModel.email }
                    };
                }
                //cinfo.responseModel.email.ToString()
                //if (!darftData.SectionData.Any(e => e.SectionName == "GENERAL_INFORMATION"))
                {
                    var OwnerInfo = GetSectionData(model, "GENERAL_INFORMATION", SectionType.Form);
                    OwnerInfo.FormData = new Dictionary<string, object>
                    {
                        // {"DROPDOWN_CITIZEN_TITLE",darftData.SectionData.Where(x=>x.SectionName=="GENERAL_INFORMATION").ToList()[0].FormData["DROPDOWN_CITIZEN_TITLE"]},
                        // {"DROPDOWN_CITIZEN_TITLE_TEXT",darftData.SectionData.Where(x=>x.SectionName=="GENERAL_INFORMATION").ToList()[0].FormData["DROPDOWN_CITIZEN_TEXT"]},
                    };
                }
                string tel = cinfo.responseModel.garden_info.tel;
                string[] arrTel = tel.Split(new[] { "ต่อ" }, StringSplitOptions.None);
                string telext = string.Empty;
                if (arrTel.Count() > 1)
                {
                    tel = arrTel[0];
                    telext = arrTel[1];
                }
                // //////////////////////////////////////ข้อมูลใบรับรอง 
                string address_type = string.Empty;
                string APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_INFORMATION_SHOW_ADDRESS_OPTION = string.Empty;
                if (cinfo.responseModelBiz.garden_info.address_type.ToString() == "1")
                {
                    APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_INFORMATION_SHOW_ADDRESS_OPTION = "LOCATION_ADDRESS";
                }
                else if (cinfo.responseModelBiz.garden_info.address_type.ToString() == "2")
                {
                    APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_INFORMATION_SHOW_ADDRESS_OPTION = "PRODUCE_ADDRESS";
                }
                string DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_SIGNAL_TYPE = string.Empty;
                string DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_SIGNAL_TYPE_TEXT = string.Empty;
                if (cinfo.responseModel.garden_info.request_hallmark.ToString().ToLower() == "true")
                {
                    DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_SIGNAL_TYPE = "YES";
                    DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_SIGNAL_TYPE_TEXT = "ใช่";
                }
                else
                {
                    DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_SIGNAL_TYPE = "NO";
                    DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_SIGNAL_TYPE_TEXT = "ไม่ใช่";
                }
                /////////////////////////////////////////////////ข้อมูลที่ตั้งแหล่งผลิต/ที่ตั้งแปลง
                ///
                //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION"))
                {
                    var APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION = GetSectionData(model, "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION", SectionType.Form);
                    APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION.FormData = new Dictionary<string, object>
                    {
                    {"ADDRESS_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS" ,cinfo.responseModel.garden_info.village_no },
                    {"ADDRESS_MOO_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS" , cinfo.responseModel.garden_info.moo },
                    {"ADDRESS_VILLAGE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS"  ,string.Empty },//draf
                    {"ADDRESS_SOI_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS" , cinfo.responseModel.garden_info.lane },
                    {"ADDRESS_ROAD_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS" ,cinfo.responseModel.garden_info.road},
                    {"ADDRESS_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS" , cinfo.responseModel.garden_info.province_id },
                    {"ADDRESS_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS" , cinfo.responseModel.garden_info.amphur_id },
                    {"ADDRESS_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS" ,cinfo.responseModel.garden_info.district_id },
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_MOBILE" , cinfo.responseModel.garden_info.mobile },
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_EMAIL" , cinfo.responseModel.garden_info.email },
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION" , string.Empty },
                    {"ADDRESS_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_TEXT" , GeoService.GetProvinceText(cinfo.responseModel.garden_info.province_id)},
                    {"ADDRESS_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_TEXT" , GeoService.GetAmphurText(cinfo.responseModel.garden_info.province_id,cinfo.responseModel.garden_info.amphur_id) },
                    {"ADDRESS_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_TEXT" , GeoService.GetTambolText(cinfo.responseModel.garden_info.province_id,cinfo.responseModel.garden_info.amphur_id,cinfo.responseModel.garden_info.district_id) },
                    {"ADDRESS_POSTCODE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS",cinfo.responseModel.garden_info.postcode},
                    {"ADDRESS_TELEPHONE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS",tel},
                    {"ADDRESS_TELEPHONE_EXT_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS",telext},
                    {"ADDRESS_FAX_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS",cinfo.responseModel.garden_info.fax },
                    {"ADDRESS_LNG_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS",string.Empty},
                    {"ADDRESS_LAT_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS",string.Empty},
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_1" , cinfo.responseModelBiz.garden_info.doc_type_1!=null ? cinfo.responseModelBiz.garden_info.doc_type_1.is_check.ToString().ToLower():"false"},
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_4" , cinfo.responseModelBiz.garden_info.doc_type_4!=null ? cinfo.responseModelBiz.garden_info.doc_type_4.is_check.ToString().ToLower():"false"},
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_7" , cinfo.responseModelBiz.garden_info.doc_type_7!=null ? cinfo.responseModelBiz.garden_info.doc_type_7.is_check.ToString().ToLower():"false"},
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_ID_TEXT" , cinfo.responseModelBiz.garden_info.doc_type_description},
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_2" , cinfo.responseModelBiz.garden_info.doc_type_2!=null ? cinfo.responseModelBiz.garden_info.doc_type_2.is_check.ToString().ToLower():"false"},
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_3" , cinfo.responseModelBiz.garden_info.doc_type_3!=null ? cinfo.responseModelBiz.garden_info.doc_type_3.is_check.ToString().ToLower():"false"},
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_5" , cinfo.responseModelBiz.garden_info.doc_type_5!=null ? cinfo.responseModelBiz.garden_info.doc_type_5.is_check.ToString().ToLower():"false"},
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_6" , cinfo.responseModelBiz.garden_info.doc_type_6!=null ? cinfo.responseModelBiz.garden_info.doc_type_6.is_check.ToString().ToLower():"false"},
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_8" , cinfo.responseModelBiz.garden_info.doc_type_8!=null ? cinfo.responseModelBiz.garden_info.doc_type_8.is_check.ToString().ToLower():"false"},
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_9" , cinfo.responseModelBiz.garden_info.doc_type_9!=null ? cinfo.responseModelBiz.garden_info.doc_type_9.is_check.ToString().ToLower():"false"},
                    {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_10" ,cinfo.responseModelBiz.garden_info.doc_type_10!=null ? cinfo.responseModelBiz.garden_info.doc_type_10.is_check.ToString().ToLower():"false"},
                    {"AJAX_DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_PRODUCE_ORGANIC_TYPE", cinfo.responseModel.garden_info.plant_type_id},
                    {"AJAX_DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_PRODUCE_ORGANIC_TYPE_TEXT", cinfo.responseModel.garden_info.plant_type_name},
                    {"DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_SIGNAL_TYPE",DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_SIGNAL_TYPE },
                    {"DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_SIGNAL_TYPE_TEXT",DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_SIGNAL_TYPE_TEXT }
                    };
                }
                /////////////////////////////////////////////////ข้อมูลแผนการผลิตพืชอินทรีย์

                //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1"))
                // {
                var APP_ORGANIC_SECTION = GetSectionData(model, "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION", SectionType.ArrayOfForms);
                List<Dictionary<string, object>> ORGANICList = new List<Dictionary<string, object>>();
                if (cinfo.responseModel.garden_item != null)
                {
                    foreach (var gardenitem in cinfo.responseModel.garden_item)
                    {
                        var tmpStartMonth = string.Empty;
                        var tmpEndMonth = string.Empty;



                        var dic = new Dictionary<string, object>
                                            {
                                                //{"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_TYPE", gardenitem.plant_id},
                                                //{"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_TYPE_TEXT", gardenitem.plant_name},
                                                {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_TYPE", gardenitem.plant_name},
                                                {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_AREA",gardenitem.area_size},
                                                {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_TREE_AMOUNT",gardenitem.plant_qty},
                                                {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_YEAR_AMOUNT",gardenitem.cycle_qty},
                                                {"DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_START_MONTH", (gardenitem.start_harvest.ToString())},
                                                {"DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_START_MONTH_TEXT", getMonthText(gardenitem.start_harvest.ToString()) },
                                                {"DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_END_MONTH",(gardenitem.end_harvest.ToString())},
                                                {"DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_END_MONTH_TEXT", getMonthText(gardenitem.end_harvest.ToString()) },
                                                { "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_HARVEST_MONTH",gardenitem.date_harvest},
                                                {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_PRODUCE_AMOUNT",gardenitem.qty }
                                            };

                        ORGANICList.Add(dic);
                    }
                    APP_ORGANIC_SECTION.ArrayData = ORGANICList;
                }

                // }


                // if (!darftData.SectionData.Any(e => e.SectionName == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE"))
                {
                    var APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE = GetSectionData(model, "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE", SectionType.Form);
                    APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE.FormData = new Dictionary<string, object>
                     {
                         {"DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_SIGNAL_TYPE", DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_SIGNAL_TYPE},
                         {"DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_SIGNAL_TYPE_TEXT", DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_SIGNAL_TYPE_TEXT},
                         {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_INFORMATION_SHOW_ADDRESS_OPTION",APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_INFORMATION_SHOW_ADDRESS_OPTION},
                         {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_LICENSE_INFORMATION_FARMER",cinfo.responseModel.garden_info.request_certificate_type_1.ToString().ToLower()},
                         {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_LICENSE_INFORMATION_NAME_THAI",cinfo.responseModel.garden_info.request_certificate_type_2.ToString().ToLower()},
                         {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_LICENSE_INFORMATION_NAME_ENG",cinfo.responseModel.garden_info.request_certificate_type_3.ToString().ToLower()},

                     };
                }

                var APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2 = GetSectionData(model, "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2", SectionType.Form);
                APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2.FormData = new Dictionary<string, object>
                     {
                           {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION_FARMER",cinfo.responseModel.garden_info.request_certificate_type_1.ToString().ToLower()},
                           {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION_NAME_THAI",cinfo.responseModel.garden_info.request_certificate_type_2.ToString().ToLower()},
                           {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION_NAME_ENG",cinfo.responseModel.garden_info.request_certificate_type_3.ToString().ToLower()},
                           {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_INFORMATION_SHOW_ADDRESS_OPTION",APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_LICENCE_INFORMATION_SHOW_ADDRESS_OPTION},

                     };



                // //////////////////////////////////////ข้อมูลชื่ออื่นภาษาไทย
                // ///
                //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAI_SECTION"))
                {
                    var APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAI_SECTION = GetSectionData(model, "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAI_SECTION", SectionType.Form);
                    APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAI_SECTION.FormData = new Dictionary<string, object>
                     {
                           {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAI_SECTION_INFORMATION_NAME_THAI_TEXT",cinfo.responseModel.garden_info.th_name.ToString()}
                     };
                }
                // ///////////////////////////////////////ข้อมูลชื่ออื่นภาษาไทย-ภาษาอังกฤษ    
                // ///
                //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION"))
                {
                    var APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION = GetSectionData(model, "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION", SectionType.Form);
                    if (cinfo.responseModelBiz.garden_info.address_type.ToString() == "1")
                    {
                        APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION.FormData = new Dictionary<string, object>
                          {
                             {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_NAME_ENG_TEXT" ,  cinfo.responseModel.garden_info.eng_name},
                             {"ADDRESS_EN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.address_no},
                             {"ADDRESS_EN_MOO_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.moo},
                             {"ADDRESS_EN_SOI_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.soi},
                             {"ADDRESS_EN_BUILDING_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.building},
                             {"ADDRESS_EN_ROOMNO_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.room},
                             {"ADDRESS_EN_FLOOR_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.floor},
                             {"ADDRESS_EN_ROAD_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.road},
                             {"ADDRESS_EN_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.province_code},
                             {"ADDRESS_EN_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.amphur_code},
                             {"ADDRESS_EN_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.tambon_name},
                             {"ADDRESS_EN_POSTCODE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.post_code},
                             {"ADDRESS_EN_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS_TEXT" ,cinfo.responseModel.garden_info.eng_address.province_name},
                             {"ADDRESS_EN_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS_TEXT" ,cinfo.responseModel.garden_info.eng_address.amphur_name},
                             {"ADDRESS_EN_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS_TEXT" ,cinfo.responseModel.garden_info.eng_address.tambon_name},

                         };
                    }
                    else if (cinfo.responseModelBiz.garden_info.address_type.ToString() == "2")
                    {
                        APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION.FormData = new Dictionary<string, object>
                         {
                             {"APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_NAME_ENG_TEXT" ,  cinfo.responseModel.garden_info.eng_name},
                             {"ADDRESS_EN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.address_no},
                             {"ADDRESS_EN_MOO_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.moo},
                             {"ADDRESS_EN_SOI_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.soi},
                             {"ADDRESS_EN_BUILDING_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS" ,cinfo.responseModel.garden_info.eng_address.building},
                             {"ADDRESS_EN_ROOMNO_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS" ,cinfo.responseModel.garden_info.eng_address.room},
                             {"ADDRESS_EN_FLOOR_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.floor},
                             {"ADDRESS_EN_ROAD_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.road},
                             {"ADDRESS_EN_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS" ,cinfo.responseModel.garden_info.eng_address.province_code},
                             {"ADDRESS_EN_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS" , cinfo.responseModel.garden_info.eng_address.amphur_code},
                             {"ADDRESS_EN_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS" ,cinfo.responseModel.garden_info.eng_address.tambon_code},
                             {"ADDRESS_EN_POSTCODE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS" ,cinfo.responseModel.garden_info.eng_address.post_code},
                             {"ADDRESS_EN_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS_TEXT" ,cinfo.responseModel.garden_info.eng_address.province_name},
                             {"ADDRESS_EN_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS_TEXT" ,cinfo.responseModel.garden_info.eng_address.amphur_name},
                             {"ADDRESS_EN_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS_TEXT" , cinfo.responseModel.garden_info.eng_address.tambon_name},

                         };
                    }
                }



                // ///////////////////////////////////////ข้อมูลบุคคลที่สามารถติดต่อได้สะดวก
                string ctel = cinfo.responseModel.contact_tel;
                string[] carrTel = ctel.Split(new[] { "ต่อ" }, StringSplitOptions.None);
                string ctelext = string.Empty;
                if (carrTel.Count() > 1)
                {
                    ctel = arrTel[0];
                    ctelext = arrTel[1];
                }
                string cname = cinfo.responseModel.contact_name;
                string[] carrname = cname.Split(new[] { "    " }, StringSplitOptions.None);
                string clname = string.Empty;
                string ctitleid = string.Empty;
                string ctitle = string.Empty;
                if (carrname.Count() > 1)
                {
                    cname = carrname[0];
                    if (cname.Contains("นาย"))
                    {
                        ctitleid = "01";
                        cname = carrname[0].Replace("นาย", "");
                        ctitle = "นาย";
                    }
                    else if (cname.Contains("นางสาว"))
                    {
                        ctitleid = "02";
                        cname = carrname[0].Replace("นางสาว", "");
                        ctitle = "นางสาว";

                    }
                    else if (cname.Contains("นาง"))
                    {
                        ctitleid = "03";
                        cname = carrname[0].Replace("นาง", "");
                        ctitle = "นาง";
                    }
                    clname = carrname[1];
                }

                var APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION = GetSectionData(model, "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION", SectionType.Form);
                APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION.FormData = new Dictionary<string, object>
                         {

                                 { "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_FIRSTNAME" , cname},
                                 { "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_LASTNAME" , clname},
                                 { "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_TEL" , ctel},
                                 { "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_EXT" , ctelext},
                                 { "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_FAX" , cinfo.responseModel.contact_fax},
                                 { "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_MOBILE" , cinfo.responseModel.contact_mobile},
                                 { "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_EMAIL" , cinfo.responseModel.contact_email},
                                 { "DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_TITLE" , ctitleid},
                                 { "DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_TITLE_TEXT" , ctitle},


                         };


                result = true;
            }
            else
            {
                result = true;
            }

            return result;
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;
            try
            {
                switch (stage)
                {
                    case AppsStage.UserUpdate:
                    case AppsStage.UserCreate:
                        //var post = string.Empty; // Model data
                        string requestval = string.Empty;
                        string requestvalTitle = string.Empty;
                        switch (model.IdentityType)
                        {
                            case UserTypeEnum.Citizen:

                                requestval = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                                requestvalTitle = requestval == "REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER" ? "ขออนุญาตเองโดยเจ้าของกิจการ" : "มอบอำนาจให้ผู้อื่นดำเนินการแทน";

                                break;
                            case UserTypeEnum.Juristic:

                                requestval = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                                requestvalTitle = requestval == "REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD" ? "ขออนุญาตเองโดยเจ้าของกิจการ" : "มอบอำนาจให้ผู้อื่นดำเนินการแทน";

                                break;
                            default:
                                break;
                        }
                        //string requestval = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                        BizPortalInfo bizinfo = new BizPortalInfo();
                        bizinfo.ApplicationID = model.ApplicationID;
                        bizinfo.ApplicationRequestID = model.ApplicationRequestID.ToString();
                        bizinfo.IdentityID = model.IdentityID;
                        bizinfo.IdentityType = model.IdentityType.ToString();
                        bizinfo.IdentityName = model.IdentityName;
                        bizinfo.RequsetDateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", new CultureInfo("en"));
                        bizinfo.RequsetTypeValue = requestval;
                        bizinfo.RequsetTypeName = requestvalTitle;//requestval == "REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER" ? "ขออนุญาตเองโดยเจ้าของกิจการ" : "มอบอำนาจให้ผู้อื่นดำเนินการแทน";
                        bizinfo.RequestAt = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("INFORMATION_HEADER__REQUEST_AT");
                        bizinfo.ApplicationRequestNumber = model.ApplicationRequestNumber;
                        string Consumer_Key = ConfigurationManager.AppSettings["DOA_CONSUMERKEY"];
                        string certificateID = string.Empty;
                        certificateID = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_RENEW_SEARCH_SEARCH_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_SEARCH_SEARCH_SECTION_LICENSE_RENEW");
                        List<DOAFileMetaData> listdocuments = new List<DOAFileMetaData>();
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

                                var file = new DOAFileMetaData
                                {
                                    Name = fileTypeCode,
                                    Content = Convert.ToBase64String(fileMetaModel.GetBytes()),
                                    FileName = f.FileName,
                                    ContentType = f.ContentType,
                                    Description = Description == "" ? fileTypeCode : Description,
                                };
                                listdocuments.Add(file);
                            }
                        }
                        var requestdocumnt = GetOrgPdfFormContent(request, HttpContext.Current.Server.MapPath);
                        var requestdocuments = new List<DOAFileMetaData>
                        {
                            new DOAFileMetaData
                            {
                                Name = "REQUEST_DOCUMENT",
                                FileName = "ใบคำร้อง.pdf",
                                Content = Convert.ToBase64String(requestdocumnt),
                                ContentType = "application/pdf",
                                Description = "ใบคำร้อง"
                            }
                        };
                        var postReNew = new OrganicDetailRenew
                        {
                            ConsumerKey = Consumer_Key,
                            certificateID = certificateID,
                            idcard = model.IdentityID.ToString(),
                            garden_document = listdocuments,
                            garden_document_request = requestdocuments,
                            bizportal_info = bizinfo
                        };
                        string regisUrl = ConfigurationManager.AppSettings["DOA_REQUEST_FARMERRENEW_WS_URL"];
                        var jsonPost = JsonConvert.SerializeObject(postReNew,
                        Newtonsoft.Json.Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
                        string DGA_WS_URL = ConfigurationManager.AppSettings["DGA_WS_URL"];
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

                        using (var response = httpWebRequest.GetResponse() as HttpWebResponse)
                        {
                            if (httpWebRequest.HaveResponse && response != null)
                            {
                                using (var reader = new StreamReader(response.GetResponseStream()))
                                {
                                    var res = reader.ReadToEnd();
                                    if (response.StatusCode == HttpStatusCode.Created)
                                    {
                                        result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, "", response.StatusCode.ToString(), jsonPost);
                                        result.Success = true;
                                    }
                                    else
                                    {
                                        result.Message = response.StatusDescription;
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, result.Message, response.StatusCode.ToString(), jsonPost);
                                        result.Success = false;
                                    }
                                }
                            }
                            else
                            {
                                result.Message = response.StatusDescription;
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, result.Message, response.StatusCode.ToString(), jsonPost);
                                result.Success = false;
                            }
                        }
                        /*Api.OnCheckingApplicationError += (ex) =>
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

                        if (response.HasValues)
                        {
                            result.Success = true;
                        }
                        else
                        {
                            result.Success = false;
                        }*/

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
                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, result.Message, ex.ToString());
                result.Success = false;
            }




            return result;
        }


        private static string getMonthValue(string monthID, string type)
        {
            string monthcode = string.Empty;
            switch (monthID)
            {
                case "1":
                    monthcode = type + "_January";
                    break;
                case "2":
                    monthcode = type + "_February";
                    break;
                case "3":
                    monthcode = type + "_March";
                    break;
                case "4":
                    monthcode = type + "_April";
                    break;
                case "5":
                    monthcode = type + "_May";
                    break;
                case "6":
                    monthcode = type + "_June";
                    break;
                case "7":
                    monthcode = type + "_July";
                    break;
                case "8":
                    monthcode = type + "_August";
                    break;
                case "9":
                    monthcode = type + "_September";
                    break;
                case "10":
                    monthcode = type + "_October";
                    break;
                case "11":
                    monthcode = type + "_November";
                    break;
                case "12":
                    monthcode = type + "_December";
                    break;
                default:

                    break;
            }
            return monthcode;
        }
        private static string getMonthText(string monthID)
        {
            string monthcode = string.Empty;
            switch (monthID)
            {
                case "1":
                    monthcode = "มกราคม";
                    break;
                case "2":
                    monthcode = "กุมภาพันธ์";
                    break;
                case "3":
                    monthcode = "มีนาคม";
                    break;
                case "4":
                    monthcode = "เมษายน";
                    break;
                case "5":
                    monthcode = "พฤษภาคม";
                    break;
                case "6":
                    monthcode = "มิถุนายน";
                    break;
                case "7":
                    monthcode = "กรกฎาคม";
                    break;
                case "8":
                    monthcode = "สิงหาคม";
                    break;
                case "9":
                    monthcode = "กันยายน";
                    break;
                case "10":
                    monthcode = "ตุลาคม";
                    break;
                case "11":
                    monthcode = "พฤศจิกายน";
                    break;
                case "12":
                    monthcode = "ธันวาคม";
                    break;
                default:

                    break;
            }
            return monthcode;
        }
        private (JObject response, OrganicDetail responseModel, OrganicDetail responseModelBiz) getCertificateInfo(string identityID, string certificateNumber)
        {
            //result_bizportal
            JObject response = null;
            Dictionary<string, string> args = null;
            var responseModel = new OrganicDetail();
            var responseModelBiz = new OrganicDetail();


            if (!string.IsNullOrEmpty(certificateNumber) && !string.IsNullOrEmpty(identityID))
            {
                Api.OnCheckingApplicationError += (ex) =>
                {
                    throw ex;
                };
                GetCertificate data = new GetCertificate();
                data.ConsumerKey = ConfigurationManager.AppSettings["DOA_CONSUMERKEY"];
                data.idcard = identityID;
                data.certificateNumber = certificateNumber;

                if (data != null)
                {
                    #region "Frontis: For test purpose only. Delete me once everything is done."
                    bool isTestMode = (ConfigurationManager.AppSettings["TestMode"] + "").ToLower() == "true";
                    if (isTestMode && data.idcard == "1100800933211" && new List<string> { "TAS-55266", "TAS-55265" }.Contains(data.certificateNumber))
                    {
                        data.idcard = "0455561000302";
                    }
                    #endregion

                    var Data = new
                    {
                        data.ConsumerKey,
                        data.idcard,
                        data.certificateNumber
                    };
                    string ws_url = ConfigurationManager.AppSettings["DOA_REQUEST_CERTIFICATE_WS_URL"];
                    var doa_response = Api.Call(ws_url, EGA.WS.HttpVerb.POST, null, JsonConvert.SerializeObject(Data), EGA.WS.ContentType.ApplicationJson);
                    if (doa_response != null && doa_response.HasValues)
                    {
                        //List<ListCertificate> lsc = JsonConvert.DeserializeObject<List<ListCertificate>>(doa_response["result"].ToString());
                        //if (doa_response != null && doa_response.HasValues)
                        //{
                        //    foreach (var obj in lsc)
                        //    {

                        //    }
                        //}
                        response = (JObject)doa_response["result"][0];
                        responseModel = doa_response["result"][0].ToObject<OrganicDetail>();
                        responseModelBiz = doa_response["bizportal_old"][0].ToObject<OrganicDetail>();
                    }
                    else
                    {
                        throw new Exception("ไม่พบข้อมูล");
                    }

                }
            }

            return (response, responseModel, responseModelBiz);
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
            string src = serverMapPathFunc("~/Uploads/apps/ag/AG_Report.pdf");
            List<PDFFieldValue> model = new List<PDFFieldValue>();
            //PDFFieldValue field;

            model.Add(new PDFFieldValue() { FieldName = "chk1", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            model.Add(new PDFFieldValue() { FieldName = "chkAddArea", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            model.Add(new PDFFieldValue() { FieldName = "chkAddPlant", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            model.Add(new PDFFieldValue() { FieldName = "chkDecreaseArea", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            model.Add(new PDFFieldValue() { FieldName = "chkDecreasePlant", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            model.Add(new PDFFieldValue() { FieldName = "chkAdd", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
            model.Add(new PDFFieldValue() { FieldName = "chkDecrease", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });

            var generalInfo = req.Data["GENERAL_INFORMATION"].Data;
            if (req.IdentityType == UserTypeEnum.Citizen)
            {
                model.Add(new PDFFieldValue() { FieldName = "chkShowIDCard", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
                string personID = string.Empty;
                int i = 1;
                model.Add(new PDFFieldValue() { FieldName = "txtJusristict", Value = "------------------------" });
                model.Add(new PDFFieldValue() { FieldName = "txtFullName", Value = string.Format("{0} {1} {2}", generalInfo.TryGetString("DROPDOWN_CITIZEN_TITLE_TEXT", ""), generalInfo.TryGetString("CITIZEN_NAME", ""), generalInfo.TryGetString("CITIZEN_LASTNAME", "")) });
                personID = generalInfo["IDENTITY_ID"].ToString();
                foreach (char c in personID)
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtID1_" + i, Value = c.ToString() });
                    i++;
                }

                var addInfo = req.Data["CITIZEN_ADDRESS_INFORMATION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "txtOffice", Value = "----------------------------------" });
                model.Add(new PDFFieldValue() { FieldName = "txtVillNo", Value = addInfo.TryGetString("ADDRESS_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtVillageName", Value = "-" });
                model.Add(new PDFFieldValue() { FieldName = "txtLane", Value = addInfo.TryGetString("ADDRESS_SOI_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtRoad", Value = addInfo.TryGetString("ADDRESS_ROAD_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtMoo", Value = addInfo.TryGetString("ADDRESS_MOO_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtDistrict", Value = addInfo.TryGetString("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtAmphur", Value = addInfo.TryGetString("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtProvince", Value = addInfo.TryGetString("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtPostCode", Value = addInfo.TryGetString("ADDRESS_POSTCODE_CITIZEN_ADDRESS", "") });
                if (!string.IsNullOrEmpty(addInfo.TryGetString("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS", "")))
                {
                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtTel",
                        Value = addInfo.TryGetString("ADDRESS_TELEPHONE_CITIZEN_ADDRESS", "").ToString()
                        + " ต่อ. " + addInfo.TryGetString("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS", "").ToString()
                    });
                }
                else
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtTel", Value = addInfo.TryGetString("ADDRESS_TELEPHONE_CITIZEN_ADDRESS", "") });
                }
                model.Add(new PDFFieldValue() { FieldName = "txtFax", Value = addInfo.TryGetString("ADDRESS_FAX_CITIZEN_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtMobile", Value = "-" });
                model.Add(new PDFFieldValue() { FieldName = "txtEmail", Value = addInfo.TryGetString("CITIZEN_EMAIL", "") });

                // มอบอำนาจ
                var committee = req.Data["REQUESTOR_INFORMATION__HEADER"].Data;
                if (committee.TryGetString("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION", "").Equals("REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE"))
                {
                    model.Add(new PDFFieldValue() { FieldName = "chkCommitee", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
                }
            }

            else if (req.IdentityType == UserTypeEnum.Juristic)
            {
                model.Add(new PDFFieldValue() { FieldName = "chkShowJurCard", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
                model.Add(new PDFFieldValue() { FieldName = "txtPerson", Value = "--------------------------" });
                model.Add(new PDFFieldValue() { FieldName = "txtFullName", Value = generalInfo.TryGetString("COMPANY_NAME_TH", "") });
                int i = 1;
                foreach (char c in req.IdentityID)
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtID2_" + i, Value = c.ToString() });
                    i++;
                }
                model.Add(new PDFFieldValue() { FieldName = "txtNoneOffice", Value = "----------------------" });
                var addrInfo = req.Data["JURISTIC_ADDRESS_INFORMATION"].Data;
                model.Add(new PDFFieldValue() { FieldName = "txtVillNo", Value = addrInfo.TryGetString("ADDRESS_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtVillageName", Value = "-" });
                model.Add(new PDFFieldValue() { FieldName = "txtLane", Value = addrInfo.TryGetString("ADDRESS_SOI_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtRoad", Value = addrInfo.TryGetString("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtMoo", Value = addrInfo.TryGetString("ADDRESS_MOO_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtDistrict", Value = addrInfo.TryGetString("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtAmphur", Value = addrInfo.TryGetString("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtProvince", Value = addrInfo.TryGetString("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtPostCode", Value = addrInfo.TryGetString("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS", "") });
                if (!string.IsNullOrEmpty(addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS", "")))
                {
                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtTel",
                        Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS", "").ToString()
                        + " ต่อ. " + addrInfo.TryGetString("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS", "").ToString()
                    });
                }
                else
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtTel", Value = addrInfo.TryGetString("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS", "") });
                }
                model.Add(new PDFFieldValue() { FieldName = "txtFax", Value = addrInfo.TryGetString("ADDRESS_FAX_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtMobile", Value = addrInfo.TryGetString("ADDRESS_MOBILE_JURISTIC_HQ_ADDRESS", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtEmail", Value = addrInfo.TryGetString("ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS", "") });

                //มอบอำนาจ
                var committee = req.Data["REQUESTOR_INFORMATION__HEADER"].Data;
                if (committee.TryGetString("REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION", "").Equals("REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE"))
                {
                    model.Add(new PDFFieldValue() { FieldName = "chkCommitee", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
                }
            }

            // ข้อมูลเฉพาะ
            var plnatAddressObj = req.Data["APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION"].Data;
            model.Add(new PDFFieldValue() { FieldName = "txtVillNo2", Value = plnatAddressObj.TryGetString("ADDRESS_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtVillageName2", Value = plnatAddressObj.TryGetString("ADDRESS_VILLAGE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtLane2", Value = plnatAddressObj.TryGetString("ADDRESS_SOI_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtRoad2", Value = plnatAddressObj.TryGetString("ADDRESS_ROAD_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtMoo2", Value = plnatAddressObj.TryGetString("ADDRESS_MOO_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtDistrict2", Value = plnatAddressObj.TryGetString("ADDRESS_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_TEXT", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtAmphur2", Value = plnatAddressObj.TryGetString("ADDRESS_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_TEXT", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtProvince2", Value = plnatAddressObj.TryGetString("ADDRESS_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_TEXT", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtPost2", Value = plnatAddressObj.TryGetString("ADDRESS_POSTCODE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS", "") });
            if (!string.IsNullOrEmpty(plnatAddressObj.TryGetString("ADDRESS_TELEPHONE_EXT_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS", "")))
            {
                model.Add(new PDFFieldValue()
                {
                    FieldName = "txtTel2",
                    Value = plnatAddressObj.TryGetString("ADDRESS_TELEPHONE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS", "").ToString()
                    + " ต่อ. " + plnatAddressObj.TryGetString("ADDRESS_TELEPHONE_EXT_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS", "").ToString()
                });
            }
            else
            {
                model.Add(new PDFFieldValue() { FieldName = "txtTel2", Value = plnatAddressObj.TryGetString("ADDRESS_TELEPHONE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS", "") });
            }
            model.Add(new PDFFieldValue() { FieldName = "txtFax2", Value = plnatAddressObj.TryGetString("ADDRESS_FAX_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS", "") });

            model.Add(new PDFFieldValue() { FieldName = "txtMoblie2", Value = plnatAddressObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_MOBILE", string.Empty) });
            model.Add(new PDFFieldValue() { FieldName = "txtEmail2", Value = plnatAddressObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_EMAIL", string.Empty) });
            model.Add(new PDFFieldValue() { FieldName = "txtATFNbr", Value = plnatAddressObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_ID_TEXT", "").ToString() });

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_1"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF1", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_2"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF2", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_3"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF3", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_4"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF4", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_5"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF5", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_6"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF6", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_7"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF7", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_8"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF8", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_9"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF9", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_FORM_TYPE_10"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF10", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION", "").Equals("OWNED"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkOWN", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION", "").Equals("RENT"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkRENT", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION", "").Equals("RENT_FREE"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkFREE", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetString("DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_SIGNAL_TYPE", "").ToString().Equals("YES"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chk2", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }
            else
            {
                model.Add(new PDFFieldValue() { FieldName = "chk3", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (req.Data.ContainsKey("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION"))
            {
                var intemObj = req.Data["APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION"].Data;
                string strPlant = string.Empty;
                decimal totalArea = 0;
                int j = 0;
                int total = int.Parse(intemObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_TOTAL", "0"));

                while (j < total)
                {
                    strPlant = strPlant + intemObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_TYPE_" + j, "").ToString() + ",";

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtPlantNameArr_" + (j + 1)
                        ,
                        Value = intemObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_TYPE_" + j, "")
                    });

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtAreaSizeArr_" + (j + 1)
                        ,
                        Value = intemObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_AREA_" + j, "")
                    });
                    totalArea = totalArea + Convert.ToDecimal(intemObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_AREA_" + j, ""));

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtPlantQtyArr_" + (j + 1)
                        ,
                        Value = intemObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_TREE_AMOUNT_" + j, "")
                    });

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtCycleQtyArr_" + (j + 1)
                        ,
                        Value = intemObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_YEAR_AMOUNT_" + j, "")
                    });

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtStartHarvestArr_" + (j + 1)
                        ,
                        Value = intemObj.TryGetString("DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_START_MONTH_TEXT_" + j, "")
                    });

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtHarvestDateArr_" + (j + 1)
                        ,
                        Value = intemObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_HARVEST_MONTH_" + j, "")
                    });

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtQtyArr_" + (j + 1)
                        ,
                        Value = intemObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ORGANIC_TYPE_SECTION_ORGANIC_PRODUCE_AMOUNT_" + j, "")
                    });

                    j++;
                }

                strPlant = strPlant.Trim();
                model.Add(new PDFFieldValue() { FieldName = "txtPlantName", Value = strPlant });
                model.Add(new PDFFieldValue() { FieldName = "txtKindOfPlant", Value = j.ToString() });
                model.Add(new PDFFieldValue() { FieldName = "txtTotalAreaSize", Value = totalArea.ToString("n2") });
            }

            if (req.Data.ContainsKey("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2"))
            {
                var docPlant = req.Data["APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2"].Data;

                if (docPlant.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION_FARMER")
                    || docPlant.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION_NAME_THAI"))
                {
                    model.Add(new PDFFieldValue() { FieldName = "chk4", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
                }
                if (docPlant.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION_NAME_ENG"))
                {
                    model.Add(new PDFFieldValue() { FieldName = "chk5", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });

                    model.Add(new PDFFieldValue() { FieldName = "txtEngName", Value = req.Data["APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION"].Data.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_NAME_ENG_TEXT", "") });

                    string addressNo = docPlant.TryGetString("ADDRESS_EN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS", "").ToString();
                    string moo = docPlant.TryGetString("ADDRESS_EN_MOO_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS", "").ToString();
                    string soi = docPlant.TryGetString("ADDRESS_EN_SOI_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS", "").ToString();
                    string building = docPlant.TryGetString("ADDRESS_EN_BUILDING_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS", "").ToString();
                    string room = docPlant.TryGetString("ADDRESS_EN_ROOMNO_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS", "").ToString();
                    string floor = docPlant.TryGetString("ADDRESS_EN_FLOOR_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS", "").ToString();
                    string road = docPlant.TryGetString("ADDRESS_EN_ROAD_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS", "").ToString();
                    string addressStr = string.Empty;

                    if (string.IsNullOrEmpty(addressNo))
                    {
                        addressNo = "- ";
                    }
                    addressStr = addressStr + "Address No: " + addressNo + " ";

                    if (string.IsNullOrEmpty(moo))
                    {
                        moo = "- ";
                    }
                    addressStr = addressStr + "Moo: " + moo + " ";

                    if (string.IsNullOrEmpty(soi))
                    {
                        soi = "- ";
                    }
                    addressStr = addressStr + "Trok/Soi: " + soi + " ";

                    if (string.IsNullOrEmpty(building))
                    {
                        building = "- ";
                    }
                    addressStr = addressStr + "Building: " + building + " ";

                    if (string.IsNullOrEmpty(room))
                    {
                        room = "- ";
                    }
                    addressStr = addressStr + "Room: " + room + " ";

                    if (string.IsNullOrEmpty(floor))
                    {
                        floor = "- ";
                    }
                    addressStr = addressStr + "Floor: " + floor + " ";

                    if (string.IsNullOrEmpty(road))
                    {
                        road = "- ";
                    }
                    addressStr = addressStr + "Road: " + road + " ";
                    model.Add(new PDFFieldValue() { FieldName = "txtEngAddress1", Value = addressStr });
                    addressStr = string.Empty;

                    addressStr = addressStr + "Province: " + docPlant.TryGetString("ADDRESS_EN_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS_TEXT", "").ToString() + " ";
                    addressStr = addressStr + "District/Amphur: " + docPlant.TryGetString("ADDRESS_EN_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS_TEXT", "").ToString() + " ";
                    addressStr = addressStr + "Sub-District/Tumbol: " + docPlant.TryGetString("ADDRESS_EN_TUMBOL_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG_TEXT", "").ToString() + " ";
                    addressStr = addressStr + "Postcode: " + docPlant.TryGetString("ADDRESS_EN_POSTCODE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS", "").ToString() + " ";
                    model.Add(new PDFFieldValue() { FieldName = "txtEngAddress2", Value = addressStr });

                    addressStr = string.Empty;
                    string addressNo2 = docPlant.TryGetString("ADDRESS_EN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS", "").ToString();
                    string moo2 = docPlant.TryGetString("ADDRESS_EN_MOO_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS", "").ToString();
                    string mooBan = docPlant.TryGetString("", "").ToString();
                    string soi2 = docPlant.TryGetString("ADDRESS_EN_SOI_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS", "").ToString();
                    string road2 = docPlant.TryGetString("ADDRESS_EN_ROAD_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS", "").ToString();

                    if (string.IsNullOrEmpty(addressNo2))
                    {
                        addressNo2 = "- ";
                    }
                    addressStr = addressStr + "Address No: " + addressNo2 + " ";

                    if (string.IsNullOrEmpty(moo2))
                    {
                        moo2 = "- ";
                    }
                    addressStr = addressStr + "Moo: " + moo2 + " ";

                    if (string.IsNullOrEmpty(mooBan))
                    {
                        mooBan = "- ";
                    }
                    addressStr = addressStr + "Muban: " + mooBan + " ";

                    if (string.IsNullOrEmpty(soi2))
                    {
                        soi2 = "- ";
                    }
                    addressStr = addressStr + "Trok/Soi: " + soi2 + " ";

                    if (string.IsNullOrEmpty(road2))
                    {
                        road2 = "- ";
                    }
                    addressStr = addressStr + "Trok/Soi: " + road2 + " ";
                    model.Add(new PDFFieldValue() { FieldName = "txtEngPlantAddress1", Value = addressStr });

                    addressStr = string.Empty;
                    addressStr = addressStr + "Province: " + docPlant.TryGetString("ADDRESS_EN_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS_TEXT", "").ToString() + " ";
                    addressStr = addressStr + "District/Amphur: " + docPlant.TryGetString("ADDRESS_EN_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS_TEXT", "").ToString() + " ";
                    addressStr = addressStr + "Sub-District/Tumbol: " + docPlant.TryGetString("ADDRESS_EN_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS_TEXT", "").ToString() + " ";
                    addressStr = addressStr + "Postcode: " + docPlant.TryGetString("ADDRESS_EN_POSTCODE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS", "").ToString() + " ";
                    model.Add(new PDFFieldValue() { FieldName = "txtEngPlantAddress2", Value = addressStr });

                }
            }

            if (req.Data.ContainsKey("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION"))
            {
                var contact = req.Data["APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION"].Data;
                model.Add(new PDFFieldValue()
                {
                    FieldName = "txtContactName",
                    Value = contact.TryGetString("DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_TITLE_TEXT", "").ToString()
                    + " " + contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_FIRSTNAME", "").ToString()
                    + " " + contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_LASTNAME", "").ToString()
                });
                if (string.IsNullOrEmpty(contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_EXT", "").ToString()))
                {
                    model.Add(new PDFFieldValue() { FieldName = "txtContactTel", Value = contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_TEL", "") });
                }
                else
                {
                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtContactTel",
                        Value = contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_TEL", "").ToString()
                    + " " + "ต่อ " + contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_EXT", "").ToString()
                    });
                }

                model.Add(new PDFFieldValue() { FieldName = "txtContactFax", Value = contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_FAX", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtContactMobile", Value = contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_MOBILE", "") });
                model.Add(new PDFFieldValue() { FieldName = "txtContactEmail", Value = contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONTACT_SECTION_CONTACT_EMAIL", "") });
            }

            PDFConfig cfg = new PDFConfig() { FontName = "THSarabunNew.ttf", FontSize = 12 };
            var bytes = ApplyPDFFieldValues(src, model);
            return bytes;
        }

    }
}


