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
    public partial class APP_CLINIC_EDIT_APP_CLINIC_EDIT_WORKING_DAY_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_WORKING_DAY_SECTION()
        {
            return new FormControlDisplayCondition
            {
                //IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    //new FormControlDisplayCondition.ControlWithAnswer
                    //{
                    //    ControlName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_DETAIL",
                    //    ControlAnswer = "true",
                    //},
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_2_REQUEST_CHANGE__REQUEST_CHANGE_WORKING_DATE",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_CLINIC_EDIT_WORKING_DAY_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_2_REQUEST_CHANGE__REQUEST_CHANGE_WORKING_DATE",
                        ControlAnswer = "false",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_WORKING_DAY_SECTION_CUS()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_2_REQUEST_CHANGE__REQUEST_CHANGE_WORKING_DATE",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static Select2Opt[] DROPDOWN_APP_CLINIC_EDIT_WORKING_DAY_SECTION_START_TIME()
        {
            return FormSectionRow.optWorkingTime;
        }

        private static Select2Opt[] DROPDOWN_APP_CLINIC_EDIT_WORKING_DAY_SECTION_END_TIME()
        {
            return FormSectionRow.optHospitalWorkingTime;
        }
    }
}
