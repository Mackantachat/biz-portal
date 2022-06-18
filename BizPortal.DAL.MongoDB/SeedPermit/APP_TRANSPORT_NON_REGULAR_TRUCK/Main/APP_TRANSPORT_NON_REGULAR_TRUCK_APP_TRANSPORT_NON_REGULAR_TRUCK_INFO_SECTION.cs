
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION",
                    SectionGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_01_01",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_HEADER",
                            Type = ControlType.Label,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_HEADER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_02",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REQUEST_BY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REQUEST_BY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	PreFill = true,
                        	DisplayStaticIfHasData = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        //new FormControl()
                        //{
                        //    FieldID = "F39_01_03",
                        //    Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_NATIONALITY",
                        //    Type = ControlType.TextBox,
                        //    DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_NATIONALITY",
                        //    IdentityTypes = new UserTypeEnum[] {
                        //        UserTypeEnum.Citizen,
                        //    },
                        //    Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                        //    ColFixed = 6,
                        //	PreFill = true,
                        //	DisplayStaticIfHasData = true,
                        //	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        //},
                    }
                });
                items.Add(new FormSectionRow
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION",
                    RowNumber = 1, // Row Ordering
                    Controls = new List<FormControl>
                    {
                        new FormControl
                        {
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REGIS_CAPITAL", //Resource
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REGIS_CAPITAL", // field mongodb
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Juristic,
                         
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            PreFill = true,
                            DisplayStaticIfHasData = true,
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl
                        {
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REGIS_DATE", // Resource
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REGIS_DATE", // field  mongodb
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Juristic,
                           
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            PreFill = true,
                            DisplayStaticIfHasData = true,
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        }
                    }
                });
                items.Add(new FormSectionRow
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION",
                    RowNumber = 2, // Row Ordering
                    Controls = new List<FormControl>
                    {
                        new FormControl
                        {
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REASON", // Resource
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REASON", // field  mongodb
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                           // Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        }
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F39_01_04",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_TRANSPORT_IN",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_TRANSPORT_IN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_TRANSPORT_IN_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "KINGDOM", RadioButtonText = "ทั่วราชอาณาจักร" },
                        			new FormRadioButton() { RadioButtonValue = "OTHER", RadioButtonText = "อื่นๆ" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
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
