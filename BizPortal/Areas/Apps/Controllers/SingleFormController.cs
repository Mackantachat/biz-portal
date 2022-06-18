using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Annotations;
using BizPortal.Utils.Helpers;
using BizPortal.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Globalization;
using BizPortal.Integrated;
using BizPortal.ViewModels.ControlData;
using MongoDB.Driver;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels;
using Mapster;
using System.Collections.Specialized;
using NCalc;
using BizPortal.Models.Reports;
using System.Text.RegularExpressions;
using BizPortal.DAL;
using BizPortal.AppsHook;
using Newtonsoft.Json.Linq;
using System.Configuration;
using BizPortal.ViewModels.Apps;
using BizPortal.Service;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace BizPortal.Areas.Apps.Controllers
{
    [AllowAnonymous]
    public class SingleFormController : AppsControllerBase
    {

        //--// Frontis fix: For supporting OR between each condition 
        private bool QuizAnswerFulFill(QuizAnswer expectedAnswer, Dictionary<string, string> actualAnswer)
        {
            if (expectedAnswer.Key == "")
            {
                foreach (var innerAnswer in expectedAnswer.Answers)
                {
                    bool fulfill = this.QuizAnswerFulFill(innerAnswer, actualAnswer);
                    if (expectedAnswer.IsOr && fulfill)
                        return true;
                    if (!expectedAnswer.IsOr && !fulfill)
                        return false;
                }
                return !expectedAnswer.IsOr;
            }
            else
            {
                if (actualAnswer.ContainsKey(expectedAnswer.Key))
                {
                    string value = actualAnswer[expectedAnswer.Key];
                    return expectedAnswer.Values.Contains(value);
                }
                return false;
            }
        }

        private List<string> GetWarning(AppWarning[] appWarnings, Dictionary<string, string> actualAnswer)
        {
            if (appWarnings == null)
                return new List<string>();

            var warnings = new List<string>();
            foreach (var appWarning in appWarnings)
            {
                var shouldDisplay = true;
                foreach (var cond in appWarning.Conditions)
                {
                    shouldDisplay &= QuizAnswerFulFill(cond, actualAnswer);
                }
                if (shouldDisplay)
                {
                    warnings.Add(appWarning.Message);
                }
            }
            return warnings;
        }

        private SingleFormAppsViewModel GetAppViewModel(UserTypeEnum idType, string appSysName, string[] warnings)
        {
            SingleFormAppsViewModel vm = null;
            if (idType == UserTypeEnum.Citizen)
            {
                vm = DB.Applications.Where(o => !o.IsDeleted && o.SingleFormEnabled && o.ApplicationSysName == appSysName)
                        .GroupJoin(DB.ApplicationTranslations.Where(o => o.LanguageID == DB.CurrentLanguageID), l => l.ApplicationID, r => r.ApplicationID, (l, r) => new { Application = l, Translation = r.FirstOrDefault() })
                        .GroupJoin(DB.OrganizationTranslations, l => l.Application.OrgCode, r => r.OrgCode, (l, r) => new { Application = l.Application, Translation = l.Translation, OrgTranslation = r.FirstOrDefault() })
                        .Select(o => new SingleFormAppsViewModel()
                        {
                            ApplicationID = o.Application.ApplicationID,
                            AppSysName = o.Application.ApplicationSysName,
                            AppName = o.Translation != null ? o.Translation.ApplicationName : o.Application.ApplicationSysName,
                            OrganizationName = o.OrgTranslation != null ? o.OrgTranslation.OrgName : o.Application.Organization.OrgSysName,
                            LogoFileID = o.Application.LogoFileID,
                            OnlineRequestAllowed = !o.Application.CitizenRequestAtOrg && !o.Application.TemporaryDisable,
                            TemporaryDisable = o.Application.TemporaryDisable,
                            TemporaryRemark = o.Application.TemporaryDisable ? o.Application.TemporaryRemark : null,
                            HideBizPortalSoon = o.Application.TemporaryDisable,
                            HandbookUrl = o.Application.CitizenHandbookUrl,
                            ApplicationUrl = o.Application.CitizenApplicationUrl,
                            OperatingCostType = o.Application.CitizenOperatingCostType,
                            OperatingCost2 = o.Application.CitizenOperatingCost2,
                            OperatingCost = o.Application.CitizenOperatingCost,
                            OperatingDaysType = o.Application.CitizenOperatingDayType,
                            OperatingDays = o.Application.CitizenOperatingDays,
                            OperatingDays2 = o.Application.CitizenOperatingDays2,
                            ShowRemark = o.Application.CitizenShowRemark,
                            Remark = o.Application.CitizenRemark
                        }).FirstOrDefault();
            }
            else
            {
                vm = DB.Applications.Where(o => !o.IsDeleted && o.SingleFormEnabled &&
                                                o.ApplicationSysName == appSysName)// &&  // Somjet: สามารถเป็น null ได้ ไม่สามารถใช้เงื่อนไขนี้ได้ครับ
                                                                                   //o.OperatingDays != null) // ใช้ OperatingDays ในการดูว่านิติบุคคลสามารถขออนุญาตได้หรือไม่
                        .GroupJoin(DB.ApplicationTranslations.Where(o => o.LanguageID == DB.CurrentLanguageID), l => l.ApplicationID, r => r.ApplicationID, (l, r) => new { Application = l, Translation = r.FirstOrDefault() })
                        .GroupJoin(DB.OrganizationTranslations, l => l.Application.OrgCode, r => r.OrgCode, (l, r) => new { Application = l.Application, Translation = l.Translation, OrgTranslation = r.FirstOrDefault() })
                        .Select(o => new SingleFormAppsViewModel()
                        {
                            ApplicationID = o.Application.ApplicationID,
                            AppSysName = o.Application.ApplicationSysName,
                            AppName = o.Translation != null ? o.Translation.ApplicationName : o.Application.ApplicationSysName,
                            OrganizationName = o.OrgTranslation != null ? o.OrgTranslation.OrgName : o.Application.Organization.OrgSysName,
                            OnlineRequestAllowed = !o.Application.RequestAtOrg && !o.Application.TemporaryDisable,
                            TemporaryDisable = o.Application.TemporaryDisable,
                            TemporaryRemark = o.Application.TemporaryDisable ? o.Application.TemporaryRemark : null,
                            HideBizPortalSoon = o.Application.TemporaryDisable,
                            LogoFileID = o.Application.LogoFileID,
                            HandbookUrl = o.Application.HandbookUrl,
                            ApplicationUrl = o.Application.ApplicationUrl,
                            OperatingCostType = o.Application.OperatingCostType,
                            OperatingCost2 = o.Application.OperatingCost2,
                            OperatingCost = o.Application.OperatingCost,
                            OperatingDaysType = o.Application.OperatingDayType,
                            OperatingDays = o.Application.OperatingDays,
                            OperatingDays2 = o.Application.OperatingDays2,
                            ShowRemark = o.Application.ShowRemark,
                            Remark = o.Application.Remark
                        }).FirstOrDefault();
            }
            if (vm != null)
            {
                vm.Warning = warnings;
                vm.NotAllowedReason = vm.TemporaryDisable ? new string[] { vm.TemporaryRemark } : null;
            }

            return vm;
        }

        private SingleFormAttachmentViewModel AddFileConsumerKey(SingleFormAttachmentViewModel viewModel, List<string> fileConsumeyKeys)
        {
            if (fileConsumeyKeys != null && fileConsumeyKeys.Count > 0)
            {
                viewModel.FileConsumerKey = fileConsumeyKeys;
            }
            else
            {
                viewModel.FileConsumerKey = null;
            }
            return viewModel;
        }

        //--// Frontis fix: For supporting OR between each condition 
        [HttpGet]
        public ActionResult List2(string bid, string appList, string viewName)
        {
            // เพื่อรองรับการ login ในหน้า List2 จำเป็นต้องทำแบบ Get เพื่อให้สามารถกลับมาทำงานต่อได้
            // เนื่องจากแบบ Get ข้อมูลที่จำเป็นอยู่ใน return url อยู่แล้ว
            var appIDList = Newtonsoft.Json.JsonConvert.DeserializeObject<int[]>(appList);
            var model = new PermitSummaryViewModel();
            var appModelList = new List<SingleFormAppsViewModel>();
            foreach (var app in DB.Applications.Where(a => appIDList.Contains(a.ApplicationID)))
            {
                var vm = new SingleFormAppsViewModel()
                {
                    ApplicationID = app.ApplicationID,
                    AppSysName = app.ApplicationSysName,
                };
                appModelList.Add(vm);
            }

            ViewBag.BusinessType = bid;
            ViewBag.PreviousPage = viewName;
            model.Apps = appModelList.ToArray();
            return List(model, bid);
        }

        [HttpPost]
        public ActionResult List2()
        {
            // หน้า Business/PermitList ส่งมาเป็น AppSysName ก่อน
            // ทำการแปลง appSysName ให้เป็น appID แล้ว redirect ไปเป็น Get เพื่อให้สถานะการเลือกใบอนุญาตไปอยู่ใน parameter ของ url
            // เพื่อให้รองรับการ login ในหน้า PermitSummary ได้ 
            var appList1 = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(Request["appList"]);
            var bid = Request["bid"];
            var appIDList = DB.Applications.Where(a => appList1.Contains(a.ApplicationSysName)).Select(x => x.ApplicationID).ToArray();
            var viewName = "PermitList"; //สำหรับ return กลับไปหน้า business
            if (Request["viewName"] != null)
            {
                viewName = Request["viewName"];
            }
            return Redirect(Url.ServiceAction("List2", "SingleForm", new { area = "Apps", bid = bid, appList = Newtonsoft.Json.JsonConvert.SerializeObject(appIDList), viewName = viewName }));
        }

        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        [EncryptedActionParameter]
        [HttpGet]
        public ActionResult BackToSummary(string tID) 
        {
            var singleFormTransaction = MongoFactory.GetSingleFormTransactionCollection().AsQueryable().Where(x => x.TransactionID.Equals(tID));
            string bID = singleFormTransaction.FirstOrDefault().BusinessId;
            string[] listAppSysName = singleFormTransaction.FirstOrDefault().Apps.ToArray();
            int[] appIDList = DB.Applications.Where(a => listAppSysName.Contains(a.ApplicationSysName)).Select(x => x.ApplicationID).ToArray();

            return Redirect(Url.ServiceAction("List2", "SingleForm", new { area = "Apps", bid = bID, appList = Newtonsoft.Json.JsonConvert.SerializeObject(appIDList), viewName = "PermitList" }));
        }

        [AllowAnonymous]
        public ActionResult List(PermitSummaryViewModel model, string id = null, Guid? qaID = null)
        {
            List<SingleFormAppsViewModel> supportApps = new List<SingleFormAppsViewModel>();
            List<QuizAppMapping> mappings = null;
            Dictionary<string, string> collection = null;
            SingleFormAppsViewModel app = null;
            UserTypeEnum idType;
            ViewBag.AgeAllowed = true;

            try
            {

                idType = IdentityType;

                if (idType == UserTypeEnum.Citizen) 
                {

                    if (model.Apps != null) 
                    {
                        // Try get IdentityType
                        ViewBag.AgeAllowed = false;
                        ViewBag.IsSpa = false;

                        if (model.Apps.Any(x => x.AppSysName.Equals("ขอใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ")))
                        {

                            ViewBag.IsSpa = true;
                            string strBirtDate = User.Identity.GetBirthDate(); ;

                            if (!strBirtDate.Trim().Equals(string.Empty))
                            {

                                string[] appStrBirtDate = strBirtDate.Split(' ');
                                appStrBirtDate = appStrBirtDate[0].Split('/');
                                DateTime citizenBirtDate = new DateTime(Convert.ToInt32(appStrBirtDate[2]) - 543, Convert.ToInt32(appStrBirtDate[1]), Convert.ToInt32(appStrBirtDate[0]));
                                ViewBag.AgeAllowed = IdentityHelper.GetAllowAge(citizenBirtDate);

                            }

                        }
                    }
                }

            }
            catch
            {
                // Exception -> Anonymous user
                // Show Permit
                idType = UserTypeEnum.Anonymous;
            }

            if (model.Apps != null)
            {
                collection = new Dictionary<string, string>();
                collection.Add(QuizAnswer.UserIdentityType, idType.ToString());
                mappings = QuizAppMapping.GetQuizAppMappings(id);
                foreach (var app1 in model.Apps)
                {
                    SingleFormAppsViewModel dbApp = null;

                    // เปลี่ยนจาก SingleOrDefault เป็น FirstOrDefault เพราะมีโอกาสที่ mapping จะมีมากกว่า 1  (ต่างเงื่อนไข แต่ได้ใบเดียวกัน)
                    var mapping = mappings.Where(m => m.AppSystemName == app1.AppSysName).FirstOrDefault();

                    if (mapping != null)
                    {
                        var warnings = GetWarning(mapping.Warnings, collection);
                        dbApp = GetAppViewModel(idType, app1.AppSysName, warnings.ToArray());
                    }
                    else
                    {
                        dbApp = GetAppViewModel(idType, app1.AppSysName, null);
                    }

                    if (dbApp != null)
                    {
                        supportApps.Add(dbApp);
                    }
                }
            }
            else if (qaID != null || (!string.IsNullOrEmpty(id) && Request.HttpMethod == "POST"))
            {
                if (!string.IsNullOrEmpty(id) && Request.HttpMethod == "POST")
                {
                    collection = Request.Form.ToDictionary<string, string>();
                }
                else
                {
                    var qa = MongoFactory.GetAnonymousQuizAnswerCollection().AsQueryable()
                        .Where(q => q.QaID == qaID).SingleOrDefault();
                    collection = qa.Collection;
                    id = collection["id"];
                }
                collection.Add(QuizAnswer.UserIdentityType, idType.ToString());

                if (id == "FINANCE")
                {
                    string TYPE_A = "QUESTION_FINANCE_ACT_CONSULTANT";
                    string TYPE_B = "QUESTION_FINANCE_ACT_STOCK";
                    string TYPE_C = "QUESTION_FINANCE_ACT_DEBT";
                    string TYPE_D = "QUESTION_FINANCE_ACT_INVESTMENT";
                    string TYPE_E = "QUESTION_FINANCE_ACT_CONTRACT";
                    string TYPE_F = "QUESTION_FINANCE_ACT_FUNDS";
                    string TYPE_G = "QUESTION_FINANCE_ACT_STOCK_SBL";

                    int countChecked = 0;

                    countChecked += (collection.ContainsKey(TYPE_A) && collection[TYPE_A] == "on") ? 1 : 0;
                    countChecked += (collection.ContainsKey(TYPE_B) && collection[TYPE_B] == "on") ? 1 : 0;
                    countChecked += (collection.ContainsKey(TYPE_C) && collection[TYPE_C] == "on") ? 1 : 0;
                    countChecked += (collection.ContainsKey(TYPE_D) && collection[TYPE_D] == "on") ? 1 : 0;
                    countChecked += (collection.ContainsKey(TYPE_E) && collection[TYPE_E] == "on") ? 1 : 0;
                    countChecked += (collection.ContainsKey(TYPE_F) && collection[TYPE_F] == "on") ? 1 : 0;
                    countChecked += (collection.ContainsKey(TYPE_G) && collection[TYPE_G] == "on") ? 1 : 0;


                    if (countChecked > 2)
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            // X
                        });
                    }
                    else if (collection.ContainsKey(TYPE_A) && collection[TYPE_A] == "on" &&
                        collection.ContainsKey(TYPE_B) && collection[TYPE_B] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_A, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_A) && collection[TYPE_A] == "on" &&
                             collection.ContainsKey(TYPE_C) && collection[TYPE_C] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_B, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_A) && collection[TYPE_A] == "on" &&
                             collection.ContainsKey(TYPE_D) && collection[TYPE_D] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_D, new string[] { }),
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_E, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_A) && collection[TYPE_A] == "on" &&
                             collection.ContainsKey(TYPE_E) && collection[TYPE_E] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            // SEC
                        });
                    }
                    else if (collection.ContainsKey(TYPE_A) && collection[TYPE_A] == "on" &&
                             collection.ContainsKey(TYPE_F) && collection[TYPE_F] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_C, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_A) && collection[TYPE_A] == "on" &&
                             collection.ContainsKey(TYPE_G) && collection[TYPE_G] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_E, new string[] { }),
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_F, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_B) && collection[TYPE_B] == "on" &&
                             collection.ContainsKey(TYPE_C) && collection[TYPE_C] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_A, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_B) && collection[TYPE_B] == "on" &&
                             collection.ContainsKey(TYPE_D) && collection[TYPE_D] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_A, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_B) && collection[TYPE_B] == "on" &&
                             collection.ContainsKey(TYPE_E) && collection[TYPE_E] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            // X
                        });
                    }
                    else if (collection.ContainsKey(TYPE_B) && collection[TYPE_B] == "on" &&
                             collection.ContainsKey(TYPE_F) && collection[TYPE_F] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_A, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_B) && collection[TYPE_B] == "on" &&
                             collection.ContainsKey(TYPE_G) && collection[TYPE_G] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_A, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_C) && collection[TYPE_C] == "on" &&
                             collection.ContainsKey(TYPE_D) && collection[TYPE_D] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_B, new string[] { }),
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_D, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_C) && collection[TYPE_C] == "on" &&
                             collection.ContainsKey(TYPE_E) && collection[TYPE_E] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_B, new string[] { }),
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_G, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_C) && collection[TYPE_C] == "on" &&
                             collection.ContainsKey(TYPE_F) && collection[TYPE_F] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            // SEC
                        });
                    }
                    else if (collection.ContainsKey(TYPE_C) && collection[TYPE_C] == "on" &&
                             collection.ContainsKey(TYPE_G) && collection[TYPE_G] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_B, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_D) && collection[TYPE_D] == "on" &&
                             collection.ContainsKey(TYPE_E) && collection[TYPE_E] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_D, new string[] { }),
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_G, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_D) && collection[TYPE_D] == "on" &&
                             collection.ContainsKey(TYPE_F) && collection[TYPE_F] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_C, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_D) && collection[TYPE_D] == "on" &&
                             collection.ContainsKey(TYPE_G) && collection[TYPE_G] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_D, new string[] { }),
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_F, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_E) && collection[TYPE_E] == "on" &&
                             collection.ContainsKey(TYPE_F) && collection[TYPE_F] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_C, new string[] { }),
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_G, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_E) && collection[TYPE_E] == "on" &&
                             collection.ContainsKey(TYPE_G) && collection[TYPE_G] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_F, new string[] { }),
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_G, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_F) && collection[TYPE_F] == "on" &&
                             collection.ContainsKey(TYPE_G) && collection[TYPE_G] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_C, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_A) && collection[TYPE_A] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_E, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_B) && collection[TYPE_B] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_A, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_C) && collection[TYPE_C] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_B, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_D) && collection[TYPE_D] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_D, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_E) && collection[TYPE_E] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_G, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_F) && collection[TYPE_F] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_C, new string[] { }),
                        });
                    }
                    else if (collection.ContainsKey(TYPE_G) && collection[TYPE_G] == "on")
                    {
                        supportApps.AddRange(new List<SingleFormAppsViewModel>()
                        {
                            GetAppViewModel(idType, AppSystemNameTextConst.APP_SEC_NEW_F, new string[] { }),
                        });
                    }
                }
                else
                {
                    // From Quiz
                    mappings = QuizAppMapping.GetQuizAppMappings(id);

                    foreach (var map in mappings)
                    {
                        int countDisplayCondition = map.DisplayConditions != null ? map.DisplayConditions.Count() : 0;

                        //--// Frontis fix: For supporting OR between each condition 
                        bool shouldDisplay = true;
                        foreach (var cond in map.DisplayConditions)
                        {
                            shouldDisplay &= QuizAnswerFulFill(cond, collection);
                        }
                        //--// Frontis fix: For supporting OR between each condition 

                        if (shouldDisplay)
                        {
                            int countOnlineRequestAllowedConditions = map.OnlineRequestAllowedConditions != null ? map.OnlineRequestAllowedConditions.Count() : 0;
                            int validOnlineRequestAllowedConditions = 0;
                            List<string> rejectReasons = new List<string>();

                            foreach (var cond in map.OnlineRequestAllowedConditions)
                            {
                                if (collection[cond.Key] != null)
                                {
                                    string value = collection[cond.Key];
                                    if (cond.Values.Contains(value))
                                    {
                                        validOnlineRequestAllowedConditions++;
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(cond.InvalidAnswerReason))
                                        {
                                            rejectReasons.Add(ResourceHelper.GetResourceWordWithDefault(cond.InvalidAnswerReason, "QuizConditionReason", cond.InvalidAnswerReason));
                                        }
                                    }
                                }
                            }

                            List<string> warnings = GetWarning(map.Warnings, collection);

                            app = GetAppViewModel(idType, map.AppSystemName, warnings.ToArray());
                            if (app != null)
                            {
                                app.OnlineRequestAllowed &= (countOnlineRequestAllowedConditions == validOnlineRequestAllowedConditions);
                                if (!app.OnlineRequestAllowed)
                                {
                                    app.NotAllowedReason = rejectReasons.ToArray();
                                }
                                supportApps.Add(app);
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.EnableBackButton = true;
                ViewBag.QuizGroupName = id;
            }
            else
            {
                ViewBag.EnableBackButton = false;
            }

            ViewBag.IdentityType = idType;
            ViewBag.ReturnUrl = Request.Url.ToString();
            ViewBag.LogoutReturnUrl = ""; // Logout will return to home
            ViewBag.BusinessType = id;

            JObject juristicProfile = null;
            if (idType == UserTypeEnum.Juristic)
            {
                juristicProfile = IdentityHelper.GetJuristicProfile(IdentityID);
                if (juristicProfile == null || !juristicProfile.HasValues)
                {
                    foreach (var supportApp in supportApps)
                    {
                        supportApp.OnlineRequestAllowed = false;
                        supportApp.NotAllowedReason = new string[] { "ไม่สามารถเชื่อมต่อฐานข้อมูลกรมพัฒนาธุรกิจการค้าได้ กรุณาลองใหม่อีกครั้ง" };
                    }

                    supportApps = supportApps.OrderByDescending(o => o.OnlineRequestAllowed).ToList();
                    return View(new SingleForm() { BusinessID = id, SingleFormApps = supportApps });
                }
            }

            var vcdApp = supportApps.Where(x => x.AppSysName == AppSystemNameTextConst.APP_VCD).FirstOrDefault();
            if (vcdApp != null)
            {
                if (supportApps.Where(x => x.AppSysName == AppSystemNameTextConst.APP_BROTHEL).Count() > 0)
                {
                    supportApps.Remove(vcdApp);
                }
            }

            var unsupportedType = new UserTypeEnum[] { UserTypeEnum.Citizen, UserTypeEnum.Foreigner, UserTypeEnum.GovernmentAgent };

            var swhApp = supportApps.Where(x => x.AppSysName == AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW ||
                                                x.AppSysName == AppSystemNameTextConst.APP_SOFTWARE_HOUSE_RENEW ||
                                                x.AppSysName == AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT).FirstOrDefault();
            if (swhApp != null)
            {
                //swhApp.HideBizPortalSoon = true;
                //swhApp.OnlineRequestAllowed = false;
                //swhApp.NotAllowedReason = new string[] { "ไม่สามารถเชื่อมต่อฐานข้อมูลกรมสรรพากรได้ กรุณาลองใหม่อีกครั้ง" };

                // แก้ตาม Comment ของ กรมสรรพากร
                swhApp.OnlineRequestAllowed = false;
                //return View(new SingleForm() { BusinessID = id, SingleFormApps = supportApps });

                if (idType.ToString() == "Anonymous")
                {
                    swhApp.OnlineRequestAllowed = false;
                    swhApp.NotAllowedReason = new string[] { "คุณยังไม่ได้ Login เข้าสู่ระบบ BizPortal" };
                    return View(new SingleForm() { BusinessID = id, SingleFormApps = supportApps });
                }

                Dictionary<string, string> vatArgs = new Dictionary<string, string>();
                vatArgs.Add("TaxID", IdentityID);

                var vatStatus = Api.Get(ConfigurationManager.AppSettings["VAT_STATUS_WS_URL"], vatArgs);

                if (vatStatus != null)
                {
                    //ไม่ต้อง validate จด vat ชั่วคราวช่วง test
                    var isNotChecking = ConfigurationManager.AppSettings["SoftwareHouseVatChecking"] == "false";

                    if (isNotChecking)
                    {
                        swhApp.OnlineRequestAllowed = true;
                        swhApp.NotAllowedReason = null;
                    }
                    else
                    {
                        if (vatStatus.HasValues &&
                               vatStatus["responseData"]["VatStatus"].ToString() == "Y" &&
                               (vatStatus["responseData"]["VatApr"].ToString() == "1" ||
                               vatStatus["responseData"]["VatApr"].ToString() == "3"))
                        {
                            swhApp.OnlineRequestAllowed = true;
                            swhApp.NotAllowedReason = null;
                        }
                        else
                        {
                            swhApp.OnlineRequestAllowed = false;
                            swhApp.NotAllowedReason = new string[] { "ท่านยังไม่ได้ดำเนินการจดทะเบียนภาษีมูลค่าเพิ่ม กรุณาดำเนินการให้แล้วเสร็จก่อนยื่นคำขอ" };
                            if (idType == UserTypeEnum.Juristic)
                            {
                                string vatUrl = Url.ServiceAction("List2", "SingleForm", new { area = "Apps", bid = "STARTING_BUSINESS", appList = Newtonsoft.Json.JsonConvert.SerializeObject(new int[] { 9 }), viewName = "PermitList" });
                                swhApp.ApplicationUrl = vatUrl;
                            }
                            else if (idType == UserTypeEnum.Citizen)
                            {
                                swhApp.ApplicationUrl = ConfigurationManager.AppSettings["VAT_EXTERNAL_REGISTATION_URL"];
                            }
                        }
                    }
                }
            }

            var vatApp = supportApps.Where(o => o.AppSysName == AppSystemNameTextConst.APP_VAT && !o.TemporaryDisable).SingleOrDefault();
            if (vatApp != null)
            {
                vatApp.HideBizPortalSoon = true;

                if (unsupportedType.Contains(idType))
                {
                    vatApp.OnlineRequestAllowed = false;
                    vatApp.NotAllowedReason = new string[] { "กรุณาเข้าสู่ระบบด้วยบัญชีนิติบุคคล" };
                }
                else if (idType == UserTypeEnum.Juristic)
                {
                    vatApp.OnlineRequestAllowed = false;
                    vatApp.NotAllowedReason = new string[] { "ไม่สามารถเชื่อมต่อฐานข้อมูลกรมสรรพากรได้ กรุณาลองใหม่อีกครั้ง" };

                    Dictionary<string, string> vatArgs = new Dictionary<string, string>();
                    vatArgs.Add("TaxID", IdentityID);

                    var vatStatus = Api.Get(ConfigurationManager.AppSettings["VAT_STATUS_WS_URL"], vatArgs);

                    if (vatStatus != null)
                    {
                        if (vatStatus.HasValues &&
                            vatStatus["responseData"]["VatStatus"].ToString() == "N" &&
                            vatStatus["responseData"]["VatApr"].ToString() != "0" &&
                            vatStatus["responseData"]["VatApr"].ToString() != "1")
                        {
                            if (juristicProfile != null)
                            {
                                if (juristicProfile.HasValues)
                                {
                                    JuristicProfile jur = juristicProfile.ToObject<JuristicProfile>();
                                    var vatCommitteeErrors = 0;
                                    var vatAddrErrors = 0;
                                    List<string> errorText = new List<string>();
                                    if (jur.CommitteeInformations != null && jur.CommitteeInformations.Length > 0)
                                    {
                                        foreach (var committee in jur.CommitteeInformations.Where(o => string.IsNullOrEmpty(o.CitizenID) || o.CitizenID.Length != 13))
                                        {
                                            vatCommitteeErrors++;
                                        }
                                    }
                                    if (jur.AddressInformations != null && jur.AddressInformations.Length > 0)
                                    {
                                        var addrInfo = jur.AddressInformations[0];
                                        if (string.IsNullOrEmpty(addrInfo.Soi) && string.IsNullOrEmpty(addrInfo.Road))
                                        {
                                            vatAddrErrors++;
                                        }
                                    }

                                    if (vatCommitteeErrors == 0 && vatAddrErrors == 0)
                                    {
                                        vatApp.OnlineRequestAllowed = true;
                                        vatApp.NotAllowedReason = null;
                                    }
                                    else
                                    {
                                        if (vatCommitteeErrors > 0)
                                        {
                                            errorText.Add("ข้อมูลกรรมการผู้มีอำนาจลงนามไม่ครบ");
                                        }
                                        if (vatAddrErrors > 0)
                                        {
                                            errorText.Add("ต้องมีตรอก/ซอย หรือถนน");
                                        }
                                        errorText.Add("กรุณาติดต่อเข้ารับบริการ ณ กรมสรรพากรพื้นที่ หรือสอบถามรายละเอียดเพิ่มเติมได้ที่หมายเลขโทรศัพท์ 1161");
                                        vatApp.NotAllowedReason = errorText.ToArray();
                                    }
                                }
                                else
                                {
                                    vatApp.NotAllowedReason = new string[] { "ไม่พบข้อมูล" };
                                }
                            }
                            else
                            {
                                vatApp.NotAllowedReason = new string[] { "ไม่สามารถเชื่อมต่อฐานข้อมูลกรมพัฒนาธุรกิจการค้าได้ กรุณาลองใหม่อีกครั้ง" };
                            }
                        }
                        else
                        {
                            vatApp.NotAllowedReason = new string[] { "ท่านได้ทำการขอจดทะเบียนภาษีมูลค่าเพิ่มแล้ว" };
                        }
                    }
                }
            }

            var dptAllowIdent = new string[]
            {
                "1218651012566", // bizconstruct1
                "1799574096868", // bizconstruct2
                "7206125329670", // bizconstruct3
                "8069373231354", // bizconstruct4
                "1913323903854", // bizconstruct5
                "0105561157085", // นิติก่อสร้าง
                "0105561156151", // นิติก่อสร้าง
                "0105561153411", // นิติก่อสร้าง
                "0105561168532", // นิติก่อสร้าง
                "0105561161333", // นิติ biz
                "0105561150161", // นิติ biz
                "3102002585001", // บุคคล พี่เอ
                "0903560003816", // นิติ พี่เอ
                "9339535092261", // บุคคล พี่เล็ก
                "5337039793561", // บุคคล UAT bizuser1
                "6579075889555", // บุคคล UAT bizuser2
                "3173006484448", // บุคคล UAT bizuser3
                "0105500002383"  // นิติบุคคล dbdid
            };

            var dptValue = bool.Parse(ConfigurationManager.AppSettings["DPT_SHOW_MENU"]);
            var dptBuildingApps = supportApps.Where(o => (o.AppSysName == AppSystemNameTextConst.APP_BUILDING_G1 || o.AppSysName == AppSystemNameTextConst.APP_BUILDING_R6) && !o.TemporaryDisable).ToList();
            if (dptBuildingApps != null && dptBuildingApps.Count > 0)
            {
                foreach (var dptBuildingApp in dptBuildingApps)
                {
                    dptBuildingApp.OnlineRequestAllowed = false;
                    if (idType == UserTypeEnum.Anonymous || dptValue || (!dptValue && dptAllowIdent.Contains(IdentityID)))
                    {
                        dptBuildingApp.OnlineRequestAllowed = true;
                    }
                }
            }

            supportApps = supportApps.OrderByDescending(o => o.OnlineRequestAllowed).ToList();

            // Find Only BKK
            List<string> appSysArr = new List<string>();
            bool foundStoreSection = false;

            foreach (SingleFormAppsViewModel dr in supportApps) 
            {

                appSysArr.Add(dr.AppSysName);

            }

            FormSection[] formSection = FormSection.GetSections("INFORMATION", appSysArr.ToArray(), idType);
            foreach (FormSection dr in formSection) 
            {
                if (dr.Section.Trim().Equals("INFORMATION_STORE")) 
                {
                 
                    foundStoreSection = true;
                    break;
                }

            }

            //if (foundStoreSection) 
            //{
            //    bool foundAllProvince = false;
            //    bool foundOnlyBKK = false;
            //    List<PermitServiceType> permitServiceType = new List<PermitServiceType>();

            //    foreach (string dr in appSysArr) 
            //    {
            //        permitServiceType.Add(new PermitServiceType() { AppSysName = dr});
            //    }

            //    FormSectionRow[] formSectionRow = FormSectionRow.GetSectionRows("INFORMATION_STORE", appSysArr.ToArray(),idType);

            //    foreach (FormSectionRow dr in formSectionRow) 
            //    {

            //        if (foundOnlyBKK & foundAllProvince) { break; }

            //        foreach (FormControl cr in dr.Controls) 
            //        {

            //            if (foundOnlyBKK & foundAllProvince) { break; }

            //            if (cr.Type != ControlType.AddressForm) { continue; }

            //            // Statment
            //            if (!foundAllProvince) 
            //            {
            //                if (cr.AddressForm_ProvinceType != ProvinceType.BKK) 
            //                {
            //                    foundAllProvince = true;
            //                    foreach (string appSysName in cr.AppSystemNames)
            //                    {

            //                        foreach (PermitServiceType appService in permitServiceType) 
            //                        {
            //                            if (appSysName.Equals(appService.AppSysName)) 
            //                            {
            //                                permitServiceType.Where(x => x.AppSysName.Equals(appService.AppSysName)).FirstOrDefault().ProvinceService = ProvinceType.All;
            //                            }
            //                        }

            //                    }
            //                }
            //            }
            //            if (!foundOnlyBKK) 
            //            {
            //                if (cr.AddressForm_ProvinceType == ProvinceType.BKK) 
            //                {
            //                    foundOnlyBKK = true;
            //                    foreach (string appSysName in cr.AppSystemNames)
            //                    {

            //                        foreach (PermitServiceType appService in permitServiceType)
            //                        {
            //                            if (appSysName.Equals(appService.AppSysName))
            //                            {
            //                                permitServiceType.Where(x => x.AppSysName.Equals(appService.AppSysName)).FirstOrDefault().ProvinceService = ProvinceType.BKK;
            //                            }
            //                        }

            //                    }
            //                }
            //            }
            //        }
            //    }

            //    ViewBag.FoundAllProvince = foundAllProvince;
            //    ViewBag.FoundOnlyBKK = foundOnlyBKK;
            //    ViewBag.PermitServiceType = permitServiceType;

            //}
            
            return View(new SingleForm() { BusinessID = id, SingleFormApps = supportApps });
        }

        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        [HttpPost]
        public ActionResult SubmitList(SingleForm model)
        {
            SingleForm singleForm = new SingleForm();
            if (model == null)
            {
                return RedirectToAction("List");
            }

            var dbTrans = MongoFactory.GetSingleFormTransactionCollection();
            var tran = dbTrans.AsQueryable().Where(o => o.IdentityID == IdentityID).SingleOrDefault();
            if (tran == null)
            {
                tran = new SingleFormTransaction()
                {
                    TransactionID = Guid.NewGuid(),
                    IdentityID = IdentityID
                };
                dbTrans.InsertOne(tran);
            }

            singleForm.SingleFormApps = model.SingleFormApps.Where(o => o.isChecked).ToList();
            tran.Apps = singleForm.SingleFormApps.Select(o => o.AppSysName).ToList();
            tran.Step = 0;
            tran.AppStep = 0;
            var group = FormSectionGroup.GetAllSectionGroup(tran.Apps.ToArray());
            tran.AppStepTotal = group.Count;
            tran.LastUpdateTime = DateTime.Now;
            tran.FileCnt = -1;
            tran.FileTotal = -1;
            tran.BusinessId = model.BusinessID;
            dbTrans.Update(tran);

            return RedirectToAction("ConsentDeclare", "SingleForm", new { tID = tran.TransactionID });

            //TODO: เอาออกก่อนรอตกลงกันค่อยขึ้น production 
            //return RedirectToAction("Index", "SingleForm", new { tID = tran.TransactionID });
        }


        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        [HttpPost]
        public ActionResult SubmitConsent(ConsentModel parm)
        {

            if (parm == null || parm.TransID == null || parm.ConsentResult == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (parm.ConsentResult == 0)
            {
                return Redirect(Url.EncodedAction("BackToSummary", "SingleForm", new { area = "Apps", tID = parm.TransID }));
            }

            IMongoCollection<SingleFormTransaction> dbTran = MongoFactory.GetSingleFormTransactionCollection();
            SingleFormTransaction tran = dbTran.AsQueryable().Where(x => x.TransactionID == parm.TransID).SingleOrDefault();
            tran.ConsentTimeStamp = DateTime.Now;
            dbTran.Update(tran);

            return Redirect(Url.EncodedAction("Index", "SingleForm", new { trid = parm.TransID }));
        }

        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        public ActionResult ConsentDeclare(Guid? tID)
        {
            ViewBag.TransID = tID;
            return View();
        }

        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        public ActionResult Resume()
        {
            var db = MongoFactory.GetSingleFormTransactionCollection().AsQueryable();
            if (User.Identity.IsAuthenticated)
            {
                var tran = db.Where(o => o.IdentityID == IdentityID).SingleOrDefault();
                if (tran.Apps.Count == 0)
                {
                    // No on going transaction
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                switch (tran.Step)
                {
                    case 1: // Upload files
                        return Redirect(Url.EncodedAction("Attachment", "SingleForm", new
                        {
                            area = "Apps",
                            trid = tran.TransactionID,
                            fromDB = true
                        }));
                    case 2: // Confirm
                        return Redirect(Url.ServiceAction("Confirmation", "SingleForm", new
                        {
                            area = "Apps",
                            fromDB = true
                        }));
                    default:
                        return Redirect(Url.EncodedAction("Index", "SingleForm", new
                        {
                            area = "Apps",
                            trid = tran.TransactionID,
                            app = tran.AppStep,
                            fromDB = true
                        }));
                }
            }
            throw new UnauthorizedAccessException();
        }

        // GET: Apps/SingleForm
        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        [EncryptedActionParameter]
        public ActionResult Index(Guid? trid = null, int step = 1, int app = 1, int edit = 0, bool fromDB = false)
 {
            var db = MongoFactory.GetSingleFormTransactionCollection().AsQueryable();
            SingleFormTransaction tran = null;

            if (trid == null)
            {
                tran = db.Where(o => o.IdentityID == IdentityID).SingleOrDefault();
                if (tran == null)
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                return Redirect(Url.EncodedAction("Index", "SingleForm", new { trid = tran.TransactionID }));
            }

            tran = db.Where(o => o.TransactionID == trid).Single();
            var apps = tran.Apps.ToArray();
            var groups = FormSectionGroup.GetAllSectionGroup(apps);
            if (app > groups.Count)
            {
                return Redirect(Url.EncodedAction("Attachment", "SingleForm", new { area = "Apps", trid = trid }));
            }
            if (app <= 1)
            {
                app = 1;
            }
            var group = groups[app - 1];
            //var group = FormSectionGroup.GetSectionGroup(apps, app - 1);

            if (group == null)
            {
                return RedirectToAction("List");
            }

            if (groups.Count != tran.AppStepTotal)
            {
                tran.AppStepTotal = groups.Count;
                MongoFactory.GetSingleFormTransactionCollection().Update(tran);
            }

            FormSection[] sections = FormSection.GetSections(group.SectionGroup, apps, IdentityType);
            List<FormSectionRow> sectionRows = new List<FormSectionRow>();
            foreach (var section in sections)
            {
                sectionRows.AddRange(FormSectionRow.GetSectionRows(section.Section, apps, IdentityType));
            }

            var allApps = new List<string>() { "INFORMATION" };
            allApps.AddRange(apps);

            ViewBag.SectionGroup = group;
            ViewBag.Sections = sections;
            ViewBag.SectionRows = sectionRows.ToArray();
            ViewBag.TransactionId = trid;
            ViewBag.NumberOfApps = FormSectionGroup.GetSectionGroupQuery(apps).Count();
            ViewBag.CurrentApp = app;
            ViewBag.AllApps = allApps;
            ViewBag.AllSectionGroups = FormSectionGroup.GetSectionGroupQuery(apps).ToList();
            ViewBag.IsDBDError = false;
            ViewBag.BackUrl = string.Empty;
            ViewBag.GetArrayData = false;
            Dictionary<string, object> defaults = new Dictionary<string, object>();
            Dictionary<string, object> arrayData = new Dictionary<string, object>();

            var appPrefillAll = MongoFactory.GetAppPrefillAnswerCollection()
                .AsQueryable()
                .Where(o => apps.Contains(o.AppSystemName))
                .ToArray();

            foreach (var appPrefill in appPrefillAll)
            {
                foreach (var prefill in appPrefill.Prefill)
                {
                    var key = prefill.SectionName != null ? (prefill.SectionName + "::" + prefill.ControlName) : prefill.ControlName;
                    if (defaults.ContainsKey(key))
                    {
                        defaults[key] = prefill.ControlPrefill;
                    }
                    else
                    {
                        defaults.Add(key, prefill.ControlPrefill);
                    }
                }
            }
            defaults.Add("APP_FACTORY_CLASS_2", IdentityID);
            defaults.Add("IDENTITY_ID", IdentityID);
            defaults.Add("GENERAL_INFORMATION__JURISTIC_VAT_ID", IdentityID);

            defaults.Add("INFORMATION_HEADER__REQUEST_DATE", DateTime.Now);
            defaults.Add("INFORMATION_HEADER__REQUEST_AT", "Biz Portal");
            defaults.Add("INFORMATION_COUNTRY", "ไทย");
            defaults.Add("APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_REGISTER", "สมัครใหม่");
            defaults.Add("APP_TAX_YEAR", DateTime.Now.Year + 543);
            defaults.Add("APP_RADIO_TYPE", "ค้า");
            defaults.Add("APP_HEALTH_CARE_MANAGER_TYPE", "กิจการสปาเพื่อสุขภาพ");
            defaults.Add("APP_HEALTH_SECTION_TYPE", "กิจการสปาเพื่อสุขภาพ การบริการโดยวิธีการบำบัดด้วยน้ำและการนวดร่างกายเป็นหลักและประกอบด้วยบริการอื่นอีกอย่างน้อย 3 อย่าง");
            defaults.Add("APP_HEALTH_CANCEL_SPECIFICALLY_EXPANT", "กิจการสปาเพื่อสุขภาพ");
            defaults.Add("APP_HEALTH_EDIT_SPECIFICALLY_EXPANT", "กิจการสปาเพื่อสุขภาพ");
            defaults.Add("FACTORY_TYPE2", 2);
            defaults.Add("APP_SPA_FEE_PER_YEAR_SECTION_B_CONTROL_B", "ค่าธรรมเนียมการประกอบกิจการสถานประกอบการ เพื่อสุขภาพรายปี(สปา) ปีละ 1, 000 บาท");
            defaults.Add("APP_CLINIC_NOT_ONE_NIGHT_STAND_B_CONTROL_A",DateTime.Now.Date < new DateTime(DateTime.Now.Year,10,1) ? DateTime.Now.Year + 543 : (DateTime.Now.Year + 1) + 543);
            defaults.Add("APP_CLINIC_OVER_NIGHT_SECTION_B_CONTROL_A",DateTime.Now.Date < new DateTime(DateTime.Now.Year, 10, 1) ? DateTime.Now.Year + 543 : (DateTime.Now.Year + 1) + 543);
            //defaults.Add("SOFTWARE_TOTAL_MODULE", 0);
            if (IdentityType == UserTypeEnum.Citizen)
            {
                defaults.Add("INFORMATION__REQUEST_AS", "INFORMATION__REQUEST_AS_CITIZEN");

                defaults.Add("CITIZEN_FULLNAME_TH", string.Format("{0} {1}", IdentityFirstname, IdentityLastname));
                if (IdentityProvider == UserProviderEnum.NDID.GetEnumStringValue())
                {
                    defaults.Add("CITIZEN_FULLNAME_EN", string.Format("{0} {1}", IdentityFirstnameEN, IdentityLastnameEN));
                    defaults.Add("BIRTH_DATE", CitizenBirthDate);
                }
                defaults.Add("CITIZEN_NAME", IdentityFirstname);
                defaults.Add("CITIZEN_LASTNAME", IdentityLastname);
                defaults.Add("GENERAL_EMAIL", IdentityEmail);
            }
            else if (IdentityType == UserTypeEnum.Juristic)
            {
                var profile = IdentityHelper.GetJuristicProfile(IdentityID);
                defaults.Add("INFORMATION__REQUEST_AS", "INFORMATION__REQUEST_AS_JURISTIC");
                var provinces = GeoService.Provinces(string.Empty);
                var provinceID = IdentityID.Substring(1, 2);
                var regisProvince = provinces.Where(o => o.ID == provinceID).SingleOrDefault();
                if (regisProvince != null)
                {
                    defaults.Add("REGISTER_PROVINCE", new AddressControlData() { Province = regisProvince });
                }

                if (profile != null && profile.HasValues)
                {
                    defaults.Add("COMPANY_NAME_TH", profile["JuristicName_TH"].DefaultString());
                    defaults.Add("COMPANY_NAME_EN", profile["JuristicName_EN"].DefaultString());
                    defaults.Add("GENERAL_INFORMATION__JURISTIC_TYPE", profile["JuristicType"].DefaultString());
                    defaults.Add("JURISTIC_TYPE", profile["JuristicType"].DefaultString());
                    //defaults.Add("FACTORY_TYPE2", 2); // APP_FACTORY_TYPE2                   

                    var capital = profile["RegisterCapital"].DefaultDecimal();
                    if (capital > 0)
                    {
                        defaults.Add("REGISTER_CAPITAL", capital.ToString("#,##0.00"));
                    }
                    var paidCapital = profile["PaidRegisterCapital"].DefaultDecimal();
                    if (paidCapital > 0)
                    {
                        defaults.Add("REGISTER_CAPITAL_PAID", capital.ToString("#,##0.00"));
                    }

                    defaults.Add("REGISTER_DATE", profile["RegisterDate"].DefaultDateTime("yyyy-MM-dd+HH:mm", CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.None));

                    // ดึงข้อมูล Default จาก DBD มาใส่อัตโนมัติ
                    SingleFormRequestEntity model = SingleFormRequestEntity.Get(IdentityID, IdentityType, new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.DRAFT });
                    if (model == null)
                    {
                        model = new SingleFormRequestEntity();
                        model.Create();
                        model.IdentityID = IdentityID;
                        model.IdentityType = IdentityType;
                    }
                    // TODO : ที่อยู่สำนักงานใหญ่
                    var oldBr0Info = model.SectionData.Where(x => x.SectionName == "BRANCH0_ADDRESS_INFORMATION").FirstOrDefault();
                    if (oldBr0Info == null)
                    {
                        oldBr0Info = new SingleFormSectionDataEntity
                        {
                            SectionName = "BRANCH0_ADDRESS_INFORMATION",
                            Type = SectionType.ArrayOfForms,
                            ArrayData = new List<Dictionary<string, object>>()
                        };
                        model.SectionData.Add(oldBr0Info);
                    }

                    if (profile["AddressInformations"] != null && profile["AddressInformations"].Count() > 0)
                    {
                        var address = profile["AddressInformations"][0];

                        // Get Province Id
                        var addrProvince = GeoService.Provinces(address["Province"].DefaultString()).SingleOrDefault();

                        //Get Amphur Id
                        var addrAmphur = GeoService.Amphurs(addrProvince.ID != null ? addrProvince.ID : string.Empty,
                            address["Ampur"].DefaultString()).SingleOrDefault();

                        //Get Tumbol Id
                        var addrTumbol = GeoService.Tambols(addrProvince.ID != null ? addrProvince.ID : string.Empty,
                            addrAmphur.ID != null ? addrAmphur.ID : string.Empty,
                            address["Tumbol"].DefaultString()).FirstOrDefault();

                        // HQ address ในหน้าฟอร์ม (ที่อยู่ตามทะเบียนนิติบุคคล)
                        defaults.Add("JURISTIC_HQ_ADDRESS", new AddressControlData()
                        {
                            Address = address["AddressNo"].DefaultString(),
                            Moo = address["Moo"].DefaultString(),
                            Village = address["VillageName"].DefaultString(),
                            Soi = address["Soi"].DefaultString(),
                            Building = address["Building"].DefaultString(),
                            Floor = address["Floor"].DefaultString(),
                            Road = address["Road"].DefaultString(),
                            Province = addrProvince,
                            Amphur = addrAmphur,
                            Tumbol = addrTumbol,
                            Telephone = address["Phone"].DefaultString()
                        });

                        // Branch 0 ในหน้าฟอร์ม (ที่อยู่สำนักงานใหญ่)
                        if (oldBr0Info != null && oldBr0Info.ArrayData.Count == 0)
                        {
                            var br0FromDBD = new Dictionary<string, object>();
                            br0FromDBD["ARR_IDX"] = "0";
                            br0FromDBD["ADDRESS_JURISTIC_BRANCH0_ADDRESS"] = address["AddressNo"].DefaultString();
                            br0FromDBD["ADDRESS_MOO_JURISTIC_BRANCH0_ADDRESS"] = address["Moo"].DefaultString();
                            br0FromDBD["ADDRESS_VILLAGE_JURISTIC_BRANCH0_ADDRESS"] = address["VillageName"].DefaultString();
                            br0FromDBD["ADDRESS_SOI_JURISTIC_BRANCH0_ADDRESS"] = address["Soi"].DefaultString();
                            br0FromDBD["ADDRESS_BUILDING_JURISTIC_BRANCH0_ADDRESS"] = address["Building"].DefaultString();
                            br0FromDBD["ADDRESS_FLOOR_JURISTIC_BRANCH0_ADDRESS"] = address["Floor"].DefaultString();
                            br0FromDBD["ADDRESS_ROAD_JURISTIC_BRANCH0_ADDRESS"] = address["Road"].DefaultString();
                            br0FromDBD["ADDRESS_PROVINCE_JURISTIC_BRANCH0_ADDRESS"] = addrProvince.ID.DefaultString();
                            br0FromDBD["ADDRESS_PROVINCE_JURISTIC_BRANCH0_ADDRESS_TEXT"] = address["Province"].DefaultString();
                            br0FromDBD["ADDRESS_AMPHUR_JURISTIC_BRANCH0_ADDRESS"] = addrAmphur.ID.DefaultString();
                            br0FromDBD["ADDRESS_AMPHUR_JURISTIC_BRANCH0_ADDRESS_TEXT"] = address["Ampur"].DefaultString();
                            br0FromDBD["ADDRESS_TUMBOL_JURISTIC_BRANCH0_ADDRESS"] = addrTumbol.ID.DefaultString();
                            br0FromDBD["ADDRESS_TUMBOL_JURISTIC_BRANCH0_ADDRESS_TEXT"] = address["Tumbol"].DefaultString();
                            oldBr0Info.ArrayData.Add(br0FromDBD);
                        }
                    }


                    // กรรมการ Test 123
                    var oldCommitteeInfo = model.SectionData.Where(x => x.SectionName == "COMMITTEE_INFORMATION").FirstOrDefault();
                    if (oldCommitteeInfo == null)
                    {
                        oldCommitteeInfo = new SingleFormSectionDataEntity
                        {
                            SectionName = "COMMITTEE_INFORMATION",
                            Type = SectionType.ArrayOfForms,
                            ArrayData = new List<Dictionary<string, object>>()
                        };
                        model.SectionData.Add(oldCommitteeInfo);
                    }

                    Dictionary<string, JToken> committeeFromDBD = new Dictionary<string, JToken>();
                    foreach (var committee in profile["CommitteeInformations"])
                    {
                        string key = committee["FirstName"].DefaultString().Replace("/", "") + " " + committee["LastName"].DefaultString().Replace("/", "");
                        committeeFromDBD.Add(key, committee);
                    }
                    // Remove not existing committee from old committee info
                    oldCommitteeInfo.ArrayData.RemoveAll(x =>
                            !x.ContainsKey("JURISTIC_COMMITTEE_NAME") ||
                            !x.ContainsKey("JURISTIC_COMMITTEE_LASTNAME") ||
                            (!committeeFromDBD.ContainsKey(x["JURISTIC_COMMITTEE_NAME"] + " " + x["JURISTIC_COMMITTEE_LASTNAME"])));
                    foreach (var key in committeeFromDBD.Keys)
                    {
                        var old = oldCommitteeInfo.ArrayData.Where(x => (x["JURISTIC_COMMITTEE_NAME"] + " " + x["JURISTIC_COMMITTEE_LASTNAME"]) == key).FirstOrDefault();
                        if (old == null)
                        {
                            var dataFromDBD = committeeFromDBD[key];
                            var newData = new Dictionary<string, object>();
                            newData["ARR_IDX"] = dataFromDBD["Sequence"].DefaultString();
                            newData["JURISTIC_COMMITTEE_NUMBER"] = dataFromDBD["Sequence"].DefaultString();
                            newData["JURISTIC_COMMITTEE_TITLE"] = dataFromDBD["Title"].DefaultString();
                            newData["JURISTIC_COMMITTEE_NAME"] = dataFromDBD["FirstName"].DefaultString().Replace("/", "");
                            newData["JURISTIC_COMMITTEE_LASTNAME"] = dataFromDBD["LastName"].DefaultString().Replace("/", "");
                            newData["JURISTIC_COMMITTEE_CITIZEN_ID"] = dataFromDBD["CitizenID"].DefaultString();
                            newData["COMMITTEE_INFORMATION_CITIZEN_ID"] = dataFromDBD["CitizenID"].DefaultString();
                            newData["JURISTIC_COMMITTEE_NATIONALITY_OPTION"] = "thai";
                            newData["DROPDOWN_JURISTIC_COMMITTEE_TITLE_TEXT"] = dataFromDBD["Title"].DefaultString();

                            //var titleDropdown = FormSectionRow.optPersonTitle.Where(x => x.Text == dataFromDBD["Title"].DefaultString()).FirstOrDefault();
                            //if (titleDropdown != null)
                            //{
                            //    newData["DROPDOWN_JURISTIC_COMMITTEE_TITLE"] = titleDropdown.ID;
                            //}
                            //else
                            //{
                            //    newData["DROPDOWN_JURISTIC_COMMITTEE_TITLE"] = FormSectionRow.optPersonTitle[0].ID;
                            //}
                            oldCommitteeInfo.ArrayData.Add(newData);
                        }
                    }
                    model.Update();
                }
                else
                {
                    //notifyMsg(NotifyMsgType.Error, "ไม่สามารถเชื่อมต่อฐานข้อมูลกรมพัฒนาธุรกิจการค้าได้ กรุณาลองใหม่อีกครั้ง", !string.IsNullOrEmpty(Request.UrlReferrer.AbsoluteUri) ? Request.UrlReferrer.AbsoluteUri : Url.BizAction("Index", "Home", new { area = "", language = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName }));
                    ViewBag.IsDBDError = true;
                    var backUrl = Url.BizAction("Index", "Home", new { area = "", language = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName });
                    ViewBag.BackUrl = backUrl;
                }
            }

            if (apps.Contains("ขออนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร"))
            {
                defaults.Add("APP_BUILDING_BUILDING_SECTION_DURATION", " ");
            }
            #region [สำหรับกรณีที่มีการ Filter การเลือกจังหวัด]
            var metroUtility = new string[] { AppSystemNameTextConst.APP_MEA, AppSystemNameTextConst.APP_MWA };
            var provinUtility = new string[] { AppSystemNameTextConst.APP_PEA, AppSystemNameTextConst.APP_PWA };
            if (apps.Any(x => metroUtility.Contains(x)))
            {
                defaults.Add("UTILITY_ADDRESS", ProvinceType.Metro.ToString());
            }
            else if (apps.Any(x => provinUtility.Contains(x)))
            {
                defaults.Add("UTILITY_ADDRESS", ProvinceType.Provin.ToString());
            }
            else
            {
                defaults.Add("UTILITY_ADDRESS", ProvinceType.All.ToString());
            }

            var bkkG1 = new string[] { AppSystemNameTextConst.APP_BUILDING_G1 };
            if (apps.Any(x => bkkG1.Contains(x)))
            {
                defaults.Add("APP_BUILDING_G1_CONSTRUCTION_SITE_ADDRESS", ProvinceType.NotBKK.ToString());
            }

            #endregion

            var mapApps = new List<string>() { "APP_MEA", "APP_MWA", "APP_TOT", "APP_PWA" };
            if (mapApps.Contains(group.SectionGroup))
            {
                var dbRequest = MongoFactory.GetSingleFormRequestCollection().AsQueryable();
                var request = dbRequest.Where(o => o.IdentityID == IdentityID).SingleOrDefault();
                if (request != null)
                {
                    var data = request.SectionData.Where(o => o.SectionName == "UTILITY_ADDRESS_INFORMATION").SingleOrDefault();
                    Dictionary<string, string> UtilData = new Dictionary<string, string>();

                    var pCode = data.FormData["ADDRESS_PROVINCE_UTILITY_ADDRESS"].ToString();
                    var aCode = data.FormData["ADDRESS_AMPHUR_UTILITY_ADDRESS"].ToString();
                    var tCode = data.FormData["ADDRESS_TUMBOL_UTILITY_ADDRESS"].ToString();
                    var lat = data.FormData["ADDRESS_LAT_UTILITY_ADDRESS"].ToString();
                    var lng = data.FormData["ADDRESS_LNG_UTILITY_ADDRESS"].ToString();

                    if (!string.IsNullOrEmpty(pCode) && !string.IsNullOrEmpty(aCode) && !string.IsNullOrEmpty(tCode) &&
                        !string.IsNullOrEmpty(lat) && !string.IsNullOrEmpty(lng))
                    {
                        UtilData.Add("pCode", pCode);
                        UtilData.Add("aCode", aCode);
                        UtilData.Add("tCode", tCode);
                        UtilData.Add("lat", lat);
                        UtilData.Add("lng", lng);

                        defaults.Add("UtilData", UtilData);
                    }
                }
            }

            if (apps.Contains(AppSystemNameTextConst.APP_LAW_OFFICE))
            {
                defaults.Add("APP_LAW_OFFICE_INFO_SECTION_REQUEST_DATETIME", DateTime.Now);
            }

            if (apps.Contains(AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW))
            {
                defaults.Add("SOFTWARE_HOUSE_REQ_DATE", DateTime.Now);
            }

            if (apps.Contains(AppSystemNameTextConst.APP_SOFTWARE_HOUSE_EDIT))
            {
                defaults.Add("SOFTWARE_HOUSE_REQ_DATE_EDIT", DateTime.Now);
            }

            #region [ 37 ] APP ELECTRONIC COMMERCIAL

            if (group.SectionGroup == "APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH")
            {
                if (IdentityType == UserTypeEnum.Citizen)
                {
                    defaults.Add("APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION_CITIZEN_ID", IdentityID);
                }
                else if (IdentityType == UserTypeEnum.Juristic)
                {
                    defaults.Add("APP_ELECTRONIC_COMMERCIAL_EDIT_SEARCH_SEARCH_SECTION_JURISTIC_ID", IdentityID);
                }
            }

            if (group.SectionGroup == "APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH")
            {
                if (IdentityType == UserTypeEnum.Citizen)
                {
                    defaults.Add("APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_CITIZEN_ID", IdentityID);
                }
                else if (IdentityType == UserTypeEnum.Juristic)
                {
                    defaults.Add("APP_ELECTRONIC_COMMERCIAL_CANCEL_SEARCH_SEARCH_SECTION_JURISTIC_ID", IdentityID);
                }
            }

            #endregion

            #region [ 38 ] APP ENERGY PRODUCTION

            if (group.SectionGroup == "APP_ENERGY_PRODUCTION_EDIT_SEARCH")
            {
                defaults.Add("APP_ENERGY_PRODUCTION_EDIT_SEARCH_IDENTITY_ID", IdentityID);
            }

            if (group.SectionGroup == "APP_ENERGY_PRODUCTION_RENEW_SEARCH")
            {
                defaults.Add("APP_ENERGY_PRODUCTION_RENEW_SEARCH_IDENTITY_ID", IdentityID);
            }

            if (group.SectionGroup == "APP_ENERGY_PRODUCTION_CANCEL_SEARCH")
            {
                defaults.Add("APP_ENERGY_PRODUCTION_CANCEL_SEARCH_IDENTITY_ID", IdentityID);
            }

            #endregion

            #region [ 39 ] APP ELECTRONIC COMMERCIAL

            if (group.SectionGroup == "APP_TRANSPORT_NON_REGULAR_TRUCK")
            {
                defaults.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REQUEST_BY", IdentityFullName);
                if (IdentityType == UserTypeEnum.Juristic)
                {
                    var profile = IdentityHelper.GetJuristicProfile(IdentityID);
                    var capital = profile["RegisterCapital"].DefaultDecimal();
                    if (capital > 0)
                    {
                        defaults.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REGIS_CAPITAL", capital.ToString("#,##0.00"));
                    }
                    var regisdate = profile["RegisterDate"].DefaultDateTime("yyyy-MM-dd+HH:mm", CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.None);
                    if (regisdate != null)
                    {
                        defaults.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_REGIS_DATE", regisdate);
                    }
                   
                }
            }

            if (group.SectionGroup == "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW")
            {
                defaults.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_MAINTENANCE_SECTION_2_REQUEST_BY", IdentityFullName);
                if (IdentityType == UserTypeEnum.Citizen)
                {
                    //defaults.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_INFO_SECTION_NATIONALITY", IdentityID);
                }
            }

            #endregion

            #region [ 43 ] APP ACCOUNTING SERVICE

            if (group.SectionGroup == "APP_ACCOUNTING_SERVICE_RENEW_SEARCH")
            {
                if (IdentityType == UserTypeEnum.Juristic)
                {
                    defaults.Add("APP_ACCOUNTING_SERVICE_RENEW_SEARCH_SEARCH_SECTION_ID_CARD", IdentityID);
                }
            }
            if (group.SectionGroup == "APP_ACCOUNTING_SERVICE_EDIT_SEARCH")
            {
                if (IdentityType == UserTypeEnum.Juristic)
                {
                    defaults.Add("APP_ACCOUNTING_SERVICE_EDIT_SEARCH_SEARCH_SECTION_JURISTIC_ID", IdentityID);
                }
            }
            if (group.SectionGroup == "APP_ACCOUNTING_SERVICE_CANCEL_SEARCH")
            {
                if (IdentityType == UserTypeEnum.Juristic)
                {
                    defaults.Add("APP_ACCOUNTING_SERVICE_CANCEL_SEARCH_SEARCH_SECTION_JURISTIC_ID", IdentityID);
                }
            }

            #endregion

            #region [ 45 ] APP_FACTORY_CLASS_2
            if (group.SectionGroup == "APP_FACTORY_CLASS_2_NEW_SEARCH")
            {
                var fac_2 = MongoFactory.GetApplicationRequestCollection().AsQueryable()
                    .Where(u => u.Status == ApplicationStatusV2Enum.COMPLETED && u.StatusOther == "DONE"
                    && u.AppSysName == "APP_FACTORY_TYPE2" && u.IdentityID == IdentityID);

                ViewBag.Factory_List = fac_2.Select(o => o.ApplicationRequestNumber);
            }
            #endregion

            #region [ 49.2 ] APP_ORGANIC_PLANT_PRODUCTION_RENEW

            if (group.SectionGroup == "APP_ORGANIC_PLANT_PRODUCTION_RENEW_SEARCH")
            {
                if (IdentityType == UserTypeEnum.Citizen)
                {
                    defaults.Add("APP_ORGANIC_PLANT_PRODUCTION_RENEW_SEARCH_SEARCH_SECTION_CITIZEN_ID", IdentityID);
                }
                else if (IdentityType == UserTypeEnum.Juristic)
                {
                    defaults.Add("APP_ORGANIC_PLANT_PRODUCTION_RENEW_SEARCH_SEARCH_SECTION_JURISTIC_ID", IdentityID);
                }
            }
            #endregion

            #region [ 49.3 ] APP_ORGANIC_PLANT_PRODUCTION_EDIT

            if (group.SectionGroup == "APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH")
            {
                if (IdentityType == UserTypeEnum.Citizen)
                {
                    defaults.Add("APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_CITIZEN_ID", IdentityID);
                }
                else if (IdentityType == UserTypeEnum.Juristic)
                {
                    defaults.Add("APP_ORGANIC_PLANT_PRODUCTION_EDIT_SEARCH_SEARCH_SECTION_JURISTIC_ID", IdentityID);
                }
            }
            #endregion

            #region [ 49.4 ] APP_ORGANIC_PLANT_PRODUCTION_CANCEL

            if (group.SectionGroup == "APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH")
            {
                if (IdentityType == UserTypeEnum.Citizen)
                {
                    defaults.Add("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION_CITIZEN_ID", IdentityID);
                }
                else if (IdentityType == UserTypeEnum.Juristic)
                {
                    defaults.Add("APP_ORGANIC_PLANT_PRODUCTION_CANCEL_SEARCH_SEARCH_SECTION_JURISTIC_ID", IdentityID);
                }
            }
            #endregion

            ViewBag.Defaults = defaults; // Default Data
            //ViewBag.IdentityType = IdentityType;
            ViewBag.EditMode = (edit == 1);
            ViewBag.FromDB = fromDB;
            ViewBag.ArrayData = arrayData;

            return View();
        }

        private bool EvaluateCondition(SingleFormFileConfig.ConditionItem condition, SingleFormRequestEntity req)
        {
            var sectionData = req.SectionData.Where(s => s.SectionName == condition.Data.SectionName).FirstOrDefault();
            if (sectionData != null)
            {
                if (condition.CheckIfSectionExist)
                {
                    if (condition.Data.DataName != null)
                    {
                        return sectionData.FormData.ContainsKey(condition.Data.DataName);
                    }
                    // Check only section
                    return true;
                }
                if (sectionData.FormData.ContainsKey(condition.Data.DataName))
                {
                    if (condition.CheckIfDataExist)
                    {
                        return true;
                    }
                    if (condition.ExpectedValue == ">=300")
                    {
                        int area = 0;
                        if (int.TryParse(sectionData.FormData.TryGetString(condition.Data.DataName, "0"), out area))
                        {
                            return area >= 300;
                        }
                    }
                    return sectionData.FormData.TryGetString(condition.Data.DataName, null) == condition.ExpectedValue;
                }
            }

            return false;
        }

        private bool EvaluateArrayCondition(SingleFormFileConfig.ConditionItem condition, Dictionary<string, object> item)
        {
            if (item != null)
            {
                if (condition.CheckIfSectionExist)
                {
                    if (condition.Data.DataName != null)
                    {
                        return item.ContainsKey(condition.Data.DataName);
                    }
                    // Check only section
                    return true;
                }
                if (item.ContainsKey(condition.Data.DataName))
                {
                    if (condition.CheckIfDataExist)
                    {
                        return true;
                    }
                    return item.TryGetString(condition.Data.DataName, null) == condition.ExpectedValue;
                }
            }

            return false;
        }

        private bool EvaluateDisplayCondition(SingleFormFileConfig.ConditionConfig config, SingleFormRequestEntity req)
        {
            if (config.Condition != null)
            {
                var ret = EvaluateCondition(config.Condition, req);
                return config.Not ? !ret : ret;
            }
            if (config.InnerConditions != null)
            {
                foreach (var innerCond in config.InnerConditions)
                {
                    if (config.IsOr && EvaluateDisplayCondition(innerCond, req))
                    {
                        return config.Not ? false : true;
                    }
                    if (!config.IsOr && !EvaluateDisplayCondition(innerCond, req))
                    {
                        return config.Not ? true : false;
                    }
                }
            }
            return config.Not ? config.IsOr : !config.IsOr;
        }

        private static bool ValidateSectionItemAdvancedCondition(SingleFormRequestEntity request, SingleFormFileConfig.ConditionConfig advancedCond, Dictionary<string, object> sectionItem)
        {
            bool passFilter = true;

            if (advancedCond.Condition != null)
            {
                var data = advancedCond.Condition.Data;
                if (data != null)
                {
                    if (!string.IsNullOrEmpty(data.SectionName))
                    {
                        // Look-up value in other section.
                        var sectionData = request.SectionData.FirstOrDefault(o => o.SectionName == data.SectionName);
                        if (sectionData != null)
                        {
                            passFilter = sectionItem.TryGetString(data.DataName, "") == advancedCond.Condition.ExpectedValue;
                        }
                        else
                        {
                            // Section not found, this condition will be fault.
                            passFilter = false;
                        }
                    }
                    else
                    {
                        passFilter = sectionItem.TryGetString(advancedCond.Condition.Data.DataName, "") == advancedCond.Condition.ExpectedValue;
                    }
                }
            }
            else if (advancedCond.InnerConditions != null)
            {
                bool isPassed = true;
                foreach (var innerCond in advancedCond.InnerConditions)
                {
                    // Recursively validate all inner conditions.
                    bool condResult = ValidateSectionItemAdvancedCondition(request, innerCond, sectionItem);

                    // if IsOr, just match only one condition.
                    if (advancedCond.IsOr && condResult)
                    {
                        isPassed = true;
                        break;
                    }

                    isPassed &= condResult;
                }

                passFilter &= isPassed;
            }

            return !advancedCond.Not && passFilter;
        }

        private static void ParameterizeFileName(SingleFormRequestEntity req, SingleFormFileList file, SingleFormAttachmentViewModel fileView)
        {
            if (file.DisplayFormatSources != null && file.DisplayFormatSources.Length > 0)
            {
                List<string> values = new List<string>();

                for (int i = 0; i < file.DisplayFormatSources.Length; i++)
                {
                    var src = file.DisplayFormatSources[i];
                    var sec = req.SectionData.FirstOrDefault(o => o.SectionName == src.SectionName);
                    if (sec != null) values.Add(sec.FormData.TryGetString(src.DataName, ""));
                }

                fileView.FileNameDisplay = fileView.FileName;
                fileView.FormatValues = values;
            }
        }

        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        [EncryptedActionParameter]
        public async Task<ActionResult> Attachment(Guid trid, bool fromDB = false)
        {
            var dbTrans = MongoFactory.GetSingleFormTransactionCollection();
            var tran = dbTrans.AsQueryable().Where(o => o.TransactionID == trid).Single();

            var req = MongoFactory.GetSingleFormRequestCollection()
                .AsQueryable()
                .Where(o => (o.IdentityID == IdentityID && o.IdentityType == IdentityType)).SingleOrDefault();
            if (req == null)
            {
                req = new SingleFormRequestEntity();
                req.Create();
                var viewModel = new SingleFormRequestViewModel()
                {
                    IdentityID = IdentityID,
                    IdentityType = IdentityType,
                };
                TypeAdapter.Adapt<SingleFormRequestViewModel, SingleFormRequestEntity>(viewModel, req);
                req.Update();
            }

            // Add requested App info
            var tranApps = new Dictionary<string, object>();
            var fileConsumerKeys = new List<string>();

            foreach (var app in tran.Apps)
            {
                var fileConsumeyKey = DB.Applications.Where(o => o.ApplicationSysName == app).Select(o => o.FileOwner).SingleOrDefault();
                if (!string.IsNullOrEmpty(fileConsumeyKey))
                {
                    fileConsumerKeys.Add(fileConsumeyKey);
                }

                tranApps.Add(app, "");
            }

            var dbAppFile = MongoFactory.GetSingleFormAppFileCollection().AsQueryable();
            var dbFileList = MongoFactory.GetSingleFormFileListCollection().AsQueryable();

            tran.Files = dbAppFile.Where(o => tran.Apps.Contains(o.AppSysName)).SelectMany(o => o.Files).Distinct().ToList();

            if (tran.Files == null || tran.Files.Count == 0)
            {
                return RedirectToAction("Confirmation", "SingleForm");
            }

            var fileList = dbFileList.Where(o => tran.Files.Contains(o.FileName)).ToList();
            fileList.Add(dbFileList.Where(o => o.FileName == "FREE_DOC").SingleOrDefault());
            var fileGroups = fileList.Select(o => o.FileGroup).Distinct().ToList();

            req.SectionData.Add(new SingleFormSectionDataEntity
            {
                SectionName = SingleFormDataItem.TransactionAppSectionName,
                FormData = tranApps,
            });

            int countFileMutiple = 0;

            List<SingleFormAttachmentViewModel> model = new List<SingleFormAttachmentViewModel>();
            foreach (var file in fileList)
            {
                if (file.Config != null)
                {
                    bool shouldDisplay = true;
                    bool shouldDisplayArray = false;
                    if (file.Config.DisplayCondition != null)
                    {
                        if (file.Config.ShowMultiple != null &&
                            file.Config.ShowMultiple.BindToSection &&
                            file.Config.DisplayCondition.Condition != null &&
                            file.Config.DisplayCondition.Condition.Data != null &&
                            file.Config.DisplayCondition.Condition.Data.SectionType == SingleFormDataItem.SectionTypeEnum.ArraySection)
                        {
                            // For Multiple(ArrayOfForm)
                            shouldDisplay = true;
                            shouldDisplayArray = true;
                        }
                        else
                        {
                            // Check if Display condition satisfied
                            shouldDisplay = EvaluateDisplayCondition(file.Config.DisplayCondition, req);
                        }
                    }

                    if (shouldDisplay)
                    {
                        if (file.Config.ShowMultiple != null)
                        {
                            if (file.Config.ShowMultiple.BindToSection)
                            {
                                var section = req.SectionData.Where(s => s.SectionName == file.Config.ShowMultiple.SectionName).SingleOrDefault();
                                if (section != null)
                                {
                                    int itemIndex = 0;
                                    foreach (var sectionItem in section.ArrayData)
                                    {
                                        bool passFilter = true;
                                        bool passDisplay = true;
                                        if (shouldDisplayArray)
                                        {
                                            passDisplay = EvaluateArrayCondition(file.Config.DisplayCondition.Condition, sectionItem);
                                        }

                                        if (passDisplay)
                                        {
                                            // First check if AdvancedCondition should be used.
                                            if (file.Config.ShowMultiple.AdvancedCondition != null
                                                && (file.Config.ShowMultiple.AdvancedCondition.Condition != null
                                                    || (file.Config.ShowMultiple.AdvancedCondition.InnerConditions != null && file.Config.ShowMultiple.AdvancedCondition.InnerConditions.Length > 0)))
                                            {
                                                passFilter = ValidateSectionItemAdvancedCondition(req, file.Config.ShowMultiple.AdvancedCondition, sectionItem);
                                            }
                                            // Otherwise check with existing logic.
                                            else if (file.Config.ShowMultiple.FilterDataItem != null)
                                            {
                                                if (sectionItem.TryGetString(file.Config.ShowMultiple.FilterDataItem.DataName, "") != file.Config.ShowMultiple.FilterDataItemText)
                                                {
                                                    passFilter = false;
                                                }
                                            }
                                            if (passFilter)
                                            {
                                                var fileO = file.Adapt<SingleFormAttachmentViewModel>();
                                                fileO.IsOptional = file.Config != null ? file.Config.IsOptional : false;
                                                List<string> values = new List<string>();
                                                foreach (var dataItem in file.Config.ShowMultiple.DataItems)
                                                {
                                                    if (dataItem.Type == SingleFormDataItem.DataItemType.ItemRunningNo)
                                                    {
                                                        values.Add((itemIndex + 1).ToString());
                                                    }
                                                    else
                                                    {
                                                        if (dataItem.BindDataValueToResourceText)
                                                        {
                                                            string textValue = sectionItem.ContainsKey(dataItem.DataName) ?
                                                                ResourceHelper.GetResourceWordWithDefault(dataItem.DataName + "_" + sectionItem[dataItem.DataName].ToString(), "Apps_SingleForm_Filelist", sectionItem[dataItem.DataName].ToString())
                                                                : dataItem.DataName;
                                                            values.Add(textValue);
                                                        }
                                                        else
                                                        {
                                                            values.Add(
                                                                sectionItem.ContainsKey(dataItem.DataName) ?
                                                                sectionItem[dataItem.DataName].ToString() : dataItem.DataName
                                                            );
                                                        }
                                                    }
                                                }
                                                fileO.FileNameDisplay = fileO.FileName;
                                                fileO.FileName += "_" + (sectionItem.ContainsKey("ARR_ITEM_ID") ? sectionItem["ARR_ITEM_ID"] : itemIndex.ToString());
                                                fileO.FormatValues = values;
                                                AddFileConsumerKey(fileO, fileConsumerKeys);
                                                model.Add(fileO);
                                                itemIndex++;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (file.Config.ShowMultiple.AllowMultipleEqualToSectionItem)
                            {

                                int maxNumber = 0 + file.Config.ShowMultiple.AllowMultipleEqualToSectionItemAdjust;
                                var section = req.SectionData.Where(s => s.SectionName == file.Config.ShowMultiple.SectionName).SingleOrDefault();
                                if (section != null)
                                {
                                    maxNumber = section.ArrayData.Count() + file.Config.ShowMultiple.AllowMultipleEqualToSectionItemAdjust;
                                }

                                var fileGroup = tran.UploadedFiles.Where(x => x.Description == file.FileGroup)
                                    .FirstOrDefault();
                                int fileCnt = 0;
                                if (fileGroup != null)
                                {
                                    Regex reg = new Regex(file.FileName + "\\-([0-9]+)");
                                    var uploadedFiles = fileGroup.Files.Where(x => reg.IsMatch(x.FileTypeCode)).ToList();
                                    foreach (var uploadedFile in uploadedFiles)
                                    {
                                        var uf = file.Adapt<SingleFormAttachmentViewModel>();

                                        // Try to parameterize filename with form data.
                                        ParameterizeFileName(req, file, uf);

                                        uf.FileNameDisplay = file.FileName;
                                        uf.FileName = uploadedFile.FileTypeCode;
                                        uf.FileReason = uploadedFile.FileReason;
                                        uf.OriginalFileName = file.FileName;
                                        uf.IsAddedItem = true;
                                        AddFileConsumerKey(uf, fileConsumerKeys);
                                        model.Add(uf);
                                        fileCnt++;
                                    }
                                }

                                countFileMutiple = countFileMutiple + fileCnt;

                                if (fileCnt == 0)
                                {
                                    var uf = file.Adapt<SingleFormAttachmentViewModel>();

                                    // Try to parameterize filename with form data.
                                    ParameterizeFileName(req, file, uf);

                                    uf.FileNameDisplay = file.FileName;
                                    uf.FileName = file.FileName + "-0";
                                    uf.OriginalFileName = file.FileName;
                                    uf.IsAddedItem = true;
                                    AddFileConsumerKey(uf, fileConsumerKeys);
                                    model.Add(uf);

                                    if (file.FileName != "FREE_DOC")
                                    {
                                        countFileMutiple++;
                                    }
                                }

                                var fileO = file.Adapt<SingleFormAttachmentViewModel>();

                                // Try to parameterize filename with form data.
                                ParameterizeFileName(req, file, fileO);

                                fileO.AddItemMin = file.Config.ShowMultiple.AllowMultipleMinItem;
                                fileO.AddItemButton = true;
                                fileO.AddItemBtnText = file.Config.ShowMultiple.AddItemBtnText != null ? file.Config.ShowMultiple.AddItemBtnText : file.FileName;
                                fileO.AddItemMax = maxNumber;
                                AddFileConsumerKey(fileO, fileConsumerKeys);
                                model.Add(fileO);
                            }
                            else
                            {
                                var existFileGroup = tran.UploadedFiles.Where(t => t.Description == file.FileGroup).ToList();
                                if (existFileGroup.Count > 0)
                                {
                                    var existFiles = existFileGroup[0].Files.Where(i => i.FileTypeCode.StartsWith(file.FileName + "_ITEM_")).ToList();
                                    int index = 1;
                                    foreach (var exf in existFiles)
                                    {
                                        SingleFormAttachmentViewModel vm = new SingleFormAttachmentViewModel
                                        {
                                            FileName = exf.FileName,
                                            FileNameDisplay = file.FileName,
                                            FileGroup = file.FileGroup,
                                            FormatValues = new List<string> { index.ToString() },
                                        };
                                        index++;
                                        model.Add(vm);
                                    }
                                }
                                var fileO = file.Adapt<SingleFormAttachmentViewModel>();

                                // Try to parameterize filename with form data.
                                ParameterizeFileName(req, file, fileO);

                                fileO.AddItemButton = true;
                                fileO.IsOptional = file.Config != null ? file.Config.IsOptional : false;
                                AddFileConsumerKey(fileO, fileConsumerKeys);
                                model.Add(fileO);
                            }
                        }
                        else
                        {
                            var fileO = file.Adapt<SingleFormAttachmentViewModel>();

                            // Try to parameterize filename with form data.
                            ParameterizeFileName(req, file, fileO);

                            fileO.IsOptional = file.Config != null ? file.Config.IsOptional : false;
                            AddFileConsumerKey(fileO, fileConsumerKeys);
                            model.Add(fileO);
                        }
                    }
                }
                else
                {
                    var fileO = file.Adapt<SingleFormAttachmentViewModel>();

                    // Try to parameterize filename with form data.
                    ParameterizeFileName(req, file, fileO);

                    AddFileConsumerKey(fileO, fileConsumerKeys);
                    model.Add(fileO);
                }
            }

            //TypeAdapter.Adapt<List<SingleFormFileList>, List<SingleFormAttachmentViewModel>>(fileList, model);
            //model = fileList.Adapt<List<SingleFormAttachmentViewModel>>();

            var predocService = new PredocService();
            foreach (var attach in model)
            {
                //var file = attach;
                if (attach.PreDoc || attach.PreDocApphook)
                {
                    FileMetadataEntity fileMeta = null;
                    var fileGroup = tran.UploadedFiles.Where(x => x.Description == attach.FileGroup).FirstOrDefault();
                    if (fileGroup == null)
                    {
                        fileGroup = new FileGroupEntity
                        {
                            FileGroupID = Guid.NewGuid(),
                            TransactionID = trid,
                            IdentityID = IdentityID,
                            IdentityType = IdentityType,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                            Description = attach.FileGroup,
                            Files = new List<FileMetadataEntity>(),
                        };
                        tran.UploadedFiles.Add(fileGroup);
                    }

                    if (attach.PreDoc)
                    {
                        fileMeta = await predocService.preparePredocForAttachmentScreen(User, attach);
                    }
                    else if (attach.PreDocApphook)
                    {

                        var file = fileGroup.Files.Where(x => x.FileTypeCode == attach.FileName).SingleOrDefault();

                        if (file != null)
                        {
                            // draft
                            fileMeta = file;
                        }
                        else
                        {
                            //ถ้าไม่มีใน Draft ให้ดึงไฟล์จาก service
                            foreach (var appSysName in tran.Apps)
                            {
                                var app = DB.Applications.FirstOrDefault(o => o.ApplicationSysName == appSysName);

                                if (app != null && !string.IsNullOrEmpty(app.AppsHookClassName))
                                {
                                    var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", app.AppsHookClassName).Unwrap();
                                    fileMeta = hook.InvokeFilePreDoc(IdentityID, attach.FileName);
                                    //fileMeta.ApplicationRequestID  = 
                                    fileMeta.Extras = new Dictionary<string, object>()
                                    {
                                        { "OWNER_IDENT_ID", IdentityID }
                                    };

                                    if (fileConsumerKeys != null && fileConsumerKeys.Count > 0)
                                    {
                                        fileMeta.Extras.Add("OWNERS", fileConsumerKeys);
                                    }
                                    else
                                    {
                                        fileMeta.Extras.Add("OWNERS", null);
                                    }
                                }
                            }
                        }
                    }

                    fileGroup.Files.RemoveAll(x => x.FileTypeCode == attach.FileName);
                    if (fileMeta != null)
                    {
                        fileMeta.FileTypeCode = attach.FileName;
                        fileMeta.FileGroupID = fileGroup.FileGroupID;
                        fileMeta.TransactionID = fileGroup.TransactionID;
                        fileMeta.IdentityID = fileGroup.IdentityID;
                        fileMeta.IdentityType = fileGroup.IdentityType;
                        fileGroup.Files.Add(fileMeta);

                        // register file metadata
                        var fdb = MongoFactory.GetFileMetadataCollection();
                        var fdbQuery = MongoFactory.GetFileMetadataCollection().AsQueryable();
                        if (!fdbQuery.Any(e => e.FileID == fileMeta.FileID && e.TransactionID == fileMeta.TransactionID))
                        {
                            fdb.InsertOne(fileMeta);
                        }
                    }
                    dbTrans.Update(tran);
                }
            }
            int fileTotal = model.Where(x => !x.IsOptional && (x.FileNameDisplay != "FREE_DOC" && x.FileName != "FREE_DOC")).Count();
            fileTotal = fileTotal - countFileMutiple;
            if (fileTotal != tran.FileTotal || tran.Step < SingleFormTransaction.STEP_DOC)
            {
                tran.Step = Math.Max(SingleFormTransaction.STEP_DOC, tran.Step);
                tran.FileTotal = fileTotal;
                MongoFactory.GetSingleFormTransactionCollection().Update(tran);
            }
            ViewBag.FileTotal = fileTotal;
            ViewBag.IdentityId = IdentityID;
            ViewBag.fileGroups = fileGroups;
            ViewBag.trid = trid.ToString();
            ViewBag.FromDB = fromDB;
            ViewBag.AppStepTotal = tran.AppStepTotal;
            return View(model);
        }

        private bool CanDownloadJuristicShareHolderList()
        {
            string id = id = IdentityID;
            var doc = Api.Get("/dbd/v2/shareholder/pdf?JuristicID=" + id);
            return doc != null && doc.HasValues;
        }

        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        public ActionResult Confirmation(bool fromDB = false)
        {
            ViewBag.FromDB = fromDB;
            var dbTrans = MongoFactory.GetSingleFormTransactionCollection().AsQueryable();
            var tran = dbTrans.Where(o => o.IdentityID == IdentityID).SingleOrDefault();

            var dbRequest = MongoFactory.GetSingleFormRequestCollection().AsQueryable();
            var request = dbRequest.Where(o => o.IdentityID == IdentityID).SingleOrDefault();

            if (tran == null || request == null)
            {
                return RedirectToAction("List");
            }

            var hasAttach = true;
            if (tran.Files == null || tran.Files.Count == 0)
            {
                hasAttach = false;
            }

            var apps = tran.Apps.ToArray();
            var groups = FormSectionGroup.GetAllSectionGroup(apps);
            groups = groups.Where(o => !o.SearchPage).ToList();

            List<SingleFormAppsViewModel> supportApps = new List<SingleFormAppsViewModel>(); ;
            foreach (var app in apps)
            {
                SingleFormAppsViewModel dbApp = null;
                SingleFormAppsViewModel sApp = new SingleFormAppsViewModel();
                if (IdentityType == UserTypeEnum.Citizen)
                {
                    dbApp = DB.Applications.Where(o => !o.IsDeleted && o.SingleFormEnabled && app == o.ApplicationSysName)
                                .GroupJoin(DB.ApplicationTranslations.Where(o => o.LanguageID == DB.CurrentLanguageID), l => l.ApplicationID, r => r.ApplicationID, (l, r) => new { Application = l, Translation = r.FirstOrDefault() })
                                .GroupJoin(DB.OrganizationTranslations, l => l.Application.OrgCode, r => r.OrgCode, (l, r) => new { Application = l.Application, Translation = l.Translation, OrgTranslation = r.FirstOrDefault() })
                                .Select(o => new SingleFormAppsViewModel()
                                {
                                    ApplicationID = o.Application.ApplicationID,
                                    AppSysName = o.Application.ApplicationSysName,
                                    AppName = o.Translation != null ? o.Translation.ApplicationName : o.Application.ApplicationSysName,
                                    OrganizationName = o.OrgTranslation != null ? o.OrgTranslation.OrgName : o.Application.Organization.OrgSysName,
                                    LogoFileID = o.Application.LogoFileID,
                                    HandbookUrl = o.Application.CitizenHandbookUrl,
                                    ApplicationUrl = o.Application.CitizenApplicationUrl,
                                    OperatingCostType = o.Application.CitizenOperatingCostType,
                                    OperatingCost2 = o.Application.CitizenOperatingCost2,
                                    OperatingCost = o.Application.CitizenOperatingCost,
                                    OperatingDays = o.Application.CitizenOperatingDays,
                                    OperatingDays2 = o.Application.CitizenOperatingDays2,
                                    OperatingDaysType = o.Application.CitizenOperatingDayType,
                                    AppsHookClassName = o.Application.AppsHookClassName,
                                    ShowRemark = o.Application.ShowRemark,
                                    Remark = o.Application.Remark,
                                    CitizenShowRemark = o.Application.CitizenShowRemark,
                                    CitizenRemark = o.Application.CitizenRemark,
                                }).FirstOrDefault();
                }
                else
                {
                    dbApp = DB.Applications.Where(o => !o.IsDeleted && o.SingleFormEnabled && app == o.ApplicationSysName)
                                .GroupJoin(DB.ApplicationTranslations.Where(o => o.LanguageID == DB.CurrentLanguageID), l => l.ApplicationID, r => r.ApplicationID, (l, r) => new { Application = l, Translation = r.FirstOrDefault() })
                                .GroupJoin(DB.OrganizationTranslations, l => l.Application.OrgCode, r => r.OrgCode, (l, r) => new { Application = l.Application, Translation = l.Translation, OrgTranslation = r.FirstOrDefault() })
                                .Select(o => new SingleFormAppsViewModel()
                                {
                                    ApplicationID = o.Application.ApplicationID,
                                    AppSysName = o.Application.ApplicationSysName,
                                    AppName = o.Translation != null ? o.Translation.ApplicationName : o.Application.ApplicationSysName,
                                    OrganizationName = o.OrgTranslation != null ? o.OrgTranslation.OrgName : o.Application.Organization.OrgSysName,
                                    LogoFileID = o.Application.LogoFileID,
                                    HandbookUrl = o.Application.HandbookUrl,
                                    ApplicationUrl = o.Application.ApplicationUrl,
                                    OperatingCostType = o.Application.OperatingCostType,
                                    OperatingCost2 = o.Application.OperatingCost2,
                                    OperatingCost = o.Application.OperatingCost,
                                    OperatingDays = o.Application.OperatingDays,
                                    OperatingDays2 = o.Application.OperatingDays2,
                                    OperatingDaysType = o.Application.OperatingDayType,
                                    AppsHookClassName = o.Application.AppsHookClassName,
                                    ShowRemark = o.Application.ShowRemark,
                                    Remark = o.Application.Remark,
                                    CitizenShowRemark = o.Application.CitizenShowRemark,
                                    CitizenRemark = o.Application.CitizenRemark,
                                }).FirstOrDefault();
                }

                if (dbApp != null)
                {
                    sApp.AppName = dbApp.AppName;
                    sApp.LogoFileID = dbApp.LogoFileID;
                    sApp.OrganizationName = dbApp.OrganizationName;
                    sApp.OperatingDays = dbApp.OperatingDays;
                    sApp.OperatingDaysType = dbApp.OperatingDaysType;
                    sApp.OperatingDays2 = dbApp.OperatingDays2;
                    sApp.ShowRemark = dbApp.ShowRemark;
                    sApp.Remark = dbApp.Remark;
                    sApp.CitizenShowRemark = dbApp.CitizenShowRemark;
                    sApp.CitizenRemark = dbApp.CitizenRemark;

                    if (!string.IsNullOrEmpty(dbApp.AppsHookClassName))
                    {
                        IAppsHook hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", dbApp.AppsHookClassName).Unwrap();
                        if (hook.ShowPermitSummaryInSingleFormConfirmScreen)
                        {
                            var fee = hook.CalculateFee(request.SectionData.Cast<ISectionData>().ToList());
                            if (fee >= 0)
                            {
                                sApp.OperatingCost = fee;
                                sApp.OperatingCostType = "Fixed";
                            }
                            else
                            {
                                sApp.OperatingCost = dbApp.OperatingCost;
                                sApp.OperatingCost2 = dbApp.OperatingCost2;
                                sApp.OperatingCostType = dbApp.OperatingCostType;
                            }
                            supportApps.Add(sApp);
                        }
                    }
                }

            }

            List<FormSection> sections = new List<FormSection>();
            foreach (var group in groups)
            {
                sections.AddRange(FormSection.GetSections(group.SectionGroup, apps, IdentityType).OrderBy(o => o.Ordering));
            }

            List<FormSectionRow> sectionRows = new List<FormSectionRow>();
            foreach (var section in sections)
            {
                sectionRows.AddRange(FormSectionRow.GetSectionRows(section.Section, apps, IdentityType));
            }

            ViewBag.SectionGroups = groups.ToArray();
            ViewBag.Sections = sections.ToArray();
            ViewBag.SectionRows = sectionRows.ToArray();
            ViewBag.Attach = hasAttach;
            ViewBag.AppStepTotal = tran.AppStepTotal;
            ViewBag.trid = tran.TransactionID.ToString();

            Dictionary<string, object> defaults = new Dictionary<string, object>();
            defaults.Add("IDENTITY_ID", IdentityID);
            var filledData = request.SectionData.ToList();
            if (filledData != null && filledData.Count > 0)
            {
                foreach (var sec in filledData)
                {
                    string key = sec.SectionName + "::";
                    foreach (var secDataKey in sec.FormData.Keys)
                    {
                        defaults.Add(key + secDataKey, sec.FormData[secDataKey]);
                    }
                }
            }

            if (IdentityType == UserTypeEnum.Citizen)
            {
                //defaults.Add("CITIZEN_FULLNAME_TH", IdentityFullName);
                defaults.Add("CITIZEN_FULLNAME_TH", string.Format("{0} {1}", IdentityFirstname, IdentityLastname));
            }
            else if (IdentityType == UserTypeEnum.Juristic)
            {
                var profile = IdentityHelper.GetJuristicProfile(IdentityID);
                var provinces = GeoService.Provinces(string.Empty);
                var provinceID = IdentityID.Substring(1, 2);
                var regisProvince = provinces.Where(o => o.ID == provinceID).SingleOrDefault();
                if (regisProvince != null)
                {
                    defaults.Add("REGISTER_PROVINCE", new AddressControlData() { Province = regisProvince });
                    //defaults.Add("APP_HEALTH_REQUEST_PLACE", regisProvince);
                }

                if (profile != null && profile.HasValues)
                {
                    defaults.Add("COMPANY_NAME_TH", profile["JuristicName_TH"].DefaultString());
                    defaults.Add("COMPANY_NAME_EN", profile["JuristicName_EN"].DefaultString());
                    var capital = profile["RegisterCapital"].DefaultDecimal();
                    if (capital > 0)
                    {
                        defaults.Add("REGISTER_CAPITAL", capital.ToString("#,##0.00"));
                    }
                    var paidCapital = profile["PaidRegisterCapital"].DefaultDecimal();
                    if (paidCapital > 0)
                    {
                        defaults.Add("REGISTER_CAPITAL_PAID", capital.ToString("#,##0.00"));
                    }
                    defaults.Add("REGISTER_DATE", profile["RegisterDate"].DefaultDateTime("yyyy-MM-dd+HH:mm", CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.None));

                    if (profile["AddressInformations"] != null && profile["AddressInformations"].Count() > 0)
                    {
                        var address = profile["AddressInformations"][0];

                        // Get Province Id
                        var addrProvince = GeoService.Provinces(address["Province"].DefaultString()).SingleOrDefault();

                        //Get Amphur Id
                        var addrAmphur = GeoService.Amphurs(addrProvince.ID != null ? addrProvince.ID : string.Empty,
                            address["Ampur"].DefaultString()).SingleOrDefault();

                        //Get Tumbol Id
                        var addrTumbol = GeoService.Tambols(addrProvince.ID != null ? addrProvince.ID : string.Empty,
                            addrAmphur.ID != null ? addrAmphur.ID : string.Empty,
                            address["Tumbol"].DefaultString()).FirstOrDefault();

                        defaults.Add("JURISTIC_HQ_ADDRESS", new AddressControlData()
                        {
                            Address = address["AddressNo"].DefaultString(),
                            Moo = address["Moo"].DefaultString(),
                            Village = address["VillageName"].DefaultString(),
                            Soi = address["Soi"].DefaultString(),
                            Building = address["Building"].DefaultString(),
                            Floor = address["Floor"].DefaultString(),
                            Road = address["Road"].DefaultString(),
                            Province = addrProvince,
                            Amphur = addrAmphur,
                            Tumbol = addrTumbol,
                            Telephone = address["Phone"].DefaultString()
                        });
                    }
                }
            }

            var mapApps = new List<string>() { "MEA_INFORMATION", "MWA_INFORMATION", "TOT_INFORMATION", "PWA_INFORMATION" };
            var mapData = request.SectionData.Where(o => mapApps.Contains(o.SectionName)).ToList();
            if (mapData != null && mapData.Count > 0)
            {
                foreach (var sec in mapData)
                {
                    if (sec.SectionName == "MEA_INFORMATION" && sec.FormData.ContainsKey("MEA_BRANCH_TH"))
                    {
                        defaults.Add("MEA_BRANCH", sec.FormData["MEA_BRANCH_TH"]);
                    }
                    else if (sec.SectionName == "MWA_INFORMATION" && sec.FormData.ContainsKey("MWA_BRANCH_TEXT"))
                    {
                        defaults.Add("MWA_BRANCH", sec.FormData["MWA_BRANCH_TEXT"]);
                    }
                    else if (sec.SectionName == "TOT_INFORMATION" && sec.FormData.ContainsKey("TOT_1ST_BRANCH_TEXT") && sec.FormData.ContainsKey("TOT_2ND_BRANCH_TEXT"))
                    {
                        defaults.Add("TOT_BRANCH", new List<string>() { sec.FormData["TOT_1ST_BRANCH_TEXT"].ToString(), sec.FormData["TOT_2ND_BRANCH_TEXT"].ToString() });
                    }
                    else if (sec.SectionName == "PWA_INFORMATION" && sec.FormData.ContainsKey("PWA_BRANCH_TEXT"))
                    {
                        defaults.Add("PWA_BRANCH", sec.FormData["PWA_BRANCH_TEXT"]);
                    }
                }
            }
            ViewBag.Defaults = defaults;
            if (tran.Step < SingleFormTransaction.STEP_CONFIRM)
            {
                tran.Step = Math.Max(SingleFormTransaction.STEP_CONFIRM, tran.Step);
                MongoFactory.GetSingleFormTransactionCollection().Update(tran);
            }
            return View(supportApps.ToArray());
        }

        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        public ActionResult Finish(Guid? batchID)
        {
            if (!batchID.HasValue)
            {
                return Redirect(Url.BizAction("Index", "Home", new { area = "" }));
            }
            var appBatchList = MongoFactory.GetApplicationRequestCollection().AsQueryable()
                .Where(r => r.RequestBatchID == batchID && r.IdentityID == IdentityID).ToList();
            ViewBag.AppBatchList = appBatchList;
            var appIDList = appBatchList.Select(x => x.ApplicationID).ToList();
            var appNameList = DB.Applications.Where(o => !o.IsDeleted && appIDList.Contains(o.ApplicationID))
                                .GroupJoin(DB.ApplicationTranslations.Where(o => o.LanguageID == DB.CurrentLanguageID), l => l.ApplicationID, r => r.ApplicationID, (l, r) => new { Application = l, Translation = r.FirstOrDefault() })
                                .Select(o => new
                                {
                                    ApplicationID = o.Application.ApplicationID,
                                    AppName = o.Translation != null ? o.Translation.ApplicationName : o.Application.ApplicationSysName,
                                }).ToList();
            foreach (var app in appBatchList)
            {
                app.ApplicationName = appNameList.Where(x => x.ApplicationID == app.ApplicationID).First().AppName;
            }
            var user = DB.Users.Where(w => w.JuristicID == IdentityID || w.CitizenID == IdentityID).FirstOrDefault();
            if (user != null)
            {
                ViewBag.Email = user.Email;
            }
            else
            {
                ViewBag.Email = null;
            }
            return View(batchID);
        }

        public static ConfirmationFormJuristicPDFModel GetConfirmationFormModel(Guid batchID, UserTypeEnum IdentityType, ApplicationDbContext DB)
        {
            var appRequests = MongoFactory.GetApplicationRequestCollection().AsQueryable()
                .Where(r => r.RequestBatchID == batchID).ToList();

            ConfirmationFormJuristicPDFModel model = new ConfirmationFormJuristicPDFModel();

            model.PreferFileName = "ใบรับคำขอ_" + model.RequestDateTime.ToString("yyyyMMdd_HHmmss") + ".pdf";
            model.RequestDateTime = appRequests[0].CreatedDate;
            model.IsNormal = IdentityType == UserTypeEnum.Citizen;
            model.IPAddress = appRequests[0].SourceIPAddress;

            if (IdentityType == UserTypeEnum.Juristic)
            {
                model.CompanyName = appRequests[0].IdentityName;
                model.CompanyID = appRequests[0].IdentityID;
                model.CompanyVatID = appRequests[0].IdentityID;
                if (appRequests[0].Data.ContainsKey("REQUESTOR_INFORMATION"))
                {
                    var requestorInfo = appRequests[0].Data["REQUESTOR_INFORMATION"];
                    model.RequestorName = requestorInfo.Data["DROPDOWN_REQUESTOR_INFORMATION_TITLE_TEXT_0"] + " " +
                        requestorInfo.Data["REQUESTOR_INFORMATION_NAME_0"] + " " + requestorInfo.Data["REQUESTOR_INFORMATION_LASTNAME_0"];
                }
                else
                {
                    model.RequestorName = "-";
                }
            }
            else
            {
                model.CompanyVatID = appRequests[0].IdentityID;// appRequests[0].Data["GENERAL_INFORMATION"].Data["IDENTITY_ID"];
                if (appRequests[0].Data["GENERAL_INFORMATION"].Data.ContainsKey("CITIZEN_FULLNAME_TH"))
                {
                    model.RequestorName = appRequests[0].Data["GENERAL_INFORMATION"].Data["CITIZEN_FULLNAME_TH"];
                }
                else
                {
                    var genSec = appRequests[0].Data["GENERAL_INFORMATION"].Data;
                    var fullName = genSec["CITIZEN_NAME"] + " " + genSec["CITIZEN_LASTNAME"];
                    if (genSec.ContainsKey("DROPDOWN_CITIZEN_TITLE_TEXT"))
                    {
                        fullName = genSec["DROPDOWN_CITIZEN_TITLE_TEXT"] + " " + fullName;
                    }
                    model.RequestorName = fullName;
                }
            }

            model.Requests = new List<ConfirmationFormJuristicPDFModel.ApplicationRequest>();
            foreach (var req in appRequests)
            {
                ConfirmationFormJuristicPDFModel.ApplicationRequest d1 = new ConfirmationFormJuristicPDFModel.ApplicationRequest();
                d1.RequestID = req.ApplicationRequestNumber;
                var app = DB.Applications.Where(o => !o.IsDeleted && o.ApplicationID == req.ApplicationID)
                                .GroupJoin(DB.ApplicationTranslations.Where(o => o.LanguageID == DB.CurrentLanguageID), l => l.ApplicationID, r => r.ApplicationID, (l, r) => new { Application = l, Translation = r.FirstOrDefault() })
                                .GroupJoin(DB.OrganizationTranslations, l => l.Application.OrgCode, r => r.OrgCode, (l, r) => new { Application = l.Application, Translation = l.Translation, OrgTranslation = r.FirstOrDefault() })
                                .Select(o => new
                                {
                                    AppName = o.Translation != null ? o.Translation.ApplicationName : o.Application.ApplicationSysName,
                                    OrganizationName = o.OrgTranslation != null ? o.OrgTranslation.OrgName : o.Application.Organization.OrgSysName,

                                }).FirstOrDefault();
                d1.ApplicationName = app.AppName;
                d1.OrgName = app.OrganizationName;
                d1.Duration = req.Duration;
                d1.RequestStatus = req.Status;
                model.Requests.Add(d1);
            }
            return model;
        }

        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        public ActionResult FinishNote(Guid batchID)
        {
            var model = GetConfirmationFormModel(batchID, IdentityType, DB);
            var b = iTextReportHelper.GetConfirmationFormJuristicReport(model);
            //Response.AddHeader("content-disposition", "attachment;filename=" + model.PreferFileName);
            return File(b, "application/pdf");
        }
    }

    public class PermitServiceType
    {
        
        public string AppSysName { get; set; }

        public ProvinceType ProvinceService { get; set; }

    }

}