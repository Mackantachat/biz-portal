
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_CANCEL
{
    public partial class APP_ENERGY_PRODUCTION_CANCEL_APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION",
                    SectionGroup = "APP_ENERGY_PRODUCTION_CANCEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ENERGY_PRODUCTION_CANCEL,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_CANCEL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F38_04_01",
                            Control = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_CANCEL_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_CANCEL_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_CANCEL_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "SOME_NON_RENEWABLE", RadioButtonText = "ยกเลิกบางส่วน/ไม่มีทดแทน" },
                                    new FormRadioButton() { RadioButtonValue = "SOME_RENEWABLE", RadioButtonText = "ยกเลิกบางส่วน/มีทดแทน" },
                                    new FormRadioButton() { RadioButtonValue = "ALL", RadioButtonText = "ยกเลิกทั้งหมด" },
                                }
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_CANCEL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F38_04_02",
                            Control = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_OBJECTIVE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_OBJECTIVE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            Textbox_Rows = 4,
                        	DisplayCondition = CONDITION_APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_CANCEL_TYPE_FOR_OBJECTIVE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ENERGY_PRODUCTION_CANCEL",
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
