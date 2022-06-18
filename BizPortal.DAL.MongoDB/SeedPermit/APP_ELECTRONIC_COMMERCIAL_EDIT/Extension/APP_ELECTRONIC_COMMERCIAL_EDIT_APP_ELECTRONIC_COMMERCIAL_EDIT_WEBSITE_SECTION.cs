using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_EDIT
{
    public partial class APP_ELECTRONIC_COMMERCIAL_EDIT_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION
    {
        private static FormControlDisplayCondition.ControlWithAnswer IS_COMMERCIAL_ELECTRONIC()
        {
            return new FormControlDisplayCondition.ControlWithAnswer
            {
                ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE",
                ControlAnswer = "ELECTRONIC",
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_WEBSITE_SECTION_CHECKBOX_EDIT__EDIT_WEBSITE_SECTION_CHECKED",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_WEBSITE_SECTION_CHECKBOX_EDIT__EDIT_WEBSITE_SECTION_CHECKED",
                        ControlAnswer = "false",
                    },
                },
            };
        }

        private static FormControl CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN()
        {
            return new FormControl()
            {
                FieldID = "F37_03_76",
                Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN",
                Type = ControlType.AddressFormEN,
                DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ADDRESS_EN",
                ColFixed = 12,
                DisplayCondition = new FormControlDisplayCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        IS_COMMERCIAL_ELECTRONIC(),
                    },
                },
                ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_NAME()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static string APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_TYPE_URL()
        {
            return "~/Api/v2/DBD/BusinessTypes"; 
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_TYPE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ORDER_SYSTEM()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ORDER_SYSTEM_OTHER_TEXT()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_ORDER_SYSTEM__OTHER",
                        ControlAnswer = "true",
                    },
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_PAYMENT()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_PAYMENT_OTHER_TEXT()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_PAYMENT__OTHER",
                        ControlAnswer = "true",
                    },
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_DELIVER()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_DELIVER_OTHER_TEXT()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_METHOD_DELIVER__OTHER",
                        ControlAnswer = "true",
                    },
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_BUDGET()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_EMAIL()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_WEBSITE_SECTION_CONFIRM()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_WEBSITE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "01" //"WEBSITE",
                    },
                    IS_COMMERCIAL_ELECTRONIC(),
                },
                IsOr = false
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_LINE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "03" //"LINE",
                    },
                    IS_COMMERCIAL_ELECTRONIC()
                },
                IsOr = false
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_INSTRAGRAM()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "04" // "INSTRAGRAM",
                    },
                    IS_COMMERCIAL_ELECTRONIC()
                },
                IsOr = false
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_FACEBOOK()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "02" //"FACEBOOK",
                    },
                    IS_COMMERCIAL_ELECTRONIC()
                },
                IsOr = false
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_MARKETPLACE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "05" //"MARKETPLACE",
                    },
                    IS_COMMERCIAL_ELECTRONIC()
                },
                IsOr = false
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_APPLICATION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "06" //"APPLICATION",
                    },
                    IS_COMMERCIAL_ELECTRONIC()
                },
                IsOr = false
            };
        }
    }
}
