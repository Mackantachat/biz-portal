using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Controllers
{
    public class TempController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Banner()
        {
            return View();
        }
    }
}