
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION
{
    public partial class APP_HOSPITAL_PERMISSION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_HOSPITAL_PERMISSION").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_HOSPITAL_PERMISSION",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION,
                        AppSystemNameTextConst.APP_HOSPITAL_BUSINESS
                    },
					Ordering = 4701,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_HOSPITAL_PERMISSION_APP_HOSPITAL_PERMISSION_INFO_SECTION.Init();
        }
    }
}
