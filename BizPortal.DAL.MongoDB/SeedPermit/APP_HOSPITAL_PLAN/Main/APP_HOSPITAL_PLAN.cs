
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PLAN
{
    public partial class APP_HOSPITAL_PLAN
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_HOSPITAL_PLAN").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_HOSPITAL_PLAN",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL,
                    },
					Ordering = 3502,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN"
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            // Init Section
            APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_INFO_SECTION.Init();
            APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_HEADER.Init();
            APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION.Init();
            APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION.Init();
            APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION.Init();
            APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_GOV.Init();
            APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_ARR_GOV.Init();
            APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION.Init();
            APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_PERSONNEL_SECTION.Init();
            APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION.Init();
            //APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_CONFIRM_SIGNATURE.Init();
            APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_TIME.Init();
        }
    }
}
