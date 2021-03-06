using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH
{
    public partial class APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION
    {
        private static FormControl CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION_SEARCH()
        {
            return new FormControl()
            {
                FieldID = "F37_03_04",
                Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION_SEARCH",
                Type = ControlType.Literal,
                DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION_SEARCH",
                ColFixed = 2,
                LiteralContent = "<label>&nbsp;</label><button name=\"APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION_SEARCH\" type=\"button\" class=\"btn btn-primary\">ค้นหาข้อมูล</button>",
                HideLabel = true,
                ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH",
            };
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION_PERMIT_LIST()
        {
            return new Select2Opt[] {
                new Select2Opt{ ID = "REG01", Text = "REG01" },
                new Select2Opt{ ID = "REG02", Text = "REG02" },
                new Select2Opt{ ID = "REG03", Text = "REG03" },
                new Select2Opt{ ID = "REG04", Text = "REG04" },
            };
        }
    }
}
