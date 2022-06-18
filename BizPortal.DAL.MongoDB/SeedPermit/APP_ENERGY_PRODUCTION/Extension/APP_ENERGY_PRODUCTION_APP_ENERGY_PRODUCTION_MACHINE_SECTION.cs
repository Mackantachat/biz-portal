using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION
{
    public partial class APP_ENERGY_PRODUCTION_APP_ENERGY_PRODUCTION_MACHINE_SECTION
    {
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ENERGY_PRODUCTION_MACHINE_SECTION_HORSEPOWER()
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
