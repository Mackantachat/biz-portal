
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_HEALTH_EDIT_SEARCH_GROUP
{
    public partial class APP_HEALTH_EDIT_SEARCH_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HEALTH_EDIT_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HEALTH_EDIT_SEARCH_SECTION",
                    SectionGroup = "APP_HEALTH_EDIT_SEARCH_SECTION_GROUP",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HEALTH_EDIT,
                    },
                    Ordering = 1,
                    ResourceName = "PermitResource.RESOURCE_APP_HEALTH_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HEALTH_EDIT_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HEALTH_EDIT_SEARCH_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            //FieldID = "FS37_03_03",
                            Control = "APP_HEALTH_DOC_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HEALTH_DOC_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_HEALTH_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HEALTH_EDIT_SEARCH_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            //FieldID = "F37_03_04",
                            Control = "APP_HEALTH_EDIT_SEARCH_BTN",
                            Type = ControlType.Literal,
                            DataKey = "APP_HEALTH_EDIT_SEARCH_BTN",
                            ColFixed = 2,
                            LiteralContent = "<label>&nbsp;</label><button name=\"APP_HEALTH_EDIT_SEARCH_BTN\" type=\"button\" class=\"btn btn-primary\">ค้นหาข้อมูล</button>",
                            HideLabel = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HEALTH_EDIT",
                        }
                    }
                }); ;
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }

    }
}
