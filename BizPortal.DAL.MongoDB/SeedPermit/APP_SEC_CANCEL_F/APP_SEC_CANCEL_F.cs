using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.SeedPermit.APP_SEC_CANCEL_F
{
    class AppContext
    {
        #region [ Const ]
        public static string StrSectionGroup
        {
            get
            {
                return "APP_SEC_CANCEL_F_SECTION_GROUP";
            }
        }

        public static string StrResources
        {
            get
            {
                return "PermitResource.APP_SEC_CANCEL_F";
            }
        }
        #endregion

        #region [ Section ]
        public static string APP_SEC_CANCEL_F_INFORMATION_SECTION
        {
            get
            {
                return "APP_SEC_CANCEL_F_INFORMATION_SECTION";
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
                        AppSystemNameTextConst.APP_SEC_CANCEL_F,
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
            APP_SEC_CANCEL_F_INFORMATION_SECTION();
        }

        private static void APP_SEC_CANCEL_F_INFORMATION_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_CANCEL_F_INFORMATION_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = AppContext.APP_SEC_CANCEL_F_INFORMATION_SECTION,
                        SectionGroup = AppContext.StrSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_SEC_CANCEL_F,
                        },
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            Control.APP_SEC_CANCEL_F_INFORMATION_SECTION_CONTROL();

        }
    }

    public static class Control
    {
        public static void APP_SEC_CANCEL_F_INFORMATION_SECTION_CONTROL()
        {

            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == AppContext.APP_SEC_CANCEL_F_INFORMATION_SECTION).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_CANCEL_F_INFORMATION_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_CANCEL_F_INFORMATION_TEXTBOX_CONTROL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SEC_CANCEL_F_INFORMATION_TEXTBOX_CONTROL",
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Textbox_Rows = 3,
                            ResourceName = AppContext.StrResources
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = AppContext.APP_SEC_CANCEL_F_INFORMATION_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_SEC_CANCEL_F_SIGNATURE",
                            Type = ControlType.Confirm_SEC_EDIT,
                            DataKey = "APP_SEC_CANCEL_F_SIGNATURE",
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
