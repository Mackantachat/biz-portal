using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Apps;
using BizPortal.ViewModels.Apps.DPTStandard;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using EGA.WS;
using Mapster;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;

namespace BizPortal.AppsHook
{
    public class DPTBuildingR6AppHook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData) => null;

        public override bool IsEnabledChat() => false;

        public override bool InvokeSingleForm(Guid trid, string currentSectionGroup, ref SingleFormRequestViewModel model)
        {
            var result = false;
            var draftData = SingleFormRequestEntity.Get(model.IdentityID);
            if (currentSectionGroup == "APP_BUILDING_R6_SEARCH")
            {
                // Clear section data
                var singleformReq = new SingleFormRequestEntity();
                singleformReq.DeleteSections(model.IdentityID, null, new string[] { "APP_BUILDING_R6_INFORMATION", "" });
                singleformReq.DeleteSections(model.IdentityID, null, new string[] { "APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION", "" });
                singleformReq.DeleteSections(model.IdentityID, null, new string[] { "APP_BUILDING_R6_TITLE_DEED", "" });
                singleformReq.DeleteSections(model.IdentityID, null, new string[] { "APP_BUILDING_R6_CONSTRUCT_COMPLETEDATE", "" });
                singleformReq.DeleteSections(model.IdentityID, null, new string[] { "APP_BUILDING_R6_BUILDING_OWNER", "" });
                singleformReq.DeleteSections(model.IdentityID, null, new string[] { "APP_BUILDING_R6_BUILDING_POSSESSORS", "" });
                singleformReq.DeleteSections(model.IdentityID, null, new string[] { "APP_BUILDING_R6_BUILDING_INFORMATION", "" });
                singleformReq.DeleteSections(model.IdentityID, null, new string[] { "APP_BUILDING_G1_SUPERVISE_ENGINEER", "" });
                singleformReq.DeleteSections(model.IdentityID, null, new string[] { "APP_BUILDING_G1_SUPERVISE_ARCHITECT", "" });
                result = true;
            }
            else if (currentSectionGroup == "APP_BUILDING_R6")
            {

                if (!draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_R6_INFORMATION") ||
                    !draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION") ||
                    !draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_R6_TITLE_DEED") ||
                    !draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_R6_BUILDING_OWNER") ||
                    !draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_R6_BUILDING_INFORMATION") ||
                    !draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_G1_SUPERVISE_ENGINEER") ||
                    !draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_G1_SUPERVISE_ARCHITECT")
                    )
                {
                    var cinfo = getA1LicenseInfo(model);

                    //ข้อมูลใบอนุญาต
                    if (!draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_R6_INFORMATION"))
                    {
                        string purpose_text = string.Empty;
                        switch (cinfo.responseModel.PurposeType)
                        {
                            case 1:
                                purpose_text = BuildingTypeRequestOptionTextConst.BUILDING;
                                break;
                            case 2:
                                purpose_text = BuildingTypeRequestOptionTextConst.MODIFY;
                                break;
                            case 4:
                                purpose_text = BuildingTypeRequestOptionTextConst.DISMANTLE;
                                break;
                        }

                        var a1Info = GetSectionData(model, "APP_BUILDING_R6_INFORMATION", SectionType.Form);
                        a1Info.FormData = new Dictionary<string, object>
                        {
                            { "APP_BUILDING_R6_A1TYPE", purpose_text },
                            { "APP_BUILDING_R6_CONSTRUCTION_SITE_FOR", cinfo.responseModel.Name},
                            { "APP_BUILDING_R6_A1LICENSENO", cinfo.responseModel.A1LicenseNo },
                            { "APP_BUILDING_R6_A1LICENSERELEASEDDATE", cinfo.responseModel.A1LicenseReleasedDate.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("th-TH")) },
                            { "APP_BUILDING_R6_A1LICENSESTARTDDATE", cinfo.responseModel.A1LicenseReleasedDate.AddDays(1).ToString("dd MMM yyyy", new System.Globalization.CultureInfo("th-TH")) },
                            { "APP_BUILDING_R6_A1REFERENCECODE",  model.SectionData.Find(o => o.SectionName == "APP_BUILDING_R6_SEARCH_SECTION").FormData.TryGetString("APP_BUILDING_R6_SEARCH_SECTION_REF_ID")},

                        };
                    }
                    //สถานที่ก่อสร้าง
                    if (!draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION"))
                    {
                        var siteInfo = GetSectionData(model, "APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION", SectionType.Form);
                        siteInfo.FormData = new Dictionary<string, object>
                        {
                            { "ADDRESS_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS", cinfo.responseModel.Address.AddressNo },
                            { "ADDRESS_MOO_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS", cinfo.responseModel.Address.VillageNo },
                            { "ADDRESS_SOI_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS", cinfo.responseModel.Address.Soi },
                            { "ADDRESS_ROAD_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS", cinfo.responseModel.Address.Road },
                            { "ADDRESS_PROVINCE_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS_TEXT", cinfo.responseModel.Address.Province },
                            { "ADDRESS_PROVINCE_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS", cinfo.responseModel.Address.GeoCode.Substring(0,2) },
                            { "ADDRESS_AMPHUR_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS_TEXT", cinfo.responseModel.Address.District },
                            { "ADDRESS_AMPHUR_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS", cinfo.responseModel.Address.GeoCode.Substring(2,2) },
                            { "ADDRESS_TUMBOL_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS_TEXT", cinfo.responseModel.Address.SubDistrict },
                            { "ADDRESS_TUMBOL_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS", cinfo.responseModel.Address.GeoCode.Substring(4,2) },
                            { "ADDRESS_POSTCODE_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS", cinfo.responseModel.Address.PostCode },

                            { "ADDRESS_LAT_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS", cinfo.responseModel.Address.Latitude },
                            { "ADDRESS_LNG_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS", cinfo.responseModel.Address.Longitude },
                            { "APP_BUILDING_R6_CONSTRUCTION_SITE_RESPONSIBLE_AREA", cinfo.responseModel.IssueByOrgCode },
                            { "APP_BUILDING_R6_CONSTRUCTION_SITE_RESPONSIBLE_AREA_TEXT", cinfo.responseModel.IssueByOrgName },

                        };

                    }

                    //ข้อมูลที่ดิน
                    if (!draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_R6_TITLE_DEED"))
                    {
                        var deedInfo = GetSectionData(model, "APP_BUILDING_R6_TITLE_DEED", SectionType.ArrayOfForms);
                        List<Dictionary<string, object>> deedList = new List<Dictionary<string, object>>();
                        if (cinfo.responseModel.TitleDeeds != null)
                        {
                            int i = 1;
                            foreach (var deed in cinfo.responseModel.TitleDeeds)
                            {
                                int deed_type = deed.DeedType;
                                string deed_type_text = string.Empty;
                                switch (deed_type)
                                {
                                    case 1:
                                        deed_type_text = "โฉนดที่ดิน";
                                        break;
                                    case 2:
                                        deed_type_text = "น.ส.3";
                                        break;
                                    case 3:
                                        deed_type_text = "ส.ค.1";
                                        break;
                                    case 4:
                                        deed_type_text = "อื่นๆ";
                                        break;
                                }

                                var Dic_Deed = new Dictionary<string, object>
                                {
                                    { "ARR_IDX", i++ },
                                    { "APP_BUILDING_R6_TITLE_DEED_ID_TYPE", deed.DeedType },
                                    { "APP_BUILDING_R6_TITLE_DEED_ID_TYPE_TEXT", deed_type_text },
                                    { "APP_BUILDING_R6_TITLE_DEED_ID", deed.DeedNo },
                                    { "APP_BUILDING_R6_TITLE_DEED_OWNER_NAME", deed.OwnerName },
                                };
                                deedList.Add(Dic_Deed);
                            }
                            deedInfo.ArrayData = deedList;
                        }
                    }
                    //ข้อมูลแต่ละเจ้าของ
                    if (!draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_R6_BUILDING_OWNER"))
                    {
                        var ownerInfo = GetSectionData(model, "APP_BUILDING_R6_BUILDING_OWNER", SectionType.ArrayOfForms);
                        List<Dictionary<string, object>> ownerList = new List<Dictionary<string, object>>();
                        if (cinfo.responseModel.Owners != null)
                        {
                            int i = 1;
                            foreach (var owner in cinfo.responseModel.Owners)
                            {
                                if (owner is DPTPersonApplicant)
                                {
                                    var tmp = (DPTPersonApplicant)owner;
                                    var title_value = string.Empty;
                                    var province_code = string.Empty;
                                    var amphur_code = string.Empty;
                                    var tambon_code = string.Empty;
                                    if (!String.IsNullOrEmpty(tmp.Address.GeoCode) && tmp.Address.GeoCode.Length == 6)
                                    {
                                        province_code = tmp.Address.GeoCode.Substring(0, 2);
                                        amphur_code = tmp.Address.GeoCode.Substring(2, 2);
                                        tambon_code = tmp.Address.GeoCode.Substring(4, 2);
                                    }
                                    //var citizen_text = string.Empty;
                                    //double num = 0;
                                    //if (double.TryParse(tmp.CitizenId, out num) && tmp.CitizenId.Length == 13)
                                    //{
                                    //    citizen_text = string.Format("{0:0-0000-00000-00-0}", num);
                                    //}
                                    var phone = string.Empty;
                                    var fax = string.Empty;
                                    if (tmp.Contacts != null && tmp.Contacts.Count() > 0)
                                    {
                                        var p = tmp.Contacts.Where(x => x.ContactType == 1).FirstOrDefault();
                                        phone = (p != null) ? p.Detail : "-";

                                        var f = tmp.Contacts.Where(x => x.ContactType == 4).FirstOrDefault();
                                        fax = (f != null) ? f.Detail : "";
                                    }

                                    switch (tmp.Title)
                                    {
                                        case "นาย":
                                            title_value = "01";
                                            break;
                                        case "นาง":
                                            title_value = "02";
                                            break;
                                        case "นางสาว":
                                            title_value = "03";
                                            break;
                                    }

                                    var Dic_Owner = new Dictionary<string, object>
                                    {

                                        { "ARR_IDX", i++ },
                                        { "ARR_ITEM_ID", i },
                                        { "APP_BUILDING_R6_BUILDING_OWNER_KIND_OF_PERSON_OPTION", "1" },
                                        { "DROPDOWN_APP_BUILDING_R6_BUILDING_OWNER_TITLE", title_value },
                                        { "DROPDOWN_APP_BUILDING_R6_BUILDING_OWNER_TITLE_TEXT", tmp.Title },
                                        { "APP_BUILDING_R6_BUILDING_OWNER_FIRSTNAME", tmp.FirstName },
                                        { "APP_BUILDING_R6_BUILDING_OWNER_LASTNAME", tmp.LastName },
                                        { "APP_BUILDING_R6_BUILDING_OWNER_CITIZENID", tmp.CitizenId },

                                        { "ADDRESS_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS", tmp.Address.AddressNo },
                                        { "ADDRESS_MOO_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS", tmp.Address.VillageNo },
                                        { "ADDRESS_SOI_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS", tmp.Address.Soi },
                                        { "ADDRESS_ROAD_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS", tmp.Address.Road },
                                        { "ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_TEXT", tmp.Address.SubDistrict },
                                        { "ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS", tambon_code },
                                        { "ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_TEXT", tmp.Address.District },
                                        { "ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS", amphur_code },
                                        { "ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_TEXT", tmp.Address.Province },
                                        { "ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS", province_code },
                                        { "ADDRESS_POSTCODE_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS", tmp.Address.PostCode },

                                        { "ADDRESS_TELEPHONE_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS", phone },
                                        { "ADDRESS_FAX_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS", fax },
                                    };
                                    ownerList.Add(Dic_Owner);
                                }
                                else
                                {
                                    var tmp = (DPTJuristicPersonApplicant)owner;
                                    var province_code = string.Empty;
                                    var amphur_code = string.Empty;
                                    var tambon_code = string.Empty;
                                    if (!String.IsNullOrEmpty(tmp.Address.GeoCode) && tmp.Address.GeoCode.Length == 6)
                                    {
                                        province_code = tmp.Address.GeoCode.Substring(0, 2);
                                        amphur_code = tmp.Address.GeoCode.Substring(2, 2);
                                        tambon_code = tmp.Address.GeoCode.Substring(4, 2);
                                    }
                                    var phone = string.Empty;
                                    var fax = string.Empty;
                                    if (tmp.Contacts != null && tmp.Contacts.Count() > 0)
                                    {
                                        var p = tmp.Contacts.Where(x => x.ContactType == 1).FirstOrDefault();
                                        phone = (p != null) ? p.Detail : "-";

                                        var f = tmp.Contacts.Where(x => x.ContactType == 4).FirstOrDefault();
                                        fax = (f != null) ? f.Detail : "";
                                    }
                                    var Dic_Owner = new Dictionary<string, object>
                                    {
                                        { "ARR_IDX", i++ },
                                        { "ARR_ITEM_ID", i },
                                        { "APP_BUILDING_R6_BUILDING_OWNER_KIND_OF_PERSON_OPTION", "2" },
                                        { "APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_NAME", tmp.Name },
                                        { "APP_BUILDING_R6_BUILDING_OWNER_JURISTICID", tmp.JuristicPersonNo },

                                        { "ADDRESS_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS", tmp.Address.AddressNo },
                                        { "ADDRESS_MOO_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS", tmp.Address.VillageNo },
                                        { "ADDRESS_SOI_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS", tmp.Address.Soi },
                                        { "ADDRESS_ROAD_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS", tmp.Address.Road },
                                        { "ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_TEXT", tmp.Address.SubDistrict },
                                        { "ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS", tambon_code },
                                        { "ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_TEXT", tmp.Address.District },
                                        { "ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS", amphur_code },
                                        { "ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_TEXT", tmp.Address.Province },
                                        { "ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS", province_code },
                                        { "ADDRESS_POSTCODE_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS", tmp.Address.PostCode },

                                        { "ADDRESS_TELEPHONE_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS", phone },
                                        { "ADDRESS_FAX_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS", fax },
                                    };
                                    ownerList.Add(Dic_Owner);
                                }
                            }
                            ownerInfo.ArrayData = ownerList;
                        }
                    }

                    //ข้อมูลแต่ละอาคาร
                    if (!draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_R6_BUILDING_INFORMATION"))
                    {
                        var buildingInfo = GetSectionData(model, "APP_BUILDING_R6_BUILDING_INFORMATION", SectionType.ArrayOfForms);
                        List<Dictionary<string, object>> buildingList = new List<Dictionary<string, object>>();
                        if (cinfo.responseModel.Buildings != null)
                        {
                            int i = 1;
                            foreach (var building in cinfo.responseModel.Buildings)
                            {
                                var Dic_Building = new Dictionary<string, object>
                                {
                                    { "ARR_IDX", i++ },
                                    { "APP_BUILDING_R6_BUILDING_TYPE", building.Type },
                                    { "APP_BUILDING_R6_BUILDING_AMOUNT", building.Amount },
                                    { "APP_BUILDING_R6_BUILDING_AMOUNT_TYPE", building.UnitType },
                                    { "APP_BUILDING_R6_BUILDING_AREA", building.Area },
                                    { "APP_BUILDING_R6_BUILDING_PARKING", building.ParkingLot },
                                    { "APP_BUILDING_R6_BUILDING_PARKINGAREA", building.ParkingArea },
                                    { "APP_BUILDING_R6_BUILDING_PURPOSE", JsonConvert.SerializeObject(building.Purpose) }
                                };

                                //add วัตถุประสงค์อาคาร
                                foreach (var purpose in building.Purpose)
                                {
                                    var dic_purpose = SetBuildingPurpose(purpose.Trim()).FirstOrDefault();
                                    if (!dic_purpose.IsDefault())
                                        Dic_Building.Add(dic_purpose.Key, dic_purpose.Value);
                                }

                                buildingList.Add(Dic_Building);
                            }
                            buildingInfo.ArrayData = buildingList;
                        }
                    }

                    //ผู้ควบคุมงาน - สถาปนิก
                    if (!draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_G1_SUPERVISE_ARCHITECT"))
                    {
                        var architectInfo = GetSectionData(model, "APP_BUILDING_G1_SUPERVISE_ARCHITECT", SectionType.ArrayOfForms);
                        List<Dictionary<string, object>> architectList = new List<Dictionary<string, object>>();
                        if (cinfo.responseModel.Buildings != null)
                        {
                            int i = 1;
                            foreach (var architect in cinfo.responseModel.SiteArchitectDocuments)
                            {
                                string title_value = getTitleValue(architect.Title);

                                var Dic_Architect = new Dictionary<string, object>
                                {
                                    { "ARR_IDX", i++ },
                                    { "ARR_ITEM_ID", i },
                                    { "DROPDOWN_APP_BUILDING_G1_SUPERVISE_ARCHITECT_TITLE", title_value },
                                    { "DROPDOWN_APP_BUILDING_G1_SUPERVISE_ARCHITECT_TITLE_TEXT", architect.Title },
                                    { "APP_BUILDING_G1_SUPERVISE_ARCHITECT_CITIZENID", architect.CitizenId },
                                    { "APP_BUILDING_G1_SUPERVISE_ARCHITECT_FIRSTNAME", architect.FirstName },
                                    { "APP_BUILDING_G1_SUPERVISE_ARCHITECT_LASTNAME", architect.LastName },
                                    { "APP_BUILDING_G1_SUPERVISE_ARCHITECT_MEMBER_NO", architect.MemberNo },
                                    { "APP_BUILDING_G1_SUPERVISE_ARCHITECT_LICENSE_ID", architect.LicenseNo },
                                    { "APP_BUILDING_G1_SUPERVISE_ARCHITECT_TITLE", title_value },
                                    { "APP_BUILDING_G1_SUPERVISE_ARCHITECT_TITLE_TEXT", architect.Title },
                                };
                                architectList.Add(Dic_Architect);
                            }
                            architectInfo.ArrayData = architectList;
                        }
                    }
                    //ผู้ควบคุมงาน - วิศวกร
                    if (!draftData.SectionData.Any(e => e.SectionName == "APP_BUILDING_G1_SUPERVISE_ENGINEER"))
                    {
                        var engineerInfo = GetSectionData(model, "APP_BUILDING_G1_SUPERVISE_ENGINEER", SectionType.ArrayOfForms);
                        List<Dictionary<string, object>> engineerList = new List<Dictionary<string, object>>();
                        if (cinfo.responseModel.Buildings != null)
                        {
                            int i = 1;
                            foreach (var engineer in cinfo.responseModel.SiteEngineerDocuments)
                            {
                                string title_value = getTitleValue(engineer.Title);
                                //EAType = 1,
                                //Title = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("DROPDOWN_APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_TITLE_TEXT_" + i),
                                //FirstName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_FIRSTNAME_" + i),
                                //LastName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_LASTNAME_" + i),
                                //LicenseNo = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_LICENSE_ID_" + i),
                                //MemberNo = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_MEMBER_NO_" + i),

                                if (engineer.EAType == 1)//วิศวกร
                                {
                                    var Dic_Engineer = new Dictionary<string, object>
                                    {
                                        { "ARR_IDX", i++ },
                                        { "ARR_ITEM_ID", i },
                                        { "DROPDOWN_APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_TITLE", title_value },
                                        { "DROPDOWN_APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_TITLE_TEXT", engineer.Title },
                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZENID", engineer.CitizenId },
                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_FIRSTNAME", engineer.FirstName },
                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_LASTNAME", engineer.LastName },
                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_MEMBER_NO", engineer.MemberNo },
                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_LICENSE_ID", engineer.LicenseNo },
                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_TITLE", title_value},
                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_TITLE_TEXT", engineer.Title },

                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_KIND_OF_PERSON_OPTION", 1 },
                                    };
                                    engineerList.Add(Dic_Engineer);
                                }
                                else
                                {
                                    var Dic_Engineer = new Dictionary<string, object>
                                    {
                                        { "ARR_IDX", i++ },
                                        { "ARR_ITEM_ID", i },
                                        { "DROPDOWN_APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZEN_TITLE", title_value },
                                        { "DROPDOWN_APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZEN_TITLE_TEXT", engineer.Title },
                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZEN_FIRSTNAME", engineer.FirstName },
                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZENID", engineer.CitizenId },
                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZEN_LASTNAME", engineer.LastName },
                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZEN_TITLE", title_value},
                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZEN_TITLE_TEXT", engineer.Title },

                                        { "APP_BUILDING_G1_SUPERVISE_ENGINEER_KIND_OF_PERSON_OPTION", 2 },
                                    };
                                    engineerList.Add(Dic_Engineer);
                                }
                                
                            }
                            engineerInfo.ArrayData = engineerList;
                        }
                    }

                    result = true;
                }
                else
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

        private string getTitleValue(string title)
        {
            string code = string.Empty;
            switch (title)
            {
                case "นาย":
                    code = "01";
                    break;
                case "นาง":
                    code = "02";
                    break;
                case "นางสาว":
                    code = "03";
                    break;
            }
            return code;
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;
            if (model.IdentityType == UserTypeEnum.Juristic)
            {
                result.SendToEmail = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("APPLICANT_EMAIL");
            }
            else if (model.IdentityType == UserTypeEnum.Citizen)
            {
                result.SendToEmail = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_EMAIL");
            }

            #region [Juristic Titles]
            Dictionary<int, string> jurTitles = new Dictionary<int, string>();
            jurTitles.Add(1, "ห้างหุ้นส่วนจำกัด");
            jurTitles.Add(2, "บริษัทจำกัด");
            jurTitles.Add(3, "ห้างหุ้นส่วนสามัญ");
            jurTitles.Add(4, "สมาคม");
            jurTitles.Add(5, "มูลนิธิ");
            #endregion
            try
            {
                string regisUrl = ConfigurationManager.AppSettings["DPT_BUILDING_G1_WS_URL"];

                switch (stage)
                {
                    case AppsStage.UserCreate:
                        {
                            //var post = string.Empty; // Model data

                            #region [PostData]
                            string a1ReleaseDate_txt = model.Data.TryGetData("APP_BUILDING_R6_INFORMATION").ThenGetStringData("APP_BUILDING_R6_A1LICENSERELEASEDDATE");

                            var post = new DPTR6Request()
                            {
                                RequestType = "R6",
                                Name = model.Data.TryGetData("APP_BUILDING_R6_INFORMATION").ThenGetStringData("APP_BUILDING_R6_CONSTRUCTION_SITE_FOR"),
                                A1LicenseNo = model.Data.TryGetData("APP_BUILDING_R6_INFORMATION").ThenGetStringData("APP_BUILDING_R6_A1LICENSENO"),
                                A1LicenseReleasedDate = DateTime.Parse(a1ReleaseDate_txt, new System.Globalization.CultureInfo("th-TH")),
                                WroteAt = "Biz Portal",
                                BizId = model.ApplicationRequestNumber,
                                BizGuid = model.ApplicationRequestID.ToString(),
                                SubmitDate = DateTime.Now,
                                ConstructionCompletedDate = DateTime.Parse(model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCT_COMPLETEDATE").ThenGetStringData("APP_BUILDING_R6_A1CONSTRUCTIONCOMPLETEDDATE"), new System.Globalization.CultureInfo("th-TH")),
                                //EstimateDay = model.Data.TryGetData("APP_BUILDING_G1_INFORMATION").ThenGetIntData("APP_BUILDING_G1_DURATION"),
                                Address = new DPTAddress()
                                {
                                    AddressNo = model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS"),
                                    VillageNo = model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_MOO_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS"),
                                    Soi = model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_SOI_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS"),
                                    Road = model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_ROAD_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS"),
                                    SubDistrict = model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS_TEXT"),
                                    District = model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS_TEXT"),
                                    Province = model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS_TEXT"),
                                    PostCode = model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS"),
                                    GeoCode = model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS") +
                                                    model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS") +
                                                    model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS"),
                                    Latitude = model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_LAT_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS"),
                                    Longitude = model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_LNG_APP_BUILDING_R6_CONSTRUCTION_SITE_ADDRESS")
                                },
                                IssueByOrgCode = model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("APP_BUILDING_R6_CONSTRUCTION_SITE_RESPONSIBLE_AREA"),
                                IssueByOrgName = model.Data.TryGetData("APP_BUILDING_R6_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("APP_BUILDING_R6_CONSTRUCTION_SITE_RESPONSIBLE_AREA_TEXT")
                            };
                            var purpose = model.Data.TryGetData("APP_BUILDING_R6_INFORMATION").ThenGetStringData("APP_BUILDING_R6_A1TYPE");
                            switch (purpose)
                            {
                                case BuildingTypeOptionTextConst.BUILDING:
                                    post.PurposeType = 1;
                                    break;
                                case BuildingTypeOptionTextConst.MODIFY:
                                    post.PurposeType = 2;
                                    break;
                                case BuildingTypeOptionTextConst.DISMANTLE:
                                    post.PurposeType = 4;
                                    break;
                                default:
                                    post.PurposeType = 1;
                                    break;
                            }
                            if (model.IdentityType == UserTypeEnum.Citizen)
                            {
                                #region [Citizen Contact]
                                var contacts = new List<DPTContact>()
                                {
                                    new DPTContact()
                                    {
                                       ContactType = 2,
                                       Detail = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL")
                                    }
                                };

                                var contactEmail = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_EMAIL");
                                if (!string.IsNullOrEmpty(contactEmail))
                                {
                                    contacts.Add(new DPTContact()
                                    {
                                        ContactType = 3,
                                        Detail = contactEmail
                                    });
                                }
                                #endregion

                                post.Applicant = new DPTPersonApplicant()
                                {
                                    CitizenId = model.IdentityID,
                                    Title = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_TITLE_TEXT"),
                                    FirstName = model.Data.TryGetData("OPENID").ThenGetStringData("FIRSTNAME_TH"),
                                    LastName = model.Data.TryGetData("OPENID").ThenGetStringData("LASTNAME_TH"),
                                    Address = new DPTAddress()
                                    {
                                        AddressNo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_CITIZEN_ADDRESS"),
                                        VillageNo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS"),
                                        Soi = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS"),
                                        Road = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS"),
                                        SubDistrict = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT"),
                                        District = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT"),
                                        Province = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT"),
                                        PostCode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS"),
                                        GeoCode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS") +
                                            model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS") +
                                            model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS"),
                                        Latitude = null,
                                        Longitude = null
                                    },
                                    ContactAddress = new DPTAddress()
                                    {
                                        AddressNo = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_CONTACT_ADDRESS"),
                                        VillageNo = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_MOO_CONTACT_ADDRESS"),
                                        Soi = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_SOI_CONTACT_ADDRESS"),
                                        Road = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_ROAD_CONTACT_ADDRESS"),
                                        SubDistrict = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CONTACT_ADDRESS_TEXT"),
                                        District = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CONTACT_ADDRESS_TEXT"),
                                        Province = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CONTACT_ADDRESS_TEXT"),
                                        PostCode = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_CONTACT_ADDRESS"),
                                        GeoCode = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CONTACT_ADDRESS") +
                                            model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CONTACT_ADDRESS") +
                                            model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CONTACT_ADDRESS"),
                                        Latitude = null,
                                        Longitude = null
                                    },
                                    Contacts = contacts.ToArray(),
                                };

                                var citiFiles = model.UploadedFiles.Where(o => o.Description == "CITIZEN_INFORMATION").FirstOrDefault().Files;
                                var citizenCardFile = citiFiles.Where(o => o.FileTypeCode == "APPLICANT_ID_CARD_COPY").FirstOrDefault();
                                var citizenCardTypeName = citizenCardFile.Extras.ContainsKey("FILETYPENAME") ? citizenCardFile.Extras["FILETYPENAME"] : string.Empty;

                                post.CitizenIDCards = new DPTFileMetaData[]
                                {
                                    new DPTFileMetaData()
                                    {
                                        FileId = citizenCardFile.FileID,
                                        DocName = citizenCardTypeName,
                                        ContentType = citizenCardFile.ContentType,
                                        FileSize = citizenCardFile.FileSize,
                                        Name = citizenCardFile.FileName,
                                        FileTypeCode = citizenCardFile.FileTypeCode
                                    }
                                };
                            }
                            else if (model.IdentityType == UserTypeEnum.Juristic)
                            {
                                #region [Juristic Contact]
                                var jurDataTel = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS");
                                var jurDataExt = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS");
                                var jurTel = string.IsNullOrEmpty(jurDataExt) ? jurDataTel : jurDataTel + " ext." + jurDataExt;

                                var jurContacts = new List<DPTContact>()
                                {
                                    new DPTContact()
                                    {
                                       ContactType = 1,
                                       Detail = jurTel
                                    }
                                };

                                var jurDataFax = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_JURISTIC_HQ_ADDRESS");
                                if (!string.IsNullOrEmpty(jurDataFax))
                                {
                                    jurContacts.Add(new DPTContact()
                                    {
                                        ContactType = 4,
                                        Detail = jurDataFax
                                    });
                                }
                                #endregion

                                #region [Applicant Contact]
                                var appContacts = new List<DPTContact>()
                                {
                                    new DPTContact()
                                    {
                                       ContactType = 2,
                                       Detail = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("APPLICANT_TEL")
                                    }
                                };

                                var appDataEmail = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("APPLICANT_EMAIL");
                                if (!string.IsNullOrEmpty(appDataEmail))
                                {
                                    appContacts.Add(new DPTContact()
                                    {
                                        ContactType = 3,
                                        Detail = appDataEmail
                                    });
                                }
                                #endregion

                                post.Applicant = new DPTJuristicPersonApplicant()
                                {
                                    JuristicPersonNo = model.IdentityID,
                                    Type = jurTitles.FirstOrDefault(x => x.Value == model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("JURISTIC_TYPE")).Key,
                                    Name = model.IdentityName,
                                    RegisterDate = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("REGISTER_DATE"),
                                    Address = new DPTAddress()
                                    {
                                        AddressNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                                        VillageNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),
                                        Soi = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"),
                                        Road = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"),
                                        SubDistrict = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT"),
                                        District = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT"),
                                        Province = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT"),
                                        PostCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"),
                                        GeoCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS") +
                                            model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS") +
                                            model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS"),
                                        Latitude = null,
                                        Longitude = null
                                    },
                                    Contacts = jurContacts.ToArray(),
                                    DelegatedPerson = new DPTPerson()
                                    {
                                        Title = null,
                                        FirstName = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("APPLICANT_FIRSTNAME"),
                                        LastName = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("APPLICANT_LASTNAME"),
                                        CitizenId = null,
                                        Address = new DPTAddress()
                                        {
                                            AddressNo = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_APPLICANT_ADDRESS"),
                                            VillageNo = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_MOO_APPLICANT_ADDRESS"),
                                            Soi = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_SOI_APPLICANT_ADDRESS"),
                                            Road = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_ROAD_APPLICANT_ADDRESS"),
                                            SubDistrict = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_APPLICANT_ADDRESS_TEXT"),
                                            District = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_APPLICANT_ADDRESS_TEXT"),
                                            Province = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_APPLICANT_ADDRESS_TEXT"),
                                            PostCode = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_APPLICANT_ADDRESS"),
                                            GeoCode = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_APPLICANT_ADDRESS") +
                                                model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_APPLICANT_ADDRESS") +
                                                model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_APPLICANT_ADDRESS"),
                                            Latitude = null,
                                            Longitude = null
                                        },
                                        ContactAddress = null,
                                        Contacts = appContacts.ToArray()
                                    }
                                };

                                var jurFiles = model.UploadedFiles.Where(o => o.Description == "JURISTIC_INFORMATION").FirstOrDefault().Files;

                                var jurCertificateFile = jurFiles.Where(o => o.FileTypeCode == "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY").FirstOrDefault();
                                var jurCertificateTypeName = jurCertificateFile.Extras.ContainsKey("FILETYPENAME") ? jurCertificateFile.Extras["FILETYPENAME"] : string.Empty;
                                post.JuristicPersonRegisterationDocuments = new DPTFileMetaData[]
                                {
                                    new DPTFileMetaData()
                                    {
                                        FileId = jurCertificateFile.FileID,
                                        DocName = jurCertificateTypeName,
                                        ContentType = jurCertificateFile.ContentType,
                                        FileSize = jurCertificateFile.FileSize,
                                        Name = jurCertificateFile.FileName,
                                        FileTypeCode = jurCertificateFile.FileTypeCode
                                    }
                                };

                                var jurDelegatorFile = jurFiles.Where(o => o.FileTypeCode == "JURISTIC_DELEGATOR_DOC").FirstOrDefault();
                                var jurDelegatorTypeName = jurDelegatorFile.Extras.ContainsKey("FILETYPENAME") ? jurDelegatorFile.Extras["FILETYPENAME"] : string.Empty;
                                post.DelegationRepresentationDocuments = new DPTFileMetaData[]
                                {
                                    new DPTFileMetaData()
                                    {
                                        FileId = jurDelegatorFile.FileID,
                                        DocName = jurDelegatorTypeName,
                                        ContentType = jurDelegatorFile.ContentType,
                                        FileSize = jurDelegatorFile.FileSize,
                                        Name = jurDelegatorFile.FileName,
                                        FileTypeCode = jurDelegatorFile.FileTypeCode
                                    }
                                };
                            }

                            var consFiles = model.UploadedFiles.Where(o => o.Description == "CONSTRUCTION_SITE_INFORMATION").FirstOrDefault().Files;

                            #region [REQUESTOR_INFORMATION]
                            var request_type = model.Data.TryGetData("APP_BUILDING_R6_REQUESTOR_INFORMATION_HEADER").ThenGetStringData("APP_BUILDING_R6_REQUESTOR_INFORMATION_REQUEST_TYPE_OPTION");
                            
                            #endregion

                            #region [Building_Owners]
                            var ownerTotal = int.Parse(model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_R6_BUILDING_OWNER_TOTAL"));
                            var ownerList = new List<IApplicant>();
                            if (ownerTotal > 0 && request_type == "BUILDING_OWNER")
                            {
                                //var ownerList = new List<IApplicant>();
                                for (int i = 0; i < ownerTotal; i++)
                                {
                                    var person_type = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_R6_BUILDING_OWNER_KIND_OF_PERSON_OPTION_" + i);

                                    if (person_type == "1")
                                    {
                                        string tmp_citizenid = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_R6_BUILDING_OWNER_CITIZENID_" + i);

                                        var owner = new DPTPersonApplicant();
                                        var contacts = new List<DPTContact>();
                                        var phone = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_TELEPHONE_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i);
                                        var fax = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_FAX_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i);
                                        
                                        owner.Title = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("DROPDOWN_APP_BUILDING_R6_BUILDING_OWNER_TITLE_TEXT_" + i);
                                        owner.FirstName = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_R6_BUILDING_OWNER_FIRSTNAME_" + i);
                                        owner.LastName = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_R6_BUILDING_OWNER_LASTNAME_" + i);
                                        owner.CitizenId = tmp_citizenid.Replace("-", "");
                                        owner.Address = new DPTAddress {
                                            AddressNo = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i),
                                            VillageNo = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_MOO_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i), 
                                            Soi = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_SOI_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i),
                                            Road = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_ROAD_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i),
                                            SubDistrict = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_TEXT_" + i),
                                            District = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_TEXT_" + i),
                                            Province = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_TEXT_" + i),
                                            PostCode = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_POSTCODE_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i),
                                            GeoCode = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i) +
                                            model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i) +
                                            model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i),
                                        };
                                        if (!String.IsNullOrEmpty(phone))
                                        {
                                            contacts.Add(new DPTContact()
                                            {
                                                ContactType = 1,
                                                Detail = phone
                                            });
                                        }
                                        if (!String.IsNullOrEmpty(fax))
                                        {
                                            contacts.Add(new DPTContact
                                            {
                                                ContactType = 4,
                                                Detail = fax
                                            });
                                        }
                                        owner.Contacts = contacts.ToArray();

                                        ownerList.Add(owner);
                                    }
                                    else
                                    {
                                        var owner = new DPTJuristicPersonApplicant();
                                        var contacts = new List<DPTContact>();
                                        var phone = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_TELEPHONE_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i);
                                        var fax = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_FAX_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i);

                                        owner.Name = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_NAME_" + i);
                                        owner.JuristicPersonNo = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_R6_BUILDING_OWNER_JURISTICID_" + i);
                                        owner.RegisterDate = DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"));
                                        owner.Address = new DPTAddress()
                                        {
                                            AddressNo = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i),
                                            VillageNo = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_MOO_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i),
                                            Soi = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_SOI_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i),
                                            Road = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_ROAD_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i),
                                            SubDistrict = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_TEXT_" + i),
                                            District = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_TEXT_" + i),
                                            Province = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_TEXT_" + i),
                                            PostCode = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_POSTCODE_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i),
                                            GeoCode = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i) +
                                            model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i) +
                                            model.Data.TryGetData("APP_BUILDING_R6_BUILDING_OWNER").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i),
                                            
                                        };
                                        if (!String.IsNullOrEmpty(phone))
                                        {
                                            contacts.Add(new DPTContact()
                                            {
                                                ContactType = 1,
                                                Detail = phone
                                            });
                                        }
                                        if (!String.IsNullOrEmpty(fax))
                                        {
                                            contacts.Add(new DPTContact
                                            {
                                                ContactType = 4,
                                                Detail = fax
                                            });
                                        }
                                        owner.Contacts = contacts.ToArray();

                                        ownerList.Add(owner);
                                    }
                                }
                                //post.Owners = ownerList.ToArray();
                            }
                            post.Owners = ownerList.ToArray();

                            #endregion

                            #region [Building_Posessors]
                            var possessorTotal = int.Parse(model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("APP_BUILDING_R6_BUILDING_POSSESSORS_TOTAL"));
                            var possessorList = new List<IApplicant>();
                            if (possessorTotal > 0 && request_type == "BUILDING_POSSESSOR")
                            {
                                //var possessorList = new List<IApplicant>();
                                for (int i = 0; i < possessorTotal; i++)
                                {
                                    var person_type = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("APP_BUILDING_R6_BUILDING_POSSESSORS_KIND_OF_PERSON_OPTION_" + i);
                                    if (person_type == "1")
                                    {
                                        string tmp_citizenid = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZENID_" + i);
                                        var phone = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_TELEPHONE_APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZEN_ADDRESS_" + i);
                                        var fax = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_FAX_APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZEN_ADDRESS_" + i);
                                        
                                        var possessor = new DPTPersonApplicant();
                                        possessor.Title = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("DROPDOWN_APP_BUILDING_R6_BUILDING_POSSESSORS_TITLE_TEXT_" + i);
                                        possessor.FirstName = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("APP_BUILDING_R6_BUILDING_POSSESSORS_FIRSTNAME_" + i);
                                        possessor.LastName = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("APP_BUILDING_R6_BUILDING_POSSESSORS_LASTNAME_" + i);
                                        possessor.CitizenId = tmp_citizenid.Replace("-", "");
                                        possessor.Address = new DPTAddress
                                        {
                                            AddressNo = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZEN_ADDRESS_" + i),
                                            VillageNo = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_MOO_APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZEN_ADDRESS_" + i),
                                            Soi = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_SOI_APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZEN_ADDRESS_" + i),
                                            Road = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_ROAD_APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZEN_ADDRESS_" + i),
                                            SubDistrict = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZEN_ADDRESS_TEXT_" + i),
                                            District = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZEN_ADDRESS_TEXT_" + i),
                                            Province = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZEN_ADDRESS_TEXT_" + i),
                                            PostCode = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_POSTCODE_APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZEN_ADDRESS_" + i),
                                            GeoCode = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZEN_ADDRESS_" + i) +
                                            model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZEN_ADDRESS_" + i) +
                                            model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_POSSESSORS_CITIZEN_ADDRESS_" + i),
                                        };
                                        var contacts = new List<DPTContact>();
                                        if (!String.IsNullOrEmpty(phone))
                                        {
                                            contacts.Add(new DPTContact()
                                            {
                                                ContactType = 1,
                                                Detail = phone
                                            });
                                        }
                                        if (!String.IsNullOrEmpty(fax))
                                        {
                                            contacts.Add(new DPTContact
                                            {
                                                ContactType = 4,
                                                Detail = fax
                                            });
                                        }
                                        possessor.Contacts = contacts.ToArray();

                                        possessorList.Add(possessor);
                                    }
                                    else
                                    {
                                        var phone = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_TELEPHONE_APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_ADDRESS_" + i);
                                        var fax = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_FAX_APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_ADDRESS_" + i);
                                        
                                        var possessor = new DPTJuristicPersonApplicant();
                                        possessor.Name = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_NAME_" + i);
                                        possessor.JuristicPersonNo = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTICID_" + i);
                                        possessor.RegisterDate = DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"));
                                        possessor.Address = new DPTAddress()
                                        {
                                            AddressNo = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_ADDRESS_" + i),
                                            VillageNo = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_MOO_APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_ADDRESS_" + i),
                                            Soi = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_SOI_APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_ADDRESS_" + i),
                                            Road = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_ROAD_APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_ADDRESS_" + i),
                                            SubDistrict = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_ADDRESS_TEXT_" + i),
                                            District = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_ADDRESS_TEXT_" + i),
                                            Province = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_ADDRESS_TEXT_" + i),
                                            PostCode = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_POSTCODE_APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_ADDRESS_" + i),
                                            GeoCode = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_ADDRESS_" + i) +
                                            model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_ADDRESS_" + i) +
                                            model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_POSSESSORS_JURISTIC_ADDRESS_" + i),

                                        };
                                        var contacts = new List<DPTContact>();
                                        if (!String.IsNullOrEmpty(phone))
                                        {
                                            contacts.Add(new DPTContact()
                                            {
                                                ContactType = 1,
                                                Detail = phone
                                            });
                                        }
                                        if (!String.IsNullOrEmpty(fax))
                                        {
                                            contacts.Add(new DPTContact
                                            {
                                                ContactType = 4,
                                                Detail = fax
                                            });
                                        }
                                        possessor.Contacts = contacts.ToArray();

                                        possessorList.Add(possessor);
                                    }
                                    //var possessor_fullname = string.Format("{0}{1} {2}",

                                    //    model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("DROPDOWN_APP_BUILDING_R6_BUILDING_POSSESSORS_TITLE_TEXT_" + i),
                                    //    model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("APP_BUILDING_R6_BUILDING_POSSESSORS_FIRSTNAME_" + i),
                                    //    model.Data.TryGetData("APP_BUILDING_R6_BUILDING_POSSESSORS").ThenGetStringData("APP_BUILDING_R6_BUILDING_POSSESSORS_LASTNAME_" + i)

                                    //    );
                                    //possessorList.Add(possessor_fullname);
                                }
                                //post.Possessors = possessorList.ToArray();
                            }
                            post.Possessors = possessorList.ToArray();
                            #endregion

                            #region [Buildings]
                            var buildingTotal = int.Parse(model.Data.TryGetData("APP_BUILDING_R6_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_R6_BUILDING_INFORMATION_TOTAL"));

                            if (buildingTotal > 0)
                            {
                                var buildingList = new List<DPTBuilding>();

                                for (int i = 0; i < buildingTotal; i++)
                                {
                                    var isCertified = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_R6_BUILDING_CONFIRMOPEN_" + i);

                                    if (isCertified == "ALL" || isCertified == "PARTIAL")
                                    {
                                        string remark = string.Empty;
                                        if (isCertified == "PARTIAL")
                                            remark = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_R6_BUILDING_CONFIRMOPEN_PARTIAL_TEXT_" + i);

                                        var building = new DPTBuilding()
                                        {
                                            Type = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_R6_BUILDING_TYPE_" + i),
                                            Amount = int.Parse(model.Data.TryGetData("APP_BUILDING_R6_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_R6_BUILDING_AMOUNT_" + i)),
                                            UnitType = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_R6_BUILDING_AMOUNT_TYPE_" + i),
                                            Purpose = JsonConvert.DeserializeObject<string[]>(model.Data.TryGetData("APP_BUILDING_R6_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_R6_BUILDING_PURPOSE_" + i)),//GetBuildingPurpose(model.Data.TryGetData("APP_BUILDING_G1_BUILDING_INFORMATION"), i),
                                            ParkingLot = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_R6_BUILDING_PARKING_" + i),
                                            Area = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_R6_BUILDING_AREA_" + i),
                                            ParkingArea = model.Data.TryGetData("APP_BUILDING_R6_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_R6_BUILDING_PARKINGAREA_" + i),
                                            Remark = remark,
                                        };

                                        buildingList.Add(building);
                                    }
                                }
                                post.Buildings = buildingList.ToArray();
                            }
                            #endregion

                            #region [Title Deed]
                            var DeedTotal = int.Parse(model.Data.TryGetData("APP_BUILDING_R6_TITLE_DEED").ThenGetStringData("APP_BUILDING_R6_TITLE_DEED_TOTAL"));
                            if (DeedTotal > 0)
                            {
                                var deedList = new List<DPTTitleDeed>();
                                var deedFiles = new List<DPTFileMetaData>();
                                for (int i = 0; i < DeedTotal; i++)
                                {
                                    var itemId = model.Data.TryGetData("APP_BUILDING_R6_TITLE_DEED").ThenGetStringData("ARR_ITEM_ID_" + i);
                                    var deed = new DPTTitleDeed()
                                    {
                                        DeedType = int.Parse(model.Data.TryGetData("APP_BUILDING_R6_TITLE_DEED").ThenGetStringData("APP_BUILDING_R6_TITLE_DEED_ID_TYPE_" + i)),
                                        DeedNo = model.Data.TryGetData("APP_BUILDING_R6_TITLE_DEED").ThenGetStringData("APP_BUILDING_R6_TITLE_DEED_ID_" + i),
                                        OwnerName = model.Data.TryGetData("APP_BUILDING_R6_TITLE_DEED").ThenGetStringData("APP_BUILDING_R6_TITLE_DEED_OWNER_NAME_" + i),
                                    };
                                    deedList.Add(deed);

                                    //var deedFile = consFiles.Where(o => o.FileTypeCode == "TITLE_DEED_COPY_" + itemId).FirstOrDefault();
                                    //var deedTypeName = deedFile.Extras.ContainsKey("FILETYPENAME") ? deedFile.Extras["FILETYPENAME"] : string.Empty;
                                    //deedFiles.Add(new DPTFileMetaData()
                                    //{
                                    //    FileId = deedFile.FileID,
                                    //    DocName = deedTypeName,
                                    //    ContentType = deedFile.ContentType,
                                    //    FileSize = deedFile.FileSize,
                                    //    Name = deedFile.FileName,
                                    //    FileTypeCode = deedFile.FileTypeCode
                                    //});

                                }
                                post.TitleDeeds = deedList.ToArray();
                                //post.TitleDeedDocuments = deedFiles.ToArray();
                            }
                            #endregion

                            #region [Sup En]
                            var supEnTotal = int.Parse(model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_TOTAL"));
                            if (supEnTotal > 0)
                            {
                                var pplList = new List<DPTEADocument>();
                                for (int i = 0; i < supEnTotal; i++)
                                {
                                    var itemId = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("ARR_ITEM_ID_" + i);

                                    var person_type = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_KIND_OF_PERSON_OPTION_" + i);

                                    if (person_type == "1")//engineer
                                    {
                                        var ppl = new DPTEADocument()
                                        {
                                            EAType = 1,
                                            CitizenId = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZENID_" + i),
                                            Title = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("DROPDOWN_APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_TITLE_TEXT_" + i),
                                            FirstName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_FIRSTNAME_" + i),
                                            LastName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_LASTNAME_" + i),
                                            LicenseNo = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_LICENSE_ID_" + i),
                                            MemberNo = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_MEMBER_NO_" + i),
                                        };

                                        var licenseFile = consFiles.Where(o => o.FileTypeCode == "SUPERVISE_ENGINEER_PROFESSIONAL_LICENSE_" + itemId).FirstOrDefault();
                                        var licenseTypeName = licenseFile.Extras.ContainsKey("FILETYPENAME") ? licenseFile.Extras["FILETYPENAME"] : string.Empty;
                                        List<DPTFileMetaData> licenseList = new List<DPTFileMetaData>()
                                        {
                                            new DPTFileMetaData()
                                            {
                                                FileId = licenseFile.FileID,
                                                DocName = licenseTypeName,
                                                ContentType = licenseFile.ContentType,
                                                FileSize = licenseFile.FileSize,
                                                Name = licenseFile.FileName,
                                                FileTypeCode = licenseFile.FileTypeCode
                                            }
                                        };
                                        ppl.LicenseForms = licenseList.ToArray();

                                        var consentFile = consFiles.Where(o => o.FileTypeCode == "SUPERVISE_ENGINEER_CONSENT_DOC_" + itemId).FirstOrDefault();
                                        var consentTypeName = consentFile.Extras.ContainsKey("FILETYPENAME") ? consentFile.Extras["FILETYPENAME"] : string.Empty;
                                        List<DPTFileMetaData> consentList = new List<DPTFileMetaData>()
                                        {
                                            new DPTFileMetaData()
                                            {
                                                FileId = consentFile.FileID,
                                                DocName = consentTypeName,
                                                ContentType = consentFile.ContentType,
                                                FileSize = consentFile.FileSize,
                                                Name = consentFile.FileName,
                                                FileTypeCode = consentFile.FileTypeCode
                                            }
                                        };
                                        ppl.ConsentForms = consentList.ToArray();

                                        pplList.Add(ppl);
                                    }
                                    else//person
                                    {
                                        var ppl = new DPTEADocument()
                                        {
                                            EAType = 3,
                                            CitizenId = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZENID_" + i),
                                            Title = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("DROPDOWN_APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZEN_TITLE_TEXT_" + i),
                                            FirstName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZEN_FIRSTNAME_" + i),
                                            LastName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZEN_LASTNAME_" + i),
                                        };
                                        pplList.Add(ppl);
                                    }
                                    /*
                                    var ppl = new DPTEADocument()
                                    {
                                        EAType = 1,
                                        Title = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("DROPDOWN_APP_BUILDING_G1_SUPERVISE_ENGINEER_TITLE_TEXT_" + i),
                                        FirstName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_FIRSTNAME_" + i),
                                        LastName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_LASTNAME_" + i),
                                        LicenseNo = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_LICENSE_ID_" + i),
                                        MemberNo = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_MEMBER_NO_" + i),
                                    };

                                    var licenseFile = consFiles.Where(o => o.FileTypeCode == "SUPERVISE_ENGINEER_PROFESSIONAL_LICENSE_" + itemId).FirstOrDefault();
                                    var licenseTypeName = licenseFile.Extras.ContainsKey("FILETYPENAME") ? licenseFile.Extras["FILETYPENAME"] : string.Empty;
                                    List<DPTFileMetaData> licenseList = new List<DPTFileMetaData>()
                                    {
                                        new DPTFileMetaData()
                                        {
                                            FileId = licenseFile.FileID,
                                            DocName = licenseTypeName,
                                            ContentType = licenseFile.ContentType,
                                            FileSize = licenseFile.FileSize,
                                            Name = licenseFile.FileName,
                                            FileTypeCode = licenseFile.FileTypeCode
                                        }
                                    };
                                    ppl.LicenseForms = licenseList.ToArray();

                                    var consentFile = consFiles.Where(o => o.FileTypeCode == "SUPERVISE_ENGINEER_CONSENT_DOC_" + itemId).FirstOrDefault();
                                    var consentTypeName = consentFile.Extras.ContainsKey("FILETYPENAME") ? consentFile.Extras["FILETYPENAME"] : string.Empty;
                                    List<DPTFileMetaData> consentList = new List<DPTFileMetaData>()
                                    {
                                        new DPTFileMetaData()
                                        {
                                            FileId = consentFile.FileID,
                                            DocName = consentTypeName,
                                            ContentType = consentFile.ContentType,
                                            FileSize = consentFile.FileSize,
                                            Name = consentFile.FileName,
                                            FileTypeCode = consentFile.FileTypeCode
                                        }
                                    };
                                    ppl.ConsentForms = consentList.ToArray();
                                    
                                    pplList.Add(ppl);
                                    */
                                }
                                post.SiteEngineerDocuments = pplList.ToArray();
                            }
                            #endregion

                            #region [Sup Arch]
                            var supArchTotal = int.Parse(model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ARCHITECT_TOTAL"));
                            if (supArchTotal > 0)
                            {
                                var pplList = new List<DPTEADocument>();
                                for (int i = 0; i < supArchTotal; i++)
                                {
                                    var itemId = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("ARR_ITEM_ID_" + i);
                                    var ppl = new DPTEADocument()
                                    {
                                        EAType = 2,
                                        CitizenId = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ARCHITECT_CITIZENID_" + i),
                                        Title = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("DROPDOWN_APP_BUILDING_G1_SUPERVISE_ARCHITECT_TITLE_TEXT_" + i),
                                        FirstName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ARCHITECT_FIRSTNAME_" + i),
                                        LastName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ARCHITECT_LASTNAME_" + i),
                                        LicenseNo = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ARCHITECT_LICENSE_ID_" + i),
                                        MemberNo = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ARCHITECT_MEMBER_NO_" + i),
                                    };

                                    var licenseFile = consFiles.Where(o => o.FileTypeCode == "SUPERVISE_ARCHITECT_PROFESSIONAL_LICENSE_" + itemId).FirstOrDefault();
                                    var licenseTypeName = licenseFile.Extras.ContainsKey("FILETYPENAME") ? licenseFile.Extras["FILETYPENAME"] : string.Empty;
                                    List<DPTFileMetaData> licenseList = new List<DPTFileMetaData>()
                                    {
                                        new DPTFileMetaData()
                                        {
                                            FileId = licenseFile.FileID,
                                            DocName = licenseTypeName,
                                            ContentType = licenseFile.ContentType,
                                            FileSize = licenseFile.FileSize,
                                            Name = licenseFile.FileName,
                                            FileTypeCode = licenseFile.FileTypeCode
                                        }
                                    };
                                    ppl.LicenseForms = licenseList.ToArray();

                                    var consentFile = consFiles.Where(o => o.FileTypeCode == "SUPERVISE_ARCHITECT_CONSENT_DOC_" + itemId).FirstOrDefault();
                                    var consentTypeName = consentFile.Extras.ContainsKey("FILETYPENAME") ? consentFile.Extras["FILETYPENAME"] : string.Empty;
                                    List<DPTFileMetaData> consentList = new List<DPTFileMetaData>()
                                    {
                                        new DPTFileMetaData()
                                        {
                                            FileId = consentFile.FileID,
                                            DocName = consentTypeName,
                                            ContentType = consentFile.ContentType,
                                            FileSize = consentFile.FileSize,
                                            Name = consentFile.FileName,
                                            FileTypeCode = consentFile.FileTypeCode
                                        }
                                    };
                                    ppl.ConsentForms = consentList.ToArray();

                                    pplList.Add(ppl);
                                }
                                post.SiteArchitectDocuments = pplList.ToArray();
                            }
                            #endregion

                            var buildingOwnerFile = consFiles.Where(o => o.FileTypeCode == "BUILDING_OWNER_CONSENT_DOC").FirstOrDefault();
                            if (buildingOwnerFile != null)
                            {
                                var buildingOwnerTypeName = buildingOwnerFile.Extras.ContainsKey("FILETYPENAME") ? buildingOwnerFile.Extras["FILETYPENAME"] : string.Empty;
                                post.DelegationDocuments = new DPTFileMetaData[]
                                {
                                    new DPTFileMetaData()
                                    {
                                        FileId = buildingOwnerFile.FileID,
                                        DocName = buildingOwnerTypeName,
                                        ContentType = buildingOwnerFile.ContentType, 
                                        FileSize = buildingOwnerFile.FileSize,
                                        Name = buildingOwnerFile.FileName,
                                        FileTypeCode = buildingOwnerFile.FileTypeCode
                                    }
                                };
                            }
                            #region [หนังสือแสดงความยินยอมเจ้าของอาคาร]
                            //var buildingOwnerConsentFileTotal = consFiles.Where(o => o.FileTypeCode.Contains("BUILDING_OWNERSHIP_DOC")).Count();
                            var buildingOwnerConsentFiles = consFiles.Where(o => o.FileTypeCode.Contains("BUILDING_OWNERSHIP_DOC")).ToList();
                            if (buildingOwnerConsentFiles.Count > 0)
                            {
                                var pplList = new List<DPTFileMetaData>();

                                for (int i = 0; i < buildingOwnerConsentFiles.Count; i++)
                                {
                                    //var buildingOwnerConsentFile = consFiles.Where(o => o.FileTypeCode == "BUILDING_OWNERSHIP_DOC-" + i).FirstOrDefault();
                                    var buildingOwnerConsentFile = buildingOwnerConsentFiles[i];
                                    var buildingOwnerConsentTypeName = buildingOwnerConsentFile.Extras.ContainsKey("FILETYPENAME") ? buildingOwnerConsentFile.Extras["FILETYPENAME"] + "(ไฟล์ที่ " + (i+1) + ")" : string.Empty;
                                    pplList.Add(new DPTFileMetaData()
                                    {
                                        FileId = buildingOwnerConsentFile.FileID,
                                        DocName = buildingOwnerConsentTypeName,
                                        ContentType = buildingOwnerConsentFile.ContentType,
                                        FileSize = buildingOwnerConsentFile.FileSize,
                                        Name = buildingOwnerConsentFile.FileName,
                                        FileTypeCode = "BUILDING_OWNERSHIP_DOC"
                                        //FileTypeCode = buildingOwnerConsentFile.FileTypeCode
                                    });
                                }
                                post.BuildingOwnershipDocuments = pplList.ToArray();
                                //var buildingOwnerConsentFile = consFiles.Where(o => o.FileTypeCode == "BUILDING_OWNERSHIP_DOC").FirstOrDefault();
                                //var buildingOwnerConsentTypeName = buildingOwnerConsentFile.Extras.ContainsKey("FILETYPENAME") ? buildingOwnerConsentFile.Extras["FILETYPENAME"] : string.Empty;
                                //post.BuildingOwnershipDocuments = new DPTFileMetaData[]
                                //{
                                //    new DPTFileMetaData()
                                //    {
                                //        FileId = buildingOwnerConsentFile.FileID,
                                //        DocName = buildingOwnerConsentTypeName,
                                //        ContentType = buildingOwnerConsentFile.ContentType,
                                //        FileSize = buildingOwnerConsentFile.FileSize,
                                //        Name = buildingOwnerConsentFile.FileName,
                                //        FileTypeCode = buildingOwnerConsentFile.FileTypeCode
                                //    }
                                //};
                            }
                            #endregion

                            var otherFileSection = model.UploadedFiles.Where(o => o.Description == "FREE_DOC_SECTION").FirstOrDefault();
                            if (otherFileSection != null)
                            {
                                var otherFiles = otherFileSection.Files;
                                var otherFileList = new List<DPTFileMetaData>();

                                var gp = otherFiles.GroupBy(n => n.FileReason);

                                foreach (var g in gp)
                                {
                                    if (g.Count() > 1)
                                    {
                                        int i = 0;
                                        foreach (var file in g)
                                        {
                                            i++;
                                            var fileTypeName = file.Extras.ContainsKey("FILETYPENAME") ? file.Extras["FILETYPENAME"] : string.Empty;

                                            otherFileList.Add(new DPTFileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = (!String.IsNullOrEmpty(file.FileReason) ? file.FileReason : fileTypeName) + " (" + i + ")",
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = "OTHER_DOC"
                                            });
                                        }
                                    }
                                    else
                                    {
                                        var file = g.FirstOrDefault();
                                        var fileTypeName = file.Extras.ContainsKey("FILETYPENAME") ? file.Extras["FILETYPENAME"] : string.Empty;

                                        otherFileList.Add(new DPTFileMetaData()
                                        {
                                            FileId = file.FileID,
                                            DocName = (!String.IsNullOrEmpty(file.FileReason) ? file.FileReason : fileTypeName),
                                            ContentType = file.ContentType,
                                            FileSize = file.FileSize,
                                            Name = file.FileName,
                                            FileTypeCode = "OTHER_DOC"
                                        });
                                    }
                                }
                                /*
                                foreach (var file in otherFiles)
                                {
                                    var fileTypeName = file.Extras.ContainsKey("FILETYPENAME") ? file.Extras["FILETYPENAME"] : string.Empty;
                                    otherFileList.Add(new DPTFileMetaData()
                                    {
                                        FileId = file.FileID,
                                        DocName = (!String.IsNullOrEmpty(file.FileReason) ? file.FileReason : fileTypeName),
                                        ContentType = file.ContentType,
                                        FileSize = file.FileSize,
                                        Name = file.FileName,
                                        FileTypeCode = file.FileTypeCode
                                    });
                                }*/
                                if (otherFileList != null && otherFileList.Count > 0)
                                {
                                    post.OtherDocuments = otherFileList.ToArray();
                                }
                            }
                            var a1LicenseFile = consFiles.Where(o => o.FileTypeCode == "BUILDING_A1LICENSE_DOC").FirstOrDefault();
                            var a1LicenseTypeName = a1LicenseFile.Extras.ContainsKey("FILETYPENAME") ? a1LicenseFile.Extras["FILETYPENAME"] : string.Empty;
                            post.A1LicenseDocuments = new DPTFileMetaData[]
                            {
                                new DPTFileMetaData()
                                {
                                    FileId = a1LicenseFile.FileID,
                                    DocName = a1LicenseTypeName,
                                    ContentType = a1LicenseFile.ContentType,
                                    FileSize = a1LicenseFile.FileSize,
                                    Name = a1LicenseFile.FileName,
                                    FileTypeCode = a1LicenseFile.FileTypeCode
                                }
                            };
                            #endregion

                            var jsonPost = JsonConvert.SerializeObject(post); // Serialize model data to JSON

                            if (post.Buildings.Count() <= 0)
                            {
                                string error = "ระบบไม่ส่งคำร้อง เนื่องจากไม่ได้ระบุอาคารที่ต้องการขอใบรับรองฯ กรุณายื่นคำร้องใหม่อีกครั้ง";
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                throw new Exception(error);
                            }

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

                            var apiResponse = Api.Call(regisUrl, HttpVerb.POST, null, jsonPost, ContentType.ApplicationJson);
                            if (apiResponse != null)
                            {
                                if (apiResponse.HasValues)
                                {
                                    if (apiResponse["MessageCode"] != null && apiResponse["MessageCode"].ToString() == "00000")
                                    {
                                        // Clear section data
                                        var singleformReq = new SingleFormRequestEntity();
                                        singleformReq.DeleteSections(model.IdentityID, null,
                                            new string[]
                                            {
                                                "APP_BUILDING_R6_SEARCH_AREA_SECTION",
                                                "APP_BUILDING_R6_SEARCH_SECTION",
                                                "APP_BUILDING_G1_SUPERVISE_ARCHITECT",
                                                "APP_BUILDING_G1_SUPERVISE_ENGINEER"
                                            }
                                        );


                                        // Clear uploaded files
                                        var singleFormTran = SingleFormTransaction.Get(model.IdentityID);
                                        if (singleFormTran != null && singleFormTran.UploadedFiles != null && singleFormTran.UploadedFiles.Count > 0)
                                        {
                                            var fg = singleFormTran.UploadedFiles.Where(o => o.Description == "CONSTRUCTION_SITE_INFORMATION").SingleOrDefault();
                                            if (fg != null)
                                            {
                                                singleFormTran.RemoveUploadedFiles(fg.FileGroupID);
                                            }

                                            var otherFg = singleFormTran.UploadedFiles.Where(o => o.Description == "FREE_DOC_SECTION").FirstOrDefault();
                                            if (otherFg != null)
                                            {
                                                singleFormTran.RemoveUploadedFiles(otherFg.FileGroupID);
                                            }
                                        }

                                        result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, apiResponse["Result"].ToString(), apiResponse.ToString(), jsonPost);
                                        result.Success = true;
                                    }
                                    else
                                    {
                                        string msg = string.Format("[{0}]{1}", apiResponse["MessageCode"].ToString(), apiResponse["Message"].ToString());
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, msg, apiResponse.ToString(), jsonPost, true);
                                        throw new Exception(msg);
                                    }
                                }
                                else
                                {
                                    string error = "error";
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
                    case AppsStage.None:
                    case AppsStage.UserUpdate:
                        {
                            if (model.Status == ApplicationStatusV2Enum.CHECK || model.Status == ApplicationStatusV2Enum.PENDING)
                            {
                                #region [POST DATA]
                                var post = new DPTR6Request()
                                {
                                    RequestType = "R6",
                                    BizGuid = model.ApplicationRequestID.ToString(),
                                };

                                var requestedFiles = model.UploadedFiles.Where(o => o.Description == "REQUESTED_FILE").OrderByDescending(o => o.CreatedDate).FirstOrDefault();
                                if (requestedFiles != null && requestedFiles.Files != null && requestedFiles.Files.Count > 0)
                                {
                                    List<DPTFileMetaData> ownerConsentDocList = new List<DPTFileMetaData>(); 
                                    List<DPTFileMetaData> titleDeedList = new List<DPTFileMetaData>();
                                    List<DPTFileMetaData> additionalList = new List<DPTFileMetaData>();
                                    List<DPTFileMetaData> otherList = new List<DPTFileMetaData>();
                                    //List<DPTEADocument> deList = new List<DPTEADocument>();
                                    //List<DPTEADocument> daList = new List<DPTEADocument>();
                                    List<DPTEADocument> seList = new List<DPTEADocument>();
                                    List<DPTEADocument> saList = new List<DPTEADocument>();

                                    foreach (var file in requestedFiles.Files)
                                    {
                                        var fileTypeCode = file.FileTypeCode;
                                        var fileTypeName = file.Extras.ContainsKey("FILETYPENAME") ? file.Extras["FILETYPENAME"].ToString() : string.Empty;
                                        var fileId = file.Extras.ContainsKey("FILEID") ? file.Extras["FILEID"].ToString() : string.Empty;
                                        Dictionary<string, object> extras = new Dictionary<string, object>();
                                        extras.Add("FileId", fileId);

                                        var eaValid = false;
                                        var licenseNo = string.Empty;
                                        var eaType = 0;

                                        if (file.Extras.ContainsKey("EADOCUMENT_LICENSENO") && file.Extras.ContainsKey("EADOCUMENT_EATYPE"))
                                        {
                                            eaValid = true;
                                            licenseNo = file.Extras["EADOCUMENT_LICENSENO"].ToString();
                                            eaType = int.Parse(file.Extras["EADOCUMENT_EATYPE"].ToString());
                                        }

                                        if (!string.IsNullOrEmpty(fileTypeCode) && !string.IsNullOrEmpty(fileTypeName) && !string.IsNullOrEmpty(fileId))
                                        {
                                            if (fileTypeCode == "APPLICANT_ID_CARD_COPY")
                                            {
                                                post.CitizenIDCards = new DPTFileMetaData[]
                                                {
                                                    new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    }
                                                };
                                            }
                                            else if (fileTypeCode == "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY")
                                            {
                                                List<DPTFileMetaData> fileList = new List<DPTFileMetaData>()
                                                {
                                                    new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    }
                                                };
                                                post.JuristicPersonRegisterationDocuments = fileList.ToArray();
                                            }
                                            else if (fileTypeCode == "JURISTIC_DELEGATOR_DOC")
                                            {
                                                List<DPTFileMetaData> fileList = new List<DPTFileMetaData>()
                                                {
                                                    new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    }
                                                };
                                                post.DelegationRepresentationDocuments = fileList.ToArray();
                                            }
                                            /*
                                            else if (fileTypeCode == "CONSTRUCTION_BLUEPRINT")
                                            {
                                                List<DPTFileMetaData> fileList = new List<DPTFileMetaData>()
                                                {
                                                    new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    }
                                                };
                                                post.Plans = fileList.ToArray();
                                            }
                                            else if (fileTypeCode == "CALCULATION_PLAN")
                                            {
                                                List<DPTFileMetaData> fileList = new List<DPTFileMetaData>()
                                                {
                                                    new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    }
                                                };
                                                post.Calculations = fileList.ToArray();
                                            }*/
                                            else if (fileTypeCode == "BUILDING_OWNERSHIP_DOC")
                                            {
                                                //List<DPTFileMetaData> fileList = new List<DPTFileMetaData>()
                                                //{
                                                //    new DPTFileMetaData()
                                                //    {
                                                //        FileId = file.FileID,
                                                //        DocName = fileTypeName,
                                                //        ContentType = file.ContentType,
                                                //        FileSize = file.FileSize,
                                                //        Name = file.FileName,
                                                //        FileTypeCode = file.FileTypeCode,
                                                //        Extras = extras
                                                //    }
                                                //};
                                                //post.BuildingOwnershipDocuments = fileList.ToArray();
                                                var item = new DPTFileMetaData()
                                                {
                                                    FileId = file.FileID,
                                                    DocName = fileTypeName,
                                                    ContentType = file.ContentType,
                                                    FileSize = file.FileSize,
                                                    Name = file.FileName,
                                                    FileTypeCode = file.FileTypeCode,
                                                    Extras = extras
                                                };
                                                ownerConsentDocList.Add(item);
                                            }
                                            else if (fileTypeCode == "BUILDING_OWNER_CONSENT_DOC")
                                            {
                                                List<DPTFileMetaData> fileList = new List<DPTFileMetaData>()
                                                {
                                                    new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    }
                                                };
                                                post.DelegationDocuments = fileList.ToArray();
                                            }
                                            else if (fileTypeCode == "BUILDING_A1LICENSE_DOC")
                                            {
                                                List<DPTFileMetaData> fileList = new List<DPTFileMetaData>()
                                                {
                                                    new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    }
                                                };
                                                post.A1LicenseDocuments = fileList.ToArray();
                                            }
                                            else if (fileTypeCode == "TITLE_DEED_COPY")
                                            {
                                                var item = new DPTFileMetaData()
                                                {
                                                    FileId = file.FileID,
                                                    DocName = fileTypeName,
                                                    ContentType = file.ContentType,
                                                    FileSize = file.FileSize,
                                                    Name = file.FileName,
                                                    FileTypeCode = file.FileTypeCode,
                                                    Extras = extras
                                                };
                                                titleDeedList.Add(item);
                                            }
                                            /*
                                            else if (fileTypeCode == "DESIGN_ENGINEER_PROFESSIONAL_LICENSE" && eaValid)
                                            {
                                                var thisEA = deList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.LicenseForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        LicenseForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    deList.Add(newEA);
                                                }

                                            }
                                            else if (fileTypeCode == "DESIGN_ENGINEER_CONSENT_DOC" && eaValid)
                                            {
                                                var thisEA = deList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.ConsentForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        ConsentForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    deList.Add(newEA);
                                                }
                                            }
                                            else if (fileTypeCode == "DESIGN_ARCHITECT_PROFESSIONAL_LICENSE" && eaValid)
                                            {
                                                var thisEA = daList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.LicenseForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        LicenseForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    daList.Add(newEA);
                                                }
                                            }
                                            else if (fileTypeCode == "DESIGN_ARCHITECT_CONSENT_DOC" && eaValid)
                                            {
                                                var thisEA = daList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.ConsentForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        ConsentForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    daList.Add(newEA);
                                                }
                                            }*/
                                            else if (fileTypeCode == "SUPERVISE_ENGINEER_PROFESSIONAL_LICENSE" && eaValid)
                                            {
                                                var thisEA = seList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.LicenseForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        LicenseForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    seList.Add(newEA);
                                                }
                                            }
                                            else if (fileTypeCode == "SUPERVISE_ENGINEER_CONSENT_DOC" && eaValid)
                                            {
                                                var thisEA = seList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.ConsentForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        ConsentForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    seList.Add(newEA);
                                                }
                                            }
                                            else if (fileTypeCode == "SUPERVISE_ARCHITECT_PROFESSIONAL_LICENSE" && eaValid)
                                            {
                                                var thisEA = saList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.LicenseForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        LicenseForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    saList.Add(newEA);
                                                }
                                            }
                                            else if (fileTypeCode == "SUPERVISE_ARCHITECT_CONSENT_DOC" && eaValid)
                                            {
                                                var thisEA = saList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.ConsentForms = new DPTFileMetaData[]
                                                    {
                                                    new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        ConsentForms = new DPTFileMetaData[]
                                                        {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                        }
                                                    };
                                                    saList.Add(newEA);
                                                }
                                            }
                                            else if (fileTypeCode == "ADDITIONAL_DOC")
                                            {
                                                var additionalFile = new DPTFileMetaData()
                                                {
                                                    FileId = file.FileID,
                                                    DocName = fileTypeName,
                                                    ContentType = file.ContentType,
                                                    FileSize = file.FileSize,
                                                    Name = file.FileName,
                                                    FileTypeCode = file.FileTypeCode,
                                                    Extras = extras
                                                };
                                                additionalList.Add(additionalFile);
                                            }
                                            else if (fileTypeCode == "OTHER_DOC")
                                            {
                                                var otherFile = new DPTFileMetaData()
                                                {
                                                    FileId = file.FileID,
                                                    DocName = fileTypeName,
                                                    ContentType = file.ContentType,
                                                    FileSize = file.FileSize,
                                                    Name = file.FileName,
                                                    FileTypeCode = file.FileTypeCode,
                                                    Extras = extras
                                                };
                                                otherList.Add(otherFile);
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(fileTypeCode) && !string.IsNullOrEmpty(fileTypeName))
                                        {
                                            if (fileTypeCode == "ADDITIONAL_DOC")
                                            {
                                                var additionalFile = new DPTFileMetaData()
                                                {
                                                    FileId = file.FileID,
                                                    DocName = fileTypeName,
                                                    ContentType = file.ContentType,
                                                    FileSize = file.FileSize,
                                                    Name = file.FileName,
                                                    FileTypeCode = file.FileTypeCode
                                                };
                                                additionalList.Add(additionalFile);
                                            }
                                            else if (fileTypeCode == "OTHER_DOC")
                                            {
                                                var otherFile = new DPTFileMetaData()
                                                {
                                                    FileId = file.FileID,
                                                    DocName = fileTypeName,
                                                    ContentType = file.ContentType,
                                                    FileSize = file.FileSize,
                                                    Name = file.FileName,
                                                    FileTypeCode = file.FileTypeCode
                                                };
                                                otherList.Add(otherFile);
                                            }
                                        }
                                    }

                                    //post.TitleDeedDocuments = titleDeedList.Count > 0 ? titleDeedList.ToArray() : null;
                                    //post.DesignEngineerDocuments = deList.Count > 0 ? deList.ToArray() : null;
                                    //post.DesignArchitectDocuments = daList.Count > 0 ? daList.ToArray() : null;

                                    post.BuildingOwnershipDocuments = ownerConsentDocList.Count > 0 ? ownerConsentDocList.ToArray() : null;
                                    post.SiteEngineerDocuments = seList.Count > 0 ? seList.ToArray() : null;
                                    post.SiteArchitectDocuments = saList.Count > 0 ? saList.ToArray() : null;
                                    post.AdditionalDocuments = additionalList.Count > 0 ? additionalList.ToArray() : null;
                                    post.OtherDocuments = otherList.Count > 0 ? otherList.ToArray() : null;
                                }
                                #endregion
                                var jsonPost = JsonConvert.SerializeObject(post);

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

                                var apiResponse = Api.Call(regisUrl, HttpVerb.PUT, null, jsonPost, ContentType.ApplicationJson);
                                if (apiResponse != null)
                                {
                                    if (apiResponse.HasValues)
                                    {
                                        if (apiResponse["MessageCode"] != null && apiResponse["MessageCode"].ToString() == "00000")
                                        {
                                            DateTime expDt = DateTime.MinValue;
                                            if (apiResponse["ExpectedFinishDate"] != null && !string.IsNullOrEmpty(apiResponse["MessageCode"].ToString()))
                                            {
                                                var isDt = DateTime.TryParse(apiResponse["ExpectedFinishDate"].ToString(), out expDt);
                                                if (isDt)
                                                {
                                                    request.ExpectedFinishDate = expDt;
                                                }
                                            }
                                            result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, apiResponse["Result"].ToString(), apiResponse.ToString(), jsonPost);
                                            result.Success = true;
                                        }
                                        else
                                        {
                                            string msg = string.Format("[{0}]{1}", apiResponse["MessageCode"].ToString(), apiResponse["Message"].ToString());
                                            result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, msg, apiResponse.ToString(), jsonPost, true);
                                            throw new Exception(msg);
                                        }
                                    }
                                    else
                                    {
                                        string error = "error";
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

        public override FileMetadataEntity InvokeFilePreDoc(string IdentityID, string FileTypeCode)
        {
            var draftData = SingleFormRequestEntity.Get(IdentityID);
            var model = draftData.Adapt<SingleFormRequestViewModel>();

            var cInfo = getA1LicenseInfo(model);
            FileMetadataEntity entity = null;

            // TODO: Get FileId from service
            if (cInfo.responseModel != null)
            {
                if (cInfo.responseModel.A1LicenseDocuments != null && cInfo.responseModel.A1LicenseDocuments.Length > 0)
                {
                    var a1Metadata = cInfo.responseModel.A1LicenseDocuments[0];

                    entity = new FileMetadataEntity
                    {
                        FileName = a1Metadata.Name,
                        ContentType = a1Metadata.ContentType, 
                        FileSize = a1Metadata.FileSize,
                        FileID = a1Metadata.FileId
                    };

                }
                // TODO: Get file from file server by using FileId


            }

            // Return file as FileMetadataEntity
            return entity;
            //return base.InvokeFilePreDoc(IdentityID, FileTypeCode);
        }

        private (JObject response, DPTR6Request responseModel) getA1LicenseInfo(SingleFormRequestViewModel model)
        {
            JObject response = null;
            Dictionary<string, string> args = null;
            var responseModel = new DPTR6Request();
            //if (model.IdentityType == UserTypeEnum.Citizen)
            //    responseModel.Applicant = new DPTPersonApplicant();
            //else if (model.IdentityType == UserTypeEnum.Juristic)
            //    responseModel.Applicant = new DPTJuristicPersonApplicant();

            var formdata_area = model.SectionData.Where(e => e.SectionName == "APP_BUILDING_R6_SEARCH_AREA_SECTION").Select(e => e.FormData).FirstOrDefault();
            var formdata = model.SectionData.Where(e => e.SectionName == "APP_BUILDING_R6_SEARCH_SECTION").Select(e => e.FormData).FirstOrDefault();
            if (formdata_area != null && formdata != null)
            {
                Api.OnCheckingApplicationError += (ex) =>
                {
                    throw ex;
                };

                var url = ConfigurationManager.AppSettings["DPT_A1LICENSE_CHECK_WS_URL"];
                //var url = "http://164.115.9.148/api/BizPortal/License/A1?orgcode=21050101&licenseNo=%E0%B8%AE999666544&releasedDate=2019-10-14";

                // check เลขที่ใบอนุญาต และ refCode
                string releasedate_txt = formdata.TryGetString("APP_BUILDING_R6_SEARCH_SECTION_RELEASED_DATE");
                DateTime releasedate_dt = DateTime.ParseExact(releasedate_txt, "dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"));
                args = new Dictionary<string, string> {
                    { "orgcode", formdata_area.TryGetString("AJAX_DROPDOWN_APP_BUILDING_R6_AREA_SEARCH_RESPONSIBLE_AREA") },
                    { "licenseNo", formdata.TryGetString("APP_BUILDING_R6_SEARCH_SECTION_LICENSE_ID").Trim() },
                    { "releasedDate", releasedate_dt.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) }
                };

                //args = new Dictionary<string, string> { { "orgcode", "21050101" }, { "licenseNo", "ฮ999666544" }, { "releasedDate", "2019-10-14" } };
                response = Api.Get(url, args, ContentType.ApplicationJson);

                if (response.HasValues && response["Result"] != null)
                {
                    if (model.IdentityType == UserTypeEnum.Citizen)
                        responseModel = response["Result"].ToObject<DPTR6RequestCitizen>();
                    else if (model.IdentityType == UserTypeEnum.Juristic)
                        responseModel = response["Result"].ToObject<DPTR6RequestJuristic>();
                }
                else
                {
                    throw new Exception("ไม่พบข้อมูล");
                }
            }

            return (response, responseModel);
        }
        public Dictionary<string, object> SetBuildingPurpose(string text)
        {
            //var result = new List<string>();
            var purpose = new Dictionary<string, string>
            {
                {"1","พักอาศัย"},
                {"2","อยู่อาศัยรวม"},
                {"3","พาณิชยกรรม"},
                {"4","อาคารชุด"},
                {"5","สำนักงาน"},
                {"6","หอพัก"},
                {"7","โรงแรม"},
                {"8","โรงงาน"},
                {"9","คลังสินค้า"},
                {"10","ค้าปลีกค้าส่ง"},
                {"11","ชุมนุมคน"},
                {"12","โรงมหรสพ"},
                {"13","ป้ายโฆษณา"},
                {"14","สถานศึกษา"},
                {"15","สถานพยาบาล"},
                {"16","อุตสาหกรรม"},
                {"17","ที่จอดรถยนต์"},
                {"18","สถานบริการ"},
                {"19","หอประชุม"},
                {"20","เครื่องเล่น"},
                {"21","เพื่อการศาสนา"},
                {"22","เก็บวัตถุอันตราย"},
                {"23","เลี้ยงสัตว์"},
                {"24","รั้ว กำแพง"},
                {"25","อื่นๆ"}
            };


            var ret = (from p in purpose
                       where p.Value == text
                       select p).FirstOrDefault();

            if (!ret.IsDefault())
            {
                return new Dictionary<string, object> { { "APP_BUILDING_R6_BUILDING_FOR_" + ret.Key, "true" } };
            }
            return new Dictionary<string, object>();
        }
    }
}
