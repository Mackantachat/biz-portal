
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_CANCEL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_CANCEL_APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL_CANCEL",
                    Type = SectionType.ArrayOfForms,
                    EmptyDataMessage = "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2_EMPTY",
                    AddButtonText = "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2_ADD",
                    SubmitButtonText = "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2_SUBMIT",
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_CANCEL,
                    },
                    IdentityTypes = new UserTypeEnum[] {
                        UserTypeEnum.Juristic,
                    },
					Ordering = 14,
					HideSectionHeader = true,
					ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_04_44",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2_NATIONALITY",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2_NATIONALITY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            Select2Opts = DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2_NATIONALITY(),
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_45",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2_SHARE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_STOCK_SECTION_2_SHARE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "###0.00",
                            MaskInputReverse = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
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
