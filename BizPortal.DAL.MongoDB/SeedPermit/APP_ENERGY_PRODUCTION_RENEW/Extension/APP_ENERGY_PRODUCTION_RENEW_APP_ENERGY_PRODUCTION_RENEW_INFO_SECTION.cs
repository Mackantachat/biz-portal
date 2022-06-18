using BizPortal.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_RENEW
{
    public partial class APP_ENERGY_PRODUCTION_RENEW_APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION_REASON()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION_EDIT_INFO_SECTION__EDIT_INFO",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION_TYPE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION_EDIT_INFO_SECTION__EDIT_INFO",
                        ControlAnswer = "true",
                    },
                },
            };
        }
    }
}
