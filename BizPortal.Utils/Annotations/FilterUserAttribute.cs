using BizPortal.DAL;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BizPortal.Utils.Annotations
{
    public class FilterUserAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        private ApplicationDbContext _db;

        public ApplicationDbContext DB
        {
            get
            {
                if (_db == null)
                    _db = new ApplicationDbContext();
                return _db;
            }
            protected set
            {
                _db = value;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var mvcContext = new HttpContextWrapper(HttpContext.Current);

            mvcContext.Request.RequestContext.RouteData.DataTokens.TryGetValue("area", out object requestArea);
            mvcContext.Request.RequestContext.RouteData.Values.TryGetValue("controller", out object requestController);

            if (mvcContext.User.Identity.IsAuthenticated && (requestArea == null || requestArea.ToString().ToLower() != "manage"))
            {
                var id = mvcContext.User.Identity.GetUserId();
                var isAgent = DB.Users.Include(e => e.Roles).Any(e => e.Id == id && e.Roles.Count > 0);

                if (isAgent)
                {
                    bool applyDomainRoute = bool.Parse(ConfigurationManager.AppSettings["ApplyDomainRoute"]);
                    if (applyDomainRoute)
                    {
                        filterContext.Result = new RedirectResult(ConfigurationManager.AppSettings["ServicesDomain"] + "/th/BackOffice/ApplicationStatus");
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(
                             new RouteValueDictionary(
                                 new
                                 {
                                     area = "Manage",
                                     controller = "ApplicationStatus",
                                     action = "Index"
                                 }
                             )
                         );
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
