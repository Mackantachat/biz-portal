using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PLAN
{
    public partial class APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION
    {
        private static FormControl.NumberSettings SETTING_NUMBER_APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_PERCENT()
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
