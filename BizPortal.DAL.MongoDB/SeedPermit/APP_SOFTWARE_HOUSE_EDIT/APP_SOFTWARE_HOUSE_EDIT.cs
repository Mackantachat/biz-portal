using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_SOFTWARE_HOUSE_EDIT
{
    public class AppContext
    {
        #region [ Const ]
        public static string StrSectionGroup
        {
            get
            {
                return "APP_SOFTWARE_HOUSE_EDIT_HEADER";
            }
        }

        public static string StrResources
        {
            get
            {
                return "PermitResource.APP_SOFTWARE_HOUSE_EDIT";
            }
        }
        #endregion

        #region [Section]
        public static string SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT
        {
            get
            {
                return "SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT";
            }
        }

        public static string SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EDIT
        {
            get
            {
                return "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EDIT";
            }
        }

        public static string SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT
        {
            get
            {
                return "SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT";
            }
        }
        #endregion

    }

    public class SectionGroup
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
                        AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT,
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
            SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EDIT();
            SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT();
        }

        private static void SOFTWARE_HOUSE_BUSINESS_DETAIL()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT,
                        },
                        ResourceName = AppContext.StrResources,
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            Control.SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT_CONTROL();

        }

        private static void SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EDIT()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EDIT).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EDIT,
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
                            AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT
                        },
                        HideSectionHeader = true,
                        EmptyDataMessage = "ไม่มีข้อมูล ชื่อโมดูล ในขณะนี้ คุณสามารถเพิ่มข้อมูลชื่อโมดูล <br/>โดยคลิกที่ปุ่ม \"เพิ่มชื่อโมดูล\"",
                        AddButtonText = "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_SUMITMSG_EDIT",
                        SubmitButtonText = "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_ADDMSG_EDIT",
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

            Control.SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EDIT_CONTROL();

        }

        private static void SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT,
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

            Control.SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT_CONTROL();

        }

    }

    public static class Control
    {
        public static void SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT_CONTROL()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "LATEST_SOFTWARE_ID_NUMBER_EDIT",
                            Type = ControlType.TextBox,
                            DataKey = "LATEST_SOFTWARE_ID_NUMBER_EDIT",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0000"
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "STORE_SIZE_EDIT",
                            Type = ControlType.Dropdown,
                            DataKey = "STORE_SIZE_EDIT",
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
                            Control = "NUMBER_OF_BRANCH_EDIT",
                            Type = ControlType.TextBox,
                            DataKey = "NUMBER_OF_BRANCH_EDIT",
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
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "STORE_SIZE_OTHER_EDIT",
                            Type = ControlType.TextBox,
                            DataKey = "STORE_SIZE_OTHER_EDIT",
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
                                        ControlName = "STORE_SIZE_EDIT",
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
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "VAT_LICENSE_LIST_AT_EDIT",
                            Type = ControlType.RadioGroup,
                            DataKey = "VAT_LICENSE_LIST_AT_EDIT",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "VAT_LICENSE_LIST_AT_EDIT_OPTION",
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
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "VAT_LICENSE_LIST_AT_BRANCH_NO_EDIT",
                            Type = ControlType.TextBox,
                            DataKey = "VAT_LICENSE_LIST_AT_BRANCH_NO_EDIT",
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
                            Control = "BRANCH_ADDRESS_EDIT",
                            Type = ControlType.AddressForm,
                            DataKey = "BRANCH_ADDRESS_EDIT",
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
                                    ControlName = "VAT_LICENSE_LIST_AT_EDIT",
                                    ControlAnswer = "branch"
                                }
                            }
                    },
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_PRODUCT_OR_SERVICE_TYPE_EDIT",
                            Type = ControlType.CheckBox,
                            DataKey = "SOFTWARE_PRODUCT_OR_SERVICE_TYPE_EDIT",
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            CheckboxName = new string[]
                            {
                                "SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_1_EDIT",
                                "SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_2_EDIT",
                                "SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_3_EDIT",
                                "SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_4_EDIT",
                            },
                            ResourceName = AppContext.StrResources
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_PRODUCT_OR_SERVICE_OPT_4_ANS_EDIT",
                            Type = ControlType.TextBox,
                            DataKey = "SOFTWARE_PRODUCT_OR_SERVICE_OPT_4_ANS_EDIT",
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
                                        ControlName = "SOFTWARE_PRODUCT_OR_SERVICE_TYPE_EDIT__SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_4_EDIT",
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
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_NAME_EDIT",
                            Type = ControlType.TextBox,
                            DataKey = "SOFTWARE_NAME_EDIT",
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
                            Control = "SOFTWARE_VERSION_EDIT",
                            Type = ControlType.TextBox,
                            DataKey = "SOFTWARE_VERSION_EDIT",
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
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_TOTAL_MODULE_EDIT",
                            Type = ControlType.TextBox,
                            DataKey = "SOFTWARE_TOTAL_MODULE_EDIT",
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

        public static void SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EDIT_CONTROL()
        {

            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EDIT).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_MODULE_NAME_EDIT",
                            Type = ControlType.TextBox,
                            DataKey = "SOFTWARE_MODULE_NAME_EDIT",
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

        public static void SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT_CONTROL()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_TYPE_EDIT",
                            Type = ControlType.RadioGroup,
                            DataKey = "SOFTWARE_TYPE_EDIT",
                            Info = "SOFTWARE_TYPE_INFO_EDIT",
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
                                RadioGroupName = "SOFTWARE_TYPE_EDIT_OPTION",
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
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_HOUSE_PROP_AND_INFO_EDIT",
                            Type = ControlType.RadioGroup,
                            DataKey = "SOFTWARE_HOUSE_PROP_AND_INFO_EDIT",
                            IdentityTypes = new UserTypeEnum[]
                            {
                                UserTypeEnum.Citizen,
                                UserTypeEnum.Juristic
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "SOFTWARE_HOUSE_PROP_AND_INFO_EDIT_OPT",
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
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_HOUSE_REQ_DATE_EDIT",
                            Type = ControlType.DatePicker,
                            DataKey = "SOFTWARE_HOUSE_REQ_DATE_EDIT",
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
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "SOFTWARE_HOUSE_BUSINESS_CONFIRMATION_EDIT",
                            Type = ControlType.CheckBox,
                            DataKey = "SOFTWARE_HOUSE_BUSINESS_CONFIRMATION_EDIT",
                            Info = "SOFTWARE_HOUSE_BUSINESS_CONFIRMATION_INFO_EDIT",
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
                                "SOFTWARE_HOUSE_BUSINESS_CONFIRMATION__TRUE_EDIT"
                            },
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT,
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
