using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using EGA.WS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Controllers
{
    public class ContactUsController : ControllerBase
    {
        // GET: ContactUs
        public ActionResult Index()
        {
            EGAWSAPI api = EGAWSAPI.CreateInstance(ConfigurationValues.ConsumerKey, ConfigurationValues.ConsumerSecret);
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("id", DateTime.Now.ToString("yyyy", new System.Globalization.CultureInfo("en-US")));
            var objResult = api.Get(ConfigurationValues.GetHolliday, param);
            List<HollidayViewModel> day = JsonConvert.DeserializeObject<List<HollidayViewModel>>(objResult["Result"].ToString());
            ViewBag.Holliday = day.Where(w => w.Date > DateTime.Now).Take(3).ToList();
            ViewBag.Questions = getContactQuestion(DropdownlistType.NONE);
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Question == "อื่นๆ")
                        model.Question = "ติดต่อสอบถาม";

                    EmailHelper.SendContactEmail(model.Question, model.Message, model.Name, model.CitizenId, model.Email, model.Telephone, "", false);
                    notifyMsg(NotifyMsgType.Success, Resources.ContactUs.SEND_MAIL_COMPLETED);
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    notifyMsg(NotifyMsgType.Error, Resources.ContactUs.SEND_MAIL_FAILED);
                }
            }

            EGAWSAPI api = EGAWSAPI.CreateInstance(ConfigurationValues.ConsumerKey, ConfigurationValues.ConsumerSecret);
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("id", DateTime.Now.ToString("yyyy", new System.Globalization.CultureInfo("en-US")));
            var objResult = api.Get(ConfigurationValues.GetHolliday, param);
            List<HollidayViewModel> day = JsonConvert.DeserializeObject<List<HollidayViewModel>>(objResult["Result"].ToString());
            ViewBag.Holliday = day.Where(w => w.Date > DateTime.Now).Take(3).ToList();
            ViewBag.Questions = getContactQuestion(DropdownlistType.NONE);
            return View(model);
        }
    }


}