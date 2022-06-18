
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_CANCEL_SEARCH
{
    public partial class APP_ENERGY_PRODUCTION_CANCEL_SEARCH_APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION",
                    SectionGroup = "APP_ENERGY_PRODUCTION_CANCEL_SEARCH",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION_CANCEL,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_CANCEL_SEARCH",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "38_04_01",
                            Control = "APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION_IDENTITY_ID_CITIZEN",
                            Type = ControlType.TextBox,
                            DisplayStaticIfHasData = true,
                            DataKey = "APP_ENERGY_PRODUCTION_CANCEL_SEARCH_IDENTITY_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_CANCEL_SEARCH",
                        },
                        new FormControl()
                        {
                            FieldID = "38_04_02",
                            Control = "APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION_IDENTITY_ID_JURISTIC",
                            Type = ControlType.TextBox,
                            DisplayStaticIfHasData = true,
                            DataKey = "APP_ENERGY_PRODUCTION_CANCEL_SEARCH_IDENTITY_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_CANCEL_SEARCH",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "38_04_03",
                            Control = "APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION_LICENSE_ID",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ENERGY_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION_LICENSE_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/DEDE/permitsOwnerOptions",
                            AjaxQueryString = "?identity={Identity}",
                            ControlVariables = new ControlVariable[] {
                                new ControlVariable() { Name = "Identity", ControlSelector = "input[name='IDENTITY_ID']", DefaultIfEmpty = "-1", ListenOnChange = false },
                            },
                            PlaceholderText = "เลือกใบอนุญาต",
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_CANCEL_SEARCH",
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
