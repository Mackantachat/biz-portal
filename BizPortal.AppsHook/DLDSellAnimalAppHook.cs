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
    class DLDSellAnimalAppHook : StoreBaseAppHook
    {
        public override bool PermitCanBeDeliveredOnPayment
        {
            get
            {
                return true;
            }
        }
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            var feeDict = new Dictionary<string, decimal>
            {
                { "more_than_1_province", 1000 },
                { "one_province", 250 },
                { "import", 5000 },
                { "export", 5000 },
                { "import_export", 7500 },
            };
            decimal fee = 0;
            var sellAnimalSection = sectionData.Where(x => x.SectionName == "APP_SELL_ANIMAL_INFO2").Single();
            var item = sellAnimalSection.FormData;
            if (item.TryGetString("SELL_ANIMAL_PURPOSE_SELL_ANIMAL_PURPOSE_DOMESTIC", "false") == "true")
            {
                var domesticType = item.TryGetString("SELL_ANIMAL_PURPOSE_DETAIL_DOMESTIC_OPTION", "");
                if (feeDict.ContainsKey(domesticType))
                {
                    fee += feeDict[domesticType];
                    feeDict.Remove(domesticType);
                }
                fee += 50;
            }
            if (item.TryGetString("SELL_ANIMAL_PURPOSE_SELL_ANIMAL_PURPOSE_INTERNATIONAL", "false") == "true")
            {
                var interType = item.TryGetString("SELL_ANIMAL_PURPOSE_DETAIL_INTERNATIONAL_OPTION", "");
                if (feeDict.ContainsKey(interType))
                {
                    fee += feeDict[interType];
                    feeDict.Remove(interType);
                }
                fee += 50;
            }
            return fee;
            //throw new Exception("ไม่พบข้อมูลการจำหน่ายสุราใน SingleFormRequest");
        }

        public override bool IsEnabledChat()
        {
            return true;
        }

    }
}
