
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_EDIT_SEARCH
{
    public partial class APP_ENERGY_PRODUCTION_EDIT_SEARCH_APP_ENERGY_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ENERGY_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION",
                    SectionGroup = "APP_ENERGY_PRODUCTION_EDIT_SEARCH",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION_EDIT,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_EDIT_SEARCH",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "38_0_01",
                            Control = "APP_ENERGY_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_IDENTITY_ID_CITIZEN",
                            Type = ControlType.TextBox,
                            DisplayStaticIfHasData = true,
                            DataKey = "APP_ENERGY_PRODUCTION_EDIT_SEARCH_IDENTITY_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_EDIT_SEARCH",
                        },
                        new FormControl()
                        {
                            FieldID = "38_0_02",
                            Control = "APP_ENERGY_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_IDENTITY_ID_JURISTIC",
                            Type = ControlType.TextBox,
                            DisplayStaticIfHasData = true,
                            DataKey = "APP_ENERGY_PRODUCTION_EDIT_SEARCH_IDENTITY_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_EDIT_SEARCH",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "38_0_02",
                            Control = "APP_ENERGY_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_LICENSE_ID",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ENERGY_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_LICENSE_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "CHOICE_1", Text = "ใบที่ 1" },
                                new Select2Opt() { ID = "CHOICE_2", Text = "ใบที่ 2" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_EDIT_SEARCH",
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
