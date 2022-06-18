using BizPortal.Utils.Helpers;
using EGA.Owin.Security.EGAOpenID;
using EGA.WS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using EGA.Owin.Security.Utils.Extensions;
using Newtonsoft.Json.Linq;
using System.Configuration;
using BizPortal.ViewModels.SingleForm;
using Newtonsoft.Json;
using BizPortal.DAL.MongoDB;
using Mapster;
using System.Web.Mvc;
using BizPortal.Areas.WebApi.Controllers;
using System.Threading.Tasks;

namespace BizPortal.Service
{
    public class PredocService: IProgress<int>
    {

        private EGAWSAPI api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);

        public class PredocInfo
        {
            public string MimeType;
            public string ContentBase64;
            public byte[] Content
            {
                get
                {
                    return Base64UrlStringHelper.Base64urlStringToByte(ContentBase64);
                }
            }
            internal PredocInfo(JObject egaWsResponse)
            {
                MimeType = egaWsResponse["mimeType"].ToString();
                ContentBase64 = egaWsResponse["Result"].ToString();
            }
        }

        public class UnknownPredocException: Exception
        {
            public UnknownPredocException(string message) : base(message) { }
        }
        public class PredocNotFoundException : Exception
        {
            public PredocNotFoundException(string message) : base(message) { }
        }

        public async Task<FileMetadataEntity> preparePredocForAttachmentScreen(IPrincipal currentUser, SingleFormAttachmentViewModel viewModel)
        {
            try
            {
                var predoc = GetPredoc(currentUser, viewModel.FileName, null);
                string serviceTokenPath = ConfigurationManager.AppSettings["FileServiceUploadTokenPath"];
                string serviceUploadPath = ConfigurationManager.AppSettings["FileServicePath"];
                string consumerKey = ConfigurationManager.AppSettings["FileConsumerKey"];
                string secret = ConfigurationManager.AppSettings["FileConsumerSecret"];

                var fileConsumers = new string[] { };
                if (viewModel.FileConsumerKey != null && viewModel.FileConsumerKey.Count > 0)
                {
                    fileConsumers = viewModel.FileConsumerKey.ToArray();
                }

                var stream = new System.IO.MemoryStream(predoc.Content);
                var fileInfo = new
                {
                    Name = viewModel.PreDocFileName,
                    ContentType = predoc.MimeType,
                    Size = stream.Length,
                    IsPublic = false,
                    Consumers = fileConsumers
                };
                string fileInfoJson = JsonConvert.SerializeObject(fileInfo);
                string token = FileHelper.RequestAccessToken(serviceTokenPath, consumerKey, secret, fileInfoJson);

                var uploaded = await FileHelper.UploadFile(serviceUploadPath, token, consumerKey, secret, fileInfoJson, stream, this);
                var fileItem = new FileMetadataEntity();
                TypeAdapter.Adapt<FileItem, FileMetadataEntity>(uploaded, fileItem);

                fileItem.IdentityID = getIdentityID(currentUser);
                fileItem.IdentityType = getIdentityType(currentUser);
                fileItem.Extras = new Dictionary<string, object>
                {
                    { "OWNER_IDENT_ID", fileItem.IdentityID },
                    { "OWNERS", fileConsumers }
                };
                
                //var fdb = MongoFactory.GetFileMetadataCollection();
                //fdb.InsertOne(fileItem);

                viewModel.PreDoc = true;
                return fileItem;
            }
            catch (Exception e)
            {
                viewModel.PreDoc = false;
                return null;
            }
        }

        public PredocInfo GetPredoc(IPrincipal currentUser, string docName, string id = null)
        {
            switch (docName)
            {
                case "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY":
                    return DownloadJuristicCert(currentUser, id);
                case "ID_CARD_COPY":
                    return DownloadCitizenCert(currentUser, id);
                //case "JURISTIC_MEMORANDUM":
                //    return DownloadJuristicMemorandum(currentUser, id);
                case "JURISTIC_SHARE_HOLDER_LIST":
                    return DownloadJuristicShareHolderList(currentUser, id);
                default:
                    throw new UnknownPredocException("Unknown Predoc name '" + docName + "'");
            }
        }

