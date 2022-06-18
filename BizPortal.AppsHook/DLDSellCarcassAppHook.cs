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
    class DLDSellCarcassAppHook : StoreBaseAppHook
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
                { "more_than_1_province", 240 },
                { "one_province", 50 },
                { "import", 1000 },
                { "export", 1000 },
                { "import_export", 1250 },
            };
            decimal fee = 0;
            var sellCarcassSection = sectionData.Where(x => x.SectionName == "APP_SELL_CARCASS_INFO2").Single();
            var item = sellCarcassSection.FormData;
            if (item.TryGetString("SELL_CARCASS_PURPOSE_SELL_CARCASS_PURPOSE_DOMESTIC", "false") == "true")
            {
                var domesticType = item.TryGetString("SELL_CARCASS_PURPOSE_DETAIL_DOMESTIC_OPTION", "");
                if (feeDict.ContainsKey(domesticType))
                {
                    fee += feeDict[domesticType];
                    feeDict.Remove(domesticType);
                }
                fee += 50;
            }
            if (item.TryGetString("SELL_CARCASS_PURPOSE_SELL_CARCASS_PURPOSE_INTERNATIONAL", "false") == "true")
            {
                var interType = item.TryGetString("SELL_CARCASS_PURPOSE_DETAIL_INTERNATIONAL_OPTION", "");
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
