
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION
{
    public partial class APP_ENERGY_PRODUCTION_APP_ENERGY_PRODUCTION_CONTACT_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_CONTACT_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ENERGY_PRODUCTION_CONTACT_SECTION",
                    SectionGroup = "APP_ENERGY_PRODUCTION",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                    },
                    Ordering = 6,
                    ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_CONTACT_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_CONTACT_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_TITLE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            PlaceholderText = "คำนำหน้า",
                            ColFixed = 2,
                            IdentityTypes = new UserTypeEnum[] {
                                    UserTypeEnum.Juristic,
                                    UserTypeEnum.Citizen,
                            },
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[] {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                            },
                            IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/ERC/Prefix",
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            Control = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_NAME",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 3,
                            IdentityTypes = new UserTypeEnum[] {
                                    UserTypeEnum.Juristic,
                                    UserTypeEnum.Citizen,
                            },
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[] {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            Control = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_LASTNAME",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 3,
                            IdentityTypes = new UserTypeEnum[] {
                                    UserTypeEnum.Juristic,
                                    UserTypeEnum.Citizen,
                            },
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[] {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            Control = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_ID_CARD",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_ID_CARD",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",
                            IdentityTypes = new UserTypeEnum[] {
                                    UserTypeEnum.Juristic,
                                    UserTypeEnum.Citizen,
                            },
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[] {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            Control = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_MOBILE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_MOBILE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 3,
                            IdentityTypes = new UserTypeEnum[] {
                                    UserTypeEnum.Juristic,
                                    UserTypeEnum.Citizen,
                            },
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[] {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            Control = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_TEL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_TEL",
                            ColFixed = 3,
                            IdentityTypes = new UserTypeEnum[] {
                                    UserTypeEnum.Juristic,
                                    UserTypeEnum.Citizen,
                            },
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[] {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            Control = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_FAX",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_FAX",
                            ColFixed = 3,
                            IdentityTypes = new UserTypeEnum[] {
                                    UserTypeEnum.Juristic,
                                    UserTypeEnum.Citizen,
                            },
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[] {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                        new FormControl()
                        {
                            Control = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_EMAIL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_EMAIL",
                            ColFixed = 3,
                            IdentityTypes = new UserTypeEnum[] {
                                    UserTypeEnum.Juristic,
                                    UserTypeEnum.Citizen,
                            },
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[] {
                                AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
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
