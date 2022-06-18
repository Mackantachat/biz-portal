
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_RENEW
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_RENEW_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_EMPTY",
                    AddButtonText = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_ADD",
                    SubmitButtonText = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_SUBMIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW,
                    },
					Ordering = 10,
					DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION(),
					DisableCondition = DISABLE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "49_02_02_44",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_FIRSTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_FIRSTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_45",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_CITIZEN_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_CITIZEN_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",
                            MaskInputReverse = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "49_02_02_46",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_ADDRESS_AREA",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_ADDRESS_AREA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_47",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_LOCATION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_LOCATION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_48",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_PLANT_TYPE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_PLANT_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "49_02_02_49",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_START_MONTH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_START_MONTH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "START_January", Text = "มกราคม" },
                                new Select2Opt() { ID = "START_February", Text = "กุมภาพันธ์" },
                                new Select2Opt() { ID = "START_March", Text = "มีนาคม" },
                                new Select2Opt() { ID = "START_April", Text = "เมษายน" },
                                new Select2Opt() { ID = "START_May", Text = "พฤษภาคม" },
                                new Select2Opt() { ID = "START_June", Text = "มิถุนายน" },
                                new Select2Opt() { ID = "START_July", Text = "กรกฎาคม" },
                                new Select2Opt() { ID = "START_August", Text = "สิงหาคม" },
                                new Select2Opt() { ID = "START_September", Text = "กันยายน" },
                                new Select2Opt() { ID = "START_October", Text = "ตุลาคม" },
                                new Select2Opt() { ID = "START_November", Text = "พฤศจิกายน" },
                                new Select2Opt() { ID = "START_December", Text = "ธันวาคม" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_50",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_LAST_MONTH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_LAST_MONTH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "END_January", Text = "มกราคม" },
                                new Select2Opt() { ID = "END_February", Text = "กุมภาพันธ์" },
                                new Select2Opt() { ID = "END_March", Text = "มีนาคม" },
                                new Select2Opt() { ID = "END_April", Text = "เมษายน" },
                                new Select2Opt() { ID = "END_May", Text = "พฤษภาคม" },
                                new Select2Opt() { ID = "END_June", Text = "มิถุนายน" },
                                new Select2Opt() { ID = "END_July", Text = "กรกฎาคม" },
                                new Select2Opt() { ID = "END_August", Text = "สิงหาคม" },
                                new Select2Opt() { ID = "END_September", Text = "กันยายน" },
                                new Select2Opt() { ID = "END_October", Text = "ตุลาคม" },
                                new Select2Opt() { ID = "END_November", Text = "พฤศจิกายน" },
                                new Select2Opt() { ID = "END_December", Text = "ธันวาคม" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_51",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_HARVEST_MONTH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_HARVEST_MONTH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "HARVEST_January", Text = "มกราคม" },
                                new Select2Opt() { ID = "HARVEST_February", Text = "กุมภาพันธ์" },
                                new Select2Opt() { ID = "HARVEST_March", Text = "มีนาคม" },
                                new Select2Opt() { ID = "HARVEST_April", Text = "เมษายน" },
                                new Select2Opt() { ID = "HARVEST_May", Text = "พฤษภาคม" },
                                new Select2Opt() { ID = "HARVEST_June", Text = "มิถุนายน" },
                                new Select2Opt() { ID = "HARVEST_July", Text = "กรกฎาคม" },
                                new Select2Opt() { ID = "HARVEST_August", Text = "สิงหาคม" },
                                new Select2Opt() { ID = "HARVEST_September", Text = "กันยายน" },
                                new Select2Opt() { ID = "HARVEST_October", Text = "ตุลาคม" },
                                new Select2Opt() { ID = "HARVEST_November", Text = "พฤศจิกายน" },
                                new Select2Opt() { ID = "HARVEST_December", Text = "ธันวาคม" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "49_02_02_52",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_BENEFIT_PER_YEAR",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_BENEFIT_PER_YEAR",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	TextboxNumberSettings = SETTING_NUMBER_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_BENEFIT_PER_YEAR(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
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
