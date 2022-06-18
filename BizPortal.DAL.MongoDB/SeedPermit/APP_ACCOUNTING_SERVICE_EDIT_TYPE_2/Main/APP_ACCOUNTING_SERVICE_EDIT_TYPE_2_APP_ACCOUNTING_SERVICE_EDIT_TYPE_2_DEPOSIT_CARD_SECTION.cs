
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2
{
    public partial class APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_EMPTY",
                    AddButtonText = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_ADD",
                    SubmitButtonText = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_SUBMIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2,
                    },
					Ordering = 16,
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    DisplayCondition = CONDITION_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION(),
					DisableCondition = DISABLE_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_03_50",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_BANK_NAME",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_BANK_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Select2Opts = DROPDOWN_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_BANK_NAME(),
                            WillTriggerDisplayOfOtherUI = true,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_51",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_BRANCH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_BRANCH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_03_52",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_ACCOUNT_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_ACCOUNT_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_53",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_ACCOUNT_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_ACCOUNT_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_03_54",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_DUE_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_DUE_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties()
                            {
                                StartDate = "1d"
                            }
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_03_55",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_DEPOSIT_CARD_SECTION_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2",
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
