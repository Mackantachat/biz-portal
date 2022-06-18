using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_LAW_OFFICE_EDIT
{
    public partial class APP_LAW_OFFICE_EDIT_APP_LAW_OFFICE_EDIT_LAWYER_JOIN_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_LAW_OFFICE_EDIT_LAWYER_JOIN_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_LAW_OFFICE_EDIT_LAWYER_JOIN_HEADER_SECTION_CHECKBOX__ADD_LAWYER_JOIN",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_LAW_OFFICE_EDIT_LAWYER_JOIN_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_LAW_OFFICE_EDIT_LAWYER_JOIN_HEADER_SECTION_CHECKBOX__ADD_LAWYER_JOIN",
                        ControlAnswer = "false",
                    },
                },
            };
        }

        private static Select2Opt[] DROPDOWN_APP_LAW_OFFICE_EDIT_LAWYER_JOIN_SECTION_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }
    }
}
