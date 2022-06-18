
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                    },
                    IdentityTypes = new UserTypeEnum[] {
                        UserTypeEnum.Juristic,
                    },
					Ordering = 6,
					ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_01_42",
                            Control = "APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_REGISTERED_CAPITAL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_REGISTERED_CAPITAL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] 
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.JSExpression, JSExpression = "return APP_ELECTRONIC_COMMERCIAL_validateRegisteredCapital(); ", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_REGISTERED_CAPITAL_INVALID" }
                            },
                            ColFixed = 4,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_43",
                            Control = "APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_SEPERATED_TO",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_SEPERATED_TO",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                new FormValidationRule() { Type = ValidationType.JSExpression, JSExpression = "return APP_ELECTRONIC_COMMERCIAL_validateSeparatedTo(); ", ErrorMessage = "APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_SEPERATED_TO_INVALID" }
                            },
                            ColFixed = 4,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_01_45",
                            Control = "APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_SHARE_BATH",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_SHARE_BATH",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            TextboxNumberSettings =  new FormControl.NumberSettings()
                            {
                                Min = "0",
                                Max = int.MaxValue.ToString(),
                                Step = "0.01"
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL",
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
