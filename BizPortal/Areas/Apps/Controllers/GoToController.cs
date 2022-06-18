using BizPortal.Utils.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Areas.Apps.Controllers
{
    [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
    public class GoToController : AppsControllerBase
    {
        // GET: Apps/GoTo
        /// <summary>
        /// Go to app by application id
        /// </summary>
        /// <param name="id">application id</param>
        /// <returns></returns>
        public ActionResult Id(int id)
        {
            var app = DB.Applications.Where(o => !o.IsDeleted && o.ApplicationID == id).SingleOrDefault();
            if (app == null || string.IsNullOrEmpty(app.ApplicationUrl))
                return RedirectToAction("Index", "Home", new { area = string.Empty, showMsg = ShowMsgType.ApplicationNotFound.GetEnumStringValue() });

            app.ApplicationUrl = app.ApplicationUrl.Replace("{language}", CurrentCulture);

            return Redirect(app.ApplicationUrl);
        }
    }
}