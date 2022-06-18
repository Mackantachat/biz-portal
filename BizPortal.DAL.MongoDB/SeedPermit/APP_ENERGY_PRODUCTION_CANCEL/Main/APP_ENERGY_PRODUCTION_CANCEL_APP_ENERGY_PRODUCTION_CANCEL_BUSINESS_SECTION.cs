using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_CANCEL
{
    public partial class APP_ENERGY_PRODUCTION_CANCEL_APP_ENERGY_PRODUCTION_CANCEL_BUSINESS_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_CANCEL_BUSINESS_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ENERGY_PRODUCTION_CANCEL_BUSINESS_SECTION",
                    SectionGroup = "APP_ENERGY_PRODUCTION_CANCEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION_CANCEL,
                    },
                    Ordering = 2,
                    ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_CANCEL",
                    DisplayCondition = CONDITION_APP_ENERGY_PRODUCTION_CANCEL_BUSINESS_SECTION_CANCEL_TYPE_FOR_RENEWABLE_ENERGY(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_CANCEL_BUSINESS_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_CANCEL_BUSINESS_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F38_02_03",
                            Control = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_PRODUCE_FOR",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_PRODUCE_FOR",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_PRODUCE_FOR_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "EMERGENCY", RadioButtonText = "ใช้สารองฉุกเฉิน" },
                                    new FormRadioButton() { RadioButtonValue = "STANDALONE", RadioButtonText = "ใช้แบบแยกเดี่ยว (Stand Alone)" },
                                }
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_CANCEL",
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
