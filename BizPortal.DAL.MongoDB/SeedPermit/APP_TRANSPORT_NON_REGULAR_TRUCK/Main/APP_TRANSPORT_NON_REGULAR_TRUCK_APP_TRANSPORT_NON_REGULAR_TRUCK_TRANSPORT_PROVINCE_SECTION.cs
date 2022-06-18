
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION",
                    SectionGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION_EMPTY",
                    AddButtonText = "APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION_ADD",
                    SubmitButtonText = "APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION_SUBMIT",
					ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK,
                    },
					Ordering = 2,
					HideSectionHeader = true,
					DisplayCondition = CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION(),
					DisableCondition = DISABLE_APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_01_05",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION_PROVINCE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION_PROVINCE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            IsAjaxDropdown = true,
                        	AjaxUrl = APP_TRANSPORT_NON_REGULAR_TRUCK_TRANSPORT_PROVINCE_SECTION_PROVINCE_URL(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK",
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
