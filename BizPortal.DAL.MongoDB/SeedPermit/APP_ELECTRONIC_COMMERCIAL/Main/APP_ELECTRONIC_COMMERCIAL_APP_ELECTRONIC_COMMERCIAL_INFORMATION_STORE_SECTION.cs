
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;
using BizPortal.Utils.Helpers;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                    },
					Ordering = 10,
					ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl() {
                            Control = "INFORMATION_STORE_SUGGESTION",
                            ColFixed = 12,
                            DataKey = "INFORMATION_STORE_SUGGESTION",
                            DefaultShowInfo = true,
                            Info = "INFORMATION_STORE_APP_ELECTRONIC_COMMERCIAL_SUGGESTION_INFO",
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                            },
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl() {
                            FieldID = "70",
                            Control = "INFORMATION_STORE_NAME_TH",
                            Type = ControlType.TextBox,
                            DataKey = "INFORMATION_STORE_NAME_TH",
                            ColFixed = 6,
                            Info = "INFORMATION_STORE_NAME_TH_INFO",
                            DefaultShowInfo=true,
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", "{ pattern: /[^a-zA-Z]/, optional: true, recursive: true }"
                                }
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                            },
                        },
                        new FormControl() {
                            FieldID = "71",
                            Control = "INFORMATION_STORE_NAME_EN",
                            Type = ControlType.TextBox,
                            DataKey = "INFORMATION_STORE_NAME_EN",
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", "{ pattern: /[^¡-ù]/, optional: true, recursive: true }"
                                }
                            },
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                            },
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>
                    {
                        new FormControl()
                        {
                            Control = "INFORMATION_STORE__ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "INFORMATION_STORE__ADDRESS",
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL
                            },

                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },

                            AddressForm_ShowTelephoneControl = true,
                            AddressForm_ShowFaxControl = true,
                            AddressForm_ShowSearchControl = false,
                            AddressForm_ShowVillageControl = false,
                            AddressForm_ShowBuildingControl = true,
                            AddressForm_ShowPostCodeControl = true,
                            AddressForm_ShowMapControl = true,
                            AddressForm_ShowEmailControl = true,
                            AddressForm_ShowMobileControl = false,
                            PreFill = true,
                            AddressForm_ProvinceType = ProvinceType.BKK
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "INFORMATION_STORE_OFFICE_CODE",
                            Type = ControlType.Dropdown,
                            IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/DBD/Office",
                            AjaxQueryString = "?ProvinceCode={ProvinceCode}&AmphurCode={AmphurCode}",
                            ControlVariables = new ControlVariable[] {
                                new ControlVariable() { Name = "ProvinceCode", ControlSelector = "select[name='ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS']", DefaultIfEmpty = "-1", ListenOnChange = true },
                                new ControlVariable() { Name = "AmphurCode", ControlSelector = "select[name='ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS']", DefaultIfEmpty = "-1", ListenOnChange = true }
                            },
                            DataKey = "INFORMATION_STORE_OFFICE_CODE",
                            ColFixed = 6,
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[] {
                                AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                        },
                    },

                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_INFORMATION_STORE_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "INFORMATION_STORE_BUILDING_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "INFORMATION_STORE_BUILDING_TYPE",
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[] {
                                AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
                            ColFixed = 12,
                            WillTriggerDisplayOfOtherUI = true,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "INFORMATION_STORE_BUILDING_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingTypeOptionValueConst.OWNED, RadioButtonText = StoreInformationBuildingTypeOptionTextConst.OWNED },
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingTypeOptionValueConst.RENT, RadioButtonText = StoreInformationBuildingTypeOptionTextConst.RENT },
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingTypeOptionValueConst.RENT_FREE, RadioButtonText = StoreInformationBuildingTypeOptionTextConst.RENT_FREE },
                                }

                            },
                        },
                        new FormControl()
                        {
                            Control = "CITIZEN_INFORMATION_STORE_BUILDING_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "CITIZEN_INFORMATION_STORE_BUILDING_TYPE",
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[] {
                                AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
                            ColFixed = 12,
                            WillTriggerDisplayOfOtherUI = true,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingTypeOptionValueConst.OWNED, RadioButtonText = StoreInformationBuildingTypeOptionTextConst.OWNED_CITIZEN },
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingTypeOptionValueConst.RENT, RadioButtonText = StoreInformationBuildingTypeOptionTextConst.RENT },
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingTypeOptionValueConst.RENT_FREE, RadioButtonText = StoreInformationBuildingTypeOptionTextConst.RENT_FREE },
                                },
                            },
                        },
                        new FormControl() {
                            FieldID = "78",
                            Control = "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE",
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[] {
                                AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "INFORMATION_STORE_BUILDING_TYPE",
                                        ControlAnswer = StoreInformationBuildingTypeOptionValueConst.RENT
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "INFORMATION_STORE_BUILDING_TYPE",
                                        ControlAnswer = StoreInformationBuildingTypeOptionValueConst.RENT_FREE
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "CITIZEN_INFORMATION_STORE_BUILDING_TYPE",
                                        ControlAnswer = StoreInformationBuildingTypeOptionValueConst.RENT
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "CITIZEN_INFORMATION_STORE_BUILDING_TYPE",
                                        ControlAnswer = StoreInformationBuildingTypeOptionValueConst.RENT_FREE
                                    },
                                },
                            },
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.JURISTIC },
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.CITIZEN, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.CITIZEN },
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.Government, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.Government },
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.Royal, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.Royal },
                                },
                            },
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
