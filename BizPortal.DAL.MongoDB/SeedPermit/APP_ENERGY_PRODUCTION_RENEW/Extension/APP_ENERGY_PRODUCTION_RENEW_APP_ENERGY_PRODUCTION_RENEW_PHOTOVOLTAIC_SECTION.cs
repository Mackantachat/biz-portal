using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_RENEW
{
    public partial class APP_ENERGY_PRODUCTION_RENEW_APP_ENERGY_PRODUCTION_RENEW_PHOTOVOLTAIC_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ENERGY_PRODUCTION_RENEW_PHOTOVOLTAIC_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION_TYPE__PHOTOVOLTEIC",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControlDisableCondition DISABLE_APP_ENERGY_PRODUCTION_RENEW_PHOTOVOLTAIC_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION_TYPE__PHOTOVOLTEIC",
                        ControlAnswer = "false",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ENERGY_PRODUCTION_RENEW_PHOTOVOLTAIC_SECTION_OTHER()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ENERGY_PRODUCTION_RENEW_PHOTOVOLTAIC_SECTION_TYPE",
                        ControlAnswer = "3",
                    },
                },
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ENERGY_PRODUCTION_RENEW_PHOTOVOLTAIC_SECTION_WATT()
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
