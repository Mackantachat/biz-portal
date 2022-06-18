using BizPortal.DAL.MongoDB;
using BizPortal.Utils;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Apps;
using BizPortal.ViewModels.Apps.TFACStandard;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using EGA.WS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BizPortal.AppsHook
{
    public class  TFACAccountingEditAppHook : SingleFormRendererAppHook
    {

        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public override bool IsEnabledChat() => false;

        public override bool InvokeSingleForm(Guid trid, string currentSectionGroup, ref SingleFormRequestViewModel model)
        {
            var result = true;
            var response = (JObject)null;
            var responseModel = new TFACDataResponse();

            var currentSection = currentSectionGroup;





             var userData = new TFACDataGetRequest
            {
                data = new data()
                {
                    Registration_No = "0722559000028"
                }
            };
            var userDataString = JsonConvert.SerializeObject(userData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            //response = Api.Post(ConfigurationManager.AppSettings["TFAC_WS_URL_GETREQUEST"], null, ContentType.ApplicationJson);
            response =  Api.Call(ConfigurationManager.AppSettings["TFAC_WS_URL_GETREQUESTBYREGISNO"], HttpVerb.POST, null, userDataString, ContentType.ApplicationJson);


           
            if (response.HasValues)
            {

                //var jsonObject = new JObject();
                // you can explicitly add values here using class interface
                // jsonObject.Add("isSuccess", DateTime.Now);

                //  responseModel.ResultData = response.ToObject<ResultData>();


                dynamic json = response;

                // values require casting
                bool isSuccess = Convert.ToBoolean(json.isSuccess);
             

            }
            else
            {
                throw new Exception("ไม่พบข้อมูล");
            }


            //APP_ACCOUNTING_SERVICE_EDIT
            //if (currentSectionGroup == "APP_ACCOUNTING_DETAIL")
            //{
            //    if (model.IdentityType == UserTypeEnum.Juristic)
            //    {
            //        var generalInfo = GetSectionData(model, "APP_ACCOUNTING_SERVICE_INFO_SECTION", SectionType.Form);
            //        var contactInfo = GetSectionData(model, "APP_ACCOUNTING_DETAIL_CALCULATE_SECTION", SectionType.Form);

            //        contactInfo.FormData.AddOrUpdate("APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_FUND", generalInfo.FormData.TryGetString("APP_ACCOUNTING_SERVICE_INFO_SECTION_REGISTER_CAPITAL", ""));
            //    }

            //}

            if (currentSectionGroup == "INFORMATION")
            {
                if (model.IdentityType.Equals(UserTypeEnum.Juristic))
                {
                    //var JURISTIC_ADDRESS_INFO = GetSectionData(model, "JURISTIC_ADDRESS_INFORMATION", SectionType.Form);
                    //JURISTIC_ADDRESS_INFO.FormData = new Dictionary<string, object>
                    // {
                    //    { "ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.telephone},
                    //    { "ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.email},
                    //    { "ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.telephone_ext},
                    //    { "ADDRESS_FAX_JURISTIC_HQ_ADDRESS",cinfo.responseModel.owner.fax}
                    // };


                }

            }
            else if(currentSectionGroup == "APP_ACCOUNTING_SERVICE_EDIT")
            {

            }


                return result;
        }

        // <BIZBIZ> CODE FOR BIZ_BIZ  <BIZBIZ>
        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();


            try
            {
                switch (stage)
                {
                    case AppsStage.UserCreate:
                        //Mapping data here
                        var PersonList = new List<Person>();
                        //Get Request_Date
                        var request_date_type = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_INFO_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_INFO_SECTION_OPTION_OPTION");
                        var request_date = string.Empty;
                        if (request_date_type == "REGISTER_DATE")
                        {
                            request_date = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_INFO_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_INFO_SECTION_REGISTER_DATE_OPTION");
                        }
                        else if (request_date_type == "START_DATE")
                        {
                            request_date = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_INFO_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_INFO_SECTION_START_DATE_OPTION");
                        }
                        else if (request_date_type == "CHANGE_DATE")
                        {
                            request_date = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_INFO_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_INFO_SECTION_CHANGE_DATE_OPTION");
                        }

                        
                        var post = new TFACDataService()
                        {
                            data = new Data()
                            {
                                ApplicationRequest_ID = model.ApplicationRequestID.ToString(),
                                Registration_No = model.IdentityID,
                                Tax_ID = model.IdentityID,
                                Name_TH = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_TH"),
                                Name_EN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_EN"),
                                DBD_Registration_Date = TFACUtility.GetDateFormat(model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("REGISTER_DATE")),
                                Registration_Date = TFACUtility.GetDateFormat(model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("INFORMATION_HEADER__REQUEST_DATE")), //วันที่ลงทะเบียน
                                Business_Type_ID = TFACUtility.GetJulisticType().FirstOrDefault(x => x.Value == model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_INFORMATION__JURISTIC_TYPE")).Key.ToString(),
                                Request_Form_ID = "6", // 6 คือ แก้ไขข้อมูลนิติบุคคล

                                Corporate_Service_Type_ID = TFACUtility.GetCorporateServiceType().FirstOrDefault(x => x.Value == model.Data.TryGetData("APP_ACCOUNTING_SERVICE_INFO_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_INFO_SECTION_TYPE_OPTION")).Key.ToString(),
                                Capital = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_INFO_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_INFO_SECTION_REGISTER_CAPITAL"),


                               // Revenue_Year = "",
                                Fiscal_Year_End_Date = "",
                               // Accounting_Revenue = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_CALCULATE_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_ACCOUNTING_TOTAL_INCOME"),
                              //  Auditing_Revenue = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_CALCULATE_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_AUDITORING_TOTAL_INCOME"),
                                //Other_Revenue = 0,
                                Total_Revenue = Convert.ToDecimal(model.Data.TryGetData("APP_ACCOUNTING_DETAIL_CALCULATE_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_TOTAL_INCOME")),
                                Rate_Type = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_CALCULATE_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_REASON_TYPE_OPTION") == "FUND" ? "1" : "2", // 1 = FUND/ 2 = INCOME


                                Request_Date = TFACUtility.GetDateFormat(request_date), // วันที่ยืนคำขอ
                                Request_Date_type = TFACUtility.GetRequestDateType().FirstOrDefault(x => x.Value == model.Data.TryGetData("APP_ACCOUNTING_SERVICE_INFO_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_INFO_SECTION_OPTION_OPTION")).Key.ToString(),


                                Document_Receive_Date = TFACUtility.GetDateFormat(model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("INFORMATION_HEADER__REQUEST_DATE")),
                                Membership_Period = 1, //1 is default value
                                Fee = 2400, //Fee 2000 + ค่าแจ้งหลักประกัน 400
                                Is_First_Time_Guarantee = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DETAIL_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_DETAIL_SECTION_REASON_OPTION") == "FIRST_TIME" ? "1" : "2", // value -> 1 = FIRST_TIME/2 = NOT_FIRST_TIME
                                Is_Audited_Financial_Report = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_CALCULATE_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_CALCULATE_SECTION_STATEMENT_OPTION") == "BUDGET" ? "1" : "2"// value -> 1= BUDGET/2 = FORECAST
                            }
                        };

                        //ตรวจสอบการแก้ไขเงินทุนจดทะเบียน
                        if (model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION").ThenGetBooleanData("APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_REGISTERED_CHECKED_EDIT_AMOUNT_REGISTERED"))
                        {
                            post.data.Capital = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_AMOUNT_BATH");
                        }


                        //ตรวจสอบการแก้ไขประเภทการให้บิการ
                        if (model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION").ThenGetBooleanData("APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_SERVICE_TYPE_CHECKED_SERVICE_TYPE_EDIT"))
                        {
                            //TFACUtility.GetCorporateServiceType().FirstOrDefault(x => x.Value == model.Data.TryGetData("APP_ACCOUNTING_SERVICE_INFO_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_INFO_SECTION_TYPE_OPTION")).Key.ToString()                
                            post.data.Corporate_Service_Type_ID = TFACUtility.GetCorporateServiceType().FirstOrDefault(x => x.Value == model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_INFO_SECTION_SERVICE_TYPE_OPTION")).Key.ToString();
                        }
                      




                        #region[Address]
                        var addressList = new List<Address>();
                        //ที่อยู่ที่จดทะเบียนนิติบุคคล
                        var juristicAddress = new Address
                        {
                            Office_Name = "",
                            Address_Type_ID = "2", // ที่อยู่ปัจจุบัน
                            Building_Name = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS"),
                            Building_Room_No = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS"),
                            Floor = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS"),
                            Address_Number = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                            Moo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),
                            Village = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS"),
                            Soi = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"),
                            Street = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"),

                            Sub_District = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS"),
                            District = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS"),
                            Province = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS"),

                            Postcode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"),
                            Phone = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS"),
                            Fax = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_JURISTIC_HQ_ADDRESS"),
                            Mobile_Phone = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOBILE_JURISTIC_HQ_ADDRESS"),
                            E_Mail = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS"),
                            Is_Mail_Address = ""
                        };
                        addressList.Add(juristicAddress);
                        //ที่อยู่ร้าน สถานประกอบการ
                        var officeAddress = new Address
                        {
                            Office_Name = "",
                            Address_Type_ID = "3", // 3 คือที่อยู่ที่ทำงาน 
                            Building_Name = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"),
                            Building_Room_No = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS"),
                            Floor = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS"),
                            Address_Number = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS"),
                            Moo = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS"),
                            Village = string.Empty,
                            Soi = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS"),
                            Street = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"),
                            Sub_District = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS"),
                            District = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS"),
                            Province = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"),
                            Postcode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"),
                            Phone = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                            Fax = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS"),
                            Mobile_Phone = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOBILE_INFORMATION_STORE__ADDRESS"),
                            E_Mail = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS"),
                            Is_Mail_Address = string.Empty
                        };
                        addressList.Add(officeAddress);
                        post.data.Address = addressList;

                        #endregion

                        #region[Persons]
                        // ข้อมูลกรรมการผู้มีอำนาจลงนามผูกพันนิติบุคคล Committee COMMITTEE_INFORMATION     
                        var committeeTotal = int.Parse(model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("COMMITTEE_INFORMATION_TOTAL"));
                        if (committeeTotal > 0)
                        {
                            var committeeList = new List<Person>();
                            for (int i = 0; i < committeeTotal; i++)
                            {
                                var committee = new Person
                                {
                                    Person_Type_ID = (int)PersonType.Committee,
                                    ID_No = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_CITIZEN_ID_" + i),
                                    CPA_No = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_ACCOUNTING_LICENSE_ID_" + i),
                                    Accounting_No = "",//ไม่มี
                                    Title_TH = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("DROPDOWN_JURISTIC_COMMITTEE_TITLE_TEXT_" + i),
                                    First_Name_TH = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_NAME_" + i),
                                    Last_Name_TH = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_LASTNAME_" + i),
                                    Is_FullTime = 1,
                                    Is_Authorize = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION_" + i) == "yes" ? 1 : 0,
                                };
                                committeeList.Add(committee);
                            }
                            PersonList.AddRange(committeeList);
                        }

                       //ข้อมูลหัวหน้าสำนักงาน Manager APP_ACCOUNTING_SERVICE_MANAGER_SECTION
                       var Is_check_manager =  model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_CHECKED").ThenGetBooleanData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_CHECKED_MANAGER_EDIT_CHECKED_EDIT_MANAGER_SECTION");
                       if(Is_check_manager)
                       {
                            var managerTotal = int.Parse(model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_TOTAL"));

                            if (managerTotal > 0)
                            {
                                var managerList = new List<Person>();
                                for (int i = 0; i < managerTotal; i++)
                                {
                                    var manager = new Person
                                    {
                                        Person_Type_ID = (int)PersonType.Manager,
                                        ID_No = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_ID_" + i),
                                        CPA_No = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_AUDITOR_ID_" + i),
                                        Accounting_No = "",//ไม่มี
                                        Title_TH = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION").ThenGetStringData("DROPDOWN_APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_TITLE_TEXT_" + i),
                                        First_Name_TH = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_FIRSTNAME_" + i),
                                        Last_Name_TH = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_LASTNAME_" + i),
                                        Is_FullTime = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_MANAGER_SECTION_WORKING_TYPE_OPTION_" + i) == "FULLTIME" ? 1 : 0,
                                        Is_Authorize = 0, // 0 because manager section did not have this value
                                    };
                                    managerList.Add(manager);
                                }
                                PersonList.AddRange(managerList);
                            }

                        }


                       //ข้อมูลรายนามผู้ทำบัญชีที่รับผิดชอบในฐานะผู้ทำบัญชีของนิติบุคคล APP_ACCOUNTING_SERVICE_ACCOUTANT_SECTION                     
                       var Is_check_accounting = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_CHECKED").ThenGetBooleanData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_CHECKED_ACCOUNTANT_EDIT_CHECKED_EDIT_ACCOUNTANT_SECTION");
                       if(Is_check_accounting)
                        {
                            var accountantTotal = int.Parse(model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_TOTAL"));
                            if (accountantTotal > 0)
                            {
                                var accountantList = new List<Person>();
                                for (int i = 0; i < accountantTotal; i++)
                                {
                                    var accountant = new Person
                                    {
                                        Person_Type_ID = (int)PersonType.Accountant,
                                        ID_No = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_ID_" + i),
                                        CPA_No = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_AUDITOR_ID_" + i),
                                        Accounting_No = "", //ไม่มี
                                        Title_TH = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION").ThenGetStringData("DROPDOWN_APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_TITLE_TEXT_" + i),
                                        First_Name_TH = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_FIRSTNAME_" + i),
                                        Last_Name_TH = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_LASTNAME_" + i),
                                        Is_FullTime = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_ACCOUTANT_SECTION_WORKING_TYPE_OPTION_" + i) == "FULLTIME" ? 1 : 0,
                                        Is_Authorize = 0, // 0 because this accountant section did not have this value
                                    };
                                    accountantList.Add(accountant);
                                }
                                PersonList.AddRange(accountantList);
                            }
                        }




                        //ข้อมูลรายนามผู้สอบบัญชีรับอนุญาตที่ลงลายมือชื่อในฐานะผู้สอบบัญชีของนิติบุคคล APP_ACCOUNTING_SERVICE_AUDITOR_SECTION
                        var Is_check_AUDITOR = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_CHECKED").ThenGetBooleanData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_CHECKED_AUDITOR_EDIT_CHECKED_EDIT_AUDITOR_SECTION");
                        if(Is_check_AUDITOR)
                        {
                            var auditorTotal = int.Parse(model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_TOTAL"));
                            if (auditorTotal > 0)
                            {
                                var auditorList = new List<Person>();
                                for (int i = 0; i < auditorTotal; i++)
                                {
                                    var auditor = new Person
                                    {
                                        Person_Type_ID = (int)PersonType.Auditor,
                                        ID_No = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_ID_" + i),
                                        CPA_No = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_AUDITOR_ID_" + i),
                                        Accounting_No = "",//ไม่มี
                                        Title_TH = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION").ThenGetStringData("DROPDOWN_APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_TITLE_TEXT_" + i),
                                        First_Name_TH = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_FIRSTNAME_" + i),
                                        Last_Name_TH = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_LASTNAME_" + i),
                                        Is_FullTime = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION").ThenGetStringData("APP_ACCOUNTING_SERVICE_EDIT_AUDITOR_SECTION_WORKING_TYPE_OPTION_" + i) == "FULLTIME" ? 1 : 0,
                                        Is_Authorize = 0, // 0 because auditor section did not have this value
                                    };
                                    auditorList.Add(auditor);
                                }
                                PersonList.AddRange(auditorList);
                            }

                            post.data.Persons = PersonList;
                        }


                        #endregion


                       





                        #region[Guarantee]                    
                        var guaranteeList = new List<Guarantee>();

                        // พันธบัตรรัฐบาลไทย
                        var THAI_BONDS = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_PROVIDING_SECTION").ThenGetBooleanData("APP_ACCOUNTING_DETAIL_PROVIDING_SECTION_TYPE_THAI_BONDS");
                        if (THAI_BONDS == true)
                        {
                            var guaranteeList_thai_bonds = new List<Guarantee>();
                            var THAI_BONDS_Total = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_THAI_BONDS_SECTION").ThenGetIntData("APP_ACCOUNTING_DETAIL_THAI_BONDS_SECTION_TOTAL");

                            for (int i = 0; i < THAI_BONDS_Total; i++)
                            {
                                var quarantee = new Guarantee
                                {
                                    Guarantee_Type_ID = (int)GuaranteeType.THAI_BONDS,
                                    //เลขที่
                                    Bond_No = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_THAI_BONDS_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_THAI_BONDS_SECTION_NUMBER_" + i),
                                    //วันที่
                                    Bond_Date = TFACUtility.GetDateFormat(model.Data.TryGetData("APP_ACCOUNTING_DETAIL_THAI_BONDS_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_THAI_BONDS_SECTION_DATE_" + i)),
                                    //วันที่ครบกำหนด
                                    Bond_Due_Date = TFACUtility.GetDateFormat(model.Data.TryGetData("APP_ACCOUNTING_DETAIL_THAI_BONDS_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_THAI_BONDS_SECTION_DUE_DATE_" + i)),
                                    //จำนวนเงิน
                                    Amount = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_THAI_BONDS_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_THAI_BONDS_SECTION_AMOUNT_" + i),

                                    // พันธบัตรรัฐบาลไทย ไม่มีข้อมูลตามด้านล่าง
                                    Description = "",
                                    Bank_Year = "",
                                    Bank_ID = "",
                                    Bank_Name = "",
                                    Bank_Branch = "",
                                    Bank_Account_ID = "",
                                    Bank_Account_Name = "",
                                    Year_Number = "",
                                    Bond_Of = "", //พันธบัตรรัฐบาลไทยไม่มี Bond_Of
                                };
                                guaranteeList_thai_bonds.Add(quarantee);
                            }
                            guaranteeList.AddRange(guaranteeList_thai_bonds);
                        }

                        // บัญชีเงินฝากประจำ
                        var DEPOSIT = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_PROVIDING_SECTION").ThenGetBooleanData("APP_ACCOUNTING_DETAIL_PROVIDING_SECTION_TYPE_DEPOSIT");
                        if (DEPOSIT == true)
                        {
                            var guaranteeList_deposit = new List<Guarantee>();
                            var DEPOSIT_Total = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION").ThenGetIntData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_TOTAL");
                            for (int i = 0; i < DEPOSIT_Total; i++)
                            {
                                var quarantee = new Guarantee
                                {
                                    Guarantee_Type_ID = (int)GuaranteeType.DEPOSIT,
                                    //รหัสธนาคาร
                                    Bank_ID = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION").ThenGetStringData("DROPDOWN_APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_BANK_NAME_" + i),
                                    //ชื่อธนาคาร
                                    Bank_Name = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION").ThenGetStringData("DROPDOWN_APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_BANK_NAME_TEXT_" + i),
                                    //สาขา
                                    Bank_Branch = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_BRANCH_" + i),
                                    //ชื่อบัญชี
                                    Bank_Account_ID = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_ACCOUNT_ID_" + i),
                                    //เลขที่บัญชี
                                    Bank_Account_Name = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_ACCOUNT_NAME_" + i),
                                    //วันที่ครบกำหนด
                                    Bond_Due_Date = TFACUtility.GetDateFormat(model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_DUE_DATE_" + i)),
                                    //จำนวนเงิน
                                    Amount = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_AMOUNT_" + i),
                                    //ระยะเวลาการฝากประจำ(ปี)
                                    Year_Number = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_DEPOSIT_SECTION_DEPOSIT_TIME_YEAR_" + i),

                                    //บัญชีเงินฝากประจำ ไม่มีข้อมูลตามข้างล่าง
                                    Description = "",
                                    Bank_Year = "",
                                    Bond_Of = "",
                                    Bond_No = "",
                                    Bond_Date = ""

                                };
                                guaranteeList_deposit.Add(quarantee);
                            }
                            guaranteeList.AddRange(guaranteeList_deposit);
                        }

                        // บัตรเงินฝาก
                        var DEPOSIT_CARD = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_PROVIDING_SECTION").ThenGetBooleanData("APP_ACCOUNTING_DETAIL_PROVIDING_SECTION_TYPE_DEPOSIT_CARD");
                        if (DEPOSIT_CARD == true)
                        {
                            var guaranteeList_deposit_card = new List<Guarantee>();
                            var DEPOSIT_CARD_Total = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION").ThenGetIntData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION_TOTAL");
                            for (int i = 0; i < DEPOSIT_CARD_Total; i++)
                            {
                                var quarantee = new Guarantee
                                {
                                    Guarantee_Type_ID = (int)GuaranteeType.DEPOSIT_CARD,
                                    //รหัสธนาคาร
                                    Bank_ID = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION").ThenGetStringData("DROPDOWN_APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION_BANK_NAME_" + i),
                                    //ชื่อธนาคาร
                                    Bank_Name = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION").ThenGetStringData("DROPDOWN_APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION_BANK_NAME_TEXT_" + i),
                                    //สาขาธนาคาร
                                    Bank_Branch = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION_BRANCH_" + i),
                                    //ชื่อบัญชี
                                    Bank_Account_ID = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION_ACCOUNT_ID_" + i),
                                    //เลขที่บัญชี
                                    Bank_Account_Name = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION_ACCOUNT_NAME_" + i),
                                    //วันที่ครบกำหนด
                                    Bond_Due_Date = TFACUtility.GetDateFormat(model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION_DUE_DATE_" + i)),
                                    //จำนวนเงิน
                                    Amount = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_DEPOSIT_CARD_SECTION_AMOUNT_" + i),

                                    //บัตรเงินฝาก ไม่มีข้อมูลตามด้านล่าง
                                    Description = "",
                                    Bank_Year = "",
                                    Bond_Of = "",
                                    Bond_No = "",
                                    Bond_Date = "",
                                    Year_Number = ""
                                };
                                guaranteeList_deposit_card.Add(quarantee);
                            }
                            guaranteeList.AddRange(guaranteeList_deposit_card);
                        }

                        // พันธบัตรองค์กรหรือรัฐวิสาหกิจ
                        var CORPARATE_BONDS = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_PROVIDING_SECTION").ThenGetBooleanData("APP_ACCOUNTING_DETAIL_PROVIDING_SECTION_TYPE_CORPARATE_BONDS");
                        if (CORPARATE_BONDS == true)
                        {
                            var guaranteeList_corparate_bonds = new List<Guarantee>();
                            var CORPARATE_BONDS_Total = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_CORPARATE_BONDS_SECTION").ThenGetIntData("APP_ACCOUNTING_DETAIL_CORPARATE_BONDS_SECTION_TOTAL");
                            for (int i = 0; i < CORPARATE_BONDS_Total; i++)
                            {
                                var quarantee = new Guarantee
                                {
                                    Guarantee_Type_ID = (int)GuaranteeType.CORPARATE_BONDS,
                                    //ผู้ออกพันธบัตรองค์กรหรือรัฐวิสาหกิจ
                                    Bank_Name = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_CORPARATE_BONDS_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_CORPARATE_BONDS_SECTION_ISSUER_" + i),
                                    //เลขที่
                                    Bond_No = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_CORPARATE_BONDS_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_CORPARATE_BONDS_SECTION_NUMBER_" + i),
                                    //วันที่
                                    Bond_Date = TFACUtility.GetDateFormat(model.Data.TryGetData("APP_ACCOUNTING_DETAIL_CORPARATE_BONDS_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_CORPARATE_BONDS_SECTION_DATE_" + i)),
                                    //วันที่ครบกำหนด
                                    Bond_Due_Date = TFACUtility.GetDateFormat(model.Data.TryGetData("APP_ACCOUNTING_DETAIL_CORPARATE_BONDS_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_CORPARATE_BONDS_SECTION_DUE_DATE_" + i)),
                                    // จำนวนเงิน
                                    Amount = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_CORPARATE_BONDS_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_CORPARATE_BONDS_SECTION_AMOUNT_" + i),

                                    // พันธบัตรองค์กรหรือรัฐวิสาหกิจ ไม่มีข้อมูลด้านล่าง
                                    Description = "",
                                    Bank_Year = "",
                                    Bank_ID = "",
                                    Bank_Branch = "",
                                    Bank_Account_ID = "",
                                    Bank_Account_Name = "",
                                    Bond_Of = "",
                                    Year_Number = ""
                                };
                                guaranteeList_corparate_bonds.Add(quarantee);
                            }
                            guaranteeList.AddRange(guaranteeList_corparate_bonds);
                        }

                        // กรมธรรม์ประกันภัย
                        var POLICY = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_PROVIDING_SECTION").ThenGetBooleanData("APP_ACCOUNTING_DETAIL_PROVIDING_SECTION_TYPE_POLICY");
                        if (POLICY == true)
                        {
                            var POLICY_Total = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_POLICY_SECTION").ThenGetIntData("APP_ACCOUNTING_DETAIL_POLICY_SECTION_TOTAL");
                            var guaranteeList_policy = new List<Guarantee>();
                            for (int i = 0; i < POLICY_Total; i++)
                            {
                                var quarantee = new Guarantee
                                {
                                    Guarantee_Type_ID = (int)GuaranteeType.POLICY,
                                    //ชื่อบริษัทกรมธรรม์ประกันภัย
                                    Bank_Name = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_POLICY_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_POLICY_SECTION_NAME_" + i),
                                    // เลขที่
                                    Bond_No = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_POLICY_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_POLICY_SECTION_NUMBER_" + i),
                                    // วันที่ครบกำหนด
                                    Bond_Due_Date = TFACUtility.GetDateFormat(model.Data.TryGetData("APP_ACCOUNTING_DETAIL_POLICY_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_POLICY_SECTION_DUE_DATE_" + i)),
                                    // จำนวนเงิน
                                    Amount = model.Data.TryGetData("APP_ACCOUNTING_DETAIL_POLICY_SECTION").ThenGetStringData("APP_ACCOUNTING_DETAIL_POLICY_SECTION_AMOUNT_" + i),

                                    // กรมธรรม์ประกันภัย ไม่มีข้อมูลด้านล่าง
                                    Description = "",
                                    Bank_Year = "",
                                    Bank_ID = "",
                                    Bank_Branch = "",
                                    Bank_Account_ID = "",
                                    Bank_Account_Name = "",
                                    Bond_Of = "",
                                    Bond_Date = "",
                                    Year_Number = ""
                                };
                                guaranteeList_policy.Add(quarantee);
                            }
                            guaranteeList.AddRange(guaranteeList_policy);
                        }
                        post.data.Guarantee = guaranteeList;


                        // Edit Guarantee
                        var Is_check_Guarantee = model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_PROVIDING_SECTION_CHECKED").ThenGetBooleanData("APP_ACCOUNTING_SERVICE_EDIT_PROVIDING_SECTION_CHECKED_PROVIDING_CHECKED_EDIT_PROVIDING");
                        if (Is_check_Guarantee)
                        {
                            

                            if (model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_PROVIDING_SECTION").ThenGetBooleanData("APP_ACCOUNTING_SERVICE_EDIT_PROVIDING_SECTION_PROVIDING_TYPE_DEPOSIT"))
                            {
                                 var deposits = post.data.Guarantee.Where(e => e.Guarantee_Type_ID == (int)GuaranteeType.DEPOSIT);
                                 foreach(var deposit in deposits)
                                 {
                                    post.data.Guarantee.Remove(deposit);
                                 }
                               

                            }

                            if (model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_PROVIDING_SECTION").ThenGetBooleanData("APP_ACCOUNTING_SERVICE_EDIT_PROVIDING_SECTION_PROVIDING_TYPE_DEPOSIT_CARD"))
                            {

                            }

                            if (model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_PROVIDING_SECTION").ThenGetBooleanData("APP_ACCOUNTING_SERVICE_EDIT_PROVIDING_SECTION_PROVIDING_TYPE_THAI_BONDS"))
                            {

                            }

                            if (model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_PROVIDING_SECTION").ThenGetBooleanData("APP_ACCOUNTING_SERVICE_EDIT_PROVIDING_SECTION_PROVIDING_TYPE_CORPARATE_BONDS"))
                            {

                            }

                            if (model.Data.TryGetData("APP_ACCOUNTING_SERVICE_EDIT_PROVIDING_SECTION").ThenGetBooleanData("APP_ACCOUNTING_SERVICE_EDIT_PROVIDING_SECTION_PROVIDING_TYPE_POLICY"))
                            {

                            }



                        }



                        #endregion



                        #region[Attachments]
                        var attachList = new List<Attach>();
                        int file_index = 1;
                        foreach (FileGroup group in model.UploadedFiles)
                        {
                            foreach (var item in group.Files)
                            {
                                var description = item.Extras.ContainsKey("FILETYPENAME") ? item.Extras["FILETYPENAME"].ToString() : string.Empty;

                                //string fileType = TFACUtility.GetDocumentType().FirstOrDefault(x => item.FileTypeCode.Contains(x.Value)).Key.ToString();
                                string fileType = TFACUtility.GetDocumentType().FirstOrDefault(x => item.FileTypeCode.Contains(x.Key)).Value;
                                var attach = new Attach()
                                {
                                    base64String = item.GetBased64String(),
                                    contentType = item.ContentType,
                                    description = description,
                                    fileName = item.FileName,
                                    fileSize = item.FileSize.ToString(),
                                    fileType = fileType,
                                    //fileId = item.FileID,
                                    seqNo = file_index++.ToString()
                                };
                                attachList.Add(attach);
                            }
                        }
                        post.data.Attachments = attachList;
                        #endregion
                        // Model data                      
                        string regisUrl = ConfigurationManager.AppSettings["TFAC_WS_URL_REQUEST"];
                        var jsonPost = JsonConvert.SerializeObject(post); // Serialize model data to JSON

                        // API Exception
                        Api.OnCheckingApplicationError += (ex) =>
                        {
                            result.Exception = ex;
                            var egaEx = ex as EGAWSAPIException;
                            if (egaEx != null)
                            {
                                var message = string.Format("{0}: {1}", (int)egaEx.HttpStatusCode, egaEx.ResponseData["Message"].ToString());
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, message, egaEx.ResponseData.ToString(), jsonPost);
                                result.Message = egaEx.ResponseData["Message"].ToString();
                            }
                            else
                            {
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, ex.Message, ex.StackTrace, jsonPost);
                                result.Message = ex.Message;
                            }
                        };

                        // Call API
                        var apiResult = Api.Call(regisUrl, HttpVerb.POST, null, jsonPost, ContentType.ApplicationJson);
                        if (apiResult != null)
                        {
                            if (apiResult.HasValues)
                            {
                                var DataResult = apiResult["data"];

                                //  var  flag = DataResult["IsSuccess"].ToString();                              

                                if (DataResult.HasValues && DataResult["IsSuccess"].ToString() == "True")
                                {
                                    result.Success = true;
                                }

                                //Dictionary<string, string> respData = new Dictionary<string, string>()
                                //    {
                                //        { "processId", apiResult["processId"].ToString() }
                                //    };

                                //if (request.Data.ContainsKey("DBD_RESPONSE_DATA"))
                                //{
                                //    request.Data.Remove("DBD_RESPONSE_DATA");
                                //}
                                //request.Data.Add("DBD_RESPONSE_DATA", new ApplicationRequestDataGroupEntity()
                                //{
                                //    GroupName = "DBD_RESPONSE_DATA",
                                //    Data = respData
                                //});


                            }
                            else
                            {
                                string error = "No value";
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                throw new Exception(error);
                            }
                        }
                        else
                        {
                            string error = "Unable to request to " + regisUrl + ".";
                            result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                            throw new Exception(error);
                        }

                        break;
                    case AppsStage.None:
                    case AppsStage.UserUpdate:
                    case AppsStage.AgentUpdate:
                    case AppsStage.ApiUpdate:
                    default:
                        result.Success = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Exception = ex;

                result.Success = false;
            }



            //result.Success = true;
            return result;
        }

        private (JObject response, Data responseModel) getCommerceInfo(string commerceNo, string registerNo)
        {
            var response = (JObject)null;
            var responseModel = new Data();


            var preFillData = SingleFormPreFillData.Get(commerceNo, registerNo);

            if (preFillData != null)
            {
                // ใช้ข้อมูลที่เคยดึงมา
                response = JObject.Parse(preFillData.Data.ToString());
                responseModel = response.ToObject<Data>();
            }
            else
            {
                // ดึงข้อมูลจาก DBD
                Api.OnCheckingApplicationError += (ex) =>
                {
                    throw ex;
                };





                var userData = new TFACDataGetRequest
                {
                    data = new data()
                    {
                        Registration_No = "0722559000028"
                    }
                };
                var userDataString = JsonConvert.SerializeObject(userData, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

          
                response = Api.Call(ConfigurationManager.AppSettings["TFAC_WS_URL_GETREQUESTBYREGISNO"], HttpVerb.POST, null, userDataString, ContentType.ApplicationJson);

                //  response = Api.Get(ConfigurationManager.AppSettings["TFAC_WS_URL_GETREQUESTBYREGISNO"], new Dictionary<string, string> { { "commerceNo", commerceNo }, { "registerNo", registerNo } }, ContentType.ApplicationJson);

                if (response.HasValues)
                {
                    // เก็บข้อมูล pre fill จาก dbd ลง mongo
                    preFillData = new SingleFormPreFillData
                    {
                        IdentityID = registerNo,
                        ReferenceID = registerNo,
                        Data = response.ToString(Formatting.None)
                    };
                    preFillData.Create();
                    responseModel = response.ToObject<Data>();
                }
                else
                {
                    throw new Exception("ไม่พบข้อมูล");
                }
            }

            return (response, responseModel);
        }

    }
}
