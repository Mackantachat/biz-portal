using BizPortal.DAL.MongoDB;
using BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL;
using BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_CANCEL;
using BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH;
using BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_EDIT;
using BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH;
using BizPortal.SeedPermit.APP_ENERGY_PRODUCTION;
using BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_CANCEL;
using BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_CANCEL_SEARCH;
using BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_EDIT;
using BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_EDIT_SEARCH;
using BizPortal.SeedPermit.APP_HEALTH_CANCEL_SEARCH_GROUP;
using BizPortal.SeedPermit.APP_HEALTH_EDIT_SEARCH_GROUP;
using BizPortal.SeedPermit.APP_HEALTH_RENEW_SEARCH_GROUP;
using BizPortal.SeedPermit.APP_HOSPITAL_LICENSE;
using BizPortal.SeedPermit.APP_HOSPITAL_PLAN;
using BizPortal.SeedPermit.APP_LAW_OFFICE;
using BizPortal.SeedPermit.APP_LAW_OFFICE_CANCEL;
using BizPortal.SeedPermit.APP_LAW_OFFICE_EDIT;
using BizPortal.SeedPermit.APP_SECURITIES_BUSINESS;
using BizPortal.SeedPermit.APP_TOURISM_BUSINESS;
using BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_RENEW;
using BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_RENEW_SEARCH;
using BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_EDIT;
using BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH;
using BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_CANCEL;
using BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH;
using BizPortal.SeedPermit.APP_ORGANIC_NEW_SECTION_GROUP;
using BizPortal.UnitTest.Permit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BizPortal.UnitTest
{
    [TestClass]
    public class SingleFormTest
    {
        [TestMethod]
        public void Init()
        {
            FormSectionGroup.Init();
            FormSection.Init();
            FormSectionRow.Init();
            SingleFormAppFile.Init();
            SingleFormFileList.Init();

            AppPrefillAnswer.Init();
            //ApplicationStatusOther.Init();

            //ReceiptRunningTransaction.Init();

            InitPermitAll();
        }

        [TestMethod]
        public void CleanAndInit()
        {

            //[TODO] : ถ้า Deploy Production ต้องแก้ revisionCode กับ revisionName ทุกครั้ง
            bool applyRevision = false;
            int revisionCode = 9;
            string revisionName = "Phase2.1 Update 20201218";
            string revisionDesc = "update 2020 12 18";
            InitLog log = new InitLog() { StartDeployDate = DateTime.Now, Data = new Dictionary<string, string>() };

            #region Init log
            log.HostName = Dns.GetHostName();
            var addrList = Dns.GetHostEntry(log.HostName).AddressList.Where(o => o.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToList();
            string myIP = "";
            foreach (var addr in addrList)
            {
                if (myIP != "") myIP += "; ";
                myIP += addr;
            }

            log.StartDeployDate = DateTime.Now;
            log.IPAddress = myIP;
            log.Data.Add("START_DEPLOY_DATE", log.StartDeployDate.ToString("yyyy-MM-dd HH:mm:ss"));
            MongoFactory.GetInitLogCollection().InsertOne(log);
            #endregion

            if (applyRevision)
            {
                #region Revision control

                //FormConfig: Check if revisionCode already exist in FormSectionXXXRevision or exist in FormSectionXXX
                var db = MongoFactory.GetFormSectionGroupCollection().AsQueryable();
                //var oldRevisionCount = db.Where(o => o.RevisionCode == revisionCode).Count();
                var oldRevisionCount = MongoFactory.GetFormSectionGroupRevisionCollection()
                    .AsQueryable()
                    .Where(o => o.RevisionCode == revisionCode)
                    .Count();
                if (oldRevisionCount > 0)
                {
                    // The revisionCode is already used
                    throw new Exception("The revisionCode (" + revisionCode + ") is already in used. Please increase revisionCode then try again.");
                }

                // Check if the deploying revision is newer than current revision.
                int? currentRevision = db.Any() ? db.Max(o => (int?)o.RevisionCode) : (int?)0;
                if (revisionCode > currentRevision.GetValueOrDefault(0))
                {
                    // Archive existing revision before replace with new revision.
                    archiveFormConfig(currentRevision.Value);
                }
                #endregion
            }

            MongoFactory.GetQuizGroupCollection()
                .DeleteMany<QuizGroup>(x => true);
            MongoFactory.GetQuizSectionRowCollection()
                .DeleteMany<QuizSectionRow>(x => true);
            MongoFactory.GetQuizAppMappingCollection()
                .DeleteMany<QuizAppMapping>(x => true);
            MongoFactory.GetSmartQuizCollection()
                .DeleteMany<SmartQuiz>(x => true);

            MongoFactory.GetFormSectionGroupCollection().DeleteMany<FormSectionGroup>(x => true);
            MongoFactory.GetFormSectionCollection().DeleteMany<FormSection>(x => true);
            MongoFactory.GetFormSectionRowCollection().DeleteMany<FormSectionRow>(x => true);

            MongoFactory.GetSingleFormAppFileCollection().DeleteMany<SingleFormAppFile>(x => true);
            MongoFactory.GetSingleFormFileListCollection().DeleteMany<SingleFormFileList>(x => true);
            MongoFactory.GetAppPrefillAnswerCollection().DeleteMany<AppPrefillAnswer>(x => true);

            InitQuiz();
            Init();
            InitPermitAll();
            InitHoliday();

            FixWrongStatus();
            FixWrongApplicationID();

            //FormConfig Revision: Override revisionCode and revisionName on every item in FormSectionXXX
            if (applyRevision) assignRevision(revisionCode, revisionName, revisionDesc);

            logInit(log);
        }

        [TestMethod]
        public void FixWrongNationID()
        {
            // แก้ไข nation
            // ApplicationRequestEntity.FixWrongNationID();
            ApplicationRequestEntity.FixWrongUpdateSectionControlID("APP_SELL_FOOD_RENEW_LT_MANAGER_INFO", "DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__NATIONALITY","70","TH");
            ApplicationRequestEntity.FixWrongUpdateSectionControlID("SELL_FOOD_FOOD_MANAGER_INFO", "DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__NATIONALITY", "70", "TH");
        }

        [TestMethod]
        public void CleanPermitAll()
        {
            MongoFactory.GetQuizGroupCollection()
                .DeleteMany<QuizGroup>(x => true);
            MongoFactory.GetQuizSectionRowCollection()
                .DeleteMany<QuizSectionRow>(x => true);
            MongoFactory.GetQuizAppMappingCollection()
                .DeleteMany<QuizAppMapping>(x => true);
            MongoFactory.GetSmartQuizCollection()
                .DeleteMany<SmartQuiz>(x => true);

            MongoFactory.GetFormSectionGroupCollection().DeleteMany<FormSectionGroup>(x => true);
            MongoFactory.GetFormSectionCollection().DeleteMany<FormSection>(x => true);
            MongoFactory.GetFormSectionRowCollection().DeleteMany<FormSectionRow>(x => true);

            MongoFactory.GetSingleFormAppFileCollection().DeleteMany<SingleFormAppFile>(x => true);
            MongoFactory.GetSingleFormFileListCollection().DeleteMany<SingleFormFileList>(x => true);
            MongoFactory.GetAppPrefillAnswerCollection().DeleteMany<AppPrefillAnswer>(x => true);
        }

        [TestMethod]
        public void InitPermitAll()
        {
            InitPermit5();
            InitPermit6();
            InitPermit35();
            InitPermit36();
            InitPermit37();
            InitPermit38();
            InitPermit39();
            InitPermit40();
            InitPermit42();
            InitPermit43();
            InitPermit46();
            InitPermit47();
            InitPermit49();
            InitAPP_HEALTH();
            InitAPP_ORGANIC_NEW();
            InitSEC();
            InitSoftWareHouse();
            Init_SPA();
        }

        [TestMethod]
        public void InitPermit5()
        {
            Permit_5.Init();
        }

        [TestMethod]
        public void InitPermit6()
        {
            Permit_6.Init();
        }

        [TestMethod]
        public void InitPermit35()
        {
            APP_HOSPITAL_PLAN.Init();
            APP_HOSPITAL_LICENSE.Init();
        }

        [TestMethod]
        public void InitPermit36()
        {
            APP_LAW_OFFICE.Init();
            APP_LAW_OFFICE_EDIT.Init();
            APP_LAW_OFFICE_CANCEL.Init();
        }

        [TestMethod]
        public void InitPermit37()
        {
            APP_ELECTRONIC_COMMERCIAL.Init();
            APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH.Init();
            APP_ELECTRONIC_COMMERCIAL_EDIT.Init();
            APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH.Init();
            APP_ELECTRONIC_COMMERCIAL_CANCEL.Init();
        }

        [TestMethod]
        public void InitPermit38()
        {
            APP_ENERGY_PRODUCTION.Init();
            SeedPermit.APP_ENERGY_PRODUCTION_RENEW.APP_ENERGY_PRODUCTION_RENEW.Init();
            // <BIZBIZ> CODE FOR BIZ_BIZ  <BIZBIZ>
            //SeedPermit.APP_ENERGY_PRODUCTION_RENEW_SEARCH.APP_ENERGY_PRODUCTION_RENEW_SEARCH.Init();
            APP_ENERGY_PRODUCTION_EDIT.Init();
            // <BIZBIZ> CODE FOR BIZ_BIZ  <BIZBIZ>
            //APP_ENERGY_PRODUCTION_EDIT_SEARCH.Init();
            APP_ENERGY_PRODUCTION_CANCEL.Init();
            // <BIZBIZ> CODE FOR BIZ_BIZ  <BIZBIZ>
            //APP_ENERGY_PRODUCTION_CANCEL_SEARCH.Init();
        }

        [TestMethod]
        public void InitPermit39()
        {
            SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK.APP_TRANSPORT_NON_REGULAR_TRUCK.Init();
            SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.Init();
            SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.Init();
            SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.Init();
        }

        [TestMethod]
        public void InitPermit40()
        {
            APP_SECURITIES_BUSINESS.Init();
        }

        [TestMethod]
        public void InitPermit43()
        {
            SeedPermit.APP_ACCOUNTING_SERVICE.APP_ACCOUNTING_SERVICE.Init();
            SeedPermit.APP_ACCOUNTING_DETAIL.APP_ACCOUNTING_DETAIL.Init();
            // <BIZBIZ> CODE FOR BIZ_BIZ  <BIZBIZ>
            //SeedPermit.APP_ACCOUNTING_SERVICE_RENEW_SEARCH.APP_ACCOUNTING_SERVICE_RENEW_SEARCH.Init();
            SeedPermit.APP_ACCOUNTING_SERVICE_RENEW.APP_ACCOUNTING_SERVICE_RENEW.Init();

            // <BIZBIZ> CODE FOR BIZ_BIZ  <BIZBIZ>
            //SeedPermit.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2_SEARCH.Init();
            SeedPermit.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.Init();

            // <BIZBIZ> CODE FOR BIZ_BIZ  <BIZBIZ>
            //SeedPermit.APP_ACCOUNTING_SERVICE_EDIT_SEARCH.APP_ACCOUNTING_SERVICE_EDIT_SEARCH.Init();
            SeedPermit.APP_ACCOUNTING_SERVICE_EDIT.APP_ACCOUNTING_SERVICE_EDIT.Init();

            // <BIZBIZ> CODE FOR BIZ_BIZ  <BIZBIZ>
            //SeedPermit.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_SEARCH.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2_SEARCH.Init();
            SeedPermit.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.Init();

            // <BIZBIZ> CODE FOR BIZ_BIZ  <BIZBIZ>
            //SeedPermit.APP_ACCOUNTING_SERVICE_CANCEL_SEARCH.APP_ACCOUNTING_SERVICE_CANCEL_SEARCH.Init();
            SeedPermit.APP_ACCOUNTING_SERVICE_CANCEL.APP_ACCOUNTING_SERVICE_CANCEL.Init();
        }

        [TestMethod]
        public void InitPermit46()
        {
            APP_TOURISM_BUSINESS.Init();
        }
        [TestMethod]

        public void InitPermit42()
        {
            SeedPermit.APP_CLINIC.APP_CLINIC.Init();
            SeedPermit.APP_CLINIC_PLAN.APP_CLINIC_PLAN.Init();
            SeedPermit.APP_CLINIC_LICENSE.APP_CLINIC_LICENSE.Init();
            SeedPermit.APP_CLINIC_OWNED.APP_CLINIC_OWNED.Init();
            SeedPermit.APP_CLINIC_RENEW.APP_CLINIC_RENEW.Init();
            SeedPermit.APP_CLINIC_RENEW_SEARCH.APP_CLINIC_RENEW_SEARCH.Init();
            SeedPermit.APP_CLINIC_EDIT.APP_CLINIC_EDIT.Init();
            SeedPermit.APP_CLINIC_EDIT_SEARCH.APP_CLINIC_EDIT_SEARCH.Init();
            SeedPermit.APP_CLINIC_CANCEL.APP_CLINIC_CANCEL.Init();
            SeedPermit.APP_CLINIC_CANCEL_SEARCH.APP_CLINIC_CANCEL_SEARCH.Init();
        }
        
        [TestMethod]
        public void InitPermit47()
        {
            SeedPermit.APP_HOSPITAL_PERMISSION.APP_HOSPITAL_PERMISSION.Init();
            SeedPermit.APP_HOSPITAL_PERMISSION_OWNER.APP_HOSPITAL_PERMISSION_OWNER.Init();
            SeedPermit.APP_HOSPITAL_PERMISSION_RENEW.APP_HOSPITAL_PERMISSION_RENEW.Init();
            SeedPermit.APP_HOSPITAL_PERMISSION_RENEW_SEARCH.APP_HOSPITAL_PERMISSION_RENEW_SEARCH.Init();
            SeedPermit.APP_HOSPITAL_PERMISSION_EDIT.APP_HOSPITAL_PERMISSION_EDIT.Init();
            SeedPermit.APP_HOSPITAL_PERMISSION_EDIT_SEARCH.APP_HOSPITAL_PERMISSION_EDIT_SEARCH.Init();
            SeedPermit.APP_HOSPITAL_PERMISSION_CANCEL.APP_HOSPITAL_PERMISSION_CANCEL.Init();
            SeedPermit.APP_HOSPITAL_PERMISSION_CANCEL_SEARCH.APP_HOSPITAL_PERMISSION_CANCEL_SEARCH.Init();
        }
        
        [TestMethod]
        public void InitPermit49()
        {

            APP_ORGANIC_PLANT_PRODUCTION_RENEW_SEARCH.Init();
            APP_ORGANIC_PLANT_PRODUCTION_RENEW.Init();
            APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH.Init();
            APP_ORGANIC_PLANT_PRODUCTION_EDIT.Init();
            APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH.Init();
            APP_ORGANIC_PLANT_PRODUCTION_CANCEL.Init();
        }
        
        [TestMethod]
        public void InitHoliday()
        {
            Holiday.Init();
        }

        [TestMethod]
        public void InitQuiz()
        {
            QuizGroup.Init();
            QuizSectionRow.Init();
            QuizAppMapping.Init();
            SmartQuiz.Init();
        }

        [TestMethod]
        public void InitAPP_HEALTH()
        {
            APP_HEALTH_RENEW_SEARCH_GROUP.Init();
            APP_HEALTH_CANCEL_SEARCH_GROUP.Init();
            APP_HEALTH_EDIT_SEARCH_GROUP.Init();
        }

        [TestMethod]
        public void InitAPP_ORGANIC_NEW()
        {
            APP_ORGANIC_NEW_SECTION_GROUP.AddSectionGroup();
        }

        public void InitSEC()
        {
            SeedPermit.APP_SEC_NEW_A.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_NEW_B.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_NEW_C.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_NEW_D.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_NEW_E.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_NEW_F.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_NEW_G.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_EDIT.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_CANCEL_A.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_CANCEL_B.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_CANCEL_C.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_CANCEL_D.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_CANCEL_E.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_CANCEL_F.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SEC_CANCEL_G.SectionGroup.CreateSecionGroup();
        }

        public void InitSoftWareHouse()
        {
            SeedPermit.APP_SOFTWARE_HOUSE_NEW.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_SOFTWARE_HOUSE_EDIT.SectionGroup.CreateSecionGroup();
        }

        private void Init_SPA() 
        {
            SeedPermit.APP_SPA_FEE_PER_YEAR.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_CLINIC_NOT_ONE_NIGHT_STAND.SectionGroup.CreateSecionGroup();
            SeedPermit.APP_CLINIC_OVER_NIGHT.SectionGroup.CreateSecionGroup();
        }

        [TestMethod]
        public void CleanAndInitQuiz()
        {
            MongoFactory.GetQuizGroupCollection()
                .DeleteMany<QuizGroup>(x => true);
            MongoFactory.GetQuizSectionRowCollection()
                .DeleteMany<QuizSectionRow>(x => true);
            MongoFactory.GetQuizAppMappingCollection()
                .DeleteMany<QuizAppMapping>(x => true);
            MongoFactory.GetSmartQuizCollection()
                .DeleteMany<SmartQuiz>(x => true);

            QuizGroup.Init();
            QuizSectionRow.Init();
            QuizAppMapping.Init();
            SmartQuiz.Init();
        }

        [TestMethod]
        public void SectionsByIdentityType()
        {
            Assert.IsTrue(FormSection.GetSections("INFORMATION", null).Length == 4, "ดึงข้อมูล Form Section โดยไม่กำหนด Identity Type");
            Assert.IsTrue(FormSection.GetSections("INFORMATION", null, UserTypeEnum.Citizen).Length == 2, "ดึงข้อมูล Form Section โดยกำหนด Identity Type เป็น Citizen");
            Assert.IsTrue(FormSection.GetSections("INFORMATION", null, UserTypeEnum.Juristic).Length == 4, "ดึงข้อมูล Form Section โดยกำหนด Identity Type เป็น Juristic");
        }

        [TestMethod]
        public void SectionRowsByIdentityType()
        {
            // All Types
            FormSectionRow[] rows = FormSectionRow.GetSectionRows("GENERAL_INFORMATION");
            Assert.IsTrue(rows.Length == 3, "All Types - มีข้อมูลทั้งหมด 3 แถว");
            Assert.IsTrue(rows[0].Controls.Count() == 4, "All Types 1 แถว - แถวที่ 1 ต้องมี 4 คอนโทรน");
            Assert.IsTrue(rows[1].Controls.Count() == 4, "All Types 2 แถว - แถวที่ 2 ต้องมี 4 คอนโทรน");
            Assert.IsTrue(rows[2].Controls.Count() == 2, "All Types 3 แถว - แถวที่ 3 ต้องมี 2 คอนโทรน");

            // Citizen Type
            rows = FormSectionRow.GetSectionRows("GENERAL_INFORMATION", null, UserTypeEnum.Citizen);
            Assert.IsTrue(rows.Length == 2, "Citizen Type - มีข้อมูลทั้งหมด 2 แถว");
            Assert.IsTrue(rows[0].Controls.Count() == 2, "Citizen Type 1 แถว - แถวที่ 1 ต้องมี 2 คอนโทรน");
            Assert.IsTrue(rows[1].Controls.Count() == 2, "Citizen Type 2 แถว - แถวที่ 2 ต้องมี 2 คอนโทรน");

            // Juristic Type
            rows = FormSectionRow.GetSectionRows("GENERAL_INFORMATION", null, UserTypeEnum.Juristic);
            Assert.IsTrue(rows.Length == 3, "Juristic Type - มีข้อมูลทั้งหมด 3 แถว");
            Assert.IsTrue(rows[0].Controls.Count() == 2, "Juristic Type 1 แถว - แถวที่ 1 ต้องมี 2 คอนโทรน");
            Assert.IsTrue(rows[1].Controls.Count() == 2, "Juristic Type 2 แถว - แถวที่ 2 ต้องมี 2 คอนโทรน");
            Assert.IsTrue(rows[2].Controls.Count() == 2, "Juristic Type 3 แถว - แถวที่ 3 ต้องมี 2 คอนโทรน");
        }

        [TestMethod]
        public void InitGroupSectionRow()
        {
            FormSectionGroup.Init();
            FormSection.Init();
            FormSectionRow.Init();
        }

        [TestMethod]
        public void Clean_Init_GroupSectionRow()
        {
            MongoFactory.GetFormSectionGroupCollection().DeleteMany<FormSectionGroup>(x => true);
            MongoFactory.GetFormSectionCollection().DeleteMany<FormSection>(x => true);
            MongoFactory.GetFormSectionRowCollection().DeleteMany<FormSectionRow>(x => true);

            FormSectionGroup.Init();
            FormSection.Init();
            FormSectionRow.Init();
        }

        [TestMethod]
        public void InitSection()
        {
            FormSection.Init();
        }

        [TestMethod]
        public void UpdateArea()
        {
            // อย่าลืมแก้ app id ก่อน run เพราะ id ตัวจริงกับ test ไม่ตรงกัน
            ApplicationRequestEntity.UpdateApplicationRequsetArea();
        }

        [TestMethod]
        public void FixWrongStatus()
        {
            // แก้ไข status
            ApplicationRequestEntity.FixWrongStatus("PAID_FREE_CREATING_LICENSE", "PAID_FEE_CREATING_LICENSE");
        }

        [TestMethod]
        public void FixWrongApplicationID()
        {
            // แก้ไข status
            ApplicationRequestEntity.FixWrongApplicationID(12,59);
            ApplicationRequestEntity.FixWrongApplicationID(13, 60);

        }


        private void logInit(InitLog log)
        {
            log.Data.Add("TOTAL_FORM_SECTION_GROUP", "" + MongoFactory.GetFormSectionGroupCollection().AsQueryable().Count());
            log.Data.Add("TOTAL_FORM_SECTION", "" + MongoFactory.GetFormSectionCollection().AsQueryable().Count());
            log.Data.Add("TOTAL_FORM_SECTION_ROW", "" + MongoFactory.GetFormSectionRowCollection().AsQueryable().Count());
            log.Data.Add("TOTAL_FILE_GROUP", "" + MongoFactory.GetFileGroupCollection().AsQueryable().Count());
            log.Data.Add("TOTAL_SINGLE_FORM_APP_FILE", "" + MongoFactory.GetSingleFormAppFileCollection().AsQueryable().Count());
            log.Data.Add("TOTAL_SINGLE_FORM_FILE_FILE", "" + MongoFactory.GetSingleFormFileListCollection().AsQueryable().Count());
            log.Data.Add("TOTAL_SMART_QUIZ", "" + MongoFactory.GetSmartQuizCollection().AsQueryable().Count());

            log.FinishDeployDate = DateTime.Now;
            log.Data.Add("FINISH_DEPLOY_DATE", log.FinishDeployDate.ToString("yyyy-MM-dd HH:mm:ss"));

            MongoFactory.GetInitLogCollection().Update(log);
        }

        private void archiveFormConfig(int revisionCode)
        {
            // FormConfig: Copy all current FormSectionXXX -> FormSectionXXXRevision
            if (MongoFactory.GetFormSectionGroupCollection().AsQueryable().Any())
            {
                MongoFactory.GetFormSectionGroupRevisionCollection().InsertMany(MongoFactory.GetFormSectionGroupCollection().AsQueryable().ToList());
                MongoFactory.GetFormSectionRevisionCollection().InsertMany(MongoFactory.GetFormSectionCollection().AsQueryable().ToList());
                MongoFactory.GetFormSectionRowRevisionCollection().InsertMany(MongoFactory.GetFormSectionRowCollection().AsQueryable().ToList());

                // Update archive date of this revision
                var cfg = MongoFactory.GetFormConfigRevisionCollection().Find(o => o.RevisionCode == revisionCode).FirstOrDefault();
                if (cfg != null)
                {
                    cfg.ArchivedDate = DateTime.Now;
                    MongoFactory.GetFormConfigRevisionCollection().Update(cfg);
                }
                else if (cfg == null && revisionCode == 0)
                {
                    cfg = new FormConfigRevision();
                    cfg.RevisionCode = revisionCode;
                    cfg.RevisionName = new FormSectionGroup().RevisionName;
                    cfg.Description = "All previous application requests will be using form config of this revision.";
                    cfg.EffectiveDate = DateTime.Now;
                    cfg.ArchivedDate = DateTime.Now;

                    MongoFactory.GetFormConfigRevisionCollection().InsertOne(cfg);
                }
            }
        }

        private void assignRevision(int revisionCode, string revisionName, string revisionDesc)
        {
            // Create new revison record if not exists
            var cfg = MongoFactory.GetFormConfigRevisionCollection().Find(o => o.RevisionCode == revisionCode).FirstOrDefault();
            if (cfg == null)
            {
                cfg = new FormConfigRevision();
                cfg.RevisionCode = revisionCode;
                cfg.RevisionName = revisionName;
                cfg.Description = revisionDesc;
                cfg.EffectiveDate = DateTime.Now;
                cfg.ArchivedDate = null;

                MongoFactory.GetFormConfigRevisionCollection().InsertOne(cfg);
            }

            cfg.LastDeployedDate = DateTime.Now;
            MongoFactory.GetFormConfigRevisionCollection().Update(cfg);

            var groupUpdateBuilder = new UpdateDefinitionBuilder<FormSectionGroup>();
            MongoFactory.GetFormSectionGroupCollection().UpdateMany(
                x => true,
                groupUpdateBuilder.Set(x => x.RevisionCode, revisionCode).Set(x => x.RevisionName, revisionName)
            );
            var sectionUpdateBuilder = new UpdateDefinitionBuilder<FormSection>();
            MongoFactory.GetFormSectionCollection().UpdateMany(
                x => true,
                sectionUpdateBuilder.Set(x => x.RevisionCode, revisionCode).Set(x => x.RevisionName, revisionName)
            );
            var rowUpdateBuilder = new UpdateDefinitionBuilder<FormSectionRow>();
            MongoFactory.GetFormSectionRowCollection().UpdateMany(
                x => true,
                rowUpdateBuilder.Set(x => x.RevisionCode, revisionCode).Set(x => x.RevisionName, revisionName)
            );
        }
        
    }
}
