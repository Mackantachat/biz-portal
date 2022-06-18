using BizPortal.Areas.Apps.Controllers;
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Annotations;
using BizPortal.Utils.Helpers;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;

namespace BizPortal.Controllers
{
    [FilterUser]
    public class QuizController : AppsControllerBase
    {
        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        public ActionResult Group(string id)
        {

            if (id.ToUpper() == "STARTINGBUSINESS")// Go to view StartingBusiness
            {
                if (IdentityType == UserTypeEnum.Citizen)
                {
                    return RedirectToAction("View", "Content", new { area = "Landing", id = 41, language = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName });
                }
                return View("StartingBusiness");
            }

            ViewBag.Title = ResourceHelper.GetResourceWord("TITLE_" + id.ToUpper(), "Quiz");

            var group = QuizGroup.GetQuizGroup(id);

            if (group == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var rows = QuizSectionRow.GetQuizSectionRows(group.QuizGroupName);

            ViewBag.CitizenTime = false;
            ViewBag.Group = group;
            ViewBag.SectionRows = rows;

            return View();
        }

        public ActionResult Business(string id)
        {
            var smartQuiz = SmartQuiz.GetSmartQuiz(id.ToUpper());
            if (smartQuiz == null)
            {
                // ถ้าไม่เจอ group ใน Quiz ให้ไปหาใน SmartQuiz
                return RedirectToAction("Group", new { id = id });
            }
            ViewBag.ReturnUrl = Request.Url.ToString();
            ViewBag.LogoutReturnUrl = Request.Url.ToString();
            return View(smartQuiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnonymousQuizAnswerSubmit(string qid)
        {
            var collection = Request.Form;
            var qa = MongoFactory.GetAnonymousQuizAnswerCollection();
            var existing = MongoFactory.GetAnonymousQuizAnswerCollection().AsQueryable()
                .Where(x => x.QaID == Guid.Parse(collection["qaID"])).ToList();
            foreach (var e in existing)
            {
                MongoFactory.GetAnonymousQuizAnswerCollection().Delete(e);
            }
            AnonymousQuizAnswer item = new AnonymousQuizAnswer
            {
                QaID = Guid.Parse(collection["qaID"]),
                Collection = collection.ToDictionary<string, string>(),
                CreatedTime = DateTime.Now,
            };

            qa.InsertOne(item);

            return Redirect(Url.ServiceAction("List", "SingleForm", new { area = "Apps", qaID = item.QaID }));
        }
    }
}