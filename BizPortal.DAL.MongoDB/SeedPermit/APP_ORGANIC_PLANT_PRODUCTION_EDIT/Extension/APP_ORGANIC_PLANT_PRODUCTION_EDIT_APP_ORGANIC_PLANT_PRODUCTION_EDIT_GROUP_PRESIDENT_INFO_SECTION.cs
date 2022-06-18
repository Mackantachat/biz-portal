using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_EDIT
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_EDIT_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE",
                        ControlAnswer = "GROUP",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_GROUP_PRESIDENT_EDIT__GROUP_PRESIDENT_CHECKED",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControl CUSTOM_FORM_CONTROL_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_ADDERSS()
        {
            return new FormControl()
            {
                Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_ADDERSS",
                Type = ControlType.AddressForm,
                DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_ADDERSS",
                ColFixed = 12,
                AddressForm_ShowTelephoneControl = true,
                AddressForm_ShowFaxControl = true,
                AddressForm_ShowSearchControl = false,
                AddressForm_ShowVillageControl = false,
                AddressForm_ShowBuildingControl = true,
                AddressForm_ShowPostCodeControl = true,
                HideLabel = true,
                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                ResourceName = "PermitResource.APP_ORGANIC_PLANT_PRODUCTION_EDIT",
            };
        }
    }
}
