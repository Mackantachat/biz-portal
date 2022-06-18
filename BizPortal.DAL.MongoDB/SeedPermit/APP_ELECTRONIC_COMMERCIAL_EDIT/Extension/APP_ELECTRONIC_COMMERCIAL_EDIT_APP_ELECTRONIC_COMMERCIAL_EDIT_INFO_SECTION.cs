using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_EDIT
{
    public partial class APP_ELECTRONIC_COMMERCIAL_EDIT_APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION
    {
        private static string APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_TYPE_URL()
        {
            return "~/Api/v2/DBD/ServiceTypes";
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INFO_SECTION_CHECKBOX_EDIT__EDIT_INFO_SECTION_CHECKED",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_INFO_SECTION_CHECKBOX_EDIT__EDIT_INFO_SECTION_CHECKED",
                        ControlAnswer = "false",
                    },
                },
            };
        }

        private static FormControl CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_TYPE()
        {
            return new FormControl
            {
                Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_TYPE",
                Type = ControlType.DataTable,
                DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_TYPE",
                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                ColFixed = 12,
                DataTableColumns = new DataTableColumn[]
                {
                    new DataTableColumn()
                    {
                        Name = "TYPE",
                        Control = new FormControl()
                        {
                            FieldID = "F37_03_01",
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_TYPE__TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_TYPE__TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            IsAjaxDropdown = true,
                            AjaxUrl = APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_TYPE_URL(),
                            WillTriggerDisplayOfOtherUI = true,
                        },
                    }
                },
                ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
                DataTableRowKeys = new string[] { "TYPE" }
            };
        }
    }
}
