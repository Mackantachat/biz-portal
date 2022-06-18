
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_EDIT
{
    public partial class APP_CLINIC_EDIT_APP_CLINIC_EDIT_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_EDIT_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_EDIT_INFO_SECTION",
                    SectionGroup = "APP_CLINIC_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC_EDIT,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_EDIT_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_EDIT_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F47_01_01",
                            Control = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "PURPOSE_CHANGE_DETAIL", /* เปลี่ยนแปลงรายการใบอนุญาตให้ประกอบกิจการสถานพยาบาลและใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่ไม่รับผู้ป่วยค้างคืน) */
                                "PURPOSE_CHANGE_OPERATOR", /* เปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (ประเภทที่ไม่รับผู้ป่วยค้างคืน) */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
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
