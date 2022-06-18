
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_EDIT
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_EDIT_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_EDIT,
                    },
					Ordering = 14,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_03_45",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_TITLE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_03_46",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_FIRSTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_FIRSTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_03_47",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_LASTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_03_48",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_CITIZEN_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_CITIZEN_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                         
                        CUSTOM_FORM_CONTROL_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRESIDENT_INFO_SECTION_PRESIDENT_ADDERSS(),
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
