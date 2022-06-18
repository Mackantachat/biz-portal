using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Areas.Apps.Controllers
{
    public class MWAController : AppsControllerBase
    {
        // GET: Apps/MWA
        public ActionResult Index()
        {
            return View();
        }
    }
}