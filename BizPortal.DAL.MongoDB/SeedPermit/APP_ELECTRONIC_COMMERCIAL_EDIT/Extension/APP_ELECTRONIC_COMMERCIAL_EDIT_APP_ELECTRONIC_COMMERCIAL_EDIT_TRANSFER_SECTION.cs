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
    public partial class APP_ELECTRONIC_COMMERCIAL_EDIT_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_TRANSFER_SECTION_CHECKBOX_EDIT__EDIT_TRANSFER_SECTION_CHECKED",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_TRANSFER_SECTION_CHECKBOX_EDIT__EDIT_TRANSFER_SECTION_CHECKED",
                        ControlAnswer = "false",
                    },
                },
            };
        }

        private static FormControl CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_SEARCH()
        {
            return new FormControl()
            {
                FieldID = "F37_03_22",
                Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_SEARCH",
                Type = ControlType.Literal,
                DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_SEARCH",
                ColFixed = 2,
                DisplayReadonly = false,
                HideLabel = true,
                LiteralContent = "<label>&nbsp;</label><button name=\"APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_SEARCH\" type=\"button\" class=\"btn btn-primary\">ค้นหาข้อมูล</button>"
            };
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_TRANSFER_SECTION_NATIONALITY()
        {
            return FormSectionRow.optNationality;
        }
    }
}
