
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH
{
    public partial class APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH_SEARCH_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH_SEARCH_SECTION",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH_SEARCH_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "FS43_02_01",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH_SEARCH_SECTION_ID_CARD",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH_SEARCH_SECTION_ID_CARD",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	PreFill = true,
                        	DisplayStaticIfHasData = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH",
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
