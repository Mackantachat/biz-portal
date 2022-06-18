
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_CLINIC_OWNED
{
    public partial class APP_CLINIC_OWNED
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_CLINIC_OWNED").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_CLINIC_OWNED",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC,
                        AppSystemNameTextConst.APP_CLINIC_OPERATION
                    },
					Ordering = 4204,
					ResourceName = "PermitResource.RESOURCE_APP_CLINIC_OWNED"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_CLINIC_OPERATION_SECA.Init(); 
            APP_CLINIC_OWNED_APP_CLINIC_OWNED_OPARETOR_SECTION.Init();
            APP_CLINIC_OWNED_APP_CLINIC_OWNED_CONFIRM_SECTION.Init();
        }
    }
}
