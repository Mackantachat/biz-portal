
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_EDIT_SEARCH
{
    public partial class APP_ACCOUNTING_SERVICE_EDIT_SEARCH_APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_EDIT,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_SEARCH",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F43_03_01_01",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION_REASON_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION_REASON_TYPE",
                            Info = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION_REASON_TYPE_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION_REASON_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่ ฉันได้แจ้งเปลี่ยนแปลงเลขทะเบียนนิติบุคคล (เลขใหม่) กับสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์แล้ว" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่ ฉันยังไม่ได้แจ้งเปลี่ยนแปลงเลขทะเบียนนิติบุคคล (เลขใหม่) กับสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_SEARCH",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_03_01_02",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION_JURISTIC_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION_JURISTIC_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	PreFill = true,
                        	DisplayStaticIfHasData = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_SEARCH",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_03_01_03",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION_JURISTIC_ID_OLD",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION_JURISTIC_ID_OLD",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.OnlyDigitLength, MaxLength = 13 },
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            MaxLength = 13,
                            DisplayCondition = CONDITION_APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION_JURISTIC_ID_OLD(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_SEARCH",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                         
                        CUSTOM_FORM_CONTROL_APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION_SEARCH(),
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
