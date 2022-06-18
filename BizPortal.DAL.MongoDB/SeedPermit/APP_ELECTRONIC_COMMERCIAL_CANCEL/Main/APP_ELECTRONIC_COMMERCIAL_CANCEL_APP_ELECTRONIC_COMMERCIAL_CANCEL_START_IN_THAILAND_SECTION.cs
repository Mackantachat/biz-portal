
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_CANCEL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_CANCEL_APP_ELECTRONIC_COMMERCIAL_CANCEL_START_IN_THAILAND_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_START_IN_THAILAND_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_START_IN_THAILAND_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL_CANCEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_CANCEL,
                    },
					Ordering = 5,
					ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_START_IN_THAILAND_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_START_IN_THAILAND_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_04_12",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_START_IN_THAILAND_SECTION_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_START_IN_THAILAND_SECTION_DATE",
                            Info = "APP_ELECTRONIC_COMMERCIAL_CANCEL_START_IN_THAILAND_SECTION_DATE_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
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
