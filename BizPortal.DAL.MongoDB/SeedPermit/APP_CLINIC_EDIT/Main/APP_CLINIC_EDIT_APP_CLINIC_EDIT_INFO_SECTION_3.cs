
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_EDIT
{
    public partial class APP_CLINIC_EDIT_APP_CLINIC_EDIT_INFO_SECTION_3
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_EDIT_INFO_SECTION_3").Count() == 0)
            {
     //           items.Add(new FormSection()
     //           {
     //               Section = "APP_CLINIC_EDIT_INFO_SECTION_3",
     //               SectionGroup = "APP_CLINIC_EDIT",
     //               Type = SectionType.Form,
     //               ShowOnSpecificApps = true,
     //               AppSystemNames = new string[] {
     //                   AppSystemNameTextConst.APP_CLINIC_EDIT,
     //               },
					//Ordering = 5,
					//HideSectionHeader = true,
					//ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
     //           });
                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_EDIT_INFO_SECTION_3",
                    SectionGroup = "APP_CLINIC_EDIT_CUS",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_HOSPITAL_BUSINESS_EDIT,
                        AppSystemNameTextConst.APP_CLINIC_BUSINESS_EDIT,
                    },
					Ordering = 5,
					HideSectionHeader = true,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_EDIT_INFO_SECTION_3").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_EDIT_INFO_SECTION_3",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F47_01_13",
                            Control = "APP_CLINIC_EDIT_INFO_SECTION_3_DESCRIPTION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_EDIT_INFO_SECTION_3_DESCRIPTION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	DisplayCondition = CONDITION_APP_CLINIC_EDIT_INFO_SECTION_3_DESCRIPTION(),
                        	ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F47_01_14",
                            Control = "APP_CLINIC_EDIT_INFO_SECTION_3_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_CLINIC_EDIT_INFO_SECTION_3_CONFIRM",
                            Info = "APP_CLINIC_EDIT_INFO_SECTION_3_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
                            },
                        	//DisplayCondition = CONDITION_APP_CLINIC_EDIT_INFO_SECTION_3_CONFIRM(),
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
