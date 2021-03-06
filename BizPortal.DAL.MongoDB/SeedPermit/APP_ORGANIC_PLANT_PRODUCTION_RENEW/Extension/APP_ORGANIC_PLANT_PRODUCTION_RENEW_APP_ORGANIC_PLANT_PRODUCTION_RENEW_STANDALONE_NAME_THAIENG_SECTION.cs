using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_RENEW
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_RENEW_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION__NAME_ENG",
                        ControlAnswer = "true",
                    },
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",
                        ControlAnswer = "STANDALONE",
                    },
                },
            };
        }
        private static FormControlDisableCondition DISABLE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION()
        {
            return new FormControlDisableCondition
            {
                IsOr = true,
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",
                        ControlAnswer = "GROUP",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION__NAME_ENG",
                        ControlAnswer = "false",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_NAME_ENG()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",
                        ControlAnswer = "STANDALONE",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION__NAME_ENG",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS_HEADER()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",
                        ControlAnswer = "STANDALONE",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION__NAME_ENG",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS_HEADER()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",
                        ControlAnswer = "STANDALONE",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION__NAME_ENG",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControl CUSTOM_FORM_CONTROL_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS()
        {
            return new FormControl()
            {
                Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS",
                Type = ControlType.AddressFormEN,
                DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_INFORMATION_ENG_ADDRESS",
                ColFixed = 12,
                HideLabel = true,
                ResourceName = "PermitResource.APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                DisplayCondition = new FormControlDisplayCondition
                {
                    IsOr = false,
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",
                            ControlAnswer = "STANDALONE",
                        },
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION__NAME_ENG",
                            ControlAnswer = "true",
                        },
                    }
                },
            };
        }
        private static FormControl CUSTOM_FORM_CONTROL_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS()
        {
            return new FormControl()
            {
                Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS",
                Type = ControlType.AddressFormEN,
                DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_NAME_THAIENG_SECTION_PRODUCE_ENG_ADDRESS",
                ColFixed = 12,
                HideLabel = true,
                ResourceName = "PermitResource.APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                DisplayCondition = new FormControlDisplayCondition
                {
                    IsOr = false,
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",
                            ControlAnswer = "STANDALONE",
                        },
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION__NAME_ENG",
                            ControlAnswer = "true",
                        },
                    }
                },
            };
        }
    }
}
