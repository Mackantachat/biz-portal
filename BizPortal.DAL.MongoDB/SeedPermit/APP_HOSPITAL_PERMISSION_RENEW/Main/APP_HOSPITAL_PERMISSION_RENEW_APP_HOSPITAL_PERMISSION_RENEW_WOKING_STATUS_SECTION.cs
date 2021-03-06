
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_RENEW
{
    public partial class APP_HOSPITAL_PERMISSION_RENEW_APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_RENEW",
                    Type = SectionType.Form,
					Info = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_INFO",
					DefaultShowInfo = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_RENEW,
                    },
					Ordering = 2,
					DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                });

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION",
                    SectionGroup = "APP_HOSPITAL_OPERATION_RENEW",
                    Type = SectionType.Form,
                    Info = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_INFO",
                    DefaultShowInfo = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION_RENEW,
                    },
                    Ordering = 2,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                });
            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTIONA").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTIONA",
                    SectionGroup = "APP_HOSPITAL_OPERATION_RENEW",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTIONA_EMPTY",
                    AddButtonText = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTIONA_ADD",
                    SubmitButtonText = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTIONA_SUBMIT",
                    ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION_RENEW,
                    },
                    Ordering = 3,
                    HideSectionHeader = true,
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION").Count() == 0)
            {
                //items.Add(new FormSectionRow()
                //{
                //    Section = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION",
                //    RowNumber = 0,
                //    Controls = new List<FormControl>()
                //    {
                //        new FormControl()
                //        {
                //            Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_RENEW_ID",
                //            Type = ControlType.TextBox,
                //            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_RENEW_ID",
                //            IdentityTypes = new UserTypeEnum[] {
                //                UserTypeEnum.Juristic,
                //                UserTypeEnum.Citizen,
                //            },
                //            ColFixed = 6,
                //            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                //            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                //        },
                //    }
                //});
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_06",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_RENEW_TYPE",
                            Type = ControlType.Label,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_RENEW_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_07",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_DATE",
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
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "47_02_08",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_RENEW_STATUS_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "OPARETION_RENEW", RadioButtonText = "?????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????" },
                        			new FormRadioButton() { RadioButtonValue = "EMPLOYEE_RENEW", RadioButtonText = "??????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????? ???????????????????????????????????????????????? ???????????????????????????????????????????????? ??????????????????" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                         
                        //new FormControl()
                        //{
                        //    FieldID = "47_02_09",
                        //    Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                        //    Type = ControlType.RadioGroup,
                        //    DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE",
                        //    IdentityTypes = new UserTypeEnum[] {
                        //        UserTypeEnum.Juristic,
                        //        UserTypeEnum.Citizen,
                        //    },
                        //    ColFixed = 12,
                        //    RadioGroup = new FormRadioGroup()
                        //    {
                        //        RadioGroupName = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE_OPTION",
                        //        RadioButtons = new FormRadioButton[]
                        //        {
                        //			new FormRadioButton() { RadioButtonValue = "CLINIC_RENEW", RadioButtonText = "????????????????????????????????????????????????????????????????????????????????????????????????" },
                        //			new FormRadioButton() { RadioButtonValue = "HOSPITAL_RENEW", RadioButtonText = "???????????????????????????????????????????????????????????????????????????????????????" },
                        //        }
                        //    },
                        //	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_OPARETER_TYPE(),
                        //	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        //},
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_10",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "MEDICAL_CLINIC", Text = "???????????????????????????????????????" },
                                new Select2Opt() { ID = "DENTAL_CLINIC", Text = "??????????????????????????????????????????" },
                                new Select2Opt() { ID = "MIDWIFERY_CLINIC", Text = "??????????????????????????????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "MEDICAL_TECHNOLOGY_CLINIC", Text = "????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "THAI_TRADITIONAL_MEDICINE_CLINIC", Text = "????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "APPLIED_THAI_TRADITIONAL_MEDICINE_CLINIC", Text = "????????????????????????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "MEDICINE_CLINIC", Text = "?????????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "SPECIALIZED_MEDICAL_CLINIC", Text = "???????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "SPECIALIZED_DENTAL_CLINIC", Text = "??????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "SPECIALTY_CLINICS_MIDWIFERY", Text = "?????????????????????????????????????????????????????????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "UNITED_CLINIC", Text = "????????????????????????" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_CLINIC_DETAIL(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_11",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_CLINIC_CHOICE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_CLINIC_CHOICE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "THERAPY", Text = "????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "INTERPRETATION_DISORDERS", Text = "???????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "HEART_AND_CHEST_TECHNOLOGY", Text = "?????????????????????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "TECHNICAL_RADIATION", Text = "?????????????????????????????????????????????" },
                                new Select2Opt() { ID = "CLINICAL_PSYCHOLOGY", Text = "??????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "ORTHOTICS", Text = "??????????????????????????????????????????" },
                                new Select2Opt() { ID = "TRADITIONAL_CHINESE", Text = "??????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "OTHER", Text = "???????????????" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_CLINIC_CHOICE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_12",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_CLINIC_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_CLINIC_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_CLINIC_OTHER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_13",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_HOSPITAL_DETAIL",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_HOSPITAL_DETAIL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "GENERAL_HOSPITAL", Text = "?????????????????????????????????????????????" },
                                new Select2Opt() { ID = "DENTAL_HOSPITAL", Text = "???????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "MIDWIFERY_HOSPITAL", Text = "???????????????????????????????????????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "PHYSICAL_THERAPY", Text = "????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "THAI_TRADITIONAL", Text = "?????????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "THAI_TRADITIONAL_APPLIED", Text = "?????????????????????????????????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "SPECIALIZED_EAR", Text = "??????????????????????????????????????????????????? ?????? ?????? ????????????" },
                                new Select2Opt() { ID = "SPECIALIZED_HEART_DISEASE", Text = "??????????????????????????????????????????????????? ????????????????????????" },
                                new Select2Opt() { ID = "SPECIALIZED_CANCER", Text = "??????????????????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "SPECIFIC_PATIENT", Text = "?????????????????????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "SPECIALIZED_OTHER", Text = "??????????????????????????????????????????????????????????????????" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_HOSPITAL_DETAIL(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_14",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_HOSPITAL_CHOICE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_HOSPITAL_CHOICE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "CHRONIC_PATIENTS", Text = "????????????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "PSYCHIATRIC", Text = "??????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "ELDERLY", Text = "?????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "MOTHER_CHILD", Text = "?????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "DRUG_TREATMENT", Text = "??????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "SPECIFIC_PATIENT", Text = "?????????????????????????????????????????????????????????????????????????????????" },
                                new Select2Opt() { ID = "OTHER_PATIENTS", Text = "????????????????????????????????????????????????????????????????????????????????????????????????" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_HOSPITAL_CHOICE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_15",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_HOSPITAL_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_HOSPITAL_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_HOSPITAL_OTHER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_16",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_EMPLOYEE_LICENSE_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_EMPLOYEE_LICENSE_NUMBER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "47_02_17",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_HOSPITAL_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_HOSPITAL_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION",
                    RowNumber = 5,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "47_02_18",
                            Control = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTION_ADDRESS",
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
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },
                    }
                });
            }

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTIONA").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_RENEW_WOKING_STATUS_SECTIONA",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "HOPITAL_RENEW_CONA",
                            Type = ControlType.Dropdown,
                            DataKey = "HOPITAL_RENEW_CONA",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "MONDAY", Text = "??????????????????" },
                                new Select2Opt() { ID = "TUESDAY", Text = "??????????????????" },
                                new Select2Opt() { ID = "WEDNESDAY", Text = "?????????" },
                                new Select2Opt() { ID = "THURSDAY", Text = "???????????????" },
                                new Select2Opt() { ID = "FRIDAY", Text = "???????????????" },
                                new Select2Opt() { ID = "SATURDAY", Text = "???????????????" },
                                new Select2Opt() { ID = "SUNDAY", Text = "?????????????????????" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },

                        new FormControl()
                        {
                            Control = "HOPITAL_RENEW_CONB",
                            Type = ControlType.Dropdown,
                            DataKey = "HOPITAL_RENEW_CONB",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Select2Opts = FormSectionRow.optWorkingTime,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
                        },

                        new FormControl()
                        {
                            Control = "HOPITAL_RENEW_CONC",
                            Type = ControlType.Dropdown,
                            DataKey = "HOPITAL_RENEW_CONC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                            Select2Opts = FormSectionRow.optWorkingTime,
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_RENEW",
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
