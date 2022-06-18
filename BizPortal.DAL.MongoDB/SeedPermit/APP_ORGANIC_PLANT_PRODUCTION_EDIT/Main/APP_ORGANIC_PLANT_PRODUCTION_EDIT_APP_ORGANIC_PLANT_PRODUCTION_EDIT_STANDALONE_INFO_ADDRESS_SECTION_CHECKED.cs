
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_EDIT
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_EDIT_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CHECKED
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CHECKED").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CHECKED",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_EDIT,
                    },
					Ordering = 2,
					DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CHECKED(),
					DisableCondition = DISABLE_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CHECKED(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CHECKED").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CHECKED",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        //new FormControl()
                        //{
                        //    FieldID = "F49_03_02",
                        //    Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CHECKED_STANDALONE_ADDRESS_EDIT",
                        //    Type = ControlType.CheckBox,
                        //    DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ADDRESS_SECTION_CHECKED_STANDALONE_ADDRESS_EDIT",
                        //    IdentityTypes = new UserTypeEnum[] {
                        //        UserTypeEnum.Juristic,
                        //        UserTypeEnum.Citizen,
                        //    },
                        //    ColFixed = 12,
                        //    CheckboxName = new string[]
                        //    {
                        //        "STANDALONE_ADDRESS_CHECKED", /* แก้ไขข้อมูลที่ตั้งแหล่งผลิต/ที่ตั้งแปลง */
                        //    },
                        //	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        //},
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
