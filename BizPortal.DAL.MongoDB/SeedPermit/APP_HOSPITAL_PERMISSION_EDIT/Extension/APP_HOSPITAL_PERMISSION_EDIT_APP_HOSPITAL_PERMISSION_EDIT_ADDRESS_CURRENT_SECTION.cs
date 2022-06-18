using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_EDIT
{
    public partial class APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_OPERATOR",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_OPERATOR",
                        ControlAnswer = "false",
                    },
                },
            };
        }

        private static FormControl CUSTOM_FORM_CONTROL_APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION_USED_CURRENT_ADDRESS()
        {
            return new FormControl()
            {
                FieldID = "F47_01_36",
                Control = "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION_USED_CURRENT_ADDRESS",
                Type = ControlType.CheckBox,
                DataKey = "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION_USED_CURRENT_ADDRESS",
                IdentityTypes = new UserTypeEnum[] {
                    UserTypeEnum.Juristic,
                    UserTypeEnum.Citizen,
                },
                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                ColFixed = 12,
                CheckboxName = new string[]
                {
                    "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION_USED_CURRENT_ADDRESS", /* เปลี่ยนแปลงรายการใบอนุญาตให้ประกอบกิจการสถานพยาบาล (ประเภทที่รับผู้ป่วยค้างคืน) */
                },
                HideLabel = true,
                DisplayCondition = new FormControlDisplayCondition
                {
                    IsOr = false,
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_OPERATOR",
                            ControlAnswer = "true",
                        },
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_HOSPITAL_PERMISSION_EDIT_OPERATOR_SECTION_NATIONALITY",
                            ControlAnswer = "TH",
                        },
                    },
                },
                ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                WillTriggerDisplayOfOtherUI = true,
            };
        }
    }
}
