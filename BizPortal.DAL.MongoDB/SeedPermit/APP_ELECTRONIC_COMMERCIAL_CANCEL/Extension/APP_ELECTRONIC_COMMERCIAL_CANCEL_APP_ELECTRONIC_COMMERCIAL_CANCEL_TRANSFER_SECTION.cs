using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_CANCEL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_CANCEL_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION
    {
        private static FormControlDisplayCondition IS_TRANSFER()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_TYPE_SECTION_IS_TRANSFER",
                        ControlAnswer = "YES",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION()
        {
            return IS_TRANSFER();
        }

        private static FormControlDisableCondition DISABLE_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_TYPE_SECTION_IS_TRANSFER",
                        ControlAnswer = "NO",
                    }
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_COMMERCIAL_NUMBER()
        {
            return IS_TRANSFER();
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_REQUEST_NUMBER()
        {
            return IS_TRANSFER();
        }

        private static FormControl CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_SEARCH()
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
                    AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_CANCEL,
                },
                DisplayReadonly = false,
                HideLabel = true,
                LiteralContent = "<label>&nbsp;</label><button name=\"btnCheckCommerce\" type=\"button\" class=\"btn btn-primary\">ค้นหาข้อมูล</button>"
            };
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_TITLE()
        {
            return IS_TRANSFER();
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_FIRSTNAME()
        {
            return IS_TRANSFER();
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_LASTNAME()
        {
            return IS_TRANSFER();
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ID_CARD()
        {
            return IS_TRANSFER();
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_NATIONALITY()
        {
            return FormSectionRow.optNationality;
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_NATIONALITY()
        {
            return IS_TRANSFER();
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_COMMERCIAL_NAME()
        {
            return IS_TRANSFER();
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_DATE()
        {
            return IS_TRANSFER();
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS()
        {
            return IS_TRANSFER();
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_CAUSE()
        {
            return IS_TRANSFER();
        }
    }
}
