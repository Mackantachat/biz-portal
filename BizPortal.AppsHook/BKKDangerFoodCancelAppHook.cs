using System;
using System.Linq;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;

namespace BizPortal.AppsHook
{
    public class BKKDangerFoodCancelAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return null;
        }

        public override string PrintFormHeaderRight
        {
            get
            {
                return "แบบ อภ.9";
            }
        }

        public override string PrintFormTitle
        {
            get
            {
                return "คำขอยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ";
            }
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
    }
}
