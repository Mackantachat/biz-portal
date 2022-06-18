
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_EDIT
{
    public partial class APP_ELECTRONIC_COMMERCIAL_EDIT_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_EDIT,
                    },
					Ordering = 27,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION(),
					DisableCondition = DISABLE_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_03_67",
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_OTHER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F37_03_68",
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_ELECTRONIC_MEDIA",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_ELECTRONIC_MEDIA",
                            Info = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_ELECTRONIC_MEDIA_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_ELECTRONIC_MEDIA_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "01", RadioButtonText = "เว็บไซต์ของผู้ประกอบการ (Own Website)" },
                                    new FormRadioButton() { RadioButtonValue = "02", RadioButtonText = "เฟซบุ๊ก (Facebook)" },
                                    new FormRadioButton() { RadioButtonValue = "03", RadioButtonText = "ไลน์ (Line)" },
                                    new FormRadioButton() { RadioButtonValue = "04", RadioButtonText = "อินสตาแกรม (Instragram)" },
                                    new FormRadioButton() { RadioButtonValue = "05", RadioButtonText = "เว็บไซต์ตลาดกลาง (Marketplace Website)" },
                                    new FormRadioButton() { RadioButtonValue = "06", RadioButtonText = "แอปพลิเคชัน (Application)" },
                                }
                            },
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_ELECTRONIC_MEDIA(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
                        },

                        // 2019-08-19: ไม่ใช้แล้ว
                        /*
                        new FormControl()
                        {
                            FieldID = "F37_03_69",
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_WEBSITE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_WEBSITE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_WEBSITE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_03_70",
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_LINE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_LINE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_LINE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_03_71",
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_INSTRAGRAM",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_INSTRAGRAM",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_INSTRAGRAM(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_03_72",
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_FACEBOOK",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_FACEBOOK",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_FACEBOOK(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_03_73",
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_APPLICATION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_APPLICATION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_OTHER_SECTION_APPLICATION(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
                        },
                        */
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
