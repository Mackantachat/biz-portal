
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_CANCEL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_CANCEL_APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_TYPE_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_TYPE_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_TYPE_SECTION",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL_CANCEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_CANCEL,
                    },
					Ordering = 7,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_TYPE_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_TYPE_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                         
                        new FormControl()
                        {
                            FieldID = "F37_04_14",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_TYPE_SECTION_IS_TRANSFER",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_TYPE_SECTION_IS_TRANSFER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 6,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_TRANSFER_TYPE_SECTION_IS_TRANSFER_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                        			new FormRadioButton() { RadioButtonValue = "YES", RadioButtonText = "ใช่" },
                        			new FormRadioButton() { RadioButtonValue = "NO", RadioButtonText = "ไม่ใช่" },
                                }
                            },
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
