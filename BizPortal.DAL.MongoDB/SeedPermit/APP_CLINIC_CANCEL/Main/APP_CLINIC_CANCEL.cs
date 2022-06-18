
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_CLINIC_CANCEL
{
    public partial class APP_CLINIC_CANCEL
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_CLINIC_CANCEL").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_CLINIC_CANCEL",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC_CANCEL,
                    },
					Ordering = 4241,
					ResourceName = "PermitResource.RESOURCE_APP_CLINIC_CANCEL"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_CLINIC_CANCEL_APP_CLINIC_CANCEL_INFO_SECTION.Init();
        }
    }
}
