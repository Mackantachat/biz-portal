using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_EDIT
{
    public partial class APP_ACCOUNTING_SERVICE_EDIT_APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_AMOUNT_BATH()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_REGISTERED_CHECKED__EDIT_AMOUNT_REGISTERED",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_AMOUNT_BATH()
        {
            return new FormControl.NumberSettings
            {
                Min = "0",
                Max = long.MaxValue.ToString(),
                Step = "0.01"
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_SERVICE_TYPE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_SERVICE_TYPE_CHECKED__SERVICE_TYPE_EDIT",
                        ControlAnswer = "true",
                    },
                },
            };
        }
    }
}
