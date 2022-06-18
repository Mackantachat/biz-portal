
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_RENEW
{
    public partial class APP_ACCOUNTING_SERVICE_RENEW_APP_ACCOUNTING_SERVICE_RENEW_GUARANTEE_SECTION_2
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_RENEW_GUARANTEE_SECTION_2").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_GUARANTEE_SECTION_2",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW,
                    },
                    IdentityTypes = new UserTypeEnum[] {
                        UserTypeEnum.Juristic,
                    },
					Ordering = 11,
					HideSectionHeader = true,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_RENEW_GUARANTEE_SECTION_2").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_GUARANTEE_SECTION_2",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F43_02_62",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_GUARANTEE_SECTION_2_GUARANTEE_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_GUARANTEE_SECTION_2_GUARANTEE_CONFIRM",
                            Info = "APP_ACCOUNTING_SERVICE_RENEW_GUARANTEE_SECTION_2_GUARANTEE_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "GUARANTEE_CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่าข้อมูลต่างๆ ที่ได้ยื่นต่อสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์เป็นข้อมูลที่ถูกต้องครบถ้วน และข้าพเจ้ายินดีที่จะแสดงหลักฐานและข้อมูลอื่นใดที่เกี่ยวข้องกับการปฎิบัติงานของผู้สอบบัญชีในสังกัด รวมทั้งระบบควบคุมคุณภาพสำนักงาน และยินยอมให้ผู้ที่ได้รับมอบหมายจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ เข้าเยี่ยมและตรวจสอบระบบควบคุมคุณภาพสำนักงาน ตลอดจนเรียกข้าพเจ้ามาให้ถ้อยคำ หรือชี้แจง หรือ ทำคำชี้แจงเป็นหนังสือ หรือ ส่งมอบเอกสารหลักฐานอื่นใดที่เกี่ยวข้องกับการปฎิบัติงานสอบบัญชีของผู้สอบบัญชีในสังกัด และระบบควบคุมคุณภาพสำนักงาน เพื่อประโยชน์ในการกำกับดูแลการปฎิบัติงานให้เป็นไปตามมาตราฐานการสอบบัญชีและมาตราฐานการควบคุมคุณภาพฉบับที่ 1 */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
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
