
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION
{
    public partial class APP_ENERGY_PRODUCTION_APP_ENERGY_PRODUCTION_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ENERGY_PRODUCTION_INFO_SECTION",
                    SectionGroup = "APP_ENERGY_PRODUCTION",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            InitFormSectionRow();
        }

        private static void InitFormSectionRow()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F38_01_01_01",
                            Control = "APP_ENERGY_PRODUCTION_LICENSE_REASON",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_LICENSE_REASON",
                            Textbox_Rows = 5,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        //new FormControl()
                        //{
                        //    FieldID = "F38_01_01",
                        //    Control = "APP_ENERGY_PRODUCTION_INFO_SECTION_REASON",
                        //    Type = ControlType.RadioGroup,
                        //    DataKey = "APP_ENERGY_PRODUCTION_INFO_SECTION_REASON",
                        //    IdentityTypes = new UserTypeEnum[] {
                        //        UserTypeEnum.Juristic,
                        //        UserTypeEnum.Citizen,
                        //     },
                        //    Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                        //    ColFixed = 12,
                        //    RadioGroup = new FormRadioGroup()
                        //    {
                        //        RadioGroupName = "APP_ENERGY_PRODUCTION_INFO_SECTION_REASON_OPTION",
                        //        RadioButtons = new FormRadioButton[]
                        //        {
                        //			new FormRadioButton() { RadioButtonValue = "EMERGENCY", RadioButtonText = "ใช้สำรองฉุกเฉิน" },
                        //			new FormRadioButton() { RadioButtonValue = "ALONE", RadioButtonText = "ใช้แบบแยกเดี่ยว (Stand alone)" },
                        //        }
                        //    },
                        //	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        //},
                        new FormControl()
                        {
                            FieldID = "F38_01_01",
                            Control = "APP_ENERGY_PRODUCTION_INFO_SECTION_REASON",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ENERGY_PRODUCTION_INFO_SECTION_REASON",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            PlaceholderText = "พลังงานควบคุมที่ผลิตมีไว้เพื่อ",
                            ColFixed = 6,
                            IdentityTypes = new UserTypeEnum[] {
                                    UserTypeEnum.Juristic,
                                    UserTypeEnum.Citizen,
                            },
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[] {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                            },
                            IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/ERC/Purpose",
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            Control = "APP_ENERGY_PRODUCTION_INFO_SECTION_MACHINE_CHECK",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ENERGY_PRODUCTION_INFO_SECTION_MACHINE_CHECK",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            CheckboxName = new string[]
                            {
                                "HAS_PHOTOVOLTEIC", /* มีเครื่องต้นกำลังแบบโฟโตโวลเทอิก */
                            },
                            IsCustomClass = true,
                            CustomClassName = "hide",
                            WillTriggerDisplayOfOtherUI = true
                        },
                    }
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
    }
}
