
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;
using BizPortal.Utils.Helpers;

namespace BizPortal.SeedPermit.APP_CLINIC_EDIT
{
    public class APP_HOSPITAL_OPERATION_EDIT_B_GROUP
    {

        public static Select2Opt[] optPersonTitle
        {
            get
            {
                return new Select2Opt[]
                {
                    new Select2Opt() { ID = "01", Text = "นาย" },
                    new Select2Opt() { ID = "02", Text = "นาง" },
                    new Select2Opt() { ID = "03", Text = "นางสาว" }
                };
            }
        }

        private static FormControlDisplayCondition APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONB_CONDITION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONA",
                        ControlAnswer = "OPERATOR",
                    },
                },
            };
        }

        private static FormControlDisplayCondition APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONC_CONDITION()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONA",
                        ControlAnswer = "OPERATOR",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONB",
                        ControlAnswer = "CLINIC",
                    },
                },
            };
        }

        private static FormControlDisplayCondition APP_HOSPITAL_OPERATION_EDIT_B_SECD_COND_CONDITION()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONA",
                        ControlAnswer = "OPERATOR",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONB",
                        ControlAnswer = "HOSPITAL",
                    },
                },
            };
        }

        public static void Init() 
        {

            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_OPERATION_EDIT_B_SECA").Count() == 0)
            {

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECA",
                    SectionGroup = "APP_HOSPITAL_OPERATION_EDIT_B_GROUP",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                        AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                    },
                    Ordering = 4,
                    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                });

            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_OPERATION_EDIT_B_SECB").Count() == 0)
            {

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECB",
                    SectionGroup = "APP_HOSPITAL_OPERATION_EDIT_B_GROUP",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_HOSPITAL_OPERATION_EDIT_B_SECB_EMPTY",
                    AddButtonText = "APP_HOSPITAL_OPERATION_EDIT_B_SECB_ADD",
                    SubmitButtonText = "APP_HOSPITAL_OPERATION_EDIT_B_SECB_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                        AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                    },
                    Ordering = 5,
                    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                });

            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_OPERATION_EDIT_B_SECC").Count() == 0)
            {

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECC",
                    SectionGroup = "APP_HOSPITAL_OPERATION_EDIT_B_GROUP",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                        AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                    },
                    Ordering = 6,
                    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                });

            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_OPERATION_EDIT_B_SECD").Count() == 0)
            {

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECD",
                    SectionGroup = "APP_HOSPITAL_OPERATION_EDIT_B_GROUP",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                        AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                    },
                    Ordering = 6,
                    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                });

            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_OPERATION_EDIT_B_SECE").Count() == 0)
            {

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECE",
                    SectionGroup = "APP_HOSPITAL_OPERATION_EDIT_B_GROUP",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_HOSPITAL_OPERATION_EDIT_B_SECE_EMPTY",
                    AddButtonText = "APP_HOSPITAL_OPERATION_EDIT_B_SECE_ADD",
                    SubmitButtonText = "APP_HOSPITAL_OPERATION_EDIT_B_SECE_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                        AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                    },
                    Ordering = 6,
                    HideSectionHeader = true,
                    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                });

            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_OPERATION_EDIT_B_SECF").Count() == 0)
            {

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECF",
                    SectionGroup = "APP_HOSPITAL_OPERATION_EDIT_B_GROUP",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                        AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                    },
                    Ordering = 6,
                    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_OPERATION_EDIT_B_SECA").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECA",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECA",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONA",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 2,
                            Select2Opts = optPersonTitle,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONB",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONB",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONC",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_COND",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_COND",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = FormSectionRow.optNationality,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        //new FormControl()
                        //{
                        //    Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONF",
                        //    Type = ControlType.TextBox,
                        //    DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONF",
                        //    IdentityTypes = new UserTypeEnum[] {
                        //        UserTypeEnum.Juristic,
                        //        UserTypeEnum.Citizen,
                        //    },
                        //    Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                        //    ColFixed = 6,
                        //    DisplayMaskInput = true,
                        //    MaskInputPattern = "0-0000-00000-00-0",
                        //    MaskInputReverse = true,
                        //    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        //},
                        //new FormControl()
                        //{
                        //    Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONG",
                        //    Type = ControlType.TextBox,
                        //    DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONG",
                        //    IdentityTypes = new UserTypeEnum[] {
                        //        UserTypeEnum.Juristic,
                        //        UserTypeEnum.Citizen,
                        //    },
                        //    Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                        //    ColFixed = 6,
                        //    DisplayMaskInput = true,
                        //    MaskInputPattern = "A",
                        //    MaskInputPatternTranslation = new Dictionary<string, string>
                        //    {
                        //        { "A", "{ pattern: /[^ก-๙]/, optional: true, recursive: true }" },
                        //    },
                        //    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        //},
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONH",
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
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONI",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONI",
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
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONJ",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONJ",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONK",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECA_CONK",
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
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                    }
                });
                
            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_OPERATION_EDIT_B_SECB").Count() == 0)
            {

                
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECB",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F47_01_32",
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECB_CONA",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECB_CONA",
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
                                new Select2Opt() { ID = "THURSDAY", Text = "พฤหัสบดี" },
                                new Select2Opt() { ID = "FRIDAY", Text = "ศุกร์" },
                                new Select2Opt() { ID = "SATURDAY", Text = "เสาร์" },
                                new Select2Opt() { ID = "SUNDAY", Text = "อาทิตย์" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_33",
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECB_CONB",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECB_CONB",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = FormSectionRow.optWorkingTime,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_34",
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECB_CONC",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECB_CONC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = FormSectionRow.optHospitalWorkingTime,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                    }
                });


            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_OPERATION_EDIT_B_SECC").Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECC",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECC_CONA",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECC_CONA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                            AddressForm_ShowVillageControl = true,
                            AddressForm_ShowBuildingControl = true,
                            AddressForm_ShowPostCodeControl = true,
                            AddressForm_ShowEmailControl = true,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            HideLabel = true,
                        },
                    }
                });


            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_OPERATION_EDIT_B_SECD").Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECD",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONA",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONA_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "OPERATOR", RadioButtonText = "เป็นผู้ดำเนินการสถานพยาบาลประเภทที่" },
                                    new FormRadioButton() { RadioButtonValue = "PROFESSIONAL", RadioButtonText = "เป็นผู้ประกอบวิชาชีพหรือปฏิบัติงานหน้าที่อื่นในสถานพยาบาล หรือในส่วนราชการ หรือหน่วยงานอื่น ดังนี้" },
                                }
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },

                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONB",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONB",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONB_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "CLINIC", RadioButtonText = "ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน" },
                                    new FormRadioButton() { RadioButtonValue = "HOSPITAL", RadioButtonText = "ประเภทที่รับผู้ป่วยไว้ค้างคืน" },
                                }
                            },
                            DisplayCondition = APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONB_CONDITION(),
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },

                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONC",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "MEDICAL_CLINIC", Text = "คลินิกเวชกรรม" },
                                new Select2Opt() { ID = "DENTAL_CLINIC", Text = "คลินิกทันตกรรม" },
                                new Select2Opt() { ID = "MIDWIFERY_CLINIC", Text = "คลินิกการพยาบาลและการผดุงครรภ์" },
                                new Select2Opt() { ID = "MEDICAL_TECHNOLOGY_CLINIC", Text = "คลินิกเทคนิคการแพทย์" },
                                new Select2Opt() { ID = "THAI_TRADITIONAL_MEDICINE_CLINIC", Text = "คลินิกการแพทย์แผนไทย" },
                                new Select2Opt() { ID = "APPLIED_THAI_TRADITIONAL_MEDICINE_CLINIC", Text = "คลินิกการแพทย์แผนไทยประยุกต์" },
                                new Select2Opt() { ID = "MEDICINE_CLINIC", Text = "คลินิกการประกอบโรคศิลปะ" },
                                new Select2Opt() { ID = "SPECIALIZED_MEDICAL_CLINIC", Text = "คลินิกเฉพาะทางเวชกรรม" },
                                new Select2Opt() { ID = "SPECIALIZED_DENTAL_CLINIC", Text = "คลินิกเฉพาะทางทันตกรรม" },
                                new Select2Opt() { ID = "SPECIALTY_CLINICS_MIDWIFERY", Text = "คลินิกเฉพาะทางด้านการพยาบาลและผดุงครรภ์" },
                                new Select2Opt() { ID = "UNITED_CLINIC", Text = "สหคลินิก" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            DisplayCondition = APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONC_CONDITION(),
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_COND",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_COND",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "GENERAL_HOSPITAL", Text = "โรงพยาบาลทั่วไป" },
                                new Select2Opt() { ID = "DENTAL_HOSPITAL", Text = "โรงพยาบาลทันตกรรม" },
                                new Select2Opt() { ID = "MIDWIFERY_HOSPITAL", Text = "โรงพยาบาลการพยาบาลและการผดุงครรภ์" },
                                new Select2Opt() { ID = "PHYSICAL_THERAPY", Text = "โรงพยาบาลกายภาพบำบัด" },
                                new Select2Opt() { ID = "THAI_TRADITIONAL", Text = "โรงพยาบาลการแพทย์แผนไทย" },
                                new Select2Opt() { ID = "THAI_TRADITIONAL_APPLIED", Text = "โรงพยาบาลการแพทย์แผนไทยประยุกต์" },
                                new Select2Opt() { ID = "SPECIALIZED_EAR", Text = "โรงพยาบาลเฉพาะทาง หู คอ จมูก" },
                                new Select2Opt() { ID = "SPECIALIZED_HEART_DISEASE", Text = "โรงพยาบาลเฉพาะทาง โรคหัวใจ" },
                                new Select2Opt() { ID = "SPECIALIZED_CANCER", Text = "โรงพยาบาลเฉพาะทางโรคมะเร็ง" },
                                new Select2Opt() { ID = "SPECIFIC_PATIENT", Text = "โรงพยาบาลเฉพาะประเภทผู้ป่วย" },
                                new Select2Opt() { ID = "SPECIALIZED_OTHER", Text = "โรงพยาบาลเฉพาะทางอื่นๆ" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            DisplayCondition = APP_HOSPITAL_OPERATION_EDIT_B_SECD_COND_CONDITION(),
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECD",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONB_CONDITION(),
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONF",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONF",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONG",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECD_CONG",
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
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                    }
                });

            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_OPERATION_EDIT_B_SECE").Count() == 0) 
            {

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECE",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECE_CONA",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECE_CONA",
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
                                new Select2Opt() { ID = "THURSDAY", Text = "พฤหัสบดี" },
                                new Select2Opt() { ID = "FRIDAY", Text = "ศุกร์" },
                                new Select2Opt() { ID = "SATURDAY", Text = "เสาร์" },
                                new Select2Opt() { ID = "SUNDAY", Text = "อาทิตย์" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECE_CONB",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECE_CONB",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = FormSectionRow.optWorkingTime,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECE_CONC",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECE_CONC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = FormSectionRow.optHospitalWorkingTime,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                    }
                });

            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_OPERATION_EDIT_B_SECF").Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_OPERATION_EDIT_B_SECF",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECF_CONA",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECF_CONA",
                            Info = "APP_HOSPITAL_OPERATION_EDIT_B_SECF_CONA_INFO",
                            DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "APP_HOSPITAL_OPERATION_EDIT_B_SECF_CONA_CHKA", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                        	//DisplayCondition = CONDITION_APP_CLINIC_EDIT_CONFIRM_SECTION_OPERATOR_CONFIRM(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },

                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_OPERATION_EDIT_B_SECF_CONB",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_OPERATION_EDIT_B_SECF_CONB",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "APP_HOSPITAL_OPERATION_EDIT_B_SECF_CONB_CHKA", /* ข้าพเจ้ายินยอมให้บุคคลดังกล่าวข้างต้น เป็นผู้ดำเนินการสถานพยาบาลแห่งนี้ */
                            },
                        	//DisplayCondition = CONDITION_APP_CLINIC_EDIT_CONFIRM_SECTION_OPERATOR_CONFIRM_2(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
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
