using BizPortal.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.SeedPermit.APP_CLINIC_RENEW
{
    public partial class APP_CLINIC_RENEW_APP_CLINIC_RENEW_WOKING_STATUS_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION()
        {
            return new FormControlDisplayCondition
            {
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

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                        ControlAnswer = "OPARETION_RENEW",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                        ControlAnswer = "OPARETION_RENEW",
                    },
                    //new FormControlDisplayCondition.ControlWithAnswer
                    //{
                    //    ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                    //    ControlAnswer = "CLINIC_RENEW",
                    //},
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_CHOICE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                        ControlAnswer = "OPARETION_RENEW",
                    },
                    //new FormControlDisplayCondition.ControlWithAnswer
                    //{
                    //    ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                    //    ControlAnswer = "CLINIC_RENEW",
                    //},
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL",
                        ControlAnswer = "MEDICINE_CLINIC",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_OTHER_TEXT()
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
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                                ControlAnswer = "OPARETION_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                                ControlAnswer = "CLINIC_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL",
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
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                                ControlAnswer = "OPARETION_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                                ControlAnswer = "CLINIC_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL",
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
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                                ControlAnswer = "OPARETION_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                                ControlAnswer = "CLINIC_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL",
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
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                                ControlAnswer = "OPARETION_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                                ControlAnswer = "CLINIC_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL",
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
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                                ControlAnswer = "OPARETION_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                                ControlAnswer = "CLINIC_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL",
                                ControlAnswer = "MEDICINE_CLINIC",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_CHOICE",
                                ControlAnswer = "OTHER",
                            },
                        },
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_DETAIL()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                        ControlAnswer = "OPARETION_RENEW",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                        ControlAnswer = "HOSPITAL_RENEW",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_CHOICE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                        ControlAnswer = "OPARETION_RENEW",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                        ControlAnswer = "HOSPITAL_RENEW",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_DETAIL",
                        ControlAnswer = "SPECIFIC_PATIENT",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_OTHER_TEXT()
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
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                                ControlAnswer = "OPARETION_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                                ControlAnswer = "HOSPITAL_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_DETAIL",
                                ControlAnswer = "SPECIFIC_PATIENT",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_CHOICE",
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
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                                ControlAnswer = "OPARETION_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                                ControlAnswer = "HOSPITAL_RENEW",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_DETAIL",
                                ControlAnswer = "SPECIALIZED_OTHER",
                            },
                        },
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_EMPLOYEE_LICENSE_NUMBER()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                        ControlAnswer = "OPARETION_RENEW",
                    },
                },
            };
        }

    }
}

