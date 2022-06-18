using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.SmartQuizWeb.Controllers
{
    public class SmartQuizController : Controller
    {
        // GET: SmartQuiz
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Init()
        {
            return null;
        }
    }
}