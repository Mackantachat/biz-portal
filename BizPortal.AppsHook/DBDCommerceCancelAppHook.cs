using BizPortal.DAL.MongoDB;
using BizPortal.Utils;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Apps.DBDStandard;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Helpers;
using EGA.WS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using BizPortal.Integrated;

namespace BizPortal.AppsHook
{
    public class DBDCommerceCancelAppHook : SingleFormRendererAppHook
    {
        decimal Total_ShareAmout;
        decimal ShareAmout;
        decimal ShareValue;
        decimal budgetAmt;

        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public override bool IsEnabledChat() => false;

        public override bool InvokeSingleForm(Guid trid, string currentSectionGroup, ref SingleFormRequestViewModel model)
        {
            var result = false;
            var darftData = SingleFormRequestEntity.Get(model.IdentityID);

            if (currentSectionGroup == "APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH")
            {
                // Clear section data
                var singleformReq = new SingleFormRequestEntity();
                singleformReq.DeleteSections(model.IdentityID, "APP_ELECTRONIC_COMMERCIAL_EDIT", new string[] { "REQUESTOR_INFORMATION__HEADER", "INFORMATION_STORE" });

                // Clear prefill data
                var preFillData = new SingleFormPreFillData();
                preFillData.Delete(model.IdentityID);

                result = true;
            }
            else if (currentSectionGroup == "INFORMATION")
            {
                var cinfo = getCommerceInfo(model);
                string CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION = "REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER";
                string REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION = "REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD";

                if (model.IdentityType.Equals(UserTypeEnum.Citizen))
                {
                    foreach (var attach in cinfo.responseModel.attachs)
                    {
                        if (("" + attach.fileType).Equals("9"))
                        {
                            CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION = "REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE";
                        }
                    }
                }
                else
                {
                    foreach (var attach in cinfo.responseModel.attachs)
                    {
                        if (("" + attach.fileType).Equals("6"))
                        {
                            REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION = "REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE";
                        }

                    }
                }

                //General Information
                #region Citizen
                if (model.IdentityType.Equals(UserTypeEnum.Citizen))
                {
                    //ข้อมูลบุคคลผู้ขออนุญาต
                    //Citizen General Information
                    var OwnerGeneralInfo = GetSectionData(model, "GENERAL_INFORMATION", SectionType.Form);
                    //OwnerGeneralInfo.FormData = new Dictionary<string, object>
                    //{
                    //    {"AJAX_DROPDOWN_CITIZEN_TITLE",cinfo.responseModel.owner.titleCode},
                    //    {"AJAX_DROPDOWN_CITIZEN_TITLE_TEXT",NationalityService.GetTitleTextThai(cinfo.responseModel.owner.titleCode)},

                    //    {"CITIZEN_FIRSTNAME_EN",cinfo.responseModel.owner.firstNameEN},
                    //    {"CITIZEN_LASTNAME_EN",cinfo.responseModel.owner.lastNameEN},
                    //    {"BIRTH_DATE",DBDUtility.GetDateFormat(cinfo.responseModel.owner.birthDate) },
                    //    {"GENERAL_INFORMATION__CITIZEN_AGE",cinfo.responseModel.owner.age},

                    //    {"AJAX_DROPDOWN_CITIZEN_TITLE_EN",cinfo.responseModel.owner.titleCode},
                    //    {"AJAX_DROPDOWN_CITIZEN_TITLE_EN_TEXT",NationalityService.GetTitleTextEng(cinfo.responseModel.owner.titleCode)},

                    //    {"DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY",cinfo.responseModel.owner.nationCode},
                    //    {"DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT",NationalityService.GetNationText(cinfo.responseModel.owner.nationCode)},

                    //    {"DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE",cinfo.responseModel.owner.race},
                    //    {"DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE_TEXT",NationalityService.GetNationalityText(cinfo.responseModel.owner.race)},

                    //};
                    //Citizen Address
                    var OwnerAddressInfo = GetSectionData(model, "CITIZEN_ADDRESS_INFORMATION", SectionType.Form);
                    //OwnerAddressInfo.FormData = new Dictionary<string, object>
                    //{
                    //    { "ADDRESS_CITIZEN_ADDRESS",cinfo.responseModel.owner.houseNo},
                    //    { "ADDRESS_MOO_CITIZEN_ADDRESS",cinfo.responseModel.owner.moo},
                    //    { "ADDRESS_SOI_CITIZEN_ADDRESS",cinfo.responseModel.owner.soi},
                    //    { "ADDRESS_BUILDING_CITIZEN_ADDRESS",cinfo.responseModel.owner.building},
                    //    { "ADDRESS_ROOMNO_CITIZEN_ADDRESS",cinfo.responseModel.owner.roomNo},
                    //    { "ADDRESS_FLOOR_CITIZEN_ADDRESS",cinfo.responseModel.owner.buildingFloor},
                    //    { "ADDRESS_ROAD_CITIZEN_ADDRESS",cinfo.responseModel.owner.road},
                    //    { "ADDRESS_PROVINCE_CITIZEN_ADDRESS",cinfo.responseModel.owner.provinceCode},
                    //    { "ADDRESS_AMPHUR_CITIZEN_ADDRESS",cinfo.responseModel.owner.amphurCode},
                    //    { "ADDRESS_TUMBOL_CITIZEN_ADDRESS",cinfo.responseModel.owner.tumbonCode},
                    //    { "ADDRESS_POSTCODE_CITIZEN_ADDRESS",cinfo.responseModel.owner.postCode},
                    //    { "ADDRESS_TELEPHONE_CITIZEN_ADDRESS",cinfo.responseModel.owner.telephone},
                    //    { "ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS",cinfo.responseModel.owner.telephone_ext},
                    //    { "ADDRESS_FAX_CITIZEN_ADDRESS",cinfo.responseModel.owner.fax},
                    //    { "ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT",GeoService.GetProvinceText(cinfo.responseModel.owner.provinceCode)},
                    //    { "ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT",GeoService.GetAmphurText(cinfo.responseModel.owner.provinceCode,cinfo.responseModel.owner.amphurCode)},
                    //    { "ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT",GeoService.GetTambolText(cinfo.responseModel.owner.provinceCode,cinfo.responseModel.owner.amphurCode,cinfo.responseModel.owner.tumbonCode)},
                    //};
                }
                #endregion
                #region Juristic
                else
                {
                    //ข้อมูลนิติบุคคล
                    //Juristic General Information
                    var OwnerGeneralInfo = GetSectionData(model, "GENERAL_INFORMATION", SectionType.Form);
                    //OwnerGeneralInfo.FormData = new Dictionary<string, object>
                    //{
                    //    {"INFORMATION_HEADER__REQUEST_DATE",""},
                    //    {"INFORMATION_HEADER__REQUEST_AT",""},
                    //    {"INFORMATION__REQUEST_AS_OPTION",""},
                    //    {"COMPANY_NAME_TH",cinfo.responseModel.owner.firstNameTH},
                    //    {"COMPANY_NAME_EN",cinfo.responseModel.owner.firstNameEN},

                    //    {"GENERAL_INFORMATION__JURISTIC_TYPE",cinfo.responseModel.ownerType},

                    //    {"IDENTITY_ID",cinfo.responseModel.owner.identityID},
                    //    {"REGISTER_DATE",DBDUtility.GetDateFormat(cinfo.responseModel.registerDate)},

                    //};
                    //Juristic Address
                    var OwnerAddress = GetSectionData(model, "JURISTIC_ADDRESS_INFORMATION", SectionType.Form);
                    OwnerAddress.FormData = new Dictionary<string, object>
                    {
                        //{"ADDRESS_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.houseNo },
                        //{"ADDRESS_MOO_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.moo.DefaultString() },
                        //{"ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.village.DefaultString() },
                        //{"ADDRESS_SOI_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.soi },
                        //{"ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.building },
                        //{"ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.roomNo },
                        //{"ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.buildingFloor },
                        //{"ADDRESS_ROAD_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.road },
                        //{"ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.provinceCode },
                        //{"ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT",GeoService.GetProvinceText(cinfo.responseModel.owner.provinceCode) },
                        //{"ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.amphurCode },
                        //{"ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT",GeoService.GetAmphurText(cinfo.responseModel.owner.provinceCode,cinfo.responseModel.owner.amphurCode) },
                        //{"ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.tumbonCode },
                        //{"ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT",GeoService.GetTambolText(cinfo.responseModel.owner.provinceCode,cinfo.responseModel.owner.amphurCode,cinfo.responseModel.owner.tumbonCode) },
                        //{"ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.postCode },
                        {"ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.telephone },
                        {"ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.telephone_ext },
                        {"ADDRESS_FAX_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.fax },
                        {"ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.email },
                        //{"ADDRESS_MOBILE_JURISTIC_HQ_ADDRESS","" },
                        //{"CHECKBOX_SHOW_COMMITTEE_INFORMATION_ADDRESS","" }
                    };

                    //ข้อมูลกรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล Section COMMITTEE_INFORMATION

                }
                #endregion

                // ข้อมูลผู้ขออนุญาต
                //if (!darftData.SectionData.Any(e => e.SectionName == "REQUESTOR_INFORMATION__HEADER"))
                {
                    var requestorInfo = GetSectionData(model, "REQUESTOR_INFORMATION__HEADER", SectionType.Form);
                    requestorInfo.FormData = new Dictionary<string, object>
                    {
                        { "CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION", CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION },
                        { "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION", REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION },
                    };
                }

                // ข้อมูลร้าน/ข้อมูลสถานประกอบการ
                //if (!darftData.SectionData.Any(e => e.SectionName == "INFORMATION_STORE"))
                {
                    var storeInfo = GetSectionData(model, "INFORMATION_STORE", SectionType.Form);
                    storeInfo.FormData = new Dictionary<string, object>
                    {
                        { "INFORMATION_STORE__USE_CITIZEN_ADDRESS_INFORMATION_STORE__USE_CITIZEN_ADDRESS__TRUE", "true" },
                        { "INFORMATION_STORE_NAME_TH", cinfo.response.GetStringValue("commerceNameTH") },
                        { "INFORMATION_STORE_NAME_EN", cinfo.response.GetStringValue("commerceNameEN") },
                        { "ADDRESS_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("houseNo") },
                        { "ADDRESS_MOO_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("moo") },
                        { "ADDRESS_SOI_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("soi") },
                        { "ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("building") },
                        { "ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("roomNo") },
                        { "ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("buildingFloor") },
                        { "ADDRESS_ROAD_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("road") },

                        { "ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("provinceCode") },
                        { "ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT", GeoService.GetProvinceText(cinfo.responseModel.headOffice.provinceCode) },
                        { "ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("amphurCode") },
                        { "ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT", GeoService.GetAmphurText(cinfo.responseModel.headOffice.provinceCode,cinfo.responseModel.headOffice.amphurCode) },
                        { "ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("tumbonCode") },
                        { "ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT", GeoService.GetTambolText(cinfo.responseModel.headOffice.provinceCode,cinfo.responseModel.headOffice.amphurCode,cinfo.responseModel.headOffice.tumbonCode) },
                        { "ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("postCode") },
                        { "ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("telephone") },
                        { "ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("email")},
                        { "ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", cinfo.responseModel.headOffice.telephone_ext },
                        { "ADDRESS_FAX_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("fax") },

                        { "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION", "INFORMATION_STORE_BUILDING_TYPE_OPTION__RENT" },
                        { "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION", "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION__CITIZEN"}
                    };
                }

                result = true;
            }
            else if (currentSectionGroup == "APP_ELECTRONIC_COMMERCIAL_CANCEL")
            {
                if (model.IdentityType == UserTypeEnum.Juristic)
                {
                    // Frontis: Store juristic type in APP_ELECTRONIC_COMMERCIAL_CANCEL_REQUEST_SECTION_JURISTIC_TYPE, it will be used in display condition.
                    var reqSection = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_REQUEST_SECTION", SectionType.Form);
                    if (reqSection.FormData.TryGetString("APP_ELECTRONIC_COMMERCIAL_CANCEL_REQUEST_SECTION_JURISTIC_TYPE", "") == "")
                    {
                        var profile = IdentityHelper.GetJuristicProfile(model.IdentityID);
                        string juristicType = profile["JuristicType"].DefaultString();
                        reqSection.FormData.AddOrUpdate("APP_ELECTRONIC_COMMERCIAL_CANCEL_REQUEST_SECTION_JURISTIC_TYPE", juristicType);
                    }
                }
                    

                var cinfo = getCommerceInfo(model);

                // ข้อมูลชนิดแห่งพาณิชย์กิจ
                //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION"))
                {
                    var commercialInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION", SectionType.Form);
                    List<Dictionary<string, string>> objList = new List<Dictionary<string, string>>();
                    foreach (var obj in cinfo.responseModel.objectives)
                    {
                        objList.Add(new Dictionary<string, string>()
                        {
                            { "TYPE", obj.objectiveCode },
                            { "TYPE_TEXT", obj.description }
                        });
                    }

                    var isElectronic = string.Empty;
                    if (cinfo.responseModel.isElectronic == "N")
                    {
                        isElectronic = "NORMAL";
                    }
                    else if (cinfo.responseModel.isElectronic == "Y")
                    {
                        isElectronic = "ELECTRONIC";
                    }

                    commercialInfo.FormData = new Dictionary<string, object>
                {
                    { "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_TYPE", JsonConvert.SerializeObject(objList) },
                    { "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_REQUEST_TYPE_OPTION", isElectronic }
                };
                }

                // ข้อมูลจำนวนเงินที่นำมาใช้ในการประกอบพาณิชยกิจเป็นประจำ
                //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_CANCEL_INVESTMENT_SECTION"))
                {
                    decimal.TryParse(cinfo.responseModel.budgetAmt.ToString(), out budgetAmt);

                    var investmentInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_INVESTMENT_SECTION", SectionType.Form);
                    investmentInfo.FormData = new Dictionary<string, object>
                    {
                        { "APP_ELECTRONIC_COMMERCIAL_CANCEL_INVESTMENT_SECTION_AMOUNT", cinfo.responseModel.budgetAmt },
                        { "APP_ELECTRONIC_COMMERCIAL_CANCEL_INVESTMENT_SECTION_AMOUNT_TEXT", string.Empty }
                    };
                }

                // ข้อมูลผู้จัดการ managers
                //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION"))
                {
                    var managerInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION", SectionType.ArrayOfForms);
                    List<Dictionary<string, object>> managerList = new List<Dictionary<string, object>>();
                    if (cinfo.responseModel.managers != null)
                    {
                        foreach (var manager in cinfo.responseModel.managers)
                        {
                            var dic = new Dictionary<string, object>
                            {
                                { "AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_TITLE", manager.titleCode },
                                { "AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_TITLE_TEXT", NationalityService.GetTitleTextThai(manager.titleCode)},
                                //{ "DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_TITLE", manager.titleCode },
                                { "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_FIRSTNAME", manager.firstNameTH },
                                { "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_LASTNAME", manager.lastNameTH },
                                { "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ID_CARD", (manager.nationCode == "TH") ? manager.identityID : "" },
                                { "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_PASSPORT_NUMBER", (manager.nationCode != "TH") ? manager.identityID : "" },
                                { "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_BIRTH_DATE",DBDUtility.GetDateFormat( manager.birthDate) },
                                { "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_BIRTH_DATE_AGE", manager.age },
                                { "DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_NATIONALITY", manager.nationCode },
                                { "DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_NATIONALITY_TEXT",NationalityService.GetNationalityText(manager.nationCode)},
                                { "ADDRESS_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.houseNo },
                                { "ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.moo },
                                { "ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.village },
                                { "ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.soi },
                                { "ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.building },
                                { "ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.roomNo },
                                { "ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.buildingFloor },
                                { "ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.road },

                                { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.provinceCode },
                                { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS_TEXT", GeoService.GetProvinceText(manager.provinceCode) },

                                { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.amphurCode },
                                { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS_TEXT", GeoService.GetAmphurText(manager.provinceCode,manager.amphurCode) },

                                { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.tumbonCode },
                                { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS_TEXT", GeoService.GetTambolText(manager.provinceCode,manager.amphurCode,manager.tumbonCode) },

                                { "ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.postCode },
                                { "ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.telephone },
                                { "ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.telephone_ext },
                                { "ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS", manager.fax }
                            };
                            managerList.Add(dic);
                        }
                        managerInfo.ArrayData = managerList;
                    }
                }

                // ข้อมูลวันที่เริ่มต้นประกอบพาณิชยกิจในประเทศไทย
                //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_CANCEL_START_IN_THAILAND_SECTION"))
                {
                    var startDate = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_START_IN_THAILAND_SECTION", SectionType.Form);
                    startDate.FormData = new Dictionary<string, object>
                {
                    { "APP_ELECTRONIC_COMMERCIAL_CANCEL_START_IN_THAILAND_SECTION_DATE",DBDUtility.GetDateFormat(cinfo.responseModel.startDate)}
                };
                }

                // ข้อมูลวันที่ขอจดทะเบียนพาณิชย์
                //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_CANCEL_REQUEST_SECTION"))
                {
                    var regisDate = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_REQUEST_SECTION", SectionType.Form);
                    regisDate.FormData = new Dictionary<string, object>
                    {
                        { "APP_ELECTRONIC_COMMERCIAL_CANCEL_REQUEST_SECTION_DATE",DBDUtility.GetDateFormat(cinfo.responseModel.registerDate) }
                    };
                }
                //ข้อมูลการรับโอนพาณิชยกิจทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์
                var transferInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_TYPE_SECTION", SectionType.Form);
                if (cinfo.responseModel.transfer != null)
                {
                    transferInfo.FormData = new Dictionary<string, object>
                    {
                        { "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_TYPE_SECTION_IS_TRANSFER_OPTION","YES"},

                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_COMMERCIAL_NUMBER", cinfo.responseModel.transfer.refCommerceNo},
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_REQUEST_NUMBER" ,cinfo.responseModel.transfer.refRegisterNo},
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_TITLE", cinfo.responseModel.transfer.transfererTitle},
                        //{"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_TITLE",NationalityService.GetTitleTextThai(cinfo.responseModel.transfer.transfererTitle) },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_TITLE_TEXT",NationalityService.GetTitleTextThai(cinfo.responseModel.transfer.transfererTitle) },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_FIRSTNAME" ,cinfo.responseModel.transfer.transfererFirstName},
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_LASTNAME" ,cinfo.responseModel.transfer.transfererLastName},
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ID_CARD" ,cinfo.responseModel.transfer.refCommerceNo},//transfererIdentityID
                        {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_NATIONALITY" ,cinfo.responseModel.transfer.transfererNation},
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_COMMERCIAL_NAME" , cinfo.responseModel.transfer.oldCommerceName},
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_DATE",DBDUtility.GetDateFormat(cinfo.responseModel.transfer.transferDate)},
                        {"ADDRESS_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.houseNo},
                        {"ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.moo},
                        {"ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.village},
                        {"ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS",cinfo.responseModel.transfer.soi},
                        {"ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.building},
                        {"ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.roomNo},
                        {"ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.buildingFloor},
                        {"ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.road},
                        {"ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.provinceCode},
                        {"ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS_TEXT", GeoService.GetProvinceText(cinfo.responseModel.transfer.provinceCode) },
                        {"ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.amphurCode},
                        {"ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS_TEXT",GeoService.GetAmphurText(cinfo.responseModel.transfer.provinceCode,cinfo.responseModel.transfer.amphurCode) },
                        {"ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.tumbonCode},
                        {"ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS_TEXT",GeoService.GetTambolText(cinfo.responseModel.transfer.provinceCode,cinfo.responseModel.transfer.amphurCode,cinfo.responseModel.transfer.tumbonCode) },
                        {"ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.postCode},
                        {"ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.telephone},
                        {"ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.telephone_ext},
                        {"ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS",cinfo.responseModel.transfer.fax},
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_CAUSE" , cinfo.responseModel.transfer.transferReason},

                    };
                }
                else
                {
                    transferInfo.FormData = new Dictionary<string, object>
                    {
                        { "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_TYPE_SECTION_IS_TRANSFER_OPTION","NO"},
                    };
                }
                // ข้อมูลที่ตั้งสำนักงานสาขา branchs
                //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION"))
                {
                    var branchInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION", SectionType.ArrayOfForms);
                    List<Dictionary<string, object>> branchList = new List<Dictionary<string, object>>();
                    if (cinfo.responseModel.branchs != null)
                    {
                        foreach (var branch in cinfo.responseModel.branchs)
                        {
                            var dic = new Dictionary<string, object>
                            //branchInfo.ArrayData.Add(new Dictionary<string, object>()
                            {
                                { "ADDRESS_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.houseNo },
                                { "ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.moo },
                                { "ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.village },
                                { "ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.soi },
                                { "ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.building },
                                { "ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.roomNo },
                                { "ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.buildingFloor },
                                { "ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.road },
                                { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.provinceCode },
                                { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS_TEXT", GeoService.GetProvinceText(branch.provinceCode) },
                                { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.amphurCode },
                                { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS_TEXT", GeoService.GetAmphurText(branch.provinceCode,branch.amphurCode) },
                                { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.tumbonCode },
                                { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS_TEXT", GeoService.GetTambolText(branch.provinceCode,branch.amphurCode,branch.tumbonCode) },
                                { "ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.postCode },
                                { "ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.telephone },
                                { "ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.telephone_ext },
                                { "ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_CANCEL_BRANCH_SECTION_ADDRESS", branch.fax }
                            };
                            branchList.Add(dic);
                        }
                        branchInfo.ArrayData = branchList;
                    }
                }

                // ข้อมูลที่ตั้งโรงเก็บสินค้า warehouses
                //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION"))
                {
                    var warehouseInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION", SectionType.ArrayOfForms);
                    List<Dictionary<String, Object>> warehouseList = new List<Dictionary<String, Object>>();
                    if (cinfo.responseModel.warehouses != null)
                    {
                        foreach (var warehouse in cinfo.responseModel.warehouses)
                        {
                            var dic = new Dictionary<String, Object>
                        //warehouseInfo.ArrayData.Add(new Dictionary<string, object>()
                        {
                            { "ADDRESS_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.houseNo },
                            { "ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.moo },
                            { "ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.village },
                            { "ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.soi },
                            { "ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.building },
                            { "ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.roomNo },
                            { "ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.buildingFloor },
                            { "ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.road },
                            { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.provinceCode },
                            { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS_TEXT", GeoService.GetProvinceText(warehouse.provinceCode) },
                            { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.amphurCode },
                            { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS_TEXT", GeoService.GetAmphurText(warehouse.provinceCode,warehouse.amphurCode) },
                            { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.tumbonCode },
                            { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS_TEXT", GeoService.GetTambolText(warehouse.provinceCode,warehouse.amphurCode,warehouse.tumbonCode) },
                            { "ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.postCode },
                            { "ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.telephone },
                            { "ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.telephone_ext },
                            { "ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_CANCEL_WAREHOUSE_SECTION_ADDRESS", warehouse.fax }
                        };
                            warehouseList.Add(dic);
                        }
                        warehouseInfo.ArrayData = warehouseList;
                    }
                }

                // ข้อมูลตัวแทนค้าต่าง agents
                //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION"))
                {
                    var agentInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION", SectionType.ArrayOfForms);
                    List<Dictionary<String, Object>> agentList = new List<Dictionary<string, object>>();
                    if (cinfo.responseModel.agents != null)
                    {
                        foreach (var agent in cinfo.responseModel.agents)
                        {
                            var dic = new Dictionary<String, Object>
                            //agentInfo.ArrayData.Add(new Dictionary<string, object>()
                            {
                                { "APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_NAME", agent.agentName },
                                { "ADDRESS_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.houseNo },
                                { "ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.moo },
                                { "ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.village },
                                { "ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.soi },
                                { "ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.building },
                                { "ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.roomNo },
                                { "ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.buildingFloor },
                                { "ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.road },
                                { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.provinceCode },
                                { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS_TEXT", GeoService.GetProvinceText(agent.provinceCode) },
                                { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.amphurCode },
                                { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS_TEXT", GeoService.GetAmphurText(agent.provinceCode,agent.amphurCode) },
                                { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.tumbonCode },
                                { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS_TEXT", GeoService.GetTambolText(agent.provinceCode,agent.amphurCode,agent.tumbonCode) },
                                { "ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.postCode },
                                { "ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.telephone },
                                { "ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.telephone_ext },
                                { "ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_CANCEL_AGENT_SECTION_ADDRESS", agent.fax }
                            };
                            agentList.Add(dic);
                        }
                        agentInfo.ArrayData = agentList;
                    }
                }

                // ข้อมูลรายละเอียดเว็บไซต์ webSite
                //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION"))
                {
                    var websiteInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION", SectionType.Form);
                    if (cinfo.responseModel.webSite != null)
                    {
                        #region [GetDescription]
                        // Get Delivery Other Description
                        var deliveryOther = "";
                        if (cinfo.responseModel.webSite.deliveryMethods != null)
                        {
                            foreach (var item in cinfo.responseModel.webSite.deliveryMethods)
                            {
                                if (item.deliverMethodCode == "99")
                                {
                                    deliveryOther = item.description;
                                }

                            }
                        }
                        // Get Order Other Description
                        var orderOther = "";
                        if (cinfo.responseModel.webSite.orderMethods != null)
                        {
                            foreach (var item in cinfo.responseModel.webSite.orderMethods)
                            {
                                if (item.orderMethodCode == "99")
                                {
                                    orderOther = item.description;
                                }
                            }
                        }
                        // Get Payment Other Description
                        var paymentOther = "";
                        if (cinfo.responseModel.webSite.paymentMethods != null)
                        {
                            foreach (var item in cinfo.responseModel.webSite.paymentMethods)
                            {
                                if (item.paymentMethodCode == "99")
                                {
                                    paymentOther = item.description;
                                }
                            }
                        }
                        #endregion
                        // Get app electronic channel
                        string str_url = string.Empty;
                        foreach (URLC_CODE url in (URLC_CODE[])Enum.GetValues(typeof(URLC_CODE)))
                        {
                            int code = (int)url;
                            if (cinfo.responseModel.webSite.urlChannel == code.ToString("D2"))
                            {
                                str_url = url.GetEnumStringValue();

                            }
                        }

                        websiteInfo.FormData = new Dictionary<string, object>
                    {
                        {"ADDRESS_EN_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.houseNo },
                        {"ADDRESS_EN_MOO_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.moo },
                        {"ADDRESS_EN_SOI_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.soi },
                        {"ADDRESS_EN_BUILDING_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.building },
                        {"ADDRESS_EN_ROOMNO_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.roomNo },
                        {"ADDRESS_EN_FLOOR_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.buildingFloor },
                        {"ADDRESS_EN_ROAD_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.road },
                        {"ADDRESS_EN_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.provinceCode },
                        {"ADDRESS_EN_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN_TEXT",GeoService.GetProvinceText(cinfo.responseModel.headOffice.provinceCode,"en")},
                        {"ADDRESS_EN_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.amphurCode },
                        {"ADDRESS_EN_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN_TEXT",GeoService.GetAmphurText(cinfo.responseModel.headOffice.provinceCode,cinfo.responseModel.headOffice.amphurCode,"en") },
                        {"ADDRESS_EN_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.tumbonCode },
                        {"ADDRESS_EN_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN_TEXT",GeoService.GetTambolText(cinfo.responseModel.headOffice.provinceCode,cinfo.responseModel.headOffice.amphurCode,cinfo.responseModel.headOffice.tumbonCode,"en") },
                        {"ADDRESS_EN_POSTCODE_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.postCode },
                        
                        // App electronic channel
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_MEDIA_OPTION",cinfo.responseModel.webSite.urlChannel },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME_" + str_url,cinfo.responseModel.webSite.websiteURL },
                        //Order
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM_BASKET",cinfo.responseModel.webSite.orderMethods.Count >0? cinfo.responseModel.webSite.orderMethods.Any(e => Convert.ToInt16(e.orderMethodCode).Equals((Int16)OrderMethodCode.BASKET))? "true":"false" : "false"},
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM_FORM",cinfo.responseModel.webSite.orderMethods.Count >0? cinfo.responseModel.webSite.orderMethods.Any(e => Convert.ToInt16(e.orderMethodCode).Equals((Int16)OrderMethodCode.FORM))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM_EMAIL",cinfo.responseModel.webSite.orderMethods.Count >0? cinfo.responseModel.webSite.orderMethods.Any(e => Convert.ToInt16(e.orderMethodCode).Equals((Int16)OrderMethodCode.EMAIL))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM_PHONE",cinfo.responseModel.webSite.orderMethods.Count >0? cinfo.responseModel.webSite.orderMethods.Any(e => Convert.ToInt16(e.orderMethodCode).Equals((Int16)OrderMethodCode.PHONE))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM_FAX",cinfo.responseModel.webSite.orderMethods.Count >0? cinfo.responseModel.webSite.orderMethods.Any(e => Convert.ToInt16(e.orderMethodCode).Equals((Int16)OrderMethodCode.FAX))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM_OTHER",cinfo.responseModel.webSite.orderMethods.Count >0? cinfo.responseModel.webSite.orderMethods.Any(e => Convert.ToInt16(e.orderMethodCode).Equals((Int16)OrderMethodCode.OTHER))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM_OTHER_TEXT",orderOther},
                        //Payment
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT_OFFLINE",cinfo.responseModel.webSite.paymentMethods.Count>0? cinfo.responseModel.webSite.paymentMethods.Any(e => Convert.ToInt16(e.paymentMethodCode).Equals((Int16)PaymentMethodCode.OFFLINE))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT_ONLINE_CREDIT_CARD",cinfo.responseModel.webSite.paymentMethods.Count>0? cinfo.responseModel.webSite.paymentMethods.Any(e => Convert.ToInt16(e.paymentMethodCode).Equals((Int16)PaymentMethodCode.ONLINE_CREDIT_CARD))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT_ONLINE_E_BANKING",cinfo.responseModel.webSite.paymentMethods.Count>0? cinfo.responseModel.webSite.paymentMethods.Any(e => Convert.ToInt16(e.paymentMethodCode).Equals((Int16)PaymentMethodCode.ONLINE_AGENT))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT_ONLINE_AGENT",cinfo.responseModel.webSite.paymentMethods.Count>0? cinfo.responseModel.webSite.paymentMethods.Any(e => Convert.ToInt16(e.paymentMethodCode).Equals((Int16)PaymentMethodCode.ONLINE_AGENT))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT_OTHER",cinfo.responseModel.webSite.paymentMethods.Count>0? cinfo.responseModel.webSite.paymentMethods.Any(e => Convert.ToInt16(e.paymentMethodCode).Equals((Int16)PaymentMethodCode.OTHER))? "true":"false" : "false"},
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT_OTHER_TEXT",paymentOther },
                        //Deliver
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER_TRANSPORT_COMPANY",cinfo.responseModel.webSite.deliveryMethods.Count>0? cinfo.responseModel.webSite.deliveryMethods.Any(e => Convert.ToInt16(e.deliverMethodCode).Equals((Int16)DeliveryMethodCode.TRANSPORT_COMPANY))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER_POST_OFFICE", cinfo.responseModel.webSite.deliveryMethods.Count>0? cinfo.responseModel.webSite.deliveryMethods.Any(e => Convert.ToInt16(e.deliverMethodCode).Equals((Int16)DeliveryMethodCode.POST_OFFICE))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER_DELEVERY_STAFF",cinfo.responseModel.webSite.deliveryMethods.Count>0? cinfo.responseModel.webSite.deliveryMethods.Any(e => Convert.ToInt16(e.deliverMethodCode).Equals((Int16)DeliveryMethodCode.DELEVERY_STAFF))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER_DOWNLOAD",cinfo.responseModel.webSite.deliveryMethods.Count>0? cinfo.responseModel.webSite.deliveryMethods.Any(e => Convert.ToInt16(e.deliverMethodCode).Equals((Int16)DeliveryMethodCode.DOWNLOAD))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER_EMAIL",cinfo.responseModel.webSite.deliveryMethods.Count>0? cinfo.responseModel.webSite.deliveryMethods.Any(e => Convert.ToInt16(e.deliverMethodCode).Equals((Int16)DeliveryMethodCode.EMAIL))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER_OTHER",cinfo.responseModel.webSite.deliveryMethods.Count>0? cinfo.responseModel.webSite.deliveryMethods.Any(e => Convert.ToInt16(e.deliverMethodCode).Equals((Int16)DeliveryMethodCode.OTHER))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER_OTHER_TEXT",deliveryOther},
                        //TypeOfBusiness                       
                        {"AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_TYPE",cinfo.responseModel.webSite.typeOfBussiness[0].typeOfBussinessCode},
                        {"AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_TYPE_TEXT",cinfo.responseModel.webSite.typeOfBussiness[0].description },     
                        //Budget
                        {"APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_BUDGET", cinfo.responseModel.webSite.webBudget.DefaultString() },
                    };
                    }
                }

                #region [Juristic]
                if (model.IdentityType.Equals(UserTypeEnum.Juristic))
                {
                    //ข้อมูลผู้เป็นหุ้นส่วนของทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์
                    //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION"))
                    {
                        var partnerInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION", SectionType.ArrayOfForms);
                        List<Dictionary<string, object>> partnerList = new List<Dictionary<string, object>>();
                        if (cinfo.responseModel.partners != null)
                        {
                            foreach (var partner in cinfo.responseModel.partners)
                            {
                                var dic = new Dictionary<string, object>
                                {
                                    {"APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_IS_FROM_DBD", true},
                                    {"AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_TITLE",partner.titleCode },
                                    {"AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_TITLE_TEXT", NationalityService.GetTitleTextThai(partner.titleCode)},

                                    {"APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_BIRTH_DATE_AGE",partner.age},
                                    {"APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_BIRTH_DATE",DBDUtility.GetDateFormat(partner.birthDate)},
                                    {"APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_FIRSTNAME",partner.firstName},
                                    {"ADDRESS_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_ADDRESS",partner.houseNo},
                                    {"APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_ID_CARD", (partner.nationCode == "TH") ? partner.identityID : "" },
                                    {"APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_PASSPORT_NUMBER", (partner.nationCode != "TH") ? partner.identityID : "" },
                                    {"APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_AMOUNT",partner.investAmt},
                                    {"APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_LASTNAME",partner.lastName},
                                    {"ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_ADDRESS",partner.postCode},
                                    {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_RACE",partner.race},
                                    {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_RACE_TEXT",NationalityService.GetNationText(partner.race)},
                                    {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_NATIONALITY", partner.nationCode },
                                    {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_NATIONALITY_TEXT",NationalityService.GetNationalityText(partner.nationCode)},
                                    {"ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_ADDRESS",partner.tumbonCode },
                                    {"ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_ADDRESS", partner.amphurCode},
                                    {"ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_ADDRESS", partner.provinceCode},

                                    {"ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_ADDRESS_TEXT", GeoService.GetProvinceText(partner.provinceCode)},
                                    {"ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_ADDRESS_TEXT", GeoService.GetAmphurText(partner.provinceCode,partner.amphurCode)},
                                    {"ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_ADDRESS_TEXT", GeoService.GetTambolText(partner.provinceCode,partner.amphurCode,partner.tumbonCode)},
                                    {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_INVEST_TYPE",NationalityService.GetInvestType(partner.investCode)},
                                    {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_INVEST_TYPE_TEXT", DBDUtility.GetInvestCode().FirstOrDefault(x => x.Key ==Convert.ToUInt32(partner.investCode)).Value},

                                    {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_STATUS",partner.status },
                                    {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_STATUS_TEXT",NationalityService.GetPartnerStatus(partner.status)},
                                    {"ARR_IDX",partner.seqNo},
                                    {"ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_ADDRESS",partner.telephone},
                                };
                                partnerList.Add(dic);
                            }
                            partnerInfo.ArrayData = partnerList;
                        }
                    }

                    // ShareHolder
                    //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2"))
                    {
                        List<Dictionary<string, object>> shareholderList = new List<Dictionary<string, object>>();
                        if (cinfo.responseModel.shareholders != null)
                        {
                            var shareholderInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2", SectionType.ArrayOfForms);
                            foreach (var shareholder in cinfo.responseModel.shareholders)
                            {
                                var dic = new Dictionary<string, object>
                                {
                                    {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2_NATIONALITY_TEXT",NationalityService.GetNationalityText(shareholder.nationCode.DefaultString()) },
                                    {"APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2_SHARE",shareholder.shareAmt.DefaultString() },
                                    {"APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2_SHARE_BATH",shareholder.shareValue.DefaultString() }
                                };
                                shareholderList.Add(dic);

                                decimal.TryParse(shareholder.shareAmt.DefaultString(), out ShareAmout);
                                Total_ShareAmout = Total_ShareAmout + ShareAmout;
                                decimal.TryParse(shareholder.shareValue.DefaultString(), out ShareValue);
                            }
                            shareholderInfo.ArrayData = shareholderList;
                        }
                    }

                    //ข้อมูลมูลค่าหุ้น
                    //if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION"))
                    {
                        var stockInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION", SectionType.Form);
                        stockInfo.FormData = new Dictionary<string, object>
                        {
                            {"APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_REGISTERED_CAPITAL",budgetAmt },
                            {"APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_SEPERATED_TO",Total_ShareAmout.ToString() },
                            {"APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_SHARE_BATH",ShareValue.ToString()}
                        };
                    }
                }
                #endregion

                // TODO : ลบไฟล์เก่า (ลบการแสดงผลออกไปก่อน ยังไม่สรุปเรื่องการ prefield file attachment)
                var singleFormTrans = SingleFormTransaction.Get(model.IdentityID);
                var fileGroup = singleFormTrans.UploadedFiles.Where(o => o.Description == "APP_ELECTONIC_COMMERCIAL_CANCEL_SECTION" || o.Description == "FREE_DOC_SECTION").ToList();
                foreach (var group in fileGroup)
                {
                    singleFormTrans.UploadedFiles.Remove(group);
                }
                singleFormTrans.Save();

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
                    case AppsStage.UserCreate:
                        {
                            #region [Attachs] 
                            var attachList = new List<Attach>();
                            int file_index = 1;
                            foreach (FileGroup group in model.UploadedFiles)
                            {
                                foreach (var item in group.Files)
                                {
                                    var description = item.Extras.ContainsKey("FILETYPENAME") ? item.Extras["FILETYPENAME"].ToString() : string.Empty;

                                    string fileType = DBDUtility.GetFileTypeRef().FirstOrDefault(x => item.FileTypeCode.Contains(x.Value)).Key.ToString();
                                    var attach = new Attach()
                                    {
                                        base64Sting = item.GetBased64String(),
                                        contentType = item.ContentType,
                                        description = description,
                                        fileName = item.FileName,
                                        fileSize = item.FileSize.ToString(),
                                        fileType = fileType,
                                        fileId = item.FileID,
                                        seqNo = file_index++.ToString()
                                    };
                                    attachList.Add(attach);
                                }
                            }
                            #endregion
                            var post = new CommerceDataServiceCancel()
                            {
                                reqNo = model.ApplicationRequestNumber.ToString(),
                                bizReqNo = model.ApplicationRequestID.ToString(),
                                bizReqDateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", new CultureInfo("en")),
                                commerceNo = model.IdentityID.ToString(),
                                registerNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_NUMBER"),
                                reasonCloseCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2_CAUSE"),
                                reasonClose = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2_CAUSE_TEXT"),
                                closeDate = DateTime.ParseExact(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2_DATE"), "dd/MM/yyyy", new CultureInfo("th")).ToString("yyyyMMdd"),
                                attach = attachList.ToList()
                            };

                            // Model data
                            var regisUrl = ConfigurationManager.AppSettings["DBD_COMMERCE_CLOSE_WS_URL"];
                            var jsonPost = JsonConvert.SerializeObject(post, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                            // API Exception
                            Api.OnCheckingApplicationError += (ex) =>
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

                            // Call API
                            var apiResult = Api.Call(regisUrl, HttpVerb.PUT, null, jsonPost, ContentType.ApplicationJson);

                            // Clear section data
                            var singleformReq = new SingleFormRequestEntity();
                            singleformReq.DeleteSections(request.IdentityID, "APP_ELECTRONIC_COMMERCIAL_CANCEL", new string[] { "REQUESTOR_INFORMATION__HEADER", "INFORMATION_STORE" });

                            if (apiResult != null)
                            {
                                if (apiResult.HasValues)
                                {
                                    if (apiResult["messageCode"].ToString() == "2000" && !string.IsNullOrEmpty(apiResult["processId"].ToString()))
                                    {
                                        Dictionary<string, string> respData = new Dictionary<string, string>()
                                        {
                                            { "processId", apiResult["processId"].ToString() }
                                        };

                                        if (request.Data.ContainsKey("DBD_RESPONSE_DATA"))
                                        {
                                            request.Data.Remove("DBD_RESPONSE_DATA");
                                        }

                                        request.Data.Add("DBD_RESPONSE_DATA", new ApplicationRequestDataGroupEntity()
                                        {
                                            GroupName = "DBD_RESPONSE_DATA",
                                            Data = respData
                                        });


                                        result.Success = true;
                                    }
                                    else
                                    {
                                        // Response has value but fail
                                        string error = apiResult["messageDesc"].ToString();
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                        throw new Exception(error);
                                    }
                                }
                                else
                                {
                                    string error = "No value";
                                    result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                    throw new Exception(error);
                                }
                            }
                            else
                            {
                                string error = "Unable to request to " + regisUrl + ".";
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                throw new Exception(error);
                            }
                        }
                        break;
                    case AppsStage.UserUpdate:
                        {
                            if (model.Status == ApplicationStatusV2Enum.CHECK || model.Status == ApplicationStatusV2Enum.PENDING)
                            {
                                var post = new CommerceAddAttach()
                                {
                                    bizReqNo = model.ApplicationRequestID.ToString(),
                                    reqNo = model.ApplicationRequestNumber,
                                    bizReqDateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", new CultureInfo("en")),//DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                                    //remark = model.Remark.ToString(),
                                };
                                #region [Attachs]
                                var attachList = new List<Attach>();
                                // int file_index = 1;
                                var requestedFiles = model.UploadedFiles.Where(o => o.Description == "REQUESTED_FILE").OrderByDescending(o => o.CreatedDate).FirstOrDefault();
                                if (requestedFiles != null && requestedFiles.Files != null && requestedFiles.Files.Count > 0)
                                {
                                    foreach (var item in requestedFiles.Files)
                                    {
                                        var fileTypeDesc = item.Extras.ContainsKey("FILETYPEDESC") ? item.Extras["FILETYPEDESC"].ToString() : string.Empty;
                                        var fileType = item.Extras.ContainsKey("FILETYPE") ? item.Extras["FILETYPE"].ToString() : string.Empty;
                                        var fileTypeCode = DBDUtility.GetFileTypeRef().FirstOrDefault(x => x.Key.ToString() == fileType).Value.ToString();
                                        var description = ResourceHelper.GetResourceWordWithDefault(fileTypeCode, "Apps_SingleForm_Filelist", fileTypeCode).Replace(": ({0} {1} {2})", "");
                                        var fileIdOld = item.Extras.ContainsKey("FILEID") ? item.Extras["FILEID"].ToString() : string.Empty;
                                        //string fileType = DBDUtility.GetFileTypeRef().FirstOrDefault(x => item.FileTypeCode.Contains(x.Value)).Key.ToString();
                                        var attach = new Attach()
                                        {
                                            base64Sting = item.GetBased64String(),
                                            contentType = item.ContentType,
                                            description = fileTypeDesc == string.Empty ? description : fileTypeDesc,
                                            fileName = item.FileName,
                                            fileSize = item.FileSize.ToString(),
                                            fileType = fileType.ToString(),//fileType,
                                            fileId = fileIdOld == string.Empty ? item.FileID : fileIdOld,
                                            //seqNo = seqNo.ToString()
                                        };
                                        attachList.Add(attach);
                                    }

                                }
                                post.attachs = attachList.ToList();
                                #endregion
                                string regisUrl = ConfigurationManager.AppSettings["DBD_COMMERCE_UPDATE_WS_URL"];
                                var jsonPost = JsonConvert.SerializeObject(post,
                                    Newtonsoft.Json.Formatting.None,
                                    new JsonSerializerSettings
                                    {
                                        NullValueHandling = NullValueHandling.Ignore
                                    }
                                ); // Serialize model data to JSON

                                // API Exception
                                Api.OnCheckingApplicationError += (ex) =>
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
                                // Call API
                                var apiResult = Api.Call(regisUrl, HttpVerb.POST, null, jsonPost, ContentType.ApplicationJson);

                                if (apiResult != null)
                                {
                                    if (apiResult.HasValues)
                                    {
                                        if (apiResult["messageCode"].ToString() == "2000" && !string.IsNullOrEmpty(apiResult["processId"].ToString()))
                                        {
                                            Dictionary<string, string> respData = new Dictionary<string, string>()
                                            {
                                                { "processId", apiResult["processId"].ToString() }
                                            };

                                            if (request.Data.ContainsKey("DBD_RESPONSE_DATA"))
                                            {
                                                request.Data.Remove("DBD_RESPONSE_DATA");
                                            }
                                            request.Data.Add("DBD_RESPONSE_DATA", new ApplicationRequestDataGroupEntity()
                                            {
                                                GroupName = "DBD_RESPONSE_DATA",
                                                Data = respData
                                            });
                                            result.Success = true;
                                        }
                                        else
                                        {
                                            string error = "messageCode" + apiResult["messageCode"].ToString();
                                            result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                            throw new Exception(error);
                                        }
                                    }
                                    else
                                    {
                                        string error = "No value";
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                        throw new Exception(error);
                                    }
                                }
                                else
                                {
                                    string error = "Unable to request to " + regisUrl + ".";
                                    result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                    throw new Exception(error);
                                }
                            }
                            else
                            {
                                result.Success = true;
                            }
                        }
                        break;
                    case AppsStage.ApiUpdate:
                        if (model.Extras != null)
                        {
                            var registNo = model.Extras.Where(o => o.Key == "registNo").FirstOrDefault();
                            if (!registNo.IsDefault())
                            {
                                request = request.AddExtraData("APP_ELECTRONIC_COMMERCIAL_EXTRAS", "APP_ELECTRONIC_COMMERCIAL_REGIST_NO", registNo.Value.ToString());
                                request = request.AddExtraData("APP_ELECTRONIC_COMMERCIAL_EXTRAS", "APP_ELECTRONIC_COMMERCIAL_COMMERCE_NO", model.IdentityID);
                            }
                        }
                        result.Success = true;
                        break;
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
        }

        private (JObject response, CommerceRegistInfo responseModel) getCommerceInfo(SingleFormRequestViewModel model)
        {
            var response = (JObject)null;
            var responseModel = new CommerceRegistInfo();
            var identityID = "";
            var registerNo = "";
            var formdata = model.SectionData.Where(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION").Select(e => e.FormData).FirstOrDefault();

            if (formdata != null)
            {
                // check citizen or juristic
                if (model.IdentityType.Equals(UserTypeEnum.Citizen))
                {
                    identityID = formdata.TryGetString("APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_CITIZEN_ID");
                    registerNo = formdata.TryGetString("APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_NUMBER");
                }
                else
                {
                    identityID = formdata.TryGetString("APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_JURISTIC_ID");
                    registerNo = formdata.TryGetString("APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_NUMBER");
                }

                var preFillData = SingleFormPreFillData.Get(identityID, registerNo);

                if (preFillData != null)
                {
                    // ใช้ข้อมูลที่เคยดึงมา
                    response = JObject.Parse(preFillData.Data.ToString());
                    responseModel = response.ToObject<CommerceRegistInfo>();
                }
                else
                {
                    // ดึงข้อมูลจาก DBD
                    Api.OnCheckingApplicationError += (ex) =>
                    {
                        throw ex;
                    };

                    response = Api.Get(ConfigurationManager.AppSettings["DBD_COMMERCE_CHECK_WS_URL"], new Dictionary<string, string> { { "commerceNo", identityID }, { "registerNo", registerNo } }, ContentType.ApplicationJson);

                    if (response.HasValues)
                    {
                        // เก็บข้อมูล pre fill จาก dbd ลง mongo
                        preFillData = new SingleFormPreFillData
                        {
                            IdentityID = identityID,
                            ReferenceID = registerNo,
                            Data = response.ToString(Formatting.None)
                        };
                        preFillData.Create();

                        responseModel = response.ToObject<CommerceRegistInfo>();
                    }
                    else
                    {
                        throw new Exception("ไม่พบข้อมูล");
                    }
                }
            }

            return (response, responseModel);
        }

    }
}
