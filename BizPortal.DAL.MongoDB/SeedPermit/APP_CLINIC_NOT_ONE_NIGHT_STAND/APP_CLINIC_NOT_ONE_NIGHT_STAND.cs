using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_NOT_ONE_NIGHT_STAND
{
    public class AppContext
    {
        #region [ Const ]
        public static string StrSectionGroup
        {
            get
            {
                return "APP_CLINIC_NOT_ONE_NIGHT_STAND_GROUP";
            }
        }

        public static string StrResources
        {
            get
            {
                return "PermitResource.APP_CLINIC_NOT_ONE_NIGHT_STAND";
            }
        }
        #endregion

        #region [ Section ]
        public static string APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_A
        {
            get
            {
                return "APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_A";
            }
        }

        public static string APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_B
        {
            get
            {
                return "APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_B";
            }
        }
        #endregion

        #region Extension

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

        public static Select2Opt[] Year
        {
            get
            {
                return new Select2Opt[]
                {
                    new Select2Opt(){ ID = "2563" ,Text = "2563"},
                    new Select2Opt(){ ID = "2564" ,Text = "2564"},
                    new Select2Opt(){ ID = "2565" ,Text = "2565"},
                    new Select2Opt(){ ID = "2566" ,Text = "2566"},
                    new Select2Opt(){ ID = "2567" ,Text = "2567"},
                    new Select2Opt(){ ID = "2568" ,Text = "2568"},
                    new Select2Opt(){ ID = "2569" ,Text = "2569"},
                    new Select2Opt(){ ID = "2570" ,Text = "2570"},
                    new Select2Opt(){ ID = "2571" ,Text = "2571"},
                    new Select2Opt(){ ID = "2572" ,Text = "2572"},
                    new Select2Opt(){ ID = "2573" ,Text = "2573"},
                    new Select2Opt(){ ID = "2574" ,Text = "2574"},
                    new Select2Opt(){ ID = "2575" ,Text = "2575"},
                    new Select2Opt(){ ID = "2576" ,Text = "2576"},
                    new Select2Opt(){ ID = "2577" ,Text = "2577"},
                    new Select2Opt(){ ID = "2578" ,Text = "2578"},
                    new Select2Opt(){ ID = "2579" ,Text = "2579"},
                    new Select2Opt(){ ID = "2580" ,Text = "2580"},
                    new Select2Opt(){ ID = "2581" ,Text = "2581"},
                    new Select2Opt(){ ID = "2582" ,Text = "2582"},
                    new Select2Opt(){ ID = "2583" ,Text = "2583"},
                };
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
                        AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND,
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
            APP_CLINIC_NOT_ONE_NIGHT_STAND_A_SECTION_CREATE();
            APP_CLINIC_NOT_ONE_NIGHT_STAND_B_SECTION_CREATE();
        }

        private static void APP_CLINIC_NOT_ONE_NIGHT_STAND_A_SECTION_CREATE()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_A).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_A,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND,
                        },
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            Control.APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_CREATE();

        }

        private static void APP_CLINIC_NOT_ONE_NIGHT_STAND_B_SECTION_CREATE()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_B).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_B,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND,
                        },
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            Control.APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_CREATE();

        }

    }

    public static class Control
    {
        public static void APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_CREATE()
        {

            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_A).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_A,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_A",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_A",
                            ColFixed = 6,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = AppContext.StrResources
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_A,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_B",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_B",
                            ColFixed = 6,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            MaxLength = 13,
                            ResourceName = AppContext.StrResources
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_A,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_C",
                            Type = ControlType.AddressForm,
                            AddressForm_ShowVillageControl = true,
                            AddressForm_ShowBuildingControl = true,
                            AddressForm_ShowTelephoneControl = true,
                            AddressForm_ShowFaxControl = true,
                            AddressForm_ShowPostCodeControl = true,
                            DataKey = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_C",
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = AppContext.StrResources,
                            AddressForm_ProvinceType = ProvinceType.BKK
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_A,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_D",
                            Type = ControlType.Heading,
                            DataKey = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_D",
                            ColFixed = 6,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = AppContext.StrResources
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_A,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_E",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_E",
                            ColFixed = 2,
                            Select2Opts = AppContext.optPersonTitle,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = AppContext.StrResources
                        },

                        new FormControl()
                        {
                            Control = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_F",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_F",
                            ColFixed = 5,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = AppContext.StrResources
                        },

                        new FormControl()
                        {
                            Control = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_G",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_NOT_ONE_NIGHT_STAND_A_CONTROL_G",
                            ColFixed = 5,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
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

        public static void APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_CREATE() 
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_B).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_B,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_A",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_A",
                            ColFixed = 6,
                            //Select2Opts = AppContext.Year,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = AppContext.StrResources,
                            PreFill = true,
                            DisplayStaticIfHasData = true,
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_B,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_B",
                            Type = ControlType.TextBox,
                            DataKey = "APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_B",
                            Info = "APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_B_INFO",
                            DefaultShowInfo = true,
                            ColFixed = 6,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = AppContext.StrResources,
                            DisplayReadonly = true,
                        }
                    }
                });

                //items.Add(new FormSectionRow()
                //{
                //    Section = AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_B,
                //    Controls = new List<FormControl>()
                //    {
                //        new FormControl()
                //        {
                //            Control = "APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_C",
                //            Type = ControlType.TextBox,
                //            DataKey = "APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_C",
                //            Info = "APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_C_INFO",
                //            DefaultShowInfo = true,
                //            ColFixed = 6,
                //            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                //            ResourceName = AppContext.StrResources,
                //            DisplayReadonly = true,
                //        }
                //    }
                //});

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_CLINIC_NOT_ONE_NIGHT_STAND_SECTION_B,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_D",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_D",
                            Info = "APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_D_INFO",
                            DefaultShowInfo = true,
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayCheckboxInline = true,
                            CheckboxName = new string[]
                            {
                                "APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_D_CHK_A",
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

    }

}
