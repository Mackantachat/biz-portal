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
    public partial class APP_ORGANIC_PLANT_PRODUCTION_RENEW_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",
                        ControlAnswer = "STANDALONE",
                    },
                },
            };
        }
    }
    public partial class APP_ORGANIC_PLANT_PRODUCTION_RENEW_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION
    {
        private static FormControl CUSTOM_FORM_CONTROL_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS()
        {
            return new FormControl()
            {
                Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS",
                Type = ControlType.AddressForm,                
                DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS",
                ColFixed = 12,
                HideLabel = true,
                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                AddressForm_ShowTelephoneControl = true,
                AddressForm_ShowFaxControl = true,
                AddressForm_ShowPostCodeControl = true,
                AddressForm_ShowMapControl = true,
                AddressForm_ShowMobileControl = true,
                AddressForm_ShowEmailControl = true,
                ResourceName = "PermitResource.APP_ORGANIC_PLANT_PRODUCTION_RENEW",
            };
        }
    }
    public partial class APP_ORGANIC_PLANT_PRODUCTION_RENEW_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_RENT_OWNED_TYPE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE",
                        ControlAnswer = "RENT",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE",
                        ControlAnswer = "RENT_FREE",
                    },
                },
            };
        }
    }
    //private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE()
    //{
    //    return new FormControlDisplayCondition
    //    {
    //        IsOr = false,
    //        Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
    //        {
    //                new FormControlDisplayCondition.ControlWithAnswer
    //                {
    //                    ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",
    //                    ControlAnswer = "STANDALONE",
    //                },
    //                new FormControlDisplayCondition.ControlWithAnswer
    //                {
    //                    ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CHECKED_STANDALONE_ADDRESS_RENEW__STANDALONE_ADDRESS_CHECKED",
    //                    ControlAnswer = "true",
    //                },
    //        },
    //    };
    //}
}
