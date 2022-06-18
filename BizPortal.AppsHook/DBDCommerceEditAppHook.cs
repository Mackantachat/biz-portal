using BizPortal.DAL.MongoDB;
using BizPortal.Utils;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using BizPortal.ViewModels.Apps.DBDStandard;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using EGA.WS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using static BizPortal.ViewModels.DBDCommearceData;
using BizPortal.Integrated;
using Mapster;

namespace BizPortal.AppsHook
{
    public class DBDCommerceEditAppHook : SingleFormRendererAppHook
    {
        decimal TotalShareAmout;
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
            var formdata = model.SectionData.Where(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION").Select(e => e.FormData).FirstOrDefault();
            var commerceNo = "";
            var registerNo = "";

            if (formdata != null)
            {
                // check citizen or juristic
                if (model.IdentityType.Equals(UserTypeEnum.Citizen))
                {
                    commerceNo = formdata.TryGetString("APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION_CITIZEN_ID");
                    registerNo = formdata.TryGetString("APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION_NUMBER");
                }
                else
                {
                    commerceNo = formdata.TryGetString("APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION_JURISTIC_ID");
                    registerNo = formdata.TryGetString("APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION_NUMBER");
                }
            }

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
                var cinfo = getCommerceInfo(commerceNo, registerNo);
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

                var OwnerInfo = GetSectionData(model, "GENERAL_INFORMATION", SectionType.Form);
                OwnerInfo.FormData = new Dictionary<string, object>
                {
                    {"AJAX_DROPDOWN_CITIZEN_TITLE",cinfo.responseModel.owner.titleCode},
                    {"AJAX_DROPDOWN_CITIZEN_TITLE_TEXT",NationalityService.GetTitleTextThai(cinfo.responseModel.owner.titleCode)},

                    {"CITIZEN_FIRSTNAME_EN",cinfo.responseModel.owner.firstNameEN},
                    {"CITIZEN_LASTNAME_EN",cinfo.responseModel.owner.lastNameEN},

                    {"GENERAL_INFORMATION__CITIZEN_AGE",cinfo.responseModel.owner.age},

                    {"AJAX_DROPDOWN_CITIZEN_TITLE_EN",cinfo.responseModel.owner.titleCode},
                    {"AJAX_DROPDOWN_CITIZEN_TITLE_EN_TEXT",NationalityService.GetTitleTextEng(cinfo.responseModel.owner.titleCode)},

                    {"DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY",cinfo.responseModel.owner.nationCode},
                    {"DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT",NationalityService.GetNationText(cinfo.responseModel.owner.nationCode)},

                    {"DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE",cinfo.responseModel.owner.race},
                    {"DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE_TEXT",NationalityService.GetNationalityText(cinfo.responseModel.owner.race)},

                };


                if (model.IdentityType.Equals(UserTypeEnum.Juristic))
                {
                    var JURISTIC_ADDRESS_INFO = GetSectionData(model, "JURISTIC_ADDRESS_INFORMATION", SectionType.Form);
                    JURISTIC_ADDRESS_INFO.FormData = new Dictionary<string, object>
                     {
                        { "ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.telephone},
                        { "ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.email},
                        { "ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.telephone_ext},
                        { "ADDRESS_FAX_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.fax}
                     };


                }

                // ข้อมูลผู้ขออนุญาต
                if (!darftData.SectionData.Any(e => e.SectionName == "REQUESTOR_INFORMATION__HEADER"))
                {
                    var requestorInfo = GetSectionData(model, "REQUESTOR_INFORMATION__HEADER", SectionType.Form);
                    requestorInfo.FormData = new Dictionary<string, object>
                    {
                        { "CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION", CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION },
                        { "REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION", REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION },
                    };
                }

                // ข้อมูลร้าน/ข้อมูลสถานประกอบการ
                if (!darftData.SectionData.Any(e => e.SectionName == "INFORMATION_STORE"))
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
                    { "ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT",GeoService.GetProvinceText(cinfo.responseModel.headOffice.provinceCode)},

                    //cinfo.response.GetObjectData("headOffice").GetStringValue("provinceCode")
                    //GeoService
                    { "ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("amphurCode") },
                    { "ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("tumbonCode") },
                    { "ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("postCode") },
                    { "ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("telephone") },
                    { "ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", string.Empty },
                    { "ADDRESS_FAX_INFORMATION_STORE__ADDRESS", cinfo.response.GetObjectData("headOffice").GetStringValue("fax") },
                    { "ADDRESS_LAT_INFORMATION_STORE__ADDRESS", string.Empty },
                    { "ADDRESS_LNG_INFORMATION_STORE__ADDRESS", string.Empty },
                    { "SEARCH_TEXT_GOOGLE_MAP", string.Empty },
                    { "INFORMATION_STORE_AREA", string.Empty },
                    { "INFORMATION_STORE_HEALTH_OTHER", string.Empty },

                    { "ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT", GeoService.GetAmphurText(cinfo.responseModel.headOffice.provinceCode,cinfo.responseModel.headOffice.amphurCode)},
                    { "ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT", GeoService.GetTambolText(cinfo.responseModel.headOffice.provinceCode,cinfo.responseModel.headOffice.amphurCode,cinfo.responseModel.headOffice.tumbonCode) },

                    { "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION",string.Empty},
                    { "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION", string.Empty},
                        { "ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS",cinfo.response.GetObjectData("headOffice").GetStringValue("email") }
                    // use for julistic
                    // {"INFORMATION_STORE_BUILDING_TYPE_OPTION",INFORMATION_STORE_BUILDING_TYPE_OPTION}
                };

                }

                result = true;
            }
            else if (currentSectionGroup == "APP_ELECTRONIC_COMMERCIAL_EDIT")
            {
                var cinfo = getCommerceInfo(commerceNo, registerNo);

                // ข้อมูลชนิดแห่งพาณิชย์กิจ
                if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION"))
                {
                    var commercialInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION", SectionType.Form);

                    List<Dictionary<string, string>> objList = new List<Dictionary<string, string>>();
                    foreach (var obj in cinfo.responseModel.objectives)
                    {
                        objList.Add(new Dictionary<string, string>()
                        {
                            { "TYPE", obj.objectiveCode },
                            { "TYPE_TEXT", obj.description }
                        });
                    }
                    commercialInfo.FormData = new Dictionary<string, object>
                    {
                        { "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_TYPE", JsonConvert.SerializeObject(objList) },
                        { "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE_OPTION",string.Equals(cinfo.responseModel.isElectronic,"Y")? "ELECTRONIC" : "NORMAL"},

                        // จำประเภทของการจดทะเบียนที่ดึงมาจาก DBD ไว้  ถ้าเป็น ELECTRONIC จะมีข้อมูลบางส่วนที่แก้ไขไม่ได้
                        { "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE_FROM_DBD",string.Equals(cinfo.responseModel.isElectronic,"Y")? "ELECTRONIC" : "NORMAL"}
                    };
                }

                // ข้อมูลจำนวนเงินที่นำมาใช้ในการประกอบพาณิชยกิจเป็นประจำ
                if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_INVESTMENT_SECTION"))
                {
                    decimal.TryParse(cinfo.responseModel.budgetAmt.ToString(), out budgetAmt);

                    var investmentInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_INVESTMENT_SECTION", SectionType.Form);
                    investmentInfo.FormData = new Dictionary<string, object>
                    {
                        { "APP_ELECTRONIC_COMMERCIAL_EDIT_INVESTMENT_SECTION_AMOUNT",cinfo.responseModel.budgetAmt },
                        { "APP_ELECTRONIC_COMMERCIAL_EDIT_INVESTMENT_SECTION_AMOUNT_TEXT", string.Empty}
                    };
                }

                // ข้อมูลผู้จัดการ
                if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION"))
                {
                    var managerInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION", SectionType.ArrayOfForms);
                    List<Dictionary<string, object>> managerList = new List<Dictionary<string, object>>();
                    if (cinfo.responseModel.managers != null)
                    {
                        int i = 1;
                        foreach (var manager in cinfo.responseModel.managers)
                        {
                            var Dic_manager = new Dictionary<string, object>
                            {
                            {"ARR_IDX",  i++},
                            { "AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_TITLE", manager.titleCode },
                            { "AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_TITLE_TEXT", NationalityService.GetTitleTextThai(manager.titleCode)},

                            { "APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_FIRSTNAME", manager.firstNameTH },
                            { "APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_LASTNAME", manager.lastNameTH },
                            { "APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ID_CARD", (manager.nationCode == "TH") ? manager.identityID : "" },
                            { "APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_PASSPORT_NUMBER", (manager.nationCode != "TH") ? manager.identityID : "" },
                            { "APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_BIRTH_DATE", DBDUtility.GetDateFormat(manager.birthDate) },
                            { "APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_BIRTH_DATE_AGE", manager.age },

                            { "DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_NATIONALITY", manager.nationCode },
                            { "DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_NATIONALITY_TEXT",NationalityService.GetNationalityText(manager.nationCode)},
                            //
                            { "ADDRESS_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.houseNo },
                            { "ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.moo },
                            { "ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.village },
                            { "ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.soi },
                            { "ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.building },
                            { "ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.roomNo },
                            { "ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.buildingFloor },
                            { "ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.road },

                            { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.provinceCode },
                            { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_TEXT",GeoService.GetProvinceText(manager.provinceCode)},

                            { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.amphurCode },
                            { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_TEXT", GeoService.GetAmphurText(manager.provinceCode,manager.amphurCode)},

                            { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.tumbonCode },
                            { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_TEXT", GeoService.GetTambolText(manager.provinceCode,manager.amphurCode,manager.tumbonCode) },



                            { "ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.postCode },
                            { "ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.telephone },
                            { "ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.telephone_ext },
                            { "ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS", manager.fax }
                        };
                            managerList.Add(Dic_manager);

                        }
                        managerInfo.ArrayData = managerList;
                    }
                }

                //วันที่เริ่มประกอบกิจการ  
                if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_START_IN_THAILAND_SECTION"))
                {
                    var start_in_thailand_Info = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_START_IN_THAILAND_SECTION", SectionType.Form);
                    start_in_thailand_Info.FormData = new Dictionary<string, object>
                {
                    { "APP_ELECTRONIC_COMMERCIAL_EDIT_START_IN_THAILAND_SECTION_DATE", DBDUtility.GetDateFormat(cinfo.responseModel.startDate)},
                };
                }

                //วันที่ขอจดทะเบียน
                if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_REQUEST_SECTION"))
                {
                    var request_Info = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_REQUEST_SECTION", SectionType.Form);
                    request_Info.FormData = new Dictionary<string, object>
                {
                    { "APP_ELECTRONIC_COMMERCIAL_EDIT_REQUEST_SECTION_DATE",DBDUtility.GetDateFormat(cinfo.responseModel.registerDate)},
                };
                }

                //ข้อมูลการโอน
                if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION"))
                {
                    var transfer_Info = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION", SectionType.Form);
                    if (cinfo.responseModel.transfer != null)
                    {
                        transfer_Info.FormData = new Dictionary<string, object>
                    {
                        {"ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.amphurCode},
                        {"ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.building},
                        {"ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.buildingFloor},
                        {"ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS",cinfo.responseModel.transfer.fax},
                        {"ADDRESS_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.houseNo},
                        {"ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.moo},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_COMMERCIAL_NAME" , ""},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_COMMERCIAL_NUMBER", ""},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_REQUEST_NUMBER" ,""},
                        {"ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.road},
                        {"ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.roomNo},
                        {"ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS",cinfo.responseModel.transfer.soi},
                        {"ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.telephone},
                        {"ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.telephone_ext},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_DATE",cinfo.responseModel.transfer.transferDate},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_FIRSTNAME" ,cinfo.responseModel.transfer.firstnameTH},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ID_CARD" ,cinfo.responseModel.transfer.transfererIdentityID},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_LASTNAME" ,cinfo.responseModel.transfer.transfererLastName},
                        {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_NATIONALITY" ,cinfo.responseModel.transfer.transfererNation},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_CAUSE" , ""},
                        {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_TITLE", cinfo.responseModel.transfer.transfererTitle},
                        {"ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.tumbonCode},
                        {"ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.village},
                        {"ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.postCode},
                        {"ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS" ,cinfo.responseModel.transfer.provinceCode},
                    };
                    }
                }

                // ข้อมูลสาขา
                if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION"))
                {
                    var BranchesInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION", SectionType.ArrayOfForms);
                    List<Dictionary<string, object>> BranchesList = new List<Dictionary<string, object>>();
                    int b = 0;
                    if (cinfo.responseModel.branchs != null)
                    {
                        int i = 1;
                        foreach (var branch in cinfo.responseModel.branchs)
                        {
                            var Dic_branch = new Dictionary<string, object>
                                    {
                                        {"ARR_IDX",  i++},
                                        { "ADDRESS_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.houseNo },
                                        { "ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.moo },
                                        { "ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.village },
                                        { "ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.soi },
                                        { "ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.building },
                                        { "ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.roomNo },
                                        { "ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.buildingFloor },
                                        { "ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.road },


                                        { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.provinceCode },
                                        { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_TEXT", GeoService.GetProvinceText(branch.provinceCode)},


                                        { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.amphurCode },
                                        { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_TEXT", GeoService.GetAmphurText(branch.provinceCode,branch.amphurCode) },


                                        { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.tumbonCode },
                                        { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_TEXT", GeoService.GetTambolText(branch.provinceCode,branch.amphurCode,branch.tumbonCode) },


                                        { "ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.postCode },
                                        { "ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.telephone },
                                        { "ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.telephone_ext },
                                        { "ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS", branch.fax }
                                    };
                            BranchesList.Add(Dic_branch);
                            b++;
                        }
                        BranchesInfo.ArrayData = BranchesList;
                    }
                }

                //ข้อมูลที่ตั้งโรงเก็บสินค้า
                if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION"))
                {
                    var WareHouseInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION", SectionType.ArrayOfForms);
                    List<Dictionary<string, object>> WareHouseList = new List<Dictionary<string, object>>();
                    int w = 0;
                    if (cinfo.responseModel.warehouses != null)
                    {
                        int i = 1;
                        foreach (var warehouse in cinfo.responseModel.warehouses)
                        {
                            var Dic_warehouse = new Dictionary<string, object>
                            {
                            {"ARR_IDX",  i++},
                            { "ADDRESS_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.houseNo },
                            { "ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.moo },
                            { "ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.village },
                            { "ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.soi },
                            { "ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.building },
                            { "ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.roomNo },
                            { "ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.buildingFloor },
                            { "ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.road },


                            { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.provinceCode },
                            { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_TEXT",  GeoService.GetProvinceText(warehouse.provinceCode)},


                            { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.amphurCode },
                            { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_TEXT", GeoService.GetAmphurText(warehouse.provinceCode,warehouse.amphurCode)},


                            { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.tumbonCode },
                            { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_TEXT", GeoService.GetTambolText(warehouse.provinceCode,warehouse.amphurCode,warehouse.tumbonCode)},



                            { "ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.postCode },
                            { "ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.telephone },
                            { "ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.telephone_ext },
                            { "ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS", warehouse.fax }
                            };
                            WareHouseList.Add(Dic_warehouse);
                            w++;
                        }
                        WareHouseInfo.ArrayData = WareHouseList;
                    }
                }

                //ตัวแทนค้าต่าง
                if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION"))
                {
                    var AgentsInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION", SectionType.ArrayOfForms);
                    List<Dictionary<string, object>> AgentsList = new List<Dictionary<string, object>>();
                    int a = 0;
                    if (cinfo.responseModel.agents != null)
                    {
                        int i = 1;
                        foreach (var agent in cinfo.responseModel.agents)
                        {
                            var Dic_agent = new Dictionary<string, object>
                                        {
                                            {"ARR_IDX",  i++},
                                            { "APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_NAME", agent.agentName },
                                            { "ADDRESS_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.houseNo },
                                            { "ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.moo },
                                            { "ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.village },
                                            { "ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.soi },
                                            { "ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.building },
                                            { "ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.roomNo },
                                            { "ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.buildingFloor },
                                            { "ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.road },


                                            { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.provinceCode },
                                            { "ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_TEXT",GeoService.GetProvinceText(agent.provinceCode)},


                                            { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.amphurCode },
                                            { "ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_TEXT", GeoService.GetAmphurText(agent.provinceCode,agent.amphurCode)},



                                            { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.tumbonCode },
                                            { "ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_TEXT", GeoService.GetAmphurText(agent.provinceCode,agent.amphurCode,agent.tumbonCode)},



                                            { "ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.postCode },
                                            { "ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.telephone },
                                            { "ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.telephone_ext },
                                            { "ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS", agent.fax }
                                        };

                            AgentsList.Add(Dic_agent);
                            a++;
                        }
                        AgentsInfo.ArrayData = AgentsList;
                    }
                }

                //เว็บไซด์
                if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION"))
                {
                    var WebSiteInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION", SectionType.Form);
                    if (cinfo.responseModel.webSite != null)
                    {


                        var res = cinfo.responseModel.webSite.urlChannel;
                        string str_url = string.Empty;
                        //
                        var Other = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION", SectionType.Form);
                        if (cinfo.responseModel.webSite.urlChannel != null)
                        {
                            Other.FormData = new Dictionary<string, object>
                        {
                            {"APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_ELECTRONIC_MEDIA_OPTION",cinfo.responseModel.webSite.urlChannel}
                        };

                            foreach (URLC_CODE url in (URLC_CODE[])Enum.GetValues(typeof(URLC_CODE)))
                            {
                                int code = (int)url;
                                if (cinfo.responseModel.webSite.urlChannel == code.ToString("D2"))
                                {
                                    str_url = url.GetEnumStringValue();
                                    //webSite.websiteURL = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_NAME_" + url);

                                }
                            }


                        }





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


                        WebSiteInfo.FormData = new Dictionary<string, object>
                    {

                        //order
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ORDER_SYSTEM_BASKET",(cinfo.responseModel.webSite.orderMethods.Count > 0 && cinfo.responseModel.webSite.orderMethods != null) ? cinfo.responseModel.webSite.orderMethods.Any(e => Convert.ToInt16(e.orderMethodCode).Equals((Int16)OrderMethodCode.BASKET))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ORDER_SYSTEM_FORM", (cinfo.responseModel.webSite.orderMethods.Count > 0 && cinfo.responseModel.webSite.orderMethods != null) ? cinfo.responseModel.webSite.orderMethods.Any(e => Convert.ToInt16(e.orderMethodCode).Equals((Int16)OrderMethodCode.FORM))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ORDER_SYSTEM_EMAIL",(cinfo.responseModel.webSite.orderMethods.Count > 0 && cinfo.responseModel.webSite.orderMethods != null) ? cinfo.responseModel.webSite.orderMethods.Any(e => Convert.ToInt16(e.orderMethodCode).Equals((Int16)OrderMethodCode.EMAIL))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ORDER_SYSTEM_PHONE",(cinfo.responseModel.webSite.orderMethods.Count > 0 && cinfo.responseModel.webSite.orderMethods != null) ? cinfo.responseModel.webSite.orderMethods.Any(e => Convert.ToInt16(e.orderMethodCode).Equals((Int16)OrderMethodCode.PHONE))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ORDER_SYSTEM_FAX",(cinfo.responseModel.webSite.orderMethods.Count > 0 && cinfo.responseModel.webSite.orderMethods != null) ? cinfo.responseModel.webSite.orderMethods.Any(e => Convert.ToInt16(e.orderMethodCode).Equals((Int16)OrderMethodCode.FAX))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ORDER_SYSTEM_OTHER",(cinfo.responseModel.webSite.orderMethods.Count > 0 && cinfo.responseModel.webSite.orderMethods != null) ? cinfo.responseModel.webSite.orderMethods.Any(e => Convert.ToInt16(e.orderMethodCode).Equals((Int16)OrderMethodCode.OTHER))? "true":"false" : "false"},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ORDER_SYSTEM_OTHER_TEXT",orderOther},

                        //payment
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_PAYMENT_OFFLINE",cinfo.responseModel.webSite.paymentMethods.Count > 0? cinfo.responseModel.webSite.paymentMethods.Any(e => Convert.ToInt16(e.paymentMethodCode).Equals((Int16)PaymentMethodCode.OFFLINE))? "true":"false" : "false"},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_PAYMENT_ONLINE_CREDIT_CARD",cinfo.responseModel.webSite.paymentMethods.Count > 0? cinfo.responseModel.webSite.paymentMethods.Any(e => Convert.ToInt16(e.paymentMethodCode).Equals((Int16)PaymentMethodCode.ONLINE_CREDIT_CARD))? "true":"false" : "false"},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_PAYMENT_ONLINE_E_BANKING",cinfo.responseModel.webSite.paymentMethods.Count > 0? cinfo.responseModel.webSite.paymentMethods.Any(e => Convert.ToInt16(e.paymentMethodCode).Equals((Int16)PaymentMethodCode.ONLINE_E_BANKING))? "true":"false" : "false"},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_PAYMENT_ONLINE_AGENT",cinfo.responseModel.webSite.paymentMethods.Count > 0? cinfo.responseModel.webSite.paymentMethods.Any(e => Convert.ToInt16(e.paymentMethodCode).Equals((Int16)PaymentMethodCode.ONLINE_AGENT))? "true":"false" : "false"},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_PAYMENT_OTHER",cinfo.responseModel.webSite.paymentMethods.Count > 0? cinfo.responseModel.webSite.paymentMethods.Any(e => Convert.ToInt16(e.paymentMethodCode).Equals((Int16)PaymentMethodCode.OTHER))? "true":"false" : "false"},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_PAYMENT_OTHER_TEXT",paymentOther },

                        //delivery
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_DELIVER_TRANSPORT_COMPANY",cinfo.responseModel.webSite.deliveryMethods.Count>0? cinfo.responseModel.webSite.deliveryMethods.Any(e => Convert.ToInt16(e.deliverMethodCode).Equals((Int16)DeliveryMethodCode.TRANSPORT_COMPANY))? "true":"false" : "false"},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_DELIVER_POST_OFFICE",cinfo.responseModel.webSite.deliveryMethods.Count>0? cinfo.responseModel.webSite.deliveryMethods.Any(e => Convert.ToInt16(e.deliverMethodCode).Equals((Int16)DeliveryMethodCode.POST_OFFICE))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_DELIVER_DELEVERY_STAFF",cinfo.responseModel.webSite.deliveryMethods.Count>0? cinfo.responseModel.webSite.deliveryMethods.Any(e => Convert.ToInt16(e.deliverMethodCode).Equals((Int16)DeliveryMethodCode.DELEVERY_STAFF))? "true":"false" : "false"},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_DELIVER_DOWNLOAD",cinfo.responseModel.webSite.deliveryMethods.Count>0? cinfo.responseModel.webSite.deliveryMethods.Any(e => Convert.ToInt16(e.deliverMethodCode).Equals((Int16)DeliveryMethodCode.DOWNLOAD))? "true":"false" : "false"},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_DELIVER_EMAIL",cinfo.responseModel.webSite.deliveryMethods.Count>0? cinfo.responseModel.webSite.deliveryMethods.Any(e => Convert.ToInt16(e.deliverMethodCode).Equals((Int16)DeliveryMethodCode.EMAIL))? "true":"false" : "false"},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_DELIVER_OTHER",cinfo.responseModel.webSite.deliveryMethods.Count>0? cinfo.responseModel.webSite.deliveryMethods.Any(e => Convert.ToInt16(e.deliverMethodCode).Equals((Int16)DeliveryMethodCode.OTHER))? "true":"false" : "false" },
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_DELIVER_OTHER_TEXT",deliveryOther},
                         //TypeOfBusiness                       
                        {"AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_TYPE",cinfo.responseModel.webSite.typeOfBussiness[0].typeOfBussinessCode},
                        {"AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_TYPE_TEXT",cinfo.responseModel.webSite.typeOfBussiness[0].description },



                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_NAME_"+str_url,cinfo.responseModel.webSite.websiteURL},
                        //{"AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_TYPE",""},


                        //{"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ORDER_SYSTEM_OTHER_TEXT",""},
                        //{"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_PAYMENT_OTHER_TEXT",""},
                        //{"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_DELIVER_OTHER_TEXT",""},
                        {"APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_BUDGET",cinfo.responseModel.webSite.webBudget.ToString()},



                        {"ADDRESS_EN_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.provinceCode},
                        {"ADDRESS_EN_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN_TEXT",GeoService.GetProvinceText(cinfo.responseModel.headOffice.provinceCode,"en")},

                        {"ADDRESS_EN_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.amphurCode},
                        {"ADDRESS_EN_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN_TEXT",GeoService.GetAmphurText(cinfo.responseModel.headOffice.provinceCode,cinfo.responseModel.headOffice.amphurCode,"en")},

                        {"ADDRESS_EN_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN",cinfo.responseModel.headOffice.tumbonCode},
                        {"ADDRESS_EN_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN_TEXT",GeoService.GetTambolText(cinfo.responseModel.headOffice.provinceCode,cinfo.responseModel.headOffice.amphurCode,cinfo.responseModel.headOffice.tumbonCode,"en")}



                      };
                    }


                }


                #region Address ของเว็บใช้ดึงมาจาก ข้อมูลที่อยู่ร้าน

                var storeAddr = model.SectionData.FirstOrDefault(o => o.SectionName == "INFORMATION_STORE");
                var websiteAddr = model.SectionData.FirstOrDefault(o => o.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION");
                if (storeAddr != null && websiteAddr != null)
                {
                    websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", storeAddr.FormData.TryGetString("ADDRESS_INFORMATION_STORE__ADDRESS", ""));
                    websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_MOO_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", storeAddr.FormData.TryGetString("ADDRESS_MOO_INFORMATION_STORE__ADDRESS", ""));

                    if (websiteAddr.FormData.TryGetString("ADDRESS_EN_SOI_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", "") == "")
                        websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_SOI_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", cinfo.responseModel.headOffice?.soiEN);

                    if (websiteAddr.FormData.TryGetString("ADDRESS_EN_BUILDING_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", "") == "")
                        websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_BUILDING_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", cinfo.responseModel.headOffice?.buildingEN);

                    if (websiteAddr.FormData.TryGetString("ADDRESS_EN_ROAD_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", "") == "")
                        websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_ROAD_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", cinfo.responseModel.headOffice?.roadEN);

                    websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_ROOMNO_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", storeAddr.FormData.TryGetString("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS", ""));
                    websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_FLOOR_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", storeAddr.FormData.TryGetString("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS", ""));
                    websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", storeAddr.FormData.TryGetString("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS", ""));
                    websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", storeAddr.FormData.TryGetString("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS", ""));
                    websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", storeAddr.FormData.TryGetString("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS", ""));
                    websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_POSTCODE_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", storeAddr.FormData.TryGetString("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS", ""));

                    var provinceId = websiteAddr.FormData.TryGetString("ADDRESS_EN_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", "");
                    var amphurId = websiteAddr.FormData.TryGetString("ADDRESS_EN_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", "");
                    var tumbolId = websiteAddr.FormData.TryGetString("ADDRESS_EN_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN", "");
                    if (provinceId != "")
                    {
                        var province = GeoService.Provinces("", "", "en").FirstOrDefault(o => o.ID == provinceId);
                        if (province != null) websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN_TEXT", province.Text);

                        var amphur = GeoService.Amphurs(provinceId, "", "en").FirstOrDefault(o => o.ID == amphurId);
                        if (amphur != null) websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN_TEXT", amphur.Text);

                        var tumbol = GeoService.Tambols(provinceId, amphurId, "", "en").FirstOrDefault(o => o.ID == tumbolId);
                        if (tumbol != null) websiteAddr.FormData.AddOrUpdate("ADDRESS_EN_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN_TEXT", tumbol.Text);
                    }

                }
                #endregion

                #region Juristic   
                if (model.IdentityType.Equals(UserTypeEnum.Juristic))
                {

                    // Frontis: Store juristic type in APP_ELECTRONIC_COMMERCIAL_EDIT_REQUEST_SECTION_JURISTIC_TYPE, it will be used in display condition.
                    var reqSection = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_REQUEST_SECTION", SectionType.Form);
                    if (reqSection.FormData.TryGetString("APP_ELECTRONIC_COMMERCIAL_EDIT_REQUEST_SECTION_JURISTIC_TYPE", "") == "")
                    {
                        var profile = IdentityHelper.GetJuristicProfile(model.IdentityID);
                        string juristicType = profile["JuristicType"].DefaultString();
                        reqSection.FormData.AddOrUpdate("APP_ELECTRONIC_COMMERCIAL_EDIT_REQUEST_SECTION_JURISTIC_TYPE", juristicType);
                    }

                    //ข้อมูลผู้เป็นหุ้นส่วนของทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์
                    if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION"))
                    {
                        var partnerInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION", SectionType.ArrayOfForms);
                        List<Dictionary<string, object>> partnerList = new List<Dictionary<string, object>>();
                        if (cinfo.responseModel.partners != null)
                        {
                            int i = 1;
                            foreach (var partner in cinfo.responseModel.partners)
                            {
                                var dic = new Dictionary<string, object>
                                            {
                                                // Frontis: To track if this partner is pulled from DBD or manually added
                                               // {"ARR_IDX",  i++},
                                                {"APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_IS_FROM_DBD", true},
                                                {"AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_TITLE",partner.titleCode },
                                                {"AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_TITLE_TEXT", NationalityService.GetTitleTextThai(partner.titleCode)},

                                                {"APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_BIRTH_DATE_AGE",partner.age},
                                                {"APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_BIRTH_DATE",DBDUtility.GetDateFormat(partner.birthDate)},
                                                {"APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_FIRSTNAME",partner.firstName},
                                                {"ADDRESS_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS",partner.houseNo},
                                                {"APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ID_CARD", (partner.nationCode == "TH") ? partner.identityID : "" },
                                                {"APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_PASSPORT_NUMBER", (partner.nationCode != "TH") ? partner.identityID : "" },
                                                {"APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_AMOUNT",partner.investAmt},
                                                {"APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_LASTNAME",partner.lastName},
                                                {"ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS",partner.postCode},
                                                {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_RACE",partner.race},
                                                {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_RACE_TEXT",NationalityService.GetNationText(partner.race)},
                                                {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_NATIONALITY", partner.nationCode },
                                                {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_NATIONALITY_TEXT",NationalityService.GetNationalityText(partner.nationCode)},
                                                {"ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS",partner.tumbonCode },
                                                {"ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS", partner.amphurCode},
                                                {"ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS", partner.provinceCode},

                                                {"ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_TEXT", GeoService.GetProvinceText(partner.provinceCode)},
                                                {"ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_TEXT", GeoService.GetAmphurText(partner.provinceCode,partner.amphurCode)},
                                                {"ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_TEXT", GeoService.GetTambolText(partner.provinceCode,partner.amphurCode,partner.tumbonCode)},
                                                {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_INVEST_TYPE",NationalityService.GetInvestType(partner.investCode)},
                                                {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_INVEST_TYPE_TEXT", DBDUtility.GetInvestCode().FirstOrDefault(x => x.Key ==Convert.ToUInt32(partner.investCode)).Value},

                                                {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_STATUS",partner.status },
                                                {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_STATUS_TEXT",NationalityService.GetPartnerStatus(partner.status)},
                                                {"ARR_IDX",partner.seqNo},
                                                {"ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS",partner.telephone},
                                            };

                                partnerList.Add(dic);
                            }
                            partnerInfo.ArrayData = partnerList;
                        }

                    }



                    // ShareHolder


                    if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2"))
                    {
                        List<Dictionary<string, object>> shareholderList = new List<Dictionary<string, object>>();
                        if (cinfo.responseModel.shareholders != null)
                        {
                            var shareholderInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2", SectionType.ArrayOfForms);
                            int i = 0;
                            foreach (var shareholder in cinfo.responseModel.shareholders)
                            {

                                var dic = new Dictionary<string, object>
                                            {

                                                {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2_NATIONALITY_TEXT",NationalityService.GetNationText(shareholder.nationCode.DefaultString()) },
                                                {"DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2_NATIONALITY",shareholder.nationCode.DefaultString()},

                                                {"APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2_SHARE",shareholder.shareAmt.DefaultString() },
                                                {"ARR_IDX", ++i }, // ข้อมูลแต่ละ record ใน array of form ต้องมีฟิลด์ ARR_IDX เสมอ
                                                //{"APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2_SHARE_BATH",shareholder.shareValue.DefaultString() }
                                            };



                                shareholderList.Add(dic);


                                decimal.TryParse(shareholder.shareAmt.DefaultString(), out ShareAmout);
                                TotalShareAmout = TotalShareAmout + ShareAmout;


                                decimal.TryParse(shareholder.shareValue.DefaultString(), out ShareValue);

                            }
                            shareholderInfo.ArrayData = shareholderList;

                            //  REGISTERED_CAPITAL = Total_ShareAmout * ShareValue;


                        }
                    }


                    //ข้อมูลมูลค่าหุ้น ยัง map ไม่ได้เนื่องจาก DBD ไม่ได้รับค่านี้และไม่ได้ส่งคืนมา
                    if (!darftData.SectionData.Any(e => e.SectionName == "APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION"))
                    {
                        var stockInfo = GetSectionData(model, "APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION", SectionType.Form);
                        stockInfo.FormData = new Dictionary<string, object>
                        {
                            {"APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_REGISTERED_CAPITAL",budgetAmt},
                            {"APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_SEPERATED_TO",TotalShareAmout.ToString()},
                            {"APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_SHARE_BATH",ShareValue.ToString()}
                        };

                    }


                }

                #endregion

                // ลบไฟล์เก่า
                var singleFormTrans = SingleFormTransaction.Get(model.IdentityID);
                var fileGroup = singleFormTrans.UploadedFiles.Where(o => o.Description == "APP_ELECTONIC_COMMERCIAL_EDIT_SECTION" || o.Description == "FREE_DOC_SECTION").ToList();
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
                            var lschangeDesc = new List<string>();
                            var is_electronic = string.Empty;
                            var post = new CommerceDataService()
                            {
                                reqNo = model.ApplicationRequestNumber.ToString(),
                                bizReqNo = model.ApplicationRequestID.ToString(),
                                bizReqDateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", new CultureInfo("en")),
                                changeDate = DateTime.Now.ToString("yyyyMMdd", new CultureInfo("th-TH")),
                                commerceRegistInfo = new CommerceRegistInfo()
                                {
                                    registerNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION_NUMBER"),
                                    ownerType = DBDUtility.GetOwnerType().FirstOrDefault(x => x.Value == model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("INFORMATION__REQUEST_AS_OPTION")).Key.ToString(),
                                    officeCode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("AJAX_DROPDOWN_INFORMATION_STORE_OFFICE_CODE"),
                                    // commerceNameEN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_EN"),
                                    // commerceNameTH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH"),
                                    commerceNo = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("IDENTITY_ID"),
                                    //headOffice = new ViewModels.Apps.DBDStandard.HeadOffice()
                                    //{
                                    //    amphurCode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS"),
                                    //    building = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"),
                                    //    buildingFloor = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS"),
                                    //    fax = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS"),
                                    //    houseNo = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS"),
                                    //    moo = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS"),
                                    //    postCode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"),
                                    //    provinceCode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"),
                                    //    road = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"),
                                    //    roomNo = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS"),
                                    //    soi = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS"),
                                    //    telephone = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                                    //    telephone_ext = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"),
                                    //    tumbonCode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS"),
                                    //    village = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_VILLAGE_INFORMATION_STORE__ADDRESS"),
                                    //    email = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS"),

                                    //},
                                    commerceStatus = 1,
                                    //delistDate = "25620219",
                                    //isElectronic = "Y",                               
                                    //otherInfo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_OTHER_NON_ELECTRONIC_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_OTHER_NON_ELECTRONIC_SECTION_OTHER")
                                }
                            };
                            var oldData = getCommerceInfo(model.IdentityID, model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION_NUMBER"));
                            var Is_STORE_NAME = model.Data.TryGetData("INFORMATION_STORE").ThenGetBooleanData("CHECKBOX_SHOW_INFORMATION_STORE_NAME");
                            var Is_STORE_ADDRESS = model.Data.TryGetData("INFORMATION_STORE").ThenGetBooleanData("CHECKBOX_SHOW_INFORMATION_STORE_ADDRESS");

                            if (Is_STORE_NAME)
                            {
                                var C_InStore = ((int)CommercialSection.Information_Store).ToString();
                                lschangeDesc.Add(C_InStore);

                                // var C_HeadOffice = ((int)CommercialSection.Commercial_HeadOffice).ToString();
                                // lschangeDesc.Add(C_HeadOffice);

                                post.commerceRegistInfo.commerceNameEN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_EN");
                                post.commerceRegistInfo.commerceNameTH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH");
                            }

                            if (Is_STORE_ADDRESS)
                            {
                                // var C_InStore = ((int)CommercialSection.Information_Store).ToString();
                                // lschangeDesc.Add(C_InStore);

                                var C_HeadOffice = ((int)CommercialSection.Commercial_HeadOffice).ToString();
                                lschangeDesc.Add(C_HeadOffice);

                                var headOffice = new ViewModels.Apps.DBDStandard.HeadOffice()
                                {
                                    amphurCode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS"),
                                    building = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"),
                                    buildingFloor = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS"),
                                    fax = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS"),
                                    houseNo = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS"),
                                    moo = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS"),
                                    postCode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"),
                                    provinceCode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"),
                                    road = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"),
                                    roomNo = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS"),
                                    soi = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS"),
                                    telephone = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                                    telephone_ext = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS"),
                                    tumbonCode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS"),
                                    village = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_VILLAGE_INFORMATION_STORE__ADDRESS"),
                                    email = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS")

                                };
                                post.commerceRegistInfo.headOffice = headOffice;
                            }

                            #region [Objective]
                            //var IS_INFO_SECTION = Convert.ToBoolean(model.Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INFO_SECTION"].Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INFO_SECTION_CHECKBOX_EDIT_EDIT_INFO_SECTION_CHECKED"]);
                            var IS_INFO_SECTION = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INFO_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INFO_SECTION_CHECKBOX_EDIT_EDIT_INFO_SECTION_CHECKED");
                            if (IS_INFO_SECTION)
                            {
                                //var Is_Info_Section =  Convert.ToBoolean(model.Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INFO_SECTION"].Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INFO_SECTION_CHECKBOX_EDIT_EDIT_INFO_SECTION_CHECKED"]);
                                //if (Is_Info_Section)
                                //{
                                is_electronic = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE_OPTION");
                                if (is_electronic == "ELECTRONIC")
                                {
                                    post.commerceRegistInfo.isElectronic = "Y";
                                    //var c_Electronic = ((int)CommercialSection.Commercial_Electronic).ToString();
                                    //lschangeDesc.Add(c_Electronic);
                                }
                                else
                                {
                                    post.commerceRegistInfo.isElectronic = "N";
                                }
                                // }
                                var c_type = ((int)CommercialSection.Commercial_Type).ToString();

                                lschangeDesc.Add(c_type);

                                var str = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_TYPE");
                                JArray parsedArray = JArray.Parse(str);
                                var objectiveList = new List<ViewModels.Apps.DBDStandard.Objective>();
                                int j = 1;
                                foreach (JObject parsedObject in parsedArray.Children<JObject>())
                                {
                                    var objective = new ViewModels.Apps.DBDStandard.Objective();
                                    foreach (JProperty parsedProperty in parsedObject.Properties())
                                    {

                                        string propertyName = parsedProperty.Name;
                                        if (propertyName.Equals("TYPE_TEXT"))
                                        {
                                            objective.description = (string)parsedProperty.Value;

                                        }
                                        if (propertyName.Equals("TYPE"))
                                        {
                                            objective.objectiveCode = (string)parsedProperty.Value;

                                        }
                                    }
                                    objective.seqNo = j++;
                                    objectiveList.Add(objective);
                                }
                                post.commerceRegistInfo.objectives = objectiveList;
                            }
                            #endregion

                            #region [investment]
                            //var IS_INVESTMENT_SECTION = Convert.ToBoolean(model.Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INVESTMENT_SECTION"].Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INVESTMENT_SECTION_CHECKBOX_EDIT_EDIT_INVESTMENT_SECTION_CHECKED"]);
                            var IS_INVESTMENT_SECTION = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INVESTMENT_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INVESTMENT_SECTION_CHECKBOX_EDIT_EDIT_INVESTMENT_SECTION_CHECKED");
                            if (IS_INVESTMENT_SECTION)
                            {

                                var c_Investment = ((int)CommercialSection.Commercial_Investment).ToString();
                                lschangeDesc.Add(c_Investment);
                                post.commerceRegistInfo.budgetAmt = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_INVESTMENT_SECTION").ThenGetDoubleData("APP_ELECTRONIC_COMMERCIAL_EDIT_INVESTMENT_SECTION_AMOUNT");
                            }
                            //else
                            //{

                            //    post.commerceRegistInfo.budgetAmt = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_INVESTMENT_SECTION").ThenGetDoubleData("APP_ELECTRONIC_COMMERCIAL_INVESTMENT_SECTION_AMOUNT");
                            //}
                            //var budgetAmt = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_INVESTMENT_SECTION").ThenGetIntData("APP_ELECTRONIC_COMMERCIAL_EDIT_INVESTMENT_SECTION_AMOUNT");
                            #endregion

                            #region [Managers]
                            //var IS_MANAGER_SECTION = Convert.ToBoolean(model.Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_MANAGER_SECTION"].Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_MANAGER_SECTION_CHECKBOX_EDIT_EDIT_MANAGER_SECTION_CHECKED"]);
                            var IS_MANAGER_SECTION = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_MANAGER_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_MANAGER_SECTION_CHECKBOX_EDIT_EDIT_MANAGER_SECTION_CHECKED");
                            if (IS_MANAGER_SECTION)
                            {

                                var c_MANAGER = ((int)CommercialSection.Commercial_Manager).ToString();
                                lschangeDesc.Add(c_MANAGER);

                                var managerTotal = int.Parse(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_TOTAL"));
                                if (managerTotal > 0)
                                {
                                    var managerList = new List<ViewModels.Apps.DBDStandard.Manager>();
                                    for (int i = 0; i < managerTotal; i++)
                                    {
                                        var manager = new ViewModels.Apps.DBDStandard.Manager()
                                        {

                                            age = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetIntData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_BIRTH_DATE_AGE_" + i),
                                            amphurCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            titleCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_TITLE_" + i),
                                            nationality = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_NATIONALITY_TEXT_" + i),
                                            birthDate = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetDateTHStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_BIRTH_DATE_" + i),
                                            building = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            buildingFloor = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            nationCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_NATIONALITY_" + i),
                                            firstNameEN = "-",
                                            lastNameEN = "-",
                                            firstNameTH = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_FIRSTNAME_" + i),
                                            lastNameTH = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_LASTNAME_" + i),
                                            houseNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            // identityID = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ID_CARD_" + i).Replace("-", ""),
                                            moo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            postCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            provinceCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            road = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            roomNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            seqNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetIntData("ARR_IDX_" + i),
                                            soi = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            telephone_ext = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            tumbonCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                            village = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ADDRESS_" + i),
                                        };
                                        if (manager.nationCode == "TH")
                                        {
                                            manager.identityID = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_ID_CARD_" + i).Replace("-", "");
                                        }
                                        else
                                        {
                                            manager.identityID = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_MANAGER_SECTION_PASSPORT_NUMBER_" + i).Replace("-", "");
                                        }
                                        managerList.Add(manager);
                                    }
                                    post.commerceRegistInfo.managers = managerList.ToList();
                                }
                            }
                            #endregion

                            //หน้า Form  firstNameTH, lastNameTH, identityID ไม่ได้ให้แก้อยู่แล้ว
                            if (post.commerceRegistInfo.ownerType == "1")
                            {
                                var owner = new ViewModels.Apps.DBDStandard.Owner()
                                {
                                    age = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetIntData("BIRTH_DATE_AGE"),
                                    amphurCode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS") ?? "",
                                    birthDate = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetDateTHStringData("BIRTH_DATE") ?? "",
                                    building = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_CITIZEN_ADDRESS") ?? "",
                                    buildingFloor = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_CITIZEN_ADDRESS") ?? "",
                                    fax = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_CITIZEN_ADDRESS") ?? "",
                                    firstNameEN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_FIRSTNAME_EN") ?? "",
                                    firstNameTH = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_NAME") ?? "",
                                    houseNo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_CITIZEN_ADDRESS") ?? "",
                                    identityID = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("IDENTITY_ID") ?? "",
                                    lastNameEN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME_EN") ?? "",
                                    lastNameTH = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME") ?? "",
                                    moo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS") ?? "",
                                    nationCode = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY") ?? "",
                                    postCode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS") ?? "",
                                    provinceCode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS") ?? "",
                                    race = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE") ?? "",
                                    road = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS") ?? "",
                                    roomNo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROOMNO_CITIZEN_ADDRESS") ?? "",
                                    soi = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS") ?? "",
                                    telephone = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS") ?? "",
                                    telephone_ext = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS") ?? "",
                                    titleCode = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("AJAX_DROPDOWN_CITIZEN_TITLE") ?? "",
                                    tumbonCode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS") ?? "",
                                    village = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_CITIZEN_ADDRESS") ?? "",
                                    email = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_EMAIL_CITIZEN_ADDRESS") ?? "",
                                };
                                var oldOwner = new ViewModels.Apps.DBDStandard.Owner
                                {
                                    age = oldData.responseModel.owner.age,
                                    amphurCode = oldData.responseModel.owner.amphurCode ?? "",
                                    birthDate = oldData.responseModel.owner.birthDate ?? "",
                                    building = oldData.responseModel.owner.building ?? "",
                                    buildingFloor = oldData.responseModel.owner.buildingFloor ?? "",
                                    fax = oldData.responseModel.owner.fax ?? "",
                                    firstNameEN = oldData.responseModel.owner.firstNameEN ?? "",
                                    firstNameTH = oldData.responseModel.owner.firstNameTH ?? "",
                                    houseNo = oldData.responseModel.owner.houseNo ?? "",
                                    identityID = oldData.responseModel.owner.identityID ?? "",
                                    lastNameEN = oldData.responseModel.owner.lastNameEN ?? "",
                                    lastNameTH = oldData.responseModel.owner.lastNameTH ?? "",
                                    moo = oldData.responseModel.owner.moo ?? "",
                                    nationCode = oldData.responseModel.owner.nationCode ?? "",
                                    postCode = oldData.responseModel.owner.postCode ?? "",
                                    provinceCode = oldData.responseModel.owner.provinceCode ?? "",
                                    race = oldData.responseModel.owner.race ?? "",
                                    road = oldData.responseModel.owner.road ?? "",
                                    roomNo = oldData.responseModel.owner.roomNo ?? "",
                                    soi = oldData.responseModel.owner.soi ?? "",
                                    telephone = oldData.responseModel.owner.telephone ?? "",
                                    telephone_ext = oldData.responseModel.owner.telephone_ext ?? "",
                                    titleCode = oldData.responseModel.owner.titleCode ?? "",
                                    tumbonCode = oldData.responseModel.owner.tumbonCode ?? "",
                                    village = oldData.responseModel.owner.village ?? "",
                                    email = oldData.responseModel.owner.email ?? "",
                                };

                                if (!string.Equals(JObject.FromObject(owner).ToString(Formatting.None), JObject.FromObject(oldOwner).ToString(Formatting.None)))
                                {
                                    post.commerceRegistInfo.owner = owner;
                                    var c_Owner = ((int)CommercialSection.Information_Owner).ToString();
                                    lschangeDesc.Add(c_Owner);
                                }
                            }

                            if (post.commerceRegistInfo.ownerType == "2")
                            {
                                //var owner = new ViewModels.Apps.DBDStandard.Owner()
                                //{
                                //    age = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetIntData("BIRTH_DATE_AGE"),
                                //    amphurCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS"),
                                //    birthDate = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetDateTHStringData("BIRTH_DATE"),
                                //    building = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS"),
                                //    buildingFloor = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS"),
                                //    fax = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_JURISTIC_HQ_ADDRESS"),
                                //    firstNameEN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_EN"),
                                //    firstNameTH = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_TH"),
                                //    houseNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                                //    identityID = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("IDENTITY_ID"),
                                //    lastNameEN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME_EN"),
                                //    lastNameTH = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME"),
                                //    moo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),
                                //    nationality = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY"),
                                //    postCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"),
                                //    provinceCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS"),
                                //    race = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_RACE"),
                                //    road = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"),
                                //    roomNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS"),
                                //    soi = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"),
                                //    telephone = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS"),
                                //    //titleCode = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("AJAX_DROPDOWN_CITIZEN_TITLE"),
                                //    tumbonCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS"),
                                //    village = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS"),
                                //    email = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS"),
                                //};
                                //post.commerceRegistInfo.owner = owner;

                                #region [Partner]

                                var Is_Partner_Check = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_SECTION_CHECKBOX_EDIT_EDIT_PARTNER_SECTION_CHECKED");
                                if (Is_Partner_Check)
                                {
                                    var partnerTotal = int.Parse(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_TOTAL"));
                                    if (partnerTotal > 0)
                                    {
                                        var c_Partner = ((int)CommercialSection.Commercial_Partner).ToString();
                                        lschangeDesc.Add(c_Partner);


                                        var partnerList = new List<Partner>();
                                        for (int i = 0; i < partnerTotal; i++)
                                        {
                                            var partner = new Partner()
                                            {
                                                age = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetIntData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_BIRTH_DATE_AGE_" + i),
                                                amphurCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),
                                                titleCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_TITLE_" + i),
                                                nationality = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_NATIONALITY_TEXT_" + i),
                                                nationCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_NATIONALITY_" + i),
                                                birthDate = "",//model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetDateTHStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_BIRTH_DATE_" + i),
                                                building = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),
                                                buildingFloor = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),
                                                investAmt = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetDoubleData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_AMOUNT_" + i),
                                                //investCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_INVEST_TYPE_" + i),
                                                investCode = DBDUtility.GetInvestCode().FirstOrDefault(x => x.Value == model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_INVEST_TYPE_TEXT_" + i)).Key.ToString("D2"),
                                                fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),
                                                firstName = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_FIRSTNAME_" + i),
                                                firstNameTH = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_FIRSTNAME_" + i),
                                                houseNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),

                                                identityID = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ID_CARD_" + i).Replace("-", ""),
                                                //lastNameEN = "-",
                                                lastNameTH = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_LASTNAME_" + i),
                                                moo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),

                                                postCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),
                                                provinceCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),
                                                road = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),
                                                roomNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),
                                                race = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_RACE_" + i),
                                                seqNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetIntData("ARR_IDX_" + i),
                                                soi = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),

                                                telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),
                                                telephone_ext = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),
                                                tumbonCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),
                                                status = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_STATUS_" + i),

                                                village = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ADDRESS_" + i),
                                            };
                                            if (partner.nationCode == "TH")
                                            {
                                                partner.identityID = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_ID_CARD_" + i).Replace("-", "");
                                            }
                                            else
                                            {
                                                partner.identityID = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_SECTION_PASSPORT_NUMBER_" + i).Replace("-", "");
                                            }
                                            partnerList.Add(partner);
                                        }
                                        post.commerceRegistInfo.partners = partnerList.ToList();
                                    }
                                }

                                #endregion

                                #region [Partner_Leave]


                                //APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_LEAVE_SECTION
                                //APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_LEAVE_SECTION_CHECKBOX_EDIT_EDIT_PARTNER_LEAVE_SECTION_CHECKED



                                #endregion

                                #region [Shareholder]

                                var Is_STOCK_Check = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_STOCK_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_STOCK_SECTION_CHECKBOX_EDIT_EDIT_STOCK_SECTION_CHECKED");
                                if (Is_STOCK_Check)
                                {
                                    var shareholderTotal = int.Parse(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2_TOTAL"));
                                    if (shareholderTotal > 0)
                                    {
                                        var c_Shareholder = ((int)CommercialSection.Commercial_Shareholder).ToString();
                                        lschangeDesc.Add(c_Shareholder);
                                        int share_index = 1;
                                        var shareholderList = new List<Shareholder>();
                                        for (int i = 0; i < shareholderTotal; i++)
                                        {

                                            var shareholder = new Shareholder()
                                            {

                                                nationality = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2_NATIONALITY_TEXT_" + i),
                                                nationCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2_NATIONALITY_" + i),
                                                seqNo = share_index++,//model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2").ThenGetIntData("ARR_IDX_" + i),
                                                shareAmt = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_2_SHARE_" + i),
                                                shareValue = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_SHARE_BATH"),
                                            };
                                            shareholderList.Add(shareholder);
                                        }
                                        post.commerceRegistInfo.shareholders = shareholderList.ToList();
                                    }
                                }

                                #endregion
                            }

                            #region [START_IN_THAILAND_SECTION]

                            //var c_Start_In_Thailand = ((int)CommercialSection.Commercial_Start_In_Thailand).ToString();
                            //lschangeDesc.Add(c_Start_In_Thailand);


                            //// var IS_START_IN_THAILAND_SECTION = Convert.ToBoolean(model.Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_START_IN_THAILAND_SECTION"].Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_START_IN_THAILAND_SECTION_CHECKBOX_EDIT_EDIT_START_IN_THAILAND_SECTION_CHECKED"]);
                            //var IS_START_IN_THAILAND_SECTION = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_START_IN_THAILAND_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_START_IN_THAILAND_SECTION_CHECKBOX_EDIT_EDIT_START_IN_THAILAND_SECTION_CHECKED");
                            //if (IS_START_IN_THAILAND_SECTION)
                            //{
                            //    var startDate = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_START_IN_THAILAND_SECTION").ThenGetDateTHStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_START_IN_THAILAND_SECTION_DATE");
                            //    post.commerceRegistInfo.startDate = startDate;
                            //}
                            //else
                            //{
                            //    var startDate = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_START_IN_THAILAND_SECTION").ThenGetDateTHStringData("APP_ELECTRONIC_COMMERCIAL_START_IN_THAILAND_SECTION_DATE");
                            //    post.commerceRegistInfo.startDate = startDate;
                            //}
                            #endregion

                            #region [COMMERCIAL_REQUEST_SECTION]
                            ////var IS__REQUEST_SECTION = Convert.ToBoolean(model.Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_REQUEST_SECTION"].Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_REQUEST_SECTION_CHECKBOX_EDIT_EDIT_REQUEST_SECTION_CHECKED"]);
                            //var IS__REQUEST_SECTION = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_REQUEST_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_REQUEST_SECTION_CHECKBOX_EDIT_EDIT_REQUEST_SECTION_CHECKED");
                            //var c_REQUEST = ((int)CommercialSection.Commercial_Request).ToString();
                            //lschangeDesc.Add(c_REQUEST);

                            //if (IS__REQUEST_SECTION)
                            //{
                            //    var registerDate = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_REQUEST_SECTION").ThenGetDateTHStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_REQUEST_SECTION_DATE");
                            //    post.commerceRegistInfo.registerDate = registerDate;
                            //}
                            //else
                            //{
                            //    var registerDate = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_REQUEST_SECTION").ThenGetDateTHStringData("APP_ELECTRONIC_COMMERCIAL_REQUEST_SECTION_DATE");
                            //    post.commerceRegistInfo.registerDate = registerDate;
                            //}
                            #endregion

                            #region [Transfer]
                            /*var is_transfer = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_TRANSFER_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_TRANSFER_SECTION_CHECKBOX_EDIT_EDIT_TRANSFER_SECTION_CHECKED");
                            if (is_transfer)
                            {

                                var c_Transfer = ((int)CommercialSection.Commercial_Transfer).ToString();
                                lschangeDesc.Add(c_Transfer);
                                var transfer = new ViewModels.Apps.DBDStandard.Transfer()
                                {
                                    amphurCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    building = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    buildingFloor = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    houseNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    moo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    oldCommerceName = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_COMMERCIAL_NAME"),
                                    postCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    provinceCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    refCommerce = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_COMMERCIAL_NUMBER"),
                                    refRegisterNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_REQUEST_NUMBER"),
                                    road = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    roomNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    soi = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    telephone_ext = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    transfererAge = "-",//model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData(""),
                                    transfererBirthDate = "-",//model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData(""),
                                                              // DateTime.Now.ToString("yyyyMMdd", new CultureInfo("th-TH"))
                                    transferDate = DBDUtility.GetDateFormat_Dashes(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_DATE")),
                                    transfererFristName = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_FIRSTNAME"),
                                    transfererIdentityID = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ID_CARD"),
                                    transfererLastName = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_LASTNAME"),
                                    transfererNation = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_NATIONALITY"),
                                    transferRece = "-",//model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION").ThenGetStringData(""),
                                    transferReason = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_CAUSE"),
                                    transfererTitle = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_TITLE"),
                                    tumbonCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    village = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION").ThenGetStringData("ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_ADDRESS"),
                                    trnPid = " 8530514205980",
                                    firstnameTH = "xxx"
                                };
                                post.commerceRegistInfo.transfer = transfer;
                            }*/
                            #endregion

                            #region [Branches]
                            //var Is_Branches = Convert.ToBoolean(model.Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_BRANCH_SECTION"].Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_BRANCH_SECTION_CHECKBOX_EDIT_EDIT_BRANCH_SECTION_CHECKED"]);
                            var Is_Branches = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_BRANCH_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_BRANCH_SECTION_CHECKBOX_EDIT_EDIT_BRANCH_SECTION_CHECKED");
                            if (Is_Branches)
                            {
                                var brancheTotal = int.Parse(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_TOTAL"));
                                if (brancheTotal > 0)
                                {
                                    var c_Branch = ((int)CommercialSection.Commercial_Branch).ToString();
                                    lschangeDesc.Add(c_Branch);


                                    var brancheList = new List<Branch>();
                                    for (int i = 0; i < brancheTotal; i++)
                                    {
                                        var branche = new Branch()
                                        {
                                            amphurCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            building = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            buildingFloor = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            houseNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            information = "-",// model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_BRANCH_SECTION").ThenGetStringData("" + i),
                                            moo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            postCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            provinceCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            road = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            roomNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            seqNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetIntData("ARR_IDX_" + i),
                                            soi = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            telephone_ext = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            tumbonCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                            village = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION").ThenGetStringData("ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_EDIT_BRANCH_SECTION_ADDRESS_" + i),
                                        };
                                        brancheList.Add(branche);
                                    }
                                    post.commerceRegistInfo.branchs = brancheList.ToList();
                                }
                            }
                            #endregion

                            #region [Warehouse]
                            // var Is_Warehouse = Convert.ToBoolean(model.Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_WAREHOUSE_SECTION"].Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_WAREHOUSE_SECTION_CHECKBOX_EDIT_EDIT_WAREHOUSE_SECTION_CHECKED"]);
                            var Is_Warehouse = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_WAREHOUSE_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_WAREHOUSE_SECTION_CHECKBOX_EDIT_EDIT_WAREHOUSE_SECTION_CHECKED");
                            if (Is_Warehouse)
                            {
                                var warehouseTotal = int.Parse(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_TOTAL"));
                                if (warehouseTotal > 0)
                                {

                                    var c_Warehouse = ((int)CommercialSection.Commercial_Warehouse).ToString();
                                    lschangeDesc.Add(c_Warehouse);

                                    var warehouseList = new List<ViewModels.Apps.DBDStandard.Warehouse>();
                                    for (int i = 0; i < warehouseTotal; i++)
                                    {
                                        var warehouse = new ViewModels.Apps.DBDStandard.Warehouse()
                                        {
                                            amphurCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            building = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            buildingFloor = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            houseNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            information = "-",//model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_WAREHOUSE_SECTION").ThenGetStringData("" + i),
                                            moo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            postCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            provinceCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            road = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            roomNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            seqNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetIntData("ARR_IDX_" + i),
                                            soi = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            telephone_ext = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            tumbonCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                            village = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION").ThenGetStringData("ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_EDIT_WAREHOUSE_SECTION_ADDRESS_" + i),
                                        };
                                        warehouseList.Add(warehouse);
                                    }
                                    post.commerceRegistInfo.warehouses = warehouseList.ToList();
                                }
                            }
                            #endregion

                            #region [Agents]
                            // var Is_Agents = Convert.ToBoolean(model.Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_AGENT_SECTION"].Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_AGENT_SECTION_CHECKBOX_EDIT_EDIT_AGENT_SECTION_CHECKED"]);
                            var Is_Agents = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_AGENT_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_AGENT_SECTION_CHECKBOX_EDIT_EDIT_AGENT_SECTION_CHECKED");
                            if (Is_Agents)
                            {
                                var agentTotal = int.Parse(model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_TOTAL"));
                                if (agentTotal > 0)
                                {
                                    var c_Agent = ((int)CommercialSection.Commercial_Agent).ToString();
                                    lschangeDesc.Add(c_Agent);

                                    var agentList = new List<ViewModels.Apps.DBDStandard.Agent>();
                                    for (int i = 0; i < agentTotal; i++)
                                    {
                                        var agent = new ViewModels.Apps.DBDStandard.Agent()
                                        {

                                            agentName = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_NAME_" + i),
                                            amphurCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_AMPHUR_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                            building = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_BUILDING_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                            buildingFloor = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_FLOOR_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                            fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_FAX_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                            houseNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                            moo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_MOO_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                            postCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_POSTCODE_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                            provinceCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_PROVINCE_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                            road = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_ROAD_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                            roomNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_ROOMNO_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                            seqNo = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetIntData("ARR_IDX" + i),
                                            soi = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_SOI_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                            telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                            telephone_ext = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS" + i),
                                            tumbonCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_TUMBOL_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                            village = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION").ThenGetStringData("ADDRESS_VILLAGE_APP_ELECTRONIC_COMMERCIAL_EDIT_AGENT_SECTION_ADDRESS_" + i),
                                        };
                                        agentList.Add(agent);
                                    }
                                    post.commerceRegistInfo.agents = agentList.ToList();
                                }
                            }
                            #endregion

                            #region [WebSite]
                            //var Is_Info_Section =  Convert.ToBoolean(model.Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INFO_SECTION"].Data["APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INFO_SECTION_CHECKBOX_EDIT_EDIT_INFO_SECTION_CHECKED"]);
                            //if (Is_Info_Section)
                            //{
                            //is_electronic = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE_OPTION");
                            //if (is_electronic == "ELECTRONIC")
                            //{
                            //    post.commerceRegistInfo.isElectronic = "Y";
                            //    var c_Electronic = ((int)CommercialSection.Commercial_Electronic).ToString();
                            //    lschangeDesc.Add(c_Electronic);
                            //}
                            //else
                            //{
                            //    post.commerceRegistInfo.isElectronic = "N";
                            //}
                            // }



                            var Is_Website = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_WEBSITE_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_WEBSITE_SECTION_CHECKBOX_EDIT_EDIT_WEBSITE_SECTION_CHECKED");
                            if (Is_Website)
                            {
                                var c_Electronic = ((int)CommercialSection.Commercial_Electronic).ToString();
                                lschangeDesc.Add(c_Electronic);

                                var is_electronic_OPTION = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE_OPTION");
                                if (is_electronic == "ELECTRONIC")
                                {
                                    //  post.commerceRegistInfo.isElectronic = "Y";
                                    var webSite = new ECommerceWebsite()
                                    {

                                        email = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_EMAIL"),
                                        fax = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetStringData("ADDRESS_EN_FAX_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN"),

                                        //  otherDescription = "บริการซื้อขำย",

                                        Telephone = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetStringData("ADDRESS_EN_TELEPHONE_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN"),
                                        typeOfBussiness = new List<TypeOfBussiness>()
                                        {
                                            new TypeOfBussiness()
                                            {
                                                description = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_TYPE_TEXT"),
                                                typeOfBussinessCode = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetStringData("AJAX_DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_TYPE"),
                                                seqNo = 1
                                            }
                                        },
                                        webBudget = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetDoubleData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_BUDGET"),
                                        // websiteURL = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_NAME"),
                                    };
                                    var orderMethods = new List<OrderMethod>();

                                    int i = 1;
                                    foreach (OrderMethodCode method in (OrderMethodCode[])Enum.GetValues(typeof(OrderMethodCode)))
                                    {
                                        bool is_chk = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ORDER_SYSTEM_" + method);
                                        if (is_chk)
                                        {
                                            string description = method.GetEnumStringValue();
                                            int orderMethodCode = (int)method;
                                            if (OrderMethodCode.OTHER == method)
                                            {
                                                description = description + model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ORDER_SYSTEM_" + method + "_TEXT");
                                            }
                                            var order = new OrderMethod()
                                            {
                                                description = description,
                                                orderMethodCode = orderMethodCode.ToString("D2"),
                                                seqNo = i++
                                            };
                                            orderMethods.Add(order);
                                        }
                                    }

                                    webSite.orderMethods = orderMethods.ToList();
                                    var deliveryMethods = new List<DeliveryMethod>();
                                    i = 1;
                                    foreach (DeliveryMethodCode method in (DeliveryMethodCode[])Enum.GetValues(typeof(DeliveryMethodCode)))
                                    {
                                        bool is_chk = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_DELIVER_" + method);
                                        if (is_chk)
                                        {
                                            string description = method.GetEnumStringValue();
                                            int code = (int)method;
                                            if (DeliveryMethodCode.OTHER == method)
                                            {
                                                description = description + model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_DELIVER_" + method + "_TEXT");
                                            }
                                            var delivery = new DeliveryMethod()
                                            {
                                                deliverMethodCode = code.ToString("D2"),
                                                description = description,
                                                seqNo = i++
                                            };
                                            deliveryMethods.Add(delivery);
                                        }
                                    }

                                    webSite.deliveryMethods = deliveryMethods.ToList();
                                    var paymentMethods = new List<PaymentMethod>();
                                    i = 1;
                                    foreach (PaymentMethodCode method in (PaymentMethodCode[])Enum.GetValues(typeof(PaymentMethodCode)))
                                    {
                                        bool is_chk = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_PAYMENT_" + method);
                                        if (is_chk)
                                        {
                                            string description = method.GetEnumStringValue();
                                            int code = (int)method;
                                            if (PaymentMethodCode.OTHER == method)
                                            {
                                                description = description + model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_PAYMENT_" + method + "_TEXT");
                                            }
                                            var payment = new PaymentMethod()
                                            {
                                                paymentMethodCode = code.ToString("D2"),
                                                description = description,
                                                seqNo = i++
                                            };
                                            paymentMethods.Add(payment);
                                        }
                                    }
                                    webSite.paymentMethods = paymentMethods.ToList();

                                    webSite.urlChannel = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_ELECTRONIC_MEDIA_OPTION");
                                    foreach (URLC_CODE url in (URLC_CODE[])Enum.GetValues(typeof(URLC_CODE)))
                                    {
                                        int code = (int)url;
                                        if (webSite.urlChannel == code.ToString("D2"))
                                        {
                                            webSite.websiteURL = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_NAME_" + url);

                                        }
                                    }

                                    post.commerceRegistInfo.webSite = webSite;

                                }
                                //else
                                //{
                                //    post.commerceRegistInfo.isElectronic = "N";
                                //}
                            }
                            #endregion

                            #region [Other]
                            var Is_Other = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_OTHER_SECTION").ThenGetBooleanData("APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_OTHER_SECTION_CHECKBOX_EDIT_EDIT_OTHER_SECTION_CHECKED");
                            if (Is_Other)
                            {


                                is_electronic = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE_OPTION");
                                if (is_electronic != "ELECTRONIC") // ถ้าเป็น พานิชย์ ธรรมดาถึงจะส่ง section 14
                                {
                                    var c_Other = ((int)CommercialSection.Commercial_Other).ToString();
                                    lschangeDesc.Add(c_Other);

                                    string text_Other = model.Data.TryGetData("APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION").ThenGetStringData("APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_OTHER");
                                    if (string.IsNullOrEmpty(text_Other))
                                    {
                                        text_Other = " ";
                                    }
                                    post.commerceRegistInfo.otherInfo = text_Other;
                                }

                            }
                            #endregion

                            #region [Attachs]
                            var attachList = new List<Attach>();
                            int file_index = 1;
                            foreach (FileGroup group in model.UploadedFiles)
                            {
                                foreach (var item in group.Files)
                                {
                                    var description = item.Extras.ContainsKey("FILETYPENAME") ? item.Extras["FILETYPENAME"].ToString() : string.Empty;
                                    string fileType = DBDUtility.GetFileTypeRef().FirstOrDefault(x => item.FileTypeCode.Contains(x.Value)).Key.ToString();

                                    fileType = fileType.Equals("0") ? "12" : fileType;

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
                            post.commerceRegistInfo.attachs = attachList.ToList();
                            #endregion

                            var con_toArray = lschangeDesc.ToArray().Distinct();
                            post.changeDesc = string.Join(",", con_toArray);

                            // Model data
                            string regisUrl = ConfigurationManager.AppSettings["DBD_COMMERCE_CHANGE_WS_URL"];
                            var jsonPost = JsonConvert.SerializeObject(post, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            }); // Serialize model data to JSON

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
                            singleformReq.DeleteSections(request.IdentityID, "APP_ELECTRONIC_COMMERCIAL_EDIT", new string[] { "REQUESTOR_INFORMATION__HEADER", "INFORMATION_STORE" });

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
                                        string error = "messageCode : " + apiResult["messageCode"].ToString() + ",messageDesc : " + apiResult["messageDesc"].ToString();
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
                                    }); // Serialize model data to JSON
                                //jsonPost = null;
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
                                            string error = "messageCode : " + apiResult["messageCode"].ToString() + ",messageDesc : " + apiResult["messageDesc"].ToString();
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
                result.Success = false;
            }

            return result;
        }

        private (JObject response, CommerceRegistInfo responseModel) getCommerceInfo(string commerceNo, string registerNo)
        {
            var response = (JObject)null;
            var responseModel = new CommerceRegistInfo();
            var preFillData = SingleFormPreFillData.Get(commerceNo, registerNo);

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

                response = Api.Get(ConfigurationManager.AppSettings["DBD_COMMERCE_CHECK_WS_URL"], new Dictionary<string, string> { { "commerceNo", commerceNo }, { "registerNo", registerNo } }, ContentType.ApplicationJson);

                if (response.HasValues)
                {
                    // เก็บข้อมูล pre fill จาก dbd ลง mongo
                    preFillData = new SingleFormPreFillData
                    {
                        IdentityID = commerceNo,
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

            return (response, responseModel);
        }

        public enum CommercialSection
        {
            [StringValue("Information_Owner")]
            Information_Owner = 1,

            [StringValue("Information_Store")]
            Information_Store = 2,

            [StringValue("Commercial_Type")]
            Commercial_Type = 3,

            [StringValue("Commercial_Investment")]
            Commercial_Investment = 4,

            [StringValue("Commercial_HeadOffice")]
            Commercial_HeadOffice = 5,

            [StringValue("Commercial_Manager")]
            Commercial_Manager = 6,

            [StringValue("Commercial_Start_In_Thailand")]
            Commercial_Start_In_Thailand = 7,


            [StringValue("Commercial_Request")]
            Commercial_Request = 8,

            [StringValue("Commercial_Transfer")]
            Commercial_Transfer = 9,


            [StringValue("Commercial_Branch")]
            Commercial_Branch = 10,
            [StringValue("Commercial_Warehouse")]
            Commercial_Warehouse = 10,
            [StringValue("Commercial_Agent")]
            Commercial_Agent = 10,


            [StringValue("Commercial_Partner")]
            Commercial_Partner = 11,






            [StringValue("Commercial_Shareholder")]//[12] จำนวนหุ้น
            Commercial_Shareholder = 12,

            //[13] หุ้นส่วนออกหรือตาย




            [StringValue("Commercial_Other")]
            Commercial_Other = 14,

            [StringValue("Commercial_Electronic")]
            Commercial_Electronic = 15

        };

    }
}
