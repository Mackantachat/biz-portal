
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_EDIT
{
    public partial class APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION").Count() == 0)
            {
     //           items.Add(new FormSection()
     //           {
     //               Section = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION",
     //               SectionGroup = "APP_HOSPITAL_PERMISSION_EDIT",
     //               Type = SectionType.ArrayOfForms,
     //               EmptyDataMessage = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_EMPTY",
     //               AddButtonText = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_ADD",
     //               SubmitButtonText = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_SUBMIT",
					//ArrayRequiredAtLeast = true,
     //               NumberRequiredAtLeast = 1,
     //               ShowOnSpecificApps = true,
     //               AppSystemNames = new string[] {
     //                   AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
     //               },
					//Ordering = 3,
					//HideSectionHeader = true,
					//DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION(),
					//DisableCondition = DISABLE_APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION(),
					//ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
     //           });

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITA",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_EMPTY",
                    AddButtonText = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_ADD",
                    SubmitButtonText = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_CLINIC_BUSINESS_EDIT,
                        AppSystemNameTextConst.APP_HOSPITAL_BUSINESS_EDIT,
                    },
                    Ordering = 3,
                    HideSectionHeader = true,
                    DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION(),
                    //DisableCondition = DISABLE_APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F47_01_07",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 2,
                            Select2Opts = DROPDOWN_APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_08",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_FIRSTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_FIRSTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_09",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_LASTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
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
