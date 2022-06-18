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
    public partial class APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION
    {
        private static FormControl.NumberSettings SETTING_NUMBER_APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_DURATION_MONTH()
        {
            return new FormControl.NumberSettings
            {
                Min = "0",
                Max = "11",
                Step = "1"
            };
        }
    }
}
