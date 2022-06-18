using System;
using System.Linq;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using BizPortal.ViewModels.Apps.DBDStandard;
using BizPortal.Utils.Extensions;
using Newtonsoft.Json;
using EGA.WS;
using System.Configuration;

namespace BizPortal.AppsHook
{
    public abstract class StoreBaseAppHook : SingleFormRendererAppHook
    {
        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.Success = true;
            result.SendToEmail = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL").DefaultString();


            // คำร้องใหม่
            if (stage == AppsStage.UserCreate)
            {
                // ใส่ข้อมูลพื้นที่เจ้าของคำร้องจากที่อยู่ร้านค้า
                if (AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY.Contains(request.AppSysName))
                {
                    if (request.AppSysName == AppSystemNameTextConst.APP_TAX || request.AppSysName == AppSystemNameTextConst.APP_TAX_RENEW)
                    {
                        var storeInfo = request.Data["INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"].Data;
                        if (storeInfo.ContainsKey("ADDRESS_PROVINCE_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"))
                        {
                            request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"]);
                            request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"]);
                            request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"]);

                            request.Province = (storeInfo["ADDRESS_PROVINCE_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS_TEXT"]);
                            request.Amphur = (storeInfo["ADDRESS_AMPHUR_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS_TEXT"]);
                            request.Tumbol = (storeInfo["ADDRESS_TUMBOL_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS_TEXT"]);
                        }
                    }
                    else if (request.AppSysName == AppSystemNameTextConst.APP_TAX_EDIT || request.AppSysName == AppSystemNameTextConst.APP_TAX_CANCEL)
                    {
                        var storeInfo = request.Data["INFORMATION_STORE_TAX_PROVINCE_ONLY"].Data;
                        if (storeInfo.ContainsKey("ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY"))
                        {
                            request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY"]);
                            request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY"]);
                            request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY"]);

                            request.Province = (storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY_TEXT"]);
                            request.Amphur = (storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY_TEXT"]);
                            request.Tumbol = (storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY_TEXT"]);
                        }
                    }
                    else
                    {
                        var storeInfo = request.Data["INFORMATION_STORE_HAS_PROVINCE_ONLY"].Data;
                        if (storeInfo.ContainsKey("ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY"))
                        {
                            request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY"]);
                            request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY"]);
                            request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY"]);

                            request.Province = (storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY_TEXT"]);
                            request.Amphur = (storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY_TEXT"]);
                            request.Tumbol = (storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY_TEXT"]);
                        }
                    }
                }
                else
                {
                    if (request.Data.ContainsKey("INFORMATION_STORE"))
                    {
                        var storeInfo = request.Data["INFORMATION_STORE"].Data;
                        if (storeInfo.ContainsKey("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"))
                        {
                            request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"]);
                            request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS"]);
                            request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS"]);

                            request.Province = (storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"]);
                            request.Amphur = (storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"]);
                            request.Tumbol = (storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"]);
                        }
                    }
                }
            }

            return result;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }

    }
}
