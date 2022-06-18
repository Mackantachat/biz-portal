
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                    SectionGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_EMPTY",
                    AddButtonText = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADD",
                    SubmitButtonText = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_SUBMIT",
					ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW,
                    },
					Ordering = 5,
					ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_02_27",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_ADDRESS",
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
                        	AddressForm_ShowMapControl = true,
                        	AddressForm_ShowDeed = true,
                            AddressForm_IgnoreAddressControl = true,
                            ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F39_02_28",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                        	RadioGroup = RADIO_GROUP_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_JURISTIC(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F39_02_29",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                        	RadioGroup = RADIO_GROUP_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_TYPE_CITIZEN(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F39_02_30",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                        	RadioGroup = RADIO_GROUP_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE(),
                        	DisplayCondition = CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_BUILDING_RENTING_OWNER_TYPE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
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
