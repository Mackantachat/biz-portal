
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PLAN
{
    public partial class APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION",
                    SectionGroup = "APP_HOSPITAL_PLAN",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_EMPTY",
                    AddButtonText = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_ADD",
                    SubmitButtonText = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_SUBMIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_13",
                            Control = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_CHOICE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_CHOICE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_CHOICE_1", Text = "ผู้ป่วยใน(เตียง)" },
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_CHOICE_2", Text = "ห้องผ่าตัด(เตียง)" },
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_CHOICE_3", Text = "เครื่องเอกซเรย์คอมพิวเตอร์(ห้อง) " },
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_CHOICE_4", Text = "เครื่องตรวจอวัยวะภายในชนิดสนามแม่เหล็กไฟฟ้า(ห้อง) " },
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_CHOICE_5", Text = "เครื่องสลายนิ่ว(ห้อง)" },
                                new Select2Opt() { ID = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_CHOICE_6", Text = "เครื่องล้างไต(ห้อง)" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_14",
                            Control = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_TYPE_SECTION_AMOUNT",
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
