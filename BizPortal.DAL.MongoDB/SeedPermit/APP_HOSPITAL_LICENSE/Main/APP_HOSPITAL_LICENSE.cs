
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_HOSPITAL_LICENSE
{
    public partial class APP_HOSPITAL_LICENSE
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_HOSPITAL_LICENSE").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_HOSPITAL_LICENSE",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL,
                    },
					Ordering = 3501,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_LICENSE"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_HOSPITAL_LICENSE_APP_HOSPITAL_LICENSE_INFO_SECTION.Init();
        }
    }
}
