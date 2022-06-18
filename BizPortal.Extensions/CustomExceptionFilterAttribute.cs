using BizPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Configuration;
using System.Web.Http.Filters;
using System.Net.Http;

namespace BizPortal.Extensions
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private void CustomHandleError(HttpActionExecutedContext actionExecutedContext)
        {
            ResponseData<Exception> resp = new ResponseData<Exception>();
            resp.Type = ResultDataType.Error;
            resp.Message = actionExecutedContext.Exception.Message;

            if (actionExecutedContext.Exception.GetType() == typeof(WarningException))
                resp.Type = ResultDataType.Warning;

            var configuration = WebConfigurationManager.OpenWebConfiguration("~/Web.config");
            CustomErrorsSection customErrors = (CustomErrorsSection)configuration.GetSection("system.web/customErrors");
            if (customErrors.Mode == CustomErrorsMode.Off)
            {
                resp.Data = actionExecutedContext.Exception;

                if (actionExecutedContext.Exception.GetType() == typeof(CustomValidationException))
                {
                    CustomValidationException exception = (CustomValidationException)actionExecutedContext.Exception;

                    Dictionary<string, object> errors = new Dictionary<string, object>();
                    foreach (var item in exception.ValidationErrors)
                    {
                        errors.Add(item.Key, item.Value);
                    }
                    resp.ValidationErrors = errors;
                }
                //else if (actionExecutedContext.Exception.GetType() == typeof(DbEntityValidationException))
                //{
                //    DbEntityValidationException exception = (DbEntityValidationException)actionExecutedContext.Exception;
                //    var errors = exception.EntityValidationErrors.SelectMany(o => o.ValidationErrors).ToList();
                //    resp.SetFormValidationErrors(errors);
                //}
            }

            if (actionExecutedContext.Exception.GetType().IsSubclassOf(typeof(BizPortalException)) || actionExecutedContext.Exception.GetType() == typeof(BizPortalException))
            {
                BizPortalException ex = (BizPortalException)actionExecutedContext.Exception;
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(ex.HttpStatusCode, resp);
            }
            else
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest, resp);
            }
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            CustomHandleError(actionExecutedContext);
        }
    }
}
