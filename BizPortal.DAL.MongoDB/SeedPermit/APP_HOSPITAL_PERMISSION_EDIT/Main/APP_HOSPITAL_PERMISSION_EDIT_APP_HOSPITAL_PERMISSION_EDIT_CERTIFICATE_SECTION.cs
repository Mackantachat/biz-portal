
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_EDIT
{
    public partial class APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDIT",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_EMPTY",
                    AddButtonText = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_ADD",
                    SubmitButtonText = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_SUBMIT",
					ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                    },
					Ordering = 8,
					DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION(),
					DisableCondition = DISABLE_APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F47_01_28",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "DIPLOMA", Text = "วุฒิบัตร" },
                                new Select2Opt() { ID = "APPROVAL_LETTER", Text = "หนังสืออนุมัติ" },
                                new Select2Opt() { ID = "CERTIFICATE", Text = "หนังสือรับรอง" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_29",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_BRANCH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_BRANCH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_30",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_NUMBER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_31",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_32",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_EXPIRED_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_CERTIFICATE_SECTION_EXPIRED_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
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
