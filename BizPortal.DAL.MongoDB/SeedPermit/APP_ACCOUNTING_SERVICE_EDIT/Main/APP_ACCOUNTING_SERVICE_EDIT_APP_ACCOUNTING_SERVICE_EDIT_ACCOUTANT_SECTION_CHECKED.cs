
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_EDIT
{
    public partial class APP_ACCOUNTING_SERVICE_EDIT_APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_CHECKED
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_CHECKED").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_CHECKED",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_EDIT,
                    },
					Ordering = 4,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_CHECKED").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_CHECKED",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F43_03_16",
                            Control = "APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_CHECKED_ACCOUNTANT_EDIT_CHECKED",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_CHECKED_ACCOUNTANT_EDIT_CHECKED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                            HideLabel = true,
                            CheckboxName = new string[]
                            {
                                "EDIT_ACCOUNTANT_SECTION", /* แก้ไขข้อมูลรายนามผู้ทำบัญชีที่รับผิดชอบในฐานะผู้ทำบัญชีของนิติบุคคล */
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
