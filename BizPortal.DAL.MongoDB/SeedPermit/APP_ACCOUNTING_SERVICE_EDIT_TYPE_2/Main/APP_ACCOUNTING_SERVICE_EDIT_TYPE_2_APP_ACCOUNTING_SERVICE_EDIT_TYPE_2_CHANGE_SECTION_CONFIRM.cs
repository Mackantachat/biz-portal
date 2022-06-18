
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2
{
    public partial class APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_CHANGE_SECTION_CONFIRM
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_CHANGE_SECTION_CONFIRM").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_CHANGE_SECTION_CONFIRM",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2,
                    },
					Ordering = 20,
					HideSectionHeader = true,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_CHANGE_SECTION_CONFIRM").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_CHANGE_SECTION_CONFIRM",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F43_03_69",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_CHANGE_SECTION_CONFIRM_CHANGE_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_CHANGE_SECTION_CONFIRM_CHANGE_CONFIRM",
                            Info = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_CHANGE_SECTION_CONFIRM_CHANGE_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "CHANGE_CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่าข้อมูลต่างๆ ที่ได้ยื่นต่อสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์เป็นข้อมูลที่ถูกต้องครบถ้วน และข้าพเจ้ายินดีที่จะแสดงหลักฐานและข้อมูลอื่นใดที่เกี่ยวข้องกับการปฎิบัติงานของผู้สอบบัญชีในสังกัด รวมทั้งระบบควบคุมคุณภาพสำนักงาน และยินยอมให้ผู้ที่ได้รับมอบหมายจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ เข้าเยี่ยมและตรวจสอบระบบควบคุมคุณภาพสำนักงาน ตลอดจนเรียกข้าพเจ้ามาให้ถ้อยคำ หรือชี้แจง หรือ ทำคำชี้แจงเป็นหนังสือ หรือ ส่งมอบเอกสารหลักฐานอื่นใดที่เกี่ยวข้องกับการปฎิบัติงานสอบบัญชีของผู้สอบบัญชีในสังกัด และระบบควบคุมคุณภาพสำนักงาน เพื่อประโยชน์ในการกำกับดูแลการปฎิบัติงานให้เป็นไปตามมาตราฐานการสอบบัญชีและมาตราฐานการควบคุมคุณภาพฉบับที่ 1 */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_EDIT_TYPE_2",
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
