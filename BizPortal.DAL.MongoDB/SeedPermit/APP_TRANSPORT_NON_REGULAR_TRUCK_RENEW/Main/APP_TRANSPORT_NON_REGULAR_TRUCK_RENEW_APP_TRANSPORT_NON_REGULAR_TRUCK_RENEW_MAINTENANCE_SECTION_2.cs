
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2",
                    SectionGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW,
                    },
					Ordering = 8,
					HideSectionHeader = true,
					ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_02_31",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_AREA_METER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_AREA_METER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_02_32",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_CAR_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_CAR_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F39_02_33",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_READY",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_READY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_READY_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "GET_PERMIT", RadioButtonText = "วันที่ได้รับใบอนุญาต" },
                        			new FormRadioButton() { RadioButtonValue = "WITHIN", RadioButtonText = "ระบุวันที่" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_02_34",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_DATETIME",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_DATETIME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	DisplayCondition = CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_DATETIME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_02_35",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_CAR_CURRENT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_CAR_CURRENT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_02_36",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_REQUEST_BY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_REQUEST_BY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	PreFill = true,
                        	DisplayStaticIfHasData = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_02_37",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_TYPE",
                            Type = ControlType.Label,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_02_38",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_RENEW_PERMIT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_RENEW_PERMIT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_02_39",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_EXPIRE_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_EXPIRE_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F39_02_40",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_CONFIRM",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "CONFIRM", /* ตามใบอนุญาตฉบับเดิมทุกประการ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_02_41",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_SIGNATURE",
                            Type = ControlType.Signature,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_SIGNATURE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
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
