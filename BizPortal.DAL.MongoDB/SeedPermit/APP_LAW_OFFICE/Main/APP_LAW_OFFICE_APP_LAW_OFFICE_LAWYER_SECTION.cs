
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_LAW_OFFICE
{
    public partial class APP_LAW_OFFICE_APP_LAW_OFFICE_LAWYER_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_LAW_OFFICE_LAWYER_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_LAW_OFFICE_LAWYER_SECTION",
                    SectionGroup = "APP_LAW_OFFICE",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_LAW_OFFICE_LAWYER_SECTION_EMPTY",
                    AddButtonText = "APP_LAW_OFFICE_LAWYER_SECTION_ADD",
                    SubmitButtonText = "APP_LAW_OFFICE_LAWYER_SECTION_SUBMIT",
					ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_LAW_OFFICE,
                    },
					Ordering = 2,
					ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_LAW_OFFICE_LAWYER_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_LAW_OFFICE_LAWYER_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F36_01_02",
                            Control = "APP_LAW_OFFICE_LAWYER_SECTION_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_LAW_OFFICE_LAWYER_SECTION_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 2,
                            Select2Opts = DROPDOWN_APP_LAW_OFFICE_LAWYER_SECTION_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_01_03",
                            Control = "APP_LAW_OFFICE_LAWYER_SECTION_FIRSTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_LAWYER_SECTION_FIRSTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_01_04",
                            Control = "APP_LAW_OFFICE_LAWYER_SECTION_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_LAWYER_SECTION_LASTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 5,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_LAW_OFFICE_LAWYER_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F36_01_05",
                            Control = "APP_LAW_OFFICE_LAWYER_SECTION_LICENSE_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_LAWYER_SECTION_LICENSE_NUMBER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", "{ pattern: /[0-9//]/, optional: true, recursive: true }"
                                }
                            },
                            ColFixed = 6,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE",
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
