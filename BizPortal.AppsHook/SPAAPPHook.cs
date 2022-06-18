using System;
using System.Linq;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;

namespace BizPortal.AppsHook
{
    public class SPAAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            decimal feePerYear = 1000;
            decimal fee = 0 + feePerYear;
            var sec = sectionData.Where(x => x.SectionName == "APP_HEALTH_SECTION").FirstOrDefault();
            if (sec != null)
            {
                var area = Convert.ToDecimal(sec.FormData["APP_HEALTH_CARE_SPA_AREA"].ToString());
                if (area <= 100)
                {
                    fee += 1000;
                }
                else if (area > 100 && area <= 200)
                {
                    fee += 3000;
                }
                else if (area > 200 && area <= 400)
                {
                    fee += 6000;
                }
                else if (area > 400)
                {
                    fee += 10000;
                }
            }
            return fee;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
    }

    public class SPAAppHookEditOnly : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 300;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
    }

    public class SPAAppHookCancelOnly : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
    }
}
