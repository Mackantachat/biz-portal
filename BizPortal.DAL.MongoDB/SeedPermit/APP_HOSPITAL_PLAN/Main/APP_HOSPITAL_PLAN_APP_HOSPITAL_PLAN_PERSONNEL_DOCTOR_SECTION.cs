
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PLAN
{
    public partial class APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION",
                    SectionGroup = "APP_HOSPITAL_PLAN",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_EMPTY",
                    AddButtonText = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_ADD",
                    SubmitButtonText = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_SUBMIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL,
                    },
					Ordering = 2,
					HideSectionHeader = true,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_31",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_POSITION",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_POSITION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_POSITION_OWNER", Text = "ผู้ดำเนินการสถานพยาบาล" },
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_POSITION_DIRECTOR", Text = "ผู้อำนวยการฝ่ายการแพทย์" },
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_POSITION_NURSE", Text = "ผู้อำนวยการฝ่ายการพยาบาล" },
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_POSITION_OTHER", Text = "อื่นๆ" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_32",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_OTHER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_33",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 2,
                            Select2Opts = DROPDOWN_APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_34",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_FIRSTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_FIRSTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_35",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_LASTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_36",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_ID_CARD",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_DOCTOR_SECTION_ID_CARD",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
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
