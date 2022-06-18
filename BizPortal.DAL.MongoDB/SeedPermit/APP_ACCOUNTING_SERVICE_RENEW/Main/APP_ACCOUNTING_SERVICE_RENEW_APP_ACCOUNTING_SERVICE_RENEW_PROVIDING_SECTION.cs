
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE_RENEW
{
    public partial class APP_ACCOUNTING_SERVICE_RENEW_APP_ACCOUNTING_SERVICE_RENEW_PROVIDING_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_RENEW_PROVIDING_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_PROVIDING_SECTION",
                    SectionGroup = "APP_ACCOUNTING_SERVICE_RENEW",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW,
                    },
                    IdentityTypes = new UserTypeEnum[] {
                        UserTypeEnum.Juristic,
                    },
					Ordering = 5,
					ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ACCOUNTING_SERVICE_RENEW_PROVIDING_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ACCOUNTING_SERVICE_RENEW_PROVIDING_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        //new FormControl()
                        //{
                        //    FieldID = "F43_02_35",
                        //    Control = "APP_ACCOUNTING_SERVICE_RENEW_PROVIDING_SECTION_PROVIDING_TYPE",
                        //    Type = ControlType.CheckBox,
                        //    DataKey = "APP_ACCOUNTING_SERVICE_RENEW_PROVIDING_SECTION_PROVIDING_TYPE",
                        //    IdentityTypes = new UserTypeEnum[] {
                        //        UserTypeEnum.Juristic,
                        //    },
                        //    Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                        //    ColFixed = 12,
                        //    CheckboxName = new string[]
                        //    {
                        //        "DEPOSIT", /* บัญชีเงินฝากประจำ */
                        //        "DEPOSIT_CARD", /* บัตรเงินฝาก */
                        //        "THAI_BONDS", /* พันธบัตรรัฐบาลไทย  */
                        //        "CORPARATE_BONDS", /* พันธบัตรองค์กรหรือรัฐวิสาหกิจ */
                        //        "POLICY", /* กรมธรรม์ประกันภัย */
                        //    },
                        //	ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
                        //},
                        new FormControl()
                        {
                            FieldID = "F43_02_35",
                            Control = "APP_ACCOUNTING_SERVICE_RENEW_PROVIDING_SECTION_PROVIDING_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ACCOUNTING_SERVICE_RENEW_PROVIDING_SECTION_PROVIDING_TYPE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ACCOUNTING_SERVICE_RENEW_PROVIDING_SECTION_PROVIDING_TYPE_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() {
                                        RadioButtonValue = "DEPOSIT",
                                        RadioButtonText = "บัญชีเงินฝากประจำ"
                                    },
                                    new FormRadioButton() {
                                        RadioButtonValue = "DEPOSIT_CARD",
                                        RadioButtonText = "บัตรเงินฝาก"
                                    },
                                    new FormRadioButton() {
                                        RadioButtonValue = "THAI_BONDS",
                                        RadioButtonText = "พันธบัตรรัฐบาลไทย"
                                    },
                                    new FormRadioButton() {
                                        RadioButtonValue = "CORPARATE_BONDS",
                                        RadioButtonText = "พันธบัตรองค์กรหรือรัฐวิสาหกิจ"
                                    },
                                    new FormRadioButton() {
                                        RadioButtonValue = "POLICY",
                                        RadioButtonText = "กรมธรรม์ประกันภัย"
                                    },
                                },
                            },
                            ResourceName = "PermitResource.RESOURCE_APP_ACCOUNTING_SERVICE_RENEW",
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
