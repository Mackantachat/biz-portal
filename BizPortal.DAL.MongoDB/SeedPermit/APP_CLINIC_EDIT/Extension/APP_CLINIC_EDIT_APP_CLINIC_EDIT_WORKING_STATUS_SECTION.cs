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
    public partial class APP_CLINIC_EDIT_APP_CLINIC_EDIT_WORKING_STATUS_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_OPERATOR",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_CLINIC_EDIT_WORKING_STATUS_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_OPERATOR",
                        ControlAnswer = "false",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                        ControlAnswer = "OPERATOR",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_TYPE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                        ControlAnswer = "OPERATOR",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE",
                        ControlAnswer = "CLINIC",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_MEDICAL_PRACTICE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                        ControlAnswer = "OPERATOR",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE",
                        ControlAnswer = "CLINIC",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_TYPE",
                        ControlAnswer = "MEDICAL_CLINIC",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_OTHER()
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
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                                ControlAnswer = "OPERATOR",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE",
                                ControlAnswer = "CLINIC",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_TYPE",
                                ControlAnswer = "SPECIALIZED_MEDICAL_CLINIC",
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
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                                ControlAnswer = "OPERATOR",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE",
                                ControlAnswer = "CLINIC",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_TYPE",
                                ControlAnswer = "SPECIALIZED_DENTAL_CLINIC",
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
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                                ControlAnswer = "OPERATOR",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE",
                                ControlAnswer = "CLINIC",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_TYPE",
                                ControlAnswer = "SPECIALTY_CLINICS_MIDWIFERY",
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
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                                ControlAnswer = "OPERATOR",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE",
                                ControlAnswer = "CLINIC",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_TYPE",
                                ControlAnswer = "UNITED_CLINIC",
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
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                                ControlAnswer = "OPERATOR",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE",
                                ControlAnswer = "CLINIC",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_TYPE",
                                ControlAnswer = "MEDICAL_CLINIC",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_MEDICAL_PRACTICE",
                                ControlAnswer = "OTHER",
                            },
                        },
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_TYPE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                        ControlAnswer = "OPERATOR",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE",
                        ControlAnswer = "HOSPITAL",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_MEDICAL_PRACTICE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                        ControlAnswer = "OPERATOR",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE",
                        ControlAnswer = "HOSPITAL",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_TYPE",
                        ControlAnswer = "SPECIFIC_PATIENT",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_OTHER()
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
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                                ControlAnswer = "OPERATOR",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE",
                                ControlAnswer = "HOSPITAL",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_TYPE",
                                ControlAnswer = "SPECIALIZED_OTHER",
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
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                                ControlAnswer = "OPERATOR",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE",
                                ControlAnswer = "HOSPITAL",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_TYPE",
                                ControlAnswer = "SPECIFIC_PATIENT",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_MEDICAL_PRACTICE",
                                ControlAnswer = "OTHER_PATIENTS",
                            },
                        },
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_LICENSE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                        ControlAnswer = "OPERATOR",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_NAME()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_OPERATOR",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_ADDRESS()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_OPERATOR",
                        ControlAnswer = "true",
                    },
                },
            };
        }
    }
}
