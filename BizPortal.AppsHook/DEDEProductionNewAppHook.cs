using BizPortal.DAL.MongoDB;
using BizPortal.Integrated;
using BizPortal.Utils;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Apps;
using BizPortal.ViewModels.Apps.DEDEStandard;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using EGA.WS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;

namespace BizPortal.AppsHook
{
    public class DEDEProductionNewAppHook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public override bool InvokeSingleForm(Guid trid, string currentSectionGroup, ref SingleFormRequestViewModel model)
        {
            var result = true;

            if (currentSectionGroup == "APP_ENERGY_PRODUCTION")
            {
                if (model.IdentityType == UserTypeEnum.Citizen)
                {
                    var generalInfo = GetSectionData(model, "GENERAL_INFORMATION", SectionType.Form);
                    var contactInfo = GetSectionData(model, "APP_ENERGY_PRODUCTION_CONTACT_SECTION", SectionType.Form);

                    // Prefill contact from citizen info for the first load.
                    if (contactInfo.FormData.TryGetString("APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_NAME", "") == "")
                    {
                        contactInfo.FormData.AddOrUpdate("DROPDOWN_APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_TITLE", generalInfo.FormData.TryGetString("DROPDOWN_CITIZEN_TITLE", ""));
                        contactInfo.FormData.AddOrUpdate("DROPDOWN_APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_TITLE_TEXT", generalInfo.FormData.TryGetString("DROPDOWN_CITIZEN_TITLE_TEXT", ""));
                        contactInfo.FormData.AddOrUpdate("APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_NAME", generalInfo.FormData.TryGetString("CITIZEN_NAME", ""));
                        contactInfo.FormData.AddOrUpdate("APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_LASTNAME", generalInfo.FormData.TryGetString("CITIZEN_LASTNAME", ""));
                        contactInfo.FormData.AddOrUpdate("APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_ID_CARD", generalInfo.FormData.TryGetString("IDENTITY_ID", ""));
                    }
                }

            }

            return result;
        }

        public override bool IsEnabledChat() => false;

        // <BIZBIZ> CODE FOR BIZ_BIZ  <BIZBIZ>
        //public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        //{
        //    InvokeResult result = new InvokeResult();
        //    result.Success = true;

        //    return result;
        //}

