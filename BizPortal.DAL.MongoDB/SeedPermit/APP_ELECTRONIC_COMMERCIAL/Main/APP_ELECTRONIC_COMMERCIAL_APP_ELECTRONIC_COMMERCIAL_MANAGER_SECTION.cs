
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;
using BizPortal.Utils.Helpers;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL",
                    Type = SectionType.ArrayOfForms,
					Info = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_INFO",
					DefaultShowInfo = true,
                    EmptyDataMessage = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_EMPTY",
                    AddButtonText = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADD",
                    SubmitButtonText = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_SUBMIT",
					ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                    },
					Ordering = 2,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_01_05",
                            Control = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 2,
                            IsAjaxDropdown = true,
                            AjaxUrl = APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_TITLE_URL(),
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_06",
                            Control = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_FIRSTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_FIRSTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_07",
                            Control = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_LASTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_01_09",
                            Control = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_BIRTH_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_BIRTH_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                            IsShowAge = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                            {
                                EndDate = "-1d",
                            }
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_10",
                            Control = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_NATIONALITY",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_NATIONALITY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = DROPDOWN_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_NATIONALITY(),
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_01_08",
                            Control = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ID_CARD",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ID_CARD",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_NATIONALITY",
                                        ControlAnswer = "TH",
                                    },
                                },
                            }
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_12",
                            Control = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_PASSPORT_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_PASSPORT_NUMBER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                { "A", "{ pattern: /[^¡-ù]/, optional: true, recursive: true }" },
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                IsOr = false,
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_NATIONALITY",
                                        ControlAnswer = "",
                                        IsNotEquals = true
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_NATIONALITY",
                                        ControlAnswer = "TH",
                                        IsNotEquals = true
                                    },
                                },
                            }
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_01_11",
                            Control = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_ADDRESS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                        	AddressForm_ShowVillageControl = true,
                        	AddressForm_ShowBuildingControl = true,
                        	AddressForm_ShowPostCodeControl = true,
                        	AddressForm_ShowTelephoneControl = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
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
