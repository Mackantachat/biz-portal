using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_SEC_NEW_G
{
    public static class AppContext
    {

        #region [ Const ]
        public static string StrSectionGroup
        {
            get
            {
                return "APP_SEC_NEW_G_SECTION_GROUP";
            }
        }

        public static string StrResources
        {
            get
            {
                return "PermitResource.APP_SEC_NEW_G";
            }
        }

        public static string[] Rad1
        {
            get
            {
                return new string[] { "1", "1. บริษัทหลักทรัพย์ที่ได้รับใบอนุญาตประกอบธุรกิจหลักทรัพย์แบบ ก หรือผู้อยู่ระหว่างการขอรับใบอนุญาตดังกล่าว" };
            }
        }

        public static string[] Rad2
        {
            get
            {
                return new string[] { "2", "2. บริษัทหลักทรัพย์นอกเหนือจากบริษัทหลักทรัพย์ตาม 1 ที่ได้รับใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทการเป็นนายหน้าซื้อขายหลักทรัพย์ ซึ่งมิได้จำกัดเฉพาะหลักทรัพย์อันเป็นตราสารแห่งหนี้หรือหน่วยลงทุน" };
            }
        }

        public static string[] Rad3
        {
            get
            {
                return new string[] { "3", "3. ผู้ได้รับใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าประเภทการเป็นตัวแทนซื้อขายสัญญาซื้อขายล่วงหน้า ซึ่งมีธนาคารพาณิชย์หรือบริษัทหลักทรัพย์ตาม 1 หรือ 2 รายใดรายหนึ่งเป็นผู้ถือหุ้นเกินกว่าร้อยละห้าสิบของจำนวนหุ้นที่มีสิทธิออกเสียงทั้งหมดของบริษัท" };
            }
        }

        public static string[] Rad4
        {
            get
            {
                return new string[] { "4", "4. บริษัทที่จัดตั้งขึ้นใหม่เพื่อขอรับใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1 ซึ่งมีธนาคารพาณิชย์หรือบริษัทหลักทรัพย์ตาม 1 หรือ 2 รายใดรายหนึ่งเป็นผู้ถือหุ้นเกินกว่าร้อยละห้าสิบของจำนวนฅหุ้นที่มีสิทธิออกเสียงทั้งหมดของบริษัท" };
            }
        }

        public static string[] Rad5
        {
            get
            {
                return new string[] { "5", "5. บริษัทหลักทรัพย์นอกเหนือจากบริษัทหลักทรัพย์ตาม 1 และ 2 แต่ไม่รวมถึงสถาบันการเงินที่จัดตั้งขึ้นตามกฎหมายอื่นซึ่งได้รับใบอนุญาตประกอบธุรกิจหลักทรัพย์ภายหลังจากมีสถานะเป็นสถาบันการเงินแล้ว" };
            }
        }

        public static string[] Rad6
        {
            get
            {
                return new string[] { "6", "6. ผู้ได้รับใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าประเภทการเป็นตัวแทนซื้อขายสัญญาซื้อขายล่วงหน้าที่จำกัดเฉพาะสัญญาซื้อขายล่วงหน้าที่เกี่ยวกับโลหะมีค่า" };
            }
        }

        public static string[] Rad7
        {
            get
            {
                return new string[] { "7", "7. ธนาคารพาณิชย์" };
            }
        }

        public static string[] Rad8
        {
            get
            {
                return new string[] { "8", "8. สถาบันการเงินอื่นตามที่คณะกรรมการ ก.ล.ต. ประกาศกำหนด" };
            }
        }

        public static string[] Rad9
        {
            get
            {
                return new string[] { "9", "9. บริษัทนอกเหนือจากบริษัทตาม 4 ที่จัดตั้งขึ้นใหม่เพื่อขอรับใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1" };
            }
        }

        public static string[] Rad10
        {
            get
            {
                return new string[] { "10", "มีใบอนุญาตที่ต้องคืน" };
            }
        }

        public static string[] Rad11
        {
            get
            {
                return new string[] { "11", "ไม่มีใบอนุญาตที่ต้องคืน" };
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
        public static string APP_SEC_NEW_G_SECTION_REQUEST_INFO
        {
            get
            {
                return "APP_SEC_NEW_G_SECTION_REQUEST_INFO";
            }
        }

        public static string APP_SEC_NEW_G_PAY_BACK_SECTION
        {
            get
            {
                return "APP_SEC_NEW_G_PAY_BACK_SECTION";
            }
        }

        public static string APP_SEC_NEW_G_OFFICER_SECTION
        {
            get
            {
                return "APP_SEC_NEW_G_OFFICER_SECTION";
            }
        }
        #endregion

        #region [ Condition ]
        public static FormControlDisplayCondition APP_SEC_NEW_G_SECTION_REQUEST_INFO_OTHER
        {
            get
            {
                FormControlDisplayCondition APP_SEC_NEW_G_SECTION_REQUEST_INFO_OTHER = new FormControlDisplayCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_SEC_NEW_G_SECTION_REQUEST_INFO_CONTROL",
                            ControlAnswer = Rad8[0]
                        },
                    },

                };
                return APP_SEC_NEW_G_SECTION_REQUEST_INFO_OTHER;
            }
        }

        public static FormControlDisplayCondition SHOW_APP_SEC_NEW_G_PAY_BACK_SECTION
        {
            get
            {
                FormControlDisplayCondition SHOW_APP_SEC_NEW_G_PAY_BACK_SECTION = new FormControlDisplayCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_SEC_NEW_G_SECTION_REQUEST_STOCK_HOLDER",
                            ControlAnswer = Rad10[0]
                        },
                    },

                };
                return SHOW_APP_SEC_NEW_G_PAY_BACK_SECTION;
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
                        AppSystemNameTextConst.APP_SEC_NEW_G,
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
            APP_SEC_NEW_G_SECTION_REQUEST_INFO();
            APP_SEC_NEW_G_PAY_BACK_SECTION();
            APP_SEC_NEW_G_OFFICER_SECTION();
        }

        private static void APP_SEC_NEW_G_SECTION_REQUEST_INFO()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_NEW_G_SECTION_REQUEST_INFO).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.APP_SEC_NEW_G_SECTION_REQUEST_INFO,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SEC_NEW_G,
                        },
                        ResourceName = AppContext.StrResources,
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            Control.APP_SEC_NEW_G_SECTION_REQUEST_INFO_CONTROL();

        }

        private static void APP_SEC_NEW_G_PAY_BACK_SECTION()
        {

            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_NEW_G_PAY_BACK_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.APP_SEC_NEW_G_PAY_BACK_SECTION,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SEC_NEW_G,
                        },
                        DisplayCondition = AppContext.SHOW_APP_SEC_NEW_G_PAY_BACK_SECTION,
                        DefaultShowInfo = true,
                        Info = "APP_SEC_NEW_G_PAY_BACK_SECTION_INFO"
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
            Control.APP_SEC_NEW_G_PAY_BACK_SECTION_CONTROL();
        }

        private static void APP_SEC_NEW_G_OFFICER_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_NEW_G_OFFICER_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.APP_SEC_NEW_G_OFFICER_SECTION,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SEC_NEW_G,
                        },
                        ResourceName = AppContext.StrResources,
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            Control.APP_SEC_NEW_G_OFFICER_SECTION_CONTROL();

        }

    }

    public static class Control
    {

        public static void APP_SEC_NEW_G_SECTION_REQUEST_INFO_CONTROL()
        {

            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_NEW_G_SECTION_REQUEST_INFO).Count() == 0)
            {
                #region [ สถานผู้ขอรับใบอนุญาต ]

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_G_SECTION_REQUEST_INFO,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_SECTION_REQUEST_INFO_CONTROL",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_SEC_NEW_G_SECTION_REQUEST_INFO_CONTROL",
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_SEC_NEW_G_SECTION_REQUEST_INFO_CONTROL_RAD",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad1[0],  RadioButtonText = AppContext.Rad1[1] },
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad2[0] , RadioButtonText = AppContext.Rad2[1] },
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad3[0] , RadioButtonText = AppContext.Rad3[1] },
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad4[0] , RadioButtonText = AppContext.Rad4[1] },
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad5[0] , RadioButtonText = AppContext.Rad5[1] },
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad6[0] , RadioButtonText = AppContext.Rad6[1] },
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad7[0] , RadioButtonText = AppContext.Rad7[1] },
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad8[0] , RadioButtonText = AppContext.Rad8[1] },
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad9[0] , RadioButtonText = AppContext.Rad9[1] },
                                }
                            },
                            ResourceName = AppContext.StrResources,
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_G_SECTION_REQUEST_INFO,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_SECTION_REQUEST_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_G_SECTION_REQUEST_OTHER",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DisplayCondition = AppContext.APP_SEC_NEW_G_SECTION_REQUEST_INFO_OTHER,
                            ResourceName = AppContext.StrResources
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_G_SECTION_REQUEST_INFO,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_SECTION_REQUEST_STOCK_HOLDER",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_SEC_NEW_G_SECTION_REQUEST_STOCK_HOLDER",
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_SEC_NEW_G_SECTION_REQUEST_STOCK_HOLDER_RAD",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad10[0],  RadioButtonText = AppContext.Rad10[1] },
                                    new FormRadioButton() { RadioButtonValue = AppContext.Rad11[0] , RadioButtonText = AppContext.Rad11[1] },
                                }
                            },
                            ResourceName = AppContext.StrResources,
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

        public static void APP_SEC_NEW_G_PAY_BACK_SECTION_CONTROL()
        {

            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_NEW_G_PAY_BACK_SECTION).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_G_PAY_BACK_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_CURRENT_SECTION_REQUEST",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_SEC_NEW_G_CURRENT_SECTION_REQUEST",
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayCheckboxInline = true,
                            CheckboxName = new string[]
                            {
                                "CHK_1",
                                "CHK_2",
                                "CHK_3",
                                "CHK_4",
                                "CHK_5",
                                "CHK_6",
                                "CHK_7",
                                "CHK_8",
                                "CHK_9",
                            },
                            ResourceName = AppContext.StrResources
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_G_PAY_BACK_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_PAY_BACK_SECTION_REQUEST",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_SEC_NEW_G_PAY_BACK_SECTION_REQUEST",
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayCheckboxInline = true,
                            CheckboxName = new string[]
                            {
                                "CHK_1",
                                "CHK_2",
                                "CHK_3",
                                "CHK_4",
                                "CHK_5",
                                "CHK_6",
                                "CHK_7",
                                "CHK_8",
                                "CHK_9",
                            },
                            ResourceName = AppContext.StrResources
                        }
                    }
                });

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

        }

        public static void APP_SEC_NEW_G_OFFICER_SECTION_CONTROL()
        {

            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_NEW_G_OFFICER_SECTION).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_G_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_HEADER_1",
                            Type = ControlType.Heading,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_HEADER_1",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 12,
                            ResourceName = AppContext.StrResources,
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_G_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_TITLE_1",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_TITLE_1",
                            ShowOnSpecificApps =true,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 4,
                            Select2Opts = AppContext.optPersonTitle,
                            ResourceName = AppContext.StrResources,
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_FIRST_NAME_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_FIRST_NAME_1",
                            ShowOnSpecificApps =true,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 4,
                            ResourceName = AppContext.StrResources,
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_LAST_NAME_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_LAST_NAME_1",
                            ShowOnSpecificApps =true,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 4,
                            ResourceName = AppContext.StrResources,
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_G_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_POSITION_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_POSITION_1",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_TELL_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_TELL_1",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
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
                    Section = AppContext.APP_SEC_NEW_G_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_FAX_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_FAX_1",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_EMAIL_1",
                            Type = ControlType.Email,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_EMAIL_1",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_G_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_HEADER_2",
                            Type = ControlType.Heading,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_HEADER_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 12,
                            ResourceName = AppContext.StrResources,
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_G_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_TITLE_2",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_TITLE_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 4,
                            Select2Opts = AppContext.optPersonTitle,
                            ResourceName = AppContext.StrResources,
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_FIRST_NAME_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_FIRST_NAME_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 4,
                            ResourceName = AppContext.StrResources,
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_LAST_NAME_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_LAST_NAME_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 4,
                            ResourceName = AppContext.StrResources,
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_G_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_POSITION_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_POSITION_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_TELL_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_TELL_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
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
                    Section = AppContext.APP_SEC_NEW_G_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_FAX_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_FAX_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        },

                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_EMAIL_2",
                            Type = ControlType.Email,
                            DataKey = "APP_SEC_NEW_G_OFFICER_SECTION_CONTACT_EMAIL_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            ColFixed = 6,
                            ResourceName = AppContext.StrResources,
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_G_OFFICER_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_NEW_G_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_SEC_NEW_G_CONFIRM",
                            ShowOnSpecificApps =true,
                            Info = "APP_SEC_NEW_G_CONFIRM_INFO",
                            DefaultShowInfo = true,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_SEC_NEW_G,
                            },
                            CheckboxName = new string[]
                            {
                                "APP_SEC_NEW_G_CONFIRM_CHECK_BOX",
                            },
                            ColFixed = 12,
                            ResourceName = AppContext.StrResources,
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_NEW_G_OFFICER_SECTION,
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
