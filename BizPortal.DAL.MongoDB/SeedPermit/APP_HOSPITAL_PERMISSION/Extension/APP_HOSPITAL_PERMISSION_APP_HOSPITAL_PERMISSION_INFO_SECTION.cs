using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION
{
    public partial class APP_HOSPITAL_PERMISSION_APP_HOSPITAL_PERMISSION_INFO_SECTION
    {
        private static FormControl.NumberSettings SETTING_NUMBER_APP_HOSPITAL_PERMISSION_INFO_SECTION_BED_AMOUNT()
        {
            return new FormControl.NumberSettings
            {
                Min = "1",
                Max = Int64.MaxValue.ToString(),
                Step = "1"
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_CHOICE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_DETAIL",
                        ControlAnswer = "SPECIFIC_PATIENT",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_OTHER()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                SubDisplayConditions = new FormControlDisplayCondition[]
                {
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_DETAIL",
                                ControlAnswer = "SPECIFIC_PATIENT",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_CHOICE",
                                ControlAnswer = "OTHER_PATIENTS",
                            },
                        },
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_INFO_SECTION_HOSPITAL_DETAIL",
                                ControlAnswer = "SPECIALIZED_OTHER",
                            },
                        },
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE_OTHER_TEXT()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_INFO_SECTION_ONLINE_SERVICE__OTHER",
                        ControlAnswer = "true",
                    },
                },
            };
        }
    }
}
