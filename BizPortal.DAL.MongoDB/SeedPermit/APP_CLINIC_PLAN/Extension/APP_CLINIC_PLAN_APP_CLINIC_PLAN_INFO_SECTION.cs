using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_PLAN
{
    public partial class APP_CLINIC_PLAN_APP_CLINIC_PLAN_INFO_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_CLINIC_PLAN_INFO_SECTION_SERVICES_OTHER_TEXT()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_PLAN_INFO_SECTION_SERVICES__OTHER",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_CLINIC_PLAN_INFO_SECTION_TYPE_OTHER()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_PLAN_INFO_SECTION_TYPE",
                        ControlAnswer = "OTHER",
                    },
                },
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_CLINIC_PLAN_INFO_SECTION_WIDTH()
        {
            return new FormControl.NumberSettings
            {
                Min = "1",
                Max = int.MaxValue.ToString(),
                Step = "0.01"
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_CLINIC_PLAN_INFO_SECTION_LENGTH()
        {
            return new FormControl.NumberSettings
            {
                Min = "1",
                Max = int.MaxValue.ToString(),
                Step = "0.01"
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_CLINIC_PLAN_INFO_SECTION_HIGH()
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
