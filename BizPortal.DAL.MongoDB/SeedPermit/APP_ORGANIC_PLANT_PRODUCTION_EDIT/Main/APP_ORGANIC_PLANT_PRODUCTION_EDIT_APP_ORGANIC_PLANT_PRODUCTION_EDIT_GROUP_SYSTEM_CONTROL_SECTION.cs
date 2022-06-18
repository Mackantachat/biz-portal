
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_EDIT
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_EDIT_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_EDIT,
                    },
					Ordering = 19,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F49_03_63",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_APP_CERT",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_APP_CERT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_APP_CERT_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F49_03_64",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_TRACKING",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_TRACKING",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_TRACKING_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F49_03_65",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_COMPLAINT",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_COMPLAINT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_COMPLAINT_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F49_03_66",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_CONTROL_DOC",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_CONTROL_DOC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_CONTROL_DOC_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F49_03_67",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_TRAINING",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_TRAINING",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_TRAINING_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F49_03_68",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_PRODUCE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_PRODUCE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_PRODUCE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F49_03_69",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_OTHER",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "OTHER_CHECKED", /* อื่นๆ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_03_70",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_OTHER_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_OTHER_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_SYSTEM_CONTROL_SECTION_QUESTION_OTHER_TEXT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
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
