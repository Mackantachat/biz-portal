using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_SOFTWARE_HOUSE_NEW
{

    public class AppContext
    {

        #region [ Const ]
        public static string StrSectionGroup
        {
            get
            {
                return "APP_SOFTWARE_HOUSE";
            }
        }

        public static string StrResources
        {
            get
            {
                return "PermitResource.APP_SOFTWARE_HOUSE_NEW";
            }
        }
        #endregion

        #region [Section]
        public static string SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW
        {
            get
            {
                return "SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW";
            }
        }

        public static string SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE
        {
            get
            {
                return "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE";
            }
        }

        public static string SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW
        {
            get
            {
                return "SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW";
            }
        }
        #endregion

    }

    public static class SectionGroup
    {
        public static void CreateSecionGroup()
        {

            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == AppContext.StrSectionGroup).Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = AppContext.StrSectionGroup,
                    Ordering = 5,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW,
                    },
                    ResourceName = AppContext.StrResources
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            Section.CreateSection();

        }
    }
    
    public static class Section
    {
        public static void CreateSection()
        {
            SOFTWARE_HOUSE_BUSINESS_DETAIL();
            SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE();
            SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW();
        }

        private static void SOFTWARE_HOUSE_BUSINESS_DETAIL()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW,
                        },
                        ResourceName = AppContext.StrResources,
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            Control.SOFTWARE_HOUSE_BUSINESS_DETAIL_CONTROL();

        }

        private static void SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.ArrayOfForms,
                        IdentityTypes = new UserTypeEnum[]
                        {
                            UserTypeEnum.Juristic,
                            UserTypeEnum.Citizen
                        },
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW
                        },
                        HideSectionHeader = true,
                        EmptyDataMessage = "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EMPTYMSG",
                        AddButtonText = "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_ADDMSG",
                        SubmitButtonText = "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_SUMITMSG",
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1,
                        ResourceName = AppContext.StrResources
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            Control.SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_CONTROL();

        }

        private static void SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW,
                        },
                        HideSectionHeader = true,
                        ResourceName = AppContext.StrResources
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            Control.SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW_CONTROL();

        }
    }

    public static class Control
    {
        public static void SOFTWARE_HOUSE_BUSINESS_DETAIL_CONTROL()
        {

            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_ID_NUMBER_REQUIREMENT",
                            Type = ControlType.RadioGroup,
                            DataKey = "SOFTWARE_ID_NUMBER_REQUIREMENT",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "SOFTWARE_ID_NUMBER_REQUIREMENT_OPT",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() {
                                        RadioButtonValue = "1",
                                        RadioButtonText = "ขอมีเลขประจำตัวซอฟต์แวร์ครั้งแรก" },
                                    new FormRadioButton() {
                                        RadioButtonValue = "2",
                                        RadioButtonText = "ขอเปลี่ยนเวอร์ชั่นของซอฟต์แวร์" },
                                }
                            },
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "LATEST_SOFTWARE_ID_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "LATEST_SOFTWARE_ID_NUMBER",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = new FormControlDisplayCondition()
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer()
                                    {
                                        ControlName = "SOFTWARE_ID_NUMBER_REQUIREMENT",
                                        ControlAnswer = "2"
                                    }
                                }
                            },
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "STORE_SIZE",
                            Type = ControlType.Dropdown,
                            DataKey = "STORE_SIZE",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "01", Text = "เล็ก" },
                                new Select2Opt() { ID = "02", Text = "เล็ก-กลาง" },
                                new Select2Opt() { ID = "03", Text = "กลาง" },
                                new Select2Opt() { ID = "04", Text = "กลาง-ใหญ่" },
                                new Select2Opt() { ID = "05", Text = "ใหญ่" },
                                new Select2Opt() { ID = "06", Text = "อื่นๆ" },
                            },
                            WillTriggerDisplayOfOtherUI = true,
                            ResourceName = AppContext.StrResources
                        },
                        new FormControl()
                        {
                            Control = "NUMBER_OF_BRANCH",
                            Type = ControlType.TextBox,
                            DataKey = "NUMBER_OF_BRANCH",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            AdvancedTextboxType = AdvancedTextboxType.Numeric,
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "STORE_SIZE_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "STORE_SIZE_OTHER",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = new FormControlDisplayCondition()
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer()
                                    {
                                        ControlName = "STORE_SIZE",
                                        ControlAnswer = "06"
                                    }
                                }
                            },
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "VAT_LICENSE_LIST_AT",
                            Type = ControlType.RadioGroup,
                            DataKey = "VAT_LICENSE_LIST_AT",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "VAT_LICENSE_LIST_AT_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() {
                                        RadioButtonValue = "main",
                                        RadioButtonText = "VAT_LICENSE_LIST_AT_MAIN" },
                                    new FormRadioButton() {
                                        RadioButtonValue = "branch",
                                        RadioButtonText = "VAT_LICENSE_LIST_AT_BRANCH" },
                                }
                            },
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "VAT_LICENSE_LIST_AT_BRANCH_NO",
                            Type = ControlType.TextBox,
                            DataKey = "VAT_LICENSE_LIST_AT_BRANCH_NO",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources
                        },
                        new FormControl()
                        {
                            Control = "BRANCH_ADDRESS",
                            Type = ControlType.AddressForm,
                            DataKey = "BRANCH_ADDRESS",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            AddressForm_ShowBuildingControl = true,
                            AddressForm_ShowVillageControl = false,
                            AddressForm_ShowPostCodeControl = true,
                            AddressForm_ShowTelephoneControl = true,
                            AddressForm_ShowFaxControl = true,
                            ResourceName = AppContext.StrResources
                        },
                    },
                    DisplayCondition = new FormControlDisplayCondition
                    {
                        Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                            {
                                new FormControlDisplayCondition.ControlWithAnswer
                                {
                                    ControlName = "VAT_LICENSE_LIST_AT",
                                    ControlAnswer = "branch"
                                }
                            }
                    },
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_PRODUCT_OR_SERVICE_TYPE",
                            Type = ControlType.CheckBox,
                            DataKey = "SOFTWARE_PRODUCT_OR_SERVICE_TYPE",
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            CheckboxName = new string[]
                            {
                                "SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_1",
                                "SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_2",
                                "SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_3",
                                "SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_4",
                            },
                            ResourceName = AppContext.StrResources
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_PRODUCT_OR_SERVICE_OPT_4_ANS",
                            Type = ControlType.TextBox,
                            DataKey = "SOFTWARE_PRODUCT_OR_SERVICE_OPT_4_ANS",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "SOFTWARE_PRODUCT_OR_SERVICE_TYPE__SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_4",
                                        ControlAnswer = "true",
                                    }
                                }
                            },
                            ResourceName = AppContext.StrResources
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "SOFTWARE_NAME",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources
                        },
                        new FormControl()
                        {
                            Control = "SOFTWARE_VERSION",
                            Type = ControlType.TextBox,
                            DataKey = "SOFTWARE_VERSION",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.MaxLength, ErrorMessage = "MaxLength", MaxLength = 10 }
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_TOTAL_MODULE",
                            Type = ControlType.TextBox,
                            DataKey = "SOFTWARE_TOTAL_MODULE",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            //Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayReadonly = true,
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

        }

        public static void SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_CONTROL()
        {

            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_MODULE_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "SOFTWARE_MODULE_NAME",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

        }

        public static void SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW_CONTROL()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_TYPE_NEW",
                            Type = ControlType.RadioGroup,
                            DataKey = "SOFTWARE_TYPE_NEW",
                            Info = "SOFTWARE_TYPE_INFO",
                            DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "SOFTWARE_TYPE_NEW_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() {
                                        RadioButtonValue = "one",
                                        RadioButtonText = "SOFTWARE_TYPE_ONE"
                                    },
                                    new FormRadioButton() {
                                        RadioButtonValue = "two",
                                        RadioButtonText = "SOFTWARE_TYPE_TWO"
                                    },
                                    new FormRadioButton() {
                                        RadioButtonValue = "three",
                                        RadioButtonText = "SOFTWARE_TYPE_THREE"
                                    },
                                    new FormRadioButton() {
                                        RadioButtonValue = "four",
                                        RadioButtonText = "SOFTWARE_TYPE_FOUR"
                                    },
                                }
                            },
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_HOUSE_PROP_AND_INFO_NEW",
                            Type = ControlType.RadioGroup,
                            DataKey = "SOFTWARE_HOUSE_PROP_AND_INFO_NEW",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "SOFTWARE_HOUSE_PROP_AND_INFO_NEW_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() {
                                        RadioButtonValue = "one",
                                        RadioButtonText = "SOFTWARE_HOUSE_PROP_AND_INFO_ONE"
                                    },
                                    new FormRadioButton() {
                                        RadioButtonValue = "two",
                                        RadioButtonText = "SOFTWARE_HOUSE_PROP_AND_INFO_TWO"
                                    },
                                    new FormRadioButton() {
                                        RadioButtonValue = "three",
                                        RadioButtonText = "SOFTWARE_HOUSE_PROP_AND_INFO_THREE"
                                    },
                                    new FormRadioButton() {
                                        RadioButtonValue = "four",
                                        RadioButtonText = "SOFTWARE_HOUSE_PROP_AND_INFO_FOUR"
                                    },
                                }
                            },
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_HOUSE_REQ_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "SOFTWARE_HOUSE_REQ_DATE",
                            DataFormat = "dd/MM/yyyy",
                            PreFill = true,
                            DisplayStaticIfHasData = true,
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_HOUSE_BUSINESS_CONFIRMATION_NEW",
                            Type = ControlType.CheckBox,
                            DataKey = "SOFTWARE_HOUSE_BUSINESS_CONFIRMATION_NEW",
                            Info = "SOFTWARE_HOUSE_BUSINESS_CONFIRMATION_INFO_NEW",
                            DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "SOFTWARE_HOUSE_BUSINESS_CONFIRMATION__TRUE_NEW"
                            },
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()                        
                        {
                            Control = "SOFTWARE_HOUSE_BUSINESS_SIGNATURE",
                            Type = ControlType.ConfirmSignatureEDIT,
                            DataKey = "SOFTWARE_HOUSE_BUSINESS_SIGNATURE",
                            DisplayReadonly = true,
                            HideLabel = true,
                            ColFixed = 12,
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
