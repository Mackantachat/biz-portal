
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH
{
    public partial class APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_CANCEL,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "FS37_04_01",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_CITIZEN_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_CITIZEN_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	PreFill = true,
                        	DisplayStaticIfHasData = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH",
                        },
                        new FormControl()
                        {
                            FieldID = "FS37_04_02",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_JURISTIC_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_JURISTIC_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	PreFill = true,
                        	DisplayStaticIfHasData = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "FS37_04_03",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_NUMBER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                         
                        CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_SEARCH(),
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
