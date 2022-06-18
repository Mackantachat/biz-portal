using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using EGA.EGA_Development.Util.MailV2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace BizPortal.Areas.WebApi.Controllers
{
    public class ApplicationStatusController : ApiControllerBase
    {
        private JuristicApplicationStatusRequestSubmitViewModel CreateRequest(ApplicationStatusApiModel model)
        {
            JuristicApplicationStatusRequestSubmitViewModel request = new JuristicApplicationStatusRequestSubmitViewModel()
            {
                JuristicID = model.JuristicID,
                ApplicationID = model.ServiceID,
                FileIDs = new List<Utils.FileMetadata>(),
                Remark = model.Remark,
                DisableSendingEmail = model.DisabledSendingSystemEmail,
                CustomTransactionCode = model.TransactionCode,
                CustomTransactionDate = model.TransactionDate
            };

            //if (model.TransactionDate != null)
            //{
            //DateTime transDate = new UnixTimestamp(model.TransactionDate.Value);
            //request.CustomTransactionDate = transDate.ToLocalTime();
            //}

            if (model.StatusID != null && model.StatusID >= 1 && model.StatusID <= 6)
            {
                request.CustomAppStatusID = model.StatusID.Value;
            }

            if (model.Files != null && model.Files.Length > 0)
            {
                foreach (var file in model.Files)
                {
                    Utils.FileMetadata fmeta = new Utils.FileMetadata()
                    {
                        FileID = file.FileID,
                        FileName = file.FileName,
                        FileDescription = file.FileDescription,
                        Extras = file.Extras
                    };
                    request.FileIDs.Add(fmeta);
                }
            }

            return request;
        }

        [Route("Api/ApplicationStatus")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] ApplicationStatusApiModel model)
        {
            ResponseData<object> result = null;

            if (!ModelState.IsValid)
            {
                result = new ResponseData<object>()
                {
                    Type = ResultDataType.Error,
                    Message = "One or more entities are invalid."
                };

                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    var errors = state.Errors.Select(o => o.ErrorMessage).ToArray();

                    if (errors.Length > 1)
                        result.ValidationErrors.Add(key, errors);
                    else if (errors.Length == 1)
                        result.ValidationErrors.Add(key, errors[0]);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (model.SmsMessage != null)
            {
                if (model.SmsMessage.MobileNumbers == null)
                {
                    result = new ResponseData<object>()
                    {
                        Type = ResultDataType.Error,
                        Message = "MobileNumbers is required.",
                    };
                }
                else
                {
                    MobileNumberInfo[] invalidNumbers = model.SmsMessage.GetInvalidNumbers();
                    if (invalidNumbers.Length > 0)
                    {
                        result = new ResponseData<object>()
                        {
                            Type = ResultDataType.Error,
                            Message = "Invalid mobile numbers.",
                            Data = invalidNumbers
                        };
                    }
                }

                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
            }

            var request = CreateRequest(model);
            result = request.Save();
            if (result.Type == ResultDataType.Success && model.EmailMessage != null)
            {
                var user = DB.Users.Where(w => w.JuristicID == request.JuristicID).FirstOrDefault();
                if (model.EmailMessage != null && user != null && !string.IsNullOrEmpty(user.Email))
                {
                    var msg = model.EmailMessage;
                    bool sent = EmailHelper.SendEmailV2(
                         new MailAddress[] { new MailAddress { Name = user.FullName, Address = user.Email } },
                         msg.Subject, msg.Body, msg.IsHtmlBody,
                         null, null, msg.Attachments);

                    if (!sent)
                    {
                        result.Type = ResultDataType.SuccessWithErrors;
                        result.ValidationErrors.Add("EmailResult", "Sending email failed.");
                    }
                }

                if (model.SmsMessage != null)
                {
                    SmsMessageSendingStatus smsResult = model.SmsMessage.SendSms();
                    if (smsResult.Status != SendingStatus.Success)
                    {
                        result.Type = ResultDataType.SuccessWithErrors;
                        result.ValidationErrors.Add("SmsResult", smsResult);
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("Api/ApplicationStatus")]
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] ApplicationStatusApiModel model)
        {
            ResponseData<object> result = null;

            if (!ModelState.IsValid)
            {
                result = new ResponseData<object>()
                {
                    Type = ResultDataType.Error,
                    Message = "One or more entities are invalid."
                };

                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    var errors = state.Errors.Select(o => o.ErrorMessage).ToArray();

                    if (errors.Length > 1)
                        result.ValidationErrors.Add(key, errors);
                    else if (errors.Length == 1)
                        result.ValidationErrors.Add(key, errors[0]);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (model.SmsMessage != null)
            {
                if (model.SmsMessage.MobileNumbers == null)
                {
                    result = new ResponseData<object>()
                    {
                        Type = ResultDataType.Error,
                        Message = "MobileNumbers is required.",
                    };
                }
                else
                {
                    MobileNumberInfo[] invalidNumbers = model.SmsMessage.GetInvalidNumbers();
                    if (invalidNumbers.Length > 0)
                    {
                        result = new ResponseData<object>()
                        {
                            Type = ResultDataType.Error,
                            Message = "Invalid mobile numbers.",
                            Data = invalidNumbers
                        };
                    }
                }

                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
            }

            var request = CreateRequest(model);

            if (id == 0)
            {
                int pendingID = (int)ApplicationStatusEnum.PENDING;
                var query = DB.JuristicApplicationStatusRequests
                        .Where(o => o.ApplicationID == model.ServiceID && o.JuristicID == model.JuristicID && o.ApplicationStatusID == pendingID)
                        .Select(o => o.JuristicApplicationStatusRequestID);
                if (query.Any())
                {
                    request.JuristicApplicationStatusRequestID = query.OrderByDescending(o => o).FirstOrDefault();
                }
            }
            else if (id > 0)
            {
                request.JuristicApplicationStatusRequestID = id;
            }

            result = request.Save();
            if (result.Type == ResultDataType.Success && model.EmailMessage != null)
            {
                var user = DB.Users.Where(w => w.JuristicID == request.JuristicID).FirstOrDefault();
                if (model.EmailMessage != null && user != null && !string.IsNullOrEmpty(user.Email))
                {
                    var msg = model.EmailMessage;
                    bool sent = EmailHelper.SendEmailV2(
                         new MailAddress[] { new MailAddress { Name = user.FullName, Address = user.Email } },
                         msg.Subject, msg.Body, msg.IsHtmlBody,
                         null, null, msg.Attachments);

                    if (!sent)
                    {
                        result.Type = ResultDataType.SuccessWithErrors;
                        result.ValidationErrors.Add("EmailResult", "Sending email failed.");
                    }
                }

                if (model.SmsMessage != null)
                {
                    SmsMessageSendingStatus smsResult = model.SmsMessage.SendSms();
                    if (smsResult.Status != SendingStatus.Success)
                    {
                        result.Type = ResultDataType.SuccessWithErrors;
                        result.ValidationErrors.Add("SmsResult", smsResult);
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
