using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;

namespace BizPortal.AppsHook
{
    class DLDSellAnimalFoodAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            var sec = sectionData.Where(x => x.SectionName == "APP_SELL_ANIMAL_FOOD").FirstOrDefault();
            if (sec != null)
            {
                if (sec.FormData.TryGetString("SELL_ANIMAL_FOOD_SELL_TYPE_OPTION", "") == "wholesale")
                {
                    return 300;
                }
            }
            return 100;
            //throw new Exception("ไม่พบข้อมูลการจำหน่ายสุราใน SingleFormRequest");
        }
        public override bool PermitCanBeDeliveredOnPayment
        {
            get
            {
                return true;
            }
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
    }
}
