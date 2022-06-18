
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_LICENSE
{
    public partial class APP_CLINIC_LICENSE_APP_CLINIC_LICENSE_DETAIL_SECTION_EN
    {
        public static void Init()
        {
     //       var db = MongoFactory.GetFormSectionCollection();
     //       List<FormSection> items = new List<FormSection>();

     //       if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_LICENSE_DETAIL_SECTION_EN").Count() == 0)
     //       {
     //           items.Add(new FormSection()
     //           {
     //               Section = "APP_CLINIC_LICENSE_DETAIL_SECTION_EN",
     //               SectionGroup = "APP_CLINIC_LICENSE",
     //               Type = SectionType.ArrayOfForms,
     //               EmptyDataMessage = "APP_CLINIC_LICENSE_DETAIL_SECTION_EN_EMPTY",
     //               AddButtonText = "APP_CLINIC_LICENSE_DETAIL_SECTION_EN_ADD",
     //               SubmitButtonText = "APP_CLINIC_LICENSE_DETAIL_SECTION_EN_SUBMIT",
     //               ShowOnSpecificApps = true,
     //               AppSystemNames = new string[] {
     //                   AppSystemNameTextConst.APP_CLINIC,
     //               },
					//Ordering = 3,
					//ResourceName = "PermitResource.RESOURCE_APP_CLINIC_LICENSE",
     //           });
     //       }

     //       if (items.Count > 0)
     //       {
     //           db.InsertMany(items.ToArray());
     //       }

     //       InitFormSectionRow();
        }

        private static void InitFormSectionRow()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_LICENSE_DETAIL_SECTION_EN").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_LICENSE_DETAIL_SECTION_EN",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_03_05",
                            Control = "APP_CLINIC_LICENSE_DETAIL_SECTION_EN_DAY_EN",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_LICENSE_DETAIL_SECTION_EN_DAY_EN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 3,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "MONDAY", Text = "Monday" },
                                new Select2Opt() { ID = "TUESDAY", Text = "Tuesday" },
                                new Select2Opt() { ID = "WEDNESDAY", Text = "Wednesday" },
                                new Select2Opt() { ID = "THURSDAY", Text = "Thursday" },
                                new Select2Opt() { ID = "FRIDAY", Text = "Friday" },
                                new Select2Opt() { ID = "SATURDAY", Text = "Saturday" },
                                new Select2Opt() { ID = "SUNDAY", Text = "Sunday" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_LICENSE",
                        },
                        new FormControl()
                        {
                            FieldID = "42_03_06",
                            Control = "APP_CLINIC_LICENSE_DETAIL_SECTION_EN_START_TIME_EN",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_LICENSE_DETAIL_SECTION_EN_START_TIME_EN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 3,
                            Select2Opts = DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_EN_START_TIME_EN(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_LICENSE",
                        },
                        new FormControl()
                        {
                            FieldID = "42_03_07",
                            Control = "APP_CLINIC_LICENSE_DETAIL_SECTION_EN_TIME_OUT_EN",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_LICENSE_DETAIL_SECTION_EN_TIME_OUT_EN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 3,
                            Select2Opts = DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_EN_TIME_OUT_EN(),
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
