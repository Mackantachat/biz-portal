
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_LAW_OFFICE
{
    public partial class APP_LAW_OFFICE_APP_LAW_OFFICE_INFO_SECTION_2
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_LAW_OFFICE_INFO_SECTION_2").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_LAW_OFFICE_INFO_SECTION_2",
                    SectionGroup = "APP_LAW_OFFICE",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_LAW_OFFICE,
                    },
					Ordering = 3,
					HideSectionHeader = true,
					ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_LAW_OFFICE_INFO_SECTION_2").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_LAW_OFFICE_INFO_SECTION_2",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F36_01_06",
                            Control = "APP_LAW_OFFICE_INFO_SECTION_2_TOTAL_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_LAW_OFFICE_INFO_SECTION_2_TOTAL_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	DisplayReadonly = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_LAW_OFFICE_INFO_SECTION_2",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F36_01_07",
                            Control = "APP_LAW_OFFICE_INFO_SECTION_2_IS_REQUEST",
                            Type = ControlType.Label,
                            DataKey = "APP_LAW_OFFICE_INFO_SECTION_2_IS_REQUEST",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE",
                        },
                         
                        new FormControl()
                        {
                            FieldID = "F36_01_08",
                            Control = "APP_LAW_OFFICE_INFO_SECTION_2_CONFIRM",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_LAW_OFFICE_INFO_SECTION_2_CONFIRM",
                            Info = "APP_LAW_OFFICE_INFO_SECTION_2_CONFIRM_INFO",
                        	DefaultShowInfo = true,
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "APP_LAW_OFFICE_INFO_SECTION_2_CONFIRM_TRUE", /* ข้าพเจ้าขอรับรองว่า ข้อมูลที่ข้าพเจ้าให้ไว้ต่อสภาทนายความเป็นจริงทั้งสิ้น หากเป็นเท็จยินยอมให้สภาทนายความดำเนินการตามที่เห็นสมควรได้ทันที และข้าพเจ้ายินดีให้ปฏิบัติตามกฎ ระเบียบ ที่สำนักงานทะเบียนสภาทนายความได้ประกาศใช้แล้ว หรือที่จะประกาศใช้ต่อไปทุกประการ */
                            },
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE",
                        },
                        new FormControl()
                        {
                            FieldID = "F36_01_09",
                            Control = "APP_LAW_OFFICE_INFO_SECTION_2_SIGNATURE",
                            Type = ControlType.Signature,
                            DataKey = "APP_LAW_OFFICE_INFO_SECTION_2_SIGNATURE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                             },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_LAW_OFFICE",
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
