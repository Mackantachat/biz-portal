using BizPortal.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.SeedPermit.APP_CLINIC_RENEW
{
    public partial class APP_CLINIC_RENEW_APP_CLINIC_RENEW_RENEW_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_CLINIC_RENEW_RENEW_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_RENEW_INFO_SECTION_PURPOSE__OPARETION",
                        ControlAnswer = "true",
                    },
                },
            };
        }
    }
}
