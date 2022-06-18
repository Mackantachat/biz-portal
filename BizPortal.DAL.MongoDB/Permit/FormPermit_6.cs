using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using BizPortal.Utils.Helpers;

namespace BizPortal.DAL.MongoDB.Permit
{
    public class FormPermit_6
    {

        public static void InitFormSectionGroup()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_SELL_TOBACCO").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_SELL_TOBACCO",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_TOBACCO,
                    },
                    Ordering = 601,
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }

        public static void InitFormSection()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "SELL_TOBACCO_INFO").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "SELL_TOBACCO_INFO",
                    SectionGroup = "APP_SELL_TOBACCO",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_TOBACCO,
                    },
                    Ordering = 1,
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }

        public static void InitFormSectionRow()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == "SELL_TOBACCO_INFO").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "SELL_TOBACCO_INFO",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "208",
                            Control = "SELL_TOBACCO_TYPE",
                            Type = ControlType.CheckBox,
                            DataKey = "SELL_TOBACCO_TYPE",
                            Info = "SELL_TOBACCO_TYPE__INFO",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "SELL_TOBACCO_TYPE_WHOLE_SELL",
                                "SELL_TOBACCO_TYPE_RETAIL",
                            },  
                            WillTriggerDisplayOfOtherUI = true,
                        },
                        new FormControl()
                        {
                            FieldID = "209",
                            Control = "SELL_TOBACCO_TOBACCO_TYPE",
                            Type = ControlType.CheckBox,
                            DataKey = "SELL_TOBACCO_TOBACCO_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "SELL_TOBACCO_TOBACCO_TYPE_CIGARATE",
                                "SELL_TOBACCO_TOBACCO_TYPE_TOBACCO",
                            },
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                IsOr = true,
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_TOBACCO_TYPE__SELL_TOBACCO_TYPE_WHOLE_SELL",
                                        ControlAnswer = "true",
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_TOBACCO_TYPE__SELL_TOBACCO_TYPE_RETAIL",
                                        ControlAnswer = "true",
                                    },
                                },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                        },
                        new FormControl()
                        {
                            FieldID = "210",
                            Control = "SELL_TOBACCO_SELL_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "SELL_TOBACCO_SELL_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "SELL_TOBACCO_SELL_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() {
                                        RadioButtonValue = "SELL_TOBACCO_SELL_TYPE_TOBACCO",
                                        RadioButtonText = "ขายเส้นที่ผู้ขายเป็นผู้เพาะปลูกต้นยาสูบและผลิตเอง",
                                    },
                                    new FormRadioButton() {
                                        RadioButtonValue = "SELL_TOBACCO_SELL_TYPE_OTHER",
                                        RadioButtonText = "อื่นๆ",
                                    },
                                },
                            },
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                IsOr = false,
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_TOBACCO_TYPE__SELL_TOBACCO_TYPE_WHOLE_SELL",
                                        ControlAnswer = "true",
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_TOBACCO_TOBACCO_TYPE__SELL_TOBACCO_TOBACCO_TYPE_TOBACCO",
                                        ControlAnswer = "true",
                                    },
                                },
                            },
                        },
                        new FormControl() {
                            FieldID = "210*",
                            Control = "SELL_TOBACCO_SELL_TYPE_OTHER_INFO",
                            Type = ControlType.TextBox,
                            DataKey = "SELL_TOBACCO_SELL_TYPE_OTHER_INFO",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                IsOr = false,
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_TOBACCO_TYPE__SELL_TOBACCO_TYPE_WHOLE_SELL",
                                        ControlAnswer = "true",
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_TOBACCO_TOBACCO_TYPE__SELL_TOBACCO_TOBACCO_TYPE_TOBACCO",
                                        ControlAnswer = "true",
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_TOBACCO_SELL_TYPE",
                                        ControlAnswer = "SELL_TOBACCO_SELL_TYPE_OTHER",
                                    },
                                },
                            },
                        },
                    },
                });
                items.Add(new FormSectionRow()
                {
                    Section = "SELL_TOBACCO_INFO",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "211",
                            Control = "SELL_TOBACCO_STORE_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "SELL_TOBACCO_STORE_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "SELL_TOBACCO_STORE_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() {
                                        RadioButtonValue = "SELL_TOBACCO_STORE_TYPE_DUTY_FREE",
                                        RadioButtonText = "สำหรับคลังสินค้าทัณฑ์บนประเภทร้านค้าปลอดอากรตามกฎหมายว่าด้วยศุลกากร"
                                    },
                                    new FormRadioButton() {
                                        RadioButtonValue = "SELL_TOBACCO_STORE_TYPE_VAT",
                                        RadioButtonText = "สำหรับสถานที่ขายยาสูบที่จดทะเบียนภาษีมูลค่าเพิ่ม"
                                    },
                                    new FormRadioButton() {
                                        RadioButtonValue = "SELL_TOBACCO_STORE_TYPE_NO_VAT",
                                        RadioButtonText = "สำหรับสถานที่ขายยาสูบที่มิได้จดทะเบียนภาษีมูลค่าเพิ่ม"
                                    },
                                },
                            },
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                IsOr = false,
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_TOBACCO_TYPE__SELL_TOBACCO_TYPE_RETAIL",
                                        ControlAnswer = "true",
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_TOBACCO_TOBACCO_TYPE__SELL_TOBACCO_TOBACCO_TYPE_CIGARATE",
                                        ControlAnswer = "true",
                                    },
                                },
                            },
                        },
                        new FormControl()
                        {
                            FieldID = "",
                            Control = "SELL_TOBACCO_VAT_ID",
                            Type = ControlType.TextBox,
                            DataKey = "SELL_TOBACCO_VAT_ID",
                            ColFixed = 6,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                IsOr = false,
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_TOBACCO_TYPE__SELL_TOBACCO_TYPE_RETAIL",
                                        ControlAnswer = "true",
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_TOBACCO_TOBACCO_TYPE__SELL_TOBACCO_TOBACCO_TYPE_CIGARATE",
                                        ControlAnswer = "true",
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_TOBACCO_STORE_TYPE",
                                        ControlAnswer = "SELL_TOBACCO_STORE_TYPE_VAT",
                                    },
                                },
                            },
                        },
                    },
                });
                items.Add(new FormSectionRow()
                {
                    Section = "SELL_TOBACCO_INFO",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "",
                            Control = "SELL_TABACCO_CONFIRM",
                            Type = ControlType.CheckBox,
                            ColFixed = 12,
                            DataKey = "SELL_TABACCO_CONFIRM",
                            Rules = new FormValidationRule[] { new FormValidationRule() { FixedMessage = true, Type = ValidationType.Required, ErrorMessage = "SELL_TABACCO_CONFIRM" } },
                            CheckboxName = new string[]
                            {
                                "SELL_TABACCO_CONFIRM__TRUE"
                            },
                            HideLabel = true,
                        },
                    },
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }

        public static void InitFormAppFile()
        {
            var db = MongoFactory.GetSingleFormAppFileCollection();
            List<SingleFormAppFile> items = new List<SingleFormAppFile>();

            if (db.AsQueryable().Where(x => x.AppSysName == AppSystemNameTextConst.APP_SELL_TOBACCO).Count() == 0)
            {
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SELL_TOBACCO,
                    Files = {

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",

                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",

                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",

                        "INFORMATION_STORE_MAP",

                        "VAT_REGISTRATION_CARD",

                    }
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }

        public static void InitFormFileList()
        {
            var db = MongoFactory.GetSingleFormFileListCollection();
            List<SingleFormFileList> items = new List<SingleFormFileList>();

            if (db.AsQueryable().Where(x => x.FileName == "SELL_TOBACCO_STORE_MAP").Count() == 0)
            {
                items.Add(new SingleFormFileList()
                {
                    FileName = "SELL_TOBACCO_STORE_MAP",
                    FileGroup = "INFORMATION_STORE_FILE_SECTION",
                });
            }

            if (db.AsQueryable().Where(x => x.FileName == "SELL_TOBACCO_STORE_LAYOUT").Count() == 0)
            {
                items.Add(new SingleFormFileList()
                {
                    FileName = "SELL_TOBACCO_STORE_LAYOUT",
                    FileGroup = "INFORMATION_STORE_FILE_SECTION",
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }

    }
}
