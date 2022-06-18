
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2",
                    SectionGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK,
                    },
					Ordering = 4,
					HideSectionHeader = true,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_01_13",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_CHECK_CAR_SECTION",
                            Type = ControlType.Hidden,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_CHECK_CAR_SECTION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { 
                        	},
                        	HideLabel = true,
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_14",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_1",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_15",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_2",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_16",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_3",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_3",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_17",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_4",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_4",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_18",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_5",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_5",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_19",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_6",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_6",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_20",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_7",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_7",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_21",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_8",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_8",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_22",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_9",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_TYPE_9",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_23",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_CAR_TOTAL_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_CAR_TOTAL_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_24",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_DRIVER_TOTAL_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_DRIVER_TOTAL_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        }
                        
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_01_25",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_WITHIN_TIME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_2_WITHIN_TIME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
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
