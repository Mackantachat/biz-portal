using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_NEW_SECTION_GROUP
{
    #region [ Context ]

    public class Context
    {
        #region [ Constant  ]
        public static string StrFormSectionGroup
        {
            get
            {  
                return "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC";
            }
        }

        public static string StrResource
        {
            get
            {
                return "PermitResource.RESOURCE_APP_ORGANIC_NEW";
            }
        }

        public static Select2Opt[] optLogoAG
        {
            get
            {
                return new Select2Opt[]
                {
                    new Select2Opt() { ID = "1" , Text = "ใช่"},
                    new Select2Opt() { ID = "2" , Text = "ไม่ใช่"},
                };
            }
        }

        public static Select2Opt[] optProductionMoth
        {
            get
            {
                return new Select2Opt[]
                {
                    new Select2Opt() { ID = "1" , Text = "มกราคม"},
                    new Select2Opt() { ID = "2" , Text = "กุมภาพันธ์"},
                    new Select2Opt() { ID = "3" , Text = "มีนาคม"},
                    new Select2Opt() { ID = "4" , Text = "เมษายน"},
                    new Select2Opt() { ID = "5" , Text = "พฤษภาคม"},
                    new Select2Opt() { ID = "6" , Text = "มิถุนายน"},
                    new Select2Opt() { ID = "7" , Text = "กรกฎาคม"},
                    new Select2Opt() { ID = "8" , Text = "สิงหาคม"},
                    new Select2Opt() { ID = "9" , Text = "กันยายน"},
                    new Select2Opt() { ID = "10" , Text = "ตุลาคม"},
                    new Select2Opt() { ID = "11" , Text = "พฤษจิกายน"},
                    new Select2Opt() { ID = "12" , Text = "ธันวาคม"},
                };
            }
        }

        public static Select2Opt[] optPersonTitle
        {
            get
            {
                return new Select2Opt[]
                {
                    new Select2Opt() { ID = "01", Text = "นาย" },
                    new Select2Opt() { ID = "02", Text = "นาง" },
                    new Select2Opt() { ID = "03", Text = "นางสาว" }
                };
            }
        }

        #endregion

        #region [ Section ]
        public static string APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC
        {
            get
            {
                return "APP_ORGANIC_PLANT_PRODUCTION_NEW_CONFIRM_SECTION";
            }
        }

        public static string APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION
        {
            get
            {
                return "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION";
            }
        }

        public static string APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION
        {
            get
            {
                return "APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION";
            }
        }

        public static string APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION
        {
            get
            {
                return "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION";
            }
        }

        public static string APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION
        {
            get
            {
                return "APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION";
            }
        }

        public static string APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_SECTION
        {
            get
            {
                return "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_SECTION";
            }
        }

        public static string APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION
        {
            get
            {
                return "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION";
            }
        }

        public static string APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION
        {
            get
            {
                return "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION";
            }
        }

        public static string APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION
        {
            get
            {
                return "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION";	
            }
        }

        public static string APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION
        {
            get
            {
                return "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION";
            }
        }

        public static string APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION
        {
            get
            {
                return "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION";
            }
        }

        #endregion

        #region [ Condition ]
        public static FormControlDisplayCondition HEAD_OF_GROUP
        {
            get
            {
                FormControlDisplayCondition HEAD_OF_GROUP = new FormControlDisplayCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_KIND_OF_PLANT",
                            ControlAnswer = "2"
                        },
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_HEAD_OF_GROUP",
                            ControlAnswer = "2"
                        },
                    },
                    IsOr = false,
                };
                return HEAD_OF_GROUP;
            }
        }

        public static FormControlDisplayCondition GROUP_PRODUCTION
        {
            get
            {
                FormControlDisplayCondition GROUP_PRODUCTION = new FormControlDisplayCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_KIND_OF_PLANT",
                            ControlAnswer = "2"
                        },
                    },
                };
                return GROUP_PRODUCTION;
            }
        }

        public static FormControlDisplayCondition ALONE_PRODUCTION
        {
            get
            {
                FormControlDisplayCondition ALONE_PRODUCTION = new FormControlDisplayCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_KIND_OF_PLANT",
                            ControlAnswer = "1"
                        },
                    },
                };
                return ALONE_PRODUCTION;
            }
        }

    }
    #endregion

    #endregion

    #region [ FormSectionGroup ]
    public class APP_ORGANIC_NEW_SECTION_GROUP
    {

        public static void AddSectionGroup()
        {
            var db = MongoFactory.GetFormSectionGroupCollection();
            List<FormSectionGroup> items = new List<FormSectionGroup>();

            if (db.AsQueryable().Where(x => x.SectionGroup == Context.StrFormSectionGroup).Count() == 0)
            {
                items.Add(new FormSectionGroup()
                {
                    SectionGroup = Context.StrFormSectionGroup,
                    Ordering = 103,
                    ShowOnSpecificApps = true,
                    AppSystemNames = new string[]
                    {
                        AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                    },
                    ResourceName = Context.StrResource
                });
            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            APP_ORGANIC_NEW_SECTION.AddSection(); // ข้อมูลการขอใบรับรอง

        }
 
    }
    #endregion

    #region [ FormSection ] 
    public class APP_ORGANIC_NEW_SECTION
    {

        #region [ Method Add Section ]
        public static void AddSection()
        {

            #region [ แปลงเดี่ยว ]

            APP_ORGANIC_PLANT_PRODUCTION_NEW_CONFIRM_SECTION();// ข้อมูลการขอใบรับรอง
            APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION();// Section สำหรับแปลงเดี่ยว
            APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION();// ArrayOFForm ข้อมูลแผนการผลิตพืชอินทรีย์
            APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION();//ข้อมูลใบรับรอง            
            APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION();//ข้อมูลบุคคลที่สามารถติดต่อได้สะดวก

            #endregion

            #region [ กลุ่ม ]

            APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_SECTION();
            APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION();
            APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION();
            APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION();
            APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION();
            APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION();

            #endregion

        }
        #endregion

        #region [ แปลงเดี่ยว ]

        #region [ APP_ORGANIC_NEW_SECTION ข้อมูลการขอใบรับรอง ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_CONFIRM_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section= Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC,
                        SectionGroup = Context.StrFormSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                        },
                        ResourceName = Context.StrResource
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            APP_ORGANIC_NEW_CONTROL.APP_ORGANIC_PLANT_PRODUCTION_NEW_CONFIRM_SECTION_ADD_CONTROL();
        }
        #endregion

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION Section แปลงเดี่ยว ก่อน ArrayOfForm ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION,
                        SectionGroup = Context.StrFormSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                        },
                        DisplayCondition = Context.ALONE_PRODUCTION,
                        HideSectionHeader = true,
                        ResourceName = Context.StrResource
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            APP_ORGANIC_NEW_CONTROL.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION_ADD_CONTROL();
        }
        #endregion

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION ArrayOfForm ข้อมูลแผนการผลิตพืชอินทรีย์ ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION,
                        SectionGroup = Context.StrFormSectionGroup,
                        Type = SectionType.ArrayOfForms,
                        ShowOnSpecificApps = true,
                        HideSectionHeader = true,
                        AppSystemNames = new string[]
                          {
                              AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                          },
                        DisplayCondition = Context.ALONE_PRODUCTION,
                        EmptyDataMessage = "APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION_EMPTY_MSG",
                        AddButtonText = "APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION_ADD_MSG",
                        SubmitButtonText = "APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION_SUBMIT_MSG",
                        ResourceName = Context.StrResource,
                        NumberRequiredAtLeast = 1,
                        ArrayRequiredAtLeast = true,
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            APP_ORGANIC_NEW_CONTROL.APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION_ADD_CONTROL();
        }
        #endregion

        #region [ APP_ORGANIC_NEW_SECTION ข้อมูลใบรับรอง ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = Context.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION,
                        SectionGroup = Context.StrFormSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                        },
                        HideSectionHeader = true,
                        DisplayCondition = Context.ALONE_PRODUCTION,
                        ResourceName = Context.StrResource
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            APP_ORGANIC_NEW_CONTROL.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION_ADD_CONTROL();
        }
        #endregion

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION	ข้อมูลบุคคลที่สามารถติดต่อได้สะดวก ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION,
                        SectionGroup = Context.StrFormSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                          {
                              AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW
                          },
                        DisplayCondition = Context.ALONE_PRODUCTION,
                        ResourceName = Context.StrResource
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            APP_ORGANIC_NEW_CONTROL.APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION_ADD_CONTROL();
        }
        #endregion

        #endregion

        #region [ กลุ่ม ]

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_SECTION ประธานกลุ่ม หรือผู้มีอำนาจของกลุ่ม เป็นบุคคลเดียวกันกับบุคคลผู้ขออนุญาต ใช่หรือไม่ ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_SECTION,
                        SectionGroup = Context.StrFormSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                        },
                        DisplayCondition = Context.GROUP_PRODUCTION,
                        HideSectionHeader = true,
                        ResourceName = Context.StrResource
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            APP_ORGANIC_NEW_CONTROL.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_ADD_CONTROL_SECTION();
        }
        #endregion

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION ข้อมูลประธานกลุ่ม ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section= Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION,
                        SectionGroup = Context.StrFormSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW
                        },
                        DisplayCondition = Context.HEAD_OF_GROUP,
                        ResourceName = Context.StrResource
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }

            APP_ORGANIC_NEW_CONTROL.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION_ADD_CONTROL();
        }
        #endregion

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION  ข้อมูลรายละเอียดการผลิตของกลุ่ม ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION,
                        SectionGroup = Context.StrFormSectionGroup,
                        Type = SectionType.ArrayOfForms,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                        },
                        DisplayCondition = Context.GROUP_PRODUCTION,
                        EmptyDataMessage = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION_EMPTY_MSG",
                        AddButtonText = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION_ADD_MSG",
                        SubmitButtonText = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION_SUBMIT_MSG",
                        ResourceName = Context.StrResource,
                        ArrayRequiredAtLeast = true,
                        NumberRequiredAtLeast = 1,
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
            APP_ORGANIC_NEW_CONTROL.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION_ADD_CONTROL();
        }
        #endregion

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION สรุปรายละเอียดของพื้นที่การผลิตและวิธีการผลิตโดยทั่วไปในพื้นที่ ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION,
                        SectionGroup = Context.StrFormSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        HideSectionHeader = true,
                        AppSystemNames = new string[]
                        {
                            AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                        },
                        DisplayCondition = Context.GROUP_PRODUCTION,
                        ResourceName = Context.StrResource
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
            APP_ORGANIC_NEW_CONTROL.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION_ADD_CONTROL();
        }

        #endregion

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION	ข้อมูลระบบการควบคุมภายในที่มี ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION,
                        SectionGroup = Context.StrFormSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] 
                        {
                          AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                        },
                        DisplayCondition = Context.GROUP_PRODUCTION,
                        ResourceName = Context.StrResource
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
            APP_ORGANIC_NEW_CONTROL.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION_ADD_CONTROL();
        }
        #endregion

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION ข้อมูลบุคคลที่สามารถติดต่อได้สะดวก(ควรเป็นบุคคลที่เข้าใจระบบของกลุ่ม) ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION()
        {
            var db = MongoFactory.GetFormSectionCollection();
            List<FormSection> items = new List<FormSection>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION).Count() == 0)
            {

                items.Add
                (
                    new FormSection()
                    {
                        Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION,
                        SectionGroup = Context.StrFormSectionGroup,
                        Type = SectionType.Form,
                        ShowOnSpecificApps = true,
                        AppSystemNames = new string[] 
                        {
                             AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                        },
                        DisplayCondition = Context.GROUP_PRODUCTION,
                        ResourceName = Context.StrResource
                    }
                );

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
            APP_ORGANIC_NEW_CONTROL.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION_ADD_CONTROL();
        }
        #endregion

        #endregion

    }
    #endregion

    #region [ FormSectionRow ]
    public class APP_ORGANIC_NEW_CONTROL
    {

        #region [ Add Method ]

        #region [ แปลงเดี่ยว ]
        public static void APP_ORGANIC_PLANT_PRODUCTION_NEW_CONFIRM_SECTION_ADD_CONTROL()
        {
            APP_ORGANIC_PLANT_PRODUCTION_NEW_CONFIRM_SECTION_CONTROL();    
        }

        public static void APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION_ADD_CONTROL()
        {
            APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION_CONTROL(); 
        }

        public static void APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION_ADD_CONTROL()
        {
            APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION();
        }

        public static void APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION_ADD_CONTROL()
        {
            APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION_CONTROL();
        }

        public static void APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION_ADD_CONTROL()
        {
            APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION_CONTROL();
        }
        #endregion

        #region [ กลุ่ม ]
        public static void APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_ADD_CONTROL_SECTION()
        {
            APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_CONTROL_SECTION();
        }

        public static void APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION_ADD_CONTROL()
        {
            APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION_CONTROL();
        }

        public static void APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION_ADD_CONTROL()
        {
            APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION_CONTROL();
        }

        public static void APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION_ADD_CONTROL()
        {
            APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION_CONTROL();
        }

        public static void APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION_ADD_CONTROL()
        {
            APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION_CONTROL();
        }

        public static void APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION_ADD_CONTROL()
        {
            APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION_CONTROL();
        }
        #endregion

        #endregion

        #region [ APP_ORGANIC_NEW_SECTION ข้อมูลการขอใบรับรอง ]

        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_CONFIRM_SECTION_CONTROL()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC).Count() == 0)
            {
                #region [ คุณต้องการขอใบรับรองในกรณีใด ]

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_REGISTER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_REGISTER",
                            ColFixed = 12,
                            PreFill = true,
                            DisplayStaticIfHasData = true,
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,

                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = Context.StrResource,
                        },
                    }
                });

                #endregion

                #region [ คุณต้องการขอใบรับรองแหล่งผลิตพืชอินทรีย์ในรูปแบบใด ]

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_KIND_OF_PLANT",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_KIND_OF_PLANT",
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            WillTriggerDisplayOfOtherUI = true,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_KIND_OF_PLANT_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "1", RadioButtonText = "แปลงเดี่ยว/รายเดียว" },
                                    //new FormRadioButton() { RadioButtonValue = "2", RadioButtonText = "กลุ่ม" },
                                }

                            },
                        },
                    }
                });

                #endregion

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }

        #endregion

        #region [ แปลงเดี่ยว / รายเดียว ]

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION_CONTROL ก่อน ArrayOfForm ]

        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION_CONTROL()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION).Count() == 0)
            {
                #region [ Header ข้อมูลที่ตั้งแหล่งผลิต/ที่ตั้งแปลง ]

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION",
                            Type = ControlType.Heading,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ResourceName = Context.StrResource,
                            ColFixed = 6,
                        },
                    }
                });

                #endregion

                #region [ AddressForm ข้อมูลที่ตั้งแหล่งผลิต/ที่ตั้งแปลง ]

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_LOCATION_FORM",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            AddressForm_ShowTelephoneControl = true,
                            AddressForm_ShowFaxControl = true,
                            AddressForm_ShowPostCodeControl = true,
                            AddressForm_ShowMapControl = true,
                            AddressForm_ShowMobileControl = true,
                            AddressForm_ShowEmailControl = true,
                            ResourceName = Context.StrResource,
                        },
                    }
                });

                #endregion

                #region [ สถานที่ตั้งแหล่งผลิต/ที่ตั้งแปลงของคุณมีลักษณะกรรมสิทธิตามข้อใด ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_INFORMATION_BUILDING_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_INFORMATION_BUILDING_TYPE",
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 8,
                            WillTriggerDisplayOfOtherUI = true,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_INFORMATION_BUILDING_TYPE",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "1", RadioButtonText = "เป็นเจ้าของสถานที่ตั้งแหล่งผลิต/ที่ตั้งแปลงเอง" },
                                    new FormRadioButton() { RadioButtonValue = "2" , RadioButtonText = "เช่าสถานที่ตั้งแหล่งผลิต/ที่ตั้งแปลงของผู้อื่น" },
                                    new FormRadioButton() { RadioButtonValue = "3" , RadioButtonText = "ใช้สถานที่ตั้งแหล่งผลิต/ที่ตั้งแปลงของผู้อื่นแบบไม่เสียค่าใช้จ่าย" },
                                }
                            },
                            ResourceName = Context.StrResource,
                        },

                    }
                });
                #endregion

                #region [ โปรดระบุประเภทของผู้ให้เช่า/ให้ใช้สถานที่ ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_BUILDING_RENT",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_BUILDING_RENT",
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            WillTriggerDisplayOfOtherUI = true,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_BUILDING_RENT_TYPE",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.JURISTIC },
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.CITIZEN, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.CITIZEN },
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.Government, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.Government },
                                    new FormRadioButton() { RadioButtonValue = StoreInformationBuildingRentingOwnerTypeOptionValueConst.Royal, RadioButtonText = StoreInformationBuildingRentingOwnerTypeOptionTextConst.Royal },
                                }

                            },
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_INFORMATION_BUILDING_TYPE",
                                        ControlAnswer = "2"
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_INFORMATION_BUILDING_TYPE",
                                        ControlAnswer = "3"
                                    }
                                }
                            },
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ คุณต้องการขอใช้เครื่องหมายรับรองผลิตพืชอินทรีย์ จากกรมวิชาการเกษตร ใช่หรือไม่ &  คุณต้องการผลิตพืชประเภทใด ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_ORG_AG",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_ORG_AG",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            Select2Opts = Context.optLogoAG,
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGRANIC_KIND_OF_PRODUCTION",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGRANIC_KIND_OF_PRODUCTION",
                            ShowOnSpecificApps = true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/ORGANICPLANT/Plant2",
                            //Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            PlaceholderText = "กรุณาเลือกชนิดพืช",
                            ResourceName = Context.StrResource
                        }

                    }
                });
                #endregion

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_ATTACT",
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayCheckboxInline = true,
                            CheckboxName = new string[]
                            {
                                "CHK1",
                                "CHK2",
                                "CHK3",
                                "CHK4",
                                "CHK5",
                                "CHK6",
                                "CHK7",
                                "CHK8",
                                "CHK9",
                                "CHK10",                             
                            },
                            ResourceName = Context.StrResource
                        }

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_ALONE_CONDITION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_DOC_TEXT",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 13,
                            Textbox_Rows = 3,
                            ResourceName = Context.StrResource
                        },

                    }
                });

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
        #endregion

        #region [ ArrayOfForm ข้อมูลแผนการผลิตพืชอินทรีย์ ]

        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION).Count() == 0)
            {

                #region [ ชนิดพืช & ขนาดพื้นที่ & จำนวนต้น ]

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_KIND_OF_PLANT",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_KIND_OF_PLANT",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            IsAjaxDropdown = true,
                            AjaxUrl = "~/Api/v2/ORGANICPLANT/Plant",                            
                            PlaceholderText = "เลือกชนิดพืช",
                            AjaxQueryString = "?typid={ID}",
                            ControlVariables = new ControlVariable[] {
                                new ControlVariable() { Name = "ID", ControlSelector = "select[name='AJAX_DROPDOWN_APP_ORGRANIC_KIND_OF_PRODUCTION']", DefaultIfEmpty = "-1", ListenOnChange = true },
                            },
                            ColFixed = 4,
                            ResourceName = Context.StrResource
                            
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_AREA_SIZE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_AREA_SIZE",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            //TextboxNumberSettings = new FormControl.NumberSettings
                            //{
                            //    Min = "0",
                            //    Max = "10000000",
                            //    Step = "0.01"
                            //},
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_NUMBER_OF_TREE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_NUMBER_OF_TREE",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 4,
                            //Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = Context.StrResource,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        },

                    }
                });

                #endregion

                #region [ จำนวนรอบการผลิต/ปี & ช่วงการผลิต (เดือนเริ่มต้น) & ช่วงการผลิต (เดือนสิ้นสุด)]

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_NUMBER_OF_YEAR",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_NUMBER_OF_YEAR",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            ResourceName = Context.StrResource,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0#",
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_RANK_START",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_RANK_START",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[]
                            {
                                //new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" },
                                //new FormValidationRule() { Type = ValidationType.JSExpression, ErrorMessage = "INVALID_MONTH"
                                //, JSExpression = "return $(\"select[name='DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_RANK_START']\").val() < $(\"select[name='DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_RANK_STOP']\").val() ;" }
                                //new FormValidationRule()
                                //{
                                //    Type = ValidationType.JSExpression,
                                //    JSExpression = "return(parseInt($(\"select[name='DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_RANK_START']\").val()) < parseInt($(\"select[name='DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_NEW_RANK_STOP']\").val())) ",
                                //    ErrorMessage = "INVALID_MONTH"
                                //},
                                new FormValidationRule()
                                {
                                    Type = ValidationType.Required ,
                                    ErrorMessage = "* Required",
                                },

                               
                            },
                            ColFixed = 4,
                            Select2Opts = Context.optProductionMoth,
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_RANK_STOP",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_RANK_STOP",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[]
                            {
                                new FormValidationRule()
                                {
                                    Type = ValidationType.Required ,
                                    ErrorMessage = "* Required",
                                },
                            },
                            ColFixed = 4,
                            Select2Opts = Context.optProductionMoth,
                            ResourceName = Context.StrResource
                        },

                    }
                }); 

                #endregion

                #region [ เดือนที่คาดว่าจะเก็บเกี่ยว & ปริมาณผลผลิตที่คาดว่าจะได้รับ (กิโลกรัม) ]

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_INFORMATION_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ESTIMATE_HARVEST",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ESTIMATE_HARVEST",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            //Select2Opts = Context.optProductionMoth,
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ESTIMATE_VOLUME_OF_HARVEST",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_ESTIMATE_VOLUME_OF_HARVEST",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            TextboxNumberSettings = new FormControl.NumberSettings
                            {
                                Min = "0",
                                Max = "10000000",
                                Step = "0.01"
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,      
                            ResourceName = Context.StrResource
                        },

                    }
                });

                #endregion

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
        #endregion

        #region [ ข้อมูลใบรับรอง & ข้อมูลชื่ออื่นภาษาไทย & ข้อมูลชื่ออื่นภาษาไทย-ภาษาอังกฤษ]

        private static void APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION_CONTROL()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_LOCATION_TYPE",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_LOCATION_TYPE",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            WillTriggerDisplayOfOtherUI = true,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_LOCATION_TYPE_OPT",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "1", RadioButtonText = " ที่อยู่บ้าน/สำนักงาน" },
                                    new FormRadioButton() { RadioButtonValue = "2", RadioButtonText = "ที่ตั้งแปลง" },
                                }

                            },
                            ResourceName = Context.StrResource
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE",
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            CheckboxName = new string[]
                            {
                                "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_1",
                                "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_2",
                                "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_3",
                                //"APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_4",
                            },
                            ResourceName = Context.StrResource
                        }

                    }
                });

                #region [ ข้อมูลชื่ออื่นภาษาไทย ]

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION,
                    Controls = new List<FormControl>()
                    {

                         new FormControl()
                         {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_FULL_NAME_THAI_HEADER",
                            Type = ControlType.Heading,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_FULL_NAME_THAI_HEADER",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            ResourceName = Context.StrResource,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_2",
                                        ControlAnswer = "true"
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_4",
                                        ControlAnswer = "true"
                                    },
                                }
                            },
                         },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION,
                    Controls = new List<FormControl>()
                    {

                         new FormControl()
                         {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_FULL_NAME_THAI",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_FULL_NAME_THAI",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = Context.StrResource,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_2",
                                        ControlAnswer = "true"
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_4",
                                        ControlAnswer = "true"
                                    },
                                }
                            },
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", @"{ pattern: /[\u0E00-\u0E7F\s]/, optional: true, recursive: true }"
                                }
                            },
                         },

                    }
                });

                #endregion

                #region [ ข้อมูลชื่ออื่นภาษาไทย-ภาษาอังกฤษ ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_ENG_FULL_NAME_HEADER",
                            Type = ControlType.Heading,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_ENG_FULL_NAME_HEADER",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            ResourceName = Context.StrResource,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_3",
                                        ControlAnswer = "true"
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_4",
                                        ControlAnswer = "true"
                                    },
                                }
                            },
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_ENG_FULL_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_ENG_FULL_NAME",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = Context.StrResource,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_3",
                                        ControlAnswer = "true"
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_4",
                                        ControlAnswer = "true"
                                    },
                                }
                            },
                            DisplayMaskInput = true,
                            MaskInputPattern = "A",
                            MaskInputPatternTranslation = new Dictionary<string, string>
                            {
                                {
                                    "A", @"{ pattern: /[A-Za-z\s]/, optional: true, recursive: true }"
                                }
                            },
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG_HEADING",
                            Type = ControlType.Heading,
                            DataKey = "APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG_HEADING",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 6,
                            ResourceName = Context.StrResource,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_3",
                                        ControlAnswer = "true"
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_4",
                                        ControlAnswer = "true"
                                    },
                                }
                            },
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG",
                            Type = ControlType.AddressFormEN,
                            DataKey = "APP_ORGANIC_OFFICE_LOCATION_INFORMATION_ENG",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_3",
                                        ControlAnswer = "true"
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_4",
                                        ControlAnswer = "true"
                                    },
                                }
                            },
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_LOCATION_HEADING",
                            Type = ControlType.Heading,
                            DataKey = "APP_ORGANIC_PLANT_LOCATION_HEADING",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 6,
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_3",
                                        ControlAnswer = "true"
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_4",
                                        ControlAnswer = "true"
                                    },
                                }
                            },
                        }
                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_LOCATION_FORM",
                            Type = ControlType.AddressFormEN,
                            DataKey = "APP_ORGANIC_PLANT_LOCATION_FORM",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_3",
                                        ControlAnswer = "true"
                                    },
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_4",
                                        ControlAnswer = "true"
                                    },
                                }
                            },
                            AddressFormEN_ShowBuildingControl = false,
                            AddressFormEN_ShowVillageControl = true,
                        },

                    }
                });
                #endregion

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }

        #endregion

        #region [ ข้อมูลบุคคลที่สามารถติดต่อได้สะดวก & ข้าพเจ้าขอรับรองว่า]

        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION_CONTROL()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION).Count() == 0)
            {

                #region [ ข้อมูลผู้ติดต่อ ]

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_TITLE",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = Context.optPersonTitle,
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_FIRST_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_FIRST_NAME",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_LAST_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_LAST_NAME",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            ResourceName = Context.StrResource
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_TELL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_TELL",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_TELL_NEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_TELL_NEXT",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 4,
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_FAX",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_FAX",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 4,
                            ResourceName = Context.StrResource
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_MOBILE_PHONE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_MOBILE_PHONE",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 4,
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_EMAIL",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_EMAIL",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 4,
                            ResourceName = Context.StrResource
                        },

                    }
                });

                #endregion

                #region [ ข้าพเจ้าขอรับรองว่า ]

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_PERSONAL_CONTACT_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONFIRM_ALONE",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONFIRM_ALONE",
                            Info = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONFIRM_ALONE_INFO",
                            DefaultShowInfo = true,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONFIRM_ALONE__TRUE"
                            }
                        }

                    }
                });

                #endregion

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }

        #endregion

        #endregion

        #region [ กลุ่ม ]

        #region [ Radio ประธานกลุ่ม หรือผู้มีอำนาจของกลุ่ม เป็นบุคคลเดียวกันกับบุคคลผู้ขออนุญาต ใช่หรือไม่ ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_CONTROL_SECTION()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_SECTION).Count() == 0)
            {
               
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_SECTION,
                    Controls = new List<FormControl>()
                    {
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_HEAD_OF_GROUP",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_HEAD_OF_GROUP",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            WillTriggerDisplayOfOtherUI = true,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_HEAD_OF_GROUP_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "1", RadioButtonText = "ใช่" },
                                    new FormRadioButton() { RadioButtonValue = "2", RadioButtonText = "ไม่ใช่" },
                                }

                            },
                            ResourceName = Context.StrResource
                        }
                    }
                });

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
        #endregion

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION ข้อมูลประธานกลุ่ม ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION_CONTROL()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_TITLE",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_TITLE",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            Select2Opts = Context.optPersonTitle,
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_FIRST_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_FIRST_NAME",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_FIRST_LAST_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_FIRST_LAST_NAME",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            ResourceName = Context.StrResource
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_ID_CRADE",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_ID_CRADE",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 4,
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",
                            ResourceName = Context.StrResource
                        },

                    }
                });

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_LOCATION_INFORMATION",
                            Type = ControlType.AddressForm,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_LOCATION_INFORMATION",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            AddressForm_ShowBuildingControl = true,
                            AddressForm_ShowTelephoneControl = true,
                            AddressForm_ShowFaxControl = true,
                            AddressForm_ShowPostCodeControl = true,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = Context.StrResource
                        },

                    }
                });

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
        #endregion

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION  ข้อมูลรายละเอียดการผลิตของกลุ่ม ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION_CONTROL()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION).Count() == 0)
            {

                #region [ ข้อมูลรายชื่อ พื้นที่ และชนิดของผู้ผลิตแต่ละราย ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_INDIVIDUAL_AREA",
                            Type = ControlType.Heading,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_INDIVIDUAL_AREA",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ ชื่อ - นามสกุล & เลขบัตรประจำตัวประชาชน 13 หลัก ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_FULL_NAME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_FULL_NAME",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 6,
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_ID_CARD_NUMBER",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_ID_CARD_NUMBER",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            DisplayMaskInput = true,
                            MaskInputPattern = "0-0000-00000-00-0",
                            ColFixed = 6,
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion
                
                #region [ Area ที่อยู่ ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_ADDRESS_LOCATION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_ADDRESS_LOCATION",
                            Textbox_Rows = 3,
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ Area ที่ตั้งแปลง ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_PLANT_LOCATION",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_PLANT_LOCATION",
                            Textbox_Rows = 3,
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ TextBox ชนิดพืช ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_KIND_OF_PLANT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_KIND_OF_PLANT",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ TextBox ช่วงการผลิต (เดือนเริ่มต้น) & ช่วงการผลิต (เดือนสิ้นสุด) ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_START",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_START",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 6,
                            Info = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_START_INFO",
                            DefaultShowInfo = true,
                            Select2Opts = Context.optProductionMoth,
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_STOP",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_STOP",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 6,
                            Info = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_START_INFO",
                            DefaultShowInfo = true,
                            Select2Opts = Context.optProductionMoth,
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ TextBox เดือนที่คาดว่าจะเก็บเกี่ยว & ผลผลิตรวมที่คาดว่าจะได้รับต่อปี ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_ESTIMATE_HARVEST_MONTH",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_ESTIMATE_HARVEST_MONTH",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 6,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            Select2Opts = Context.optProductionMoth,
                            ResourceName = Context.StrResource
                        },

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_ESTIMATE_HARVEST_VOLUME",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_ESTIMATE_HARVEST_VOLUME",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 6,
                            Rules = new FormValidationRule[] { new FormValidationRule () { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
        #endregion

        #region [ สรุปรายละเอียดของพื้นที่การผลิตและวิธีการผลิตโดยทั่วไปในพื้นที่ ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION_CONTROL()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION).Count() == 0)
            {

                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            Info = "APP_ORGANIC_PLANT_PRODUCTION_NEW_HEAD_OF_GROUP_PRODUCTION_SUMMARY_INFORMATION",
                            Textbox_Rows = 3,
                            DefaultShowInfo  = true,
                        },

                    }
                });           

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
        #endregion

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION	ข้อมูลระบบการควบคุมภายในที่มี ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION_CONTROL()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION).Count() == 0)
            {

                #region [ การตรวจติดตามคุณภาพภายใน และการปฏิบัติการแก้ไข ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_CRITERIA",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_CRITERIA",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_CRITERIA_OPT",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "มี", RadioButtonText = "มี" },
                                    new FormRadioButton() { RadioButtonValue = "ไม่มี", RadioButtonText = "ไม่มี" },
                                }

                            },
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ การตรวจติดตามคุณภาพภายใน และการปฏิบัติการแก้ไข ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_INSPECTION",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_INSPECTION",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_INSPECTION_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "มี", RadioButtonText = "มี" },
                                    new FormRadioButton() { RadioButtonValue = "ไม่มี", RadioButtonText = "ไม่มี" },
                                }

                            },
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ การจัดการข้อร้องเรียน ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_COMPLAIN",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_COMPLAIN",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_COMPLAIN_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "มี", RadioButtonText = "มี" },
                                    new FormRadioButton() { RadioButtonValue = "ไม่มี", RadioButtonText = "ไม่มี" },
                                }

                            },
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ การควบคุมเอกสาร และบันทึก ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_DOC",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_DOC",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            WillTriggerDisplayOfOtherUI = true,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_DOC_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "มี", RadioButtonText = "มี" },
                                    new FormRadioButton() { RadioButtonValue = "ไม่มี", RadioButtonText = "ไม่มี" },
                                }

                            },
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ การฝึกอบรม ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_TRAINING",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_TRAINING",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            WillTriggerDisplayOfOtherUI = true,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_TRAINING_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "มี", RadioButtonText = "มี" },
                                    new FormRadioButton() { RadioButtonValue = "ไม่มี", RadioButtonText = "ไม่มี" },
                                }

                            },
                            ResourceName = Context.StrResource
                        }
                    }
                });
                #endregion

                #region [ ระบบการตามสอบผลิตผล (Traceability of Produce) ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_TRACING",
                            Type = ControlType.RadioGroup,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_TRACING",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 12,
                            WillTriggerDisplayOfOtherUI = true,
                            RadioGroup = new FormRadioGroup()
                            {
                                RadioGroupName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_TRACING_OPTION",
                                RadioButtons = new FormRadioButton[]
                                {
                                    new FormRadioButton() { RadioButtonValue = "มี", RadioButtonText = "มี" },
                                    new FormRadioButton() { RadioButtonValue = "ไม่มี", RadioButtonText = "ไม่มี" },
                                }
                            },
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ อื่นๆ ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_OTHER",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_OTHER",
                            ColFixed = 12,
                            DisplayStaticIfHasData = true,
                            HideLabel = true,
                            CheckboxName = new string[]
                            {
                                "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_OTHER_CHECKBOX"
                            },
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ ระบุอื่นๆ ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_OTHER_TEXT",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_OTHER_TEXT",
                            ColFixed = 12,
                            PreFill = true,
                            AppSystemNames = new string[]
                            {
                            AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            DisplayCondition = new FormControlDisplayCondition
                            {
                                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                                {
                                    new FormControlDisplayCondition.ControlWithAnswer
                                    {
                                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_OTHER__APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_INSIDE_CONTROL_OTHER_CHECKBOX",
                                        ControlAnswer = "true",
                                    },
                                }
                            },
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "*Required" } },
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
        #endregion

        #region [ APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION ข้อมูลบุคคลที่สามารถติดต่อได้สะดวก(ควรเป็นบุคคลที่เข้าใจระบบของกลุ่ม) ]
        private static void APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION_CONTROL()
        {
            var db = MongoFactory.GetFormSectionRowCollection();

            List<FormSectionRow> items = new List<FormSectionRow>();

            if (db.AsQueryable().Where(x => x.Section == Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION).Count() == 0)
            {

                #region [ Heading ลำดับที่ 1 ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_INFO_1",
                            Type = ControlType.Heading,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_INFO_1",
                            ColFixed = 12,
                            ResourceName = Context.StrResource
                        },
                      
                    }
                });
                #endregion

                #region [ คำนำหน้าชื่อ & ชื่อ & นามสกุล]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_TITLE_1",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_TITLE_1",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 4,
                            Select2Opts = Context.optPersonTitle,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "*Required" } },
                            ResourceName = Context.StrResource
                        },
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_FIRST_NAME_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_FIRST_NAME_1",
                            ColFixed = 4,
                            PreFill = true,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "*Required" } },
                            ResourceName = Context.StrResource
                        },
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_LAST_NAME_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_LAST_NAME_1",
                            ColFixed = 4,
                            PreFill = true,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "*Required" } },
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ ตำแหน่ง & โทรศัพท์ & ต่อ ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_POSITION_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_POSITION_1",
                            ColFixed = 4,
                            PreFill = true,
                            ResourceName = Context.StrResource
                        },
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_TELL_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_TELL_1",
                            ColFixed = 4,
                            PreFill = true,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "*Required" } },
                            ResourceName = Context.StrResource
                        },
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_TELL_EXT_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_TELL_EXT_1",
                            ColFixed = 4,
                            PreFill = true,
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ โทรสาร & มือถือ & อีเมล์ ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_FAX_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_FAX_1",
                            ColFixed = 4,
                            PreFill = true,
                            ResourceName = Context.StrResource
                        },
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_MOBILE_PHONE_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_MOBILE_PHONE_1",
                            ColFixed = 4,
                            PreFill = true,
                            ResourceName = Context.StrResource
                        },
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_EMAIL_1",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_EMAIL_1",
                            ColFixed = 4,
                            PreFill = true,
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ Heading ลำดับที่ 2 ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_INFO_2",
                            Type = ControlType.Heading,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_INFO_2",
                            ColFixed = 12,
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ คำนำหน้าชื่อ & ชื่อ & นามสกุล]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_TITLE_2",
                            Type = ControlType.Dropdown,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_TITLE_2",
                            ShowOnSpecificApps =true,
                            AppSystemNames = new string[]
                            {
                                AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                            },
                            ColFixed = 4,
                            Select2Opts = Context.optPersonTitle,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "*Required" } },
                            ResourceName = Context.StrResource
                        },
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_FIRST_NAME_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_FIRST_NAME_2",
                            ColFixed = 4,
                            PreFill = true,
                            //Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "*Required" } },
                            ResourceName = Context.StrResource
                        },
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_LAST_NAME_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_LAST_NAME_2",
                            ColFixed = 4,
                            PreFill = true,
                            //Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "*Required" } },
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ ตำแหน่ง & โทรศัพท์ & ต่อ ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_POSITION_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_POSITION_2",
                            ColFixed = 4,
                            PreFill = true,
                            ResourceName = Context.StrResource
                        },
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_TELL_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_TELL_2",
                            ColFixed = 4,
                            PreFill = true,
                            //Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "*Required" } },
                            ResourceName = Context.StrResource
                        },
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_TELL_EXT_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_TELL_EXT_2",
                            ColFixed = 4,
                            PreFill = true,
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ โทรสาร & มือถือ & อีเมล์ ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_FAX_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_FAX_2",
                            ColFixed = 4,
                            PreFill = true,
                            ResourceName = Context.StrResource
                        },
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_MOBILE_PHONE_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_MOBILE_PHONE_2",
                            ColFixed = 4,
                            PreFill = true,
                            ResourceName = Context.StrResource
                        },
                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_EMAIL_2",
                            Type = ControlType.TextBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_EMAIL_2",
                            ColFixed = 4,
                            PreFill = true,
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

                #region [ ข้าพเจ้าขอรับรองว่า ]
                items.Add(new FormSectionRow()
                {
                    Section = Context.APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONTACT_SECTION,
                    Controls = new List<FormControl>()
                    {

                        new FormControl()
                        {
                            Control = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONFIRM_GROUP",
                            Type = ControlType.CheckBox,
                            DataKey = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONFIRM_GROUP",
                            Info = "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONFIRM_GROUP_INFO",
                            DefaultShowInfo = true,
                            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
                            ColFixed = 12,
                            CheckboxName = new string[]
                            {
                                "APP_ORGANIC_PLANT_PRODUCTION_NEW_GROUP_PRODUCTION_CONFIRM_GROUP__TRUE"
                            },
                            ResourceName = Context.StrResource
                        },

                    }
                });
                #endregion

            }

            if (items.Count > 0)
            {
                db.InsertMany(items.ToArray());
            }
        }
        #endregion

        #endregion

    }
    #endregion

}