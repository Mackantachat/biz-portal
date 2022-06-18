
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_RENEW
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_RENEW_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONFIRM_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONFIRM_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONFIRM_SECTION",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW,
                    },
					Ordering = 8,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONFIRM_SECTION(),
					DisableCondition = DISABLE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONFIRM_SECTION(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONFIRM_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONFIRM_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "49_02_02_37",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONFIRM_SECTION_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONFIRM_SECTION_CONFIRM",
                            Info = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_CONFIRM_SECTION_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            CheckboxName = new string[]
                            {
                                "CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่าหลักฐานและเอกสารที่จัดส่งให้หน่วยรับรองกรมวิชาการเกษตร เป็นความจริงและเป็นปัจจุบัน รวมทั้งได้รับเอกสารหลักเกณฑ์และเงื่อนไขที่เกี่ยวข้องกับการรับรองและการตรวจประเมินการผลิตพืชอินทรีย์เรียบร้อยแล้ว */
                            },
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
