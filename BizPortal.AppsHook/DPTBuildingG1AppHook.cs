using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Apps;
using BizPortal.ViewModels.Apps.DPTStandard;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using EGA.WS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BizPortal.AppsHook
{
    public class DPTBuildingG1AppHook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData) => null;

        public override bool IsEnabledChat() => false;

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;
            if (model.IdentityType == UserTypeEnum.Juristic)
            {
                result.SendToEmail = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("APPLICANT_EMAIL");
            }
            else if (model.IdentityType == UserTypeEnum.Citizen)
            {
                result.SendToEmail = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_EMAIL");
            }

            #region [Juristic Titles]
            Dictionary<int, string> jurTitles = new Dictionary<int, string>();
            jurTitles.Add(1, "ห้างหุ้นส่วนจำกัด");
            jurTitles.Add(2, "บริษัทจำกัด");
            jurTitles.Add(3, "ห้างหุ้นส่วนสามัญ");
            jurTitles.Add(4, "สมาคม");
            jurTitles.Add(5, "มูลนิธิ");
            #endregion

            try
            {
                string regisUrl = ConfigurationManager.AppSettings["DPT_BUILDING_G1_WS_URL"];

                switch (stage)
                {
                    case AppsStage.UserCreate:
                        {
                            #region [POST Data]
                            var post = new DPTG1Request()
                            {
                                RequestType = "R1",
                                Name = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("APP_BUILDING_G1_CONSTRUCTION_SITE_FOR"),
                                WroteAt = "Biz Portal",
                                BizId = model.ApplicationRequestNumber,
                                BizGuid = model.ApplicationRequestID.ToString(),
                                SubmitDate = DateTime.Now,
                                EstimateDay = model.Data.TryGetData("APP_BUILDING_G1_INFORMATION").ThenGetIntData("APP_BUILDING_G1_DURATION"),
                                Address = new DPTAddress()
                                {
                                    AddressNo = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS"),
                                    VillageNo = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_MOO_APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS"),
                                    Soi = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_SOI_APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS"),
                                    Road = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_ROAD_APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS"),
                                    SubDistrict = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS_TEXT"),
                                    District = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS_TEXT"),
                                    Province = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS_TEXT"),
                                    PostCode = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS"),
                                    GeoCode = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS") +
                                                model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS") +
                                                model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS"),
                                    Latitude = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_LAT_APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS"),
                                    Longitude = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("ADDRESS_LNG_APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS")
                                },
                                IssueByOrgCode = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("AJAX_DROPDOWN_APP_BUILDING_G1_CONSTRUCTION_SITE_RESPONSIBLE_AREA"),
                                IssueByOrgName = model.Data.TryGetData("APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION").ThenGetStringData("AJAX_DROPDOWN_APP_BUILDING_G1_CONSTRUCTION_SITE_RESPONSIBLE_AREA_TEXT")
                            };

                            var purpose = model.Data.TryGetData("APP_BUILDING_G1_INFORMATION").ThenGetStringData("APP_BUILDING_G1_TYPE");
                            switch (purpose)
                            {
                                case BuildingTypeOptionValueConst.BUILDING:
                                    post.PurposeType = 1;
                                    break;
                                case BuildingTypeOptionValueConst.MODIFY:
                                    post.PurposeType = 2;
                                    break;
                                case BuildingTypeOptionValueConst.DISMANTLE:
                                    post.PurposeType = 4;
                                    break;
                                default:
                                    post.PurposeType = 1;
                                    break;
                            }

                            if (model.IdentityType == UserTypeEnum.Citizen)
                            {
                                #region [Citizen Contact]
                                var contacts = new List<DPTContact>()
                                {
                                    new DPTContact()
                                    {
                                       ContactType = 2,
                                       Detail = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_TEL")
                                    }
                                };

                                var contactEmail = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("CONTACT_EMAIL");
                                if (!string.IsNullOrEmpty(contactEmail))
                                {
                                    contacts.Add(new DPTContact()
                                    {
                                        ContactType = 3,
                                        Detail = contactEmail
                                    });
                                }
                                #endregion

                                post.Applicant = new DPTPersonApplicant()
                                {
                                    CitizenId = model.IdentityID,
                                    Title = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("DROPDOWN_CITIZEN_TITLE_TEXT"),
                                    FirstName = model.Data.TryGetData("OPENID").ThenGetStringData("FIRSTNAME_TH"),
                                    LastName = model.Data.TryGetData("OPENID").ThenGetStringData("LASTNAME_TH"),
                                    Address = new DPTAddress()
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
                                    ContactAddress = new DPTAddress()
                                    {
                                        AddressNo = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_CONTACT_ADDRESS"),
                                        VillageNo = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_MOO_CONTACT_ADDRESS"),
                                        Soi = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_SOI_CONTACT_ADDRESS"),
                                        Road = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_ROAD_CONTACT_ADDRESS"),
                                        SubDistrict = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CONTACT_ADDRESS_TEXT"),
                                        District = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CONTACT_ADDRESS_TEXT"),
                                        Province = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CONTACT_ADDRESS_TEXT"),
                                        PostCode = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_CONTACT_ADDRESS"),
                                        GeoCode = model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_CONTACT_ADDRESS") +
                                            model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_CONTACT_ADDRESS") +
                                            model.Data.TryGetData("CONTACT_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_CONTACT_ADDRESS"),
                                        Latitude = null,
                                        Longitude = null
                                    },
                                    Contacts = contacts.ToArray(),
                                };

                                var citiFiles = model.UploadedFiles.Where(o => o.Description == "CITIZEN_INFORMATION").FirstOrDefault().Files;
                                var citizenCardFile = citiFiles.Where(o => o.FileTypeCode == "APPLICANT_ID_CARD_COPY").FirstOrDefault();
                                var citizenCardTypeName = citizenCardFile.Extras.ContainsKey("FILETYPENAME") ? citizenCardFile.Extras["FILETYPENAME"] : string.Empty;

                                post.CitizenIDCards = new DPTFileMetaData[]
                                {
                                    new DPTFileMetaData()
                                    {
                                        FileId = citizenCardFile.FileID,
                                        DocName = citizenCardTypeName,
                                        ContentType = citizenCardFile.ContentType,
                                        FileSize = citizenCardFile.FileSize,
                                        Name = citizenCardFile.FileName,
                                        FileTypeCode = citizenCardFile.FileTypeCode
                                    }
                                };
                            }
                            else if (model.IdentityType == UserTypeEnum.Juristic)
                            {
                                #region [Juristic Contact]
                                var jurDataTel = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS");
                                var jurDataExt = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_EXT_JURISTIC_HQ_ADDRESS");
                                var jurTel = string.IsNullOrEmpty(jurDataExt) ? jurDataTel : jurDataTel + " ext." + jurDataExt;

                                var jurContacts = new List<DPTContact>()
                                {
                                    new DPTContact()
                                    {
                                       ContactType = 1,
                                       Detail = jurTel
                                    }
                                };

                                var jurDataFax = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FAX_JURISTIC_HQ_ADDRESS");
                                if (!string.IsNullOrEmpty(jurDataFax))
                                {
                                    jurContacts.Add(new DPTContact()
                                    {
                                        ContactType = 4,
                                        Detail = jurDataFax
                                    });
                                }
                                #endregion

                                #region [Applicant Contact]
                                var appContacts = new List<DPTContact>()
                                {
                                    new DPTContact()
                                    {
                                       ContactType = 2,
                                       Detail = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("APPLICANT_TEL")
                                    }
                                };

                                var appDataEmail = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("APPLICANT_EMAIL");
                                if (!string.IsNullOrEmpty(appDataEmail))
                                {
                                    appContacts.Add(new DPTContact()
                                    {
                                        ContactType = 3,
                                        Detail = appDataEmail
                                    });
                                }
                                #endregion

                                post.Applicant = new DPTJuristicPersonApplicant()
                                {
                                    JuristicPersonNo = model.IdentityID,
                                    Type = jurTitles.FirstOrDefault(x => x.Value == model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("JURISTIC_TYPE")).Key,
                                    Name = model.IdentityName,
                                    RegisterDate = model.Data.TryGetData("GENERAL_INFORMATION").ThenGetStringData("REGISTER_DATE"),
                                    Address = new DPTAddress()
                                    {
                                        AddressNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                                        VillageNo = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),
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
                                    DelegatedPerson = new DPTPerson()
                                    {
                                        Title = null,
                                        FirstName = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("APPLICANT_FIRSTNAME"),
                                        LastName = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("APPLICANT_LASTNAME"),
                                        CitizenId = null,
                                        Address = new DPTAddress()
                                        {
                                            AddressNo = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_APPLICANT_ADDRESS"),
                                            VillageNo = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_MOO_APPLICANT_ADDRESS"),
                                            Soi = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_SOI_APPLICANT_ADDRESS"),
                                            Road = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_ROAD_APPLICANT_ADDRESS"),
                                            SubDistrict = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_APPLICANT_ADDRESS_TEXT"),
                                            District = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_APPLICANT_ADDRESS_TEXT"),
                                            Province = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_APPLICANT_ADDRESS_TEXT"),
                                            PostCode = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_APPLICANT_ADDRESS"),
                                            GeoCode = model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_APPLICANT_ADDRESS") +
                                                model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_APPLICANT_ADDRESS") +
                                                model.Data.TryGetData("APPLICANT_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_APPLICANT_ADDRESS"),
                                            Latitude = null,
                                            Longitude = null
                                        },
                                        ContactAddress = null,
                                        Contacts = appContacts.ToArray()
                                    }
                                };

                                var jurFiles = model.UploadedFiles.Where(o => o.Description == "JURISTIC_INFORMATION").FirstOrDefault().Files;

                                var jurCertificateFile = jurFiles.Where(o => o.FileTypeCode == "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY").FirstOrDefault();
                                var jurCertificateTypeName = jurCertificateFile.Extras.ContainsKey("FILETYPENAME") ? jurCertificateFile.Extras["FILETYPENAME"] : string.Empty;
                                post.JuristicPersonRegisterationDocuments = new DPTFileMetaData[]
                                {
                                    new DPTFileMetaData()
                                    {
                                        FileId = jurCertificateFile.FileID,
                                        DocName = jurCertificateTypeName,
                                        ContentType = jurCertificateFile.ContentType,
                                        FileSize = jurCertificateFile.FileSize,
                                        Name = jurCertificateFile.FileName,
                                        FileTypeCode = jurCertificateFile.FileTypeCode
                                    }
                                };

                                var jurDelegatorFile = jurFiles.Where(o => o.FileTypeCode == "JURISTIC_DELEGATOR_DOC").FirstOrDefault();
                                var jurDelegatorTypeName = jurDelegatorFile.Extras.ContainsKey("FILETYPENAME") ? jurDelegatorFile.Extras["FILETYPENAME"] : string.Empty;
                                post.DelegationRepresentationDocuments = new DPTFileMetaData[]
                                {
                                    new DPTFileMetaData()
                                    {
                                        FileId = jurDelegatorFile.FileID,
                                        DocName = jurDelegatorTypeName,
                                        ContentType = jurDelegatorFile.ContentType,
                                        FileSize = jurDelegatorFile.FileSize,
                                        Name = jurDelegatorFile.FileName,
                                        FileTypeCode = jurDelegatorFile.FileTypeCode
                                    }
                                };
                            }

                            var consFiles = model.UploadedFiles.Where(o => o.Description == "CONSTRUCTION_SITE_INFORMATION").FirstOrDefault().Files;

                            #region [Building_Owners]
                            var ownerTotal = int.Parse(model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_G1_BUILDING_OWNER_TOTAL"));
                            if (ownerTotal > 0)
                            {
                                var ownerList = new List<IApplicant>();
                                //var ownerAddress = new DPTAddress();
                                //if (model.IdentityType == UserTypeEnum.Citizen)
                                //{
                                //    ownerAddress = ((DPTPersonApplicant)post.Applicant).Address;
                                //}
                                //else if (model.IdentityType == UserTypeEnum.Juristic)
                                //{
                                //    ownerAddress = ((DPTJuristicPersonApplicant)post.Applicant).Address;
                                //}


                                for (int i = 0; i < ownerTotal; i++)
                                {
                                    var person_type = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_G1_BUILDING_OWNER_KIND_OF_PERSON_OPTION_" + i);
                                    
                                    if (person_type == "1")
                                    {
                                        string tmp_citizenid = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_R6_BUILDING_OWNER_CITIZENID_" + i);

                                        var owner = new DPTPersonApplicant();
                                        var contacts = new List<DPTContact>();
                                        var phone = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_TELEPHONE_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i);
                                        var fax = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_FAX_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i);

                                        owner.Title = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("DROPDOWN_APP_BUILDING_R6_BUILDING_OWNER_TITLE_TEXT_" + i);
                                        owner.FirstName = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_R6_BUILDING_OWNER_FIRSTNAME_" + i);
                                        owner.LastName = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_R6_BUILDING_OWNER_LASTNAME_" + i);
                                        owner.CitizenId = tmp_citizenid.Replace("-", "");
                                        owner.Address = new DPTAddress
                                        {
                                            AddressNo = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i),
                                            VillageNo = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_MOO_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i),
                                            Soi = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_SOI_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i),
                                            Road = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_ROAD_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i),
                                            SubDistrict = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_TEXT_" + i),
                                            District = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_TEXT_" + i),
                                            Province = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_TEXT_" + i),
                                            PostCode = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_POSTCODE_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i),
                                            GeoCode = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i) +
                                            model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i) +
                                            model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_OWNER_CITIZEN_ADDRESS_" + i),
                                        };
                                        if (!String.IsNullOrEmpty(phone))
                                        {
                                            contacts.Add(new DPTContact()
                                            {
                                                ContactType = 1,
                                                Detail = phone
                                            });
                                        }
                                        if (!String.IsNullOrEmpty(fax))
                                        {
                                            contacts.Add(new DPTContact
                                            {
                                                ContactType = 4,
                                                Detail = fax
                                            });
                                        }
                                        owner.Contacts = contacts.ToArray();

                                        ownerList.Add(owner);
                                    }
                                    else
                                    {
                                        var owner = new DPTJuristicPersonApplicant();
                                        var contacts = new List<DPTContact>();
                                        var phone = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_TELEPHONE_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i);
                                        var fax = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_FAX_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i);
                                        
                                        owner.Name = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_NAME_" + i);
                                        owner.JuristicPersonNo = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("APP_BUILDING_R6_BUILDING_OWNER_JURISTICID_" + i);
                                        owner.RegisterDate = DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"));
                                        owner.Address = new DPTAddress()
                                        {
                                            AddressNo = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i),
                                            VillageNo = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_MOO_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i),
                                            Soi = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_SOI_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i),
                                            Road = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_ROAD_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i),
                                            SubDistrict = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_TEXT_" + i),
                                            District = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_TEXT_" + i),
                                            Province = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_TEXT_" + i),
                                            PostCode = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_POSTCODE_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i),
                                            GeoCode = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_PROVINCE_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i) +
                                            model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_AMPHUR_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i) +
                                            model.Data.TryGetData("APP_BUILDING_G1_BUILDING_OWNER").ThenGetStringData("ADDRESS_TUMBOL_APP_BUILDING_R6_BUILDING_OWNER_JURISTIC_ADDRESS_" + i),

                                        };
                                        if (!String.IsNullOrEmpty(phone))
                                        {
                                            contacts.Add(new DPTContact()
                                            {
                                                ContactType = 1,
                                                Detail = phone
                                            });
                                        }
                                        if (!String.IsNullOrEmpty(fax))
                                        {
                                            contacts.Add(new DPTContact
                                            {
                                                ContactType = 4,
                                                Detail = fax
                                            });
                                        }
                                        owner.Contacts = contacts.ToArray();
                                        ownerList.Add(owner);
                                    }

                                }
                                post.Owners = ownerList.ToArray();
                            }

                            #endregion

                            #region [Buildings]
                            var buildingTotal = int.Parse(model.Data.TryGetData("APP_BUILDING_G1_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_G1_BUILDING_INFORMATION_TOTAL"));

                            if (buildingTotal > 0)
                            {
                                var buildingList = new List<DPTBuilding>();

                                for (int i = 0; i < buildingTotal; i++)
                                {
                                    string area_txt = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_G1_BUILDING_AREA_" + i);
                                    decimal area_decimal = System.Convert.ToDecimal(area_txt);
                                    string area = string.Format("{0:n}", area_decimal);
                                    string area_unit = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_G1_BUILDING_AREA_UNIT_" + i);
                                    

                                    string parkingArea_txt = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_G1_BUILDING_PARKINGAREA_" + i);
                                    decimal parkingArea_decimal = System.Convert.ToDecimal(parkingArea_txt);
                                    string parkingArea = string.Format("{0:n}", parkingArea_decimal);

                                    var building = new DPTBuilding()
                                    {
                                        Type = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_G1_BUILDING_TYPE_" + i),
                                        Amount = int.Parse(model.Data.TryGetData("APP_BUILDING_G1_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_G1_BUILDING_AMOUNT_" + i)),
                                        UnitType = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_G1_BUILDING_AMOUNT_TYPE_" + i),
                                        Purpose = GetBuildingPurpose(model.Data.TryGetData("APP_BUILDING_G1_BUILDING_INFORMATION"), i),
                                        ParkingLot = model.Data.TryGetData("APP_BUILDING_G1_BUILDING_INFORMATION").ThenGetStringData("APP_BUILDING_G1_BUILDING_PARKING_" + i),
                                        Area = string.Format("{0} {1}", area, area_unit), 
                                        ParkingArea = parkingArea
                                    };

                                    buildingList.Add(building);
                                }
                                post.Buildings = buildingList.ToArray();
                            }
                            #endregion

                            #region [Title Deed]
                            var DeedTotal = int.Parse(model.Data.TryGetData("APP_BUILDING_G1_TITLE_DEED").ThenGetStringData("APP_BUILDING_G1_TITLE_DEED_TOTAL"));
                            if (DeedTotal > 0)
                            {
                                var deedList = new List<DPTTitleDeed>();
                                var deedFiles = new List<DPTFileMetaData>();
                                for (int i = 0; i < DeedTotal; i++)
                                {
                                    var itemId = model.Data.TryGetData("APP_BUILDING_G1_TITLE_DEED").ThenGetStringData("ARR_ITEM_ID_" + i);
                                    var deed = new DPTTitleDeed()
                                    {
                                        DeedType = int.Parse(model.Data.TryGetData("APP_BUILDING_G1_TITLE_DEED").ThenGetStringData("DROPDOWN_APP_BUILDING_G1_TITLE_DEED_ID_TYPE_" + i)),
                                        DeedNo = model.Data.TryGetData("APP_BUILDING_G1_TITLE_DEED").ThenGetStringData("APP_BUILDING_G1_TITLE_DEED_ID_" + i),
                                        OwnerName = model.Data.TryGetData("APP_BUILDING_G1_TITLE_DEED").ThenGetStringData("APP_BUILDING_G1_TITLE_DEED_OWNER_NAME_" + i),
                                    };
                                    deedList.Add(deed);

                                    var deedFile = consFiles.Where(o => o.FileTypeCode == "TITLE_DEED_COPY_" + itemId).FirstOrDefault();
                                    var deedTypeName = deedFile.Extras.ContainsKey("FILETYPENAME") ? deedFile.Extras["FILETYPENAME"] : string.Empty;
                                    deedFiles.Add(new DPTFileMetaData()
                                    {
                                        FileId = deedFile.FileID,
                                        DocName = deedTypeName,
                                        ContentType = deedFile.ContentType,
                                        FileSize = deedFile.FileSize,
                                        Name = deedFile.FileName,
                                        FileTypeCode = deedFile.FileTypeCode
                                    });

                                }
                                post.TitleDeeds = deedList.ToArray();
                                post.TitleDeedDocuments = deedFiles.ToArray();
                            }
                            #endregion

                            #region [Design En]
                            var desEnTotal = int.Parse(model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ENGINEER").ThenGetStringData("APP_BUILDING_G1_DESIGN_ENGINEER_TOTAL"));
                            if (desEnTotal > 0)
                            {
                                var pplList = new List<DPTEADocument>();

                                for (int i = 0; i < desEnTotal; i++)
                                {
                                    var itemId = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ENGINEER").ThenGetStringData("ARR_ITEM_ID_" + i);
                                    var ppl = new DPTEADocument()
                                    {
                                        EAType = 1,
                                        CitizenId = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ENGINEER").ThenGetStringData("APP_BUILDING_G1_DESIGN_ENGINEER_CITIZENID_" + i),
                                        Title = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ENGINEER").ThenGetStringData("DROPDOWN_APP_BUILDING_G1_DESIGN_ENGINEER_TITLE_TEXT_" + i),
                                        FirstName = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ENGINEER").ThenGetStringData("APP_BUILDING_G1_DESIGN_ENGINEER_FIRSTNAME_" + i),
                                        LastName = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ENGINEER").ThenGetStringData("APP_BUILDING_G1_DESIGN_ENGINEER_LASTNAME_" + i),
                                        LicenseNo = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ENGINEER").ThenGetStringData("APP_BUILDING_G1_DESIGN_ENGINEER_LICENSE_ID_" + i),
                                        MemberNo = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ENGINEER").ThenGetStringData("APP_BUILDING_G1_DESIGN_ENGINEER_MEMBER_NO_" + i)
                                    };

                                    var licenseFile = consFiles.Where(o => o.FileTypeCode == "DESIGN_ENGINEER_PROFESSIONAL_LICENSE_" + itemId).FirstOrDefault();
                                    var licenseTypeName = licenseFile.Extras.ContainsKey("FILETYPENAME") ? licenseFile.Extras["FILETYPENAME"] : string.Empty;
                                    List<DPTFileMetaData> licenseList = new List<DPTFileMetaData>()
                                    {
                                        new DPTFileMetaData()
                                        {
                                            FileId = licenseFile.FileID,
                                            DocName = licenseTypeName,
                                            ContentType = licenseFile.ContentType,
                                            FileSize = licenseFile.FileSize,
                                            Name = licenseFile.FileName,
                                            FileTypeCode = licenseFile.FileTypeCode
                                        }
                                    };
                                    ppl.LicenseForms = licenseList.ToArray();

                                    var consentFile = consFiles.Where(o => o.FileTypeCode == "DESIGN_ENGINEER_CONSENT_DOC_" + itemId).FirstOrDefault();
                                    var consentTypeName = consentFile.Extras.ContainsKey("FILETYPENAME") ? consentFile.Extras["FILETYPENAME"] : string.Empty;
                                    List<DPTFileMetaData> consentList = new List<DPTFileMetaData>()
                                    {
                                        new DPTFileMetaData()
                                        {
                                            FileId = consentFile.FileID,
                                            DocName = consentTypeName,
                                            ContentType = consentFile.ContentType,
                                            FileSize = consentFile.FileSize,
                                            Name = consentFile.FileName,
                                            FileTypeCode = consentFile.FileTypeCode
                                        }
                                    };
                                    ppl.ConsentForms = consentList.ToArray();

                                    pplList.Add(ppl);
                                }

                                post.DesignEngineerDocuments = pplList.ToArray();
                            }
                            #endregion

                            #region [Design Arch]
                            var desArchTotal = int.Parse(model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_DESIGN_ARCHITECT_TOTAL"));
                            if (desArchTotal > 0)
                            {
                                var pplList = new List<DPTEADocument>();
                                for (int i = 0; i < desArchTotal; i++)
                                {
                                    var itemId = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ARCHITECT").ThenGetStringData("ARR_ITEM_ID_" + i);
                                    var ppl = new DPTEADocument()
                                    {
                                        EAType = 2,
                                        CitizenId = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_DESIGN_ARCHITECT_CITIZENID_" + i),
                                        Title = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ARCHITECT").ThenGetStringData("DROPDOWN_APP_BUILDING_G1_DESIGN_ARCHITECT_TITLE_TEXT_" + i),
                                        FirstName = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_DESIGN_ARCHITECT_FIRSTNAME_" + i),
                                        LastName = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_DESIGN_ARCHITECT_LASTNAME_" + i),
                                        LicenseNo = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_DESIGN_ARCHITECT_LICENSE_ID_" + i),
                                        MemberNo = model.Data.TryGetData("APP_BUILDING_G1_DESIGN_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_DESIGN_ARCHITECT_MEMBER_NO_" + i),
                                    };

                                    var licenseFile = consFiles.Where(o => o.FileTypeCode == "DESIGN_ARCHITECT_PROFESSIONAL_LICENSE_" + itemId).FirstOrDefault();
                                    var licenseTypeName = licenseFile.Extras.ContainsKey("FILETYPENAME") ? licenseFile.Extras["FILETYPENAME"] : string.Empty;
                                    List<DPTFileMetaData> licenseList = new List<DPTFileMetaData>()
                                    {
                                        new DPTFileMetaData()
                                        {
                                            FileId = licenseFile.FileID,
                                            DocName = licenseTypeName,
                                            ContentType = licenseFile.ContentType,
                                            FileSize = licenseFile.FileSize,
                                            Name = licenseFile.FileName,
                                            FileTypeCode = licenseFile.FileTypeCode
                                        }
                                    };
                                    ppl.LicenseForms = licenseList.ToArray();

                                    var consentFile = consFiles.Where(o => o.FileTypeCode == "DESIGN_ARCHITECT_CONSENT_DOC_" + itemId).FirstOrDefault();
                                    var consentTypeName = consentFile.Extras.ContainsKey("FILETYPENAME") ? consentFile.Extras["FILETYPENAME"] : string.Empty;
                                    List<DPTFileMetaData> consentList = new List<DPTFileMetaData>()
                                    {
                                        new DPTFileMetaData()
                                        {
                                            FileId = consentFile.FileID,
                                            DocName = consentTypeName,
                                            ContentType = consentFile.ContentType,
                                            FileSize = consentFile.FileSize,
                                            Name = consentFile.FileName,
                                            FileTypeCode = consentFile.FileTypeCode
                                        }
                                    };
                                    ppl.ConsentForms = consentList.ToArray();

                                    pplList.Add(ppl);
                                }
                                post.DesignArchitectDocuments = pplList.ToArray();
                            }
                            #endregion

                            #region [Sup En]
                            var supEnTotal = int.Parse(model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_TOTAL"));
                            if (supEnTotal > 0)
                            {
                                var pplList = new List<DPTEADocument>();
                                for (int i = 0; i < supEnTotal; i++)
                                {
                                    var itemId = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("ARR_ITEM_ID_" + i);
                                    var person_type = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_KIND_OF_PERSON_OPTION_" + i);

                                    if (person_type == "1")//engineer
                                    {
                                        var ppl = new DPTEADocument()
                                        {
                                            EAType = 1,
                                            CitizenId = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZENID_" + i),
                                            Title = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("DROPDOWN_APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_TITLE_TEXT_" + i),
                                            FirstName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_FIRSTNAME_" + i),
                                            LastName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_LASTNAME_" + i),
                                            LicenseNo = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_LICENSE_ID_" + i),
                                            MemberNo = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_ARCHITECT_MEMBER_NO_" + i),
                                        };

                                        var licenseFile = consFiles.Where(o => o.FileTypeCode == "SUPERVISE_ENGINEER_PROFESSIONAL_LICENSE_" + itemId).FirstOrDefault();
                                        var licenseTypeName = licenseFile.Extras.ContainsKey("FILETYPENAME") ? licenseFile.Extras["FILETYPENAME"] : string.Empty;
                                        List<DPTFileMetaData> licenseList = new List<DPTFileMetaData>()
                                        {
                                            new DPTFileMetaData()
                                            {
                                                FileId = licenseFile.FileID,
                                                DocName = licenseTypeName,
                                                ContentType = licenseFile.ContentType,
                                                FileSize = licenseFile.FileSize,
                                                Name = licenseFile.FileName,
                                                FileTypeCode = licenseFile.FileTypeCode
                                            }
                                        };
                                        ppl.LicenseForms = licenseList.ToArray();

                                        var consentFile = consFiles.Where(o => o.FileTypeCode == "SUPERVISE_ENGINEER_CONSENT_DOC_" + itemId).FirstOrDefault();
                                        var consentTypeName = consentFile.Extras.ContainsKey("FILETYPENAME") ? consentFile.Extras["FILETYPENAME"] : string.Empty;
                                        List<DPTFileMetaData> consentList = new List<DPTFileMetaData>()
                                        {
                                            new DPTFileMetaData()
                                            {
                                                FileId = consentFile.FileID,
                                                DocName = consentTypeName,
                                                ContentType = consentFile.ContentType,
                                                FileSize = consentFile.FileSize,
                                                Name = consentFile.FileName,
                                                FileTypeCode = consentFile.FileTypeCode
                                            }
                                        };
                                        ppl.ConsentForms = consentList.ToArray();

                                        pplList.Add(ppl);
                                    }
                                    else//person
                                    {
                                        var ppl = new DPTEADocument()
                                        {
                                            EAType = 3,
                                            CitizenId = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZENID_" + i),
                                            Title = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("DROPDOWN_APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZEN_TITLE_TEXT_" + i),
                                            FirstName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZEN_FIRSTNAME_" + i),
                                            LastName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ENGINEER").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ENGINEER_CITIZEN_LASTNAME_" + i),
                                        };
                                        pplList.Add(ppl);
                                    }
                                }
                                post.SiteEngineerDocuments = pplList.ToArray();
                            }
                            #endregion

                            #region [Sup Arch]
                            var supArchTotal = int.Parse(model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ARCHITECT_TOTAL"));
                            if (supArchTotal > 0)
                            {
                                var pplList = new List<DPTEADocument>();
                                for (int i = 0; i < supArchTotal; i++)
                                {
                                    var itemId = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("ARR_ITEM_ID_" + i);
                                    var ppl = new DPTEADocument()
                                    {
                                        EAType = 2,
                                        CitizenId = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ARCHITECT_CITIZENID_" + i),
                                        Title = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("DROPDOWN_APP_BUILDING_G1_SUPERVISE_ARCHITECT_TITLE_TEXT_" + i),
                                        FirstName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ARCHITECT_FIRSTNAME_" + i),
                                        LastName = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ARCHITECT_LASTNAME_" + i),
                                        LicenseNo = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ARCHITECT_LICENSE_ID_" + i),
                                        MemberNo = model.Data.TryGetData("APP_BUILDING_G1_SUPERVISE_ARCHITECT").ThenGetStringData("APP_BUILDING_G1_SUPERVISE_ARCHITECT_MEMBER_NO_" + i),
                                    };

                                    var licenseFile = consFiles.Where(o => o.FileTypeCode == "SUPERVISE_ARCHITECT_PROFESSIONAL_LICENSE_" + itemId).FirstOrDefault();
                                    var licenseTypeName = licenseFile.Extras.ContainsKey("FILETYPENAME") ? licenseFile.Extras["FILETYPENAME"] : string.Empty;
                                    List<DPTFileMetaData> licenseList = new List<DPTFileMetaData>()
                                    {
                                        new DPTFileMetaData()
                                        {
                                            FileId = licenseFile.FileID,
                                            DocName = licenseTypeName,
                                            ContentType = licenseFile.ContentType,
                                            FileSize = licenseFile.FileSize,
                                            Name = licenseFile.FileName,
                                            FileTypeCode = licenseFile.FileTypeCode
                                        }
                                    };
                                    ppl.LicenseForms = licenseList.ToArray();

                                    var consentFile = consFiles.Where(o => o.FileTypeCode == "SUPERVISE_ARCHITECT_CONSENT_DOC_" + itemId).FirstOrDefault();
                                    var consentTypeName = consentFile.Extras.ContainsKey("FILETYPENAME") ? consentFile.Extras["FILETYPENAME"] : string.Empty;
                                    List<DPTFileMetaData> consentList = new List<DPTFileMetaData>()
                                    {
                                        new DPTFileMetaData()
                                        {
                                            FileId = consentFile.FileID,
                                            DocName = consentTypeName,
                                            ContentType = consentFile.ContentType,
                                            FileSize = consentFile.FileSize,
                                            Name = consentFile.FileName,
                                            FileTypeCode = consentFile.FileTypeCode
                                        }
                                    };
                                    ppl.ConsentForms = consentList.ToArray();

                                    pplList.Add(ppl);
                                }
                                post.SiteArchitectDocuments = pplList.ToArray();
                            }
                            #endregion

                            //var bluePrintFile = consFiles.Where(o => o.FileTypeCode == "CONSTRUCTION_BLUEPRINT").FirstOrDefault();
                            //var bluePrintFileTotal = consFiles.Where(o => o.FileTypeCode.Contains("CONSTRUCTION_BLUEPRINT")).Count();
                            var bluePrintFiles = consFiles.Where(o => o.FileTypeCode.Contains("CONSTRUCTION_BLUEPRINT")).ToList();
                            if (bluePrintFiles.Count > 0)
                            {
                                var pplList = new List<DPTFileMetaData>();
                                for (int i = 0; i < bluePrintFiles.Count; i++)
                                {
                                    //var bluePrintFile = consFiles.Where(o => o.FileTypeCode == "CONSTRUCTION_BLUEPRINT-" + i).FirstOrDefault();
                                    var bluePrintFile = bluePrintFiles[i];
                                    var bluePrintTypeName = bluePrintFile.Extras.ContainsKey("FILETYPENAME") ? bluePrintFile.Extras["FILETYPENAME"] + "(ไฟล์ที่ " + (i + 1) + ")" : string.Empty;
                                    if (bluePrintFile.ContentType == "file/url" && !String.IsNullOrEmpty(bluePrintFile.FileURL))
                                    {
                                        pplList.Add(new DPTFileMetaData()
                                        {
                                            FileId = bluePrintFile.FileID,
                                            DocName = bluePrintTypeName,
                                            ContentType = "text/plain",
                                            FileSize = bluePrintFile.FileSize,
                                            Name = bluePrintTypeName,
                                            //FileTypeCode = bluePrintFile.FileTypeCode,
                                            FileTypeCode = "CONSTRUCTION_BLUEPRINT",
                                            FileUrl = bluePrintFile.FileURL
                                        });
                                    }
                                    else
                                    {
                                        pplList.Add(new DPTFileMetaData()
                                        {
                                            FileId = bluePrintFile.FileID,
                                            DocName = bluePrintTypeName,
                                            ContentType = bluePrintFile.ContentType,
                                            FileSize = bluePrintFile.FileSize,
                                            Name = bluePrintFile.FileName,
                                            //FileTypeCode = bluePrintFile.FileTypeCode,
                                            FileTypeCode = "CONSTRUCTION_BLUEPRINT",
                                        });
                                    }
                                }
                                post.Plans = pplList.ToArray();
                            }
                            
                            //post.Plans = new DPTFileMetaData[]
                            //{
                            //    new DPTFileMetaData()
                            //    {
                            //        FileId = bluePrintFile.FileID,
                            //        DocName = bluePrintTypeName,
                            //        ContentType = bluePrintFile.ContentType,
                            //        FileSize = bluePrintFile.FileSize,
                            //        Name = bluePrintFile.FileName,
                            //        FileTypeCode = bluePrintFile.FileTypeCode
                            //    }
                            //};
                            //var calPlanFileTotal = consFiles.Where(o => o.FileTypeCode.Contains("CALCULATION_PLAN")).Count();
                            var calPlanFiles = consFiles.Where(o => o.FileTypeCode.Contains("CALCULATION_PLAN")).ToList();
                            if (calPlanFiles.Count > 0)
                            {
                                var pplList = new List<DPTFileMetaData>();
                                for (int i = 0; i < calPlanFiles.Count; i++)
                                {
                                    //var calPlanFile = consFiles.Where(o => o.FileTypeCode == "CALCULATION_PLAN-" + i).FirstOrDefault();
                                    var calPlanFile = calPlanFiles[i];
                                    if (calPlanFile != null)
                                    {
                                        var calPlanTypeName = calPlanFile.Extras.ContainsKey("FILETYPENAME") ? calPlanFile.Extras["FILETYPENAME"] + "(ไฟล์ที่ " + (i + 1) + ")" : string.Empty;
                                        pplList.Add(new DPTFileMetaData()
                                        {
                                            FileId = calPlanFile.FileID,
                                            DocName = calPlanTypeName,
                                            ContentType = calPlanFile.ContentType,
                                            FileSize = calPlanFile.FileSize,
                                            Name = calPlanFile.FileName,
                                            //FileTypeCode = calPlanFile.FileTypeCode,
                                            FileTypeCode = "CALCULATION_PLAN",
                                        });
                                    }
                                }
                                post.Calculations = pplList.ToArray();
                            }

                            var buildingOwnerFile = consFiles.Where(o => o.FileTypeCode == "BUILDING_OWNER_DOC").FirstOrDefault();
                            if (buildingOwnerFile != null)
                            {
                                var buildingOwnerTypeName = buildingOwnerFile.Extras.ContainsKey("FILETYPENAME") ? buildingOwnerFile.Extras["FILETYPENAME"] : string.Empty;
                                post.DelegationDocuments = new DPTFileMetaData[]
                                {
                                    new DPTFileMetaData()
                                    {
                                        FileId = buildingOwnerFile.FileID,
                                        DocName = buildingOwnerTypeName,
                                        ContentType = buildingOwnerFile.ContentType,
                                        FileSize = buildingOwnerFile.FileSize,
                                        Name = buildingOwnerFile.FileName,
                                        FileTypeCode = buildingOwnerFile.FileTypeCode
                                    }
                                };
                            }

                            var otherFileSection = model.UploadedFiles.Where(o => o.Description == "FREE_DOC_SECTION").FirstOrDefault();
                            if (otherFileSection != null)
                            {
                                var otherFiles = otherFileSection.Files;
                                var otherFileList = new List<DPTFileMetaData>();
                                var gp = otherFiles.GroupBy(n => n.FileReason);

                                foreach (var g in gp)
                                {
                                    if (g.Count() > 1)
                                    {
                                        int i = 0;
                                        foreach (var file in g)
                                        {
                                            i++;
                                            var fileTypeName = file.Extras.ContainsKey("FILETYPENAME") ? file.Extras["FILETYPENAME"] : string.Empty;

                                            otherFileList.Add(new DPTFileMetaData()
                                            {
                                                FileId = file.FileID,
                                                DocName = (!String.IsNullOrEmpty(file.FileReason) ? file.FileReason : fileTypeName) + " ("+i+")",
                                                ContentType = file.ContentType,
                                                FileSize = file.FileSize,
                                                Name = file.FileName,
                                                FileTypeCode = "OTHER_DOC"
                                            });
                                        }
                                    }
                                    else
                                    {
                                        var file = g.FirstOrDefault();
                                        var fileTypeName = file.Extras.ContainsKey("FILETYPENAME") ? file.Extras["FILETYPENAME"] : string.Empty;

                                        otherFileList.Add(new DPTFileMetaData()
                                        {
                                            FileId = file.FileID,
                                            DocName = (!String.IsNullOrEmpty(file.FileReason) ? file.FileReason : fileTypeName),
                                            ContentType = file.ContentType,
                                            FileSize = file.FileSize,
                                            Name = file.FileName,
                                            FileTypeCode = "OTHER_DOC"
                                        });
                                    }
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
                                if (otherFileList != null && otherFileList.Count > 0)
                                {
                                    post.OtherDocuments = otherFileList.ToArray();
                                }
                            }
                            #endregion
                            
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

                            
                            var apiResponse = Api.Call(regisUrl, HttpVerb.POST, null, jsonPost, ContentType.ApplicationJson);
                            if (apiResponse != null)
                            {
                                if (apiResponse.HasValues)
                                {
                                    if (apiResponse["MessageCode"] != null && apiResponse["MessageCode"].ToString() == "00000")
                                    {
                                        // Clear section data
                                        var singleformReq = new SingleFormRequestEntity();
                                        singleformReq.DeleteSections(model.IdentityID, null,
                                            new string[]
                                            {
                                                "APP_BUILDING_G1_INFORMATION",
                                                "APP_BUILDING_G1_CONSTRUCTION_SITE_INFORMATION",
                                                "APP_BUILDING_G1_BUILDING_OWNER",
                                                "APP_BUILDING_G1_BUILDING_INFORMATION",
                                                "APP_BUILDING_G1_TITLE_DEED",
                                                "APP_BUILDING_G1_DESIGN_ARCHITECT",
                                                "APP_BUILDING_G1_DESIGN_ENGINEER",
                                                "APP_BUILDING_G1_SUPERVISE_ARCHITECT",
                                                "APP_BUILDING_G1_SUPERVISE_ENGINEER"
                                            }
                                        );

                                        // Clear uploaded files
                                        var singleFormTran = SingleFormTransaction.Get(model.IdentityID);
                                        if (singleFormTran != null && singleFormTran.UploadedFiles != null && singleFormTran.UploadedFiles.Count > 0)
                                        {
                                            var fg = singleFormTran.UploadedFiles.Where(o => o.Description == "CONSTRUCTION_SITE_INFORMATION").SingleOrDefault();
                                            if (fg != null)
                                            {
                                                singleFormTran.RemoveUploadedFiles(fg.FileGroupID);
                                            }

                                            var otherFg = singleFormTran.UploadedFiles.Where(o => o.Description == "FREE_DOC_SECTION").FirstOrDefault();
                                            if (otherFg != null)
                                            {
                                                singleFormTran.RemoveUploadedFiles(otherFg.FileGroupID);
                                            }
                                        }

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
                        }
                        break;
                    case AppsStage.UserUpdate:
                        {
                            if (model.Status == ApplicationStatusV2Enum.CHECK || model.Status == ApplicationStatusV2Enum.PENDING)
                            {
                                #region [POST DATA]
                                var post = new DPTG1Request()
                                {
                                    RequestType = "R1",
                                    BizGuid = model.ApplicationRequestID.ToString(),
                                };

                                var requestedFiles = model.UploadedFiles.Where(o => o.Description == "REQUESTED_FILE").OrderByDescending(o => o.CreatedDate).FirstOrDefault();
                                if (requestedFiles != null && requestedFiles.Files != null && requestedFiles.Files.Count > 0)
                                {
                                    List<DPTFileMetaData> bluePrintList = new List<DPTFileMetaData>();
                                    List<DPTFileMetaData> calPlanList = new List<DPTFileMetaData>();
                                    List<DPTFileMetaData> titleDeedList = new List<DPTFileMetaData>();
                                    List<DPTFileMetaData> additionalList = new List<DPTFileMetaData>();
                                    List<DPTFileMetaData> otherList = new List<DPTFileMetaData>();
                                    List<DPTEADocument> deList = new List<DPTEADocument>();
                                    List<DPTEADocument> daList = new List<DPTEADocument>();
                                    List<DPTEADocument> seList = new List<DPTEADocument>();
                                    List<DPTEADocument> saList = new List<DPTEADocument>();

                                    foreach (var file in requestedFiles.Files)
                                    {
                                        var fileTypeCode = file.FileTypeCode;
                                        var fileTypeName = file.Extras.ContainsKey("FILETYPENAME") ? file.Extras["FILETYPENAME"].ToString() : string.Empty;
                                        var fileId = file.Extras.ContainsKey("FILEID") ? file.Extras["FILEID"].ToString() : string.Empty;
                                        
                                        Dictionary<string, object> extras = new Dictionary<string, object>();
                                        extras.Add("FileId", fileId);

                                        var eaValid = false;
                                        var licenseNo = string.Empty;
                                        var eaType = 0;

                                        if (file.Extras.ContainsKey("EADOCUMENT_LICENSENO") && file.Extras.ContainsKey("EADOCUMENT_EATYPE"))
                                        {
                                            eaValid = true;
                                            licenseNo = file.Extras["EADOCUMENT_LICENSENO"].ToString();
                                            eaType = int.Parse(file.Extras["EADOCUMENT_EATYPE"].ToString());
                                        }

                                        if (!string.IsNullOrEmpty(fileTypeCode) && !string.IsNullOrEmpty(fileTypeName) && !string.IsNullOrEmpty(fileId))
                                        {
                                            if (fileTypeCode == "APPLICANT_ID_CARD_COPY")
                                            {
                                                post.CitizenIDCards = new DPTFileMetaData[]
                                                {
                                                    new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    }
                                                };
                                            }
                                            else if (fileTypeCode == "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY")
                                            {
                                                List<DPTFileMetaData> fileList = new List<DPTFileMetaData>()
                                                {
                                                    new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    }
                                                };
                                                post.JuristicPersonRegisterationDocuments = fileList.ToArray();
                                            }
                                            else if (fileTypeCode == "JURISTIC_DELEGATOR_DOC")
                                            {
                                                List<DPTFileMetaData> fileList = new List<DPTFileMetaData>()
                                                {
                                                    new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    }
                                                };
                                                post.DelegationRepresentationDocuments = fileList.ToArray();
                                            }
                                            else if (fileTypeCode == "CONSTRUCTION_BLUEPRINT")
                                            {
                                                if (file.ContentType == "file/url" && !string.IsNullOrEmpty(file.FileURL))
                                                {
                                                    //post.Plans = new DPTFileMetaData[]
                                                    //{
                                                    //    new DPTFileMetaData()
                                                    //    {
                                                    //        FileId = file.FileID,
                                                    //        DocName = fileTypeName,
                                                    //        ContentType = "text/plain",
                                                    //        FileSize = file.FileSize,
                                                    //        Name = fileTypeName,
                                                    //        FileTypeCode = file.FileTypeCode,
                                                    //        FileUrl = file.FileURL, 
                                                    //        Extras = extras
                                                    //    }
                                                    // };
                                                    var item = new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = "text/plain",
                                                        FileSize = file.FileSize,
                                                        Name = fileTypeName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        FileUrl = file.FileURL,
                                                        Extras = extras
                                                    };
                                                    bluePrintList.Add(item);
                                                }
                                                else
                                                {
                                                    //post.Plans = new DPTFileMetaData[]
                                                    //{
                                                    //    new DPTFileMetaData()
                                                    //    {
                                                    //        FileId = file.FileID,
                                                    //        DocName = fileTypeName,
                                                    //        ContentType = file.ContentType,
                                                    //        FileSize = file.FileSize,
                                                    //        Name = file.FileName,
                                                    //        FileTypeCode = file.FileTypeCode, 
                                                    //        Extras = extras
                                                    //    }
                                                    //};
                                                    var item = new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    };
                                                    bluePrintList.Add(item);
                                                }
                                            }
                                            else if (fileTypeCode == "CALCULATION_PLAN")
                                            {
                                                //List<DPTFileMetaData> fileList = new List<DPTFileMetaData>()
                                                //{
                                                //    new DPTFileMetaData()
                                                //    {
                                                //        FileId = file.FileID,
                                                //        DocName = fileTypeName,
                                                //        ContentType = file.ContentType,
                                                //        FileSize = file.FileSize,
                                                //        Name = file.FileName,
                                                //        FileTypeCode = file.FileTypeCode,
                                                //        Extras = extras
                                                //    }
                                                //};
                                                //post.Calculations = fileList.ToArray();
                                                var item = new DPTFileMetaData()
                                                {
                                                    FileId = file.FileID,
                                                    DocName = fileTypeName,
                                                    ContentType = file.ContentType,
                                                    FileSize = file.FileSize,
                                                    Name = file.FileName,
                                                    FileTypeCode = file.FileTypeCode,
                                                    Extras = extras
                                                };
                                                calPlanList.Add(item);
                                            }
                                            else if (fileTypeCode == "BUILDING_OWNER_DOC")
                                            {
                                                List<DPTFileMetaData> fileList = new List<DPTFileMetaData>()
                                                {
                                                    new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    }
                                                };
                                                post.DelegationDocuments = fileList.ToArray();
                                            }
                                            else if (fileTypeCode == "TITLE_DEED_COPY")
                                            {
                                                var item = new DPTFileMetaData()
                                                {
                                                    FileId = file.FileID,
                                                    DocName = fileTypeName,
                                                    ContentType = file.ContentType,
                                                    FileSize = file.FileSize,
                                                    Name = file.FileName,
                                                    FileTypeCode = file.FileTypeCode,
                                                    Extras = extras
                                                };
                                                titleDeedList.Add(item);
                                            }
                                            else if (fileTypeCode == "DESIGN_ENGINEER_PROFESSIONAL_LICENSE" && eaValid)
                                            {
                                                var thisEA = deList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.LicenseForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        LicenseForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    deList.Add(newEA);
                                                }

                                            }
                                            else if (fileTypeCode == "DESIGN_ENGINEER_CONSENT_DOC" && eaValid)
                                            {
                                                var thisEA = deList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.ConsentForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        ConsentForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    deList.Add(newEA);
                                                }
                                            }
                                            else if (fileTypeCode == "DESIGN_ARCHITECT_PROFESSIONAL_LICENSE" && eaValid)
                                            {
                                                var thisEA = daList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.LicenseForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        LicenseForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    daList.Add(newEA);
                                                }
                                            }
                                            else if (fileTypeCode == "DESIGN_ARCHITECT_CONSENT_DOC" && eaValid)
                                            {
                                                var thisEA = daList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.ConsentForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        ConsentForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    daList.Add(newEA);
                                                }
                                            }
                                            else if (fileTypeCode == "SUPERVISE_ENGINEER_PROFESSIONAL_LICENSE" && eaValid)
                                            {
                                                var thisEA = seList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.LicenseForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        LicenseForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    seList.Add(newEA);
                                                }
                                            }
                                            else if (fileTypeCode == "SUPERVISE_ENGINEER_CONSENT_DOC" && eaValid)
                                            {
                                                var thisEA = seList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.ConsentForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        ConsentForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    seList.Add(newEA);
                                                }
                                            }
                                            else if (fileTypeCode == "SUPERVISE_ARCHITECT_PROFESSIONAL_LICENSE" && eaValid)
                                            {
                                                var thisEA = saList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.LicenseForms = new DPTFileMetaData[]
                                                    {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        LicenseForms = new DPTFileMetaData[]
                                                        {
                                                            new DPTFileMetaData()
                                                            {
                                                                FileId = file.FileID,
                                                                DocName = fileTypeName,
                                                                ContentType = file.ContentType,
                                                                FileSize = file.FileSize,
                                                                Name = file.FileName,
                                                                FileTypeCode = file.FileTypeCode,
                                                                Extras = extras
                                                            }
                                                        }
                                                    };
                                                    saList.Add(newEA);
                                                }
                                            }
                                            else if (fileTypeCode == "SUPERVISE_ARCHITECT_CONSENT_DOC" && eaValid)
                                            {
                                                var thisEA = saList.Where(o => o.LicenseNo == licenseNo).FirstOrDefault();
                                                if (thisEA != null)
                                                {
                                                    thisEA.ConsentForms = new DPTFileMetaData[]
                                                    {
                                                    new DPTFileMetaData()
                                                    {
                                                        FileId = file.FileID,
                                                        DocName = fileTypeName,
                                                        ContentType = file.ContentType,
                                                        FileSize = file.FileSize,
                                                        Name = file.FileName,
                                                        FileTypeCode = file.FileTypeCode,
                                                        Extras = extras
                                                    }
                                                    };
                                                }
                                                else
                                                {
                                                    var newEA = new DPTEADocument()
                                                    {
                                                        LicenseNo = licenseNo,
                                                        EAType = eaType,
                                                        ConsentForms = new DPTFileMetaData[]
                                                        {
                                                        new DPTFileMetaData()
                                                        {
                                                            FileId = file.FileID,
                                                            DocName = fileTypeName,
                                                            ContentType = file.ContentType,
                                                            FileSize = file.FileSize,
                                                            Name = file.FileName,
                                                            FileTypeCode = file.FileTypeCode,
                                                            Extras = extras
                                                        }
                                                        }
                                                    };
                                                    saList.Add(newEA);
                                                }
                                            }
                                            else if (fileTypeCode == "ADDITIONAL_DOC")
                                            {
                                                var additionalFile = new DPTFileMetaData()
                                                {
                                                    FileId = file.FileID,
                                                    DocName = fileTypeName,
                                                    ContentType = file.ContentType,
                                                    FileSize = file.FileSize,
                                                    Name = file.FileName,
                                                    FileTypeCode = file.FileTypeCode,
                                                    Extras = extras
                                                };
                                                additionalList.Add(additionalFile);
                                            }
                                            else if (fileTypeCode == "OTHER_DOC")
                                            {
                                                var otherFile = new DPTFileMetaData()
                                                {
                                                    FileId = file.FileID,
                                                    DocName = fileTypeName,
                                                    ContentType = file.ContentType,
                                                    FileSize = file.FileSize,
                                                    Name = file.FileName,
                                                    FileTypeCode = file.FileTypeCode,
                                                    Extras = extras
                                                };
                                                otherList.Add(otherFile);
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(fileTypeCode) && !string.IsNullOrEmpty(fileTypeName))
                                        {
                                            if (fileTypeCode == "ADDITIONAL_DOC")
                                            {
                                                var additionalFile = new DPTFileMetaData()
                                                {
                                                    FileId = file.FileID,
                                                    DocName = fileTypeName,
                                                    ContentType = file.ContentType,
                                                    FileSize = file.FileSize,
                                                    Name = file.FileName,
                                                    FileTypeCode = file.FileTypeCode
                                                };
                                                additionalList.Add(additionalFile);
                                            }
                                            else if (fileTypeCode == "OTHER_DOC")
                                            {
                                                var otherFile = new DPTFileMetaData()
                                                {
                                                    FileId = file.FileID,
                                                    DocName = fileTypeName,
                                                    ContentType = file.ContentType,
                                                    FileSize = file.FileSize,
                                                    Name = file.FileName,
                                                    FileTypeCode = file.FileTypeCode
                                                };
                                                otherList.Add(otherFile);
                                            }
                                        }
                                    }
                                    post.Plans = bluePrintList.Count > 0 ? bluePrintList.ToArray() : null;
                                    post.Calculations = calPlanList.Count > 0 ? calPlanList.ToArray() : null;
                                    post.TitleDeedDocuments = titleDeedList.Count > 0 ? titleDeedList.ToArray() : null;
                                    post.DesignEngineerDocuments = deList.Count > 0 ? deList.ToArray() : null;
                                    post.DesignArchitectDocuments = daList.Count > 0 ? daList.ToArray() : null;
                                    post.SiteEngineerDocuments = seList.Count > 0 ? seList.ToArray() : null;
                                    post.SiteArchitectDocuments = saList.Count > 0 ? saList.ToArray() : null;
                                    post.AdditionalDocuments = additionalList.Count > 0 ? additionalList.ToArray() : null;
                                    post.OtherDocuments = otherList.Count > 0 ? otherList.ToArray() : null;
                                }
                                #endregion
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

                                var apiResponse = Api.Call(regisUrl, HttpVerb.PUT, null, jsonPost, ContentType.ApplicationJson);
                                if (apiResponse != null)
                                {
                                    if (apiResponse.HasValues)
                                    {
                                        if (apiResponse["MessageCode"] != null && apiResponse["MessageCode"].ToString() == "00000")
                                        {
                                            DateTime expDt = DateTime.MinValue;
                                            if (apiResponse["ExpectedFinishDate"] != null && !string.IsNullOrEmpty(apiResponse["MessageCode"].ToString()))
                                            {
                                                var isDt = DateTime.TryParse(apiResponse["ExpectedFinishDate"].ToString(), out expDt);
                                                if (isDt)
                                                {
                                                    request.ExpectedFinishDate = expDt;
                                                }
                                            }
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
        }

        public string[] GetBuildingPurpose(ApplicationRequestDataGroupViewModel model, int buildingNo)
        {
            var result = new List<string>();
            var purpose = new Dictionary<string, string>
            {
                {"1","พักอาศัย"},
                {"2","อยู่อาศัยรวม"},
                {"3","พาณิชยกรรม"},
                {"4","อาคารชุด"},
                {"5","สำนักงาน"},
                {"6","หอพัก"},
                {"7","โรงแรม"},
                {"8","โรงงาน"},
                {"9","คลังสินค้า"},
                {"10","ค้าปลีกค้าส่ง"},
                {"11","ชุมนุมคน"},
                {"12","โรงมหรสพ"},
                {"13","ป้ายโฆษณา"},
                {"14","สถานศึกษา"},
                {"15","สถานพยาบาล"},
                {"16","อุตสาหกรรม"},
                {"17","ที่จอดรถยนต์"},
                {"18","สถานบริการ"},
                {"19","หอประชุม"},
                {"20","เครื่องเล่น"},
                {"21","เพื่อการศาสนา"},
                {"22","เก็บวัตถุอันตราย"},
                {"23","เลี้ยงสัตว์"},
                {"24","รั้ว กำแพง"},
                {"25","อื่นๆ"}
            };

            foreach (var p in purpose)
            {
                var isSelected = model.ThenGetBooleanData($"APP_BUILDING_G1_BUILDING_FOR_{p.Key}_{buildingNo}");

                if (isSelected)
                {

                    if (p.Key == "25")
                    {
                        result.Add(model.ThenGetStringData($"APP_BUILDING_G1_BUILDING_FOR_{p.Key}_TEXT_{buildingNo}"));
                    }
                    else
                    {
                        result.Add(p.Value);
                    }
                }
            }

            return result.ToArray();
        }
    }
}
