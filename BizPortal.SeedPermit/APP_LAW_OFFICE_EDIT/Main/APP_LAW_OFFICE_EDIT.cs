
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_LAW_OFFICE_EDIT
{
    public partial class APP_LAW_OFFICE_EDIT
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_LAW_OFFICE_EDIT").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_LAW_OFFICE_EDIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_LAW_OFFICE_EDIT,
                    },
					Ordering = 3603,
					ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_LAW_OFFICE_EDIT_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION.Init();
            APP_LAW_OFFICE_EDIT_APP_LAW_OFFICE_EDIT_LAWYER_JOIN_HEADER_SECTION.Init();
            APP_LAW_OFFICE_EDIT_APP_LAW_OFFICE_EDIT_LAWYER_JOIN_SECTION.Init();
            APP_LAW_OFFICE_EDIT_APP_LAW_OFFICE_EDIT_LAWYER_LEAVE_HEADER_SECTION.Init();
            APP_LAW_OFFICE_EDIT_APP_LAW_OFFICE_EDIT_LAWYER_LEAVE_SECTION.Init();
            APP_LAW_OFFICE_EDIT_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_2.Init();
        }
    }
}
