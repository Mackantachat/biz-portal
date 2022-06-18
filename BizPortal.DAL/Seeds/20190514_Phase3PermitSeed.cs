using BizPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.Seeds
{
    class _20190514_Phase3PermitSeed
    {
        private static void createApplication(ApplicationDbContext context, Application application, string translateName, ApplicationUser user)
        {
            application.MultipleRequestSupport = true;
            application.SingleFormEnabled = true;
            application.CreatedUserID = user.Id;
            application.CreatedDate = DateTime.Now;
            application.UpdatedUserID = user.Id;
            application.UpdatedDate = DateTime.Now;

            context.Applications.Add(application);
            context.SaveChanges(false);

            Application newApp = context.Applications.Where(x => x.ApplicationSysName == application.ApplicationSysName).FirstOrDefault();
            if (newApp != null)
            {
                int appID = newApp.ApplicationID;
                var AppTranslate = new List<ApplicationTranslation>()
                {
                   new ApplicationTranslation
                   {
                        ApplicationID = appID,
                        ApplicationName = translateName,
                        ApplicationDetail = "<p><br></p>",
                        LanguageID = 1,
                        ApprovedMailMessage = "<p><br></p>",
                        RejectedMailMessage = "<p><br></p>",
                        SubmitMailMessage = "<p><br></p>",
                    },
                    new ApplicationTranslation
                    {
                        ApplicationID = appID,
                        ApplicationName = translateName + " (en)",
                        ApplicationDetail = "<p><br></p>",
                        LanguageID = 2,
                        ApprovedMailMessage = "<p><br></p>",
                        RejectedMailMessage = "<p><br></p>",
                        SubmitMailMessage = "<p><br></p>",
                    },
                };
                context.ApplicationTranslations.AddRange(AppTranslate);
                context.SaveChanges(false);
            }
        }

        private static void updateApplication(ApplicationDbContext context, Application application, string translateName, ApplicationUser user)
        {
            application.UpdatedUserID = user.Id;
            application.UpdatedDate = DateTime.Now;
            context.SaveChanges(false);

            List<ApplicationTranslation> appTranslation = context.ApplicationTranslations.Where(x => x.ApplicationID == application.ApplicationID).ToList();
            if (appTranslation.Count > 0)
            {
                foreach (ApplicationTranslation appTran in appTranslation)
                {
                    if (appTran.LanguageID == 1)
                    {
                        appTran.ApplicationName = translateName;
                    }
                    else if (appTran.LanguageID == 2)
                    {
                        appTran.ApplicationName = translateName + " (en)";
                    }

                }
                context.SaveChanges(false);
            }
            else
            {
                var AppTranslate = new List<ApplicationTranslation>()
                {
                   new ApplicationTranslation
                   {
                        ApplicationID = application.ApplicationID,
                        ApplicationName = translateName,
                        ApplicationDetail = "<p><br></p>",
                        LanguageID = 1,
                        ApprovedMailMessage = "<p><br></p>",
                        RejectedMailMessage = "<p><br></p>",
                        SubmitMailMessage = "<p><br></p>",
                    },
                    new ApplicationTranslation
                    {
                        ApplicationID = application.ApplicationID,
                        ApplicationName = translateName + " (en)",
                        ApplicationDetail = "<p><br></p>",
                        LanguageID = 2,
                        ApprovedMailMessage = "<p><br></p>",
                        RejectedMailMessage = "<p><br></p>",
                        SubmitMailMessage = "<p><br></p>",
                    },
                };
                context.ApplicationTranslations.AddRange(AppTranslate);
                context.SaveChanges(false);
            }
        }

        private class ApplicationSysName
        {
            public const string APP_HOSPITAL = "APP_HOSPITAL";

            #region [ 36 ] APP LAW OFFICE
            public const string APP_LAW_OFFICE = "APP_LAW_OFFICE";
            public const string APP_LAW_OFFICE_EDIT = "APP_LAW_OFFICE_EDIT";
            public const string APP_LAW_OFFICE_CANCEL = "APP_LAW_OFFICE_CANCEL";
            #endregion

            #region [ 37 ] APP ELECTRONIC COMMERCIAL
            public const string APP_ELECTRONIC_COMMERCIAL = "APP_ELECTRONIC_COMMERCIAL";
            public const string APP_ELECTRONIC_COMMERCIAL_EDIT = "APP_ELECTRONIC_COMMERCIAL_EDIT";
            public const string APP_ELECTRONIC_COMMERCIAL_CANCEL = "APP_ELECTRONIC_COMMERCIAL_CANCEL";
            #endregion

            #region [ 38 ] APP ENERGY PRODUCTION
            public const string APP_ENERGY_PRODUCTION = "APP_ENERGY_PRODUCTION";
            public const string APP_ENERGY_PRODUCTION_RENEW = "APP_ENERGY_PRODUCTION_RENEW";
            public const string APP_ENERGY_PRODUCTION_EDIT = "APP_ENERGY_PRODUCTION_EDIT";
            public const string APP_ENERGY_PRODUCTION_CANCEL = "APP_ENERGY_PRODUCTION_CANCEL";
            #endregion

            #region [ 39 ] APP TRANSPORT NON REGULAR TRUCK
            public const string APP_TRANSPORT_NON_REGULAR_TRUCK = "APP_TRANSPORT_NON_REGULAR_TRUCK";
            public const string APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW";
            public const string APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT";
            public const string APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL = "APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL";
            #endregion

            #region [ 40 ] APP SECURITIES BUSINESS
            public const string APP_SECURITIES_BUSINESS = "APP_SECURITIES_BUSINESS";
            public const string APP_SECURITIES_BUSINESS_RENEW = "APP_SECURITIES_BUSINESS_RENEW";
            public const string APP_SECURITIES_BUSINESS_EDIT = "APP_SECURITIES_BUSINESS_EDIT";
            public const string APP_SECURITIES_BUSINESS_CANCEL = "APP_SECURITIES_BUSINESS_CANCEL";
            #endregion

            #region [ 43 ] APP ACCOUNTING SERVICE
            public const string APP_ACCOUNTING_SERVICE = "APP_ACCOUNTING_SERVICE";
            public const string APP_ACCOUNTING_SERVICE_RENEW = "APP_ACCOUNTING_SERVICE_RENEW";
            public const string APP_ACCOUNTING_SERVICE_RENEW_TYPE_2 = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2";
            public const string APP_ACCOUNTING_SERVICE_EDIT = "APP_ACCOUNTING_SERVICE_EDIT";
            public const string APP_ACCOUNTING_SERVICE_EDIT_TYPE_2 = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2";
            public const string APP_ACCOUNTING_SERVICE_CANCEL = "APP_ACCOUNTING_SERVICE_CANCEL";
            #endregion

            #region [ 45 ] APP_FACTORY_CLASS_2
            public const string APP_FACTORY_CLASS_2_NEW = "APP_FACTORY_CLASS_2_NEW"; //ใบรับแจ้งการประกอบกิจการโรงงานจำพวกที่ 2
            public const string APP_FACTORY_CLASS_2_EDIT = "APP_FACTORY_CLASS_2_EDIT"; //ขอแก้ไขการแจ้งการประกอบกิจการโรงงานจำพวกที่ 2
            public const string APP_FACTORY_CLASS_2_CANCEL = "APP_FACTORY_CLASS_2_CANCEL"; //การแจ้งยกเลิกการประกอบกิจการโรงงานจำพวกที่ 2
            #endregion

            #region [ 46 ] APP TOURISM BUSINESS
            public const string APP_TOURISM_BUSINESS = "APP_TOURISM_BUSINESS";
            public const string APP_TOURISM_BUSINESS_RENEW = "APP_TOURISM_BUSINESS_RENEW";
            public const string APP_TOURISM_BUSINESS_EDIT = "APP_TOURISM_BUSINESS_EDIT";
            public const string APP_TOURISM_BUSINESS_CANCEL = "APP_TOURISM_BUSINESS_CANCEL";
            #endregion

            #region DANGER APP
            public const string APP_36 = "APP_FRT_NEW_001";
            public const string APP_37 = "APP_FRT_RENEW_001";
            public const string APP_38 = "APP_FRT_EDIT_001";
            public const string APP_39 = "APP_FRT_CANCEL_001";
            #endregion

            #region [ 41 ] APP SOFTWARE HOUSE
            public const string APP_SOFTWARE_HOUSE_NEW = "APP_SOFTWARE_HOUSE_NEW";
            public const string APP_SOFTWARE_HOUSE_RENEW = "APP_SOFTWARE_HOUSE_RENEW";
            public const string APP_SOFTWARE_HOUSE_EDIT = "APP_SOFTWARE_HOUSE_EDIT";
            #endregion

            #region [ 42 ] APP CLINIC
            public const string APP_CLINIC = "APP_CLINIC";
            public const string APP_CLINIC_RENEW = "APP_CLINIC_RENEW";
            public const string APP_CLINIC_EDIT = "APP_CLINIC_EDIT";
            public const string APP_CLINIC_CANCEL = "APP_CLINIC_CANCEL";
            #endregion

            #region [ 47 ] APP HOSPITAL PERMISSION
            public const string APP_HOSPITAL_PERMISSION = "APP_HOSPITAL_PERMISSION";
            public const string APP_HOSPITAL_PERMISSION_RENEW = "APP_HOSPITAL_PERMISSION_RENEW";
            public const string APP_HOSPITAL_PERMISSION_EDIT = "APP_HOSPITAL_PERMISSION_EDIT";
            public const string APP_HOSPITAL_PERMISSION_CANCEL = "APP_HOSPITAL_PERMISSION_CANCEL";
            #endregion

            #region [ 48 ] APP_ENERGY_PRODUCTION_NOT_PERMIT
            public const string APP_ENERGY_PRODUCTION_NOT_PERMIT = "APP_ENERGY_PRODUCTION_NOT_PERMIT";
            #endregion

            #region [ 49 ] APP_ORGANIC_PLANT_PRODUCTION
            public const string APP_ORGANIC_PLANT_PRODUCTION_NEW = "APP_ORGANIC_PLANT_PRODUCTION_NEW";
            public const string APP_ORGANIC_PLANT_PRODUCTION_RENEW = "APP_ORGANIC_PLANT_PRODUCTION_RENEW";
            public const string APP_ORGANIC_PLANT_PRODUCTION_EDIT = "APP_ORGANIC_PLANT_PRODUCTION_EDIT";
            public const string APP_ORGANIC_PLANT_PRODUCTION_CANCEL = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL";
            #endregion
            public const string APP_FACTORY_TYPE2 = "APP_FACTORY_TYPE2";

            public const string APP_HEALTH_RENEW = "ขอต่ออายุใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา";
            public const string APP_HEALTH_EDIT = "ขอแก้ไขใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา";
            public const string APP_HEALTH_CANCEL = "ขอยกเลิกใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา";

            #region [ SEC ]

            public const string APP_SEC_NEW_A = "APP_SEC_NEW_A";
            public const string APP_SEC_NEW_B = "APP_SEC_NEW_B";
            public const string APP_SEC_NEW_C = "APP_SEC_NEW_C";
            public const string APP_SEC_NEW_D = "APP_SEC_NEW_D";
            public const string APP_SEC_NEW_E = "APP_SEC_NEW_E";
            public const string APP_SEC_NEW_F = "APP_SEC_NEW_F";
            public const string APP_SEC_NEW_G = "APP_SEC_NEW_G";

            public const string APP_SEC_EDIT = "APP_SEC_EDIT";

            public const string APP_SEC_CANCEL_A = "APP_SEC_CANCEL_A";
            public const string APP_SEC_CANCEL_B = "APP_SEC_CANCEL_B";
            public const string APP_SEC_CANCEL_C = "APP_SEC_CANCEL_C";
            public const string APP_SEC_CANCEL_D = "APP_SEC_CANCEL_D";
            public const string APP_SEC_CANCEL_E = "APP_SEC_CANCEL_E";
            public const string APP_SEC_CANCEL_F = "APP_SEC_CANCEL_F";
            public const string APP_SEC_CANCEL_G = "APP_SEC_CANCEL_G";

            #endregion

            #region SSO Permits

            /// <summary>
            /// หนังสือรับรองประกอบการนำเข้าเครื่องมือแพทย์
            /// </summary>
            public const string APP_IMPORT_MEDICAL_EQUIPMENT = "APP_IMPORT_MEDICAL_EQUIPMENT";

            /// <summary>
            /// ขออนุญาตจัดตั้งโรงเรียนนอกระบบ
            /// </summary>
            public const string APP_EDUCATION_PRIVATE_SCHOOL = "APP_EDUCATION_PRIVATE_SCHOOL";

            /// <summary>
            /// หนังสือแจ้งผลการรับฟังความคิดเห็นของประชาชนในการพิจารณาเกี่ยวกับโรงงานจำพวกที่ 3
            /// </summary>
            public const string APP_FACTORY_TYPE3 = "APP_FACTORY_TYPE3";

            /// <summary>
            /// ใบอนุญาตประกอบกิจการโรงงานจำพวกที่ 3
            /// </summary>
            public const string APP_FACTORY_CLASS_3_NEW = "APP_FACTORY_CLASS_3_NEW";

            /// <summary>
            /// การแจ้งเริ่มประกอบกิจการโรงงานจำพวกที่ 3
            /// </summary>
            public const string APP_FACTORY_CLASS_3_START_NEW = "APP_FACTORY_CLASS_3_START_NEW";


            /// <summary>
            /// ใบอนุญาตนำเข้าอาหาร
            /// </summary>
            public const string APP_IMPORT_FOOD = "APP_IMPORT_FOOD";

            /// <summary>
            /// ใบอนุญาตผลิตอาหาร
            /// </summary>
            public const string APP_FOOD_PRODUCTION = "APP_FOOD_PRODUCTION";

            /// <summary>
            /// ใบอนุญาตโฆษณาอาหาร (ฆอ.2)
            /// </summary>
            public const string APP_FOOD_ADVERTISEMENT = "APP_FOOD_ADVERTISEMENT";

            /// <summary>
            /// หนังสือรับรองผลิตภัณฑ์อาหาร (Certificate of Free Sale)
            /// </summary>
            public const string APP_FOOD_CERTIFICATE = "APP_FOOD_CERTIFICATE";

            #endregion

            public const string APP_SPA_FEE_PER_YEAR = "APP_SPA_FEE_PER_YEAR"; // งานบริการชำระค่าธรรมเนียมการประกอบกิจการสถานประกอบการเพื่อสุขภาพรายปี (สปา)
            public const string APP_CLINIC_NOT_ONE_NIGHT_STAND = "APP_CLINIC_NOT_ONE_NIGHT_STAND"; // งานบริการชำระค่าธรรมเนียมการประกอบกิจการสถานพยาบาล (แบบไม่รับผู้ป่วยค้างคืน)
            public const string APP_CLINIC_OVER_NIGHT = "APP_CLINIC_OVER_NIGHT";

            public const string APP_CLINIC_BUSINESS = "APP_CLINIC_BUSINESS"; // ขออนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)
            public const string APP_CLINIC_OPERATION = "APP_CLINIC_OPERATION"; // ขอรับใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)

            public const string APP_HOSPITAL_BUSINESS = "APP_HOSPITAL_BUSINESS"; // งานบริการขออนุญาตให้ประกอบกิจการสถานพยาบาล (โรงพยาบาล)
            public const string APP_HOSPITAL_OPERATION = "APP_HOSPITAL_OPERATION"; // งานบริการขอรับใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)

            public const string APP_CLINIC_BUSINESS_RENEW = "APP_CLINIC_BUSINESS_RENEW"; // ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)
            public const string APP_CLINIC_OPERATION_RENEW = "APP_CLINIC_OPERATION_RENEW"; // ขอต่ออายุใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)

            public const string APP_HOSPITAL_BUSINESS_RENEW = "APP_HOSPITAL_BUSINESS_RENEW"; // ใบอนุญาตให้ประกอบกิจการสถานพยาบาล (ประเภทที่รับผู้ป่วยค้างคืน)
            public const string APP_HOSPITAL_OPERATION_RENEW = "APP_HOSPITAL_OPERATION_RENEW"; // ใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่รับผู้ป่วยค้างคืน)

            //public const string APP_HOSPITAL_BUSINESS_EDIT = "APP_HOSPITAL_BUSINESS_EDIT"; // ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาลและใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน)
            //public const string APP_HOSPITAL_OPERATION_EDIT = "APP_HOSPITAL_OPERATION_EDIT"; // ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาลประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน
            //public const string APP_HOSPITAL_OPERATION_EDIT_B = "APP_HOSPITAL_OPERATION_EDIT_B"; // ขอแก้ไขใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน)

            //public const string APP_CLINIC_BUSINESS_EDIT = "APP_CLINIC_BUSINESS_EDIT"; // ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)
            //public const string APP_CLINIC_OPERATION_EDIT = "APP_CLINIC_OPERATION_EDIT"; // ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)
            //public const string APP_CLINIC_OPERATION_EDIT_B = "APP_CLINIC_OPERATION_EDIT_B"; // ขอแก้ไขใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)

            public const string APP_HOSPITAL_BUSINESS_EDIT = "APP_HOSPITAL_BUSINESS_EDIT"; // ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)
            public const string APP_HOSPITAL_OPERATION_EDIT = "APP_HOSPITAL_OPERATION_EDIT"; // ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)
            public const string APP_HOSPITAL_OPERATION_EDIT_B = "APP_HOSPITAL_OPERATION_EDIT_B"; // ขอแก้ไขใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน)

            public const string APP_CLINIC_BUSINESS_EDIT = "APP_CLINIC_BUSINESS_EDIT"; // ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาลและใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน)
            public const string APP_CLINIC_OPERATION_EDIT = "APP_CLINIC_OPERATION_EDIT"; // ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาลประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน
            public const string APP_CLINIC_OPERATION_EDIT_B = "APP_CLINIC_OPERATION_EDIT_B"; // ขอแก้ไขใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)

            public const string APP_HEALTH = "ขอใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ";
        }

        private class CostType
        {
            public const string Fixed = "Fixed";
            public const string Range = "Range";
            public const string StartAt = "StartAt";
        }

        const string APP_HOOK_NEW_PERMIT = "BizPortal.AppsHook.NEW_PERMIT";
        const string REMARK_BKK = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น ค่าธรรมเนียมข้างต้นยังไม่รวมค่าดำเนินการ 250 บาท</span>";
        const string REMARK_BKK_CANCEL = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น กรณีที่มียอดชำระค่าธรรมเนียมที่ค้างอยู่ให้ชำระค่าธนนมเนียมให้เรียบร้อยก่อนขอยกเลิกกิจการ</span>";
        const string REMARK_BKK_AND_2_PROVINCE = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานคร ฉะเชิงเทราและราชบุรี เท่านั้น</span>";

        public static void Seed(BizPortal.DAL.ApplicationDbContext context, ApplicationUser creater)
        {

            #region APP 35 HOSPITAL

            Application APP_HOSPITAL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HOSPITAL).FirstOrDefault();

            if (APP_HOSPITAL == null)
            {
                APP_HOSPITAL = new Application();
                APP_HOSPITAL.ApplicationSysName = ApplicationSysName.APP_HOSPITAL;
                APP_HOSPITAL.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalNewAppHook";
                APP_HOSPITAL.OrgCode = "19007000";
                APP_HOSPITAL.LogoFileID = null;
                APP_HOSPITAL.HandbookUrl = "https://www.info.go.th/#!/th/search/7470/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/7470/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL.OperatingDays = 81;
                APP_HOSPITAL.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL.OperatingDays2 = null;
                APP_HOSPITAL.OperatingCost = 0;
                APP_HOSPITAL.OperatingCostType = CostType.Fixed;
                APP_HOSPITAL.OperatingCost2 = null;
                APP_HOSPITAL.CitizenOperatingDays = 81;
                APP_HOSPITAL.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL.CitizenOperatingDays2 = null;
                APP_HOSPITAL.CitizenOperatingCost = 0;
                APP_HOSPITAL.CitizenOperatingCostType = CostType.Fixed;
                APP_HOSPITAL.CitizenOperatingCost2 = null;
                APP_HOSPITAL.ShowRemark = true;
                APP_HOSPITAL.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HOSPITAL.CitizenShowRemark = true;
                APP_HOSPITAL.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HOSPITAL.SingleFormEnabled = true;

                string TranslateName = "ขออนุมัติแผนงานการจัดตั้งสถานพยาบาล(โรงพยาบาล)";

                createApplication(context, APP_HOSPITAL, TranslateName, creater);
            }
            else
            {
                APP_HOSPITAL.ApplicationSysName = ApplicationSysName.APP_HOSPITAL;
                APP_HOSPITAL.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalNewAppHook";
                APP_HOSPITAL.OrgCode = "19007000";
                APP_HOSPITAL.LogoFileID = null;
                APP_HOSPITAL.HandbookUrl = "https://www.info.go.th/#!/th/search/7470/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/7470/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL.OperatingDays = 81;
                APP_HOSPITAL.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL.OperatingDays2 = null;
                APP_HOSPITAL.OperatingCost = 0;
                APP_HOSPITAL.OperatingCostType = CostType.Fixed;
                APP_HOSPITAL.OperatingCost2 = null;
                APP_HOSPITAL.CitizenOperatingDays = 81;
                APP_HOSPITAL.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL.CitizenOperatingDays2 = null;
                APP_HOSPITAL.CitizenOperatingCost = 0;
                APP_HOSPITAL.CitizenOperatingCostType = CostType.Fixed;
                APP_HOSPITAL.CitizenOperatingCost2 = null;
                APP_HOSPITAL.ShowRemark = true;
                APP_HOSPITAL.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HOSPITAL.CitizenShowRemark = true;
                APP_HOSPITAL.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HOSPITAL.SingleFormEnabled = true;
                APP_HOSPITAL.MultipleRequestSupport = true;

                string TranslateName = "ขออนุมัติแผนงานการจัดตั้งสถานพยาบาล(โรงพยาบาล)";

                updateApplication(context, APP_HOSPITAL, TranslateName, creater);
            }

            /* UPDATE HERE */
            /* APP_X.XXX = XXXXX */

            #endregion

            #region APP LAW OFFICE

            #region NEW

            Application APP_LAW_OFFICE = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_LAW_OFFICE).FirstOrDefault();

            if (APP_LAW_OFFICE == null)
            {
                APP_LAW_OFFICE = new Application();
                APP_LAW_OFFICE.ApplicationSysName = ApplicationSysName.APP_LAW_OFFICE;
                APP_LAW_OFFICE.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_LAW_OFFICE.OrgCode = "21044000";
                APP_LAW_OFFICE.LogoFileID = null;
                APP_LAW_OFFICE.HandbookUrl = "https://www.lawyerscouncil.or.th/2019/register-f";
                APP_LAW_OFFICE.CitizenHandbookUrl = "https://www.lawyerscouncil.or.th/2019/register-f";
                APP_LAW_OFFICE.OperatingDays = 15;
                APP_LAW_OFFICE.OperatingDayType = CostType.Fixed;
                APP_LAW_OFFICE.OperatingDays2 = null;
                APP_LAW_OFFICE.OperatingCost = 1200;
                APP_LAW_OFFICE.OperatingCostType = CostType.Fixed;
                APP_LAW_OFFICE.OperatingCost2 = null;
                APP_LAW_OFFICE.CitizenOperatingDays = 15;
                APP_LAW_OFFICE.CitizenOperatingDayType = CostType.Fixed;
                APP_LAW_OFFICE.CitizenOperatingDays2 = null;
                APP_LAW_OFFICE.CitizenOperatingCost = 1200;
                APP_LAW_OFFICE.CitizenOperatingCostType = CostType.Fixed;
                APP_LAW_OFFICE.CitizenOperatingCost2 = null;
                APP_LAW_OFFICE.ShowRemark = true;
                APP_LAW_OFFICE.Remark = REMARK_BKK;
                APP_LAW_OFFICE.CitizenShowRemark = true;
                APP_LAW_OFFICE.CitizenRemark = REMARK_BKK;
                APP_LAW_OFFICE.SingleFormEnabled = true;
                string TranslateName = "ขอขึ้นทะเบียนสำนักงานทนายความ";
                APP_LAW_OFFICE.SingleFormEnabled = true;
                createApplication(context, APP_LAW_OFFICE, TranslateName, creater);
            }
            else
            {
                APP_LAW_OFFICE.ApplicationSysName = ApplicationSysName.APP_LAW_OFFICE;
                APP_LAW_OFFICE.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_LAW_OFFICE.OrgCode = "21044000";
                APP_LAW_OFFICE.LogoFileID = null;
                APP_LAW_OFFICE.HandbookUrl = "https://www.lawyerscouncil.or.th/2019/register-f";
                APP_LAW_OFFICE.CitizenHandbookUrl = "https://www.lawyerscouncil.or.th/2019/register-f";
                APP_LAW_OFFICE.OperatingDays = 15;
                APP_LAW_OFFICE.OperatingDayType = CostType.Fixed;
                APP_LAW_OFFICE.OperatingDays2 = null;
                APP_LAW_OFFICE.OperatingCost = 1200;
                APP_LAW_OFFICE.OperatingCostType = CostType.Fixed;
                APP_LAW_OFFICE.OperatingCost2 = null;
                APP_LAW_OFFICE.CitizenOperatingDays = 15;
                APP_LAW_OFFICE.CitizenOperatingDayType = CostType.Fixed;
                APP_LAW_OFFICE.CitizenOperatingDays2 = null;
                APP_LAW_OFFICE.CitizenOperatingCost = 1200;
                APP_LAW_OFFICE.CitizenOperatingCostType = CostType.Fixed;
                APP_LAW_OFFICE.CitizenOperatingCost2 = null;
                APP_LAW_OFFICE.ShowRemark = true;
                APP_LAW_OFFICE.Remark = REMARK_BKK;
                APP_LAW_OFFICE.CitizenShowRemark = true;
                APP_LAW_OFFICE.CitizenRemark = REMARK_BKK;
                APP_LAW_OFFICE.SingleFormEnabled = true;
                string TranslateName = "ขอขึ้นทะเบียนสำนักงานทนายความ";
                APP_LAW_OFFICE.SingleFormEnabled = true;
                updateApplication(context, APP_LAW_OFFICE, TranslateName, creater);
            }

            #endregion

            #region EDIT

            Application APP_LAW_OFFICE_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_LAW_OFFICE_EDIT).FirstOrDefault();

            if (APP_LAW_OFFICE_EDIT == null)
            {
                APP_LAW_OFFICE_EDIT = new Application();
                APP_LAW_OFFICE_EDIT.ApplicationSysName = ApplicationSysName.APP_LAW_OFFICE_EDIT;
                APP_LAW_OFFICE_EDIT.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_LAW_OFFICE_EDIT.OrgCode = "21044000";
                APP_LAW_OFFICE_EDIT.LogoFileID = null;
                APP_LAW_OFFICE_EDIT.HandbookUrl = "https://www.lawyerscouncil.or.th/2019/register-f";
                APP_LAW_OFFICE_EDIT.CitizenHandbookUrl = "https://www.lawyerscouncil.or.th/2019/register-f";
                APP_LAW_OFFICE_EDIT.OperatingDays = 1;
                APP_LAW_OFFICE_EDIT.OperatingDayType = CostType.Fixed;
                APP_LAW_OFFICE_EDIT.OperatingDays2 = null;
                APP_LAW_OFFICE_EDIT.OperatingCost = 0;
                APP_LAW_OFFICE_EDIT.OperatingCostType = CostType.Fixed;
                APP_LAW_OFFICE_EDIT.OperatingCost2 = null;
                APP_LAW_OFFICE_EDIT.CitizenOperatingDays = 1;
                APP_LAW_OFFICE_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_LAW_OFFICE_EDIT.CitizenOperatingDays2 = null;
                APP_LAW_OFFICE_EDIT.CitizenOperatingCost = 0;
                APP_LAW_OFFICE_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_LAW_OFFICE_EDIT.CitizenOperatingCost2 = null;
                APP_LAW_OFFICE_EDIT.ShowRemark = true;
                APP_LAW_OFFICE_EDIT.Remark = REMARK_BKK;
                APP_LAW_OFFICE_EDIT.CitizenShowRemark = true;
                APP_LAW_OFFICE_EDIT.CitizenRemark = REMARK_BKK;
                APP_LAW_OFFICE_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอเปลี่ยนแปลงข้อมูลสำนักงานทนายความ";

                createApplication(context, APP_LAW_OFFICE_EDIT, TranslateName, creater);
            }
            else
            {
                APP_LAW_OFFICE_EDIT.ApplicationSysName = ApplicationSysName.APP_LAW_OFFICE_EDIT;
                APP_LAW_OFFICE_EDIT.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_LAW_OFFICE_EDIT.OrgCode = "21044000";
                APP_LAW_OFFICE_EDIT.LogoFileID = null;
                APP_LAW_OFFICE_EDIT.HandbookUrl = "https://www.lawyerscouncil.or.th/2019/register-f";
                APP_LAW_OFFICE_EDIT.CitizenHandbookUrl = "https://www.lawyerscouncil.or.th/2019/register-f";
                APP_LAW_OFFICE_EDIT.OperatingDays = 1;
                APP_LAW_OFFICE_EDIT.OperatingDayType = CostType.Fixed;
                APP_LAW_OFFICE_EDIT.OperatingDays2 = null;
                APP_LAW_OFFICE_EDIT.OperatingCost = 0;
                APP_LAW_OFFICE_EDIT.OperatingCostType = CostType.Fixed;
                APP_LAW_OFFICE_EDIT.OperatingCost2 = null;
                APP_LAW_OFFICE_EDIT.CitizenOperatingDays = 1;
                APP_LAW_OFFICE_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_LAW_OFFICE_EDIT.CitizenOperatingDays2 = null;
                APP_LAW_OFFICE_EDIT.CitizenOperatingCost = 0;
                APP_LAW_OFFICE_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_LAW_OFFICE_EDIT.CitizenOperatingCost2 = null;
                APP_LAW_OFFICE_EDIT.ShowRemark = true;
                APP_LAW_OFFICE_EDIT.Remark = REMARK_BKK;
                APP_LAW_OFFICE_EDIT.CitizenShowRemark = true;
                APP_LAW_OFFICE_EDIT.CitizenRemark = REMARK_BKK;
                APP_LAW_OFFICE_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอเปลี่ยนแปลงข้อมูลสำนักงานทนายความ";

                updateApplication(context, APP_LAW_OFFICE_EDIT, TranslateName, creater);
            }

            #endregion

            #region CANCEL

            Application APP_LAW_OFFICE_CANCEL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_LAW_OFFICE_CANCEL).FirstOrDefault();

            if (APP_LAW_OFFICE_CANCEL == null)
            {
                APP_LAW_OFFICE_CANCEL = new Application();
                APP_LAW_OFFICE_CANCEL.ApplicationSysName = ApplicationSysName.APP_LAW_OFFICE_CANCEL;
                APP_LAW_OFFICE_CANCEL.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_LAW_OFFICE_CANCEL.OrgCode = "21044000";
                APP_LAW_OFFICE_CANCEL.LogoFileID = null;
                APP_LAW_OFFICE_CANCEL.HandbookUrl = "https://www.lawyerscouncil.or.th/2019/register-f";
                APP_LAW_OFFICE_CANCEL.CitizenHandbookUrl = "https://www.lawyerscouncil.or.th/2019/register-f";
                APP_LAW_OFFICE_CANCEL.OperatingDays = 1;
                APP_LAW_OFFICE_CANCEL.OperatingDayType = CostType.Fixed;
                APP_LAW_OFFICE_CANCEL.OperatingDays2 = null;
                APP_LAW_OFFICE_CANCEL.OperatingCost = 0;
                APP_LAW_OFFICE_CANCEL.OperatingCostType = CostType.Fixed;
                APP_LAW_OFFICE_CANCEL.OperatingCost2 = null;
                APP_LAW_OFFICE_CANCEL.CitizenOperatingDays = 1;
                APP_LAW_OFFICE_CANCEL.CitizenOperatingDayType = CostType.Fixed;
                APP_LAW_OFFICE_CANCEL.CitizenOperatingDays2 = null;
                APP_LAW_OFFICE_CANCEL.CitizenOperatingCost = 0;
                APP_LAW_OFFICE_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_LAW_OFFICE_CANCEL.CitizenOperatingCost2 = null;
                APP_LAW_OFFICE_CANCEL.ShowRemark = true;
                APP_LAW_OFFICE_CANCEL.Remark = REMARK_BKK;
                APP_LAW_OFFICE_CANCEL.CitizenShowRemark = true;
                APP_LAW_OFFICE_CANCEL.CitizenRemark = REMARK_BKK;
                APP_LAW_OFFICE_CANCEL.SingleFormEnabled = true;
                string TranslateName = "ขอยกเลิกขึ้นทะเบียนสำนักงานทนายความ";

                createApplication(context, APP_LAW_OFFICE_CANCEL, TranslateName, creater);
            }
            else
            {
                APP_LAW_OFFICE_CANCEL.ApplicationSysName = ApplicationSysName.APP_LAW_OFFICE_CANCEL;
                APP_LAW_OFFICE_CANCEL.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_LAW_OFFICE_CANCEL.OrgCode = "21044000";
                APP_LAW_OFFICE_CANCEL.LogoFileID = null;
                APP_LAW_OFFICE_CANCEL.HandbookUrl = "https://www.lawyerscouncil.or.th/2019/register-f";
                APP_LAW_OFFICE_CANCEL.CitizenHandbookUrl = "https://www.lawyerscouncil.or.th/2019/register-f";
                APP_LAW_OFFICE_CANCEL.OperatingDays = 1;
                APP_LAW_OFFICE_CANCEL.OperatingDayType = CostType.Fixed;
                APP_LAW_OFFICE_CANCEL.OperatingDays2 = null;
                APP_LAW_OFFICE_CANCEL.OperatingCost = 0;
                APP_LAW_OFFICE_CANCEL.OperatingCostType = CostType.Fixed;
                APP_LAW_OFFICE_CANCEL.OperatingCost2 = null;
                APP_LAW_OFFICE_CANCEL.CitizenOperatingDays = 1;
                APP_LAW_OFFICE_CANCEL.CitizenOperatingDayType = CostType.Fixed;
                APP_LAW_OFFICE_CANCEL.CitizenOperatingDays2 = null;
                APP_LAW_OFFICE_CANCEL.CitizenOperatingCost = 0;
                APP_LAW_OFFICE_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_LAW_OFFICE_CANCEL.CitizenOperatingCost2 = null;
                APP_LAW_OFFICE_CANCEL.ShowRemark = true;
                APP_LAW_OFFICE_CANCEL.Remark = REMARK_BKK;
                APP_LAW_OFFICE_CANCEL.CitizenShowRemark = true;
                APP_LAW_OFFICE_CANCEL.CitizenRemark = REMARK_BKK;
                APP_LAW_OFFICE_CANCEL.SingleFormEnabled = true;
                string TranslateName = "ขอยกเลิกขึ้นทะเบียนสำนักงานทนายความ";

                updateApplication(context, APP_LAW_OFFICE_CANCEL, TranslateName, creater);
            }

            #endregion

            #endregion

            #region APP 37 ELECTRONIC COMMERCIAL

            #region NEW

            Application APP_ELECTRONIC_COMMERCIAL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ELECTRONIC_COMMERCIAL).FirstOrDefault();

            if (APP_ELECTRONIC_COMMERCIAL == null)
            {
                APP_ELECTRONIC_COMMERCIAL = new Application();
                APP_ELECTRONIC_COMMERCIAL.ApplicationSysName = ApplicationSysName.APP_ELECTRONIC_COMMERCIAL;
                APP_ELECTRONIC_COMMERCIAL.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_ELECTRONIC_COMMERCIAL.OrgCode = "12007000";
                APP_ELECTRONIC_COMMERCIAL.LogoFileID = null;
                APP_ELECTRONIC_COMMERCIAL.HandbookUrl = null;
                APP_ELECTRONIC_COMMERCIAL.CitizenHandbookUrl = null;
                APP_ELECTRONIC_COMMERCIAL.OperatingDays = 0;
                APP_ELECTRONIC_COMMERCIAL.OperatingDayType = CostType.Fixed;
                APP_ELECTRONIC_COMMERCIAL.OperatingDays2 = null;
                APP_ELECTRONIC_COMMERCIAL.OperatingCost = 0;
                APP_ELECTRONIC_COMMERCIAL.OperatingCostType = CostType.Fixed;
                APP_ELECTRONIC_COMMERCIAL.OperatingCost2 = null;
                APP_ELECTRONIC_COMMERCIAL.CitizenOperatingDays = 0;
                APP_ELECTRONIC_COMMERCIAL.CitizenOperatingDayType = CostType.Fixed;
                APP_ELECTRONIC_COMMERCIAL.CitizenOperatingDays2 = null;
                APP_ELECTRONIC_COMMERCIAL.CitizenOperatingCost = 0;
                APP_ELECTRONIC_COMMERCIAL.CitizenOperatingCostType = CostType.Fixed;
                APP_ELECTRONIC_COMMERCIAL.CitizenOperatingCost2 = null;
                APP_ELECTRONIC_COMMERCIAL.ShowRemark = true;
                APP_ELECTRONIC_COMMERCIAL.Remark = "<span style=\"color:red;\">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น<span>";
                APP_ELECTRONIC_COMMERCIAL.CitizenShowRemark = true;
                APP_ELECTRONIC_COMMERCIAL.CitizenRemark = "<span style=\"color:red;\">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น<span>";
                APP_ELECTRONIC_COMMERCIAL.SingleFormEnabled = true;
                APP_ELECTRONIC_COMMERCIAL.ConsumerKey = Guid.Parse("d216ffeb-9d71-4483-9ac7-e6752dbe8c1f");
                string TranslateName = "ขอจดทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์";

                createApplication(context, APP_ELECTRONIC_COMMERCIAL, TranslateName, creater);
            }
            else
            {
                APP_ELECTRONIC_COMMERCIAL.AppsHookClassName = "BizPortal.AppsHook.DBDCommerceNewAppHook";
                APP_ELECTRONIC_COMMERCIAL.OperatingDays = 1;
                APP_ELECTRONIC_COMMERCIAL.OperatingCost = 50;
                APP_ELECTRONIC_COMMERCIAL.CitizenOperatingDays = 1;
                APP_ELECTRONIC_COMMERCIAL.CitizenOperatingCost = 50;
                APP_ELECTRONIC_COMMERCIAL.ConsumerKey = Guid.Parse("d216ffeb-9d71-4483-9ac7-e6752dbe8c1f");
                APP_ELECTRONIC_COMMERCIAL.ShowRemark = true;
                APP_ELECTRONIC_COMMERCIAL.Remark = "<span style=\"color:red;\">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น<span>";
                APP_ELECTRONIC_COMMERCIAL.CitizenShowRemark = true;
                APP_ELECTRONIC_COMMERCIAL.CitizenRemark = "<span style=\"color:red;\">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น<span>";

                string TranslateName = "ขอจดทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์";

                updateApplication(context, APP_ELECTRONIC_COMMERCIAL, TranslateName, creater);
            }

            #endregion

            #region EDIT

            Application APP_ELECTRONIC_COMMERCIAL_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ELECTRONIC_COMMERCIAL_EDIT).FirstOrDefault();

            if (APP_ELECTRONIC_COMMERCIAL_EDIT == null)
            {
                APP_ELECTRONIC_COMMERCIAL_EDIT = new Application();
                APP_ELECTRONIC_COMMERCIAL_EDIT.ApplicationSysName = ApplicationSysName.APP_ELECTRONIC_COMMERCIAL_EDIT;
                APP_ELECTRONIC_COMMERCIAL_EDIT.AppsHookClassName = "BizPortal.AppsHook.DBDCommerceEditAppHook";
                APP_ELECTRONIC_COMMERCIAL_EDIT.OrgCode = "12007000";
                APP_ELECTRONIC_COMMERCIAL_EDIT.LogoFileID = null;
                APP_ELECTRONIC_COMMERCIAL_EDIT.HandbookUrl = null;
                APP_ELECTRONIC_COMMERCIAL_EDIT.CitizenHandbookUrl = null;
                APP_ELECTRONIC_COMMERCIAL_EDIT.OperatingDays = 0;
                APP_ELECTRONIC_COMMERCIAL_EDIT.OperatingDayType = CostType.Fixed;
                APP_ELECTRONIC_COMMERCIAL_EDIT.OperatingDays2 = null;
                APP_ELECTRONIC_COMMERCIAL_EDIT.OperatingCost = 0;
                APP_ELECTRONIC_COMMERCIAL_EDIT.OperatingCostType = CostType.Fixed;
                APP_ELECTRONIC_COMMERCIAL_EDIT.OperatingCost2 = null;
                APP_ELECTRONIC_COMMERCIAL_EDIT.CitizenOperatingDays = 0;
                APP_ELECTRONIC_COMMERCIAL_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_ELECTRONIC_COMMERCIAL_EDIT.CitizenOperatingDays2 = null;
                APP_ELECTRONIC_COMMERCIAL_EDIT.CitizenOperatingCost = 0;
                APP_ELECTRONIC_COMMERCIAL_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_ELECTRONIC_COMMERCIAL_EDIT.CitizenOperatingCost2 = null;
                APP_ELECTRONIC_COMMERCIAL_EDIT.ShowRemark = false;
                APP_ELECTRONIC_COMMERCIAL_EDIT.Remark = null;
                APP_ELECTRONIC_COMMERCIAL_EDIT.CitizenShowRemark = false;
                APP_ELECTRONIC_COMMERCIAL_EDIT.CitizenRemark = null;
                APP_ELECTRONIC_COMMERCIAL_EDIT.SingleFormEnabled = true;
                APP_ELECTRONIC_COMMERCIAL_EDIT.ConsumerKey = Guid.Parse("d216ffeb-9d71-4483-9ac7-e6752dbe8c1f");

                string TranslateName = "ขอแก้ไขทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์";

                createApplication(context, APP_ELECTRONIC_COMMERCIAL_EDIT, TranslateName, creater);
            }
            else
            {
                APP_ELECTRONIC_COMMERCIAL_EDIT.AppsHookClassName = "BizPortal.AppsHook.DBDCommerceEditAppHook";
                APP_ELECTRONIC_COMMERCIAL_EDIT.OperatingDays = 1;
                APP_ELECTRONIC_COMMERCIAL_EDIT.OperatingCost = 20;
                APP_ELECTRONIC_COMMERCIAL_EDIT.CitizenOperatingDays = 1;
                APP_ELECTRONIC_COMMERCIAL_EDIT.CitizenOperatingCost = 20;
                APP_ELECTRONIC_COMMERCIAL_EDIT.ConsumerKey = Guid.Parse("d216ffeb-9d71-4483-9ac7-e6752dbe8c1f");

                string TranslateName = "ขอแก้ไขทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์";

                updateApplication(context, APP_ELECTRONIC_COMMERCIAL_EDIT, TranslateName, creater);
            }

            #endregion

            #region CANCEL

            Application APP_ELECTRONIC_COMMERCIAL_CANCEL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ELECTRONIC_COMMERCIAL_CANCEL).FirstOrDefault();

            if (APP_ELECTRONIC_COMMERCIAL_CANCEL == null)
            {
                APP_ELECTRONIC_COMMERCIAL_CANCEL = new Application();
                APP_ELECTRONIC_COMMERCIAL_CANCEL.ApplicationSysName = ApplicationSysName.APP_ELECTRONIC_COMMERCIAL_CANCEL;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.AppsHookClassName = "BizPortal.AppsHook.DBDCommerceCancelAppHook";
                APP_ELECTRONIC_COMMERCIAL_CANCEL.OrgCode = "12007000";
                APP_ELECTRONIC_COMMERCIAL_CANCEL.LogoFileID = null;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.HandbookUrl = null;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.CitizenHandbookUrl = null;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.OperatingDays = 0;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.OperatingDayType = CostType.Fixed;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.OperatingDays2 = null;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.OperatingCost = 0;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.OperatingCostType = CostType.Fixed;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.OperatingCost2 = null;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.CitizenOperatingDays = 0;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.CitizenOperatingDayType = CostType.Fixed;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.CitizenOperatingDays2 = null;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.CitizenOperatingCost = 0;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.CitizenOperatingCost2 = null;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.ShowRemark = false;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.Remark = null;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.CitizenShowRemark = false;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.CitizenRemark = null;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.SingleFormEnabled = true;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.ConsumerKey = Guid.Parse("d216ffeb-9d71-4483-9ac7-e6752dbe8c1f");

                string TranslateName = "ขอยกเลิกทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์";

                createApplication(context, APP_ELECTRONIC_COMMERCIAL_CANCEL, TranslateName, creater);
            }
            else
            {
                APP_ELECTRONIC_COMMERCIAL_CANCEL.AppsHookClassName = "BizPortal.AppsHook.DBDCommerceCancelAppHook";
                APP_ELECTRONIC_COMMERCIAL_CANCEL.OperatingDays = 1;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.OperatingCost = 20;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.CitizenOperatingDays = 1;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.CitizenOperatingCost = 20;
                APP_ELECTRONIC_COMMERCIAL_CANCEL.ConsumerKey = Guid.Parse("d216ffeb-9d71-4483-9ac7-e6752dbe8c1f");

                string TranslateName = "ขอยกเลิกทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์";

                updateApplication(context, APP_ELECTRONIC_COMMERCIAL_CANCEL, TranslateName, creater);
            }

            #endregion

            #endregion

            #region [ 38 ] APP ENERGY PRODUCTION

            #region NEW

            Application APP_ENERGY_PRODUCTION = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ENERGY_PRODUCTION).FirstOrDefault();

            if (APP_ENERGY_PRODUCTION == null)
            {
                APP_ENERGY_PRODUCTION = new Application();
                APP_ENERGY_PRODUCTION.ApplicationSysName = ApplicationSysName.APP_ENERGY_PRODUCTION;
                APP_ENERGY_PRODUCTION.AppsHookClassName = "BizPortal.AppsHook.DEDEProductionNewAppHook";
                APP_ENERGY_PRODUCTION.OrgCode = "11003000";
                APP_ENERGY_PRODUCTION.LogoFileID = null;
                APP_ENERGY_PRODUCTION.HandbookUrl = "https://info.go.th/#!/th/search/9076/%E0%B8%9C%E0%B8%A5%E0%B8%B4%E0%B8%95%E0%B8%9E%E0%B8%A5%E0%B8%B1%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99%E0%B8%84%E0%B8%A7%E0%B8%9A%E0%B8%84%E0%B8%B8%E0%B8%A1/";
                APP_ENERGY_PRODUCTION.CitizenHandbookUrl = "https://info.go.th/#!/th/search/9076/%E0%B8%9C%E0%B8%A5%E0%B8%B4%E0%B8%95%E0%B8%9E%E0%B8%A5%E0%B8%B1%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99%E0%B8%84%E0%B8%A7%E0%B8%9A%E0%B8%84%E0%B8%B8%E0%B8%A1/";
                APP_ENERGY_PRODUCTION.OperatingDays = 90;
                APP_ENERGY_PRODUCTION.OperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION.OperatingDays2 = null;
                APP_ENERGY_PRODUCTION.OperatingCost = 0;
                APP_ENERGY_PRODUCTION.OperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION.OperatingCost2 = null;
                APP_ENERGY_PRODUCTION.CitizenOperatingDays = 90;
                APP_ENERGY_PRODUCTION.CitizenOperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION.CitizenOperatingDays2 = null;
                APP_ENERGY_PRODUCTION.CitizenOperatingCost = 0;
                APP_ENERGY_PRODUCTION.CitizenOperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION.CitizenOperatingCost2 = null;
                APP_ENERGY_PRODUCTION.ShowRemark = true;
                APP_ENERGY_PRODUCTION.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_ENERGY_PRODUCTION.CitizenShowRemark = true;
                APP_ENERGY_PRODUCTION.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_ENERGY_PRODUCTION.SingleFormEnabled = true;
                string TranslateName = "ใบอนุญาตให้ผลิตพลังงานควบคุม";

                createApplication(context, APP_ENERGY_PRODUCTION, TranslateName, creater);
            }
            else
            {
                APP_ENERGY_PRODUCTION.ApplicationSysName = ApplicationSysName.APP_ENERGY_PRODUCTION;
                APP_ENERGY_PRODUCTION.AppsHookClassName = "BizPortal.AppsHook.DEDEProductionNewAppHook";
                APP_ENERGY_PRODUCTION.OrgCode = "11003000";
                APP_ENERGY_PRODUCTION.LogoFileID = null;
                APP_ENERGY_PRODUCTION.HandbookUrl = "https://info.go.th/#!/th/search/9076/%E0%B8%9C%E0%B8%A5%E0%B8%B4%E0%B8%95%E0%B8%9E%E0%B8%A5%E0%B8%B1%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99%E0%B8%84%E0%B8%A7%E0%B8%9A%E0%B8%84%E0%B8%B8%E0%B8%A1/";
                APP_ENERGY_PRODUCTION.CitizenHandbookUrl = "https://info.go.th/#!/th/search/9076/%E0%B8%9C%E0%B8%A5%E0%B8%B4%E0%B8%95%E0%B8%9E%E0%B8%A5%E0%B8%B1%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99%E0%B8%84%E0%B8%A7%E0%B8%9A%E0%B8%84%E0%B8%B8%E0%B8%A1/";
                APP_ENERGY_PRODUCTION.OperatingDays = 90;
                APP_ENERGY_PRODUCTION.OperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION.OperatingDays2 = null;
                APP_ENERGY_PRODUCTION.OperatingCost = 0;
                APP_ENERGY_PRODUCTION.OperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION.OperatingCost2 = null;
                APP_ENERGY_PRODUCTION.CitizenOperatingDays = 90;
                APP_ENERGY_PRODUCTION.CitizenOperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION.CitizenOperatingDays2 = null;
                APP_ENERGY_PRODUCTION.CitizenOperatingCost = 0;
                APP_ENERGY_PRODUCTION.CitizenOperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION.CitizenOperatingCost2 = null;
                APP_ENERGY_PRODUCTION.ShowRemark = true;
                APP_ENERGY_PRODUCTION.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_ENERGY_PRODUCTION.CitizenShowRemark = true;
                APP_ENERGY_PRODUCTION.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_ENERGY_PRODUCTION.SingleFormEnabled = true;
                string TranslateName = "ใบอนุญาตให้ผลิตพลังงานควบคุม";

                updateApplication(context, APP_ENERGY_PRODUCTION, TranslateName, creater);
            }

            /* UPDATE HERE */
            /* APP_X.XXX = XXXXX */

            #endregion

            #region RENEW

            Application APP_ENERGY_PRODUCTION_RENEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ENERGY_PRODUCTION_RENEW).FirstOrDefault();

            if (APP_ENERGY_PRODUCTION_RENEW == null)
            {
                APP_ENERGY_PRODUCTION_RENEW = new Application();
                APP_ENERGY_PRODUCTION_RENEW.ApplicationSysName = ApplicationSysName.APP_ENERGY_PRODUCTION_RENEW;
                APP_ENERGY_PRODUCTION_RENEW.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_ENERGY_PRODUCTION_RENEW.OrgCode = "11003000";
                APP_ENERGY_PRODUCTION_RENEW.LogoFileID = null;
                APP_ENERGY_PRODUCTION_RENEW.HandbookUrl = "https://info.go.th/#!/th/search/9076/%E0%B8%9C%E0%B8%A5%E0%B8%B4%E0%B8%95%E0%B8%9E%E0%B8%A5%E0%B8%B1%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99%E0%B8%84%E0%B8%A7%E0%B8%9A%E0%B8%84%E0%B8%B8%E0%B8%A1/";
                APP_ENERGY_PRODUCTION_RENEW.CitizenHandbookUrl = "https://info.go.th/#!/th/search/9076/%E0%B8%9C%E0%B8%A5%E0%B8%B4%E0%B8%95%E0%B8%9E%E0%B8%A5%E0%B8%B1%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99%E0%B8%84%E0%B8%A7%E0%B8%9A%E0%B8%84%E0%B8%B8%E0%B8%A1/";
                APP_ENERGY_PRODUCTION_RENEW.OperatingDays = 90;
                APP_ENERGY_PRODUCTION_RENEW.OperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_RENEW.OperatingDays2 = null;
                APP_ENERGY_PRODUCTION_RENEW.OperatingCost = 0;
                APP_ENERGY_PRODUCTION_RENEW.OperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_RENEW.OperatingCost2 = null;
                APP_ENERGY_PRODUCTION_RENEW.CitizenOperatingDays = 90;
                APP_ENERGY_PRODUCTION_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_RENEW.CitizenOperatingDays2 = null;
                APP_ENERGY_PRODUCTION_RENEW.CitizenOperatingCost = 0;
                APP_ENERGY_PRODUCTION_RENEW.CitizenOperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_RENEW.CitizenOperatingCost2 = null;
                APP_ENERGY_PRODUCTION_RENEW.ShowRemark = true;
                APP_ENERGY_PRODUCTION_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_ENERGY_PRODUCTION_RENEW.CitizenShowRemark = true;
                APP_ENERGY_PRODUCTION_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_ENERGY_PRODUCTION_RENEW.SingleFormEnabled = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตผลิตพลังงานควบคุม";

                createApplication(context, APP_ENERGY_PRODUCTION_RENEW, TranslateName, creater);
            }

            /* UPDATE HERE */
            /* APP_X.XXX = XXXXX */
            APP_ENERGY_PRODUCTION_RENEW.ShowRemark = true;
            APP_ENERGY_PRODUCTION_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
            APP_ENERGY_PRODUCTION_RENEW.CitizenShowRemark = true;
            APP_ENERGY_PRODUCTION_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
            context.SaveChanges(false);
            #endregion

            #region EDIT

            Application APP_ENERGY_PRODUCTION_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ENERGY_PRODUCTION_EDIT).FirstOrDefault();

            if (APP_ENERGY_PRODUCTION_EDIT == null)
            {
                APP_ENERGY_PRODUCTION_EDIT = new Application();
                APP_ENERGY_PRODUCTION_EDIT.ApplicationSysName = ApplicationSysName.APP_ENERGY_PRODUCTION_EDIT;
                APP_ENERGY_PRODUCTION_EDIT.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_ENERGY_PRODUCTION_EDIT.OrgCode = "11003000";
                APP_ENERGY_PRODUCTION_EDIT.LogoFileID = null;
                APP_ENERGY_PRODUCTION_EDIT.HandbookUrl = "https://info.go.th/#!/th/search/9076/%E0%B8%9C%E0%B8%A5%E0%B8%B4%E0%B8%95%E0%B8%9E%E0%B8%A5%E0%B8%B1%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99%E0%B8%84%E0%B8%A7%E0%B8%9A%E0%B8%84%E0%B8%B8%E0%B8%A1/";
                APP_ENERGY_PRODUCTION_EDIT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/9076/%E0%B8%9C%E0%B8%A5%E0%B8%B4%E0%B8%95%E0%B8%9E%E0%B8%A5%E0%B8%B1%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99%E0%B8%84%E0%B8%A7%E0%B8%9A%E0%B8%84%E0%B8%B8%E0%B8%A1/";
                APP_ENERGY_PRODUCTION_EDIT.OperatingDays = 90;
                APP_ENERGY_PRODUCTION_EDIT.OperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_EDIT.OperatingDays2 = null;
                APP_ENERGY_PRODUCTION_EDIT.OperatingCost = 0;
                APP_ENERGY_PRODUCTION_EDIT.OperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_EDIT.OperatingCost2 = null;
                APP_ENERGY_PRODUCTION_EDIT.CitizenOperatingDays = 90;
                APP_ENERGY_PRODUCTION_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_EDIT.CitizenOperatingDays2 = null;
                APP_ENERGY_PRODUCTION_EDIT.CitizenOperatingCost = 0;
                APP_ENERGY_PRODUCTION_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_EDIT.CitizenOperatingCost2 = null;
                APP_ENERGY_PRODUCTION_EDIT.ShowRemark = true;
                APP_ENERGY_PRODUCTION_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_ENERGY_PRODUCTION_EDIT.CitizenShowRemark = true;
                APP_ENERGY_PRODUCTION_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_ENERGY_PRODUCTION_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตผลิตพลังงานควบคุม";

                createApplication(context, APP_ENERGY_PRODUCTION_EDIT, TranslateName, creater);
            }

            /* UPDATE HERE */
            /* APP_X.XXX = XXXXX */
            APP_ENERGY_PRODUCTION_EDIT.ShowRemark = true;
            APP_ENERGY_PRODUCTION_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
            APP_ENERGY_PRODUCTION_EDIT.CitizenShowRemark = true;
            APP_ENERGY_PRODUCTION_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
            context.SaveChanges(false);
            #endregion

            #region CANCEL

            Application APP_ENERGY_PRODUCTION_CANCEL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ENERGY_PRODUCTION_CANCEL).FirstOrDefault();

            if (APP_ENERGY_PRODUCTION_CANCEL == null)
            {
                APP_ENERGY_PRODUCTION_CANCEL = new Application();
                APP_ENERGY_PRODUCTION_CANCEL.ApplicationSysName = ApplicationSysName.APP_ENERGY_PRODUCTION_CANCEL;
                APP_ENERGY_PRODUCTION_CANCEL.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_ENERGY_PRODUCTION_CANCEL.OrgCode = "11003000";
                APP_ENERGY_PRODUCTION_CANCEL.LogoFileID = null;
                APP_ENERGY_PRODUCTION_CANCEL.HandbookUrl = null;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenHandbookUrl = null;
                APP_ENERGY_PRODUCTION_CANCEL.OperatingDays = 90;
                APP_ENERGY_PRODUCTION_CANCEL.OperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_CANCEL.OperatingDays2 = null;
                APP_ENERGY_PRODUCTION_CANCEL.OperatingCost = 0;
                APP_ENERGY_PRODUCTION_CANCEL.OperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_CANCEL.OperatingCost2 = null;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenOperatingDays = 90;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenOperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenOperatingDays2 = null;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenOperatingCost = 0;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenOperatingCost2 = null;
                APP_ENERGY_PRODUCTION_CANCEL.ShowRemark = true;
                APP_ENERGY_PRODUCTION_CANCEL.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_ENERGY_PRODUCTION_CANCEL.CitizenShowRemark = true;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_ENERGY_PRODUCTION_CANCEL.SingleFormEnabled = true;
                string TranslateName = "ขอยกเลิกใบอนุญาตผลิตพลังงานควบคุม";

                createApplication(context, APP_ENERGY_PRODUCTION_CANCEL, TranslateName, creater);
            }
            else
            {
                APP_ENERGY_PRODUCTION_CANCEL.ApplicationSysName = ApplicationSysName.APP_ENERGY_PRODUCTION_CANCEL;
                APP_ENERGY_PRODUCTION_CANCEL.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_ENERGY_PRODUCTION_CANCEL.OrgCode = "11003000";
                APP_ENERGY_PRODUCTION_CANCEL.LogoFileID = null;
                APP_ENERGY_PRODUCTION_CANCEL.HandbookUrl = null;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenHandbookUrl = null;
                APP_ENERGY_PRODUCTION_CANCEL.OperatingDays = 90;
                APP_ENERGY_PRODUCTION_CANCEL.OperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_CANCEL.OperatingDays2 = null;
                APP_ENERGY_PRODUCTION_CANCEL.OperatingCost = 0;
                APP_ENERGY_PRODUCTION_CANCEL.OperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_CANCEL.OperatingCost2 = null;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenOperatingDays = 90;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenOperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenOperatingDays2 = null;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenOperatingCost = 0;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenOperatingCost2 = null;
                APP_ENERGY_PRODUCTION_CANCEL.ShowRemark = true;
                APP_ENERGY_PRODUCTION_CANCEL.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_ENERGY_PRODUCTION_CANCEL.CitizenShowRemark = true;
                APP_ENERGY_PRODUCTION_CANCEL.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_ENERGY_PRODUCTION_CANCEL.SingleFormEnabled = true;
                APP_ENERGY_PRODUCTION_CANCEL.MultipleRequestSupport = true;
                string TranslateName = "ขอยกเลิกใบอนุญาตผลิตพลังงานควบคุม";

                updateApplication(context, APP_ENERGY_PRODUCTION_CANCEL, TranslateName, creater);
            }

            /* UPDATE HERE */
            /* APP_X.XXX = XXXXX */
            APP_ENERGY_PRODUCTION_CANCEL.ShowRemark = true;
            APP_ENERGY_PRODUCTION_CANCEL.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
            APP_ENERGY_PRODUCTION_CANCEL.CitizenShowRemark = true;
            APP_ENERGY_PRODUCTION_CANCEL.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
            context.SaveChanges(false);
            #endregion

            #endregion

            #region [ 39 ] APP TRANSPORT NON REGULAR TRUCK

            #region NEW

            Application APP_TRANSPORT_NON_REGULAR_TRUCK = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_TRANSPORT_NON_REGULAR_TRUCK).FirstOrDefault();

            if (APP_TRANSPORT_NON_REGULAR_TRUCK == null)
            {
                APP_TRANSPORT_NON_REGULAR_TRUCK = new Application();
                APP_TRANSPORT_NON_REGULAR_TRUCK.ApplicationSysName = ApplicationSysName.APP_TRANSPORT_NON_REGULAR_TRUCK;
                APP_TRANSPORT_NON_REGULAR_TRUCK.AppsHookClassName = "BizPortal.AppsHook.DLTNonRegularTruckNewAppHook";
                APP_TRANSPORT_NON_REGULAR_TRUCK.OrgCode = "08003000";
                APP_TRANSPORT_NON_REGULAR_TRUCK.LogoFileID = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK.HandbookUrl = "https://info.go.th/Intro#!/th/search/5353/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%AD%E0%B8%AD%E0%B8%81%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%99%E0%B8%AA%E0%B9%88%E0%B8%87%E0%B9%84%E0%B8%A1%E0%B9%88%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%88%E0%B8%B3%E0%B8%97%E0%B8%B2%E0%B8%87%E0%B8%94%E0%B9%89%E0%B8%A7%E0%B8%A2%E0%B8%A3%E0%B8%96%E0%B8%9A%E0%B8%A3%E0%B8%A3%E0%B8%97%E0%B8%B8%E0%B8%81%E0%B8%95%E0%B8%B2%E0%B8%A1%E0%B8%81%E0%B8%8E%E0%B8%AB%E0%B8%A1%E0%B8%B2%E0%B8%A2%E0%B8%A7%E0%B9%88%E0%B8%B2%E0%B8%94%E0%B9%89%E0%B8%A7%E0%B8%A2%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%99%E0%B8%AA%E0%B9%88%E0%B8%87%E0%B8%97%E0%B8%B2%E0%B8%87%E0%B8%9A%E0%B8%81/";
                APP_TRANSPORT_NON_REGULAR_TRUCK.CitizenHandbookUrl = "https://info.go.th/Intro#!/th/search/5353/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%AD%E0%B8%AD%E0%B8%81%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%99%E0%B8%AA%E0%B9%88%E0%B8%87%E0%B9%84%E0%B8%A1%E0%B9%88%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%88%E0%B8%B3%E0%B8%97%E0%B8%B2%E0%B8%87%E0%B8%94%E0%B9%89%E0%B8%A7%E0%B8%A2%E0%B8%A3%E0%B8%96%E0%B8%9A%E0%B8%A3%E0%B8%A3%E0%B8%97%E0%B8%B8%E0%B8%81%E0%B8%95%E0%B8%B2%E0%B8%A1%E0%B8%81%E0%B8%8E%E0%B8%AB%E0%B8%A1%E0%B8%B2%E0%B8%A2%E0%B8%A7%E0%B9%88%E0%B8%B2%E0%B8%94%E0%B9%89%E0%B8%A7%E0%B8%A2%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%99%E0%B8%AA%E0%B9%88%E0%B8%87%E0%B8%97%E0%B8%B2%E0%B8%87%E0%B8%9A%E0%B8%81/";
                APP_TRANSPORT_NON_REGULAR_TRUCK.OperatingDays = 16;
                APP_TRANSPORT_NON_REGULAR_TRUCK.OperatingDayType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK.OperatingDays2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK.OperatingCost = 1500;
                APP_TRANSPORT_NON_REGULAR_TRUCK.OperatingCostType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK.OperatingCost2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK.CitizenOperatingDays = 16;
                APP_TRANSPORT_NON_REGULAR_TRUCK.CitizenOperatingDayType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK.CitizenOperatingDays2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK.CitizenOperatingCost = 1500;
                APP_TRANSPORT_NON_REGULAR_TRUCK.CitizenOperatingCostType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK.CitizenOperatingCost2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK.ShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK.Remark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น และกรุณาตอบคำถาม Smart Quiz ก่อนยื่นคำขอ เพื่อตรวจสอบว่าธุรกิจของคุณเข้าข่ายการขอใบอนุญาตนี้หรือไม่</span>";
                APP_TRANSPORT_NON_REGULAR_TRUCK.CitizenShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK.CitizenRemark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น และกรุณาตอบคำถาม Smart Quiz ก่อนยื่นคำขอ เพื่อตรวจสอบว่าธุรกิจของคุณเข้าข่ายการขอใบอนุญาตนี้หรือไม่</span>";
                APP_TRANSPORT_NON_REGULAR_TRUCK.SingleFormEnabled = true;
                string TranslateName = "ขอใบอนุญาตประกอบการขนส่งไม่ประจำทางด้วยรถบรรทุก";

                createApplication(context, APP_TRANSPORT_NON_REGULAR_TRUCK, TranslateName, creater);
            }
            else
            {
                APP_TRANSPORT_NON_REGULAR_TRUCK.AppsHookClassName = "BizPortal.AppsHook.DLTNonRegularTruckNewAppHook";
                APP_TRANSPORT_NON_REGULAR_TRUCK.OrgCode = "08003000";
                APP_TRANSPORT_NON_REGULAR_TRUCK.ShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK.Remark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น และกรุณาตอบคำถาม Smart Quiz ก่อนยื่นคำขอ เพื่อตรวจสอบว่าธุรกิจของคุณเข้าข่ายการขอใบอนุญาตนี้หรือไม่</span>";
                APP_TRANSPORT_NON_REGULAR_TRUCK.CitizenShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK.CitizenRemark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น และกรุณาตอบคำถาม Smart Quiz ก่อนยื่นคำขอ เพื่อตรวจสอบว่าธุรกิจของคุณเข้าข่ายการขอใบอนุญาตนี้หรือไม่</span>";

                context.SaveChanges(false);
            }

            #endregion

            #region RENEW

            Application APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW).FirstOrDefault();

            if (APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW == null)
            {
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW = new Application();
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.ApplicationSysName = ApplicationSysName.APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.AppsHookClassName = "BizPortal.AppsHook.DLTNonRegularTruckRenewAppHook";
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.OrgCode = "08003000";
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.LogoFileID = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.HandbookUrl = "https://info.go.th/#!/th/search/6680/%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%99%E0%B8%AA%E0%B9%88%E0%B8%87%E0%B9%84%E0%B8%A1%E0%B9%88%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%88%E0%B8%B3%E0%B8%97%E0%B8%B2%E0%B8%87/";
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.CitizenHandbookUrl = "https://info.go.th/#!/th/search/6680/%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%99%E0%B8%AA%E0%B9%88%E0%B8%87%E0%B9%84%E0%B8%A1%E0%B9%88%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%88%E0%B8%B3%E0%B8%97%E0%B8%B2%E0%B8%87/";
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.OperatingDays = 5;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.OperatingDayType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.OperatingDays2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.OperatingCost = 1500;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.OperatingCostType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.OperatingCost2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.CitizenOperatingDays = 5;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.CitizenOperatingDays2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.CitizenOperatingCost = 1500;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.CitizenOperatingCostType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.CitizenOperatingCost2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.ShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.Remark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.CitizenShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.CitizenRemark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.SingleFormEnabled = true;

                string TranslateName = "ขอต่ออายุใบอนุญาตประกอบการขนส่งไม่ประจำทางด้วยรถบรรทุก";

                createApplication(context, APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW, TranslateName, creater);
            }
            else
            {
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.OrgCode = "08003000";
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.ShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.Remark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.CitizenShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW.CitizenRemark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";

                context.SaveChanges(false);
            }

            #endregion

            #region EDIT

            Application APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT).FirstOrDefault();

            if (APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT == null)
            {
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT = new Application();
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.ApplicationSysName = ApplicationSysName.APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.AppsHookClassName = "BizPortal.AppsHook.DLTNonRegularTruckEditAppHook";
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.OrgCode = "08003000";
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.LogoFileID = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.HandbookUrl = "https://info.go.th/#!/th/search/6993/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%81%E0%B8%81%E0%B9%89%E0%B9%84%E0%B8%82%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B8%A5%E0%B8%B0%E0%B9%80%E0%B8%AD%E0%B8%B5%E0%B8%A2%E0%B8%94%E0%B9%83%E0%B8%99%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%99%E0%B8%AA%E0%B9%88%E0%B8%87%E0%B9%84%E0%B8%A1%E0%B9%88%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%88%E0%B8%B3%E0%B8%97%E0%B8%B2%E0%B8%87%E0%B8%94%E0%B9%89%E0%B8%A7%E0%B8%A2%E0%B8%A3%E0%B8%96%E0%B8%9A%E0%B8%A3%E0%B8%A3%E0%B8%97%E0%B8%B8%E0%B8%81/";
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/6993/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%81%E0%B8%81%E0%B9%89%E0%B9%84%E0%B8%82%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B8%A5%E0%B8%B0%E0%B9%80%E0%B8%AD%E0%B8%B5%E0%B8%A2%E0%B8%94%E0%B9%83%E0%B8%99%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%99%E0%B8%AA%E0%B9%88%E0%B8%87%E0%B9%84%E0%B8%A1%E0%B9%88%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%88%E0%B8%B3%E0%B8%97%E0%B8%B2%E0%B8%87%E0%B8%94%E0%B9%89%E0%B8%A7%E0%B8%A2%E0%B8%A3%E0%B8%96%E0%B8%9A%E0%B8%A3%E0%B8%A3%E0%B8%97%E0%B8%B8%E0%B8%81/";
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.OperatingDays = 1;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.OperatingDayType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.OperatingDays2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.OperatingCost = 0;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.OperatingCostType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.OperatingCost2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.CitizenOperatingDays = 1;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.CitizenOperatingDays2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.CitizenOperatingCost = 0;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.CitizenOperatingCost2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.ShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.Remark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น และสามารถแก้ไขข้อมูลสถานที่ประกอบการได้เท่านั้น</span>";
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.CitizenShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.CitizenRemark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น และสามารถแก้ไขข้อมูลสถานที่ประกอบการได้เท่านั้น</span>";
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.SingleFormEnabled = true;

                string TranslateName = "ขอแก้ไขใบอนุญาตประกอบการขนส่งไม่ประจำทางด้วยรถบรรทุก";

                createApplication(context, APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT, TranslateName, creater);
            }
            else
            {
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.OrgCode = "08003000";
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.ShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.Remark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น และสามารถแก้ไขข้อมูลสถานที่ประกอบการได้เท่านั้น</span>";
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.CitizenShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT.CitizenRemark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น และสามารถแก้ไขข้อมูลสถานที่ประกอบการได้เท่านั้น</span>";

                context.SaveChanges(false);
            }

            #endregion

            #region CANCEL

            Application APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL).FirstOrDefault();

            if (APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL == null)
            {
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL = new Application();
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.ApplicationSysName = ApplicationSysName.APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.AppsHookClassName = "BizPortal.AppsHook.DLTNonRegularTruckCancelAppHook";
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.OrgCode = "08003000";
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.LogoFileID = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.HandbookUrl = "https://info.go.th/#!/th/search/6972/%E0%B8%82%E0%B8%99%E0%B8%AA%E0%B9%88%E0%B8%87%E0%B9%84%E0%B8%A1%E0%B9%88%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%88%E0%B8%B3%E0%B8%97%E0%B8%B2%E0%B8%87/";
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.CitizenHandbookUrl = "https://info.go.th/#!/th/search/6972/%E0%B8%82%E0%B8%99%E0%B8%AA%E0%B9%88%E0%B8%87%E0%B9%84%E0%B8%A1%E0%B9%88%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%88%E0%B8%B3%E0%B8%97%E0%B8%B2%E0%B8%87/";
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.OperatingDays = 3;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.OperatingDayType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.OperatingDays2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.OperatingCost = 0;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.OperatingCostType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.OperatingCost2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.CitizenOperatingDays = 3;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.CitizenOperatingDayType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.CitizenOperatingDays2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.CitizenOperatingCost = 0;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.CitizenOperatingCost2 = null;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.ShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.Remark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.CitizenShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.CitizenRemark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.SingleFormEnabled = true;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบการขนส่งไม่ประจำทางด้วยรถบรรทุก";

                createApplication(context, APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL, TranslateName, creater);
            }
            else
            {
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.OrgCode = "08003000";
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.ShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.Remark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.CitizenShowRemark = true;
                APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL.CitizenRemark = "<span style=\"color: red; \">ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";

                context.SaveChanges(false);
            }

            #endregion

            #endregion

            #region [ 40 ] APP SECURITIES BUSINESS

            #region NEW

            Application APP_SECURITIES_BUSINESS = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SECURITIES_BUSINESS).FirstOrDefault();

            if (APP_SECURITIES_BUSINESS == null)
            {
                APP_SECURITIES_BUSINESS = new Application();
                APP_SECURITIES_BUSINESS.ApplicationSysName = ApplicationSysName.APP_SECURITIES_BUSINESS;
                APP_SECURITIES_BUSINESS.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_SECURITIES_BUSINESS.OrgCode = "21032000";
                APP_SECURITIES_BUSINESS.LogoFileID = null;
                APP_SECURITIES_BUSINESS.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SECURITIES_BUSINESS.CitizenHandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SECURITIES_BUSINESS.OperatingDays = 90;
                APP_SECURITIES_BUSINESS.OperatingDayType = CostType.Range;
                APP_SECURITIES_BUSINESS.OperatingDays2 = 150;
                APP_SECURITIES_BUSINESS.OperatingCost = 0;
                APP_SECURITIES_BUSINESS.OperatingCostType = CostType.Range;
                APP_SECURITIES_BUSINESS.OperatingCost2 = 20000000;
                APP_SECURITIES_BUSINESS.CitizenOperatingDays = 150;
                APP_SECURITIES_BUSINESS.CitizenOperatingDayType = CostType.Fixed;
                APP_SECURITIES_BUSINESS.CitizenOperatingDays2 = null;
                APP_SECURITIES_BUSINESS.CitizenOperatingCost = 0;
                APP_SECURITIES_BUSINESS.CitizenOperatingCostType = CostType.Range;
                APP_SECURITIES_BUSINESS.CitizenOperatingCost2 = 20000000;
                APP_SECURITIES_BUSINESS.ShowRemark = true;
                APP_SECURITIES_BUSINESS.Remark = "<span style=\"color: red; \">*ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ คำขอละ 30,000 บาท</span>";
                APP_SECURITIES_BUSINESS.CitizenShowRemark = true;
                APP_SECURITIES_BUSINESS.CitizenRemark = "<span style=\"color: red; \">*ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ คำขอละ 30,000 บาท</span>";

                APP_SECURITIES_BUSINESS.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_SECURITIES_BUSINESS.SingleFormEnabled = true;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์และสัญญาซื้อขายล่วงหน้า";

                createApplication(context, APP_SECURITIES_BUSINESS, TranslateName, creater);
            }
            else
            {
                APP_SECURITIES_BUSINESS.ApplicationSysName = ApplicationSysName.APP_SECURITIES_BUSINESS;
                APP_SECURITIES_BUSINESS.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_SECURITIES_BUSINESS.OrgCode = "21032000";
                APP_SECURITIES_BUSINESS.LogoFileID = null;
                APP_SECURITIES_BUSINESS.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SECURITIES_BUSINESS.CitizenHandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SECURITIES_BUSINESS.OperatingDays = 90;
                APP_SECURITIES_BUSINESS.OperatingDayType = CostType.Range;
                APP_SECURITIES_BUSINESS.OperatingDays2 = 150;
                APP_SECURITIES_BUSINESS.OperatingCost = 0;
                APP_SECURITIES_BUSINESS.OperatingCostType = CostType.Range;
                APP_SECURITIES_BUSINESS.OperatingCost2 = 20000000;
                APP_SECURITIES_BUSINESS.CitizenOperatingDays = 150;
                APP_SECURITIES_BUSINESS.CitizenOperatingDayType = CostType.Fixed;
                APP_SECURITIES_BUSINESS.CitizenOperatingDays2 = null;
                APP_SECURITIES_BUSINESS.CitizenOperatingCost = 0;
                APP_SECURITIES_BUSINESS.CitizenOperatingCostType = CostType.Range;
                APP_SECURITIES_BUSINESS.CitizenOperatingCost2 = 20000000;
                APP_SECURITIES_BUSINESS.ShowRemark = true;
                APP_SECURITIES_BUSINESS.Remark = "<span style=\"color: red; \">*ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ คำขอละ 30,000 บาท</span>";
                APP_SECURITIES_BUSINESS.CitizenShowRemark = true;
                APP_SECURITIES_BUSINESS.CitizenRemark = "<span style=\"color: red; \">*ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ คำขอละ 30,000 บาท</span>";

                APP_SECURITIES_BUSINESS.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_SECURITIES_BUSINESS.SingleFormEnabled = true;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์และสัญญาซื้อขายล่วงหน้า";

                updateApplication(context, APP_SECURITIES_BUSINESS, TranslateName, creater);
            }

            /* UPDATE HERE */
            /* APP_X.XXX = XXXXX */

            #endregion

            #endregion

            #region [ 40 ] APP SECURITIES BUSINESS EDIT

            #region EDIT

            Application APP_SECURITIES_BUSINESS_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SECURITIES_BUSINESS_EDIT).FirstOrDefault();

            if (APP_SECURITIES_BUSINESS_EDIT == null)
            {
                APP_SECURITIES_BUSINESS_EDIT = new Application();
                APP_SECURITIES_BUSINESS_EDIT.ApplicationSysName = ApplicationSysName.APP_SECURITIES_BUSINESS_EDIT;
                APP_SECURITIES_BUSINESS_EDIT.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_SECURITIES_BUSINESS_EDIT.OrgCode = "21032000";
                APP_SECURITIES_BUSINESS_EDIT.LogoFileID = null;
                APP_SECURITIES_BUSINESS_EDIT.HandbookUrl = "https://www.sec.or.th/TH/Pages/LawandRegulations/Intermediaries.asp";
                APP_SECURITIES_BUSINESS_EDIT.CitizenHandbookUrl = "https://www.sec.or.th/TH/Pages/LawandRegulations/Intermediaries.asp";
                APP_SECURITIES_BUSINESS_EDIT.OperatingDays = 1;
                APP_SECURITIES_BUSINESS_EDIT.OperatingDayType = CostType.Fixed;
                APP_SECURITIES_BUSINESS_EDIT.OperatingDays2 = 1;
                APP_SECURITIES_BUSINESS_EDIT.OperatingCost = 0;
                APP_SECURITIES_BUSINESS_EDIT.OperatingCostType = CostType.Fixed;
                APP_SECURITIES_BUSINESS_EDIT.OperatingCost2 = null;
                APP_SECURITIES_BUSINESS_EDIT.CitizenOperatingDays = 1;
                APP_SECURITIES_BUSINESS_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_SECURITIES_BUSINESS_EDIT.CitizenOperatingDays2 = null;
                APP_SECURITIES_BUSINESS_EDIT.CitizenOperatingCost = 0;
                APP_SECURITIES_BUSINESS_EDIT.CitizenOperatingCostType = CostType.Range;
                APP_SECURITIES_BUSINESS_EDIT.CitizenOperatingCost2 = 20000000;
                APP_SECURITIES_BUSINESS_EDIT.ShowRemark = true;
                APP_SECURITIES_BUSINESS_EDIT.Remark = "<span style=\"color: red; \">*ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ คำขอละ 30,000 บาท</span>";
                APP_SECURITIES_BUSINESS_EDIT.CitizenShowRemark = true;
                APP_SECURITIES_BUSINESS_EDIT.CitizenRemark = "<span style=\"color: red; \">*ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ คำขอละ 30,000 บาท</span>";

                APP_SECURITIES_BUSINESS_EDIT.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_SECURITIES_BUSINESS_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตประกอบธุรกิจหลักทรัพย์และสัญญาซื้อขายล่วงหน้า";

                createApplication(context, APP_SECURITIES_BUSINESS_EDIT, TranslateName, creater);
            }
            else
            {
                APP_SECURITIES_BUSINESS_EDIT.ApplicationSysName = ApplicationSysName.APP_SECURITIES_BUSINESS_EDIT;
                APP_SECURITIES_BUSINESS_EDIT.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_SECURITIES_BUSINESS_EDIT.OrgCode = "21032000";
                APP_SECURITIES_BUSINESS_EDIT.LogoFileID = null;
                APP_SECURITIES_BUSINESS_EDIT.HandbookUrl = "https://www.sec.or.th/TH/Pages/LawandRegulations/Intermediaries.asp";
                APP_SECURITIES_BUSINESS_EDIT.CitizenHandbookUrl = "https://www.sec.or.th/TH/Pages/LawandRegulations/Intermediaries.asp";
                APP_SECURITIES_BUSINESS_EDIT.OperatingDays = 1;
                APP_SECURITIES_BUSINESS_EDIT.OperatingDayType = CostType.Fixed;
                APP_SECURITIES_BUSINESS_EDIT.OperatingDays2 = 1;
                APP_SECURITIES_BUSINESS_EDIT.OperatingCost = 0;
                APP_SECURITIES_BUSINESS_EDIT.OperatingCostType = CostType.Fixed;
                APP_SECURITIES_BUSINESS_EDIT.OperatingCost2 = null;
                APP_SECURITIES_BUSINESS_EDIT.CitizenOperatingDays = 1;
                APP_SECURITIES_BUSINESS_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_SECURITIES_BUSINESS_EDIT.CitizenOperatingDays2 = null;
                APP_SECURITIES_BUSINESS_EDIT.CitizenOperatingCost = 0;
                APP_SECURITIES_BUSINESS_EDIT.CitizenOperatingCostType = CostType.Range;
                APP_SECURITIES_BUSINESS_EDIT.CitizenOperatingCost2 = 20000000;
                APP_SECURITIES_BUSINESS_EDIT.ShowRemark = true;
                APP_SECURITIES_BUSINESS_EDIT.Remark = "<span style=\"color: red; \">*ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ คำขอละ 30,000 บาท</span>";
                APP_SECURITIES_BUSINESS_EDIT.CitizenShowRemark = true;
                APP_SECURITIES_BUSINESS_EDIT.CitizenRemark = "<span style=\"color: red; \">*ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ คำขอละ 30,000 บาท</span>";

                APP_SECURITIES_BUSINESS_EDIT.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_SECURITIES_BUSINESS_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตประกอบธุรกิจหลักทรัพย์และสัญญาซื้อขายล่วงหน้า";

                updateApplication(context, APP_SECURITIES_BUSINESS_EDIT, TranslateName, creater);
            }

            /* UPDATE HERE */
            /* APP_X.XXX = XXXXX */

            #endregion

            #endregion

            #region [ 41 ] APP SOFTWARE HOUSE

            #region [NEW]
            Application APP_SOFTWARE_HOUSE = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SOFTWARE_HOUSE_NEW).FirstOrDefault();

            if (APP_SOFTWARE_HOUSE == null)
            {
                APP_SOFTWARE_HOUSE = new Application();
                APP_SOFTWARE_HOUSE.ApplicationSysName = ApplicationSysName.APP_SOFTWARE_HOUSE_NEW;
                APP_SOFTWARE_HOUSE.AppsHookClassName = "BizPortal.AppsHook.RDSoftwareHouseNewAppHook";
                APP_SOFTWARE_HOUSE.OrgCode = "03007000";
                APP_SOFTWARE_HOUSE.LogoFileID = null;
                APP_SOFTWARE_HOUSE.HandbookUrl = "https://www.rd.go.th/publish/5993.0.html";
                APP_SOFTWARE_HOUSE.CitizenHandbookUrl = "https://www.rd.go.th/publish/5993.0.html";
                APP_SOFTWARE_HOUSE.MultipleRequestSupport = true;
                APP_SOFTWARE_HOUSE.OperatingDays = 45;
                APP_SOFTWARE_HOUSE.OperatingDayType = CostType.Fixed;
                APP_SOFTWARE_HOUSE.OperatingDays2 = null;
                APP_SOFTWARE_HOUSE.OperatingCost = 0;
                APP_SOFTWARE_HOUSE.OperatingCostType = CostType.Fixed;
                APP_SOFTWARE_HOUSE.OperatingCost2 = null;
                APP_SOFTWARE_HOUSE.CitizenOperatingDays = 45;
                APP_SOFTWARE_HOUSE.CitizenOperatingDayType = CostType.Fixed;
                APP_SOFTWARE_HOUSE.CitizenOperatingDays2 = null;
                APP_SOFTWARE_HOUSE.CitizenOperatingCost = 0;
                APP_SOFTWARE_HOUSE.CitizenOperatingCostType = CostType.Fixed;
                APP_SOFTWARE_HOUSE.CitizenOperatingCost2 = null;
                APP_SOFTWARE_HOUSE.ShowRemark = true;
                APP_SOFTWARE_HOUSE.Remark = "<span style=\"color: red; \">กรุณายื่นคำขอที่หน่วยงาน โดยระยะเวลาการพิจารณาภายใน 45 วัน นับแต่วันที่ได้รับเอกสารถูกต้องครบถ้วนจากผู้ประกอบการจดทะเบียน ภายหลังการสาธิตโปรแกรม ณ กรมสรรพากร ซึ่งเป็นไปตามหลักเกณฑ์ วิธีการ และเงื่อนไขที่กรมสรรพากรกำหนด </span>";
                APP_SOFTWARE_HOUSE.CitizenShowRemark = true;
                APP_SOFTWARE_HOUSE.CitizenRemark = "<span style=\"color: red; \">กรุณายื่นคำขอที่หน่วยงาน โดยระยะเวลาการพิจารณาภายใน 45 วัน นับแต่วันที่ได้รับเอกสารถูกต้องครบถ้วนจากผู้ประกอบการจดทะเบียน ภายหลังการสาธิตโปรแกรม ณ กรมสรรพากร ซึ่งเป็นไปตามหลักเกณฑ์ วิธีการ และเงื่อนไขที่กรมสรรพากรกำหนด </span>";
                APP_SOFTWARE_HOUSE.SingleFormEnabled = true;
                APP_SOFTWARE_HOUSE.StatusSequence = "CHECK,PENDING,COMPLETED";
                string TranslateName = "ขอมีเลขประจำตัวซอฟต์แวร์เฮ้าส์";

                createApplication(context, APP_SOFTWARE_HOUSE, TranslateName, creater);
            }
            else
            {
                APP_SOFTWARE_HOUSE.ApplicationSysName = ApplicationSysName.APP_SOFTWARE_HOUSE_NEW;
                APP_SOFTWARE_HOUSE.AppsHookClassName = "BizPortal.AppsHook.RDSoftwareHouseNewAppHook";
                APP_SOFTWARE_HOUSE.OrgCode = "03007000";
                APP_SOFTWARE_HOUSE.LogoFileID = null;
                APP_SOFTWARE_HOUSE.HandbookUrl = "https://www.rd.go.th/publish/5993.0.html";
                APP_SOFTWARE_HOUSE.CitizenHandbookUrl = "https://www.rd.go.th/publish/5993.0.html";
                APP_SOFTWARE_HOUSE.MultipleRequestSupport = true;
                APP_SOFTWARE_HOUSE.OperatingDays = 45;
                APP_SOFTWARE_HOUSE.OperatingDayType = CostType.Fixed;
                APP_SOFTWARE_HOUSE.OperatingDays2 = null;
                APP_SOFTWARE_HOUSE.OperatingCost = 0;
                APP_SOFTWARE_HOUSE.OperatingCostType = CostType.Fixed;
                APP_SOFTWARE_HOUSE.OperatingCost2 = null;
                APP_SOFTWARE_HOUSE.CitizenOperatingDays = 45;
                APP_SOFTWARE_HOUSE.CitizenOperatingDayType = CostType.Fixed;
                APP_SOFTWARE_HOUSE.CitizenOperatingDays2 = null;
                APP_SOFTWARE_HOUSE.CitizenOperatingCost = 0;
                APP_SOFTWARE_HOUSE.CitizenOperatingCostType = CostType.Fixed;
                APP_SOFTWARE_HOUSE.CitizenOperatingCost2 = null;
                APP_SOFTWARE_HOUSE.ShowRemark = true;
                APP_SOFTWARE_HOUSE.Remark = "<span style=\"color: red; \">กรุณายื่นคำขอที่หน่วยงาน โดยระยะเวลาการพิจารณาภายใน 45 วัน นับแต่วันที่ได้รับเอกสารถูกต้องครบถ้วนจากผู้ประกอบการจดทะเบียน ภายหลังการสาธิตโปรแกรม ณ กรมสรรพากร ซึ่งเป็นไปตามหลักเกณฑ์ วิธีการ และเงื่อนไขที่กรมสรรพากรกำหนด </span>";
                APP_SOFTWARE_HOUSE.CitizenShowRemark = true;
                APP_SOFTWARE_HOUSE.CitizenRemark = "<span style=\"color: red; \">กรุณายื่นคำขอที่หน่วยงาน โดยระยะเวลาการพิจารณาภายใน 45 วัน นับแต่วันที่ได้รับเอกสารถูกต้องครบถ้วนจากผู้ประกอบการจดทะเบียน ภายหลังการสาธิตโปรแกรม ณ กรมสรรพากร ซึ่งเป็นไปตามหลักเกณฑ์ วิธีการ และเงื่อนไขที่กรมสรรพากรกำหนด </span>";
                APP_SOFTWARE_HOUSE.SingleFormEnabled = true;
                APP_SOFTWARE_HOUSE.StatusSequence = "CHECK,PENDING,COMPLETED";
                string TranslateName = "ขอมีเลขประจำตัวซอฟต์แวร์เฮ้าส์";

                updateApplication(context, APP_SOFTWARE_HOUSE, TranslateName, creater);
            }
            #endregion

            #region [RENEW]
            Application APP_SOFTWARE_HOUSE_RENEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SOFTWARE_HOUSE_RENEW).FirstOrDefault();

            if (APP_SOFTWARE_HOUSE_RENEW == null)
            {
                APP_SOFTWARE_HOUSE_RENEW = new Application();
                APP_SOFTWARE_HOUSE_RENEW.ApplicationSysName = ApplicationSysName.APP_SOFTWARE_HOUSE_RENEW;
                APP_SOFTWARE_HOUSE_RENEW.AppsHookClassName = "BizPortal.AppsHook.RDSoftwareHouseRenewAppHook";
                APP_SOFTWARE_HOUSE_RENEW.OrgCode = "03007000";
                APP_SOFTWARE_HOUSE_RENEW.LogoFileID = null;
                APP_SOFTWARE_HOUSE_RENEW.HandbookUrl = "http://www.rd.go.th/publish/272.0.html";
                APP_SOFTWARE_HOUSE_RENEW.CitizenHandbookUrl = "http://www.rd.go.th/publish/272.0.html";
                APP_SOFTWARE_HOUSE_RENEW.MultipleRequestSupport = true;
                APP_SOFTWARE_HOUSE_RENEW.OperatingDays = 1;
                APP_SOFTWARE_HOUSE_RENEW.OperatingDayType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_RENEW.OperatingDays2 = null;
                APP_SOFTWARE_HOUSE_RENEW.OperatingCost = 0;
                APP_SOFTWARE_HOUSE_RENEW.OperatingCostType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_RENEW.OperatingCost2 = null;
                APP_SOFTWARE_HOUSE_RENEW.CitizenOperatingDays = 1;
                APP_SOFTWARE_HOUSE_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_RENEW.CitizenOperatingDays2 = null;
                APP_SOFTWARE_HOUSE_RENEW.CitizenOperatingCost = 0;
                APP_SOFTWARE_HOUSE_RENEW.CitizenOperatingCostType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_RENEW.CitizenOperatingCost2 = null;
                APP_SOFTWARE_HOUSE_RENEW.ShowRemark = true;
                APP_SOFTWARE_HOUSE_RENEW.Remark = "<span style=\"color: red; \">ระยะเวลาการพิจารณาภายใน 45 วัน นับแต่วันที่ได้รับเอกสารถูกต้องครบถ้วนจากผู้ประกอบการจดทะเบียน ภายหลังการสาธิตโปรแกรม ณ กรมสรรพากร ซึ่งเป็นไปตามหลักเกณฑ์ วิธีการ และเงื่อนไขที่กรมสรรพากรกำหนด </span>";
                APP_SOFTWARE_HOUSE_RENEW.CitizenShowRemark = true;
                APP_SOFTWARE_HOUSE_RENEW.CitizenRemark = "<span style=\"color: red; \">ระยะเวลาการพิจารณาภายใน 45 วัน นับแต่วันที่ได้รับเอกสารถูกต้องครบถ้วนจากผู้ประกอบการจดทะเบียน ภายหลังการสาธิตโปรแกรม ณ กรมสรรพากร ซึ่งเป็นไปตามหลักเกณฑ์ วิธีการ และเงื่อนไขที่กรมสรรพากรกำหนด </span>";
                APP_SOFTWARE_HOUSE_RENEW.SingleFormEnabled = true;
                APP_SOFTWARE_HOUSE_RENEW.StatusSequence = "CHECK,PENDING,COMPLETED";
                string TranslateName = "ขอแจ้งข้อมูลการจำหน่ายซอฟต์แวร์ตามมาตราฐานซอฟต์แวร์ของกรมสรรพากร";

                createApplication(context, APP_SOFTWARE_HOUSE_RENEW, TranslateName, creater);
            }
            else
            {
                APP_SOFTWARE_HOUSE_RENEW.ApplicationSysName = ApplicationSysName.APP_SOFTWARE_HOUSE_RENEW;
                APP_SOFTWARE_HOUSE_RENEW.AppsHookClassName = "BizPortal.AppsHook.RDSoftwareHouseRenewAppHook";
                APP_SOFTWARE_HOUSE_RENEW.OrgCode = "03007000";
                APP_SOFTWARE_HOUSE_RENEW.LogoFileID = null;
                APP_SOFTWARE_HOUSE_RENEW.HandbookUrl = "http://www.rd.go.th/publish/272.0.html";
                APP_SOFTWARE_HOUSE_RENEW.CitizenHandbookUrl = "http://www.rd.go.th/publish/272.0.html";
                APP_SOFTWARE_HOUSE_RENEW.MultipleRequestSupport = true;
                APP_SOFTWARE_HOUSE_RENEW.OperatingDays = 1;
                APP_SOFTWARE_HOUSE_RENEW.OperatingDayType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_RENEW.OperatingDays2 = null;
                APP_SOFTWARE_HOUSE_RENEW.OperatingCost = 0;
                APP_SOFTWARE_HOUSE_RENEW.OperatingCostType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_RENEW.OperatingCost2 = null;
                APP_SOFTWARE_HOUSE_RENEW.CitizenOperatingDays = 1;
                APP_SOFTWARE_HOUSE_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_RENEW.CitizenOperatingDays2 = null;
                APP_SOFTWARE_HOUSE_RENEW.CitizenOperatingCost = 0;
                APP_SOFTWARE_HOUSE_RENEW.CitizenOperatingCostType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_RENEW.CitizenOperatingCost2 = null;
                APP_SOFTWARE_HOUSE_RENEW.ShowRemark = true;
                APP_SOFTWARE_HOUSE_RENEW.Remark = "<span style=\"color: red; \">ระยะเวลาการพิจารณาภายใน 45 วัน นับแต่วันที่ได้รับเอกสารถูกต้องครบถ้วนจากผู้ประกอบการจดทะเบียน ภายหลังการสาธิตโปรแกรม ณ กรมสรรพากร ซึ่งเป็นไปตามหลักเกณฑ์ วิธีการ และเงื่อนไขที่กรมสรรพากรกำหนด </span>";
                APP_SOFTWARE_HOUSE_RENEW.CitizenShowRemark = true;
                APP_SOFTWARE_HOUSE_RENEW.CitizenRemark = "<span style=\"color: red; \">ระยะเวลาการพิจารณาภายใน 45 วัน นับแต่วันที่ได้รับเอกสารถูกต้องครบถ้วนจากผู้ประกอบการจดทะเบียน ภายหลังการสาธิตโปรแกรม ณ กรมสรรพากร ซึ่งเป็นไปตามหลักเกณฑ์ วิธีการ และเงื่อนไขที่กรมสรรพากรกำหนด </span>";
                APP_SOFTWARE_HOUSE_RENEW.SingleFormEnabled = true;
                APP_SOFTWARE_HOUSE_RENEW.StatusSequence = "CHECK,PENDING,COMPLETED";
                string TranslateName = "ขอแจ้งข้อมูลการจำหน่ายซอฟต์แวร์ตามมาตราฐานซอฟต์แวร์ของกรมสรรพากร";

                updateApplication(context, APP_SOFTWARE_HOUSE_RENEW, TranslateName, creater);
            }
            #endregion

            #region [EDIT]
            Application APP_SOFTWARE_HOUSE_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SOFTWARE_HOUSE_EDIT).FirstOrDefault();

            if (APP_SOFTWARE_HOUSE_EDIT == null)
            {
                APP_SOFTWARE_HOUSE_EDIT = new Application();
                APP_SOFTWARE_HOUSE_EDIT.ApplicationSysName = ApplicationSysName.APP_SOFTWARE_HOUSE_EDIT;
                APP_SOFTWARE_HOUSE_EDIT.AppsHookClassName = "BizPortal.AppsHook.RDSoftwareHouseEditAppHook";
                APP_SOFTWARE_HOUSE_EDIT.OrgCode = "03007000";
                APP_SOFTWARE_HOUSE_EDIT.LogoFileID = null;
                APP_SOFTWARE_HOUSE_EDIT.HandbookUrl = "https://www.rd.go.th/publish/5993.0.html";
                APP_SOFTWARE_HOUSE_EDIT.CitizenHandbookUrl = "https://www.rd.go.th/publish/5993.0.html";
                APP_SOFTWARE_HOUSE_EDIT.MultipleRequestSupport = true;
                APP_SOFTWARE_HOUSE_EDIT.OperatingDays = 45;
                APP_SOFTWARE_HOUSE_EDIT.OperatingDayType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_EDIT.OperatingDays2 = null;
                APP_SOFTWARE_HOUSE_EDIT.OperatingCost = 0;
                APP_SOFTWARE_HOUSE_EDIT.OperatingCostType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_EDIT.OperatingCost2 = null;
                APP_SOFTWARE_HOUSE_EDIT.CitizenOperatingDays = 45;
                APP_SOFTWARE_HOUSE_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_EDIT.CitizenOperatingDays2 = null;
                APP_SOFTWARE_HOUSE_EDIT.CitizenOperatingCost = 0;
                APP_SOFTWARE_HOUSE_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_EDIT.CitizenOperatingCost2 = null;
                APP_SOFTWARE_HOUSE_EDIT.ShowRemark = true;
                APP_SOFTWARE_HOUSE_EDIT.Remark = "<span style=\"color: red; \">ระยะเวลาการพิจารณาภายใน 45 วัน นับแต่วันที่ได้รับเอกสารถูกต้องครบถ้วนจากผู้ประกอบการจดทะเบียน ภายหลังการสาธิตโปรแกรม ณ กรมสรรพากร ซึ่งเป็นไปตามหลักเกณฑ์ วิธีการ และเงื่อนไขที่กรมสรรพากรกำหนด </span>"; ;
                APP_SOFTWARE_HOUSE_EDIT.CitizenShowRemark = true;
                APP_SOFTWARE_HOUSE_EDIT.CitizenRemark = "<span style=\"color: red; \">ระยะเวลาการพิจารณาภายใน 45 วัน นับแต่วันที่ได้รับเอกสารถูกต้องครบถ้วนจากผู้ประกอบการจดทะเบียน ภายหลังการสาธิตโปรแกรม ณ กรมสรรพากร ซึ่งเป็นไปตามหลักเกณฑ์ วิธีการ และเงื่อนไขที่กรมสรรพากรกำหนด </span>"; ;
                APP_SOFTWARE_HOUSE_EDIT.SingleFormEnabled = true;
                APP_SOFTWARE_HOUSE_EDIT.StatusSequence = "CHECK,PENDING,COMPLETED";
                string TranslateName = "ขอแก้ไขเลขประจำตัวซอฟต์แวร์เฮ้าส์";

                createApplication(context, APP_SOFTWARE_HOUSE_EDIT, TranslateName, creater);
            }
            else
            {
                APP_SOFTWARE_HOUSE_EDIT.ApplicationSysName = ApplicationSysName.APP_SOFTWARE_HOUSE_EDIT;
                APP_SOFTWARE_HOUSE_EDIT.AppsHookClassName = "BizPortal.AppsHook.RDSoftwareHouseEditAppHook";
                APP_SOFTWARE_HOUSE_EDIT.OrgCode = "03007000";
                APP_SOFTWARE_HOUSE_EDIT.LogoFileID = null;
                APP_SOFTWARE_HOUSE_EDIT.HandbookUrl = "https://www.rd.go.th/publish/5993.0.html";
                APP_SOFTWARE_HOUSE_EDIT.CitizenHandbookUrl = "https://www.rd.go.th/publish/5993.0.html";
                APP_SOFTWARE_HOUSE_EDIT.MultipleRequestSupport = true;
                APP_SOFTWARE_HOUSE_EDIT.OperatingDays = 45;
                APP_SOFTWARE_HOUSE_EDIT.OperatingDayType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_EDIT.OperatingDays2 = null;
                APP_SOFTWARE_HOUSE_EDIT.OperatingCost = 0;
                APP_SOFTWARE_HOUSE_EDIT.OperatingCostType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_EDIT.OperatingCost2 = null;
                APP_SOFTWARE_HOUSE_EDIT.CitizenOperatingDays = 45;
                APP_SOFTWARE_HOUSE_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_EDIT.CitizenOperatingDays2 = null;
                APP_SOFTWARE_HOUSE_EDIT.CitizenOperatingCost = 0;
                APP_SOFTWARE_HOUSE_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_SOFTWARE_HOUSE_EDIT.CitizenOperatingCost2 = null;
                APP_SOFTWARE_HOUSE_EDIT.ShowRemark = true;
                APP_SOFTWARE_HOUSE_EDIT.Remark = "<span style=\"color: red; \">ระยะเวลาการพิจารณาภายใน 45 วัน นับแต่วันที่ได้รับเอกสารถูกต้องครบถ้วนจากผู้ประกอบการจดทะเบียน ภายหลังการสาธิตโปรแกรม ณ กรมสรรพากร ซึ่งเป็นไปตามหลักเกณฑ์ วิธีการ และเงื่อนไขที่กรมสรรพากรกำหนด </span>"; ;
                APP_SOFTWARE_HOUSE_EDIT.CitizenShowRemark = true;
                APP_SOFTWARE_HOUSE_EDIT.CitizenRemark = "<span style=\"color: red; \">ระยะเวลาการพิจารณาภายใน 45 วัน นับแต่วันที่ได้รับเอกสารถูกต้องครบถ้วนจากผู้ประกอบการจดทะเบียน ภายหลังการสาธิตโปรแกรม ณ กรมสรรพากร ซึ่งเป็นไปตามหลักเกณฑ์ วิธีการ และเงื่อนไขที่กรมสรรพากรกำหนด </span>";
                APP_SOFTWARE_HOUSE_EDIT.SingleFormEnabled = true;
                APP_SOFTWARE_HOUSE_EDIT.StatusSequence = "CHECK,PENDING,COMPLETED";
                string TranslateName = "ขอแก้ไขเลขประจำตัวซอฟต์แวร์เฮ้าส์";

                updateApplication(context, APP_SOFTWARE_HOUSE_EDIT, TranslateName, creater);
            }
            #endregion

            #endregion

            #region [ 42 ] APP CLINIC

            #region [NEW]
            Application APP_CLINIC = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_CLINIC).FirstOrDefault();

            if (APP_CLINIC == null)
            {
                APP_CLINIC = new Application();
                APP_CLINIC.ApplicationSysName = ApplicationSysName.APP_CLINIC;
                APP_CLINIC.AppsHookClassName = "BizPortal.AppsHook.HSSClinicNewAppHook";
                APP_CLINIC.OrgCode = "19007000";
                APP_CLINIC.LogoFileID = null;
                APP_CLINIC.MultipleRequestSupport = true;
                APP_CLINIC.HandbookUrl = "https://info.go.th/#!/th/search/6660/คลินิก/";
                APP_CLINIC.CitizenHandbookUrl = "https://info.go.th/#!/th/search/6660/คลินิก/";
                APP_CLINIC.OperatingDays = 67;
                APP_CLINIC.OperatingDayType = CostType.Fixed;
                APP_CLINIC.OperatingDays2 = null;
                APP_CLINIC.OperatingCost = 1000;
                APP_CLINIC.OperatingCostType = CostType.Fixed;
                APP_CLINIC.OperatingCost2 = null;
                APP_CLINIC.CitizenOperatingDays = 67;
                APP_CLINIC.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC.CitizenOperatingDays2 = null;
                APP_CLINIC.CitizenOperatingCost = 1000;
                APP_CLINIC.CitizenOperatingCostType = CostType.Fixed;
                APP_CLINIC.CitizenOperatingCost2 = null;
                APP_CLINIC.ShowRemark = true;
                APP_CLINIC.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span></br><span style=\"color: red; \">*ค่าธรรมเนียมข้างต้นยังไม่รวมค่าดำเนินการ 250 บาท</span>";
                APP_CLINIC.CitizenShowRemark = true;
                APP_CLINIC.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span></br><span style=\"color: red; \">*ค่าธรรมเนียมข้างต้นยังไม่รวมค่าดำเนินการ 250 บาท</span>";
                APP_CLINIC.SingleFormEnabled = true;
                string TranslateName = "ขอใบอนุญาตประกอบกิจการสถานพยาบาล (คลินิก)";

                createApplication(context, APP_CLINIC, TranslateName, creater);
            }
            else
            {
                APP_CLINIC.AppsHookClassName = "BizPortal.AppsHook.HSSClinicNewAppHook";
                APP_CLINIC.ShowRemark = true;
                APP_CLINIC.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span></br><span style=\"color: red; \">*ค่าธรรมเนียมข้างต้นยังไม่รวมค่าดำเนินการ 250 บาท</span>";
                APP_CLINIC.CitizenShowRemark = true;
                APP_CLINIC.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span></br><span style=\"color: red; \">*ค่าธรรมเนียมข้างต้นยังไม่รวมค่าดำเนินการ 250 บาท</span>";
                context.SaveChanges(false);

                string TranslateName = "ขอใบอนุญาตประกอบกิจการสถานพยาบาล (คลินิก)";
                updateApplication(context, APP_CLINIC, TranslateName, creater);
            }
            #endregion

            #region [RENEW]
            Application APP_CLINIC_RENEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_CLINIC_RENEW).FirstOrDefault();

            if (APP_CLINIC_RENEW == null)
            {
                APP_CLINIC_RENEW = new Application();
                APP_CLINIC_RENEW.ApplicationSysName = ApplicationSysName.APP_CLINIC_RENEW;
                APP_CLINIC_RENEW.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_CLINIC_RENEW.OrgCode = "19007000";
                APP_CLINIC_RENEW.LogoFileID = null;
                APP_CLINIC_RENEW.MultipleRequestSupport = true;
                APP_CLINIC_RENEW.HandbookUrl = "https://info.go.th/#!/th/search/7625/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_RENEW.CitizenHandbookUrl = "https://info.go.th/#!/th/search/7625/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_RENEW.OperatingDays = 21;
                APP_CLINIC_RENEW.OperatingDayType = CostType.Fixed;
                APP_CLINIC_RENEW.OperatingDays2 = null;
                APP_CLINIC_RENEW.OperatingCost = 250;
                APP_CLINIC_RENEW.OperatingCostType = CostType.Range;
                APP_CLINIC_RENEW.OperatingCost2 = 1000;
                APP_CLINIC_RENEW.CitizenOperatingDays = 21;
                APP_CLINIC_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_RENEW.CitizenOperatingDays2 = null;
                APP_CLINIC_RENEW.CitizenOperatingCost = 250;
                APP_CLINIC_RENEW.CitizenOperatingCostType = CostType.Range;
                APP_CLINIC_RENEW.CitizenOperatingCost2 = 1000;
                APP_CLINIC_RENEW.ShowRemark = true;
                APP_CLINIC_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_CLINIC_RENEW.CitizenShowRemark = true;
                APP_CLINIC_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_CLINIC_RENEW.SingleFormEnabled = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาลหรือใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)";

                createApplication(context, APP_CLINIC_RENEW, TranslateName, creater);
            }
            else
            {
                APP_CLINIC_RENEW.ShowRemark = true;
                APP_CLINIC_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_CLINIC_RENEW.CitizenShowRemark = true;
                APP_CLINIC_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                context.SaveChanges(false);

                string TranslateName = "ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาลหรือใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)";
                updateApplication(context, APP_CLINIC_RENEW, TranslateName, creater);
            }
            #endregion

            #region [EDIT]
            Application APP_CLINIC_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_CLINIC_EDIT).FirstOrDefault();

            if (APP_CLINIC_EDIT == null)
            {
                APP_CLINIC_EDIT = new Application();
                APP_CLINIC_EDIT.ApplicationSysName = ApplicationSysName.APP_CLINIC_EDIT;
                APP_CLINIC_EDIT.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_CLINIC_EDIT.OrgCode = "19007000";
                APP_CLINIC_EDIT.LogoFileID = null;
                APP_CLINIC_EDIT.MultipleRequestSupport = true;
                APP_CLINIC_EDIT.HandbookUrl = "https://info.go.th/#!/th/search/7787/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_EDIT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/7787/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_EDIT.OperatingDays = 21;
                APP_CLINIC_EDIT.OperatingDayType = CostType.Fixed;
                APP_CLINIC_EDIT.OperatingDays2 = null;
                APP_CLINIC_EDIT.OperatingCost = 100;
                APP_CLINIC_EDIT.OperatingCostType = CostType.Fixed;
                APP_CLINIC_EDIT.OperatingCost2 = null;
                APP_CLINIC_EDIT.CitizenOperatingDays = 21;
                APP_CLINIC_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_EDIT.CitizenOperatingDays2 = null;
                APP_CLINIC_EDIT.CitizenOperatingCost = 100;
                APP_CLINIC_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_CLINIC_EDIT.CitizenOperatingCost2 = null;
                APP_CLINIC_EDIT.ShowRemark = true;
                APP_CLINIC_EDIT.Remark = REMARK_BKK + "<br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_CLINIC_EDIT.CitizenShowRemark = true;
                APP_CLINIC_EDIT.CitizenRemark = REMARK_BKK + "<br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_CLINIC_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาลและใบอนุญาตให้ดำเนินการสถานพยาบาลหรือเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (คลินิก)";

                createApplication(context, APP_CLINIC_EDIT, TranslateName, creater);
            }
            else
            {
                string TranslateName = "ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาลและใบอนุญาตให้ดำเนินการสถานพยาบาลหรือเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (คลินิก)";
                updateApplication(context, APP_CLINIC_EDIT, TranslateName, creater);
            }
            #endregion

            #region [CANCEL]
            Application APP_CLINIC_CANCEL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_CLINIC_CANCEL).FirstOrDefault();

            if (APP_CLINIC_CANCEL == null)
            {
                APP_CLINIC_CANCEL = new Application();
                APP_CLINIC_CANCEL.ApplicationSysName = ApplicationSysName.APP_CLINIC_CANCEL;
                APP_CLINIC_CANCEL.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_CLINIC_CANCEL.OrgCode = "19007000";
                APP_CLINIC_CANCEL.LogoFileID = null;
                APP_CLINIC_CANCEL.MultipleRequestSupport = true;
                APP_CLINIC_CANCEL.HandbookUrl = "https://info.go.th/#!/th/search/8523/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_CANCEL.CitizenHandbookUrl = "https://info.go.th/#!/th/search/8523/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_CANCEL.OperatingDays = 7;
                APP_CLINIC_CANCEL.OperatingDayType = CostType.Fixed;
                APP_CLINIC_CANCEL.OperatingDays2 = null;
                APP_CLINIC_CANCEL.OperatingCost = 0;
                APP_CLINIC_CANCEL.OperatingCostType = CostType.Fixed;
                APP_CLINIC_CANCEL.OperatingCost2 = null;
                APP_CLINIC_CANCEL.CitizenOperatingDays = 7;
                APP_CLINIC_CANCEL.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_CANCEL.CitizenOperatingDays2 = null;
                APP_CLINIC_CANCEL.CitizenOperatingCost = 0;
                APP_CLINIC_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_CLINIC_CANCEL.CitizenOperatingCost2 = null;
                APP_CLINIC_CANCEL.ShowRemark = true;
                APP_CLINIC_CANCEL.Remark = REMARK_BKK + "<br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าธรรมเนียมย้อนหลัง</span>";
                APP_CLINIC_CANCEL.CitizenShowRemark = true;
                APP_CLINIC_CANCEL.CitizenRemark = REMARK_BKK + "<br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าธรรมเนียมย้อนหลัง</span>";
                APP_CLINIC_CANCEL.SingleFormEnabled = true;
                string TranslateName = "ขอแจ้งเลิกกิจการสถานพยาบาล (คลินิก)";

                createApplication(context, APP_CLINIC_CANCEL, TranslateName, creater);
            }
            else
            {
                string TranslateName = "ขอแจ้งเลิกกิจการสถานพยาบาล (คลินิก)";
                updateApplication(context, APP_CLINIC_CANCEL, TranslateName, creater);
            }
            #endregion

            #endregion

            #region [ 43 ] APP ACCOUNTING SERVICE

            #region [ NEW ]

            Application APP_ACCOUNTING_SERVICE = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ACCOUNTING_SERVICE).FirstOrDefault();

            if (APP_ACCOUNTING_SERVICE == null)
            {
                APP_ACCOUNTING_SERVICE = new Application();
                APP_ACCOUNTING_SERVICE.ApplicationSysName = ApplicationSysName.APP_ACCOUNTING_SERVICE;
                APP_ACCOUNTING_SERVICE.AppsHookClassName = "BizPortal.AppsHook.TFACAccountingNewAppHook";
                APP_ACCOUNTING_SERVICE.OrgCode = "12014000";
                APP_ACCOUNTING_SERVICE.LogoFileID = null;
                APP_ACCOUNTING_SERVICE.HandbookUrl = "https://info.go.th/#!/th/search/7102/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%88%E0%B8%94%E0%B8%97%E0%B8%B0%E0%B9%80%E0%B8%9A%E0%B8%B5%E0%B8%A2%E0%B8%99%E0%B8%99%E0%B8%B4%E0%B8%95%E0%B8%B4%E0%B8%9A%E0%B8%B8%E0%B8%84%E0%B8%84%E0%B8%A5%E0%B8%95%E0%B8%B2%E0%B8%A1%E0%B8%A1%E0%B8%B2%E0%B8%95%E0%B8%A3%E0%B8%B2%2011/";
                APP_ACCOUNTING_SERVICE.CitizenHandbookUrl = "https://info.go.th/#!/th/search/7102/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%88%E0%B8%94%E0%B8%97%E0%B8%B0%E0%B9%80%E0%B8%9A%E0%B8%B5%E0%B8%A2%E0%B8%99%E0%B8%99%E0%B8%B4%E0%B8%95%E0%B8%B4%E0%B8%9A%E0%B8%B8%E0%B8%84%E0%B8%84%E0%B8%A5%E0%B8%95%E0%B8%B2%E0%B8%A1%E0%B8%A1%E0%B8%B2%E0%B8%95%E0%B8%A3%E0%B8%B2%2011/";
                APP_ACCOUNTING_SERVICE.OperatingDays = 30;
                APP_ACCOUNTING_SERVICE.OperatingDayType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE.OperatingDays2 = null;
                APP_ACCOUNTING_SERVICE.OperatingCost = 2000;
                APP_ACCOUNTING_SERVICE.OperatingCostType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE.OperatingCost2 = null;
                APP_ACCOUNTING_SERVICE.CitizenOperatingDays = null;
                APP_ACCOUNTING_SERVICE.CitizenOperatingDayType = null;
                APP_ACCOUNTING_SERVICE.CitizenOperatingDays2 = null;
                APP_ACCOUNTING_SERVICE.CitizenOperatingCost = null;
                APP_ACCOUNTING_SERVICE.CitizenOperatingCostType = null;
                APP_ACCOUNTING_SERVICE.CitizenOperatingCost2 = null;
                APP_ACCOUNTING_SERVICE.ShowRemark = true;
                APP_ACCOUNTING_SERVICE.Remark = "<span style=\"color: red; \">ระยะเวลาดำเนินการ 30 วันทำการ นับจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ ได้รับข้อมูลตามแบบฟอร์มและเอกสารครบถ้วนแล้ว ทั้งนี้รวมถึงระยะเวลาการออกหนังสือรับรองนิติบุคคลด้วย</span>";
                APP_ACCOUNTING_SERVICE.CitizenShowRemark = true;
                APP_ACCOUNTING_SERVICE.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชีได้</span>";
                APP_ACCOUNTING_SERVICE.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_ACCOUNTING_SERVICE.SingleFormEnabled = true;
                string TranslateName = "ขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชี";

                createApplication(context, APP_ACCOUNTING_SERVICE, TranslateName, creater);
            }
            else
            {
                APP_ACCOUNTING_SERVICE.ApplicationSysName = ApplicationSysName.APP_ACCOUNTING_SERVICE;
                APP_ACCOUNTING_SERVICE.AppsHookClassName = "BizPortal.AppsHook.TFACAccountingNewAppHook";
                APP_ACCOUNTING_SERVICE.OrgCode = "12014000";
                APP_ACCOUNTING_SERVICE.LogoFileID = null;
                APP_ACCOUNTING_SERVICE.HandbookUrl = "https://info.go.th/#!/th/search/7102/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%88%E0%B8%94%E0%B8%97%E0%B8%B0%E0%B9%80%E0%B8%9A%E0%B8%B5%E0%B8%A2%E0%B8%99%E0%B8%99%E0%B8%B4%E0%B8%95%E0%B8%B4%E0%B8%9A%E0%B8%B8%E0%B8%84%E0%B8%84%E0%B8%A5%E0%B8%95%E0%B8%B2%E0%B8%A1%E0%B8%A1%E0%B8%B2%E0%B8%95%E0%B8%A3%E0%B8%B2%2011/";
                APP_ACCOUNTING_SERVICE.CitizenHandbookUrl = "https://info.go.th/#!/th/search/7102/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%88%E0%B8%94%E0%B8%97%E0%B8%B0%E0%B9%80%E0%B8%9A%E0%B8%B5%E0%B8%A2%E0%B8%99%E0%B8%99%E0%B8%B4%E0%B8%95%E0%B8%B4%E0%B8%9A%E0%B8%B8%E0%B8%84%E0%B8%84%E0%B8%A5%E0%B8%95%E0%B8%B2%E0%B8%A1%E0%B8%A1%E0%B8%B2%E0%B8%95%E0%B8%A3%E0%B8%B2%2011/";
                APP_ACCOUNTING_SERVICE.OperatingDays = 30;
                APP_ACCOUNTING_SERVICE.OperatingDayType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE.OperatingDays2 = null;
                APP_ACCOUNTING_SERVICE.OperatingCost = 2000;
                APP_ACCOUNTING_SERVICE.OperatingCostType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE.OperatingCost2 = null;
                APP_ACCOUNTING_SERVICE.CitizenOperatingDays = null;
                APP_ACCOUNTING_SERVICE.CitizenOperatingDayType = null;
                APP_ACCOUNTING_SERVICE.CitizenOperatingDays2 = null;
                APP_ACCOUNTING_SERVICE.CitizenOperatingCost = null;
                APP_ACCOUNTING_SERVICE.CitizenOperatingCostType = null;
                APP_ACCOUNTING_SERVICE.CitizenOperatingCost2 = null;
                APP_ACCOUNTING_SERVICE.ShowRemark = true;
                APP_ACCOUNTING_SERVICE.Remark = "<span style=\"color: red; \">ระยะเวลาดำเนินการ 30 วันทำการ นับจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ ได้รับข้อมูลตามแบบฟอร์มและเอกสารครบถ้วนแล้ว ทั้งนี้รวมถึงระยะเวลาการออกหนังสือรับรองนิติบุคคลด้วย</span>";
                APP_ACCOUNTING_SERVICE.CitizenShowRemark = true;
                APP_ACCOUNTING_SERVICE.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชีได้</span>";
                APP_ACCOUNTING_SERVICE.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_ACCOUNTING_SERVICE.SingleFormEnabled = true;
                string TranslateName = "ขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชี";

                updateApplication(context, APP_ACCOUNTING_SERVICE, TranslateName, creater);
            }

            #endregion

            #region [ RENEW ]

            #region [ TYPE 1 ]

            Application APP_ACCOUNTING_SERVICE_RENEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ACCOUNTING_SERVICE_RENEW).FirstOrDefault();
            if (APP_ACCOUNTING_SERVICE_RENEW == null)
            {
                APP_ACCOUNTING_SERVICE_RENEW = new Application();
                APP_ACCOUNTING_SERVICE_RENEW.ApplicationSysName = ApplicationSysName.APP_ACCOUNTING_SERVICE_RENEW;
                APP_ACCOUNTING_SERVICE_RENEW.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_ACCOUNTING_SERVICE_RENEW.OrgCode = "12014000";
                APP_ACCOUNTING_SERVICE_RENEW.LogoFileID = null;
                APP_ACCOUNTING_SERVICE_RENEW.HandbookUrl = "https://info.go.th/#!/th/search/7104/การต่ออายุนิติบุคคลตามมาตรา%2011/";
                APP_ACCOUNTING_SERVICE_RENEW.CitizenHandbookUrl = "";
                APP_ACCOUNTING_SERVICE_RENEW.OperatingDays = 7;
                APP_ACCOUNTING_SERVICE_RENEW.OperatingDayType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_RENEW.OperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_RENEW.OperatingCost = 400;
                APP_ACCOUNTING_SERVICE_RENEW.OperatingCostType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_RENEW.OperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenOperatingDays = null;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenOperatingDayType = null;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenOperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenOperatingCost = null;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenOperatingCostType = null;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenOperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_RENEW.ShowRemark = true;
                APP_ACCOUNTING_SERVICE_RENEW.Remark = "<span style=\"color: red; \">ระยะเวลาดำเนินการ 7 วันทำการ นับจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ ได้รับข้อมูลตามแบบฟอร์มและเอกสารครบถ้วนแล้ว</span>";
                APP_ACCOUNTING_SERVICE_RENEW.CitizenShowRemark = true;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชีได้</span>";
                APP_ACCOUNTING_SERVICE_RENEW.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_ACCOUNTING_SERVICE_RENEW.SingleFormEnabled = true;
                string TranslateName = "ขอแจ้งรายละเอียดเกี่ยวกับหลักประกันเพื่อประกันความรับผิดชอบต่อบุคคลที่สาม";

                createApplication(context, APP_ACCOUNTING_SERVICE_RENEW, TranslateName, creater);
            }
            else
            {
                APP_ACCOUNTING_SERVICE_RENEW.ApplicationSysName = ApplicationSysName.APP_ACCOUNTING_SERVICE_RENEW;
                APP_ACCOUNTING_SERVICE_RENEW.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_ACCOUNTING_SERVICE_RENEW.OrgCode = "12014000";
                APP_ACCOUNTING_SERVICE_RENEW.LogoFileID = null;
                APP_ACCOUNTING_SERVICE_RENEW.HandbookUrl = "https://info.go.th/#!/th/search/7104/การต่ออายุนิติบุคคลตามมาตรา%2011/";
                APP_ACCOUNTING_SERVICE_RENEW.CitizenHandbookUrl = "";
                APP_ACCOUNTING_SERVICE_RENEW.OperatingDays = 7;
                APP_ACCOUNTING_SERVICE_RENEW.OperatingDayType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_RENEW.OperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_RENEW.OperatingCost = 400;
                APP_ACCOUNTING_SERVICE_RENEW.OperatingCostType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_RENEW.OperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenOperatingDays = null;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenOperatingDayType = null;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenOperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenOperatingCost = null;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenOperatingCostType = null;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenOperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_RENEW.ShowRemark = true;
                APP_ACCOUNTING_SERVICE_RENEW.Remark = "<span style=\"color: red; \">ระยะเวลาดำเนินการ 7 วันทำการ นับจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ ได้รับข้อมูลตามแบบฟอร์มและเอกสารครบถ้วนแล้ว</span>";
                APP_ACCOUNTING_SERVICE_RENEW.CitizenShowRemark = true;
                APP_ACCOUNTING_SERVICE_RENEW.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชีได้</span>";
                APP_ACCOUNTING_SERVICE_RENEW.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_ACCOUNTING_SERVICE_RENEW.SingleFormEnabled = true;
                string TranslateName = "ขอแจ้งรายละเอียดเกี่ยวกับหลักประกันเพื่อประกันความรับผิดชอบต่อบุคคลที่สาม";

                updateApplication(context, APP_ACCOUNTING_SERVICE_RENEW, TranslateName, creater);
            }

            #endregion

            #region [ TYPE 2 ]

            Application APP_ACCOUNTING_SERVICE_RENEW_TYPE_2 = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2).FirstOrDefault();
            if (APP_ACCOUNTING_SERVICE_RENEW_TYPE_2 == null)
            {
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2 = new Application();
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.ApplicationSysName = ApplicationSysName.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OrgCode = "12014000";
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.LogoFileID = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.HandbookUrl = "https://info.go.th/#!/th/search/7104/การต่ออายุนิติบุคคลตามมาตรา%2011/";
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenHandbookUrl = "";
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OperatingDays = 30;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OperatingDayType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OperatingCost = 2000;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OperatingCostType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenOperatingDays = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenOperatingDayType = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenOperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenOperatingCost = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenOperatingCostType = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenOperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.ShowRemark = true;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.Remark = "<span style=\"color: red; \">ระยะเวลาดำเนินการ 30 วันทำการ นับจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ ได้รับข้อมูลตามแบบฟอร์มและเอกสารครบถ้วนแล้ว ทั้งนี้รวมถึงระยะเวลาการออกหนังสือรับรองนิติบุคคลด้วย</span>";
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenShowRemark = true;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชีได้</span>";
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.SingleFormEnabled = true;
                string TranslateName = "ขอต่ออายุทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชี";

                createApplication(context, APP_ACCOUNTING_SERVICE_RENEW_TYPE_2, TranslateName, creater);
            }
            else
            {
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.ApplicationSysName = ApplicationSysName.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OrgCode = "12014000";
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.LogoFileID = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.HandbookUrl = "https://info.go.th/#!/th/search/7104/การต่ออายุนิติบุคคลตามมาตรา%2011/";
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenHandbookUrl = "";
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OperatingDays = 30;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OperatingDayType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OperatingCost = 2000;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OperatingCostType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.OperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenOperatingDays = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenOperatingDayType = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenOperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenOperatingCost = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenOperatingCostType = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenOperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.ShowRemark = true;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.Remark = "<span style=\"color: red; \">ระยะเวลาดำเนินการ 30 วันทำการ นับจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ ได้รับข้อมูลตามแบบฟอร์มและเอกสารครบถ้วนแล้ว ทั้งนี้รวมถึงระยะเวลาการออกหนังสือรับรองนิติบุคคลด้วย</span>";
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenShowRemark = true;
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชีได้</span>";
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_ACCOUNTING_SERVICE_RENEW_TYPE_2.SingleFormEnabled = true;
                string TranslateName = "ขอต่ออายุทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชี";

                updateApplication(context, APP_ACCOUNTING_SERVICE_RENEW_TYPE_2, TranslateName, creater);
            }

            #endregion

            #endregion

            #region [ EDIT ]

            #region [ TYPE 1 ]

            Application APP_ACCOUNTING_SERVICE_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ACCOUNTING_SERVICE_EDIT).FirstOrDefault();
            if (APP_ACCOUNTING_SERVICE_EDIT == null)
            {
                APP_ACCOUNTING_SERVICE_EDIT = new Application();
                APP_ACCOUNTING_SERVICE_EDIT.ApplicationSysName = ApplicationSysName.APP_ACCOUNTING_SERVICE_EDIT;
                APP_ACCOUNTING_SERVICE_EDIT.AppsHookClassName = "BizPortal.AppsHook.TFACAccountingEditAppHook";
                APP_ACCOUNTING_SERVICE_EDIT.OrgCode = "12014000";
                APP_ACCOUNTING_SERVICE_EDIT.LogoFileID = null;
                APP_ACCOUNTING_SERVICE_EDIT.HandbookUrl = "http://www.tfac.or.th/Article/Detail/66930";
                APP_ACCOUNTING_SERVICE_EDIT.CitizenHandbookUrl = "http://www.tfac.or.th/Article/Detail/66930";
                APP_ACCOUNTING_SERVICE_EDIT.OperatingDays = 7;
                APP_ACCOUNTING_SERVICE_EDIT.OperatingDayType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_EDIT.OperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_EDIT.OperatingCost = 500;
                APP_ACCOUNTING_SERVICE_EDIT.OperatingCostType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_EDIT.OperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenOperatingDays = null;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenOperatingDayType = null;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenOperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenOperatingCost = null;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenOperatingCostType = null;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenOperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_EDIT.ShowRemark = true;
                APP_ACCOUNTING_SERVICE_EDIT.Remark = "<span style=\"color: red; \">ระยะเวลาดำเนินการ 7 วันทำการ นับจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ ได้รับข้อมูลตามแบบฟอร์มและเอกสารครบถ้วนแล้ว</span>";
                APP_ACCOUNTING_SERVICE_EDIT.CitizenShowRemark = true;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชีได้</span>";
                APP_ACCOUNTING_SERVICE_EDIT.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_ACCOUNTING_SERVICE_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชี";

                createApplication(context, APP_ACCOUNTING_SERVICE_EDIT, TranslateName, creater);
            }
            else
            {
                APP_ACCOUNTING_SERVICE_EDIT.ApplicationSysName = ApplicationSysName.APP_ACCOUNTING_SERVICE_EDIT;
                APP_ACCOUNTING_SERVICE_EDIT.AppsHookClassName = "BizPortal.AppsHook.TFACAccountingEditAppHook";
                APP_ACCOUNTING_SERVICE_EDIT.OrgCode = "12014000";
                APP_ACCOUNTING_SERVICE_EDIT.LogoFileID = null;
                APP_ACCOUNTING_SERVICE_EDIT.HandbookUrl = "http://www.tfac.or.th/Article/Detail/66930";
                APP_ACCOUNTING_SERVICE_EDIT.CitizenHandbookUrl = "http://www.tfac.or.th/Article/Detail/66930";
                APP_ACCOUNTING_SERVICE_EDIT.OperatingDays = 7;
                APP_ACCOUNTING_SERVICE_EDIT.OperatingDayType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_EDIT.OperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_EDIT.OperatingCost = 500;
                APP_ACCOUNTING_SERVICE_EDIT.OperatingCostType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_EDIT.OperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenOperatingDays = null;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenOperatingDayType = null;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenOperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenOperatingCost = null;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenOperatingCostType = null;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenOperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_EDIT.ShowRemark = true;
                APP_ACCOUNTING_SERVICE_EDIT.Remark = "<span style=\"color: red; \">ระยะเวลาดำเนินการ 7 วันทำการ นับจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ ได้รับข้อมูลตามแบบฟอร์มและเอกสารครบถ้วนแล้ว</span>";
                APP_ACCOUNTING_SERVICE_EDIT.CitizenShowRemark = true;
                APP_ACCOUNTING_SERVICE_EDIT.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชีได้</span>";
                APP_ACCOUNTING_SERVICE_EDIT.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_ACCOUNTING_SERVICE_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชี";

                updateApplication(context, APP_ACCOUNTING_SERVICE_EDIT, TranslateName, creater);
            }

            #endregion

            #region [ TYPE 2 ]

            Application APP_ACCOUNTING_SERVICE_EDIT_TYPE_2 = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2).FirstOrDefault();
            if (APP_ACCOUNTING_SERVICE_EDIT_TYPE_2 == null)
            {
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2 = new Application();
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.ApplicationSysName = ApplicationSysName.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.AppsHookClassName = "BizPortal.AppsHook.TFACAccountingEditAppHook";
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OrgCode = "12014000";
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.LogoFileID = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.HandbookUrl = "http://www.tfac.or.th/Article/Detail/66930";
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenHandbookUrl = "http://www.tfac.or.th/Article/Detail/66930";
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OperatingDays = 7;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OperatingDayType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OperatingCost = 200;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OperatingCostType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenOperatingDays = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenOperatingDayType = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenOperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenOperatingCost = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenOperatingCostType = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenOperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.ShowRemark = true;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.Remark = "<span style=\"color: red; \">ระยะเวลาดำเนินการ 7 วันทำการ นับจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ ได้รับข้อมูลตามแบบฟอร์มและเอกสารครบถ้วนแล้ว</span>";
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenShowRemark = true;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชีได้</span>";
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.SingleFormEnabled = true;
                string TranslateName = "ขอแจ้งเปลี่ยนแปลงประเภทของหลักประกันเพื่อประกันความรับผิดชอบต่อบุคคลที่สาม";

                createApplication(context, APP_ACCOUNTING_SERVICE_EDIT_TYPE_2, TranslateName, creater);
            }
            else
            {
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.ApplicationSysName = ApplicationSysName.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.AppsHookClassName = "BizPortal.AppsHook.TFACAccountingEditAppHook";
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OrgCode = "12014000";
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.LogoFileID = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.HandbookUrl = "http://www.tfac.or.th/Article/Detail/66930";
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenHandbookUrl = "http://www.tfac.or.th/Article/Detail/66930";
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OperatingDays = 7;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OperatingDayType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OperatingCost = 200;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OperatingCostType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.OperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenOperatingDays = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenOperatingDayType = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenOperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenOperatingCost = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenOperatingCostType = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenOperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.ShowRemark = true;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.Remark = "<span style=\"color: red; \">ระยะเวลาดำเนินการ 7 วันทำการ นับจากสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ ได้รับข้อมูลตามแบบฟอร์มและเอกสารครบถ้วนแล้ว</span>";
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenShowRemark = true;
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชีได้</span>";
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_ACCOUNTING_SERVICE_EDIT_TYPE_2.SingleFormEnabled = true;
                string TranslateName = "ขอแจ้งเปลี่ยนแปลงประเภทของหลักประกันเพื่อประกันความรับผิดชอบต่อบุคคลที่สาม";

                updateApplication(context, APP_ACCOUNTING_SERVICE_EDIT_TYPE_2, TranslateName, creater);
            }

            #endregion

            #endregion

            #region [ CANCEL ]

            Application APP_ACCOUNTING_SERVICE_CANCEL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ACCOUNTING_SERVICE_CANCEL).FirstOrDefault();
            if (APP_ACCOUNTING_SERVICE_CANCEL == null)
            {
                APP_ACCOUNTING_SERVICE_CANCEL = new Application();
                APP_ACCOUNTING_SERVICE_CANCEL.ApplicationSysName = ApplicationSysName.APP_ACCOUNTING_SERVICE_CANCEL;
                APP_ACCOUNTING_SERVICE_CANCEL.AppsHookClassName = "BizPortal.AppsHook.TFACAccountingCancelAppHook";
                APP_ACCOUNTING_SERVICE_CANCEL.OrgCode = "12014000";
                APP_ACCOUNTING_SERVICE_CANCEL.LogoFileID = null;
                APP_ACCOUNTING_SERVICE_CANCEL.HandbookUrl = "http://www.tfac.or.th/Article/Detail/66930";
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenHandbookUrl = "http://www.tfac.or.th/Article/Detail/66930";
                APP_ACCOUNTING_SERVICE_CANCEL.OperatingDays = 7;
                APP_ACCOUNTING_SERVICE_CANCEL.OperatingDayType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_CANCEL.OperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_CANCEL.OperatingCost = 500;
                APP_ACCOUNTING_SERVICE_CANCEL.OperatingCostType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_CANCEL.OperatingCost2 = 0;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenOperatingDays = null;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenOperatingDayType = null;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenOperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenOperatingCost = null;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenOperatingCostType = null;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenOperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_CANCEL.ShowRemark = true;
                APP_ACCOUNTING_SERVICE_CANCEL.Remark = "<span style=\"color: red; \">7 วันทำการนับจากวันที่ท่านได้ยื่นเอกสารครบถ้วนต่อสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ โดยจะดำเนินการทันทีหลังจากที่กรอกข้อมูลและเอกสารครบถ้วน</span>";
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenShowRemark = true;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชีได้</span>";
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_ACCOUNTING_SERVICE_CANCEL.SingleFormEnabled = true;
                string TranslateName = "ขอยกเลิกทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชี";

                createApplication(context, APP_ACCOUNTING_SERVICE_CANCEL, TranslateName, creater);
            }
            else
            {
                APP_ACCOUNTING_SERVICE_CANCEL.ApplicationSysName = ApplicationSysName.APP_ACCOUNTING_SERVICE_CANCEL;
                APP_ACCOUNTING_SERVICE_CANCEL.AppsHookClassName = "BizPortal.AppsHook.TFACAccountingCancelAppHook";
                APP_ACCOUNTING_SERVICE_CANCEL.OrgCode = "12014000";
                APP_ACCOUNTING_SERVICE_CANCEL.LogoFileID = null;
                APP_ACCOUNTING_SERVICE_CANCEL.HandbookUrl = "http://www.tfac.or.th/Article/Detail/66930";
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenHandbookUrl = "http://www.tfac.or.th/Article/Detail/66930";
                APP_ACCOUNTING_SERVICE_CANCEL.OperatingDays = 7;
                APP_ACCOUNTING_SERVICE_CANCEL.OperatingDayType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_CANCEL.OperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_CANCEL.OperatingCost = 500;
                APP_ACCOUNTING_SERVICE_CANCEL.OperatingCostType = CostType.Fixed;
                APP_ACCOUNTING_SERVICE_CANCEL.OperatingCost2 = 0;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenOperatingDays = null;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenOperatingDayType = null;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenOperatingDays2 = null;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenOperatingCost = null;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenOperatingCostType = null;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenOperatingCost2 = null;
                APP_ACCOUNTING_SERVICE_CANCEL.ShowRemark = true;
                APP_ACCOUNTING_SERVICE_CANCEL.Remark = "<span style=\"color: red; \">7 วันทำการนับจากวันที่ท่านได้ยื่นเอกสารครบถ้วนต่อสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ โดยจะดำเนินการทันทีหลังจากที่กรอกข้อมูลและเอกสารครบถ้วน</span>";
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenShowRemark = true;
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชีได้</span>";
                APP_ACCOUNTING_SERVICE_CANCEL.CitizenRequestAtOrg = true; // Citizen can't request permit
                APP_ACCOUNTING_SERVICE_CANCEL.SingleFormEnabled = true;
                string TranslateName = "ขอยกเลิกทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชี";

                updateApplication(context, APP_ACCOUNTING_SERVICE_CANCEL, TranslateName, creater);
            }

            #endregion

            #endregion

            #region [ 44 ] APP_FACTORY_TYPE

            #region [ NEW ]

            Application APP_FACTORY_TYPE2 = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_FACTORY_TYPE2).FirstOrDefault();

            if (APP_FACTORY_TYPE2 == null)
            {
                APP_FACTORY_TYPE2 = new Application();
                APP_FACTORY_TYPE2.ApplicationSysName = ApplicationSysName.APP_FACTORY_TYPE2;
                APP_FACTORY_TYPE2.AppsHookClassName = "BizPortal.AppsHook.AppsHookFactoryType2";
                APP_FACTORY_TYPE2.OrgCode = "22000000";
                APP_FACTORY_TYPE2.LogoFileID = null;
                APP_FACTORY_TYPE2.HandbookUrl = "http://www.diw.go.th/hawk/default.php";
                APP_FACTORY_TYPE2.CitizenHandbookUrl = "http://www.diw.go.th/hawk/default.php";
                APP_FACTORY_TYPE2.MultipleRequestSupport = true;
                APP_FACTORY_TYPE2.OperatingDays = 32;
                APP_FACTORY_TYPE2.OperatingDayType = CostType.Fixed;
                APP_FACTORY_TYPE2.OperatingDays2 = null;
                APP_FACTORY_TYPE2.OperatingCost = 0;
                APP_FACTORY_TYPE2.OperatingCostType = CostType.Fixed;
                APP_FACTORY_TYPE2.OperatingCost2 = null;
                APP_FACTORY_TYPE2.CitizenOperatingDays = 32;
                APP_FACTORY_TYPE2.CitizenOperatingDayType = CostType.Fixed;
                APP_FACTORY_TYPE2.CitizenOperatingDays2 = null;
                APP_FACTORY_TYPE2.CitizenOperatingCost = 0;
                APP_FACTORY_TYPE2.CitizenOperatingCostType = CostType.Fixed;
                APP_FACTORY_TYPE2.CitizenOperatingCost2 = null;
                APP_FACTORY_TYPE2.ShowRemark = true;
                APP_FACTORY_TYPE2.Remark = REMARK_BKK;
                APP_FACTORY_TYPE2.CitizenShowRemark = true;
                APP_FACTORY_TYPE2.CitizenRemark = REMARK_BKK;
                APP_FACTORY_TYPE2.SingleFormEnabled = true;
                string TranslateName = "ขอหนังสือแจ้งผลการรับฟังความคิดเห็นของประชาชนในการพิจารณาเกี่ยวกับโรงงานจำพวกที่ 2";

                createApplication(context, APP_FACTORY_TYPE2, TranslateName, creater);
            }
            else
            {
                APP_FACTORY_TYPE2.ApplicationSysName = ApplicationSysName.APP_FACTORY_TYPE2;
                APP_FACTORY_TYPE2.AppsHookClassName = "BizPortal.AppsHook.AppsHookFactoryType2";
                APP_FACTORY_TYPE2.OrgCode = "22000000";
                APP_FACTORY_TYPE2.LogoFileID = null;
                APP_FACTORY_TYPE2.HandbookUrl = "http://www.diw.go.th/hawk/default.php";
                APP_FACTORY_TYPE2.CitizenHandbookUrl = "http://www.diw.go.th/hawk/default.php";
                APP_FACTORY_TYPE2.MultipleRequestSupport = true;
                APP_FACTORY_TYPE2.OperatingDays = 32;
                APP_FACTORY_TYPE2.OperatingDayType = CostType.Fixed;
                APP_FACTORY_TYPE2.OperatingDays2 = null;
                APP_FACTORY_TYPE2.OperatingCost = 0;
                APP_FACTORY_TYPE2.OperatingCostType = CostType.Fixed;
                APP_FACTORY_TYPE2.OperatingCost2 = null;
                APP_FACTORY_TYPE2.CitizenOperatingDays = 32;
                APP_FACTORY_TYPE2.CitizenOperatingDayType = CostType.Fixed;
                APP_FACTORY_TYPE2.CitizenOperatingDays2 = null;
                APP_FACTORY_TYPE2.CitizenOperatingCost = 0;
                APP_FACTORY_TYPE2.CitizenOperatingCostType = CostType.Fixed;
                APP_FACTORY_TYPE2.CitizenOperatingCost2 = null;
                APP_FACTORY_TYPE2.ShowRemark = true;
                APP_FACTORY_TYPE2.Remark = REMARK_BKK;
                APP_FACTORY_TYPE2.CitizenShowRemark = true;
                APP_FACTORY_TYPE2.CitizenRemark = REMARK_BKK;
                APP_FACTORY_TYPE2.SingleFormEnabled = true;
                string TranslateName = "ขอหนังสือแจ้งผลการรับฟังความคิดเห็นของประชาชนในการพิจารณาเกี่ยวกับโรงงานจำพวกที่ 2";

                updateApplication(context, APP_FACTORY_TYPE2, TranslateName, creater);
            }
            #endregion

            #endregion

            #region [45] APP_FACTORY_CLASS_2
            #region --New--
            Application APP_FACTORY_CLASS_2_NEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_FACTORY_CLASS_2_NEW).FirstOrDefault();
            if (APP_FACTORY_CLASS_2_NEW == null)
            {
                APP_FACTORY_CLASS_2_NEW = new Application();
                APP_FACTORY_CLASS_2_NEW.ApplicationSysName = ApplicationSysName.APP_FACTORY_CLASS_2_NEW;
                APP_FACTORY_CLASS_2_NEW.AppsHookClassName = "BizPortal.AppsHook.DIWFactory2AppHook";
                APP_FACTORY_CLASS_2_NEW.OrgCode = "20003000";
                APP_FACTORY_CLASS_2_NEW.LogoFileID = null;
                APP_FACTORY_CLASS_2_NEW.HandbookUrl = "https://info.go.th/#!/th/search/7225/%E0%B8%88%E0%B8%B3%E0%B8%9E%E0%B8%A7%E0%B8%81%E0%B8%97%E0%B8%B5%E0%B9%88%202/";  //Juristic
                APP_FACTORY_CLASS_2_NEW.CitizenHandbookUrl = "https://info.go.th/#!/th/search/7225/%E0%B8%88%E0%B8%B3%E0%B8%9E%E0%B8%A7%E0%B8%81%E0%B8%97%E0%B8%B5%E0%B9%88%202/";  //Citizen
                //Juristic
                APP_FACTORY_CLASS_2_NEW.OperatingDays = 25;
                APP_FACTORY_CLASS_2_NEW.OperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_2_NEW.OperatingDays2 = null;
                APP_FACTORY_CLASS_2_NEW.OperatingCost = 300;
                APP_FACTORY_CLASS_2_NEW.OperatingCostType = CostType.Range;
                APP_FACTORY_CLASS_2_NEW.OperatingCost2 = 45000;
                //Citizen
                APP_FACTORY_CLASS_2_NEW.CitizenOperatingDays = 25;
                APP_FACTORY_CLASS_2_NEW.CitizenOperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_2_NEW.CitizenOperatingDays2 = null;
                APP_FACTORY_CLASS_2_NEW.CitizenOperatingCost = 300;
                APP_FACTORY_CLASS_2_NEW.CitizenOperatingCostType = CostType.Range;
                APP_FACTORY_CLASS_2_NEW.CitizenOperatingCost2 = 45000;

                APP_FACTORY_CLASS_2_NEW.ShowRemark = true;  //Juristic
                APP_FACTORY_CLASS_2_NEW.Remark = REMARK_BKK;  //Juristic
                APP_FACTORY_CLASS_2_NEW.CitizenShowRemark = true;  //Citizen
                APP_FACTORY_CLASS_2_NEW.CitizenRemark = REMARK_BKK;  //Citizen
                APP_FACTORY_CLASS_2_NEW.SingleFormEnabled = true;
                string TranslateName = "ขอใบรับแจ้งประกอบกิจการโรงงานจำพวกที่ 2";

                createApplication(context, APP_FACTORY_CLASS_2_NEW, TranslateName, creater);
            }
            else
            {
                APP_FACTORY_CLASS_2_NEW.ApplicationSysName = ApplicationSysName.APP_FACTORY_CLASS_2_NEW;
                APP_FACTORY_CLASS_2_NEW.AppsHookClassName = "BizPortal.AppsHook.DIWFactory2AppHook";
                APP_FACTORY_CLASS_2_NEW.OrgCode = "20003000";
                APP_FACTORY_CLASS_2_NEW.LogoFileID = null;
                APP_FACTORY_CLASS_2_NEW.HandbookUrl = "https://info.go.th/#!/th/search/7225/%E0%B8%88%E0%B8%B3%E0%B8%9E%E0%B8%A7%E0%B8%81%E0%B8%97%E0%B8%B5%E0%B9%88%202/";  //Juristic
                APP_FACTORY_CLASS_2_NEW.CitizenHandbookUrl = "https://info.go.th/#!/th/search/7225/%E0%B8%88%E0%B8%B3%E0%B8%9E%E0%B8%A7%E0%B8%81%E0%B8%97%E0%B8%B5%E0%B9%88%202/";  //Citizen
                //Juristic
                APP_FACTORY_CLASS_2_NEW.OperatingDays = 25;
                APP_FACTORY_CLASS_2_NEW.OperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_2_NEW.OperatingDays2 = null;
                APP_FACTORY_CLASS_2_NEW.OperatingCost = 300;
                APP_FACTORY_CLASS_2_NEW.OperatingCostType = CostType.Range;
                APP_FACTORY_CLASS_2_NEW.OperatingCost2 = 45000;
                //Citizen
                APP_FACTORY_CLASS_2_NEW.CitizenOperatingDays = 25;
                APP_FACTORY_CLASS_2_NEW.CitizenOperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_2_NEW.CitizenOperatingDays2 = null;
                APP_FACTORY_CLASS_2_NEW.CitizenOperatingCost = 300;
                APP_FACTORY_CLASS_2_NEW.CitizenOperatingCostType = CostType.Range;
                APP_FACTORY_CLASS_2_NEW.CitizenOperatingCost2 = 45000;

                APP_FACTORY_CLASS_2_NEW.ShowRemark = true;  //Juristic
                APP_FACTORY_CLASS_2_NEW.Remark = REMARK_BKK;  //Juristic
                APP_FACTORY_CLASS_2_NEW.CitizenShowRemark = true;  //Citizen
                APP_FACTORY_CLASS_2_NEW.CitizenRemark = REMARK_BKK;  //Citizen
                APP_FACTORY_CLASS_2_NEW.SingleFormEnabled = true;
                string TranslateName = "ขอใบรับแจ้งประกอบกิจการโรงงานจำพวกที่ 2";

                updateApplication(context, APP_FACTORY_CLASS_2_NEW, TranslateName, creater);
            }
            #endregion
            #region --Edit--
            Application APP_FACTORY_CLASS_2_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_FACTORY_CLASS_2_EDIT).FirstOrDefault();
            if (APP_FACTORY_CLASS_2_EDIT == null)
            {
                APP_FACTORY_CLASS_2_EDIT = new Application();
                APP_FACTORY_CLASS_2_EDIT.ApplicationSysName = ApplicationSysName.APP_FACTORY_CLASS_2_EDIT;
                APP_FACTORY_CLASS_2_EDIT.AppsHookClassName = "BizPortal.AppsHook.DIWFactory2AppHook";
                APP_FACTORY_CLASS_2_EDIT.OrgCode = "20003000";
                APP_FACTORY_CLASS_2_EDIT.LogoFileID = null;
                APP_FACTORY_CLASS_2_EDIT.HandbookUrl = "https://info.go.th/#!/th/search/7313/%E0%B9%80%E0%B8%9B%E0%B8%A5%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%99%E0%B8%8A%E0%B8%B7%E0%B9%88%E0%B8%AD%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99/";
                APP_FACTORY_CLASS_2_EDIT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/7313/%E0%B9%80%E0%B8%9B%E0%B8%A5%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%99%E0%B8%8A%E0%B8%B7%E0%B9%88%E0%B8%AD%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99/";
                APP_FACTORY_CLASS_2_EDIT.OperatingDays = 30;
                APP_FACTORY_CLASS_2_EDIT.OperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_2_EDIT.OperatingDays2 = null;
                APP_FACTORY_CLASS_2_EDIT.OperatingCost = 0;
                APP_FACTORY_CLASS_2_EDIT.OperatingCostType = CostType.Fixed;
                APP_FACTORY_CLASS_2_EDIT.OperatingCost2 = null;
                APP_FACTORY_CLASS_2_EDIT.CitizenOperatingDays = 30;
                APP_FACTORY_CLASS_2_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_2_EDIT.CitizenOperatingDays2 = null;
                APP_FACTORY_CLASS_2_EDIT.CitizenOperatingCost = 0;
                APP_FACTORY_CLASS_2_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_FACTORY_CLASS_2_EDIT.CitizenOperatingCost2 = null;
                APP_FACTORY_CLASS_2_EDIT.ShowRemark = true;
                APP_FACTORY_CLASS_2_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_FACTORY_CLASS_2_EDIT.CitizenShowRemark = true;
                APP_FACTORY_CLASS_2_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_FACTORY_CLASS_2_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขการแจ้งประกอบกิจการโรงงานจำพวกที่ 2";

                createApplication(context, APP_FACTORY_CLASS_2_EDIT, TranslateName, creater);
            }
            else
            {
                APP_FACTORY_CLASS_2_EDIT.ApplicationSysName = ApplicationSysName.APP_FACTORY_CLASS_2_EDIT;
                APP_FACTORY_CLASS_2_EDIT.AppsHookClassName = "BizPortal.AppsHook.DIWFactory2AppHook";
                APP_FACTORY_CLASS_2_EDIT.OrgCode = "20003000";
                APP_FACTORY_CLASS_2_EDIT.LogoFileID = null;
                APP_FACTORY_CLASS_2_EDIT.HandbookUrl = "https://info.go.th/#!/th/search/7313/%E0%B9%80%E0%B8%9B%E0%B8%A5%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%99%E0%B8%8A%E0%B8%B7%E0%B9%88%E0%B8%AD%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99/";
                APP_FACTORY_CLASS_2_EDIT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/7313/%E0%B9%80%E0%B8%9B%E0%B8%A5%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%99%E0%B8%8A%E0%B8%B7%E0%B9%88%E0%B8%AD%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99/";
                APP_FACTORY_CLASS_2_EDIT.OperatingDays = 30;
                APP_FACTORY_CLASS_2_EDIT.OperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_2_EDIT.OperatingDays2 = null;
                APP_FACTORY_CLASS_2_EDIT.OperatingCost = 0;
                APP_FACTORY_CLASS_2_EDIT.OperatingCostType = CostType.Fixed;
                APP_FACTORY_CLASS_2_EDIT.OperatingCost2 = null;
                APP_FACTORY_CLASS_2_EDIT.CitizenOperatingDays = 30;
                APP_FACTORY_CLASS_2_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_2_EDIT.CitizenOperatingDays2 = null;
                APP_FACTORY_CLASS_2_EDIT.CitizenOperatingCost = 0;
                APP_FACTORY_CLASS_2_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_FACTORY_CLASS_2_EDIT.CitizenOperatingCost2 = null;
                APP_FACTORY_CLASS_2_EDIT.ShowRemark = true;
                APP_FACTORY_CLASS_2_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_FACTORY_CLASS_2_EDIT.CitizenShowRemark = true;
                APP_FACTORY_CLASS_2_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_FACTORY_CLASS_2_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขการแจ้งประกอบกิจการโรงงานจำพวกที่ 2";

                updateApplication(context, APP_FACTORY_CLASS_2_EDIT, TranslateName, creater);
            }
            #endregion
            #region --Cancel--
            Application APP_FACTORY_CLASS_2_CANCEL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_FACTORY_CLASS_2_CANCEL).FirstOrDefault();
            if (APP_FACTORY_CLASS_2_CANCEL == null)
            {
                APP_FACTORY_CLASS_2_CANCEL = new Application();
                APP_FACTORY_CLASS_2_CANCEL.ApplicationSysName = ApplicationSysName.APP_FACTORY_CLASS_2_CANCEL;
                APP_FACTORY_CLASS_2_CANCEL.AppsHookClassName = "BizPortal.AppsHook.DIWFactory2AppHook";
                APP_FACTORY_CLASS_2_CANCEL.OrgCode = "20003000";
                APP_FACTORY_CLASS_2_CANCEL.LogoFileID = null;
                APP_FACTORY_CLASS_2_CANCEL.HandbookUrl = "https://info.go.th/#!/th/search/144/%E0%B9%81%E0%B8%88%E0%B9%89%E0%B8%87%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A/";
                APP_FACTORY_CLASS_2_CANCEL.CitizenHandbookUrl = "https://info.go.th/#!/th/search/144/%E0%B9%81%E0%B8%88%E0%B9%89%E0%B8%87%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A/";
                APP_FACTORY_CLASS_2_CANCEL.OperatingDays = 30;
                APP_FACTORY_CLASS_2_CANCEL.OperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_2_CANCEL.OperatingDays2 = null;
                APP_FACTORY_CLASS_2_CANCEL.OperatingCost = 0;
                APP_FACTORY_CLASS_2_CANCEL.OperatingCostType = CostType.Fixed;
                APP_FACTORY_CLASS_2_CANCEL.OperatingCost2 = null;
                APP_FACTORY_CLASS_2_CANCEL.CitizenOperatingDays = 30;
                APP_FACTORY_CLASS_2_CANCEL.CitizenOperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_2_CANCEL.CitizenOperatingDays2 = null;
                APP_FACTORY_CLASS_2_CANCEL.CitizenOperatingCost = 0;
                APP_FACTORY_CLASS_2_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_FACTORY_CLASS_2_CANCEL.CitizenOperatingCost2 = null;
                APP_FACTORY_CLASS_2_CANCEL.ShowRemark = true;
                APP_FACTORY_CLASS_2_CANCEL.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_FACTORY_CLASS_2_CANCEL.CitizenShowRemark = true;
                APP_FACTORY_CLASS_2_CANCEL.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_FACTORY_CLASS_2_CANCEL.SingleFormEnabled = true;
                string TranslateName = "ขอแจ้งเลิกประกอบกิจการโรงงานจำพวกที่ 2";

                createApplication(context, APP_FACTORY_CLASS_2_CANCEL, TranslateName, creater);
            }
            else
            {
                APP_FACTORY_CLASS_2_CANCEL.ApplicationSysName = ApplicationSysName.APP_FACTORY_CLASS_2_CANCEL;
                APP_FACTORY_CLASS_2_CANCEL.AppsHookClassName = "BizPortal.AppsHook.DIWFactory2AppHook";
                APP_FACTORY_CLASS_2_CANCEL.OrgCode = "20003000";
                APP_FACTORY_CLASS_2_CANCEL.LogoFileID = null;
                APP_FACTORY_CLASS_2_CANCEL.HandbookUrl = "https://info.go.th/#!/th/search/144/%E0%B9%81%E0%B8%88%E0%B9%89%E0%B8%87%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A/";
                APP_FACTORY_CLASS_2_CANCEL.CitizenHandbookUrl = "https://info.go.th/#!/th/search/144/%E0%B9%81%E0%B8%88%E0%B9%89%E0%B8%87%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A/";
                APP_FACTORY_CLASS_2_CANCEL.OperatingDays = 30;
                APP_FACTORY_CLASS_2_CANCEL.OperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_2_CANCEL.OperatingDays2 = null;
                APP_FACTORY_CLASS_2_CANCEL.OperatingCost = 0;
                APP_FACTORY_CLASS_2_CANCEL.OperatingCostType = CostType.Fixed;
                APP_FACTORY_CLASS_2_CANCEL.OperatingCost2 = null;
                APP_FACTORY_CLASS_2_CANCEL.CitizenOperatingDays = 30;
                APP_FACTORY_CLASS_2_CANCEL.CitizenOperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_2_CANCEL.CitizenOperatingDays2 = null;
                APP_FACTORY_CLASS_2_CANCEL.CitizenOperatingCost = 0;
                APP_FACTORY_CLASS_2_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_FACTORY_CLASS_2_CANCEL.CitizenOperatingCost2 = null;
                APP_FACTORY_CLASS_2_CANCEL.ShowRemark = true;
                APP_FACTORY_CLASS_2_CANCEL.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_FACTORY_CLASS_2_CANCEL.CitizenShowRemark = true;
                APP_FACTORY_CLASS_2_CANCEL.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_FACTORY_CLASS_2_CANCEL.SingleFormEnabled = true;
                string TranslateName = "ขอแจ้งเลิกประกอบกิจการโรงงานจำพวกที่ 2";

                updateApplication(context, APP_FACTORY_CLASS_2_CANCEL, TranslateName, creater);
            }
            #endregion
            #endregion

            #region [ 46 ] APP TOURISM BUSINESS

            #region NEW

            Application APP_TOURISM_BUSINESS = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_TOURISM_BUSINESS).FirstOrDefault();

            if (APP_TOURISM_BUSINESS == null)
            {
                APP_TOURISM_BUSINESS = new Application();
                APP_TOURISM_BUSINESS.ApplicationSysName = ApplicationSysName.APP_TOURISM_BUSINESS;
                APP_TOURISM_BUSINESS.AppsHookClassName = "BizPortal.AppsHook.DOTTourismNewApphook";
                APP_TOURISM_BUSINESS.OrgCode = "05004000";
                APP_TOURISM_BUSINESS.LogoFileID = null;
                APP_TOURISM_BUSINESS.HandbookUrl = "https://info.go.th/#!/th/search/9190/การออกใบอนุญาตประกอบธุรกิจนำเที่ยว%20ประเภทนิติบุคคล%20(กรณีทั่วไป)/";
                APP_TOURISM_BUSINESS.CitizenHandbookUrl = "https://info.go.th/#!/th/search/9190/การออกใบอนุญาตประกอบธุรกิจนำเที่ยว%20ประเภทนิติบุคคล%20(กรณีทั่วไป)/";
                APP_TOURISM_BUSINESS.OperatingDays = 15;
                APP_TOURISM_BUSINESS.OperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS.OperatingDays2 = null;
                APP_TOURISM_BUSINESS.OperatingCost = 1200;
                APP_TOURISM_BUSINESS.OperatingCostType = CostType.Range;
                APP_TOURISM_BUSINESS.OperatingCost2 = 202000;
                APP_TOURISM_BUSINESS.CitizenOperatingDays = 15;
                APP_TOURISM_BUSINESS.CitizenOperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS.CitizenOperatingDays2 = null;
                APP_TOURISM_BUSINESS.CitizenOperatingCost = 1200;
                APP_TOURISM_BUSINESS.CitizenOperatingCostType = CostType.Range;
                APP_TOURISM_BUSINESS.CitizenOperatingCost2 = 202000;
                APP_TOURISM_BUSINESS.ShowRemark = true;
                APP_TOURISM_BUSINESS.Remark = REMARK_BKK;
                APP_TOURISM_BUSINESS.CitizenShowRemark = true;
                APP_TOURISM_BUSINESS.CitizenRemark = REMARK_BKK;
                APP_TOURISM_BUSINESS.SingleFormEnabled = true;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจนำเที่ยว";

                createApplication(context, APP_TOURISM_BUSINESS, TranslateName, creater);
            }
            else
            {
                APP_TOURISM_BUSINESS.ApplicationSysName = ApplicationSysName.APP_TOURISM_BUSINESS;
                APP_TOURISM_BUSINESS.AppsHookClassName = "BizPortal.AppsHook.DOTTourismNewApphook";
                APP_TOURISM_BUSINESS.OrgCode = "05004000";
                APP_TOURISM_BUSINESS.LogoFileID = null;
                APP_TOURISM_BUSINESS.HandbookUrl = "https://info.go.th/#!/th/search/9190/การออกใบอนุญาตประกอบธุรกิจนำเที่ยว%20ประเภทนิติบุคคล%20(กรณีทั่วไป)/";
                APP_TOURISM_BUSINESS.CitizenHandbookUrl = "https://info.go.th/#!/th/search/9190/การออกใบอนุญาตประกอบธุรกิจนำเที่ยว%20ประเภทนิติบุคคล%20(กรณีทั่วไป)/";
                APP_TOURISM_BUSINESS.OperatingDays = 15;
                APP_TOURISM_BUSINESS.OperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS.OperatingDays2 = null;
                APP_TOURISM_BUSINESS.OperatingCost = 1200;
                APP_TOURISM_BUSINESS.OperatingCostType = CostType.Range;
                APP_TOURISM_BUSINESS.OperatingCost2 = 202000;
                APP_TOURISM_BUSINESS.CitizenOperatingDays = 15;
                APP_TOURISM_BUSINESS.CitizenOperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS.CitizenOperatingDays2 = null;
                APP_TOURISM_BUSINESS.CitizenOperatingCost = 1200;
                APP_TOURISM_BUSINESS.CitizenOperatingCostType = CostType.Range;
                APP_TOURISM_BUSINESS.CitizenOperatingCost2 = 202000;
                APP_TOURISM_BUSINESS.ShowRemark = true;
                APP_TOURISM_BUSINESS.Remark = REMARK_BKK;
                APP_TOURISM_BUSINESS.CitizenShowRemark = true;
                APP_TOURISM_BUSINESS.CitizenRemark = REMARK_BKK;
                APP_TOURISM_BUSINESS.SingleFormEnabled = true;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจนำเที่ยว";

                updateApplication(context, APP_TOURISM_BUSINESS, TranslateName, creater);
            }

            #endregion

            #region RENEW

            Application APP_TOURISM_BUSINESS_RENEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_TOURISM_BUSINESS_RENEW).FirstOrDefault();

            if (APP_TOURISM_BUSINESS_RENEW == null)
            {
                APP_TOURISM_BUSINESS_RENEW = new Application();
                APP_TOURISM_BUSINESS_RENEW.ApplicationSysName = ApplicationSysName.APP_TOURISM_BUSINESS_RENEW;
                APP_TOURISM_BUSINESS_RENEW.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_TOURISM_BUSINESS_RENEW.OrgCode = "05004000";
                APP_TOURISM_BUSINESS_RENEW.LogoFileID = null;
                APP_TOURISM_BUSINESS_RENEW.HandbookUrl = "https://info.go.th/#!/th/search/72557/%E0%B8%99%E0%B8%B3%E0%B9%80%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%A7/";
                APP_TOURISM_BUSINESS_RENEW.CitizenHandbookUrl = "https://info.go.th/#!/th/search/72553/%E0%B8%99%E0%B8%B3%E0%B9%80%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%A7/";
                APP_TOURISM_BUSINESS_RENEW.OperatingDays = null;
                APP_TOURISM_BUSINESS_RENEW.OperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS_RENEW.OperatingDays2 = null;
                APP_TOURISM_BUSINESS_RENEW.OperatingCost = null;
                APP_TOURISM_BUSINESS_RENEW.OperatingCostType = CostType.Fixed;
                APP_TOURISM_BUSINESS_RENEW.OperatingCost2 = null;
                APP_TOURISM_BUSINESS_RENEW.CitizenOperatingDays = null;
                APP_TOURISM_BUSINESS_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS_RENEW.CitizenOperatingDays2 = null;
                APP_TOURISM_BUSINESS_RENEW.CitizenOperatingCost = null;
                APP_TOURISM_BUSINESS_RENEW.CitizenOperatingCostType = CostType.Fixed;
                APP_TOURISM_BUSINESS_RENEW.CitizenOperatingCost2 = null;
                APP_TOURISM_BUSINESS_RENEW.ShowRemark = true;
                APP_TOURISM_BUSINESS_RENEW.Remark = "<span style=\"color: red; \">(เนื่องจากช่วงนี้อยู่ระหว่างการเชื่อมโยงระบบ Biz Portal กับระบบของกรมการท่องเที่ยว ท่านสามารถใช้บริการโดยคลิกที่ \"ดูรายละเอียด\" เพื่อดูรายละเอียดหรือดาวน์โหลดแบบฟอร์มเพื่อใช้ยื่นคำขอต่อไป)</span>";
                APP_TOURISM_BUSINESS_RENEW.CitizenShowRemark = true;
                APP_TOURISM_BUSINESS_RENEW.CitizenRemark = "<span style=\"color: red; \">(เนื่องจากช่วงนี้อยู่ระหว่างการเชื่อมโยงระบบ Biz Portal กับระบบของกรมการท่องเที่ยว ท่านสามารถใช้บริการโดยคลิกที่ \"ดูรายละเอียด\" เพื่อดูรายละเอียดหรือดาวน์โหลดแบบฟอร์มเพื่อใช้ยื่นคำขอต่อไป)</span>";
                APP_TOURISM_BUSINESS_RENEW.SingleFormEnabled = true;
                APP_TOURISM_BUSINESS_RENEW.RequestAtOrg = true;
                APP_TOURISM_BUSINESS_RENEW.CitizenRequestAtOrg = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตประกอบธุรกิจนำเที่ยว";

                createApplication(context, APP_TOURISM_BUSINESS_RENEW, TranslateName, creater);
            }
            else
            {
                APP_TOURISM_BUSINESS_RENEW.OrgCode = "05004000";
                APP_TOURISM_BUSINESS_RENEW.HandbookUrl = "https://info.go.th/#!/th/search/72557/%E0%B8%99%E0%B8%B3%E0%B9%80%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%A7/";
                APP_TOURISM_BUSINESS_RENEW.CitizenHandbookUrl = "https://info.go.th/#!/th/search/72553/%E0%B8%99%E0%B8%B3%E0%B9%80%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%A7/";
                APP_TOURISM_BUSINESS_RENEW.OperatingDays = null;
                APP_TOURISM_BUSINESS_RENEW.OperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS_RENEW.OperatingDays2 = null;
                APP_TOURISM_BUSINESS_RENEW.OperatingCost = null;
                APP_TOURISM_BUSINESS_RENEW.OperatingCostType = CostType.Fixed;
                APP_TOURISM_BUSINESS_RENEW.OperatingCost2 = null;
                APP_TOURISM_BUSINESS_RENEW.CitizenOperatingDays = null;
                APP_TOURISM_BUSINESS_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS_RENEW.CitizenOperatingDays2 = null;
                APP_TOURISM_BUSINESS_RENEW.CitizenOperatingCost = null;
                APP_TOURISM_BUSINESS_RENEW.CitizenOperatingCostType = CostType.Fixed;
                APP_TOURISM_BUSINESS_RENEW.CitizenOperatingCost2 = null;
                APP_TOURISM_BUSINESS_RENEW.ShowRemark = true;
                APP_TOURISM_BUSINESS_RENEW.Remark = "<span style=\"color: red; \">(เนื่องจากช่วงนี้อยู่ระหว่างการเชื่อมโยงระบบ Biz Portal กับระบบของกรมการท่องเที่ยว ท่านสามารถใช้บริการโดยคลิกที่ \"ดูรายละเอียด\" เพื่อดูรายละเอียดหรือดาวน์โหลดแบบฟอร์มเพื่อใช้ยื่นคำขอต่อไป)</span>";
                APP_TOURISM_BUSINESS_RENEW.CitizenShowRemark = true;
                APP_TOURISM_BUSINESS_RENEW.CitizenRemark = "<span style=\"color: red; \">(เนื่องจากช่วงนี้อยู่ระหว่างการเชื่อมโยงระบบ Biz Portal กับระบบของกรมการท่องเที่ยว ท่านสามารถใช้บริการโดยคลิกที่ \"ดูรายละเอียด\" เพื่อดูรายละเอียดหรือดาวน์โหลดแบบฟอร์มเพื่อใช้ยื่นคำขอต่อไป)</span>";
                APP_TOURISM_BUSINESS_RENEW.RequestAtOrg = true;
                APP_TOURISM_BUSINESS_RENEW.CitizenRequestAtOrg = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตประกอบธุรกิจนำเที่ยว";

                updateApplication(context, APP_TOURISM_BUSINESS_RENEW, TranslateName, creater);
            }

            #endregion

            #region EDIT

            Application APP_TOURISM_BUSINESS_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_TOURISM_BUSINESS_EDIT).FirstOrDefault();

            if (APP_TOURISM_BUSINESS_EDIT == null)
            {
                APP_TOURISM_BUSINESS_EDIT = new Application();
                APP_TOURISM_BUSINESS_EDIT.ApplicationSysName = ApplicationSysName.APP_TOURISM_BUSINESS_EDIT;
                APP_TOURISM_BUSINESS_EDIT.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_TOURISM_BUSINESS_EDIT.OrgCode = "05004000";
                APP_TOURISM_BUSINESS_EDIT.LogoFileID = null;
                APP_TOURISM_BUSINESS_EDIT.HandbookUrl = "https://info.go.th/#!/th/search/72565/%E0%B8%99%E0%B8%B3%E0%B9%80%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%A7/";
                APP_TOURISM_BUSINESS_EDIT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/72563/%E0%B8%99%E0%B8%B3%E0%B9%80%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%A7/";
                APP_TOURISM_BUSINESS_EDIT.OperatingDays = null;
                APP_TOURISM_BUSINESS_EDIT.OperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS_EDIT.OperatingDays2 = null;
                APP_TOURISM_BUSINESS_EDIT.OperatingCost = null;
                APP_TOURISM_BUSINESS_EDIT.OperatingCostType = CostType.Fixed;
                APP_TOURISM_BUSINESS_EDIT.OperatingCost2 = null;
                APP_TOURISM_BUSINESS_EDIT.CitizenOperatingDays = null;
                APP_TOURISM_BUSINESS_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS_EDIT.CitizenOperatingDays2 = null;
                APP_TOURISM_BUSINESS_EDIT.CitizenOperatingCost = null;
                APP_TOURISM_BUSINESS_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_TOURISM_BUSINESS_EDIT.CitizenOperatingCost2 = null;
                APP_TOURISM_BUSINESS_EDIT.ShowRemark = true;
                APP_TOURISM_BUSINESS_EDIT.Remark = "<span style=\"color: red; \">(เนื่องจากช่วงนี้อยู่ระหว่างการเชื่อมโยงระบบ Biz Portal กับระบบของกรมการท่องเที่ยว ท่านสามารถใช้บริการโดยคลิกที่ \"ดูรายละเอียด\" เพื่อดูรายละเอียดหรือดาวน์โหลดแบบฟอร์มเพื่อใช้ยื่นคำขอต่อไป)</span>";
                APP_TOURISM_BUSINESS_EDIT.CitizenShowRemark = true;
                APP_TOURISM_BUSINESS_EDIT.CitizenRemark = "<span style=\"color: red; \">(เนื่องจากช่วงนี้อยู่ระหว่างการเชื่อมโยงระบบ Biz Portal กับระบบของกรมการท่องเที่ยว ท่านสามารถใช้บริการโดยคลิกที่ \"ดูรายละเอียด\" เพื่อดูรายละเอียดหรือดาวน์โหลดแบบฟอร์มเพื่อใช้ยื่นคำขอต่อไป)</span>";
                APP_TOURISM_BUSINESS_EDIT.SingleFormEnabled = true;
                APP_TOURISM_BUSINESS_EDIT.RequestAtOrg = true;
                APP_TOURISM_BUSINESS_EDIT.CitizenRequestAtOrg = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตประกอบธุรกิจนำเที่ยว";

                createApplication(context, APP_TOURISM_BUSINESS_EDIT, TranslateName, creater);
            }
            else
            {
                APP_TOURISM_BUSINESS_EDIT.OrgCode = "05004000";
                APP_TOURISM_BUSINESS_EDIT.HandbookUrl = "https://info.go.th/#!/th/search/72565/%E0%B8%99%E0%B8%B3%E0%B9%80%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%A7/";
                APP_TOURISM_BUSINESS_EDIT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/72563/%E0%B8%99%E0%B8%B3%E0%B9%80%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%A7/";
                APP_TOURISM_BUSINESS_EDIT.OperatingDays = null;
                APP_TOURISM_BUSINESS_EDIT.OperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS_EDIT.OperatingDays2 = null;
                APP_TOURISM_BUSINESS_EDIT.OperatingCost = null;
                APP_TOURISM_BUSINESS_EDIT.OperatingCostType = CostType.Fixed;
                APP_TOURISM_BUSINESS_EDIT.OperatingCost2 = null;
                APP_TOURISM_BUSINESS_EDIT.CitizenOperatingDays = null;
                APP_TOURISM_BUSINESS_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS_EDIT.CitizenOperatingDays2 = null;
                APP_TOURISM_BUSINESS_EDIT.CitizenOperatingCost = null;
                APP_TOURISM_BUSINESS_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_TOURISM_BUSINESS_EDIT.CitizenOperatingCost2 = null;
                APP_TOURISM_BUSINESS_EDIT.ShowRemark = true;
                APP_TOURISM_BUSINESS_EDIT.Remark = "<span style=\"color: red; \">(เนื่องจากช่วงนี้อยู่ระหว่างการเชื่อมโยงระบบ Biz Portal กับระบบของกรมการท่องเที่ยว ท่านสามารถใช้บริการโดยคลิกที่ \"ดูรายละเอียด\" เพื่อดูรายละเอียดหรือดาวน์โหลดแบบฟอร์มเพื่อใช้ยื่นคำขอต่อไป)</span>";
                APP_TOURISM_BUSINESS_EDIT.CitizenShowRemark = true;
                APP_TOURISM_BUSINESS_EDIT.CitizenRemark = "<span style=\"color: red; \">(เนื่องจากช่วงนี้อยู่ระหว่างการเชื่อมโยงระบบ Biz Portal กับระบบของกรมการท่องเที่ยว ท่านสามารถใช้บริการโดยคลิกที่ \"ดูรายละเอียด\" เพื่อดูรายละเอียดหรือดาวน์โหลดแบบฟอร์มเพื่อใช้ยื่นคำขอต่อไป)</span>";
                APP_TOURISM_BUSINESS_EDIT.RequestAtOrg = true;
                APP_TOURISM_BUSINESS_EDIT.CitizenRequestAtOrg = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตประกอบธุรกิจนำเที่ยว";

                updateApplication(context, APP_TOURISM_BUSINESS_EDIT, TranslateName, creater);
            }

            #endregion

            #region CANCEL

            Application APP_TOURISM_BUSINESS_CANCEL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_TOURISM_BUSINESS_CANCEL).FirstOrDefault();

            if (APP_TOURISM_BUSINESS_CANCEL == null)
            {
                APP_TOURISM_BUSINESS_CANCEL = new Application();
                APP_TOURISM_BUSINESS_CANCEL.ApplicationSysName = ApplicationSysName.APP_TOURISM_BUSINESS_CANCEL;
                APP_TOURISM_BUSINESS_CANCEL.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_TOURISM_BUSINESS_CANCEL.OrgCode = "05004000";
                APP_TOURISM_BUSINESS_CANCEL.LogoFileID = null;
                APP_TOURISM_BUSINESS_CANCEL.HandbookUrl = "https://info.go.th/#!/th/search/72561/%E0%B8%99%E0%B8%B3%E0%B9%80%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%A7/";
                APP_TOURISM_BUSINESS_CANCEL.CitizenHandbookUrl = "https://info.go.th/#!/th/search/72560/%E0%B8%99%E0%B8%B3%E0%B9%80%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%A7/";
                APP_TOURISM_BUSINESS_CANCEL.OperatingDays = null;
                APP_TOURISM_BUSINESS_CANCEL.OperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS_CANCEL.OperatingDays2 = null;
                APP_TOURISM_BUSINESS_CANCEL.OperatingCost = null;
                APP_TOURISM_BUSINESS_CANCEL.OperatingCostType = CostType.Fixed;
                APP_TOURISM_BUSINESS_CANCEL.OperatingCost2 = null;
                APP_TOURISM_BUSINESS_CANCEL.CitizenOperatingDays = null;
                APP_TOURISM_BUSINESS_CANCEL.CitizenOperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS_CANCEL.CitizenOperatingDays2 = null;
                APP_TOURISM_BUSINESS_CANCEL.CitizenOperatingCost = null;
                APP_TOURISM_BUSINESS_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_TOURISM_BUSINESS_CANCEL.CitizenOperatingCost2 = null;
                APP_TOURISM_BUSINESS_CANCEL.ShowRemark = true;
                APP_TOURISM_BUSINESS_CANCEL.Remark = "<span style=\"color: red; \">(เนื่องจากช่วงนี้อยู่ระหว่างการเชื่อมโยงระบบ Biz Portal กับระบบของกรมการท่องเที่ยว ท่านสามารถใช้บริการโดยคลิกที่ \"ดูรายละเอียด\" เพื่อดูรายละเอียดหรือดาวน์โหลดแบบฟอร์มเพื่อใช้ยื่นคำขอต่อไป)</span>";
                APP_TOURISM_BUSINESS_CANCEL.CitizenShowRemark = true;
                APP_TOURISM_BUSINESS_CANCEL.CitizenRemark = "<span style=\"color: red; \">(เนื่องจากช่วงนี้อยู่ระหว่างการเชื่อมโยงระบบ Biz Portal กับระบบของกรมการท่องเที่ยว ท่านสามารถใช้บริการโดยคลิกที่ \"ดูรายละเอียด\" เพื่อดูรายละเอียดหรือดาวน์โหลดแบบฟอร์มเพื่อใช้ยื่นคำขอต่อไป)</span>";
                APP_TOURISM_BUSINESS_CANCEL.SingleFormEnabled = true;
                APP_TOURISM_BUSINESS_CANCEL.RequestAtOrg = true;
                APP_TOURISM_BUSINESS_CANCEL.CitizenRequestAtOrg = true;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจนำเที่ยว";

                createApplication(context, APP_TOURISM_BUSINESS_CANCEL, TranslateName, creater);
            }
            else
            {
                APP_TOURISM_BUSINESS_CANCEL.OrgCode = "05004000";
                APP_TOURISM_BUSINESS_CANCEL.HandbookUrl = "https://info.go.th/#!/th/search/72561/%E0%B8%99%E0%B8%B3%E0%B9%80%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%A7/";
                APP_TOURISM_BUSINESS_CANCEL.CitizenHandbookUrl = "https://info.go.th/#!/th/search/72560/%E0%B8%99%E0%B8%B3%E0%B9%80%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%A2%E0%B8%A7/";
                APP_TOURISM_BUSINESS_CANCEL.OperatingDays = null;
                APP_TOURISM_BUSINESS_CANCEL.OperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS_CANCEL.OperatingDays2 = null;
                APP_TOURISM_BUSINESS_CANCEL.OperatingCost = null;
                APP_TOURISM_BUSINESS_CANCEL.OperatingCostType = CostType.Fixed;
                APP_TOURISM_BUSINESS_CANCEL.OperatingCost2 = null;
                APP_TOURISM_BUSINESS_CANCEL.CitizenOperatingDays = null;
                APP_TOURISM_BUSINESS_CANCEL.CitizenOperatingDayType = CostType.Fixed;
                APP_TOURISM_BUSINESS_CANCEL.CitizenOperatingDays2 = null;
                APP_TOURISM_BUSINESS_CANCEL.CitizenOperatingCost = null;
                APP_TOURISM_BUSINESS_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_TOURISM_BUSINESS_CANCEL.CitizenOperatingCost2 = null;
                APP_TOURISM_BUSINESS_CANCEL.ShowRemark = true;
                APP_TOURISM_BUSINESS_CANCEL.Remark = "<span style=\"color: red; \">(เนื่องจากช่วงนี้อยู่ระหว่างการเชื่อมโยงระบบ Biz Portal กับระบบของกรมการท่องเที่ยว ท่านสามารถใช้บริการโดยคลิกที่ \"ดูรายละเอียด\" เพื่อดูรายละเอียดหรือดาวน์โหลดแบบฟอร์มเพื่อใช้ยื่นคำขอต่อไป)</span>";
                APP_TOURISM_BUSINESS_CANCEL.CitizenShowRemark = true;
                APP_TOURISM_BUSINESS_CANCEL.CitizenRemark = "<span style=\"color: red; \">(เนื่องจากช่วงนี้อยู่ระหว่างการเชื่อมโยงระบบ Biz Portal กับระบบของกรมการท่องเที่ยว ท่านสามารถใช้บริการโดยคลิกที่ \"ดูรายละเอียด\" เพื่อดูรายละเอียดหรือดาวน์โหลดแบบฟอร์มเพื่อใช้ยื่นคำขอต่อไป)</span>";
                APP_TOURISM_BUSINESS_CANCEL.RequestAtOrg = true;
                APP_TOURISM_BUSINESS_CANCEL.CitizenRequestAtOrg = true;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจนำเที่ยว";

                updateApplication(context, APP_TOURISM_BUSINESS_CANCEL, TranslateName, creater);
            }

            #endregion

            #endregion

            #region FRT

            #region APP 36 APP_FRT_NEW_001
            Application APP_36 = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_36).FirstOrDefault();

            if (APP_36 == null)
            {
                APP_36 = new Application();
                APP_36.ApplicationSysName = ApplicationSysName.APP_36;
                APP_36.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_36.OrgCode = "21043000";
                APP_36.LogoFileID = null;
                APP_36.HandbookUrl = "https://info.go.th/#!/th/search/5014/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B9%80%E0%B8%9B%E0%B9%87%E0%B8%99%E0%B8%AD%E0%B8%B1%E0%B8%99%E0%B8%95%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B8%95%E0%B9%88%E0%B8%AD%E0%B8%AA%E0%B8%B8%E0%B8%82%E0%B8%A0%E0%B8%B2%E0%B8%9E%20(%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B9%83%E0%B8%AB%E0%B8%A1%E0%B9%88)/";
                APP_36.CitizenHandbookUrl = "https://info.go.th/#!/th/search/5014/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B9%80%E0%B8%9B%E0%B9%87%E0%B8%99%E0%B8%AD%E0%B8%B1%E0%B8%99%E0%B8%95%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B8%95%E0%B9%88%E0%B8%AD%E0%B8%AA%E0%B8%B8%E0%B8%82%E0%B8%A0%E0%B8%B2%E0%B8%9E%20(%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B9%83%E0%B8%AB%E0%B8%A1%E0%B9%88)/";

                APP_36.OperatingDays = 30;
                APP_36.OperatingDayType = CostType.Range;
                APP_36.OperatingDays2 = null;
                APP_36.OperatingCost = 1400;
                APP_36.OperatingCostType = CostType.Range;
                APP_36.OperatingCost2 = 15000;

                APP_36.CitizenOperatingDays = 30;
                APP_36.CitizenOperatingDayType = CostType.Fixed;
                APP_36.CitizenOperatingDays2 = null;
                APP_36.CitizenOperatingCost = 1400;
                APP_36.CitizenOperatingCostType = CostType.Fixed;
                APP_36.CitizenOperatingCost2 = 15000;

                APP_36.MultipleRequestSupport = true;
                APP_36.ShowRemark = true;
                APP_36.Remark = REMARK_BKK;
                APP_36.CitizenShowRemark = true;
                APP_36.CitizenRemark = REMARK_BKK;
                APP_36.SingleFormEnabled = true;
                string TranslateName = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การผลิต บรรจุยาสีฟัน แชมพู ผ้าเย็น กระดาษเย็น เครื่องสำอาง รวมทั้งสบู่ที่ใช้กับร่างกาย";

                createApplication(context, APP_36, TranslateName, creater);
            }
            #endregion

            #region APP 37 APP_FRT_RENEW_001
            Application APP_37 = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_37).FirstOrDefault();

            if (APP_37 == null)
            {
                APP_37 = new Application();
                APP_37.ApplicationSysName = ApplicationSysName.APP_37;
                APP_37.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_37.OrgCode = "21043000";
                APP_37.LogoFileID = null;
                APP_37.HandbookUrl = "https://info.go.th/#!/th/search/5154/%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B9%80%E0%B8%9B%E0%B9%87%E0%B8%99%E0%B8%AD%E0%B8%B1%E0%B8%99%E0%B8%95%E0%B8%A3%E0%B8%B2%E0%B8%A2/";
                APP_37.CitizenHandbookUrl = "https://info.go.th/#!/th/search/5154/%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B9%80%E0%B8%9B%E0%B9%87%E0%B8%99%E0%B8%AD%E0%B8%B1%E0%B8%99%E0%B8%95%E0%B8%A3%E0%B8%B2%E0%B8%A2/";

                APP_37.OperatingDays = 15;
                APP_37.OperatingDayType = CostType.Range;
                APP_37.OperatingDays2 = null;
                APP_37.OperatingCost = 1400;
                APP_37.OperatingCostType = CostType.Range;
                APP_37.OperatingCost2 = 15000;

                APP_37.CitizenOperatingDays = 15;
                APP_37.CitizenOperatingDayType = CostType.Range;
                APP_37.CitizenOperatingDays2 = null;
                APP_37.CitizenOperatingCost = 1400;
                APP_37.CitizenOperatingCostType = CostType.Range;
                APP_37.CitizenOperatingCost2 = 15000;

                APP_37.MultipleRequestSupport = true;
                APP_37.ShowRemark = true;
                APP_37.Remark = REMARK_BKK;
                APP_37.CitizenShowRemark = true;
                APP_37.CitizenRemark = REMARK_BKK;
                APP_37.SingleFormEnabled = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การผลิต บรรจุยาสีฟัน แชมพู ผ้าเย็น กระดาษเย็น เครื่องสำอาง รวมทั้งสบู่ที่ใช้กับร่างกาย";

                createApplication(context, APP_37, TranslateName, creater);
            }
            #endregion

            #region APP 38 APP_FRT_EDIT_001
            Application APP_38 = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_38).FirstOrDefault();

            if (APP_38 == null)
            {
                APP_38 = new Application();
                APP_38.ApplicationSysName = ApplicationSysName.APP_38;
                APP_38.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_38.OrgCode = "21043000";
                APP_38.LogoFileID = null;
                APP_38.HandbookUrl = "https://info.go.th/#!/th/search/5179/%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B9%80%E0%B8%9B%E0%B9%87%E0%B8%99%E0%B8%AD%E0%B8%B1%E0%B8%99%E0%B8%95%E0%B8%A3%E0%B8%B2%E0%B8%A2/";
                APP_38.CitizenHandbookUrl = "https://info.go.th/#!/th/search/5179/%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B9%80%E0%B8%9B%E0%B9%87%E0%B8%99%E0%B8%AD%E0%B8%B1%E0%B8%99%E0%B8%95%E0%B8%A3%E0%B8%B2%E0%B8%A2/";

                APP_38.OperatingDays = 30;
                APP_38.OperatingDayType = CostType.Range;
                APP_38.OperatingDays2 = null;
                APP_38.OperatingCost = 0;
                APP_38.OperatingCostType = CostType.Range;
                APP_38.OperatingCost2 = 15000;

                APP_38.CitizenOperatingDays = 30;
                APP_38.CitizenOperatingDayType = CostType.Range;
                APP_38.CitizenOperatingDays2 = null;
                APP_38.CitizenOperatingCost = 0;
                APP_38.CitizenOperatingCostType = CostType.Range;
                APP_38.CitizenOperatingCost2 = 15000;

                APP_38.MultipleRequestSupport = true;
                APP_38.ShowRemark = true;
                APP_38.Remark = REMARK_BKK;
                APP_38.CitizenShowRemark = true;
                APP_38.CitizenRemark = REMARK_BKK;
                APP_38.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การผลิต บรรจุยาสีฟัน แชมพู ผ้าเย็น กระดาษเย็น เครื่องสำอาง รวมทั้งสบู่ที่ใช้กับร่างกาย";

                createApplication(context, APP_38, TranslateName, creater);
            }
            #endregion

            #region APP 39 APP_FRT_CANCEL_001
            Application APP_39 = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_39).FirstOrDefault();

            if (APP_39 == null)
            {
                APP_39 = new Application();
                APP_39.ApplicationSysName = ApplicationSysName.APP_39;
                APP_39.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_39.OrgCode = "21043000";
                APP_39.LogoFileID = null;
                APP_39.HandbookUrl = "https://info.go.th/#!/th/search/5296/%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%94%E0%B8%B3%E0%B9%80%E0%B8%99%E0%B8%B4%E0%B8%99%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3/8jt";
                APP_39.CitizenHandbookUrl = "https://info.go.th/#!/th/search/5296/%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%94%E0%B8%B3%E0%B9%80%E0%B8%99%E0%B8%B4%E0%B8%99%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3/8jt";

                APP_39.OperatingDays = 7;
                APP_39.OperatingDayType = CostType.Fixed;
                APP_39.OperatingDays2 = null;
                APP_39.OperatingCost = 0;
                APP_39.OperatingCostType = CostType.Fixed;
                APP_39.OperatingCost2 = null;

                APP_39.CitizenOperatingDays = 7;
                APP_39.CitizenOperatingDayType = CostType.Fixed;
                APP_39.CitizenOperatingDays2 = null;
                APP_39.CitizenOperatingCost = 0;
                APP_39.CitizenOperatingCostType = CostType.Fixed;
                APP_39.CitizenOperatingCost2 = null;

                APP_39.MultipleRequestSupport = true;
                APP_39.ShowRemark = true;
                APP_39.Remark = REMARK_BKK;
                APP_39.CitizenShowRemark = true;
                APP_39.CitizenRemark = REMARK_BKK;
                APP_39.SingleFormEnabled = true;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การผลิต บรรจุยาสีฟัน แชมพู ผ้าเย็น กระดาษเย็น เครื่องสำอาง รวมทั้งสบู่ที่ใช้กับร่างกาย";

                createApplication(context, APP_39, TranslateName, creater);
            }
            #endregion

            #endregion

            #region [ 47 ] APP HOSPITAL PERMISSION

            #region [NEW]
            Application APP_HOSPITAL_PERMISSION = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HOSPITAL_PERMISSION).FirstOrDefault();

            if (APP_HOSPITAL_PERMISSION == null)
            {
                APP_HOSPITAL_PERMISSION = new Application();
                APP_HOSPITAL_PERMISSION.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_PERMISSION;
                APP_HOSPITAL_PERMISSION.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalPermissionNewAppHook";
                APP_HOSPITAL_PERMISSION.OrgCode = "19007000";
                APP_HOSPITAL_PERMISSION.LogoFileID = null;
                APP_HOSPITAL_PERMISSION.MultipleRequestSupport = true;
                APP_HOSPITAL_PERMISSION.HandbookUrl = "https://www.info.go.th/#!/th/search/8656/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_PERMISSION.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/8656/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_HOSPITAL_PERMISSION.OperatingDays = 91;
                APP_HOSPITAL_PERMISSION.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_PERMISSION.OperatingDays2 = null;
                APP_HOSPITAL_PERMISSION.CitizenOperatingDays = 91;
                APP_HOSPITAL_PERMISSION.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_PERMISSION.CitizenOperatingDays2 = null;

                APP_HOSPITAL_PERMISSION.OperatingCost = 2000;
                APP_HOSPITAL_PERMISSION.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_PERMISSION.OperatingCost2 = null;
                APP_HOSPITAL_PERMISSION.CitizenOperatingCost = 2000;
                APP_HOSPITAL_PERMISSION.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_PERMISSION.CitizenOperatingCost2 = null;

                APP_HOSPITAL_PERMISSION.ShowRemark = true;
                APP_HOSPITAL_PERMISSION.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_PERMISSION.CitizenShowRemark = true;
                APP_HOSPITAL_PERMISSION.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_PERMISSION.SingleFormEnabled = true;
                string TranslateName = "ขอใบอนุญาตให้ประกอบกิจการสถานพยาบาล(โรงพยาบาล)";

                createApplication(context, APP_HOSPITAL_PERMISSION, TranslateName, creater);
            }
            else
            {
                APP_HOSPITAL_PERMISSION.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalPermissionNewAppHook";
                APP_HOSPITAL_PERMISSION.OperatingDays = 91;
                APP_HOSPITAL_PERMISSION.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_PERMISSION.OperatingDays2 = null;
                APP_HOSPITAL_PERMISSION.CitizenOperatingDays = 91;
                APP_HOSPITAL_PERMISSION.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_PERMISSION.CitizenOperatingDays2 = null;

                APP_HOSPITAL_PERMISSION.OperatingCost = 2000;
                APP_HOSPITAL_PERMISSION.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_PERMISSION.OperatingCost2 = null;
                APP_HOSPITAL_PERMISSION.CitizenOperatingCost = 2000;
                APP_HOSPITAL_PERMISSION.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_PERMISSION.CitizenOperatingCost2 = null;
                APP_HOSPITAL_PERMISSION.ShowRemark = true;
                APP_HOSPITAL_PERMISSION.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_PERMISSION.CitizenShowRemark = true;
                APP_HOSPITAL_PERMISSION.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                context.SaveChanges(false);

                string TranslateName = "ขอใบอนุญาตให้ประกอบกิจการสถานพยาบาล(โรงพยาบาล)";
                updateApplication(context, APP_HOSPITAL_PERMISSION, TranslateName, creater);
            }
            #endregion

            #region [RENEW]
            Application APP_HOSPITAL_PERMISSION_RENEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HOSPITAL_PERMISSION_RENEW).FirstOrDefault();

            if (APP_HOSPITAL_PERMISSION_RENEW == null)
            {
                APP_HOSPITAL_PERMISSION_RENEW = new Application();
                APP_HOSPITAL_PERMISSION_RENEW.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_PERMISSION_RENEW;
                APP_HOSPITAL_PERMISSION_RENEW.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_HOSPITAL_PERMISSION_RENEW.OrgCode = "19007000";
                APP_HOSPITAL_PERMISSION_RENEW.LogoFileID = null;
                APP_HOSPITAL_PERMISSION_RENEW.MultipleRequestSupport = true;
                APP_HOSPITAL_PERMISSION_RENEW.HandbookUrl = "https://www.info.go.th/#!/th/search/8656/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_PERMISSION_RENEW.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/8656/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_PERMISSION_RENEW.OperatingDays = 1;
                APP_HOSPITAL_PERMISSION_RENEW.OperatingDayType = CostType.Range;
                APP_HOSPITAL_PERMISSION_RENEW.OperatingDays2 = 91;
                APP_HOSPITAL_PERMISSION_RENEW.OperatingCost = 500;
                APP_HOSPITAL_PERMISSION_RENEW.OperatingCostType = CostType.Range;
                APP_HOSPITAL_PERMISSION_RENEW.OperatingCost2 = 20000;
                APP_HOSPITAL_PERMISSION_RENEW.CitizenOperatingDays = 1;
                APP_HOSPITAL_PERMISSION_RENEW.CitizenOperatingDayType = CostType.Range;
                APP_HOSPITAL_PERMISSION_RENEW.CitizenOperatingDays2 = 91;
                APP_HOSPITAL_PERMISSION_RENEW.CitizenOperatingCost = 500;
                APP_HOSPITAL_PERMISSION_RENEW.CitizenOperatingCostType = CostType.Range;
                APP_HOSPITAL_PERMISSION_RENEW.CitizenOperatingCost2 = 20000;
                APP_HOSPITAL_PERMISSION_RENEW.ShowRemark = true;
                APP_HOSPITAL_PERMISSION_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_PERMISSION_RENEW.CitizenShowRemark = true;
                APP_HOSPITAL_PERMISSION_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_PERMISSION_RENEW.SingleFormEnabled = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาลหรือใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)";

                createApplication(context, APP_HOSPITAL_PERMISSION_RENEW, TranslateName, creater);
            }
            else
            {
                APP_HOSPITAL_PERMISSION_RENEW.ShowRemark = true;
                APP_HOSPITAL_PERMISSION_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_PERMISSION_RENEW.CitizenShowRemark = true;
                APP_HOSPITAL_PERMISSION_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                context.SaveChanges(false);

                string TranslateName = "ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาลหรือใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)";
                updateApplication(context, APP_HOSPITAL_PERMISSION_RENEW, TranslateName, creater);
            }
            #endregion

            #region [EDIT]
            Application APP_HOSPITAL_PERMISSION_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HOSPITAL_PERMISSION_EDIT).FirstOrDefault();

            if (APP_HOSPITAL_PERMISSION_EDIT == null)
            {
                APP_HOSPITAL_PERMISSION_EDIT = new Application();
                APP_HOSPITAL_PERMISSION_EDIT.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_PERMISSION_EDIT;
                APP_HOSPITAL_PERMISSION_EDIT.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_HOSPITAL_PERMISSION_EDIT.OrgCode = "19007000";
                APP_HOSPITAL_PERMISSION_EDIT.LogoFileID = null;
                APP_HOSPITAL_PERMISSION_EDIT.MultipleRequestSupport = true;
                APP_HOSPITAL_PERMISSION_EDIT.HandbookUrl = " https://www.info.go.th/#!/th/search/32693/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_PERMISSION_EDIT.CitizenHandbookUrl = " https://www.info.go.th/#!/th/search/32693/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_PERMISSION_EDIT.OperatingDays = 21;
                APP_HOSPITAL_PERMISSION_EDIT.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_PERMISSION_EDIT.OperatingDays2 = null;
                APP_HOSPITAL_PERMISSION_EDIT.OperatingCost = 100;
                APP_HOSPITAL_PERMISSION_EDIT.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_PERMISSION_EDIT.OperatingCost2 = null;
                APP_HOSPITAL_PERMISSION_EDIT.CitizenOperatingDays = 21;
                APP_HOSPITAL_PERMISSION_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_PERMISSION_EDIT.CitizenOperatingDays2 = null;
                APP_HOSPITAL_PERMISSION_EDIT.CitizenOperatingCost = 100;
                APP_HOSPITAL_PERMISSION_EDIT.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_PERMISSION_EDIT.CitizenOperatingCost2 = null;
                APP_HOSPITAL_PERMISSION_EDIT.ShowRemark = true;
                APP_HOSPITAL_PERMISSION_EDIT.Remark = REMARK_BKK + "<br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541 </span>";
                APP_HOSPITAL_PERMISSION_EDIT.CitizenShowRemark = true;
                APP_HOSPITAL_PERMISSION_EDIT.CitizenRemark = REMARK_BKK + "<br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_HOSPITAL_PERMISSION_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาลและใบอนุญาตให้ดำเนินการสถานพยาบาลหรือเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (โรงพยาบาล)";

                createApplication(context, APP_HOSPITAL_PERMISSION_EDIT, TranslateName, creater);
            }
            else
            {
                APP_HOSPITAL_PERMISSION_EDIT.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_PERMISSION_EDIT.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_PERMISSION_EDIT.ShowRemark = true;
                APP_HOSPITAL_PERMISSION_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_PERMISSION_EDIT.CitizenShowRemark = true;
                APP_HOSPITAL_PERMISSION_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";

                string TranslateName = "ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาลและใบอนุญาตให้ดำเนินการสถานพยาบาลหรือเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (โรงพยาบาล)";
                updateApplication(context, APP_HOSPITAL_PERMISSION_EDIT, TranslateName, creater);
            }
            #endregion

            #region [CANCEL]
            Application APP_HOSPITAL_PERMISSION_CANCEL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HOSPITAL_PERMISSION_CANCEL).FirstOrDefault();

            if (APP_HOSPITAL_PERMISSION_CANCEL == null)
            {
                APP_HOSPITAL_PERMISSION_CANCEL = new Application();
                APP_HOSPITAL_PERMISSION_CANCEL.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_PERMISSION_CANCEL;
                APP_HOSPITAL_PERMISSION_CANCEL.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_HOSPITAL_PERMISSION_CANCEL.OrgCode = "19007000";
                APP_HOSPITAL_PERMISSION_CANCEL.LogoFileID = null;
                APP_HOSPITAL_PERMISSION_CANCEL.MultipleRequestSupport = true;
                APP_HOSPITAL_PERMISSION_CANCEL.HandbookUrl = "https://www.info.go.th/#!/th/search/35707/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_PERMISSION_CANCEL.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/35707/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_PERMISSION_CANCEL.OperatingDays = 21;
                APP_HOSPITAL_PERMISSION_CANCEL.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_PERMISSION_CANCEL.OperatingDays2 = null;
                APP_HOSPITAL_PERMISSION_CANCEL.OperatingCost = 0;
                APP_HOSPITAL_PERMISSION_CANCEL.OperatingCostType = CostType.Fixed;
                APP_HOSPITAL_PERMISSION_CANCEL.OperatingCost2 = null;
                APP_HOSPITAL_PERMISSION_CANCEL.CitizenOperatingDays = 21;
                APP_HOSPITAL_PERMISSION_CANCEL.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_PERMISSION_CANCEL.CitizenOperatingDays2 = null;
                APP_HOSPITAL_PERMISSION_CANCEL.CitizenOperatingCost = 0;
                APP_HOSPITAL_PERMISSION_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_HOSPITAL_PERMISSION_CANCEL.CitizenOperatingCost2 = null;
                APP_HOSPITAL_PERMISSION_CANCEL.ShowRemark = true;
                APP_HOSPITAL_PERMISSION_CANCEL.Remark = REMARK_BKK + "<br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าธรรมเนียมย้อนหลัง</span>";
                APP_HOSPITAL_PERMISSION_CANCEL.CitizenShowRemark = true;
                APP_HOSPITAL_PERMISSION_CANCEL.CitizenRemark = REMARK_BKK + "<br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าธรรมเนียมย้อนหลัง</span>";
                APP_HOSPITAL_PERMISSION_CANCEL.SingleFormEnabled = true;
                string TranslateName = "ขอแจ้งเลิกกิจการสถานพยาบาล (โรงพยาบาล)";

                createApplication(context, APP_HOSPITAL_PERMISSION_CANCEL, TranslateName, creater);
            }
            else
            {
                string TranslateName = "ขอแจ้งเลิกกิจการสถานพยาบาล (โรงพยาบาล)";
                updateApplication(context, APP_HOSPITAL_PERMISSION_CANCEL, TranslateName, creater);
            }
            #endregion

            #endregion

            #region [ 48 ] APP_ENERGY_PRODUCTION_NOT_PERMIT
            Application APP_ENERGY_PRODUCTION_NOT_PERMIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ENERGY_PRODUCTION_NOT_PERMIT).FirstOrDefault();

            if (APP_ENERGY_PRODUCTION_NOT_PERMIT == null)
            {
                APP_ENERGY_PRODUCTION_NOT_PERMIT = new Application();
                APP_ENERGY_PRODUCTION_NOT_PERMIT.ApplicationSysName = ApplicationSysName.APP_ENERGY_PRODUCTION_NOT_PERMIT;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OrgCode = "11003000";
                APP_ENERGY_PRODUCTION_NOT_PERMIT.LogoFileID = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.MultipleRequestSupport = true;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.HandbookUrl = "http://www.erc.or.th/ERCWeb2/Upload/Document/คู่มือ/คู่มือ%20New/updated_ver3การแจ้งการประกอบกิจการไฟฟ้าที่ได้รับยกเว้นไม่ต้องขอรับใบอนุญาต.pdf";
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenHandbookUrl = "http://www.erc.or.th/ERCWeb2/Upload/Document/คู่มือ/คู่มือ%20New/updated_ver3การแจ้งการประกอบกิจการไฟฟ้าที่ได้รับยกเว้นไม่ต้องขอรับใบอนุญาต.pdf";
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OperatingDays = 15;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OperatingDays2 = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OperatingCost = 0;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OperatingCost2 = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenOperatingDays = 15;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenOperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenOperatingDays2 = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenOperatingCost = 0;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenOperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenOperatingCost2 = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.ShowRemark = false;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.Remark = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenShowRemark = false;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenRemark = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.SingleFormEnabled = true;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.ApplicationUrl = "http://app04.erc.or.th/elicense/login.aspx";
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenRequestAtOrg = true;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.RequestAtOrg = true;
                string TranslateName = "การรับแจ้งการประกอบกิจการพลังงานที่ได้รับการยกเว้นไม่ต้องขอรับใบอนุญาต";

                createApplication(context, APP_ENERGY_PRODUCTION_NOT_PERMIT, TranslateName, creater);
            }
            else
            {
                APP_ENERGY_PRODUCTION_NOT_PERMIT.ApplicationSysName = ApplicationSysName.APP_ENERGY_PRODUCTION_NOT_PERMIT;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OrgCode = "11003000";
                APP_ENERGY_PRODUCTION_NOT_PERMIT.LogoFileID = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.MultipleRequestSupport = true;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.HandbookUrl = "http://www.erc.or.th/ERCWeb2/Upload/Document/คู่มือ/คู่มือ%20New/updated_ver3การแจ้งการประกอบกิจการไฟฟ้าที่ได้รับยกเว้นไม่ต้องขอรับใบอนุญาต.pdf";
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenHandbookUrl = "http://www.erc.or.th/ERCWeb2/Upload/Document/คู่มือ/คู่มือ%20New/updated_ver3การแจ้งการประกอบกิจการไฟฟ้าที่ได้รับยกเว้นไม่ต้องขอรับใบอนุญาต.pdf";
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OperatingDays = 15;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OperatingDays2 = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OperatingCost = 0;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.OperatingCost2 = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenOperatingDays = 15;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenOperatingDayType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenOperatingDays2 = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenOperatingCost = 0;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenOperatingCostType = CostType.Fixed;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenOperatingCost2 = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.ShowRemark = false;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.Remark = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenShowRemark = false;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenRemark = null;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.SingleFormEnabled = true;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.ApplicationUrl = "http://app04.erc.or.th/elicense/login.aspx";
                APP_ENERGY_PRODUCTION_NOT_PERMIT.CitizenRequestAtOrg = true;
                APP_ENERGY_PRODUCTION_NOT_PERMIT.RequestAtOrg = true;
                string TranslateName = "การรับแจ้งการประกอบกิจการพลังงานที่ได้รับการยกเว้นไม่ต้องขอรับใบอนุญาต";

                updateApplication(context, APP_ENERGY_PRODUCTION_NOT_PERMIT, TranslateName, creater);
            }

            #endregion

            #region [ 49 ] APP_ORGANIC_PLANT_PRODUCTION

            #region [ NEW ]

            Application APP_ORGANIC_PLANT_PRODUCTION_NEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ORGANIC_PLANT_PRODUCTION_NEW).FirstOrDefault();
            if (APP_ORGANIC_PLANT_PRODUCTION_NEW == null)
            {
                APP_ORGANIC_PLANT_PRODUCTION_NEW = new Application();
                APP_ORGANIC_PLANT_PRODUCTION_NEW.ApplicationSysName = ApplicationSysName.APP_ORGANIC_PLANT_PRODUCTION_NEW;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.AppsHookClassName = "BizPortal.AppsHook.DOAOrganicPlantNewAppHook";
                APP_ORGANIC_PLANT_PRODUCTION_NEW.OrgCode = "07008000";
                APP_ORGANIC_PLANT_PRODUCTION_NEW.LogoFileID = null;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.HandbookUrl = "http://www.doa.go.th/main/";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenHandbookUrl = "http://www.doa.go.th/main/";  //Citizen

                APP_ORGANIC_PLANT_PRODUCTION_NEW.OperatingDays = 65;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.OperatingDayType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.OperatingDays2 = null;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.OperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.OperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.OperatingCost2 = 0;
                //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenOperatingDays = 65;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenOperatingDayType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenOperatingDays2 = null;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenOperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenOperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenOperatingCost2 = 0;

                APP_ORGANIC_PLANT_PRODUCTION_NEW.ShowRemark = false;  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_NEW.Remark = null;  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenShowRemark = false;  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenRemark = null;  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_NEW.SingleFormEnabled = true;
                string TranslateName = "ขอใบรับรองแหล่งผลิตพืชอินทรีย์ (แปลงเดียว)";

                createApplication(context, APP_ORGANIC_PLANT_PRODUCTION_NEW, TranslateName, creater);
            }
            else
            {
                APP_ORGANIC_PLANT_PRODUCTION_NEW.ApplicationSysName = ApplicationSysName.APP_ORGANIC_PLANT_PRODUCTION_NEW;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.AppsHookClassName = "BizPortal.AppsHook.DOAOrganicPlantNewAppHook";
                APP_ORGANIC_PLANT_PRODUCTION_NEW.OrgCode = "07008000";
                APP_ORGANIC_PLANT_PRODUCTION_NEW.LogoFileID = null;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.HandbookUrl = "http://www.doa.go.th/main/";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenHandbookUrl = "http://www.doa.go.th/main/";  //Citizen

                APP_ORGANIC_PLANT_PRODUCTION_NEW.OperatingDays = 65;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.OperatingDayType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.OperatingDays2 = null;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.OperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.OperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.OperatingCost2 = 0;
                //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenOperatingDays = 65;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenOperatingDayType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenOperatingDays2 = null;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenOperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenOperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenOperatingCost2 = 0;

                APP_ORGANIC_PLANT_PRODUCTION_NEW.ShowRemark = false;  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_NEW.Remark = null;  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenShowRemark = false;  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_NEW.CitizenRemark = null;  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_NEW.SingleFormEnabled = true;
                string TranslateName = "ขอใบรับรองแหล่งผลิตพืชอินทรีย์ (แปลงเดียว)";

                updateApplication(context, APP_ORGANIC_PLANT_PRODUCTION_NEW, TranslateName, creater);
            }

            #endregion

            #region [ RENEW ]
            Application APP_ORGANIC_PLANT_PRODUCTION_RENEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ORGANIC_PLANT_PRODUCTION_RENEW).FirstOrDefault();
            if (APP_ORGANIC_PLANT_PRODUCTION_RENEW == null)
            {
                APP_ORGANIC_PLANT_PRODUCTION_RENEW = new Application();
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.ApplicationSysName = ApplicationSysName.APP_ORGANIC_PLANT_PRODUCTION_RENEW;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.AppsHookClassName = "BizPortal.AppsHook.DOAOrganicPlantReNewAppHook";
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OrgCode = "07008000";
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.LogoFileID = null;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.HandbookUrl = "http://www.doa.go.th/main/";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenHandbookUrl = "http://www.doa.go.th/main/";  //Citizen
                                                                                                       //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OperatingDays = 65;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OperatingDayType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OperatingDays2 = null;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OperatingCost2 = null;
                //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenOperatingDays = 65;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenOperatingDays2 = null;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenOperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenOperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenOperatingCost2 = null;

                APP_ORGANIC_PLANT_PRODUCTION_RENEW.ShowRemark = true;  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenShowRemark = true;  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.SingleFormEnabled = true;
                string TranslateName = "ขอต่ออายุใบรับรองแหล่งผลิตพืชอินทรีย์ (แปลงเดียว/รายเดียว หรือกลุ่ม)";

                createApplication(context, APP_ORGANIC_PLANT_PRODUCTION_RENEW, TranslateName, creater);
            }
            else
            {
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.ApplicationSysName = ApplicationSysName.APP_ORGANIC_PLANT_PRODUCTION_RENEW;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.AppsHookClassName = "BizPortal.AppsHook.DOAOrganicPlantReNewAppHook";
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OrgCode = "07008000";
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.LogoFileID = null;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.HandbookUrl = "http://www.doa.go.th/main/";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenHandbookUrl = "http://www.doa.go.th/main/";  //Citizen
                                                                                                       //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OperatingDays = 65;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OperatingDayType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OperatingDays2 = null;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.OperatingCost2 = null;
                //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenOperatingDays = 65;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenOperatingDays2 = null;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenOperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenOperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenOperatingCost2 = null;

                APP_ORGANIC_PLANT_PRODUCTION_RENEW.ShowRemark = true;  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenShowRemark = true;  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_RENEW.SingleFormEnabled = true;
                string TranslateName = "ขอต่ออายุใบรับรองแหล่งผลิตพืชอินทรีย์ (แปลงเดียว/รายเดียว หรือกลุ่ม)";

                updateApplication(context, APP_ORGANIC_PLANT_PRODUCTION_RENEW, TranslateName, creater);
            }
            #endregion

            #region [ Edit ]
            Application APP_ORGANIC_PLANT_PRODUCTION_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ORGANIC_PLANT_PRODUCTION_EDIT).FirstOrDefault();
            if (APP_ORGANIC_PLANT_PRODUCTION_EDIT == null)
            {
                APP_ORGANIC_PLANT_PRODUCTION_EDIT = new Application();
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.ApplicationSysName = ApplicationSysName.APP_ORGANIC_PLANT_PRODUCTION_EDIT;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.AppsHookClassName = "BizPortal.AppsHook.DOAOrganicPlantEditAppHook";
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OrgCode = "07008000";
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.LogoFileID = null;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.HandbookUrl = "http://www.doa.go.th/main/";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenHandbookUrl = "http://www.doa.go.th/main/";  //Citizen
                //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OperatingDays = 5;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OperatingDayType = CostType.Range;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OperatingDays2 = 65;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OperatingCost2 = null;
                //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenOperatingDays = 5;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenOperatingDayType = CostType.Range;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenOperatingDays2 = 65;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenOperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenOperatingCost2 = null;

                APP_ORGANIC_PLANT_PRODUCTION_EDIT.ShowRemark = true;  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenShowRemark = true;  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขใบรับรองแหล่งผลิตพืชอินทรีย์ (แปลงเดียว/รายเดียว)";

                createApplication(context, APP_ORGANIC_PLANT_PRODUCTION_EDIT, TranslateName, creater);
            }
            else
            {
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.ApplicationSysName = ApplicationSysName.APP_ORGANIC_PLANT_PRODUCTION_EDIT;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.AppsHookClassName = "BizPortal.AppsHook.DOAOrganicPlantEditAppHook";
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OrgCode = "07008000";
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.LogoFileID = null;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.HandbookUrl = "http://www.doa.go.th/main/";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenHandbookUrl = "http://www.doa.go.th/main/";  //Citizen
                //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OperatingDays = 5;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OperatingDayType = CostType.Range;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OperatingDays2 = 65;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.OperatingCost2 = null;
                //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenOperatingDays = 5;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenOperatingDayType = CostType.Range;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenOperatingDays2 = 65;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenOperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenOperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenOperatingCost2 = null;

                APP_ORGANIC_PLANT_PRODUCTION_EDIT.ShowRemark = true;  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenShowRemark = true;  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขใบรับรองแหล่งผลิตพืชอินทรีย์ (แปลงเดียว/รายเดียว)";

                updateApplication(context, APP_ORGANIC_PLANT_PRODUCTION_EDIT, TranslateName, creater);
            }
            #endregion

            #region [ Cancel ]
            Application APP_ORGANIC_PLANT_PRODUCTION_CANCEL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_ORGANIC_PLANT_PRODUCTION_CANCEL).FirstOrDefault();
            if (APP_ORGANIC_PLANT_PRODUCTION_CANCEL == null)
            {
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL = new Application();
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.ApplicationSysName = ApplicationSysName.APP_ORGANIC_PLANT_PRODUCTION_CANCEL;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.AppsHookClassName = "BizPortal.AppsHook.DOAOrganicPlantCancelAppHook";
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OrgCode = "07008000";
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.LogoFileID = null;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.HandbookUrl = "http://www.doa.go.th/main/";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenHandbookUrl = "http://www.doa.go.th/main/";  //Citizen
                //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OperatingDays = 5;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OperatingDayType = CostType.Range;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OperatingDays2 = 45;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OperatingCost2 = null;
                //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenOperatingDays = 5;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenOperatingDayType = CostType.Range;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenOperatingDays2 = 45;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenOperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenOperatingCost2 = null;

                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.ShowRemark = true;  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenShowRemark = true;  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.SingleFormEnabled = true;
                string TranslateName = "ขอยกเลิกใบรับรองแหล่งผลิตพืชอินทรีย์ (แปลงเดียว/รายเดียว หรือกลุ่ม)";

                createApplication(context, APP_ORGANIC_PLANT_PRODUCTION_CANCEL, TranslateName, creater);
            }
            else
            {
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.ApplicationSysName = ApplicationSysName.APP_ORGANIC_PLANT_PRODUCTION_CANCEL;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.AppsHookClassName = "BizPortal.AppsHook.DOAOrganicPlantCancelAppHook";
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OrgCode = "07008000";
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.LogoFileID = null;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.HandbookUrl = "http://www.doa.go.th/main/";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenHandbookUrl = "http://www.doa.go.th/main/";  //Citizen
                //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OperatingDays = 5;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OperatingDayType = CostType.Range;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OperatingDays2 = 45;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.OperatingCost2 = null;
                //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenOperatingDays = 5;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenOperatingDayType = CostType.Range;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenOperatingDays2 = 45;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenOperatingCost = 0;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenOperatingCostType = CostType.Fixed;
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenOperatingCost2 = null;

                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.ShowRemark = true;  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";  //Juristic
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenShowRemark = true;  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";  //Citizen
                APP_ORGANIC_PLANT_PRODUCTION_CANCEL.SingleFormEnabled = true;
                string TranslateName = "ขอยกเลิกใบรับรองแหล่งผลิตพืชอินทรีย์ (แปลงเดียว/รายเดียว หรือกลุ่ม)";

                updateApplication(context, APP_ORGANIC_PLANT_PRODUCTION_CANCEL, TranslateName, creater);
            }
            #endregion

            #endregion

            #region [ สปา ]

            #region [ Renew ]
            Application APP_HEALTH_RENEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HEALTH_RENEW).FirstOrDefault();
            if (APP_HEALTH_RENEW == null)
            {
                APP_HEALTH_RENEW = new Application();
                APP_HEALTH_RENEW.ApplicationSysName = ApplicationSysName.APP_HEALTH_RENEW;
                APP_HEALTH_RENEW.OrgCode = "19007000";
                APP_HEALTH_RENEW.ApplicationUrl = null;
                APP_HEALTH_RENEW.ConsumerKey = null;
                APP_HEALTH_RENEW.IsDeleted = false;
                //APP_HEALTH_RENEW.CreatedUser = Auto gen
                //APP_HEALTH_RENEW.CreatedDate = Auto gen
                //APP_HEALTH_RENEW.UpdatedUserID = Auto gen
                //APP_HEALTH_RENEW.UpdatedDate = Auto gen
                APP_HEALTH_RENEW.DeletedUserID = null;
                APP_HEALTH_RENEW.DeletedDate = null;
                APP_HEALTH_RENEW.UrlCallBack = null;
                APP_HEALTH_RENEW.ParamCallBack = null;
                APP_HEALTH_RENEW.MultipleRequestSupport = true;
                APP_HEALTH_RENEW.AppsHookClassName = "BizPortal.AppsHook.SPAAppHook";
                APP_HEALTH_RENEW.SingleFormEnabled = true;
                APP_HEALTH_RENEW.HandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_HEALTH_RENEW.LogoFileID = null;
                APP_HEALTH_RENEW.CitizenApplicationUrl = null;
                APP_HEALTH_RENEW.CitizenHandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_HEALTH_RENEW.OperatingDays = 60;
                APP_HEALTH_RENEW.CitizenOperatingDays = 60;
                APP_HEALTH_RENEW.OperatingCost = 1000;
                APP_HEALTH_RENEW.OperatingCostType = "Range";
                APP_HEALTH_RENEW.OperatingCost2 = 11000;
                APP_HEALTH_RENEW.CitizenOperatingCost = 1000;
                APP_HEALTH_RENEW.CitizenOperatingCostType = "Range";
                APP_HEALTH_RENEW.CitizenOperatingCost2 = 11000;
                APP_HEALTH_RENEW.ShowRemark = true;
                APP_HEALTH_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HEALTH_RENEW.CitizenShowRemark = true;
                APP_HEALTH_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HEALTH_RENEW.RequestAtOrg = false;
                APP_HEALTH_RENEW.CitizenRequestAtOrg = false;
                APP_HEALTH_RENEW.OperatingDayType = "Fixed";
                APP_HEALTH_RENEW.OperatingDays2 = null;
                APP_HEALTH_RENEW.CitizenOperatingDays2 = null;
                APP_HEALTH_RENEW.TemporaryDisable = false;
                APP_HEALTH_RENEW.TemporaryRemark = null;
                APP_HEALTH_RENEW.FileOwner = null;
                APP_HEALTH_RENEW.StatusSequence = null;
                string TranslateName = "ขอต่ออายุใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา";
                createApplication(context, APP_HEALTH_RENEW, TranslateName, creater);
            }
            else
            {
                APP_HEALTH_RENEW.ApplicationSysName = ApplicationSysName.APP_HEALTH_RENEW;
                APP_HEALTH_RENEW.OrgCode = "19007000";
                APP_HEALTH_RENEW.ApplicationUrl = null;
                APP_HEALTH_RENEW.ConsumerKey = null;
                APP_HEALTH_RENEW.IsDeleted = false;
                //APP_HEALTH_RENEW.CreatedUser = Auto gen
                //APP_HEALTH_RENEW.CreatedDate = Auto gen
                //APP_HEALTH_RENEW.UpdatedUserID = Auto gen
                //APP_HEALTH_RENEW.UpdatedDate = Auto gen
                APP_HEALTH_RENEW.DeletedUserID = null;
                APP_HEALTH_RENEW.DeletedDate = null;
                APP_HEALTH_RENEW.UrlCallBack = null;
                APP_HEALTH_RENEW.ParamCallBack = null;
                APP_HEALTH_RENEW.MultipleRequestSupport = true;
                APP_HEALTH_RENEW.AppsHookClassName = "BizPortal.AppsHook.SPAAppHook";
                APP_HEALTH_RENEW.SingleFormEnabled = true;
                APP_HEALTH_RENEW.HandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_HEALTH_RENEW.LogoFileID = null;
                APP_HEALTH_RENEW.CitizenApplicationUrl = null;
                APP_HEALTH_RENEW.CitizenHandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_HEALTH_RENEW.OperatingDays = 60;
                APP_HEALTH_RENEW.CitizenOperatingDays = 60;
                APP_HEALTH_RENEW.OperatingCost = 1000;
                APP_HEALTH_RENEW.OperatingCostType = "Range";
                APP_HEALTH_RENEW.OperatingCost2 = 11000;
                APP_HEALTH_RENEW.CitizenOperatingCost = 1000;
                APP_HEALTH_RENEW.CitizenOperatingCostType = "Range";
                APP_HEALTH_RENEW.CitizenOperatingCost2 = 11000;
                APP_HEALTH_RENEW.ShowRemark = true;
                APP_HEALTH_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HEALTH_RENEW.CitizenShowRemark = true;
                APP_HEALTH_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HEALTH_RENEW.RequestAtOrg = false;
                APP_HEALTH_RENEW.CitizenRequestAtOrg = false;
                APP_HEALTH_RENEW.OperatingDayType = "Fixed";
                APP_HEALTH_RENEW.OperatingDays2 = null;
                APP_HEALTH_RENEW.CitizenOperatingDays2 = null;
                APP_HEALTH_RENEW.TemporaryDisable = false;
                APP_HEALTH_RENEW.TemporaryRemark = null;
                APP_HEALTH_RENEW.FileOwner = null;
                APP_HEALTH_RENEW.StatusSequence = null;
                string TranslateName = "ขอต่ออายุใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา";
                updateApplication(context, APP_HEALTH_RENEW, TranslateName, creater);
            }
            #endregion

            #region [ Edit ]
            Application APP_HEALTH_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HEALTH_EDIT).FirstOrDefault();
            if (APP_HEALTH_EDIT == null)
            {
                APP_HEALTH_EDIT = new Application();
                APP_HEALTH_EDIT.ApplicationSysName = ApplicationSysName.APP_HEALTH_EDIT;
                APP_HEALTH_EDIT.OrgCode = "19007000";
                APP_HEALTH_EDIT.ApplicationUrl = null;
                APP_HEALTH_EDIT.ConsumerKey = null;
                APP_HEALTH_EDIT.IsDeleted = false;
                //APP_HEALTH_RENEW.CreatedUser = Auto gen
                //APP_HEALTH_RENEW.CreatedDate = Auto gen
                //APP_HEALTH_RENEW.UpdatedUserID = Auto gen
                //APP_HEALTH_RENEW.UpdatedDate = Auto gen
                APP_HEALTH_EDIT.DeletedUserID = null;
                APP_HEALTH_EDIT.DeletedDate = null;
                APP_HEALTH_EDIT.UrlCallBack = null;
                APP_HEALTH_EDIT.ParamCallBack = null;
                APP_HEALTH_EDIT.MultipleRequestSupport = true;
                APP_HEALTH_EDIT.AppsHookClassName = "BizPortal.AppsHook.SPAAppHookEditOnly";
                APP_HEALTH_EDIT.SingleFormEnabled = true;
                APP_HEALTH_EDIT.HandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_HEALTH_EDIT.LogoFileID = null;
                APP_HEALTH_EDIT.CitizenApplicationUrl = null;
                APP_HEALTH_EDIT.CitizenHandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_HEALTH_EDIT.OperatingDays = 30;
                APP_HEALTH_EDIT.CitizenOperatingDays = 30;
                APP_HEALTH_EDIT.OperatingCost = 300;
                APP_HEALTH_EDIT.OperatingCostType = "Fixed";
                APP_HEALTH_EDIT.OperatingCost2 = 0;
                APP_HEALTH_EDIT.CitizenOperatingCost = 300;
                APP_HEALTH_EDIT.CitizenOperatingCostType = "Fixed";
                APP_HEALTH_EDIT.CitizenOperatingCost2 = 0;
                APP_HEALTH_EDIT.ShowRemark = true;
                APP_HEALTH_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HEALTH_EDIT.CitizenShowRemark = true;
                APP_HEALTH_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HEALTH_EDIT.RequestAtOrg = false;
                APP_HEALTH_EDIT.CitizenRequestAtOrg = false;
                APP_HEALTH_EDIT.OperatingDayType = "Fixed";
                APP_HEALTH_EDIT.OperatingDays2 = null;
                APP_HEALTH_EDIT.CitizenOperatingDays2 = null;
                APP_HEALTH_EDIT.TemporaryDisable = false;
                APP_HEALTH_EDIT.TemporaryRemark = null;
                APP_HEALTH_EDIT.FileOwner = null;
                APP_HEALTH_EDIT.StatusSequence = null;
                string TranslateName = "ขอแก้ไขใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา";
                createApplication(context, APP_HEALTH_EDIT, TranslateName, creater);
            }
            else
            {
                APP_HEALTH_EDIT.ApplicationSysName = ApplicationSysName.APP_HEALTH_EDIT;
                APP_HEALTH_EDIT.OrgCode = "19007000";
                APP_HEALTH_EDIT.ApplicationUrl = null;
                APP_HEALTH_EDIT.ConsumerKey = null;
                APP_HEALTH_EDIT.IsDeleted = false;
                //APP_HEALTH_RENEW.CreatedUser = Auto gen
                //APP_HEALTH_RENEW.CreatedDate = Auto gen
                //APP_HEALTH_RENEW.UpdatedUserID = Auto gen
                //APP_HEALTH_RENEW.UpdatedDate = Auto gen
                APP_HEALTH_EDIT.DeletedUserID = null;
                APP_HEALTH_EDIT.DeletedDate = null;
                APP_HEALTH_EDIT.UrlCallBack = null;
                APP_HEALTH_EDIT.ParamCallBack = null;
                APP_HEALTH_EDIT.MultipleRequestSupport = true;
                APP_HEALTH_EDIT.AppsHookClassName = "BizPortal.AppsHook.SPAAppHookEditOnly";
                APP_HEALTH_EDIT.SingleFormEnabled = true;
                APP_HEALTH_EDIT.HandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_HEALTH_EDIT.LogoFileID = null;
                APP_HEALTH_EDIT.CitizenApplicationUrl = null;
                APP_HEALTH_EDIT.CitizenHandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_HEALTH_EDIT.OperatingDays = 30;
                APP_HEALTH_EDIT.CitizenOperatingDays = 30;
                APP_HEALTH_EDIT.OperatingCost = 300;
                APP_HEALTH_EDIT.OperatingCostType = "Fixed";
                APP_HEALTH_EDIT.OperatingCost2 = 0;
                APP_HEALTH_EDIT.CitizenOperatingCost = 300;
                APP_HEALTH_EDIT.CitizenOperatingCostType = "Fixed";
                APP_HEALTH_EDIT.CitizenOperatingCost2 = 0;
                APP_HEALTH_EDIT.ShowRemark = true;
                APP_HEALTH_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HEALTH_EDIT.CitizenShowRemark = true;
                APP_HEALTH_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HEALTH_EDIT.RequestAtOrg = false;
                APP_HEALTH_EDIT.CitizenRequestAtOrg = false;
                APP_HEALTH_EDIT.OperatingDayType = "Fixed";
                APP_HEALTH_EDIT.OperatingDays2 = null;
                APP_HEALTH_EDIT.CitizenOperatingDays2 = null;
                APP_HEALTH_EDIT.TemporaryDisable = false;
                APP_HEALTH_EDIT.TemporaryRemark = null;
                APP_HEALTH_EDIT.FileOwner = null;
                APP_HEALTH_EDIT.StatusSequence = null;
                string TranslateName = "ขอแก้ไขใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา";
                updateApplication(context, APP_HEALTH_EDIT, TranslateName, creater);
            }
            #endregion

            #region [ Cancel ]
            Application APP_HEALTH_CANCEL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HEALTH_CANCEL).FirstOrDefault();
            if (APP_HEALTH_CANCEL == null)
            {
                APP_HEALTH_CANCEL = new Application();
                APP_HEALTH_CANCEL.ApplicationSysName = ApplicationSysName.APP_HEALTH_CANCEL;
                APP_HEALTH_CANCEL.OrgCode = "19007000";
                APP_HEALTH_CANCEL.ApplicationUrl = null;
                APP_HEALTH_CANCEL.ConsumerKey = null;
                APP_HEALTH_CANCEL.IsDeleted = false;
                //APP_HEALTH_RENEW.CreatedUser = Auto gen
                //APP_HEALTH_RENEW.CreatedDate = Auto gen
                //APP_HEALTH_RENEW.UpdatedUserID = Auto gen
                //APP_HEALTH_RENEW.UpdatedDate = Auto gen
                APP_HEALTH_CANCEL.DeletedUserID = null;
                APP_HEALTH_CANCEL.DeletedDate = null;
                APP_HEALTH_CANCEL.UrlCallBack = null;
                APP_HEALTH_CANCEL.ParamCallBack = null;
                APP_HEALTH_CANCEL.MultipleRequestSupport = true;
                APP_HEALTH_CANCEL.AppsHookClassName = "BizPortal.AppsHook.SPAAppHookCancelOnly";
                APP_HEALTH_CANCEL.SingleFormEnabled = true;
                APP_HEALTH_CANCEL.HandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_HEALTH_CANCEL.LogoFileID = null;
                APP_HEALTH_CANCEL.CitizenApplicationUrl = null;
                APP_HEALTH_CANCEL.CitizenHandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_HEALTH_CANCEL.OperatingDays = 30;
                APP_HEALTH_CANCEL.CitizenOperatingDays = 30;
                APP_HEALTH_CANCEL.OperatingCost = 0;
                APP_HEALTH_CANCEL.OperatingCostType = "Fixed";
                APP_HEALTH_CANCEL.OperatingCost2 = 0;
                APP_HEALTH_CANCEL.CitizenOperatingCost = 0;
                APP_HEALTH_CANCEL.CitizenOperatingCostType = "Fixed";
                APP_HEALTH_CANCEL.CitizenOperatingCost2 = 0;
                APP_HEALTH_CANCEL.ShowRemark = true;
                APP_HEALTH_CANCEL.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HEALTH_CANCEL.CitizenShowRemark = true;
                APP_HEALTH_CANCEL.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HEALTH_CANCEL.RequestAtOrg = false;
                APP_HEALTH_CANCEL.CitizenRequestAtOrg = false;
                APP_HEALTH_CANCEL.OperatingDayType = "Fixed";
                APP_HEALTH_CANCEL.OperatingDays2 = null;
                APP_HEALTH_CANCEL.CitizenOperatingDays2 = null;
                APP_HEALTH_CANCEL.TemporaryDisable = false;
                APP_HEALTH_CANCEL.TemporaryRemark = null;
                APP_HEALTH_CANCEL.FileOwner = null;
                APP_HEALTH_CANCEL.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา";
                createApplication(context, APP_HEALTH_CANCEL, TranslateName, creater);
            }
            else
            {
                APP_HEALTH_CANCEL.ApplicationSysName = ApplicationSysName.APP_HEALTH_CANCEL;
                APP_HEALTH_CANCEL.OrgCode = "19007000";
                APP_HEALTH_CANCEL.ApplicationUrl = null;
                APP_HEALTH_CANCEL.ConsumerKey = null;
                APP_HEALTH_CANCEL.IsDeleted = false;
                //APP_HEALTH_RENEW.CreatedUser = Auto gen
                //APP_HEALTH_RENEW.CreatedDate = Auto gen
                //APP_HEALTH_RENEW.UpdatedUserID = Auto gen
                //APP_HEALTH_RENEW.UpdatedDate = Auto gen
                APP_HEALTH_CANCEL.DeletedUserID = null;
                APP_HEALTH_CANCEL.DeletedDate = null;
                APP_HEALTH_CANCEL.UrlCallBack = null;
                APP_HEALTH_CANCEL.ParamCallBack = null;
                APP_HEALTH_CANCEL.MultipleRequestSupport = true;
                APP_HEALTH_CANCEL.AppsHookClassName = "BizPortal.AppsHook.SPAAppHookCancelOnly";
                APP_HEALTH_CANCEL.SingleFormEnabled = true;
                APP_HEALTH_CANCEL.HandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_HEALTH_CANCEL.LogoFileID = null;
                APP_HEALTH_CANCEL.CitizenApplicationUrl = null;
                APP_HEALTH_CANCEL.CitizenHandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_HEALTH_CANCEL.OperatingDays = 30;
                APP_HEALTH_CANCEL.CitizenOperatingDays = 30;
                APP_HEALTH_CANCEL.OperatingCost = 0;
                APP_HEALTH_CANCEL.OperatingCostType = "Fixed";
                APP_HEALTH_CANCEL.OperatingCost2 = 0;
                APP_HEALTH_CANCEL.CitizenOperatingCost = 0;
                APP_HEALTH_CANCEL.CitizenOperatingCostType = "Fixed";
                APP_HEALTH_CANCEL.CitizenOperatingCost2 = 0;
                APP_HEALTH_CANCEL.ShowRemark = true;
                APP_HEALTH_CANCEL.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HEALTH_CANCEL.CitizenShowRemark = true;
                APP_HEALTH_CANCEL.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HEALTH_CANCEL.RequestAtOrg = false;
                APP_HEALTH_CANCEL.CitizenRequestAtOrg = false;
                APP_HEALTH_CANCEL.OperatingDayType = "Fixed";
                APP_HEALTH_CANCEL.OperatingDays2 = null;
                APP_HEALTH_CANCEL.CitizenOperatingDays2 = null;
                APP_HEALTH_CANCEL.TemporaryDisable = false;
                APP_HEALTH_CANCEL.TemporaryRemark = null;
                APP_HEALTH_CANCEL.FileOwner = null;
                APP_HEALTH_CANCEL.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา";
                updateApplication(context, APP_HEALTH_CANCEL, TranslateName, creater);
            }
            #endregion

            #region งานบริการชำระค่าธรรมเนียมการประกอบกิจการสถานประกอบการเพื่อสุขภาพรายปี (สปา)
            Application APP_SPA_FEE_PER_YEAR = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SPA_FEE_PER_YEAR).FirstOrDefault();
            if (APP_SPA_FEE_PER_YEAR == null)
            {
                APP_SPA_FEE_PER_YEAR = new Application();
                APP_SPA_FEE_PER_YEAR.ApplicationSysName = ApplicationSysName.APP_SPA_FEE_PER_YEAR;
                APP_SPA_FEE_PER_YEAR.OrgCode = "19007000";
                APP_SPA_FEE_PER_YEAR.ApplicationUrl = null;
                APP_SPA_FEE_PER_YEAR.ConsumerKey = null;
                APP_SPA_FEE_PER_YEAR.IsDeleted = false;
                APP_SPA_FEE_PER_YEAR.DeletedUserID = null;
                APP_SPA_FEE_PER_YEAR.DeletedDate = null;
                APP_SPA_FEE_PER_YEAR.UrlCallBack = null;
                APP_SPA_FEE_PER_YEAR.ParamCallBack = null;
                APP_SPA_FEE_PER_YEAR.MultipleRequestSupport = true;
                APP_SPA_FEE_PER_YEAR.AppsHookClassName = "BizPortal.AppsHook.HSSSpaFeeApphook";
                APP_SPA_FEE_PER_YEAR.SingleFormEnabled = true;
                APP_SPA_FEE_PER_YEAR.HandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_SPA_FEE_PER_YEAR.LogoFileID = null;
                APP_SPA_FEE_PER_YEAR.CitizenApplicationUrl = null;
                APP_SPA_FEE_PER_YEAR.CitizenHandbookUrl = "http://hss.moph.go.th/index2.php";

                APP_SPA_FEE_PER_YEAR.OperatingCostType = CostType.Fixed;
                APP_SPA_FEE_PER_YEAR.OperatingCost = 1000;
                APP_SPA_FEE_PER_YEAR.OperatingCost2 = null;

                APP_SPA_FEE_PER_YEAR.CitizenOperatingCostType = CostType.Fixed;
                APP_SPA_FEE_PER_YEAR.CitizenOperatingCost = 1000;
                APP_SPA_FEE_PER_YEAR.CitizenOperatingCost2 = null;

                APP_SPA_FEE_PER_YEAR.ShowRemark = false;
                APP_SPA_FEE_PER_YEAR.CitizenShowRemark = false;

                APP_SPA_FEE_PER_YEAR.RequestAtOrg = false;
                APP_SPA_FEE_PER_YEAR.CitizenRequestAtOrg = false;


                APP_SPA_FEE_PER_YEAR.OperatingDayType = CostType.Fixed;
                APP_SPA_FEE_PER_YEAR.OperatingDays = 3;
                APP_SPA_FEE_PER_YEAR.OperatingDays2 = null;

                APP_SPA_FEE_PER_YEAR.CitizenOperatingDayType = CostType.Fixed;
                APP_SPA_FEE_PER_YEAR.CitizenOperatingDays = 3;
                APP_SPA_FEE_PER_YEAR.CitizenOperatingDays2 = null;


                APP_SPA_FEE_PER_YEAR.TemporaryDisable = false;
                APP_SPA_FEE_PER_YEAR.TemporaryRemark = null;
                APP_SPA_FEE_PER_YEAR.FileOwner = null;
                APP_SPA_FEE_PER_YEAR.StatusSequence = null;
                string TranslateName = "การชำระค่าธรรมเนียมการประกอบกิจการสถานประกอบการเพื่อสุขภาพรายปี (สปา)";
                createApplication(context, APP_SPA_FEE_PER_YEAR, TranslateName, creater);
            }
            else
            {
                APP_SPA_FEE_PER_YEAR.OrgCode = "19007000";
                APP_SPA_FEE_PER_YEAR.ApplicationUrl = null;
                APP_SPA_FEE_PER_YEAR.ConsumerKey = null;
                APP_SPA_FEE_PER_YEAR.IsDeleted = false;
                APP_SPA_FEE_PER_YEAR.DeletedUserID = null;
                APP_SPA_FEE_PER_YEAR.DeletedDate = null;
                APP_SPA_FEE_PER_YEAR.UrlCallBack = null;
                APP_SPA_FEE_PER_YEAR.ParamCallBack = null;
                APP_SPA_FEE_PER_YEAR.MultipleRequestSupport = true;
                APP_SPA_FEE_PER_YEAR.AppsHookClassName = "BizPortal.AppsHook.HSSSpaFeeApphook";
                APP_SPA_FEE_PER_YEAR.SingleFormEnabled = true;
                APP_SPA_FEE_PER_YEAR.HandbookUrl = "http://hss.moph.go.th/index2.php";
                APP_SPA_FEE_PER_YEAR.LogoFileID = null;
                APP_SPA_FEE_PER_YEAR.CitizenApplicationUrl = null;
                APP_SPA_FEE_PER_YEAR.CitizenHandbookUrl = "http://hss.moph.go.th/index2.php";

                APP_SPA_FEE_PER_YEAR.OperatingCostType = CostType.Fixed;
                APP_SPA_FEE_PER_YEAR.OperatingCost = 1000;
                APP_SPA_FEE_PER_YEAR.OperatingCost2 = null;

                APP_SPA_FEE_PER_YEAR.CitizenOperatingCostType = CostType.Fixed;
                APP_SPA_FEE_PER_YEAR.CitizenOperatingCost = 1000;
                APP_SPA_FEE_PER_YEAR.CitizenOperatingCost2 = null;

                APP_SPA_FEE_PER_YEAR.ShowRemark = false;
                APP_SPA_FEE_PER_YEAR.CitizenShowRemark = false;

                APP_SPA_FEE_PER_YEAR.RequestAtOrg = false;
                APP_SPA_FEE_PER_YEAR.CitizenRequestAtOrg = false;

                APP_SPA_FEE_PER_YEAR.OperatingDayType = CostType.Fixed;
                APP_SPA_FEE_PER_YEAR.OperatingDays = 3;
                APP_SPA_FEE_PER_YEAR.OperatingDays2 = null;

                APP_SPA_FEE_PER_YEAR.CitizenOperatingDayType = CostType.Fixed;
                APP_SPA_FEE_PER_YEAR.CitizenOperatingDays = 3;
                APP_SPA_FEE_PER_YEAR.CitizenOperatingDays2 = null;


                APP_SPA_FEE_PER_YEAR.TemporaryDisable = false;
                APP_SPA_FEE_PER_YEAR.TemporaryRemark = null;
                APP_SPA_FEE_PER_YEAR.FileOwner = null;
                APP_SPA_FEE_PER_YEAR.StatusSequence = null;
                string TranslateName = "การชำระค่าธรรมเนียมการประกอบกิจการสถานประกอบการเพื่อสุขภาพรายปี (สปา)";
                updateApplication(context, APP_SPA_FEE_PER_YEAR, TranslateName, creater);
            }
            #endregion

            #region การชำระค่าธรรมเนียมประจำปี (คลินิก)
            Application APP_CLINIC_NOT_ONE_NIGHT_STAND = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_CLINIC_NOT_ONE_NIGHT_STAND).FirstOrDefault();
            if (APP_CLINIC_NOT_ONE_NIGHT_STAND == null)
            {
                APP_CLINIC_NOT_ONE_NIGHT_STAND = new Application();
                APP_CLINIC_NOT_ONE_NIGHT_STAND.ApplicationSysName = ApplicationSysName.APP_CLINIC_NOT_ONE_NIGHT_STAND;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.OrgCode = "19007000";
                APP_CLINIC_NOT_ONE_NIGHT_STAND.ApplicationUrl = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.ConsumerKey = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.IsDeleted = false;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.DeletedUserID = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.DeletedDate = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.UrlCallBack = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.ParamCallBack = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.MultipleRequestSupport = true;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.AppsHookClassName = "BizPortal.AppsHook.HSSClinicFeeApphook";
                APP_CLINIC_NOT_ONE_NIGHT_STAND.SingleFormEnabled = true;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.HandbookUrl = "https://www.info.go.th/#!/th/search/76387/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%8A%E0%B8%B3%E0%B8%A3%E0%B8%B0%E0%B8%84%E0%B9%88%E0%B8%B2%E0%B8%98%E0%B8%A3%E0%B8%A3%E0%B8%A1%E0%B9%80%E0%B8%99%E0%B8%B5%E0%B8%A2%E0%B8%A1%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81%20(N)/";
                APP_CLINIC_NOT_ONE_NIGHT_STAND.LogoFileID = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenApplicationUrl = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/76387/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%8A%E0%B8%B3%E0%B8%A3%E0%B8%B0%E0%B8%84%E0%B9%88%E0%B8%B2%E0%B8%98%E0%B8%A3%E0%B8%A3%E0%B8%A1%E0%B9%80%E0%B8%99%E0%B8%B5%E0%B8%A2%E0%B8%A1%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81%20(N)/";

                APP_CLINIC_NOT_ONE_NIGHT_STAND.OperatingCostType = CostType.Fixed;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.OperatingCost = 500;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.OperatingCost2 = null;

                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenOperatingCostType = CostType.Fixed;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenOperatingCost = 500;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenOperatingCost2 = null;

                APP_CLINIC_NOT_ONE_NIGHT_STAND.ShowRemark = true;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* การให้บริการมีการปรังปรุงระยะเวลาเหลือ 1 ชั่วโมงแล้ว ขณะนี้อยู่ระหว่างการปรับปรุงระบบ</ span><br/>";
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenShowRemark = true;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* การให้บริการมีการปรังปรุงระยะเวลาเหลือ 1 ชั่วโมงแล้ว ขณะนี้อยู่ระหว่างการปรับปรุงระบบ</ span><br/>";

                APP_CLINIC_NOT_ONE_NIGHT_STAND.RequestAtOrg = false;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenRequestAtOrg = false;


                APP_CLINIC_NOT_ONE_NIGHT_STAND.OperatingDayType = CostType.Fixed;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.OperatingDays = 1;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.OperatingDays2 = null;

                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenOperatingDays = 1;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenOperatingDays2 = null;


                APP_CLINIC_NOT_ONE_NIGHT_STAND.TemporaryDisable = false;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.TemporaryRemark = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.FileOwner = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.StatusSequence = null;
                string TranslateName = "การชำระค่าธรรมเนียมประจำปี (คลินิก)";
                createApplication(context, APP_CLINIC_NOT_ONE_NIGHT_STAND, TranslateName, creater);
            }
            else
            {
                APP_CLINIC_NOT_ONE_NIGHT_STAND.OrgCode = "19007000";
                APP_CLINIC_NOT_ONE_NIGHT_STAND.ApplicationUrl = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.ConsumerKey = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.IsDeleted = false;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.DeletedUserID = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.DeletedDate = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.UrlCallBack = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.ParamCallBack = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.MultipleRequestSupport = true;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.AppsHookClassName = "BizPortal.AppsHook.HSSClinicFeeApphook";
                APP_CLINIC_NOT_ONE_NIGHT_STAND.SingleFormEnabled = true;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.HandbookUrl = "https://www.info.go.th/#!/th/search/76387/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%8A%E0%B8%B3%E0%B8%A3%E0%B8%B0%E0%B8%84%E0%B9%88%E0%B8%B2%E0%B8%98%E0%B8%A3%E0%B8%A3%E0%B8%A1%E0%B9%80%E0%B8%99%E0%B8%B5%E0%B8%A2%E0%B8%A1%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81%20(N)/";
                APP_CLINIC_NOT_ONE_NIGHT_STAND.LogoFileID = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenApplicationUrl = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/76387/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%8A%E0%B8%B3%E0%B8%A3%E0%B8%B0%E0%B8%84%E0%B9%88%E0%B8%B2%E0%B8%98%E0%B8%A3%E0%B8%A3%E0%B8%A1%E0%B9%80%E0%B8%99%E0%B8%B5%E0%B8%A2%E0%B8%A1%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81%20(N)/";

                APP_CLINIC_NOT_ONE_NIGHT_STAND.OperatingCostType = CostType.Fixed;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.OperatingCost = 500;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.OperatingCost2 = null;

                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenOperatingCostType = CostType.Fixed;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenOperatingCost = 500;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenOperatingCost2 = null;

                APP_CLINIC_NOT_ONE_NIGHT_STAND.ShowRemark = true;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* การให้บริการมีการปรังปรุงระยะเวลาเหลือ 1 ชั่วโมงแล้ว ขณะนี้อยู่ระหว่างการปรับปรุงระบบ</ span><br/>";
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenShowRemark = true;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* การให้บริการมีการปรังปรุงระยะเวลาเหลือ 1 ชั่วโมงแล้ว ขณะนี้อยู่ระหว่างการปรับปรุงระบบ</ span><br/>";

                APP_CLINIC_NOT_ONE_NIGHT_STAND.RequestAtOrg = false;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenRequestAtOrg = false;

                APP_CLINIC_NOT_ONE_NIGHT_STAND.OperatingDayType = CostType.Fixed;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.OperatingDays = 1;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.OperatingDays2 = null;

                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenOperatingDays = 1;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.CitizenOperatingDays2 = null;


                APP_CLINIC_NOT_ONE_NIGHT_STAND.TemporaryDisable = false;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.TemporaryRemark = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.FileOwner = null;
                APP_CLINIC_NOT_ONE_NIGHT_STAND.StatusSequence = null;
                string TranslateName = "การชำระค่าธรรมเนียมประจำปี (คลินิก)";
                updateApplication(context, APP_CLINIC_NOT_ONE_NIGHT_STAND, TranslateName, creater);
            }
            #endregion

            #region การชำระค่าธรรมเนียมประจำปี (โรงพยาบาล)

            Application APP_CLINIC_OVER_NIGHT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_CLINIC_OVER_NIGHT).FirstOrDefault();
            if (APP_CLINIC_OVER_NIGHT == null)
            {
                APP_CLINIC_OVER_NIGHT = new Application();
                APP_CLINIC_OVER_NIGHT.ApplicationSysName = ApplicationSysName.APP_CLINIC_OVER_NIGHT;
                APP_CLINIC_OVER_NIGHT.OrgCode = "19007000";
                APP_CLINIC_OVER_NIGHT.ApplicationUrl = null;
                APP_CLINIC_OVER_NIGHT.ConsumerKey = null;
                APP_CLINIC_OVER_NIGHT.IsDeleted = false;
                APP_CLINIC_OVER_NIGHT.DeletedUserID = null;
                APP_CLINIC_OVER_NIGHT.DeletedDate = null;
                APP_CLINIC_OVER_NIGHT.UrlCallBack = null;
                APP_CLINIC_OVER_NIGHT.ParamCallBack = null;
                APP_CLINIC_OVER_NIGHT.MultipleRequestSupport = true;
                APP_CLINIC_OVER_NIGHT.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalFeeApphook";
                APP_CLINIC_OVER_NIGHT.SingleFormEnabled = true;
                APP_CLINIC_OVER_NIGHT.HandbookUrl = "https://www.info.go.th/#!/th/search/75281/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_CLINIC_OVER_NIGHT.LogoFileID = null;
                APP_CLINIC_OVER_NIGHT.CitizenApplicationUrl = null;
                APP_CLINIC_OVER_NIGHT.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/75281/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_CLINIC_OVER_NIGHT.OperatingCostType = CostType.StartAt;
                APP_CLINIC_OVER_NIGHT.OperatingCost = 500;
                APP_CLINIC_OVER_NIGHT.OperatingCost2 = null;

                APP_CLINIC_OVER_NIGHT.CitizenOperatingCostType = CostType.StartAt;
                APP_CLINIC_OVER_NIGHT.CitizenOperatingCost = 500;
                APP_CLINIC_OVER_NIGHT.CitizenOperatingCost2 = null;

                APP_CLINIC_OVER_NIGHT.ShowRemark = true;
                APP_CLINIC_OVER_NIGHT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* การให้บริการมีการปรังปรุงระยะเวลาเหลือ 1 ชั่วโมงแล้ว ขณะนี้อยู่ระหว่างการปรับปรุงระบบ</ span><br/>";
                APP_CLINIC_OVER_NIGHT.CitizenShowRemark = true;
                APP_CLINIC_OVER_NIGHT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* การให้บริการมีการปรังปรุงระยะเวลาเหลือ 1 ชั่วโมงแล้ว ขณะนี้อยู่ระหว่างการปรับปรุงระบบ</ span><br/>";

                APP_CLINIC_OVER_NIGHT.RequestAtOrg = false;
                APP_CLINIC_OVER_NIGHT.CitizenRequestAtOrg = false;

                APP_CLINIC_OVER_NIGHT.OperatingDayType = CostType.Fixed;
                APP_CLINIC_OVER_NIGHT.OperatingDays = 1;
                APP_CLINIC_OVER_NIGHT.OperatingDays2 = null;

                APP_CLINIC_OVER_NIGHT.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_OVER_NIGHT.CitizenOperatingDays = 1;
                APP_CLINIC_OVER_NIGHT.CitizenOperatingDays2 = null;

                APP_CLINIC_OVER_NIGHT.TemporaryDisable = false;
                APP_CLINIC_OVER_NIGHT.TemporaryRemark = null;
                APP_CLINIC_OVER_NIGHT.FileOwner = null;
                APP_CLINIC_OVER_NIGHT.StatusSequence = null;
                string TranslateName = "การชำระค่าธรรมเนียมประจำปี (โรงพยาบาล)";
                createApplication(context, APP_CLINIC_OVER_NIGHT, TranslateName, creater);
            }
            else
            {

                APP_CLINIC_OVER_NIGHT.ApplicationSysName = ApplicationSysName.APP_CLINIC_OVER_NIGHT;
                APP_CLINIC_OVER_NIGHT.OrgCode = "19007000";
                APP_CLINIC_OVER_NIGHT.ApplicationUrl = null;
                APP_CLINIC_OVER_NIGHT.ConsumerKey = null;
                APP_CLINIC_OVER_NIGHT.IsDeleted = false;
                APP_CLINIC_OVER_NIGHT.DeletedUserID = null;
                APP_CLINIC_OVER_NIGHT.DeletedDate = null;
                APP_CLINIC_OVER_NIGHT.UrlCallBack = null;
                APP_CLINIC_OVER_NIGHT.ParamCallBack = null;
                APP_CLINIC_OVER_NIGHT.MultipleRequestSupport = true;
                APP_CLINIC_OVER_NIGHT.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalFeeApphook";
                APP_CLINIC_OVER_NIGHT.SingleFormEnabled = true;
                APP_CLINIC_OVER_NIGHT.HandbookUrl = "https://www.info.go.th/#!/th/search/75281/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_CLINIC_OVER_NIGHT.LogoFileID = null;
                APP_CLINIC_OVER_NIGHT.CitizenApplicationUrl = null;
                APP_CLINIC_OVER_NIGHT.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/75281/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_CLINIC_OVER_NIGHT.OperatingCostType = CostType.StartAt;
                APP_CLINIC_OVER_NIGHT.OperatingCost = 500;
                APP_CLINIC_OVER_NIGHT.OperatingCost2 = null;

                APP_CLINIC_OVER_NIGHT.CitizenOperatingCostType = CostType.StartAt;
                APP_CLINIC_OVER_NIGHT.CitizenOperatingCost = 500;
                APP_CLINIC_OVER_NIGHT.CitizenOperatingCost2 = null;

                APP_CLINIC_OVER_NIGHT.ShowRemark = true;
                APP_CLINIC_OVER_NIGHT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* การให้บริการมีการปรังปรุงระยะเวลาเหลือ 1 ชั่วโมงแล้ว ขณะนี้อยู่ระหว่างการปรับปรุงระบบ</ span><br/>";
                APP_CLINIC_OVER_NIGHT.CitizenShowRemark = true;
                APP_CLINIC_OVER_NIGHT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* การให้บริการมีการปรังปรุงระยะเวลาเหลือ 1 ชั่วโมงแล้ว ขณะนี้อยู่ระหว่างการปรับปรุงระบบ</ span><br/>";

                APP_CLINIC_OVER_NIGHT.RequestAtOrg = false;
                APP_CLINIC_OVER_NIGHT.CitizenRequestAtOrg = false;

                APP_CLINIC_OVER_NIGHT.OperatingDayType = CostType.Fixed;
                APP_CLINIC_OVER_NIGHT.OperatingDays = 1;
                APP_CLINIC_OVER_NIGHT.OperatingDays2 = null;

                APP_CLINIC_OVER_NIGHT.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_OVER_NIGHT.CitizenOperatingDays = 1;
                APP_CLINIC_OVER_NIGHT.CitizenOperatingDays2 = null;

                APP_CLINIC_OVER_NIGHT.TemporaryDisable = false;
                APP_CLINIC_OVER_NIGHT.TemporaryRemark = null;
                APP_CLINIC_OVER_NIGHT.FileOwner = null;
                APP_CLINIC_OVER_NIGHT.StatusSequence = null;
                string TranslateName = "การชำระค่าธรรมเนียมประจำปี (โรงพยาบาล)";
                updateApplication(context, APP_CLINIC_OVER_NIGHT, TranslateName, creater);
            }

            #endregion

            #endregion

            #region [ SEC ]

            Application APP_SEC_NEW_A = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_NEW_A).FirstOrDefault();
            if (APP_SEC_NEW_A == null)
            {
                APP_SEC_NEW_A = new Application();
                APP_SEC_NEW_A.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_A;
                APP_SEC_NEW_A.OrgCode = "21032000";
                APP_SEC_NEW_A.ApplicationUrl = null;
                APP_SEC_NEW_A.ConsumerKey = null;
                APP_SEC_NEW_A.IsDeleted = false;
                //APP_SEC_NEW_A.CreatedUser = Auto gen
                //APP_SEC_NEW_A.CreatedDate = Auto gen
                //APP_SEC_NEW_A.UpdatedUserID = Auto gen
                //APP_SEC_NEW_A.UpdatedDate = Auto gen
                APP_SEC_NEW_A.DeletedUserID = null;
                APP_SEC_NEW_A.DeletedDate = null;
                APP_SEC_NEW_A.UrlCallBack = null;
                APP_SEC_NEW_A.ParamCallBack = null;
                APP_SEC_NEW_A.MultipleRequestSupport = true;
                APP_SEC_NEW_A.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_A.SingleFormEnabled = true;
                APP_SEC_NEW_A.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_A.LogoFileID = null;
                APP_SEC_NEW_A.CitizenApplicationUrl = null;
                APP_SEC_NEW_A.CitizenHandbookUrl = null;
                APP_SEC_NEW_A.OperatingDays = 150;
                APP_SEC_NEW_A.CitizenOperatingDays = null;
                APP_SEC_NEW_A.OperatingCost = 0;
                APP_SEC_NEW_A.OperatingCostType = CostType.Range;
                APP_SEC_NEW_A.OperatingCost2 = 20000000;
                APP_SEC_NEW_A.CitizenOperatingCost = 0;
                APP_SEC_NEW_A.CitizenOperatingCostType = null;
                APP_SEC_NEW_A.CitizenOperatingCost2 = null;
                APP_SEC_NEW_A.ShowRemark = true;
                APP_SEC_NEW_A.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์แบบ ก และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้า แบบ ส-1 คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_A.CitizenShowRemark = true;
                APP_SEC_NEW_A.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ก และใบอนุญาต ประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1 ได้</span>"; ;
                APP_SEC_NEW_A.RequestAtOrg = false;
                APP_SEC_NEW_A.CitizenRequestAtOrg = true;
                APP_SEC_NEW_A.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_A.OperatingDays2 = 0;
                APP_SEC_NEW_A.CitizenOperatingDays2 = null;
                APP_SEC_NEW_A.TemporaryDisable = false;
                APP_SEC_NEW_A.TemporaryRemark = null;
                APP_SEC_NEW_A.FileOwner = null;
                APP_SEC_NEW_A.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ก และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1";
                createApplication(context, APP_SEC_NEW_A, TranslateName, creater);
            }
            else
            {
                APP_SEC_NEW_A.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_A;
                APP_SEC_NEW_A.OrgCode = "21032000";
                APP_SEC_NEW_A.ApplicationUrl = null;
                APP_SEC_NEW_A.ConsumerKey = null;
                APP_SEC_NEW_A.IsDeleted = false;
                //APP_SEC_NEW_A.CreatedUser = Auto gen
                //APP_SEC_NEW_A.CreatedDate = Auto gen
                //APP_SEC_NEW_A.UpdatedUserID = Auto gen
                //APP_SEC_NEW_A.UpdatedDate = Auto gen
                APP_SEC_NEW_A.DeletedUserID = null;
                APP_SEC_NEW_A.DeletedDate = null;
                APP_SEC_NEW_A.UrlCallBack = null;
                APP_SEC_NEW_A.ParamCallBack = null;
                APP_SEC_NEW_A.MultipleRequestSupport = true;
                APP_SEC_NEW_A.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_A.SingleFormEnabled = true;
                APP_SEC_NEW_A.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_A.LogoFileID = null;
                APP_SEC_NEW_A.CitizenApplicationUrl = null;
                APP_SEC_NEW_A.CitizenHandbookUrl = null;
                APP_SEC_NEW_A.OperatingDays = 150;
                APP_SEC_NEW_A.CitizenOperatingDays = null;
                APP_SEC_NEW_A.OperatingCost = 0;
                APP_SEC_NEW_A.OperatingCostType = CostType.Range;
                APP_SEC_NEW_A.OperatingCost2 = 20000000;
                APP_SEC_NEW_A.CitizenOperatingCost = 0;
                APP_SEC_NEW_A.CitizenOperatingCostType = null;
                APP_SEC_NEW_A.CitizenOperatingCost2 = null;
                APP_SEC_NEW_A.ShowRemark = true;
                APP_SEC_NEW_A.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์แบบ ก และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้า แบบ ส-1 คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_A.CitizenShowRemark = true;
                APP_SEC_NEW_A.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ก และใบอนุญาต ประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1 ได้</span>"; ;
                APP_SEC_NEW_A.RequestAtOrg = false;
                APP_SEC_NEW_A.CitizenRequestAtOrg = true;
                APP_SEC_NEW_A.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_A.OperatingDays2 = 0;
                APP_SEC_NEW_A.CitizenOperatingDays2 = null;
                APP_SEC_NEW_A.TemporaryDisable = false;
                APP_SEC_NEW_A.TemporaryRemark = null;
                APP_SEC_NEW_A.FileOwner = null;
                APP_SEC_NEW_A.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ก และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1";
                updateApplication(context, APP_SEC_NEW_A, TranslateName, creater);
            }

            Application APP_SEC_NEW_B = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_NEW_B).FirstOrDefault();
            if (APP_SEC_NEW_B == null)
            {
                APP_SEC_NEW_B = new Application();
                APP_SEC_NEW_B.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_B;
                APP_SEC_NEW_B.OrgCode = "21032000";
                APP_SEC_NEW_B.ApplicationUrl = null;
                APP_SEC_NEW_B.ConsumerKey = null;
                APP_SEC_NEW_B.IsDeleted = false;
                //APP_SEC_NEW_B.CreatedUser = Auto gen
                //APP_SEC_NEW_B.CreatedDate = Auto gen
                //APP_SEC_NEW_B.UpdatedUserID = Auto gen
                //APP_SEC_NEW_B.UpdatedDate = Auto gen
                APP_SEC_NEW_B.DeletedUserID = null;
                APP_SEC_NEW_B.DeletedDate = null;
                APP_SEC_NEW_B.UrlCallBack = null;
                APP_SEC_NEW_B.ParamCallBack = null;
                APP_SEC_NEW_B.MultipleRequestSupport = true;
                APP_SEC_NEW_B.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_B.SingleFormEnabled = true;
                APP_SEC_NEW_B.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_B.LogoFileID = null;
                APP_SEC_NEW_B.CitizenApplicationUrl = null;
                APP_SEC_NEW_B.CitizenHandbookUrl = null;
                APP_SEC_NEW_B.OperatingDays = 150;
                APP_SEC_NEW_B.CitizenOperatingDays = null;
                APP_SEC_NEW_B.OperatingCost = 0;
                APP_SEC_NEW_B.OperatingCostType = CostType.Range;
                APP_SEC_NEW_B.OperatingCost2 = 5000000;
                APP_SEC_NEW_B.CitizenOperatingCost = 0;
                APP_SEC_NEW_B.CitizenOperatingCostType = null;
                APP_SEC_NEW_B.CitizenOperatingCost2 = null;
                APP_SEC_NEW_B.ShowRemark = true;
                APP_SEC_NEW_B.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์แบบ ข และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้า แบบ ส-2 คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_B.CitizenShowRemark = true;
                APP_SEC_NEW_B.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ข และใบอนุญาต ประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-2 ได้</span>"; ;
                APP_SEC_NEW_B.RequestAtOrg = false;
                APP_SEC_NEW_B.CitizenRequestAtOrg = true;
                APP_SEC_NEW_B.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_B.OperatingDays2 = 0;
                APP_SEC_NEW_B.CitizenOperatingDays2 = null;
                APP_SEC_NEW_B.TemporaryDisable = false;
                APP_SEC_NEW_B.TemporaryRemark = null;
                APP_SEC_NEW_B.FileOwner = null;
                APP_SEC_NEW_B.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ข และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-2";
                createApplication(context, APP_SEC_NEW_B, TranslateName, creater);
            }
            else
            {
                APP_SEC_NEW_B.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_B;
                APP_SEC_NEW_B.OrgCode = "21032000";
                APP_SEC_NEW_B.ApplicationUrl = null;
                APP_SEC_NEW_B.ConsumerKey = null;
                APP_SEC_NEW_B.IsDeleted = false;
                //APP_SEC_NEW_B.CreatedUser = Auto gen
                //APP_SEC_NEW_B.CreatedDate = Auto gen
                //APP_SEC_NEW_B.UpdatedUserID = Auto gen
                //APP_SEC_NEW_B.UpdatedDate = Auto gen
                APP_SEC_NEW_B.DeletedUserID = null;
                APP_SEC_NEW_B.DeletedDate = null;
                APP_SEC_NEW_B.UrlCallBack = null;
                APP_SEC_NEW_B.ParamCallBack = null;
                APP_SEC_NEW_B.MultipleRequestSupport = true;
                APP_SEC_NEW_B.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_B.SingleFormEnabled = true;
                APP_SEC_NEW_B.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_B.LogoFileID = null;
                APP_SEC_NEW_B.CitizenApplicationUrl = null;
                APP_SEC_NEW_B.CitizenHandbookUrl = null;
                APP_SEC_NEW_B.OperatingDays = 150;
                APP_SEC_NEW_B.CitizenOperatingDays = null;
                APP_SEC_NEW_B.OperatingCost = 0;
                APP_SEC_NEW_B.OperatingCostType = CostType.Range;
                APP_SEC_NEW_B.OperatingCost2 = 5000000;
                APP_SEC_NEW_B.CitizenOperatingCost = 0;
                APP_SEC_NEW_B.CitizenOperatingCostType = null;
                APP_SEC_NEW_B.CitizenOperatingCost2 = null;
                APP_SEC_NEW_B.ShowRemark = true;
                APP_SEC_NEW_B.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์แบบ ข และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้า แบบ ส-2 คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_B.CitizenShowRemark = true;
                APP_SEC_NEW_B.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ข และใบอนุญาต ประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-2 ได้</span>"; ;
                APP_SEC_NEW_B.RequestAtOrg = false;
                APP_SEC_NEW_B.CitizenRequestAtOrg = true;
                APP_SEC_NEW_B.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_B.OperatingDays2 = 0;
                APP_SEC_NEW_B.CitizenOperatingDays2 = null;
                APP_SEC_NEW_B.TemporaryDisable = false;
                APP_SEC_NEW_B.TemporaryRemark = null;
                APP_SEC_NEW_B.FileOwner = null;
                APP_SEC_NEW_B.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ข และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-2";
                updateApplication(context, APP_SEC_NEW_B, TranslateName, creater);
            }

            Application APP_SEC_NEW_C = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_NEW_C).FirstOrDefault();
            if (APP_SEC_NEW_C == null)
            {
                APP_SEC_NEW_C = new Application();
                APP_SEC_NEW_C.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_C;
                APP_SEC_NEW_C.OrgCode = "21032000";
                APP_SEC_NEW_C.ApplicationUrl = null;
                APP_SEC_NEW_C.ConsumerKey = null;
                APP_SEC_NEW_C.IsDeleted = false;
                //APP_SEC_NEW_C.CreatedUser = Auto gen
                //APP_SEC_NEW_C.CreatedDate = Auto gen
                //APP_SEC_NEW_C.UpdatedUserID = Auto gen
                //APP_SEC_NEW_C.UpdatedDate = Auto gen
                APP_SEC_NEW_C.DeletedUserID = null;
                APP_SEC_NEW_C.DeletedDate = null;
                APP_SEC_NEW_C.UrlCallBack = null;
                APP_SEC_NEW_C.ParamCallBack = null;
                APP_SEC_NEW_C.MultipleRequestSupport = true;
                APP_SEC_NEW_C.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_C.SingleFormEnabled = true;
                APP_SEC_NEW_C.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_C.LogoFileID = null;
                APP_SEC_NEW_C.CitizenApplicationUrl = null;
                APP_SEC_NEW_C.CitizenHandbookUrl = null;
                APP_SEC_NEW_C.OperatingDays = 150;
                APP_SEC_NEW_C.CitizenOperatingDays = null;
                APP_SEC_NEW_C.OperatingCost = 0;
                APP_SEC_NEW_C.OperatingCostType = CostType.Range;
                APP_SEC_NEW_C.OperatingCost2 = 5000000;
                APP_SEC_NEW_C.CitizenOperatingCost = 0;
                APP_SEC_NEW_C.CitizenOperatingCostType = null;
                APP_SEC_NEW_C.CitizenOperatingCost2 = null;
                APP_SEC_NEW_C.ShowRemark = true;
                APP_SEC_NEW_C.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ค และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้า ประเภทการเป็นผู้จัดการทุนสัญญาซื้อขายล่วงหน้าผู้จัดการเงินทุนสัญญา คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_C.CitizenShowRemark = true;
                APP_SEC_NEW_C.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ค และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้า ประเภทการเป็นผู้จัดการทุนสัญญา ซื้อขายล่วงหน้าผู้จัดการเงินทุนสัญญาได้</span>"; ;
                APP_SEC_NEW_C.RequestAtOrg = false;
                APP_SEC_NEW_C.CitizenRequestAtOrg = true;
                APP_SEC_NEW_C.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_C.OperatingDays2 = 0;
                APP_SEC_NEW_C.CitizenOperatingDays2 = null;
                APP_SEC_NEW_C.TemporaryDisable = false;
                APP_SEC_NEW_C.TemporaryRemark = null;
                APP_SEC_NEW_C.FileOwner = null;
                APP_SEC_NEW_C.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ค และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้า ประเภทการเป็นผู้จัดการทุนสัญญา ซื้อขายล่วงหน้าผู้จัดการเงินทุนสัญญา";
                createApplication(context, APP_SEC_NEW_C, TranslateName, creater);
            }
            else
            {
                APP_SEC_NEW_C.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_C;
                APP_SEC_NEW_C.OrgCode = "21032000";
                APP_SEC_NEW_C.ApplicationUrl = null;
                APP_SEC_NEW_C.ConsumerKey = null;
                APP_SEC_NEW_C.IsDeleted = false;
                //APP_SEC_NEW_C.CreatedUser = Auto gen
                //APP_SEC_NEW_C.CreatedDate = Auto gen
                //APP_SEC_NEW_C.UpdatedUserID = Auto gen
                //APP_SEC_NEW_C.UpdatedDate = Auto gen
                APP_SEC_NEW_C.DeletedUserID = null;
                APP_SEC_NEW_C.DeletedDate = null;
                APP_SEC_NEW_C.UrlCallBack = null;
                APP_SEC_NEW_C.ParamCallBack = null;
                APP_SEC_NEW_C.MultipleRequestSupport = true;
                APP_SEC_NEW_C.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_C.SingleFormEnabled = true;
                APP_SEC_NEW_C.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_C.LogoFileID = null;
                APP_SEC_NEW_C.CitizenApplicationUrl = null;
                APP_SEC_NEW_C.CitizenHandbookUrl = null;
                APP_SEC_NEW_C.OperatingDays = 150;
                APP_SEC_NEW_C.CitizenOperatingDays = null;
                APP_SEC_NEW_C.OperatingCost = 0;
                APP_SEC_NEW_C.OperatingCostType = CostType.Range;
                APP_SEC_NEW_C.OperatingCost2 = 5000000;
                APP_SEC_NEW_C.CitizenOperatingCost = 0;
                APP_SEC_NEW_C.CitizenOperatingCostType = null;
                APP_SEC_NEW_C.CitizenOperatingCost2 = null;
                APP_SEC_NEW_C.ShowRemark = true;
                APP_SEC_NEW_C.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ค และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้า ประเภทการเป็นผู้จัดการทุนสัญญาซื้อขายล่วงหน้าผู้จัดการเงินทุนสัญญา คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_C.CitizenShowRemark = true;
                APP_SEC_NEW_C.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ค และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้า ประเภทการเป็นผู้จัดการทุนสัญญา ซื้อขายล่วงหน้าผู้จัดการเงินทุนสัญญาได้</span>"; ;
                APP_SEC_NEW_C.RequestAtOrg = false;
                APP_SEC_NEW_C.CitizenRequestAtOrg = true;
                APP_SEC_NEW_C.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_C.OperatingDays2 = 0;
                APP_SEC_NEW_C.CitizenOperatingDays2 = null;
                APP_SEC_NEW_C.TemporaryDisable = false;
                APP_SEC_NEW_C.TemporaryRemark = null;
                APP_SEC_NEW_C.FileOwner = null;
                APP_SEC_NEW_C.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ค และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้า ประเภทการเป็นผู้จัดการทุนสัญญา ซื้อขายล่วงหน้าผู้จัดการเงินทุนสัญญา";
                updateApplication(context, APP_SEC_NEW_C, TranslateName, creater);
            }

            Application APP_SEC_NEW_D = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_NEW_D).FirstOrDefault();
            if (APP_SEC_NEW_D == null)
            {
                APP_SEC_NEW_D = new Application();
                APP_SEC_NEW_D.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_D;
                APP_SEC_NEW_D.OrgCode = "21032000";
                APP_SEC_NEW_D.ApplicationUrl = null;
                APP_SEC_NEW_D.ConsumerKey = null;
                APP_SEC_NEW_D.IsDeleted = false;
                //APP_SEC_NEW_D.CreatedUser = Auto gen
                //APP_SEC_NEW_D.CreatedDate = Auto gen
                //APP_SEC_NEW_D.UpdatedUserID = Auto gen
                //APP_SEC_NEW_D.UpdatedDate = Auto gen
                APP_SEC_NEW_D.DeletedUserID = null;
                APP_SEC_NEW_D.DeletedDate = null;
                APP_SEC_NEW_D.UrlCallBack = null;
                APP_SEC_NEW_D.ParamCallBack = null;
                APP_SEC_NEW_D.MultipleRequestSupport = true;
                APP_SEC_NEW_D.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_D.SingleFormEnabled = true;
                APP_SEC_NEW_D.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_D.LogoFileID = null;
                APP_SEC_NEW_D.CitizenApplicationUrl = null;
                APP_SEC_NEW_D.CitizenHandbookUrl = null;
                APP_SEC_NEW_D.OperatingDays = 150;
                APP_SEC_NEW_D.CitizenOperatingDays = null;
                APP_SEC_NEW_D.OperatingCost = 0;
                APP_SEC_NEW_D.OperatingCostType = CostType.Range;
                APP_SEC_NEW_D.OperatingCost2 = 30000;
                APP_SEC_NEW_D.CitizenOperatingCost = 0;
                APP_SEC_NEW_D.CitizenOperatingCostType = null;
                APP_SEC_NEW_D.CitizenOperatingCost2 = null;
                APP_SEC_NEW_D.ShowRemark = true;
                APP_SEC_NEW_D.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ง คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_D.CitizenShowRemark = true;
                APP_SEC_NEW_D.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ง ได้</span>"; ;
                APP_SEC_NEW_D.RequestAtOrg = false;
                APP_SEC_NEW_D.CitizenRequestAtOrg = true;
                APP_SEC_NEW_D.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_D.OperatingDays2 = 0;
                APP_SEC_NEW_D.CitizenOperatingDays2 = null;
                APP_SEC_NEW_D.TemporaryDisable = false;
                APP_SEC_NEW_D.TemporaryRemark = null;
                APP_SEC_NEW_D.FileOwner = null;
                APP_SEC_NEW_D.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ง";
                createApplication(context, APP_SEC_NEW_D, TranslateName, creater);
            }
            else
            {
                APP_SEC_NEW_D.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_D;
                APP_SEC_NEW_D.OrgCode = "21032000";
                APP_SEC_NEW_D.ApplicationUrl = null;
                APP_SEC_NEW_D.ConsumerKey = null;
                APP_SEC_NEW_D.IsDeleted = false;
                //APP_SEC_NEW_D.CreatedUser = Auto gen
                //APP_SEC_NEW_D.CreatedDate = Auto gen
                //APP_SEC_NEW_D.UpdatedUserID = Auto gen
                //APP_SEC_NEW_D.UpdatedDate = Auto gen
                APP_SEC_NEW_D.DeletedUserID = null;
                APP_SEC_NEW_D.DeletedDate = null;
                APP_SEC_NEW_D.UrlCallBack = null;
                APP_SEC_NEW_D.ParamCallBack = null;
                APP_SEC_NEW_D.MultipleRequestSupport = true;
                APP_SEC_NEW_D.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_D.SingleFormEnabled = true;
                APP_SEC_NEW_D.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_D.LogoFileID = null;
                APP_SEC_NEW_D.CitizenApplicationUrl = null;
                APP_SEC_NEW_D.CitizenHandbookUrl = null;
                APP_SEC_NEW_D.OperatingDays = 150;
                APP_SEC_NEW_D.CitizenOperatingDays = null;
                APP_SEC_NEW_D.OperatingCost = 0;
                APP_SEC_NEW_D.OperatingCostType = CostType.Range;
                APP_SEC_NEW_D.OperatingCost2 = 30000;
                APP_SEC_NEW_D.CitizenOperatingCost = 0;
                APP_SEC_NEW_D.CitizenOperatingCostType = null;
                APP_SEC_NEW_D.CitizenOperatingCost2 = null;
                APP_SEC_NEW_D.ShowRemark = true;
                APP_SEC_NEW_D.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ง คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_D.CitizenShowRemark = true;
                APP_SEC_NEW_D.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ง ได้</span>"; ;
                APP_SEC_NEW_D.RequestAtOrg = false;
                APP_SEC_NEW_D.CitizenRequestAtOrg = true;
                APP_SEC_NEW_D.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_D.OperatingDays2 = 0;
                APP_SEC_NEW_D.CitizenOperatingDays2 = null;
                APP_SEC_NEW_D.TemporaryDisable = false;
                APP_SEC_NEW_D.TemporaryRemark = null;
                APP_SEC_NEW_D.FileOwner = null;
                APP_SEC_NEW_D.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ง";
                updateApplication(context, APP_SEC_NEW_D, TranslateName, creater);
            }

            Application APP_SEC_NEW_E = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_NEW_E).FirstOrDefault();
            if (APP_SEC_NEW_E == null)
            {
                APP_SEC_NEW_E = new Application();
                APP_SEC_NEW_E.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_E;
                APP_SEC_NEW_E.OrgCode = "21032000";
                APP_SEC_NEW_E.ApplicationUrl = null;
                APP_SEC_NEW_E.ConsumerKey = null;
                APP_SEC_NEW_E.IsDeleted = false;
                //APP_SEC_NEW_E.CreatedUser = Auto gen
                //APP_SEC_NEW_E.CreatedDate = Auto gen
                //APP_SEC_NEW_E.UpdatedUserID = Auto gen
                //APP_SEC_NEW_E.UpdatedDate = Auto gen
                APP_SEC_NEW_E.DeletedUserID = null;
                APP_SEC_NEW_E.DeletedDate = null;
                APP_SEC_NEW_E.UrlCallBack = null;
                APP_SEC_NEW_E.ParamCallBack = null;
                APP_SEC_NEW_E.MultipleRequestSupport = true;
                APP_SEC_NEW_E.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_E.SingleFormEnabled = true;
                APP_SEC_NEW_E.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_E.LogoFileID = null;
                APP_SEC_NEW_E.CitizenApplicationUrl = null;
                APP_SEC_NEW_E.CitizenHandbookUrl = null;
                APP_SEC_NEW_E.OperatingDays = 150;
                APP_SEC_NEW_E.CitizenOperatingDays = null;
                APP_SEC_NEW_E.OperatingCost = 0;
                APP_SEC_NEW_E.OperatingCostType = CostType.Range;
                APP_SEC_NEW_E.OperatingCost2 = 30000;
                APP_SEC_NEW_E.CitizenOperatingCost = 0;
                APP_SEC_NEW_E.CitizenOperatingCostType = null;
                APP_SEC_NEW_E.CitizenOperatingCost2 = null;
                APP_SEC_NEW_E.ShowRemark = true;
                APP_SEC_NEW_E.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทการเป็นที่ปรึกษา การลงทุน และใบอนุญาตประกอบธุรกิจที่ปรึกษาสัญญาซื้อขายล่วงหน้า คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_E.CitizenShowRemark = true;
                APP_SEC_NEW_E.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทการเป็นที่ปรึกษา การลงทุน และใบอนุญาตประกอบธุรกิจที่ปรึกษาสัญญาซื้อขายล่วงหน้าได้</span>"; ;
                APP_SEC_NEW_E.RequestAtOrg = false;
                APP_SEC_NEW_E.CitizenRequestAtOrg = true;
                APP_SEC_NEW_E.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_E.OperatingDays2 = 0;
                APP_SEC_NEW_E.CitizenOperatingDays2 = null;
                APP_SEC_NEW_E.TemporaryDisable = false;
                APP_SEC_NEW_E.TemporaryRemark = null;
                APP_SEC_NEW_E.FileOwner = null;
                APP_SEC_NEW_E.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทการเป็นที่ปรึกษา การลงทุน และใบอนุญาตประกอบธุรกิจที่ปรึกษาสัญญาซื้อขายล่วงหน้า";
                createApplication(context, APP_SEC_NEW_E, TranslateName, creater);
            }
            else
            {
                APP_SEC_NEW_E.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_E;
                APP_SEC_NEW_E.OrgCode = "21032000";
                APP_SEC_NEW_E.ApplicationUrl = null;
                APP_SEC_NEW_E.ConsumerKey = null;
                APP_SEC_NEW_E.IsDeleted = false;
                //APP_SEC_NEW_E.CreatedUser = Auto gen
                //APP_SEC_NEW_E.CreatedDate = Auto gen
                //APP_SEC_NEW_E.UpdatedUserID = Auto gen
                //APP_SEC_NEW_E.UpdatedDate = Auto gen
                APP_SEC_NEW_E.DeletedUserID = null;
                APP_SEC_NEW_E.DeletedDate = null;
                APP_SEC_NEW_E.UrlCallBack = null;
                APP_SEC_NEW_E.ParamCallBack = null;
                APP_SEC_NEW_E.MultipleRequestSupport = true;
                APP_SEC_NEW_E.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_E.SingleFormEnabled = true;
                APP_SEC_NEW_E.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_E.LogoFileID = null;
                APP_SEC_NEW_E.CitizenApplicationUrl = null;
                APP_SEC_NEW_E.CitizenHandbookUrl = null;
                APP_SEC_NEW_E.OperatingDays = 150;
                APP_SEC_NEW_E.CitizenOperatingDays = null;
                APP_SEC_NEW_E.OperatingCost = 0;
                APP_SEC_NEW_E.OperatingCostType = CostType.Range;
                APP_SEC_NEW_E.OperatingCost2 = 30000;
                APP_SEC_NEW_E.CitizenOperatingCost = 0;
                APP_SEC_NEW_E.CitizenOperatingCostType = null;
                APP_SEC_NEW_E.CitizenOperatingCost2 = null;
                APP_SEC_NEW_E.ShowRemark = true;
                APP_SEC_NEW_E.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทการเป็นที่ปรึกษา การลงทุน และใบอนุญาตประกอบธุรกิจที่ปรึกษาสัญญาซื้อขายล่วงหน้า คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_E.CitizenShowRemark = true;
                APP_SEC_NEW_E.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทการเป็นที่ปรึกษา การลงทุน และใบอนุญาตประกอบธุรกิจที่ปรึกษาสัญญาซื้อขายล่วงหน้าได้</span>"; ;
                APP_SEC_NEW_E.RequestAtOrg = false;
                APP_SEC_NEW_E.CitizenRequestAtOrg = true;
                APP_SEC_NEW_E.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_E.OperatingDays2 = 0;
                APP_SEC_NEW_E.CitizenOperatingDays2 = null;
                APP_SEC_NEW_E.TemporaryDisable = false;
                APP_SEC_NEW_E.TemporaryRemark = null;
                APP_SEC_NEW_E.FileOwner = null;
                APP_SEC_NEW_E.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทการเป็นที่ปรึกษา การลงทุน และใบอนุญาตประกอบธุรกิจที่ปรึกษาสัญญาซื้อขายล่วงหน้า";
                updateApplication(context, APP_SEC_NEW_E, TranslateName, creater);
            }

            Application APP_SEC_NEW_F = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_NEW_F).FirstOrDefault();
            if (APP_SEC_NEW_F == null)
            {
                APP_SEC_NEW_F = new Application();
                APP_SEC_NEW_F.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_F;
                APP_SEC_NEW_F.OrgCode = "21032000";
                APP_SEC_NEW_F.ApplicationUrl = null;
                APP_SEC_NEW_F.ConsumerKey = null;
                APP_SEC_NEW_F.IsDeleted = false;
                //APP_SEC_NEW_F.CreatedUser = Auto gen
                //APP_SEC_NEW_F.CreatedDate = Auto gen
                //APP_SEC_NEW_F.UpdatedUserID = Auto gen
                //APP_SEC_NEW_F.UpdatedDate = Auto gen
                APP_SEC_NEW_F.DeletedUserID = null;
                APP_SEC_NEW_F.DeletedDate = null;
                APP_SEC_NEW_F.UrlCallBack = null;
                APP_SEC_NEW_F.ParamCallBack = null;
                APP_SEC_NEW_F.MultipleRequestSupport = true;
                APP_SEC_NEW_F.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_F.SingleFormEnabled = true;
                APP_SEC_NEW_F.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_F.LogoFileID = null;
                APP_SEC_NEW_F.CitizenApplicationUrl = null;
                APP_SEC_NEW_F.CitizenHandbookUrl = null;
                APP_SEC_NEW_F.OperatingDays = 150;
                APP_SEC_NEW_F.CitizenOperatingDays = null;
                APP_SEC_NEW_F.OperatingCost = 0;
                APP_SEC_NEW_F.OperatingCostType = CostType.Range;
                APP_SEC_NEW_F.OperatingCost2 = 30000;
                APP_SEC_NEW_F.CitizenOperatingCost = 0;
                APP_SEC_NEW_F.CitizenOperatingCostType = null;
                APP_SEC_NEW_F.CitizenOperatingCost2 = null;
                APP_SEC_NEW_F.ShowRemark = true;
                APP_SEC_NEW_F.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ ประเภทกิจการการยืมและให้ยืมหลักทรัพย์ คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_F.CitizenShowRemark = true;
                APP_SEC_NEW_F.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทกิจการการยืมและให้ยืมหลักทรัพย์ได้</span>"; ;
                APP_SEC_NEW_F.RequestAtOrg = false;
                APP_SEC_NEW_F.CitizenRequestAtOrg = true;
                APP_SEC_NEW_F.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_F.OperatingDays2 = 0;
                APP_SEC_NEW_F.CitizenOperatingDays2 = null;
                APP_SEC_NEW_F.TemporaryDisable = false;
                APP_SEC_NEW_F.TemporaryRemark = null;
                APP_SEC_NEW_F.FileOwner = null;
                APP_SEC_NEW_F.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทกิจการการยืมและให้ยืมหลักทรัพย์";
                createApplication(context, APP_SEC_NEW_F, TranslateName, creater);
            }
            else
            {
                APP_SEC_NEW_F.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_F;
                APP_SEC_NEW_F.OrgCode = "21032000";
                APP_SEC_NEW_F.ApplicationUrl = null;
                APP_SEC_NEW_F.ConsumerKey = null;
                APP_SEC_NEW_F.IsDeleted = false;
                //APP_SEC_NEW_F.CreatedUser = Auto gen
                //APP_SEC_NEW_F.CreatedDate = Auto gen
                //APP_SEC_NEW_F.UpdatedUserID = Auto gen
                //APP_SEC_NEW_F.UpdatedDate = Auto gen
                APP_SEC_NEW_F.DeletedUserID = null;
                APP_SEC_NEW_F.DeletedDate = null;
                APP_SEC_NEW_F.UrlCallBack = null;
                APP_SEC_NEW_F.ParamCallBack = null;
                APP_SEC_NEW_F.MultipleRequestSupport = true;
                APP_SEC_NEW_F.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_F.SingleFormEnabled = true;
                APP_SEC_NEW_F.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_F.LogoFileID = null;
                APP_SEC_NEW_F.CitizenApplicationUrl = null;
                APP_SEC_NEW_F.CitizenHandbookUrl = null;
                APP_SEC_NEW_F.OperatingDays = 150;
                APP_SEC_NEW_F.CitizenOperatingDays = null;
                APP_SEC_NEW_F.OperatingCost = 0;
                APP_SEC_NEW_F.OperatingCostType = CostType.Range;
                APP_SEC_NEW_F.OperatingCost2 = 30000;
                APP_SEC_NEW_F.CitizenOperatingCost = 0;
                APP_SEC_NEW_F.CitizenOperatingCostType = null;
                APP_SEC_NEW_F.CitizenOperatingCost2 = null;
                APP_SEC_NEW_F.ShowRemark = true;
                APP_SEC_NEW_F.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจหลักทรัพย์ ประเภทกิจการการยืมและให้ยืมหลักทรัพย์ คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_F.CitizenShowRemark = true;
                APP_SEC_NEW_F.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทกิจการการยืมและให้ยืมหลักทรัพย์ได้</span>"; ;
                APP_SEC_NEW_F.RequestAtOrg = false;
                APP_SEC_NEW_F.CitizenRequestAtOrg = true;
                APP_SEC_NEW_F.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_F.OperatingDays2 = 0;
                APP_SEC_NEW_F.CitizenOperatingDays2 = null;
                APP_SEC_NEW_F.TemporaryDisable = false;
                APP_SEC_NEW_F.TemporaryRemark = null;
                APP_SEC_NEW_F.FileOwner = null;
                APP_SEC_NEW_F.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทกิจการการยืมและให้ยืมหลักทรัพย์";
                updateApplication(context, APP_SEC_NEW_F, TranslateName, creater);
            }

            Application APP_SEC_NEW_G = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_NEW_G).FirstOrDefault();
            if (APP_SEC_NEW_G == null)
            {
                APP_SEC_NEW_G = new Application();
                APP_SEC_NEW_G.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_G;
                APP_SEC_NEW_G.OrgCode = "21032000";
                APP_SEC_NEW_G.ApplicationUrl = null;
                APP_SEC_NEW_G.ConsumerKey = null;
                APP_SEC_NEW_G.IsDeleted = false;
                //APP_SEC_NEW_G.CreatedUser = Auto gen
                //APP_SEC_NEW_G.CreatedDate = Auto gen
                //APP_SEC_NEW_G.UpdatedUserID = Auto gen
                //APP_SEC_NEW_G.UpdatedDate = Auto gen
                APP_SEC_NEW_G.DeletedUserID = null;
                APP_SEC_NEW_G.DeletedDate = null;
                APP_SEC_NEW_G.UrlCallBack = null;
                APP_SEC_NEW_G.ParamCallBack = null;
                APP_SEC_NEW_G.MultipleRequestSupport = true;
                APP_SEC_NEW_G.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_G.SingleFormEnabled = true;
                APP_SEC_NEW_G.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_G.LogoFileID = null;
                APP_SEC_NEW_G.CitizenApplicationUrl = null;
                APP_SEC_NEW_G.CitizenHandbookUrl = null;
                APP_SEC_NEW_G.OperatingDays = 90;
                APP_SEC_NEW_G.CitizenOperatingDays = null;
                APP_SEC_NEW_G.OperatingCost = 0;
                APP_SEC_NEW_G.OperatingCostType = CostType.Range;
                APP_SEC_NEW_G.OperatingCost2 = 5000000;
                APP_SEC_NEW_G.CitizenOperatingCost = 0;
                APP_SEC_NEW_G.CitizenOperatingCostType = null;
                APP_SEC_NEW_G.CitizenOperatingCost2 = null;
                APP_SEC_NEW_G.ShowRemark = true;
                APP_SEC_NEW_G.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1 คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_G.CitizenShowRemark = true;
                APP_SEC_NEW_G.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอขอใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1 ได้</span>"; ;
                APP_SEC_NEW_G.RequestAtOrg = false;
                APP_SEC_NEW_G.CitizenRequestAtOrg = true;
                APP_SEC_NEW_G.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_G.OperatingDays2 = 0;
                APP_SEC_NEW_G.CitizenOperatingDays2 = null;
                APP_SEC_NEW_G.TemporaryDisable = false;
                APP_SEC_NEW_G.TemporaryRemark = null;
                APP_SEC_NEW_G.FileOwner = null;
                APP_SEC_NEW_G.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1";
                createApplication(context, APP_SEC_NEW_G, TranslateName, creater);
            }
            else
            {
                APP_SEC_NEW_G.ApplicationSysName = ApplicationSysName.APP_SEC_NEW_G;
                APP_SEC_NEW_G.OrgCode = "21032000";
                APP_SEC_NEW_G.ApplicationUrl = null;
                APP_SEC_NEW_G.ConsumerKey = null;
                APP_SEC_NEW_G.IsDeleted = false;
                //APP_SEC_NEW_G.CreatedUser = Auto gen
                //APP_SEC_NEW_G.CreatedDate = Auto gen
                //APP_SEC_NEW_G.UpdatedUserID = Auto gen
                //APP_SEC_NEW_G.UpdatedDate = Auto gen
                APP_SEC_NEW_G.DeletedUserID = null;
                APP_SEC_NEW_G.DeletedDate = null;
                APP_SEC_NEW_G.UrlCallBack = null;
                APP_SEC_NEW_G.ParamCallBack = null;
                APP_SEC_NEW_G.MultipleRequestSupport = true;
                APP_SEC_NEW_G.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_NEW_G.SingleFormEnabled = true;
                APP_SEC_NEW_G.HandbookUrl = "https://info.go.th/#!/th/search/68152/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_NEW_G.LogoFileID = null;
                APP_SEC_NEW_G.CitizenApplicationUrl = null;
                APP_SEC_NEW_G.CitizenHandbookUrl = null;
                APP_SEC_NEW_G.OperatingDays = 90;
                APP_SEC_NEW_G.CitizenOperatingDays = null;
                APP_SEC_NEW_G.OperatingCost = 0;
                APP_SEC_NEW_G.OperatingCostType = CostType.Range;
                APP_SEC_NEW_G.OperatingCost2 = 5000000;
                APP_SEC_NEW_G.CitizenOperatingCost = 0;
                APP_SEC_NEW_G.CitizenOperatingCostType = null;
                APP_SEC_NEW_G.CitizenOperatingCost2 = null;
                APP_SEC_NEW_G.ShowRemark = true;
                APP_SEC_NEW_G.Remark = "<span style=\"color: red; \">หมายเหตุ: ค่าธรรมเนียมข้างต้นยังไม่รวมค่าภาษีมูลค่าเพิ่ม (vat) และค่าคำขอรับใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1 คำขอละ 0-30,000 บาท</span>";
                APP_SEC_NEW_G.CitizenShowRemark = true;
                APP_SEC_NEW_G.CitizenRemark = "<span style=\"color: red; \">บุคคลธรรมดาไม่สามารถขอขอใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1 ได้</span>"; ;
                APP_SEC_NEW_G.RequestAtOrg = false;
                APP_SEC_NEW_G.CitizenRequestAtOrg = true;
                APP_SEC_NEW_G.OperatingDayType = CostType.Fixed;
                APP_SEC_NEW_G.OperatingDays2 = 0;
                APP_SEC_NEW_G.CitizenOperatingDays2 = null;
                APP_SEC_NEW_G.TemporaryDisable = false;
                APP_SEC_NEW_G.TemporaryRemark = null;
                APP_SEC_NEW_G.FileOwner = null;
                APP_SEC_NEW_G.StatusSequence = null;
                string TranslateName = "ขอใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1";
                updateApplication(context, APP_SEC_NEW_G, TranslateName, creater);
            }

            Application APP_SEC_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_EDIT).FirstOrDefault();
            if (APP_SEC_EDIT == null)
            {
                APP_SEC_EDIT = new Application();
                APP_SEC_EDIT.ApplicationSysName = ApplicationSysName.APP_SEC_EDIT;
                APP_SEC_EDIT.OrgCode = "21032000";
                APP_SEC_EDIT.ApplicationUrl = null;
                APP_SEC_EDIT.ConsumerKey = null;
                APP_SEC_EDIT.IsDeleted = false;
                //APP_SEC_EDIT.CreatedUser = Auto gen
                //APP_SEC_EDIT.CreatedDate = Auto gen
                //APP_SEC_EDIT.UpdatedUserID = Auto gen
                //APP_SEC_EDIT.UpdatedDate = Auto gen
                APP_SEC_EDIT.DeletedUserID = null;
                APP_SEC_EDIT.DeletedDate = null;
                APP_SEC_EDIT.UrlCallBack = null;
                APP_SEC_EDIT.ParamCallBack = null;
                APP_SEC_EDIT.MultipleRequestSupport = true;
                APP_SEC_EDIT.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_SEC_EDIT.SingleFormEnabled = true;
                APP_SEC_EDIT.HandbookUrl = "https://www.sec.or.th/TH/Pages/LawandRegulations/Intermediaries.aspx";
                APP_SEC_EDIT.LogoFileID = null;
                APP_SEC_EDIT.CitizenApplicationUrl = null;
                APP_SEC_EDIT.CitizenHandbookUrl = null;
                APP_SEC_EDIT.OperatingDays = 30;
                APP_SEC_EDIT.CitizenOperatingDays = null;
                APP_SEC_EDIT.OperatingCost = 0;
                APP_SEC_EDIT.OperatingCostType = CostType.Fixed;
                APP_SEC_EDIT.OperatingCost2 = 0;
                APP_SEC_EDIT.CitizenOperatingCost = null;
                APP_SEC_EDIT.CitizenOperatingCostType = null;
                APP_SEC_EDIT.CitizenOperatingCost2 = null;
                APP_SEC_EDIT.ShowRemark = false;
                APP_SEC_EDIT.Remark = null;
                APP_SEC_EDIT.CitizenShowRemark = false;
                APP_SEC_EDIT.CitizenRemark = null;
                APP_SEC_EDIT.RequestAtOrg = false;
                APP_SEC_EDIT.CitizenRequestAtOrg = true;
                APP_SEC_EDIT.OperatingDayType = CostType.Fixed;
                APP_SEC_EDIT.OperatingDays2 = 0;
                APP_SEC_EDIT.CitizenOperatingDays2 = null;
                APP_SEC_EDIT.TemporaryDisable = false;
                APP_SEC_EDIT.TemporaryRemark = null;
                APP_SEC_EDIT.FileOwner = null;
                APP_SEC_EDIT.StatusSequence = null;
                string TranslateName = "ขอแก้ไขข้อมูลผู้ประกอบธุรกิจหลักทรัพย์และสัญญาซื้อขายล่วงหน้า";
                createApplication(context, APP_SEC_EDIT, TranslateName, creater);
            }
            else
            {
                APP_SEC_EDIT.ApplicationSysName = ApplicationSysName.APP_SEC_EDIT;
                APP_SEC_EDIT.OrgCode = "21032000";
                APP_SEC_EDIT.ApplicationUrl = null;
                APP_SEC_EDIT.ConsumerKey = null;
                APP_SEC_EDIT.IsDeleted = false;
                //APP_SEC_EDIT.CreatedUser = Auto gen
                //APP_SEC_EDIT.CreatedDate = Auto gen
                //APP_SEC_EDIT.UpdatedUserID = Auto gen
                //APP_SEC_EDIT.UpdatedDate = Auto gen
                APP_SEC_EDIT.DeletedUserID = null;
                APP_SEC_EDIT.DeletedDate = null;
                APP_SEC_EDIT.UrlCallBack = null;
                APP_SEC_EDIT.ParamCallBack = null;
                APP_SEC_EDIT.MultipleRequestSupport = true;
                APP_SEC_EDIT.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_SEC_EDIT.SingleFormEnabled = true;
                APP_SEC_EDIT.HandbookUrl = "https://www.sec.or.th/TH/Pages/LawandRegulations/Intermediaries.aspx";
                APP_SEC_EDIT.LogoFileID = null;
                APP_SEC_EDIT.CitizenApplicationUrl = null;
                APP_SEC_EDIT.CitizenHandbookUrl = null;
                APP_SEC_EDIT.OperatingDays = 30;
                APP_SEC_EDIT.CitizenOperatingDays = null;
                APP_SEC_EDIT.OperatingCost = 0;
                APP_SEC_EDIT.OperatingCostType = CostType.Fixed;
                APP_SEC_EDIT.OperatingCost2 = 0;
                APP_SEC_EDIT.CitizenOperatingCost = null;
                APP_SEC_EDIT.CitizenOperatingCostType = null;
                APP_SEC_EDIT.CitizenOperatingCost2 = null;
                APP_SEC_EDIT.ShowRemark = false;
                APP_SEC_EDIT.Remark = null;
                APP_SEC_EDIT.CitizenShowRemark = false;
                APP_SEC_EDIT.CitizenRemark = null;
                APP_SEC_EDIT.RequestAtOrg = false;
                APP_SEC_EDIT.CitizenRequestAtOrg = true;
                APP_SEC_EDIT.OperatingDayType = CostType.Fixed;
                APP_SEC_EDIT.OperatingDays2 = 0;
                APP_SEC_EDIT.CitizenOperatingDays2 = null;
                APP_SEC_EDIT.TemporaryDisable = false;
                APP_SEC_EDIT.TemporaryRemark = null;
                APP_SEC_EDIT.FileOwner = null;
                APP_SEC_EDIT.StatusSequence = null;
                string TranslateName = "ขอแก้ไขข้อมูลผู้ประกอบธุรกิจหลักทรัพย์และสัญญาซื้อขายล่วงหน้า";
                updateApplication(context, APP_SEC_EDIT, TranslateName, creater);
            }

            Application APP_SEC_CANCEL_A = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_CANCEL_A).FirstOrDefault();
            if (APP_SEC_CANCEL_A == null)
            {
                APP_SEC_CANCEL_A = new Application();
                APP_SEC_CANCEL_A.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_A;
                APP_SEC_CANCEL_A.OrgCode = "21032000";
                APP_SEC_CANCEL_A.ApplicationUrl = null;
                APP_SEC_CANCEL_A.ConsumerKey = null;
                APP_SEC_CANCEL_A.IsDeleted = false;
                //APP_SEC_CANCEL_A.CreatedUser = Auto gen
                //APP_SEC_CANCEL_A.CreatedDate = Auto gen
                //APP_SEC_CANCEL_A.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_A.UpdatedDate = Auto gen
                APP_SEC_CANCEL_A.DeletedUserID = null;
                APP_SEC_CANCEL_A.DeletedDate = null;
                APP_SEC_CANCEL_A.UrlCallBack = null;
                APP_SEC_CANCEL_A.ParamCallBack = null;
                APP_SEC_CANCEL_A.MultipleRequestSupport = true;
                APP_SEC_CANCEL_A.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_A.SingleFormEnabled = true;
                APP_SEC_CANCEL_A.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_A.LogoFileID = null;
                APP_SEC_CANCEL_A.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_A.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_A.OperatingDays = 30;
                APP_SEC_CANCEL_A.CitizenOperatingDays = null;
                APP_SEC_CANCEL_A.OperatingCost = 0;
                APP_SEC_CANCEL_A.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_A.OperatingCost2 = 0;
                APP_SEC_CANCEL_A.CitizenOperatingCost = null;
                APP_SEC_CANCEL_A.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_A.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_A.ShowRemark = false;
                APP_SEC_CANCEL_A.Remark = null;
                APP_SEC_CANCEL_A.CitizenShowRemark = false;
                APP_SEC_CANCEL_A.CitizenRemark = null;
                APP_SEC_CANCEL_A.RequestAtOrg = false;
                APP_SEC_CANCEL_A.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_A.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_A.OperatingDays2 = 0;
                APP_SEC_CANCEL_A.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_A.TemporaryDisable = false;
                APP_SEC_CANCEL_A.TemporaryRemark = null;
                APP_SEC_CANCEL_A.FileOwner = null;
                APP_SEC_CANCEL_A.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจหลักทรัพย์แบบ ก และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1";
                createApplication(context, APP_SEC_CANCEL_A, TranslateName, creater);
            }
            else
            {
                APP_SEC_CANCEL_A.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_A;
                APP_SEC_CANCEL_A.OrgCode = "21032000";
                APP_SEC_CANCEL_A.ApplicationUrl = null;
                APP_SEC_CANCEL_A.ConsumerKey = null;
                APP_SEC_CANCEL_A.IsDeleted = false;
                //APP_SEC_CANCEL_A.CreatedUser = Auto gen
                //APP_SEC_CANCEL_A.CreatedDate = Auto gen
                //APP_SEC_CANCEL_A.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_A.UpdatedDate = Auto gen
                APP_SEC_CANCEL_A.DeletedUserID = null;
                APP_SEC_CANCEL_A.DeletedDate = null;
                APP_SEC_CANCEL_A.UrlCallBack = null;
                APP_SEC_CANCEL_A.ParamCallBack = null;
                APP_SEC_CANCEL_A.MultipleRequestSupport = true;
                APP_SEC_CANCEL_A.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_A.SingleFormEnabled = true;
                APP_SEC_CANCEL_A.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_A.LogoFileID = null;
                APP_SEC_CANCEL_A.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_A.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_A.OperatingDays = 30;
                APP_SEC_CANCEL_A.CitizenOperatingDays = null;
                APP_SEC_CANCEL_A.OperatingCost = 0;
                APP_SEC_CANCEL_A.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_A.OperatingCost2 = 0;
                APP_SEC_CANCEL_A.CitizenOperatingCost = null;
                APP_SEC_CANCEL_A.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_A.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_A.ShowRemark = false;
                APP_SEC_CANCEL_A.Remark = null;
                APP_SEC_CANCEL_A.CitizenShowRemark = false;
                APP_SEC_CANCEL_A.CitizenRemark = null;
                APP_SEC_CANCEL_A.RequestAtOrg = false;
                APP_SEC_CANCEL_A.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_A.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_A.OperatingDays2 = 0;
                APP_SEC_CANCEL_A.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_A.TemporaryDisable = false;
                APP_SEC_CANCEL_A.TemporaryRemark = null;
                APP_SEC_CANCEL_A.FileOwner = null;
                APP_SEC_CANCEL_A.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจหลักทรัพย์แบบ ก และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1";
                updateApplication(context, APP_SEC_CANCEL_A, TranslateName, creater);
            }

            Application APP_SEC_CANCEL_B = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_CANCEL_B).FirstOrDefault();
            if (APP_SEC_CANCEL_B == null)
            {
                APP_SEC_CANCEL_B = new Application();
                APP_SEC_CANCEL_B.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_B;
                APP_SEC_CANCEL_B.OrgCode = "21032000";
                APP_SEC_CANCEL_B.ApplicationUrl = null;
                APP_SEC_CANCEL_B.ConsumerKey = null;
                APP_SEC_CANCEL_B.IsDeleted = false;
                //APP_SEC_CANCEL_B.CreatedUser = Auto gen
                //APP_SEC_CANCEL_B.CreatedDate = Auto gen
                //APP_SEC_CANCEL_B.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_B.UpdatedDate = Auto gen
                APP_SEC_CANCEL_B.DeletedUserID = null;
                APP_SEC_CANCEL_B.DeletedDate = null;
                APP_SEC_CANCEL_B.UrlCallBack = null;
                APP_SEC_CANCEL_B.ParamCallBack = null;
                APP_SEC_CANCEL_B.MultipleRequestSupport = true;
                APP_SEC_CANCEL_B.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_B.SingleFormEnabled = true;
                APP_SEC_CANCEL_B.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_B.LogoFileID = null;
                APP_SEC_CANCEL_B.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_B.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_B.OperatingDays = 30;
                APP_SEC_CANCEL_B.CitizenOperatingDays = null;
                APP_SEC_CANCEL_B.OperatingCost = 0;
                APP_SEC_CANCEL_B.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_B.OperatingCost2 = 0;
                APP_SEC_CANCEL_B.CitizenOperatingCost = null;
                APP_SEC_CANCEL_B.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_B.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_B.ShowRemark = false;
                APP_SEC_CANCEL_B.Remark = null;
                APP_SEC_CANCEL_B.CitizenShowRemark = false;
                APP_SEC_CANCEL_B.CitizenRemark = null;
                APP_SEC_CANCEL_B.RequestAtOrg = false;
                APP_SEC_CANCEL_B.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_B.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_B.OperatingDays2 = 0;
                APP_SEC_CANCEL_B.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_B.TemporaryDisable = false;
                APP_SEC_CANCEL_B.TemporaryRemark = null;
                APP_SEC_CANCEL_B.FileOwner = null;
                APP_SEC_CANCEL_B.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจหลักทรัพย์แบบ ข และใบอนุญาตประกอบกิจสัญญาซื้อขายล่วงหน้าแบบ ส-2";
                createApplication(context, APP_SEC_CANCEL_B, TranslateName, creater);
            }
            else
            {
                APP_SEC_CANCEL_B.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_B;
                APP_SEC_CANCEL_B.OrgCode = "21032000";
                APP_SEC_CANCEL_B.ApplicationUrl = null;
                APP_SEC_CANCEL_B.ConsumerKey = null;
                APP_SEC_CANCEL_B.IsDeleted = false;
                //APP_SEC_CANCEL_B.CreatedUser = Auto gen
                //APP_SEC_CANCEL_B.CreatedDate = Auto gen
                //APP_SEC_CANCEL_B.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_B.UpdatedDate = Auto gen
                APP_SEC_CANCEL_B.DeletedUserID = null;
                APP_SEC_CANCEL_B.DeletedDate = null;
                APP_SEC_CANCEL_B.UrlCallBack = null;
                APP_SEC_CANCEL_B.ParamCallBack = null;
                APP_SEC_CANCEL_B.MultipleRequestSupport = true;
                APP_SEC_CANCEL_B.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_B.SingleFormEnabled = true;
                APP_SEC_CANCEL_B.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_B.LogoFileID = null;
                APP_SEC_CANCEL_B.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_B.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_B.OperatingDays = 30;
                APP_SEC_CANCEL_B.CitizenOperatingDays = null;
                APP_SEC_CANCEL_B.OperatingCost = 0;
                APP_SEC_CANCEL_B.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_B.OperatingCost2 = 0;
                APP_SEC_CANCEL_B.CitizenOperatingCost = null;
                APP_SEC_CANCEL_B.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_B.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_B.ShowRemark = false;
                APP_SEC_CANCEL_B.Remark = null;
                APP_SEC_CANCEL_B.CitizenShowRemark = false;
                APP_SEC_CANCEL_B.CitizenRemark = null;
                APP_SEC_CANCEL_B.RequestAtOrg = false;
                APP_SEC_CANCEL_B.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_B.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_B.OperatingDays2 = 0;
                APP_SEC_CANCEL_B.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_B.TemporaryDisable = false;
                APP_SEC_CANCEL_B.TemporaryRemark = null;
                APP_SEC_CANCEL_B.FileOwner = null;
                APP_SEC_CANCEL_B.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจหลักทรัพย์แบบ ข และใบอนุญาตประกอบกิจสัญญาซื้อขายล่วงหน้าแบบ ส-2";
                updateApplication(context, APP_SEC_CANCEL_B, TranslateName, creater);
            }

            Application APP_SEC_CANCEL_C = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_CANCEL_C).FirstOrDefault();
            if (APP_SEC_CANCEL_C == null)
            {
                APP_SEC_CANCEL_C = new Application();
                APP_SEC_CANCEL_C.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_C;
                APP_SEC_CANCEL_C.OrgCode = "21032000";
                APP_SEC_CANCEL_C.ApplicationUrl = null;
                APP_SEC_CANCEL_C.ConsumerKey = null;
                APP_SEC_CANCEL_C.IsDeleted = false;
                //APP_SEC_CANCEL_C.CreatedUser = Auto gen
                //APP_SEC_CANCEL_C.CreatedDate = Auto gen
                //APP_SEC_CANCEL_C.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_C.UpdatedDate = Auto gen
                APP_SEC_CANCEL_C.DeletedUserID = null;
                APP_SEC_CANCEL_C.DeletedDate = null;
                APP_SEC_CANCEL_C.UrlCallBack = null;
                APP_SEC_CANCEL_C.ParamCallBack = null;
                APP_SEC_CANCEL_C.MultipleRequestSupport = true;
                APP_SEC_CANCEL_C.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_C.SingleFormEnabled = true;
                APP_SEC_CANCEL_C.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_C.LogoFileID = null;
                APP_SEC_CANCEL_C.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_C.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_C.OperatingDays = 30;
                APP_SEC_CANCEL_C.CitizenOperatingDays = null;
                APP_SEC_CANCEL_C.OperatingCost = 0;
                APP_SEC_CANCEL_C.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_C.OperatingCost2 = 0;
                APP_SEC_CANCEL_C.CitizenOperatingCost = null;
                APP_SEC_CANCEL_C.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_C.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_C.ShowRemark = false;
                APP_SEC_CANCEL_C.Remark = null;
                APP_SEC_CANCEL_C.CitizenShowRemark = false;
                APP_SEC_CANCEL_C.CitizenRemark = null;
                APP_SEC_CANCEL_C.RequestAtOrg = false;
                APP_SEC_CANCEL_C.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_C.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_C.OperatingDays2 = 0;
                APP_SEC_CANCEL_C.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_C.TemporaryDisable = false;
                APP_SEC_CANCEL_C.TemporaryRemark = null;
                APP_SEC_CANCEL_C.FileOwner = null;
                APP_SEC_CANCEL_C.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจหลักทรัพย์แบบ ค และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้า ประเภทการเป็นผู้จัดการทุนสัญญาซื้อขายล่วงหน้าผู้จัดการเงินทุนสัญญา";
                createApplication(context, APP_SEC_CANCEL_C, TranslateName, creater);
            }
            else
            {
                APP_SEC_CANCEL_C.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_C;
                APP_SEC_CANCEL_C.OrgCode = "21032000";
                APP_SEC_CANCEL_C.ApplicationUrl = null;
                APP_SEC_CANCEL_C.ConsumerKey = null;
                APP_SEC_CANCEL_C.IsDeleted = false;
                //APP_SEC_CANCEL_C.CreatedUser = Auto gen
                //APP_SEC_CANCEL_C.CreatedDate = Auto gen
                //APP_SEC_CANCEL_C.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_C.UpdatedDate = Auto gen
                APP_SEC_CANCEL_C.DeletedUserID = null;
                APP_SEC_CANCEL_C.DeletedDate = null;
                APP_SEC_CANCEL_C.UrlCallBack = null;
                APP_SEC_CANCEL_C.ParamCallBack = null;
                APP_SEC_CANCEL_C.MultipleRequestSupport = true;
                APP_SEC_CANCEL_C.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_C.SingleFormEnabled = true;
                APP_SEC_CANCEL_C.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_C.LogoFileID = null;
                APP_SEC_CANCEL_C.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_C.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_C.OperatingDays = 30;
                APP_SEC_CANCEL_C.CitizenOperatingDays = null;
                APP_SEC_CANCEL_C.OperatingCost = 0;
                APP_SEC_CANCEL_C.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_C.OperatingCost2 = 0;
                APP_SEC_CANCEL_C.CitizenOperatingCost = null;
                APP_SEC_CANCEL_C.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_C.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_C.ShowRemark = false;
                APP_SEC_CANCEL_C.Remark = null;
                APP_SEC_CANCEL_C.CitizenShowRemark = false;
                APP_SEC_CANCEL_C.CitizenRemark = null;
                APP_SEC_CANCEL_C.RequestAtOrg = false;
                APP_SEC_CANCEL_C.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_C.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_C.OperatingDays2 = 0;
                APP_SEC_CANCEL_C.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_C.TemporaryDisable = false;
                APP_SEC_CANCEL_C.TemporaryRemark = null;
                APP_SEC_CANCEL_C.FileOwner = null;
                APP_SEC_CANCEL_C.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจหลักทรัพย์แบบ ค และใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้า ประเภทการเป็นผู้จัดการทุนสัญญาซื้อขายล่วงหน้าผู้จัดการเงินทุนสัญญา";
                updateApplication(context, APP_SEC_CANCEL_C, TranslateName, creater);
            }

            Application APP_SEC_CANCEL_D = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_CANCEL_D).FirstOrDefault();
            if (APP_SEC_CANCEL_D == null)
            {
                APP_SEC_CANCEL_D = new Application();
                APP_SEC_CANCEL_D.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_D;
                APP_SEC_CANCEL_D.OrgCode = "21032000";
                APP_SEC_CANCEL_D.ApplicationUrl = null;
                APP_SEC_CANCEL_D.ConsumerKey = null;
                APP_SEC_CANCEL_D.IsDeleted = false;
                //APP_SEC_CANCEL_D.CreatedUser = Auto gen
                //APP_SEC_CANCEL_D.CreatedDate = Auto gen
                //APP_SEC_CANCEL_D.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_D.UpdatedDate = Auto gen
                APP_SEC_CANCEL_D.DeletedUserID = null;
                APP_SEC_CANCEL_D.DeletedDate = null;
                APP_SEC_CANCEL_D.UrlCallBack = null;
                APP_SEC_CANCEL_D.ParamCallBack = null;
                APP_SEC_CANCEL_D.MultipleRequestSupport = true;
                APP_SEC_CANCEL_D.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_D.SingleFormEnabled = true;
                APP_SEC_CANCEL_D.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_D.LogoFileID = null;
                APP_SEC_CANCEL_D.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_D.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_D.OperatingDays = 30;
                APP_SEC_CANCEL_D.CitizenOperatingDays = null;
                APP_SEC_CANCEL_D.OperatingCost = 0;
                APP_SEC_CANCEL_D.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_D.OperatingCost2 = 0;
                APP_SEC_CANCEL_D.CitizenOperatingCost = null;
                APP_SEC_CANCEL_D.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_D.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_D.ShowRemark = false;
                APP_SEC_CANCEL_D.Remark = null;
                APP_SEC_CANCEL_D.CitizenShowRemark = false;
                APP_SEC_CANCEL_D.CitizenRemark = null;
                APP_SEC_CANCEL_D.RequestAtOrg = false;
                APP_SEC_CANCEL_D.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_D.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_D.OperatingDays2 = 0;
                APP_SEC_CANCEL_D.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_D.TemporaryDisable = false;
                APP_SEC_CANCEL_D.TemporaryRemark = null;
                APP_SEC_CANCEL_D.FileOwner = null;
                APP_SEC_CANCEL_D.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ง";
                createApplication(context, APP_SEC_CANCEL_D, TranslateName, creater);
            }
            else
            {
                APP_SEC_CANCEL_D.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_D;
                APP_SEC_CANCEL_D.OrgCode = "21032000";
                APP_SEC_CANCEL_D.ApplicationUrl = null;
                APP_SEC_CANCEL_D.ConsumerKey = null;
                APP_SEC_CANCEL_D.IsDeleted = false;
                //APP_SEC_CANCEL_D.CreatedUser = Auto gen
                //APP_SEC_CANCEL_D.CreatedDate = Auto gen
                //APP_SEC_CANCEL_D.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_D.UpdatedDate = Auto gen
                APP_SEC_CANCEL_D.DeletedUserID = null;
                APP_SEC_CANCEL_D.DeletedDate = null;
                APP_SEC_CANCEL_D.UrlCallBack = null;
                APP_SEC_CANCEL_D.ParamCallBack = null;
                APP_SEC_CANCEL_D.MultipleRequestSupport = true;
                APP_SEC_CANCEL_D.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_D.SingleFormEnabled = true;
                APP_SEC_CANCEL_D.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_D.LogoFileID = null;
                APP_SEC_CANCEL_D.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_D.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_D.OperatingDays = 30;
                APP_SEC_CANCEL_D.CitizenOperatingDays = null;
                APP_SEC_CANCEL_D.OperatingCost = 0;
                APP_SEC_CANCEL_D.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_D.OperatingCost2 = 0;
                APP_SEC_CANCEL_D.CitizenOperatingCost = null;
                APP_SEC_CANCEL_D.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_D.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_D.ShowRemark = false;
                APP_SEC_CANCEL_D.Remark = null;
                APP_SEC_CANCEL_D.CitizenShowRemark = false;
                APP_SEC_CANCEL_D.CitizenRemark = null;
                APP_SEC_CANCEL_D.RequestAtOrg = false;
                APP_SEC_CANCEL_D.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_D.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_D.OperatingDays2 = 0;
                APP_SEC_CANCEL_D.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_D.TemporaryDisable = false;
                APP_SEC_CANCEL_D.TemporaryRemark = null;
                APP_SEC_CANCEL_D.FileOwner = null;
                APP_SEC_CANCEL_D.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจหลักทรัพย์ แบบ ง";
                updateApplication(context, APP_SEC_CANCEL_D, TranslateName, creater);
            }

            Application APP_SEC_CANCEL_E = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_CANCEL_E).FirstOrDefault();
            if (APP_SEC_CANCEL_E == null)
            {
                APP_SEC_CANCEL_E = new Application();
                APP_SEC_CANCEL_E.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_E;
                APP_SEC_CANCEL_E.OrgCode = "21032000";
                APP_SEC_CANCEL_E.ApplicationUrl = null;
                APP_SEC_CANCEL_E.ConsumerKey = null;
                APP_SEC_CANCEL_E.IsDeleted = false;
                //APP_SEC_CANCEL_E.CreatedUser = Auto gen
                //APP_SEC_CANCEL_E.CreatedDate = Auto gen
                //APP_SEC_CANCEL_E.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_E.UpdatedDate = Auto gen
                APP_SEC_CANCEL_E.DeletedUserID = null;
                APP_SEC_CANCEL_E.DeletedDate = null;
                APP_SEC_CANCEL_E.UrlCallBack = null;
                APP_SEC_CANCEL_E.ParamCallBack = null;
                APP_SEC_CANCEL_E.MultipleRequestSupport = true;
                APP_SEC_CANCEL_E.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_E.SingleFormEnabled = true;
                APP_SEC_CANCEL_E.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_E.LogoFileID = null;
                APP_SEC_CANCEL_E.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_E.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_E.OperatingDays = 30;
                APP_SEC_CANCEL_E.CitizenOperatingDays = null;
                APP_SEC_CANCEL_E.OperatingCost = 0;
                APP_SEC_CANCEL_E.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_E.OperatingCost2 = 0;
                APP_SEC_CANCEL_E.CitizenOperatingCost = null;
                APP_SEC_CANCEL_E.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_E.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_E.ShowRemark = false;
                APP_SEC_CANCEL_E.Remark = null;
                APP_SEC_CANCEL_E.CitizenShowRemark = false;
                APP_SEC_CANCEL_E.CitizenRemark = null;
                APP_SEC_CANCEL_E.RequestAtOrg = false;
                APP_SEC_CANCEL_E.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_E.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_E.OperatingDays2 = 0;
                APP_SEC_CANCEL_E.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_E.TemporaryDisable = false;
                APP_SEC_CANCEL_E.TemporaryRemark = null;
                APP_SEC_CANCEL_E.FileOwner = null;
                APP_SEC_CANCEL_E.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทการเป็นที่ปรึกษาการลงทุน และใบอนุญาตประกอบธุรกิจที่ปรึกษาสัญญาซื้อขายล่วงหน้า";
                createApplication(context, APP_SEC_CANCEL_E, TranslateName, creater);
            }
            else
            {
                APP_SEC_CANCEL_E.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_E;
                APP_SEC_CANCEL_E.OrgCode = "21032000";
                APP_SEC_CANCEL_E.ApplicationUrl = null;
                APP_SEC_CANCEL_E.ConsumerKey = null;
                APP_SEC_CANCEL_E.IsDeleted = false;
                //APP_SEC_CANCEL_E.CreatedUser = Auto gen
                //APP_SEC_CANCEL_E.CreatedDate = Auto gen
                //APP_SEC_CANCEL_E.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_E.UpdatedDate = Auto gen
                APP_SEC_CANCEL_E.DeletedUserID = null;
                APP_SEC_CANCEL_E.DeletedDate = null;
                APP_SEC_CANCEL_E.UrlCallBack = null;
                APP_SEC_CANCEL_E.ParamCallBack = null;
                APP_SEC_CANCEL_E.MultipleRequestSupport = true;
                APP_SEC_CANCEL_E.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_E.SingleFormEnabled = true;
                APP_SEC_CANCEL_E.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_E.LogoFileID = null;
                APP_SEC_CANCEL_E.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_E.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_E.OperatingDays = 30;
                APP_SEC_CANCEL_E.CitizenOperatingDays = null;
                APP_SEC_CANCEL_E.OperatingCost = 0;
                APP_SEC_CANCEL_E.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_E.OperatingCost2 = 0;
                APP_SEC_CANCEL_E.CitizenOperatingCost = null;
                APP_SEC_CANCEL_E.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_E.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_E.ShowRemark = false;
                APP_SEC_CANCEL_E.Remark = null;
                APP_SEC_CANCEL_E.CitizenShowRemark = false;
                APP_SEC_CANCEL_E.CitizenRemark = null;
                APP_SEC_CANCEL_E.RequestAtOrg = false;
                APP_SEC_CANCEL_E.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_E.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_E.OperatingDays2 = 0;
                APP_SEC_CANCEL_E.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_E.TemporaryDisable = false;
                APP_SEC_CANCEL_E.TemporaryRemark = null;
                APP_SEC_CANCEL_E.FileOwner = null;
                APP_SEC_CANCEL_E.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทการเป็นที่ปรึกษาการลงทุน และใบอนุญาตประกอบธุรกิจที่ปรึกษาสัญญาซื้อขายล่วงหน้า";
                updateApplication(context, APP_SEC_CANCEL_E, TranslateName, creater);
            }

            Application APP_SEC_CANCEL_F = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_CANCEL_F).FirstOrDefault();
            if (APP_SEC_CANCEL_F == null)
            {
                APP_SEC_CANCEL_F = new Application();
                APP_SEC_CANCEL_F.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_F;
                APP_SEC_CANCEL_F.OrgCode = "21032000";
                APP_SEC_CANCEL_F.ApplicationUrl = null;
                APP_SEC_CANCEL_F.ConsumerKey = null;
                APP_SEC_CANCEL_F.IsDeleted = false;
                //APP_SEC_CANCEL_F.CreatedUser = Auto gen
                //APP_SEC_CANCEL_F.CreatedDate = Auto gen
                //APP_SEC_CANCEL_F.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_F.UpdatedDate = Auto gen
                APP_SEC_CANCEL_F.DeletedUserID = null;
                APP_SEC_CANCEL_F.DeletedDate = null;
                APP_SEC_CANCEL_F.UrlCallBack = null;
                APP_SEC_CANCEL_F.ParamCallBack = null;
                APP_SEC_CANCEL_F.MultipleRequestSupport = true;
                APP_SEC_CANCEL_F.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_F.SingleFormEnabled = true;
                APP_SEC_CANCEL_F.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_F.LogoFileID = null;
                APP_SEC_CANCEL_F.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_F.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_F.OperatingDays = 30;
                APP_SEC_CANCEL_F.CitizenOperatingDays = null;
                APP_SEC_CANCEL_F.OperatingCost = 0;
                APP_SEC_CANCEL_F.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_F.OperatingCost2 = 0;
                APP_SEC_CANCEL_F.CitizenOperatingCost = null;
                APP_SEC_CANCEL_F.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_F.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_F.ShowRemark = false;
                APP_SEC_CANCEL_F.Remark = null;
                APP_SEC_CANCEL_F.CitizenShowRemark = false;
                APP_SEC_CANCEL_F.CitizenRemark = null;
                APP_SEC_CANCEL_F.RequestAtOrg = false;
                APP_SEC_CANCEL_F.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_F.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_F.OperatingDays2 = 0;
                APP_SEC_CANCEL_F.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_F.TemporaryDisable = false;
                APP_SEC_CANCEL_F.TemporaryRemark = null;
                APP_SEC_CANCEL_F.FileOwner = null;
                APP_SEC_CANCEL_F.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทกิจการการยืมและให้ยืมหลักทรัพย์";
                createApplication(context, APP_SEC_CANCEL_F, TranslateName, creater);
            }
            else
            {
                APP_SEC_CANCEL_F.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_F;
                APP_SEC_CANCEL_F.OrgCode = "21032000";
                APP_SEC_CANCEL_F.ApplicationUrl = null;
                APP_SEC_CANCEL_F.ConsumerKey = null;
                APP_SEC_CANCEL_F.IsDeleted = false;
                //APP_SEC_CANCEL_F.CreatedUser = Auto gen
                //APP_SEC_CANCEL_F.CreatedDate = Auto gen
                //APP_SEC_CANCEL_F.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_F.UpdatedDate = Auto gen
                APP_SEC_CANCEL_F.DeletedUserID = null;
                APP_SEC_CANCEL_F.DeletedDate = null;
                APP_SEC_CANCEL_F.UrlCallBack = null;
                APP_SEC_CANCEL_F.ParamCallBack = null;
                APP_SEC_CANCEL_F.MultipleRequestSupport = true;
                APP_SEC_CANCEL_F.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_F.SingleFormEnabled = true;
                APP_SEC_CANCEL_F.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_F.LogoFileID = null;
                APP_SEC_CANCEL_F.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_F.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_F.OperatingDays = 30;
                APP_SEC_CANCEL_F.CitizenOperatingDays = null;
                APP_SEC_CANCEL_F.OperatingCost = 0;
                APP_SEC_CANCEL_F.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_F.OperatingCost2 = 0;
                APP_SEC_CANCEL_F.CitizenOperatingCost = null;
                APP_SEC_CANCEL_F.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_F.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_F.ShowRemark = false;
                APP_SEC_CANCEL_F.Remark = null;
                APP_SEC_CANCEL_F.CitizenShowRemark = false;
                APP_SEC_CANCEL_F.CitizenRemark = null;
                APP_SEC_CANCEL_F.RequestAtOrg = false;
                APP_SEC_CANCEL_F.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_F.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_F.OperatingDays2 = 0;
                APP_SEC_CANCEL_F.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_F.TemporaryDisable = false;
                APP_SEC_CANCEL_F.TemporaryRemark = null;
                APP_SEC_CANCEL_F.FileOwner = null;
                APP_SEC_CANCEL_F.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจหลักทรัพย์ประเภทกิจการการยืมและให้ยืมหลักทรัพย์";
                updateApplication(context, APP_SEC_CANCEL_F, TranslateName, creater);
            }

            Application APP_SEC_CANCEL_G = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_SEC_CANCEL_G).FirstOrDefault();
            if (APP_SEC_CANCEL_G == null)
            {
                APP_SEC_CANCEL_G = new Application();
                APP_SEC_CANCEL_G.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_G;
                APP_SEC_CANCEL_G.OrgCode = "21032000";
                APP_SEC_CANCEL_G.ApplicationUrl = null;
                APP_SEC_CANCEL_G.ConsumerKey = null;
                APP_SEC_CANCEL_G.IsDeleted = false;
                //APP_SEC_CANCEL_G.CreatedUser = Auto gen
                //APP_SEC_CANCEL_G.CreatedDate = Auto gen
                //APP_SEC_CANCEL_G.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_G.UpdatedDate = Auto gen
                APP_SEC_CANCEL_G.DeletedUserID = null;
                APP_SEC_CANCEL_G.DeletedDate = null;
                APP_SEC_CANCEL_G.UrlCallBack = null;
                APP_SEC_CANCEL_G.ParamCallBack = null;
                APP_SEC_CANCEL_G.MultipleRequestSupport = true;
                APP_SEC_CANCEL_G.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_G.SingleFormEnabled = true;
                APP_SEC_CANCEL_G.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_G.LogoFileID = null;
                APP_SEC_CANCEL_G.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_G.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_G.OperatingDays = 30;
                APP_SEC_CANCEL_G.CitizenOperatingDays = null;
                APP_SEC_CANCEL_G.OperatingCost = 0;
                APP_SEC_CANCEL_G.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_G.OperatingCost2 = 0;
                APP_SEC_CANCEL_G.CitizenOperatingCost = null;
                APP_SEC_CANCEL_G.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_G.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_G.ShowRemark = false;
                APP_SEC_CANCEL_G.Remark = null;
                APP_SEC_CANCEL_G.CitizenShowRemark = false;
                APP_SEC_CANCEL_G.CitizenRemark = null;
                APP_SEC_CANCEL_G.RequestAtOrg = false;
                APP_SEC_CANCEL_G.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_G.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_G.OperatingDays2 = 0;
                APP_SEC_CANCEL_G.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_G.TemporaryDisable = false;
                APP_SEC_CANCEL_G.TemporaryRemark = null;
                APP_SEC_CANCEL_G.FileOwner = null;
                APP_SEC_CANCEL_G.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1";
                createApplication(context, APP_SEC_CANCEL_G, TranslateName, creater);
            }
            else
            {
                APP_SEC_CANCEL_G.ApplicationSysName = ApplicationSysName.APP_SEC_CANCEL_G;
                APP_SEC_CANCEL_G.OrgCode = "21032000";
                APP_SEC_CANCEL_G.ApplicationUrl = null;
                APP_SEC_CANCEL_G.ConsumerKey = null;
                APP_SEC_CANCEL_G.IsDeleted = false;
                //APP_SEC_CANCEL_G.CreatedUser = Auto gen
                //APP_SEC_CANCEL_G.CreatedDate = Auto gen
                //APP_SEC_CANCEL_G.UpdatedUserID = Auto gen
                //APP_SEC_CANCEL_G.UpdatedDate = Auto gen
                APP_SEC_CANCEL_G.DeletedUserID = null;
                APP_SEC_CANCEL_G.DeletedDate = null;
                APP_SEC_CANCEL_G.UrlCallBack = null;
                APP_SEC_CANCEL_G.ParamCallBack = null;
                APP_SEC_CANCEL_G.MultipleRequestSupport = true;
                APP_SEC_CANCEL_G.AppsHookClassName = "BizPortal.AppsHook.NEW_PERMIT";
                APP_SEC_CANCEL_G.SingleFormEnabled = true;
                APP_SEC_CANCEL_G.HandbookUrl = "https://info.go.th/#!/th/search/68157/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B9%80%E0%B8%A5%E0%B8%B4%E0%B8%81%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%98%E0%B8%B8%E0%B8%A3%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%AB%E0%B8%A5%E0%B8%B1%E0%B8%81%E0%B8%97%E0%B8%A3%E0%B8%B1%E0%B8%9E%E0%B8%A2%E0%B9%8C/";
                APP_SEC_CANCEL_G.LogoFileID = null;
                APP_SEC_CANCEL_G.CitizenApplicationUrl = null;
                APP_SEC_CANCEL_G.CitizenHandbookUrl = null;
                APP_SEC_CANCEL_G.OperatingDays = 30;
                APP_SEC_CANCEL_G.CitizenOperatingDays = null;
                APP_SEC_CANCEL_G.OperatingCost = 0;
                APP_SEC_CANCEL_G.OperatingCostType = CostType.Fixed;
                APP_SEC_CANCEL_G.OperatingCost2 = 0;
                APP_SEC_CANCEL_G.CitizenOperatingCost = null;
                APP_SEC_CANCEL_G.CitizenOperatingCostType = null;
                APP_SEC_CANCEL_G.CitizenOperatingCost2 = null;
                APP_SEC_CANCEL_G.ShowRemark = false;
                APP_SEC_CANCEL_G.Remark = null;
                APP_SEC_CANCEL_G.CitizenShowRemark = false;
                APP_SEC_CANCEL_G.CitizenRemark = null;
                APP_SEC_CANCEL_G.RequestAtOrg = false;
                APP_SEC_CANCEL_G.CitizenRequestAtOrg = true;
                APP_SEC_CANCEL_G.OperatingDayType = CostType.Fixed;
                APP_SEC_CANCEL_G.OperatingDays2 = 0;
                APP_SEC_CANCEL_G.CitizenOperatingDays2 = null;
                APP_SEC_CANCEL_G.TemporaryDisable = false;
                APP_SEC_CANCEL_G.TemporaryRemark = null;
                APP_SEC_CANCEL_G.FileOwner = null;
                APP_SEC_CANCEL_G.StatusSequence = null;
                string TranslateName = "ขอยกเลิกใบอนุญาตประกอบธุรกิจสัญญาซื้อขายล่วงหน้าแบบ ส-1";
                updateApplication(context, APP_SEC_CANCEL_G, TranslateName, creater);
            }
            #endregion

            #region SSO Permits

            #region APP_EDUCATION_PRIVATE_SCHOOL

            Application APP_EDUCATION_PRIVATE_SCHOOL = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_EDUCATION_PRIVATE_SCHOOL).FirstOrDefault();

            if (APP_EDUCATION_PRIVATE_SCHOOL == null)
            {
                APP_EDUCATION_PRIVATE_SCHOOL = new Application();
                APP_EDUCATION_PRIVATE_SCHOOL.ApplicationSysName = ApplicationSysName.APP_EDUCATION_PRIVATE_SCHOOL;
                APP_EDUCATION_PRIVATE_SCHOOL.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_EDUCATION_PRIVATE_SCHOOL.ApplicationUrl = "https://psds.opec.go.th/psds/ApplySchool.htm?mode=initLoginSchool";
                APP_EDUCATION_PRIVATE_SCHOOL.RequestAtOrg = true;
                APP_EDUCATION_PRIVATE_SCHOOL.CitizenRequestAtOrg = true;
                APP_EDUCATION_PRIVATE_SCHOOL.OrgCode = "22000000";
                APP_EDUCATION_PRIVATE_SCHOOL.LogoFileID = null;
                APP_EDUCATION_PRIVATE_SCHOOL.HandbookUrl = null;
                APP_EDUCATION_PRIVATE_SCHOOL.OperatingDays = null;
                APP_EDUCATION_PRIVATE_SCHOOL.OperatingDayType = CostType.Fixed;
                APP_EDUCATION_PRIVATE_SCHOOL.OperatingDays2 = null;
                APP_EDUCATION_PRIVATE_SCHOOL.OperatingCost = null;
                APP_EDUCATION_PRIVATE_SCHOOL.OperatingCostType = CostType.Fixed;
                APP_EDUCATION_PRIVATE_SCHOOL.OperatingCost2 = null;
                APP_EDUCATION_PRIVATE_SCHOOL.CitizenOperatingDays = null;
                APP_EDUCATION_PRIVATE_SCHOOL.CitizenOperatingDayType = CostType.Fixed;
                APP_EDUCATION_PRIVATE_SCHOOL.CitizenOperatingDays2 = null;
                APP_EDUCATION_PRIVATE_SCHOOL.CitizenOperatingCost = null;
                APP_EDUCATION_PRIVATE_SCHOOL.CitizenOperatingCostType = CostType.Fixed;
                APP_EDUCATION_PRIVATE_SCHOOL.CitizenOperatingCost2 = null;
                APP_EDUCATION_PRIVATE_SCHOOL.SingleFormEnabled = true;
                string TranslateName = "ขออนุญาตจัดตั้งโรงเรียนนอกระบบ";

                createApplication(context, APP_EDUCATION_PRIVATE_SCHOOL, TranslateName, creater);
            }

            // Cumulative update info
            APP_EDUCATION_PRIVATE_SCHOOL.AppsHookClassName = APP_HOOK_NEW_PERMIT;
            APP_EDUCATION_PRIVATE_SCHOOL.OperatingDays = 29;
            APP_EDUCATION_PRIVATE_SCHOOL.OperatingCost = 500;
            APP_EDUCATION_PRIVATE_SCHOOL.OperatingCost2 = 3500;
            APP_EDUCATION_PRIVATE_SCHOOL.OperatingCostType = CostType.Range;
            APP_EDUCATION_PRIVATE_SCHOOL.HandbookUrl = "https://info.go.th/#!/th/search/17317/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%88%E0%B8%B1%E0%B8%94%E0%B8%95%E0%B8%B1%E0%B9%89%E0%B8%87%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B9%80%E0%B8%A3%E0%B8%B5%E0%B8%A2%E0%B8%99%E0%B8%99%E0%B8%AD%E0%B8%81%E0%B8%A3%E0%B8%B0%E0%B8%9A%E0%B8%9A/";
            APP_EDUCATION_PRIVATE_SCHOOL.CitizenOperatingDays = 29;
            APP_EDUCATION_PRIVATE_SCHOOL.CitizenOperatingCost = 500;
            APP_EDUCATION_PRIVATE_SCHOOL.CitizenOperatingCost2 = 3500;
            APP_EDUCATION_PRIVATE_SCHOOL.CitizenOperatingCostType = CostType.Range;
            APP_EDUCATION_PRIVATE_SCHOOL.CitizenHandbookUrl = "https://info.go.th/#!/th/search/17317/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B8%88%E0%B8%B1%E0%B8%94%E0%B8%95%E0%B8%B1%E0%B9%89%E0%B8%87%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B9%80%E0%B8%A3%E0%B8%B5%E0%B8%A2%E0%B8%99%E0%B8%99%E0%B8%AD%E0%B8%81%E0%B8%A3%E0%B8%B0%E0%B8%9A%E0%B8%9A/";
            APP_EDUCATION_PRIVATE_SCHOOL.CitizenApplicationUrl = "https://psds.opec.go.th/psds/ApplySchool.htm?mode=initLoginSchool";
            context.Applications.AddOrUpdate(APP_EDUCATION_PRIVATE_SCHOOL);

            foreach (var t in context.ApplicationTranslations.Where(o => o.ApplicationID == APP_EDUCATION_PRIVATE_SCHOOL.ApplicationID).ToList())
            {
                t.ApplicationName = "ยื่นคำขอจัดตั้งโรงเรียนเอกชนแบบออนไลน์ (ขออนุญาตจัดตั้งโรงเรียนนอกระบบ)";
                if (t.LanguageID == 2) t.ApplicationName += " (en)";

                context.ApplicationTranslations.AddOrUpdate(t);
            }


            #endregion

            #region APP_IMPORT_MEDICAL_EQUIPMENT

            Application APP_IMPORT_MEDICAL_EQUIPMENT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_IMPORT_MEDICAL_EQUIPMENT).FirstOrDefault();

            if (APP_IMPORT_MEDICAL_EQUIPMENT == null)
            {
                APP_IMPORT_MEDICAL_EQUIPMENT = new Application();
                APP_IMPORT_MEDICAL_EQUIPMENT.ApplicationSysName = ApplicationSysName.APP_IMPORT_MEDICAL_EQUIPMENT;
                APP_IMPORT_MEDICAL_EQUIPMENT.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_IMPORT_MEDICAL_EQUIPMENT.ApplicationUrl = "https://slatest.info.go.th/bizportal-phase2";
                APP_IMPORT_MEDICAL_EQUIPMENT.RequestAtOrg = true;
                APP_IMPORT_MEDICAL_EQUIPMENT.CitizenRequestAtOrg = true;
                APP_IMPORT_MEDICAL_EQUIPMENT.OrgCode = "19010000";
                APP_IMPORT_MEDICAL_EQUIPMENT.LogoFileID = null;
                APP_IMPORT_MEDICAL_EQUIPMENT.HandbookUrl = null;
                APP_IMPORT_MEDICAL_EQUIPMENT.OperatingDays = null;
                APP_IMPORT_MEDICAL_EQUIPMENT.OperatingDayType = CostType.Fixed;
                APP_IMPORT_MEDICAL_EQUIPMENT.OperatingDays2 = null;
                APP_IMPORT_MEDICAL_EQUIPMENT.OperatingCost = null;
                APP_IMPORT_MEDICAL_EQUIPMENT.OperatingCostType = CostType.Fixed;
                APP_IMPORT_MEDICAL_EQUIPMENT.OperatingCost2 = null;
                APP_IMPORT_MEDICAL_EQUIPMENT.CitizenOperatingDays = null;
                APP_IMPORT_MEDICAL_EQUIPMENT.CitizenOperatingDayType = CostType.Fixed;
                APP_IMPORT_MEDICAL_EQUIPMENT.CitizenOperatingDays2 = null;
                APP_IMPORT_MEDICAL_EQUIPMENT.CitizenOperatingCost = null;
                APP_IMPORT_MEDICAL_EQUIPMENT.CitizenOperatingCostType = CostType.Fixed;
                APP_IMPORT_MEDICAL_EQUIPMENT.CitizenOperatingCost2 = null;
                APP_IMPORT_MEDICAL_EQUIPMENT.SingleFormEnabled = true;
                string TranslateName = "หนังสือรับรองประกอบการนำเข้าเครื่องมือแพทย์";

                createApplication(context, APP_IMPORT_MEDICAL_EQUIPMENT, TranslateName, creater);
            }

            // Cumulative update info
            APP_IMPORT_MEDICAL_EQUIPMENT.AppsHookClassName = APP_HOOK_NEW_PERMIT;
            APP_IMPORT_MEDICAL_EQUIPMENT.ApplicationUrl = "https://privus.fda.moph.go.th/index.aspx";
            APP_IMPORT_MEDICAL_EQUIPMENT.OrgCode = "19010000";
            APP_IMPORT_MEDICAL_EQUIPMENT.OperatingDays = 1;
            APP_IMPORT_MEDICAL_EQUIPMENT.OperatingDayType = CostType.StartAt;
            APP_IMPORT_MEDICAL_EQUIPMENT.OperatingCost = 100;
            APP_IMPORT_MEDICAL_EQUIPMENT.OperatingCost2 = 2100;
            APP_IMPORT_MEDICAL_EQUIPMENT.OperatingCostType = CostType.Range;
            APP_IMPORT_MEDICAL_EQUIPMENT.CitizenApplicationUrl = "https://privus.fda.moph.go.th/index.aspx";
            APP_IMPORT_MEDICAL_EQUIPMENT.CitizenOperatingDays = 1;
            APP_IMPORT_MEDICAL_EQUIPMENT.CitizenOperatingDayType = CostType.StartAt;
            APP_IMPORT_MEDICAL_EQUIPMENT.CitizenOperatingCost = 100;
            APP_IMPORT_MEDICAL_EQUIPMENT.CitizenOperatingCost2 = 2100;
            APP_IMPORT_MEDICAL_EQUIPMENT.CitizenOperatingCostType = CostType.Range;
            APP_IMPORT_MEDICAL_EQUIPMENT.HandbookUrl = "https://info.go.th/#!/th/search/2675/%E0%B9%80%E0%B8%84%E0%B8%A3%E0%B8%B7%E0%B9%88%E0%B8%AD%E0%B8%87%E0%B8%A1%E0%B8%B7%E0%B8%AD%E0%B9%81%E0%B8%9E%E0%B8%97%E0%B8%A2%E0%B9%8C/";
            APP_IMPORT_MEDICAL_EQUIPMENT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/2675/%E0%B9%80%E0%B8%84%E0%B8%A3%E0%B8%B7%E0%B9%88%E0%B8%AD%E0%B8%87%E0%B8%A1%E0%B8%B7%E0%B8%AD%E0%B9%81%E0%B8%9E%E0%B8%97%E0%B8%A2%E0%B9%8C/";
            context.Applications.AddOrUpdate(APP_IMPORT_MEDICAL_EQUIPMENT);

            #endregion



            #region APP_FACTORY_TYPE3

            Application APP_FACTORY_TYPE3 = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_FACTORY_TYPE3).FirstOrDefault();

            if (APP_FACTORY_TYPE3 == null)
            {
                APP_FACTORY_TYPE3 = new Application();
                APP_FACTORY_TYPE3.ApplicationSysName = ApplicationSysName.APP_FACTORY_TYPE3;
                APP_FACTORY_TYPE3.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_FACTORY_TYPE3.ApplicationUrl = "https://slatest.info.go.th/bizportal-phase2";
                APP_FACTORY_TYPE3.RequestAtOrg = true;
                APP_FACTORY_TYPE3.CitizenRequestAtOrg = true;
                APP_FACTORY_TYPE3.OrgCode = "22000000";
                APP_FACTORY_TYPE3.LogoFileID = null;
                APP_FACTORY_TYPE3.HandbookUrl = null;
                APP_FACTORY_TYPE3.OperatingDays = null;
                APP_FACTORY_TYPE3.OperatingDayType = CostType.Fixed;
                APP_FACTORY_TYPE3.OperatingDays2 = null;
                APP_FACTORY_TYPE3.OperatingCost = null;
                APP_FACTORY_TYPE3.OperatingCostType = CostType.Fixed;
                APP_FACTORY_TYPE3.OperatingCost2 = null;
                APP_FACTORY_TYPE3.CitizenOperatingDays = null;
                APP_FACTORY_TYPE3.CitizenOperatingDayType = CostType.Fixed;
                APP_FACTORY_TYPE3.CitizenOperatingDays2 = null;
                APP_FACTORY_TYPE3.CitizenOperatingCost = null;
                APP_FACTORY_TYPE3.CitizenOperatingCostType = CostType.Fixed;
                APP_FACTORY_TYPE3.CitizenOperatingCost2 = null;
                APP_FACTORY_TYPE3.SingleFormEnabled = true;
                string TranslateName = "หนังสือแจ้งผลการรับฟังความคิดเห็นของประชาชนในการพิจารณาเกี่ยวกับโรงงานจำพวกที่ 3";

                createApplication(context, APP_FACTORY_TYPE3, TranslateName, creater);
            }

            // Cumulative update info
            APP_FACTORY_TYPE3.AppsHookClassName = APP_HOOK_NEW_PERMIT;
            APP_FACTORY_TYPE3.ApplicationUrl = "http://dsapptest.diw.go.th/BizCheckProfile.aspx?typesend=Kl0fEDVdjQfcDPj2uYgIEMJH%2focLRkObZMiQiRO9jrtl%2f2bzzI8ywWieEOXT%2bIQx6r5jjCvV7Wx1sxbv5swtk0jozf%2fXrkaKqbTkK89EBz29CTz5KjHgmDKx2OkjiANA6DAQ5wEaaRR1tXVke4bTRS8o7G8lWtMuADfEheMDANrkh1aEtLcr26YKJbh2gN4iv%2fHidvbTx9tSwLX6VYrV0g%3d%3d";
            APP_FACTORY_TYPE3.OrgCode = "20003000";
            APP_FACTORY_TYPE3.OperatingDays = 30;
            APP_FACTORY_TYPE3.OperatingCost = 0;
            APP_FACTORY_TYPE3.CitizenApplicationUrl = "https://slatest.info.go.th/bizportal-phase2";
            APP_FACTORY_TYPE3.CitizenOperatingDays = 30;
            APP_FACTORY_TYPE3.CitizenOperatingCost = 0;
            context.Applications.AddOrUpdate(APP_FACTORY_TYPE3);
            #endregion


            #region APP_FACTORY_CLASS_3_NEW

            Application APP_FACTORY_CLASS_3_NEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_FACTORY_CLASS_3_NEW).FirstOrDefault();

            if (APP_FACTORY_CLASS_3_NEW == null)
            {
                APP_FACTORY_CLASS_3_NEW = new Application();
                APP_FACTORY_CLASS_3_NEW.ApplicationSysName = ApplicationSysName.APP_FACTORY_CLASS_3_NEW;
                APP_FACTORY_CLASS_3_NEW.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_FACTORY_CLASS_3_NEW.ApplicationUrl = "https://slatest.info.go.th/bizportal-phase2";
                APP_FACTORY_CLASS_3_NEW.RequestAtOrg = true;
                APP_FACTORY_CLASS_3_NEW.CitizenRequestAtOrg = true;
                APP_FACTORY_CLASS_3_NEW.OrgCode = "20003000";
                APP_FACTORY_CLASS_3_NEW.LogoFileID = null;
                APP_FACTORY_CLASS_3_NEW.HandbookUrl = null;
                APP_FACTORY_CLASS_3_NEW.OperatingDays = null;
                APP_FACTORY_CLASS_3_NEW.OperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_3_NEW.OperatingDays2 = null;
                APP_FACTORY_CLASS_3_NEW.OperatingCost = null;
                APP_FACTORY_CLASS_3_NEW.OperatingCostType = CostType.Fixed;
                APP_FACTORY_CLASS_3_NEW.OperatingCost2 = null;
                APP_FACTORY_CLASS_3_NEW.CitizenOperatingDays = null;
                APP_FACTORY_CLASS_3_NEW.CitizenOperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_3_NEW.CitizenOperatingDays2 = null;
                APP_FACTORY_CLASS_3_NEW.CitizenOperatingCost = null;
                APP_FACTORY_CLASS_3_NEW.CitizenOperatingCostType = CostType.Fixed;
                APP_FACTORY_CLASS_3_NEW.CitizenOperatingCost2 = null;
                APP_FACTORY_CLASS_3_NEW.SingleFormEnabled = true;
                string TranslateName = "ใบอนุญาตประกอบกิจการโรงงานจำพวกที่ 3";

                createApplication(context, APP_FACTORY_CLASS_3_NEW, TranslateName, creater);
            }

            // Cumulative update info
            APP_FACTORY_CLASS_3_NEW.AppsHookClassName = APP_HOOK_NEW_PERMIT;
            APP_FACTORY_CLASS_3_NEW.ApplicationUrl = "http://dsapptest.diw.go.th/BizCheckProfile.aspx?typesend=Kl0fEDVdjQfcDPj2uYgIEMJH%2focLRkObZMiQiRO9jrtl%2f2bzzI8ywWieEOXT%2bIQx6r5jjCvV7Wx1sxbv5swtk0jozf%2fXrkaKqbTkK89EBz29CTz5KjHgmDKx2OkjiANA6DAQ5wEaaRR1tXVke4bTRS8o7G8lWtMuADfEheMDANrkh1aEtLcr26YKJbh2gN4iv%2fHidvbTx9tSwLX6VYrV0g%3d%3d";
            APP_FACTORY_CLASS_3_NEW.OrgCode = "20003000";
            APP_FACTORY_CLASS_3_NEW.OperatingDays = 30;
            APP_FACTORY_CLASS_3_NEW.OperatingCost = 500;
            APP_FACTORY_CLASS_3_NEW.OperatingCost2 = 60000;
            APP_FACTORY_CLASS_3_NEW.OperatingCostType = CostType.Range;
            APP_FACTORY_CLASS_3_NEW.CitizenApplicationUrl = "http://dsapptest.diw.go.th/BizCheckProfile.aspx?typesend=Kl0fEDVdjQfcDPj2uYgIEMJH%2focLRkObZMiQiRO9jrtl%2f2bzzI8ywWieEOXT%2bIQx6r5jjCvV7Wx1sxbv5swtk0jozf%2fXrkaKqbTkK89EBz29CTz5KjHgmDKx2OkjiANA6DAQ5wEaaRR1tXVke4bTRS8o7G8lWtMuADfEheMDANrkh1aEtLcr26YKJbh2gN4iv%2fHidvbTx9tSwLX6VYrV0g%3d%3d";
            APP_FACTORY_CLASS_3_NEW.CitizenOperatingDays = 30;
            APP_FACTORY_CLASS_3_NEW.CitizenOperatingCost = 500;
            APP_FACTORY_CLASS_3_NEW.CitizenOperatingCost2 = 60000;
            APP_FACTORY_CLASS_3_NEW.CitizenOperatingCostType = CostType.Range;
            APP_FACTORY_CLASS_3_NEW.ShowRemark = true;
            APP_FACTORY_CLASS_3_NEW.Remark = "ค่าธรรมเนียมขึ้นอยู่กับค่าเครื่องจักร (แรงม้า)";
            APP_FACTORY_CLASS_3_NEW.HandbookUrl = "https://info.go.th/#!/th/search/132/%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99/";
            APP_FACTORY_CLASS_3_NEW.CitizenShowRemark = true;
            APP_FACTORY_CLASS_3_NEW.CitizenRemark = "ค่าธรรมเนียมขึ้นอยู่กับค่าเครื่องจักร (แรงม้า)";
            APP_FACTORY_CLASS_3_NEW.CitizenHandbookUrl = "https://info.go.th/#!/th/search/132/%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99/";

            context.Applications.AddOrUpdate(APP_FACTORY_CLASS_3_NEW);

            #endregion

            #region APP_FACTORY_CLASS_3_START_NEW

            Application APP_FACTORY_CLASS_3_START_NEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_FACTORY_CLASS_3_START_NEW).FirstOrDefault();

            if (APP_FACTORY_CLASS_3_START_NEW == null)
            {
                APP_FACTORY_CLASS_3_START_NEW = new Application();
                APP_FACTORY_CLASS_3_START_NEW.ApplicationSysName = ApplicationSysName.APP_FACTORY_CLASS_3_START_NEW;
                APP_FACTORY_CLASS_3_START_NEW.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_FACTORY_CLASS_3_START_NEW.ApplicationUrl = "https://slatest.info.go.th/bizportal-phase2";
                APP_FACTORY_CLASS_3_START_NEW.RequestAtOrg = true;
                APP_FACTORY_CLASS_3_START_NEW.CitizenRequestAtOrg = true;
                APP_FACTORY_CLASS_3_START_NEW.OrgCode = "20003000";
                APP_FACTORY_CLASS_3_START_NEW.LogoFileID = null;
                APP_FACTORY_CLASS_3_START_NEW.HandbookUrl = null;
                APP_FACTORY_CLASS_3_START_NEW.OperatingDays = null;
                APP_FACTORY_CLASS_3_START_NEW.OperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_3_START_NEW.OperatingDays2 = null;
                APP_FACTORY_CLASS_3_START_NEW.OperatingCost = null;
                APP_FACTORY_CLASS_3_START_NEW.OperatingCostType = CostType.Fixed;
                APP_FACTORY_CLASS_3_START_NEW.OperatingCost2 = null;
                APP_FACTORY_CLASS_3_START_NEW.CitizenOperatingDays = null;
                APP_FACTORY_CLASS_3_START_NEW.CitizenOperatingDayType = CostType.Fixed;
                APP_FACTORY_CLASS_3_START_NEW.CitizenOperatingDays2 = null;
                APP_FACTORY_CLASS_3_START_NEW.CitizenOperatingCost = null;
                APP_FACTORY_CLASS_3_START_NEW.CitizenOperatingCostType = CostType.Fixed;
                APP_FACTORY_CLASS_3_START_NEW.CitizenOperatingCost2 = null;
                APP_FACTORY_CLASS_3_START_NEW.SingleFormEnabled = true;
                string TranslateName = "การแจ้งเริ่มประกอบกิจการโรงงานจำพวกที่ 3";

                createApplication(context, APP_FACTORY_CLASS_3_START_NEW, TranslateName, creater);
            }

            // Cumulative update info
            APP_FACTORY_CLASS_3_START_NEW.AppsHookClassName = APP_HOOK_NEW_PERMIT;
            APP_FACTORY_CLASS_3_START_NEW.ApplicationUrl = "http://dsapptest.diw.go.th/BizCheckProfile.aspx?typesend=Kl0fEDVdjQfcDPj2uYgIEMJH%2focLRkObZMiQiRO9jrtl%2f2bzzI8ywWieEOXT%2bIQx6r5jjCvV7Wx1sxbv5swtk0jozf%2fXrkaKqbTkK89EBz29CTz5KjHgmDKx2OkjiANA6DAQ5wEaaRR1tXVke4bTRS8o7G8lWtMuADfEheMDANrkh1aEtLcr26YKJbh2gN4iv%2fHidvbTx9tSwLX6VYrV0g%3d%3d";
            APP_FACTORY_CLASS_3_START_NEW.OrgCode = "20003000";
            APP_FACTORY_CLASS_3_START_NEW.OperatingDays = 15;
            APP_FACTORY_CLASS_3_START_NEW.OperatingCost = 150;
            APP_FACTORY_CLASS_3_START_NEW.OperatingCost2 = 18000;
            APP_FACTORY_CLASS_3_START_NEW.OperatingCostType = CostType.Range;
            APP_FACTORY_CLASS_3_START_NEW.CitizenApplicationUrl = "http://dsapptest.diw.go.th/BizCheckProfile.aspx?typesend=Kl0fEDVdjQfcDPj2uYgIEMJH%2focLRkObZMiQiRO9jrtl%2f2bzzI8ywWieEOXT%2bIQx6r5jjCvV7Wx1sxbv5swtk0jozf%2fXrkaKqbTkK89EBz29CTz5KjHgmDKx2OkjiANA6DAQ5wEaaRR1tXVke4bTRS8o7G8lWtMuADfEheMDANrkh1aEtLcr26YKJbh2gN4iv%2fHidvbTx9tSwLX6VYrV0g%3d%3d";
            APP_FACTORY_CLASS_3_START_NEW.CitizenOperatingDays = 15;
            APP_FACTORY_CLASS_3_START_NEW.CitizenOperatingCost = 150;
            APP_FACTORY_CLASS_3_START_NEW.CitizenOperatingCost2 = 18000;
            APP_FACTORY_CLASS_3_START_NEW.CitizenOperatingCostType = CostType.Range;
            APP_FACTORY_CLASS_3_START_NEW.HandbookUrl = "https://info.go.th/#!/th/search/7205/%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99/";
            APP_FACTORY_CLASS_3_START_NEW.CitizenHandbookUrl = "https://info.go.th/#!/th/search/7205/%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%87%E0%B8%B2%E0%B8%99/";
            context.Applications.AddOrUpdate(APP_FACTORY_CLASS_3_NEW);

            #endregion


            #region APP_IMPORT_FOOD

            Application APP_IMPORT_FOOD = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_IMPORT_FOOD).FirstOrDefault();

            if (APP_IMPORT_FOOD == null)
            {
                APP_IMPORT_FOOD = new Application();
                APP_IMPORT_FOOD.ApplicationSysName = ApplicationSysName.APP_IMPORT_FOOD;
                APP_IMPORT_FOOD.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_IMPORT_FOOD.ApplicationUrl = "https://slatest.info.go.th/bizportal-phase2";
                APP_IMPORT_FOOD.RequestAtOrg = true;
                APP_IMPORT_FOOD.CitizenRequestAtOrg = true;
                APP_IMPORT_FOOD.OrgCode = "19010000";
                APP_IMPORT_FOOD.LogoFileID = null;
                APP_IMPORT_FOOD.HandbookUrl = null;
                APP_IMPORT_FOOD.OperatingDays = null;
                APP_IMPORT_FOOD.OperatingDayType = CostType.Fixed;
                APP_IMPORT_FOOD.OperatingDays2 = null;
                APP_IMPORT_FOOD.OperatingCost = null;
                APP_IMPORT_FOOD.OperatingCostType = CostType.Fixed;
                APP_IMPORT_FOOD.OperatingCost2 = null;
                APP_IMPORT_FOOD.CitizenOperatingDays = null;
                APP_IMPORT_FOOD.CitizenOperatingDayType = CostType.Fixed;
                APP_IMPORT_FOOD.CitizenOperatingDays2 = null;
                APP_IMPORT_FOOD.CitizenOperatingCost = null;
                APP_IMPORT_FOOD.CitizenOperatingCostType = CostType.Fixed;
                APP_IMPORT_FOOD.CitizenOperatingCost2 = null;
                APP_IMPORT_FOOD.SingleFormEnabled = true;
                string TranslateName = "ใบอนุญาตนำเข้าอาหาร";

                createApplication(context, APP_IMPORT_FOOD, TranslateName, creater);
            }

            APP_IMPORT_FOOD.AppsHookClassName = APP_HOOK_NEW_PERMIT;
            context.Applications.AddOrUpdate(APP_IMPORT_FOOD);

            #endregion

            #region APP_FOOD_PRODUCTION

            Application APP_FOOD_PRODUCTION = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_FOOD_PRODUCTION).FirstOrDefault();

            if (APP_FOOD_PRODUCTION == null)
            {
                APP_FOOD_PRODUCTION = new Application();
                APP_FOOD_PRODUCTION.ApplicationSysName = ApplicationSysName.APP_FOOD_PRODUCTION;
                APP_FOOD_PRODUCTION.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_FOOD_PRODUCTION.ApplicationUrl = "https://slatest.info.go.th/bizportal-phase2";
                APP_FOOD_PRODUCTION.RequestAtOrg = true;
                APP_FOOD_PRODUCTION.CitizenRequestAtOrg = true;
                APP_FOOD_PRODUCTION.OrgCode = "19010000";
                APP_FOOD_PRODUCTION.LogoFileID = null;
                APP_FOOD_PRODUCTION.HandbookUrl = null;
                APP_FOOD_PRODUCTION.OperatingDays = null;
                APP_FOOD_PRODUCTION.OperatingDayType = CostType.Fixed;
                APP_FOOD_PRODUCTION.OperatingDays2 = null;
                APP_FOOD_PRODUCTION.OperatingCost = null;
                APP_FOOD_PRODUCTION.OperatingCostType = CostType.Fixed;
                APP_FOOD_PRODUCTION.OperatingCost2 = null;
                APP_FOOD_PRODUCTION.CitizenOperatingDays = null;
                APP_FOOD_PRODUCTION.CitizenOperatingDayType = CostType.Fixed;
                APP_FOOD_PRODUCTION.CitizenOperatingDays2 = null;
                APP_FOOD_PRODUCTION.CitizenOperatingCost = null;
                APP_FOOD_PRODUCTION.CitizenOperatingCostType = CostType.Fixed;
                APP_FOOD_PRODUCTION.CitizenOperatingCost2 = null;
                APP_FOOD_PRODUCTION.SingleFormEnabled = true;
                string TranslateName = "ใบอนุญาตผลิตอาหาร";

                createApplication(context, APP_FOOD_PRODUCTION, TranslateName, creater);
            }

            APP_FOOD_PRODUCTION.AppsHookClassName = APP_HOOK_NEW_PERMIT;
            context.Applications.AddOrUpdate(APP_FOOD_PRODUCTION);

            #endregion


            #region APP_FOOD_ADVERTISEMENT

            Application APP_FOOD_ADVERTISEMENT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_FOOD_ADVERTISEMENT).FirstOrDefault();

            if (APP_FOOD_ADVERTISEMENT == null)
            {
                APP_FOOD_ADVERTISEMENT = new Application();
                APP_FOOD_ADVERTISEMENT.ApplicationSysName = ApplicationSysName.APP_FOOD_ADVERTISEMENT;
                APP_FOOD_ADVERTISEMENT.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_FOOD_ADVERTISEMENT.ApplicationUrl = "https://slatest.info.go.th/bizportal-phase2";
                APP_FOOD_ADVERTISEMENT.RequestAtOrg = true;
                APP_FOOD_ADVERTISEMENT.CitizenRequestAtOrg = true;
                APP_FOOD_ADVERTISEMENT.OrgCode = "19010000";
                APP_FOOD_ADVERTISEMENT.LogoFileID = null;
                APP_FOOD_ADVERTISEMENT.HandbookUrl = null;
                APP_FOOD_ADVERTISEMENT.OperatingDays = null;
                APP_FOOD_ADVERTISEMENT.OperatingDayType = CostType.Fixed;
                APP_FOOD_ADVERTISEMENT.OperatingDays2 = null;
                APP_FOOD_ADVERTISEMENT.OperatingCost = null;
                APP_FOOD_ADVERTISEMENT.OperatingCostType = CostType.Fixed;
                APP_FOOD_ADVERTISEMENT.OperatingCost2 = null;
                APP_FOOD_ADVERTISEMENT.CitizenOperatingDays = null;
                APP_FOOD_ADVERTISEMENT.CitizenOperatingDayType = CostType.Fixed;
                APP_FOOD_ADVERTISEMENT.CitizenOperatingDays2 = null;
                APP_FOOD_ADVERTISEMENT.CitizenOperatingCost = null;
                APP_FOOD_ADVERTISEMENT.CitizenOperatingCostType = CostType.Fixed;
                APP_FOOD_ADVERTISEMENT.CitizenOperatingCost2 = null;
                APP_FOOD_ADVERTISEMENT.SingleFormEnabled = true;
                string TranslateName = "ใบอนุญาตโฆษณาอาหาร (ฆอ.2)";

                createApplication(context, APP_FOOD_ADVERTISEMENT, TranslateName, creater);
            }

            // Cumulative update info
            APP_FOOD_ADVERTISEMENT.AppsHookClassName = APP_HOOK_NEW_PERMIT;
            APP_FOOD_ADVERTISEMENT.ApplicationUrl = "https://privus.fda.moph.go.th/index.aspx";
            APP_FOOD_ADVERTISEMENT.OrgCode = "19010000";
            APP_FOOD_ADVERTISEMENT.OperatingDays = 8;
            APP_FOOD_ADVERTISEMENT.OperatingCost = 660;
            APP_FOOD_ADVERTISEMENT.OperatingCost2 = 7000;
            APP_FOOD_ADVERTISEMENT.OperatingCostType = CostType.Range;
            APP_FOOD_ADVERTISEMENT.CitizenApplicationUrl = "https://privus.fda.moph.go.th/index.aspx";
            APP_FOOD_ADVERTISEMENT.CitizenOperatingDays = 8;
            APP_FOOD_ADVERTISEMENT.CitizenOperatingCost = 660;
            APP_FOOD_ADVERTISEMENT.CitizenOperatingCost2 = 7000;
            APP_FOOD_ADVERTISEMENT.CitizenOperatingCostType = CostType.Range;
            APP_FOOD_ADVERTISEMENT.HandbookUrl = "https://www.info.go.th/#!/th/search/113/โฆษณาอาหาร/";
            APP_FOOD_ADVERTISEMENT.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/113/โฆษณาอาหาร/";
            context.Applications.AddOrUpdate(APP_FOOD_ADVERTISEMENT);

            #endregion


            #region APP_FOOD_CERTIFICATE

            Application APP_FOOD_CERTIFICATE = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_FOOD_CERTIFICATE).FirstOrDefault();

            if (APP_FOOD_CERTIFICATE == null)
            {
                APP_FOOD_CERTIFICATE = new Application();
                APP_FOOD_CERTIFICATE.ApplicationSysName = ApplicationSysName.APP_FOOD_CERTIFICATE;
                APP_FOOD_CERTIFICATE.AppsHookClassName = APP_HOOK_NEW_PERMIT;
                APP_FOOD_CERTIFICATE.ApplicationUrl = "https://slatest.info.go.th/bizportal-phase2";
                APP_FOOD_CERTIFICATE.RequestAtOrg = true;
                APP_FOOD_CERTIFICATE.CitizenRequestAtOrg = true;
                APP_FOOD_CERTIFICATE.OrgCode = "19010000";
                APP_FOOD_CERTIFICATE.LogoFileID = null;
                APP_FOOD_CERTIFICATE.HandbookUrl = null;
                APP_FOOD_CERTIFICATE.OperatingDays = null;
                APP_FOOD_CERTIFICATE.OperatingDayType = CostType.Fixed;
                APP_FOOD_CERTIFICATE.OperatingDays2 = null;
                APP_FOOD_CERTIFICATE.OperatingCost = null;
                APP_FOOD_CERTIFICATE.OperatingCostType = CostType.Fixed;
                APP_FOOD_CERTIFICATE.OperatingCost2 = null;
                APP_FOOD_CERTIFICATE.CitizenOperatingDays = null;
                APP_FOOD_CERTIFICATE.CitizenOperatingDayType = CostType.Fixed;
                APP_FOOD_CERTIFICATE.CitizenOperatingDays2 = null;
                APP_FOOD_CERTIFICATE.CitizenOperatingCost = null;
                APP_FOOD_CERTIFICATE.CitizenOperatingCostType = CostType.Fixed;
                APP_FOOD_CERTIFICATE.CitizenOperatingCost2 = null;
                APP_FOOD_CERTIFICATE.SingleFormEnabled = true;
                string TranslateName = "หนังสือรับรองผลิตภัณฑ์อาหาร (Certificate of Free Sale)";

                createApplication(context, APP_FOOD_CERTIFICATE, TranslateName, creater);
            }

            APP_FOOD_CERTIFICATE.AppsHookClassName = APP_HOOK_NEW_PERMIT;
            APP_FOOD_CERTIFICATE.ApplicationUrl = "https://privus.fda.moph.go.th/index.aspx";
            APP_FOOD_CERTIFICATE.OrgCode = "19010000";
            APP_FOOD_CERTIFICATE.OperatingDays = 4;
            APP_FOOD_CERTIFICATE.OperatingCost = 5000;
            APP_FOOD_CERTIFICATE.OperatingCostType = CostType.StartAt;
            APP_FOOD_CERTIFICATE.CitizenApplicationUrl = "https://privus.fda.moph.go.th/index.aspx";
            APP_FOOD_CERTIFICATE.CitizenOperatingDays = 4;
            APP_FOOD_CERTIFICATE.CitizenOperatingCost = 5000;
            APP_FOOD_CERTIFICATE.CitizenOperatingCostType = CostType.StartAt;
            APP_FOOD_CERTIFICATE.HandbookUrl = "https://info.go.th/#!/th/search/118/%E0%B8%AD%E0%B8%AD%E0%B8%81%E0%B8%AB%E0%B8%99%E0%B8%B1%E0%B8%87%E0%B8%AA%E0%B8%B7%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B8%A3%E0%B8%AD%E0%B8%87%E0%B8%AA%E0%B8%96%E0%B8%B2%E0%B8%99%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%9C%E0%B8%A5%E0%B8%B4%E0%B8%95%E0%B9%81%E0%B8%A5%E0%B8%B0%E0%B8%9C%E0%B8%A5%E0%B8%B4%E0%B8%95%E0%B8%A0%E0%B8%B1%E0%B8%93%E0%B8%91%E0%B9%8C%E0%B8%AD%E0%B8%B2%E0%B8%AB%E0%B8%B2%E0%B8%A3%E0%B9%80%E0%B8%9E%E0%B8%B7%E0%B9%88%E0%B8%AD%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%AA%E0%B9%88%E0%B8%87%E0%B8%AD%E0%B8%AD%E0%B8%81/";
            APP_FOOD_CERTIFICATE.CitizenHandbookUrl = "https://info.go.th/#!/th/search/118/%E0%B8%AD%E0%B8%AD%E0%B8%81%E0%B8%AB%E0%B8%99%E0%B8%B1%E0%B8%87%E0%B8%AA%E0%B8%B7%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B8%A3%E0%B8%AD%E0%B8%87%E0%B8%AA%E0%B8%96%E0%B8%B2%E0%B8%99%E0%B8%97%E0%B8%B5%E0%B9%88%E0%B8%9C%E0%B8%A5%E0%B8%B4%E0%B8%95%E0%B9%81%E0%B8%A5%E0%B8%B0%E0%B8%9C%E0%B8%A5%E0%B8%B4%E0%B8%95%E0%B8%A0%E0%B8%B1%E0%B8%93%E0%B8%91%E0%B9%8C%E0%B8%AD%E0%B8%B2%E0%B8%AB%E0%B8%B2%E0%B8%A3%E0%B9%80%E0%B8%9E%E0%B8%B7%E0%B9%88%E0%B8%AD%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%AA%E0%B9%88%E0%B8%87%E0%B8%AD%E0%B8%AD%E0%B8%81/";
            context.Applications.AddOrUpdate(APP_FOOD_CERTIFICATE);

            #endregion

            #endregion

            #region สบส
            
            #region ขออนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)
            Application APP_CLINIC_BUSINESS = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_CLINIC_BUSINESS).FirstOrDefault();
            if (APP_CLINIC_BUSINESS == null)
            {
                APP_CLINIC_BUSINESS = new Application();
                APP_CLINIC_BUSINESS.ApplicationSysName = ApplicationSysName.APP_CLINIC_BUSINESS;
                APP_CLINIC_BUSINESS.AppsHookClassName = "BizPortal.AppsHook.HSSClinicBusinessApphook";
                APP_CLINIC_BUSINESS.OrgCode = "19007000";
                APP_CLINIC_BUSINESS.LogoFileID = null;
                APP_CLINIC_BUSINESS.MultipleRequestSupport = true;
                APP_CLINIC_BUSINESS.HandbookUrl = "https://info.go.th/#!/th/search/6660/คลินิก/";
                APP_CLINIC_BUSINESS.CitizenHandbookUrl = "https://info.go.th/#!/th/search/6660/คลินิก/";
                APP_CLINIC_BUSINESS.OperatingDays = 67;
                APP_CLINIC_BUSINESS.OperatingDayType = CostType.Fixed;
                APP_CLINIC_BUSINESS.OperatingDays2 = null;
                APP_CLINIC_BUSINESS.OperatingCost = 1000;
                APP_CLINIC_BUSINESS.OperatingCostType = CostType.Fixed;
                APP_CLINIC_BUSINESS.OperatingCost2 = null;
                APP_CLINIC_BUSINESS.CitizenOperatingDays = 67;
                APP_CLINIC_BUSINESS.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_BUSINESS.CitizenOperatingDays2 = null;
                APP_CLINIC_BUSINESS.CitizenOperatingCost = 1000;
                APP_CLINIC_BUSINESS.CitizenOperatingCostType = CostType.Fixed;
                APP_CLINIC_BUSINESS.CitizenOperatingCost2 = null;
                APP_CLINIC_BUSINESS.ShowRemark = true;
                APP_CLINIC_BUSINESS.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_CLINIC_BUSINESS.CitizenShowRemark = true;
                APP_CLINIC_BUSINESS.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_CLINIC_BUSINESS.SingleFormEnabled = true;
                string TranslateName = "ขออนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)";
                createApplication(context, APP_CLINIC_BUSINESS, TranslateName, creater);
            }
            else
            {
                APP_CLINIC_BUSINESS.ApplicationSysName = ApplicationSysName.APP_CLINIC_BUSINESS;
                APP_CLINIC_BUSINESS.AppsHookClassName = "BizPortal.AppsHook.HSSClinicBusinessApphook";
                APP_CLINIC_BUSINESS.OrgCode = "19007000";
                APP_CLINIC_BUSINESS.LogoFileID = null;
                APP_CLINIC_BUSINESS.MultipleRequestSupport = true;
                APP_CLINIC_BUSINESS.HandbookUrl = "https://info.go.th/#!/th/search/6660/คลินิก/";
                APP_CLINIC_BUSINESS.CitizenHandbookUrl = "https://info.go.th/#!/th/search/6660/คลินิก/";
                APP_CLINIC_BUSINESS.OperatingDays = 67;
                APP_CLINIC_BUSINESS.OperatingDayType = CostType.Fixed;
                APP_CLINIC_BUSINESS.OperatingDays2 = null;
                APP_CLINIC_BUSINESS.OperatingCost = 1000;
                APP_CLINIC_BUSINESS.OperatingCostType = CostType.Fixed;
                APP_CLINIC_BUSINESS.OperatingCost2 = null;
                APP_CLINIC_BUSINESS.CitizenOperatingDays = 67;
                APP_CLINIC_BUSINESS.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_BUSINESS.CitizenOperatingDays2 = null;
                APP_CLINIC_BUSINESS.CitizenOperatingCost = 1000;
                APP_CLINIC_BUSINESS.CitizenOperatingCostType = CostType.Fixed;
                APP_CLINIC_BUSINESS.CitizenOperatingCost2 = null;
                APP_CLINIC_BUSINESS.ShowRemark = true;
                APP_CLINIC_BUSINESS.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_CLINIC_BUSINESS.CitizenShowRemark = true;
                APP_CLINIC_BUSINESS.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_CLINIC_BUSINESS.SingleFormEnabled = true;
                string TranslateName = "ขออนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)";
                updateApplication(context, APP_CLINIC_BUSINESS, TranslateName, creater);
            }
            #endregion

            #region ขอรับใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)
            Application APP_CLINIC_OPERATION = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_CLINIC_OPERATION).FirstOrDefault();
            if (APP_CLINIC_OPERATION == null)
            {
                APP_CLINIC_OPERATION = new Application();
                APP_CLINIC_OPERATION.ApplicationSysName = ApplicationSysName.APP_CLINIC_OPERATION;
                APP_CLINIC_OPERATION.AppsHookClassName = "BizPortal.AppsHook.HSSClinicOperationApphook";
                APP_CLINIC_OPERATION.OrgCode = "19007000";
                APP_CLINIC_OPERATION.LogoFileID = null;
                APP_CLINIC_OPERATION.MultipleRequestSupport = true;
                APP_CLINIC_OPERATION.HandbookUrl = "https://info.go.th/#!/th/search/6660/คลินิก/";
                APP_CLINIC_OPERATION.CitizenHandbookUrl = "https://info.go.th/#!/th/search/6660/คลินิก/";
                APP_CLINIC_OPERATION.OperatingDays = 21;
                APP_CLINIC_OPERATION.OperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION.OperatingDays2 = null;
                APP_CLINIC_OPERATION.OperatingCost = 250;
                APP_CLINIC_OPERATION.OperatingCostType = CostType.Fixed;
                APP_CLINIC_OPERATION.OperatingCost2 = null;
                APP_CLINIC_OPERATION.CitizenOperatingDays = 21;
                APP_CLINIC_OPERATION.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION.CitizenOperatingDays2 = null;
                APP_CLINIC_OPERATION.CitizenOperatingCost = 250;
                APP_CLINIC_OPERATION.CitizenOperatingCostType = CostType.Fixed;
                APP_CLINIC_OPERATION.CitizenOperatingCost2 = null;
                APP_CLINIC_OPERATION.ShowRemark = true;
                APP_CLINIC_OPERATION.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</ span>";
                APP_CLINIC_OPERATION.CitizenShowRemark = true;
                APP_CLINIC_OPERATION.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</ span>";
                APP_CLINIC_OPERATION.SingleFormEnabled = true;
                APP_CLINIC_OPERATION.RequestAtOrg = true;
                APP_CLINIC_OPERATION.TemporaryDisable = false;
                string TranslateName = "ขอรับใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)";
                createApplication(context, APP_CLINIC_OPERATION, TranslateName, creater);
            }
            else
            {
                APP_CLINIC_OPERATION.ApplicationSysName = ApplicationSysName.APP_CLINIC_OPERATION;
                APP_CLINIC_OPERATION.AppsHookClassName = "BizPortal.AppsHook.HSSClinicOperationApphook";
                APP_CLINIC_OPERATION.OrgCode = "19007000";
                APP_CLINIC_OPERATION.LogoFileID = null;
                APP_CLINIC_OPERATION.MultipleRequestSupport = true;
                APP_CLINIC_OPERATION.HandbookUrl = "https://info.go.th/#!/th/search/6660/คลินิก/";
                APP_CLINIC_OPERATION.CitizenHandbookUrl = "https://info.go.th/#!/th/search/6660/คลินิก/";
                APP_CLINIC_OPERATION.OperatingDays = 21;
                APP_CLINIC_OPERATION.OperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION.OperatingDays2 = null;
                APP_CLINIC_OPERATION.OperatingCost = 250;
                APP_CLINIC_OPERATION.OperatingCostType = CostType.Fixed;
                APP_CLINIC_OPERATION.OperatingCost2 = null;
                APP_CLINIC_OPERATION.CitizenOperatingDays = 21;
                APP_CLINIC_OPERATION.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION.CitizenOperatingDays2 = null;
                APP_CLINIC_OPERATION.CitizenOperatingCost = 250;
                APP_CLINIC_OPERATION.CitizenOperatingCostType = CostType.Fixed;
                APP_CLINIC_OPERATION.CitizenOperatingCost2 = null;
                APP_CLINIC_OPERATION.ShowRemark = true;
                APP_CLINIC_OPERATION.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</ span>";
                APP_CLINIC_OPERATION.CitizenShowRemark = true;
                APP_CLINIC_OPERATION.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</ span>";
                APP_CLINIC_OPERATION.SingleFormEnabled = true;
                APP_CLINIC_OPERATION.RequestAtOrg = true;
                APP_CLINIC_OPERATION.TemporaryDisable = false;
                string TranslateName = "ขอรับใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)";
                updateApplication(context, APP_CLINIC_OPERATION, TranslateName, creater);
            }
            #endregion

            #region งานบริการขออนุญาตให้ประกอบกิจการสถานพยาบาล (โรงพยาบาล)
            Application APP_HOSPITAL_BUSINESS = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HOSPITAL_BUSINESS).FirstOrDefault();
            if (APP_HOSPITAL_BUSINESS == null)
            {
                APP_HOSPITAL_BUSINESS = new Application();
                APP_HOSPITAL_BUSINESS.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_BUSINESS;
                APP_HOSPITAL_BUSINESS.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalBusinessApphook";
                APP_HOSPITAL_BUSINESS.OrgCode = "19007000";
                APP_HOSPITAL_BUSINESS.LogoFileID = null;
                APP_HOSPITAL_BUSINESS.MultipleRequestSupport = true;
                APP_HOSPITAL_BUSINESS.HandbookUrl = "https://www.info.go.th/#!/th/search/8656/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_BUSINESS.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/8656/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_HOSPITAL_BUSINESS.OperatingDays = 91;
                APP_HOSPITAL_BUSINESS.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_BUSINESS.OperatingDays2 = null;
                APP_HOSPITAL_BUSINESS.CitizenOperatingDays = 91;
                APP_HOSPITAL_BUSINESS.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_BUSINESS.CitizenOperatingDays2 = null;

                APP_HOSPITAL_BUSINESS.OperatingCost = 2500;
                APP_HOSPITAL_BUSINESS.OperatingCostType = CostType.Range;
                APP_HOSPITAL_BUSINESS.OperatingCost2 = 25000;
                APP_HOSPITAL_BUSINESS.CitizenOperatingCost = 2500;
                APP_HOSPITAL_BUSINESS.CitizenOperatingCostType = CostType.Range;
                APP_HOSPITAL_BUSINESS.CitizenOperatingCost2 = 25000;

                APP_HOSPITAL_BUSINESS.ShowRemark = true;
                APP_HOSPITAL_BUSINESS.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎ กระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_BUSINESS.CitizenShowRemark = true;
                APP_HOSPITAL_BUSINESS.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎ กระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_BUSINESS.SingleFormEnabled = true;
                string TranslateName = "ขออนุญาตให้ประกอบกิจการสถานพยาบาล (โรงพยาบาล)";
                createApplication(context, APP_HOSPITAL_BUSINESS, TranslateName, creater);
            }
            else
            {
                APP_HOSPITAL_BUSINESS.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_BUSINESS;
                APP_HOSPITAL_BUSINESS.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalBusinessApphook";
                APP_HOSPITAL_BUSINESS.OrgCode = "19007000";
                APP_HOSPITAL_BUSINESS.LogoFileID = null;
                APP_HOSPITAL_BUSINESS.MultipleRequestSupport = true;
                APP_HOSPITAL_BUSINESS.HandbookUrl = "https://www.info.go.th/#!/th/search/8656/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_BUSINESS.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/8656/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_HOSPITAL_BUSINESS.OperatingDays = 91;
                APP_HOSPITAL_BUSINESS.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_BUSINESS.OperatingDays2 = null;
                APP_HOSPITAL_BUSINESS.CitizenOperatingDays = 91;
                APP_HOSPITAL_BUSINESS.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_BUSINESS.CitizenOperatingDays2 = null;

                APP_HOSPITAL_BUSINESS.OperatingCost = 2500;
                APP_HOSPITAL_BUSINESS.OperatingCostType = CostType.Range;
                APP_HOSPITAL_BUSINESS.OperatingCost2 = 25000;
                APP_HOSPITAL_BUSINESS.CitizenOperatingCost = 2500;
                APP_HOSPITAL_BUSINESS.CitizenOperatingCostType = CostType.Range;
                APP_HOSPITAL_BUSINESS.CitizenOperatingCost2 = 25000;

                APP_HOSPITAL_BUSINESS.ShowRemark = true;
                APP_HOSPITAL_BUSINESS.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎ กระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_BUSINESS.CitizenShowRemark = true;
                APP_HOSPITAL_BUSINESS.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎ กระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_BUSINESS.SingleFormEnabled = true;
                string TranslateName = "ขออนุญาตให้ประกอบกิจการสถานพยาบาล (โรงพยาบาล)";
                updateApplication(context, APP_HOSPITAL_BUSINESS, TranslateName, creater);
            }
            #endregion

            #region งานบริการขอรับใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)
            Application APP_HOSPITAL_OPERATION = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HOSPITAL_OPERATION).FirstOrDefault();
            if (APP_HOSPITAL_OPERATION == null)
            {
                APP_HOSPITAL_OPERATION = new Application();
                APP_HOSPITAL_OPERATION.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_OPERATION;
                APP_HOSPITAL_OPERATION.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalOperationApphook";
                APP_HOSPITAL_OPERATION.OrgCode = "19007000";
                APP_HOSPITAL_OPERATION.LogoFileID = null;
                APP_HOSPITAL_OPERATION.MultipleRequestSupport = true;
                APP_HOSPITAL_OPERATION.HandbookUrl = "https://www.info.go.th/#!/th/search/8656/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_OPERATION.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/8656/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_HOSPITAL_OPERATION.OperatingDays = 21;
                APP_HOSPITAL_OPERATION.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION.OperatingDays2 = null;
                APP_HOSPITAL_OPERATION.CitizenOperatingDays = 21;
                APP_HOSPITAL_OPERATION.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION.CitizenOperatingDays2 = null;

                APP_HOSPITAL_OPERATION.OperatingCost = 500;
                APP_HOSPITAL_OPERATION.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION.OperatingCost2 = null;
                APP_HOSPITAL_OPERATION.CitizenOperatingCost = 500;
                APP_HOSPITAL_OPERATION.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION.CitizenOperatingCost2 = null;

                APP_HOSPITAL_OPERATION.ShowRemark = true;
                APP_HOSPITAL_OPERATION.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span><br/><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_OPERATION.CitizenShowRemark = true;
                APP_HOSPITAL_OPERATION.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span><br/><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_OPERATION.SingleFormEnabled = true;
                APP_HOSPITAL_OPERATION.RequestAtOrg = true;
                string TranslateName = "ขอรับใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)";
                createApplication(context, APP_HOSPITAL_OPERATION, TranslateName, creater);
            }
            else
            {
                APP_HOSPITAL_OPERATION.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_OPERATION;
                APP_HOSPITAL_OPERATION.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalOperationApphook";
                APP_HOSPITAL_OPERATION.OrgCode = "19007000";
                APP_HOSPITAL_OPERATION.LogoFileID = null;
                APP_HOSPITAL_OPERATION.MultipleRequestSupport = true;
                APP_HOSPITAL_OPERATION.HandbookUrl = "https://www.info.go.th/#!/th/search/8656/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_OPERATION.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/8656/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_HOSPITAL_OPERATION.OperatingDays = 21;
                APP_HOSPITAL_OPERATION.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION.OperatingDays2 = null;
                APP_HOSPITAL_OPERATION.CitizenOperatingDays = 21;
                APP_HOSPITAL_OPERATION.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION.CitizenOperatingDays2 = null;

                APP_HOSPITAL_OPERATION.OperatingCost = 500;
                APP_HOSPITAL_OPERATION.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION.OperatingCost2 = null;
                APP_HOSPITAL_OPERATION.CitizenOperatingCost = 500;
                APP_HOSPITAL_OPERATION.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION.CitizenOperatingCost2 = null;

                APP_HOSPITAL_OPERATION.ShowRemark = true;
                APP_HOSPITAL_OPERATION.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span><br/><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_OPERATION.CitizenShowRemark = true;
                APP_HOSPITAL_OPERATION.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span><br/><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_OPERATION.SingleFormEnabled = true;
                APP_HOSPITAL_OPERATION.RequestAtOrg = true;
                string TranslateName = "ขอรับใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)";
                updateApplication(context, APP_HOSPITAL_OPERATION, TranslateName, creater);
            }
            #endregion

            #region ขอใบอนุญาตให้ตั้งสถานพยาบาลสัตว์
            Application APP_PET_HOSPITAL_OPERATION = context.Applications.Where(x => x.ApplicationSysName.Equals("ขอใบอนุญาตให้ตั้งสถานพยาบาลสัตว์")).FirstOrDefault();
            if (APP_PET_HOSPITAL_OPERATION == null)
            {
                // Do nothing : this permit is already data in Table Application 
                // Data insert from Seed Phase 2
            }
            else 
            {
                APP_PET_HOSPITAL_OPERATION.OperatingDays = 45;
                APP_PET_HOSPITAL_OPERATION.CitizenOperatingDays = 45;
                updateApplication(context, APP_PET_HOSPITAL_OPERATION, "ขอใบอนุญาตให้ตั้งสถานพยาบาลสัตว์ (ต้องได้รับหนังสืออนุมัติแผนการจัดตั้งสถานพยาบาลสัตว์ก่อน)", creater);
            }
            #endregion

            #region ขอใบอนุญาตดำเนินการสถานพยาบาลสัตว์
            Application APP_PET_HOSPITAL_BUSINESS = context.Applications.Where(x => x.ApplicationSysName.Equals("ขอใบอนุญาตดำเนินการสถานพยาบาลสัตว์")).FirstOrDefault();
            if (APP_PET_HOSPITAL_BUSINESS == null)
            {
                // Do nothing : this permit is already data in Table Application 
                // Data insert from Seed Phase 2
            }
            else
            {
                APP_PET_HOSPITAL_BUSINESS.OperatingDays = 45;
                APP_PET_HOSPITAL_BUSINESS.CitizenOperatingDays = 45;
                updateApplication(context, APP_PET_HOSPITAL_BUSINESS, "ขอใบอนุญาตดำเนินการสถานพยาบาลสัตว์ (ต้องได้รับหนังสืออนุมัติแผนการจัดตั้งสถานพยาบาลสัตว์ก่อน)", creater);
            }
            #endregion

            #region ขอต่ออายุใบอนุญาตให้ตั้งสถานพยาบาลสัตว์
            Application APP_PET_HOSPITAL_OPERATION_RENEW = context.Applications.Where(x => x.ApplicationSysName.Equals("ขอต่ออายุใบอนุญาตให้ตั้งสถานพยาบาลสัตว์")).FirstOrDefault();
            if (APP_PET_HOSPITAL_OPERATION_RENEW == null)
            {
                // Do nothing : this permit is already data in Table Application 
                // Data insert from Seed Phase 2
            }
            else
            {
                APP_PET_HOSPITAL_OPERATION_RENEW.OperatingDays = 45;
                APP_PET_HOSPITAL_OPERATION_RENEW.CitizenOperatingDays = 45;
                updateApplication(context, APP_PET_HOSPITAL_OPERATION_RENEW, "ขอต่ออายุใบอนุญาตให้ตั้งสถานพยาบาลสัตว์", creater);
            }
            #endregion

            #region ขอต่ออายุใบอนุญาตดำเนินการสถานพยาบาลสัตว์
            Application APP_PET_HOSPITAL_BUSINESS_RENEW = context.Applications.Where(x => x.ApplicationSysName.Equals("ขอต่ออายุใบอนุญาตดำเนินการสถานพยาบาลสัตว์")).FirstOrDefault();
            if (APP_PET_HOSPITAL_BUSINESS_RENEW == null)
            {
                // Do nothing : this permit is already data in Table Application 
                // Data insert from Seed Phase 2
            }
            else
            {
                APP_PET_HOSPITAL_BUSINESS_RENEW.OperatingDays = 45;
                APP_PET_HOSPITAL_BUSINESS_RENEW.CitizenOperatingDays = 45;
                updateApplication(context, APP_PET_HOSPITAL_BUSINESS_RENEW, "ขอต่ออายุใบอนุญาตดำเนินการสถานพยาบาลสัตว์", creater);
            }
            #endregion

            #region แก้ไขคำขอต่ออายุใบอนุญาตดำเนินการสถานพยาบาลสัตว์
            Application APP_PET_HOSPITAL_OPERATION_EDIT = context.Applications.Where(x => x.ApplicationSysName.Equals("แก้ไขคำขอต่ออายุใบอนุญาตดำเนินการสถานพยาบาลสัตว์")).FirstOrDefault();
            if (APP_PET_HOSPITAL_OPERATION_EDIT == null)
            {
                // Do nothing : this permit is already data in Table Application 
                // Data insert from Seed Phase 2
            }
            else
            {
                updateApplication(context, APP_PET_HOSPITAL_OPERATION_EDIT, "ขอแก้ไขใบอนุญาตให้ดำเนินการสถานพยาบาลสัตว์", creater);
            }
            #endregion

            #region ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การสะสม ผสมซีเมนต์ หิน ดิน ทราย วัสดุก่อสร้าง รวมทั้งการขุด ตัก ดูด โม่ บด ย่อย ด้วยเครื่องจักร
            Application APP_BUSINESS_BAD_HEALTH_CONSTRUCTION = context.Applications.Where(x => x.ApplicationSysName.Equals("ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การสะสม ผสมซีเมนต์ หิน ดิน ทราย วัสดุก่อสร้าง รวมทั้งการขุด ตัก ดูด โม่ บด ย่อย ด้วยเครื่องจักร")).FirstOrDefault();
            if (APP_BUSINESS_BAD_HEALTH_CONSTRUCTION == null)
            {
                // Do nothing : this permit is already data in Table Application 
                // Data insert from Seed Phase 2
            }
            else
            {
                APP_BUSINESS_BAD_HEALTH_CONSTRUCTION.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้ที่เทศบาลนคร และเทศบาลเมือง</span>";
                APP_BUSINESS_BAD_HEALTH_CONSTRUCTION.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้ที่เทศบาลนคร และเทศบาลเมือง</span>";
                updateApplication(context, APP_BUSINESS_BAD_HEALTH_CONSTRUCTION, "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การสะสม ผสมซีเมนต์ หิน ดิน ทราย วัสดุก่อสร้าง รวมทั้งการขุด ตัก ดูด โม่ บด ย่อย ด้วยเครื่องจักร", creater);
            }
            #endregion

            #region ขอใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ
            Application APP_SPA_NEW = context.Applications.Where(x => x.ApplicationSysName.Equals("ขอใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ")).FirstOrDefault();
            if (APP_SPA_NEW == null)
            {
                // Do nothing : this permit is already data in Table Application 
                // Data insert from Seed Phase 2
            }
            else
            {
                APP_SPA_NEW.AppsHookClassName = "BizPortal.AppsHook.HSSHealthCareAppHook";
                updateApplication(context, APP_SPA_NEW, "ขอใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา", creater);
            }
            #endregion


            #region ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)
            Application APP_CLINIC_BUSINESS_RENEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_CLINIC_BUSINESS_RENEW).FirstOrDefault();
            if (APP_CLINIC_BUSINESS_RENEW == null)
            {
                APP_CLINIC_BUSINESS_RENEW = new Application();
                APP_CLINIC_BUSINESS_RENEW.ApplicationSysName = ApplicationSysName.APP_CLINIC_BUSINESS_RENEW;
                APP_CLINIC_BUSINESS_RENEW.AppsHookClassName = "BizPortal.AppsHook.HSSClinicBusinessRenewAppHook";
                APP_CLINIC_BUSINESS_RENEW.OrgCode = "19007000";
                APP_CLINIC_BUSINESS_RENEW.LogoFileID = null;
                APP_CLINIC_BUSINESS_RENEW.MultipleRequestSupport = true;
                APP_CLINIC_BUSINESS_RENEW.HandbookUrl = "https://info.go.th/#!/th/search/76396/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%95%E0%B9%88%E0%B8%AD%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B9%89%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81%20(N)/";
                APP_CLINIC_BUSINESS_RENEW.CitizenHandbookUrl = "https://info.go.th/#!/th/search/76396/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%95%E0%B9%88%E0%B8%AD%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B9%89%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81%20(N)/";

                APP_CLINIC_BUSINESS_RENEW.OperatingDays = 21;
                APP_CLINIC_BUSINESS_RENEW.OperatingDayType = CostType.Fixed;
                APP_CLINIC_BUSINESS_RENEW.OperatingDays2 = null;
                APP_CLINIC_BUSINESS_RENEW.CitizenOperatingDays = 21;
                APP_CLINIC_BUSINESS_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_BUSINESS_RENEW.CitizenOperatingDays2 = null;

                APP_CLINIC_BUSINESS_RENEW.OperatingCost = 1000;
                APP_CLINIC_BUSINESS_RENEW.OperatingCostType = CostType.Fixed;
                APP_CLINIC_BUSINESS_RENEW.OperatingCost2 = null;
                APP_CLINIC_BUSINESS_RENEW.CitizenOperatingCost = 1000;
                APP_CLINIC_BUSINESS_RENEW.CitizenOperatingCostType = CostType.Fixed;
                APP_CLINIC_BUSINESS_RENEW.CitizenOperatingCost2 = null;

                APP_CLINIC_BUSINESS_RENEW.ShowRemark = true;
                APP_CLINIC_BUSINESS_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_CLINIC_BUSINESS_RENEW.CitizenShowRemark = true;
                APP_CLINIC_BUSINESS_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_CLINIC_BUSINESS_RENEW.SingleFormEnabled = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)";
                createApplication(context, APP_CLINIC_BUSINESS_RENEW, TranslateName, creater);
            }
            else
            {
                APP_CLINIC_BUSINESS_RENEW.ApplicationSysName = ApplicationSysName.APP_CLINIC_BUSINESS_RENEW;
                APP_CLINIC_BUSINESS_RENEW.AppsHookClassName = "BizPortal.AppsHook.HSSClinicBusinessRenewAppHook";
                APP_CLINIC_BUSINESS_RENEW.OrgCode = "19007000";
                APP_CLINIC_BUSINESS_RENEW.LogoFileID = null;
                APP_CLINIC_BUSINESS_RENEW.MultipleRequestSupport = true;
                APP_CLINIC_BUSINESS_RENEW.HandbookUrl = "https://info.go.th/#!/th/search/76396/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%95%E0%B9%88%E0%B8%AD%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B9%89%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81%20(N)/";
                APP_CLINIC_BUSINESS_RENEW.CitizenHandbookUrl = "https://info.go.th/#!/th/search/76396/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%95%E0%B9%88%E0%B8%AD%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B9%89%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B8%81%E0%B8%AD%E0%B8%9A%E0%B8%81%E0%B8%B4%E0%B8%88%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81%20(N)/";

                APP_CLINIC_BUSINESS_RENEW.OperatingDays = 21;
                APP_CLINIC_BUSINESS_RENEW.OperatingDayType = CostType.Fixed;
                APP_CLINIC_BUSINESS_RENEW.OperatingDays2 = null;
                APP_CLINIC_BUSINESS_RENEW.CitizenOperatingDays = 21;
                APP_CLINIC_BUSINESS_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_BUSINESS_RENEW.CitizenOperatingDays2 = null;

                APP_CLINIC_BUSINESS_RENEW.OperatingCost = 1000;
                APP_CLINIC_BUSINESS_RENEW.OperatingCostType = CostType.Fixed;
                APP_CLINIC_BUSINESS_RENEW.OperatingCost2 = null;
                APP_CLINIC_BUSINESS_RENEW.CitizenOperatingCost = 1000;
                APP_CLINIC_BUSINESS_RENEW.CitizenOperatingCostType = CostType.Fixed;
                APP_CLINIC_BUSINESS_RENEW.CitizenOperatingCost2 = null;

                APP_CLINIC_BUSINESS_RENEW.ShowRemark = true;
                APP_CLINIC_BUSINESS_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_CLINIC_BUSINESS_RENEW.CitizenShowRemark = true;
                APP_CLINIC_BUSINESS_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_CLINIC_BUSINESS_RENEW.SingleFormEnabled = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)";
                updateApplication(context, APP_CLINIC_BUSINESS_RENEW, TranslateName, creater);
            }
            #endregion

            #region ขอต่ออายุใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)
            Application APP_CLINIC_OPERATION_RENEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_CLINIC_OPERATION_RENEW).FirstOrDefault();
            if (APP_CLINIC_OPERATION_RENEW == null)
            {
                APP_CLINIC_OPERATION_RENEW = new Application();
                APP_CLINIC_OPERATION_RENEW.ApplicationSysName = ApplicationSysName.APP_CLINIC_OPERATION_RENEW;
                APP_CLINIC_OPERATION_RENEW.AppsHookClassName = "BizPortal.AppsHook.HSSClinicOperationRenewAppHook";
                APP_CLINIC_OPERATION_RENEW.OrgCode = "19007000";
                APP_CLINIC_OPERATION_RENEW.LogoFileID = null;
                APP_CLINIC_OPERATION_RENEW.MultipleRequestSupport = true;
                APP_CLINIC_OPERATION_RENEW.HandbookUrl = "https://info.go.th/#!/th/search/76339/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_OPERATION_RENEW.CitizenHandbookUrl = "https://info.go.th/#!/th/search/76339/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";

                APP_CLINIC_OPERATION_RENEW.OperatingDays = 21;
                APP_CLINIC_OPERATION_RENEW.OperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION_RENEW.OperatingDays2 = null;
                APP_CLINIC_OPERATION_RENEW.CitizenOperatingDays = 21;
                APP_CLINIC_OPERATION_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION_RENEW.CitizenOperatingDays2 = null;

                APP_CLINIC_OPERATION_RENEW.OperatingCost = 250;
                APP_CLINIC_OPERATION_RENEW.OperatingCostType = CostType.Fixed;
                APP_CLINIC_OPERATION_RENEW.OperatingCost2 = null;
                APP_CLINIC_OPERATION_RENEW.CitizenOperatingCost = 250;
                APP_CLINIC_OPERATION_RENEW.CitizenOperatingCostType = CostType.Fixed;
                APP_CLINIC_OPERATION_RENEW.CitizenOperatingCost2 = null;

                APP_CLINIC_OPERATION_RENEW.ShowRemark = true;
                APP_CLINIC_OPERATION_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</ span>";
                APP_CLINIC_OPERATION_RENEW.CitizenShowRemark = true;
                APP_CLINIC_OPERATION_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span>";
                APP_CLINIC_OPERATION_RENEW.SingleFormEnabled = true;
                APP_CLINIC_OPERATION_RENEW.RequestAtOrg = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)";
                createApplication(context, APP_CLINIC_OPERATION_RENEW, TranslateName, creater);
            }
            else
            {
                APP_CLINIC_OPERATION_RENEW.ApplicationSysName = ApplicationSysName.APP_CLINIC_OPERATION_RENEW;
                APP_CLINIC_OPERATION_RENEW.AppsHookClassName = "BizPortal.AppsHook.HSSClinicOperationRenewAppHook";
                APP_CLINIC_OPERATION_RENEW.OrgCode = "19007000";
                APP_CLINIC_OPERATION_RENEW.LogoFileID = null;
                APP_CLINIC_OPERATION_RENEW.MultipleRequestSupport = true;
                APP_CLINIC_OPERATION_RENEW.HandbookUrl = "https://info.go.th/#!/th/search/76339/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_OPERATION_RENEW.CitizenHandbookUrl = "https://info.go.th/#!/th/search/76339/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";

                APP_CLINIC_OPERATION_RENEW.OperatingDays = 21;
                APP_CLINIC_OPERATION_RENEW.OperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION_RENEW.OperatingDays2 = null;
                APP_CLINIC_OPERATION_RENEW.CitizenOperatingDays = 21;
                APP_CLINIC_OPERATION_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION_RENEW.CitizenOperatingDays2 = null;

                APP_CLINIC_OPERATION_RENEW.OperatingCost = 250;
                APP_CLINIC_OPERATION_RENEW.OperatingCostType = CostType.Fixed;
                APP_CLINIC_OPERATION_RENEW.OperatingCost2 = null;
                APP_CLINIC_OPERATION_RENEW.CitizenOperatingCost = 250;
                APP_CLINIC_OPERATION_RENEW.CitizenOperatingCostType = CostType.Fixed;
                APP_CLINIC_OPERATION_RENEW.CitizenOperatingCost2 = null;

                APP_CLINIC_OPERATION_RENEW.ShowRemark = true;
                APP_CLINIC_OPERATION_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</ span>";
                APP_CLINIC_OPERATION_RENEW.CitizenShowRemark = true;
                APP_CLINIC_OPERATION_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span>";
                APP_CLINIC_OPERATION_RENEW.SingleFormEnabled = true;
                APP_CLINIC_OPERATION_RENEW.RequestAtOrg = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)";
                updateApplication(context, APP_CLINIC_OPERATION_RENEW, TranslateName, creater);
            }
            #endregion

            #region ใบอนุญาตให้ประกอบกิจการสถานพยาบาล (ประเภทที่รับผู้ป่วยค้างคืน)
            Application APP_HOSPITAL_BUSINESS_RENEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HOSPITAL_BUSINESS_RENEW).FirstOrDefault();
            if (APP_HOSPITAL_BUSINESS_RENEW == null)
            {
                APP_HOSPITAL_BUSINESS_RENEW = new Application();
                APP_HOSPITAL_BUSINESS_RENEW.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_BUSINESS_RENEW;
                APP_HOSPITAL_BUSINESS_RENEW.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalBusinessRenewAppHook";
                APP_HOSPITAL_BUSINESS_RENEW.OrgCode = "19007000";
                APP_HOSPITAL_BUSINESS_RENEW.LogoFileID = null;
                APP_HOSPITAL_BUSINESS_RENEW.MultipleRequestSupport = true;
                APP_HOSPITAL_BUSINESS_RENEW.HandbookUrl = "https://www.info.go.th/#!/th/search/75193/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_BUSINESS_RENEW.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/75193/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_HOSPITAL_BUSINESS_RENEW.OperatingDays = 91;
                APP_HOSPITAL_BUSINESS_RENEW.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_BUSINESS_RENEW.OperatingDays2 = null;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenOperatingDays = 91;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenOperatingDays2 = null;

                APP_HOSPITAL_BUSINESS_RENEW.OperatingCost = 2000;
                APP_HOSPITAL_BUSINESS_RENEW.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_BUSINESS_RENEW.OperatingCost2 = null;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenOperatingCost = 2000;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenOperatingCost2 = null;

                APP_HOSPITAL_BUSINESS_RENEW.ShowRemark = true;
                APP_HOSPITAL_BUSINESS_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HOSPITAL_BUSINESS_RENEW.CitizenShowRemark = true;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HOSPITAL_BUSINESS_RENEW.SingleFormEnabled = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาล (โรงพยาบาล)";
                createApplication(context, APP_HOSPITAL_BUSINESS_RENEW, TranslateName, creater);
            }
            else
            {
                APP_HOSPITAL_BUSINESS_RENEW.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_BUSINESS_RENEW;
                APP_HOSPITAL_BUSINESS_RENEW.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalBusinessRenewAppHook";
                APP_HOSPITAL_BUSINESS_RENEW.OrgCode = "19007000";
                APP_HOSPITAL_BUSINESS_RENEW.LogoFileID = null;
                APP_HOSPITAL_BUSINESS_RENEW.MultipleRequestSupport = true;
                APP_HOSPITAL_BUSINESS_RENEW.HandbookUrl = "https://www.info.go.th/#!/th/search/75193/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_BUSINESS_RENEW.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/75193/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_HOSPITAL_BUSINESS_RENEW.OperatingDays = 91;
                APP_HOSPITAL_BUSINESS_RENEW.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_BUSINESS_RENEW.OperatingDays2 = null;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenOperatingDays = 91;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenOperatingDays2 = null;

                APP_HOSPITAL_BUSINESS_RENEW.OperatingCost = 2000;
                APP_HOSPITAL_BUSINESS_RENEW.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_BUSINESS_RENEW.OperatingCost2 = null;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenOperatingCost = 2000;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenOperatingCost2 = null;

                APP_HOSPITAL_BUSINESS_RENEW.ShowRemark = true;
                APP_HOSPITAL_BUSINESS_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HOSPITAL_BUSINESS_RENEW.CitizenShowRemark = true;
                APP_HOSPITAL_BUSINESS_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span>";
                APP_HOSPITAL_BUSINESS_RENEW.SingleFormEnabled = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาล (โรงพยาบาล)";
                updateApplication(context, APP_HOSPITAL_BUSINESS_RENEW, TranslateName, creater);
            }
            #endregion

            #region ใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่รับผู้ป่วยค้างคืน)
            Application APP_HOSPITAL_OPERATION_RENEW = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HOSPITAL_OPERATION_RENEW).FirstOrDefault();
            if (APP_HOSPITAL_OPERATION_RENEW == null)
            {
                APP_HOSPITAL_OPERATION_RENEW = new Application();
                APP_HOSPITAL_OPERATION_RENEW.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_OPERATION_RENEW;
                APP_HOSPITAL_OPERATION_RENEW.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalOperationRenewAppHook";
                APP_HOSPITAL_OPERATION_RENEW.OrgCode = "19007000";
                APP_HOSPITAL_OPERATION_RENEW.LogoFileID = null;
                APP_HOSPITAL_OPERATION_RENEW.MultipleRequestSupport = true;
                APP_HOSPITAL_OPERATION_RENEW.HandbookUrl = "https://www.info.go.th/#!/th/search/75239/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%95%E0%B9%88%E0%B8%AD%E0%B8%AD%E0%B8%B2%E0%B8%A2%E0%B8%B8%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B9%89%E0%B8%94%E0%B8%B3%E0%B9%80%E0%B8%99%E0%B8%B4%E0%B8%99%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5%20(%E0%B8%81%E0%B8%A3%E0%B8%93%E0%B8%B5%E0%B8%AD%E0%B8%AD%E0%B8%81%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B8%A1%E0%B9%88)%20(N)/";
                APP_HOSPITAL_OPERATION_RENEW.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/75239/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%95%E0%B9%88%E0%B8%AD%E0%B8%AD%E0%B8%B2%E0%B8%A2%E0%B8%B8%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B9%89%E0%B8%94%E0%B8%B3%E0%B9%80%E0%B8%99%E0%B8%B4%E0%B8%99%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5%20(%E0%B8%81%E0%B8%A3%E0%B8%93%E0%B8%B5%E0%B8%AD%E0%B8%AD%E0%B8%81%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B8%A1%E0%B9%88)%20(N)/";

                APP_HOSPITAL_OPERATION_RENEW.OperatingDays = 21;
                APP_HOSPITAL_OPERATION_RENEW.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION_RENEW.OperatingDays2 = null;
                APP_HOSPITAL_OPERATION_RENEW.CitizenOperatingDays = 21;
                APP_HOSPITAL_OPERATION_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION_RENEW.CitizenOperatingDays2 = null;

                APP_HOSPITAL_OPERATION_RENEW.OperatingCost = 500;
                APP_HOSPITAL_OPERATION_RENEW.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION_RENEW.OperatingCost2 = null;
                APP_HOSPITAL_OPERATION_RENEW.CitizenOperatingCost = 500;
                APP_HOSPITAL_OPERATION_RENEW.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION_RENEW.CitizenOperatingCost2 = null;

                APP_HOSPITAL_OPERATION_RENEW.ShowRemark = true;
                APP_HOSPITAL_OPERATION_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</ span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_OPERATION_RENEW.CitizenShowRemark = true;
                APP_HOSPITAL_OPERATION_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_OPERATION_RENEW.SingleFormEnabled = true;
                APP_HOSPITAL_OPERATION_RENEW.RequestAtOrg = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)";
                createApplication(context, APP_HOSPITAL_OPERATION_RENEW, TranslateName, creater);
            }
            else
            {
                APP_HOSPITAL_OPERATION_RENEW.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_OPERATION_RENEW;
                APP_HOSPITAL_OPERATION_RENEW.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalOperationRenewAppHook";
                APP_HOSPITAL_OPERATION_RENEW.OrgCode = "19007000";
                APP_HOSPITAL_OPERATION_RENEW.LogoFileID = null;
                APP_HOSPITAL_OPERATION_RENEW.MultipleRequestSupport = true;
                APP_HOSPITAL_OPERATION_RENEW.HandbookUrl = "https://www.info.go.th/#!/th/search/75239/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%95%E0%B9%88%E0%B8%AD%E0%B8%AD%E0%B8%B2%E0%B8%A2%E0%B8%B8%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B9%89%E0%B8%94%E0%B8%B3%E0%B9%80%E0%B8%99%E0%B8%B4%E0%B8%99%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5%20(%E0%B8%81%E0%B8%A3%E0%B8%93%E0%B8%B5%E0%B8%AD%E0%B8%AD%E0%B8%81%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B8%A1%E0%B9%88)%20(N)/";
                APP_HOSPITAL_OPERATION_RENEW.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/75239/%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%95%E0%B9%88%E0%B8%AD%E0%B8%AD%E0%B8%B2%E0%B8%A2%E0%B8%B8%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B9%89%E0%B8%94%E0%B8%B3%E0%B9%80%E0%B8%99%E0%B8%B4%E0%B8%99%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5%20(%E0%B8%81%E0%B8%A3%E0%B8%93%E0%B8%B5%E0%B8%AD%E0%B8%AD%E0%B8%81%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95%E0%B9%83%E0%B8%AB%E0%B8%A1%E0%B9%88)%20(N)/";

                APP_HOSPITAL_OPERATION_RENEW.OperatingDays = 21;
                APP_HOSPITAL_OPERATION_RENEW.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION_RENEW.OperatingDays2 = null;
                APP_HOSPITAL_OPERATION_RENEW.CitizenOperatingDays = 21;
                APP_HOSPITAL_OPERATION_RENEW.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION_RENEW.CitizenOperatingDays2 = null;

                APP_HOSPITAL_OPERATION_RENEW.OperatingCost = 500;
                APP_HOSPITAL_OPERATION_RENEW.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION_RENEW.OperatingCost2 = null;
                APP_HOSPITAL_OPERATION_RENEW.CitizenOperatingCost = 500;
                APP_HOSPITAL_OPERATION_RENEW.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION_RENEW.CitizenOperatingCost2 = null;

                APP_HOSPITAL_OPERATION_RENEW.ShowRemark = true;
                APP_HOSPITAL_OPERATION_RENEW.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</ span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_OPERATION_RENEW.CitizenShowRemark = true;
                APP_HOSPITAL_OPERATION_RENEW.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_OPERATION_RENEW.SingleFormEnabled = true;
                APP_HOSPITAL_OPERATION_RENEW.RequestAtOrg = true;
                string TranslateName = "ขอต่ออายุใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)";
                updateApplication(context, APP_HOSPITAL_OPERATION_RENEW, TranslateName, creater);
            }
            #endregion

            #region ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาลและใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน)
            Application APP_CLINIC_BUSINESS_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_CLINIC_BUSINESS_EDIT).FirstOrDefault();
            if (APP_CLINIC_BUSINESS_EDIT == null)
            {
                APP_CLINIC_BUSINESS_EDIT = new Application();
                APP_CLINIC_BUSINESS_EDIT.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_BUSINESS_EDIT;
                APP_CLINIC_BUSINESS_EDIT.AppsHookClassName = "BizPortal.AppsHook.HSSClinicBusinessEditAppHook";
                APP_CLINIC_BUSINESS_EDIT.OrgCode = "19007000";
                APP_CLINIC_BUSINESS_EDIT.LogoFileID = null;
                APP_CLINIC_BUSINESS_EDIT.MultipleRequestSupport = true;
                APP_CLINIC_BUSINESS_EDIT.HandbookUrl = "https://info.go.th/#!/th/search/76392/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_BUSINESS_EDIT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/76392/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";

                APP_CLINIC_BUSINESS_EDIT.OperatingDays = 21;
                APP_CLINIC_BUSINESS_EDIT.OperatingDayType = CostType.Fixed;
                APP_CLINIC_BUSINESS_EDIT.OperatingDays2 = null;
                APP_CLINIC_BUSINESS_EDIT.CitizenOperatingDays = 21;
                APP_CLINIC_BUSINESS_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_BUSINESS_EDIT.CitizenOperatingDays2 = null;

                APP_CLINIC_BUSINESS_EDIT.OperatingCost = 100;
                APP_CLINIC_BUSINESS_EDIT.OperatingCostType = CostType.StartAt;
                APP_CLINIC_BUSINESS_EDIT.OperatingCost2 = null;
                APP_CLINIC_BUSINESS_EDIT.CitizenOperatingCost = 100;
                APP_CLINIC_BUSINESS_EDIT.CitizenOperatingCostType = CostType.StartAt;
                APP_CLINIC_BUSINESS_EDIT.CitizenOperatingCost2 = null;

                APP_CLINIC_BUSINESS_EDIT.ShowRemark = true;
                APP_CLINIC_BUSINESS_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_CLINIC_BUSINESS_EDIT.CitizenShowRemark = true;
                APP_CLINIC_BUSINESS_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_CLINIC_BUSINESS_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)";
                createApplication(context, APP_CLINIC_BUSINESS_EDIT, TranslateName, creater);
            }
            else
            {
                APP_CLINIC_BUSINESS_EDIT.ApplicationSysName = ApplicationSysName.APP_CLINIC_BUSINESS_EDIT;
                APP_CLINIC_BUSINESS_EDIT.AppsHookClassName = "BizPortal.AppsHook.HSSClinicBusinessEditAppHook";
                APP_CLINIC_BUSINESS_EDIT.OrgCode = "19007000";
                APP_CLINIC_BUSINESS_EDIT.LogoFileID = null;
                APP_CLINIC_BUSINESS_EDIT.MultipleRequestSupport = true;
                APP_CLINIC_BUSINESS_EDIT.HandbookUrl = "https://info.go.th/#!/th/search/76392/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_BUSINESS_EDIT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/76392/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";

                APP_CLINIC_BUSINESS_EDIT.OperatingDays = 21;
                APP_CLINIC_BUSINESS_EDIT.OperatingDayType = CostType.Fixed;
                APP_CLINIC_BUSINESS_EDIT.OperatingDays2 = null;
                APP_CLINIC_BUSINESS_EDIT.CitizenOperatingDays = 21;
                APP_CLINIC_BUSINESS_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_BUSINESS_EDIT.CitizenOperatingDays2 = null;

                APP_CLINIC_BUSINESS_EDIT.OperatingCost = 100;
                APP_CLINIC_BUSINESS_EDIT.OperatingCostType = CostType.StartAt;
                APP_CLINIC_BUSINESS_EDIT.OperatingCost2 = null;
                APP_CLINIC_BUSINESS_EDIT.CitizenOperatingCost = 100;
                APP_CLINIC_BUSINESS_EDIT.CitizenOperatingCostType = CostType.StartAt;
                APP_CLINIC_BUSINESS_EDIT.CitizenOperatingCost2 = null;

                APP_CLINIC_BUSINESS_EDIT.ShowRemark = true;
                APP_CLINIC_BUSINESS_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_CLINIC_BUSINESS_EDIT.CitizenShowRemark = true;
                APP_CLINIC_BUSINESS_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_CLINIC_BUSINESS_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)";
                updateApplication(context, APP_CLINIC_BUSINESS_EDIT, TranslateName, creater);
            }
            #endregion

            #region ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาลประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน
            Application APP_CLINIC_OPERATION_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_CLINIC_OPERATION_EDIT).FirstOrDefault();
            if (APP_CLINIC_OPERATION_EDIT == null)
            {
                APP_CLINIC_OPERATION_EDIT = new Application();
                APP_CLINIC_OPERATION_EDIT.ApplicationSysName = ApplicationSysName.APP_CLINIC_OPERATION_EDIT;
                APP_CLINIC_OPERATION_EDIT.AppsHookClassName = "BizPortal.AppsHook.HSSClinicChangeOperatorApphook";
                APP_CLINIC_OPERATION_EDIT.OrgCode = "19007000";
                APP_CLINIC_OPERATION_EDIT.LogoFileID = null;
                APP_CLINIC_OPERATION_EDIT.MultipleRequestSupport = true;
                APP_CLINIC_OPERATION_EDIT.HandbookUrl = "https://info.go.th/#!/th/search/76440/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_OPERATION_EDIT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/76440/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";

                APP_CLINIC_OPERATION_EDIT.OperatingDays = 21;
                APP_CLINIC_OPERATION_EDIT.OperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION_EDIT.OperatingDays2 = null;
                APP_CLINIC_OPERATION_EDIT.CitizenOperatingDays = 21;
                APP_CLINIC_OPERATION_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION_EDIT.CitizenOperatingDays2 = null;

                APP_CLINIC_OPERATION_EDIT.OperatingCost = 250;
                APP_CLINIC_OPERATION_EDIT.OperatingCostType = CostType.StartAt;
                APP_CLINIC_OPERATION_EDIT.OperatingCost2 = null;
                APP_CLINIC_OPERATION_EDIT.CitizenOperatingCost = 250;
                APP_CLINIC_OPERATION_EDIT.CitizenOperatingCostType = CostType.StartAt;
                APP_CLINIC_OPERATION_EDIT.CitizenOperatingCost2 = null;

                APP_CLINIC_OPERATION_EDIT.ShowRemark = true;
                APP_CLINIC_OPERATION_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span>";
                APP_CLINIC_OPERATION_EDIT.CitizenShowRemark = true;
                APP_CLINIC_OPERATION_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span>";
                APP_CLINIC_OPERATION_EDIT.SingleFormEnabled = true;
                APP_CLINIC_OPERATION_EDIT.RequestAtOrg = true;
                string TranslateName = "ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (คลินิก)";
                createApplication(context, APP_CLINIC_OPERATION_EDIT, TranslateName, creater);
            }
            else
            {
                APP_CLINIC_OPERATION_EDIT.ApplicationSysName = ApplicationSysName.APP_CLINIC_OPERATION_EDIT;
                APP_CLINIC_OPERATION_EDIT.AppsHookClassName = "BizPortal.AppsHook.HSSClinicChangeOperatorApphook";
                APP_CLINIC_OPERATION_EDIT.OrgCode = "19007000";
                APP_CLINIC_OPERATION_EDIT.LogoFileID = null;
                APP_CLINIC_OPERATION_EDIT.MultipleRequestSupport = true;
                APP_CLINIC_OPERATION_EDIT.HandbookUrl = "https://info.go.th/#!/th/search/76440/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_OPERATION_EDIT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/76440/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";

                APP_CLINIC_OPERATION_EDIT.OperatingDays = 21;
                APP_CLINIC_OPERATION_EDIT.OperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION_EDIT.OperatingDays2 = null;
                APP_CLINIC_OPERATION_EDIT.CitizenOperatingDays = 21;
                APP_CLINIC_OPERATION_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION_EDIT.CitizenOperatingDays2 = null;

                APP_CLINIC_OPERATION_EDIT.OperatingCost = 250;
                APP_CLINIC_OPERATION_EDIT.OperatingCostType = CostType.StartAt;
                APP_CLINIC_OPERATION_EDIT.OperatingCost2 = null;
                APP_CLINIC_OPERATION_EDIT.CitizenOperatingCost = 250;
                APP_CLINIC_OPERATION_EDIT.CitizenOperatingCostType = CostType.StartAt;
                APP_CLINIC_OPERATION_EDIT.CitizenOperatingCost2 = null;

                APP_CLINIC_OPERATION_EDIT.ShowRemark = true;
                APP_CLINIC_OPERATION_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span>";
                APP_CLINIC_OPERATION_EDIT.CitizenShowRemark = true;
                APP_CLINIC_OPERATION_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span>";
                APP_CLINIC_OPERATION_EDIT.SingleFormEnabled = true;
                APP_CLINIC_OPERATION_EDIT.RequestAtOrg = true;
                string TranslateName = "ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (คลินิก)";
                updateApplication(context, APP_CLINIC_OPERATION_EDIT, TranslateName, creater);
            }
            #endregion

            #region ขอแก้ไขใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน) 
            Application APP_CLINIC_OPERATION_EDIT_B = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_CLINIC_OPERATION_EDIT_B).FirstOrDefault();
            if (APP_CLINIC_OPERATION_EDIT_B == null)
            {
                APP_CLINIC_OPERATION_EDIT_B = new Application();
                APP_CLINIC_OPERATION_EDIT_B.ApplicationSysName = ApplicationSysName.APP_CLINIC_OPERATION_EDIT_B;
                APP_CLINIC_OPERATION_EDIT_B.AppsHookClassName = "BizPortal.AppsHook.HSSClinicOperationEditAppHook";
                APP_CLINIC_OPERATION_EDIT_B.OrgCode = "19007000";
                APP_CLINIC_OPERATION_EDIT_B.LogoFileID = null;
                APP_CLINIC_OPERATION_EDIT_B.MultipleRequestSupport = true;
                APP_CLINIC_OPERATION_EDIT_B.HandbookUrl = "https://info.go.th/#!/th/search/76480/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_OPERATION_EDIT_B.CitizenHandbookUrl = "https://info.go.th/#!/th/search/76480/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";

                APP_CLINIC_OPERATION_EDIT_B.OperatingDays = 21;
                APP_CLINIC_OPERATION_EDIT_B.OperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION_EDIT_B.OperatingDays2 = null;
                APP_CLINIC_OPERATION_EDIT_B.CitizenOperatingDays = 21;
                APP_CLINIC_OPERATION_EDIT_B.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION_EDIT_B.CitizenOperatingDays2 = null;

                APP_CLINIC_OPERATION_EDIT_B.OperatingCost = 100;
                APP_CLINIC_OPERATION_EDIT_B.OperatingCostType = CostType.StartAt;
                APP_CLINIC_OPERATION_EDIT_B.OperatingCost2 = null;
                APP_CLINIC_OPERATION_EDIT_B.CitizenOperatingCost = 100;
                APP_CLINIC_OPERATION_EDIT_B.CitizenOperatingCostType = CostType.StartAt;
                APP_CLINIC_OPERATION_EDIT_B.CitizenOperatingCost2 = null;

                APP_CLINIC_OPERATION_EDIT_B.ShowRemark = true;
                APP_CLINIC_OPERATION_EDIT_B.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_CLINIC_OPERATION_EDIT_B.CitizenShowRemark = true;
                APP_CLINIC_OPERATION_EDIT_B.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_CLINIC_OPERATION_EDIT_B.SingleFormEnabled = true;
                APP_CLINIC_OPERATION_EDIT_B.RequestAtOrg = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)";
                createApplication(context, APP_CLINIC_OPERATION_EDIT_B, TranslateName, creater);
            }
            else
            {
                APP_CLINIC_OPERATION_EDIT_B.ApplicationSysName = ApplicationSysName.APP_CLINIC_OPERATION_EDIT_B;
                APP_CLINIC_OPERATION_EDIT_B.AppsHookClassName = "BizPortal.AppsHook.HSSClinicOperationEditAppHook";
                APP_CLINIC_OPERATION_EDIT_B.OrgCode = "19007000";
                APP_CLINIC_OPERATION_EDIT_B.LogoFileID = null;
                APP_CLINIC_OPERATION_EDIT_B.MultipleRequestSupport = true;
                APP_CLINIC_OPERATION_EDIT_B.HandbookUrl = "https://info.go.th/#!/th/search/76480/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";
                APP_CLINIC_OPERATION_EDIT_B.CitizenHandbookUrl = "https://info.go.th/#!/th/search/76480/%E0%B8%84%E0%B8%A5%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%81/";

                APP_CLINIC_OPERATION_EDIT_B.OperatingDays = 21;
                APP_CLINIC_OPERATION_EDIT_B.OperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION_EDIT_B.OperatingDays2 = null;
                APP_CLINIC_OPERATION_EDIT_B.CitizenOperatingDays = 21;
                APP_CLINIC_OPERATION_EDIT_B.CitizenOperatingDayType = CostType.Fixed;
                APP_CLINIC_OPERATION_EDIT_B.CitizenOperatingDays2 = null;

                APP_CLINIC_OPERATION_EDIT_B.OperatingCost = 100;
                APP_CLINIC_OPERATION_EDIT_B.OperatingCostType = CostType.StartAt;
                APP_CLINIC_OPERATION_EDIT_B.OperatingCost2 = null;
                APP_CLINIC_OPERATION_EDIT_B.CitizenOperatingCost = 100;
                APP_CLINIC_OPERATION_EDIT_B.CitizenOperatingCostType = CostType.StartAt;
                APP_CLINIC_OPERATION_EDIT_B.CitizenOperatingCost2 = null;

                APP_CLINIC_OPERATION_EDIT_B.ShowRemark = true;
                APP_CLINIC_OPERATION_EDIT_B.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_CLINIC_OPERATION_EDIT_B.CitizenShowRemark = true;
                APP_CLINIC_OPERATION_EDIT_B.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_CLINIC_OPERATION_EDIT_B.SingleFormEnabled = true;
                APP_CLINIC_OPERATION_EDIT_B.RequestAtOrg = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)";
                updateApplication(context, APP_CLINIC_OPERATION_EDIT_B, TranslateName, creater);
            }
            #endregion

            #region ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)
            Application APP_HOSPITAL_BUSINESS_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HOSPITAL_BUSINESS_EDIT).FirstOrDefault();
            if (APP_HOSPITAL_BUSINESS_EDIT == null)
            {
                APP_HOSPITAL_BUSINESS_EDIT = new Application();
                APP_HOSPITAL_BUSINESS_EDIT.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_BUSINESS_EDIT;
                APP_HOSPITAL_BUSINESS_EDIT.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalBusinessEditAppHook";
                APP_HOSPITAL_BUSINESS_EDIT.OrgCode = "19007000";
                APP_HOSPITAL_BUSINESS_EDIT.LogoFileID = null;
                APP_HOSPITAL_BUSINESS_EDIT.MultipleRequestSupport = true;
                APP_HOSPITAL_BUSINESS_EDIT.HandbookUrl = "https://www.info.go.th/#!/th/search/76347/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_BUSINESS_EDIT.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/76347/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_HOSPITAL_BUSINESS_EDIT.OperatingDays = 21;
                APP_HOSPITAL_BUSINESS_EDIT.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_BUSINESS_EDIT.OperatingDays2 = null;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenOperatingDays = 21;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenOperatingDays2 = null;

                APP_HOSPITAL_BUSINESS_EDIT.OperatingCost = 100;
                APP_HOSPITAL_BUSINESS_EDIT.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_BUSINESS_EDIT.OperatingCost2 = null;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenOperatingCost = 100;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenOperatingCost2 = null;

                APP_HOSPITAL_BUSINESS_EDIT.ShowRemark = true;
                APP_HOSPITAL_BUSINESS_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_HOSPITAL_BUSINESS_EDIT.CitizenShowRemark = true;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_HOSPITAL_BUSINESS_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาล (โรงพยาบาล)";
                createApplication(context, APP_HOSPITAL_BUSINESS_EDIT, TranslateName, creater);
            }
            else
            {
                APP_HOSPITAL_BUSINESS_EDIT.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_BUSINESS_EDIT;
                APP_HOSPITAL_BUSINESS_EDIT.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalBusinessEditAppHook";
                APP_HOSPITAL_BUSINESS_EDIT.OrgCode = "19007000";
                APP_HOSPITAL_BUSINESS_EDIT.LogoFileID = null;
                APP_HOSPITAL_BUSINESS_EDIT.MultipleRequestSupport = true;
                APP_HOSPITAL_BUSINESS_EDIT.HandbookUrl = "https://www.info.go.th/#!/th/search/76347/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_BUSINESS_EDIT.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/76347/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_HOSPITAL_BUSINESS_EDIT.OperatingDays = 21;
                APP_HOSPITAL_BUSINESS_EDIT.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_BUSINESS_EDIT.OperatingDays2 = null;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenOperatingDays = 21;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenOperatingDays2 = null;

                APP_HOSPITAL_BUSINESS_EDIT.OperatingCost = 100;
                APP_HOSPITAL_BUSINESS_EDIT.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_BUSINESS_EDIT.OperatingCost2 = null;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenOperatingCost = 100;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenOperatingCost2 = null;

                APP_HOSPITAL_BUSINESS_EDIT.ShowRemark = true;
                APP_HOSPITAL_BUSINESS_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_HOSPITAL_BUSINESS_EDIT.CitizenShowRemark = true;
                APP_HOSPITAL_BUSINESS_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_HOSPITAL_BUSINESS_EDIT.SingleFormEnabled = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาล (โรงพยาบาล)";
                updateApplication(context, APP_HOSPITAL_BUSINESS_EDIT, TranslateName, creater);
            }
            #endregion

            #region ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)

            Application APP_HOSPITAL_OPERATION_EDIT = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HOSPITAL_OPERATION_EDIT).FirstOrDefault();
            if (APP_HOSPITAL_OPERATION_EDIT == null)
            {
                APP_HOSPITAL_OPERATION_EDIT = new Application();
                APP_HOSPITAL_OPERATION_EDIT.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_OPERATION_EDIT;
                APP_HOSPITAL_OPERATION_EDIT.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalChangeOperatorApphook";
                APP_HOSPITAL_OPERATION_EDIT.OrgCode = "19007000";
                APP_HOSPITAL_OPERATION_EDIT.LogoFileID = null;
                APP_HOSPITAL_OPERATION_EDIT.MultipleRequestSupport = true;
                APP_HOSPITAL_OPERATION_EDIT.HandbookUrl = "https://info.go.th/#!/th/search/76352/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_OPERATION_EDIT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/76352/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_HOSPITAL_OPERATION_EDIT.OperatingDays = 21;
                APP_HOSPITAL_OPERATION_EDIT.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION_EDIT.OperatingDays2 = null;
                APP_HOSPITAL_OPERATION_EDIT.CitizenOperatingDays = 21;
                APP_HOSPITAL_OPERATION_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION_EDIT.CitizenOperatingDays2 = null;

                APP_HOSPITAL_OPERATION_EDIT.OperatingCost = 500;
                APP_HOSPITAL_OPERATION_EDIT.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION_EDIT.OperatingCost2 = null;
                APP_HOSPITAL_OPERATION_EDIT.CitizenOperatingCost = 500;
                APP_HOSPITAL_OPERATION_EDIT.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION_EDIT.CitizenOperatingCost2 = null;

                APP_HOSPITAL_OPERATION_EDIT.ShowRemark = true;
                APP_HOSPITAL_OPERATION_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_OPERATION_EDIT.CitizenShowRemark = true;
                APP_HOSPITAL_OPERATION_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_OPERATION_EDIT.SingleFormEnabled = true;
                APP_HOSPITAL_OPERATION_EDIT.RequestAtOrg = true;
                string TranslateName = "ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (โรงพยาบาล)";
                createApplication(context, APP_HOSPITAL_OPERATION_EDIT, TranslateName, creater);
            }
            else
            {
                APP_HOSPITAL_OPERATION_EDIT.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_OPERATION_EDIT;
                APP_HOSPITAL_OPERATION_EDIT.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalChangeOperatorApphook";
                APP_HOSPITAL_OPERATION_EDIT.OrgCode = "19007000";
                APP_HOSPITAL_OPERATION_EDIT.LogoFileID = null;
                APP_HOSPITAL_OPERATION_EDIT.MultipleRequestSupport = true;
                APP_HOSPITAL_OPERATION_EDIT.HandbookUrl = "https://info.go.th/#!/th/search/76352/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_OPERATION_EDIT.CitizenHandbookUrl = "https://info.go.th/#!/th/search/76352/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_HOSPITAL_OPERATION_EDIT.OperatingDays = 21;
                APP_HOSPITAL_OPERATION_EDIT.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION_EDIT.OperatingDays2 = null;
                APP_HOSPITAL_OPERATION_EDIT.CitizenOperatingDays = 21;
                APP_HOSPITAL_OPERATION_EDIT.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION_EDIT.CitizenOperatingDays2 = null;

                APP_HOSPITAL_OPERATION_EDIT.OperatingCost = 500;
                APP_HOSPITAL_OPERATION_EDIT.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION_EDIT.OperatingCost2 = null;
                APP_HOSPITAL_OPERATION_EDIT.CitizenOperatingCost = 500;
                APP_HOSPITAL_OPERATION_EDIT.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION_EDIT.CitizenOperatingCost2 = null;

                APP_HOSPITAL_OPERATION_EDIT.ShowRemark = true;
                APP_HOSPITAL_OPERATION_EDIT.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_OPERATION_EDIT.CitizenShowRemark = true;
                APP_HOSPITAL_OPERATION_EDIT.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br /><span style=\"color: red; \">* สามารถยื่นคำขอได้เฉพาะบัญชีผู้ใช้งานประเภท \"ประชาชน\" เท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นจะแปรผันตามจำนวนเตียงและธุรกรรมที่ผู้ขออนุญาตยื่นดำเนินการ อ้างอิงจากกฎกระทรวง (พ.ศ. 2543) อัตราค่าธรรมเนียมออกตามความในพระราชบัญญัติสถานพยาบาล พ.ศ. 2541</span>";
                APP_HOSPITAL_OPERATION_EDIT.SingleFormEnabled = true;
                APP_HOSPITAL_OPERATION_EDIT.RequestAtOrg = true;
                string TranslateName = "ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (โรงพยาบาล)";
                updateApplication(context, APP_HOSPITAL_OPERATION_EDIT, TranslateName, creater);
            }

            #endregion

            #region ขอแก้ไขใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)

            Application APP_HOSPITAL_OPERATION_EDIT_B = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HOSPITAL_OPERATION_EDIT_B).FirstOrDefault();
            if (APP_HOSPITAL_OPERATION_EDIT_B == null)
            {
                APP_HOSPITAL_OPERATION_EDIT_B = new Application();
                APP_HOSPITAL_OPERATION_EDIT_B.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_OPERATION_EDIT_B;
                APP_HOSPITAL_OPERATION_EDIT_B.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalOperationEditAppHook";
                APP_HOSPITAL_OPERATION_EDIT_B.OrgCode = "19007000";
                APP_HOSPITAL_OPERATION_EDIT_B.LogoFileID = null;
                APP_HOSPITAL_OPERATION_EDIT_B.MultipleRequestSupport = true;
                APP_HOSPITAL_OPERATION_EDIT_B.HandbookUrl = "https://www.info.go.th/#!/th/search/76347/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/76347/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_HOSPITAL_OPERATION_EDIT_B.OperatingDays = 21;
                APP_HOSPITAL_OPERATION_EDIT_B.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION_EDIT_B.OperatingDays2 = null;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenOperatingDays = 21;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenOperatingDays2 = null;

                APP_HOSPITAL_OPERATION_EDIT_B.OperatingCost = 100;
                APP_HOSPITAL_OPERATION_EDIT_B.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION_EDIT_B.OperatingCost2 = null;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenOperatingCost = 100;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenOperatingCost2 = null;

                APP_HOSPITAL_OPERATION_EDIT_B.ShowRemark = true;
                APP_HOSPITAL_OPERATION_EDIT_B.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenShowRemark = true;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_HOSPITAL_OPERATION_EDIT_B.SingleFormEnabled = true;
                APP_HOSPITAL_OPERATION_EDIT_B.RequestAtOrg = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)";
                createApplication(context, APP_HOSPITAL_OPERATION_EDIT_B, TranslateName, creater);
            }
            else
            {
                APP_HOSPITAL_OPERATION_EDIT_B.ApplicationSysName = ApplicationSysName.APP_HOSPITAL_OPERATION_EDIT_B;
                APP_HOSPITAL_OPERATION_EDIT_B.AppsHookClassName = "BizPortal.AppsHook.HSSHospitalOperationEditAppHook";
                APP_HOSPITAL_OPERATION_EDIT_B.OrgCode = "19007000";
                APP_HOSPITAL_OPERATION_EDIT_B.LogoFileID = null;
                APP_HOSPITAL_OPERATION_EDIT_B.MultipleRequestSupport = true;
                APP_HOSPITAL_OPERATION_EDIT_B.HandbookUrl = "https://www.info.go.th/#!/th/search/76347/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenHandbookUrl = "https://www.info.go.th/#!/th/search/76347/%E0%B9%82%E0%B8%A3%E0%B8%87%E0%B8%9E%E0%B8%A2%E0%B8%B2%E0%B8%9A%E0%B8%B2%E0%B8%A5/";

                APP_HOSPITAL_OPERATION_EDIT_B.OperatingDays = 21;
                APP_HOSPITAL_OPERATION_EDIT_B.OperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION_EDIT_B.OperatingDays2 = null;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenOperatingDays = 21;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenOperatingDayType = CostType.Fixed;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenOperatingDays2 = null;

                APP_HOSPITAL_OPERATION_EDIT_B.OperatingCost = 100;
                APP_HOSPITAL_OPERATION_EDIT_B.OperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION_EDIT_B.OperatingCost2 = null;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenOperatingCost = 100;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenOperatingCostType = CostType.StartAt;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenOperatingCost2 = null;

                APP_HOSPITAL_OPERATION_EDIT_B.ShowRemark = true;
                APP_HOSPITAL_OPERATION_EDIT_B.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenShowRemark = true;
                APP_HOSPITAL_OPERATION_EDIT_B.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ค่าธรรมเนียมข้างต้นยังไม่รวมค่าใบแทนใบอนุญาต</span>";
                APP_HOSPITAL_OPERATION_EDIT_B.SingleFormEnabled = true;
                APP_HOSPITAL_OPERATION_EDIT_B.RequestAtOrg = true;
                string TranslateName = "ขอแก้ไขใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)";
                updateApplication(context, APP_HOSPITAL_OPERATION_EDIT_B, TranslateName, creater);
            }


            #endregion

            #region คำขอแก้ไขเปลี่ยนแปลงเอกสารและหลักฐานที่ได้รับจดทะเบียนประกอบธุรกิจขายตรง
            Application APP_OCPB_DIRECT_SELL_EDIT = context.Applications.Where(x => x.ApplicationSysName.Equals("คำขอแก้ไขเปลี่ยนแปลงเอกสารและหลักฐานที่ได้รับจดทะเบียนประกอบธุรกิจขายตรง")).FirstOrDefault();
            if (APP_OCPB_DIRECT_SELL_EDIT == null)
            {
                // Do nothing : this permit is already data in Table Application 
                // Data insert from Seed Phase 2
            }
            else
            {
                APP_OCPB_DIRECT_SELL_EDIT.AppsHookClassName = "BizPortal.AppsHook.OCPBDirectSellEditAppHook";
                updateApplication(context, APP_OCPB_DIRECT_SELL_EDIT, "คำขอแก้ไขเปลี่ยนแปลงเอกสารและหลักฐานที่ได้รับจดทะเบียนประกอบธุรกิจขายตรง", creater);
            }
            #endregion

            #region คำขอแก้ไขเปลี่ยนแปลงเอกสารและหลักฐานที่ได้รับจดทะเบียนประกอบธุรกิจตลาดแบบตรง
            Application APP_OCPB_DIRECT_MARKETING_EDIT = context.Applications.Where(x => x.ApplicationSysName.Equals("คำขอแก้ไขเปลี่ยนแปลงเอกสารและหลักฐานที่ได้รับจดทะเบียนประกอบธุรกิจตลาดแบบตรง")).FirstOrDefault();
            if (APP_OCPB_DIRECT_MARKETING_EDIT == null)
            {
                // Do nothing : this permit is already data in Table Application 
                // Data insert from Seed Phase 2
            }
            else
            {
                APP_OCPB_DIRECT_MARKETING_EDIT.AppsHookClassName = "BizPortal.AppsHook.OCPBDirectMarketingEditAppHook";
                updateApplication(context, APP_OCPB_DIRECT_MARKETING_EDIT, "คำขอแก้ไขเปลี่ยนแปลงเอกสารและหลักฐานที่ได้รับจดทะเบียนประกอบธุรกิจตลาดแบบตรง", creater);
            }
            #endregion

            #region แก้ไขคำขอต่ออายุใบอนุญาตดำเนินการสถานพยาบาลสัตว์
            Application APP_DLD_ANIMAL_LICENSE_RENEW = context.Applications.Where(x => x.ApplicationSysName.Equals("แก้ไขคำขอต่ออายุใบอนุญาตดำเนินการสถานพยาบาลสัตว์")).FirstOrDefault();
            if (APP_DLD_ANIMAL_LICENSE_RENEW == null)
            {
                // Do nothing : this permit is already data in Table Application 
                // Data insert from Seed Phase 2
            }
            else
            {
                APP_DLD_ANIMAL_LICENSE_RENEW.AppsHookClassName = "BizPortal.AppsHook.DLDAnimallicenseEditApphook";
                updateApplication(context, APP_DLD_ANIMAL_LICENSE_RENEW, "แก้ไขคำขอต่ออายุใบอนุญาตดำเนินการสถานพยาบาลสัตว์", creater);
            }
            #endregion

            #region ขอใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ

            Application APP_HEALTH = context.Applications.Where(x => x.ApplicationSysName == ApplicationSysName.APP_HEALTH).FirstOrDefault();
            if (APP_HEALTH != null) 
            {
                APP_HEALTH.Remark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ผู้ขอรับใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพต้องมีอายุไม่ต่ำกว่า 20 ปีบริบูรณ์ ตามพระราชบัญญัติสถานประกอบการเพื่อสุขภาพ พ.ศ. 2559</span>";
                APP_HEALTH.CitizenRemark = "<span style=\"color: red; \">หมายเหตุ: ขณะนี้สามารถขอได้เฉพาะในเขตกรุงเทพมหานครเท่านั้น</span><br/><span style=\"color: red; \">ผู้ขอรับใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพต้องมีอายุไม่ต่ำกว่า 20 ปีบริบูรณ์ ตามพระราชบัญญัติสถานประกอบการเพื่อสุขภาพ พ.ศ. 2559</span>";
                string TranslateName = "ขอใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา";
                updateApplication(context, APP_HEALTH, TranslateName, creater);
            }

            #endregion

            #endregion
        }
    }
}
