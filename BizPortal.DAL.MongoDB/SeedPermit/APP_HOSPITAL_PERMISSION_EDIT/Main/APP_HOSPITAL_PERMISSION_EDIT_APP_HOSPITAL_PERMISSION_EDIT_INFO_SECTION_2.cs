
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_EDIT
{
    public partial class APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").Count() == 0)
            {
                //           items.Add(new FormSection()
                //           {
                //               Section = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2",
                //               SectionGroup = "APP_HOSPITAL_PERMISSION_EDIT",
                //               Type = SectionType.Form,
                //Info = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_INFO",
                //DefaultShowInfo = true,
                //               ShowOnSpecificApps = true,
                //               AppSystemNames = new string[] {
                //                   AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                //               },
                //Ordering = 2,
                //DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2(),
                //DisableCondition = DISABLE_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2(),
                //ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                //           });

                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2",
                    SectionGroup = "APP_HOSPITAL_PERMISSION_EDITA",
                    Type = SectionType.Form,
                    Info = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_INFO",
                    DefaultShowInfo = true,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        //AppSystemNameTextConst.APP_CLINIC_BUSINESS_EDIT,
                        AppSystemNameTextConst.APP_HOSPITAL_BUSINESS_EDIT,
                    },
                    Ordering = 2,
                    //DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2(),
                    //DisableCondition = DISABLE_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2(),
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        }
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F47_01_02",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_REQUEST_CHANGE",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_REQUEST_CHANGE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "REQUEST_CHANGE_NAME", /* เปลี่ยนชื่อสถานพยาบาลเป็น */
                                "REQUEST_CHANGE_ADDRESS", /* เปลี่ยนที่อยู่สถานพยาบาล */
                                "REQUEST_CHANGE_LICENSEE", /* เปลี่ยนชื่อตัว ชื่อสกุล ของผู้รับอนุญาต */
                                "REQUEST_CHANGE_WORKING_DATE", /* เปลี่ยนแปลงวัน/เวลาทำการ */
                                "REQUEST_CHANGE_OTHER", /* อื่นๆ */
                            },
                        	//DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_REQUEST_CHANGE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_03",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_NAME",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_NAME(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_04",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_NAME_ENG",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_NAME_ENG",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_NAME_ENG(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
                        },
                        new FormControl()
                        {
                            FieldID = "F47_01_05",
                            Control = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS",
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
                        	DisplayCondition = CONDITION_APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_ADDRESS(),
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PERMISSION_EDIT",
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
