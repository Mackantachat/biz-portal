
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_EDIT
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_EDIT_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_EMPTY",
                    AddButtonText = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_ADD",
                    SubmitButtonText = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_SUBMIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_EDIT,
                    },
					Ordering = 17,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1(),
					DisableCondition = DISABLE_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1(),
					ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_03_52",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_FIRSTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_FIRSTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_03_53",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_CITIZEN_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_CITIZEN_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_03_54",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_ADDRESS_AREA",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_ADDRESS_AREA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_03_55",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_LOCATION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_LOCATION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_03_56",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_PLANT_TYPE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_PLANT_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_03_57",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_START_MONTH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_START_MONTH",
                            Info = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_START_MONTH_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                //new Select2Opt() { ID = "START_January", Text = "มกราคม" },
                                //new Select2Opt() { ID = "START_February", Text = "กุมภาพันธ์" },
                                //new Select2Opt() { ID = "START_March", Text = "มีนาคม" },
                                //new Select2Opt() { ID = "START_April", Text = "เมษายน" },
                                //new Select2Opt() { ID = "START_May", Text = "พฤษภาคม" },
                                //new Select2Opt() { ID = "START_June", Text = "มิถุนายน" },
                                //new Select2Opt() { ID = "START_July", Text = "กรกฎาคม" },
                                //new Select2Opt() { ID = "START_August", Text = "สิงหาคม" },
                                //new Select2Opt() { ID = "START_September", Text = "กันยายน" },
                                //new Select2Opt() { ID = "START_October", Text = "ตุลาคม" },
                                //new Select2Opt() { ID = "START_November", Text = "พฤศจิกายน" },
                                //new Select2Opt() { ID = "START_December", Text = "ธันวาคม" },
                                new Select2Opt() { ID = "1" , Text = "มกราคม"},
                                new Select2Opt() { ID = "2" , Text = "กุมภาพันธ์"},
                                new Select2Opt() { ID = "3" , Text = "มีนาคม"},
                                new Select2Opt() { ID = "4" , Text = "เมษายน"},
                                new Select2Opt() { ID = "5" , Text = "พฤษภาคม"},
                                new Select2Opt() { ID = "6" , Text = "มิถุนายน"},
                                new Select2Opt() { ID = "7" , Text = "กรกฎาคม"},
                                new Select2Opt() { ID = "8" , Text = "สิงหาคม"},
                                new Select2Opt() { ID = "9" , Text = "กันยายน"},
                                new Select2Opt() { ID = "10" , Text = "ตุลาคม"},
                                new Select2Opt() { ID = "11" , Text = "พฤษจิกายน"},
                                new Select2Opt() { ID = "12" , Text = "ธันวาคม"},
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_03_58",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_LAST_MONTH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_LAST_MONTH",
                            Info = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_LAST_MONTH_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                //new Select2Opt() { ID = "END_January", Text = "มกราคม" },
                                //new Select2Opt() { ID = "END_February", Text = "กุมภาพันธ์" },
                                //new Select2Opt() { ID = "END_March", Text = "มีนาคม" },
                                //new Select2Opt() { ID = "END_April", Text = "เมษายน" },
                                //new Select2Opt() { ID = "END_May", Text = "พฤษภาคม" },
                                //new Select2Opt() { ID = "END_June", Text = "มิถุนายน" },
                                //new Select2Opt() { ID = "END_July", Text = "กรกฎาคม" },
                                //new Select2Opt() { ID = "END_August", Text = "สิงหาคม" },
                                //new Select2Opt() { ID = "END_September", Text = "กันยายน" },
                                //new Select2Opt() { ID = "END_October", Text = "ตุลาคม" },
                                //new Select2Opt() { ID = "END_November", Text = "พฤศจิกายน" },
                                //new Select2Opt() { ID = "END_December", Text = "ธันวาคม" },
                                new Select2Opt() { ID = "1" , Text = "มกราคม"},
                                new Select2Opt() { ID = "2" , Text = "กุมภาพันธ์"},
                                new Select2Opt() { ID = "3" , Text = "มีนาคม"},
                                new Select2Opt() { ID = "4" , Text = "เมษายน"},
                                new Select2Opt() { ID = "5" , Text = "พฤษภาคม"},
                                new Select2Opt() { ID = "6" , Text = "มิถุนายน"},
                                new Select2Opt() { ID = "7" , Text = "กรกฎาคม"},
                                new Select2Opt() { ID = "8" , Text = "สิงหาคม"},
                                new Select2Opt() { ID = "9" , Text = "กันยายน"},
                                new Select2Opt() { ID = "10" , Text = "ตุลาคม"},
                                new Select2Opt() { ID = "11" , Text = "พฤษจิกายน"},
                                new Select2Opt() { ID = "12" , Text = "ธันวาคม"},
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_03_59",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_HARVEST_MONTH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_HARVEST_MONTH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                //new Select2Opt() { ID = "HARVEST_January", Text = "มกราคม" },
                                //new Select2Opt() { ID = "HARVEST_February", Text = "กุมภาพันธ์" },
                                //new Select2Opt() { ID = "HARVEST_March", Text = "มีนาคม" },
                                //new Select2Opt() { ID = "HARVEST_April", Text = "เมษายน" },
                                //new Select2Opt() { ID = "HARVEST_May", Text = "พฤษภาคม" },
                                //new Select2Opt() { ID = "HARVEST_June", Text = "มิถุนายน" },
                                //new Select2Opt() { ID = "HARVEST_July", Text = "กรกฎาคม" },
                                //new Select2Opt() { ID = "HARVEST_August", Text = "สิงหาคม" },
                                //new Select2Opt() { ID = "HARVEST_September", Text = "กันยายน" },
                                //new Select2Opt() { ID = "HARVEST_October", Text = "ตุลาคม" },
                                //new Select2Opt() { ID = "HARVEST_November", Text = "พฤศจิกายน" },
                                //new Select2Opt() { ID = "HARVEST_December", Text = "ธันวาคม" },
                                new Select2Opt() { ID = "1" , Text = "มกราคม"},
                                new Select2Opt() { ID = "2" , Text = "กุมภาพันธ์"},
                                new Select2Opt() { ID = "3" , Text = "มีนาคม"},
                                new Select2Opt() { ID = "4" , Text = "เมษายน"},
                                new Select2Opt() { ID = "5" , Text = "พฤษภาคม"},
                                new Select2Opt() { ID = "6" , Text = "มิถุนายน"},
                                new Select2Opt() { ID = "7" , Text = "กรกฎาคม"},
                                new Select2Opt() { ID = "8" , Text = "สิงหาคม"},
                                new Select2Opt() { ID = "9" , Text = "กันยายน"},
                                new Select2Opt() { ID = "10" , Text = "ตุลาคม"},
                                new Select2Opt() { ID = "11" , Text = "พฤษจิกายน"},
                                new Select2Opt() { ID = "12" , Text = "ธันวาคม"},
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_03_60",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_BENEFIT_PER_YEAR",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_PRODUCE_SECTION_1_BENEFIT_PER_YEAR",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
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
