
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;
using BizPortal.Utils.Helpers;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_RENEW
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_RENEW_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW,
                    },
					Ordering = 2,
                    HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "49_02_02_05",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION1_HEADER",
                            Type = ControlType.Heading,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION1_HEADER",
                            ColFixed = 12,
                            ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },

                        CUSTOM_FORM_CONTROL_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS(),
                        //new FormControl()
                        //{
                        //    FieldID = "49_02_02_03",
                        //    Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_MOBILE",
                        //    Type = ControlType.TextBox,
                        //    DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_MOBILE",
                        //    IdentityTypes = new UserTypeEnum[] {
                        //        UserTypeEnum.Juristic,
                        //        UserTypeEnum.Citizen,
                        //    },
                        //    ColFixed = 6,
                        //	DisplayReadonly = true,
                        //	PreFill = true,
                        //	DisplayStaticIfHasData = true,
                        //	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        //},
                        //new FormControl()
                        //{
                        //    FieldID = "49_02_02_04",
                        //    Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_EMAIL",
                        //    Type = ControlType.TextBox,
                        //    DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_ADDRESS_EMAIL",
                        //    IdentityTypes = new UserTypeEnum[] {
                        //        UserTypeEnum.Juristic,
                        //        UserTypeEnum.Citizen,
                        //    },
                        //    ColFixed = 6,
                        //	DisplayReadonly = true,
                        //	PreFill = true,
                        //	DisplayStaticIfHasData = true,
                        //	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        //},
                         
                        new FormControl()
                        {
                            FieldID = "49_02_02_05",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic
                            },
                            ColFixed = 12,
                            WillTriggerDisplayOfOtherUI = true,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = OrganicStoreInformationBuildingTypeOptionValueConst.OWNED, RadioButtonText = OrganicStoreInformationBuildingTypeOptionTextConst.OWNED },
                                    new FormRadioButton() { RadioButtonValue = OrganicStoreInformationBuildingTypeOptionValueConst.RENT, RadioButtonText = OrganicStoreInformationBuildingTypeOptionTextConst.RENT },
                                    new FormRadioButton() { RadioButtonValue = OrganicStoreInformationBuildingTypeOptionValueConst.RENT_FREE, RadioButtonText = OrganicStoreInformationBuildingTypeOptionTextConst.RENT_FREE },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_05",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            WillTriggerDisplayOfOtherUI = true,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = OrganicStoreInformationBuildingTypeOptionValueConst.OWNED, RadioButtonText = OrganicStoreInformationBuildingTypeOptionTextConst.OWNED_CITIZEN },
                                    new FormRadioButton() { RadioButtonValue = OrganicStoreInformationBuildingTypeOptionValueConst.RENT, RadioButtonText = OrganicStoreInformationBuildingTypeOptionTextConst.RENT },
                                    new FormRadioButton() { RadioButtonValue = OrganicStoreInformationBuildingTypeOptionValueConst.RENT_FREE, RadioButtonText = OrganicStoreInformationBuildingTypeOptionTextConst.RENT_FREE },
                                },
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_06",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_RENT_OWNED_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_RENT_OWNED_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_RENT_OWNED_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.JURISTIC },
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.CITIZEN, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.CITIZEN },
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.Government, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.Government },
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.Royal, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.Royal },
                                },
                            },
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE",
                                        ControlAnswer = OrganicStoreInformationBuildingTypeOptionValueConst.RENT
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_BUILDING_TYPE",
                                        ControlAnswer = OrganicStoreInformationBuildingTypeOptionValueConst.RENT_FREE
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE",
                                        ControlAnswer = OrganicStoreInformationBuildingTypeOptionValueConst.RENT
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_CITIZEN_BUILDING_TYPE",
                                        ControlAnswer = OrganicStoreInformationBuildingTypeOptionValueConst.RENT_FREE
                                    },
                                },
                            },
                        	//DisplayCondition = CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_RENT_OWNED_TYPE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                    }
                });
                //items.Add(new FormSectionRow()
                //{
                //    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                //    RowNumber = 1,
                //    Controls = new List<FormControl>()
                //    {
                //        new FormControl()
                //        {
                //            FieldID = "49_02_02_07",
                //            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_SIGNAL_TYPE",
                //            Type = ControlType.Dropdown,
                //            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_SIGNAL_TYPE",
                //            IdentityTypes = new UserTypeEnum[] {
                //                UserTypeEnum.Juristic,
                //                UserTypeEnum.Citizen,
                //            },
                //            ColFixed = 6,
                //            Select2Opts = new Select2Opt[]
                //            {
                //                new Select2Opt() { ID = "YES", Text = "ใช่" },
                //                new Select2Opt() { ID = "NO", Text = "ไม่ใช่" },
                //            },
                //        	WillTriggerDisplayOfOtherUI = true,
                //        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                //        },
                //    }
                //});
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "49_02_02_08",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "FORM_TYPE_1", /* โฉนดที่ดิน */
                                "FORM_TYPE_2", /* น.ส.2 */
                                "FORM_TYPE_3", /* น.ส.3 */
                                "FORM_TYPE_4", /* น.ส.3.ก */
                                "FORM_TYPE_5", /* ปส.23 */
                                "FORM_TYPE_6", /* ส.ป.ก */
                                "FORM_TYPE_7", /* ก.ส.น.5 */
                                "FORM_TYPE_8", /* น.ค.3 */
                                "FORM_TYPE_9", /* ส.ค.1 */
                                "FORM_TYPE_10", /* เอกสารรับรองจากหน่วยงานที่เกี่ยวข้อง */
                            },
                        	DisplayCheckboxInline = true,
                            CheckboxConfigs = new FormControl.CheckboxConfig
                            {
                                CheckMin = true,
                                Min = 1,
                                CheckMax = true,
                                Max = 10,
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "49_02_02_09",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_ID_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_FORM_INCLUDE_ID_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_07",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_SIGNAL_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_SIGNAL_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "YES", Text = "ใช่" },
                                new Select2Opt() { ID = "NO", Text = "ไม่ใช่" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "49_02_02_10",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_PRODUCE_ORGANIC_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_STANDALONE_INFO_ADDRESS_SECTION_PRODUCE_ORGANIC_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            //Select2Opts = new Select2Opt[]
                            //{
                            //    new Select2Opt() { ID = "TYPE_1", Text = "ประเภทที่ 1" },
                            //    new Select2Opt() { ID = "TYPE_2", Text = "ประเภทที่ 2" },
                            //},
                             IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/ORGANICPLANT/Plant2",
                            WillTriggerDisplayOfOtherUI = true,                           
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
