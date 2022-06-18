using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_LICENSE
{
    public partial class APP_HOSPITAL_LICENSE_APP_HOSPITAL_LICENSE_INFO_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_CHOICE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_TYPE",
                        ControlAnswer = "SPECIFIC_PATIENT",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_TEXT()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_TYPE",
                        ControlAnswer = "SPECIALIZED_OTHER",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_LICENSE_INFO_SECTION_HOSPITAL_CHOICE",
                        ControlAnswer = "OTHER_PATIENTS",
                    },
                },
            };
        }

    }
}
