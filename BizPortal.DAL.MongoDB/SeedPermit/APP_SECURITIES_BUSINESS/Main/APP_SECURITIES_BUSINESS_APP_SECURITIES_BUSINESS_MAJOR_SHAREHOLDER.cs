
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels.Select2;
using BizPortal.Utils.Extensions;

namespace BizPortal.SeedPermit.APP_SECURITIES_BUSINESS
{
    public partial class APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER",
                    SectionGroup = "APP_SECURITIES_BUSINESS",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = AppSystemNameTextConst.ALL_APP_SECURITIES_BUSINESS,
                    Ordering = 3,
                    ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_01",
                            Control = "APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER_MAJOR_SHAREHOLDER_TYPE",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER_MAJOR_SHAREHOLDER_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Info = "APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER_MAJOR_SHAREHOLDER_TYPE_INFO",
                            DefaultShowInfo = true,
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "CITIZEN",
                                "JURISTIC"
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F40_01_02",
                            Control = "APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER_ONE_SHARE_PER_ONE_AUTHOURITY",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER_ONE_SHARE_PER_ONE_AUTHOURITY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "YES_NO",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton()
                                    {
                                        RadioButtonText = "ใช่",
                                        RadioButtonValue = "yes"
                                    },
                                    new FormRadioButton()
                                    {
                                        RadioButtonText = "ไม่ใช่",
                                        RadioButtonValue = "no"
                                    }
                                }
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                        },
                        new FormControl()
                        {
                            Control = "APP_SECURITIES_BUSINESS_SHAREHOLDER_REASONS",
                            Type = ControlType.TextBox,
                            DataKey = "APP_SECURITIES_BUSINESS_SHAREHOLDER_REASONS",
                            Textbox_Rows = 3,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            ColFixed = 12,
                            ResourceName = "PermitResource.RESOURCE_APP_SECURITIES_BUSINESS",
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER_ONE_SHARE_PER_ONE_AUTHOURITY",
                                        ControlAnswer = "no",
                                    },
                                }
                            },
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
