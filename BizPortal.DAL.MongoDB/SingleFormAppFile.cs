using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class SingleFormAppFile : Entity
    {
        public static void Init()
        {
            var db = MongoFactory.GetSingleFormAppFileCollection();
            if (db.AsQueryable().Count() == 0)
            {
                SingleFormAppFile[] items = new SingleFormAppFile[]
                {
                    new SingleFormAppFile() { AppSysName = AppSystemNameTextConst.APP_SSO, Files = { "COMPANY_PHOTOS", "COMMITTEE_IDENTITY", "DISABILITY_EMPLOYEE_IDENTITY", "FOREIGNER_EMPLOYEE_IDENTITY" } },
                    new SingleFormAppFile()
                    {
                        AppSysName = AppSystemNameTextConst.APP_BUILDING_G1,
                        Files =
                        {
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "APPLICANT_ID_CARD_COPY",
                            "CONSTRUCTION_BLUEPRINT",
                            "CALCULATION_PLAN",
                            "BUILDING_OWNER_DOC",
                            "JURISTIC_DELEGATOR_DOC",
                            "DESIGN_ENGINEER_CONSENT_DOC",
                            "DESIGN_ENGINEER_PROFESSIONAL_LICENSE",
                            "SUPERVISE_ENGINEER_CONSENT_DOC",
                            "SUPERVISE_ENGINEER_PROFESSIONAL_LICENSE",
                            "DESIGN_ARCHITECT_CONSENT_DOC",
                            "DESIGN_ARCHITECT_PROFESSIONAL_LICENSE",
                            "SUPERVISE_ARCHITECT_CONSENT_DOC",
                            "SUPERVISE_ARCHITECT_PROFESSIONAL_LICENSE",
                            "TITLE_DEED_COPY"
                        }
                    },
                    new SingleFormAppFile()
                    {
                        AppSysName = AppSystemNameTextConst.APP_BUILDING_R6,
                        Files =
                        {
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "APPLICANT_ID_CARD_COPY",
                            "BUILDING_OWNERSHIP_DOC",
                            "BUILDING_OWNER_CONSENT_DOC",
                            "JURISTIC_DELEGATOR_DOC",
                            "BUILDING_A1LICENSE_DOC",
                            "SUPERVISE_ARCHITECT_CONSENT_DOC",
                            "SUPERVISE_ARCHITECT_PROFESSIONAL_LICENSE",
                            "SUPERVISE_ENGINEER_CONSENT_DOC",
                            "SUPERVISE_ENGINEER_PROFESSIONAL_LICENSE",
                            //"TITLE_DEED_COPY"
                        }
                    },
                    new SingleFormAppFile()
                    {
                        //AppSysName = AppSystemNameTextConst.APP_HOSPITAL_BUSINESS_EDIT,
                        AppSysName = AppSystemNameTextConst.APP_CLINIC_BUSINESS_EDIT,
                        Files =
                        {
                            // Citizen
                            "ID_CARD",
                            "HOUSE_REGISTARTION",
                            //"CITIZEN_RENAME_DOC",
                            "CITIZEN_RENAME_DOC_REAL",

                            // Juristict
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                            // Sec Doc
                            "APP_CLINIC_BUSINESS_EDIT_DOCA",
                            "APP_CLINIC_BUSINESS_EDIT_DOCB",
                            "APP_CLINIC_BUSINESS_EDIT_DOCC",
                            "APP_CLINIC_BUSINESS_EDIT_DOCD",
                            "APP_CLINIC_BUSINESS_EDIT_DOCE",
                        }
                    },
                    new SingleFormAppFile()
                    {
                        //AppSysName = AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT,
                        AppSysName = AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT,
                        Files =
                        {
                            // Citizen
                            "ID_CARD_COPY",
                            "HOUSE_REGISTARTION",
                            //"CITIZEN_RENAME_DOC",

                            // Juristict
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                            // Sec Doc
                            "APP_CLINIC_OPERATION_EDIT_DOCA",
                            "APP_CLINIC_OPERATION_EDIT_DOCB",
                            "APP_CLINIC_OPERATION_EDIT_DOCC",
                            "APP_CLINIC_OPERATION_EDIT_DOCD",
                            "APP_CLINIC_OPERATION_EDIT_DOCE",
                            "APP_CLINIC_OPERATION_EDIT_DOCF",
                            "APP_CLINIC_OPERATION_EDIT_DOCG",
                        }
                    },
                    new SingleFormAppFile()
                    {
                        //AppSysName = AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                        AppSysName = AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                        Files =
                        {
                            // Citizen
                            "ID_CARD",
                            "HOUSE_REGISTARTION",
                            //"CITIZEN_RENAME_DOC",

                            // Sec Doc
                            "APP_CLINIC_OPERATION_EDIT_B_DOCA",
                            "APP_CLINIC_OPERATION_EDIT_B_DOCB",
                            "APP_CLINIC_OPERATION_EDIT_B_DOCC",
                            "APP_CLINIC_OPERATION_EDIT_B_DOCD",
                            "APP_CLINIC_OPERATION_EDIT_B_DOCE",
                            "APP_CLINIC_OPERATION_EDIT_B_DOCF",
                        }
                    },

                    new SingleFormAppFile()
                    {
                        //AppSysName = AppSystemNameTextConst.APP_CLINIC_BUSINESS_EDIT,
                        AppSysName = AppSystemNameTextConst.APP_HOSPITAL_BUSINESS_EDIT,
                        Files =
                        {
                            // Citizen
                            "ID_CARD",
                            "HOUSE_REGISTARTION",
                            "CITIZEN_RENAME_DOC",
                            "TRAVEL_ID",

                            // Juristict
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                            // Sec Doc
                            "APP_HOSPITAL_BUSINESS_EDIT_DOCA",
                            "APP_HOSPITAL_BUSINESS_EDIT_DOCB",
                            "APP_HOSPITAL_BUSINESS_EDIT_DOCC",
                            "APP_HOSPITAL_BUSINESS_EDIT_DOCD",
                            "APP_HOSPITAL_BUSINESS_EDIT_DOCE",

                        }
                    },
                    new SingleFormAppFile()
                    {
                        //AppSysName = AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT,
                        AppSysName = AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT,
                        Files =
                        {
                            // Citizen
                            "ID_CARD",
                            "HOUSE_REGISTARTION",
                            "CITIZEN_RENAME_DOC",
                            "TRAVEL_ID",

                            // Juristict
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                            // Sec Doc
                            "APP_HOSPITAL_OPERATION_EDIT_DOCA",
                            "APP_HOSPITAL_OPERATION_EDIT_DOCB",
                            "APP_HOSPITAL_OPERATION_EDIT_DOCC",
                            "APP_HOSPITAL_OPERATION_EDIT_DOCD",
                            "APP_HOSPITAL_OPERATION_EDIT_DOCE",
                            "APP_HOSPITAL_OPERATION_EDIT_DOCF",

                        }
                    },
                    new SingleFormAppFile()
                    {
                        //AppSysName = AppSystemNameTextConst.APP_CLINIC_OPERATION_EDIT_B,
                        AppSysName = AppSystemNameTextConst.APP_HOSPITAL_OPERATION_EDIT_B,
                        Files =
                        {
                            // Citizen
                            "ID_CARD",
                            "HOUSE_REGISTARTION",
                            "CITIZEN_RENAME_DOC",
                            "TRAVEL_ID",

                            // Sec Doc
                            "APP_HOSPITAL_OPERATION_EDIT_B_DOCA",
                            "APP_HOSPITAL_OPERATION_EDIT_B_DOCB",
                            "APP_HOSPITAL_OPERATION_EDIT_B_DOCC",
                            "APP_HOSPITAL_OPERATION_EDIT_B_DOCD",
                            "APP_HOSPITAL_OPERATION_EDIT_B_DOCE",
                          
                        }
                    },
                };

                db.InsertMany(items);
                InitForRestaurant(db);
                InitForRetail(db);
                InitForVeterinaryHospital(db);
                InitForSoftwareHouse(db);
                InitForClinic(db);
                InitForOrganicPlantProduction(db);
            }
        }

        public static void InitForRetail(IMongoCollection<SingleFormAppFile> db)
        {
            var items = new List<SingleFormAppFile>
            {
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SELL_ANIMAL_FOOD,
                    Files =
                    {
                        "JURISTIC_COMMITTEE_ID_CARD",
                        //"JURISTIC_COMMITTEE_WORKPERMIT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "SELL_ANIMAL_FOOD_OPERATOR_WORKPERMIT",
                        "SELL_ANIMAL_FOOD_OPERATOR_ID_CARD_COPY",

                        "ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",

                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_ANIMAL_FOOD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",

                        "INFORMATION_STORE_MAP",
                        //"INFORMATION_STORE_BUILDING_OWNER_DOC",
                       
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",

                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",

                        "VAT_REGISTRATION_FRONTIS",
                        "SELL_ANIMAL_FOOD_STORE_PHOTO",
                        "COMMERCE_REGISTRATION_DOC_CITIZEN",
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SELL_CARCASS,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "VAT_REGISTRATION_FRONTIS",

                        "ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",

                        "JURISTIC_COMMITTEE_WORKPERMIT",

                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        //"COMMERCE_REGISTRATION_DOC",
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SELL_ANIMAL,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "VAT_REGISTRATION_FRONTIS",

                        "ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",

                        "JURISTIC_COMMITTEE_WORKPERMIT",

                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",

                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        //"COMMERCE_REGISTRATION_DOC",
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_DIRECT_MARKETING,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        "JURISTIC_MEMORANDUM",

                        "JURISTIC_COMMITTEE_ID_CARD",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",

                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_COMMITTEE_CHANGE_NAME_DOCUMENT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",

                        "ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        //"CITIZEN_MEDICAL_CERT",

                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        //"AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        //"INFORMATION_STORE_BUILDING_RENTER_ID_CARD",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_HQ_MAP",
                        //"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",

                        "COMMERCE_REGISTRATION_DOC_CITIZEN",
                        "COMMERCE_REGISTRATION_DOC",
                        "ECOMMERCE_REGISTRATION_DOC",

                        "DIRECT_MARKETING_PERMIT",
                        "DIRECT_MARKETING_PRODUCT_PERMIT",
                        "DIRECT_SELL_PRODUCT_PRODUCER_PERMIT__OPTIONAL",
                        "DIRECT_SELL_PRODUCT_PRODUCER_PERMIT",
                        "DIRECT_SELL_PRODUCT_PERMIT",
                        "DIRECT_SELL_EXAMPLE_BUYER_DOC",

                        "DIRECT_MARKETING_SELL_METHOD_DETAIL",
                        //"DIRECT_SELL_INVOICE_EXAMPLE",
                        "DIRECT_MARKETING_PRODUCT_TABLE",
                        "DIRECT_MARKETING_PERMIT_BEHAVIOR",
                        "DIRECT_SELL_HQ_PHOTO",

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_DIRECT_MARKETING_EDIT,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        //"JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",

                        
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",


                        "ID_CARD_COPY",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "DIRECT_MARKETING_CITIZEN_HOUSEHOLD",

                        
                        #region Marketing EDIT
                        #region กรณี แก้ไขเปลี่ยนแปลงประเภทหรือชนิดของสินค้าหรือบริการ
                        "DIRECT_MARKETING_EDIT_PRODUCT_PICTURE",
                        "DIRECT_MARKETING_EDIT_PRODUCT_PRODUCER_PERMIT_OPTIONAL",
                        //"DIRECT_MARKETING_EDIT_REGIS_FOOD_LIST",
                        //"DIRECT_MARKETING_EDIT_PERMIT_LABEL",
                        "DIRECT_MARKETING_EDIT_CERTIFICATE_FOOD",
                        //"DIRECT_MARKETING_EDIT_PERMIT_PUBLISH",
                        "DIRECT_MARKETING_EDIT_COSMETICS",
                        "DIRECT_MARKETING_EDIT_IMPORT_RECEIPT",
                        "DIRECT_MARKETING_EDIT_CER_BOOK_C1",
                        "DIRECT_MARKETING_EDIT_PRODUCT_RECEIVE_PERMIT",
                        "DIRECT_MARKETING_TEXT_PAYMENT",
                        "DIRECT_MARKETING_TEXT_TRANSPORT",
                        #endregion

                        "DIRECT_MARKETING_EDIT_SELL_DETAIL",
                        "DIRECT_MARKETING_EDIT_ELECTRIC_COMMER",
                        "DIRECT_MARKETING_EDIT_COMPANY_NAME_COMPARE",
                        "DIRECT_MARKETING_EDIT_PRODUCT_TABLE",

                        #region กรณี แก้ไขเปลี่ยนแปลง ชื่อ กรรมการผู้มีอำนาจลงนามแทนนิติบุคคล
                        //"DIRECT_MARKETING_EDIT_JURISTIC1",
                        //"DIRECT_MARKETING_EDIT_JURISTIC2",
                        
                        "DIRECT_MARKETING_EDIT_COMMITTEE2",
                        "DIRECT_MARKETING_EDIT_CERTIFICATION_OLD",
                        "DIRECT_MARKETING_EDIT_COMMITTEE_LIST",
                        "DIRECT_MARKETING_EDIT_CRIME",
                        "DIRECT_MARKETING_EDIT_CER_BOOK",
                        #endregion
                        #region STORE
                        
                        "DIRECT_MARKETING_EDIT_STORE_USAGE_AGREEMENT",
                        "DIRECT_MARKETING_EDIT_STORE_RENTAL",
                        "DIRECT_MARKETING_EDIT_STORE_BUILDING_OWNED",
                        "DIRECT_MARKETING_EDIT_STORE_OWNED_ID_CARD",
                        "DIRECT_MARKETING_EDIT_STORE_CERTIFICATE_OWNED",
                        "DIRECT_MARKETING_EDIT_STORE_STORE_MAP",
                        "DIRECT_MARKETING_EDIT_STORE_LOCATION_PICTURE",
                        "DIRECT_MARKETING_EDIT_STORE_LOCATION_MAP",
                        "DIRECT_MARKETING_EDIT_STORE_LOCATION_COMPARE",
                        #endregion
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_DIRECT_MARKETING_CANCEL,
                    Files =
                    {
                        #region Citizen
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region Juristic
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion
                        
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                        #region MARKETING CANCEL
                        "DIRECT_MARKETING_CANCEL_LOCAL_NEWS",
                        "DIRECT_MARKETING_CANCEL_REASON",
                        "DIRECT_MARKETING_CANCEL_RESPOND",
                        #endregion
                    },
                },

                new SingleFormAppFile()
                    {
                    AppSysName = AppSystemNameTextConst.APP_DIRECT_SELL_EDIT,
                    Files =
                        {

                        #region Juristic
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region DIRECT SELL FILE
                        "DIRECT_SELL_EDIT_COMMITTEE_ID",
                        "DIRECT_SELL_EDIT_COMMITTEE_LIST",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        //"DIRECT_SELL_EDIT_CERTIFICATION",
                        "DIRECT_SELL_EDIT_CERTIFICATION_OLD",
                        "DIRECT_SELL_EDIT_CRIME",
                        "DIRECT_SELL_EDIT_CER_BOOK",
                        //"DIRECT_SELL_EDIT_COMMITTEE_ID_OPTIONAL",
                        "DIRECT_SELL_EDIT_STORE_LOCATION_COMPARE",
                        //"DIRECT_SELL_EDIT_STORE_LOCATION_MAP",
                        "DIRECT_SELL_EDIT_STORE_LOCATION_PICTURE",

                        #region Product
                        //"DIRECT_SELL_EDIT_PRODUCT_LIST",
                        //"DIRECT_SELL_EDIT_CERTIFICATE_IMPORT_FOOD",
                        "DIRECT_SELL_EDIT_CERTIFICATE_FOOD",
                        "DIRECT_SELL_EDIT_PRODUCT_PICTURE",
                        "DIRECT_SELL_PRODUCT_EDIT_LABEL",
                        "DIRECT_SELL_EDIT_PRODUCT_SOURCE",
                        "DIRECT_SELL_EDIT_COSMETICS",
                        "DIRECT_SELL_EDIT_IMPORT_RECEIPT",
                        "DIRECT_SELL_EDIT_CER_BOOK_C1",
                        "DIRECT_SELL_EDIT_PRODUCT_PRODUCER_PERMIT_OPTIONAL",
                        //"DIRECT_SELL_EDIT_REGIS_FOOD_LIST",
                        //"DIRECT_SELL_EDIT_PERMIT_LABEL",
                        //"DIRECT_SELL_EDIT_PERMIT_PUBLISH",
                        "DIRECT_SELL_EDIT_PRODUCT_RECEIVE_PERMIT",
                        "DIRECT_SELL_EDIT_COMPANY_NAME_COMPARE",
                        "DIRECT_SELL_EDIT_PRODUCT_TABLE",
                        #endregion

                        #region BENEFIT
                        "DIRECT_SELL_EDIT_BENEFIT_BOOK",
                        "DIRECT_SELL_EDIT_BENEFIT_LIST",
                        "DIRECT_SELL_EDIT_BENEFIT_COMPARE_LIST",
                        #endregion

                        #region information store
                        "DIRECT_SELL_EDIT_STORE_RENTAL_CONTACT",
                        "DIRECT_SELL_EDIT_STORE_USAGE_AGREEMENT",
                        "DIRECT_SELL_EDIT_STORE_BUILDING_OWNED",
                        "DIRECT_SELL_EDIT_STORE_OWNED_ID_CARD",
                        "DIRECT_SELL_EDIT_STORE_CERTIFICATE_OWNED",
                        "DIRECT_SELL_EDIT_STORE_MAP",
                        //"DIRECT_SELL_EDIT_STORE_OFFICE_PICTURE",
                        "DIRECT_SELL_EDIT_STORE_LOCATION_MAP",
                        "DIRECT_SELL_EDIT_STORE_LOCATION_PICTURE",
                        "DIRECT_SELL_EDIT_STORE_LOCATION_COMPARE",
                        #endregion

                        #region information store add can cancel
                        "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_USAGE_AGREEMENT",
                        "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_RENTAL_CONTACT",
                        "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_BUILDING_OWNED",
                        "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_OWNED_ID_CARD",
                        "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_CERTIFICATE_OWNED",
                        //"DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_MAP",
                        "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_OFFICE_PICTURE",
                        "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_LOCATION_COMPARE",
                        "DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_LOCATION_MAP",
                        //"DIRECT_SELL_EDIT_CANCEL_OR_ADD_STORE_LOCATION_PICTURE",
                        #endregion

                        "DIRECT_SELL_EDIT_COMMITTEE_WORK_PERMIT",
                        "DIRECT_SELL_EDIT_COMMITTEE_FOREIGNER",
                        #endregion



                    },
                    },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_DIRECT_SELL_CANCEL,
                    Files =
                    {
                        #region Citizen
                        //"CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        //"AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        //"ID_CARD_COPY",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region Juristic
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion
                        
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                        #region MARKETING CANCEL
                        "DIRECT_SELL_CANCEL_LOCAL_NEWS",
                        "DIRECT_SELL_CANCEL_REASON",
                        "DIRECT_SELL_CANCEL_RESPOND",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SELL_CARD,
                    Files =
                    {
                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region JURISTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_CHANGE_NAME_DOCUMENT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion
                        
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "VAT_REGISTRATION_CARD_REQUIRED",
                        "VAT_REGISTRATION_CARD_OPTIONAL",
                        "COMMERCE_REGISTRATION_DOC",
                    },
                },
                // แยกไฟล์เฉพาะใบ
                //new SingleFormAppFile() {
                //        AppSysName = AppSystemNameTextConst.APP_SELL_ALCOHOL,
                //        Files = {
                //            #region Citizen
                //            "ID_CARD_COPY",
                //            //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                //            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                //            "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                //            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                //            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                //            #endregion

                //            #region Juristic
                //            "JURISTIC_COMMITTEE_ID_CARD",
                //            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                //            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                //            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                //            #endregion

                //            #region STORE
                //            "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                //            "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                //            "INFORMATION_STORE_BUILDING_OWNER_DOC",
                //            "INFORMATION_STORE_MAP",
                //            "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                //            "INFORMATION_STORE_RENTAL_CONTRACT",
                //            //"INFORMATION_STORE_BUILDING_RENTER_ID_CARD",
                //            "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                //            "INFORMATION_STORE_MAP",
                //            #endregion 

                //            "ALCOHOL_STORE",
                //        }
                //    },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_BUILDING,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region Juristic
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region STORE
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_OWENED_AREA_DOC",
                        "INFORMATION_STORE_PLAN_DOC",
                        "INFORMATION_STORE_BUILDING_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",
                        #endregion

                        #region Building
                        "BUILDING_CAL_PLAN",
                        "BUILDING_DESIGNER_DOC",
                        "BUILDING_CONTROL_DOC",
                        "BUILDING_CONTROL_PERMIT_DOC",
                        "BUILDING_AUTHORIZATION_ID_CARD",
                        "BUILDING_USING_AREA",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_BUILDING_RENEW,
                    Files =
                    {
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        
                        //ทะเบียนบ้าน ผู้รับมอบอำนาจ
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_OWENED_AREA_DOC",
                        "INFORMATION_STORE_BUILDING_CONTROL_DOC",
                        "BUILDING_CONTROL_DOC",
                        "BUILDING_CONTROL_PERMIT_DOC",
                        "BUILDING_AUTHORIZATION_ID_CARD",
                        "BUILDING_USING_AREA",
                        "BUILDING_CAL_PLAN",

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_BUILDING_EDIT,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        #endregion

                        #region APP_BUILDING_EDIT
                        "BUILDING_CONTROL_DOC_EDIT",
                        "BUILDING_CONTROL_PERMIT_DOC_EDIT",
                        "BUILDING_CONTROL_CHANGE_TYPE_EDIT",
                        "BUILDING_CONTROL_CANCEL_EDIT",
                        "BUILDING_CONTROL_CANCEL_MANAGER_EDIT",
                        "BUILDING_CONTROL_CANCEL_NEW_MANGER_EDIT",
                        "BUILDING_CONTROL_CANCEL_DOC_EDIT",
                        "BUILDING_USING_AREA",
                        "BUILDING_CAL_PLAN",

                        #endregion region



                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_BUILDING_DPW,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region Juristic
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region STORE
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_OWENED_AREA_DOC",
                        "INFORMATION_STORE_PLAN_DOC",
                        "INFORMATION_STORE_BUILDING_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",
                        #endregion

                        #region Building
                        "BUILDING_CAL_PLAN",
                        "BUILDING_DESIGNER_DOC",
                        "BUILDING_CONTROL_DOC",
                        "BUILDING_CONTROL_PERMIT_DOC",
                        "BUILDING_AUTHORIZATION_ID_CARD",
                        "BUILDING_USING_AREA",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_BUILDING_DPW_RENEW,
                    Files =
                    {
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        
                        //ทะเบียนบ้าน ผู้รับมอบอำนาจ
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_OWENED_AREA_DOC",
                        "INFORMATION_STORE_BUILDING_CONTROL_DOC",
                        "BUILDING_CONTROL_DOC",
                        "BUILDING_CONTROL_PERMIT_DOC",
                        "BUILDING_AUTHORIZATION_ID_CARD",
                        "BUILDING_USING_AREA",
                        "BUILDING_CAL_PLAN",
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_BUILDING_DPW_EDIT,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        #endregion

                        #region APP_BUILDING_EDIT
                        "BUILDING_CONTROL_DOC_EDIT",
                        "BUILDING_CONTROL_PERMIT_DOC_EDIT",
                        "BUILDING_CONTROL_CHANGE_TYPE_EDIT",
                        "BUILDING_CONTROL_CANCEL_EDIT",
                        "BUILDING_CONTROL_CANCEL_MANAGER_EDIT",
                        "BUILDING_CONTROL_CANCEL_NEW_MANGER_EDIT",
                        "BUILDING_CONTROL_CANCEL_DOC_EDIT",
                        "BUILDING_USING_AREA",
                        "BUILDING_CAL_PLAN",

                        #endregion region



                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_BUILDING_DISTRICT,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region Juristic
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region STORE
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_OWENED_AREA_DOC",
                        "INFORMATION_STORE_PLAN_DOC",
                        "INFORMATION_STORE_BUILDING_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",
                        #endregion

                        #region Building
                        "BUILDING_CAL_PLAN",
                        "BUILDING_DESIGNER_DOC",
                        "BUILDING_CONTROL_DOC",
                        "BUILDING_CONTROL_PERMIT_DOC",
                        "BUILDING_AUTHORIZATION_ID_CARD",
                        "BUILDING_USING_AREA",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_BUILDING_DISTRICT_RENEW,
                    Files =
                    {
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        
                        //ทะเบียนบ้าน ผู้รับมอบอำนาจ
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_OWENED_AREA_DOC",
                        "INFORMATION_STORE_BUILDING_CONTROL_DOC",
                        "BUILDING_CONTROL_DOC",
                        "BUILDING_CONTROL_PERMIT_DOC",
                        "BUILDING_AUTHORIZATION_ID_CARD",
                        "BUILDING_USING_AREA",
                        "BUILDING_CAL_PLAN",
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_BUILDING_DISTRICT_EDIT,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        #endregion

                        #region APP_BUILDING_EDIT
                        "BUILDING_CONTROL_DOC_EDIT",
                        "BUILDING_CONTROL_PERMIT_DOC_EDIT",
                        "BUILDING_CONTROL_CHANGE_TYPE_EDIT",
                        "BUILDING_CONTROL_CANCEL_EDIT",
                        "BUILDING_CONTROL_CANCEL_MANAGER_EDIT",
                        "BUILDING_CONTROL_CANCEL_NEW_MANGER_EDIT",
                        "BUILDING_CONTROL_CANCEL_DOC_EDIT",
                        "BUILDING_USING_AREA",
                        "BUILDING_CAL_PLAN",

                        #endregion region



                    },
                },

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_BUILDING_OTHER,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region Juristic
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region STORE
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_OWENED_AREA_DOC",
                        "INFORMATION_STORE_PLAN_DOC",
                        "INFORMATION_STORE_BUILDING_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",
                        #endregion

                        #region Building
                        "BUILDING_CAL_PLAN",
                        "BUILDING_DESIGNER_DOC",
                        "BUILDING_CONTROL_DOC",
                        "BUILDING_CONTROL_PERMIT_DOC",
                        "BUILDING_AUTHORIZATION_ID_CARD",
                        "BUILDING_USING_AREA",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_BUILDING_OTHER_RENEW,
                    Files =
                    {
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",


                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",

                        "INFORMATION_STORE_OWENED_AREA_DOC",
                        "INFORMATION_STORE_BUILDING_CONTROL_DOC",
                        "BUILDING_CONTROL_DOC",
                        "BUILDING_CONTROL_PERMIT_DOC",
                        "BUILDING_AUTHORIZATION_ID_CARD",
                        "BUILDING_USING_AREA",
                        "BUILDING_CAL_PLAN",
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_BUILDING_OTHER_EDIT,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        #endregion

                        #region APP_BUILDING_EDIT
                        "BUILDING_CONTROL_DOC_EDIT",
                        "BUILDING_CONTROL_PERMIT_DOC_EDIT",
                        "BUILDING_CONTROL_CHANGE_TYPE_EDIT",
                        "BUILDING_CONTROL_CANCEL_EDIT",
                        "BUILDING_CONTROL_CANCEL_MANAGER_EDIT",
                        "BUILDING_CONTROL_CANCEL_NEW_MANGER_EDIT",
                        "BUILDING_CONTROL_CANCEL_DOC_EDIT",
                        "BUILDING_USING_AREA",
                        "BUILDING_CAL_PLAN",

                        #endregion region



                    },
                },


                #region HOTEL

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HOTEL,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",

                        "JURISTIC_SHARE_HOLDER_LIST",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "HOTEL_JURISTIC_COMMITTEE_MEDICAL_CERT",
                        "HOTEL_DOC_COMMITTEE_FINGERPRINT",

                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "HOTEL_DOC_FINGERPRINT",
                        "HOTEL_CITIZEN_MEDICAL_CERT",

                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_HOTEL",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_OWENED_AREA_DOC",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT_HOTEL",
                        "INFORMATION_STORE_DETAIL_DATE_BUILDING",

                        "DOC_HOTEL_AGREEMENT",
                        "DOC_HOTEL_ENVIRONMENT",
                        "DOC_HOTEL_JURISTIC",
                        "DOC_HOTEL_CHANGE_BUILDING",
                        "DOC_HOTEL_LAW",
                        "DOC_HOTEL_DIAGRAM",

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HOTEL_RENEW,
                    Files =
                    {

                        #region JURISTIC
                         
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "DOC_COMMITTEE_FINGERPRINT",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "HOTEL_JURISTIC_COMMITTEE_MEDICAL_CERT",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "DOC_FINGERPRINT",
                        "HOTEL_CITIZEN_MEDICAL_CERT",

                        #endregion

                        #region OTHER

                        "DOC_HOTEL_CERTIFICATE",
                        "DOC_HOTEL_JURISTIC_RENEW",
                        "DOC_HOTEL_CERTIFICATE_LICENSE",
                        "DOC_HOTEL_DOCUMENT_CHANGE",
                        "DOC_HOTEL_CERTIFICATE",

                        #endregion

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HOTEL_EDIT,
                    Files =
                    {
                        

                        #region JURISTIC
                         
                        "JURISTIC_SHARE_HOLDER_LIST",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON_HOTEL",
                        "HOTEL_DOC_COMMITTEE_FINGERPRINT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",

                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT_HOTEL_EDIT",
                         #endregion

                        #region CITIZEN

                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "HOTEL_DOC_FINGERPRINT",


                         #endregion

                        #region OTHER

                        "DOC_HOTEL_CERTIFICATE_LICENSE",
                        "DOC_HOTEL_JURISTIC",
                        "DOC_APP_HOTEL_BUILDING_MODIFY_CERTIFICATE",
                        "DOC_APP_HOTEL_FLOOR_PLAN_BEFORE_AND_AFTER_CHANGE_TYPE",
                        "DOC_APP_HOTEL_PLAN",
                        "DOC_HOTEL_EDIT_AGREEMENT",
                        "DOC_APP_HOTEL_AGREEMENT_OTHER_OWNER_HOTEL_SAME_NAME",
                        "DOC_APP_HOTEL_OTHER_OWNER_ID_CARD",
                        "DOC_APP_HOTEL_NORMAL_REQUEST_1_3"
                        

                        #endregion

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HOTEL_CANCEL,
                    Files =
                    {

                        #region JURISTIC
                         
                        "JURISTIC_SHARE_HOLDER_LIST",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT_HOTEL_EDIT",


                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON_HOTEL",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",

                         #endregion

                        #region CITIZEN

                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",


                         #endregion

                        #region OTHER

                        "DOC_HOTEL_CERTIFICATE_LICENSE",
                        "DOC_APP_HOTEL_REASON_FOR_OUT_OF_BUSINESS",
                        "DOC_APP_HOTEL_NORMAL_REQUEST_1_3",
                        "DOC_HOTEL_JURISTIC",


                        #endregion
                        
                    },
                },

                #endregion

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_COMMERCIAL,
                    Files =
                    {
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_HQ_MAP",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "DOC_COMMERCIAL_ALLOW",
                        "DOC_COMMERCIAL_BUDGET",
                        "DOC_COMMERCIAL_STATEMENT",
                        "DOC_COMMERCIAL_AREA_PICTURE",
                        "DOC_COMMERCIAL_CHILD",
                        "DOC_COMMERCIAL_DOC_TRADE",
                        "DOC_COMMERCIAL_WEBSITE",
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_COMMERCIAL_EDIT,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        
                        #endregion

                        #region APP_COMMERCIAL_EDIT
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_NO_CON",
                        "INFORMATION_STORE_BOOK_NO_CON",
                        "DOC_COMMERCIAL_ALLOW",
                        "DOC_COMMERCIAL_BUDGET",
                        "COMMERCE_REGISTRATION_DOC_JURISTIC_NO_CON",
                        "COMMERCIAL_BOOK_NO_CON",
                        "COMMERCE_REGISTRATION_DOC_NO_CON",
                        "DOC_COMMERCIAL_EDIT_STATEMENT",
                        "FORM_COMMERCIAL_REGISTRATION",
                        "FORM_COMMERCIAL",
                        #endregion region

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_COMMERCIAL_CANCEL,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        #endregion

                        #region APP_COMMERCIAL_CANCEL
                        "COMMERCE_REGISTRATION_DOC_JURISTIC_CANCEL_NO_CON",
                        "COMMERCE_REGISTRATION_CANCEL_DOC_NO_CON",

                        "COMMERCIAL_OWNER_DEATH_DOC",
                        "COMMERCIAL_OWNER_DOC",
                        "COMMERCIAL_BOOK_CANCEL_DOC",
                        "COMMERCIAL_CANCEL_DOC",
                        "FORM_COMMERCIAL_REGISTRATION",
                        #endregion region



                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_TAX,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        #endregion

                        #region Juristic
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        #region Tax
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "APP_TAX_DOC",
                        "APP_TAX_ALLOW_DOC",
                        "APP_TAX_BOARD_PICTURE",
                        #endregion region

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_TAX_RENEW,
                    Files =
                    {
                       #region Citizen
                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        #endregion

                       #region Juristic
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                       #region Tax
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "APP_TAX_BILL",
                        #endregion region
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_TAX_EDIT,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_TAX_NO_CON",
                        #endregion

                        #region Juristic
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_TAX_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region Tax
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "APP_TAX_ALLOW_DOC",
                        "APP_TAX_PICTURE",
                        "APP_TAX_BILL",
                        "APP_TAX_DETAIL",
                        "APP_TAX_CANCEL",
                        #endregion region

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_TAX_CANCEL,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        #endregion

                        #region Juristic
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_TAX_NO_CON",
                        

                        #endregion

                        #region Tax
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "APP_TAX_BILL",
                        "APP_TAX_CANCEL",
                        #endregion region

                    },
                },
                #region APP RADIO

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_RADIO,
                    Files =
                    {

                        #region JURISTIC
                        
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "VAT_REGISTRATION",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE
                        
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",

                        #endregion

                        #region OTHER

                        "COMMERCE_REGISTRATION_DOC_CITIZEN",

                        #endregion
                        
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_RADIO_RENEW,
                    Files =
                    {

                        #region JURISTIC
                        
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "VAT_REGISTRATION",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion
                        
                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        #endregion

                        #region OTHER
                        
                        "COMMERCE_REGISTRATION_DOC_CITIZEN",
                        "APP_RADIO_LICENSE",
                        "APP_RADIO_REPAIR_LICENSE",
                        "APP_RADIO_OTHER",

                        #endregion
                    },
                },

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_RADIO_EDIT,
                    Files =
                    {

                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "VAT_REGISTRATION",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        

                        #endregion

                        #region CITIZEN
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        //"AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",

                        "ID_CARD_COPY",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        
                        

                        #endregion

                        #region OTHER
                        "RADIO_COMMERCE_REGISTRATION_NO_CON",
                        "APP_RADIO_LICENSE_EDIT",
                        "APP_RADIO_OTHER",
                        "APP_RADIO_LICENSE_EDIT_DOC",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_RADIO_CANCEL,
                    Files =
                    {

                        #region JURISTIC

                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "VAT_REGISTRATION",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        
                        #endregion

                        #region CITIZEN
                        
                        "CANCEL_CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "ID_CARD_COPY_RADIO_CANCEL",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY_RADIO_CANCEL",
                        
                        #endregion

                        #region OTHER

                        "APP_RADIO_LICENSE_EDIT",
                        "APP_RADIO_LICENSE_CANCEL_DOC",

                        #endregion

                    },
                },

                #endregion

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ANIMAL_MED,
                    Files =
                    {
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "MED_ANIMAL_JURISTIC_CERT",
                        //ทะเบียนบ้าน ผู้รับมอบอำนาจ
                        "MED_ANIMAL_CITIZEN_CERT",
                        "CITIZEN_RENAME_DOC",

                        //"AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        //"JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        //"CITIZEN_MEDICAL_CERT",

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        //"INFORMATION_STORE_BUILDING_RENTER_ID_CARD",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",

                        //"INFORMATION_STORE_MAP",
                        //"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        //"DIRECT_SELL_REGISTER_REFERENCE",
                       

                        "MED_ANIMAL_PERMIT",
                        "MED_ANIMAL_DOC",
                        //"MED_ANIMAL_LICENSE",
                        "MED_ANIMAL_LICENSE_CON",
                        "MED_ANIMAL_PICTURE",
                        "MED_ANIMAL_WORK_TIME",
                        "MED_ANIMAL_ALLOW_DOC_CON",
                        "MED_ANIMAL_COPY_ID",
                        "MED_ANIMAL_TRASH",



                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ANIMAL_MED_RENEW,
                    Files =
                    {
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        //"CITIZEN_RENAME_DOC",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                      
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        //"AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        //"JURISTIC_COMMITTEE_CHANGE_NAME_DOCUMENT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        //"CITIZEN_MEDICAL_CERT",

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        //"INFORMATION_STORE_BUILDING_RENTER_ID_CARD",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",

                        //"INFORMATION_STORE_MAP",
                        //"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        //"DIRECT_SELL_REGISTER_REFERENCE",
                       
                        "ANIMAL_BUILD_STORE_MAP",
                        "ANIMAL_BUILD_STORE_PLAN_AREA",
                        "MED_ANIMAL_LICENSE_RENEW",
                        "MED_ANIMAL_PICTURE",
                        "MED_ANIMAL_WORK_TIME",
                        "MED_ANIMAL_ALLOW_DOC",
                        "MED_ANIMAL_TRASH",

                        "MED_ANIMAL_RENEW_DOC",
                        "MED_ANIMAL_RENEW_LICENSE_DOC",
                        "MED_ANIMAL_RENEW_BOOK_DOC",


                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ANIMAL_MED_EDIT,
                    Files =
                    {

                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        //"AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        

                        #endregion

                        #region CITIZEN
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "CITIZEN_RENAME_DOC",
                        //"AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",

                        "ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        
                        

                        #endregion

                        #region OTHER
                        "MED_ANIMAL_RENEW_DOC",
                        "MED_ANIMAL_RENEW_BOOK_DOC",
                        "MED_ANIMAL_RENEW_EDITED_DOC",
                        "FORM_CHANGE_PERMISSION",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ANIMAL_MED_CANCEL,
                    Files =
                    {

                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        

                        #endregion

                        #region CITIZEN
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",

                        
                        

                        #endregion

                        #region OTHER
                        "MED_ANIMAL_RENEW_DOC",
                        "MED_ANIMAL_RENEW_LICENSE_DOC",
                        "FORM_CANCEL_ANIMAL_MED",
                        "MED_ANIMAL_CANCEL_DOC",
                        "MED_ANIMAL_CANCEL_BOOK_DOC",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ANIMAL_LICENSE,
                    Files =
                    {
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_RENAME_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",

                        //ทะเบียนบ้าน ผู้รับมอบอำนาจ               
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",

                        "ANIMAL_LICENSE_DOC",
                        "ANIMAL_PICTURE",
                        "ANIMAL_DATE",
                        "ANIMAL_COPY_DOC",






                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ANIMAL_LICENSE_RENEW,
                    Files =
                    {
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                    //   "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_RENAME_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",


                        "ANIMAL_LICENSE_DOC",
                        "ANIMAL_PICTURE",
                        "ANIMAL_DATE",

                        "MED_ANIMAL_RENEW_DOC",
                        "MED_ANIMAL_RENEW_LICENSE_DOC",
                        "MED_ANIMAL_RENEW_BOOK_DOC",
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ANIMAL_LICENSE_EDIT,
                    Files =
                    {

                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "VAT_REGISTRATION",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        //"AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        

                        #endregion

                        #region CITIZEN
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_RENAME_DOC",

                        //"AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        
                        

                        #endregion

                        #region OTHER

                        //"MED_ANIMAL_RENEW_BOOK_DOC",
                        "ANIMAL_LICENSE_CHANGE_OWNER",
                        "MED_ANIMAL_RENEW_LICENSE_DOC",
                        "MED_ANIMAL_EDIT_LICENSE_DOC",
                        "MED_ANIMAL_EDIT_DOC",
                        "FORM_CHANGE_PERMISSION",
                        "ANIMAL_PICTURE",
                        "ANIMAL_LICENSE_DOC",
                        "ANIMAL_DATE",
                        "ANIMAL_COPY_DOC",
                        "FORM_CHANGE_PERMISSION_2",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ANIMAL_LICENSE_CANCEL,
                    Files =
                    {

                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",

                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        //"AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        

                        #endregion

                        #region CITIZEN
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",

                        "ID_CARD_COPY",
                        
                        

                        #endregion

                        #region OTHER
                        "MED_ANIMAL_RENEW_DOC",
                        "MED_ANIMAL_RENEW_LICENSE_DOC",
                        "MED_ANIMAL_CANCEL_DOC",
                        "FORM_CANCEL_ANIMAL_MED",
                        "APP_ANIMAL_LICENSE_STATISTICS",
                        "APP_ANIMAL_LICENSE_STATISTICS_2",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HEALTH,
                    Files =
                    {

                        #region Citizen

                        "ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        //"MED_ANIMAL_CITIZEN_CERT",
                        "MED_ANIMAL_CITIZEN_CERT_SPA_ONLY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        // TODO: ไม่มีใน MasterFile Doc spa
                        // https://docs.google.com/spreadsheets/d/1gZbXOHDhh2k_G3Srf8aIoHqKCHJfxLUxXUtT4jK5kpA/edit#gid=246865435
                        //"DOC_FINGERPRINT",

                        #endregion

                        #region Juristic

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        "JURISTIC_MEMORANDUM",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "MED_ANIMAL_JURISTIC_CERT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        
                        // TODO: ไม่มีใน MasterFile Doc spa
                        // https://docs.google.com/spreadsheets/d/1gZbXOHDhh2k_G3Srf8aIoHqKCHJfxLUxXUtT4jK5kpA/edit#gid=246865435
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",

                        #endregion

                        #region Information Store

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        //"INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_RENTAL_CONTRACT_SPA_ONLY",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",

                        #endregion

                        #region Permit
                        
                        "HEALTH_CARE_SERVICE_PROVIDER_LIST",
                        //"HEALTH_CARE_SERVICE_PROVIDER_CERTIFICATE",
                        //"HEALTH_CARE_SERVICE_PROVIDER_ID_CARD",
                        //"HEALTH_CARE_MANAGER_BOOK",
                        //"HEALTH_CARE_MANAGER_LICENSE",
                        //"HEALTH_CARE_MANAGE_ID_CARD",
                        "HEALTH_CARE_SHOP_DIAGRAM",

                        #endregion

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = "APP_HOSPITAL",
                    Files =
                    {

                        #region Citizen

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "MED_ANIMAL_CITIZEN_CERT",

                        #region Citizen Authorization
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_ALL_COMMITTEE_MEDICAL_CERT",
                        "JURISTIC_ALL_COMMITTEE_ID_CARD",
                        "JURISTIC_ALL_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                      
                         #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #endregion

                        #region Information Store

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_OPTIONAL",
                        "INFORMATION_STORE_MAP",


                        #endregion

                        #region HOSPITAL DOC
                        "HOSPITAL_STORE",
                        "HOSPITAL_PLAN",
                        "HOSPITAL_EIA",
                        "HOSPITAL_PERMIT",
                        #endregion

                    },
                },

                #region APP LAW OFFICE

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_LAW_OFFICE,
                    Files =
                    {
                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        #region INFORMATION STORE
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_BUILDING_DOC",
                        #endregion

                        #region OTHER
                        "LAW_OFFICE_NAME_PHOTO",
                        "LAW_OFFICE_LOCATION_PHOTO",
                        "LAW_OFFICE_LOCATION_INSIDE_PHOTO",
                        "LAW_OFFICE_LICENSE_PHOTO",
                        "LAW_OFFICE_LAWYER_PHOTO",
                        "APP_LAW_OFFICE_PICTURE_LAWYER",
                        "LAW_OFFICE_IMPORTANT_BRAND_PHOTO",
                        "APP_LAW_OFFICE_APPOINT_CHIEF_LAWYER",
                        "APP_LAW_OFFICE_NOT_A_LAWYER"
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_LAW_OFFICE_EDIT,
                    Files =
                    {
                        #region JURISTIC
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        //TODO: เช็คเงื่อนไขของการแสดงเอกสารที่อยู่สถานประกอบการใหม่
                        #region INFORMATION STORE
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_BUILDING_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",
                        #endregion

                        #region OTHER
                        "APP_LAW_OFFICE_CONFIRM_EDIT_PICTURE_LAWYER",
                        "APP_LAW_OFFICE_CONFIRM_EDIT_DOCUMENT_CHANGE",
                        "APP_LAW_OFFICE_CONFIRM_EDIT_PICTURE_OFFICE",
                        "APP_LAW_OFFICE_CONFIRM_EDIT_PICTURE_ADDRESS_NO",
                        "APP_LAW_OFFICE_CONFIRM_EDIT_AGREE",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_LAW_OFFICE_CANCEL,
                    Files =
                    {
                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        #region OTHER
                        "LAW_OFFICE_CANCEL_CERTIFICATE_LAWYER",
                        "LAW_OFFICE_CANCEL_MANAGER_ALLOWANCE",
                        #endregion
                    },
                },

                #endregion

                #region [ 37 ] APP ELECTRONIC COMMERCIAL

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
                    Files =
                    {
                        #region JURISTIC
                        "JURISTIC_COMMITTEE_ID_CARD",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY", // updated 2019-11-12: ไม่ใช้แล้ว
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region INFORMATION STORE
                        "INFORMATION_STORE_HQ_MAP",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_RENTAL_CONTRACT", //"INFORMATION_STORE_RENTAL_CONTRACT",
                        "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_BUILDING_USAGE_AGREEMENT", //"INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION", //"INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_HOUSEHOLD_RENT", //"INFORMATION_STORE_HOUSEHOLD_RENT",
                        "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        #endregion

                        #region OTHER
                        "DOC_COMMERCIAL_STATEMENT",
                        "APP_ELECTONIC_COMMERCIAL_DOCUMENT_TRADE",
                        "DOC_COMMERCIAL_BUDGET",
                        "DOC_COMMERCIAL_ALLOW",
                        "DOC_COMMERCIAL_AREA_PICTURE",
                        "DOC_COMMERCIAL_CHILD",
                        "APP_ELECTONIC_COMMERCIAL_PICTURE_INDEX_WEBSITE",
                        "APP_ELECTONIC_COMMERCIAL_PICTURE_PRODUCT",
                        "APP_ELECTONIC_COMMERCIAL_PICTURE_METHOD_ORDER_AND_PAYMENT",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_EDIT,
                    Files =
                    {
                        #region JURISTIC
                        "JURISTIC_COMMITTEE_ID_CARD",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY", // updated 2019-11-12: ไม่ใช้แล้ว
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region OTHER
                        "APP_ELECTONIC_COMMERCIAL_EDIT_CURRENT_PERMIT",
                        "APP_ELECTONIC_COMMERCIAL_EDIT_DOCUMENT_CHANGE",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL_CANCEL,
                    Files =
                    {
                        #region JURISTIC
                        "JURISTIC_COMMITTEE_ID_CARD",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY", // updated 2019-11-12: ไม่ใช้แล้ว
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "COMMERCIAL_BOOK_CANCEL_DOC",
                        #endregion

                        #region OTHER
                        "APP_ELECTONIC_COMMERCIAL_CANCEL_CURRENT_PERMIT",
                        "APP_ELECTONIC_COMMERCIAL_CANCEL_OWNER_DEATH",
                        "APP_ELECTONIC_COMMERCIAL_CANCEL_COUNT_ORDERED",
                        "APP_ELECTONIC_COMMERCIAL_CANCEL_HEIR",
                        "APP_ELECTONIC_COMMERCIAL_CANCEL_STOP_BUSINESS",
                        #endregion
                    },
                },

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SECURITIES_BUSINESS,
                    Files =
                    {
                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        #endregion

                        #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion
                        
                        #region SECURITIES BUSINESS DOC
                        "SECURITIES_BUSINESS_PROFILE",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_DOC",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_CAPITAL",
                        "SECURITIES_BUSINESS_ID",
                        "SECURITIES_BUSINESS_CRIME",
                        "SECURITIES_BUSINESS_DIRECT_OR_INDIRECT",
                        "SECURITIES_BUSINESS_CLUE_DOC",
                        "SECURITIES_BUSINESS_BENEFIT",
                        "SECURITIES_BUSINESS_COMMITEE_DOC",
                        "SECURITIES_BUSINESS_LICENSE",
                        #endregion

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SECURITIES_BUSINESS_EDIT,
                    Files =
                    {
                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion
                        
                        #region SECURITIES BUSINESS DOC
                        "SECURITIES_BUSINESS_EDIT_DOC",
                        "SECURITIES_BUSINESS_EDIT_CHANGE",
                        #endregion

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SECURITIES_BUSINESS_CANCEL,
                    Files =
                    {
                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion
                        
                        #region SECURITIES BUSINESS DOC
                        "SECURITIES_BUSINESS_CANCEL_DOC",
                        #endregion

                    },
                },

                #endregion

                #region [ 39 ] APP TRANSPORT NON REGULAR TRUCK

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK,
                    Files =
                    {
                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_MEMORANDUM",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY_NON_PASSPORT",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_ID_CARD_MULTIPLE",
                        #endregion

                        #region OTHER
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_OWNER_DOC",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_OWNER_ID_CARD",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_HOUSEHOLD_RENT",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_RENTAL_CONTRACT",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_OWNER_JURISTIC_REGISTRATION",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_USAGE_AGREEMENT",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_LABEL",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_WITHIN",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_MAINTENANCE_ENTRANCE",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_MAINTENANCE_PARKING",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_TRANSPORTATION_VOLUME",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_CERTIFICATION",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_SECURITIES",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PURCHASE_REQUEST",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_CAR_LICENSE",
                        //"APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_NO_1",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_NO_2",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_NO_3",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW,
                    Files =
                    {
                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_MEMORANDUM",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY_NON_PASSPORT",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region OTHER
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_OWNER_DOC",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_OWNER_ID_CARD",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_HOUSEHOLD_RENT",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_RENTAL_CONTRACT",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_OWNER_JURISTIC_REGISTRATION",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_INFORMATION_USAGE_AGREEMENT",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_LABEL",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_WITHIN",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_MAINTENANCE_ENTRANCE",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_MAINTENANCE_PARKING",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_TRANSPORTATION_VOLUME",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_CERTIFICATION",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_SECURITIES",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PURCHASE_REQUEST_RENEW",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_REQUEST_INFORMATION_CAR_LICENSE",
                        //"APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_NO_1",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_NO_2",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_NO_3",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_CURRENT_DOCUMENT",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT,
                    Files =
                    {
                        #region JURISTIC
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY_NON_PASSPORT",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region OTHER
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_DOCUMENT_CHANGE",
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT_CURRENT_DOCUMENT",
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL,
                    Files =
                    {
                        #region JURISTIC
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY_NON_PASSPORT",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region OTHER
                        "APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL_CURRENT_DOCUMENT",
                        #endregion
                    },
                },

                #endregion

                #region [ 46 ] APP TOURISM BUSINESS

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_TOURISM_BUSINESS,
                    Files =
                    {
                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_REGISTRATION_TYPE_PARTNERSHIP",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_CHANGE_NAME_DOCUMENT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "LIST_OF_SHAREHOLDERS_5_JURISTIC_OPTIONAL",
                        "MEMORANDUM_2_JURISTIC_OPTIONAL",
                        "REGISTRATION_LIST_3_JURISTIC_OPTIONAL",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_RENAME_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region INFORMATION STORE
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",
                        #endregion

                        #region OTHER
                        "APP_TOURISM_BUSINESS_PICTURE_OFFICE_CITIZEN",
                        "APP_TOURISM_BUSINESS_PICTURE_OFFICE_JURISTIC",
                        "APP_TOURISM_BUSINESS_PICTURE_INSIDE_OFFICE",
                        "APP_TOURISM_BUSINESS_LOCATION_OFFICE_CITIZEN",
                        "APP_TOURISM_BUSINESS_LOCATION_OFFICE_JURISTIC",
                        "APP_TOURISM_BUSINESS_DOCUMENT_OWNERD_AREA",
                        "APP_TOURISM_BUSINESS_COPY_DOCUMENT_POLICY",
                        "APP_TOURISM_BUSINESS_DOCUMENT_GUARANTEE_BUSINESS",
                        "APP_TOURISM_BUSINESS_SEAL_COMPANY",
                        "APP_TOURISM_BUSINESS_DOCUMENT_COLLATERAL_CITIZEN",
                        "APP_TOURISM_BUSINESS_DOCUMENT_COLLATERAL_JURISTIC",
                        "APP_TOURISM_BUSINESS_COMPANY_RULES",
                        "APP_TOURISM_BUSINESS_MEETING_REPORT",
                        "APP_TOURISM_BUSINESS_REPORT_AMENDMENT",
                        #endregion
                    },
                },

                #endregion

                #region [ 38 ] APP_ENERGY_PRODUCTION พลังงานควบคุม
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
                    Files =
                    {
                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        //"JURISTIC_COMMITTEE_ID_CARD",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        //"ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion
                        
                        #region APP_ENERGY_PRODUCTION DOC
                        "ENERGY_PRODUCTION_MAP",
                        "ENERGY_PRODUCTION_PLAN",
                        "ENERGY_PRODUCTION_DOC",
                        "ENERGY_PRODUCTION_ENGINEER_LICENSE",
                        #endregion

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ENERGY_PRODUCTION_RENEW,
                    Files =
                    {
                        #region First Doc
                        //"APP_ENERGY_PRODUCTION_REQ_DOC",
                        #endregion

                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion


                        #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion
                        
                        #region APP_ENERGY_PRODUCTION DOC
                        "ENERGY_PRODUCTION_RENEW_MAP",
                        "ENERGY_PRODUCTION_RENEW_PLAN",
                        "ENERGY_PRODUCTION_RENEW_ENGINEER_LICENSE",
                        "ENERGY_PRODUCTION_RENEW_ENGINEER_PERMIT",
                        #endregion

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ENERGY_PRODUCTION_EDIT,
                    Files =
                    {
                        #region First Doc
                        //"APP_ENERGY_PRODUCTION_REQ_DOC",
                        #endregion

                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion


                        #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion
                        
                        #region APP_ENERGY_PRODUCTION DOC
                        "ENERGY_PRODUCTION_EDIT_MAP",
                        "ENERGY_PRODUCTION_EDIT_PLAN",
                        "ENERGY_PRODUCTION_EDIT_ENGINEER_LICENSE",
                        "ENERGY_PRODUCTION_EDIT_ENGINEER_PERMIT",
                        "ENERGY_PRODUCTION_EDIT_DOC_CHANGED",
                        "ENERGY_PRODUCTION_BOOK_CHANGED"
                        #endregion

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ENERGY_PRODUCTION_CANCEL,
                    Files =
                    {
                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion
                        
                        #region APP_ENERGY_PRODUCTION DOC
                        "ENERGY_PRODUCTION_CANCEL_MACHINE_UNINSTALLATION_IMG",
                        "ENERGY_PRODUCTION_CANCEL_BOOK",
                        "ENERGY_PRODUCTION_CANCEL_SINGLE_LINE_DIAGRAM",
                        "ENERGY_PRODUCTION_CANCEL_MAP_TO_PLACE",
                        "ENERGY_PRODUCTION_CANCEL_ELECTRIC_ENGINEER_LICENSE",
                        "ENERGY_PRODUCTION_CANCEL_ELECTRIC_CONTROL_ENGINEER_LICENSE",
                        #endregion

                    },
                },
                #endregion

                #region [ 48 ] พลังงานยกเว้น
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ENERGY_PRODUCTION_NOT_PERMIT,
                    Files =
                    {
                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_ENERGY_NOT_PERMIT",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        #endregion


                        #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_ENERGY_NOT_PERMIT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        #endregion
                        
                        #region APP_ENERGY_NOT_PERMIT DOC
                        "INFORMATION_STORE_MAP",

                        "ENERGY_NOT_PERMIT_REGISTATION_AND_RESEARCH_BUILDING",
                        "ENERGY_NOT_PERMIT_CIVIL_ENGINEERING_PLAN",
                        "ENERGY_NOT_PERMIT_CIVIL_ENGINEERING_LICENSE",
                        "ENERGY_NOT_PERMIT_PRODUCTION_LICENSE",
                        "ENERGY_NOT_PERMIT_PRODUCTION_PLAN",
                        "ENERGY_NOT_PERMIT_PRODUCTION_ENGINEERING_LICENSE",
                        "ENERGY_NOT_PERMIT_CODE_OF_PRACTICE",
                        "ENERGY_NOT_PERMIT_ELECTRIC_CONTRACT",
                        "ENERGY_NOT_PERMIT_STORE_DETAIL",
                        #endregion

                    },
                },
                #endregion

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ACCOUNTING_SERVICE,
                    Files =
                    {

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                      
                         #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #endregion

                        #region ACCOUNT_SERVICE DOC
                        "ACCOUNTING_SERVICE_STATEMENT",
                        //"ACCOUNTING_SERVICE_PAYMENT_PROOF", /* Comment: หน่วยงานให้เอาเอกสารนี้ออกจากระบบ */
                        "ACCOUNTING_SERVICE_COPY_COLLATERAL",
                        "ACCOUNTING_SERVICE_COPY_STATEMENT_PREVIOUS",
                        //"ACCOUNTING_SERVICE_DETAIL_PROOF", /* Comment: หน่วยงานให้เอาเอกสารนี้ออกจากระบบ */
                        #endregion

                    },
                },

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_MOVIE,
                    Files =
                    {
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "REQUESTOR_INFORMATION__REQUEST_TYPE",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "INFORMATION_STORE_MAP",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "CITIZEN_COMMITTEE_PICTURE",
                        "COMMER_DOC",
                        "MOIVE_STORE_PICTURE",
                        "MOIVE_STORE_PICTURE_LONG_RANGE",
                        "MOIVE_STORE_PICTURE_SHORT_RANGE",

                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_KARAOKE,
                    Files =
                    {
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "REQUESTOR_INFORMATION__REQUEST_TYPE",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "INFORMATION_STORE_MAP",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "CITIZEN_COMMITTEE_PICTURE",
                        "COMMER_DOC",
                        "MOIVE_STORE_PICTURE",
                        "MOIVE_STORE_PICTURE_LONG_RANGE",
                        "MOIVE_STORE_PICTURE_SHORT_RANGE",
                        "KARAOKE_STORE_PLAN",

                    },
                },
                new SingleFormAppFile() {
                    AppSysName = AppSystemNameTextConst.APP_DIRECT_SELL,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        "JURISTIC_MEMORANDUM",

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_COMMITTEE_CHANGE_NAME_DOCUMENT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",

                        //"CITIZEN_MEDICAL_CERT",

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        //"INFORMATION_STORE_BUILDING_RENTER_ID_CARD",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_HQ_MAP",
                        //"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",

                        "DIRECT_SELL_PERMIT",
                        "DIRECT_SELL_PRODUCT_PRODUCER_PERMIT__OPTIONAL",
                        "DIRECT_SELL_PRODUCT_PRODUCER_PERMIT",
                        "DIRECT_SELL_PRODUCT_PERMIT",
                        "DIRECT_SELL_BENEFIT_PLAN",
                        "DIRECT_SELL_BUSINESS_PLAN",
                        "DIRECT_SELL_PRODUCT_LABEL",
                        "DIRECT_SELL_EXAMPLE_AGENT_DOC",
                        "DIRECT_SELL_EXAMPLE_BUYER_DOC",
                        //"DIRECT_SELL_INVOICE_EXAMPLE",
                        "DIRECT_SELL_MEMBER_CARD_EXAMPLE",
                        "DIRECT_SELL_FREE_AGENT_CARD_EXAMPLE",
                        "DIRECT_SELL_FREE_AGENT_CONTRACT_EXAMPLE",
                        "DIRECT_SELL_PRODUCT_TABLE",
                        "DIRECT_SELL_HQ_PHOTO",
                        "DIRECT_SELL_PERMIT_BEHAVIOR",
                        "DIRECT_SELL_BOOK_DOC",
                        "DIRECT_SELL_EXAMPLE_DOC",
                    },
                },
                //new SingleFormAppFile() {
                //    AppSysName = AppSystemNameTextConst.APP_SELL_TOBACCO,
                //    Files = {
                //        "JURISTIC_COMMITTEE_ID_CARD",
                //        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                //        "ID_CARD_COPY",
                //        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",

                //        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                //        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                //        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                //        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                //        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",

                //        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                //        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                //        "INFORMATION_STORE_RENTAL_CONTRACT",
                //        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                //        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",

                //        "INFORMATION_STORE_MAP",
                //        //"SELL_TOBACCO_STORE_LAYOUT",

                //        "VAT_REGISTRATION_CARD",
                //    }
                //},

            //FRT NEW ใบอันตราย
            new SingleFormAppFile()
            {

                AppSysName = AppSystemNameTextConst.APP_FRT_NEW_001,
                Files =
                    {
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_MAP_REQUIRED",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",
                        "FRT_001_DOC_01",

                        "FRT_001_DOC_02",
                        "FRT_001_DOC_03",
                        "FRT_001_DOC_04",
                        "FRT_001_DOC_05",
                        "FRT_001_DOC_06",
                        "FRT_001_DOC_07",
                        "FRT_001_DOC_08",
                        "FRT_001_DOC_16"


                       // "JURISTIC_COMMITTEE_ID_CARD",
                       // "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                       // "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                       // "ID_CARD_COPY",
                       // "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                       // //"JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                       // "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                       // "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                       // "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                       //// "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                       // "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                       // "INFORMATION_STORE_BUILDING_OWNER_DOC",
                       // "INFORMATION_STORE_MAP",
                       // "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                       // "INFORMATION_STORE_RENTAL_CONTRACT",
                       // "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                       // "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                       // "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                       // "INFORMATION_STORE_PERMIT_OPEN_BUILD",


                       // "DANGER_FOOD_PRODUCTION_FLOWCHART",
                       // "DANGER_ALL_STORE_PHOTO",
                       // "DANGER_ALL_POLUTION_CONTROL_CHART",
                       // "DANGER_ALL_HEALTH_CONTROL_CHART",
                       // "DANGER_ALL_SEFTY_CONTROL_CHART",
                       // "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                       // "DANGER_FOOD_EIA_HIA",
                       // "DANGER_FOOD_MACHINE_LOCATION",
                       // "DANGER_FOOD_ACTIVITY_PERMIT",
                       // "DANGER_FOOD_ENV_QUALITY",
                       // "DANGER_FOOD_MEDICAL_CERT",
                    },
                },
              //FRT RENEW
            new SingleFormAppFile()
            {
                AppSysName = AppSystemNameTextConst.APP_FRT_RENEW_001,
                Files =
                    {
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_MAP_REQUIRED",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",

                        "FRT_001_DOC_07", 

                        //"JURISTIC_COMMITTEE_ID_CARD_0",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        //"CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        //"ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        //"INFORMATION_STORE_BUILDING_OWNER_DOC",
                        //"INFORMATION_STORE_MAP",
                        //"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        //"INFORMATION_STORE_RENTAL_CONTRACT",
                        //"INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        //"INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        //"INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        //"INFORMATION_STORE_PERMIT_OPEN_BUILD",
                        //"DANGER_FOOD_PRODUCTION_FLOWCHART",
                        //"DANGER_ALL_STORE_PHOTO",
                        //"DANGER_ALL_POLUTION_CONTROL_CHART",
                        //"DANGER_ALL_HEALTH_CONTROL_CHART",
                        //"DANGER_ALL_SEFTY_CONTROL_CHART",
                        //"JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        //"DANGER_FOOD_EIA_HIA",
                        //"DANGER_FOOD_MACHINE_LOCATION",
                        //"DANGER_FOOD_ACTIVITY_PERMIT",
                        //"DANGER_FOOD_ENV_QUALITY",
                        //"DANGER_FOOD_MEDICAL_CERT",
                    },
                },
               //FRT EDIT
            new SingleFormAppFile()
                {

                    AppSysName = AppSystemNameTextConst.APP_FRT_EDIT_001,
                    Files =
                        {
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_WORKPERMIT",
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "ID_CARD_COPY",
                            "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                            "CITIZEN_RENAME_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "INFORMATION_STORE_BUILDING_OWNER_DOC",
                            "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                            "INFORMATION_STORE_MAP_REQUIRED",
                            "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                            "INFORMATION_STORE_RENTAL_CONTRACT",
                            "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                            "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                            "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                            "INFORMATION_STORE_PERMIT_OPEN_BUILD",
                            "INFORMATION_STORE_HOUSEHOLD_RENT",

                            "FRT_001_DOC_07",
                            //"FRT_001_DOC_10",
                            "FRT_001_DOC_11",
                            "FRT_001_DOC_16"

                           //"CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                           // "ID_CARD_COPY",
                           // "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                           // "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                           // "INFORMATION_STORE_MAP",
                           // "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                           // "INFORMATION_STORE_PERMIT_OPEN_BUILD_OPTIONAL",
                        },
                },
            //FRT CANCEL
            new SingleFormAppFile()
                {

                    AppSysName = AppSystemNameTextConst.APP_FRT_CANCEL_001,
                    Files =
                        {
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_WORKPERMIT",
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "ID_CARD_COPY",
                            "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                            "CITIZEN_RENAME_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",

                            //"FRT_001_DOC_12",
                            //"FRT_001_DOC_13",
                            //"FRT_001_DOC_14",
                            "FRT_001_DOC_15",

                           //"CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                           // "ID_CARD_COPY",
                           // "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                           // "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        },
                },
            };




            db.InsertMany(items);
        }

        public static void InitForRestaurant(IMongoCollection<SingleFormAppFile> db)
        {
            var items = new List<SingleFormAppFile>
                {
                    new SingleFormAppFile() {
                        AppSysName = AppSystemNameTextConst.APP_SELL_FOOD_LT_200,
                        Files = {

                            #region JURISTIC

                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "INFORMATION_STORE_BUILDING_OWNER_DOC",

                            #endregion

                            #region CITIZEN

                            "ID_CARD_COPY",
                            "CITIZEN_RENAME_MARRIAGE_DOC",
                            "CITIZEN_BKK_FOOD_HEALTH_CERT",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",

                            #endregion

                            #region INFORMATION STORE

                            "INFORMATION_STORE_MAP",
                            "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD",
                            "INFORMATION_STORE_RENTAL_CONTRACT",
                            "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                            "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                            "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                            "INFORMATION_STORE_BUILDING_CONTROL_DOC",
                            "INFORMATION_STORE_BUILDING_CONTROL_DOC_A6",

                            #endregion

                            #region OTHER

                            "SELL_FOOD_PRODUCTION_PROCESS_CHART",
                            "SELL_FOOD_POLUTION_CONTROL_CHART",
                            "SELL_FOOD_HEALTH_CONTROL_CHART",
                            "SELL_FOOD_SEFTY_CONTROL_CHART",
                            "SELL_FOOD_MEDICAL_CERTIFICATE",
                            "SELL_FOOD_FOOD_WORKER_CERTIFICATE",
                            
                            #endregion

                        }
                    },
                    new SingleFormAppFile() {
                        AppSysName = AppSystemNameTextConst.APP_SELL_FOOD_LT_200_RENEW,
                        Files = {

                            #region JURISTIC
                         
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                            "INFORMATION_STORE_BUILDING_OWNER_DOC",

                            #endregion

                            #region CITIZEN

                            "ID_CARD_COPY",
                            "CITIZEN_RENAME_MARRIAGE_DOC",
                            "CITIZEN_BKK_FOOD_HEALTH_CERT",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            "CITIZEN_BKK_FOOD_HEALTH_CERT",
                            #endregion

                            #region INFORMATION STORE

                            "INFORMATION_STORE_MAP",
                            "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD",
                            "INFORMATION_STORE_RENTAL_CONTRACT",
                            "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",

                            #endregion

                            #region OTHER

                            "SELL_FOOD_MEDICAL_CERTIFICATE",
                            "SELL_FOOD_FOOD_WORKER_CERTIFICATE",
                            "SELL_FOOD_LICENSE_LASTYEAR",

                            
                            #endregion

                        }
                    },
                    new SingleFormAppFile() {
                        AppSysName = AppSystemNameTextConst.APP_SELL_FOOD_LT_200_EDIT,
                        Files = {

                            #region JURISTIC

                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",  

                            #endregion

                            #region CITIZEN

                            "ID_CARD_COPY",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",

                            #endregion

                            #region INFORMATION STORE

                            "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD",

                            #endregion

                            #region OTHER

                            "SELL_FOOD_LICENSE_LASTYEAR",
                            "APP_SELL_FOOD_EDIT_DOC",
                            
                            #endregion

                        }
                    },
                    new SingleFormAppFile() {
                        AppSysName = AppSystemNameTextConst.APP_SELL_FOOD_LT_200_CANCEL,
                        Files = {

                            #region JURISTIC

                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",

                            #endregion

                            #region CITIZEN

                            "ID_CARD_COPY",
                            "CITIZEN_BKK_FOOD_HEALTH_CERT_",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",

                            #endregion

                            #region INFORMATION STORE

                            "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD",

                            #endregion

                            #region OTHER

                            "SELL_FOOD_LICENSE_LASTYEAR",
                            
                            #endregion

                        }
                    },
                    new SingleFormAppFile() {
                        AppSysName = AppSystemNameTextConst.APP_SELL_FOOD_GE_200,
                        Files = {

                            #region JURISTIC

                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",


                            #endregion

                            #region CITIZEN

                            "ID_CARD_COPY",
                            "CITIZEN_RENAME_MARRIAGE_DOC",
                            "CITIZEN_BKK_FOOD_HEALTH_CERT",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",

                            #endregion

                            #region INFORMATION STORE

                            "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD",
                            "INFORMATION_STORE_BUILDING_OWNER_DOC",
                            "INFORMATION_STORE_MAP",
                            "INFORMATION_STORE_RENTAL_CONTRACT",
                            "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                            "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                            "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                            "INFORMATION_STORE_BUILDING_CONTROL_DOC",
                            "INFORMATION_STORE_BUILDING_CONTROL_DOC_A6",

                            #endregion

                            #region OTHER

                            "SELL_FOOD_PRODUCTION_PROCESS_CHART",
                            "SELL_FOOD_POLUTION_CONTROL_CHART",
                            "SELL_FOOD_HEALTH_CONTROL_CHART",
                            "SELL_FOOD_SEFTY_CONTROL_CHART",
                            "SELL_FOOD_MEDICAL_CERTIFICATE",
                            "SELL_FOOD_FOOD_WORKER_CERTIFICATE",
                            
                            #endregion

                        }
                    },
                    new SingleFormAppFile() {
                        AppSysName = AppSystemNameTextConst.APP_SELL_FOOD_GE_200_RENEW,
                        Files = { 

                            #region JURISTIC

                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",

                            #endregion

                            #region CITIZEN

                            "CITIZEN_RENAME_MARRIAGE_DOC",
                            "ID_CARD_COPY",
                            "CITIZEN_BKK_FOOD_HEALTH_CERT",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",

                            #endregion

                            #region INFORMATION STORE
                            "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD",
                            "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                            "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                            "INFORMATION_STORE_RENTAL_CONTRACT",
                            "INFORMATION_STORE_BUILDING_OWNER_DOC",

                            #endregion

                            #region OTHER

                            "SELL_FOOD_FOOD_WORKER_CERTIFICATE",
                            "SELL_FOOD_RENEW_DOC",
                            "SELL_FOOD_MEDICAL_CERTIFICATE",
                            
                            #endregion

                        }
                    },
                    new SingleFormAppFile() {
                        AppSysName = AppSystemNameTextConst.APP_SELL_FOOD_GE_200_EDIT,
                        Files = {

                            #region JURISTIC
                            
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                            #endregion

                            #region CITIZEN
                            
                            "ID_CARD_COPY",
                            "CITIZEN_BKK_FOOD_HEALTH_CERT_",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            "CITIZEN_BKK_FOOD_HEALTH_CERT",
                            "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",

                            #endregion
                            "SELL_FOOD_LICENSE_LASTYEAR",
                            "APP_SELL_FOOD_EDIT_DOC",
                            "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD",
                            "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                            "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                            "INFORMATION_STORE_RENTAL_CONTRACT",
                            "INFORMATION_STORE_BUILDING_OWNER_DOC",

                        }
                    },
                    new SingleFormAppFile() {
                        AppSysName = AppSystemNameTextConst.APP_SELL_FOOD_GE_200_CANCEL,
                        Files = {

                            #region JURISTIC

                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",

                            #endregion

                            #region CITIZEN

                            "ID_CARD_COPY",
                            "CITIZEN_BKK_FOOD_HEALTH_CERT",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",

                            #endregion

                            #region INFORMATION STORE

                            "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD",

                            #endregion

                            #region OTHER

                            "SELL_FOOD_LICENSE_LASTYEAR",
                            
                            #endregion

                        }
                    },

                    new SingleFormAppFile() {
                        AppSysName = AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA,
                        Files = {
                            "ID_CARD_COPY",
                            "CITIZEN_RENAME_MARRIAGE_DOC",
                            "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                            "CITIZEN_MEDICAL_CERT",
                            "CITIZEN_BKK_FOOD_HEALTH_CERT",
                            "CITIZEN_PHOTO",

                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_PHOTO",
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_MEDICAL_CERT",
                            "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",

                            "INFORMATION_STORE_MAP",

                            "SELL_FOOD_IN_PUBLIC_AREA_HELPER_ID_CARD",
                            "SELL_FOOD_IN_PUBLIC_AREA_HELPER_HOUSEHOLD_REGISTRATION_COPY",
                            "SELL_FOOD_IN_PUBLIC_AREA_HELPER_MEDICAL_CERT",
                            "SELL_FOOD_IN_PUBLIC_AREA_HELPER_BKK_FOOD_HEALTH_CERT",
                            "SELL_FOOD_IN_PUBLIC_AREA_HELPER_PHOTO",
                        }
                    },
                    new SingleFormAppFile() {
                        AppSysName = AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA_RENEW,
                        Files = {

                            #region JURISTIC
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            //"JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD", //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้มอบอำนาจ
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC", //หนังสือมอบอำนาจ
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                            "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                            //"JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD", //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ
                            //"INFORMATION_STORE_BUILDING_OWNER_DOC",

                            #endregion

                            #region CITIZEN

                            "ID_CARD_COPY",
                            "CITIZEN_RENAME_MARRIAGE_DOC",
                            "CITIZEN_BKK_FOOD_HEALTH_CERT",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD", //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้มอบอำนาจ
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC", //หนังสือมอบอำนาจ
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD", //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ
                            "CITIZEN_BKK_FOOD_HEALTH_CERT",
                            "CITIZEN_PHOTO",
                            #endregion

                            "SELL_FOOD_IN_PUBLIC_AREA_HELPER_ID_CARD_RENEW",
                            "SELL_FOOD_IN_PUBLIC_AREA_HELPER_HOUSEHOLD_REGISTRATION_COPY_RENEW",
                            //"SELL_FOOD_IN_PUBLIC_AREA_HELPER_MEDICAL_CERT_RENEW",
                            //"SELL_FOOD_IN_PUBLIC_AREA_HELPER_BKK_FOOD_HEALTH_CERT_RENEW",
                            "SELL_FOOD_IN_PUBLIC_AREA_HELPER_PHOTO_RENEW",
                            "SELL_FOOD_MEDICAL_CERTIFICATE",   //ใบรับรองแพทย์การตรวจโรคติดต่อ 9 โรค: ผู้สัมผัสอาหาร ({0} {1} {2})
                            "SELL_FOOD_FOOD_WORKER_CERTIFICATE", //หนังสือรับรองผ่านการอบรมหลักสูตรสุขาภิบาลอาหารของกรุงเทพมหานคร: ผู้สัมผัสอาหาร ({0} {1} {2})
                        }
                    },
                    new SingleFormAppFile() {
                        AppSysName = AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA_EDIT,
                        Files = {

                            #region JURISTIC
                         
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                            //"INFORMATION_STORE_BUILDING_OWNER_DOC",

                            #endregion

                            #region CITIZEN

                            "ID_CARD_COPY",
                            "CITIZEN_RENAME_MARRIAGE_DOC",
                            "CITIZEN_BKK_FOOD_HEALTH_CERT",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            "CITIZEN_BKK_FOOD_HEALTH_CERT",
                            #endregion

                            "SELL_FOOD_IN_PUBLIC_AREA_LICENSE",
                            "SELL_FOOD_IN_PUBLIC_OTHER_EVIDENCE_EDIT_PRODUCT_OR_SELL_METHOD_OR_LOCATION",
                        }
                    },
                    new SingleFormAppFile() {
                        AppSysName = AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA_CANCEL,
                        Files = {
                            #region JURISTIC
                         
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            //"JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                            //"INFORMATION_STORE_BUILDING_OWNER_DOC",

                            #endregion

                            #region CITIZEN

                            "ID_CARD_COPY",
                            "CITIZEN_RENAME_MARRIAGE_DOC",
                            //"CITIZEN_BKK_FOOD_HEALTH_CERT",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            //"CITIZEN_BKK_FOOD_HEALTH_CERT",
                            "CITIZEN_PHOTO",
                            #endregion

                            "SELL_FOOD_IN_PUBLIC_AREA_LICENSE",
                        }
                    },
                    // ******************** APP_FACTORY_CLASS_2 *********************
                    new SingleFormAppFile()
                    {
                        AppSysName = AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW,
                        Files =
                        {
                            #region CITIZEN
                            "ID_CARD_COPY",
                            //"APP_FACTORY_CLASS_2_NEW_DOC_PERSON_AUTHORIZE_LIST",
                            //"APP_FACTORY_CLASS_2_NEW_DOC_PERSON_IDENTIFIED_AUTHORIZER_LIST",
                            //"APP_FACTORY_CLASS_2_NEW_DOC_PERSON_IDENTIFIED_AUTHORIZEE_LIST",
                             "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",  //หนังสือมอบอำนาจ
                             "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้มอบอำนาจ
                             "AUTHORIZATION_AUTHORIZEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ
                            #endregion

                            #region JURISTICT
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            //"APP_FACTORY_CLASS_2_NEW_DOC_BOARD_JURISTICT_PERSON_IDENTIFIED_LIST",
                            //"APP_FACTORY_CLASS_2_NEW_DOC_BOARD_JURISTICT_WORK_PERMIT_PERSON_IDENTIFIED_LIST",
                            //"APP_FACTORY_CLASS_2_NEW_DOC_PERSON_AUTHORIZE_LIST_JURISTICT",
                            //"APP_FACTORY_CLASS_2_NEW_DOC_PERSON_IDENTIFIED_AUTHORIZER_LIST_JURISTICT",
                            //"APP_FACTORY_CLASS_2_NEW_DOC_PERSON_IDENTIFIED_AUTHORIZEE_LIST_JURISTICT",
                            "JURISTIC_COMMITTEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: กรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล: ({0} {1} {2})
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",  //หนังสือมอบอำนาจ
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้มอบอำนาจ
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ

                            #endregion
                        }
                    },
                    // **************** END APP_FACTORY_CLASS_2 ************************

                    // *************** APP_FACTORY_CLASS_2_CANCEL **********************
                    new SingleFormAppFile()
                    {
                        AppSysName = AppSystemNameTextConst.APP_FACTORY_CLASS_2_CANCEL,
                        Files =
                        {
                            #region CITIZEN
                                "ID_CARD_COPY",
                                //"APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_AUTHORIZE_LIST",
                                //"APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_IDENTIFIED_AUTHORIZER_LIST",
                                //"APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_IDENTIFIED_AUTHORIZEE_LIST",
                                "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",  //หนังสือมอบอำนาจ
                                "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้มอบอำนาจ
                                "AUTHORIZATION_AUTHORIZEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ
                                "APP_FACTORY_CLASS_2_CANCEL_DOC_MAP_PLANT_LIST",
                                "APP_FACTORY_CLASS_2_CANCEL_DETIAL_LIST",
                            #endregion

                            #region JURISTICT
                                "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                                //"APP_FACTORY_CLASS_2_CANCEL_DOC_BOARD_JURISTICT_PERSON_IDENTIFIED_LIST",
                                //"APP_FACTORY_CLASS_2_CANCEL_DOC_BOARD_JURISTICT_PERSON_HOME_REGISTER_LIST",
                                //"APP_FACTORY_CLASS_2_CANCEL_DOC_BOARD_JURISTICT_WORK_PERMIT_PERSON_IDENTIFIED_LIST",
                                //"APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_AUTHORIZE_LIST_JURISTICT",
                                //"APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_IDENTIFIED_AUTHORIZER_LIST_JURISTICT",
                                //"APP_FACTORY_CLASS_2_CANCEL_DOC_PERSON_IDENTIFIED_AUTHORIZEE_LIST_JURISTICT",
                                "JURISTIC_COMMITTEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: กรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล: ({0} {1} {2})
                                "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",  //หนังสือมอบอำนาจ
                                "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้มอบอำนาจ
                                "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ
                                "JURISTIC_COMMITTEE_WORKPERMIT",
                                "APP_FACTORY_CLASS_2_CANCEL_DOC_MAP_PLANT_JURISTICT_LIST",
                                "APP_FACTORY_CLASS_2_CANCEL_DETIAL_JURISTICT_LIST",
                            #endregion
                        }
                    },
                    // *************** END APP_FACTORY_CLASS_2_CANCEL ******************

                 // FACTORY_TYPE2
                new SingleFormAppFile()
                {
                        AppSysName = AppSystemNameTextConst .APP_FACTORY_TYPE2,
                        Files =
                        {
                        #region Citizen
                        
                            "ID_CARD_COPY",//01
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",//02
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",//03
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",//04
                            //"APP_FACTOTY_TYPE2_TITLE_DEED", //05
                            //"APP_FACTORY_TYPE2_CITIZEN_BUILDING_OWNER_ID_CARD",//06
                            //"APP_FACTORY_TYP2_LESSOR_REGISTER_HOME",//07
                            //"APP_FACTORY_TYP2_RENT_PROMISE",//08
                            //"APP_FACTORY_TYP2_JURISTIC_RENT_PROMISE_ID",//09
                            //"APP_FACTORY_TYPE2_DOC_USE_BUILDING_ALLOW_CITIZEN",//หนังสือรับรองนิติบุคคล: ผู้ให้เช่า หรือยินยอมให้ใช้อาคารสถานที่
                            //"APP_FACTORY_TYP2_DOC_BUILDING_ALLOW",//10
                            //"APP_FACTORY_TYP2_DOC_USE_BUILDING_ALLOW",//11
                            "APP_FACTORY_TYPE2_PLANT_REGISTER_HOME",//12
                            "APP_FACTORY_TYP2_DOC_CONSTRUCTION_ALLOW",//13
                            "APP_FACTORY_TYP2_DOC_CONTRACTOR",//14
                            "APP_FACTORY_TYP2_DOC_CONTRUCTION_INSPECTION",//15
                            "APP_FACTORY_TYP2_LOCATION_MAP",//16
                            "APP_FACTORY_TYP2_BLUE_PRINT",//17
                            "APP_FACTORY_TYP2_BLUE_PRINT_ONE_PER_HUNDRED",//18
                            "APP_FACTORY_TYP2_ENGINE_BLUE_PRINT",//19
                            "APP_FACTORY_TYP2_INDUSTRIAL_WASTE",//20
                            //"APP_FACTORY_TYPE2_DOC_IDENTIFIED_PREMIT_CITIZEN",//21
                            //"APP_FACTORY_TYP2_JURISTIC_RENT_PROMISE_NOID",//22

                        #endregion

                            //อาคาร
                            "APP_FACTORY_TYPE2_CITIZEN_BUILDING_OWNER_ID_CARD",//เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือ หนังสือเดินทาง: ผู้ให้เช่า หรือ ยินยอมให้ใช้อาคารสถานที่ (หากผู้ให้เช่า หรือยินยอมฯเป็นนิติบุคคล เอกสารนี้หมายถึงเอกสารยืนยันตัวตนของผู้มีอำนาจดำเนินการแทนนิติบุคคลผู้ให้เช่า หรือยินยอมฯ)
                            "APP_FACTORY_TYPE2_LESSOR_REGISTER_HOME_JURISTICT",//ผู้ให้เช่า หรือผู้ยินยอมให้ใช้สถานที่
                            "APP_FACTORY_TYPE2_RENT_PROMISE_JURISTICT",//สัญญาเช่า
                            "APP_FACTORY_TYP2_DOC_USE_BUILDING_ALLOW",//หนังสือยินยอมให้ใช้อาคารสถานที่
                            "APP_FACTORY_TYP2_JURISTIC_RENT_PROMISE_ID",//หนังสือรับรองนิติบุคคล: ผู้ให้เช่า หรือยินยอมให้ใช้อาคารสถานที่
                            "APP_FACTORY_TYPE2_DOC_IDENTIFIED_PREMIT_CITIZEN",//เอกสารแสดงความเป็นเจ้าของอาคารสถานที่ที่ใช้เป็นร้าน/สถานประกอบการ เช่น ใบอนุญาตก่อสร้าง หรือสัญญาซื้อขาย

                            //ยังไม่มีเลขที่บ้าน
                            "APP_FACTORY_TYPE2_CITIZEN_BUILDING_OWNER_ID_CARD_JURISTICT",//เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือ หนังสือเดินทาง: ผู้ให้เช่า หรือ ยินยอมให้ใช้ที่ดิน (หากผู้ให้เช่า หรือยินยอมฯเป็นนิติบุคคล เอกสารนี้หมายถึงเอกสารยืนยันตัวตนของผู้มีอำนาจดำเนินการแทนนิติบุคคลผู้ให้เช่า หรือยินยอมฯ)
                            "APP_FACTORY_TYP2_LESSOR_REGISTER_HOME",//ทะเบียนบ้าน: ผู้ให้เช่า หรือผู้ยินยอมให้ใช้สถานที่
                            "APP_FACTORY_TYP2_RENT_PROMISE",//สัญญาเช่า
                            "APP_FACTORY_TYP2_LAND_ALLOW",//หนังสือยินยอมให้ใช้ที่ดิน
                            "APP_FACTORY_TYP2_JURISTIC_RENT_PROMISE_NOID",//หนังสือรับรองนิติบุคคล: ผู้ให้เช่า หรือยินยอมให้ใช้ที่ดิน
                            "APP_FACTORY_TYPE2_PLANT_TITLE_DEED_JURISTICT",//โฉนดที่ดินที่ต้องการขออนุญาต
                            "APP_FACTORY_TYP2_DOC_BUILDING_ALLOW",//หนังสือรับรองให้ปลูกสร้างอาคารในที่ดิน

                        #region Juristic
                        
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",//01
                            //"APP_FACTORY_TYPE2_JURISTICT_IDENTIFIED_DOC",//02
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_COMMITTEE_WORKPERMIT",//03
                            //"APP_FACTORY_TYPE2_AUTHORIZE_JURISTICT_DOC",//04
                            //"APP_FACTORY_TYPE2_AUTHORIZER_JURISTICT_DOC",//05
                            //"APP_FACTORY_TYPE2_AUTHORIZY_JURISTICT_DOC",//06
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            //"APP_FACTORY_TYPE2_PLANT_TITLE_DEED_JURISTICT",//07
                            "APP_FACTORY_TYPE2_PLANT_REGISTER_HOME_JURISTICT",//08
                            //"APP_FACTORY_TYPE2_CITIZEN_BUILDING_OWNER_ID_CARD_JURISTICT",//09
                            //"APP_FACTORY_TYPE2_LESSOR_REGISTER_HOME_JURISTICT",//10
                            //"APP_FACTORY_TYPE2_RENT_PROMISE_JURISTICT",//11 
                            //"APP_FACTORY_TYP2_JURISTIC_RENT_PROMISE_JURISTICT",//12
                            "APP_FACTORY_TYPE2_DOC_BUILDING_ALLOW_JURISTICT",//13
                            //"APP_FACTORY_TYPE2_DOC_USE_BUILDING_ALLOW_JURISTICT",//14
                            //"APP_FACTORY_TYP2_JURISTIC_RENT_PROMISE_ID_JURISTICT",//15
                            "APP_FACTORY_TYP2_DOC_CONSTRUCTION_ALLOW_JURISTICT",//16
                            "APP_FACTORY_TYP2_DOC_CONTRACTOR_JURISTICT",//17
                            "APP_FACTORY_TYP2_DOC_CONTRUCTION_INSPECTION_JURISTICT",//18
                            "APP_FACTORY_TYP2_LOCATION_MAP_JURITICT",//19
                            "APP_FACTORY_TYP2_BLUE_PRINT_JURISTICT",//20
                            "APP_FACTORY_TYP2_BLUE_PRINT_ONE_PER_HUNDRED_JURISTICT",//21
                            "APP_FACTORY_TYP2_ENGINE_BLUE_PRINT_JURISTICT",//22
                            "APP_FACTORY_TYP2_INDUSTRIAL_WASTE_JURISTICT",//23
                        #endregion
                    },
                },
                // END FACTORY_TYPE2

                // APP_FACTORY_CLASS2_EDIT
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_FACTORY_CLASS_2_EDIT,
                    Files =
                    {
                        #region Citizen

                        "ID_CARD_COPY",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_AUTHORIZE_LIST",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_IDENTIFIED_AUTHORIZER_LIST",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_PERSON_IDENTIFIED_AUTHORIZEE_LIST",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_MAP_PLANT_LIST",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_INFORMATION_STORE_BUILDING_OWNER",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_RENT_PROMISE",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_ALLOW_PLACE",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_ALLOW_PLACE",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_ALLOW_CONTRUCTION",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_RENT_REGISTER_HOME",
                        // *********************************

                        // สถานที่ตั้งสำหนักงานใหญ่
                        "APP_FACTORY_CLASS_2_EDIT_DOC_MAP_HEAD_OFFICE",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_INFORMATION_STORE_BUILDING_OWNER",
                        "APP_FACTORY_CLASS_2_EDIT_DOC_INFORMATION_STORE_BUILDING_OWNER_ID_CARD_2",
                        "APP_FACTORY_CLASS_2_EDIT_DOC_RENT_PROMISE_2",
                        "APP_FACTORY_CLASS_2_EDIT_DOC_ALLOW_PLACE_2",
                        "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_ALLOW_PLACE_2",
                        //"APP_FACTORY_TYPE2_EDIT_TITLE_DEED",
                        "APP_FACTORY_CLASS_2_EDIT_DOC_ALLOW_CONTRUCTION_2",
                        "APP_FACTORY_CLASS_2_EDIT_DOC_RENT_FREE_ID_CARD",
                        "APP_FACTORY_CLASS_2_EDIT_DOC_OWN_BUILDING_DOC",
                        "APP_FACTORY_CLASS_2_EDIT_DOC_HOUSE_REGISTRATION",

                        // ที่ตั้งโรงงาน
                       "APP_FACTORY_CLASS_2_EDIT_DOC_MAP_PLATLOCATION",
                       "APP_FACTORY_CLASS_2_EDIT_DOC_OWN_PLANT_DOC",
                       "APP_FACTORY_CLASS_2_EDIT_DOC_RENT_PLANT_FREE_ID_CARD",
                       "APP_FACTORY_CLASS_2_EDIT_DOC_RENT_PLANT_PROMISE",
                       "APP_FACTORY_CLASS_2_EDIT_DOC_ALLOW_PLANT",
                       "APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_ALLOW_PLANT",
                       "APP_FACTORY_CLASS_2_EDIT_DOC_ALLOW_CONTRUCTION_PLANT",
                       "APP_FACTORY_CLASS_2_EDIT_DOC_HOUSE_REGISTRATION_PLANT",
                        #endregion

                        #region Juristict

                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_BOARD_JURISTICT_PERSON_IDENTIFIED_LIST",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_BOARD_JURISTICT_PERSON_HOME_REGISTER_LIST",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_PERSON_AUTHORIZE_LIST",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_PERSON_IDENTIFIED_AUTHORIZER_LIST",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_PERSON_IDENTIFIED_AUTHORIZEE_LIST",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_MAP_PLANT_LIST",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_INFORMATION_STORE_BUILDING_OWNER",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_JUSTICT_RENT_PROMISE",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_ALLOW_PLACE_JURISTICT",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_ALLOW_PLACE_JURISTICT",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_ALLOW_CONTRUCTION",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_RENT_REGISTER_HOME",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_CHANGE_NAME_AND_LASTNAME",
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_JURISTICT_APPLICATIONFORM",

                        //"APP_FACTORY_CLASS_2_EDIT_DOC_J01", //แผนที่สังเขป แสดงสถานที่ตั้งของโรงงาน
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_J02", //เอกสารแสดงความเป็นเจ้าของสถานที่ที่ใช้เป็นสถานที่ตั้งโรงงาน เช่น ใบอนุญาตก่อสร้าง หรือสัญญาซื้อขาย  
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_J03", //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือ หนังสือเดินทาง: ผู้ให้เช่า หรือ ยินยอมให้ใช้สถานที่ (หากผู้ให้เช่า หรือยินยอมฯเป็นนิติบุคคล เอกสารนี้หมายถึงเอกสารยืนยันตัวตนของผู้มีอำนาจดำเนินการแทนนิติบุคคลผู้ให้เช่า หรือยินยอมฯ)
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_J04", //สัญญาเช่า 
                        //"APP_FACTORY_CLASS_2_EDIT_DOC_J05", //หนังสือยินยอมให้ใช้สถานที่

                        #endregion

                    },
                },
                // END APP_FACTORY_CLASS2_EDIT

                #region [ 43.2 - 1 ]

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "ACCOUNTING_SERVICE_COPY_COLLATERAL_ACC_RENEW",
                        "ACCOUNTING_SERVICE_COPY_STATEMENT_PREVIOUS_ACC_RENEW",
                        //"ACCOUNTING_SERVICE_DETAIL_PROOF", /* Comment: หน่วยงานให้เอาเอกสารนี้ออกจากระบบ */
                    }
                },

                #endregion

                #region [ 43.2 - 2 ]

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_RENEW_TYPE_2,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "APP_ACCOUNTING_SERVICE_RENEW_PAY_RENEW_BILL",
                    }
                },

                #endregion

                // APP_ACCOUNTING_SERVICE_CANCEL
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_CANCEL,
                    Files =
                    {
                        #region Juristict
                        
                            //"ACCOUNTING_DOC_CANCEL_UPLOAD_1",
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            //"JURISTIC_COMMITTEE_ID_CARD_OPIONAL_NO_CON",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            //"APP_ACCOUNTING_SERVICE_CANCEL_PAY_RENEW_BILL",                            

                        #endregion
                    }
                },
                // END APP_ACCOUNTING_SERVICE_CANCEL

                #region [ 43.3 - 1 ]
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_EDIT,
                    Files =
                    {
                        #region Juristict
                        
                            
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            "ACCOUNTING_SERVICE_DOCUMENT_LOST",
                            "ACCOUNTING_SERVICE_CHANGE_DOCUMENT",
                            //"APP_ACCOUNTING_SERVICE_EDIT_PAY_RENEW_BILL",
                            //"APP_ACC_EDIT_LOST_DOC",
                            //"APP_ACC_EDIT_REQUITE_TO_EDIT_DOC",
                            //"ACCOUNTING_DOC_EDIT_UPLOAD_1",
                            //"ACCOUNTING_DOC_EDIT_UPLOAD_2",

                        #endregion

                    }
                },
                #endregion

                #region [ 43.3 - 2 ]
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ACCOUNTING_SERVICE_EDIT_TYPE_2,
                    Files =
                    {
                        #region Juristict
                        
                            
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            "ACCOUNTING_SERVICE_DOCUMENT_LOST",
                            "ACCOUNTING_SERVICE_CHANGE_DOCUMENT",
                            //"APP_ACCOUNTING_SERVICE_EDIT_PAY_RENEW_BILL",
                            //"APP_ACC_EDIT_LOST_DOC",
                            //"APP_ACC_EDIT_REQUITE_TO_EDIT_DOC",
                            //"ACCOUNTING_DOC_EDIT_UPLOAD_1",
                            //"ACCOUNTING_DOC_EDIT_UPLOAD_2",

                        #endregion

                    }
                },
                #endregion

                // AG_NEW
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,
                    Files =
                    {
                        #region JURISTICT
                        
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            "INFORMATION_STORE_BUILDING_OWNER_DOC",
                            "INFORMATION_STORE_RENTAL_CONTRACT",
                            "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                            "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                            "INFORMATION_STORE_HOUSEHOLD_RENT",
                            "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                            //"APP_ORGANIC_PLANT_PRODUCTION_NEW_JURISTICT_DOC",

                        #endregion

                            "APP_ORGANIC_PLANT_PRODUCTION_NEW_CITIZEN_DOC",
                            "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH1",
                            "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH2",
                            "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH3",
                            "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH4",
                            "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH5",
                            "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH6",
                            "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH7",
                            "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH8",
                            "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH9",
                            "APP_ORGANIC_PLANT_PRODUCTION_NEW_ATTACH_CH10",

                        #region CITIZEN
                            
                            "ID_CARD_COPY",
                            "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                    }
                },
                // END AG_NEW

                // APP_HEALTH
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HEALTH_RENEW,
                    Files  =
                    {
                        #region [ JURISTICT ]
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_SHARE_HOLDER_LIST",
                            "JURISTIC_MEMORANDUM",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                            "MED_ANIMAL_JURISTIC_CERT",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        #region [ CITIZEN ]    
                            "ID_CARD_COPY",
                            "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                            "MED_ANIMAL_CITIZEN_CERT",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "INFORMATION_STORE_BUILDING_OWNER_DOC",
                            "APP_HEALTH_RENEW_01",
                            //"APP_HEALTH_RENEW_02",
                            "APP_HEALTH_RENEW_03",
                            //"APP_HEALTH_RENEW_04",
                            "APP_HEALTH_RENEW_05",
                            "HEALTH_CARE_SERVICE_PROVIDER_ID_CARD_RENEW",
                            "HEALTH_CARE_MANAGE_ID_CARD_RENEW"
                        #endregion
                    }
                },

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HEALTH_EDIT,
                    Files =
                    {
                        #region [ Juristic ]
                        
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",

                        #endregion

                        #region [ Citizen ]
                            
                            "ID_CARD_COPY",
                            "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                            "APP_HEALTH_EDIT_DOC_DETAIL",
                            //"APP_HEALTH_EDIT_OLD_DOC_ALLOW",
                            //"APP_HEALTH_EDIT_MUST_TO",
                            "APP_HEALTH_EDIT_CURRENT_DOC_ALLOW",
                           "AUTHORIZATION_AUTHORIZEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ 

                        #endregion

                    }
                },
                // END APP_HEALTH
                 new SingleFormAppFile()
                 {
                    AppSysName = AppSystemNameTextConst.APP_SEC_NEW_A,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        //"JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_PROFILE",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_DOC",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_CAPITAL",
                        "SECURITIES_BUSINESS_ID",
                        "SECURITIES_BUSINESS_CRIME",
                        "SECURITIES_BUSINESS_DIRECT_OR_INDIRECT",
                        "SECURITIES_BUSINESS_CLUE_DOC",
                        "SECURITIES_BUSINESS_BENEFIT",
                        "SECURITIES_BUSINESS_COMMITEE_DOC",
                        "SECURITIES_BUSINESS_LICENSE",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT_CON",
                    }
                 },

                 new SingleFormAppFile()
                 {
                    AppSysName = AppSystemNameTextConst.APP_SEC_NEW_B,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        //"JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_PROFILE",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_DOC",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_CAPITAL",
                        "SECURITIES_BUSINESS_ID",
                        "SECURITIES_BUSINESS_CRIME",
                        "SECURITIES_BUSINESS_DIRECT_OR_INDIRECT",
                        "SECURITIES_BUSINESS_CLUE_DOC",
                        "SECURITIES_BUSINESS_BENEFIT",
                        "SECURITIES_BUSINESS_COMMITEE_DOC",
                        "SECURITIES_BUSINESS_LICENSE",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT_CON",
                    }
                 },

                 new SingleFormAppFile()
                 {
                    AppSysName = AppSystemNameTextConst.APP_SEC_NEW_C,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        //"JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_PROFILE",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_DOC",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_CAPITAL",
                        "SECURITIES_BUSINESS_ID",
                        "SECURITIES_BUSINESS_CRIME",
                        "SECURITIES_BUSINESS_DIRECT_OR_INDIRECT",
                        "SECURITIES_BUSINESS_CLUE_DOC",
                        "SECURITIES_BUSINESS_BENEFIT",
                        "SECURITIES_BUSINESS_COMMITEE_DOC",
                        "SECURITIES_BUSINESS_LICENSE",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT_CON",
                    }
                 },

                 new SingleFormAppFile()
                 {
                    AppSysName = AppSystemNameTextConst.APP_SEC_NEW_D,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        //"JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_PROFILE",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_DOC",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_CAPITAL",
                        "SECURITIES_BUSINESS_ID",
                        "SECURITIES_BUSINESS_CRIME",
                        "SECURITIES_BUSINESS_DIRECT_OR_INDIRECT",
                        "SECURITIES_BUSINESS_CLUE_DOC",
                        "SECURITIES_BUSINESS_BENEFIT",
                        "SECURITIES_BUSINESS_COMMITEE_DOC",
                        "SECURITIES_BUSINESS_LICENSE",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT_CON",
                    }
                 },

                 new SingleFormAppFile()
                 {
                    AppSysName = AppSystemNameTextConst.APP_SEC_NEW_E,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        //"JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_PROFILE",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_DOC",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_CAPITAL",
                        "SECURITIES_BUSINESS_ID",
                        "SECURITIES_BUSINESS_CRIME",
                        "SECURITIES_BUSINESS_DIRECT_OR_INDIRECT",
                        "SECURITIES_BUSINESS_CLUE_DOC",
                        "SECURITIES_BUSINESS_BENEFIT",
                        "SECURITIES_BUSINESS_COMMITEE_DOC",
                        "SECURITIES_BUSINESS_LICENSE",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT_CON",
                    }
                 },

                 new SingleFormAppFile()
                 {
                    AppSysName = AppSystemNameTextConst.APP_SEC_NEW_F,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        //"JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_PROFILE",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_DOC",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_CAPITAL",
                        "SECURITIES_BUSINESS_ID",
                        "SECURITIES_BUSINESS_CRIME",
                        "SECURITIES_BUSINESS_DIRECT_OR_INDIRECT",
                        "SECURITIES_BUSINESS_CLUE_DOC",
                        "SECURITIES_BUSINESS_BENEFIT",
                        "SECURITIES_BUSINESS_COMMITEE_DOC",
                        "SECURITIES_BUSINESS_LICENSE",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT_CON",
                    }
                 },

                  new SingleFormAppFile()
                  {
                    AppSysName = AppSystemNameTextConst.APP_SEC_NEW_G,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_SHARE_HOLDER_LIST",
                        //"JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_PROFILE",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_DOC",
                        "SECURITIES_BUSINESS_SHARE_HOLDER_CAPITAL",
                        "SECURITIES_BUSINESS_ID",
                        "SECURITIES_BUSINESS_CRIME",
                        "SECURITIES_BUSINESS_DIRECT_OR_INDIRECT",
                        "SECURITIES_BUSINESS_CLUE_DOC",
                        "SECURITIES_BUSINESS_BENEFIT",
                        "SECURITIES_BUSINESS_COMMITEE_DOC",
                        "SECURITIES_BUSINESS_LICENSE",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT_CON",
                    }
                  },

                  new SingleFormAppFile()
                  {
                      AppSysName = AppSystemNameTextConst.APP_SEC_EDIT,
                      Files =
                      {
                          "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                          "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                          "SECURITIES_BUSINESS_EDIT_DOC",
                          "SECURITIES_BUSINESS_EDIT_CHANGE",
                      }
                  },

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SEC_CANCEL_A,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_CANCEL_DOC",                       
                    }
                },

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SEC_CANCEL_B,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_CANCEL_DOC",
                    }
                },

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SEC_CANCEL_C,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_CANCEL_DOC",
                    }
                },

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SEC_CANCEL_D,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_CANCEL_DOC",
                    }
                },

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SEC_CANCEL_E,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_CANCEL_DOC",
                    }
                },

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SEC_CANCEL_F,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_CANCEL_DOC",
                    }
                },

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SEC_CANCEL_G,
                    Files =
                    {
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "SECURITIES_BUSINESS_CANCEL_DOC",
                    }
                },
                // SEC_NEW_A

                // END SEC_NEW_A
            };

            for (int i = 0; i < AppSystemNameTextConst.APP_Danger_Food.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_Danger_Food[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        "DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_RENEW[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_DANGER_FOOD_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_EDIT[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_RENAME_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT_OPTIONAL",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD_OPTIONAL",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_ALL_STORE_PHOTO_OPTIONAL",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",
                        "DANGER_ALL_RENTAL_CONTACT",
                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_DANGER_FOOD_CANCEL[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_RENAME_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE
                        
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",

                        #endregion

                        #region OTHER
                        
                        "DANGER_FOOD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_INSTEAD_LICENSE_BAD_HEALTH",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_CONSTRUCTION.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_CONSTRUCTION[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        //"INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC_OPTIONAL_NO_CON",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        "DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        //"DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_STORE_PHOTO_OPTIONAL",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_EDIT[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_RENAME_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT_OPTIONAL",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD_OPTIONAL",
                        "DOC_APP_DANGER_REQUEST_EDIT_LICENSE",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_ALL_STORE_PHOTO_OPTIONAL",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",
                        "DANGER_ALL_RENTAL_CONTACT",
                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CANCEL[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "COMMITTEE_WORK_PERMIT_NO_CON",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region INFORMATION STORE

                        //"INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        //"INFORMATION_STORE_RENTAL_CONTRACT",
                        //"INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        //"INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",

                        #endregion

                        #region OTHER

                        //"DOC_APP_DANGER_REQUEST_CANCEL_LICENSE",
                        "DOC_APP_DANGER_REQUEST_CANCEL_LICENSE_OPTIONAL",
                        "DANGER_FOOD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_INSTEAD_LICENSE_BAD_HEALTH",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_OTHER.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_OTHER[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        //"INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC_OPTIONAL_NO_CON",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        "DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        //"DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_STORE_PHOTO_OPTIONAL",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_EDIT[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_RENAME_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT_OPTIONAL",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD_OPTIONAL",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_ALL_STORE_PHOTO_OPTIONAL",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",
                        "DANGER_ALL_RENTAL_CONTACT",
                        "DANGER_FOOD_LICENSE_BAD_HEALTH",
                        "BUILDING_USING_AREA_CONFIRM_AGENT",
                        "DOC_APP_DANGER_REQUEST_EDIT_LICENSE",
                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_OTHER_CANCEL[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "COMMITTEE_WORK_PERMIT_NO_CON",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region INFORMATION STORE

                        //"INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        //"INFORMATION_STORE_RENTAL_CONTRACT",
                        //"INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        //"INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",

                        #endregion

                        #region OTHER

                        //"DOC_APP_DANGER_REQUEST_CANCEL_LICENSE",
                        "DOC_APP_DANGER_REQUEST_CANCEL_LICENSE_OPTIONAL",
                        "DANGER_FOOD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_INSTEAD_LICENSE_BAD_HEALTH",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_PETROLEUM.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_PETROLEUM[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        //"INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC_OPTIONAL_NO_CON",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        "DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        //"DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_STORE_PHOTO_OPTIONAL",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_EDIT[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_RENAME_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT_OPTIONAL",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD_OPTIONAL",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_ALL_STORE_PHOTO_OPTIONAL",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",
                        "DANGER_ALL_RENTAL_CONTACT",
                        "DOC_APP_DANGER_REQUEST_EDIT_LICENSE",
                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_CANCEL[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "COMMITTEE_WORK_PERMIT_NO_CON",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region INFORMATION STORE

                        //"INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        //"INFORMATION_STORE_RENTAL_CONTRACT",
                        //"INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        //"INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",

                        #endregion

                        #region OTHER

                        //"DOC_APP_DANGER_REQUEST_CANCEL_LICENSE",
                        "DOC_APP_DANGER_REQUEST_CANCEL_LICENSE_OPTIONAL",
                        "DANGER_FOOD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_INSTEAD_LICENSE_BAD_HEALTH",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        //"INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC_OPTIONAL_NO_CON",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        "DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DOC_APP_DANGER_CURRENT_LICENSE",
                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_ALL_STORE_PHOTO_OPTIONAL",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_EDIT[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_RENAME_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT_OPTIONAL",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD_OPTIONAL",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_ALL_STORE_PHOTO_OPTIONAL",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",
                        "DANGER_ALL_RENTAL_CONTACT",
                        "DOC_APP_DANGER_REQUEST_EDIT_LICENSE",
                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CANCEL[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "COMMITTEE_WORK_PERMIT_NO_CON",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region INFORMATION STORE

                        //"INFORMATION_STORE_BUILDING_OWNER_DOC_OPTIONAL_NO_CON",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        //"DANGER_ALL_RENTAL_CONTACT",
                        //"INFORMATION_STORE_BUILDING_USAGE_AGREEMENT_OPTIONAL",
                        //"INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION_OPTIONAL",

                        #endregion

                        #region OTHER

                        //"DOC_APP_DANGER_REQUEST_CANCEL_LICENSE",
                        "DOC_APP_DANGER_REQUEST_CANCEL_LICENSE_OPTIONAL",
                        "DANGER_FOOD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_INSTEAD_LICENSE_BAD_HEALTH",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        "DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {
                        
                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT_OPTIONAL",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",

                        #endregion

                        #region PERMIT

                        "DOC_APP_DANGER_CURRENT_LICENSE",
                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "COMMITTEE_WORK_PERMIT_NO_CON",
                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",

                        #endregion

                        #region INFORMATION STORE
                        "BUILDING_USING_AREA_CONFIRM_AGENT",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DOC_APP_DANGER_REQUEST_EDIT_LICENSE",
                        "DANGER_FOOD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        "DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {
                        
                        #region JURUSTIC

                        
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "COMMITTEE_WORK_PERMIT_NO_CON",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",

                        #endregion

                        #region OTHER

                        "DOC_APP_DANGER_REQUEST_CANCEL_LICENSE",
                        "DANGER_FOOD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_INSTEAD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        "DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion
                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT_OPTIONAL",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",

                        #endregion

                        #region PERMIT

                        "DOC_APP_DANGER_CURRENT_LICENSE",
                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "COMMITTEE_WORK_PERMIT_NO_CON",
                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",

                        #endregion

                        #region INFORMATION STORE
                        "BUILDING_USING_AREA_CONFIRM_AGENT",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DOC_APP_DANGER_REQUEST_EDIT_LICENSE",
                        "DANGER_FOOD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        "DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {
                        
                        #region JURUSTIC

                        
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "COMMITTEE_WORK_PERMIT_NO_CON",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",

                        #endregion

                        #region OTHER

                        "DOC_APP_DANGER_REQUEST_CANCEL_LICENSE",
                        "DANGER_FOOD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_INSTEAD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",

                        #endregion

                    }
                });
            }

            #region SERVICES

            #region RESTAURENT

            for (int i = 0; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT.Length; i++)
            {
                var food = AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_MACHINE_LOCATION",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        "DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",
                        "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT_OPTIONAL",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",

                        #endregion

                        #region PERMIT

                        "DOC_APP_DANGER_CURRENT_LICENSE",
                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "COMMITTEE_WORK_PERMIT_NO_CON",
                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",

                        #endregion

                        #region INFORMATION STORE
                        "BUILDING_USING_AREA_CONFIRM_AGENT",
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_MAP",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD",

                        #endregion

                        #region OTHER

                        "DOC_APP_DANGER_REQUEST_EDIT_LICENSE",
                        "DANGER_FOOD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_OTHER_DOC",
                        "DANGER_FOOD_ACTIVITY_PERMIT",
                        "DANGER_FOOD_ENV_QUALITY",
                        "DANGER_FOOD_MEDICAL_CERT",
                        "DANGER_ALL_STORE_PHOTO",
                        "DANGER_ALL_POLUTION_CONTROL_CHART",
                        "DANGER_ALL_HEALTH_CONTROL_CHART",
                        "DANGER_ALL_SEFTY_CONTROL_CHART",

                        #endregion

                    }
                });
            }

            for (int i = 0; i < AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL.Length; i++)
            {
                var food = AppSystemNameTextConst.ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL[i];
                items.Add(new SingleFormAppFile()
                {
                    AppSysName = food,
                    Files = {

                        #region JURUSTIC

                        
                        "JURISTIC_COMMITTEE_ID_CARD_NO_CON",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY_NO_CON",
                        "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT",
                        "COMMITTEE_WORK_PERMIT_NO_CON",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region CITIZEN

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC_NO_CON",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD_NO_CON",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_NO_CON",
                        #endregion

                        #region INFORMATION STORE

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",

                        #endregion

                        #region OTHER

                        "DOC_APP_DANGER_REQUEST_CANCEL_LICENSE",
                        "DANGER_FOOD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_INSTEAD_LICENSE_BAD_HEALTH",
                        "DANGER_FOOD_PRODUCTION_FLOWCHART",

                        #endregion

                    }
                });
            }

            #endregion

            #endregion

            db.InsertMany(items);
        }

        public static void InitForSoftwareHouse(IMongoCollection<SingleFormAppFile> db)
        {
            var items = new List<SingleFormAppFile>
            {
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW,
                    Files =
                    {
                        #region Citizen
                        //Committee Doc
                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        #region Store Information
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        #endregion

                        #region Software house
                        "SYSTEM_STRUCTURE_DOC",
                        "SOFTWARE_MANUAL",
                        #endregion
                        
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SOFTWARE_HOUSE_RENEW,
                    Files =
                    {
                        #region Citizen
                        //Committee Doc
                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion
                        
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT,
                    Files =
                    {
                        #region Citizen
                        //Committee Doc
                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_WORKPERMIT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        #region Store Information
                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        #endregion

                        #region Software house
                        "SYSTEM_STRUCTURE_DOC_EDIT",
                        "SOFTWARE_MANUAL_EDIT",
                        #endregion
                        
                    },
                },
            };

            db.InsertMany(items);
        }

        public static void InitForClinic(IMongoCollection<SingleFormAppFile> db)
        {
            
            var items = new List<SingleFormAppFile>
            {
                #region APP CLINIC

                #region [NEW]
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_CLINIC,
                    Files =
                    {

                        #region Citizen

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "MED_ANIMAL_CITIZEN_CERT",

                        #region Citizen Authorization
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                      
                         #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion
                        #region Information Store

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_HOUSEHOLD_RENT",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        "INFORMATION_STORE_BUILDING_DOC",

                        #endregion

                        #region CLINIC DOC FILE

                        "APP_CLINIC_OWNED_ID_CARD",
                        "APP_CLINIC_OWNED_HOUSEHOLD",
                        "APP_CLINIC_OWNED_MED_CERT",
                        "APP_CLINIC_OWNED_PHOTO3M",
                        "APP_CLINIC_OWNED_PHOTO13M",
                        "APP_CLINIC_OWNED_TRADITIONAL_CERT",
                        "APP_CLINIC_OWNED_DIPLOMA",
                        "APP_CLINIC_PERMISSION_ID_CARD",
                        "APP_CLINIC_LICENSE_LOCATION_PHOTO",
                        "APP_CLINIC_OWNED_PLAN_INSIDE",
                        "APP_CLINIC_OWNED_LOCATION_MAP",
                        "APP_CLINIC_VIDEO_FOR_CONSIDERATION",

                        #endregion

                        #endregion
                    },
                },
                #endregion

                #region [RENEW]
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_CLINIC_RENEW,
                    Files =
                    {
                        #region Form Doc
                        "APP_CLINIC_RENEW_BUSSINESS_REQ_DOC",
                        "APP_CLINIC_RENEW_PROCESS_REQ_DOC",
                        #endregion  

                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "MED_ANIMAL_JURISTIC_CERT",

                        #endregion

                        #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "MED_ANIMAL_CITIZEN_CERT",
                        "APP_CLINIC_RENEW_NAME_CHANGE_COPY",
                        "APP_CLINIC_RENEW_DOCTOR_SMARTCARD_COPY",
                        //"APP_CLINIC_RENEW_OWNED_TRADITIONAL_CERT",
                        #endregion
                        
                        #region Citizen Authorization
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_ID_CARD_MULTIPLE",
                        #endregion

                        #region Store Information
                        //"INFORMATION_STORE_BUILDING_OWNER_DOC",
                        //"INFORMATION_STORE_HOUSEHOLD_RENT",
                        //"INFORMATION_STORE_RENTAL_CONTRACT",
                        //"INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        //"INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        //"INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        //"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        //"INFORMATION_STORE_BUILDING_DOC",
                        #endregion

                        #region Clinic Doc
                        //"CLINIC_VERIFY_DOC",
                        //"CLINIC_PERSONAL_TABLE",
                        //"CLINIC_PROFILE_IMG",
                        //"APP_CLINIC_RENEW_OWNED_ID_CARD",
                        //"APP_CLINIC_RENEW_OWNED_HOUSEHOLD",
                        //"APP_CLINIC_RENEW_OWNED_MED_CERT",
                        "APP_CLINIC_RENEW_OWNED_PHOTO3M",
                        "APP_CLINIC_RENEW_CLINIC_OPERATION_CERTIFICATION",
                        //"APP_CLINIC_RENEW_OWNED_PHOTO13M",
                        //"APP_CLINIC_RENEW_OWNED_DIPLOMA",
                        "APP_CLINIC_RENEW_PERMISSION_ID_CARD",
                        #endregion
                    },
                },
                #endregion

                #region [EDIT]
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_CLINIC_EDIT,
                    Files =
                    {
                        #region Form Doc
                        //"APP_CLINIC_EDIT_BUSSINESS_REQ_DOC",
                        //"APP_CLINIC_EDIT_PROCESS_REQ_DOC",
                        #endregion 

                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_CHANGE_NAME_DOCUMENT",

                        #endregion

                        #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_RENAME_DOC",
                        #endregion

                        #region Citizen Authorization
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_ID_CARD_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        #endregion

                        //#region Clinic Doc
                        //"CLINIC_EDIT_DOC",
                        //"CLINIC_OWNER_ID_CARD",
                        //"CLINIC_OWNER_HOUSEHOLD_REGISTRATION_COPY",
                        //"CLINIC_OWNER_MEDICAL_CERT",
                        //"CLINIC_OWNER_PROFILE_IMG",
                        //"CLINIC_OWNER_LICENSE",
                        //"CLINIC_OWNER_CERT",
                        "APP_CLINIC_OWNED_EDIT_ID_CARD_CON",
                        "APP_CLINIC_OWNED_EDIT_HOUSEHOLD_CON",
                        "APP_CLINIC_OWNED_EDIT_MED_CERT",
                        "APP_CLINIC_OWNED_EDIT_PHOTO3M",
                        //"APP_CLINIC_OWNED_EDIT_PHOTO13M",
                        "APP_CLINIC_OWNED_EDIT_TRADITIONAL_CERT",
                        "APP_CLINIC_OWNED_EDIT_DIPLOMA",
                        "APP_CLINIC_EDIT_DOC",
                        #endregion
                    },
                },
                #endregion

                #region [CANCEL]
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_CLINIC_CANCEL,
                    Files =
                    {
                        #region Form Doc
                        //"APP_CLINIC_CANCEL_BUSSINESS_REQ_DOC",
                        #endregion 
                        
                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",

                        #endregion

                        #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region Citizen Authorization
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_ID_CARD_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        #endregion

                        #region CLINIC CANCEL DOC
                        "APP_CLINIC_CANCEL_DETAIL_DOC",
                        "APP_CLINIC_CANCEL_CANCEL_DETAIL",
                        "APP_CLINIC_CANCEL_FILM_OTHER",
                        #endregion
                    },
                },
                #endregion
                

                #region APP HOSPITAL PERMISSION

                #region [NEW]

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HOSPITAL_PERMISSION,
                    Files =
                    {

                        #region Citizen

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "MED_ANIMAL_CITIZEN_CERT",

                        #region Citizen Authorization
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_ALL_COMMITTEE_MEDICAL_CERT",
                        "JURISTIC_ALL_COMMITTEE_ID_CARD",
                        "JURISTIC_ALL_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_ALL_COMMITTEE_PROPERTY_17",

                         #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #endregion

                        #region Information Store

                        //"INFORMATION_STORE_BUILDING_OWNER_DOC",
                        //"INFORMATION_STORE_HOUSEHOLD_RENT",
                        //"INFORMATION_STORE_RENTAL_CONTRACT",
                        //"INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        //"INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        //"INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        //"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        //"INFORMATION_STORE_MAP",

                        #endregion

                        #region HOSPITAL DOC
                        "HOSPITAL_PERMISSION_NEW_STORE_MAP",
                        "HOSPITAL_PERMISSION_NEW_WEATHER",
                        "HOSPITAL_PERMISSION_BUILDING_NEW",
                        "HOSPITAL_PERMISSION_BUILDING_NEW_VERIFICATION",
                        "HOSPITAL_PERMISSION_NEW_DRAINAGE",
                        "HOSPITAL_PERMISSION_NEW_EMERGENCY",
                        "HOSPITAL_PERMISSION_NEW_WAY_PLAN",
                        "HOSPITAL_PERMISSION_NEW_TOOLS_PLAN",
                        "HOSPITAL_PERMISSION_NEW_FORM_DOC",

                        "HOSPITAL_PERMISSION_NEW_OWNER_ID",
                        "HOSPITAL_PERMISSION_NEW_OWNER_HOUSEHOLD",
                        "HOSPITAL_PERMISSION_NEW_MED_CERT",
                        "HOSPITAL_PERMISSION_NEW_PHOTO",
                        "HOSPITAL_PERMISSION_NEW_LICENSE",
                        "HOSPITAL_PERMISSION_NEW_DIPLOMA",

                        "HOSPITAL_PERMISSION_NEW_CONFIDENT_DOC",
                        "HOSPITAL_PERMISSION_NEW_PERSEONNEL_TABLE",
                        #endregion


                    },
                },
                #endregion

                #region APP_HOSPITAL_BUSINESS
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HOSPITAL_BUSINESS,
                    Files = 
                    {
                        #region Citizen

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "MED_ANIMAL_CITIZEN_CERT",

                        #region Citizen Authorization
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_ALL_COMMITTEE_MEDICAL_CERT",
                        "JURISTIC_ALL_COMMITTEE_ID_CARD",
                        "JURISTIC_ALL_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_ALL_COMMITTEE_PROPERTY_17",

                         #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #endregion

                        #region Information Store
                        "HOSPITAL_PERMISSION_NEW_STORE_MAP",
                        "HOSPITAL_PERMISSION_NEW_WEATHER",
                        //"HOSPITAL_PERMISSION_BUILDING_NEW",
                        "HOSPITAL_PERMISSION_BUILDING_NEW_VERIFICATION",
                        "HOSPITAL_PERMISSION_NEW_DRAINAGE",
                        "HOSPITAL_PERMISSION_NEW_EMERGENCY",
                        "HOSPITAL_PERMISSION_NEW_WAY_PLAN",
                        "HOSPITAL_PERMISSION_NEW_TOOLS_PLAN",
                        //"HOSPITAL_PERMISSION_NEW_FORM_DOC",
                        #endregion

                        #region CustomFile
                        "APP_HOSPITAL_BUSINESS_CUSTOM"
                        #endregion
                    }
                },
                #endregion

                #region APP_HOSPITAL_OPERATION
                
                new SingleFormAppFile()
                { 
                    AppSysName = AppSystemNameTextConst.APP_HOSPITAL_OPERATION,
                    Files = {

                        #region Citizen

                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "MED_ANIMAL_CITIZEN_CERT",

                        #region Citizen Authorization
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_ALL_COMMITTEE_MEDICAL_CERT",
                        "JURISTIC_ALL_COMMITTEE_ID_CARD",
                        "JURISTIC_ALL_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_ALL_COMMITTEE_PROPERTY_17",

                         #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY",
                        "AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #endregion

                        #region Hospital
                        
                        "HOSPITAL_PERMISSION_NEW_PHOTO",
                        "HOSPITAL_PERMISSION_NEW_PHOTO_BIG",
                        "HOSPITAL_PERMISSION_NEW_LICENSE",
                        "HOSPITAL_PERMISSION_NEW_DIPLOMA"
                        #endregion

                    }
                },

                #endregion

                #region [RENEW]
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_RENEW,
                    Files =
                    {
                        #region Form Doc
                        //"APP_CLINIC_RENEW_BUSSINESS_REQ_DOC",
                        //"APP_CLINIC_RENEW_PROCESS_REQ_DOC",
                        #endregion  

                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "MED_ANIMAL_JURISTIC_CERT",

                        #endregion

                        #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "MED_ANIMAL_CITIZEN_CERT",
                        #endregion

                        #region Citizen Authorization
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_ID_CARD_MULTIPLE",
                        #endregion

                        #region Store Information
                        "INFORMATION_STORE_BUILDING_OWNER_DOC_NO_CON",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD_NO_CON",
                        "INFORMATION_STORE_RENTAL_CONTRACT_NO_CON",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT_NO_CON",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION_NO_CON",
                        "INFORMATION_STORE_BUILDING_DOC_NO_CON",
                        "INFORMATION_STORE_HOUSEHOLD_RENT_NO_CON",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_OPTIONAL",
                        #endregion

                        #region Hospital Permission Doc
                        "HOSPITAL_PERMISSION_VERIFY_DOC",
                        "HOSPITAL_PERMISSION_PERSONAL_TABLE",
                        "HOSPITAL_PERMISSION_ARCHITECTURAL_PLAN",
                        "HOSPITAL_PERMISSION_AREA_DETAIL",
                        "HOSPITAL_PERMISSION_BUILDING_PERMIT_1",
                        "HOSPITAL_PERMISSION_BUILDING_PERMIT_6",
                        "HOSPITAL_PERMISSION_XRAY_PERMIT",
                        "HOSPITAL_PERMISSION_STATISTIC_OF_POLLUTION",
                        "HOSPITAL_PERMISSION_REPORT_OF_POLLUTION",
                        "HOSPITAL_PERMISSION_CALIBRATION_DOC",
                        "HOSPITAL_PERMISSION_AMBULANCE_PERMIT",
                        "HOSPITAL_PERMISSION_BUILDING_VERIFY_CERT",
                        "HOSPITAL_PERMISSION_EIA_HIA_REPORT",

                        "HOSPITAL_PERMISSION_PROFILE_IMG",
                        #endregion
                    },
                },
                #endregion

                #region [EDIT]
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_EDIT,
                    Files =
                    {
                        #region Form Doc
                        //"APP_CLINIC_EDIT_BUSSINESS_REQ_DOC",
                        //"APP_CLINIC_EDIT_PROCESS_REQ_DOC",
                        #endregion 

                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_CHANGE_NAME_DOCUMENT",

                        #endregion

                        #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_RENAME_DOC",
                        #endregion

                        #region Citizen Authorization
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_ID_CARD_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        #endregion

                        #region Hospital Permission Doc
                        "HOSPITAL_PERMISSION_EDIT_DOC",

                        "HOSPITAL_PERMISSION_OWNER_ID_CARD",
                        "HOSPITAL_PERMISSION_OWNER_HOUSEHOLD_REGISTRATION_COPY",
                        "HOSPITAL_PERMISSION_OWNER_MEDICAL_CERT",
                        "HOSPITAL_PERMISSION_OWNER_PROFILE_IMG",
                        "HOSPITAL_PERMISSION_OWNER_LICENSE",
                        "HOSPITAL_PERMISSION_OWNER_CERT",
                        #endregion
                    },
                },
                #endregion

                #region [CANCEL]
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HOSPITAL_PERMISSION_CANCEL,
                    Files =
                    {
                        #region Form Doc
                        //"APP_CLINIC_CANCEL_BUSSINESS_REQ_DOC",
                        #endregion 
                        
                        #region JURISTIC
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",

                        #endregion

                        #region Juristic Authorization
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        #endregion

                        #region CITIZEN
                        "ID_CARD_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region Citizen Authorization
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_ID_CARD_MULTIPLE",
                        "CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY_MULTIPLE",
                        #endregion

                        #region Hospital Permission Doc
                        "HOSPITAL_PERMISSION_RESIDUAL_PATIENT_REPORT",
                        "HOSPITAL_PERMISSION_CANCEL_BUSSINESS_TO_MEDIA_REPORT",
                        "HOSPITAL_PERMISSION_MEDICAL_RECORDS_NON_RECEIVE_REPORT",
                        "HOSPITAL_PERMISSION_FORWARD_PATIENT_CONTRACT",
                        #endregion
                    },
                },
                #endregion

                #endregion

                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_CLINIC_BUSINESS,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "MED_ANIMAL_CITIZEN_CERT",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        #region Juris
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region Information Store
                        "INFORMATION_STORE_BUILDING_OWNER_DOC_NO_CON",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region Clinic Dog File
                        //"APP_CLINIC_VIDEO_FOR_CONSIDERATION_CUZ",
                        "APP_CLINIC_LICENSE_LOCATION_PHOTO_CUSTOM",
                        "HOSPITAL_STORE_CUSTOM",
                        "APP_CLINIC_OWNED_LOCATION_MAP_CUSTOM"
                        #endregion

                    }
                }

                ,new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_CLINIC_OPERATION,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "MED_ANIMAL_CITIZEN_CERT",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        #endregion

                        #region Juris
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region Information Store
                        "INFORMATION_STORE_BUILDING_OWNER_DOC_NO_CON",
                        "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        #endregion

                        #region Clinic Dog File
                        "APP_CLINIC_OPERATION_A",
                        "APP_CLINIC_OPERATION_B",
                        "APP_CLINIC_OPERATION_C",
                        "APP_CLINIC_OPERATION_D",
                        "APP_CLINIC_OPERATION_E",
                        "APP_CLINIC_OPERATION_F"
                        #endregion

                    }
                }

                ,new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_CLINIC_BUSINESS_RENEW, 
                    Files = { 

                        // Citizen
                        //"ID_CARD",
                        "ID_CARD_COPY",
                        "HOUSE_REGISTARTION",
                        //"CITIZEN_RENAME_DOC",
                        "CITIZEN_RENAME_DOC_REAL",

                        // Juristict
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                        // Sec
                        "APP_CLINIC_BUSINESS_RENEW_DOCA",
                        "APP_CLINIC_BUSINESS_RENEW_DOCB",
                        "APP_CLINIC_BUSINESS_RENEW_DOCC",
                        "APP_CLINIC_BUSINESS_RENEW_DOCD",
                        "APP_CLINIC_BUSINESS_RENEW_DOCE",
                        //#region JURISTIC
                        //"CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        //"JURISTIC_COMMITTEE_ID_CARD",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        //"MED_ANIMAL_JURISTIC_CERT",

                        //#endregion

                        //#region Juristic Authorization
                        //"JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        //#endregion

                        //#region CITIZEN
                        //"ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        //"MED_ANIMAL_CITIZEN_CERT",
                        //#endregion

                        //#region Citizen Authorization
                        //"CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_MULTIPLE",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZEE_ID_CARD_MULTIPLE",
                        //#endregion

                        //#region Store Information
                        ////"INFORMATION_STORE_BUILDING_OWNER_DOC",
                        //"INFORMATION_STORE_HOUSEHOLD_RENT",
                        //"INFORMATION_STORE_RENTAL_CONTRACT",
                        //"INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        //"INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        //"INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        //"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        //"INFORMATION_STORE_BUILDING_DOC",
                        //#endregion

                        //#region Clinic Doc
                        //"APP_CLINIC_BUSINESS_RENEW_A",
                        ////"APP_CLINIC_BUSINESS_RENEW_B",
                        ////"APP_CLINIC_BUSINESS_RENEW_C",
                        ////"APP_CLINIC_BUSINESS_RENEW_D",
                        ////"APP_CLINIC_BUSINESS_RENEW_E",
                        ////"APP_CLINIC_BUSINESS_RENEW_F",
                        ////"APP_CLINIC_BUSINESS_RENEW_G",
                        ////"APP_CLINIC_BUSINESS_RENEW_H",
                        ////"APP_CLINIC_BUSINESS_RENEW_I",
                        ////"APP_CLINIC_BUSINESS_RENEW_J",
                        ////"APP_CLINIC_BUSINESS_RENEW_K",
                        ////"APP_CLINIC_BUSINESS_RENEW_L",
                        ////"APP_CLINIC_BUSINESS_RENEW_M",
                        //"APP_CLINIC_BUSINESS_RENEW_N",
                        //"APP_CLINIC_BUSINESS_RENEW_O"
                        //#endregion
                    }
                }

                ,new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_CLINIC_OPERATION_RENEW,
                    Files = { 

                        // Citizen
                        "ID_CARD_COPY",
                        "MEDICINE_SMART_CARD_ID",
                        //"CITIZEN_RENAME_DOC",
                        "CITIZEN_RENAME_DOC_REAL",
                        "MEDICINE_CERTIFICATE",

                        // Juristict
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                        // Sec
                        //"APP_CLINIC_OPERATION_RENEW_DOCA",
                        //"APP_CLINIC_OPERATION_RENEW_DOCB",
                        //"APP_CLINIC_OPERATION_RENEW_DOCC",
                        //"APP_CLINIC_OPERATION_RENEW_DOCD",

                        "APP_CLINIC_OPERATION_RENEW_DOCE",
                        "APP_CLINIC_OPERATION_RENEW_DOCF",
                        "APP_CLINIC_OPERATION_RENEW_DOCG",
                        "APP_CLINIC_OPERATION_RENEW_DOCH",
                        //#region JURISTIC
                        //"CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        //"JURISTIC_COMMITTEE_ID_CARD",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        ////"MED_ANIMAL_JURISTIC_CERT",

                        //#endregion

                        //#region Juristic Authorization
                        //"JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        //#endregion

                        //#region CITIZEN
                        //"ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        ////"MED_ANIMAL_CITIZEN_CERT",
                        //#endregion

                        //#region Citizen Authorization
                        //"CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_MULTIPLE",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZEE_ID_CARD_MULTIPLE",
                        //#endregion

                        //#region Store Information
                        //"INFORMATION_STORE_BUILDING_OWNER_DOC",
                        //"INFORMATION_STORE_HOUSEHOLD_RENT",
                        //"INFORMATION_STORE_RENTAL_CONTRACT",
                        //"INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        //"INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        //"INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        //"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY",
                        //"INFORMATION_STORE_BUILDING_DOC",
                        //#endregion

                        //#region Clinic Doc
                        //"APP_CLINIC_OPERATION_RENEW_A",
                        //"APP_CLINIC_OPERATION_RENEW_B",
                        //"APP_CLINIC_OPERATION_RENEW_C",
                        //"APP_CLINIC_OPERATION_RENEW_D",
                        //#endregion

                    }
                }

                ,new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HOSPITAL_BUSINESS_RENEW,
                    Files = { 
                        
                        // Citizen
                        "ID_CARD",
                        "HOUSE_REGISTARTION",

                        // Juristict
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                        // Sec
                        "APP_HOSPITAL_BUSINESS_RENEW_DOCA",
                        "APP_HOSPITAL_BUSINESS_RENEW_DOCB",
                        "APP_HOSPITAL_BUSINESS_RENEW_DOCC",
                        "APP_HOSPITAL_BUSINESS_RENEW_DOCD",
                        "APP_HOSPITAL_BUSINESS_RENEW_DOCE",
                        "APP_HOSPITAL_BUSINESS_RENEW_DOCF",
                        "APP_HOSPITAL_BUSINESS_RENEW_DOCG",
                        "APP_HOSPITAL_BUSINESS_RENEW_DOCH",
                        "APP_HOSPITAL_BUSINESS_RENEW_DOCI",

                        #region JURISTIC
                        //"CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        //"JURISTIC_COMMITTEE_ID_CARD",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        //"MED_ANIMAL_JURISTIC_CERT",

                        #endregion

                        #region Juristic Authorization
                        //"JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        #endregion

                        #region CITIZEN
                        //"ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        //"MED_ANIMAL_CITIZEN_CERT",
                        #endregion

                        #region Citizen Authorization
                        //"CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_MULTIPLE",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZEE_ID_CARD_MULTIPLE",
                        #endregion

                        #region Store Information
                        //"INFORMATION_STORE_BUILDING_OWNER_DOC_NO_CON",
                        //"INFORMATION_STORE_BUILDING_OWNER_ID_CARD_NO_CON",
                        //"INFORMATION_STORE_RENTAL_CONTRACT_NO_CON",
                        //"INFORMATION_STORE_BUILDING_USAGE_AGREEMENT_NO_CON",
                        //"INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION_NO_CON",
                        //"INFORMATION_STORE_BUILDING_DOC_NO_CON",
                        //"INFORMATION_STORE_HOUSEHOLD_RENT_NO_CON",
                        //"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_OPTIONAL",
                        #endregion

                        #region APP_HOSPITAL_BUSINESS_RENEW
                        //"APP_HOSPITAL_BUSINESS_RENEW_DOC_A",
                        //"APP_HOSPITAL_BUSINESS_RENEW_DOC_B",
                        //"APP_HOSPITAL_BUSINESS_RENEW_DOC_C",
                        //"APP_HOSPITAL_BUSINESS_RENEW_DOC_D",
                        //"APP_HOSPITAL_BUSINESS_RENEW_DOC_E",
                        //"APP_HOSPITAL_BUSINESS_RENEW_DOC_F",
                        //"APP_HOSPITAL_BUSINESS_RENEW_DOC_G",
                        //"APP_HOSPITAL_BUSINESS_RENEW_DOC_H",
                        //"APP_HOSPITAL_BUSINESS_RENEW_DOC_I",
                        //"APP_HOSPITAL_BUSINESS_RENEW_DOC_J",
                        //"APP_HOSPITAL_BUSINESS_RENEW_DOC_K",
                        //"APP_HOSPITAL_BUSINESS_RENEW_DOC_L",
                        //"APP_HOSPITAL_BUSINESS_RENEW_DOC_M",
                        #endregion

                    }
                }

                ,new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_HOSPITAL_OPERATION_RENEW,
                    Files = { 
                        
                        // Citizen
                        "ID_CARD_COPY",
                        "MEDICINE_SMART_CARD_ID",
                        "CITIZEN_RENAME_DOC_REAL",
                        "MEDICINE_CERTIFICATE",

                        // Juristict
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",

                        // Sec
                        //"APP_HOSPITAL_OPERATION_RENEW_DOCA",
                        //"APP_HOSPITAL_OPERATION_RENEW_DOCB",
                        //"APP_HOSPITAL_OPERATION_RENEW_DOCC",
                        //"APP_HOSPITAL_OPERATION_RENEW_DOCD",
                        //"APP_HOSPITAL_OPERATION_RENEW_DOCE",

                        "APP_HOSPITAL_OPERATION_RENEW_DOCF",                      
                        "APP_HOSPITAL_OPERATION_RENEW_DOCG",                      
                        "APP_HOSPITAL_OPERATION_RENEW_DOCH",
                        "APP_HOSPITAL_OPERATION_RENEW_DOCJ",
                        "APP_HOSPITAL_OPERATION_RENEW_DOCI",                      

                        //#region JURISTIC
                        //"CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        //"JURISTIC_COMMITTEE_ID_CARD",
                        //"JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        //"MED_ANIMAL_JURISTIC_CERT",

                        //#endregion

                        //#region Juristic Authorization
                        //"JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        //"JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        //#endregion

                        //#region CITIZEN
                        //"ID_CARD_COPY",
                        //"CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        //"MED_ANIMAL_CITIZEN_CERT",
                        //#endregion

                        //#region Citizen Authorization
                        //"CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_MULTIPLE",
                        //"CITIZEN_AUTHORIZATION_AUTHORIZEE_ID_CARD_MULTIPLE",
                        //#endregion

                        //#region APP_HOSPITAL_OPERATION_RENEW
                        
                        //"APP_HOSPITAL_OPERATION_RENEW_DOC_A",
                        //"APP_HOSPITAL_OPERATION_RENEW_DOC_B",
                        //"APP_HOSPITAL_OPERATION_RENEW_DOC_C",
                        //"APP_HOSPITAL_OPERATION_RENEW_DOC_D",

                        //#endregion

                    }
                }
            };    

            db.InsertMany(items);
        }

        public static void InitForVeterinaryHospital(IMongoCollection<SingleFormAppFile> db)
        {
            var items = new List<SingleFormAppFile>
            {
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ANIMAL_BUILD,
                    Files =
                    {
                        "ID_CARD_COPY",
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                        "CITIZEN_RENAME_DOC",
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                        "MED_ANIMAL_JURISTIC_CERT",
                        "MED_ANIMAL_CITIZEN_CERT",

                        "JURISTIC_COMMITTEE_ID_CARD",
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
                        "JURISTIC_COMMITTEE_FOREIGNER_BUSINESS_CERT",
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",

                        "INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "INFORMATION_STORE_BUILDING_OWNER_ID_CARD",
                        "INFORMATION_STORE_RENTAL_CONTRACT",
                        "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "INFORMATION_STORE_PERMIT_OPEN_BUILD_OPTIONAL",


                        "ANIMAL_BUILD_STORE_MAP",
                        "ANIMAL_BUILD_STORE_PLAN_AREA",
                        "ANIMAL_BUILD_STORE_BUILDING_PLAN",
                        "ANIMAL_BUILD_STORE_NATURAL_DOC",
                        "ANIMAL_BUILD_BILL",
                        "ANIMAL_BUILD_XRAY_MACHINE",
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ANIMAL_BUILD_RENEW,
                    Files =
                    {

                            #region JURUSTIC

                            "JURISTIC_COMMITTEE_ID_CARD",
                            "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
                            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
                            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",

                            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",
                            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                            #endregion

                            #region CITIZEN

                            "ID_CARD_COPY",
                            "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
                            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
                            "AUTHORIZATION_AUTHORIZEE_ID_CARD",
                            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",

                            #endregion

                            #region OTHER

                            "APP_ANIMAL_BUILD_LICENSE",
                            "APP_ANIMAL_BUILD_PHOTO_STORE",

                            #endregion

                    },
                },
            };

            db.InsertMany(items);
        }

        public static void InitForOrganicPlantProduction(IMongoCollection<SingleFormAppFile> db)
        {
            var items = new List<SingleFormAppFile> 
            {
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_RENEW,
                    Files =
                    {
                        #region Citizen
                        "ID_CARD_COPY",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: บุคคลผู้ขออนุญาต (ชื่อ-นามสกุล)
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY", //ทะเบียนบ้าน: บุคคลผู้ขออนุญาต
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",  //หนังสือมอบอำนาจ
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้มอบอำนาจ
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ 
			            #endregion

			            #region Juristic
			            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",   //หนังสือรับรองการจดทะเบียนนิติบุคคล
                        "JURISTIC_COMMITTEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: กรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล: (ชื่อ-นามสกุล)
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",  //ทะเบียนบ้าน: กรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล: (ชื่อ-นามสกุล)
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",  //หนังสือมอบอำนาจ
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้มอบอำนาจ 
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ 
                        #endregion

                        //#region BUILDING STORE
                        //"INFORMATION_STORE_BUILDING_OWNER_DOC_OPTIONAL_NO_CON",  //เอกสารแสดงความเป็นเจ้าของอาคารสถานที่ที่ใช้เป็นร้าน/สถานประกอบการ เช่น ใบอนุญาตก่อสร้าง หรือสัญญาซื้อขาย
                        //"INFORMATION_STORE_RENTAL_CONTRACT_NO_CON",  //สัญญาเช่า 
                        //"INFORMATION_STORE_BUILDING_USAGE_AGREEMENT_NO_CON",  //หนังสือยินยอมให้ใช้อาคารสถานที่
                        //"INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION_OPTIONAL",  //หนังสือรับรองนิติบุคคล: ผู้ให้เช่า หรือยินยอมให้ใช้อาคารสถานที่
                        //"INFORMATION_STORE_HOUSEHOLD_RENT_NO_CON",  //ทะเบียนบ้าน: ผู้ให้เช่า หรือผู้ยืนยอมให้ใช้สถานที่
                        //"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_OPTIONAL",  //ทะเบียนบ้าน: อาคารที่ใช้เป็นที่ตั้งร้าน/สถานประกอบการ
                        //#endregion

                        #region SPECIFIC ORGANIC_PLANT_PRODUCTION_SECTION
                        ////กลุ่ม การขอใบรับรองแหล่งผลิตพืชอินทรีย์
                        //"ORGANIC_PLANT_PRODUCTION_CURRENT_CERTIFICATE",   //ใบรับรอง(ฉบับปัจจุบัน)
                        //"ORGANIC_PLANT_PRODUCTION_REQUEST_FORM_DOC",  //FORM SECTION
                        //"ORGANIC_PLANT_PRODUCTION_REQUEST_CER_DOC",  //FORM SECTION

                        #region Organic Doc
                        "ORGANIC_RENEW_LICENSE_PRESENT",
                        "ORGANIC_RENEW_DOC",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_1",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_2",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_3",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_4",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_5",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_6",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_7",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_8",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_9",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_FORM_INCLUDE_TYPE_10",
                        #region BUILDING STORE
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFORMATION_STORE_RENTAL_CONTRACT",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFORMATION_STORE_HOUSEHOLD_RENT",
                        "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFORMATION_STORE_HOUSEHOLD_STORE",
                        #endregion
                        #endregion
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_EDIT,
                    Files =
                    {
			            #region Citizen
			            "ID_CARD_COPY",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: บุคคลผู้ขออนุญาต (ชื่อ-นามสกุล)
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY", //ทะเบียนบ้าน: บุคคลผู้ขออนุญาต
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",  //หนังสือมอบอำนาจ
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้มอบอำนาจ
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ 
			            #endregion

			            #region Juristic
			            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",   //หนังสือรับรองการจดทะเบียนนิติบุคคล
                        "JURISTIC_COMMITTEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: กรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล: (ชื่อ-นามสกุล)
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",  //ทะเบียนบ้าน: กรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล: (ชื่อ-นามสกุล)
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",  //หนังสือมอบอำนาจ
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้มอบอำนาจ 
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ 
                        #endregion

                        //#region BUILDING STORE
                        //"INFORMATION_STORE_BUILDING_OWNER_DOC_OPTIONAL_NO_CON",  //เอกสารแสดงความเป็นเจ้าของอาคารสถานที่ที่ใช้เป็นร้าน/สถานประกอบการ เช่น ใบอนุญาตก่อสร้าง หรือสัญญาซื้อขาย
                        //"INFORMATION_STORE_RENTAL_CONTRACT_NO_CON",  //สัญญาเช่า 
                        //"INFORMATION_STORE_BUILDING_USAGE_AGREEMENT_NO_CON",  //หนังสือยินยอมให้ใช้อาคารสถานที่
                        //"INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION_OPTIONAL",  //หนังสือรับรองนิติบุคคล: ผู้ให้เช่า หรือยินยอมให้ใช้อาคารสถานที่
                        //"INFORMATION_STORE_HOUSEHOLD_RENT_NO_CON",  //ทะเบียนบ้าน: ผู้ให้เช่า หรือผู้ยืนยอมให้ใช้สถานที่
                        //"INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_OPTIONAL",  //ทะเบียนบ้าน: อาคารที่ใช้เป็นที่ตั้งร้าน/สถานประกอบการ
                        //#endregion

                        #region SPECIFIC - ORGANIC_PLANT_PRODUCTION_SECTION
                        //กลุ่ม การขอใบรับรองแหล่งผลิตพืชอินทรีย์
                        //"ORGANIC_PLANT_PRODUCTION_CURRENT_CERTIFICATE",   //ใบรับรอง(ฉบับปัจจุบัน)
                        //"ORGANIC_PLANT_PRODUCTION_CHANGED_DOC", //เอกสารที่มีการเปลี่ยนแปลง
                        //"ORGANIC_PLANT_PRODUCTION_REQUEST_FORM_DOC",
                        //"ORGANIC_PLANT_PRODUCTION_REQUEST_CER_DOC"
                        "ORGANIC_EDIT_DOC",
                        "ORGANIC_EDIT_LICENSE_PRESENT",
                        //"APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_1",
                        //"APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_2",
                        //"APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_3",
                        //"APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_4",
                        //"APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_5",
                        //"APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_6",
                        //"APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_7",
                        //"APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_8",
                        //"APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_9",
                        //"APP_ORGANIC_PLANT_PRODUCTION_EDIT_FORM_INCLUDE_TYPE_10",
                        #region BUILDING STORE
                        "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFORMATION_STORE_BUILDING_OWNER_DOC",
                        "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFORMATION_STORE_RENTAL_CONTRACT",
                        "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFORMATION_STORE_BUILDING_USAGE_AGREEMENT",
                        "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION",
                        "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFORMATION_STORE_HOUSEHOLD_RENT",
                        "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFORMATION_STORE_HOUSEHOLD_STORE",
                        #endregion
                        #endregion
                    },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_CANCEL,
                    Files =
                    {
			            #region Citizen
			            "ID_CARD_COPY",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: บุคคลผู้ขออนุญาต (ชื่อ-นามสกุล)
                        "CITIZEN_HOUSEHOLD_REGISTRATION_COPY", //ทะเบียนบ้าน: บุคคลผู้ขออนุญาต
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",  //หนังสือมอบอำนาจ
                        "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้มอบอำนาจ
                        "AUTHORIZATION_AUTHORIZEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ 
			            #endregion

			            #region Juristic
			            "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",   //หนังสือรับรองการจดทะเบียนนิติบุคคล
                        "JURISTIC_COMMITTEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: กรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล: (ชื่อ-นามสกุล)
                        "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",  //ทะเบียนบ้าน: กรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล: (ชื่อ-นามสกุล)
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",  //หนังสือมอบอำนาจ
                        "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้มอบอำนาจ 
                        "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ 
                        #endregion

                        #region SPECIFIC - ORGANIC_PLANT_PRODUCTION_SECTION
                        //กลุ่ม การขอใบรับรองแหล่งผลิตพืชอินทรีย์
                        //"ORGANIC_PLANT_PRODUCTION_CURRENT_CERTIFICATE",   //ใบรับรอง(ฉบับปัจจุบัน)
                        //"ORGANIC_PLANT_PRODUCTION_FORM_CANCEL"  //FORM SECTION
                        "ORGANIC_CANCEL_LICENSE_PRESENT",
                        #endregion
			
		            },
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_SPA_FEE_PER_YEAR,
                    Files = 
                    {
                        #region Citizen
                        "ID_CARD_COPY",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: บุคคลผู้ขออนุญาต (ชื่อ-นามสกุล)
                        "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",  //หนังสือมอบอำนาจ
                        "CITIZEN_RENAME_DOC",//ใบสำคัญการเปลี่ยนชื่อ
                        #endregion

                        #region Juristic
                        "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",   //หนังสือรับรองการจดทะเบียนนิติบุคคล
                        "JURISTIC_SHARE_HOLDER_LIST", //บัญชีรายชื่อผู้ถือหุ้น
                        "DIRECT_MARKETING_EDIT_JURISTIC2",//หนังสือบริคณห์สนธิ
                        "JURISTIC_COMMITTEE_ID_CARD",  //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: กรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล: (ชื่อ-นามสกุล)
                        "JURISTIC_COMMITTEE_CHANGE_NAME_DOCUMENT", //ใบสำคัญการเปลี่ยนชื่อ
                        "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",  //หนังสือมอบอำนาจ
                        #endregion

                        #region Dup
                        "SPA_OLD_PERMIT"//ใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ
                        #endregion


                    },                   
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_CLINIC_NOT_ONE_NIGHT_STAND,
                    Files =
                    {
                        "CLINIC_DOC_1",
                        "CLINIC_DOC_2"
                    }
                },
                new SingleFormAppFile()
                {
                    AppSysName = AppSystemNameTextConst.APP_CLINIC_OVER_NIGHT,
                    Files =
                    {
                        "CLINIC_OVER_NIGHT_DOC_1",
                    }
                },               
            };
            db.InsertMany(items);
        }

        public SingleFormAppFile()
        {
            Files = new List<string>();
        }

        public string AppSysName { get; set; }
        public List<string> Files { get; set; }
    }
}
