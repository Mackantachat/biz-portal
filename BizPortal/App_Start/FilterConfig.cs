using BizPortal.Models;
using BizPortal.Utils.Annotations;
using System.Web;
using System.Web.Mvc;

namespace BizPortal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new IntroSiteAttribute());
            filters.Add(new CustomHandleErrorAttribute());
            filters.Add(new CustomViewForHttpStatusResultFilter(new HttpNotFoundResult(), "~/Views/Error/PageNotFound.cshtml"));
        }
    }
}
