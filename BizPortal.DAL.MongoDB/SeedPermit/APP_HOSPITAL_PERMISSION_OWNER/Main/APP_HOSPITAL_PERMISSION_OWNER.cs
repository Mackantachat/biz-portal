
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_OWNER
{
    public partial class APP_HOSPITAL_PERMISSION_OWNER
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_HOSPITAL_PERMISSION_OWNER").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_HOSPITAL_PERMISSION_OWNER",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION,
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION
                    },
					Ordering = 4702,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_HOSPITAL_PERMISSION_OWNER_APP_HOSPITAL_PERMISSION_OWNER_OPARETOR_SECTION.Init();
            APP_HOSPITAL_PERMISSION_OWNER_APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION.Init();
            APP_HOSPITAL_OPERATION_SECA.Init();
        }
    }
}
