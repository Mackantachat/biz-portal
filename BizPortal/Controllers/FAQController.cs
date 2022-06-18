using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Controllers
{
    public class FAQController : ControllerBase
    {
        // GET: FAQ
        public ActionResult Index()
        {
            return View();
        }
    }
}