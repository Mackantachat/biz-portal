using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_RENEW
{
    public partial class APP_ENERGY_PRODUCTION_RENEW_APP_ENERGY_PRODUCTION_RENEW_MACHINE_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ENERGY_PRODUCTION_RENEW_MACHINE_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION_TYPE__GENERAL",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControlDisableCondition DISABLE_APP_ENERGY_PRODUCTION_RENEW_MACHINE_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION_TYPE__GENERAL",
                        ControlAnswer = "false",
                    },
                },
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ENERGY_PRODUCTION_RENEW_MACHINE_SECTION_HORSEPOWER()
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
