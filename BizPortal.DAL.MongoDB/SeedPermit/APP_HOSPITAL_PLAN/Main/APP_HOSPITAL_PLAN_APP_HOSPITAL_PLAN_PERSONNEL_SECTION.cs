
using BizPortal.DAL.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PLAN
{
    public partial class APP_HOSPITAL_PLAN_APP_HOSPITAL_PLAN_PERSONNEL_SECTION
    {
        public static void Init()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_PERSONNEL_SECTION").Count() == 0)
            {
                items.Add(new FormSection()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION",
                    SectionGroup = "APP_HOSPITAL_PLAN",
                    Type = SectionType.Form,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[] {
                        AppSystemNameTextConst.APP_HOSPITAL,
                    },
					Ordering = 2,
					ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
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

            if (db.AsQueryable().Where(x => x.Section == "APP_HOSPITAL_PLAN_PERSONNEL_SECTION").Count() == 0)
            {
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION",
                    RowNumber = 0,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_19",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_DOCTOR_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_DOCTOR_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_20",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_NURSE_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_NURSE_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION",
                    RowNumber = 1,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_21",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_DENTIST_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_DENTIST_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_22",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_PHARMACIST_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_PHARMACIST_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION",
                    RowNumber = 2,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_23",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THERAPIST_AMOUNT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THERAPIST_AMOUNT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_24",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_TECHNICAL_AMONT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_TECHNICAL_AMONT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION",
                    RowNumber = 3,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_25",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THAI_TRADITIONAL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THAI_TRADITIONAL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_26",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THAI_TRADITIONAL_APPLIED",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THAI_TRADITIONAL_APPLIED",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_27",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THAI_MEDICINE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THAI_MEDICINE",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            FieldID = "F35_02_28",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THAI_PHARMACY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_THAI_PHARMACY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PLAN_OFFICER_LABEL",
                            Type = ControlType.Heading,
                            DataKey = "APP_HOSPITAL_PLAN_OFFICER_LABEL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },                      
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_TREATMENT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_TREATMENT",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_CORRECTION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_CORRECTION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_HEART",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_HEART",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_REDIATION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_REDIATION",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_CYCOLOGY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_CYCOLOGY",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_TOOL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_TOOL",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION",
                    RowNumber = 4,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_CHINESS_MEDICEN",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_CHINESS_MEDICEN",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                            ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },                       
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION",
                    RowNumber = 5,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_29",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_OTHER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_OTHER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 6,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
                        },                        
                    }
                });
                items.Add(new FormSectionRow()
                {
                    Section = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION",
                    RowNumber = 6,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            FieldID = "F35_02_30",
                            Control = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_HEADER",
                            Type = ControlType.Heading,
                            DataKey = "APP_HOSPITAL_PLAN_PERSONNEL_SECTION_HEADER",
                            IdentityTypes = new UserTypeEnum[] {
                                UserTypeEnum.Juristic,
                                UserTypeEnum.Citizen,
                            },
                            ColFixed = 12,
                        	ResourceName = "PermitResource.RESOURCE_APP_HOSPITAL_PLAN",
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
