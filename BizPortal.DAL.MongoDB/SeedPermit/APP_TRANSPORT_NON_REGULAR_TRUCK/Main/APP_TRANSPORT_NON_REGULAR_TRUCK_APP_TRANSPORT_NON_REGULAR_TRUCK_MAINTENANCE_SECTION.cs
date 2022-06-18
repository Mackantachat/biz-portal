
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;
using BizPortal.SeedPermit.APP_SEC_CANCEL_A;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                    SectionGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_EMPTY",
                    AddButtonText = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADD",
                    SubmitButtonText = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_SUBMIT",
					ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK,
                    },
					Ordering = 5,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION").Count() == 0)
         
            {
                // row
                items.Add(new FormSectionRow 
                { 
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION", 
                    RowNumber = 0, // Row Ordering
                    Controls = new List<FormControl>
                    {
                        new FormControl
                        {
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_LAND_ID", // Resource
                            Type = ControlType.TextBox, 
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_LAND_ID", //field  mongodb
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl
                        {
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_LAND_NO", //Resource
                            Type = ControlType.TextBox, 
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_LAND_NO", // field mongodb
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl
                        {
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_LAND_CODE", // Resource
                            Type = ControlType.TextBox, 
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_LAND_CODE", // field  mongodb
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        }
                    }
                });
                items.Add(new FormSectionRow
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                    RowNumber = 1, // Row Ordering
                    Controls = new List<FormControl>
                    {
                        new FormControl
                        {
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_OWNER", // Resource
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS_OWNER", //field  mongodb
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl
                        {
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA", //Resource
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA", // field mongodb
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                           TextboxNumberSettings =  new FormControl.NumberSettings()
                            {
                                Min = "0",
                                Max = int.MaxValue.ToString(),
                                Step = "0.01"
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        //new FormControl
                        //{
                        //    Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_CNT_TRUCK_STORE", // Resource
                        //    Type = ControlType.TextBox,
                        //    DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_CNT_TRUCK_STORE", // field  mongodb
                        //    IdentityTypes = new UserTypeEnum[]
                        //    {
                        //        UserTypeEnum.Juristic,
                        //        UserTypeEnum.Citizen,
                        //    },
                        //    Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                        //    DisplayMaskInput = true,
                        //    MaskInputPattern = "0#",
                        //    ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        //}
                    }
                });
                items.Add(new FormSectionRow
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                    RowNumber = 2, // Row Ordering
                    Controls = new List<FormControl>
                    {
                        new FormControl
                        {
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA_RAI", // Resource
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA_RAI", //field  mongodb
                            Info = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA_INFO",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            TextboxNumberSettings =  new FormControl.NumberSettings()
                            {
                                Min = "0",
                                Max = int.MaxValue.ToString(),
                                Step = "0.01"
                            },
                          
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl
                        {
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA_NKAN", //Resource
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA_NKAN", // field mongodb
                             Info = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA_INFO",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            TextboxNumberSettings =  new FormControl.NumberSettings()
                            {
                                Min = "0",
                                Max = int.MaxValue.ToString(),
                                Step = "0.01"
                            },
                       
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl
                        {
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA_TARANKWA", // Resource
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA_TARANKWA", // field  mongodb
                             Info = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_AREA_INFO",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            TextboxNumberSettings =  new FormControl.NumberSettings()
                            {
                                Min = "0",
                                Max = int.MaxValue.ToString(),
                                Step = "0.01"
                            },
                        
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        }
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_01_29",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_ADDRESS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                        	AddressForm_ShowVillageControl = true,
                        	AddressForm_ShowBuildingControl = true,
                        	AddressForm_ShowPostCodeControl = true,
                        	AddressForm_ShowTelephoneControl = true,
                        	AddressForm_ShowMapControl = true,
                        	AddressForm_ShowDeed = false,
                            AddressForm_ShowEmailControl = true,
                            AddressForm_ShowMobileControl = true,
                            AddressForm_IgnoreAddressControl = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_30",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                        	RadioGroup = RADIO_GROUP_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_31",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                        	RadioGroup = RADIO_GROUP_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_32",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                        	RadioGroup = RADIO_GROUP_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE(),
                        	DisplayCondition = CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE(),
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
