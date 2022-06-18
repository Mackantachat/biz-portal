using System;
using System.Collections.Generic;
using System.Linq;
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using MongoDB.Driver;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE
{
    partial class APP_ACCOUNTING_SERVICE_APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE",
                    SectionGroup = "APP_ACCOUNTING_SERVICE",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE,
                    },
                    IdentityTypes = new UserTypeEnum[] {
                        UserTypeEnum.Juristic
                    },
                    Ordering = 6,
                    ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE",
                    Info = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_INFO",
                    DefaultShowInfo = true,
                    DisplayCondition = new FormControlDisplayCondition
                    {
                        IsOr = true,
                        Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                        {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_ACCOUNTING_SERVICE_INFO_SECTION_TYPE",
                                ControlAnswer = "AUDITORING",
                            },
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_ACCOUNTING_SERVICE_INFO_SECTION_TYPE",
                                ControlAnswer = "BOTH",
                            },
                        },
                    },
                    DisableCondition = new FormControlDisableCondition
                    {
                        Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                        {
                            new FormControlDisableCondition.ControlWithAnswer
                            {
                                ControlName = "APP_ACCOUNTING_SERVICE_INFO_SECTION_TYPE",
                                ControlAnswer = "ACCOUNTING",
                            },
                        },
                    },
                    ValidationRules = new FormValidationRule[]
                    {
                        new FormValidationRule() {
                            Type = ValidationType.JSExpression,
                            JSExpression = "return APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_VALIDATE();",
                            ErrorMessage = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_VALIDATE_EXCEED"
                        },
                        new FormValidationRule() {
                            Type = ValidationType.JSExpression,
                            JSExpression = "return APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_LICENSE_VALIDATE();",
                            ErrorMessage = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_LICENSE_VALIDATE_EXCEED"
                        },
                        new FormValidationRule() {
                            Type = ValidationType.JSExpression,
                            JSExpression = "return APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_FULL_VALIDATE();",
                            ErrorMessage = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_FULL_VALIDATE_EXCEED"
                        },
                    },
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_01_41",
                            Control = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_LICENSE",
                            Type = ControlType.DataTable,
                            DataKey = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_LICENSE",
                            HideLabel = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE",                            
                            DataTableColumns = new DataTableColumn[]
                            {
                                new DataTableColumn()
                                {
                                    Name = "TITLE",
                                    Title = "คำนำหน้า",
                                    CustomColFixed = 2,
                                    IsReadOnly = true,
                                    Control = new FormControl()
                                    {
                                        Control = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_LICENSE_TITLE",
                                        Type = ControlType.TextBox,
                                        DataKey = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_LICENSE_TITLE",
                                    },
                                },
                                new DataTableColumn()
                                {
                                    Name = "NAME",
                                    Title = "ชื่อ",
                                    CustomColFixed = 3,
                                    IsReadOnly = true,
                                    Control = new FormControl()
                                    {
                                        Control = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_LICENSE_NAME",
                                        Type = ControlType.TextBox,
                                        DataKey = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_LICENSE_NAME",
                                    },
                                },
                                new DataTableColumn()
                                {
                                    Name = "LASTNAME",
                                    Title = "นามสกุล",
                                    CustomColFixed = 3,
                                    IsReadOnly = true,
                                    Control = new FormControl()
                                    {
                                        Control = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_LICENSE_LASTNAME",
                                        Type = ControlType.TextBox,
                                        DataKey = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_LICENSE_LASTNAME",
                                    },
                                },
                                new DataTableColumn()
                                {
                                    Name = "LICENSE",
                                    Title = "เลขทะเบียนผู้สอบบัญชีรับอนุญาต",
                                    CustomColFixed = 4,
                                    Control = new FormControl()
                                    {
                                        Control = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_LICENSE_LICENSE",
                                        Type = ControlType.TextBox,
                                        DataKey = "APP_ACCOUNTING_SERVICE_INFO_SECTION_COMMITTEE_LICENSE_LICENSE",
                                        DisplayMaskInput = true,
                                        MaskInputPattern = "0#",
                                    },
                                },
                                new DataTableColumn()
                                {
                                    Name = "TYPE",
                                    Title = "ประเภทสมาชิก",
                                    CustomColFixed = 5,
                                    Control = new FormControl()
                                    {
                                        Control = "JURISTIC_COMMITTEE_ACCOUNTING_TYPE",
                                        Type = ControlType.Dropdown,
                                        DataKey = "JURISTIC_COMMITTEE_ACCOUNTING_TYPE",
                                        Select2Opts = new ViewModels.Select2.Select2Opt[]
                                        {
                                            new ViewModels.Select2.Select2Opt() { ID = "01", Text = "สมาชิกสามัญ" },
                                            new ViewModels.Select2.Select2Opt() { ID = "02", Text = "สมาชิกวิสามัญ" },
                                            new ViewModels.Select2.Select2Opt() { ID = "03", Text = "สมาชิกสมทบ" },
                                        },
                                    },
                                },
                                new DataTableColumn()
                                {
                                    Name = "DUE_DATE",
                                    Title = "ชำระค่าบำรุงสมาชิกถึงวันที่",
                                    CustomColFixed = 5,
                                    Control = new FormControl()
                                    {
                                        Control = "JURISTIC_COMMITTEE_ACCOUNTING_DUE_DATE",
                                        Type = ControlType.DatePicker,
                                        DataKey = "JURISTIC_COMMITTEE_ACCOUNTING_DUE_DATE",
                                    },
                                },
                            },
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
