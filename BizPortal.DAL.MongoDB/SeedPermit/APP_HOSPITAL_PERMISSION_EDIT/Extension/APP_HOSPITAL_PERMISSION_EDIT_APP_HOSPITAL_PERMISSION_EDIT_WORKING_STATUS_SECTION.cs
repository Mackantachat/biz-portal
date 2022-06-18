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
    public partial class APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION()
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

        private static FormControlDisableCondition DISABLE_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION()
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

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_TYPE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_STATUS",
                        ControlAnswer = "OPERATOR",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_CLINIC_TYPE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_STATUS",
                        ControlAnswer = "OPERATOR",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_TYPE",
                        ControlAnswer = "CLINIC",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_HOSPITAL_LICENSE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_STATUS",
                        ControlAnswer = "OPERATOR",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_TYPE",
                        ControlAnswer = "CLINIC",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_HOSPITAL_NAME()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_STATUS",
                        ControlAnswer = "OPERATOR",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_STATUS",
                        ControlAnswer = "PROFESSIONAL",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_ADDRESS()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_STATUS",
                        ControlAnswer = "OPERATOR",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_STATUS",
                        ControlAnswer = "PROFESSIONAL",
                    },
                },
            };
        }
    }
}