        // <API>   CODE FOR API    <API>
        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            // หลังจากทำ Biz-api เสร็จแล้วจะต้องนำโค้ดชุดนี้ออก
            if (request.Data.ContainsKey("INFORMATION_STORE"))
            {
                var storeInfo = request.Data["INFORMATION_STORE"].Data;
                if (storeInfo.ContainsKey("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"))
                {
                    request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS"]);
                    request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS"]);
                    request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS"]);

                    request.Province = (storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"]);
                    request.Amphur = (storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"]);
                    request.Tumbol = (storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"]);
                }
            }

            // TODO: Updated on 2020-05-08, do not submit data to third-party.
            return new InvokeResult() { Success = true };

            #region biz-api code

            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;

            try
            {
                switch (stage)
                {
                    case AppsStage.UserCreate:
                        {
                            var post = new BusinessData()
                            {
                                request = new request()
                                {
                                    is_company = model.IdentityType.ToString() == "Juristic" ? 1 : 0,
                                    from = "กพร",
                                    is_authorize = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION") == "REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD" ? 0 : 1,
                                    is_samartgrid = 0,
                                    ref_permittype_id = 1,//ขอใหม่
                                    ref_purposes_id = model.Data.TryGetData("APP_ENERGY_PRODUCTION_INFO_SECTION").ThenGetIntData("AJAX_DROPDOWN_APP_ENERGY_PRODUCTION_INFO_SECTION_REASON"),
                                    powerplant = null,
                                    inspector = "พพ.ตรวจ",
                                    application_request_id = model.ApplicationRequestID.ToString()
                                },
                                smartgrid = new smartgrid()
                                {
                                    installation = "",
                                    internal_used = "",
                                    Smartgrid = "",
                                    directcustomer = "",
                                    scod = ""
                                },
                                sites = new sites()
                                {
                                    name = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH"),
                                    about = model.Data.TryGetData("APP_ENERGY_PRODUCTION_INFO_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_LICENSE_REASON"),
                                    tsic = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_TSICID"),
                                    communication = new communication()
                                    {
                                        mobile = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOBILE_INFORMATION_STORE__ADDRESS").Replace("-", ""),
                                        tel = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS"),
                                        fax = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS"),
                                        email = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS")
                                    },
                                    gis = new gis()
                                    {
                                        name = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH"),
                                        latitude = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_LAT_INFORMATION_STORE__ADDRESS"),
                                        longitude = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_LNG_INFORMATION_STORE__ADDRESS")
                                    },
                                    address = new address()
                                    {
                                        name = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH"),
                                        code = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"),
                                        addr1 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS"),
                                        addr2 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS"),
                                        addr3 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"),
                                        addr4 = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS"),
                                        //geo_code = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS") + model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS") + model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS"),
                                        //district_id = GeoService.GetDistrictID(model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS") + model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS") + model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS"))
                                        district_id = GeoService.GetDEDEGeoData("code", model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS") + model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS") + model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS")).Result.FirstOrDefault().Data.FirstOrDefault().id
                                    }
                                }
                            };
                            var uPrefixList = getUPrefixList();
                            #region Owner
                            if (model.IdentityType.ToString() == "Juristic")
                            {
                                var ownerList = new List<owner>();
                                var owner = new owner();
                                owner.name = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_TH");
                                owner.id_number = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("IDENTITY_ID");
                                var cPrefixList = getCPrefixList();
                                owner.ref_company_id = Convert.ToInt32(cPrefixList.responseModel.data.FirstOrDefault(x => x.name == model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_INFORMATION__JURISTIC_TYPE")).id);
                                var boardList = new List<personel>();

                                var boardTotal = int.Parse(model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("COMMITTEE_INFORMATION_TOTAL"));
                                if (boardTotal > 0)
                                {
                                    for (int i = 0; i < boardTotal; i++)
                                    {
                                        var boardAuth = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION_" + i);
                                        if (boardAuth == "yes")
                                        {
                                            var board = new personel();
                                            board.ref_prefix_id = Convert.ToInt32(uPrefixList.responseModel.data.FirstOrDefault(x => x.name == model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_TITLE_" + 1)).id);
                                            board.first_name = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_NAME_" + i);
                                            board.last_name = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_LASTNAME_" + i);
                                            var boardNation = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION_" + i);
                                            if (boardNation == "yes")
                                            {
                                                board.idcard = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_CITIZEN_ID_" + i).Replace("-", "");
                                            }
                                            else
                                            {
                                                board.idcard = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("COMMITTEE_INFORMATION_PASSPORT_NUMBER_" + i).Replace("-", "");
                                            }

                                            board.is_proved = 0;
                                            boardList.Add(board);
                                        }

                                    }
                                }
                                owner.board = boardList;
                                ownerList.Add(owner);
                                post.owner = ownerList;
                                #region Authorizes

                                var chkAuthor = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                                if (chkAuthor != "REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD")
                                {
                                    var authorList = new List<authorizes>();
                                    var author = new authorizes()
                                    {
                                        position = null,
                                        is_authorize = 1,
                                        personel = new personel()
                                        {
                                            ref_prefix_id = Convert.ToInt32(uPrefixList.responseModel.data.FirstOrDefault(x => x.name == model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("DROPDOWN_ENERGY_REQUESTOR_INFORMATION_TITLE_TEXT")).id),//model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetIntData("DROPDOWN_ENERGY_REQUESTOR_INFORMATION_TITLE"),
                                            first_name = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_NAME"),
                                            last_name = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_LASTNAME"),
                                            idcard = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_ID_CARD").Replace("-", ""),
                                            is_proved = 0
                                        },
                                        communication = new communication()
                                        {
                                            mobile = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_MOBILE"),
                                            tel = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_TEL"),
                                            fax = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_FAX"),
                                            email = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_EMAIL"),
                                        }
                                    };
                                    authorList.Add(author);
                                    post.authorizes = authorList;

                                }
                                else
                                {
                                    var authorList = new List<authorizes>();
                                    post.authorizes = authorList;

                                }


                                #endregion
                            }
                            else
                            {
                                var ownerList = new List<owner>();
                                var owner = new owner()
                                {
                                    name = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_NAME") + " " + model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("CITIZEN_LASTNAME"),
                                    id_number = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("IDENTITY_ID")
                                };
                                var boardList = new List<personel>();
                                owner.board = boardList;
                                ownerList.Add(owner);
                                post.owner = ownerList;
                                #region Authorizes

                                var chkAuthor = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                                if (chkAuthor != "REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER")
                                {
                                    var authorList = new List<authorizes>();
                                    var author = new authorizes()
                                    {
                                        position = null,
                                        is_authorize = 1,
                                        personel = new personel()
                                        {
                                            ref_prefix_id = Convert.ToInt32(uPrefixList.responseModel.data.FirstOrDefault(x => x.name == model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("DROPDOWN_ENERGY_REQUESTOR_INFORMATION_TITLE_TEXT")).id),//model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetIntData("DROPDOWN_ENERGY_REQUESTOR_INFORMATION_TITLE"),
                                            first_name = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_NAME"),
                                            last_name = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_LASTNAME"),
                                            idcard = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_ID_CARD").Replace("-", ""),
                                            is_proved = 0
                                        },
                                        communication = new communication()
                                        {
                                            mobile = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_MOBILE"),
                                            tel = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_TEL"),
                                            fax = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_FAX"),
                                            email = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("ENERGY_REQUESTOR_INFORMATION_EMAIL"),
                                        }
                                    };
                                    authorList.Add(author);
                                    post.authorizes = authorList;

                                }
                                else
                                {
                                    var authorList = new List<authorizes>();
                                    post.authorizes = authorList;

                                }


                                #endregion
                            }
                            #endregion
                            #region Co_Id
                            var coList = new List<co_id>();
                            var co = new co_id();
                            //co.user_profile_id = "1";
                            //co.communication_id = "1";
                            var per = new personel()
                            {
                                ref_prefix_id = model.Data.TryGetData("APP_ENERGY_PRODUCTION_CONTACT_SECTION").ThenGetIntData("AJAX_DROPDOWN_APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_TITLE"),
                                first_name = model.Data.TryGetData("APP_ENERGY_PRODUCTION_CONTACT_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_NAME"),
                                last_name = model.Data.TryGetData("APP_ENERGY_PRODUCTION_CONTACT_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_LASTNAME"),
                                idcard = model.Data.TryGetData("APP_ENERGY_PRODUCTION_CONTACT_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_ID_CARD").Replace("-", ""),
                                is_proved = 0
                            };
                            var com = new communication()
                            {
                                mobile = model.Data.TryGetData("APP_ENERGY_PRODUCTION_CONTACT_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_MOBILE"),
                                tel = model.Data.TryGetData("APP_ENERGY_PRODUCTION_CONTACT_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_TEL"),
                                fax = model.Data.TryGetData("APP_ENERGY_PRODUCTION_CONTACT_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_FAX"),
                                email = model.Data.TryGetData("APP_ENERGY_PRODUCTION_CONTACT_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_EMAIL"),
                            };
                            co.personel = per;
                            co.communication = com;
                            coList.Add(co);
                            post.co_id = coList;
                            #endregion
                            #region Drivers
                            var driverTotal = int.Parse(model.Data.TryGetData("APP_ENERGY_PRODUCTION_MACHINE_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_MACHINE_SECTION_TOTAL"));
                            if (driverTotal > 0)
                            {
                                var driverList = new List<drivers>();
                                int no = 1;
                                for (int i = 0; i < driverTotal; i++)
                                {
                                    var driver = new drivers();
                                    driver.no = no++;
                                    driver.power_drive = model.Data.TryGetData("APP_ENERGY_PRODUCTION_MACHINE_SECTION").ThenGetIntData("AJAX_DROPDOWN_APP_ENERGY_PRODUCTION_MACHINE_SECTION_TYPE_" + i);
                                    driver.is_solar = 0;
                                    driver.mainfuel = model.Data.TryGetData("APP_ENERGY_PRODUCTION_MACHINE_SECTION").ThenGetIntData("AJAX_DROPDOWN_APP_ENERGY_PRODUCTION_MACHINE_SECTION_FUEL_" + i);
                                    driver.installed_year = model.Data.TryGetData("APP_ENERGY_PRODUCTION_MACHINE_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_MACHINE_SECTION_YEAR_ACTIVE_" + i);
                                    driver.brand = model.Data.TryGetData("APP_ENERGY_PRODUCTION_MACHINE_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_MACHINE_SECTION_NAME_" + i);
                                    driver.horse_power = model.Data.TryGetData("APP_ENERGY_PRODUCTION_MACHINE_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_MACHINE_SECTION_HORSEPOWER_" + i);
                                    driver.machine_num = model.Data.TryGetData("APP_ENERGY_PRODUCTION_MACHINE_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_MACHINE_SECTION_ID_" + i);
                                    driver.install_type = model.Data.TryGetData("APP_ENERGY_PRODUCTION_MACHINE_SECTION").ThenGetStringData("DROPDOWN_APP_ENERGY_PRODUCTION_MACHINE_SECTION_INSTALL_TYPE_" + i) == "NEW" ? "1" : "2";

                                    var detailList = new List<details>();
                                    if (model.Data.TryGetData("APP_ENERGY_PRODUCTION_MACHINE_SECTION").ThenGetIntData("AJAX_DROPDOWN_APP_ENERGY_PRODUCTION_MACHINE_SECTION_TYPE_" + i) == 7)
                                    {
                                        //[{"PHOTOVOLTAIC_INDEX":"1","PHOTOVOLTAIC_SOLAR_TEXT":"บนหลังคา","PHOTOVOLTAIC_SOLAR":"1","PHOTOVOLTAIC_TYPE_TEXT":"ผลึก","PHOTOVOLTAIC_TYPE":"1","PHOTOVOLTAIC_TYPE_EXTRA":"","PHOTOVOLTAIC_WATT":"50","PHOTOVOLTAIC_AMOUNT":"50"},{"PHOTOVOLTAIC_INDEX":"2","PHOTOVOLTAIC_SOLAR_TEXT":"บนพื้นดิน","PHOTOVOLTAIC_SOLAR":"2","PHOTOVOLTAIC_TYPE_TEXT":"ฟิล์มบาง","PHOTOVOLTAIC_TYPE":"2","PHOTOVOLTAIC_TYPE_EXTRA":"","PHOTOVOLTAIC_WATT":"60","PHOTOVOLTAIC_AMOUNT":"70"},{"PHOTOVOLTAIC_INDEX":"3","PHOTOVOLTAIC_SOLAR_TEXT":"บนทุ่นลอยน้ำ","PHOTOVOLTAIC_SOLAR":"3","PHOTOVOLTAIC_TYPE_TEXT":"ผลึก อืนๆ","PHOTOVOLTAIC_TYPE":"4","PHOTOVOLTAIC_TYPE_EXTRA":"","PHOTOVOLTAIC_WATT":"70","PHOTOVOLTAIC_AMOUNT":"80"}]
                                        var str = model.Data.TryGetData("APP_ENERGY_PRODUCTION_MACHINE_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_MACHINE_SECTION_PHOTOVOLTAIC_" + i);
                                        JArray parsedArray = JArray.Parse(str);
                                        //var objectiveList = new List<Objective>();                              
                                        foreach (JObject parsedObject in parsedArray.Children<JObject>())
                                        {
                                            var detail = new details();
                                            foreach (JProperty parsedProperty in parsedObject.Properties())
                                            {

                                                string propertyName = parsedProperty.Name;

                                                if (propertyName.Equals("PHOTOVOLTAIC_INDEX"))
                                                {
                                                    detail.cell_type = (int)parsedProperty.Value;

                                                }
                                                if (propertyName.Equals("PHOTOVOLTAIC_SOLAR"))
                                                {
                                                    detail.ref_solar_id = (int)parsedProperty.Value;

                                                }
                                                if (propertyName.Equals("PHOTOVOLTAIC_TYPE"))
                                                {
                                                    detail.ref_solartype_id = (int)parsedProperty.Value;

                                                }
                                                if (propertyName.Equals("PHOTOVOLTAIC_WATT"))
                                                {
                                                    detail.power = (int)parsedProperty.Value;

                                                }
                                                if (propertyName.Equals("PHOTOVOLTAIC_AMOUNT"))
                                                {
                                                    detail.num_cell = (int)parsedProperty.Value;

                                                }
                                                //if (propertyName.Equals("PHOTOVOLTAIC_TYPE_EXTRA"))
                                                //{
                                                //    detail.solar_type = parsedProperty.Value.ToString();

                                                //}

                                            }

                                            detailList.Add(detail);
                                        }
                                        driver.is_solar = 1;
                                    }
                                    driver.details = detailList;
                                    driverList.Add(driver);
                                }
                                post.drivers = driverList;
                            }

                            #endregion
                            #region Gen

                            var genTotal = int.Parse(model.Data.TryGetData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION_TOTAL"));
                            if (genTotal > 0)
                            {
                                int no = 1;
                                var genList = new List<gen>();
                                for (int i = 0; i < genTotal; i++)
                                {
                                    var gen = new gen();
                                    gen.no = no++;
                                    gen.ref_gentype_id = model.Data.TryGetData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION").ThenGetIntData("AJAX_DROPDOWN_APP_ENERGY_PRODUCTION_ELECTRIC_SECTION_TYPE_" + i);
                                    // gen.g_type = null;
                                    gen.c_power = model.Data.TryGetData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION_KVA_" + i);
                                    gen.c_voltage = model.Data.TryGetData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION_VOLTAGE_" + i);
                                    gen.c_current = model.Data.TryGetData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION_AMP_" + i);
                                    gen.c_factor = model.Data.TryGetData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION_PERCENT_" + i);
                                    //gen.g_pole = null;
                                    //gen.g_used = null;
                                    gen.c_installed_year = model.Data.TryGetData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION_YEAR_" + i);
                                    gen.c_brand = model.Data.TryGetData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION_NAME_" + i);
                                    //gen.c_serie = null;
                                    gen.install_type = model.Data.TryGetData("APP_ENERGY_PRODUCTION_INFO_SECTION").ThenGetStringData("DROPDOWN_APP_ENERGY_PRODUCTION_ELECTRIC_SECTION_INSTALL_TYPE_") == "NEW" ? "1" : "2";
                                    gen.c_machine_num = model.Data.TryGetData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION").ThenGetStringData("APP_ENERGY_PRODUCTION_ELECTRIC_SECTION_ID_" + i);
                                    gen.c_purpose_id = model.Data.TryGetData("APP_ENERGY_PRODUCTION_INFO_SECTION").ThenGetIntData("AJAX_DROPDOWN_APP_ENERGY_PRODUCTION_INFO_SECTION_REASON");
                                    gen.c_permittype_id = 1;
                                    genList.Add(gen);
                                }
                                post.gen = genList;
                            }
                            #endregion
                            #region [Attachs]
                            var attachList = new List<attached>();
                            //int file_index = 1;
                            var documentService = getDocumentList();
                            var fileServiceUrl = ConfigurationManager.AppSettings["FileServicePath"]; // "https://file.testapp2.dga.or.th/api/file/" 

                            foreach (FileGroup group in model.UploadedFiles)
                            {
                                foreach (var item in group.Files)
                                {
                                    //var ref_id = documentService.responseModel.document.FirstOrDefault(x => item.FileTypeCode.Contains(JArray.Parse(x.filetypecode).Children<JProperty>().Values)).id;
                                  
                                   var docid = getDocumentId(documentService.responseModel.document, item.FileTypeCode);
                                   var attach = new attached()
                                    {
                                       
                                        ref_document_id = docid,//DEDEUtility.GetFileTypeRef().FirstOrDefault(x => item.FileTypeCode.Contains(x.Key)).Value,
                                        url = fileServiceUrl + item.FileID + "?accesstoken=yourAccessToken",
                                        is_upload = 0,
                                        is_approved = 0
                                    };
                                    attachList.Add(attach);
                                }

                            }
                            post.attacheds = attachList.ToList();


                            #endregion

                            // Model data
                            string regisUrl = ConfigurationManager.AppSettings["DEDE_REGIS_WS_URL"];
                            var jsonPost = JsonConvert.SerializeObject(post,
                                    Newtonsoft.Json.Formatting.None,
                                    new JsonSerializerSettings
                                    {
                                        NullValueHandling = NullValueHandling.Ignore
                                    }); // Serialize model data to JSON

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

                            var client = new RestClient(regisUrl);
                            var request_ = new RestRequest(Method.POST);
                            request_.RequestFormat = DataFormat.Json;
                            request_.AddHeader("Token", Api.AccessToken);
                            request_.AddHeader("Consumer-Key", Api.ConsumerKey);
                            request_.AddHeader("Content-Type", "application/json");
                            request_.AddParameter("application/json", jsonPost, ParameterType.RequestBody);
                            // request_.AddBody(post);

                            IRestResponse resp = client.Execute(request_);
                            if (resp.StatusCode == HttpStatusCode.OK)
                            {
                                result.Success = true;
                                result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, "", resp.StatusCode.ToString(), jsonPost);
                                //RootObject opt = JsonConvert.DeserializeObject<RootObject>(resp.Content.ToString());
                                //foreach (var obj in opt.result)
                                //{
                                //    opts.Add(new Select2Opt() { ID = obj.id, Text = obj.name });
                                //}


                                // Call API
                                //var apiResult = Api.Call(regisUrl, HttpVerb.POST, null, jsonPost, ContentType.ApplicationJson);
                                //if (apiResult != null)
                                //{
                                //    if (apiResult.HasValues)
                                //    {
                                //        // TODO:
                                //    }
                                //    else
                                //    {
                                //        string error = "No value";
                                //        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                //        throw new Exception(error);
                                //    }
                            }
                            else
                            {
                                string error = "Unable to request to " + regisUrl + ".";
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                throw new Exception(error);
                            }
                        }
                        break;
                    case AppsStage.UserUpdate:
                        {
                            if (model.Status == ApplicationStatusV2Enum.CHECK || model.Status == ApplicationStatusV2Enum.PENDING)
                            {
                                //        var post = new CommerceAddAttach()
                                //        {
                                //            bizReqNo = model.ApplicationRequestID.ToString(),
                                //            reqNo = model.ApplicationRequestNumber,
                                //            bizReqDateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'", new CultureInfo("en")),//DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                                //            remark = model.Remark.ToString(),
                                //        };
                                #region [Attachs]
                                var attachList = new List<attached>();
                                //int file_index = 1;
                                var fileServiceUrl = ConfigurationManager.AppSettings["FileServicePath"]; // "https://file.testapp2.dga.or.th/api/file/" 
                                var requestedFiles = model.UploadedFiles.Where(o => o.Description == "REQUESTED_FILE").OrderByDescending(o => o.CreatedDate).FirstOrDefault();
                                
                                if (requestedFiles != null && requestedFiles.Files != null && requestedFiles.Files.Count > 0)
                                {
                                    foreach (var item in requestedFiles.Files)
                                    {
                                        var fileType = item.Extras.ContainsKey("FILETYPE") ? item.Extras["FILETYPE"].ToString() : "0";

                                        var attach = new attached()
                                        {
                                            ref_document_id = fileType,// DEDEUtility.GetFileTypeRef().FirstOrDefault(x => item.FileTypeCode.Contains(x.Key)).Value,
                                            url = fileServiceUrl + item.FileID + "?accesstoken=yourAccessToken",
                                            is_upload = 0,
                                            is_approved = 0
                                        };
                                        attachList.Add(attach);

                                    }
                                    // Call API
                                    string BaseUrl = ConfigurationManager.AppSettings["DEDEAttachedFilesServiceURL"];
                                    var bizReqNo = model.ApplicationRequestID.ToString();
                                    var url = string.Format(BaseUrl, bizReqNo);
                                    var jsonPost = JsonConvert.SerializeObject(attachList,
                                            Newtonsoft.Json.Formatting.None,
                                            new JsonSerializerSettings
                                            {
                                                NullValueHandling = NullValueHandling.Ignore
                                            }); // Serialize model data to JSON

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

                                    var client = new RestClient(url);
                                    var request_ = new RestRequest(Method.POST);
                                    request_.RequestFormat = DataFormat.Json;
                                    request_.AddHeader("Token", Api.AccessToken);
                                    request_.AddHeader("Consumer-Key", Api.ConsumerKey);
                                    request_.AddHeader("Content-Type", "application/json");
                                    request_.AddParameter("application/json", jsonPost, ParameterType.RequestBody);
                                    // request_.AddBody(post);

                                    IRestResponse resp = client.Execute(request_);
                                    if (resp.StatusCode == HttpStatusCode.OK)
                                    {
                                        result.Success = true;

                                    }
                                    else
                                    {
                                        string error = "Unable to request to " + url + ".";
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                        throw new Exception(error);
                                    }
                                }
                                else
                                {
                                    result.Success = true;
                                }
                                #endregion
                            }
                            else
                            {
                                result.Success = true;
                            }
                        }
                        break;
                    case AppsStage.None:
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

            return result;

            #endregion
        }
        private (JObject response, DocumentList responseModel) getDocumentList()
        {
            var response = (JObject)null;
            var responseModel = new DocumentList();
            // ดึงข้อมูลจาก DBD
            Api.OnCheckingApplicationError += (ex) =>
            {
                throw ex;
            };

            response = Api.Get(ConfigurationManager.AppSettings["DEDE_MASTERDATA_DOCUMENT_WS_URL"], ContentType.ApplicationJson);

            if (response.HasValues)
            {

                responseModel = JsonConvert.DeserializeObject<DocumentList>(response.ToString());
            }
            else
            {
                throw new Exception("ไม่พบข้อมูล");
            }
            return (response, responseModel);
        }
        private (JObject response, UPrefixList responseModel) getUPrefixList()
        {
            var response = (JObject)null;
            var responseModel = new UPrefixList();
            // ดึงข้อมูลจาก DBD
            Api.OnCheckingApplicationError += (ex) =>
            {
                throw ex;
            };
            response = Api.Get(ConfigurationManager.AppSettings["DEDE_MASTERDATA_PREFIX_WS_URL"], ContentType.ApplicationJson);
            if (response.HasValues)
            {

                responseModel = JsonConvert.DeserializeObject<UPrefixList>(response.ToString());
            }
            else
            {
                throw new Exception("ไม่พบข้อมูล");
            }
            return (response, responseModel);
        }
        private (JObject response, CPrefixList responseModel) getCPrefixList()
        {
            var response = (JObject)null;
            var responseModel = new CPrefixList();
            // ดึงข้อมูลจาก DBD
            Api.OnCheckingApplicationError += (ex) =>
            {
                throw ex;
            };
            response = Api.Get(ConfigurationManager.AppSettings["DEDE_MASTERDATA_COMPANY_PREFIX_WS_URL"], ContentType.ApplicationJson);
            if (response.HasValues)
            {
                responseModel = JsonConvert.DeserializeObject<CPrefixList>(response.ToString());
            }
            else
            {
                throw new Exception("ไม่พบข้อมูล");
            }
            return (response, responseModel);
        }
        private string getDocumentId(List<DocumentInfo> docList, string fileTypeCode)
        {
            foreach (var item in docList)
            {
                JArray parsedArray = JArray.Parse(item.filetypecode);
                //var result = parsedArray.Contains(fileTypeCode);
                
                foreach (JValue x in parsedArray)
                {
                    if (fileTypeCode.Contains(x.ToString()))
                    {
                        return item.id;// Process...
                    }
                }
                               
            }
            return "0";
        }
    }
}
