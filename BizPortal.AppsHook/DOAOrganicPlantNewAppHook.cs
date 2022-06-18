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
using BizPortal.Utils.Helpers;

namespace BizPortal.AppsHook
{
    public class DOAOrganicPlantNewAppHook : SingleFormRendererAppHook
    {
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

        public override bool IsEnabledChat() => false;

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

                        string Consumer_Key = ConfigurationManager.AppSettings["DOA_CONSUMERKEY"];
                        string idcardType = string.Empty;
                        string contactName = string.Empty;
                        string email = string.Empty;
                        string prefixname = string.Empty;
                        string name = string.Empty;
                        string surname = string.Empty;
                        string village_no = string.Empty;
                        string lane = string.Empty;
                        string moo = string.Empty;
                        string road = string.Empty;
                        string district_id = string.Empty;
                        string amphur_id = string.Empty;
                        string province_id = string.Empty;
                        string province_name = string.Empty;
                        string amphur_name = string.Empty;
                        string tambon_name = string.Empty;
                        string postcode = string.Empty;
                        string tel = string.Empty;
                        string mobile = string.Empty;
                        string fax = string.Empty;
                        string hallmark = string.Empty;
                        bool request_hallmark = false;
                        string requestType = string.Empty;
                        bool request_certificate_1 = false;
                        bool request_certificate_2 = false;
                        bool request_certificate_3 = false;
                        request_certificate_1 = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_1") == "true" ? true : false;
                        request_certificate_2 = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_2") == "true" ? true : false;
                        request_certificate_3 = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_3") == "true" ? true : false;
                        //request_certificate_4 = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_4") == "true" ? true : false;

                        //reUrl = string.Format("{0}/th/BackOffice/ApplicationStatus/PrintAGPDF/{1}", System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), model.ApplicationRequestNumber.ToString());

                        requestType = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_CONFIRM_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_KIND_OF_PLANT_OPTION");
                        //1 แปลงเดี่ยว/รายเดียว            2 กลุ่ม
                        hallmark = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_ORG_AG");
                        //1 ต้องการขอใช้เครื่องหมายผลิตภัณฑ์  2 ไม่ต้องการ
                        request_hallmark = (hallmark == "1" ? true : false); //ต้องการขอใช้เครื่องหมายผลิตภัณฑ์?

                        // ContactName
                        contactName = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION").ThenGetStringData("DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_TITLE_TEXT");
                        contactName += model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_FIRST_NAME");
                        contactName += "    ";
                        contactName += model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_LAST_NAME");