        private void allowOwnerOrOrgAgent(IPrincipal currentUser, string identityID)
        {
            if (currentUser.Identity.IsAuthenticated
                    && (currentUser.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME) ||
                        currentUser.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME)))
            {
                return;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        private PredocInfo DownloadCitizenCert(IPrincipal currentUser, string id = null)
        {
            var currentUserType = getIdentityType(currentUser);
            if (id != null)
            {
                allowOwnerOrOrgAgent(currentUser, id);
            }
            else if (currentUser.Identity.IsAuthenticated && currentUserType == UserTypeEnum.Citizen)
            {
                id = getIdentityID(currentUser);
            }
            else
            {
                // Other type of user
                throw new PredocNotFoundException("Can't get pre-doc CitizenCert for user of type " + currentUserType);
            }
            api.OnCheckingApplicationError += (ex) =>
            {
                var egaEx = ex as EGAWSAPIException;
            };
            var certificate = api.Get("/ega/citizen/pdf?citizenID=" + id);
            if (certificate.HasValues)
            {
                return new PredocInfo(certificate);
            }
            throw new PredocNotFoundException("Can't get pre-doc CitizenCert for ID: " + id);
        }

        public PredocInfo DownloadJuristicShareHolderList(IPrincipal currentUser, string id = null)
        {
            var currentUserType = getIdentityType(currentUser);
            if (id != null)
            {
                allowOwnerOrOrgAgent(currentUser, id);
            }
            else if (currentUser.Identity.IsAuthenticated && currentUserType == UserTypeEnum.Juristic)
            {
                id = getIdentityID(currentUser);
            }
            else
            {
                // Other type of user
                throw new PredocNotFoundException("Can't get pre-doc JuristicShareHolderList for user of type " + currentUserType);
            }

            api.OnCheckingApplicationError += (ex) =>
            {
                var egaEx = ex as EGAWSAPIException;
            };

            var doc = api.Get("/dbd/v2/shareholder/pdf?JuristicID=" + id);
            if (doc.HasValues)
            {
                return new PredocInfo(doc);
            }
            throw new PredocNotFoundException("Can't get pre-doc JuristicShareHolderList for ID: " + id);
        }

        public PredocInfo DownloadJuristicMemorandum(IPrincipal currentUser, string id = null)
        {
            var currentUserType = getIdentityType(currentUser);
            if (id != null)
            {
                allowOwnerOrOrgAgent(currentUser, id);
            }
            else if (currentUser.Identity.IsAuthenticated && currentUserType == UserTypeEnum.Juristic)
            {
                id = getIdentityID(currentUser);
            }
            else
            {
                // Other type of user
                throw new PredocNotFoundException("Can't get pre-doc JuristicMemorandum for user of type " + currentUserType);
            }
            api.OnCheckingApplicationError += (ex) =>
            {
                var egaEx = ex as EGAWSAPIException;
            };
            var doc = api.Get("/dbd/v2/juristic/document/pdf?JuristicID=" + id + "&Page=0");
            if (doc.HasValues)
            {
                return new PredocInfo(doc);
            }
            throw new PredocNotFoundException("Can't get pre-doc JuristicMemorandum for ID: " + id);
        }

        public PredocInfo DownloadJuristicCert(IPrincipal currentUser, string id = null)
        {
            var currentUserType = getIdentityType(currentUser);
            if (id != null)
            {
                allowOwnerOrOrgAgent(currentUser, id);
            }
            else if (currentUser.Identity.IsAuthenticated && currentUserType == UserTypeEnum.Juristic)
            {
                id = getIdentityID(currentUser);
            }
            else
            {
                // Other type of user
                throw new PredocNotFoundException("Can't get pre-doc JuristicCert for user of type " + currentUserType);
            }
            api.OnCheckingApplicationError += (ex) =>
            {
                var egaEx = ex as EGAWSAPIException;
            };
            var certificate = api.Get("/dbd/v2/juristic/certificate/signed?JuristicID=" + id);
            if (certificate.HasValues)
            {
                return new PredocInfo(certificate);
            }
            throw new PredocNotFoundException("Can't get pre-doc JuristicCert for ID: " + id);
        }

        

        private UserTypeEnum getIdentityType(IPrincipal currentUser)
        {
            string userType = currentUser.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.UserType);
            return EnumUtils.GetEnum<UserTypeEnum>(userType, true);
        }

        private string getIdentityID(IPrincipal currentUser)
        {
            UserTypeEnum identityType = getIdentityType(currentUser);
            if (identityType == UserTypeEnum.Juristic)
                return currentUser.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.JuristicID);
            else if (identityType == UserTypeEnum.Citizen)
                return currentUser.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.CitizenID);
            else if (identityType == UserTypeEnum.Foreigner)
                return currentUser.Identity.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.PassportID);
            else
                return string.Empty;
        }

        public void Report(int value)
        {
            
        }
    }
}