
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_EDIT
{
    public partial class APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION").Count() == 0)
            {
                //           items.Add(new FormSection()
                //           {
                //               Section = "APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION",
                //               SectionGroup = "APP_HOSPITAL_PERMISSION_EDIT",
                //               Type = SectionType.Form,
                //               ShowOnSpecificApps = true,
                //               AppSystemNames = new string[] {
                //                   AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                //               },
                //Ordering = 14,
                //HideSectionHeader = true,
                //DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION(),
                //DisableCondition = DISABLE_APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION(),
                //ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                //           });

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITB",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT,
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT,
                    },
                    Ordering = 14,
                    HideSectionHeader = true,
                    //DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION(),
                    //DisableCondition = DISABLE_APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F47_01_49",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION_OPERATOR_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION_OPERATOR_CONFIRM",
                            Info = "APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION_OPERATOR_CONFIRM_INFO",
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
                        	//DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION_OPERATOR_CONFIRM(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F47_01_50",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION_OPERATOR_CONFIRM_2",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION_OPERATOR_CONFIRM_2",
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
                        	//DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_CONFIRM_SECTION_OPERATOR_CONFIRM_2(),
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
