
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PLAN
{
    public partial class APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION",
                    SectionGroup = "APP_HOSPITAL_PLAN",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_EMPTY",
                    AddButtonText = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_ADD",
                    SubmitButtonText = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_SUBMIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL,
                    },
					Ordering = 2,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_06",
                            Control = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_TYPE_PERSONAL", Text = "ส่วนตัว" },
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_TYPE_SHARE", Text = "หุ้น" },
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_TYPE_INSIDE", Text = "สถาบันการเงินในประเทศ" },
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_TYPE_OUTSIDE", Text = "สถาบันการเงินต่างประเทศ" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_07",
                            Control = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_PERCENT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_PERCENT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	TextboxNumberSettings = SETTING_NUMBER_APP_HOSPITAL_PLAN_BUSSINESS_INVEST_SECTION_PERCENT(),
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
