
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_INFO_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                    },
					Ordering = 11,
					ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_TYPE(),
                         
                        new FormControl()
                        {
                            FieldID = "F37_01_02",
                            Control = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_REQUEST_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_REQUEST_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_REQUEST_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "NORMAL", RadioButtonText = "พาณิชย์" },
                        			new FormRadioButton() { RadioButtonValue = "ELECTRONIC", RadioButtonText = "พาณิชย์อิเล็กทรอนิกส์" },
                                }
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
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
