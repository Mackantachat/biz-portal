
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_CANCEL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_CANCEL_APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2",
                    SectionGroup = "APP_ELECTRONIC_COMMERCIAL_CANCEL",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_CANCEL,
                    },
					Ordering = 18,
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F37_04_58",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2_CAUSE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2_CAUSE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = new Select2Opt[]
                            {
                                new Select2Opt() { ID = "01", Text = "ขาดทุน" },
                                new Select2Opt() { ID = "02", Text = "ต้นทุนและค่าใช้จ่ายเพิ่มสูงขึ้น" },
                                new Select2Opt() { ID = "03", Text = "เทคโนโลยีการผลิตการบริหารเปลี่ยนแปลงไปอย่างมาก" },
                                new Select2Opt() { ID = "04", Text = "ตลาดมีการแข่งขันสูงมาก ไม่สามารถรักษาส่วนแบ่งในตลาดได้" },
                                new Select2Opt() { ID = "05", Text = "รสนิยมหรือความต้องการของผู้บริโภคเปลี่ยนแปลงไป" },
                                new Select2Opt() { ID = "06", Text = "สภาวะเศรษฐกิจตกต่ำ ผลตอบแทนน้อย" },
                                new Select2Opt() { ID = "07", Text = "ภัยธรรมชาติ" },
                                new Select2Opt() { ID = "08", Text = "โอนกิจการให้ผู้อื่น" },
                                new Select2Opt() { ID = "99", Text = "อื่นๆ" },
                            },
                        	WillTriggerDisplayOfOtherUI = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_59",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2_DATE",
                            Type = ControlType.DatePicker,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2_DATE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            DataFormat = "dd/MM/yyyy",
                            DatePickerPropertiesConfig = new FormControl.DatePickerProperties()
                            {
                                EndDate = "0d"
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                        new FormControl()
                        {
                            FieldID = "F37_04_60",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayCondition = CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2_OTHER(),
                        	ResourceName = "PermitResource.RESOURCE_APP_ELECTRONIC_COMMERCIAL_CANCEL",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F37_04_61",
                            Control = "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ELECTRONIC_COMMERCIAL_CANCEL_INFO_SECTION_2_CONFIRM",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "CONFIRM", /* ข้าพเจ้าขอรับรองว่าเอกสารและข้อความข้างต้นเป็นความจริงทุกประการ */
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
