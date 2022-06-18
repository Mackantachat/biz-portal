
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_EDIT
{
    public partial class APP_ELECTRONIC_COMMERCIAL_EDIT_APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL_EDIT",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_EDIT,
                    },
					Ordering = 2,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION(),
					DisableCondition = DISABLE_APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_TYPE(),
                         
                        new FormControl()
                        {
                            FieldID = "F37_03_03",
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] 
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.JSExpression, JSExpression = "return APP_ELECTRONIC_COMMERCIAL_EDIT_validateType();", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE_FORBIDDEN_CHANGE" },
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "NORMAL", RadioButtonText = "พาณิชย์" },
                        			new FormRadioButton() { RadioButtonValue = "ELECTRONIC", RadioButtonText = "พาณิชย์อิเล็กทรอนิกส์" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_03_04",
                            Control = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE_FROM_DBD",
                            Type = ControlType.Hidden,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_EDIT_INFO_SECTION_REQUEST_TYPE_FROM_DBD",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            HideLabel = true
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
