
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_LAW_OFFICE_CANCEL
{
    public partial class APP_LAW_OFFICE_CANCEL
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_LAW_OFFICE_CANCEL").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_LAW_OFFICE_CANCEL",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_LAW_OFFICE_CANCEL,
                    },
					Ordering = 3604,
					ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_CANCEL"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_LAW_OFFICE_CANCEL_APP_LAW_OFFICE_CANCEL_REQUEST_CANCEL_SECTION.Init();
        }
    }
}
