
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_RENEW
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_RENEW_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW,
                    },
					Ordering = 13,
					DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "49_02_02_62",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_PERSON",
                            Type = ControlType.Heading,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_PERSON",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_63",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_64",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_65",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_LASTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_66",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_POSITION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_POSITION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_67",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_TEL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_TEL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_68",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_EXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_EXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_69",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_FAX",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_FAX",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_70",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_MOBILE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_MOBILE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_71",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_EMAIL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_EMAIL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_72",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_PERSON",
                            Type = ControlType.Heading,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_PERSON",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_73",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_74",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_75",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_LASTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_76",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_POSITION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_POSITION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_77",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_TEL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_TEL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_78",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_EXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_EXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_79",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_FAX",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_FAX",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_80",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_MOBILE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_MOBILE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_81",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_EMAIL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_EMAIL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
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
