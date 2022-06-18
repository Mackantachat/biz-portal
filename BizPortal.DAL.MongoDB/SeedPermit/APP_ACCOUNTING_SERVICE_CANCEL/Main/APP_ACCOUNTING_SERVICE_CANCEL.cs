
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_CANCEL
{
    public partial class APP_ACCOUNTING_SERVICE_CANCEL
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_ACCOUNTING_SERVICE_CANCEL").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_ACCOUNTING_SERVICE_CANCEL",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_CANCEL,
                    },
					Ordering = 4341,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_CANCEL"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_ACCOUNTING_SERVICE_CANCEL_APP_ACCOUNTING_SERVICE_CANCEL_INFO_SECTION.Init();
        }
    }
}
