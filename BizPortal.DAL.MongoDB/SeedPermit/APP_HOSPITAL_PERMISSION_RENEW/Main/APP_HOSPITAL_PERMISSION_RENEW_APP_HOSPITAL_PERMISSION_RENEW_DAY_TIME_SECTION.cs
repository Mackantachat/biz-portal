
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_RENEW
{
    public partial class APP_HOSPITAL_PERMISSION_RENEW_APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_RENEW",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION_EMPTY",
                    AddButtonText = "APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION_ADD",
                    SubmitButtonText = "APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION_SUBMIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_RENEW,
                    },
					Ordering = 3,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_19",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION_DAY",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION_DAY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
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
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_20",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION_OPENING_TIME",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION_OPENING_TIME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION_OPENING_TIME(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_21",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION_CLOSING_TIME",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION_CLOSING_TIME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_HOSPITAL_PERMISSION_RENEW_DAY_TIME_SECTION_CLOSING_TIME(),
                        	WillTriggerDisplayOfOtherUI = true,
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
