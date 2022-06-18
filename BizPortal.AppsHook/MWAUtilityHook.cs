using System;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Apps;
using Newtonsoft.Json;
using EGA.WS;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BizPortal.Utils.Helpers;
using BizPortal.Integrated;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels;

namespace BizPortal.AppsHook
{
    public class MWAUtilityHook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ViewModels.SingleForm.ISectionData> sectionData)
        {
            return null;
        }

        public override bool AllowFreeDocument { get; } = false;

        public override Dictionary<string, string> TranslateKeyValue(ApplicationRequestViewModel model)
        {
            var mailMerges = base.TranslateKeyValue(model);

            // Contact Data
            mailMerges.Add("CONTACT_NAME", "คุณ" + model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_FIRSTNAME") + " " + model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_LASTNAME"));
            mailMerges.Add("CONTACT_PHONE", model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL"));
            mailMerges.Add("CONTACT_EMAIL", model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL"));

            // Document Data
            mailMerges.Add("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY", ResourceHelper.GetResourceWord("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY", "Apps_Utility", "th"));
            mailMerges.Add("IDENTITY_COPY", ResourceHelper.GetResourceWord("IDENTITY_COPY", "FileType", "th"));
            mailMerges.Add("THAI_IDENTITY_COPY", ResourceHelper.GetResourceWord("THAI_IDENTITY_COPY", "Apps_Utility", "th"));
            mailMerges.Add("FOREIGNER_PASSPORT_COPY", ResourceHelper.GetResourceWord("FOREIGNER_PASSPORT_COPY", "Apps_Utility", "th"));
            mailMerges.Add("FOREIGNER_IDENTITY_COPY", ResourceHelper.GetResourceWord("FOREIGNER_IDENTITY_COPY", "Apps_Utility", "th"));
            mailMerges.Add("HOUSEHOLD_REGISTRATION_COPY", ResourceHelper.GetResourceWord("HOUSEHOLD_REGISTRATION_COPY", "Apps_Utility", "th"));
            mailMerges.Add("RENTAL_AGREEMENT_DOCUMENT", ResourceHelper.GetResourceWord("RENTAL_AGREEMENT_DOCUMENT", "Apps_Utility", "th"));
            mailMerges.Add("LANDWITHBUILDING_RENTAL_AGREEMENT", ResourceHelper.GetResourceWord("LANDWITHBUILDING_RENTAL_AGREEMENT", "Apps_Utility", "th"));
            mailMerges.Add("HOUSE_OWNER_REG_COPY", ResourceHelper.GetResourceWord("HOUSE_OWNER_REG_COPY", "Apps_Utility", "th"));
            mailMerges.Add("HOUSE_ID_DOC_COPY", ResourceHelper.GetResourceWord("HOUSE_ID_DOC_COPY", "Apps_Utility", "th"));
            mailMerges.Add("LANDWITHBUILDING_CONTRACT_AGREEMENT", ResourceHelper.GetResourceWord("LANDWITHBUILDING_CONTRACT_AGREEMENT", "Apps_Utility", "th"));
            mailMerges.Add("CONTRACT_AGREEMENT", ResourceHelper.GetResourceWord("CONTRACT_AGREEMENT", "Apps_Utility", "th"));

            mailMerges.Add("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY_EN", ResourceHelper.GetResourceWord("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY", "Apps_Utility", "en"));
            mailMerges.Add("IDENTITY_COPY_EN", ResourceHelper.GetResourceWord("IDENTITY_COPY", "FileType", "en"));
            mailMerges.Add("THAI_IDENTITY_COPY_EN", ResourceHelper.GetResourceWord("THAI_IDENTITY_COPY", "Apps_Utility", "en"));
            mailMerges.Add("FOREIGNER_PASSPORT_COPY_EN", ResourceHelper.GetResourceWord("FOREIGNER_PASSPORT_COPY", "Apps_Utility", "en"));
            mailMerges.Add("FOREIGNER_IDENTITY_COPY_EN", ResourceHelper.GetResourceWord("FOREIGNER_IDENTITY_COPY", "Apps_Utility", "en"));
            mailMerges.Add("HOUSEHOLD_REGISTRATION_COPY_EN", ResourceHelper.GetResourceWord("HOUSEHOLD_REGISTRATION_COPY", "Apps_Utility", "en"));
            mailMerges.Add("RENTAL_AGREEMENT_DOCUMENT_EN", ResourceHelper.GetResourceWord("RENTAL_AGREEMENT_DOCUMENT", "Apps_Utility", "en"));
            mailMerges.Add("LANDWITHBUILDING_RENTAL_AGREEMENT_EN", ResourceHelper.GetResourceWord("LANDWITHBUILDING_RENTAL_AGREEMENT", "Apps_Utility", "en"));
            mailMerges.Add("HOUSE_OWNER_REG_COPY_EN", ResourceHelper.GetResourceWord("HOUSE_OWNER_REG_COPY", "Apps_Utility", "en"));
            mailMerges.Add("HOUSE_ID_DOC_COPY_EN", ResourceHelper.GetResourceWord("HOUSE_ID_DOC_COPY", "Apps_Utility", "en"));
            mailMerges.Add("LANDWITHBUILDING_CONTRACT_AGREEMENT_EN", ResourceHelper.GetResourceWord("LANDWITHBUILDING_CONTRACT_AGREEMENT", "Apps_Utility", "en"));
            mailMerges.Add("CONTRACT_AGREEMENT_EN", ResourceHelper.GetResourceWord("CONTRACT_AGREEMENT", "Apps_Utility", "en"));

            return mailMerges;
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;

            try
            {

                if (stage == AppsStage.UserCreate)
                {
                    request.NoDocument = true;
                    // เพิ่มจังหวัด อำเภอ ตำบล ลงในใบคำร้อง
                    request.ProvinceID = int.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS"));
                    request.AmphurID = int.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS"));
                    request.TumbolID = int.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS"));

                    request.Province = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS_TEXT");
                    request.Amphur = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS_TEXT");
                    request.Tumbol = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS_TEXT");

                    MWAUtilityRequest post = null;
                    var building = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_UTILITY_ADDRESS");
                    if (!string.IsNullOrEmpty(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_UTILITY_ADDRESS")))
                    {
                        building = building + " ชั้น " + model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_UTILITY_ADDRESS");
                    }

                    var mobileNo = string.Empty;
                    var phoneNo = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL");

                    if (!string.IsNullOrEmpty(phoneNo))
                    {
                        if (phoneNo.Length == 10 && phoneNo.Substring(0, 2) != "02")
                        {
                            mobileNo = phoneNo;
                            phoneNo = string.Empty;
                        }
                    }

                    if (model.IdentityType == UserTypeEnum.Citizen)
                    {
                        post = new MWAUtilityRequest()
                        {
                            requestIdBIZ = model.ApplicationRequestID,
                            requestIdWLMAR = model.ApplicationRequestNumber,
                            branchCode = model.Data.TryGetData("MWA_INFORMATION").ThenGetStringData("MWA_BRANCH_ID"),
                            receiveChannel = "00", // 00 = other
                            receiveChannelOther = "Internet",
                            requestCode = "501", // 501 = คำร้องใหม่
                            requestChannel = "9", // ประชาชน
                            requestDetail = "ขอติดตั้งประปาใหม่",
                            firstName = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_FIRSTNAME"),
                            lastName = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_LASTNAME"),
                            houseNo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_UTILITY_ADDRESS"),
                            moo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_UTILITY_ADDRESS"),
                            village = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_UTILITY_ADDRESS"),
                            building = building,
                            soi = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_UTILITY_ADDRESS"),
                            road = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_UTILITY_ADDRESS"),
                            districtCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS"),
                            amphurCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS"),
                            provinceCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS"),
                            zipCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_UTILITY_ADDRESS"),
                            mobile = mobileNo,
                            email = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL"),
                            phoneNumber = phoneNo,
                            createdBy = "PEOPLE",
                            screenId = "PEOPLE",
                            ipaddress = "PEOPLE"
                        };

                        post.requestDetailWSList.Add(new requestDetailWSList()
                        {
                            useType = "0", //ติดตั้งถาวร
                            accountType = "P", //ผู้ใช้น้ำทั่วไป
                            taxId = model.IdentityID,
                            individualType = "1",
                            prefixCode = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_PREFIX_TH"),
                            firstName = model.Data.TryGetData("OPENID").ThenGetStringData("FIRSTNAME_TH"),
                            lastName = model.Data.TryGetData("OPENID").ThenGetStringData("LASTNAME_TH"),
                            houseId = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ID_UTILITY_ADDRESS"),
                            houseNo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_UTILITY_ADDRESS"),
                            moo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_UTILITY_ADDRESS"),
                            village = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_UTILITY_ADDRESS"),
                            building = building,
                            soi = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_UTILITY_ADDRESS"),
                            road = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_UTILITY_ADDRESS"),
                            districtCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS"),
                            amphurCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS"),
                            provinceCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS"),
                            zipCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_UTILITY_ADDRESS"),
                            latitude = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_LAT_UTILITY_ADDRESS"),
                            longitude = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_LNG_UTILITY_ADDRESS"),
                            meterSize = "0", // fix "0" = 1/2
                            accountClassCode = "00", // fix "00" = ที่อยู่อาศัย
                            billPrefixCode = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_PREFIX_TH"),
                            billFirstname = model.Data.TryGetData("OPENID").ThenGetStringData("FIRSTNAME_TH"),
                            billLastname = model.Data.TryGetData("OPENID").ThenGetStringData("LASTNAME_TH"),
                            billHouseNo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_UTILITY_ADDRESS"),
                            billMoo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_UTILITY_ADDRESS"),
                            billVillage = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_UTILITY_ADDRESS"),
                            billBuilding = building,
                            billSoi = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_UTILITY_ADDRESS"),
                            billRoad = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_UTILITY_ADDRESS"),
                            billDistrictCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS"),
                            billAmphurCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS"),
                            billProvinceCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS"),
                            billZipCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_UTILITY_ADDRESS")
                        });
                    }
                    else if (model.IdentityType == UserTypeEnum.Juristic)
                    {
                        var jurBuilding = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS");
                        if (!string.IsNullOrEmpty(model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS")))
                        {
                            jurBuilding = jurBuilding + " ชั้น " + model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS");
                        }

                        post = new MWAUtilityRequest()
                        {
                            requestIdBIZ = model.ApplicationRequestID,
                            requestIdWLMAR = model.ApplicationRequestNumber,
                            branchCode = model.Data.TryGetData("MWA_INFORMATION").ThenGetStringData("MWA_BRANCH_ID"),
                            receiveChannel = "00", // 00 = other
                            receiveChannelOther = "Biz Portal",
                            requestCode = "501", // 501 = คำร้องใหม่
                            requestChannel = "8", // Biz Portal
                            requestDetail = "ขอติดตั้งประปาใหม่",
                            firstName = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_FIRSTNAME"),
                            lastName = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_LASTNAME"),
                            houseNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                            moo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),
                            village = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS"),
                            building = jurBuilding,
                            soi = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"),
                            road = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"),
                            districtCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS"),
                            amphurCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS"),
                            provinceCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS"),
                            zipCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"),
                            mobile = mobileNo,
                            email = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL"),
                            phoneNumber = phoneNo,
                            createdBy = "BIZ",
                            screenId = "BIZ",
                            ipaddress = "BIZ"
                        };

                        post.requestDetailWSList.Add(new requestDetailWSList()
                        {
                            useType = "0", //ติดตั้งถาวร
                            accountType = "P", //ผู้ใช้น้ำทั่วไป
                            taxId = model.IdentityID,
                            taxBranch = 0,
                            individualType = "2",
                            prefixCode = "93",
                            firstName = model.Data.TryGetData("DBD").ThenGetStringData("JURISTIC_NAME_TH"),
                            lastName = string.Empty,
                            houseId = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ID_UTILITY_ADDRESS"),
                            houseNo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_UTILITY_ADDRESS"),
                            moo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_UTILITY_ADDRESS"),
                            village = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_UTILITY_ADDRESS"),
                            building = building,
                            soi = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_UTILITY_ADDRESS"),
                            road = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_UTILITY_ADDRESS"),
                            districtCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS"),
                            amphurCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS"),
                            provinceCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS"),
                            zipCode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_UTILITY_ADDRESS"),
                            latitude = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_LAT_UTILITY_ADDRESS"),
                            longitude = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_LNG_UTILITY_ADDRESS"),
                            meterSize = "0", // fix "0" = 1/2
                            accountClassCode = "00", // fix "00" = ที่อยู่อาศัย
                            billFirstname = model.Data.TryGetData("DBD").ThenGetStringData("JURISTIC_NAME_TH"),
                            billLastname = string.Empty,
                            billHouseNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                            billMoo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),
                            billVillage = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS"),
                            billBuilding = jurBuilding,
                            billSoi = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"),
                            billRoad = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"),
                            billDistrictCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS"),
                            billAmphurCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS"),
                            billProvinceCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS"),
                            billZipCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"),
                            paidTaxId = model.IdentityID,
                            paidTaxName = model.Data.TryGetData("DBD").ThenGetStringData("JURISTIC_NAME_TH"),
                            paidTaxBranch = 0,
                            paidHouseNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                            paidMoo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),
                            paidVillage = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS"),
                            paidBuilding = jurBuilding,
                            paidSoi = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"),
                            paidRoad = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"),
                            paidDistrictCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS"),
                            paidAmphurCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS"),
                            paidProvinceCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS"),
                            paidZipCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS")
                        });
                    }
                    
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

                    Dictionary<string, string> respData = new Dictionary<string, string>();

                    string regisUrl = ConfigurationManager.AppSettings["MWA_REGIS_WS_URL"];
                    var mwaResult = Api.Call(regisUrl, EGA.WS.HttpVerb.POST, null, jsonPost, EGA.WS.ContentType.ApplicationJson);
                    if (mwaResult.HasValues && mwaResult["responseStatus"].ToString() == "OK")
                    {
                        result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, mwaResult["responseStatus"].ToString(), mwaResult.ToString(), jsonPost);
                        result.Success = true;

                        string mobileNUmber = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL");

                        if (!string.IsNullOrEmpty(mobileNUmber))
                        {
                            ApplicationStatusSmsMessage sms = new ApplicationStatusSmsMessage()
                            {
                                Message = "การประปานครหลวงได้รับคำร้องติดตั้งประปาเรียบร้อยแล้ว",
                                MobileNumbers = new string[] { mobileNUmber }
                            };
                            sms.SendSms();
                        }

                        respData.Add("RESPONSE_CIS", mwaResult["requestId"].ToString());

                        if (request.Data.ContainsKey("MWA_RESPONSE_DATA"))
                        {
                            request.Data.Remove("MWA_RESPONSE_DATA");
                        }

                        request.Data.Add("MWA_RESPONSE_DATA", new ApplicationRequestDataGroupEntity()
                        {
                            GroupName = "MWA_RESPONSE_DATA",
                            Data = respData
                        });
                    }
                    else
                    {
                        string error = "Unable to request to " + regisUrl + ".";
                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, mwaResult.ToString(), jsonPost, true);
                        throw new Exception(error);
                    }
                }
                else
                {
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Exception = ex;
            }

            return result;
        }
    }
}
