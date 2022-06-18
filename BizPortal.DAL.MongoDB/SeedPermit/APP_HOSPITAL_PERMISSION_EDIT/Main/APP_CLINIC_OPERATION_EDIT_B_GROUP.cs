
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;
using BizPortal.Utils.Helpers;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_EDIT
{
    public class APP_CLINIC_OPERATION_EDIT_B_GROUP
    {
        private static FormControlDisplayCondition APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_STATUS_CONDITION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDITC_SECD_CONA",
                        ControlAnswer = "OPERATOR",
                    },
                },
            };
        }

        public static void Init() 
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDITC_SECA").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDITC_SECA",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITC",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                    },
                    Ordering = 11,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                });
            }
            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDITC_SECB").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDITC_SECB",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITC",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_HOSPITAL_PERMISSION_EDITC_SECB_EMPTY",
                    AddButtonText = "APP_HOSPITAL_PERMISSION_EDITC_SECB_ADD",
                    SubmitButtonText = "APP_HOSPITAL_PERMISSION_EDITC_SECB_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                    },
                    Ordering = 12,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                });
            }
            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDITC_SECC").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDITC_SECC",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITC",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                    },
                    Ordering = 13,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                });
            }
            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDITC_SECD").Count() == 0) 
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDITC_SECD",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITC",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                            //AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                            AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                        },
                    Ordering = 14,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                });
            }
            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDITC_SECE").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDITC_SECE",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITC",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_HOSPITAL_PERMISSION_EDITC_SECE_EMPTY",
                    AddButtonText = "APP_HOSPITAL_PERMISSION_EDITC_SECE_ADD",
                    SubmitButtonText = "APP_HOSPITAL_PERMISSION_EDITC_SECE_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                    },
                    Ordering = 15,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                });
            }
            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDITC_SECF").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDITC_SECF",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITC",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                            //AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                            AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                        },
                    Ordering = 16,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                    HideSectionHeader = true
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDITC_SECA").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDITC_SECA",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECA_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECA_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDITC_SECA",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONA",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 2,
                            Select2Opts = FormSectionRow.optPersonTitle,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONB",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONB",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONC",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECA_COND",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECA_COND",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = FormSectionRow.optNationality,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONF",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONF",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "PRACTICING_DISEASE", Text = "ประกอบวิชาชีพ" },
                                new Select2Opt() { ID = "MEDICAL_PRACTICE", Text = "ประกอบโรคศิลปะ" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONG",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONG",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "MEDICINE", Text = "เวชกรรม" },
                                new Select2Opt() { ID = "DENTAL", Text = "ทันตกรรม" },
                                new Select2Opt() { ID = "NURSING_AND_MIDWIFERY", Text = "การพยาบาลและการผดุงครรภ์" },
                                new Select2Opt() { ID = "MEDICAL_TECHNIQUE", Text = "เทคนิคการแพทย์" },
                                new Select2Opt() { ID = "PHYSICAL_THERAPY", Text = "กายภาพบำบัด" },
                                new Select2Opt() { ID = "THAI_TRADITIONAL_MEDICINE", Text = "การแพทย์แผนไทย" },
                                new Select2Opt() { ID = "HEALING_ARTS_PRACTICE", Text = "การประกอบโรคศิลปะ" },
                                new Select2Opt() { ID = "SPECIALIZED_MEDICINE", Text = "เฉพาะทางเวชกรรม" },
                                new Select2Opt() { ID = "SPECIALIZED_DENTISTRY", Text = "เฉพาะทางทันตกรรม" },
                                new Select2Opt() { ID = "SPECIALIZED_IN_NURSING_AND_MIDWIFE", Text = "เฉพาะทางด้านการพยาบาลและผดุงครรภ์" },
                                new Select2Opt() { ID = "SPECIALIZED_IN_THE_ENT", Text = "เฉพาะทาง หู คอ จมูก" },
                                new Select2Opt() { ID = "HEART_DISEASE", Text = "เฉพาะทางโรคหัวใจ" },
                                new Select2Opt() { ID = "SPECIFIC_TO_CANCER", Text = "เฉพาะทางโรคมะเร็ง" },
                                new Select2Opt() { ID = "OTHER_SPECIALTY", Text = "เฉพาะทางอื่นๆ" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONI",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECA_CONI",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties
                            {
                                EndDate = "0d",
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                    }
                });
            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDITC_SECB").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDITC_SECB",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECB_CONA",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECB_CONA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "MONDAY", Text = "จันทร์" },
                                new Select2Opt() { ID = "TUESDAY", Text = "อังคาร" },
                                new Select2Opt() { ID = "WEDNESDAY", Text = "พุธ" },
                                new Select2Opt() { ID = "THURSDAY", Text = "พฤหัส" },
                                new Select2Opt() { ID = "FRIDAY", Text = "ศุกร์" },
                                new Select2Opt() { ID = "SATURDAY", Text = "เสาร์" },
                                new Select2Opt() { ID = "SUNDAY", Text = "อาทิตย์" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECB_CONB",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECB_CONB",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = FormSectionRow.optWorkingTime,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECB_CONC",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECB_CONC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = FormSectionRow.optWorkingTime,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                    }
                });
            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDITC_SECC").Count() == 0) 
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDITC_SECC",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECC_CONA",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECC_CONA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            AddressForm_ShowVillageControl = true,
                            AddressForm_ShowBuildingControl = true,
                            AddressForm_ShowPostCodeControl = true,
                            AddressForm_ShowEmailControl = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                    }
                });
            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDITC_SECD").Count() == 0) 
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDITC_SECD",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECD_CONA",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECD_CONA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_HOSPITAL_PERMISSION_EDITC_SECD_CONA_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "OPERATOR", RadioButtonText = "เป็นผู้ดำเนินการสถานพยาบาลประเภทที่" },
                                    new FormRadioButton() { RadioButtonValue = "PROFESSIONAL", RadioButtonText = "เป็นผู้ประกอบวิชาชีพหรือปฏิบัติงานหน้าที่อื่นในสถานพยาบาล หรือในส่วนราชการ หรือหน่วยงานอื่น ดังนี้" },
                                }
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },

                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECD_CONB",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECD_CONB",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "CLINIC", RadioButtonText = "ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน" },
                                    new FormRadioButton() { RadioButtonValue = "HOSPITAL", RadioButtonText = "ประเภทที่รับผู้ป่วยไว้ค้างคืน" },
                                }
                            },
                            DisplayCondition = APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_STATUS_CONDITION(),
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECD_CONC",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECD_CONC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECD_COND",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECD_COND",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECD_CONE",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECD_CONE",
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
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                    }
                });
            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDITC_SECE").Count() == 0) 
            {
                    items.Add(new FormSectionRow()
                    {
                        Section = "APP_HOSPITAL_PERMISSION_EDITC_SECE",
                        RowNumber = 0,
                        Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECE_CONA",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECE_CONA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "MONDAY", Text = "จันทร์" },
                                new Select2Opt() { ID = "TUESDAY", Text = "อังคาร" },
                                new Select2Opt() { ID = "WEDNESDAY", Text = "พุธ" },
                                new Select2Opt() { ID = "THURSDAY", Text = "พฤหัส" },
                                new Select2Opt() { ID = "FRIDAY", Text = "ศุกร์" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECE_CONB",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECE_CONB",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = FormSectionRow.optWorkingTime,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECE_CONC",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECE_CONC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = FormSectionRow.optWorkingTime,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                    }
                    });
                }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDITC_SECF").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDITC_SECF",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECF_CONA",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECF_CONA",
                            Info = "APP_HOSPITAL_PERMISSION_EDITC_SECF_CONA_INFO",
                            DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "OPERATOR_CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },

                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDITC_SECF_CONB",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDITC_SECF_CONB",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "OPERATOR_CONFIRM_2_TRUE", /* ข้าพเจ้ายินยอมให้บุคคลดังกล่าวข้างต้น เป็นผู้ดำเนินการสถานพยาบาลแห่งนี้ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
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
