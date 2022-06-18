
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_RENEW
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_RENEW_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW,
                    },
					Ordering = 4,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "49_02_02_19",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_INFORMATION_SHOW_ADDRESS",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_INFORMATION_SHOW_ADDRESS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_INFORMATION_SHOW_ADDRESS_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "LOCATION_ADDRESS", RadioButtonText = "ที่อยู่สำนักงาน/สำนักงาน" },
                        			new FormRadioButton() { RadioButtonValue = "PRODUCE_ADDRESS", RadioButtonText = "ที่ตั้งแปลง" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "49_02_02_20",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "FARMER", /* ใช้ข้อมูลเกษตรกร */
                                "NAME_THAI", /* ชื่ออื่นภาษาไทย */
                                "NAME_ENG", /* ภาษาอังกฤษ */
                            },
                        	DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_SECTION_2_LICENSE_INFORMATION(),
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
