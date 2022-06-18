
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_EDIT
{
    public partial class APP_ACCOUNTING_SERVICE_EDIT_APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_EDIT,
                    },
					Ordering = 9,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION(),
					DisableCondition = DISABLE_APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F43_03_37",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION_REASON_FOR_ISSUING",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION_REASON_FOR_ISSUING",
                            Info = "APP_ACCOUNTING_SERVICE_EDIT_NEW_CERTIFICATE_SECTION_REASON_FOR_ISSUING_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "CHANGE_NAME", /* เปลี่ยนชื่อนิติบุคคล */
                                "BROKEN_OR_LOST", /* ชำรุด/สูญหาย */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT",
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
