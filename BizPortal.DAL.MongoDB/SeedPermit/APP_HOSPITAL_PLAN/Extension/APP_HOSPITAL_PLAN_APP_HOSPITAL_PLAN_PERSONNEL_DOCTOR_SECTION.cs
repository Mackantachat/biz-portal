using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PLAN
{
    public partial class APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_OTHER()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_POSITION",
                        ControlAnswer = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_POSITION_OTHER",
                    },
                },
            };
        }

        private static Select2Opt[] DROPDOWN_APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }
    }
}
