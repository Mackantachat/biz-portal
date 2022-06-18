
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PLAN
{
    public partial class APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PLAN_INFO_SECTION",
                    SectionGroup = "APP_HOSPITAL_PLAN",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F35_02_01",
                            Control = "APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "APP_HOSPITAL_PLAN_MED_TYPE_1", /* อายุรกรรม */
                                "APP_HOSPITAL_PLAN_MED_TYPE_2", /* ศัลยกรรม */
                                "APP_HOSPITAL_PLAN_MED_TYPE_3", /* สูตินรีเวชกรรม */
                                "APP_HOSPITAL_PLAN_MED_TYPE_4", /* กุมารเวชกรรม */
                                "APP_HOSPITAL_PLAN_MED_TYPE_5", /* แผนกเทคนิคการแพทย์ */
                                "APP_HOSPITAL_PLAN_MED_TYPE_6", /* แผนกออร์โธปิดิกส์ */
                                "APP_HOSPITAL_PLAN_MED_TYPE_7", /* แผนกโรคผิวหนัง */
                                "APP_HOSPITAL_PLAN_MED_TYPE_8", /* แผนกการผสมเทียม */
                                "APP_HOSPITAL_PLAN_MED_TYPE_9", /* แผนกกายภาพบำบัด */
                                "APP_HOSPITAL_PLAN_MED_TYPE_10", /* แผนกการแพทย์แผนไทย */
                                "APP_HOSPITAL_PLAN_MED_TYPE_11", /* แผนกโภชนาการ */
                                "APP_HOSPITAL_PLAN_MED_TYPE_12", /* แผนกซักฟอก */
                                "APP_HOSPITAL_PLAN_MED_TYPE_13", /* หอผู้ป่วยหนัก */
                                "APP_HOSPITAL_PLAN_MED_TYPE_14", /* ห้องตรวจภายในและขูดมดลูก */
                                "APP_HOSPITAL_PLAN_MED_TYPE_15", /* ห้องผ่าตัดเล็ก */
                                "APP_HOSPITAL_PLAN_MED_TYPE_16", /* ห้องให้การรักษา */
                                "APP_HOSPITAL_PLAN_MED_TYPE_17", /* ห้องทารกหลังคลอด */
                                "APP_HOSPITAL_PLAN_MED_TYPE_18", /* การผ่าตัดเปลี่ยนอวัยวะ */
                                "APP_HOSPITAL_PLAN_MED_TYPE_19", /* ห้องไตเทียม */
                                "APP_HOSPITAL_PLAN_MED_TYPE_20", /* ห้องทันตกรรม */
                                "APP_HOSPITAL_PLAN_MED_TYPE_21", /* รังสีวินิจฉัยด้วยคอมพิวเตอร์ */
                                "APP_HOSPITAL_PLAN_MED_TYPE_22", /* การผ่าตัดเปิดหัวใจ */
                                "APP_HOSPITAL_PLAN_MED_TYPE_23", /* การสวนหัวใจ */
                                "APP_HOSPITAL_PLAN_MED_TYPE_24", /* รังสีบำบัด */
                                "APP_HOSPITAL_PLAN_MED_TYPE_25", /* การตรวจอวัยวะภายในชนิดสนามแม่เหล็กไฟฟ้า */
                                "APP_HOSPITAL_PLAN_MED_TYPE_26", /* การสลายนิ่วด้วยเครื่องมือ */
                                "APP_HOSPITAL_PLAN_MED_TYPE_27", /* ห้องเก็บศพ */
                                "APP_HOSPITAL_PLAN_MED_TYPE_28", /* แผนกการแพทย์แผนไทยประยุกต์ */
                                "APP_HOSPITAL_PLAN_MED_TYPE_29", /* แผนกการนวด */
                                "APP_HOSPITAL_PLAN_MED_TYPE_30", /* แผนกการแพทย์แผนจีน */
                                "APP_HOSPITAL_PLAN_MED_TYPE_31", /* อื่นๆ */
                            },
                        	DisplayCheckboxInline = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_INFO_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_02",
                            Control = "APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PLAN_INFO_SECTION_MED_TYPE_OTHER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_INFO_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F35_02_03",
                            Control = "APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE_1", RadioButtonText = "เป็นอาคารสร้างใหม่ " },
                        			new FormRadioButton() { RadioButtonValue = "APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE_2", RadioButtonText = "เป็นอาคารดัดแปลงจากอาคารเดิม" },
                        			new FormRadioButton() { RadioButtonValue = "APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE_3", RadioButtonText = "อื่นๆ" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_INFO_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_04",
                            Control = "APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE_OTHER",
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
                                { "A", "{ pattern: /[^a-zA-Z]/, optional: true, recursive: true }" },
                        	},
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PLAN_INFO_SECTION_BUILD_TYPE_OTHER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_INFO_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_05",
                            Control = "APP_HOSPITAL_PLAN_INFO_SECTION_INVESTMENT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_INFO_SECTION_INVESTMENT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "#,##0.00",
                            MaskInputReverse = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
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
