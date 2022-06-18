
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_OWNER
{
    public partial class APP_HOSPITAL_PERMISSION_OWNER_APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_OWNER",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION,
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION
                    },
					Ordering = 4,
					HideSectionHeader = true,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "47_02_32",
                            Control = "APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_OWNER_CONFIRM_SECTION_CONFIRM",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "TRUE", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_OWNER",
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
