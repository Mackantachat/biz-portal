using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace BizPortal.Utils.Annotations
{

    public class FilterIPAddressAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private string _ipAddress { get; set; }

        public FilterIPAddressAttribute()
        {

        }

        public FilterIPAddressAttribute(string ipAddress)
        {
            this._ipAddress = ipAddress;
        }
     
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string ipAddress = HttpContext.Current.Request.UserHostAddress;

            if (!CheckIp(ipAddress.Trim()))
            {
                var responseMessage = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Content = new StringContent("{\"Message\":\"Unauthorized\", \"IPAddress\":\"" + ipAddress + "\"}")
                };
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                actionContext.Response = responseMessage;
            }

            base.OnActionExecuting(actionContext);
        }

        private bool CheckIp(string IpAddress)
        {
            if (string.IsNullOrEmpty(_ipAddress) || _ipAddress == "*")
            {
                return true;
            }
            else
            {
                var allowedIps = _ipAddress.Split(',').ToList();
                return allowedIps.Contains(IpAddress);
            }
        }
    }
}
