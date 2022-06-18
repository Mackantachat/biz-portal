
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION
{
    public partial class APP_ENERGY_PRODUCTION_APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION",
                    SectionGroup = "APP_ENERGY_PRODUCTION",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_EMPTY",
                    AddButtonText = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_ADD",
                    SubmitButtonText = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_SUBMIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                    },
					Ordering = 3,
					DisplayCondition = CONDITION_APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F38_01_PHOTOVOLTAIC_IDX",
                            Control = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_IDX",
                            Type = ControlType.TextBox,
                            DataKey = "ARR_IDX",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 2,
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                            DisplayReadonly = true,
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F38_01_13",
                            Control = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_SOLAR",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_SOLAR",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/DEDE/solarOptions",
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            FieldID = "F38_01_09",
                            Control = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/DEDE/solarTypeOptions",
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            FieldID = "F38_01_10",
                            Control = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            DisplayCondition = CONDITION_APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_OTHER(),
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {                        
                        new FormControl()
                        {
                            FieldID = "F38_01_11",
                            Control = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_WATT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_WATT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	TextboxNumberSettings = SETTING_NUMBER_APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_WATT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            FieldID = "F38_01_12",
                            Control = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
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
