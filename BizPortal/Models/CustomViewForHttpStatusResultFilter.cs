using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Models
{
    public class CustomViewForHttpStatusResultFilter : IResultFilter, IExceptionFilter
    {
        private string _viewName;
        private int _statusCode;

        public CustomViewForHttpStatusResultFilter(HttpStatusCodeResult prototype, string viewName)
            : this(prototype.StatusCode, viewName)
        {
        }

        public CustomViewForHttpStatusResultFilter(int statusCode, string viewName)
        {
            _viewName = viewName;
            _statusCode = statusCode;
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            HttpStatusCodeResult httpStatusCodeResult = filterContext.Result as HttpStatusCodeResult;

            if (httpStatusCodeResult != null && httpStatusCodeResult.StatusCode == _statusCode)
            {
                ExecuteCustomViewResult(filterContext.Controller.ControllerContext);

            }
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public void OnException(ExceptionContext filterContext)
        {
            HttpException httpException = filterContext.Exception as HttpException;

            if (httpException != null && httpException.GetHttpCode() == _statusCode)
            {
                ExecuteCustomViewResult(filterContext.Controller.ControllerContext);
                // This causes ELMAH not to log exceptions, so commented out
                //filterContext.ExceptionHandled = true;
            }
        }

        void ExecuteCustomViewResult(ControllerContext controllerContext)
        {
            ViewResult viewResult = new ViewResult();
            viewResult.ViewName = _viewName;
            viewResult.ViewData = controllerContext.Controller.ViewData;
            viewResult.TempData = controllerContext.Controller.TempData;
            viewResult.ExecuteResult(controllerContext);
            controllerContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}