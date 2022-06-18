
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_EDIT_SEARCH
{
    public partial class APP_HOSPITAL_PERMISSION_EDIT_SEARCH
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_HOSPITAL_PERMISSION_EDIT_SEARCH").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDIT_SEARCH",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                    },
					Ordering = 0,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT_SEARCH"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_HOSPITAL_PERMISSION_EDIT_SEARCH_APP_HOSPITAL_PERMISSION_EDIT_SEARCH_SEARCH_SECTION.Init();
        }
    }
}
