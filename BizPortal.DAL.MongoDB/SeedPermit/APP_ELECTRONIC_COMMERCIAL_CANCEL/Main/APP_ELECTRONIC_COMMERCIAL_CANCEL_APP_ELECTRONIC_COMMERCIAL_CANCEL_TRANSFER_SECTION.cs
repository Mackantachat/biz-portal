
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_CANCEL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_CANCEL_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL_CANCEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_CANCEL,
                    },
					Ordering = 8,
					DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION(),
					DisableCondition = DISABLE_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION(),
					ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_04_15",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_COMMERCIAL_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_COMMERCIAL_NUMBER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 4,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_COMMERCIAL_NUMBER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_16",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_REQUEST_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_REQUEST_NUMBER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 4,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_REQUEST_NUMBER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                         
                        CUSTOM_FORM_CONTROL_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_SEARCH(),
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_04_18",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 2,
                            Select2Opts = DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_TITLE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_19",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_FIRSTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_FIRSTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 5,
                        	DisplayReadonly = true,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_FIRSTNAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_20",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_LASTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 5,
                        	DisplayReadonly = true,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_LASTNAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_21",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ID_CARD",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ID_CARD",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ID_CARD(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_22",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_NATIONALITY",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_NATIONALITY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            Select2Opts = DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_NATIONALITY(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_NATIONALITY(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_23",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_COMMERCIAL_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_COMMERCIAL_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_COMMERCIAL_NAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_24",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	DisplayStaticIfHasData = true,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_DATE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_25",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                        	AddressForm_ShowVillageControl = true,
                        	AddressForm_ShowBuildingControl = true,
                        	AddressForm_ShowPostCodeControl = true,
                        	AddressForm_ShowTelephoneControl = true,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_ADDRESS(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_26",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_CAUSE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_CAUSE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                            Textbox_Rows = 3,
                        	DisplayReadonly = true,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_SECTION_CAUSE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
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
