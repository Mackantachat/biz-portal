
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_RENEW
{
    public partial class APP_HOSPITAL_PERMISSION_RENEW_APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_RENEW,
                    },
					Ordering = 4,
					HideSectionHeader = true,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                });

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION",
                    SectionGroup = "APP_HOSPITAL_OPERATION_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION_RENEW,
                    },
                    Ordering = 4,
                    HideSectionHeader = true,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "47_02_22",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION_RENEW_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION_RENEW_CONFIRM",
                            Info = "APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION_RENEW_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "EMPLOYEE_CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION_RENEW_CONFIRM(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },

                        new FormControl()
                        {
                            FieldID = "47_02_22",
                            Control = "APP_HOSPITAL_OPERATION_RENEW_CF",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_OPERATION_RENEW_CF",
                            Info = "APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION_RENEW_CONFIRM_INFO",
                            DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule()
                                {
                                    Type = ValidationType.Required, ErrorMessage = "* Required"
                                }
                            },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "EMPLOYEE_CONFIRM", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_HOSPITAL_OPERATION_RENEW
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "47_02_23",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION_EMPLOYEE_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION_EMPLOYEE_CONFIRM",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "EMPLOYEE_OWNED_CONFIRM_TRUE", /* ข้าพเจ้ายินยอมให้บุคคลดังกล่าวข้างต้น เป็นผู้ดำเนินการสถานพยาบาลแห่งนี้ */
                            },
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_RENEW_CONFIRM_SECTION_EMPLOYEE_CONFIRM(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
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
