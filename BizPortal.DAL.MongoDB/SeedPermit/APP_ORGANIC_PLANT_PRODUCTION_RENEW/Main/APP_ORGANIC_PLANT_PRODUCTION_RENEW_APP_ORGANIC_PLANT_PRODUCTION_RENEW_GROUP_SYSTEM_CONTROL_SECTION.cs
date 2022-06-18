
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_RENEW
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_RENEW_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW,
                    },
					Ordering = 12,
					DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "49_02_02_54",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_APP_CERT",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_APP_CERT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_APP_CERT_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "49_02_02_55",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_TRACKING",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_TRACKING",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_TRACKING_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "49_02_02_56",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_COMPLAINT",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_COMPLAINT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_COMPLAINT_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "49_02_02_57",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_CONTROL_DOC",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_CONTROL_DOC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_CONTROL_DOC_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "49_02_02_58",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_TRAINING",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_TRAINING",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_TRAINING_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "49_02_02_59",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_PRODUCE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_PRODUCE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_PRODUCE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "49_02_02_60",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_OTHER",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "OTHER_CHECKED", /* อื่นๆ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "49_02_02_61",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_OTHER_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_OTHER_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_OTHER_TEXT(),
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
