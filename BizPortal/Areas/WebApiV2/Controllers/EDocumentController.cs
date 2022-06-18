using BizPortal.AppsHook;
using BizPortal.Areas.WebApi.Controllers;
using BizPortal.DAL.MongoDB;
using BizPortal.Models;
using BizPortal.Utils;
using BizPortal.Utils.Annotations;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using BizPortal.ViewModels.Apps.EDocument;
using EGA.EGA_Development.Util;
using EGA.EGA_Development.Util.MailV2.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using static BizPortal.Utils.Helpers.EmailHelper;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    [AuthorizeUser(OpenIDUserType = "Citizen")]
    public class EDocumentController : ApiControllerBase
    {
        private readonly string consumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
        private readonly string consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];

        /// <summary>
        /// API Document
        /// https://kb.dga.or.th/s/bnfjpvp5f5rcisvhh92g/electronic-document/d/bqf5nh15f5rclh859cig/api-digital-signing
        /// </summary>
        /// <param name="IdentityID">เลขบัตรประชาชนของผู้ที่ต้องการจะทำ Personal Signing</param>
        /// <param name="DocumentID">ID ของ Document ที่ต้องการจะทำ Digital Signature</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/v2/eDocument/Personal/QRCode")]
        public PersonalQrCodeResponse GetQRCode(PersonalQrCodeRequest request)
        {
            PersonalQrCodeResponse response = new PersonalQrCodeResponse();
            var personalToken = string.Empty;

            try
            {
                RestClient client = new RestClient("https://api.egov.go.th");
                client.AddDefaultHeader("Consumer-Key", consumerKey);

                RestRequest personalTokenRequest = new RestRequest("/ws/auth/validate", Method.GET);
                personalTokenRequest.AddHeader("Content-Type", "application/json");
                personalTokenRequest.AddQueryParameter("ConsumerSecret", consumerSecret);
                personalTokenRequest.AddQueryParameter("AgentID", IdentityID);

                IRestResponse personalTokenResponse = client.Execute(personalTokenRequest);
                if (personalTokenResponse != null)
                {
                    if (personalTokenResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var tokenObj = JObject.Parse(personalTokenResponse.Content);
                        personalToken = tokenObj["Result"].ToString();
                    }
                    else
                    {
                        throw new Exception($"[TokenNotFound] {personalTokenResponse.StatusCode}: {personalTokenResponse.ErrorMessage}");
                    }
                }
                else
                {
                    throw new Exception($"[TokenValidateError] {personalTokenResponse.StatusCode}: {personalTokenResponse.ErrorMessage}");
                }


                RestRequest signTokenRequest = new RestRequest("/api/edoc/signature/egov/v1/mobile/requested", Method.POST);
                signTokenRequest.AddHeader("Accept", "*/*");
                signTokenRequest.AddHeader("Content-Type", "application/json");
                signTokenRequest.AddHeader("Token", personalToken);

                PersonalSignTokenRequest signTokenRequestBody = new PersonalSignTokenRequest
                {
                    DocumentID = request.DocumentID
                };

                var entity = EDocumentEntity.Get(request.DocumentID);
                if (entity != null && entity.PersonalSigners != null && entity.PersonalSigners.Count > 0)
                {
                    var me = entity.PersonalSigners.Where(o => o.IdentityID == IdentityID).FirstOrDefault();
                    if (me == null)
                    {
                        throw new Exception($"[EntityError] Entity is null");
                    }

                    var signatureImage = me.SignatureBase64.ToString();
                    if (!string.IsNullOrEmpty(signatureImage))
                    {
                        var signatureHeight = me.SignatureHeight.ToString();
                        if (string.IsNullOrEmpty(signatureHeight))
                        {
                            throw new Exception($"[SignatureError] Height is null");
                        }

                        var signatureWidth = me.SignatureWidth.ToString();
                        if (string.IsNullOrEmpty(signatureWidth))
                        {
                            throw new Exception($"[SignatureError] Width is null");
                        }

                        var signatureLeft = me.SignatureLeft.ToString();
                        if (string.IsNullOrEmpty(signatureLeft))
                        {
                            throw new Exception($"[SignatureError] Left is null");
                        }

                        var signatureBottom = me.SignatureBottom.ToString();
                        if (string.IsNullOrEmpty(signatureBottom))
                        {
                            throw new Exception($"[SignatureError] Bottom is null");
                        }

                        signatureImage = signatureImage.Substring(signatureImage.IndexOf(',') + 1);

                        signTokenRequestBody.Signature = new PersonalSignature()
                        {
                            Image = signatureImage,
                            Height = signatureHeight,
                            Width = signatureWidth,
                            Left = signatureLeft,
                            Bottom = signatureBottom
                        };
                    }
                }

                signTokenRequest.AddJsonBody(signTokenRequestBody);

                IRestResponse signTokenResponse = client.Execute(signTokenRequest);
                if (signTokenResponse != null)
                {
                    if (signTokenResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var signTokenHeader = signTokenResponse.Headers.Where(o => o.Name == "X-Signed-Token").FirstOrDefault();
                        if (signTokenHeader != null)
                        {
                            response.Token = signTokenHeader.Value.ToString();
                        }
                        else
                        {
                            throw new Exception($"[SignTokenNotFound] {personalTokenResponse.StatusCode}: {personalTokenResponse.ErrorMessage}");
                        }
                    }
                    else
                    {
                        throw new Exception($"[SignTokenError] {personalTokenResponse.StatusCode}: {personalTokenResponse.ErrorMessage}");
                    }
                }

                response.Status = true;
                response.Action = "https://portal.apps.go.th/edoc/signature/signed";
                response.CallbackUrl = Url.Content("~/th/apps/signing/signeddocument");
                response.Nonce = request.DocumentID;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message.ToString();
                return response;
            }

            return response;
        }

        [HttpPost]
        [Route("Api/v2/eDocument/Personal/List")]
        public DataTablesResult<PersonalSignedDocumentViewModel> PersonalList(PersonalSigningDataTable dataTables)
        {
            var result = new DataTablesResult<PersonalSignedDocumentViewModel>();
            result.Draw = dataTables.Draw;

            var searchKeyword = string.Empty;
            if (!string.IsNullOrEmpty(dataTables.SearchKeyword))
                searchKeyword = dataTables.SearchKeyword.ToLower();

            int column = 0;
            string dir = "asc";
            if (dataTables.Order != null && dataTables.Order.Length > 0)
            {
                column = dataTables.Order.First().Column;
                dir = dataTables.Order.First().Dir;
            }

            var resultList = new List<PersonalSignedDocument>();
            var activeList = new List<EDocumentEntity>();
            var list = EDocumentEntity.List(IdentityID);
            if (list != null && list.Count > 0)
                activeList = list.FindAll(o => o.SigningType == EDocumentType.Personal && o.SigningStatus != EDocumentStatus.DELETED);

            result.RecordsTotal = activeList.Count;
            foreach (var item in activeList)
            {
                var req = ApplicationRequestEntity.Get(item.ApplicationRequestID);
                if (req != null)
                {
                    var appName = req.PermitName;
                    var identityName = req.IdentityName;

                    var me = item.PersonalSigners.Find(o => o.IdentityID == IdentityID);
                    if ((me.SigningOrder == 1 && (me.PersonalSigningStatus == PersonalSigningStatus.NEW || me.PersonalSigningStatus == PersonalSigningStatus.FAILED)) ||
                        (me.SigningOrder > 1 && (me.PersonalSigningStatus == PersonalSigningStatus.NEW || me.PersonalSigningStatus == PersonalSigningStatus.FAILED) &&
                        item.PersonalSigners[me.SigningOrder - 2].PersonalSigningStatus == PersonalSigningStatus.SIGNED))
                    {
                        resultList.Add(new PersonalSignedDocument
                        {
                            ApplicationName = appName,
                            IdentityName = identityName,
                            DocumentID = item.DocumentID,
                            Status = PersonalSigningStatus.NEW.ToString(),
                            StatusText = "รอลงนาม",
                            CreatedDate = item.CreatedDate,
                            SignedDate = null,
                            ApplicationRequestID = item.ApplicationRequestID
                        });
                    }
                    else if (me.PersonalSigningStatus == PersonalSigningStatus.SIGNED)
                    {
                        resultList.Add(new PersonalSignedDocument
                        {
                            ApplicationName = appName,
                            IdentityName = identityName,
                            DocumentID = item.DocumentID,
                            Status = PersonalSigningStatus.SIGNED.ToString(),
                            StatusText = "ลงนามสำเร็จ",
                            CreatedDate = item.CreatedDate,
                            SignedDate = me.SignedDate != null ? me.SignedDate : null,
                            ApplicationRequestID = item.ApplicationRequestID
                        });
                    }
                    else if (me.PersonalSigningStatus == PersonalSigningStatus.REJECTED)
                    {
                        resultList.Add(new PersonalSignedDocument
                        {
                            ApplicationName = appName,
                            IdentityName = identityName,
                            DocumentID = item.DocumentID,
                            Status = PersonalSigningStatus.REJECTED.ToString(),
                            StatusText = "ปฏิเสธลงนาม",
                            CreatedDate = item.CreatedDate,
                            SignedDate = me.SignedDate != null ? me.SignedDate : null,
                            ApplicationRequestID = item.ApplicationRequestID,
                            Remark = me.PersonalSigningRemark
                        });
                    }
                }
            }

            var queryList = resultList.Where(o => o.ApplicationName.ToLower().Contains(searchKeyword) || o.DocumentID.ToLower().Contains(searchKeyword)).AsQueryable();
            result.RecordsFiltered = queryList.Count();

            switch (column)
            {
                case 1:
                    if (dir == "asc")
                        queryList = queryList.OrderBy(o => o.ApplicationName);
                    else
                        queryList = queryList.OrderByDescending(o => o.ApplicationName);
                    break;
                case 2:
                    if (dir == "asc")
                        queryList = queryList.OrderBy(o => o.IdentityName);
                    else
                        queryList = queryList.OrderByDescending(o => o.IdentityName);
                    break;
                case 3:
                    if (dir == "asc")
                        queryList = queryList.OrderBy(o => o.Status);
                    else
                        queryList = queryList.OrderByDescending(o => o.Status);
                    break;
                case 4:
                    if (dir == "asc")
                        queryList = queryList.OrderBy(o => o.CreatedDate);
                    else
                        queryList = queryList.OrderByDescending(o => o.CreatedDate);
                    break;
                case 5:
                    if (dir == "asc")
                        queryList = queryList.OrderBy(o => o.SignedDate);
                    else
                        queryList = queryList.OrderByDescending(o => o.SignedDate);
                    break;
                case 0:
                default:
                    queryList = queryList.OrderBy(o => o.CreatedDate);
                    break;
            }

            result.Data = queryList.Skip(dataTables.Start)
                .Take(dataTables.Length)
                .Select(o => new PersonalSignedDocumentViewModel
                {
                    ApplicationName = o.ApplicationName,
                    IdentityName = o.IdentityName,
                    DocumentID = o.DocumentID,
                    Status = o.Status,
                    StatusText = o.StatusText,
                    CreatedDate = o.CreatedDate.ToString("dd MMM yyyy"),
                    SignedDate = o.SignedDate != null ? o.SignedDate.ToString("dd MMM yyyy") : "-",
                    ApplicationRequestID = o.ApplicationRequestID,
                    Remark = o.Remark
                })
                .ToList();
            return result;
        }

        [HttpPost]
        [Route("Api/v2/eDocument/OrgByPerson/Approve")]
        public ResponseData<bool> Approve(OrgByPersonApproveRequest model)
        {
            var errorObj = 0;
            var errorList = new List<string>();
            ResponseData<bool> response = new ResponseData<bool>
            {
                Data = false
            };

            try
            {
                var activeList = new List<EDocumentEntity>();
                var list = EDocumentEntity.List(IdentityID);
                if (list == null || list.Count < 1)
                {
                    response.Type = ResultDataType.Error;
                    response.Message = "List not found.";
                    return response;
                }

                var selectedList = list.FindAll(o => model.DocumentIDs.Contains(o.DocumentID) && o.SigningType == EDocumentType.OrgByPerson && o.SigningStatus != EDocumentStatus.DELETED);
                var dtNow = DateTime.Now;
                foreach (var item in selectedList)
                {
                    var me = item.PersonalSigners.Find(o => o.IdentityID == IdentityID);
                    if (me.PersonalSigningStatus == PersonalSigningStatus.SIGNED || me.PersonalSigningStatus == PersonalSigningStatus.REJECTED)
                    {
                        //เอกสารนี้ได้ทำการ signed หรือ rejected ไปแล้ว ไม่สามารถทำซ้ำได้
                        errorObj++;
                        errorList.Add(item.DocumentID);
                    }
                    else
                    {
                        var request = ApplicationRequestEntity.Get(item.ApplicationRequestID);
                        var app = DB.Applications.Where(w => w.ApplicationID == request.ApplicationID).SingleOrDefault();

                        if (model.Status == "APPROVE")
                        {
                           
                            if (!string.IsNullOrEmpty(app.AppsHookClassName))
                            {
                                var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", app.AppsHookClassName).Unwrap();
                                var ectrl = new EDocument(request.ApplicationID);
                                var eLicenseData = hook.GenerateELicenseData(request.ApplicationRequestID);
                                var signers = item.PersonalSigners
                                                  .FindAll(o => o.IdentityID == IdentityID || o.PersonalSigningStatus == PersonalSigningStatus.SIGNED)
                                                  .Select(o => new SigningPerson
                                                  {
                                                      FirstName = o.IdentityFirstName,
                                                      LastName = o.IdentityLastName,
                                                      Position = o.IdentityPosition,
                                                      SignatureBase64 = o.SignatureBase64
                                                  })
                                                  .ToList();

                                var documentId = ectrl.RenderDocument(item.TemplateID, eLicenseData, signers, true);

                                item.DocumentID = documentId;

                                me.PersonalSigningStatus = PersonalSigningStatus.SIGNED;
                                me.SignedDate = dtNow;
                                // TODO: signed สำเร็จแล้วส่ง email

                            }
                            else
                            {
                                throw new Exception("ไม่สามารถลงนามเอกสารได้");
                            }
                        }
                        else
                        {
                            me.PersonalSigningStatus = PersonalSigningStatus.REJECTED;
                            me.SignedDate = dtNow;
                            me.PersonalSigningRemark = model.Remark;
                            // TODO: rejected สำเร็จแล้วส่ง email
                        }
                        // ส่งอีเมลหน้าเจ้าหน้าที่ กรณีลงนามแล้ว
                        EmailHelper.SendNoticeSigningEmailToAgent(getAgentEmails(request), new StatusUpdateEmailToAgentModel
                        {
                            OrgName = request.OrgNameTH,
                            PermitName = request.PermitName,
                            RequestNumber = request.ApplicationRequestNumber,
                            Status = me.PersonalSigningStatus == PersonalSigningStatus.SIGNED ? "ลงนาม": "ปฏิเสธ",
                            StatusOther = me.PersonalSigningStatus == PersonalSigningStatus.SIGNED ? "-" :  model.Remark,
                            ViewUrl = Url.ServiceAction("Detail", "BackOffice/ApplicationStatus", "", request.ApplicationRequestID.ToString())
                        });
                    }

                    if (item.PersonalSigners.All(o => o.PersonalSigningStatus == PersonalSigningStatus.NEW))
                    {
                        item.SigningStatus = EDocumentStatus.NEW;
                    }
                    else if (item.PersonalSigners.All(o => o.PersonalSigningStatus == PersonalSigningStatus.SIGNED))
                    {
                        item.SigningStatus = EDocumentStatus.COMPLETED;
                    }
                    else if (item.PersonalSigners.Any(o => o.PersonalSigningStatus == PersonalSigningStatus.REJECTED))
                    {
                        item.SigningStatus = EDocumentStatus.REJECTED;
                    }
                    else if (item.PersonalSigners.Any(o => o.PersonalSigningStatus == PersonalSigningStatus.SIGNED))
                    {
                        item.SigningStatus = EDocumentStatus.PENDING;
                    }

                    item.UpdatedDate = dtNow;
                    item.Update();
                }

                if (errorObj == 0)
                {
                    response.Type = ResultDataType.Success;
                    response.Message = model.Status;
                }
                else
                {
                    response.Type = ResultDataType.SuccessWithErrors;
                    var msg = "มีเอกสารที่ไม่สามารถลงนามได้ เนื่องจากได้ทำการลงนาม/ปฏิเสธแล้ว [{0}]";
                    var msgList = string.Empty;
                    for (int i = 0; i < errorList.Count; i++)
                    {
                        msgList += errorList[i];
                        if (i != errorList.Count - 1)
                            msgList += ", ";
                    }
                    response.Message = string.Format(msg, msgList);
                }
                
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Type = ResultDataType.Error;
                return response;
            }
        }

        private List<MailAddress> getAgentEmails(ApplicationRequestEntity request)
        {
            var sendEmails = new List<MailAddress>();
            var emailRegex = new Regex(RegExPattern.EMAIL, RegexOptions.IgnoreCase);
            var agentEmails = new List<MailAddress>();
            agentEmails = DB.MemberServices.Include(e => e.ApplicationUser)
                                     .Include(e => e.MemberServiceAreas)
                                     .Where(e => (e.ApplicationID == request.ApplicationID && e.MemberServiceAreas.Count < 1) ||
                                        (e.ApplicationID == request.ApplicationID && e.MemberServiceAreas.Any(s => (s.ProvinceID == -1 || ((s.ProvinceID == request.ProvinceID) && (s.DistrictID == -1 || ((s.DistrictID == request.AmphurID) && (s.SectionID == -1 || (s.SectionID == request.TumbolID)))))) && !s.IsDeleted)
                                        && !e.IsDeleted))
                                     .Select(e => new MailAddress { Name = e.ApplicationUser.Email ?? "", Address = e.ApplicationUser.Email ?? "" })
                                     .ToList();

            //Get OrgAgent email who are the officer of department and not in MemberServices/Areas
            var orgAgents = (from tbApp in DB.Applications
                             join tbUser in DB.Users on tbApp.OrgCode equals tbUser.OrgCode
                             where tbApp.ApplicationID == request.ApplicationID && !(from tbMemberservices in DB.MemberServices where tbMemberservices.IsDeleted == false select tbMemberservices.UserID).Contains(tbUser.Id)
                             select tbUser.Email).ToList();

            foreach (var email in orgAgents)
            {
                agentEmails.Add(new MailAddress { Name = email ?? "", Address = email ?? "" });
            }

            //join tbMemberservices in DB.MemberServices  on tbUser.Id equals tbMemberservices.UserID
            //where tbApp.ApplicationID == applicationID

            foreach (var email in agentEmails)
            {
                if (emailRegex.IsMatch(email.Address))
                {
                    sendEmails.Add(email);
                }
            }

            return sendEmails;
        }

        [HttpPost]
        [Route("Api/v2/eDocument/OrgByPerson/List")]
        public DataTablesResult<PersonalSignedDocumentViewModel> OrgByPersonList(PersonalSigningDataTable dataTables)
        {
            var result = new DataTablesResult<PersonalSignedDocumentViewModel>();
            result.Draw = dataTables.Draw;

            var searchKeyword = string.Empty;
            if (!string.IsNullOrEmpty(dataTables.SearchKeyword))
                searchKeyword = dataTables.SearchKeyword.ToLower();

            int column = 0;
            string dir = "asc";
            if (dataTables.Order != null && dataTables.Order.Length > 0)
            {
                column = dataTables.Order.First().Column;
                dir = dataTables.Order.First().Dir;
            }

            var resultList = new List<PersonalSignedDocument>();
            var activeList = new List<EDocumentEntity>();
            var list = EDocumentEntity.List(IdentityID);
            if (list != null && list.Count > 0)
                activeList = list.FindAll(o => o.SigningType == EDocumentType.OrgByPerson && o.SigningStatus != EDocumentStatus.DELETED);

            result.RecordsTotal = activeList.Count;
            foreach (var item in activeList)
            {
                var req = ApplicationRequestEntity.Get(item.ApplicationRequestID);
                if (req != null)
                {
                    var appName = req.PermitName;
                    var identityName = req.IdentityName;

                    var me = item.PersonalSigners.Find(o => o.IdentityID == IdentityID);
                    if ((me.SigningOrder == 1 && (me.PersonalSigningStatus == PersonalSigningStatus.NEW || me.PersonalSigningStatus == PersonalSigningStatus.FAILED)) ||
                        (me.SigningOrder > 1 && (me.PersonalSigningStatus == PersonalSigningStatus.NEW || me.PersonalSigningStatus == PersonalSigningStatus.FAILED) &&
                        item.PersonalSigners[me.SigningOrder - 2].PersonalSigningStatus == PersonalSigningStatus.SIGNED))
                    {
                        resultList.Add(new PersonalSignedDocument
                        {
                            ApplicationName = appName,
                            IdentityName = identityName,
                            DocumentID = item.DocumentID,
                            Status = PersonalSigningStatus.NEW.ToString(),
                            StatusText = "รอลงนาม",
                            CreatedDate = item.CreatedDate,
                            SignedDate = null,
                            ApplicationRequestID = item.ApplicationRequestID
                        });
                    }
                    else if (me.PersonalSigningStatus == PersonalSigningStatus.SIGNED)
                    {
                        resultList.Add(new PersonalSignedDocument
                        {
                            ApplicationName = appName,
                            IdentityName = identityName,
                            DocumentID = item.DocumentID,
                            Status = PersonalSigningStatus.SIGNED.ToString(),
                            StatusText = "ลงนามสำเร็จ",
                            CreatedDate = item.CreatedDate,
                            SignedDate = me.SignedDate != null ? me.SignedDate : null,
                            ApplicationRequestID = item.ApplicationRequestID
                        });
                    }
                    else if (me.PersonalSigningStatus == PersonalSigningStatus.REJECTED)
                    {
                        resultList.Add(new PersonalSignedDocument
                        {
                            ApplicationName = appName,
                            IdentityName = identityName,
                            DocumentID = item.DocumentID,
                            Status = PersonalSigningStatus.REJECTED.ToString(),
                            StatusText = "ปฏิเสธลงนาม",
                            CreatedDate = item.CreatedDate,
                            SignedDate = me.SignedDate != null ? me.SignedDate : null,
                            ApplicationRequestID = item.ApplicationRequestID,
                            Remark = me.PersonalSigningRemark
                        });
                    }
                }
            }

            var queryList = resultList.Where(o => o.ApplicationName.ToLower().Contains(searchKeyword) || o.DocumentID.ToLower().Contains(searchKeyword)).AsQueryable();
            result.RecordsFiltered = queryList.Count();

            switch (column)
            {
                case 1:
                    if (dir == "asc")
                        queryList = queryList.OrderBy(o => o.ApplicationName);
                    else
                        queryList = queryList.OrderByDescending(o => o.ApplicationName);
                    break;
                case 2:
                    if (dir == "asc")
                        queryList = queryList.OrderBy(o => o.IdentityName);
                    else
                        queryList = queryList.OrderByDescending(o => o.IdentityName);
                    break;
                case 3:
                    if (dir == "asc")
                        queryList = queryList.OrderBy(o => o.Status);
                    else
                        queryList = queryList.OrderByDescending(o => o.Status);
                    break;
                case 4:
                    if (dir == "asc")
                        queryList = queryList.OrderBy(o => o.CreatedDate);
                    else
                        queryList = queryList.OrderByDescending(o => o.CreatedDate);
                    break;
                case 5:
                    if (dir == "asc")
                        queryList = queryList.OrderBy(o => o.SignedDate);
                    else
                        queryList = queryList.OrderByDescending(o => o.SignedDate);
                    break;
                case 0:
                default:
                    queryList = queryList.OrderBy(o => o.CreatedDate);
                    break;
            }

            result.Data = queryList.Skip(dataTables.Start)
                .Take(dataTables.Length)
                .Select(o => new PersonalSignedDocumentViewModel
                {
                    ApplicationName = o.ApplicationName,
                    IdentityName = o.IdentityName,
                    DocumentID = o.DocumentID,
                    Status = o.Status,
                    StatusText = o.StatusText,
                    CreatedDate = o.CreatedDate.ToString("dd MMM yyyy"),
                    SignedDate = o.SignedDate != null ? o.SignedDate.ToString("dd MMM yyyy") : "-",
                    ApplicationRequestID = o.ApplicationRequestID,
                    Remark = o.Remark
                })
                .ToList();
            return result;
        }
    }
}
