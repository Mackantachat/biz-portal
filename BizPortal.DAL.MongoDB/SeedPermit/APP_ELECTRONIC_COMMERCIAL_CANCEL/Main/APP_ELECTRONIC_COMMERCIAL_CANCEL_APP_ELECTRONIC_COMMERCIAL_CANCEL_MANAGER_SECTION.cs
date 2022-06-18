
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_CANCEL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_CANCEL_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL_CANCEL",
                    Type = SectionType.ArrayOfForms,
					Info = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_INFO",
					DefaultShowInfo = true,
                    EmptyDataMessage = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_EMPTY",
                    AddButtonText = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADD",
                    SubmitButtonText = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_SUBMIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_CANCEL,
                    },
					Ordering = 4,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_04_05",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_TITLE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 2,
                            Select2Opts = DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_TITLE(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_06",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_FIRSTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_FIRSTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 5,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_07",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_LASTNAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_LASTNAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 5,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_08",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ID_CARD",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ID_CARD",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_09",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_BIRTH_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_BIRTH_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                        	IsShowAge = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_10",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_NATIONALITY",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_NATIONALITY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            Select2Opts = DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_NATIONALITY(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_11",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_ADDRESS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                        	AddressForm_ShowVillageControl = true,
                        	AddressForm_ShowBuildingControl = true,
                        	AddressForm_ShowPostCodeControl = true,
                        	AddressForm_ShowTelephoneControl = true,
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
