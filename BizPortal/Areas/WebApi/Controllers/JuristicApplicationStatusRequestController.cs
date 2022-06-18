using BizPortal.ViewModels;
using BizPortal.ViewModels.Select2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Newtonsoft.Json;
using BizPortal.Models;
using Mapster;
using Mapster.Adapters;
using BizPortal.Utils;
using Newtonsoft.Json.Serialization;
using System.Web;
using BizPortal.Utils.Helpers;
using BizPortal.Utils.Extensions;
using EGA.Owin.Security.Utils.Extensions;
using System.Configuration;
using EGA.Owin.Security.EGAOpenID;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace BizPortal.Areas.WebApi.Controllers
{
    public class JuristicApplicationStatusRequestController : ApiControllerBase
    {
        [HttpPost]
        [Route("api/JuristicApplicationStatusRequest/List")]
        public DataTablesResult<JuristicApplicationStatusRequestViewModel> List(JuristicApplicationStatusRequestDataTables dataTables)
        {
            DataTablesResult<JuristicApplicationStatusRequestViewModel> result = new DataTablesResult<JuristicApplicationStatusRequestViewModel>();

            var query = DB.JuristicRequestViews.Where(o => o.TwoLetterISOLanguageName == CurrentCulture && !o.IsDeleted).AsQueryable();
            result.Draw = dataTables.Draw;
            result.RecordsTotal = result.RecordsFiltered = query.Count(); // Set default number of records. TotalDisplayRecords must be set again after using filter
            if (!string.IsNullOrEmpty(dataTables.SearchKeyword))
            {
                dataTables.SearchKeyword = dataTables.SearchKeyword.ToLower();
                query = query.Where(o =>
                    o.ApplicationName.ToLower().Contains(dataTables.SearchKeyword) ||
                    o.FullName.ToLower().Contains(dataTables.SearchKeyword) ||
                    o.JuristicID.ToLower().Contains(dataTables.SearchKeyword)
                    );
            }

            if (dataTables.ApplicationID != null)
                query = query.Where(o => o.ApplicationID == dataTables.ApplicationID);

            if (string.IsNullOrEmpty(IdentityID))
            {
                if (dataTables.IdentityID != null)
                    query = query.Where(o => o.JuristicID == dataTables.IdentityID);

                if (!User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME))
                    query = query.Where(o => o.OrgCode == OrganizationID);
                else
                    query = query.Where(o => string.IsNullOrEmpty(dataTables.OrgCode) || o.OrgCode == dataTables.OrgCode);
            }
            else
            {
                query = query.Where(o => o.JuristicID == IdentityID);
            }

            int totalPending = query.Where(w => w.ApplicationStatusID == (int)ApplicationStatusEnum.PENDING).Count();

            if (!string.IsNullOrEmpty(dataTables.ApplicationStatusID))
            {
                int statusID = int.Parse(dataTables.ApplicationStatusID);
                query = query.Where(o => o.ApplicationStatusID == statusID);
            }

            result.RecordsFiltered = query.Count(); // Update number of TotalDisplayRecords.

            string currentLang = CurrentCulture;

            var selectedQuery = query.Select(o => new JuristicApplicationStatusRequestViewModel()
            {
                JuristicApplicationStatusRequestID = o.JuristicApplicationStatusRequestID,
                ApplicationID = o.ApplicationID,
                ApplicationName = o.ApplicationName,
                ApplicationStatusID = o.ApplicationStatusID,
                ApplicationStatusName = o.ApplicationStatusName,
                ApplicationStatusOther = o.ApplicationStatusOther,
                //ApplicationUrl = o.ApplicationUrl,
                OrganizationName = o.OrgName,
                OrgCode = o.OrgCode,
                CreatedDate = o.CreatedDate,
                UpdatedDate = o.UpdatedDate.HasValue ? o.UpdatedDate.Value : o.CreatedDate,
                CreatedUserName = o.FullName + " (" + o.JuristicID + ")",
                CreatedUserID = o.CreatedUserID,
                Remark = o.Remark,
            });

            result.Data = dataTables.GenerateSearchQuery<JuristicApplicationStatusRequestViewModel>(selectedQuery, "CreatedDate", "DESC").ToList();
            result.Summary = string.Format(Resources.ApplicationStatusRequests.MSG_PENDING_APPROVE, totalPending);


            //foreach (var data in result.Data)
            //{
            //    data.ApplicationUrl = data.ApplicationUrl.Replace("{language}", CurrentCulture);
            //    if (!string.IsNullOrEmpty(data.ApplicationUrl) && data.ApplicationUrl.StartsWith("~"))
            //        data.ApplicationUrl = Url.Content(data.ApplicationUrl);
            //}

            return result;
        }

        [HttpPost]
        [Route("api/JuristicApplicationStatusRequest/Document/List")]
        public DataTablesResult<JuristicApplicationDocumentRequestViewModel> DocumentListRequest(JuristicApplicationStatusDocumentRequestDataTables dataTables)
        {
            return DocumentList(dataTables, FileRequestTypeEnum.Request);
        }

        [HttpPost]
        [Route("api/JuristicApplicationStatusRequest/Document/List/Response")]
        public DataTablesResult<JuristicApplicationDocumentRequestViewModel> DocumentListResponse(JuristicApplicationStatusDocumentRequestDataTables dataTables)
        {
            return DocumentList(dataTables, FileRequestTypeEnum.Response);
        }

        public DataTablesResult<JuristicApplicationDocumentRequestViewModel> DocumentList(JuristicApplicationStatusDocumentRequestDataTables dataTables, FileRequestTypeEnum fileRequestType)
        {
            DataTablesResult<JuristicApplicationDocumentRequestViewModel> result = new DataTablesResult<JuristicApplicationDocumentRequestViewModel>();
            var application = DB.JuristicRequestViews.Where(o => o.JuristicApplicationStatusRequestID == dataTables.JuristicApplicationStatusRequestID && o.TwoLetterISOLanguageName == CurrentCulture && !o.IsDeleted).SingleOrDefault();
            if (application != null)
            {

                var data = BizPortal.Utils.EgaContentStore.GetAllOfRequest(application.ConsumerKey.ToString(), dataTables.JuristicApplicationStatusRequestID, FileStatus.InUse, fileRequestType);
                FileUploadResponseBeanViewModel fileUploadResponse = null;

                if (data != null)
                {
                    fileUploadResponse = JsonConvert.DeserializeObject<FileUploadResponseBeanViewModel>(data.ToString(), new JsonSerializerSettings
                     {
                         Error = HandleDeserializationError
                     });
                }

                if (fileUploadResponse != null && fileUploadResponse.List != null)
                {
                    JuristicRequestFileUploadType[] juristicRequestFileUploadTypes = getJuristicRequestFileUploadType(application.ApplicationSysName);
                    result.Data = fileUploadResponse.List.Select(s => new JuristicApplicationDocumentRequestViewModel
                    {
                        DocumentID = s.FileID,
                        DocumentName = s.Extras != null ? s.Extras.Name : "-",
                        DocumentType = s.Extras != null ? s.Extras.Type : "-",
                        DocumentPath = string.Format("{0}th/File/GetWS?ConsumerKey={1}&id={2}", GetBaseUrl(), application.ConsumerKey.ToString(), s.FileID)
                    }).ToList();

                    if (juristicRequestFileUploadTypes.Length > 0)
                    {
                        foreach (var item in result.Data)
                        {
                            var juristicRequestFileUploadType = juristicRequestFileUploadTypes.Where(w => w.Code == item.DocumentType).FirstOrDefault();
                            if (juristicRequestFileUploadType != null)
                                item.DocumentType = juristicRequestFileUploadType.Name;
                        }
                    }

                    result.RecordsTotal = fileUploadResponse.Total;
                    result.RecordsFiltered = fileUploadResponse.Size;
                }
                else
                    result.Data = new List<JuristicApplicationDocumentRequestViewModel>();
            }
            else
            {
                result.Data = new List<JuristicApplicationDocumentRequestViewModel>();
            }

            return result;
        }

        public string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/") appUrl += "/";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }

        [HttpPost]
        [Route("api/JuristicApplicationStatusRequest/Log")]
        public DataTablesResult<JuristicApplicationStatusRequestDetailViewModel> Log(JuristicApplicationStatusLogRequestDataTables dataTables)
        {
            DataTablesResult<JuristicApplicationStatusRequestDetailViewModel> result = new DataTablesResult<JuristicApplicationStatusRequestDetailViewModel>();

            var query = DB.JuristicRequestLogViews.Where(o => o.TwoLetterISOLanguageName == CurrentCulture && !o.IsDeleted).AsQueryable();
            result.Draw = dataTables.Draw;
            result.RecordsTotal = result.RecordsFiltered = query.Count(); // Set default number of records. TotalDisplayRecords must be set again after using filter


            query = query.Where(o => o.JuristicApplicationStatusRequestID == dataTables.JuristicApplicationStatusRequestID);


            result.RecordsFiltered = query.Count(); // Update number of TotalDisplayRecords.

            string currentLang = CurrentCulture;
            var selectedQuery = query.Select(o => new JuristicApplicationStatusRequestDetailViewModel()
            {
                Status = o.ApplicationStatusName,
                SubmitDate = o.CreatedDate,
                Remark = o.Remark ?? "",
                JuristicApplicationStatusRequestLogID = o.JuristicApplicationStatusRequestLogID
            });


            result.Data = dataTables.GenerateSearchQuery<JuristicApplicationStatusRequestDetailViewModel>(selectedQuery, "Ordering").ToList();
            return result;
        }

        [HttpPost]
        [Route("api/JuristicApplicationStatusRequest/save")]
        public ResponseData<object> Save([FromBody] JuristicApplicationStatusRequestSubmitViewModel model)
        {
            model.IsSubmit = false;
            return ChangeStatus(model);
        }

        [HttpPost]
        [Route("api/JuristicApplicationStatusRequest/submit")]
        public ResponseData<object> Submit([FromBody] JuristicApplicationStatusRequestSubmitViewModel model)
        {
            model.IsSubmit = true;
            return ChangeStatus(model);
        }

        private ResponseData<object> ChangeStatus(JuristicApplicationStatusRequestSubmitViewModel model)
        {
            return model.Save();
        }
    }
}
