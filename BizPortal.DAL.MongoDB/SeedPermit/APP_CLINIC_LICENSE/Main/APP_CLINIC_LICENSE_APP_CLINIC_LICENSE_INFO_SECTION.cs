
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_LICENSE
{
    public partial class APP_CLINIC_LICENSE_APP_CLINIC_LICENSE_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_LICENSE_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_LICENSE_INFO_SECTION",
                    SectionGroup = "APP_CLINIC_LICENSE",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC,
                        AppSystemNameTextConst.APP_CLINIC_BUSINESS
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_CLINIC_LICENSE",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_LICENSE_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_LICENSE_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_03_01",
                            Control = "APP_CLINIC_LICENSE_INFO_SECTION_TYPE",
                            Type = ControlType.Label,
                            DataKey = "APP_CLINIC_LICENSE_INFO_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_LICENSE",
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_LICENSE_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_CLINIC_LICENSE_OLD_INFO_SECTION_TYPE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_LICENSE_OLD_INFO_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_LICENSE",
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
