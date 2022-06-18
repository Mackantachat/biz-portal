
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_EDIT
{
    public partial class APP_CLINIC_EDIT_APP_CLINIC_EDIT_INFO_SECTION_4
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_EDIT_INFO_SECTION_4").Count() == 0)
            {
     //           items.Add(new FormSection()
     //           {
     //               Section = "APP_CLINIC_EDIT_INFO_SECTION_4",
     //               SectionGroup = "APP_CLINIC_EDIT",
     //               Type = SectionType.Form,
					//Info = "APP_CLINIC_EDIT_INFO_SECTION_4_INFO",
					//DefaultShowInfo = true,
     //               ShowOnSpecificApps = true,
     //               AppSystemNames = new string[] {
     //                   AppSystemNameTextConst.APP_CLINIC_EDIT,
     //               },
					//Ordering = 6,
					//DisplayCondition = CONDITION_APP_CLINIC_EDIT_INFO_SECTION_4(),
					//DisableCondition = DISABLE_APP_CLINIC_EDIT_INFO_SECTION_4(),
					//ResourceName = "PermitResource.RESOURCE_APP_CLINIC_EDIT",
     //           });

                items.Add(new FormSection()
                {
                    Section = "APP_CLINIC_EDIT_INFO_SECTION_4",
                    SectionGroup = "APP_CLINIC_EDIT_CUS_OPERTION",
                    Type = SectionType.Form,
                    Info = "APP_CLINIC_EDIT_INFO_SECTION_4_INFO",
                    DefaultShowInfo = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT,
                        AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT,
                    },
                    Ordering = 6,
                    //DisplayCondition = CONDITION_APP_CLINIC_EDIT_INFO_SECTION_4(),
                    //DisableCondition = DISABLE_APP_CLINIC_EDIT_INFO_SECTION_4(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_CLINIC_EDIT_INFO_SECTION_4").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_CLINIC_EDIT_INFO_SECTION_4",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F47_01_15",
                            Control = "APP_CLINIC_EDIT_INFO_SECTION_4_BECAUSE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_CLINIC_EDIT_INFO_SECTION_4_BECAUSE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_CLINIC_EDIT_INFO_SECTION_4_BECAUSE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "LICENSEE_CHANGE", RadioButtonText = "ผู้รับอนุญาตให้ประกอบกิจการสถานพยาบาลประสงคจ์ะเปลี่ยน" },
                        			new FormRadioButton() { RadioButtonValue = "LICENSEE_LEAVE", RadioButtonText = "ผู้ดำเนินการสถานพยาบาลเดิมไม่ประสงค์จะเป็นผู้ดำเนินการสถานพยาบาลต่อไป " },
                                }
                            },
                        	//DisplayCondition = CONDITION_APP_CLINIC_EDIT_INFO_SECTION_4_BECAUSE(),
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