                        string requestval = string.Empty;
                        string requestvalTitle = string.Empty;
                        switch (model.IdentityType)
                        {
                            case UserTypeEnum.Citizen:

                                requestval = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                                requestvalTitle = requestval == "REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER" ? "ขออนุญาตเองโดยเจ้าของกิจการ" : "มอบอำนาจให้ผู้อื่นดำเนินการแทน";
                                email = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("CITIZEN_EMAIL");
                                prefixname = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_TITLE_TEXT");
                                name = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_NAME");
                                surname = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME");
                                village_no = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_CITIZEN_ADDRESS");
                                lane = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS");
                                moo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS");
                                road = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS");
                                district_id = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS");
                                amphur_id = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS");
                                province_id = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS");
                                postcode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS");
                                tel = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS");
                                if (!string.IsNullOrEmpty(model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS")))
                                    tel = tel + " ต่อ " + model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS");
                                mobile = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOBILE_CITIZEN_ADDRESS");
                                fax = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_CITIZEN_ADDRESS");
                                idcardType = DOAidcardTyep.personal.GetEnumStringValue();

                                province_name = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT");
                                amphur_name = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT");
                                tambon_name = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT");

                                break;
                            case UserTypeEnum.Juristic:

                                requestval = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                                requestvalTitle = requestval == "REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD" ? "ขออนุญาตเองโดยเจ้าของกิจการ" : "มอบอำนาจให้ผู้อื่นดำเนินการแทน";
                                idcardType = DOAidcardTyep.company.GetEnumStringValue();
                                name = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_TH");
                                village_no = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS");
                                lane = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS");
                                moo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS");
                                road = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS");
                                district_id = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS");
                                amphur_id = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS");
                                province_id = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS");
                                postcode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS");
                                tel = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS");
                                if (!string.IsNullOrEmpty(model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS")))
                                    tel = tel + " ต่อ " + model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS");
                                mobile = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOBILE_JURISTIC_HQ_ADDRESS");
                                fax = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_JURISTIC_HQ_ADDRESS");

                                email = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS");

                                province_name = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT");
                                amphur_name = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT");
                                tambon_name = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT");


                                break;
                            default:
                                break;
                        }
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD  ขออนุญาตเองโดยเจ้าของกิจการ
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE มอบอำนาจให้ผู้อื่นดำเนินการแทน

                        //CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE
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

                        //REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER      ขออนุญาตเองโดยเจ้าของกิจการ
                        //REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE    มอบอำนาจให้ผู้อื่นดำเนินการแทน

                        /*
                         * 
                         * email = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS");
                        village_no = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS");
                        lane = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS");
                        moo = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS");
                        road = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS");
                        district_id = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS");
                        amphur_id = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS");
                        province_id = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS");
                        postcode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS");
                        mobile = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOBILE_INFORMATION_STORE__ADDRESS");
                        tel = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS");
                        if (string.IsNullOrEmpty(model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS")))
                            tel = tel + " ต่อ " + model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS");
                        fax = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS");*/


                        List<GardenItem> ListGardenItems = new List<GardenItem>();
                        double totalarea_size = 0;
                        double area_size = 0;
                        int countitem = Convert.ToInt16(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION_TOTAL"));
                        for (int i = 0; i < countitem; i++)
                        {
                            area_size = Convert.ToDouble(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_AREA_SIZE_" + i));
                            totalarea_size = totalarea_size + area_size;
                            GardenItem GardenItems = new GardenItem();
                            GardenItems.plant_id = Convert.ToInt16(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_KIND_OF_PLANT_" + i));
                            GardenItems.area_size = area_size;
                            double numberOfTree = 0;
                            GardenItems.plant_qty =  double.TryParse(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_NUMBER_OF_TREE_" + i), out numberOfTree) ? numberOfTree : 0;
                            GardenItems.cycle_qty = Convert.ToInt16(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_NUMBER_OF_YEAR_" + i));
                            GardenItems.start_harvest = Convert.ToInt16(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION").ThenGetStringData("DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_RANK_START_" + i));
                            GardenItems.end_harvest = Convert.ToInt16(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION").ThenGetStringData("DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_RANK_STOP_" + i));
                            GardenItems.date_harvest = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_ESTIMATE_HARVEST_" + i);
                            GardenItems.qty = Convert.ToDouble(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_ESTIMATE_VOLUME_OF_HARVEST_" + i));
                            ListGardenItems.Add(GardenItems);
                        }

                        GardenInfo garden = new GardenInfo();
                        garden.address_type = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_LOCATION_TYPE_OPT");
                        garden.area_size = totalarea_size.ToString();
                        garden.plant_type_id = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("AJAX_DROPDOWN_APP_ORGRANIC_KIND_OF_PRODUCTION");
                        garden.village_no = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                        garden.lane = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_SOI_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                        garden.moo = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_MOO_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                        garden.road = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_ROAD_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                        garden.district_id = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                        garden.amphur_id = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                        garden.province_id = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                        garden.postcode = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_POSTCODE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                        garden.tel = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                        if (!string.IsNullOrEmpty(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM")))
                            garden.tel = garden.tel + " ต่อ " + model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                        garden.mobile = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM_MOBILE");
                        garden.fax = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_FAX_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                        garden.email = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM_EMAL");
                        garden.request_hallmark = request_hallmark;
                        garden.request_certificate_type_1 = request_certificate_1;
                        garden.request_certificate_type_2 = request_certificate_2;
                        garden.request_certificate_type_3 = request_certificate_3;
                        //garden.request_certificate_type_4 = request_certificate_4;
                        // garden.certificate_type = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_CONFIRM_SECTION").ThenGetStringData("ADDRESS_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");

                        garden.th_name = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_FULL_NAME_THAI");
                        garden.eng_name = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_ENG_FULL_NAME");
                        document_detail docdt = new document_detail();
                        docdt.is_check = Convert.ToBoolean(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK1"));
                        docdt.description = DoaTypeDocument.chk1.GetEnumStringValue();
                        garden.doc_type_1 = docdt;
                        docdt = new document_detail();
                        docdt.is_check = Convert.ToBoolean(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK2"));
                        docdt.description = DoaTypeDocument.chk2.GetEnumStringValue();
                        garden.doc_type_2 = docdt;
                        docdt = new document_detail();
                        docdt.is_check = Convert.ToBoolean(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK3"));
                        docdt.description = DoaTypeDocument.chk3.GetEnumStringValue();
                        garden.doc_type_3 = docdt;
                        docdt = new document_detail();
                        docdt.is_check = Convert.ToBoolean(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK4"));
                        docdt.description = DoaTypeDocument.chk4.GetEnumStringValue();
                        garden.doc_type_4 = docdt;
                        docdt = new document_detail();
                        docdt.is_check = Convert.ToBoolean(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK5"));
                        docdt.description = DoaTypeDocument.chk5.GetEnumStringValue();
                        garden.doc_type_5 = docdt;
                        docdt = new document_detail();
                        docdt.is_check = Convert.ToBoolean(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK6"));
                        docdt.description = DoaTypeDocument.chk6.GetEnumStringValue();
                        garden.doc_type_6 = docdt;
                        docdt = new document_detail();
                        docdt.is_check = Convert.ToBoolean(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK7"));
                        docdt.description = DoaTypeDocument.chk7.GetEnumStringValue();
                        garden.doc_type_7 = docdt;
                        docdt = new document_detail();
                        docdt.is_check = Convert.ToBoolean(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK8"));
                        docdt.description = DoaTypeDocument.chk8.GetEnumStringValue();
                        garden.doc_type_8 = docdt;
                        docdt = new document_detail();
                        docdt.is_check = Convert.ToBoolean(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK9"));
                        docdt.description = DoaTypeDocument.chk9.GetEnumStringValue();
                        garden.doc_type_9 = docdt;
                        docdt = new document_detail();
                        docdt.is_check = Convert.ToBoolean(model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK10"));
                        docdt.description = DoaTypeDocument.chk10.GetEnumStringValue();
                        garden.doc_type_10 = docdt;
                        garden.doc_type_description = (model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_TEXT"));





                        doa_address location_address_th = new doa_address();
                        doa_address location_address_en = new doa_address();

                        if (garden.address_type == "1") //lสำนักงาน/ตามบัตร
                        {

                            location_address_th.address_no = village_no;
                            location_address_th.moo = moo;
                            //location_address_th.village =;
                            location_address_th.soi = lane;
                            location_address_th.road = road;
                            location_address_th.province_code = province_id;
                            location_address_th.amphur_code = amphur_id;
                            location_address_th.tambon_code = district_id;
                            location_address_th.post_code = postcode;
                            location_address_th.province_name = province_name;
                            location_address_th.amphur_name = amphur_name;
                            location_address_th.tambon_name = tambon_name;


                            garden.th_address = location_address_th;

                            location_address_en.address_no = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG");
                            location_address_en.moo = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_MOO_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG");
                            location_address_en.soi = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_SOI_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG");
                            location_address_en.building = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_BUILDING_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG");
                            location_address_en.room = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_ROOMNO_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG");
                            location_address_en.floor = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_FLOOR_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG");
                            location_address_en.road = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_ROAD_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG");
                            location_address_en.province_code = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_PROVINCE_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG");
                            location_address_en.province_name = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_PROVINCE_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG_TEXT");
                            location_address_en.amphur_code = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_AMPHUR_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG");
                            location_address_en.amphur_name = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_AMPHUR_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG_TEXT");
                            location_address_en.tambon_code = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_TUMBOL_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG");
                            location_address_en.tambon_name = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_TUMBOL_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG_TEXT");
                            location_address_en.post_code = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_POSTCODE_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG");
                            garden.eng_address = location_address_en;




                        }
                        else if (garden.address_type == "2")//ตามที่ตั้ง
                        {
                            location_address_th.address_no = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                            location_address_th.moo = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_MOO_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                            location_address_th.village = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_VILLAGE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                            location_address_th.soi = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_SOI_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                            location_address_th.road = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_ROAD_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                            location_address_th.province_code = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                            location_address_th.province_name = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM_TEXT");
                            location_address_th.amphur_code = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                            location_address_th.amphur_name = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM_TEXT");
                            location_address_th.tambon_code = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                            location_address_th.tambon_name = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM_TEXT");
                            location_address_th.post_code = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION").ThenGetStringData("ADDRESS_POSTCODE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM");
                            garden.th_address = location_address_th;

                            location_address_en.address_no = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_APP_ORGANIC_PLANT_LOCATION_FORM");
                            location_address_en.moo = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_MOO_APP_ORGANIC_PLANT_LOCATION_FORM");
                            location_address_en.soi = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_SOI_APP_ORGANIC_PLANT_LOCATION_FORM");
                            location_address_en.building = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_BUILDING_APP_ORGANIC_PLANT_LOCATION_FORM");
                            location_address_en.room = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_ROOMNO_APP_ORGANIC_PLANT_LOCATION_FORM");
                            location_address_en.floor = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_FLOOR_APP_ORGANIC_PLANT_LOCATION_FORM");
                            location_address_en.road = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_ROAD_APP_ORGANIC_PLANT_LOCATION_FORM");
                            location_address_en.province_code = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_PROVINCE_APP_ORGANIC_PLANT_LOCATION_FORM");
                            location_address_en.province_name = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_PROVINCE_APP_ORGANIC_PLANT_LOCATION_FORM_TEXT");
                            location_address_en.amphur_code = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_AMPHUR_APP_ORGANIC_PLANT_LOCATION_FORM");
                            location_address_en.amphur_name = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_AMPHUR_APP_ORGANIC_PLANT_LOCATION_FORM_TEXT");
                            location_address_en.tambon_code = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_TUMBOL_APP_ORGANIC_PLANT_LOCATION_FORM");
                            location_address_en.tambon_name = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_TUMBOL_APP_ORGANIC_PLANT_LOCATION_FORM_TEXT");
                            location_address_en.post_code = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION").ThenGetStringData("ADDRESS_EN_POSTCODE_APP_ORGANIC_PLANT_LOCATION_FORM");
                            garden.eng_address = location_address_en;

                        }

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

                        var postinformations = new OrganicDetail()
                        {
                            ConsumerKey = Consumer_Key,
                            farmer_group_id = null,
                            idcard = model.IdentityID.ToString(),//model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("IDENTITY_ID"),
                            idcardType = idcardType,
                            prefixname = prefixname,
                            name = name,
                            surname = surname,
                            contact_name = contactName,
                            contact_mobile = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_MOBILE_PHONE"),
                            contact_tel = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_TELL"),
                            contact_fax = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_FAX"),
                            contact_email = model.Data.TryGetData("APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION").ThenGetStringData("APP_ORGANIC_PLANT_PRODUCTION_NEW_EMAIL"),
                            village_no = village_no,
                            lane = lane,
                            moo = moo,
                            road = road,
                            district_id = district_id,
                            amphur_id = amphur_id,
                            province_id = province_id,
                            postcode = postcode,
                            tel = tel,
                            mobile = mobile,
                            fax = fax,
                            email = email,
                            //document_url = reUrl,
                            garden_info = garden,
                            garden_item = ListGardenItems,
                            garden_document = listdocuments,
                            garden_document_request = requestdocuments,
                            bizportal_info = bizinfo
                        };


                        string regisUrl = ConfigurationManager.AppSettings["DOA_REQUEST_FARMER_WS_URL"];
                        var jsonPost = JsonConvert.SerializeObject(postinformations,
                        Newtonsoft.Json.Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });


                        /* var client = new RestClient("https://api.egov.go.th/ws/doa/request/farmer");
                         var request_ = new RestRequest(Method.POST);
                         request_.RequestFormat = DataFormat.Json;
                         request_.AddHeader("Token", Api.AccessToken);
                         request_.AddHeader("Consumer-Key", Api.ConsumerKey);
                         request_.AddHeader("Content-Type", "application/json");

                         request_.AddBody(postinformations);

                         IRestResponse resp = client.Execute(request_);
                         if (resp.StatusCode == HttpStatusCode.OK)
                         {
                             //RootObject opt = JsonConvert.DeserializeObject<RootObject>(resp.Content.ToString());
                             //foreach (var obj in opt.result)
                             //{
                             //    opts.Add(new Select2Opt() { ID = obj.id, Text = obj.name });
                             //}

                         }




                         */
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

        public override byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc)
        {
            string src = serverMapPathFunc("~/Uploads/apps/ag/AG_Report.pdf");
            List<PDFFieldValue> model = new List<PDFFieldValue>();
            //PDFFieldValue field;

            // Uncheck other options
            model.Add(new PDFFieldValue() { FieldName = "chkRenew", Value = "Off", FieldType = PDFFieldValue.eFieldType.Checkbox });
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
                personID = generalInfo.TryGetString("IDENTITY_ID", "").ToString();
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
                if (committee.TryGetString("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION", "").ToString().Equals("REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE"))
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
                foreach (char c in generalInfo.TryGetString("IDENTITY_ID", "").ToString())
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
                if (committee.TryGetString("REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION", "").ToString().Equals("REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE"))
                {
                    model.Add(new PDFFieldValue() { FieldName = "chkCommitee", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
                }
            }

            // ข้อมูลเฉพาะ
            var plnatAddressObj = req.Data["APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION"].Data;
            model.Add(new PDFFieldValue() { FieldName = "txtVillNo2", Value = plnatAddressObj.TryGetString("ADDRESS_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtVillageName2", Value = plnatAddressObj.TryGetString("ADDRESS_VILLAGE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtLane2", Value = plnatAddressObj.TryGetString("ADDRESS_SOI_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtRoad2", Value = plnatAddressObj.TryGetString("ADDRESS_ROAD_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtMoo2", Value = plnatAddressObj.TryGetString("ADDRESS_MOO_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtDistrict2", Value = plnatAddressObj.TryGetString("ADDRESS_TUMBOL_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM_TEXT", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtAmphur2", Value = plnatAddressObj.TryGetString("ADDRESS_AMPHUR_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM_TEXT", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtProvince2", Value = plnatAddressObj.TryGetString("ADDRESS_PROVINCE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM_TEXT", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtPost2", Value = plnatAddressObj.TryGetString("ADDRESS_POSTCODE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM", "") });
            if (!string.IsNullOrEmpty(plnatAddressObj.TryGetString("ADDRESS_TELEPHONE_EXT_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM", "")))
            {
                model.Add(new PDFFieldValue()
                {
                    FieldName = "txtTel2",
                    Value = plnatAddressObj.TryGetString("ADDRESS_TELEPHONE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM", "").ToString()
                    + " ต่อ. " + plnatAddressObj.TryGetString("ADDRESS_TELEPHONE_EXT_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM", "").ToString()
                });
            }
            else
            {
                model.Add(new PDFFieldValue() { FieldName = "txtTel2", Value = plnatAddressObj.TryGetString("ADDRESS_TELEPHONE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM", "") });
            }
            model.Add(new PDFFieldValue() { FieldName = "txtFax2", Value = plnatAddressObj.TryGetString("ADDRESS_FAX_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM", "") });

            model.Add(new PDFFieldValue() { FieldName = "txtMoblie2", Value = plnatAddressObj.TryGetString("ADDRESS_MOBILE_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM", string.Empty) });
            model.Add(new PDFFieldValue() { FieldName = "txtEmail2", Value = plnatAddressObj.TryGetString("ADDRESS_EMAIL_APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM", string.Empty) });
            model.Add(new PDFFieldValue() { FieldName = "txtATFNbr", Value = plnatAddressObj.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_TEXT", "").ToString() });

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK1"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF1", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK2"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF2", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK3"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF3", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK4"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF4", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK5"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF5", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK6"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF6", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK7"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF7", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK8"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF8", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK9"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF9", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT_CHK10"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkATF10", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetString("APP_ORGANIC_PLANT_INFORMATION_BUILDING_TYPE", "").ToString().Equals("1"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkOWN", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetString("APP_ORGANIC_PLANT_INFORMATION_BUILDING_TYPE", "").ToString().Equals("2"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkRENT", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetString("APP_ORGANIC_PLANT_INFORMATION_BUILDING_TYPE", "").ToString().Equals("3"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chkFREE", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            if (plnatAddressObj.TryGetString("DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_ORG_AG", "").ToString().Equals("1"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chk2", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }
            else
            {
                model.Add(new PDFFieldValue() { FieldName = "chk3", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }

            var intemObj = req.Data["APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION"].Data;
            string strPlant = string.Empty;
            decimal totalArea = 0;
            int j = 0;
            try
            {
                while (1 == 1)
                {
                    strPlant = strPlant + intemObj["AJAX_DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_KIND_OF_PLANT_TEXT_" + j].ToString() + ",";

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtPlantNameArr_" + (j + 1)
                        ,
                        Value = intemObj["AJAX_DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_KIND_OF_PLANT_TEXT_" + j]
                    });

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtAreaSizeArr_" + (j + 1)
                        ,
                        Value = intemObj["APP_ORGANIC_PLANT_PRODUCTION_NEW_AREA_SIZE_" + j]
                    });
                    totalArea = totalArea + Convert.ToDecimal(intemObj["APP_ORGANIC_PLANT_PRODUCTION_NEW_AREA_SIZE_" + j]);

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtPlantQtyArr_" + (j + 1)
                        ,
                        Value = intemObj["APP_ORGANIC_PLANT_PRODUCTION_NEW_NUMBER_OF_TREE_" + j]
                    });

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtCycleQtyArr_" + (j + 1)
                        ,
                        Value = intemObj["APP_ORGANIC_PLANT_PRODUCTION_NEW_NUMBER_OF_YEAR_" + j]
                    });

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtStartHarvestArr_" + (j + 1)
                        ,
                        Value = intemObj["DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_RANK_START_TEXT_" + j].ToString() + " - "
                        + intemObj["DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_RANK_STOP_TEXT_" + j].ToString()
                    });

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtHarvestDateArr_" + (j + 1)
                        ,
                        Value = intemObj["APP_ORGANIC_PLANT_PRODUCTION_NEW_ESTIMATE_HARVEST_" + j]
                    });

                    model.Add(new PDFFieldValue()
                    {
                        FieldName = "txtQtyArr_" + (j + 1)
                        ,
                        Value = intemObj["APP_ORGANIC_PLANT_PRODUCTION_NEW_ESTIMATE_VOLUME_OF_HARVEST_" + j]
                    });

                    j++;
                }
            }
            catch
            {
                strPlant = strPlant.Substring(0, strPlant.Length - 1);
                model.Add(new PDFFieldValue() { FieldName = "txtPlantName", Value = strPlant });
                model.Add(new PDFFieldValue() { FieldName = "txtKindOfPlant", Value = j.ToString() });
                model.Add(new PDFFieldValue() { FieldName = "txtTotalAreaSize", Value = totalArea.ToString("n2") });
            }

            var docPlant = req.Data["APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION"].Data;

            if (docPlant.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_1") || docPlant.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_2"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chk4", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });
            }
            if (docPlant.TryGetBool("APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_3"))
            {
                model.Add(new PDFFieldValue() { FieldName = "chk5", Value = "Yes", FieldType = PDFFieldValue.eFieldType.Checkbox });

                string addressNo = docPlant.TryGetString("ADDRESS_EN_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG", "").ToString();
                string moo = docPlant.TryGetString("ADDRESS_EN_MOO_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG", "").ToString();
                string soi = docPlant.TryGetString("ADDRESS_EN_SOI_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG", "").ToString();
                string building = docPlant.TryGetString("ADDRESS_EN_BUILDING_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG", "").ToString();
                string room = docPlant.TryGetString("ADDRESS_EN_ROOMNO_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG", "").ToString();
                string floor = docPlant.TryGetString("ADDRESS_EN_FLOOR_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG", "").ToString();
                string road = docPlant.TryGetString("ADDRESS_EN_ROAD_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG", "").ToString();
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

                addressStr = addressStr + "Province: " + docPlant.TryGetString("ADDRESS_EN_PROVINCE_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG_TEXT", "").ToString() + " ";
                addressStr = addressStr + "District/Amphur: " + docPlant.TryGetString("ADDRESS_EN_AMPHUR_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG_TEXT", "").ToString() + " ";
                addressStr = addressStr + "Sub-District/Tumbol: " + docPlant.TryGetString("ADDRESS_EN_TUMBOL_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG_TEXT", "").ToString() + " ";
                addressStr = addressStr + "Postcode: " + docPlant.TryGetString("ADDRESS_EN_POSTCODE_APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG", "").ToString() + " ";
                model.Add(new PDFFieldValue() { FieldName = "txtEngAddress2", Value = addressStr });

                addressStr = string.Empty;
                string addressNo2 = docPlant.TryGetString("ADDRESS_EN_APP_ORGANIC_PLANT_LOCATION_FORM", "").ToString();
                string moo2 = docPlant.TryGetString("ADDRESS_EN_MOO_APP_ORGANIC_PLANT_LOCATION_FORM", "").ToString();
                string mooBan = docPlant.TryGetString("ADDRESS_MUBAN_EN_APP_ORGANIC_PLANT_LOCATION_FORM", "").ToString();
                string soi2 = docPlant.TryGetString("ADDRESS_EN_SOI_APP_ORGANIC_PLANT_LOCATION_FORM", "").ToString();
                string road2 = docPlant.TryGetString("ADDRESS_EN_ROAD_APP_ORGANIC_PLANT_LOCATION_FORM", "").ToString();

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
                addressStr = addressStr + "Province: " + docPlant.TryGetString("ADDRESS_EN_PROVINCE_APP_ORGANIC_PLANT_LOCATION_FORM_TEXT", "").ToString() + " ";
                addressStr = addressStr + "District/Amphur: " + docPlant.TryGetString("ADDRESS_EN_AMPHUR_APP_ORGANIC_PLANT_LOCATION_FORM_TEXT", "").ToString() + " ";
                addressStr = addressStr + "Sub-District/Tumbol: " + docPlant.TryGetString("ADDRESS_EN_TUMBOL_APP_ORGANIC_PLANT_LOCATION_FORM_TEXT", "").ToString() + " ";
                addressStr = addressStr + "Postcode: " + docPlant.TryGetString("ADDRESS_EN_POSTCODE_APP_ORGANIC_PLANT_LOCATION_FORM", "").ToString() + " ";
                model.Add(new PDFFieldValue() { FieldName = "txtEngPlantAddress2", Value = addressStr });
                model.Add(new PDFFieldValue() { FieldName = "txtEngName", Value = docPlant["APP_ORGANIC_PLANT_PRODUCTION_ENG_FULL_NAME"] });
            }

            var contact = req.Data["APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION"].Data;
            model.Add(new PDFFieldValue()
            {
                FieldName = "txtContactName",
                Value = contact.TryGetString("DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_TITLE_TEXT", "").ToString()
                + " " + contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_NEW_FIRST_NAME", "").ToString()
                + " " + contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_NEW_LAST_NAME", "").ToString()
            });
            if (string.IsNullOrEmpty(contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_NEW_TELL_NEXT", "").ToString()))
            {
                model.Add(new PDFFieldValue() { FieldName = "txtContactTel", Value = contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_NEW_TELL", "") });
            }
            else
            {
                model.Add(new PDFFieldValue()
                {
                    FieldName = "txtContactTel",
                    Value = contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_NEW_TELL", "").ToString()
                + " " + "ต่อ " + contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_NEW_TELL_NEXT", "").ToString()
                });
            }

            model.Add(new PDFFieldValue() { FieldName = "txtContactFax", Value = contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_NEW_FAX", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtContactMobile", Value = contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_NEW_MOBILE_PHONE", "") });
            model.Add(new PDFFieldValue() { FieldName = "txtContactEmail", Value = contact.TryGetString("APP_ORGANIC_PLANT_PRODUCTION_NEW_EMAIL", "") });

            //PDFConfig cfg = new PDFConfig() { FontName = "THSarabunNew.ttf", FontSize = 12 };
            var bytes = ApplyPDFFieldValues(src, model);
            return bytes;
        }

    }

}
