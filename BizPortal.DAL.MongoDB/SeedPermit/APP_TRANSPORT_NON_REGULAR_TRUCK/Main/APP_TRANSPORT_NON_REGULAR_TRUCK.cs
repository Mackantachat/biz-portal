
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_TRANSPORT_NON_REGULAR_TRUCK").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK,
                    },
					Ordering = 3901,
					ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION.Init();
            APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION.Init();
            APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION.Init();
            APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2.Init();
            APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION.Init();
            APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_TRANFER_PLACE_SECTION.Init();
            APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_PLACE_SECTION.Init();
            APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2.Init();
        }
    }
}
