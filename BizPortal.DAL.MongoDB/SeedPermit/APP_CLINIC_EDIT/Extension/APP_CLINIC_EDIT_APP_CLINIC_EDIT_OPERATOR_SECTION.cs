using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_EDIT
{
    public partial class APP_CLINIC_EDIT_APP_CLINIC_EDIT_OPERATOR_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_OPERATOR_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_OPERATOR",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_CLINIC_EDIT_OPERATOR_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_OPERATOR",
                        ControlAnswer = "false",
                    },
                },
            };
        }

        private static Select2Opt[] DROPDOWN_APP_CLINIC_EDIT_OPERATOR_SECTION_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }

        private static Select2Opt[] DROPDOWN_APP_CLINIC_EDIT_OPERATOR_SECTION_NATIONALITY()
        {
            return FormSectionRow.optNationality;
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_OPERATOR_SECTION_ID_CARD()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_OPERATOR_SECTION_NATIONALITY",
                        ControlAnswer = "TH",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_OPERATOR_SECTION_PASSPORT()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_OPERATOR_SECTION_NATIONALITY",
                        ControlAnswer = "TH",
                        IsNotEquals = true,
                    },
                },
            };
        }
    }
}
