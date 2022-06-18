
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_LAW_OFFICE_EDIT
{
    public partial class APP_LAW_OFFICE_EDIT_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2",
                    SectionGroup = "APP_LAW_OFFICE_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_LAW_OFFICE_EDIT,
                    },
					Ordering = 6,
 
					HideSectionHeader = true,
					ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F36_03_46",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2_OTHER",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F36_03_47",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2_CONFIRM",
                            Info = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2_CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่า ข้อมูลที่แจ้งเป็นความจริงทุกประการ */
                            },
                            CheckboxConfigs = new FormControl.CheckboxConfig
                            {
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_48",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2_SIGNATURE",
                            Type = ControlType.Signature,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2_SIGNATURE",
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
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
