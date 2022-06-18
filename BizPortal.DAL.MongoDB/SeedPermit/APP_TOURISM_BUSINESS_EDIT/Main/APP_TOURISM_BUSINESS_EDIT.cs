
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_TOURISM_BUSINESS_EDIT
{
    public partial class APP_TOURISM_BUSINESS_EDIT
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_TOURISM_BUSINESS_EDIT").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_TOURISM_BUSINESS_EDIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TOURISM_BUSINESS_EDIT,
                    },
					Ordering = 4603,
					ResourceName = "PermitResource.RESOURCE_APP_TOURISM_BUSINESS_EDIT"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
        }
    }
}
