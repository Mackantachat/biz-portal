using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizPortal.Service
{
    public class TrackService
    {
        public static string fileNameByCodeFunc(Dictionary<string, string> appTranslates, string code)
        {
            string fileName = string.Empty;

            if (!string.IsNullOrEmpty(code))
            {
                if (appTranslates != null && appTranslates.ContainsKey(code))
                {
                    fileName = appTranslates[code];
                }
                else
                {
                    fileName = Utils.Helpers.ResourceHelper.GetResourceWordWithDefault(code.ToUpper(), "FileType", code);
                }
            }

            return fileName;
        }

        public static string fileNameFunc(FileMetadata file, Dictionary<string, string> appTranslates)
        {
            string fileName = fileNameByCodeFunc(appTranslates, file.FileTypeCode ?? file.Extras.TryGetString("FILETYPECODE"));

            if (string.IsNullOrEmpty(fileName) || (fileName == file.FileTypeCode && !string.IsNullOrEmpty(file.Extras.TryGetString("DISPLAYNAME"))))
            {
                if (fileName == file.FileTypeCode && !string.IsNullOrEmpty(file.Extras.TryGetString("DISPLAYNAME")))
                {
                    fileName = file.Extras.TryGetString("DISPLAYNAME");
                }
                else if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(file.FileTypeCode) && !string.IsNullOrEmpty(file.Extras.TryGetString("REQUEST_FILE_NAME")))
                {
                    fileName = file.Extras.TryGetString("REQUEST_FILE_NAME");
                }
                else if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(file.FileTypeCode) && !string.IsNullOrEmpty(file.Extras.TryGetString("FILETYPEDESC")))
                {
                    fileName = file.Extras.TryGetString("FILETYPEDESC");
                }
                else
                {
                    fileName = file.FileTypeCode;
                }
            }

            return fileName;
        }

        public static string fileNameFunc(FileMetadataEntity file, Dictionary<string, string> appTranslates)
        {
            string fileName = fileNameByCodeFunc(appTranslates, file.FileTypeCode ?? file.Extras.TryGetString("FILETYPECODE"));

            if (string.IsNullOrEmpty(fileName) || (fileName == file.FileTypeCode && !string.IsNullOrEmpty(file.Extras.TryGetString("DISPLAYNAME"))))
            {
                if (fileName == file.FileTypeCode && !string.IsNullOrEmpty(file.Extras.TryGetString("DISPLAYNAME")))
                {
                    fileName = file.Extras.TryGetString("DISPLAYNAME");
                }
                else if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(file.FileTypeCode) && !string.IsNullOrEmpty(file.Extras.TryGetString("REQUEST_FILE_NAME")))
                {
                    fileName = file.Extras.TryGetString("REQUEST_FILE_NAME");
                }
                else if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(file.FileTypeCode) && !string.IsNullOrEmpty(file.Extras.TryGetString("FILETYPEDESC")))
                {
                    fileName = file.Extras.TryGetString("FILETYPEDESC");
                }
                else
                {
                    fileName = file.FileTypeCode;
                }
            }

            return fileName;

        }

        public static bool IsCanGotApplicationNow(bool permitCanBeDeliveredOnPayment, string paymentMethod, string permitDeliveryType)
        {
            bool isGotAppNow = false;
            try
            {
                if (permitCanBeDeliveredOnPayment == true &&
                    paymentMethod == PaymentMethodValueConst.AT_OWNER_ORG &&
                    permitDeliveryType == PermitDeliveryTypeValueConst.AT_OWNER_ORG)
                {
                    isGotAppNow = true;
                }
                return isGotAppNow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetStoreName(Dictionary<string, ApplicationRequestDataGroupViewModel> data)
        {
            string storeName = "";
            try
            {
                if (data != null && data.ContainsKey("INFORMATION_STORE"))
                {
                    var lang = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                    string storeNameKey = "INFORMATION_STORE_NAME_TH";
                    if (lang.ToLower() == "en")
                    {
                        storeNameKey = "INFORMATION_STORE_NAME_EN";
                    }

                    storeName = data["INFORMATION_STORE"].Data.TryGetString(storeNameKey, storeNameKey);
                }
                return storeName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetStoreName(Dictionary<string, ApplicationRequestDataGroupEntity> data)
        {
            string storeName = "";
            try
            {
                if (data != null && data.ContainsKey("INFORMATION_STORE"))
                {
                    var lang = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                    string storeNameKey = "INFORMATION_STORE_NAME_TH";
                    if (lang.ToLower() == "en")
                    {
                        storeNameKey = "INFORMATION_STORE_NAME_EN";
                    }

                    storeName = data["INFORMATION_STORE"].Data.TryGetString(storeNameKey, storeNameKey);
                }
                return storeName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetDisPlayIdentityName(UserTypeEnum identityType, string storeName, string identityName)
        {
            string name = identityName;
            try
            {
                if (identityType == UserTypeEnum.Juristic)
                {
                    //name = storeName;
                }
                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CheckResponseForm(ApplicationStatusV2Enum status, string statusOther)
        {
            bool value = false;
            if (string.IsNullOrEmpty(statusOther))
            {
                return false;
            }

            if (statusOther == ApplicationStatusOtherValueConst.WAITING_USER_WORKING)
            {
                value = false;
            }
            else if (statusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING)
            {
                value = true;
            }

            return value;
        }
    }
}