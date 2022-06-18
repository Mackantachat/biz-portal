using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PLAN
{
    public partial class APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_TIME
    {

        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_BUSSINESS_TIME").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_TIME",
                    SectionGroup = "APP_HOSPITAL_PLAN",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL,
                    },
                    HideSectionHeader = true,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                    Ordering = 10,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_BUSSINESS_TIME").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_TIME",
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_YEAR_PLAN",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_YEAR_PLAN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }

    }
}
