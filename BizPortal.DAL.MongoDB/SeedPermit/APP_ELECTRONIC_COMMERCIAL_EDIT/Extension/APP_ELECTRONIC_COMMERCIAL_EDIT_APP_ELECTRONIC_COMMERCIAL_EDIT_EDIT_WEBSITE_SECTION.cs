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
    public partial class APP_ELECTRONIC_COMMERCIAL_EDIT_APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_WEBSITE_SECTION
    {
        private static FormControlDisplayCondition.ControlWithAnswer IS_COMMERCIAL_ELECTRONIC()
        {
            return new FormControlDisplayCondition.ControlWithAnswer
            {
                ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE",
                ControlAnswer = "ELECTRONIC",
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_WEBSITE_SECTION()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INFO_SECTION_CHECKBOX_EDIT__EDIT_INFO_SECTION_CHECKED",
                        ControlAnswer = "true",
                    },
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_WEBSITE_SECTION()
        {
            return new FormControlDisableCondition
            {
                IsOr = false,
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INFO_SECTION_CHECKBOX_EDIT__EDIT_INFO_SECTION_CHECKED",
                        ControlAnswer = "false",
                    },
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }
    }
}
