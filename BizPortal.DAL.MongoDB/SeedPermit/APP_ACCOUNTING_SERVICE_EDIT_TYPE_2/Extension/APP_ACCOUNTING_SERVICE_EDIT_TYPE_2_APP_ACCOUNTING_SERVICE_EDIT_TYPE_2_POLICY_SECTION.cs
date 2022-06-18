using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2
{
    public partial class APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_POLICY_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_POLICY_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_PROVIDING_SECTION_PROVIDING_TYPE",
                        ControlAnswer = "POLICY",
                    },
                },
            };
        }
        private static FormControlDisableCondition DISABLE_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_POLICY_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_PROVIDING_SECTION_PROVIDING_TYPE",
                        ControlAnswer = "POLICY",
                        IsNotEquals = true,
                    },
                },
            };
        }
    }
}
