
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_LAW_OFFICE_EDIT
{
    public partial class APP_LAW_OFFICE_EDIT_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION",
                    SectionGroup = "APP_LAW_OFFICE_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_LAW_OFFICE_EDIT,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F36_03_01",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_LICENSE_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_LICENSE_NUMBER",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Textbox_Rows = 1,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_02",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_POSITION",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_POSITION",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_POSITION_OWNER", Text = "เจ้าของ" },
                                new Select2Opt() { ID = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_POSITION_CHIEF", Text = "หัวหน้าสำนักงาน" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_03",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_LICENSE_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_LICENSE_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = DROPDOWN_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_LICENSE_TYPE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_04",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_LICENSE_EXPIRE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_LICENSE_EXPIRE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_LICENSE_EXPIRE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F36_03_05",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_NAME",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Textbox_Rows = 1,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_06",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_LICENSE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_LICENSE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Textbox_Rows = 1,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F36_03_07",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_REQUEST_EDIT",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_REQUEST_EDIT",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_REQUEST_EDIT_OFFICE_NAME", /* เปลี่ยนชื่อสำนักงานจากเดิม */
                                "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_REQUEST_EDIT_OFFICE_ADDRESS", /* เปลี่ยนที่อยู่สำนักงานจากเดิม */
                                "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_REQUEST_EDIT_IMPORTANT_PERSON", /* เปลี่ยนชื่อเจ้าของ/ หัวหน้าสำนักงาน/ ผู้มีอำนาจกระทำการแทน (กรณีนิติบุคคล) จากเดิมเป็น */
                                "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_REQUEST_EDIT_LAWYER_JOIN", /* แจ้งทนายความเข้า ได้แก่ */
                                "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_REQUEST_EDIT_LAWYER_LEAVE", /* แจ้งทนายความออก ได้แก่ */
                                "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_REQUEST_EDIT_OTHER", /* อื่นๆ */
                            },
                            CheckboxConfigs = new FormControl.CheckboxConfig
                            {
                                CheckMin = true,
                                Min = 1,
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_08",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_NAME_THAI",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_NAME_THAI",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Textbox_Rows = 1,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_NAME_THAI(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_09",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_NAME_ENGLISH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_NAME_ENGLISH",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Textbox_Rows = 1,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_NAME_ENGLISH(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_10",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_ADDRESS",
                        	AddressForm_ShowAddressIdControl = true,
                        	AddressForm_ShowVillageControl = true,
                        	AddressForm_ShowBuildingControl = true,
                        	AddressForm_ShowPostCodeControl = true,
                        	AddressForm_ShowTelephoneControl = true,
                        	AddressForm_ShowMapControl = true,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_ADDRESS(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F36_03_11",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_ADDRESS_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_ADDRESS_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_ADDRESS_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                new FormRadioButton() { RadioButtonValue = "TODO: ใช้ตัวแปร", RadioButtonText = "TODO: ใช้ตัวแปร" },
                                }
                            },
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_ADDRESS_TYPE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F36_03_12",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_ADDRESS_RENT_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_ADDRESS_RENT_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_ADDRESS_RENT_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                new FormRadioButton() { RadioButtonValue = "TODO: ใช้ตัวแปร", RadioButtonText = "TODO: ใช้ตัวแปร" },
                                }
                            },
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_OFFICE_ADDRESS_RENT_TYPE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F36_03_13",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CITIZEN",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CITIZEN",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CITIZEN_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                new FormRadioButton() { RadioButtonValue = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER", RadioButtonText = "เจ้าของกิจการ" },
                                new FormRadioButton() { RadioButtonValue = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF", RadioButtonText = "หัวหน้าสำนักงานทนายความ" },
                                }
                            },
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CITIZEN(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F36_03_14",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_JURISTIC",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_JURISTIC",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_JURISTIC_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                new FormRadioButton() { RadioButtonValue = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER", RadioButtonText = "เจ้าของกิจการ" },
                                new FormRadioButton() { RadioButtonValue = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF", RadioButtonText = "หัวหน้าสำนักงานทนายความ" },
                                new FormRadioButton() { RadioButtonValue = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE", RadioButtonText = "ผู้มีอำนาจกระทำการแทน" },
                                }
                            },
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_JURISTIC(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_15",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_TITLE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 2,
                            Select2Opts = DROPDOWN_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_TITLE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_16",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_FIRST_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_FIRST_NAME",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_FIRST_NAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_17",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_LAST_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_LAST_NAME",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_LAST_NAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_18",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_LICENSE_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_LICENSE_NUMBER",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_LICENSE_NUMBER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_19",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_LICENSE_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_LICENSE_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_LICENSE_TYPE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_LICENSE_TYPE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_20",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_LICENSE_EXPIRE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_LICENSE_EXPIRE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            DataFormat = "dd/MM/yyyy",
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_OWNER_LICENSE_EXPIRE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F36_03_21",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_TITLE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 2,
                            Select2Opts = DROPDOWN_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_TITLE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_22",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_FIRST_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_FIRST_NAME",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_FIRST_NAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_23",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LAST_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LAST_NAME",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LAST_NAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_24",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_TITLE_ENGLISH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_TITLE_ENGLISH",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 2,
                            Select2Opts = DROPDOWN_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_TITLE_ENGLISH(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_TITLE_ENGLISH(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_25",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_FIRST_NAME_ENGLISH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_FIRST_NAME_ENGLISH",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_FIRST_NAME_ENGLISH(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_26",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LAST_NAME_ENGLISH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LAST_NAME_ENGLISH",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LAST_NAME_ENGLISH(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_27",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LICENSE_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LICENSE_NUMBER",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LICENSE_NUMBER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_28",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LICENSE_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LICENSE_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LICENSE_TYPE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LICENSE_TYPE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_29",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LICENSE_EXPIRE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LICENSE_EXPIRE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            DataFormat = "dd/MM/yyyy",
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_CHIEF_LICENSE_EXPIRE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F36_03_30",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_TITLE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 2,
                            Select2Opts = DROPDOWN_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_TITLE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_31",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_FIRST_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_FIRST_NAME",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_FIRST_NAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_32",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_LAST_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_LAST_NAME",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_LAST_NAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_33",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_LICENSE_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_LICENSE_NUMBER",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_LICENSE_NUMBER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_34",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_LICENSE_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_LICENSE_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_LICENSE_TYPE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_LICENSE_TYPE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_03_35",
                            Control = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_LICENSE_EXPIRE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_LICENSE_EXPIRE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            DataFormat = "dd/MM/yyyy",
                        	DisplayCondition = CONDITION_APP_LAW_OFFICE_EDIT_REQUEST_EDIT_SECTION_IMPORTANT_PERSON_COMMITEE_LICENSE_EXPIRE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE_EDIT",
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
