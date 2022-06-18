
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                    },
					Ordering = 16,
					DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION(),
					DisableCondition = DISABLE_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F37_01_49",
                            Control = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA",
                            Info = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA_OPTION",
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
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        /*  2019-08-19: ย้ายไปไว้แทนที่ฟิลด์ "ชื่อเว็บไซต์ (Website)"  ที่อยู่ด้านล่าง
                        new FormControl()
                        {
                            FieldID = "F37_01_50",
                            Control = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_WEBSITE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_WEBSITE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_WEBSITE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_51",
                            Control = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_LINE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_LINE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_LINE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_52",
                            Control = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_INSTRAGRAM",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_INSTRAGRAM",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_INSTRAGRAM(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_53",
                            Control = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_FACEBOOK",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_FACEBOOK",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_FACEBOOK(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_54",
                            Control = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_APPLICATION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_APPLICATION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_APPLICATION(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
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
