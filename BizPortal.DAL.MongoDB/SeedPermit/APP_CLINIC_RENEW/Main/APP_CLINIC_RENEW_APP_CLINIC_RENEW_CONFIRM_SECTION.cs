
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_RENEW
{
    public partial class APP_CLINIC_RENEW_APP_CLINIC_RENEW_CONFIRM_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_RENEW_CONFIRM_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_RENEW_CONFIRM_SECTION",
                    SectionGroup = "APP_CLINIC_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC_RENEW,
                    },
					Ordering = 4,
					HideSectionHeader = true,
					ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                });

                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_RENEW_CONFIRM_SECTION",
                    SectionGroup = "APP_CLINIC_OPERATION_RENEW_SECTION_GROUP",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_CLINIC_OPERATION_RENEW,
                    },
                    Ordering = 4,
                    HideSectionHeader = true,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_RENEW_CONFIRM_SECTION").Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_RENEW_CONFIRM_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            //Control = "APP_CLINIC_RENEW_CONFIRM_SECTION_RENEW_OUT_OF_TIME_CONFIRM",
                            Control = "APP_CLINIC_RENEW_CONFIRM_A",
                            Type = ControlType.CheckBox,
                            //DataKey = "APP_CLINIC_RENEW_CONFIRM_SECTION_RENEW_OUT_OF_TIME_CONFIRM",
                            DataKey = "APP_CLINIC_RENEW_CONFIRM_A",
                            DefaultShowInfo = true,
                            Info = "APP_CLINIC_RENEW_CONFIRM_A_INFO",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },                         
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {

                                "EMPLOYEE_CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                                //"OF_TIME_CONFIRM", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                                //"EMPLOYEE_OWNED_CONFIRM_TRUE", /* ข้าพเจ้ายินยอมให้บุคคลดังกล่าวข้างต้น เป็นผู้ดำเนินการสถานพยาบาลแห่งนี้ */

                            },
                            //DisplayCondition = CONDITION_APP_CLINIC_RENEW_CONFIRM_SECTION_RENEW_CONFIRM(),
                            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                            //CheckboxConfigs = new FormControl.CheckboxConfig
                            //{
                            //    CheckMin = true,
                            //    Min = 3,
                            //}
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_RENEW_CONFIRM_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "42_02_22",
                            Control = "APP_CLINIC_RENEW_CONFIRM_SECTION_RENEW_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_CLINIC_RENEW_CONFIRM_SECTION_RENEW_CONFIRM",
                            Info = "APP_CLINIC_RENEW_CONFIRM_SECTION_RENEW_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "EMPLOYEE_CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                        	DisplayCondition = CONDITION_APP_CLINIC_RENEW_CONFIRM_SECTION_RENEW_CONFIRM(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_RENEW_CONFIRM_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "42_02_23",
                            Control = "APP_CLINIC_RENEW_CONFIRM_SECTION_EMPLOYEE_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_CLINIC_RENEW_CONFIRM_SECTION_EMPLOYEE_CONFIRM",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "EMPLOYEE_OWNED_CONFIRM_TRUE", /* ข้าพเจ้ายินยอมให้บุคคลดังกล่าวข้างต้น เป็นผู้ดำเนินการสถานพยาบาลแห่งนี้ */
                            },
                        	DisplayCondition = CONDITION_APP_CLINIC_RENEW_CONFIRM_SECTION_EMPLOYEE_CONFIRM(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                        },
                    }
                });

                //items.Add(new FormSectionRow()
                //{
                //    Section = "APP_CLINIC_RENEW_CONFIRM_SECTION",
                //    RowNumber = 0,
                //    Controls = new List<FormControl>()
                //    {

                //        new FormControl()
                //        {
                //            FieldID = "42_02_22",
                //            //Control = "APP_CLINIC_RENEW_CONFIRM_SECTION_RENEW_CONFIRM",
                //            Control = "APP_CLINIC_RENEW_CONFIRM_B",
                //            Type = ControlType.CheckBox,
                //            //DataKey = "APP_CLINIC_RENEW_CONFIRM_SECTION_RENEW_CONFIRM",
                //            DataKey = "APP_CLINIC_RENEW_CONFIRM_B",
                //            Info = "APP_CLINIC_RENEW_CONFIRM_SECTION_RENEW_CONFIRM_INFO",

                //            DefaultShowInfo = true,
                //            IdentityTypes = new UserTypeEnum[] {
                //                UserTypeEnum.Juristic,
                //                UserTypeEnum.Citizen,
                //            },
                //            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                //            ColFixed = 12,
                //            CheckboxName = new string[]
                //            {
                //                "EMPLOYEE_CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                //            },
                //            //DisplayCondition = CONDITION_APP_CLINIC_RENEW_CONFIRM_SECTION_RENEW_CONFIRM(),
                //            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                //            HideLabel = true,
                //        },
                //    }
                //});
                //items.Add(new FormSectionRow()
                //{
                //    Section = "APP_CLINIC_RENEW_CONFIRM_SECTION",
                //    RowNumber = 1,
                //    Controls = new List<FormControl>()
                //    {

                //        new FormControl()
                //        {
                //            FieldID = "42_02_23",
                //            //Control = "APP_CLINIC_RENEW_CONFIRM_SECTION_EMPLOYEE_CONFIRM",
                //            Control = "APP_CLINIC_RENEW_CONFIRM_C",
                //            Type = ControlType.CheckBox,
                //            //DataKey = "APP_CLINIC_RENEW_CONFIRM_SECTION_EMPLOYEE_CONFIRM",
                //            DataKey = "APP_CLINIC_RENEW_CONFIRM_C",
                //            IdentityTypes = new UserTypeEnum[] {
                //                UserTypeEnum.Juristic,
                //                UserTypeEnum.Citizen,
                //            },
                //            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                //            ColFixed = 12,
                //            CheckboxName = new string[]
                //            {
                //                "EMPLOYEE_OWNED_CONFIRM_TRUE", /* ข้าพเจ้ายินยอมให้บุคคลดังกล่าวข้างต้น เป็นผู้ดำเนินการสถานพยาบาลแห่งนี้ */
                //            },
                //            //DisplayCondition = CONDITION_APP_CLINIC_RENEW_CONFIRM_SECTION_EMPLOYEE_CONFIRM(),
                //            ResourceName = "PermitResource.RESOURCE_APP_CLINIC_RENEW",
                //            HideLabel = true,
                //        },
                //    }
                //});
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
    }
}
