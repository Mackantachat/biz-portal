
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION",
                    SectionGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_03_01",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION_DOCUMENT_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION_DOCUMENT_NUMBER",
                            Info = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION_DOCUMENT_NUMBER_INFO",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_03_02",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION_PURPOSE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION_PURPOSE",
                            Info = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION_PURPOSE_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_03_03",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION_SIGNATURE",
                            Type = ControlType.Signature,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION_SIGNATURE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT",
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
