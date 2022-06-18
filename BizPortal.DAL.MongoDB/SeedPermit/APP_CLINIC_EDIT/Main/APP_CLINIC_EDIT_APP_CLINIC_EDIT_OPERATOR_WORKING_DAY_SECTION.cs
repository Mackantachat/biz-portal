
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_EDIT
{
    public partial class APP_CLINIC_EDIT_APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION").Count() == 0)
            {
     //           items.Add(new FormSection()
     //           {
     //               Section = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION",
     //               SectionGroup = "APP_CLINIC_EDIT",
     //               Type = SectionType.ArrayOfForms,
     //               EmptyDataMessage = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_EMPTY",
     //               AddButtonText = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_ADD",
     //               SubmitButtonText = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_SUBMIT",
					//ArrayRequiredAtLeast = true,
     //               NumberRequiredAtLeast = 1,
     //               ShowOnSpecificApps = true,
     //               AppSystemNames = new string[] {
     //                   AppSystemNameTextConst.APP_CLINIC_EDIT,
     //               },
					//Ordering = 13,
					//HideSectionHeader = true,
					//DisplayCondition = CONDITION_APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION(),
					//DisableCondition = DISABLE_APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION(),
					//ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
     //           });

                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION",
                    SectionGroup = "APP_CLINIC_EDIT_CUS_OPERTION",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_EMPTY",
                    AddButtonText = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_ADD",
                    SubmitButtonText = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT,
                        AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT,
                    },
                    Ordering = 13,
                    HideSectionHeader = true,
                    //DisplayCondition = CONDITION_APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION(),
                    //DisableCondition = DISABLE_APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION(),
                    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F47_01_51",
                            Control = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_DAY",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_DAY",
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
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_52",
                            Control = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_START_TIME",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_START_TIME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_START_TIME(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_53",
                            Control = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_END_TIME",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_END_TIME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_CLINIC_EDIT_OPERATOR_WORKING_DAY_SECTION_END_TIME(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
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
