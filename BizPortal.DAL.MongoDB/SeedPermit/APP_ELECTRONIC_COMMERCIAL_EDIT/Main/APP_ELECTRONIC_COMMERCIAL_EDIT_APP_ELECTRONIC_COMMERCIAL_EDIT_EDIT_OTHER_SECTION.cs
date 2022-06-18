
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_EDIT
{
    public partial class APP_ELECTRONIC_COMMERCIAL_EDIT_APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_OTHER_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_OTHER_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_OTHER_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_EDIT,
                    },
					Ordering = 26,
					DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_OTHER_SECTION(),
					DisableCondition = DISABLE_APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_OTHER_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_OTHER_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_OTHER_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F37_03_66",
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_OTHER_SECTION_CHECKBOX_EDIT",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_OTHER_SECTION_CHECKBOX_EDIT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "EDIT_OTHER_SECTION_CHECKED", /* แก้ไขข้อมูลอื่น ๆ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
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
