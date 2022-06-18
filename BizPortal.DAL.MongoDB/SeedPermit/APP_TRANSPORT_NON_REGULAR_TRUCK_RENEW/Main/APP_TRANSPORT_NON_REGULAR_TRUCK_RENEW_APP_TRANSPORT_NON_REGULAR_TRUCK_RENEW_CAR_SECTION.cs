
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION",
                    SectionGroup = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                    Type = SectionType.ArrayOfForms,
					Info = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_INFO",
					DefaultShowInfo = true,
                    EmptyDataMessage = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_EMPTY",
                    AddButtonText = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_ADD",
                    SubmitButtonText = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_SUBMIT",
					ArrayRequiredAtLeast = true,
                    NumberRequiredAtLeast = 1,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW,
                    },
					Ordering = 3,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F39_02_03",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_REQUEST_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_REQUEST_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "HAVE_CAR", Text = "มีตัวรถแล้ว" },
                                new Select2Opt() { ID = "NOT_HAVE_CAR", Text = "ขอในหลักการ (ยังไม่มีรถ)" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_02_04",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_LICENSE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_LICENSE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	DisplayCondition = CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_LICENSE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_02_05",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_CASSIS",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_CASSIS",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	DisplayCondition = CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_CASSIS(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_02_06",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_ENGINE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_ENGINE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 4,
                        	DisplayCondition = CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_ENGINE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_02_07",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_BRAND",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_BRAND",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = DROPDOWN_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_BRAND(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_BRAND(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_02_08",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                        	DisplayCondition = CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_OTHER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_02_09",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_TYPE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "PICKUP", Text = "ลักษณะ1(รถกระบะบรรทุก)" },
                                new Select2Opt() { ID = "VAN", Text = "ลักษณะ2(รถตู้บรรทุก)" },
                                new Select2Opt() { ID = "LIQUID", Text = "ลักษณะ3(รถบรรทุกของเหลว)" },
                                new Select2Opt() { ID = "DANGER", Text = "ลักษณะ4(รถบรรทุกวัสดุอันตราย)" },
                                new Select2Opt() { ID = "SPECIAL", Text = "ลักษณะ5(รถบรรทุกเฉพาะกิจ)" },
                                new Select2Opt() { ID = "TRAILER", Text = "ลักษณะ6(รถพ่วง)" },
                                new Select2Opt() { ID = "SEMI_TRAILER", Text = "ลักษณะ7(รถกึ่งพ่วง)" },
                                new Select2Opt() { ID = "SEMI_TRAILER_LONG", Text = "ลักษณะ8(รถกึ่งพ่วงบรรทุกวัสดุยาว)" },
                                new Select2Opt() { ID = "TOWING_TRUCK", Text = "ลักษณะ9(รถลากจูง)" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	DisplayCondition = CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_TYPE(),
                        	ResourceName = "PermitResource.RESOURCE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                        },
                        new FormControl()
                        {
                            FieldID = "F39_02_10",
                            Control = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	DisplayCondition = CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_CAR_SECTION_AMOUNT(),
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
