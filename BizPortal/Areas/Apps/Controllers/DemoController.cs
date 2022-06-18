using BizPortal.Utils.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Areas.Apps.Controllers
{
    [AuthorizeUser(OpenIDUserType = "JuristicPerson")]
    public class DemoController : AppsControllerBase
    {
        // GET: Apps/Demo
        public ActionResult Index()
        {
            ViewBag.JuristicID = IdentityID;
            return View();
        }
    }
}