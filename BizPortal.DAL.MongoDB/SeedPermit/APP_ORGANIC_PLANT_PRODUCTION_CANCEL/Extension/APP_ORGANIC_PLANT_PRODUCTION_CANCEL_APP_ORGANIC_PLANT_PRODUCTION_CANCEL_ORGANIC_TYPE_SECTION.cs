using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_CANCEL
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_CANCEL_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION
    {
        private static FormControl CUSTOM_FORM_CONTROL_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION()
        {
            return new FormControl()
            {
                Control = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION",
                Type = ControlType.AddressForm,
                DataKey = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION",
                ColFixed = 12,
                AddressForm_ShowTelephoneControl = true,
                AddressForm_ShowFaxControl = true,
                AddressForm_ShowSearchControl = false,
                AddressForm_ShowVillageControl = false,
                AddressForm_ShowBuildingControl = true,
                AddressForm_ShowPostCodeControl = true,
                HideLabel = true,
                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                ResourceName = "PermitResource.APP_ORGANIC_PLANT_PRODUCTION_CANCEL",
            };
        }
    }
}
