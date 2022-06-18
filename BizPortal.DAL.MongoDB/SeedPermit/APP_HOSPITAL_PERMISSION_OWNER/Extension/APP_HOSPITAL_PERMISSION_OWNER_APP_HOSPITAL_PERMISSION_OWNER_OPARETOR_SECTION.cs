using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_OWNER
{
    public partial class APP_HOSPITAL_PERMISSION_OWNER_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION
    {
        private static Select2Opt[] DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }

        private static Select2Opt[] DROPDOWN_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_NATIONALITY()
        {
            return FormSectionRow.optNationality;
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_ID()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_NATIONALITY",
                        ControlAnswer = "TH",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_PASSPORT()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_NATIONALITY",
                        ControlAnswer = "TH",
                        IsNotEquals = true,
                    },
                },
            };
        }

        private static FormControl CUSTOM_FORM_CONTROL_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DIPLOMA()
        {
            return new FormControl
            {
                Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DIPLOMA",
                Type = ControlType.DataTable,
                DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DIPLOMA",
                ColFixed = 12,
                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                DisplayCondition = new FormControlDisplayCondition
                {
                    IsOr = true,
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_BRANCH",
                            ControlAnswer = "SPECIALIZED_MEDICINE",
                        },
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_BRANCH",
                            ControlAnswer = "ONLY_DENTAL",
                        },
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_BRANCH",
                            ControlAnswer = "SPECIALIZED_MIDWIFERY",
                        },
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_BRANCH",
                            ControlAnswer = "ONLY_EAR_NOSE_THROAT",
                        },
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_BRANCH",
                            ControlAnswer = "ONLY_HEART_DISEASE",
                        },
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_BRANCH",
                            ControlAnswer = "ONLY_CANCER",
                        },
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_BRANCH",
                            ControlAnswer = "ONLY_OTHER",
                        },
                    },
                },
                DataTableColumns = new DataTableColumn[]
                {
                    new DataTableColumn()
                    {
                        Name = "DIPLOMA",
                        Title = "ประเภท",
                        CustomColFixed = 2,
                        Control = new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION__DIPLOMA",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION__DIPLOMA",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            WillTriggerDisplayOfOtherUI = true,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "TYPE_1", Text = "วุฒิบัตร " },
                                new Select2Opt() { ID = "TYPE_2", Text = "หนังสืออนุมัติ" },
                                new Select2Opt() { ID = "TYPE_3", Text = "หนังสือรับรอง" },
                            },
                        },
                    },
                    new DataTableColumn()
                    {
                        Name = "BRANCH",
                        Title = "สาขา",
                        CustomColFixed = 2,
                        Control = new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION__BRANCH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION__BRANCH",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                        },
                    },
                    new DataTableColumn()
                    {
                        Name = "NUMBER",
                        Title = "เลขที่",
                        CustomColFixed = 2,
                        Control = new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION__NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION__NUMBER",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                        },
                    },
                    new DataTableColumn()
                    {
                        Name = "ISSUE_DATE",
                        Title = "ออกให้ ณ วันที่",
                        CustomColFixed = 3,
                        Control = new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION__ISSUE_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION__ISSUE_DATE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DataFormat = "dd/MM/yyyy",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties()
                            {
                                EndDate = "0d"
                            },
                        },
                    },
                    new DataTableColumn()
                    {
                        Name = "DUE_DATE",
                        Title = "วันที่หมดอายุ",
                        CustomColFixed = 3,
                        Control = new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION__DUE_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION__DUE_DATE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties()
                            {
                                StartDate = "0d"
                            },
                        },
                    },
                },
                ResourceName = "Apps_SingleForm",
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OPARETOR_STATUS()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                        ControlAnswer = "OPARETOR",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                        ControlAnswer = "EMPLOYEE",
                    },
                },
            };
        }

        private static FormControlDisplayCondition Working()
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
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                                ControlAnswer = "OPARETOR",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OPARETOR_STATUS",
                                ControlAnswer = "TYPE_1",
                            },
                        }
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                                ControlAnswer = "OPARETOR",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OPARETOR_STATUS",
                                ControlAnswer = "TYPE_2",
                            },
                        }
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                                ControlAnswer = "EMPLOYEE",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS",
                                ControlAnswer = "TYPE_3",
                            },
                        }
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                                ControlAnswer = "EMPLOYEE",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS",
                                ControlAnswer = "TYPE_4",
                            },
                        }
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                                ControlAnswer = "EMPLOYEE",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS",
                                ControlAnswer = "TYPE_5",
                            },
                        }
                    },
                },
            };
        }

        private static FormControlDisplayCondition.ControlWithAnswer[] OparetorWorkingClinic()
        {
            return new FormControlDisplayCondition.ControlWithAnswer[]
            {
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                    ControlAnswer = "OPARETOR",
                },
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_TYPE",
                    ControlAnswer = "CLINIC",
                },
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OPARETOR_STATUS",
                    ControlAnswer = "",
                    IsNotEquals = true,
                },
            };
        }

        private static FormControlDisplayCondition.ControlWithAnswer[] EmployeeWorkingClinic()
        {
            return new FormControlDisplayCondition.ControlWithAnswer[]
            {
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                    ControlAnswer = "EMPLOYEE",
                },
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_TYPE",
                    ControlAnswer = "CLINIC",
                },
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS",
                    ControlAnswer = "",
                    IsNotEquals = true
                },
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS",
                    ControlAnswer = "TYPE_1",
                    IsNotEquals = true
                },
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS",
                    ControlAnswer = "TYPE_2",
                    IsNotEquals = true
                },
            };
        }

        private static FormControlDisplayCondition.ControlWithAnswer[] OparetorWorkingHospital()
        {
            return new FormControlDisplayCondition.ControlWithAnswer[]
            {
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                    ControlAnswer = "OPARETOR",
                },
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_TYPE",
                    ControlAnswer = "HOSPITAL",
                },
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OPARETOR_STATUS",
                    ControlAnswer = "",
                    IsNotEquals = true,
                },
            };
        }

        private static FormControlDisplayCondition.ControlWithAnswer[] EmployeeWorkingHostipal()
        {
            return new FormControlDisplayCondition.ControlWithAnswer[]
            {
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                    ControlAnswer = "EMPLOYEE",
                },
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_TYPE",
                    ControlAnswer = "HOSPITAL",
                },
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS",
                    ControlAnswer = "",
                    IsNotEquals = true
                },
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS",
                    ControlAnswer = "TYPE_1",
                    IsNotEquals = true
                },
                new FormControlDisplayCondition.ControlWithAnswer
                {
                    ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS",
                    ControlAnswer = "TYPE_2",
                    IsNotEquals = true
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_WOKING_PLACE_NAME()
        {
            return Working();
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_WOKING_LICENSE_NUMBER()
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
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                                ControlAnswer = "OPARETOR",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_OPARETOR_STATUS",
                                ControlAnswer = "TYPE_1",
                            },
                        }
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                                ControlAnswer = "EMPLOYEE",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS",
                                ControlAnswer = "TYPE_4",
                            },
                        }
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_TYPE()
        {
            return Working();
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_DETAIL()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                SubDisplayConditions = new FormControlDisplayCondition[]
                {
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = OparetorWorkingClinic(),
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = EmployeeWorkingClinic(),
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_TYPE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                SubDisplayConditions = new FormControlDisplayCondition[]
                {
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = OparetorWorkingClinic().Concat(new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_DETAIL",
                                ControlAnswer = "MEDICINE_CLINIC",
                            },
                        }).ToArray(),
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = EmployeeWorkingClinic().Concat(new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_DETAIL",
                                ControlAnswer = "MEDICINE_CLINIC",
                            },
                        }).ToArray(),
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_OTHER()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                SubDisplayConditions = new FormControlDisplayCondition[]
                {
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = OparetorWorkingClinic().Concat(new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_DETAIL",
                                ControlAnswer = "UNITED_CLINIC",
                            },
                        }).ToArray(),
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = OparetorWorkingClinic().Concat(new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_DETAIL",
                                ControlAnswer = "MEDICINE_CLINIC",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_TYPE",
                                ControlAnswer = "OTHER",
                            },
                        }).ToArray(),
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = EmployeeWorkingClinic().Concat(new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_DETAIL",
                                ControlAnswer = "MEDICINE_CLINIC",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CLINIC_TYPE",
                                ControlAnswer = "OTHER",
                            },
                        }).ToArray(),
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_DETAIL()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                SubDisplayConditions = new FormControlDisplayCondition[]
                {
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = OparetorWorkingHospital(),
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = EmployeeWorkingHostipal(),
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_CHOICE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                SubDisplayConditions = new FormControlDisplayCondition[]
                {
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = OparetorWorkingHospital().Concat(new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_DETAIL",
                                ControlAnswer = "SPECIFIC_PATIENT",
                            },
                        }).ToArray(),
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = EmployeeWorkingHostipal().Concat(new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_DETAIL",
                                ControlAnswer = "SPECIFIC_PATIENT",
                            },
                        }).ToArray(),
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_OTHER()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                SubDisplayConditions = new FormControlDisplayCondition[]
                {
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = OparetorWorkingHospital().Concat(new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_DETAIL",
                                ControlAnswer = "SPECIALIZED_OTHER",
                            },
                        }).ToArray(),
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = EmployeeWorkingHostipal().Concat(new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_DETAIL",
                                ControlAnswer = "SPECIALIZED_OTHER",
                            },
                        }).ToArray(),
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = OparetorWorkingHospital().Concat(new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_DETAIL",
                                ControlAnswer = "SPECIFIC_PATIENT",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_CHOICE",
                                ControlAnswer = "OTHER_PATIENTS",
                            },
                        }).ToArray(),
                    },
                    new FormControlDisplayCondition()
                    {
                        IsOr = false,
                        Conditions = EmployeeWorkingHostipal().Concat(new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_DETAIL",
                                ControlAnswer = "SPECIFIC_PATIENT",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_HOSPITAL_CHOICE",
                                ControlAnswer = "OTHER_PATIENTS",
                            },
                        }).ToArray(),
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_MEDICAL_CENTER_ADDRESS()
        {
            return Working();
        }

        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_QUIT_WOKING_DATE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DETAIL",
                        ControlAnswer = "EMPLOYEE",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_EMPLOYEE_STATUS",
                        ControlAnswer = "TYPE_3",
                    },
                },
            };
        }

        private static FormControl CUSTOM_FORM_CONTROL_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DAY_TIME_WOKING()
        {
            return new FormControl
            {
                Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DAY_TIME_WOKING",
                Type = ControlType.DataTable,
                DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DAY_TIME_WOKING",
                ColFixed = 12,
                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                DisplayCondition = new FormControlDisplayCondition
                {
                    IsOr = true,
                    SubDisplayConditions = new FormControlDisplayCondition[]
                    {
                        new FormControlDisplayCondition()
                        {
                            IsOr = false,
                            Conditions = OparetorWorkingClinic(),
                        },
                        new FormControlDisplayCondition()
                        {
                            IsOr = false,
                            Conditions = EmployeeWorkingClinic(),
                        },
                        new FormControlDisplayCondition()
                        {
                            IsOr = false,
                            Conditions = OparetorWorkingHospital(),
                        },
                        new FormControlDisplayCondition()
                        {
                            IsOr = false,
                            Conditions = EmployeeWorkingHostipal(),
                        },
                    },
                },
                DataTableColumns = new DataTableColumn[]
                {
                    new DataTableColumn()
                    {
                        Name = "DATE",
                        Title = "วัน",
                        CustomColFixed = 4,
                        Control = new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DAY_TIME_WOKING__DATE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DAY_TIME_WOKING__DATE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            WillTriggerDisplayOfOtherUI = true,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "MONDAY", Text = "จันทร์" },
                                new Select2Opt() { ID = "TUESDAY", Text = "อังคาร" },
                                new Select2Opt() { ID = "WEDNESDAY", Text = "พุธ" },
                                new Select2Opt() { ID = "THURSDAY", Text = "พฤหัส" },
                                new Select2Opt() { ID = "FRIDAY", Text = "ศุกร์" },
                                new Select2Opt() { ID = "SATURDAY", Text = "เสาร์" },
                                new Select2Opt() { ID = "SUNDAY", Text = "อาทิตย์" },
                            },
                        },
                    },
                    new DataTableColumn()
                    {
                        Name = "START_TIME",
                        Title = "เวลาเริ่มงาน",
                        CustomColFixed = 4,
                        Control = new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DAY_TIME_WOKING__START_TIME",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DAY_TIME_WOKING__START_TIME",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Select2Opts = FormSectionRow.optWorkingTime,
                            PlaceholderText = "เวลาเริ่มงาน",
                        },
                    },
                    new DataTableColumn()
                    {
                        Name = "TIME_OUT",
                        Title = "เวลาเลิกงาน",
                        CustomColFixed = 4,
                        Control = new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DAY_TIME_WOKING__TIME_OUT",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_DAY_TIME_WOKING__TIME_OUT",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Select2Opts = FormSectionRow.optHospitalWorkingTime,
                            PlaceholderText = "เวลาเลิกงาน",
                        },
                    },
                },
                ResourceName = "Apps_SingleForm",
            };
        }

        private static FormControl CUSTOM_FORM_CONTROL_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CONFIRM_WOKING()
        {
            return new FormControl
            {
                Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CONFIRM_WOKING",
                Type = ControlType.DataTable,
                DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CONFIRM_WOKING",
                ColFixed = 12,
                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                DisplayCondition = new FormControlDisplayCondition
                {
                    IsOr = true,
                    SubDisplayConditions = new FormControlDisplayCondition[]
                    {
                        new FormControlDisplayCondition()
                        {
                            IsOr = false,
                            Conditions = OparetorWorkingClinic(),
                        },
                        new FormControlDisplayCondition()
                        {
                            IsOr = false,
                            Conditions = EmployeeWorkingClinic(),
                        },
                        new FormControlDisplayCondition()
                        {
                            IsOr = false,
                            Conditions = OparetorWorkingHospital(),
                        },
                        new FormControlDisplayCondition()
                        {
                            IsOr = false,
                            Conditions = EmployeeWorkingHostipal(),
                        },
                    },
                },
                DataTableColumns = new DataTableColumn[]
                {
                    new DataTableColumn()
                    {
                        Name = "DATE",
                        Title = "วัน",
                        CustomColFixed = 4,
                        Control = new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CONFIRM_WOKING__DATE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CONFIRM_WOKING__DATE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            WillTriggerDisplayOfOtherUI = true,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "MONDAY", Text = "จันทร์" },
                                new Select2Opt() { ID = "TUESDAY", Text = "อังคาร" },
                                new Select2Opt() { ID = "WEDNESDAY", Text = "พุธ" },
                                new Select2Opt() { ID = "THURSDAY", Text = "พฤหัส" },
                                new Select2Opt() { ID = "FRIDAY", Text = "ศุกร์" },
                                new Select2Opt() { ID = "SATURDAY", Text = "เสาร์" },
                                new Select2Opt() { ID = "SUNDAY", Text = "อาทิตย์" },
                            },
                        },
                    },
                    new DataTableColumn()
                    {
                        Name = "START_TIME",
                        Title = "เวลาเริ่มงาน",
                        CustomColFixed = 4,
                        Control = new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CONFIRM_WOKING__START_TIME",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CONFIRM_WOKING__START_TIME",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Select2Opts = FormSectionRow.optWorkingTime,
                        },
                    },
                    new DataTableColumn()
                    {
                        Name = "TIME_OUT",
                        Title = "เวลาเลิกงาน",
                        CustomColFixed = 4,
                        Control = new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CONFIRM_WOKING__TIME_OUT",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION_CONFIRM_WOKING__TIME_OUT",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Select2Opts = FormSectionRow.optHospitalWorkingTime,
                        },
                    },
                },
                ResourceName = "Apps_SingleForm",
            };
        }

    }
}
