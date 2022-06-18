using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels;
using BizPortal.ViewModels.SingleForm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BizPortal.AppsHook
{
    public class RDSoftwareHouseEditAppHook : StoreBaseAppHook
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
            var bussinessDetailEdit = request.Data.TryGetData("SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT");
            var bussinessConfirmEdit = request.Data.TryGetData("SOFTWARE_HOUSE_BUSINESS_CONFIRM_EDIT");
            var softwareModuleEdit = request.Data.TryGetData("SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EDIT");
            var uploadedfiles = request.UploadedFiles.SelectMany(e => e.Files).ToList();
            var nameModule = softwareModuleEdit.ThenGetStringArrayData("SOFTWARE_MODULE_NAME");

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

            exportRequest.IDRDSW = bussinessDetailEdit.ThenGetStringData("LATEST_SOFTWARE_ID_NUMBER_EDIT");

            exportRequest.SOFTNAME = bussinessDetailEdit.ThenGetStringData("SOFTWARE_NAME_EDIT");
            exportRequest.SWVERSION = bussinessDetailEdit.ThenGetStringData("SOFTWARE_VERSION_EDIT");

            exportRequest.SOFTTYPE1 = bussinessConfirmEdit.ThenGetStringData("SOFTWARE_TYPE_EDIT_OPTION").ToLower() == "one" ? 1 : 0;
            exportRequest.SOFTTYPE2 = bussinessConfirmEdit.ThenGetStringData("SOFTWARE_TYPE_EDIT_OPTION").ToLower() == "two" ? 1 : 0;
            exportRequest.SOFTTYPE3 = bussinessConfirmEdit.ThenGetStringData("SOFTWARE_TYPE_EDIT_OPTION").ToLower() == "three" ? 1 : 0;
            exportRequest.SOFTTYPE4 = bussinessConfirmEdit.ThenGetStringData("SOFTWARE_TYPE_EDIT_OPTION").ToLower() == "four" ? 1 : 0;

            exportRequest.TYPETAX1 = bussinessConfirmEdit.ThenGetStringData("SOFTWARE_HOUSE_PROP_AND_INFO_EDIT_OPT").ToLower() == "one" ? 1 : 0;
            exportRequest.TYPETAX2 = bussinessConfirmEdit.ThenGetStringData("SOFTWARE_HOUSE_PROP_AND_INFO_EDIT_OPT").ToLower() == "two" ? 1 : 0;
            exportRequest.TYPETAX3 = bussinessConfirmEdit.ThenGetStringData("SOFTWARE_HOUSE_PROP_AND_INFO_EDIT_OPT").ToLower() == "three" ? 1 : 0;
            exportRequest.TYPETAX4 = bussinessConfirmEdit.ThenGetStringData("SOFTWARE_HOUSE_PROP_AND_INFO_EDIT_OPT").ToLower() == "four" ? 1 : 0;

            exportRequest.DOC1 = uploadedfiles.Any(e => e.FileTypeCode == "SYSTEM_STRUCTURE_DOC") ? 1 : 0;
            exportRequest.DOC2 = uploadedfiles.Any(e => e.FileTypeCode == "SOFTWARE_MANUAL") ? 1 : 0;
            exportRequest.DOC5 = uploadedfiles.Any(e => e.FileTypeCode == "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY") ? 1 : 0;

            exportRequest.FORBUS1 = bussinessDetailEdit.ThenGetStringData("SOFTWARE_PRODUCT_OR_SERVICE_TYPE_EDIT_SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_1_EDIT").ToLower() == "true" ? 1 : 0;
            exportRequest.FORBUS2 = bussinessDetailEdit.ThenGetStringData("SOFTWARE_PRODUCT_OR_SERVICE_TYPE_EDIT_SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_2_EDIT").ToLower() == "true" ? 1 : 0;
            exportRequest.FORBUS3 = bussinessDetailEdit.ThenGetStringData("SOFTWARE_PRODUCT_OR_SERVICE_TYPE_EDIT_SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_3_EDIT").ToLower() == "true" ? 1 : 0;
            exportRequest.FORBUS4 = bussinessDetailEdit.ThenGetStringData("SOFTWARE_PRODUCT_OR_SERVICE_TYPE_EDIT_SOFTWARE_PRODUCT_OR_SERVICE_TYPE_OPT_4_EDIT").ToLower() == "true" ? 1 : 0;

            exportRequest.SIZEBUSINESS = bussinessDetailEdit.ThenGetIntData("DROPDOWN_STORE_SIZE_EDIT");

            exportRequest.NUMMODULE = bussinessDetailEdit.ThenGetStringData("SOFTWARE_TOTAL_MODULE_EDIT");
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

            return JsonConvert.SerializeObject(exportRequest, Formatting.Indented);
        }

        public override bool InvokeSingleForm(Guid trid, string currentSectionGroup, ref SingleFormRequestViewModel model)
        {
            var result = false;
            var darftData = SingleFormRequestEntity.Get(model.IdentityID);
            if (darftData.SectionData.Any(e => e.SectionName == "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EDIT"))
            {
                var mudulenbr = GetSectionData(model, "SOFTWARE_HOUSE_BUSINESS_SOFTWARE_DETAIL_MODULE_EDIT", SectionType.Form);
                var muduleinfo = GetSectionData(model, "SOFTWARE_HOUSE_BUSINESS_DETAIL_EDIT", SectionType.Form);
                muduleinfo.FormData = new Dictionary<string, object>
                {
                    { "SOFTWARE_TOTAL_MODULE_EDIT", mudulenbr.ArrayData.Count},                  
                };

            }

            result = true;

            return result;
        }
    }
   
}

