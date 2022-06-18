
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION
{
    public partial class APP_ENERGY_PRODUCTION_APP_ENERGY_PRODUCTION_INFO_SECTION_CONFIRM
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_INFO_SECTION_CONFIRM").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ENERGY_PRODUCTION_INFO_SECTION_CONFIRM",
                    SectionGroup = "APP_ENERGY_PRODUCTION",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                    },
					Ordering = 7,
					HideSectionHeader = true,
					ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_INFO_SECTION_CONFIRM").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_INFO_SECTION_CONFIRM",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F38_01_21",
                            Control = "APP_ENERGY_PRODUCTION_INFO_SECTION_CONFIRM_COMFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ENERGY_PRODUCTION_INFO_SECTION_CONFIRM_COMFIRM",
                            Info = "APP_ENERGY_PRODUCTION_INFO_SECTION_CONFIRM_COMFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "TRUE", /* ข้าพเจ้าขอรับรองว่าเอกสารแนบและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_INFO_SECTION_CONFIRM",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F38_01_22",
                            Control = "APP_ENERGY_PRODUCTION_INFO_SECTION_CONFIRM_SIGNATURE",
                            Type = ControlType.Signature,
                            DataKey = "APP_ENERGY_PRODUCTION_INFO_SECTION_CONFIRM_SIGNATURE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION",
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
