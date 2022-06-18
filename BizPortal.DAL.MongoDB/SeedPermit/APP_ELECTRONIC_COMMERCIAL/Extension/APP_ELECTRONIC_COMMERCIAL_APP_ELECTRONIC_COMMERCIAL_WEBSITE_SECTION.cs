using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_REQUEST_TYPE",
                        ControlAnswer = "ELECTRONIC",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_REQUEST_TYPE",
                        ControlAnswer = "NORMAL",
                    },
                },
            };
        }

        private static FormControl CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ADDRESS_EN()
        {
            return new FormControl()
            {
                FieldID = "F37_01_55",
                Control = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ADDRESS_EN",
                Type = ControlType.AddressFormEN,
                DataKey = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ADDRESS_EN",
                ShowOnSpecificApps = true,
                AppSystemNames = new string[]
                {
                    AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                },
                ColFixed = 12,
                ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
            };
        }

        private static string APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_TYPE_URL()
        {
            return "~/Api/v2/DBD/BusinessTypes";
        }

        private static FormControlDisplayCondition.ControlWithAnswer IS_COMMERCIAL_ELECTRONIC()
        {
            return new FormControlDisplayCondition.ControlWithAnswer
            {
                ControlName = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_REQUEST_TYPE",
                ControlAnswer = "ELECTRONIC",
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ORDER_SYSTEM_OTHER_TEXT()
        {
            return new FormControlDisplayCondition
            {
                //IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_ORDER_SYSTEM__OTHER",
                        ControlAnswer = "true",
                    },
                    //IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_PAYMENT_OTHER_TEXT()
        {
            return new FormControlDisplayCondition
            {
                //IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_PAYMENT__OTHER",
                        ControlAnswer = "true",
                    },
                    //IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_DELIVER_OTHER_TEXT()
        {
            return new FormControlDisplayCondition
            {
                //IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_METHOD_DELIVER__OTHER",
                        ControlAnswer = "true",
                    },
                    //IS_COMMERCIAL_ELECTRONIC(),
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_REQUEST_TYPE",
                        ControlAnswer = "ELECTRONIC",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_REQUEST_TYPE",
                        ControlAnswer = "NORMAL",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_WEBSITE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "01" //"WEBSITE",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_LINE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "03" //"LINE",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_INSTRAGRAM()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "04" // "INSTRAGRAM",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_FACEBOOK()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "02" //"FACEBOOK",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_MARKETPLACE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "05" //"MARKETPLACE",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION_APPLICATION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "06" //"APPLICATION",
                    },
                },
            };
        }
    }
}
