using System;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Exceptions;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BizPortal.ViewModels.Apps;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json.Linq;
using BizPortal.ViewModels.SingleForm;
using EGA.WS;

namespace BizPortal.AppsHook
{
    public class PWAUtilityHook : SingleFormRendererAppHook
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

#if DEBUG
            result.CcToEmails = new List<string>();
            result.CcToEmails.Add("viriya.foopuntuwut@ega.or.th");
#endif
            try
            {
                PWAUtilityRequest pwa = null;
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

                        pwa = new PWAUtilityRequest();
                        pwa.InstallType = model.Data.TryGetData("PWA_INFORMATION").ThenGetByteData("INSTALL_TYPE_ID", 1);
                        pwa.UserType = model.IdentityType == UserTypeEnum.Juristic ? (byte)2 : (byte)1;
                        pwa.CardID = pwa.TaxNo = model.IdentityID;
                        pwa.Email = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL");
                        pwa.BirthDate = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("BIZ_CITIZEN_BIRTH_DATE");
                        pwa.CusDptPosition = string.Format("{0}{1} {2}", pwa.CustomerTitle, pwa.CustomerName, pwa.CustomerSurname);
                        pwa.AddressNo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_UTILITY_ADDRESS");
                        pwa.Building = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_UTILITY_ADDRESS");
                        pwa.Floor = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_UTILITY_ADDRESS");
                        pwa.VillageNo = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_UTILITY_ADDRESS");
                        //request.Village = model.Data.TryGetData("ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_VILLAGE");
                        pwa.Soi = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_UTILITY_ADDRESS");
                        pwa.Road = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_UTILITY_ADDRESS");
                        pwa.Province = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_UTILITY_ADDRESS");
                        pwa.Amphoe = pwa.Province + model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_UTILITY_ADDRESS");
                        pwa.Tambon = pwa.Amphoe + model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_UTILITY_ADDRESS");
                        pwa.OwnerID = model.Data.TryGetData("PWA_INFORMATION").ThenGetStringData("PWA_BRANCH");
                        pwa.Latitude = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetDoubleData("ADDRESS_LAT_UTILITY_ADDRESS");
                        pwa.Longitude = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetDoubleData("ADDRESS_LNG_UTILITY_ADDRESS");
                        pwa.Zipcode = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_UTILITY_ADDRESS");
                        pwa.Tel = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL");
                        pwa.AddrID = model.Data.TryGetData("UTILITY_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ID_UTILITY_ADDRESS");

                        if (model.IdentityType == UserTypeEnum.Juristic)
                        {
                            pwa.BranchNo = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("BIZ_BRANCH", "00000");
                            pwa.CustomerTitle = model.Data.TryGetData("DBD").ThenGetStringData("TYPE");
                            pwa.CustomerName = model.Data.TryGetData("DBD").ThenGetStringData("JURISTIC_NAME_TH");
                            pwa.CustomerSurname = string.Empty;
                        }
                        else
                        {
                            pwa.BranchNo = "00000";
                            pwa.CustomerTitle = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_PREFIX_TH_TEXT");
                            pwa.CustomerName = model.Data.TryGetData("OPENID").ThenGetStringData("FIRSTNAME_TH");
                            pwa.CustomerSurname = model.Data.TryGetData("OPENID").ThenGetStringData("LASTNAME_TH");
                        }

                        pwa.ApplicationRequestID = model.ApplicationRequestID.Value.ToString();
                        pwa.ApplicationRequestNumber = model.ApplicationRequestNumber;
                        pwa.AdjustStringLength();

                        var jsonPost = JsonConvert.SerializeObject(pwa);
                        string regisUrl = ConfigurationManager.AppSettings["PWA_REGIS_WS_URL"];

                        //var client = new RestClient("https://customer-application.pwa.co.th/wstest/receive");
                        //var restRequest = new RestRequest();
                        //restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                        //restRequest.Method = Method.POST;
                        //restRequest.AddParameter("customer", jsonPost);

                        //IRestResponse postResp = client.Execute(restRequest);
                        //if (postResp.StatusCode == System.Net.HttpStatusCode.OK)
                        //{
                        //    var json = JObject.Parse(postResp.Content);
                        //    if (json["Status"].ToString() == "Success")
                        //    {
                        //        result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, "Created", postResp.Content, jsonPost);
                        //        result.Success = true;
                        //    }
                        //    else
                        //    {
                        //        string error = string.Format("Unable to request to {0}. Status code is {1}: {2}.", regisUrl, postResp.StatusCode, postResp.StatusCode.ToString());
                        //        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, postResp.Content, jsonPost, true);
                        //        throw new Exception(error);
                        //    }
                        //}
                        //else
                        //{
                        //    string error = string.Format("Unable to request to {0}. Status code is {1}: {2}.", regisUrl, postResp.StatusCode, postResp.StatusCode.ToString());
                        //    result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, postResp.Content, jsonPost, true);
                        //    throw new Exception(error);
                        //}

                        var param = new Dictionary<string, string>
                        {
                            { "customer", jsonPost }
                        };
                        var apiResponse = Api.Call(regisUrl, HttpVerb.POST, param, null, ContentType.ApplicationXWwwFormUrlencoded);
                        if (apiResponse != null && apiResponse.HasValues)
                        {
                            if (apiResponse["Status"].ToString() == "Success")
                            {
                                result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, apiResponse["Status"].ToString(), apiResponse["Status"].ToString(), jsonPost);
                                result.Success = true;
                            }
                            else
                            {
                                string error = string.Format("Unable to request to {0}", regisUrl);
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, apiResponse["Status"].ToString(), jsonPost, true);
                                throw new Exception(error);
                            }
                        }
                        else
                        {
                            string error = string.Format("Unable to request to {0}", regisUrl);
                            result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, error, jsonPost, true);
                            throw new Exception(error);
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
