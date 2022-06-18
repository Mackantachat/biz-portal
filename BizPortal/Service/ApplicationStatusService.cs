using BizPortal.DAL;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels;
using BizPortal.ViewModels.V2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BizPortal.Service
{
    public class ApplicationStatusService
    {
        public static bool IsEnableEPermitLogic()
        {
            return false;
        }

        public static bool IsFoundPaymentMethod(List<string> paymentMethodEnabledChoice, string paymentMethod)
        {
            bool isFound = false;
            if (paymentMethodEnabledChoice != null && paymentMethodEnabledChoice.Count > 0)
            {
                isFound = paymentMethodEnabledChoice.Where(p => p == paymentMethod).Any();
            }
            return isFound;
        }

        public static bool IsFoundPermitDeliveryType(List<string> permitDeliveryTypeEnabledChoice, string permitDeliveryType)
        {
            bool isFound = false;
            if (permitDeliveryTypeEnabledChoice != null && permitDeliveryTypeEnabledChoice.Count > 0)
            {
                isFound = permitDeliveryTypeEnabledChoice.Where(p => p == permitDeliveryType).Any();
            }
            return isFound;
        }

        public static string GetUIClassPaymentMethod(bool isFound)
        {
            string allowOrDisabledBillPayment = "disabled";
            if (isFound)
            {
                allowOrDisabledBillPayment = "allowed";
            }
            return allowOrDisabledBillPayment;
        }
        public static string GetFullNameByUserUID(string uid)
        {
            if (uid == null)
            {
                return "เจ้าหน้าที่รัฐ";
            }
            ApplicationDbContext db;
            string fullName = "";
            try
            {
                db = new ApplicationDbContext();
                var user = db.Users.Where(o => o.Id == uid).SingleOrDefault();
                if (user != null)
                {
                    fullName = user.FullName;
                }
                return fullName;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("GetFullNameByUserGUID {0}", ex.Message));
            }
            finally
            {
                db = null;
            }
        }

        public static bool IsSubMitForUpdateAppRequestNumberOnly(bool IsRequestNumberAgent)
        {
            bool requestUpdateAppNumberAgentObly = IsRequestNumberAgent == true;
            return requestUpdateAppNumberAgentObly;
        }

        public static ApplicationRequestEntity UpdateApplicationRequestNumberAgent(ApplicationRequestEntity request, ApplicationRequestViewModel model, ref ResponseData<object> response)
        {
            if (model.ReplyFromOrg)
            {
                if (model.IsRequestNumberAgent == true)
                {
                    string text = null;
                    if (!string.IsNullOrEmpty(model.ApplicationRequestNumberAgent))
                    {
                        text = model.ApplicationRequestNumberAgent.Trim();
                    }

                    if (request.ApplicationRequestNumberAgent != text)
                    {
                        request.ApplicationRequestNumberAgent = text;
                        request.Update();
                    }
                    response.Type = ResultDataType.Success;
                    response.Message = Resources.ApplicationStatusRequests.MSG_UPDATE_APP_REQ_NUMBER_AGENT_SUCCESS;
                }
            }
            return request;
        }

        public static bool IsEmptyDataAppHooks(FormSectionGroup[] sectionGroups, FormSection[] sections, FormSectionRow[] sectionRows)
        {
            bool isEmpty = false;

            bool isSectionGroupsEmpty = sectionGroups == null || sectionGroups.Count() <= 0;
            bool isSectionsEmpty = sections == null || sections.Count() <= 0;
            bool isSectionRowsEmpty = sectionRows == null || sectionRows.Count() <= 0;
            if (isSectionGroupsEmpty == true &&
                isSectionsEmpty == true &&
                isSectionRowsEmpty == true)
            {
                isEmpty = true;
            }

            return isEmpty;
        }
    }
}