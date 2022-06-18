using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE
{
    public partial class APP_ACCOUNTING_SERVICE_APP_ACCOUNTING_SERVICE_INFO_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ACCOUNTING_SERVICE_INFO_SECTION_REGISTER_DATE_OPTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_INFO_SECTION_OPTION",
                        ControlAnswer = "REGISTER_DATE",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ACCOUNTING_SERVICE_INFO_SECTION_START_DATE_OPTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_INFO_SECTION_OPTION",
                        ControlAnswer = "START_DATE",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ACCOUNTING_SERVICE_INFO_SECTION_CHANGE_DATE_OPTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_INFO_SECTION_OPTION",
                        ControlAnswer = "CHANGE_DATE",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ACCOUNTING_SERVICE_INFO_SECTION_OTHER_DATE_OPTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_INFO_SECTION_OPTION",
                        ControlAnswer = "OTHER_DATE",
                    },
                },
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ACCOUNTING_SERVICE_INFO_SECTION_REGISTER_CAPITAL()
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
