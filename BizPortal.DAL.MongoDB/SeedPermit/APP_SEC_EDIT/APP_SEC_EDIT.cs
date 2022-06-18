using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_SEC_EDIT
{
    public static class AppContext
    {

        #region [ Const ]
        public static string StrSectionGroup
        {
            get
            {
                return "APP_SEC_EDIT_SECTION_GROUP";
            }
        }

        public static string StrResources
        {
            get
            {
                return "PermitResource.APP_SEC_EDIT";
            }
        }
        #endregion

        #region [ Section ]
        public static string APP_SEC_EDIT_SECTION
        {
            get
            {
                return "APP_SEC_EDIT_SECTION";
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
                        AppSystemNameTextConst.APP_SEC_EDIT,
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
            APP_SEC_EDIT_SECTION();
        }

        private static void APP_SEC_EDIT_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_EDIT_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.APP_SEC_EDIT_SECTION,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SEC_EDIT,
                        },
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            Control.APP_SEC_EDIT_SECTION_CONTROL();

        }
    }

    public static class Control
    {
        public static void APP_SEC_EDIT_SECTION_CONTROL()
        {

            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_EDIT_SECTION).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_EDIT_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_EDIT_SECTION_CONTROL_CHECKBOX",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_SEC_EDIT_SECTION_CONTROL_CHECKBOX",
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            CheckboxName = new string[]
                            {
                                "CHK_1",
                                "CHK_2",
                                "CHK_3",
                                "CHK_4",                               
                            },
                            ResourceName = AppContext.StrResources
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_EDIT_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_EDIT_SECTION_CONTROL_TEXT_AREA",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_EDIT_SECTION_CONTROL_TEXT_AREA",
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Textbox_Rows = 3,
                            ResourceName = AppContext.StrResources
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_EDIT_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_EDIT_CONFIRM",
                            Type = ControlType.Confirm_SEC_EDIT,
                            DataKey = "APP_SEC_EDIT_CONFIRM",
                            DisplayReadonly = true,
                            HideLabel = true,
                            ColFixed = 12,
                            ResourceName = AppContext.StrResources,
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
