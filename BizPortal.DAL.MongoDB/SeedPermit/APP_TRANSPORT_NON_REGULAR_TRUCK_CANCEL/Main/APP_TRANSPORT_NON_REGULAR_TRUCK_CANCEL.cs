
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL,
                    },
					Ordering = 3904,
					ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL_APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL_INFO_SECTION.Init();
        }
    }
}
