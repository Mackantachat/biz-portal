using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using BizPortal.ViewModels.Apps.HSSStandard;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.AppsHook
{
    public class RDSoftwareHouseRenewAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }

        public override bool IsEnabledExportData(ApplicationStatusV2Enum status)
        {
            var canExportStatus = new ApplicationStatusV2Enum[]
            {
                ApplicationStatusV2Enum.PENDING
            };

            return canExportStatus.Contains(status);
        }

        public override string GenerateRequestData(Guid applicationrequestId)
        {
            var request = ApplicationRequestEntity.Get(applicationrequestId);
            var exportRequests = new List<RDSoftwareHouseRenewViewModel>();
            var generalInfomation = request.Data.TryGetData("GENERAL_INFORMATION");
            var storeInformation = request.Data.TryGetData("INFORMATION_STORE");
            var renew = request.Data.TryGetData("SOFTWARE_HOUSE_BUSSINESS_INFO_RENEW");
            var renew_arrs = request.Data.TryGetData("SOFTWARE_HOUSE_BUSSINESS_INFO_RENEW_ARR").ThenGetArrayData();

            var count = 1;
            foreach (var renew_arr in renew_arrs)
            {
                exportRequests.Add(new RDSoftwareHouseRenewViewModel
                {
                    APPLICATIONREQUESTNUMBER = request.ApplicationRequestNumber,
                    CREATEDDATE = request.CreatedDate.ToString(),
                    branobuy = storeInformation.ThenGetStringData("INFORMATION_STORE_NAME_TH"),
                    datesale = renew_arr.GetValue("SOFTWARE_PUBLISH_DATE"),
                    Idbuy = renew_arr.GetValue("SOFTWARE_PUBLISH_ORDERING_NO"),
                    IDRDSW = renew.ThenGetStringData("SOFTWARE_HOUSE_ID"),
                    offcd = "",
                    rectimestamp ="",
                    remark = renew_arr.GetValue("SOLD_SOFTWARE_REMARK"),
                    req = count.ToString(),
                    swname = renew_arr.GetValue("SOLD_SOFTWARE_NAME"),
                    swversion = "", // renew_arr.GetValue("SOLD_SOFTWARE_SYSTEM_NAME"),
                    tinbuy = renew_arr.GetValue("TAX_PAYER_NO"),
                    tinsale = generalInfomation.ThenGetStringData("IDENTITY_ID"),
                    typebuy = renew_arr.GetValue("DROPDOWN_SOLD_SOFTWARE_TYPE_TEXT"),
                    year = renew_arr.GetValue("SOFTWARE_PUBLISH_DATE").Split('/').LastOrDefault(),
                    Detail = new RDSoftwareHouseRenewDetailViewModel 
                    {
                        datesale = renew_arr.GetValue("SOFTWARE_PUBLISH_DATE"),
                        Idbuy = renew_arr.GetValue("SOFTWARE_PUBLISH_ORDERING_NO"),
                        idmodule = count.ToString(),
                        IDRDSW = renew.ThenGetStringData("SOFTWARE_HOUSE_ID"),
                        offcd = "",
                        rectimestamp = "",
                        req = count.ToString(),
                        swname = renew_arr.GetValue("SOLD_SOFTWARE_NAME"),
                        swversion = "", // renew_arr.GetValue("SOLD_SOFTWARE_SYSTEM_NAME"),
                        tinbuy = renew_arr.GetValue("TAX_PAYER_NO"),
                        tinsale = generalInfomation.ThenGetStringData("IDENTITY_ID"),
                        year = renew_arr.GetValue("SOFTWARE_PUBLISH_DATE").Split('/').LastOrDefault(),
                    }
                });

                count++;
            }

            return JsonConvert.SerializeObject(exportRequests, Formatting.Indented);
        }

        public override bool InvokeSingleForm(Guid trid, string currentSectionGroup, ref SingleFormRequestViewModel model)
        {
            var result = false;
            var darftData = SingleFormRequestEntity.Get(model.IdentityID);
            if (darftData.SectionData.Any(e => e.SectionName == "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL"))
            {
                var mudulenbr = GetSectionData(model, "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE", SectionType.Form);
                var muduleinfo = GetSectionData(model, "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL", SectionType.Form);
                muduleinfo.FormData = new Dictionary<string, object>
                {
                    { "SOFTWARE_TOTAL_MODULE", mudulenbr.ArrayData.Count},
                };

            }

            result = true;

            return result;
        }
    }

}

