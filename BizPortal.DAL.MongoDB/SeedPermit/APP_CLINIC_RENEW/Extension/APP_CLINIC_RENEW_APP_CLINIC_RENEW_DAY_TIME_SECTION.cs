using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_RENEW
{
    public partial class APP_CLINIC_RENEW_APP_CLINIC_RENEW_DAY_TIME_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_CLINIC_RENEW_DAY_TIME_SECTION()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_RENEW_INFO_SECTION_PURPOSE__EMPLOYEE",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static Select2Opt[] DROPDOWN_APP_CLINIC_RENEW_DAY_TIME_SECTION_OPENING_TIME()
        {
            return FormSectionRow.optWorkingTime;
        }
        private static Select2Opt[] DROPDOWN_APP_CLINIC_RENEW_DAY_TIME_SECTION_CLOSING_TIME()
        {
            return FormSectionRow.optHospitalWorkingTime;
        }
    }
}
