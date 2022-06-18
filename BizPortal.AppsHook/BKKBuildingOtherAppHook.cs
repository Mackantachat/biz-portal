using System;
using System.Linq;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;

namespace BizPortal.AppsHook
{
    public class BKKBuildingOtherAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            decimal fee = 0;
            var sec = sectionData.Where(x => x.SectionName == "APP_BUILDING_BUILDING_SECTION").FirstOrDefault();
            if (sec != null)
            {
                if (sec.FormData.TryGetString("APP_BUILDING_BUILDING_SECTION_TYPE_OPTION", "") == "APP_BUILDING_BUILDING_SECTION_TYPE_BUILDING")
                {
                    fee += 20;
                }
                else if (sec.FormData.TryGetString("APP_BUILDING_BUILDING_SECTION_TYPE_OPTION", "") == "APP_BUILDING_BUILDING_SECTION_TYPE_MODIFY")
                {
                    fee += 10;
                }
                else if (sec.FormData.TryGetString("APP_BUILDING_BUILDING_SECTION_TYPE_OPTION", "") == "APP_BUILDING_BUILDING_SECTION_TYPE_DISMANTLE")
                {
                    fee += 10;
                }
            }
            return fee;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
    }
}
