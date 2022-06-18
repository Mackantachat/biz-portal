
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_EDIT
{
    public partial class APP_ELECTRONIC_COMMERCIAL_EDIT_APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_EDIT,
                    },
                    IdentityTypes = new UserTypeEnum[] {
                        UserTypeEnum.Juristic,
                    },
					Ordering = 19,
                    DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_SECTION(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F37_03_39",
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_SECTION_CHECKBOX_EDIT",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_PARTNER_SECTION_CHECKBOX_EDIT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "EDIT_PARTNER_SECTION_CHECKED", /* แก้ไขข้อมูลผู้เป็นหุ้นส่วนของห้างหุ้นส่วนสามัญ/ผู้เป็นหุ้นส่วนเข้าใหม่ */
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
