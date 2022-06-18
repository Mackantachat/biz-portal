using System;
using System.Linq;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;

namespace BizPortal.AppsHook
{
    public class BKKDangerFoodRenewAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return null;
        }

        public override string PrintFormHeaderRight
        {
            get
            {
                return "แบบ อภ.3";
            }
        }

        public override string PrintFormTitle
        {
            get
            {
                return "คำขอต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ";
            }
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
    }
}
