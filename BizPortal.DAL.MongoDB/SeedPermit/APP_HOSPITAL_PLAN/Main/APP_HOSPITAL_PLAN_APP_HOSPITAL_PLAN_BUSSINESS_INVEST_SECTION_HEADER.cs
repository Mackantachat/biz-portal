
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PLAN
{
    public partial class APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_HEADER
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_HEADER").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_HEADER",
                    SectionGroup = "APP_HOSPITAL_PLAN",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL,
                    },
					Ordering = 2,
					HideSectionHeader = true,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            InitFormSectionRow();
        }

        private static void InitFormSectionRow()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_HEADER").Count() == 0)
            {
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
    }
}
