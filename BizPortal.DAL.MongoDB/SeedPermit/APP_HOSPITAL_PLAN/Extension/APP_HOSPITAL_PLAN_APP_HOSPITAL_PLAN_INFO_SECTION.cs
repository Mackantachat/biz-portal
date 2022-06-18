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
    public partial class APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_INFO_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_OTHER()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE__APP_HOSPITAL_PLAN_MED_TYPE_31",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE_OTHER()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE",
                        ControlAnswer = "APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE_3",
                    },
                },
            };
        }
    }
}
