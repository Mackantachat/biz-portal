
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PLAN
{
    public partial class APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION",
                    SectionGroup = "APP_HOSPITAL_PLAN",
                    Type = SectionType.Form,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        //new FormControl()
                        //{
                        //    Control = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_NURSE_AMOUNT_GOV",
                        //    Type = ControlType.TextBox,
                        //    DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_NURSE_AMOUNT_GOV",
                        //    IdentityTypes = new UserTypeEnum[] {
                        //        UserTypeEnum.Juristic,
                        //        UserTypeEnum.Citizen,
                        //    },
                        //    ColFixed = 6,
                        //    DisplayMaskInput = true,
                        //    MaskInputPattern = "0#",
                        //    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        //},
                        new FormControl()
                        {
                            FieldID = "F35_02_15",
                            Control = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_REASON",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_REASON",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_16",
                            Control = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_DURATION",
                            Type = ControlType.Heading,
                            DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_DURATION",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_17",
                            Control = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_DURATION_YEAR",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_DURATION_YEAR",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_18",
                            Control = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_DURATION_MONTH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_DURATION_MONTH",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	TextboxNumberSettings = SETTING_NUMBER_APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_PROBLEM_SECTION_DURATION_MONTH(),
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
