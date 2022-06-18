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
    public partial class APP_ACCOUNTING_SERVICE_EDIT_APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION_CHECKED_NEW_CERTIFICATE_CHECKED__EDIT_CERTIFICATE",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControlDisableCondition DISABLE_APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION_CHECKED_NEW_CERTIFICATE_CHECKED__EDIT_CERTIFICATE",
                        ControlAnswer = "false",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION_REASON_FOR_ISSUING()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION_CHECKED",
                        ControlAnswer = "true",
                    },
                },
            };
        }
    }
}
