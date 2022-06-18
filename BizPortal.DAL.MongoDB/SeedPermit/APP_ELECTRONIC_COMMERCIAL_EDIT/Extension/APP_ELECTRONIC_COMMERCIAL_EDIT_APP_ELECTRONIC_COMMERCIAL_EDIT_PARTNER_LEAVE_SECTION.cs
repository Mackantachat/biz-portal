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
    public partial class APP_ELECTRONIC_COMMERCIAL_EDIT_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_LEAVE_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_LEAVE_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_LEAVE_SECTION_CHECKBOX_EDIT__EDIT_PARTNER_LEAVE_SECTION_CHECKED",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_LEAVE_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_LEAVE_SECTION_CHECKBOX_EDIT__EDIT_PARTNER_LEAVE_SECTION_CHECKED",
                        ControlAnswer = "false",
                    },
                },
            };
        }

        private static string APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_LEAVE_SECTION_TITLE_URL()
        {
            return "~/Api/v2/DBD/NameTitles";
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_LEAVE_SECTION_NATIONALITY()
        {
            return FormSectionRow.optNationality;
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_PARTNER_LEAVE_SECTION_RACE()
        {
            return FormSectionRow.optNation;
        }
    }
}
