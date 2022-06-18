
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION",
                    SectionGroup = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_EDIT,
                    },
					Ordering = 1,
					ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_03_01_01",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_CITIZEN_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_CITIZEN_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Citizen,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	PreFill = true,
                        	DisplayStaticIfHasData = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F43_03_01_02",
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_JURISTIC_ID",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_JURISTIC_ID",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                        	PreFill = true,
                        	DisplayStaticIfHasData = true,
                        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH",
                        },
                    }
                });


                //items.Add(new FormSectionRow()
                //{
                //    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION",
                //    RowNumber = 2,
                //    Controls = new List<FormControl>()
                //    {
                //        new FormControl()
                //        {
                //            FieldID = "F43_03_01_03",
                //            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_LICENSE_RENEW",
                //            Type = ControlType.Dropdown,
                //            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_LICENSE_RENEW",
                //            IdentityTypes = new UserTypeEnum[] {
                //                UserTypeEnum.Juristic,
                //                UserTypeEnum.Citizen,
                //             },
                //            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                //            ColFixed = 6,
                //            Select2Opts = new Select2Opt[]
                //            {
                //                new Select2Opt() { ID = "EDIT_1", Text = "ใบที่ 1" },
                //                new Select2Opt() { ID = "EDIT_2", Text = "ใบที่ 2" },
                //            },
                //        	WillTriggerDisplayOfOtherUI = true,
                //        	ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH",
                //        },
                //    }
                //});
         
                items.Add(new FormSectionRow()
                {
                    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_LICENSE_RENEW",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_LICENSE_RENEW",                      
                            IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/ORGANICPLANT/certificatelist",                           
                             
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            PlaceholderText = "กรุณาเลือกใบรับรอง",
                            ResourceName = "PermitResource.RESOURCE_APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH",
                        }
                    }
                });
                //items.Add(new FormSectionRow()
                //{
                //    Section = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION",
                //    RowNumber = 3,
                //    Controls = new List<FormControl>()
                //    {
                         
                //        CUSTOM_FORM_CONTROL_APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_SEARCH(),
                //    }
                //});
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
    }
}
