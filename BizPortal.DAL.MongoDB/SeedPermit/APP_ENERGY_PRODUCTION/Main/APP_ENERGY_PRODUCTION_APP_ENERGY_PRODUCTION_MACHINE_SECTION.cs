
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;
using BizPortal.Utils.Helpers;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION
{
    public partial class APP_ENERGY_PRODUCTION_APP_ENERGY_PRODUCTION_MACHINE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_MACHINE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ENERGY_PRODUCTION_MACHINE_SECTION",
                    SectionGroup = "APP_ENERGY_PRODUCTION",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_EMPTY",
                    AddButtonText = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_ADD",
                    SubmitButtonText = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_SUBMIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                    },
					Ordering = 2,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_MACHINE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_MACHINE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F38_01_MACHINE_IDX",
                            Control = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_IDX",
                            Type = ControlType.TextBox,
                            DataKey = "ARR_IDX",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 3,
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                            DisplayReadonly = true,
                        },
                        new FormControl()
                        {
                            Control = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_INSTALL_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_INSTALL_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "NEW", Text = "?????????????????????????????????" },
                                new Select2Opt() { ID = "CURRENT", Text = "?????????????????????????????????" },
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_MACHINE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F38_01_03",
                            Control = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/DEDE/plantOptions",
                            PlaceholderText = "?????????????????????????????????????????????????????????",
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            FieldID = "F38_01_04",
                            Control = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_FUEL",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_FUEL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/DEDE/fuelOptions",
                            AjaxQueryString = "?pCode={PlantCode}",
                            ControlVariables = new ControlVariable[] {
                                new ControlVariable() {
                                    Name = "PlantCode",
                                    ControlSelector = "select[name='AJAX_DROPDOWN_APP_ENERGY_PRODUCTION_MACHINE_SECTION_TYPE']",
                                    DefaultIfEmpty = "-1",
                                    ListenOnChange = true
                                },
                            },
                            PlaceholderText = "??????????????????????????????",
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            FieldID = "F38_01_05",
                            Control = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_HORSEPOWER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_HORSEPOWER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 4,
                            //Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Rules = new FormValidationRule[] {
                                new FormValidationRule() {
                                    Type = ValidationType.JSExpression,
                                    JSExpression = "return APP_ENERGY_PRODUCTION_MACHINE_SECTION_HORSEPOWER_CHECK_REQUIRE();",
                                    ErrorMessage = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_HORSEPOWER_REQUIRED" }
                            },
                            TextboxNumberSettings = SETTING_NUMBER_APP_ENERGY_PRODUCTION_MACHINE_SECTION_HORSEPOWER(),
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_MACHINE_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F38_01_06",
                            Control = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_YEAR_ACTIVE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_YEAR_ACTIVE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 4,
                            Rules = new FormValidationRule[] {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() {
                                    Type = ValidationType.JSExpression,
                                    JSExpression = "return APP_ENERGY_PRODUCTION_CHECK_ERA_YEAR_ACTIVE();",
                                    ErrorMessage = "APP_ENERGY_PRODUCTION_CHECK_ERA_YEAR_ACTIVE_EXCEED"
                                },
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            FieldID = "F38_01_07",
                            Control = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            FieldID = "F38_01_08",
                            Control = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_MACHINE_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F38_01_06",
                            Control = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_PHOTOVOLTAIC",
                            Type = ControlType.DataTable,
                            DataKey = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_PHOTOVOLTAIC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ENERGY_PRODUCTION_MACHINE_SECTION_TYPE",
                                        ControlAnswer = "7", // ???????????????????????? = id 7
                                    }
                                },
                            },
                            DataTableColumns = new DataTableColumn[]
                            {
                                new DataTableColumn()
                                {
                                    Name = "PHOTOVOLTAIC_INDEX",
                                    Title = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_INDEX",
                                    IsIndexColumn = true,
                                    CustomColFixed = 1
                                },
                                new DataTableColumn()
                                {
                                    Name = "PHOTOVOLTAIC_SOLAR",
                                    Title = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_SOLAR",
                                    Control = new FormControl()
                                    {
                                        Control = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_SOLAR",
                                        Type = ControlType.Dropdown,
                                        DataKey = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_SOLAR",
                                        IsAjaxDropdown = true,
                                        AjaxUrl = "~/Api/v2/DEDE/solarOptions",
                                        Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                                    },
                                    CustomColFixed = 3
                                },
                                new DataTableColumn()
                                {
                                    Name = "PHOTOVOLTAIC_TYPE",
                                    Title = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_TYPE",
                                    Control = new FormControl()
                                    {
                                        Control = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_TYPE",
                                        Type = ControlType.Dropdown,
                                        DataKey = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_TYPE",
                                        IsAjaxDropdown = true,
                                        AjaxUrl = "~/Api/v2/DEDE/solarTypeOptions",
                                        Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                                    },
                                    ExtraControl = new FormControl()
                                    {
                                        Control = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_TYPE_OTHER",
                                        Type = ControlType.TextBox,
                                        DataKey = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_TYPE_OTHER",
                                    },
                                    AnswersForExtraControl = new string[] { "3" },
                                    CustomColFixed = 3
                                },
                                new DataTableColumn()
                                {
                                    Name = "PHOTOVOLTAIC_WATT",
                                    Title = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_WATT",
                                    Control = new FormControl()
                                    {
                                        Control = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_WATT",
                                        Type = ControlType.TextBox,
                                        DataKey = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_WATT",
                                        Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                                        DisplayMaskInput = true,
                                        MaskInputPattern = "0#",
                                    },
                                },
                                new DataTableColumn()
                                {
                                    Name = "PHOTOVOLTAIC_AMOUNT",
                                    Title = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_AMOUNT",
                                    Control = new FormControl()
                                    {
                                        Control = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_AMOUNT",
                                        Type = ControlType.TextBox,
                                        DataKey = "APP_ENERGY_PRODUCTION_PHOTOVOLTAIC_SECTION_AMOUNT",
                                        Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                                        DisplayMaskInput = true,
                                        MaskInputPattern = "0#",
                                    },
                                },
                            },
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
