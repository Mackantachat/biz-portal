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
    public partial class APP_ACCOUNTING_SERVICE_EDIT_APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_CHECKED_AUDITOR_EDIT_CHECKED__EDIT_AUDITOR_SECTION",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControlDisableCondition DISABLE_APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_CHECKED_AUDITOR_EDIT_CHECKED__EDIT_AUDITOR_SECTION",
                        ControlAnswer = "false",
                    },
                },
            };
        }
        private static Select2Opt[] DROPDOWN_APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }
    }
}
