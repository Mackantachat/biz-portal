
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_CANCEL
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_CANCEL_APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_EMPTY",
                    AddButtonText = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_ADD",
                    SubmitButtonText = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_SUBMIT",
					ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_CANCEL,
                    },
					Ordering = 2,
					ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_04_02",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_ORGANIC_TYPE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_ORGANIC_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_04_03",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_AREA_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_AREA_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F49_04_04",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_LICENCE_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_LICENCE_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F49_04_05",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_ORGANIC_TYPE_SECTION_PRODUCING_LOCATION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            HideLabel = true,
                            AddressForm_ShowVillageControl = true,
                        	AddressForm_ShowBuildingControl = true,
                        	AddressForm_ShowPostCodeControl = true,
                        	AddressForm_ShowTelephoneControl = true,
                            AddressForm_ShowFaxControl = true,
                            ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_CANCEL",
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
