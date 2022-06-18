using System;
using System.Linq;
using System.Collections.Generic;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;
using System.Configuration;
using BizPortal.ViewModels.Apps;
using BizPortal.Utils.Helpers;
//using BizPortal.ViewModels.Apps.DPTStandard;
using Newtonsoft.Json;
using EGA.WS;
using BizPortal.ViewModels.Apps.SRATStandard;
using System.Net.Http;
using System.Net;
using RestSharp;
using System.Text;

namespace BizPortal.AppsHook
{
    public class BKKSellFoodSmallAppHook : StoreBaseAppHook
    {
        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.Success = true;
            result.SendToEmail = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL").DefaultString();


            // คำร้องใหม่
            if (stage == AppsStage.UserCreate)
            {
                // ใส่ข้อมูลพื้นที่เจ้าของคำร้องจากที่อยู่ร้านค้า
                if (AppSystemNameTextConst.ALL_APP_FORM_HAS_PROVINCE_ONLY.Contains(request.AppSysName))
                {
                    if (request.AppSysName == AppSystemNameTextConst.APP_TAX || request.AppSysName == AppSystemNameTextConst.APP_TAX_RENEW)
                    {
                        var storeInfo = request.Data["INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"].Data;
                        if (storeInfo.ContainsKey("ADDRESS_PROVINCE_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"))
                        {
                            request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"]);
                            request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"]);
                            request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS"]);

                            request.Province = (storeInfo["ADDRESS_PROVINCE_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS_TEXT"]);
                            request.Amphur = (storeInfo["ADDRESS_AMPHUR_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS_TEXT"]);
                            request.Tumbol = (storeInfo["ADDRESS_TUMBOL_INFORMATION_BOARD_TAX_AGENT_AREA_ADDRESS_TEXT"]);
                        }
                    }
                    else if (request.AppSysName == AppSystemNameTextConst.APP_TAX_EDIT || request.AppSysName == AppSystemNameTextConst.APP_TAX_CANCEL)
                    {
                        var storeInfo = request.Data["INFORMATION_STORE_TAX_PROVINCE_ONLY"].Data;
                        if (storeInfo.ContainsKey("ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY"))
                        {
                            request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY"]);
                            request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY"]);
                            request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY"]);

                            request.Province = (storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY_TEXT"]);
                            request.Amphur = (storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY_TEXT"]);
                            request.Tumbol = (storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE_ADDRESS_TAX_PROVINCE_ONLY_TEXT"]);
                        }
                    }
                    else
                    {
                        var storeInfo = request.Data["INFORMATION_STORE_HAS_PROVINCE_ONLY"].Data;
                        if (storeInfo.ContainsKey("ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY"))
                        {
                            request.ProvinceID = int.Parse(storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY"]);
                            request.AmphurID = int.Parse(storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY"]);
                            request.TumbolID = int.Parse(storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY"]);

                            request.Province = (storeInfo["ADDRESS_PROVINCE_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY_TEXT"]);
                            request.Amphur = (storeInfo["ADDRESS_AMPHUR_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY_TEXT"]);
                            request.Tumbol = (storeInfo["ADDRESS_TUMBOL_INFORMATION_STORE_ADDRESS_PROVINCE_ONLY_TEXT"]);
                        }
                    }
                }
                else
                {
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
                }
            }

//#if TEST_SERVER
            //send to srat
            try
            {

                switch (stage)
                {
                    case AppsStage.UserCreate:
                        {
                            var requestTypes_Dic = new Dictionary<string, string>() {
                            {RequestorInformationValueConst.REQUEST_TYPE_OWNER, RequestorInformationTextConst.REQUEST_TYPE_OWNER},
                            {RequestorInformationValueConst.REQUEST_TYPE_NOMINEE, RequestorInformationTextConst.REQUEST_TYPE_NOMINEE}
                        };
                            var buildingTypes_Dic = new Dictionary<string, string>() {
                            {StoreInformationBuildingTypeOptionValueConst.OWNED, StoreInformationBuildingTypeOptionTextConst.OWNED},
                            {StoreInformationBuildingTypeOptionValueConst.RENT, StoreInformationBuildingTypeOptionTextConst.RENT},
                            {StoreInformationBuildingTypeOptionValueConst.RENT_FREE, StoreInformationBuildingTypeOptionTextConst.RENT_FREE},
                        };
                            var buildingTypesOption_Dic = new Dictionary<string, string>() {
                            {StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC, StoreInformationBuildingRentingOwnerTypeOptionTextConst.JURISTIC},
                            {StoreInformationBuildingRentingOwnerTypeOptionValueConst.CITIZEN, StoreInformationBuildingRentingOwnerTypeOptionTextConst.CITIZEN},
                            {StoreInformationBuildingRentingOwnerTypeOptionValueConst.Government, StoreInformationBuildingRentingOwnerTypeOptionTextConst.Government},
                            {StoreInformationBuildingRentingOwnerTypeOptionValueConst.Royal, StoreInformationBuildingRentingOwnerTypeOptionTextConst.Royal},
                        };
                            var businessType_Dic = new Dictionary<string, string>() {
                            {SellFoodInformationBusinessTypeValueConst.SELL, SellFoodInformationBusinessTypeTextConst.SELL},
                            {SellFoodInformationBusinessTypeValueConst.STOCK, SellFoodInformationBusinessTypeTextConst.STOCK}
                        };
                            var purpose_Dic = new Dictionary<string, string>() {
                            {SellFoodInformationPurposeOptionValueConst.SELL, SellFoodInformationPurposeOptionTextConst.SELL},
                            {SellFoodInformationPurposeOptionValueConst.STOCK, SellFoodInformationPurposeOptionTextConst.STOCK}
                        };
#region [Juristic Titles]
                            Dictionary<int, string> jurTitles = new Dictionary<int, string>();
                            jurTitles.Add(1, "ห้างหุ้นส่วนจำกัด");
                            jurTitles.Add(2, "บริษัทจำกัด");
                            jurTitles.Add(3, "ห้างหุ้นส่วนสามัญ");
                            jurTitles.Add(4, "สมาคม");
                            jurTitles.Add(5, "มูลนิธิ");
#endregion

                            string requestType = string.Empty;
                            string ownerType = string.Empty;
                            List<FileMetaData> fileAttachments = new List<FileMetaData>();
                            var post = new BkkSellFoodSmallRequest();
                            post.Id = model.ApplicationRequestID.ToString();
                            post.ApplicationNo = model.ApplicationRequestNumber;
                            post.SubmitDate = DateTime.Now;
                            post.WroteAt = "Biz Portal";
                            post.BizId = model.ApplicationRequestNumber;
                            post.BizGuid = model.ApplicationRequestID.ToString();
                            if (model.IdentityType == UserTypeEnum.Citizen)
                            {

#region [Contact]
                                var jurDataTel = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_CITIZEN_ADDRESS");
                                var jurDataExt = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_CITIZEN_ADDRESS");
                                var jurTel = string.IsNullOrEmpty(jurDataExt) ? jurDataTel : jurDataTel + " ext." + jurDataExt;
                                //juristic tel
                                var contacts = new List<Contact>()
                                {
                                    new Contact()
                                    {
                                       ContactType = 1,
                                       Detail = jurTel
                                    }
                                };
                                //juristic fax
                                var jurDataFax = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_CITIZEN_ADDRESS");
                                if (!string.IsNullOrEmpty(jurDataFax))
                                {
                                    contacts.Add(new Contact()
                                    {
                                        ContactType = 4,
                                        Detail = jurDataFax
                                    });
                                }
                                //Juristic email
                                var jurDataEmail = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL");
                                if (!string.IsNullOrEmpty(jurDataEmail))
                                {
                                    contacts.Add(new Contact()
                                    {
                                        ContactType = 3,
                                        Detail = jurDataEmail
                                    });
                                }
#endregion


                                post.Applicant = new PersonApplicant()
                                {
                                    Type = "Citizen",
                                    CitizenId = model.IdentityID,
                                    Title = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_TITLE_TEXT"),
                                    FirstName = model.Data.TryGetData("OPENID").ThenGetStringData("FIRSTNAME_TH"),
                                    LastName = model.Data.TryGetData("OPENID").ThenGetStringData("LASTNAME_TH"),
                                    Nationality = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_GENERAL_INFORMATION__CITIZEN_NATIONALITY_TEXT"),
                                    Age = int.Parse(model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_INFORMATION__CITIZEN_AGE")),
                                    Address = new BizPortal.ViewModels.Apps.SRATStandard.Address()
                                    {
                                        AddressNo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_CITIZEN_ADDRESS"),
                                        VillageNo = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_CITIZEN_ADDRESS"),
                                        Soi = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_CITIZEN_ADDRESS"),
                                        Road = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_CITIZEN_ADDRESS"),
                                        SubDistrict = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS_TEXT"),
                                        District = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS_TEXT"),
                                        Province = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS_TEXT"),
                                        PostCode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_CITIZEN_ADDRESS"),
                                        GeoCode = model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CITIZEN_ADDRESS") +
                                                model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CITIZEN_ADDRESS") +
                                                model.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CITIZEN_ADDRESS"),
                                        Latitude = null,
                                        Longitude = null
                                    },
                                    Contacts = contacts.ToArray(),
                                    Telephone = jurTel,
                                    Email = jurDataEmail
                                };
                                //บัตรประชาชน
                                var citizenFiles = model.UploadedFiles.Where(o => o.Description == "CITIZEN_INFORMATION").FirstOrDefault().Files;
                                var citizenCardFile = citizenFiles.Where(o => o.FileTypeCode == "ID_CARD_COPY").FirstOrDefault();
                                var citizenCardTypeName = citizenCardFile.Extras.ContainsKey("FILETYPENAME") ? citizenCardFile.Extras["FILETYPENAME"] : string.Empty;

                                fileAttachments.Add(new FileMetaData()
                                {
                                    FileId = citizenCardFile.FileID,
                                    DocName = citizenCardTypeName,
                                    ContentType = citizenCardFile.ContentType,
                                    FileSize = citizenCardFile.FileSize,
                                    Name = citizenCardFile.FileName,
                                    FileTypeCode = citizenCardFile.FileTypeCode
                                });

                                //ใบสำคัญการเปลี่ยนชื่อ/ทะเบียนสมรส
                                var reNameFile = citizenFiles.Where(o => o.FileTypeCode == "CITIZEN_RENAME_MARRIAGE_DOC").FirstOrDefault();
                                if (reNameFile != null)
                                {
                                    var reNameTypeName = reNameFile.Extras.ContainsKey("FILETYPENAME") ? reNameFile.Extras["FILETYPENAME"] : string.Empty;

                                    fileAttachments.Add(new FileMetaData()
                                    {
                                        FileId = reNameFile.FileID,
                                        DocName = reNameTypeName,
                                        ContentType = reNameFile.ContentType,
                                        FileSize = reNameFile.FileSize,
                                        Name = reNameFile.FileName,
                                        FileTypeCode = reNameFile.FileTypeCode
                                    });
                                }
                                //หนังสือรับรองผ่านการอบรมหลักสูตรสุขาภิบาลอาหารของกรุงเทพมหานคร: บุคคลผู้ขออนุญาต
                                var healthCertFile = citizenFiles.Where(o => o.FileTypeCode == "CITIZEN_BKK_FOOD_HEALTH_CERT").FirstOrDefault();
                                if (healthCertFile != null)
                                {
                                    var healthCertTypeName = healthCertFile.Extras.ContainsKey("FILETYPENAME") ? healthCertFile.Extras["FILETYPENAME"] : string.Empty;

                                    fileAttachments.Add(new FileMetaData()
                                    {
                                        FileId = healthCertFile.FileID,
                                        DocName = healthCertTypeName,
                                        ContentType = healthCertFile.ContentType,
                                        FileSize = healthCertFile.FileSize,
                                        Name = healthCertFile.FileName,
                                        FileTypeCode = healthCertFile.FileTypeCode
                                    });
                                }
                                //การขออนุญาตครั้งนี้ ตรงกับข้อใด *
                                requestType = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("CITIZEN_REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                                //อาคารที่ตั้งร้าน/สถานประกอบการของคุณมีลักษณะกรรมสิทธิ์ตามข้อใด
                                ownerType = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("CITIZEN_INFORMATION_STORE_BUILDING_TYPE_OPTION");
                            }
                            else if (model.IdentityType == UserTypeEnum.Juristic)
                            {
#region [Juristic Contact]
                                var jurDataTel = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS");
                                var jurDataExt = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS");
                                var jurTel = string.IsNullOrEmpty(jurDataExt) ? jurDataTel : jurDataTel + " ext." + jurDataExt;
                                //juristic tel
                                var jurContacts = new List<Contact>()
                                {
                                    new Contact()
                                    {
                                       ContactType = 1,
                                       Detail = jurTel
                                    }
                                };
                                //juristic fax
                                var jurDataFax = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_JURISTIC_HQ_ADDRESS");
                                if (!string.IsNullOrEmpty(jurDataFax))
                                {
                                    jurContacts.Add(new Contact()
                                    {
                                        ContactType = 4,
                                        Detail = jurDataFax
                                    });
                                }
                                //Juristic email
                                var jurDataEmail = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_EMAIL_JURISTIC_HQ_ADDRESS");
                                if (!string.IsNullOrEmpty(jurDataEmail))
                                {
                                    jurContacts.Add(new Contact()
                                    {
                                        ContactType = 3,
                                        Detail = jurDataEmail
                                    });
                                }
                                //juristic mobile
                                var jurDataMobile = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOBILE_JURISTIC_HQ_ADDRESS");
                                if (!string.IsNullOrEmpty(jurDataMobile))
                                {
                                    jurContacts.Add(new Contact()
                                    {
                                        ContactType = 2,
                                        Detail = jurDataMobile
                                    });
                                }
#endregion

#region [Applicant Contact]
                                var appContacts = new List<Contact>();
                                var appDataEmail = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_EMAIL");
                                if (!string.IsNullOrEmpty(appDataEmail))
                                {
                                    appContacts.Add(new Contact()
                                    {
                                        ContactType = 3,
                                        Detail = appDataEmail
                                    });
                                }
#endregion

                                DateTime regDateTime = DateTime.ParseExact(model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("REGISTER_DATE"), "dd/MM/yyyy", new System.Globalization.CultureInfo("th"));
                                //DateTime newDateTime = new DateTime(regDateTime.Year, regDateTime.Month, regDateTime.Day);

                                post.Applicant = new JuristicPersonApplicant()
                                {
                                    //Id = model.IdentityID,
                                    Type = "Juristic",
                                    JuristicType = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("GENERAL_INFORMATION__JURISTIC_TYPE"),
                                    JuristicId = model.IdentityID,
                                    Name = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_TH"),
                                    NameEN = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("COMPANY_NAME_EN"),
                                    RegisterDate = regDateTime.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("en")),
                                    RegisterCapital = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("REGISTER_CAPITAL"),
                                    RegisterCapitalPaid = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("REGISTER_CAPITAL_PAID"),
                                    Address = new BizPortal.ViewModels.Apps.SRATStandard.Address()
                                    {
                                        AddressNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                                        VillageNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),
                                        VillageName = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS"),
                                        BuildingName = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS"),
                                        RoomNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS"),
                                        FloorNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS"),
                                        Soi = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"),
                                        Road = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"),
                                        SubDistrict = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT"),
                                        District = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT"),
                                        Province = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT"),
                                        PostCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"),
                                        GeoCode = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS") +
                                                model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS") +
                                                model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS"),
                                        Latitude = null,
                                        Longitude = null
                                    },
                                    Contacts = jurContacts.ToArray(),
                                    Telephone = jurTel,
                                    Email = jurDataEmail
                                };
                                //หนังสือรับรองนิติบุคคล
                                var jurFiles = model.UploadedFiles.Where(o => o.Description == "JURISTIC_INFORMATION").FirstOrDefault().Files;

                                var jurCertificateFile = jurFiles.Where(o => o.FileTypeCode == "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY").FirstOrDefault();
                                var jurCertificateTypeName = jurCertificateFile.Extras.ContainsKey("FILETYPENAME") ? jurCertificateFile.Extras["FILETYPENAME"] : string.Empty;
                                fileAttachments.Add(new FileMetaData()
                                {
                                    FileId = jurCertificateFile.FileID,
                                    DocName = jurCertificateTypeName,
                                    ContentType = jurCertificateFile.ContentType,
                                    FileSize = jurCertificateFile.FileSize,
                                    Name = jurCertificateFile.FileName,
                                    FileTypeCode = jurCertificateFile.FileTypeCode
                                });
#region CommitteeData

                                var commiteeTotal = int.Parse(model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("COMMITTEE_INFORMATION_TOTAL"));
                                if (commiteeTotal > 0)
                                {
                                    var committeeList = new List<Committees>();
                                    var committeeFiles = model.UploadedFiles.Where(o => o.Description == "JURISTIC_COMMITTEE_FILE_SECTION").FirstOrDefault().Files;

                                    for (int i = 0; i < commiteeTotal; i++)
                                    {
                                        var itemId = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ARR_IDX_" + i);

                                        string title = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_TITLE_" + i);
                                        string firstname = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_NAME_" + i);
                                        string lastname = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_LASTNAME_" + i);

                                        var isAuthorizeValue = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION_" + i);
                                        var isAuthorized = (isAuthorizeValue == "yes") ? true : false;
                                        var isLawyerValue = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_IS_LAWYER_OPTION_" + i);
                                        var isLawyer = (isLawyerValue == "yes") ? true : false;
                                        committeeList.Add(new Committees
                                        {
                                            Order = int.Parse(model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_NUMBER_" + i)),
                                            Title = title,
                                            FirstName = firstname,
                                            LastName = lastname,
                                            CitizenId = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_CITIZEN_ID_" + i),
                                            CommitteeId = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_CITIZEN_ID_" + i),
                                            Nationality = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_NATIONALITY_OPTION__RADIO_TEXT_" + i),
                                            Address = new BizPortal.ViewModels.Apps.SRATStandard.Address
                                            {
                                                AddressNo = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_COMMITTEE_ADDRESS_" + i),
                                                VillageNo = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_COMMITTEE_ADDRESS_" + i),
                                                VillageName = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_COMMITTEE_ADDRESS_" + i),
                                                BuildingName = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_COMMITTEE_ADDRESS_" + i),
                                                RoomNo = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_COMMITTEE_ADDRESS_" + i),
                                                FloorNo = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_COMMITTEE_ADDRESS_" + i),
                                                Soi = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_COMMITTEE_ADDRESS_" + i),
                                                Road = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_COMMITTEE_ADDRESS_" + i),
                                                SubDistrict = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_COMMITTEE_ADDRESS_TEXT_" + i),
                                                District = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_COMMITTEE_ADDRESS_TEXT_" + i),
                                                Province = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_COMMITTEE_ADDRESS_TEXT_" + i),
                                                PostCode = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_COMMITTEE_ADDRESS_" + i),
                                                GeoCode = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_COMMITTEE_ADDRESS_" + i) +
                                                            model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_COMMITTEE_ADDRESS_" + i) +
                                                            model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_COMMITTEE_ADDRESS_" + i),

                                            },
                                            CanSigned = isAuthorized,
                                            IsAuthorizedText = model.Data.TryGetData("COMMITTEE_INFORMATION").ThenGetStringData("JURISTIC_COMMITTEE_IS_AUTHORIZED_OPTION__RADIO_TEXT_" + i),
                                            IsLawyer = isLawyer,
                                        });

                                        if (isAuthorized)//กรรมการท่านนี้มีอำนาจลงนาม
                                        {
                                            //บัตรประชาชน หรือ หนังสือเดินทาง
                                            var idCardFile = committeeFiles.Where(
                                                o => o.FileTypeCode.Contains("JURISTIC_COMMITTEE_ID_CARD") &&
                                                o.Extras["FILETYPENAME"].Replace(" ", "").Contains(title + firstname + lastname)).FirstOrDefault();
                                            if (idCardFile != null)
                                            {
                                                var idCardFileTypeName = idCardFile.Extras.ContainsKey("FILETYPENAME") ? idCardFile.Extras["FILETYPENAME"] : string.Empty;

                                                fileAttachments.Add(new FileMetaData()
                                                {
                                                    FileId = idCardFile.FileID,
                                                    DocName = idCardFileTypeName,
                                                    ContentType = idCardFile.ContentType,
                                                    FileSize = idCardFile.FileSize,
                                                    Name = idCardFile.FileName,
                                                    FileTypeCode = "JURISTIC_COMMITTEE_ID_CARD_" + i
                                                    //FileTypeCode = idCardFile.FileTypeCode
                                                });
                                            }
                                            //หนังสือรับรองผ่านการอบรมหลักสูตรสุขาภิบาลอาหาร
                                            var healthCertFile = committeeFiles.Where(
                                                o => o.FileTypeCode.Contains("JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT") &&
                                                o.Extras["FILETYPENAME"].Replace(" ", "").Contains(title + firstname + lastname)).FirstOrDefault();
                                            if (healthCertFile != null)
                                            {
                                                var healthCertFileTypeName = healthCertFile.Extras.ContainsKey("FILETYPENAME") ? idCardFile.Extras["FILETYPENAME"] : string.Empty;

                                                fileAttachments.Add(new FileMetaData()
                                                {
                                                    FileId = healthCertFile.FileID,
                                                    DocName = healthCertFileTypeName,
                                                    ContentType = healthCertFile.ContentType,
                                                    FileSize = healthCertFile.FileSize,
                                                    Name = healthCertFile.FileName,
                                                    FileTypeCode = "JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT_" + i,
                                                    //FileTypeCode = healthCertFile.FileTypeCode
                                                });
                                            }
                                        }
                                    }
                                    ((JuristicPersonApplicant)post.Applicant).Committees = committeeList.ToArray();
                                }
#endregion

                                //การขออนุญาตครั้งนี้ ตรงกับข้อใด *
                                requestType = model.Data.TryGetData("REQUESTOR_INFORMATION__HEADER").ThenGetStringData("REQUESTOR_INFORMATION__REQUEST_TYPE_OPTION");
                                //อาคารที่ตั้งร้าน/สถานประกอบการของคุณมีลักษณะกรรมสิทธิ์ตามข้อใด
                                ownerType = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_BUILDING_TYPE_OPTION");

                            }
                            //การขออนุญาตครั้งนี้ ตรงกับข้อใด *
                            var requestType_Txt = (requestTypes_Dic.ContainsKey(requestType)) ? requestTypes_Dic.Where(e => e.Key == requestType).FirstOrDefault().Value : "";
                            //shop address
                            var shopAddress = new BizPortal.ViewModels.Apps.SRATStandard.Address
                            {
                                AddressNo = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS"),
                                VillageNo = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS"),
                                BuildingName = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS"),
                                RoomNo = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS"),
                                Road = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS"),
                                SubDistrict = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT"),
                                District = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT"),
                                Province = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT"),
                                PostCode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS"),
                                GeoCode = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS") +
                                          model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS") +
                                          model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS"),
                                Latitude = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_LAT_INFORMATION_STORE__ADDRESS"),
                                Longitude = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_LAT_INFORMATION_STORE__ADDRESS")
                            };
                            //shop telephone
                            var shopDataTel = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS");
                            var shopDataExt = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS");
                            var shopTel = string.IsNullOrEmpty(shopDataExt) ? shopDataTel : shopDataTel + " ext." + shopDataExt;
                            //shop area
                            string area_txt = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_AREA");
                            double area = System.Convert.ToDouble(area_txt);
                            //string area = string.Format("{0:n}", area_decimal);
                            //shop ownership
                            var ownerType_Txt = (buildingTypes_Dic.ContainsKey(ownerType)) ? buildingTypes_Dic.Where(e => e.Key == ownerType).FirstOrDefault().Value : "";
                            var ownerOption = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION");
                            var ownerOption_Txt = (buildingTypesOption_Dic.ContainsKey(ownerOption)) ? buildingTypesOption_Dic.Where(e => e.Key == ownerOption).FirstOrDefault().Value : "";
                            var ownerShipType = (ownerType == "INFORMATION_STORE_BUILDING_TYPE_OPTION__OWNED") ? ownerType_Txt : ownerType_Txt + " (ผู้ให้เช่า/ให้ใช้สถานที่ : " + ownerOption_Txt + ")";

                            //businessType
                            var businessType = model.Data.TryGetData("SELL_FOOD_INFORMATION").ThenGetStringData("SELL_FOOD_INFORMATION__BUSINESS_TYPE_OPTION");
                            var businessType_Txt = (businessType_Dic.ContainsKey(businessType)) ? businessType_Dic.Where(e => e.Key == businessType).FirstOrDefault().Value : "";
                            //purpose
                            var purposeType = model.Data.TryGetData("SELL_FOOD_INFORMATION").ThenGetStringData("SELL_FOOD_INFORMATION__PURPOSE_OPTION");
                            var purposeType_Txt = (purpose_Dic.ContainsKey(purposeType)) ? purpose_Dic.Where(e => e.Key == purposeType).FirstOrDefault().Value : "";

                            //shopmanager
                            var shopManagerName = model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__TITLE_TEXT") +
                                model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("SELL_FOOD_FOOD_MANAGER_INFO__NAME") + " " +
                                model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("SELL_FOOD_FOOD_MANAGER_INFO__LASTNAME");
                            var shopManagerDataTel = model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("ADDRESS_TELEPHONE_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS");
                            var shopManagerDataExt = model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("ADDRESS_TELEPHONE_EXT_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS");
                            var shopManagerTel = string.IsNullOrEmpty(shopManagerDataExt) ? shopManagerDataTel : shopManagerDataTel + " ext." + shopManagerDataExt;

                            post.Address = shopAddress;
                            post.General.ApplicantInfo = new GeneralInfo.ApplicantInfomation
                            {
                                ApplicantType = requestType_Txt
                            };
                            post.General.ShopInfo = new GeneralInfo.ShopInformation
                            {
                                ShopNameTH = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_TH"),
                                ShopNameEN = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("INFORMATION_STORE_NAME_EN"),
                                ShopAddress = JsonConvert.SerializeObject(shopAddress),
                                ShopTelephone = shopTel,
                                ShopMobile = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_MOBILE_INFORMATION_STORE__ADDRESS"),
                                ShopFax = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_FAX_INFORMATION_STORE__ADDRESS"),
                                ShopEmail = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_EMAIL_INFORMATION_STORE__ADDRESS"),
                                ShopLatLong = model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_LAT_INFORMATION_STORE__ADDRESS") + "," +
                                model.Data.TryGetData("INFORMATION_STORE").ThenGetStringData("ADDRESS_LNG_INFORMATION_STORE__ADDRESS"),
                                Area = area,
                                OwnershipType = ownerShipType

                            };
                            post.SaleCollectionInfo.SaleCollectionData = new SaleCollectionInfo.SaleCollectionDataInfo
                            {
                                ApplicationType = SellFoodInformationAppTypeOptionTextConst.CERTIFICATE,
                                LicenseType = SellFoodInformationLicenseTypeOptionTextConst.CERTIFICATE,
                                ShopType = businessType_Txt,
                                Purpose = purposeType_Txt,
                                FoodType = model.Data.TryGetData("SELL_FOOD_INFORMATION").ThenGetStringData("SELL_FOOD_INFORMATION__FOOD_TYPE"),

                            };

                            post.SaleCollectionInfo.ShopManagerData = new SaleCollectionInfo.ShopManagerDataInfo
                            {
                                ShopManager = shopManagerName,
                                ShopManagerAge = model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("SELL_FOOD_FOOD_MANAGER_INFO__AGE"),
                                ShopManagerNationality = model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("DROPDOWN_SELL_FOOD_FOOD_MANAGER_INFO__NATIONALITY_TEXT"),
                                ShopManagerAddress = JsonConvert.SerializeObject(new BizPortal.ViewModels.Apps.SRATStandard.Address
                                {
                                    AddressNo = model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("ADDRESS_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS"),
                                    VillageNo = model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("ADDRESS_MOO_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS"),
                                    BuildingName = "-",
                                    Soi = model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("ADDRESS_SOI_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS"),
                                    Road = model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("ADDRESS_ROAD_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS"),
                                    Province = model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("ADDRESS_PROVINCE_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS_TEXT"),
                                    District = model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("ADDRESS_AMPHUR_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS_TEXT"),
                                    SubDistrict = model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("ADDRESS_TUMBOL_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS_TEXT"),
                                    Latitude = "-",
                                    Longitude = "-"
                                }),
                                ShopManagerTelephone = shopManagerTel,
                                ShopManagerFax = model.Data.TryGetData("SELL_FOOD_FOOD_MANAGER_INFO").ThenGetStringData("ADDRESS_FAX_SELL_FOOD_FOOD_MANAGER_INFO__ADDRESS"),

                            };
                            //JURISTIC_AUTHORIZATION_FILE_SECTION
                            if (requestType == RequestorInformationValueConst.REQUEST_TYPE_NOMINEE)
                            {
                                var authorizeFiles = model.UploadedFiles.Where(o => o.Description == "JURISTIC_AUTHORIZATION_FILE_SECTION").FirstOrDefault().Files;
                                //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้มอบอำนาจ
                                string authorizorFileTypeCode_prefix = "";
                                var authorizorIDCardFiles = new List<FileMetadata>();
                                if (model.IdentityType == UserTypeEnum.Citizen)
                                {
                                    authorizorIDCardFiles = authorizeFiles.Where(o => o.FileTypeCode.Contains("CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD")).ToList();
                                    authorizorFileTypeCode_prefix = "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD";
                                }
                                else if (model.IdentityType == UserTypeEnum.Juristic)
                                {
                                    authorizorIDCardFiles = authorizeFiles.Where(o => o.FileTypeCode.Contains("JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD")).ToList();
                                    authorizorFileTypeCode_prefix = "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD";
                                }
                                if (authorizorIDCardFiles.Count > 0)
                                {
                                    //var pplList = new List<FileMetaData>();
                                    for (int i = 0; i < authorizorIDCardFiles.Count; i++)
                                    {
                                        //var calPlanFile = consFiles.Where(o => o.FileTypeCode == "CALCULATION_PLAN-" + i).FirstOrDefault();
                                        var _file = authorizorIDCardFiles[i];
                                        if (_file != null)
                                        {
                                            var typeName = _file.Extras.ContainsKey("FILETYPENAME") ? _file.Extras["FILETYPENAME"] + "(ไฟล์ที่ " + (i + 1) + ")" : string.Empty;
                                            fileAttachments.Add(new FileMetaData()
                                            {
                                                FileId = _file.FileID,
                                                DocName = typeName,
                                                ContentType = _file.ContentType,
                                                FileSize = _file.FileSize,
                                                Name = _file.FileName,
                                                FileTypeCode = authorizorFileTypeCode_prefix + "_" + i,
                                                //FileTypeCode = "CALCULATION_PLAN",
                                            });
                                        }
                                    }
                                }
                                //หนังสือมอบอำนาจ *
                                var authorizeDocFiles = new List<FileMetadata>();
                                string authorizeDocFileTypeCode_prefix = "";
                                if (model.IdentityType == UserTypeEnum.Citizen)
                                {
                                    authorizeDocFiles = authorizeFiles.Where(o => o.FileTypeCode.Contains("CITIZEN_AUTHORIZATION_AUTHORIZE_DOC")).ToList();
                                    authorizeDocFileTypeCode_prefix = "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC";
                                }
                                else if (model.IdentityType == UserTypeEnum.Juristic)
                                {
                                    authorizeDocFiles = authorizeFiles.Where(o => o.FileTypeCode.Contains("JURISTIC_AUTHORIZATION_AUTHORIZE_DOC")).ToList();
                                    authorizeDocFileTypeCode_prefix = "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC";
                                }
                                if (authorizeDocFiles.Count > 0)
                                {
                                    //var pplList = new List<FileMetaData>();
                                    for (int i = 0; i < authorizeDocFiles.Count; i++)
                                    {
                                        //var calPlanFile = consFiles.Where(o => o.FileTypeCode == "CALCULATION_PLAN-" + i).FirstOrDefault();
                                        var _file = authorizeDocFiles[i];
                                        if (_file != null)
                                        {
                                            var typeName = _file.Extras.ContainsKey("FILETYPENAME") ? _file.Extras["FILETYPENAME"] + "(ไฟล์ที่ " + (i + 1) + ")" : string.Empty;
                                            fileAttachments.Add(new FileMetaData()
                                            {
                                                FileId = _file.FileID,
                                                DocName = typeName,
                                                ContentType = _file.ContentType,
                                                FileSize = _file.FileSize,
                                                Name = _file.FileName,
                                                FileTypeCode = authorizeDocFileTypeCode_prefix + "_" + i,
                                                //FileTypeCode = _file.FileTypeCode,
                                            });
                                        }
                                    }
                                }
                                //เอกสารยืนยันตัวตน เช่น บัตรประชาชน หรือหนังสือเดินทาง: ผู้รับมอบอำนาจ *
                                var authorizeeIDCardFiles = new List<FileMetadata>();
                                string authorizeeIDCardFileTypeCode_prefix = "";
                                if (model.IdentityType == UserTypeEnum.Citizen)
                                {
                                    authorizeeIDCardFiles = authorizeFiles.Where(o => o.FileTypeCode.Contains("AUTHORIZATION_AUTHORIZEE_ID_CARD")).ToList();
                                    authorizeeIDCardFileTypeCode_prefix = "AUTHORIZATION_AUTHORIZEE_ID_CARD";
                                }
                                else if (model.IdentityType == UserTypeEnum.Juristic)
                                {
                                    authorizeeIDCardFiles = authorizeFiles.Where(o => o.FileTypeCode.Contains("JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE")).ToList();
                                    authorizeeIDCardFileTypeCode_prefix = "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE";
                                }
                                if (authorizeeIDCardFiles.Count > 0)
                                {
                                    //var pplList = new List<FileMetaData>();
                                    for (int i = 0; i < authorizeeIDCardFiles.Count; i++)
                                    {
                                        //var calPlanFile = consFiles.Where(o => o.FileTypeCode == "CALCULATION_PLAN-" + i).FirstOrDefault();
                                        var _file = authorizeeIDCardFiles[i];
                                        if (_file != null)
                                        {
                                            var typeName = _file.Extras.ContainsKey("FILETYPENAME") ? _file.Extras["FILETYPENAME"] + "(ไฟล์ที่ " + (i + 1) + ")" : string.Empty;
                                            fileAttachments.Add(new FileMetaData()
                                            {
                                                FileId = _file.FileID,
                                                DocName = typeName,
                                                ContentType = _file.ContentType,
                                                FileSize = _file.FileSize,
                                                Name = _file.FileName,
                                                FileTypeCode = authorizeeIDCardFileTypeCode_prefix + "_" + i,
                                                //FileTypeCode = _file.FileTypeCode,
                                            });
                                        }
                                    }
                                    //post.Calculations = pplList.ToArray();
                                }
                            }

                            //INFORMATION_STORE_FILE_SECTION
                            var storeFiles = model.UploadedFiles.Where(o => o.Description == "INFORMATION_STORE_FILE_SECTION").FirstOrDefault().Files;
                            //หนังสือแจ้งการประโยชน์ที่ดินตามกฎหมายว่าด้วยการผังเมือง
                            var lawAreaFile = storeFiles.Where(o => o.FileTypeCode == "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT").FirstOrDefault();
                            var lawAreaTypeName = lawAreaFile.Extras.ContainsKey("FILETYPENAME") ? lawAreaFile.Extras["FILETYPENAME"] : string.Empty;
                            if (lawAreaFile != null)
                            {
                                fileAttachments.Add(new FileMetaData()
                                {
                                    FileId = lawAreaFile.FileID,
                                    DocName = lawAreaTypeName,
                                    ContentType = lawAreaFile.ContentType,
                                    FileSize = lawAreaFile.FileSize,
                                    Name = lawAreaFile.FileName,
                                    FileTypeCode = lawAreaFile.FileTypeCode
                                });
                            }
                            //แผนที่สังเขป แสดงสถานที่ตั้งของร้าน
                            var mapFile = storeFiles.Where(o => o.FileTypeCode == "INFORMATION_STORE_MAP").FirstOrDefault();
                            var mapTypeName = mapFile.Extras.ContainsKey("FILETYPENAME") ? mapFile.Extras["FILETYPENAME"] : string.Empty;
                            if (mapFile != null)
                            {
                                fileAttachments.Add(new FileMetaData()
                                {
                                    FileId = mapFile.FileID,
                                    DocName = mapTypeName,
                                    ContentType = mapFile.ContentType,
                                    FileSize = mapFile.FileSize,
                                    Name = mapFile.FileName,
                                    FileTypeCode = mapFile.FileTypeCode
                                });
                            }
                            //ทะเบียนบ้าน: เอกสารแสดงความเป็นเจ้าของอาคารหรือสถานที่
                            var householdRegistrationFile = storeFiles.Where(o => o.FileTypeCode == "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD").FirstOrDefault();
                            var householdRegistrationTypeName = householdRegistrationFile.Extras.ContainsKey("FILETYPENAME") ? householdRegistrationFile.Extras["FILETYPENAME"] : string.Empty;
                            if (householdRegistrationFile != null)
                            {
                                fileAttachments.Add(new FileMetaData()
                                {
                                    FileId = householdRegistrationFile.FileID,
                                    DocName = householdRegistrationTypeName,
                                    ContentType = householdRegistrationFile.ContentType,
                                    FileSize = householdRegistrationFile.FileSize,
                                    Name = householdRegistrationFile.FileName,
                                    FileTypeCode = householdRegistrationFile.FileTypeCode
                                });
                            }
                            //เจ้าของอาคาร หรือเช่า
                            if (ownerType == StoreInformationBuildingTypeOptionValueConst.OWNED)
                            {
                                //หนังสือแสดงการเป็นเจ้าของอาคาร
                                var buildingOwnerFile = storeFiles.Where(o => o.FileTypeCode == "INFORMATION_STORE_BUILDING_OWNER_DOC").FirstOrDefault();
                                var buildingOwnerTypeName = buildingOwnerFile.Extras.ContainsKey("FILETYPENAME") ? buildingOwnerFile.Extras["FILETYPENAME"] : string.Empty;
                                if (buildingOwnerFile != null)
                                {
                                    fileAttachments.Add(new FileMetaData()
                                    {
                                        FileId = buildingOwnerFile.FileID,
                                        DocName = buildingOwnerTypeName,
                                        ContentType = buildingOwnerFile.ContentType,
                                        FileSize = buildingOwnerFile.FileSize,
                                        Name = buildingOwnerFile.FileName,
                                        FileTypeCode = buildingOwnerFile.FileTypeCode
                                    });
                                }
                            }
                            else if (ownerType == StoreInformationBuildingTypeOptionValueConst.RENT)
                            {
                                //สัญญาเช่า
                                var rentalContractFile = storeFiles.Where(o => o.FileTypeCode == "INFORMATION_STORE_RENTAL_CONTRACT").FirstOrDefault();
                                var rentalContractTypeName = rentalContractFile.Extras.ContainsKey("FILETYPENAME") ? rentalContractFile.Extras["FILETYPENAME"] : string.Empty;
                                if (rentalContractFile != null)
                                {
                                    fileAttachments.Add(new FileMetaData()
                                    {
                                        FileId = rentalContractFile.FileID,
                                        DocName = rentalContractTypeName,
                                        ContentType = rentalContractFile.ContentType,
                                        FileSize = rentalContractFile.FileSize,
                                        Name = rentalContractFile.FileName,
                                        FileTypeCode = rentalContractFile.FileTypeCode
                                    });
                                }
                                //ผู้ให้เช่าเป็นนิติบุคคล แนบหนังสือรับรองนิติบุคคล
                                if (ownerOption == StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC)
                                {
                                    //หนังสือรับรองนิติบุคคล
                                    var juristicRegistrationFile = storeFiles.Where(o => o.FileTypeCode == "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION").FirstOrDefault();
                                    var juristicRegistrationTypeName = juristicRegistrationFile.Extras.ContainsKey("FILETYPENAME") ? juristicRegistrationFile.Extras["FILETYPENAME"] : string.Empty;
                                    if (juristicRegistrationFile != null)
                                    {
                                        fileAttachments.Add(new FileMetaData()
                                        {
                                            FileId = juristicRegistrationFile.FileID,
                                            DocName = juristicRegistrationTypeName,
                                            ContentType = juristicRegistrationFile.ContentType,
                                            FileSize = juristicRegistrationFile.FileSize,
                                            Name = juristicRegistrationFile.FileName,
                                            FileTypeCode = juristicRegistrationFile.FileTypeCode
                                        });
                                    }
                                }

                            }
                            else if (ownerType == StoreInformationBuildingTypeOptionValueConst.RENT_FREE)
                            {
                                //หนังสือยินยอมให้ใช้อาคารสถานที่ *
                                var usageAgreementFile = storeFiles.Where(o => o.FileTypeCode == "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT").FirstOrDefault();
                                var usageAgreementTypeName = usageAgreementFile.Extras.ContainsKey("FILETYPENAME") ? usageAgreementFile.Extras["FILETYPENAME"] : string.Empty;
                                if (usageAgreementFile != null)
                                {
                                    fileAttachments.Add(new FileMetaData()
                                    {
                                        FileId = usageAgreementFile.FileID,
                                        DocName = usageAgreementTypeName,
                                        ContentType = usageAgreementFile.ContentType,
                                        FileSize = usageAgreementFile.FileSize,
                                        Name = usageAgreementFile.FileName,
                                        FileTypeCode = usageAgreementFile.FileTypeCode
                                    });
                                }
                                //ผู้ให้เช่าเป็นนิติบุคคล แนบหนังสือรับรองนิติบุคคล
                                if (ownerOption == StoreInformationBuildingRentingOwnerTypeOptionValueConst.JURISTIC)
                                {
                                    //หนังสือรับรองนิติบุคคล
                                    var juristicRegistrationFile = storeFiles.Where(o => o.FileTypeCode == "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION").FirstOrDefault();
                                    var juristicRegistrationTypeName = juristicRegistrationFile.Extras.ContainsKey("FILETYPENAME") ? juristicRegistrationFile.Extras["FILETYPENAME"] : string.Empty;
                                    if (juristicRegistrationFile != null)
                                    {
                                        fileAttachments.Add(new FileMetaData()
                                        {
                                            FileId = juristicRegistrationFile.FileID,
                                            DocName = juristicRegistrationTypeName,
                                            ContentType = juristicRegistrationFile.ContentType,
                                            FileSize = juristicRegistrationFile.FileSize,
                                            Name = juristicRegistrationFile.FileName,
                                            FileTypeCode = juristicRegistrationFile.FileTypeCode
                                        });
                                    }
                                }
                            }
                            //BUILDING_FILE_SECTION
                            var buildingFiles = model.UploadedFiles.Where(o => o.Description == "BUILDING_FILE_SECTION").FirstOrDefault().Files;
                            //หลักฐานเกี่ยวกับการใช้อาคารตามกฏหมายว่าด้วยการควบคุมอาคาร
                            var controlDocFile = buildingFiles.Where(o => o.FileTypeCode == "INFORMATION_STORE_BUILDING_CONTROL_DOC").FirstOrDefault();
                            var controlDocTypeName = controlDocFile.Extras.ContainsKey("FILETYPENAME") ? controlDocFile.Extras["FILETYPENAME"] : string.Empty;
                            if (controlDocFile != null)
                            {
                                fileAttachments.Add(new FileMetaData()
                                {
                                    FileId = controlDocFile.FileID,
                                    DocName = controlDocTypeName,
                                    ContentType = controlDocFile.ContentType,
                                    FileSize = controlDocFile.FileSize,
                                    Name = controlDocFile.FileName,
                                    FileTypeCode = controlDocFile.FileTypeCode
                                });
                            }
                            //SELL_FOOD_FILE_SECTION
                            var sellFoodFiles = model.UploadedFiles.Where(o => o.Description == "SELL_FOOD_FILE_SECTION").FirstOrDefault().Files;
                            //แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงกระบวนการผลิต
                            var productProcessFile = sellFoodFiles.Where(o => o.FileTypeCode == "SELL_FOOD_PRODUCTION_PROCESS_CHART").FirstOrDefault();
                            if (productProcessFile != null)
                            {
                                var productProcesTypeName = productProcessFile.Extras.ContainsKey("FILETYPENAME") ? productProcessFile.Extras["FILETYPENAME"] : string.Empty;

                                fileAttachments.Add(new FileMetaData()
                                {
                                    FileId = productProcessFile.FileID,
                                    DocName = productProcesTypeName,
                                    ContentType = productProcessFile.ContentType,
                                    FileSize = productProcessFile.FileSize,
                                    Name = productProcessFile.FileName,
                                    FileTypeCode = productProcessFile.FileTypeCode
                                });
                            }
                            //แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงการป้องกันมลพิษ
                            var polutionControlFile = sellFoodFiles.Where(o => o.FileTypeCode == "SELL_FOOD_POLUTION_CONTROL_CHART").FirstOrDefault();
                            var polutionControlTypeName = polutionControlFile.Extras.ContainsKey("FILETYPENAME") ? polutionControlFile.Extras["FILETYPENAME"] : string.Empty;
                            if (polutionControlFile != null)
                            {
                                fileAttachments.Add(new FileMetaData()
                                {
                                    FileId = polutionControlFile.FileID,
                                    DocName = polutionControlTypeName,
                                    ContentType = polutionControlFile.ContentType,
                                    FileSize = polutionControlFile.FileSize,
                                    Name = polutionControlFile.FileName,
                                    FileTypeCode = polutionControlFile.FileTypeCode
                                });
                            }
                            //แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงสุขลักษณะภายในอาคาร
                            var healthControlFile = sellFoodFiles.Where(o => o.FileTypeCode == "SELL_FOOD_HEALTH_CONTROL_CHART").FirstOrDefault();
                            var healthControlTypeName = healthControlFile.Extras.ContainsKey("FILETYPENAME") ? healthControlFile.Extras["FILETYPENAME"] : string.Empty;
                            if (healthControlFile != null)
                            {
                                fileAttachments.Add(new FileMetaData()
                                {
                                    FileId = healthControlFile.FileID,
                                    DocName = healthControlTypeName,
                                    ContentType = healthControlFile.ContentType,
                                    FileSize = healthControlFile.FileSize,
                                    Name = healthControlFile.FileName,
                                    FileTypeCode = healthControlFile.FileTypeCode
                                });
                            }
                            //แผนผังหรือภาพถ่ายบริเวณภายในและภายนอกของสถานประกอบการ แสดงให้เห็นถึงระบบความปลอดภัยในการทำงาน *
                            var safetyControlFile = sellFoodFiles.Where(o => o.FileTypeCode == "SELL_FOOD_SEFTY_CONTROL_CHART").FirstOrDefault();
                            var safetyControlTypeName = safetyControlFile.Extras.ContainsKey("FILETYPENAME") ? safetyControlFile.Extras["FILETYPENAME"] : string.Empty;
                            if (safetyControlFile != null)
                            {
                                fileAttachments.Add(new FileMetaData()
                                {
                                    FileId = safetyControlFile.FileID,
                                    DocName = safetyControlTypeName,
                                    ContentType = safetyControlFile.ContentType,
                                    FileSize = safetyControlFile.FileSize,
                                    Name = safetyControlFile.FileName,
                                    FileTypeCode = safetyControlFile.FileTypeCode
                                });
                            }
                            //ผู้สัมผัสอาหาร
#region ShopCrewsData
                            var workerTotal = int.Parse(model.Data.TryGetData("SELL_FOOD_FOOD_WORKER_INFO").ThenGetStringData("SELL_FOOD_FOOD_WORKER_INFO_TOTAL"));
                            var crewsList = new List<SaleCollectionInfo.ShopCrewsDataInfo>();
                            if (workerTotal > 0)
                            {
                                //var crewsList = new List<SaleCollectionInfo.ShopCrewsDataInfo>();
                                for (int i = 0; i < workerTotal; i++)
                                {
                                    var itemId = model.Data.TryGetData("SELL_FOOD_FOOD_WORKER_INFO").ThenGetStringData("ARR_ITEM_ID_" + i);

                                    string title = model.Data.TryGetData("SELL_FOOD_FOOD_WORKER_INFO").ThenGetStringData("DROPDOWN_SELL_FOOD_FOOD_WORKER_INFO__TITLE_TEXT_" + i);
                                    string firstName = model.Data.TryGetData("SELL_FOOD_FOOD_WORKER_INFO").ThenGetStringData("SELL_FOOD_FOOD_WORKER_INFO__NAME_" + i);
                                    string lastName = model.Data.TryGetData("SELL_FOOD_FOOD_WORKER_INFO").ThenGetStringData("SELL_FOOD_FOOD_WORKER_INFO__LASTNAME_" + i);
                                    string position = model.Data.TryGetData("SELL_FOOD_FOOD_WORKER_INFO").ThenGetStringData("SELL_FOOD_FOOD_WORKER_INFO__POSITION_" + i);

                                    var crew = new SaleCollectionInfo.ShopCrewsDataInfo()
                                    {
                                        ShopCrews_Name = string.Format("{0}{1} {2}", title, firstName, lastName),
                                        ShopCrews_Position = position,
                                    };
                                    crewsList.Add(crew);

                                    //ใบรับรองแพทย์การตรวจโรคติดต่อ 9 โรค
                                    var medicalCertFile = sellFoodFiles.Where(o => o.FileTypeCode == "SELL_FOOD_MEDICAL_CERTIFICATE_" + itemId).FirstOrDefault();
                                    var medicalCertTypeName = medicalCertFile.Extras.ContainsKey("FILETYPENAME") ? medicalCertFile.Extras["FILETYPENAME"] : string.Empty;
                                    if (medicalCertFile != null)
                                    {
                                        fileAttachments.Add(new FileMetaData()
                                        {
                                            FileId = medicalCertFile.FileID,
                                            DocName = medicalCertTypeName,
                                            ContentType = medicalCertFile.ContentType,
                                            FileSize = medicalCertFile.FileSize,
                                            Name = medicalCertFile.FileName,
                                            FileTypeCode = "SELL_FOOD_MEDICAL_CERTIFICATE_" + i
                                            //FileTypeCode = medicalCertFile.FileTypeCode
                                        });
                                    }
                                    //หนังสือรับรองผ่านการอบรมหลักสูตรสุขาภิบาลอาหาร
                                    var foodCertFile = sellFoodFiles.Where(o => o.FileTypeCode == "SELL_FOOD_FOOD_WORKER_CERTIFICATE_" + itemId).FirstOrDefault();
                                    if (foodCertFile != null)
                                    {
                                        var foodCertTypeName = foodCertFile.Extras.ContainsKey("FILETYPENAME") ? foodCertFile.Extras["FILETYPENAME"] : string.Empty;

                                        fileAttachments.Add(new FileMetaData()
                                        {
                                            FileId = foodCertFile.FileID,
                                            DocName = foodCertTypeName,
                                            ContentType = foodCertFile.ContentType,
                                            FileSize = foodCertFile.FileSize,
                                            Name = foodCertFile.FileName,
                                            //FileTypeCode = foodCertFile.FileTypeCode
                                            FileTypeCode = "SELL_FOOD_FOOD_WORKER_CERTIFICATE_" + i
                                        });
                                    }
                                }
                                //post.SaleCollectionInfo.ShopCrewsData = crewsList.ToArray();

                            }
                            post.SaleCollectionInfo.ShopCrewsData = crewsList.ToArray();
                            //ไฟล์แนบเพิ่มเติม
                            var otherFileSection = model.UploadedFiles.Where(o => o.Description == "FREE_DOC_SECTION").FirstOrDefault();
                            if (otherFileSection != null)
                            {
                                var otherFiles = otherFileSection.Files;
                                //var otherFileList = new List<DPTFileMetaData>();
                                var gp = otherFiles.GroupBy(n => n.FileReason);
                                int index = 0;
                                foreach (var g in gp)
                                {
                                    if (g.Count() > 1)
                                    {
                                        int i = 0;
                                        foreach (var file in g)
                                        {
                                            i++;
                                            var fileTypeName = file.Extras.ContainsKey("FILETYPENAME") ? file.Extras["FILETYPENAME"] : string.Empty;

                                            fileAttachments.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = (!String.IsNullOrEmpty(file.FileReason) ? file.FileReason : fileTypeName) + " (" + i + ")",
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = "OTHER_DOC_" + index + "_" + i
                                            });
                                        }
                                    }
                                    else
                                    {
                                        var file = g.FirstOrDefault();
                                        var fileTypeName = file.Extras.ContainsKey("FILETYPENAME") ? file.Extras["FILETYPENAME"] : string.Empty;

                                        fileAttachments.Add(new FileMetaData()
                                        {
                                            FileId = file.FileID,
                                            DocName = (!String.IsNullOrEmpty(file.FileReason) ? file.FileReason : fileTypeName),
                                            ContentType = file.ContentType,
                                            FileSize = file.FileSize,
                                            Name = file.FileName,
                                            FileTypeCode = "OTHER_DOC_" + index
                                        });
                                    }
                                    index++;
                                }
                                /*
                                foreach (var file in otherFiles)
                                {
                                    var fileTypeName = file.Extras.ContainsKey("FILETYPENAME") ? file.Extras["FILETYPENAME"] : string.Empty;
                                    
                                    otherFileList.Add(new DPTFileMetaData()
                                    {
                                        FileId = file.FileID,
                                        DocName = (!String.IsNullOrEmpty(file.FileReason) ? file.FileReason : fileTypeName),
                                        ContentType = file.ContentType,
                                        FileSize = file.FileSize,
                                        Name = file.FileName,
                                        FileTypeCode = file.FileTypeCode
                                    });
                                }*/
                                //if (otherFileList != null && otherFileList.Count > 0)
                                //{
                                //    post.OtherDocuments = otherFileList.ToArray();
                                //}
                            }

                            post.ApplicationAttachments = fileAttachments.ToArray();

#endregion

                            string regisUrl = "/dga/srat/v1/test/application/creation";//ConfigurationManager.AppSettings["DPT_BUILDING_G1_WS_URL"];

                            var jsonPost = JsonConvert.SerializeObject(post);

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

                            var webRequest = (HttpWebRequest)WebRequest.Create("http://srat-api.testapp2.dga.or.th/api/Application?serviceNo=25632104300000001&version=1");

                            //var postData = "thing1=" + Uri.EscapeDataString("hello");
                            //postData += "&thing2=" + Uri.EscapeDataString("world");
                            var data = Encoding.UTF8.GetBytes(jsonPost);

                            webRequest.Method = "POST";
                            webRequest.ContentType = "application/json";
                            webRequest.Headers.Add("Consumer-Key", "59a92baa-4418-4b69-8ea9-67eecc042aa2");
                            webRequest.ContentLength = data.Length;

                            using (var stream = webRequest.GetRequestStream())
                            {
                                stream.Write(data, 0, data.Length);
                            }
                            var webResponse = (HttpWebResponse)webRequest.GetResponse();
                            if (webResponse.StatusCode == HttpStatusCode.OK)
                            {
                                //result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, apiResponse["Result"].ToString(), apiResponse.ToString(), jsonPost);
                                result.Success = true;
                            }
                            else
                            {
                                string error = "Unable to request to " + regisUrl + ".";
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                throw new Exception(error);
                            }
                            /*
                            Dictionary<string, string> args = new Dictionary<string, string>();
                            args.Add("serviceNo", "25632104300000001");
                            args.Add("version", "1");
                            var apiResponse = Api.Call(regisUrl, HttpVerb.POST, args, jsonPost, ContentType.ApplicationJson);
                            if (apiResponse != null)
                            {
                                if (apiResponse.HasValues)
                                {
                                    if (apiResponse["MessageCode"] != null && apiResponse["MessageCode"].ToString() == "00000")
                                    {
                                        // Clear section data
                                        //var singleformReq = new SingleFormRequestEntity();
                                        //singleformReq.DeleteSections(model.IdentityID, null,
                                        //    new string[]
                                        //    {
                                        //            "APP_BUILDING_G1_INFORMATION",
                                        //            "APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION",
                                        //            "APP_BUILDING_G1_BUILDING_OWNER",
                                        //            "APP_BUILDING_G1_BUILDING_INFORMATION",
                                        //            "APP_BUILDING_G1_TITLE_DEED",
                                        //            "APP_BUILDING_G1_DESIGN_ARCHITECT",
                                        //            "APP_BUILDING_G1_DESIGN_ENGINEER",
                                        //            "APP_BUILDING_G1_SUPERVISE_ARCHITECT",
                                        //            "APP_BUILDING_G1_SUPERVISE_ENGINEER"
                                        //    }
                                        //);

                                        // Clear uploaded files
                                        //var singleFormTran = SingleFormTransaction.Get(model.IdentityID);
                                        //if (singleFormTran != null && singleFormTran.UploadedFiles != null && singleFormTran.UploadedFiles.Count > 0)
                                        //{
                                        //    var fg = singleFormTran.UploadedFiles.Where(o => o.Description == "CONSTRUCTION_SITE_INFORMATION").SingleOrDefault();
                                        //    if (fg != null)
                                        //    {
                                        //        singleFormTran.RemoveUploadedFiles(fg.FileGroupID);
                                        //    }

                                        //    var otherFg = singleFormTran.UploadedFiles.Where(o => o.Description == "FREE_DOC_SECTION").FirstOrDefault();
                                        //    if (otherFg != null)
                                        //    {
                                        //        singleFormTran.RemoveUploadedFiles(otherFg.FileGroupID);
                                        //    }
                                        //}

                                        result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, apiResponse["Result"].ToString(), apiResponse.ToString(), jsonPost);
                                        result.Success = true;
                                    }
                                    else
                                    {
                                        string msg = string.Format("[{0}]{1}", apiResponse["MessageCode"].ToString(), apiResponse["Message"].ToString());
                                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, msg, apiResponse.ToString(), jsonPost, true);
                                        throw new Exception(msg);
                                    }
                                }
                                else
                                {
                                    string error = "error";
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
                            */
                        }
                        break;

                    case AppsStage.UserUpdate:
                        if (model.Status == ApplicationStatusV2Enum.CHECK || model.Status == ApplicationStatusV2Enum.PENDING)
                        {
                            List<FileMetaData> updateFiles = new List<FileMetaData>();
#region [POST DATA]
                            var requestedFiles = model.UploadedFiles.Where(o => o.Description == "REQUESTED_FILE").OrderByDescending(o => o.CreatedDate).FirstOrDefault();
                            if (requestedFiles != null && requestedFiles.Files != null && requestedFiles.Files.Count > 0)
                            {
                                foreach (var file in requestedFiles.Files)
                                {
                                    var fileTypeCode = file.FileTypeCode;
                                    var fileTypeName = file.Extras.ContainsKey("FILETYPENAME") ? file.Extras["FILETYPENAME"].ToString() : string.Empty;
                                    var fileId = file.Extras.ContainsKey("FILEID") ? file.Extras["FILEID"].ToString() : string.Empty;

                                    Dictionary<string, object> extras = new Dictionary<string, object>();
                                    extras.Add("FileId", fileId);

                                    if (!string.IsNullOrEmpty(fileTypeCode) && !string.IsNullOrEmpty(fileTypeName) && !string.IsNullOrEmpty(fileId))
                                    {
#region case fileTypeCode
                                        /*
                                        if (fileTypeCode == "ID_CARD_COPY")
                                        {
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }
                                        else if (fileTypeCode == "CITIZEN_RENAME_MARRIAGE_DOC")
                                        {
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }
                                        else if (fileTypeCode == "CITIZEN_BKK_FOOD_HEALTH_CERT")
                                        {
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }
                                        else if (fileTypeCode.Contains("CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD"))
                                        {
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }
                                        else if (fileTypeCode.Contains("CITIZEN_AUTHORIZATION_AUTHORIZE_DOC"))
                                        {
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }
                                        else if (fileTypeCode.Contains("AUTHORIZATION_AUTHORIZEE_ID_CARD"))
                                        {
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }
                                        //นิติบุคคล
                                        else if (fileTypeCode == "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY")
                                        {
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }
                                        else if (fileTypeCode.Contains("JURISTIC_COMMITTEE_ID_CARD"))
                                        {
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }
                                        else if (fileTypeCode.Contains("JURISTIC_COMMITTEE_BKK_FOOD_HEALTH_CERT"))
                                        {
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }
                                        else if (fileTypeCode.Contains("JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD"))
                                        { }
                                        else if (fileTypeCode.Contains("JURISTIC_AUTHORIZATION_AUTHORIZE_DOC"))
                                        { }
                                        else if (fileTypeCode.Contains("JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE"))
                                        { }
                                        //อาคาร/สถานที่ของร้าน ประกอบด้วย
                                        else if (fileTypeCode == "INFORMATION_STORE_LAW_AREA_USAGE_AGREEMENT")
                                        {//1.หนังสือแจ้งการประโยชน์ที่ดินตามกฎหมายว่าด้วยการผังเมือง 
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }
                                        else if (fileTypeCode == "INFORMATION_STORE_MAP")
                                        {//2.แผนที่สังเขป แสดงสถานที่ตั้งของร้าน
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }
                                        else if (fileTypeCode == "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY_FOOD")
                                        {//3.ทะเบียนบ้าน:อาคารที่ตั้งร้าน / สถานประกอบการ
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }

                                        //กรณีเช่าอาคาร หรือเช่าฟรี ประกอบด้วย
                                        //(กรณีเช่า)สัญญาเช่า
                                        //(กรณีผู้ให้เช่า หรือให้ใช้เป็นนิติ)หนังสือยินยอมให้ใช้อาคารสถานที่ *
                                        //(กรณีผู้ให้เช่า หรือให้ใช้เป็นนิติ)หนังสือรับรองนิติบุคคล: ผู้ให้เช่า หรือยินยอมให้ใช้อาคารสถานที่
                                        //(กรณีผู้ให้ใช้เป็นบุคคลธรรมดา)หนังสือแสดงการเป็นเจ้าของอาคาร

                                        //การขออนุญาตก่อสร้าง ประกอบด้วย
                                        //หลักฐานเกี่ยวกับการใช้อาคารตามกฏหมายว่าด้วยการควบคุมอาคาร

                                        //แผนผังต่างๆ


                                        //ผู้สัมผัสอาหาร ประกอบด้วย
                                        else if (fileTypeCode.Contains("SELL_FOOD_MEDICAL_CERTIFICATE"))
                                        {
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }
                                        else if (fileTypeCode.Contains("SELL_FOOD_FOOD_WORKER_CERTIFICATE"))
                                        {
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode,
                                                Extras = extras
                                            });
                                        }*/
#endregion

                                        updateFiles.Add(new FileMetaData()
                                        {
                                            FileId = file.FileID,
                                            DocName = fileTypeName,
                                            ContentType = file.ContentType,
                                            FileSize = file.FileSize,
                                            Name = file.FileName,
                                            FileTypeCode = file.FileTypeCode,
                                            Extras = extras
                                        });
                                    }
                                    else if (!string.IsNullOrEmpty(fileTypeCode) && !string.IsNullOrEmpty(fileTypeName))
                                    {
                                        if (fileTypeCode.Contains("ADDITIONAL_DOC"))
                                        {
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode
                                            });
                                        }
                                        else if (fileTypeCode.Contains("OTHER_DOC"))
                                        {
                                            updateFiles.Add(new FileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = fileTypeName,
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = file.FileTypeCode
                                            });
                                        }
                                    }
                                }

                            }
#endregion

                            var jsonPost = JsonConvert.SerializeObject(updateFiles.ToArray());

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

                            string url = string.Format("http://srat-api.testapp2.dga.or.th/api/Application/{0}/Modify", model.ApplicationRequestID.ToString());
                            var webRequest = (HttpWebRequest)WebRequest.Create(url);

                            //var postData = "thing1=" + Uri.EscapeDataString("hello");
                            //postData += "&thing2=" + Uri.EscapeDataString("world");
                            var data = Encoding.UTF8.GetBytes(jsonPost);

                            webRequest.Method = "PUT";
                            webRequest.ContentType = "application/json";
                            webRequest.ContentLength = data.Length;
                            webRequest.Headers.Add("Consumer-Key", "59a92baa-4418-4b69-8ea9-67eecc042aa2");

                            using (var stream = webRequest.GetRequestStream())
                            {
                                stream.Write(data, 0, data.Length);
                            }
                            var webResponse = (HttpWebResponse)webRequest.GetResponse();
                            if (webResponse.StatusCode == HttpStatusCode.OK)
                            {
                                //result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, apiResponse["Result"].ToString(), apiResponse.ToString(), jsonPost);
                                result.Success = true;
                            }
                            else
                            {
                                string error = "Unable to request to " + url + ".";
                                result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, null, jsonPost, true);
                                throw new Exception(error);
                            }
                        }
                        else if (model.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE)
                        {

                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Exception = ex;
                result.Success = false;
            }
//#endif

            return result;
        }
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return null;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }
    }
    /*
    public class BkkSellFoodSmallRequest
    {
        public string Id { get; set; }
        public DPTPerson Applicant;
        public GeneralInfo General { get; set; }
        public SaleCollectionInfo SaleCollectionInfo { get; set; }
        public DPTFileMetaData[] ApplicationAttachments { get; set; }

        public BkkSellFoodSmallRequest()
        {
            General = new GeneralInfo();
            SaleCollectionInfo = new SaleCollectionInfo();
        }
    }
    public class GeneralInfo
    {
        public ApplicantInfomation Applicant { get; set; }
        public ShopInformation ShopInfo { get; set; }

        public class ApplicantInfomation
        {
            public string ApplicantType { get; set; }
        }

        public class ShopInformation
        {
            public string ShopNameTH { get; set; }
            public string ShopNameEN { get; set; }
            public string ShopAddress { get; set; }
            public string ShopTelephone { get; set; }
            public string ShopMobile { get; set; }
            public string ShopFax { get; set; }
            public string ShopEmail { get; set; }
            public string ShopLatLong { get; set; }
            public Double Area { get; set; }
            public string OwnershipType { get; set; }
        }
    }
   
    public class SaleCollectionInfo 
    {
        public SaleCollectionDataInfo SaleCollectionData { get; set; }
        public ShopCrewsDataInfo[] ShopCrewsData { get; set; }
        public ShopManagerDataInfo ShopManagerData { get; set; }

        public class SaleCollectionDataInfo
        {
            public string ApplicationType { get; set; }
            public string LicenseType { get; set; }
            public string ShopType { get; set; }
            public string Purpose { get; set; }
            public string FoodType { get; set; }
        }
        public class ShopCrewsDataInfo
        {
            public string ShopCrews_Name { get; set; }
            public string ShopCrews_Position { get; set; }
        }
        public class ShopManagerDataInfo
        {
            public string ShopManager { get; set; }
            public string ShopManagerAge { get; set; }
            public string ShopManagerNationality { get; set; }
            public string ShopManagerAddress { get; set; }
            public string ShopManagerTelephone { get; set; }
            public string ShopManagerFax { get; set; }

        }
    }
    public class AttachmentInfos
    {
        public DPTFileMetaData CitizenCard { get; set; }
        public DPTFileMetaData CitizenRename { get; set; }
        public DPTFileMetaData CitizenHealthCert { get; set; }
        //ทะเบียนบ้าน:อาคารที่ตั้งร้าน / สถานประกอบการ
        public DPTFileMetaData ShopHouseRegistration { get; set; }
        //เอกสารแสดงความเป้นเจ้าของ หรือสัญญาเช่า หรือหนังสือยินยอมให้ใช้สถานที่
        public DPTFileMetaData BuildingOwnerDoc { get; set; }
        public DPTFileMetaData BuildingOwnerJuristicRegistration { get; set; }
        public DPTFileMetaData ShopMap { get; set; }
        public DPTFileMetaData LawAreaUsage { get; set; }
        public DPTFileMetaData BuildingControlDoc { get; set; }
        public DPTFileMetaData ProductProcessChart { get; set; }
        public DPTFileMetaData PolutionControlChart { get; set; }
        public DPTFileMetaData HealthControlChart { get; set; }
        public DPTFileMetaData SafetyControlChart { get; set; }
    }
    public class SRATAddress
    {
        public string AddressNo { get; set; }
        public string VillageNo { get; set; }
        public string BuildingName { get; set; }
        public string RoomNo { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string PostCode { get; set; }
        public string GeoCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
    */
}
