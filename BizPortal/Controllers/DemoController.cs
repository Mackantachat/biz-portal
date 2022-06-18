using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Controllers
{
    public class DemoController : ControllerBase
    {
        // GET: Demo
        public ActionResult Index()
        {
            notifyMsg(NotifyMsgType.Success, "TEST SUCCESS MESSAGE");
            return View();
        }
    }
}