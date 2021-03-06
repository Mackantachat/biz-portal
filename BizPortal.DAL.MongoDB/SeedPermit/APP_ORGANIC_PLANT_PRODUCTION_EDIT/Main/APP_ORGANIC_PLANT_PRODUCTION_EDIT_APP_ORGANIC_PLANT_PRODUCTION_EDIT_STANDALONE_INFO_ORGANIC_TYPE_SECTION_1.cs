
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_EDIT
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_EDIT_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_EMPTY",
                    AddButtonText = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ADD",
                    SubmitButtonText = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_SUBMIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_EDIT,
                    },
					Ordering = 6,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1(),
					DisableCondition = DISABLE_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_03_13",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/ORGANICPLANT/Plant",
                            WillTriggerDisplayOfOtherUI = true,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_03_14",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_AREA",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_AREA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            //TextboxNumberSettings = SETTING_NUMBER_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_AREA(),
                            ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_03_15",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_TREE_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_TREE_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            //Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_03_16",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_YEAR_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_YEAR_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_03_17",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_START_MONTH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_START_MONTH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                //new Select2Opt() { ID = "START_January", Text = "??????????????????" },
                                //new Select2Opt() { ID = "START_February", Text = "??????????????????????????????" },
                                //new Select2Opt() { ID = "START_March", Text = "??????????????????" },
                                //new Select2Opt() { ID = "START_April", Text = "??????????????????" },
                                //new Select2Opt() { ID = "START_May", Text = "?????????????????????" },
                                //new Select2Opt() { ID = "START_June", Text = "????????????????????????" },
                                //new Select2Opt() { ID = "START_July", Text = "?????????????????????" },
                                //new Select2Opt() { ID = "START_August", Text = "?????????????????????" },
                                //new Select2Opt() { ID = "START_September", Text = "?????????????????????" },
                                //new Select2Opt() { ID = "START_October", Text = "??????????????????" },
                                //new Select2Opt() { ID = "START_November", Text = "???????????????????????????" },
                                //new Select2Opt() { ID = "START_December", Text = "?????????????????????" },
                                new Select2Opt() { ID = "1" , Text = "??????????????????"},
                                new Select2Opt() { ID = "2" , Text = "??????????????????????????????"},
                                new Select2Opt() { ID = "3" , Text = "??????????????????"},
                                new Select2Opt() { ID = "4" , Text = "??????????????????"},
                                new Select2Opt() { ID = "5" , Text = "?????????????????????"},
                                new Select2Opt() { ID = "6" , Text = "????????????????????????"},
                                new Select2Opt() { ID = "7" , Text = "?????????????????????"},
                                new Select2Opt() { ID = "8" , Text = "?????????????????????"},
                                new Select2Opt() { ID = "9" , Text = "?????????????????????"},
                                new Select2Opt() { ID = "10" , Text = "??????????????????"},
                                new Select2Opt() { ID = "11" , Text = "???????????????????????????"},
                                new Select2Opt() { ID = "12" , Text = "?????????????????????"},
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_03_18",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_END_MONTH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_END_MONTH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                //new Select2Opt() { ID = "END_January", Text = "??????????????????" },
                                //new Select2Opt() { ID = "END_February", Text = "??????????????????????????????" },
                                //new Select2Opt() { ID = "END_March", Text = "??????????????????" },
                                //new Select2Opt() { ID = "END_April", Text = "??????????????????" },
                                //new Select2Opt() { ID = "END_May", Text = "?????????????????????" },
                                //new Select2Opt() { ID = "END_June", Text = "????????????????????????" },
                                //new Select2Opt() { ID = "END_July", Text = "?????????????????????" },
                                //new Select2Opt() { ID = "END_August", Text = "?????????????????????" },
                                //new Select2Opt() { ID = "END_September", Text = "?????????????????????" },
                                //new Select2Opt() { ID = "END_October", Text = "??????????????????" },
                                //new Select2Opt() { ID = "END_November", Text = "???????????????????????????" },
                                //new Select2Opt() { ID = "END_December", Text = "?????????????????????" },
                                new Select2Opt() { ID = "1" , Text = "??????????????????"},
                                new Select2Opt() { ID = "2" , Text = "??????????????????????????????"},
                                new Select2Opt() { ID = "3" , Text = "??????????????????"},
                                new Select2Opt() { ID = "4" , Text = "??????????????????"},
                                new Select2Opt() { ID = "5" , Text = "?????????????????????"},
                                new Select2Opt() { ID = "6" , Text = "????????????????????????"},
                                new Select2Opt() { ID = "7" , Text = "?????????????????????"},
                                new Select2Opt() { ID = "8" , Text = "?????????????????????"},
                                new Select2Opt() { ID = "9" , Text = "?????????????????????"},
                                new Select2Opt() { ID = "10" , Text = "??????????????????"},
                                new Select2Opt() { ID = "11" , Text = "???????????????????????????"},
                                new Select2Opt() { ID = "12" , Text = "?????????????????????"},
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_03_19",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_HARVEST_MONTH",
                            Type = ControlType.TextBox, //?????????????????????????????? Dropdown ???????????????????????????????????????????????????????????????????????? Text
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_HARVEST_MONTH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            //Select2Opts = new Select2Opt[]
                            //{
                            //    new Select2Opt() { ID = "START_HARVEST_January", Text = "??????????????????" },
                            //    new Select2Opt() { ID = "START_HARVEST_February", Text = "??????????????????????????????" },
                            //    new Select2Opt() { ID = "START_HARVEST_March", Text = "??????????????????" },
                            //    new Select2Opt() { ID = "START_HARVEST_April", Text = "??????????????????" },
                            //    new Select2Opt() { ID = "START_HARVEST_May", Text = "?????????????????????" },
                            //    new Select2Opt() { ID = "START_HARVEST_June", Text = "????????????????????????" },
                            //    new Select2Opt() { ID = "START_HARVEST_July", Text = "?????????????????????" },
                            //    new Select2Opt() { ID = "START_HARVEST_August", Text = "?????????????????????" },
                            //    new Select2Opt() { ID = "START_HARVEST_September", Text = "?????????????????????" },
                            //    new Select2Opt() { ID = "START_HARVEST_October", Text = "??????????????????" },
                            //    new Select2Opt() { ID = "START_HARVEST_November", Text = "???????????????????????????" },
                            //    new Select2Opt() { ID = "START_HARVEST_December", Text = "?????????????????????" },
                            //},
                        	//WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_03_20",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_PRODUCE_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_PRODUCE_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	PreFill = true,
                        	TextboxNumberSettings = SETTING_NUMBER_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_PRODUCE_AMOUNT(),
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
