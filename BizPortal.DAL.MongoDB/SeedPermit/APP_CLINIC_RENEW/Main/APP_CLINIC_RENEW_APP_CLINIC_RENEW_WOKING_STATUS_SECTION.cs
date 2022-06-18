
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_RENEW
{
    public partial class APP_CLINIC_RENEW_APP_CLINIC_RENEW_WOKING_STATUS_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_RENEW_WOKING_STATUS_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION",
                    SectionGroup = "APP_CLINIC_RENEW",
                    Type = SectionType.Form,
					Info = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_INFO",
					DefaultShowInfo = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC_RENEW,
                    },
					Ordering = 2,
					DisplayCondition = CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                });

                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION",
                    SectionGroup = "APP_CLINIC_OPERATION_RENEW_SECTION_GROUP",
                    Type = SectionType.Form,
                    Info = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_INFO",
                    DefaultShowInfo = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC_OPERATION_RENEW,
                    },
                    Ordering = 2,
                    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_RENEW_WOKING_STATUS_SECTION").Count() == 0)
            {
                //items.Add(new FormSectionRow()
                //{
                //    Section = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION",
                //    RowNumber = 0,
                //    Controls = new List<FormControl>()
                //    {
                //        new FormControl()
                //        {
                //            Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_ID",
                //            Type = ControlType.TextBox,
                //            DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_ID",
                //            IdentityTypes = new UserTypeEnum[] {
                //                UserTypeEnum.Juristic,
                //                UserTypeEnum.Citizen,
                //            },
                //            ColFixed = 6,
                //            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                //            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                //        },
                //    }
                //});

                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_02_06",
                            Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_TYPE",
                            Type = ControlType.Label,
                            DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "42_02_07",
                            Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_DATE",
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
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "42_02_08",
                            Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "OPARETION_RENEW", RadioButtonText = "เป็นผู้ดำเนินการสถานพยาบาลประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน" },
                        			new FormRadioButton() { RadioButtonValue = "EMPLOYEE_RENEW", RadioButtonText = "เป็นผู้ประกอบวิชาชีพหรือปฏิบัติงานหน้าที่อื่นในสถานพยาบาล หรือในส่วนราชการ หรือหน่วยงานอื่น ดังนี้" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                        },

                        //new FormControl()
                        //{
                        //    FieldID = "42_02_09",
                        //    Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                        //    Type = ControlType.RadioGroup,
                        //    DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                        //    IdentityTypes = new UserTypeEnum[] {
                        //        UserTypeEnum.Juristic,
                        //        UserTypeEnum.Citizen,
                        //    },
                        //    ColFixed = 12,
                        //    RadioGroup = new FormRadioGroup()
                        //    {
                        //        RadioGroupName = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE_OPTION",
                        //        RadioButtons = new FormRadioButton[]
                        //        {
                        //            new FormRadioButton() { RadioButtonValue = "CLINIC_RENEW", RadioButtonText = "ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน" },
                        //            new FormRadioButton() { RadioButtonValue = "HOSPITAL_RENEW", RadioButtonText = "ประเภทที่รับผู้ป่วยไว้ค้างคืน" },
                        //        }
                        //    },
                        //    DisplayCondition = CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE(),
                        //    ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                        //},
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_02_10",
                            Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
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
                                new Select2Opt() { ID = "SPECIALTY_CLINICS_MIDWIFERY", Text = "คลินิกกายภาพบำบัด" },
                                new Select2Opt() { ID = "UNITED_CLINIC", Text = "สหคลินิก" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "42_02_11",
                            Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_CHOICE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_CHOICE",
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
                                //new Select2Opt() { ID = "OTHER", Text = "อื่นๆ" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_CHOICE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "42_02_12",
                            Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_OTHER_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_OTHER_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_CLINIC_OTHER_TEXT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_02_13",
                            Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_DETAIL",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_DETAIL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
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
                        	DisplayCondition = CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_DETAIL(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "42_02_14",
                            Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_CHOICE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_CHOICE",
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
                        	DisplayCondition = CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_CHOICE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "42_02_15",
                            Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_OTHER_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_OTHER_TEXT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_OTHER_TEXT(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_02_16",
                            Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_EMPLOYEE_LICENSE_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_EMPLOYEE_LICENSE_NUMBER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	//DisplayCondition = CONDITION_APP_CLINIC_RENEW_WOKING_STATUS_SECTION_EMPLOYEE_LICENSE_NUMBER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                            DisplayMaskInput = true,
                            MaskInputPattern = "00000000000",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                        },
                        new FormControl()
                        {
                            FieldID = "42_02_17",
                            Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_HOSPITAL_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION",
                    RowNumber = 5,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "42_02_18",
                            Control = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_CLINIC_RENEW_WOKING_STATUS_SECTION_ADDRESS",
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
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
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
