
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_RENEW
{
    public partial class APP_HOSPITAL_PERMISSION_RENEW_APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_RENEW",
                    Type = SectionType.Form,
					Info = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION_INFO",
					DefaultShowInfo = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_RENEW,
                    },
					Ordering = 1,
					DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                });

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION",
                    SectionGroup = "APP_HOSPITAL_BUSINESS_RENEW",
                    Type = SectionType.Form,
                    Info = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION_INFO",
                    DefaultShowInfo = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_BUSINESS_RENEW
                    },
                    Ordering = 1,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_02",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION_TYPE",
                            Type = ControlType.Label,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_OFFICEID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_OFFICEID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_03",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            DataFormat = "dd/MM/yyyy",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                            {
                                EndDate = "0d",
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_04",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION_RENEW_TIME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION_RENEW_TIME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 8,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "47_02_05",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION_OPARETION_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION_OPARETION_CONFIRM",
                            Info = "APP_HOSPITAL_PERMISSION_RENEW_RENEW_SECTION_OPARETION_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "OPARETION_CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
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
