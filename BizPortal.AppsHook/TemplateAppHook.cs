using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Apps;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using EGA.WS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BizPortal.AppsHook
{
    public class TemplateAppHook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = true;

            try
            {
                switch (stage)
                {
                    case AppsStage.UserCreate:
                        var post = string.Empty; // Model data

                        string regisUrl = ConfigurationManager.AppSettings["WS_URL"];
                        var jsonPost = JsonConvert.SerializeObject(post); // Serialize model data to JSON

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
                                // TODO:
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

                        break;
                    case AppsStage.None:
                    case AppsStage.UserUpdate:
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
    }
}
