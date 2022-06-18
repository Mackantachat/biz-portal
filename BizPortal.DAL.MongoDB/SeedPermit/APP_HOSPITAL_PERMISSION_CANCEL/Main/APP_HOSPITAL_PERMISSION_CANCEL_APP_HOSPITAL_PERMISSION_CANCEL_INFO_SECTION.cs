
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_CANCEL
{
    public partial class APP_HOSPITAL_PERMISSION_CANCEL_APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_CANCEL",
                    Type = SectionType.Form,
					Info = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION_INFO",
					DefaultShowInfo = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_CANCEL,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_CANCEL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_04_01",
                            Control = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION_TYPE",
                            Type = ControlType.Label,
                            DataKey = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "42_04_02",
                            Control = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION_LICENSING_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION_LICENSING_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                            {
                                EndDate = "0d",
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_CANCEL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_04_03",
                            Control = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION_REASON",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION_REASON",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            Textbox_Rows = 5,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_CANCEL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_04_04",
                            Control = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_CANCEL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "42_04_05",
                            Control = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION_CONFIRM_PERMISSION",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION_CONFIRM_PERMISSION",
                            Info = "APP_HOSPITAL_PERMISSION_CANCEL_INFO_SECTION_CONFIRM_PERMISSION_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "HOSPITAL_TRUE", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_CANCEL",
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
