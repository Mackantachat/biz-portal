using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC
{
    public partial class APP_CLINIC_APP_CLINIC_INFO_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_CLINIC_INFO_SECTION_TYPE_MEDICINE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_INFO_SECTION_TYPE",
                        ControlAnswer = "MEDICINE_CLINIC",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_INFO_SECTION_OTHER()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                SubDisplayConditions = new FormControlDisplayCondition[]
                {
                    new FormControlDisplayCondition
                    {
                        Conditions =  new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_INFO_SECTION_TYPE",
                                ControlAnswer = "UNITED_CLINIC",
                            },
                        },
                    },
                    new FormControlDisplayCondition
                    {
                        IsOr = false,
                        Conditions =  new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_INFO_SECTION_TYPE",
                                ControlAnswer = "MEDICINE_CLINIC",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_INFO_SECTION_TYPE_MEDICINE",
                                ControlAnswer = "OTHER",
                            },
                        },
                    },
                },
            };
        }
    }
}
