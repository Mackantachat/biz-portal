
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_RENEW
{
    public partial class APP_ENERGY_PRODUCTION_RENEW_APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION",
                    SectionGroup = "APP_ENERGY_PRODUCTION_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION_RENEW,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_RENEW",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F38_03_01",
                            Control = "APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION_REASON",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION_REASON",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ENERGY_PRODUCTION_RENEW_INFO_SECTION_REASON_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "EMERGENCY", RadioButtonText = "?????????????????????????????????????????????" },
                        			new FormRadioButton() { RadioButtonValue = "ALONE", RadioButtonText = "????????????????????????????????????????????? (Stand alone)" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_RENEW",
                        }
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
