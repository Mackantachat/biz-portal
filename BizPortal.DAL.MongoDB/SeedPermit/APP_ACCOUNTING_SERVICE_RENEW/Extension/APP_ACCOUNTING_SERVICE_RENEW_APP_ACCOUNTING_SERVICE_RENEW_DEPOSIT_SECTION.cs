using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_RENEW
{
    public partial class APP_ACCOUNTING_SERVICE_RENEW_APP_ACCOUNTING_SERVICE_RENEW_DEPOSIT_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ACCOUNTING_SERVICE_RENEW_DEPOSIT_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_RENEW_PROVIDING_SECTION_PROVIDING_TYPE",
                        ControlAnswer = "DEPOSIT",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_ACCOUNTING_SERVICE_RENEW_DEPOSIT_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_RENEW_PROVIDING_SECTION_PROVIDING_TYPE",
                        ControlAnswer = "DEPOSIT",
                        IsNotEquals = true,
                    },
                },
            };
        }

        private static Select2Opt[] DROPDOWN_APP_ACCOUNTING_SERVICE_RENEW_DEPOSIT_SECTION_BANK_NAME()
        {
            return FormSectionRow.optTFACBank;
        }

    }
}
