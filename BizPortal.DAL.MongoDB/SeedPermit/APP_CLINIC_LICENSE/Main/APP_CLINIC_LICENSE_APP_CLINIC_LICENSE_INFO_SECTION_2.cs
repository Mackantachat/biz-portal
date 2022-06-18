
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_LICENSE
{
    public partial class APP_CLINIC_LICENSE_APP_CLINIC_LICENSE_INFO_SECTION_2
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_LICENSE_INFO_SECTION_2").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_LICENSE_INFO_SECTION_2",
                    SectionGroup = "APP_CLINIC_LICENSE",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC,
                        AppSystemNameTextConst.APP_CLINIC_BUSINESS
                    },
					Ordering = 4,
					HideSectionHeader = true,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_LICENSE_INFO_SECTION_2").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_LICENSE_INFO_SECTION_2",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "42_03_08",
                            Control = "APP_CLINIC_LICENSE_INFO_SECTION_2_CONFIRM_INFO_TRUE",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_CLINIC_LICENSE_INFO_SECTION_2_CONFIRM_INFO_TRUE",
                            Info = "APP_CLINIC_LICENSE_INFO_SECTION_2_CONFIRM_INFO_TRUE_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "INFO_CHECKED_TRUE", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
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
