using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BizPortal.Extensions
{
    public class BizPortalException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public BizPortalException()
            : this(HttpStatusCode.BadRequest)
        {

        }

        public BizPortalException(HttpStatusCode httpStatusCode)
            : this(string.Empty, httpStatusCode)
        {
        }

        public BizPortalException(string message)
            : this(message, HttpStatusCode.BadRequest)
        {
        }

        public BizPortalException(string message, HttpStatusCode httpStatusCode)
            : base(message)
        {
            HttpStatusCode = HttpStatusCode.BadRequest;

            if (!string.IsNullOrEmpty(message))
            {
                Data.Add("ErrorMessage", message);
            }

            var context = HttpContext.Current;
            if (context != null && context.Request.Headers["Consumer-Key"] != null)
            {
                Data.Add("Consumer-Key", context.Request.Headers["Consumer-Key"]);
            }
        }
    }
}
