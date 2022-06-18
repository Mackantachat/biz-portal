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
    public partial class APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "SPECIALIZED_MEDICINE",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "SPECIALIZED_DENTISTRY",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "SPECIALIZED_IN_NURSING_AND_MIDWIFE",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "SPECIALIZED_IN_THE_ENT",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "HEART_DISEASE",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "SPECIFIC_TO_CANCER",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "OTHER_SPECIALTY",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "MEDICINE",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "DENTAL",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "NURSING_AND_MIDWIFERY",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "MEDICAL_TECHNIQUE",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "PHYSICAL_THERAPY",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "THAI_TRADITIONAL_MEDICINE",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_BRANCH",
                        ControlAnswer = "HEALING_ARTS_PRACTICE",
                    },
                },
            };
        }
    }
}
