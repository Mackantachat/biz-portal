
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_RENEW
{
    public partial class APP_ENERGY_PRODUCTION_RENEW_APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION",
                    SectionGroup = "APP_ENERGY_PRODUCTION_RENEW",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_EMPTY",
                    AddButtonText = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_ADD",
                    SubmitButtonText = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_SUBMIT",
					ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION_RENEW,
                    },
					Ordering = 4,
					ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_RENEW",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F38_03_13",
                            Control = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/DEDE/genTypeOptions",
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F38_03_14",
                            Control = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_KVA",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_KVA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	TextboxNumberSettings = SETTING_NUMBER_APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_KVA(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F38_03_15",
                            Control = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_VOLTAGE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_VOLTAGE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 4,
                        	TextboxNumberSettings = SETTING_NUMBER_APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_VOLTAGE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F38_03_16",
                            Control = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_AMP",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_AMP",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 4,
                        	TextboxNumberSettings = SETTING_NUMBER_APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_AMP(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F38_03_17",
                            Control = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_PERCENT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_PERCENT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 4,
                        	TextboxNumberSettings = SETTING_NUMBER_APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_PERCENT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F38_03_18",
                            Control = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_YEAR",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_YEAR",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 4,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F38_03_19",
                            Control = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F38_03_20",
                            Control = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_RENEW_ELECTRIC_SECTION_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_RENEW",
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
