
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_LAW_OFFICE_EDIT
{
    public partial class APP_LAW_OFFICE_EDIT_APP_LAW_OFFICE_EDIT_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_LAW_OFFICE_EDIT_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_LAW_OFFICE_EDIT_INFO_SECTION",
                    SectionGroup = "APP_LAW_OFFICE_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_LAW_OFFICE_EDIT,
                    },
					Ordering = 1,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_LAW_OFFICE_EDIT_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_LAW_OFFICE_EDIT_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F36_03_01",
                            Control = "APP_LAW_OFFICE_EDIT_INFO_SECTION_POSITION",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_LAW_OFFICE_EDIT_INFO_SECTION_POSITION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_POSITION_OWNER", Text = "เจ้าของ" },
                                new Select2Opt() { ID = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_POSITION_CHIEF", Text = "หัวหน้าสำนักงาน" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_LAW_OFFICE_EDIT_INFO_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F36_03_02",
                            Control = "APP_LAW_OFFICE_EDIT_INFO_SECTION_OFFICE_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_INFO_SECTION_OFFICE_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Textbox_Rows = 1,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_03",
                            Control = "APP_LAW_OFFICE_EDIT_INFO_SECTION_OFFICE_LICENSE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_INFO_SECTION_OFFICE_LICENSE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Textbox_Rows = 1,
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
