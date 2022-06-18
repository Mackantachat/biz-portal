
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_EDIT
{
    public partial class APP_ACCOUNTING_SERVICE_EDIT_APP_ACCOUNTING_SERVICE_EDIT_EDIT_SECTION_CONFIRM
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_EDIT_SECTION_CONFIRM").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_EDIT_SECTION_CONFIRM",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_EDIT,
                    },
					Ordering = 12,
					HideSectionHeader = true,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_EDIT_SECTION_CONFIRM").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_EDIT_SECTION_CONFIRM",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F43_03_40",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_EDIT_SECTION_CONFIRM_EDIT_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_EDIT_SECTION_CONFIRM_EDIT_CONFIRM",
                            Info = "APP_ACCOUNTING_SERVICE_EDIT_EDIT_SECTION_CONFIRM_EDIT_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "EDIT_CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่าข้อมูลต่างๆที่ได้ยื่นต่อสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์เป็นข้อมูลที่ถูกต้องครบถ้วน และข้าพเจ้ายินดีแสดงหลักฐานและข้อมูลอื่นใดที่เกี่ยวข้องกับการปฎิบัติงานของผู้สอบบัญชีในสังกัดรวมทั้งระบบควบคุมคุณภาพสำนักงาน และยินยอมให้ผู้ที่ได้รับมอบหมายจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์เข้าเยี่ยมและตรวจสอบระบบควบคุมคุณภาพสำนักงาน ตลอดจนเรียกข้าพเจ้ามาให้ถ้อยคำ หรือชี้แจง หรือทำคำชี้แจงเป็นหนังสือ หรือส่งมอบเอกสารหลักฐานอื่นใดที่เกี่ยวข้องกับการปฏิบัติงานสอบบัญชีของผู้สอบบัญชีในสังกัด และระบบควบคุณคุณภาพสำนักงาน เพื่อประโยชน์ในการกำกับดูแลการปฏิบัติงานให้เป็นไปตามมาตรฐานการสอบบัญชีและมาตรฐานการควบคุมคุณภาพฉบับที่ 1 */
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
