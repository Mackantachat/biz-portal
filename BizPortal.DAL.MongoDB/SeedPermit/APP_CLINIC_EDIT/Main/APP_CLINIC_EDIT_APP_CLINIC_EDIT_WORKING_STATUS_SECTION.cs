
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_EDIT
{
    public partial class APP_CLINIC_EDIT_APP_CLINIC_EDIT_WORKING_STATUS_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_EDIT_WORKING_STATUS_SECTION").Count() == 0)
            {
                //           items.Add(new FormSection()
                //           {
                //               Section = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION",
                //               SectionGroup = "APP_CLINIC_EDIT",
                //               Type = SectionType.Form,
                //               ShowOnSpecificApps = true,
                //               AppSystemNames = new string[] {
                //                   AppSystemNameTextConst.APP_CLINIC_EDIT,
                //               },
                //Ordering = 12,
                //DisplayCondition = CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION(),
                //DisableCondition = DISABLE_APP_CLINIC_EDIT_WORKING_STATUS_SECTION(),
                //ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                //           });

                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION",
                    SectionGroup = "APP_CLINIC_EDIT_CUS_OPERTION",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT,
                        AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT,
                    },
                    Ordering = 12,
                    //DisplayCondition = CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION(),
                    //DisableCondition = DISABLE_APP_CLINIC_EDIT_WORKING_STATUS_SECTION(),
                    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_EDIT_WORKING_STATUS_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F47_01_40",
                            Control = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_STATUS_OPTION",
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
                            FieldID = "F47_01_41",
                            Control = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "CLINIC", RadioButtonText = "ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน" },
                                    new FormRadioButton() { RadioButtonValue = "HOSPITAL", RadioButtonText = "ประเภทที่รับผู้ป่วยไว้ค้างคืน" },
                                }
                            },
                            DisplayCondition = CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_TYPE(),
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_42",
                            Control = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_TYPE",
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
                            DisplayCondition = CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_TYPE(),
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_43",
                            Control = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_MEDICAL_PRACTICE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_MEDICAL_PRACTICE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "THERAPY", Text = "สาขากิจกรรมบำบัด" },
                                new Select2Opt() { ID = "INTERPRETATION_DISORDERS", Text = "สาขาการแก้ไขความผิดปกติของการสื่อความหมาย" },
                                new Select2Opt() { ID = "HEART_AND_CHEST_TECHNOLOGY", Text = "สาขาเทคโนโลยีหัวใจและทรวงอก" },
                                new Select2Opt() { ID = "TECHNICAL_RADIATION", Text = "สาขารังสีเทคนิค" },
                                new Select2Opt() { ID = "CLINICAL_PSYCHOLOGY", Text = "สาขาจิตวิทยาคลินิก" },
                                new Select2Opt() { ID = "ORTHOTICS", Text = "สาขากายอุปกรณ์" },
                                new Select2Opt() { ID = "TRADITIONAL_CHINESE", Text = "สาขาการแพทย์แผนจีน" },
                                new Select2Opt() { ID = "OTHER", Text = "อื่นๆ" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            DisplayCondition = CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_MEDICAL_PRACTICE(),
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_44",
                            Control = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_CLINIC_OTHER(),
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_45",
                            Control = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_TYPE",
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
                            DisplayCondition = CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_TYPE(),
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_46",
                            Control = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_MEDICAL_PRACTICE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_MEDICAL_PRACTICE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "CHRONIC_PATIENTS", Text = "โรงพยาบาลผู้ป่วยเรื้อรัง" },
                                new Select2Opt() { ID = "PSYCHIATRIC", Text = "โรงพยาบาลผู้ป่วยจิตเวช" },
                                new Select2Opt() { ID = "ELDERLY", Text = "โรงพยาบาลผู้สูงอายุ" },
                                new Select2Opt() { ID = "MOTHER_CHILD", Text = "โรงพยาบาลแม่และเด็ก" },
                                new Select2Opt() { ID = "DRUG_TREATMENT", Text = "โรงพยาบาลบำบัดยาเสพติด" },
                                new Select2Opt() { ID = "SPECIFIC_PATIENT", Text = "โรงพยาบาลเฉพาะประเภทผู้ป่วย" },
                                new Select2Opt() { ID = "OTHER_PATIENTS", Text = "โรงพยาบาลเฉพาะประเภทผู้ป่วยอื่นๆ" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            DisplayCondition = CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_MEDICAL_PRACTICE(),
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_47",
                            Control = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_OTHER(),
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                    },
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F47_01_48",
                            Control = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_LICENSE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_LICENSE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_LICENSE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_49",
                            Control = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	//DisplayCondition = CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_HOSPITAL_NAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_50",
                            Control = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_CLINIC_EDIT_WORKING_STATUS_SECTION_ADDRESS",
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
                        	//DisplayCondition = CONDITION_APP_CLINIC_EDIT_WORKING_STATUS_SECTION_ADDRESS(),
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
