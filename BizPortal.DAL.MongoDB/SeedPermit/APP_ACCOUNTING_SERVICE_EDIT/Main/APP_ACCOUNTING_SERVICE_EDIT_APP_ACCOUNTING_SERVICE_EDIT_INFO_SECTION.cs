
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_EDIT
{
    public partial class APP_ACCOUNTING_SERVICE_EDIT_APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_EDIT,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_03_02",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_REGISTERED_CHECKED",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_REGISTERED_CHECKED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                            HideLabel = true,
                            CheckboxName = new string[]
                            {
                                "EDIT_AMOUNT_REGISTERED", /* แก้ไขจำนวนเงินทุนจดทะเบียน */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F43_03_03",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_AMOUNT_BATH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_AMOUNT_BATH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            AdvancedTextboxType = AdvancedTextboxType.Currency,
                            DisplayCondition = CONDITION_APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_AMOUNT_BATH(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F43_03_04",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_SERVICE_TYPE_CHECKED",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_SERVICE_TYPE_CHECKED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                            HideLabel = true,
                            CheckboxName = new string[]
                            {
                                "SERVICE_TYPE_EDIT", /* แก้ไขประเภทของการให้บริการ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F43_03_05",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_SERVICE_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_SERVICE_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_SERVICE_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "ACCOUNTING", RadioButtonText = "ทำบัญชี" },
                        			new FormRadioButton() { RadioButtonValue = "AUDITORING", RadioButtonText = "สอบบัญชี" },
                        			new FormRadioButton() { RadioButtonValue = "BOTH", RadioButtonText = "ทำบัญชีและสอบบัญชี" },
                                }
                            },
                        	DisplayCondition = CONDITION_APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_SERVICE_TYPE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
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
