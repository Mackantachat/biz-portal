using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_RENEW
{
    public partial class APP_ENERGY_PRODUCTION_RENEW_APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION
    {
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_KVA()
        {
            return new FormControl.NumberSettings
            {
                Min = "1",
                Max = int.MaxValue.ToString(),
                Step = "0.01"
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_VOLTAGE()
        {
            return new FormControl.NumberSettings
            {
                Min = "1",
                Max = int.MaxValue.ToString(),
                Step = "0.01"
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_AMP()
        {
            return new FormControl.NumberSettings
            {
                Min = "1",
                Max = int.MaxValue.ToString(),
                Step = "0.01"
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_PERCENT()
        {
            return new FormControl.NumberSettings
            {
                Min = "1",
                Max = int.MaxValue.ToString(),
                Step = "0.01"
            };
        }
    }
}
