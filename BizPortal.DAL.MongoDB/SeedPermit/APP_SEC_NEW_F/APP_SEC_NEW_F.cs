using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_SEC_NEW_F
{
    public static class AppContext
    {
        #region [ Const ]
        public static string StrSectionGroup
        {
            get
            {
                return "APP_SEC_NEW_F_SECTION_GROUP";
            }
        }

        public static string StrResources
        {
            get
            {
                return "PermitResource.APP_SEC_NEW_F";
            }
        }

        public static string[] Rad1
        {
            get
            {
                return new string[] { "1", "1. บริษัทหลักทรัพย์ที่ได้รับใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทการเป็นนายหน้าซื้อขายหลักทรัพย์ แต่ไม่รวมถึงบริษัทหลักทรัพย์ที่ได้รับใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทการเป็นนายหน้าซื้อขายหลักทรัพย์อันเป็นตราสารแห่งหนี้ หรือการเป็นนายหน้าซื้อขายหลักทรัพย์ที่เป็นหน่วยลงทุน" };
            }
        }

        public static string[] Rad2
        {
            get
            {
                return new string[] { "2", "2. ธนาคารพาณิชย์" };
            }
        }

        public static string[] Rad3
        {
            get
            {
                return new string[] { "3", "3. สถาบันการเงินอื่นตามที่คณะกรรมการ ก.ล.ต. ประกาศกำหนด" };
            }
        }

        public static string[] Rad4
        {
            get
            {
                return new string[] { "4", "4. บริษัทที่จัดตั้งขึ้นใหม่เพื่อขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทกิจการการยืมและให้ยืมหลักทรัพย์" };
            }
        }

        public static Select2Opt[] optPersonTitle
        {
            get
            {
                return new Select2Opt[]
                {
                    new Select2Opt() { ID = "01", Text = "นาย" },
                    new Select2Opt() { ID = "02", Text = "นาง" },
                    new Select2Opt() { ID = "03", Text = "นางสาว" }
                };
            }
        }
        #endregion

        #region [ Section ]
        public static string APP_SEC_NEW_F_SECTION_REQUEST_INFO
        {
            get
            {
                return "APP_SEC_NEW_F_SECTION_REQUEST_INFO";
            }
        }

        public static string APP_SEC_NEW_F_OFFICER_SECTION
        {
            get
            {
                return "APP_SEC_NEW_F_OFFICER_SECTION";
            }
        }
        #endregion

        #region [ Condition ]
        public static FormControlDisplayCondition APP_SEC_NEW_F_SECTION_REQUEST_INFO_OTHER
        {
            get
            {
                FormControlDisplayCondition APP_SEC_NEW_F_SECTION_REQUEST_INFO_OTHER = new FormControlDisplayCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_SEC_NEW_F_SECTION_REQUEST_INFO_CONTROL",
                            ControlAnswer = Rad3[0]
                        },
                    },

                };
                return APP_SEC_NEW_F_SECTION_REQUEST_INFO_OTHER;
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
                {
                    items.Add(new FormSectionGroup()
                    {
                        SectionGroup = AppContext.StrSectionGroup,
                        Ordering = 4,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                        AppSystemNameTextConst.APP_SEC_NEW_F,
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

    }

    public static class Section
    {
        public static void CreateSection()
        {
            APP_SEC_NEW_F_SECTION_REQUEST_INFO();
            APP_SEC_NEW_F_OFFICER_SECTION();
        }

        private static void APP_SEC_NEW_F_SECTION_REQUEST_INFO()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_NEW_F_SECTION_REQUEST_INFO).Count() == 0)
            {
                
                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.APP_SEC_NEW_F_SECTION_REQUEST_INFO,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SEC_NEW_F,
                        },
                        ResourceName = AppContext.StrResources,
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            Control.APP_SEC_NEW_F_SECTION_REQUEST_INFO_CONTROL();

        }

        private static void APP_SEC_NEW_F_OFFICER_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_NEW_F_OFFICER_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.APP_SEC_NEW_F_OFFICER_SECTION,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SEC_NEW_F,
                        },
                        ResourceName = AppContext.StrResources,
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            Control.APP_SEC_NEW_F_OFFICER_SECTION_CONTROL();
        }

    }

    public static class Control
    {
        public static void APP_SEC_NEW_F_SECTION_REQUEST_INFO_CONTROL()
        {

            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_NEW_F_SECTION_REQUEST_INFO).Count() == 0)
            {
                #region [ สถานผู้ขอรับใบอนุญาต ]

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_F_SECTION_REQUEST_INFO,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_SECTION_REQUEST_INFO_CONTROL",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_SEC_NEW_F_SECTION_REQUEST_INFO_CONTROL",
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_SEC_NEW_F_SECTION_REQUEST_INFO_CONTROL_RAD",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad1[0],  RadioButtonText = AppContext.Rad1[1] },
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad2[0] , RadioButtonText = AppContext.Rad2[1] },
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad3[0] , RadioButtonText = AppContext.Rad3[1] },
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad4[0] , RadioButtonText = AppContext.Rad4[1] },
                                }
                            },
                            ResourceName = AppContext.StrResources,
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_F_SECTION_REQUEST_INFO,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_SECTION_REQUEST_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_F_SECTION_REQUEST_OTHER",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = AppContext.APP_SEC_NEW_F_SECTION_REQUEST_INFO_OTHER,
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

                #endregion

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

        }

        public static void APP_SEC_NEW_F_OFFICER_SECTION_CONTROL()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_NEW_F_OFFICER_SECTION).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_F_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_HEADER_1",
                            Type = ControlType.Heading,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_HEADER_1",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 12,
                            ResourceName = AppContext.StrResources,
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_F_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_TITLE_1",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_TITLE_1",
                            ShowOnSpecificApps =true,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 4,
                            Select2Opts = AppContext.optPersonTitle,
                            ResourceName = AppContext.StrResources,
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_FIRST_NAME_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_FIRST_NAME_1",
                            ShowOnSpecificApps =true,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 4,
                            ResourceName = AppContext.StrResources,
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_LAST_NAME_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_LAST_NAME_1",
                            ShowOnSpecificApps =true,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 4,
                            ResourceName = AppContext.StrResources,
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_F_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_POSITION_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_POSITION_1",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_TELL_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_TELL_1",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 6,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = AppContext.StrResources,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_F_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_FAX_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_FAX_1",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_EMAIL_1",
                            Type = ControlType.Email,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_EMAIL_1",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_F_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_HEADER_2",
                            Type = ControlType.Heading,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_HEADER_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 12,
                            ResourceName = AppContext.StrResources,
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_F_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_TITLE_2",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_TITLE_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 4,
                            Select2Opts = AppContext.optPersonTitle,
                            ResourceName = AppContext.StrResources,
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_FIRST_NAME_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_FIRST_NAME_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 4,
                            ResourceName = AppContext.StrResources,
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_LAST_NAME_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_LAST_NAME_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 4,
                            ResourceName = AppContext.StrResources,
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_F_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_POSITION_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_POSITION_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_TELL_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_TELL_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_F_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_FAX_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_FAX_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_EMAIL_2",
                            Type = ControlType.Email,
                            DataKey = "APP_SEC_NEW_F_OFFICER_SECTION_CONTACT_EMAIL_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_F_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_F_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_SEC_NEW_F_CONFIRM",
                            ShowOnSpecificApps =true,
                            Info = "APP_SEC_NEW_F_CONFIRM_INFO",
                            DefaultShowInfo = true,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_F,
                            },
                            CheckboxName = new string[]
                            {
                                "APP_SEC_NEW_F_CONFIRM_CHECK_BOX",
                            },
                            ColFixed = 12,
                            ResourceName = AppContext.StrResources,
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_F_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_A_CONFIRM",
                            Type = ControlType.Confirm_SEC_NEW_A,
                            DataKey = "APP_SEC_NEW_A_CONFIRM",
                            DisplayReadonly = true,
                            HideLabel = true,
                            ColFixed = 12,
                            ResourceName = AppContext.StrResources,
                        },

                    }
                });

                if (items.Count > 0)
                {
                    db.InsertMany(items.ToArray());
                }
            }
        }
    }

}
