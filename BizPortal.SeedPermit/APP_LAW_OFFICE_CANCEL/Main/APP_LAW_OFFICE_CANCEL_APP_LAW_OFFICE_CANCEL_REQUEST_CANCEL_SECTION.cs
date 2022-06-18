
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_LAW_OFFICE_CANCEL
{
    public partial class APP_LAW_OFFICE_CANCEL_APP_LAW_OFFICE_CANCEL_REQUEST_CANCEL_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_LAW_OFFICE_CANCEL_REQUEST_CANCEL_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_LAW_OFFICE_CANCEL_REQUEST_CANCEL_SECTION",
                    SectionGroup = "APP_LAW_OFFICE_CANCEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_LAW_OFFICE_CANCEL,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_CANCEL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_LAW_OFFICE_CANCEL_REQUEST_CANCEL_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_LAW_OFFICE_CANCEL_REQUEST_CANCEL_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F36_04_01",
                            Control = "APP_LAW_OFFICE_CANCEL_REQUEST_CANCEL_SECTION_WISHES",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_CANCEL_REQUEST_CANCEL_SECTION_WISHES",
                            Info = "APP_LAW_OFFICE_CANCEL_REQUEST_CANCEL_SECTION_WISHES_INFO",
                        	DefaultShowInfo = true,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_04_02",
                            Control = "APP_LAW_OFFICE_CANCEL_REQUEST_CANCEL_SECTION_SIGNATURE",
                            Type = ControlType.Signature,
                            DataKey = "APP_LAW_OFFICE_CANCEL_REQUEST_CANCEL_SECTION_SIGNATURE",
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_CANCEL",
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
