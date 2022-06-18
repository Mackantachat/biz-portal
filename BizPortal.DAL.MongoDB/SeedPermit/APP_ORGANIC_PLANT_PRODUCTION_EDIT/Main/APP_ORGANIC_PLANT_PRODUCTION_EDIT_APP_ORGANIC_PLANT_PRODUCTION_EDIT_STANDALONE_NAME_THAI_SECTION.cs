
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_EDIT
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_EDIT_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_EDIT,
                    },
					Ordering = 9,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_03_25",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION_INFORMATION_NAME_THAI_HEADER",
                            Type = ControlType.Heading,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION_INFORMATION_NAME_THAI_HEADER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION_INFORMATION_NAME_THAI_HEADER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_03_26",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION_INFORMATION_NAME_THAI_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION_INFORMATION_NAME_THAI_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION_INFORMATION_NAME_THAI_TEXT(),
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
