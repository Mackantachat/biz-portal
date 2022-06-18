
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_CANCEL
{
    public partial class APP_ACCOUNTING_SERVICE_CANCEL_APP_ACCOUNTING_SERVICE_CANCEL_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_CANCEL_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_CANCEL_INFO_SECTION",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_CANCEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_CANCEL,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_CANCEL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_CANCEL_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_CANCEL_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_04_01",
                            Control = "APP_ACCOUNTING_SERVICE_CANCEL_INFO_SECTION_REASON_CANCEL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_CANCEL_INFO_SECTION_REASON_CANCEL",
                            Info = "APP_ACCOUNTING_SERVICE_CANCEL_INFO_SECTION_REASON_CANCEL_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_CANCEL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_CANCEL_INFO_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F43_04_02",
                            Control = "APP_ACCOUNTING_SERVICE_CANCEL_INFO_SECTION_CANCEL_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_CANCEL_INFO_SECTION_CANCEL_CONFIRM",
                            Info = "APP_ACCOUNTING_SERVICE_CANCEL_INFO_SECTION_CANCEL_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "CANCEL_CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่าข้อมูลต่างๆที่ได้ยื่นต่อสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์เป็นข้อมูลที่ถูกต้องครบถ้วน และข้าพเจ้ายินดีแสดงหลักฐานและข้อมูลอื่นใดที่เกี่ยวข้องกับการปฎิบัติงานของผู้สอบบัญชีในสังกัดรวมทั้งระบบควบคุมคุณภาพสำนักงาน และยินยอมให้ผู้ที่ได้รับมอบหมายจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์เข้าเยี่ยมและตรวจสอบระบบควบคุมคุณภาพสำนักงาน ตลอดจนเรียกข้าพเจ้ามาให้ถ้อยคำ หรือชี้แจง หรือทำคำชี้แจงเป็นหนังสือ หรือส่งมอบเอกสารหลักฐานอื่นใดที่เกี่ยวข้องกับการปฏิบัติงานสอบบัญชีของผู้สอบบัญชีในสังกัด และระบบควบคุณคุณภาพสำนักงาน เพื่อประโยชน์ในการกำกับดูแลการปฏิบัติงานให้เป็นไปตามมาตรฐานการสอบบัญชีและมาตรฐานการควบคุมคุณภาพฉบับที่ 1 */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_CANCEL",
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
