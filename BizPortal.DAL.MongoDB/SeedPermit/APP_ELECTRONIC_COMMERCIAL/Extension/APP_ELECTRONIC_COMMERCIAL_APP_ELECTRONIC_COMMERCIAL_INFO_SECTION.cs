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
    public partial class APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_INFO_SECTION
    {
        private static string APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_TYPE_URL()
        {
            return "~/Api/v2/DBD/ServiceTypes";
        }

        private static FormControl CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_TYPE()
        {
            return new FormControl
            {
                Control = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_TYPE",
                Type = ControlType.DataTable,
                DataKey = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_TYPE",
                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                ColFixed = 12,
                DataTableColumns = new DataTableColumn[]
                {
                    new DataTableColumn()
                    {
                        Name = "TYPE",
                        //Title = "ข้อมูลชนิดแห่งพาณิชย์กิจ",
                        Control = new FormControl()
                        {
                            FieldID = "F37_01_01",
                            Control = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_TYPE__TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_TYPE__TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            IsAjaxDropdown = true,
                            AjaxUrl = APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_TYPE_URL(),
                            WillTriggerDisplayOfOtherUI = true,
                        },
                    }
                },
                ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                DataTableRowKeys = new string[] { "TYPE" }
            };
        }
    }
}
