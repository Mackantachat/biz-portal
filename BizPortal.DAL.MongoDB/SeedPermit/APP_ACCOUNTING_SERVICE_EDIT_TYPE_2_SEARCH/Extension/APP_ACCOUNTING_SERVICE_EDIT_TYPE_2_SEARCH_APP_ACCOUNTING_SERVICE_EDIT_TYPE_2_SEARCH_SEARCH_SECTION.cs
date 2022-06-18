using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_SEARCH
{
    public partial class APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_SEARCH_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_SEARCH_SEARCH_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_SEARCH_SEARCH_SECTION_JURISTIC_ID_OLD()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_SEARCH_SEARCH_SECTION_REASON_TYPE",
                        ControlAnswer = "NO",
                    },
                },
            };
        }
        private static FormControl CUSTOM_FORM_CONTROL_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_SEARCH_SEARCH_SECTION_SEARCH()
        {
            return new FormControl()
            {
                FieldID = "F43_03_01_04",
                Control = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_SEARCH_SEARCH_SECTION_SEARCH",
                Type = ControlType.Literal,
                DataKey = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_SEARCH_SEARCH_SECTION_SEARCH",
                ColFixed = 2,
                LiteralContent = "<label>&nbsp;</label><button name=\"APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_SEARCH_SEARCH_SECTION_SEARCH\" type=\"button\" class=\"btn btn-primary\">ค้นหาข้อมูล</button>",
                HideLabel = true,
                ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_SEARCH",
            };
        }
    }
}
