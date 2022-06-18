using System;
using System.Linq;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;
using static BizPortal.Utils.Helpers.iTextPDFFormFieldsHelper;
using BizPortal.Utils.Helpers;
namespace BizPortal.AppsHook
{
    public class DOCPKaraokeAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            decimal fee = 0;
            var sec = sectionData.Where(x => x.SectionName == "APP_KARAOKE_INFO").FirstOrDefault();
            if (sec != null)
            {
                var area = int.Parse(sec.FormData["APP_KARAOKE_STORE_AREA"].ToString());
                if (area < 10)
                {
                    fee += 500;
                }
                else if (area > 9 && area < 26)
                {
                    fee += 1000;
                }
                else if (area > 25 && area < 51)
                {
                    fee += 2000;
                }
                else if (area > 50)
                {
                    fee += 3000;
                }
            }
            return fee;
        }

        public override bool IsEnabledChat()
        {
            return false;
        }
    }
}
