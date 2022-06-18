
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_LICENSE
{
    public partial class APP_CLINIC_LICENSE_APP_CLINIC_LICENSE_DETAIL_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_LICENSE_DETAIL_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_LICENSE_DETAIL_SECTION",
                    SectionGroup = "APP_CLINIC_LICENSE",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_CLINIC_LICENSE_DETAIL_SECTION_EMPTY",
                    AddButtonText = "APP_CLINIC_LICENSE_DETAIL_SECTION_ADD",
                    SubmitButtonText = "APP_CLINIC_LICENSE_DETAIL_SECTION_SUBMIT",
					ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC,
                        AppSystemNameTextConst.APP_CLINIC_BUSINESS
                    },
					Ordering = 2,
					ResourceName = "PermitResource.RESOURCE_APP_CLINIC_LICENSE",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_LICENSE_DETAIL_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_LICENSE_DETAIL_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_03_02",
                            Control = "APP_CLINIC_LICENSE_DETAIL_SECTION_DAY",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_LICENSE_DETAIL_SECTION_DAY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() {
                                    Type = ValidationType.JSExpression,
                                    JSExpression = "return APP_CLINIC_LICENSE_DETAIL_SECTION_vaildateWorkingTimeOverlaping();",
                                    ErrorMessage = "APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OVERLAPPED",
                                },
                            },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "MONDAY", Text = "จันทร์" },
                                new Select2Opt() { ID = "TUESDAY", Text = "อังคาร" },
                                new Select2Opt() { ID = "WEDNESDAY", Text = "พุธ" },
                                new Select2Opt() { ID = "THURSDAY", Text = "พฤหัส" },
                                new Select2Opt() { ID = "FRIDAY", Text = "ศุกร์" },
                                new Select2Opt() { ID = "SATURDAY", Text = "เสาร์" },
                                new Select2Opt() { ID = "SUNDAY", Text = "อาทิตย์" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_LICENSE",
                        },
                        new FormControl()
                        {
                            FieldID = "42_03_03",
                            Control = "APP_CLINIC_LICENSE_DETAIL_SECTION_START_TIME",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_LICENSE_DETAIL_SECTION_START_TIME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                            },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_START_TIME(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_LICENSE",
                        },
                        new FormControl()
                        {
                            FieldID = "42_03_04",
                            Control = "APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OUT",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OUT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() {
                                    Type = ValidationType.JSExpression,
                                    JSExpression = "return APP_CLINIC_LICENSE_DETAIL_SECTION_vaildateWorkingTimeSlot();",
                                    ErrorMessage = "APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OUT_INVALID",
                                }
                            },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OUT(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_LICENSE",
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
