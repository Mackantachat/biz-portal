using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace BizPortal.Utils.Helpers
{
    /// <summary>
    /// http://stackoverflow.com/questions/3545432/how-to-get-routedata-by-url
    /// </summary>
    public static class RouteHelper
    {
        //public static RouteData GetRouteDataByUrl(string url)
        //{
        //    return RouteTable.Routes.GetRouteData(new RewritedHttpContextBase(url));
        //}

        public static RouteData GetRouteDataByUrl(Uri url)
        {
            return RouteTable.Routes.GetRouteData(new HttpContextWrapper(new HttpContext(new HttpRequest(null, new UriBuilder(url.Scheme, url.Host, url.Port, url.AbsolutePath).ToString(), url.Query), new HttpResponse(new System.IO.StringWriter()))));
        }

        public class RewritedHttpContextBase : HttpContextBase
        {
            private readonly HttpRequestBase mockHttpRequestBase;

            public RewritedHttpContextBase(string appRelativeUrl)
            {
                this.mockHttpRequestBase = new MockHttpRequestBase(appRelativeUrl);
            }


            public override HttpRequestBase Request
            {
                get
                {
                    return mockHttpRequestBase;
                }
            }

            private class MockHttpRequestBase : HttpRequestBase
            {
                private readonly string appRelativeUrl;

                public MockHttpRequestBase(string appRelativeUrl)
                {
                    this.appRelativeUrl = appRelativeUrl;
                }

                public override string AppRelativeCurrentExecutionFilePath
                {
                    get { return appRelativeUrl; }
                }

                public override string PathInfo
                {
                    get { return ""; }
                }
            }
        }
    }
}
