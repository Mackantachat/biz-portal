
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT,
                    },
					Ordering = 3903,
					ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_INFO_SECTION.Init();
        }
    }
}
