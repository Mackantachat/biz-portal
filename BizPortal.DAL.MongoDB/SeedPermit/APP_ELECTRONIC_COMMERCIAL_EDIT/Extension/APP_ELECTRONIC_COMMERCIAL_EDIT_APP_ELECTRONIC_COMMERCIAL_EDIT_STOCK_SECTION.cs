using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_EDIT
{
    public partial class APP_ELECTRONIC_COMMERCIAL_EDIT_APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_STOCK_SECTION_CHECKBOX_EDIT__EDIT_STOCK_SECTION_CHECKED",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_STOCK_SECTION_CHECKBOX_EDIT__EDIT_STOCK_SECTION_CHECKED",
                        ControlAnswer = "false",
                    },
                },
            };
        }

        private static FormControl.NumberSettings SETTING_NUMBER_APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_REGISTERED_CAPITAL()
        {
            return new FormControl.NumberSettings()
            {
                Min = "1",
                Max = int.MaxValue.ToString(),
                Step = "0.1"
            };
        }

        private static FormControl.NumberSettings SETTING_NUMBER_APP_ELECTRONIC_COMMERCIAL_EDIT_STOCK_SECTION_SEPERATED_TO()
        {
            return new FormControl.NumberSettings()
            {
                Min = "1",
                Max = int.MaxValue.ToString(),
                Step = "0.1"
            };
        }
    }
}
