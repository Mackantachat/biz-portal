
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_EDIT
{
    public partial class APP_ELECTRONIC_COMMERCIAL_EDIT_APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_STOCK_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_STOCK_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_STOCK_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_EDIT,
                    },
                    IdentityTypes = new UserTypeEnum[] {
                        UserTypeEnum.Juristic,
                    },
					Ordering = 23,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_STOCK_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_STOCK_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F37_03_60",
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_STOCK_SECTION_CHECKBOX_EDIT",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_EDIT_STOCK_SECTION_CHECKBOX_EDIT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "EDIT_STOCK_SECTION_CHECKED", /* แก้ไขข้อมูลมูลค่าหุ้น */
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
