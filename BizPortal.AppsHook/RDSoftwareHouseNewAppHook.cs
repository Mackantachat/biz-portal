using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.AppsHook
{
    public class RDSoftwareHouseNewAppHook : StoreBaseAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public override bool IsEnabledChat()
        {
            return true;
        }

        public override bool IsEnabledExportData(ApplicationStatusV2Enum status)
        {
            var canExportStatus = new ApplicationStatusV2Enum[] 
            {
                ApplicationStatusV2Enum.PENDING
            };

            return canExportStatus.Contains(status);
        }

        public override string GenerateRequestData(Guid applicationrequestId)
        {
            var request = ApplicationRequestEntity.Get(applicationrequestId);
            var exportRequest = new RDSoftwareHouseRequestViewModel();
            var generalInfomation = request.Data.TryGetData("GENERAL_INFORMATION");
            var storeInformation = request.Data.TryGetData("INFORMATION_STORE");
            var bussinessDetailNew = request.Data.TryGetData("SOFTWARE_HOUSE_BUSINESS_DETAIL_NEW");
            var bussinessConfirmNew = request.Data.TryGetData("SOFTWARE_HOUSE_BUSINESS_CONFIRM_NEW");
            var softwareModule = request.Data.TryGetData("SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE");  
            var uploadedfiles = request.UploadedFiles.SelectMany(e => e.Files).ToList();
            var nameModule = softwareModule.ThenGetStringArrayData("SOFTWARE_MODULE_NAME");

            //exportRequest.DOCRD = "";
            //exportRequest.DATEDOCRD = "";
            //exportRequest.RDSTS = "";
            //exportRequest.DATERD = "";
            //exportRequest.RDDOCTAX = "";
            //exportRequest.RDDATEDOCTAX = "";
            //exportRequest.RDDOCSN = "";
            //exportRequest.RDDATEDOCSN = "";

            if (request.IdentityType == UserTypeEnum.Citizen)
            {
                exportRequest.TIN = generalInfomation.ThenGetStringData("IDENTITY_ID");
                exportRequest.TTITLE = generalInfomation.ThenGetStringData("DROPDOWN_CITIZEN_TITLE_TEXT");
                exportRequest.FNAME = $"{generalInfomation.ThenGetStringData("CITIZEN_NAME")} {generalInfomation.ThenGetStringData("CITIZEN_LASTNAME")}";
                exportRequest.DOC3 = uploadedfiles.Any(e => e.FileTypeCode == "ID_CARD_COPY") ? 1 : 0;
                exportRequest.DOC4 = uploadedfiles.Any(e => e.FileTypeCode == "ID_CARD_COPY") ? 1 : 0;
            }
            else
            {
                exportRequest.TIN = generalInfomation.ThenGetStringData("IDENTITY_ID");
                exportRequest.TTITLE = generalInfomation.ThenGetStringData("GENERAL_INFORMATION__JURISTIC_TYPE");
                exportRequest.FNAME = generalInfomation.ThenGetStringData("COMPANY_NAME_TH");
                exportRequest.DOC3 = uploadedfiles.Any(e => e.FileTypeCode == "JURISTIC_COMMITTEE_ID_CARD_0") ? 1 : 0;
                //exportRequest.DOC4 = uploadedfiles.Any(e => e.FileTypeCode == "ID_CARD_COPY") ? 1 : 0;
            }

            exportRequest.APPLICATIONREQUESTNUMBER = request.ApplicationRequestNumber;
            exportRequest.CREATEDDATE = request.CreatedDate.ToString();

            exportRequest.IDRDSW = bussinessDetailNew.ThenGetStringData("LATEST_SOFTWARE_ID_NUMBER");

            exportRequest.SOFTNAME = bussinessDetailNew.ThenGetStringData("SOFTWARE_NAME");
            exportRequest.SWVERSION = bussinessDetailNew.ThenGetStringData("SOFTWARE_VERSION");

            exportRequest.SOFTTYPE1 = bussinessConfirmNew.ThenGetStringData("SOFTWARE_TYPE_NEW_OPTION").ToLower() == "one" ? 1 : 0;
            exportRequest.SOFTTYPE2 = bussinessConfirmNew.ThenGetStringData("SOFTWARE_TYPE_NEW_OPTION").ToLower() == "two" ? 1 : 0;
            exportRequest.SOFTTYPE3 = bussinessConfirmNew.ThenGetStringData("SOFTWARE_TYPE_NEW_OPTION").ToLower() == "three" ? 1 : 0;
            exportRequest.SOFTTYPE4 = bussinessConfirmNew.ThenGetStringData("SOFTWARE_TYPE_NEW_OPTION").ToLower() == "four" ? 1 : 0;

            exportRequest.TYPETAX1 = bussinessConfirmNew.ThenGetStringData("SOFTWARE_HOUSE_PROP_AND_INFO_NEW_OPTION").ToLower() == "one" ? 1 : 0;
            exportRequest.TYPETAX2 = bussinessConfirmNew.ThenGetStringData("SOFTWARE_HOUSE_PROP_AND_INFO_NEW_OPTION").ToLower() == "two" ? 1 : 0;
            exportRequest.TYPETAX3 = bussinessConfirmNew.ThenGetStringData("SOFTWARE_HOUSE_PROP_AND_INFO_NEW_OPTION").ToLower() == "three" ? 1 : 0;
            exportRequest.TYPETAX4 = bussinessConfirmNew.ThenGetStringData("SOFTWARE_HOUSE_PROP_AND_INFO_NEW_OPTION").ToLower() == "four" ? 1 : 0;

            exportRequest.DOC1 = uploadedfiles.Any(e => e.FileTypeCode == "SYSTEM_STRUCTURE_DOC") ? 1 : 0;
            exportRequest.DOC2 = uploadedfiles.Any(e => e.FileTypeCode == "SOFTWARE_MANUAL") ? 1 : 0;
            exportRequest.DOC5 = uploadedfiles.Any(e => e.FileTypeCode == "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY") ? 1 : 0;

            exportRequest.FORBUS1 = bussinessDetailNew.ThenGetStringData("SOFTWARE_PRODUCT_OR_SERVICE_TYPE_SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_1").ToLower() == "true" ? 1 : 0;
            exportRequest.FORBUS2 = bussinessDetailNew.ThenGetStringData("SOFTWARE_PRODUCT_OR_SERVICE_TYPE_SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_2").ToLower() == "true" ? 1 : 0;
            exportRequest.FORBUS3 = bussinessDetailNew.ThenGetStringData("SOFTWARE_PRODUCT_OR_SERVICE_TYPE_SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_3").ToLower() == "true" ? 1 : 0;
            exportRequest.FORBUS4 = bussinessDetailNew.ThenGetStringData("SOFTWARE_PRODUCT_OR_SERVICE_TYPE_SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_4").ToLower() == "true" ? 1 : 0;

            exportRequest.SIZEBUSINESS = bussinessDetailNew.ThenGetIntData("DROPDOWN_STORE_SIZE");

            exportRequest.NUMMODULE = bussinessDetailNew.ThenGetStringData("SOFTWARE_TOTAL_MODULE");
            exportRequest.NAMEMODULE = nameModule != null ? string.Join("^", nameModule) : "";

            exportRequest.BLGNAME = storeInformation.ThenGetStringData("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS");
            exportRequest.VILLAGE = "";
            exportRequest.ROOMNO = storeInformation.ThenGetStringData("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS");
            exportRequest.FLOORNO = storeInformation.ThenGetStringData("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS");
            exportRequest.ADDNO = storeInformation.ThenGetStringData("ADDRESS_INFORMATION_STORE__ADDRESS");
            exportRequest.MOONO = storeInformation.ThenGetStringData("ADDRESS_MOO_INFORMATION_STORE__ADDRESS");
            exportRequest.SOINAM = storeInformation.ThenGetStringData("ADDRESS_SOI_INFORMATION_STORE__ADDRESS");
            exportRequest.YAK = storeInformation.ThenGetStringData("ADDRESS_YAK_INFORMATION_STORE__ADDRESS");
            exportRequest.THNNAM = storeInformation.ThenGetStringData("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS");
            exportRequest.TAMCOD = storeInformation.ThenGetStringData("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT");
            exportRequest.AMPCOD = storeInformation.ThenGetStringData("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT");
            exportRequest.PROVCOD = storeInformation.ThenGetStringData("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT");
            exportRequest.POSCOD = storeInformation.ThenGetStringData("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS");
            exportRequest.OFFCD = "";

            //if (bussinessDetailNew.ThenGetStringData("VAT_LICENSE_LIST_AT_OPTION").ToLower() == "main")
            //{
            //    if (request.IdentityType == UserTypeEnum.Citizen)
            //    {
            //        var citizenAddressInformation = request.Data.TryGetData("CITIZEN_ADDRESS_INFORMATION");

            //        exportRequest.BLGNAME = citizenAddressInformation.ThenGetStringData("ADDRESS_BUILDING_BRANCH_ADDRESS");
            //        exportRequest.VILLAGE = "";
            //        exportRequest.ROOMNO = citizenAddressInformation.ThenGetStringData("ADDRESS_ROOMNO_BRANCH_ADDRESS");
            //        exportRequest.FLOORNO = citizenAddressInformation.ThenGetStringData("ADDRESS_FLOOR_BRANCH_ADDRESS");
            //        exportRequest.ADDNO = citizenAddressInformation.ThenGetStringData("ADDRESS_BRANCH_ADDRESS");
            //        exportRequest.MOONO = citizenAddressInformation.ThenGetStringData("ADDRESS_MOO_BRANCH_ADDRESS");
            //        exportRequest.SOINAM = citizenAddressInformation.ThenGetStringData("ADDRESS_SOI_BRANCH_ADDRESS");
            //        exportRequest.THNNAM = citizenAddressInformation.ThenGetStringData("ADDRESS_ROAD_BRANCH_ADDRESS");
            //        exportRequest.TAMCOD = citizenAddressInformation.ThenGetStringData("ADDRESS_TUMBOL_BRANCH_ADDRESS");
            //        exportRequest.AMPCOD = citizenAddressInformation.ThenGetStringData("ADDRESS_AMPHUR_BRANCH_ADDRESS");
            //        exportRequest.PROVCOD = citizenAddressInformation.ThenGetStringData("ADDRESS_PROVINCE_BRANCH_ADDRESS");
            //        exportRequest.POSCOD = citizenAddressInformation.ThenGetStringData("ADDRESS_POSTCODE_BRANCH_ADDRESS");
            //        exportRequest.OFFCD = "";
            //    }
            //    else 
            //    {
            //        var juristicAddressInformation = request.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION");

            //        exportRequest.BLGNAME = juristicAddressInformation.ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS");
            //        exportRequest.VILLAGE = "";
            //        exportRequest.ROOMNO = juristicAddressInformation.ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS");
            //        exportRequest.FLOORNO = juristicAddressInformation.ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS");
            //        exportRequest.ADDNO = juristicAddressInformation.ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS");
            //        exportRequest.MOONO = juristicAddressInformation.ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS");
            //        exportRequest.SOINAM = juristicAddressInformation.ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS");
            //        exportRequest.THNNAM = juristicAddressInformation.ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS");
            //        exportRequest.TAMCOD = juristicAddressInformation.ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT");
            //        exportRequest.AMPCOD = juristicAddressInformation.ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT");
            //        exportRequest.PROVCOD = juristicAddressInformation.ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT");
            //        exportRequest.POSCOD = juristicAddressInformation.ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS");
            //        exportRequest.OFFCD = "";
            //    }
            //}
            //else 
            //{
            //    exportRequest.BLGNAME = bussinessDetailNew.ThenGetStringData("ADDRESS_BUILDING_BRANCH_ADDRESS");
            //    exportRequest.VILLAGE = "";
            //    exportRequest.ROOMNO = bussinessDetailNew.ThenGetStringData("ADDRESS_ROOMNO_BRANCH_ADDRESS");
            //    exportRequest.FLOORNO = bussinessDetailNew.ThenGetStringData("ADDRESS_FLOOR_BRANCH_ADDRESS");
            //    exportRequest.ADDNO = bussinessDetailNew.ThenGetStringData("ADDRESS_BRANCH_ADDRESS");
            //    exportRequest.MOONO = bussinessDetailNew.ThenGetStringData("ADDRESS_MOO_BRANCH_ADDRESS");
            //    exportRequest.SOINAM = bussinessDetailNew.ThenGetStringData("ADDRESS_SOI_BRANCH_ADDRESS");
            //    exportRequest.THNNAM = bussinessDetailNew.ThenGetStringData("ADDRESS_ROAD_BRANCH_ADDRESS");
            //    exportRequest.TAMCOD = bussinessDetailNew.ThenGetStringData("ADDRESS_TUMBOL_BRANCH_ADDRESS_TEXT");
            //    exportRequest.AMPCOD = bussinessDetailNew.ThenGetStringData("ADDRESS_AMPHUR_BRANCH_ADDRESS_TEXT");
            //    exportRequest.PROVCOD = bussinessDetailNew.ThenGetStringData("ADDRESS_PROVINCE_BRANCH_ADDRESS_TEXT");
            //    exportRequest.POSCOD = bussinessDetailNew.ThenGetStringData("ADDRESS_POSTCODE_BRANCH_ADDRESS");
            //    exportRequest.OFFCD = "";
            //}

            return JsonConvert.SerializeObject(exportRequest, Formatting.Indented);
        }

        public override bool InvokeSingleForm(Guid trid, string currentSectionGroup, ref SingleFormRequestViewModel model)
        {
            var result = false;
            var darftData = SingleFormRequestEntity.Get(model.IdentityID);
            if (darftData.SectionData.Any(e => e.SectionName == "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL"))
            {
                var mudulenbr = GetSectionData(model, "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE", SectionType.Form);
                var muduleinfo = GetSectionData(model, "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL", SectionType.Form);
                muduleinfo.FormData = new Dictionary<string, object>
                {
                    { "SOFTWARE_TOTAL_MODULE", mudulenbr.ArrayData.Count},                  
                };

            }

            result = true;

            return result;
        }
    }
}

