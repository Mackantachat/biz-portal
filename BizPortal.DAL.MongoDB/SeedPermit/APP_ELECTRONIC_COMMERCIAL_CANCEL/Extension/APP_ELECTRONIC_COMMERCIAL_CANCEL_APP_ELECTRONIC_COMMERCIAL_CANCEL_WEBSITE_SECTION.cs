using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_CANCEL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_CANCEL_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION
    {
        private static FormControlDisplayCondition.ControlWithAnswer IS_COMMERCIAL_ELECTRONIC()
        {
            return new FormControlDisplayCondition.ControlWithAnswer
            {
                ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_REQUEST_TYPE",
                ControlAnswer = "ELECTRONIC",
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_REQUEST_TYPE",
                        ControlAnswer = "NORMAL",
                    }
                },
            };
        }

        private static FormControl CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN()
        {
            return new FormControl()
            {
                FieldID = "F37_01_55",
                Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN",
                Type = ControlType.AddressFormEN,
                DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ADDRESS_EN",
                ShowOnSpecificApps = true,
                AppSystemNames = new string[]
                {
                    AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_CANCEL,
                },
                ColFixed = 12,
                DisplayCondition = new FormControlDisplayCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        IS_COMMERCIAL_ELECTRONIC(),
                    },
                },
                ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_NAME()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static string APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_TYPE_URL()
        {
            return "~/Api/v2/DBD/BusinessTypes";
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_TYPE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM_OTHER_TEXT()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_ORDER_SYSTEM__OTHER",
                        ControlAnswer = "true",
                    },
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT_OTHER_TEXT()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_PAYMENT__OTHER",
                        ControlAnswer = "true",
                    },
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER_OTHER_TEXT()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_METHOD_DELIVER__OTHER",
                        ControlAnswer = "true",
                    },
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_BUDGET()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_EMAIL()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_WEBSITE_SECTION_CONFIRM()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }
    }
}
