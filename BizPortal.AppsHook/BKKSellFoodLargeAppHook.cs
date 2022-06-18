using System;
using System.Linq;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;

namespace BizPortal.AppsHook
{
    public class BKKSellFoodLargeAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return null;
            //var sec = sectionData.Where(x => x.SectionName == "INFORMATION_STORE").FirstOrDefault();
            //if (sec != null)
            //{
            //    if (sec.FormData.ContainsKey("INFORMATION_STORE_AREA"))
            //    {
            //        var area = int.Parse(sec.FormData["INFORMATION_STORE_AREA"].ToString());
            //        return Math.Min(Math.Max(2000, 2000 + ((area - 300) * 5)), 3000);
            //    }
            //}
            //throw new Exception("ไม่พบข้อมูล 'ข้อมูลร้าน: พื้นที่ใช้สอย (ตารางเมตร)'");
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
    }
}
