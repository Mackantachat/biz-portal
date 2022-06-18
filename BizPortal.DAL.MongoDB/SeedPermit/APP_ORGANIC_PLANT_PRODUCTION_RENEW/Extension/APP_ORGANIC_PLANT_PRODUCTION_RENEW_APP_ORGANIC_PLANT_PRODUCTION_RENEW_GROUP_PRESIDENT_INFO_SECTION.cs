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
    public partial class APP_ORGANIC_PLANT_PRODUCTION_RENEW_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRESIDENT_INFO_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRESIDENT_INFO_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",
                        ControlAnswer = "GROUP",
                    },
                },
            };
        }

        private static Select2Opt[] DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }

        private static FormControl CUSTOM_FORM_CONTROL_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_ADDERSS()
        {
            return new FormControl()
            {
                Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_ADDERSS",
                Type = ControlType.AddressForm,
                DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_ADDERSS",
                ColFixed = 12,
                HideLabel = true,
                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                ResourceName = "PermitResource.APP_ORGANIC_PLANT_PRODUCTION_RENEW",
            };
        }
    }
}
