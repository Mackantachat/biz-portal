
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;
using BizPortal.Utils.Helpers;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_EDIT
{
    public partial class APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION").Count() == 0)
            {
                //           items.Add(new FormSection()
                //           {
                //               Section = "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION",
                //               SectionGroup = "APP_HOSPITAL_PERMISSION_EDIT",
                //               Type = SectionType.Form,
                //               ShowOnSpecificApps = true,
                //               AppSystemNames = new string[] {
                //                   AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                //               },
                //Ordering = 11,
                //DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION(),
                //DisableCondition = DISABLE_APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION(),
                //ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                //           });

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITB",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT,
                        AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT,
                    },
                    Ordering = 11,
                    //DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION(),
                    //DisableCondition = DISABLE_APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION(),
                    ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        CUSTOM_FORM_CONTROL_APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION_USED_CURRENT_ADDRESS(),
                        new FormControl()
                        {
                            FieldID = "F47_01_39",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION_ADDRESS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                        	AddressForm_ShowVillageControl = true,
                        	AddressForm_ShowBuildingControl = true,
                        	AddressForm_ShowPostCodeControl = true,
                        	AddressForm_ShowEmailControl = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                            AutoFillConditions = new FormControlAutoFillCondition[]
                            {
                                new FormControlAutoFillCondition()
                                {
                                    TriggerAutoFillOnControl = "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION_USED_CURRENT_ADDRESS",
                                    TriggerAutoFillOnSpecificValue = "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_CURRENT_SECTION_USED_CURRENT_ADDRESS",
                                    TriggerAutoFillOnCheckboxControl = true,
                                    TriggerAutoFillOnChecked = true,
                                    AutoFillValueFrom = new FormControlAutoFillSource
                                    {
                                        Section = "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_SECTION",
                                        Control = "APP_HOSPITAL_PERMISSION_EDIT_ADDRESS_SECTION_ADDRESS"
                                    }
                                }
                            }
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
