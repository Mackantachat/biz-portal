
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_EDIT
{
    public partial class APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION").Count() == 0)
            {
                //           items.Add(new FormSection()
                //           {
                //               Section = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION",
                //               SectionGroup = "APP_HOSPITAL_PERMISSION_EDIT",
                //               Type = SectionType.Form,
                //               ShowOnSpecificApps = true,
                //               AppSystemNames = new string[] {
                //                   AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                //               },
                //Ordering = 12,
                //DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION(),
                //DisableCondition = DISABLE_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION(),
                //ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                //           });

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITB",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT,
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT,
                    },
                    Ordering = 12,
                    //DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION(),
                    //DisableCondition = DISABLE_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION(),
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F47_01_41",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_STATUS",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_STATUS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_STATUS_OPTION",
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
                            FieldID = "F47_01_42",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_TYPE",
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
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_TYPE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_43",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_HOSPITAL_LICENSE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_HOSPITAL_LICENSE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_HOSPITAL_LICENSE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_44",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_HOSPITAL_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_HOSPITAL_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_HOSPITAL_NAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_45",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_ADDRESS",
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
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_WORKING_STATUS_SECTION_ADDRESS(),
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
