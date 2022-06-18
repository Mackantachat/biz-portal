using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_RENEW
{
    public partial class APP_HOSPITAL_PERMISSION_RENEW_APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_RENEW_INFO_SECTION_PURPOSE__EMPLOYEE",
                        ControlAnswer = "true",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                        ControlAnswer = "EMPLOYEE_RENEW",
                    },
                },
            };
        }
        private static Select2Opt[] DROPDOWN_APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION_OPENING_TIME()
        {
            return FormSectionRow.optWorkingTime;
        }
        private static Select2Opt[] DROPDOWN_APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION_CLOSING_TIME()
        {
            return FormSectionRow.optHospitalWorkingTime;
        }
    }
}
