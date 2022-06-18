
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PLAN
{
    public partial class APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_08",
                            Control = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_HEADER",
                            Type = ControlType.Heading,
                            DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_HEADER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_09",
                            Control = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_LOCATION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_LOCATION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_10",
                            Control = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_PEOPLE_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_PEOPLE_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_11",
                            Control = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_NURSE_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_NURSE_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_12",
                            Control = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_HEADING",
                            Type = ControlType.Heading,
                            DataKey = "APP_HOSPITAL_PLAN_BUSSINESS_SERVICE_SECTION_HEADING",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
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
