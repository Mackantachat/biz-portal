using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_TRANSFER_TYPE_SECTION_IS_TRANSFER",
                        ControlAnswer = "YES",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_TRANSFER_TYPE_SECTION_IS_TRANSFER",
                        ControlAnswer = "NO",
                    },
                },
            };
        }

        private static FormControl CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_SEARCH()
        {
            return new FormControl()
            {
                FieldID = "BUTTON_SEARCH",
                Control = "BUTTON_SEARCH",
                Type = ControlType.Literal,
                DataKey = "BUTTON_SEARCH",
                ShowOnSpecificApps = true,
                ColFixed = 2,
                AppSystemNames = new string[]
                {
                    AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                },
                DisplayReadonly = false,
                HideLabel = true,
                LiteralContent = "<label>&nbsp;</label><button name=\"btnCheckCommerce\" type=\"button\" class=\"btn btn-primary\">ค้นหาข้อมูล</button>"
            };
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_TRANSFER_SECTION_NATIONALITY()
        {
            return FormSectionRow.optNationality;
        }
    }
}
