using System;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Exceptions;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BizPortal.ViewModels.SingleForm;

namespace BizPortal.AppsHook
{
    public class PEAUtilityHook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return null;
        }

        public override bool AllowFreeDocument { get; } = false;

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;

            var reqBody = new PEAServices.Insert_ReqRequest();
            try
            {
                switch (stage)
                {
                    case AppsStage.UserCreate:
                        request.NoDocument = true;
                        // เพิ่มจังหวัด อำเภอ ตำบล ลงในใบคำร้อง
                        request.ProvinceID = int.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS"));
                        request.AmphurID = int.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS"));
                        request.TumbolID = int.Parse(model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS"));

                        request.Province = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS_TEXT");
                        request.Amphur = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS_TEXT");
                        request.Tumbol = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS_TEXT");

                        PEAServices.PeaCosServiceSoap service = (PEAServices.PeaCosServiceSoap)new PEAServices.PeaCosServiceSoapClient();
                        if (model.IdentityType == UserTypeEnum.Citizen)
                        {
                            var birthDayStr = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("BIRTH_DATE");
                            DateTime birthDay = DateTime.MinValue;
                            bool birthDayStatus = DateTime.TryParseExact(birthDayStr, "dd/MM/yyyy",
                                CultureInfo.CreateSpecificCulture("th-TH"), DateTimeStyles.None, out birthDay);

                            reqBody = new PEAServices.Insert_ReqRequest()
                            {
                                Body = new PEAServices.Insert_ReqRequestBody()
                                {
                                    UserNameLogin = "bizportal",
                                    ApplicationReqId = model.ApplicationRequestID.DefaultString(),
                                    REQ_NO_EGA = model.ApplicationRequestNumber,
                                    Customer_Type = "1",
                                    // ข้อมูลประชาชน
                                    Card_No = model.IdentityID,
                                    TitleCustomer = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_PREFIX_TH_TEXT"),
                                    NameCustomer = model.Data.TryGetData("OPENID").ThenGetStringData("FIRSTNAME_TH"),
                                    SurnameCustomer = model.Data.TryGetData("OPENID").ThenGetStringData("LASTNAME_TH"),
                                    BirthDayCustomer = birthDayStatus ? birthDay.ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US")) : null,
                                    // ข้อมูลที่อยู่
                                    TSIC_CODE = model.Data.TryGetData("PEA_INFORMATION").ThenGetStringData("AJAX_DROPDOWN_PEA_TSIC"), // default = '00001'
                                    ProfileAddress_Code = null,
                                    ProfileAddress_no = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_CITIZEN_ADDRESS"),
                                    ProfileHouse_No = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS"),
                                    ProfileVillage_Name = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_CITIZEN_ADDRESS"),
                                    ProfileRoom_No = null,
                                    ProfileFloor_No = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_CITIZEN_ADDRESS"),
                                    ProfileRoad_Name = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS"),
                                    ProfileLane_Name = null,
                                    ProfileSoi_Name = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS"),
                                    ProfileTMB_CODE = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS") +
                                        model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS") +
                                        model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS"),
                                    //ProfileMobile_Number = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS"),
                                    ProfileMobile_Number = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL"),
                                    ProfileEmail_Address = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL"),
                                    // ข้อมูลสถานที่ติดตั้ง
                                    LocationAddress_Code = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ID_UTILITY_ADDRESS"),
                                    LocationAddress_no = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_UTILITY_ADDRESS"),
                                    LocationHouse_No = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_UTILITY_ADDRESS"),
                                    LocationVillage_Name = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_UTILITY_ADDRESS"),
                                    LocationRoom_No = null,
                                    LocationFloor_No = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_UTILITY_ADDRESS"),
                                    LocationRoad_Name = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_UTILITY_ADDRESS"),
                                    LocationLane_Name = null,
                                    LocationSoi_Name = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_UTILITY_ADDRESS"),
                                    LocationTMB_CODE = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS") +
                                        model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS") +
                                        model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS"),
                                    InstallPointLatitude = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_LAT_UTILITY_ADDRESS"),
                                    InstallPointLongitude = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_LNG_UTILITY_ADDRESS"),
                                    // ข้อมูลบริการ
                                    METER_TYPE_CODE = model.Data.TryGetData("PEA_INFORMATION").ThenGetStringData("DROPDOWN1ST_PEA_MEASUREMENT_TYPE"),
                                    METER_SIZE_CODE = model.Data.TryGetData("PEA_INFORMATION").ThenGetStringData("DROPDOWN2ND_PEA_MEASUREMENT_TYPE"),
                                    // ข้อมูลผู้ติดต่อ
                                    CustomerSign = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_FIRSTNAME") + " " +
                                        model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_LASTNAME") + " " +
                                        model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL")
                                }
                            };
                        }
                        else if (model.IdentityType == UserTypeEnum.Juristic)
                        {
                            var isVATstr = model.Data.TryGetData("PEA_INFORMATION").ThenGetStringData("PEA_IS_VAT_PEA_IS_VAT_TRUE");
                            string isVAT = null;
                            if (isVATstr == "true")
                            {
                                isVAT = "0";
                            }
                            else
                            {
                                isVAT = "1";
                            }

                            reqBody = new PEAServices.Insert_ReqRequest()
                            {
                                Body = new PEAServices.Insert_ReqRequestBody()
                                {
                                    UserNameLogin = "bizportal",
                                    ApplicationReqId = model.ApplicationRequestID.DefaultString(),
                                    REQ_NO_EGA = model.ApplicationRequestNumber,
                                    Customer_Type = "2",
                                    // ข้อมูลประชาชน
                                    Card_No = null,
                                    TitleCustomer = null,
                                    NameCustomer = null,
                                    SurnameCustomer = null,
                                    BirthDayCustomer = null,
                                    // ข้อมูลนิติบุคคล
                                    TAX_No = model.IdentityID,
                                    Branch_No = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("BIZ_BRANCH", "00000"),
                                    EntityName = model.Data.TryGetData("DBD").ThenGetStringData("JURISTIC_NAME_TH"),
                                    IsVAT = isVAT,
                                    // ข้อมูลที่อยู่
                                    TSIC_CODE = model.Data.TryGetData("PEA_INFORMATION").ThenGetStringData("AJAX_DROPDOWN_PEA_TSIC"), // default = '00001'
                                    ProfileAddress_Code = null,
                                    ProfileAddress_no = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                                    ProfileHouse_No = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),
                                    ProfileVillage_Name = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS"),
                                    ProfileRoom_No = null,
                                    ProfileFloor_No = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS"),
                                    ProfileRoad_Name = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"),
                                    ProfileLane_Name = null,
                                    ProfileSoi_Name = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"),
                                    ProfileTMB_CODE = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS") +
                                        model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS") +
                                        model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS"),
                                    ProfileMobile_Number = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL"),
                                    ProfileEmail_Address = null,
                                    // ข้อมูลสถานที่ติดตั้ง
                                    LocationAddress_Code = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ID_UTILITY_ADDRESS"),
                                    LocationAddress_no = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_UTILITY_ADDRESS"),
                                    LocationHouse_No = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_UTILITY_ADDRESS"),
                                    LocationVillage_Name = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_UTILITY_ADDRESS"),
                                    LocationRoom_No = null,
                                    LocationFloor_No = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_UTILITY_ADDRESS"),
                                    LocationRoad_Name = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_UTILITY_ADDRESS"),
                                    LocationLane_Name = null,
                                    LocationSoi_Name = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_UTILITY_ADDRESS"),
                                    LocationTMB_CODE = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS") +
                                        model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS") +
                                        model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS"),
                                    InstallPointLatitude = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_LAT_UTILITY_ADDRESS"),
                                    InstallPointLongitude = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_LNG_UTILITY_ADDRESS"),
                                    // ข้อมูลบริการ
                                    METER_TYPE_CODE = model.Data.TryGetData("PEA_INFORMATION").ThenGetStringData("DROPDOWN1ST_PEA_MEASUREMENT_TYPE"),
                                    METER_SIZE_CODE = model.Data.TryGetData("PEA_INFORMATION").ThenGetStringData("DROPDOWN2ND_PEA_MEASUREMENT_TYPE"),
                                    // ข้อมูลผู้ติดต่อ
                                    CustomerSign = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_FIRSTNAME") + " " +
                                        model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_LASTNAME") + " " +
                                        model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL")
                                }
                            };
                        }

                        var response = service.Insert_Req(reqBody);
                        Dictionary<string, string> respData = new Dictionary<string, string>();

                        // result
                        if (response.Body.Insert_ReqResult.REQ_NO != null)
                        {
                            result.Success = true;

                            respData.Add("REQ_NO", response.Body.Insert_ReqResult.REQ_NO);
                            respData.Add("JOB_STATUS", response.Body.Insert_ReqResult.Jobstatus);
                            respData.Add("JOB_STATUS_DESC", response.Body.Insert_ReqResult.JobStatus_Desc);
                            respData.Add("PEA_BRANCH", response.Body.Insert_ReqResult.OFFICE_NAME);

                            respData.Add("RESPONSE_JSON", JsonConvert.SerializeObject(response.Body.Insert_ReqResult, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() { IgnoreSerializableInterface = true } }));

                            if (request.Data.ContainsKey("PEA_RESPONSE_DATA"))
                            {
                                request.Data.Remove("PEA_RESPONSE_DATA");
                            }

                            request.Data.Add("PEA_RESPONSE_DATA", new ApplicationRequestDataGroupEntity()
                            {
                                GroupName = "PEA_RESPONSE_DATA",
                                Data = respData
                            });
                        }
                        else
                        {
                            string json = JsonConvert.SerializeObject(response.Body.Insert_ReqResult, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() { IgnoreSerializableInterface = true } });
                            respData.Add("RESPONSE_JSON", json);

                            if (request.Data.ContainsKey("PEA_RESPONSE_DATA"))
                            {
                                request.Data.Remove("PEA_RESPONSE_DATA");
                            }

                            request.Data.Add("PEA_RESPONSE_DATA", new ApplicationRequestDataGroupEntity()
                            {
                                GroupName = "PEA_RESPONSE_DATA",
                                Data = respData
                            });

                            throw new ExceptionWithData(response.Body.Insert_ReqResult.Message, json);
                        }
                        break;
                    case AppsStage.UserUpdate:
                    case AppsStage.AgentUpdate:
                    case AppsStage.ApiUpdate:
                    case AppsStage.None:
                    default:
                        result.Success = true;
                        break;
                }
            }
            catch (ExceptionWithData ex)
            {
                result.Message = ex.Message;
                result.Exception = ex;
                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, ex.Message, ex.CustomData.ToString(), null, true);
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
