using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_CANCEL_SEARCH
{
    public partial class APP_ENERGY_PRODUCTION_CANCEL_SEARCH_APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION
    {
        private static FormControl CUSTOM_FORM_CONTROL_APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION_SEARCH()
        {
            return new FormControl()
            {
                FieldID = "F38_04_04",
                Control = "APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION_SEARCH",
                Type = ControlType.Literal,
                DataKey = "APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION_SEARCH",
                ColFixed = 2,
                LiteralContent = "<label>&nbsp;</label><button name=\"APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION_SEARCH\" type=\"button\" class=\"btn btn-primary\">ค้นหา</button>",
                HideLabel = true,
                ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_CANCEL",
            };
        }
    }
}
