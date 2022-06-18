using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_EDIT
{
    public partial class APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_OPERATING_DATE_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_EDIT_OPERATING_DATE_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_OPERATOR",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_HOSPITAL_PERMISSION_EDIT_OPERATING_DATE_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_OPERATOR",
                        ControlAnswer = "false",
                    },
                },
            };
        }

        private static Select2Opt[] DROPDOWN_APP_HOSPITAL_PERMISSION_EDIT_OPERATING_DATE_SECTION_START_TIME()
        {
            return FormSectionRow.optWorkingTime;
        }

        private static Select2Opt[] DROPDOWN_APP_HOSPITAL_PERMISSION_EDIT_OPERATING_DATE_SECTION_END_TIME()
        {
            return FormSectionRow.optHospitalWorkingTime;
        }
    }
}
