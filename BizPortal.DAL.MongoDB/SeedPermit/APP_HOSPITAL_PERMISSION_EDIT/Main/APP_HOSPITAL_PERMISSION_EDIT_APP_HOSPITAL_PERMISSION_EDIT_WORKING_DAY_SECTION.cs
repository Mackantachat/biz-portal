
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_EDIT
{
    public partial class APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION").Count() == 0)
            {
                //           items.Add(new FormSection()
                //           {
                //               Section = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION",
                //               SectionGroup = "APP_HOSPITAL_PERMISSION_EDIT",
                //               Type = SectionType.ArrayOfForms,
                //               EmptyDataMessage = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_EMPTY",
                //               AddButtonText = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_ADD",
                //               SubmitButtonText = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_SUBMIT",
                //ArrayRequiredAtLeast = true,
                //               NumberRequiredAtLeast = 1,
                //               ShowOnSpecificApps = true,
                //               AppSystemNames = new string[] {
                //                   AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                //               },
                //Ordering = 4,
                //HideSectionHeader = true,
                //DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION(),
                //DisableCondition = DISABLE_APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION(),
                //ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                //           });

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITA",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_EMPTY",
                    AddButtonText = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_ADD",
                    SubmitButtonText = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_CLINIC_BUSINESS_EDIT,
                        AppSystemNameTextConst.APP_HOSPITAL_BUSINESS_EDIT,
                    },
                    Ordering = 4,
                    HideSectionHeader = true,
                    DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION(),
                    //DisableCondition = DISABLE_APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F47_01_10",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_DAY",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_DAY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
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
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_11",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_START_TIME",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_START_TIME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_START_TIME(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_12",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_END_TIME",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_END_TIME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_HOSPITAL_PERMISSION_EDIT_WORKING_DAY_SECTION_END_TIME(),
                        	WillTriggerDisplayOfOtherUI = true,
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
