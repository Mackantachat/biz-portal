
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2",
                    SectionGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK,
                    },
					Ordering = 8,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_01_33",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_AREA_METER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_AREA_METER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayReadonly = true,
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                 
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                   
                         
                        new FormControl()
                        {
                            FieldID = "F39_01_35",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_READY",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_READY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_READY_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "GET_PERMIT", RadioButtonText = "วันที่ได้รับใบอนุญาต" },
                        			new FormRadioButton() { RadioButtonValue = "WITHIN", RadioButtonText = "ระบุวันที่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_36",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_DATETIME",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_DATETIME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                        	{
                        		StartDate = "0d",
                        	},
                        	DisplayCondition = CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_DATETIME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_01_37",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_CAR_CURRENT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_CAR_CURRENT",
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
                         new FormControl
                        {
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_NOTE", // Resource
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_NOTE", // field  mongodb
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                           // Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_38",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_CONFIRM",
                            Info = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "CONFIRM", /* ข้าพเจ้าขอรับรองว่าเอกสารและหลักฐานที่แนบทั้งหมด รวมทั้งข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_01_39",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_SIGNATURE",
                            Type = ControlType.Signature,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_SIGNATURE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
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
