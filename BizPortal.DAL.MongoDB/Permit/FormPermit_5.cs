using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using BizPortal.Utils.Helpers;

namespace BizPortal.DAL.MongoDB.Permit
{
    public class FormPermit_5
    {

        public static void InitFormSectionGroup()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == "APP_SELL_ALCOHOL").Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = "APP_SELL_ALCOHOL",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_ALCOHOL,
                    },
                    Ordering = 501,
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

            if (db.AsQueryable().Where(x => x.Section == "SELL_ALCOHOL_INFO").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "SELL_ALCOHOL_INFO",
                    SectionGroup = "APP_SELL_ALCOHOL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_SELL_ALCOHOL,
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

            if (db.AsQueryable().Where(x => x.Section == "SELL_ALCOHOL_INFO").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "SELL_ALCOHOL_INFO",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "",
                            Control = "SELL_ALCOHOL_OBJECTIVE",
                            Type = ControlType.CheckBox,
                            DataKey = "SELL_ALCOHOL_OBJECTIVE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "SELL_ALCOHOL_OBJECTIVE_OPTION_GE10L",
                            },
                            WillTriggerDisplayOfOtherUI = true,
                        },
                        new FormControl()
                        {
                            FieldID = "",
                            Control = "SELL_ALCOHOL_PRODUCTION_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "SELL_ALCOHOL_PRODUCTION_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "SELL_ALCOHOL_PRODUCTION_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "SELL_ALCOHOL_PRODUCTION_TYPE__LOCAL1", RadioButtonText = "โรงอุตสาหกรรมผลิตสุราแช่ชุมชน" },
                                    new FormRadioButton() { RadioButtonValue = "SELL_ALCOHOL_PRODUCTION_TYPE__LOCAL2", RadioButtonText = "โรงอุตสาหกรรมผลิตสุรากลั่นชุมชน" },
                                    new FormRadioButton() { RadioButtonValue = "SELL_ALCOHOL_PRODUCTION_TYPE__FACTORY", RadioButtonText = "โรงอุตสาหกรรมผลิตสุราอื่นๆ ที่มิใช่สุราแช่ชุมชนและสุรากลั่นชุมชน" },
                                    new FormRadioButton() { RadioButtonValue = "SELL_ALCOHOL_PRODUCTION_TYPE__OTHER", RadioButtonText = "สถานประกอบการขายสุราอื่นๆ ที่มิใช่โรงงานอุตสาหกรรมผลิตสุรา" },
                                }
                            },
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_ALCOHOL_OBJECTIVE__SELL_ALCOHOL_OBJECTIVE_OPTION_GE10L",
                                        ControlAnswer = "true",
                                    },
                                },
                            },
                        },
                        new FormControl()
                        {
                            FieldID = "",
                            Control = "SELL_ALCOHOL_OBJECTIVE",
                            Type = ControlType.CheckBox,
                            DataKey = "SELL_ALCOHOL_OBJECTIVE",
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L",
                            },
                            HideLabel = true,
                            WillTriggerDisplayOfOtherUI = true,
                        },
                        new FormControl()
                        {
                            FieldID = "",
                            Control = "SELL_ALCOHOL_SELL_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "SELL_ALCOHOL_SELL_TYPE",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "SELL_ALCOHOL_SELL_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "SELL_ALCOHOL_SELL_TYPE__VAT_FREE", RadioButtonText = "คลังสินค้าทัณฑ์บนประเภทร้านค้าปลอดอากรตามกฎหมายว่าด้วยศุลกากร" },
                                    new FormRadioButton() { RadioButtonValue = "SELL_ALCOHOL_SELL_TYPE__VAT_REGISTERED", RadioButtonText = "สถานประกอบการขายสุรา ซึ่งได้จดทะเบียนภาษีมูลค่าเพิ่ม" },
                                    new FormRadioButton() { RadioButtonValue = "SELL_ALCOHOL_SELL_TYPE__VAT_NOT_REGISTERED", RadioButtonText = "สถานประกอบการขายสุรา ซึ่งมิได้จดทะเบียนภาษีมูลค่าเพิ่ม" },
                                },
                            },
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_ALCOHOL_OBJECTIVE__SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L",
                                        ControlAnswer = "true",
                                    },
                                },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                        },
                    },
                });
                items.Add(new FormSectionRow()
                {
                    Section = "SELL_ALCOHOL_INFO",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "",
                            Control = "SELL_ALCOHOL_VAT_ID",
                            Type = ControlType.TextBox,
                            DataKey = "SELL_ALCOHOL_VAT_ID",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                IsOr = false,
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_ALCOHOL_SELL_TYPE",
                                        ControlAnswer = "SELL_ALCOHOL_SELL_TYPE__VAT_REGISTERED",
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_ALCOHOL_OBJECTIVE__SELL_ALCOHOL_OBJECTIVE_OPTION_LT10L",
                                        ControlAnswer = "true",
                                    },
                                }
                            },
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",
                        },
                        //new FormControl()
                        //{
                        //    Control = "SELL_ALCOHOL_BUSINESS_TYPE",
                        //    Type = ControlType.CheckBox,
                        //    DataKey = "SELL_ALCOHOL_BUSINESS_TYPE",
                        //    Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                        //    ColFixed = 12,
                        //    CheckboxName = new string[]
                        //    {
                        //        "SELL_ALCOHOL_BUSINESS_PRODUCER",
                        //        "SELL_ALCOHOL_BUSINESS_DUTY_FREE",
                        //        "SELL_ALCOHOL_BUSINESS_RESTAURANT",
                        //        "SELL_ALCOHOL_BUSINESS_PUB",
                        //        "SELL_ALCOHOL_BUSINESS_BROTHEL",
                        //        "SELL_ALCOHOL_BUSINESS_DEPARTMENT_STORE",
                        //        "SELL_ALCOHOL_BUSINESS_CONVENIENT_STORE",
                        //        "SELL_ALCOHOL_BUSINESS_GROCERY",
                        //        "SELL_ALCOHOL_BUSINESS_ASSOCIATION",
                        //        "SELL_ALCOHOL_BUSINESS_OTHER",
                        //    },
                        //    WillTriggerDisplayOfOtherUI = true,
                        //},
                        new FormControl()
                        {
                            FieldID = "",
                            Control = "SELL_ALCOHOL_BUSINESS_TYPE_OTHER_INFO",
                            Type = ControlType.TextBox,
                            DataKey = "SELL_ALCOHOL_BUSINESS_TYPE_OTHER_INFO",
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SELL_ALCOHOL_BUSINESS_TYPE__SELL_ALCOHOL_BUSINESS_OTHER",
                                        ControlAnswer = "true",
                                    },
                                },
                            },
                        },
                        new FormControl()
                        {
                            FieldID = "",
                            Control = "SELL_ALCOHOL_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "SELL_ALCOHOL_CONFIRM",
                            Info = "SELL_ALCOHOL_CONFIRM_INFO",
                            Rules = new FormValidationRule[] { new FormValidationRule() { FixedMessage = true, Type = ValidationType.Required, ErrorMessage = "SELL_ALCOHOL_CONFIRM" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "SELL_ALCOHOL_CONFIRM__TRUE"
                            },
                            DefaultShowInfo = true,
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

            if (db.AsQueryable().Where(x => x.AppSysName == AppSystemNameTextConst.APP_SELL_ALCOHOL).Count() == 0)
            {
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SELL_ALCOHOL,
                    Files = {

                        #region Citizen

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                        #endregion

                        #region Juristic

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",

                        #endregion

                        #region Information Store

                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_MAP",

                        #endregion

                        #region Permit

                        "ALCOHOL_STORE",

                        #endregion

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

            if (db.AsQueryable().Where(x => x.FileName == "ALCOHOL_STORE").Count() == 0)
            {
                items.Add(new SingleFormFileList()
                {
                    FileName = "ALCOHOL_STORE",
                    FileGroup = "ALCOHOL_FILE_SECTION",
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }

    }
}
