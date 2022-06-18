using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BizPortal.Models;
using BizPortal.ViewModels;
using EGA.Owin.Security.Utils.Extensions;
using EGA.Owin.Security.EGAOpenID;
using BizPortal.Integrated;
using System.Configuration;
using System.Collections.Generic;
using EGA.Owin.Security.EGAOAuth;
using BizPortal.Utils.Extensions;
using EGA.Owin.Security.EGAOAuth.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BizPortal.Areas.Apps.Controllers
{
    [AllowAnonymous]
    public class AccountController : BizPortal.Controllers.AccountController
    {
        public ActionResult ChooseType(string showMsg = null, string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(showMsg))
            {
                string message = string.Empty;
                if (showMsg == ShowMsgType.Unauthorized.GetEnumStringValue())
                {
                    message = BizPortal.Resources.Global.MSG_WARNING_UNAUTHORIZED;
                    notifyMsg(NotifyMsgType.Warning, message, returnUrl);
                }
                else if (showMsg == ShowMsgType.InvalidAccount.GetEnumStringValue())
                {
                    message = BizPortal.Resources.Global.MSG_WARNING_INVALID_ACCOUNT;
                    notifyMsg(NotifyMsgType.Warning, message, returnUrl);
                }
                else if (showMsg == ShowMsgType.ProfileUpdated.GetEnumStringValue())
                {
                    message = BizPortal.Resources.Global.MSG_SUCCESS_PROFILE_UPDATED;
                    notifyMsg(NotifyMsgType.Success, message, returnUrl);
                }
                else if (showMsg == ShowMsgType.FailedToUpdate.GetEnumStringValue())
                {
                    message = BizPortal.Resources.Global.MSG_ERROR_FAILED_TO_UPDATE;
                    notifyMsg(NotifyMsgType.Error, message, returnUrl);
                }
                else if (showMsg == ShowMsgType.ApplicationNotFound.GetEnumStringValue())
                {
                    message = BizPortal.Resources.Global.MSG_ERROR_APPLICATION_NOT_FOUND;
                    notifyMsg(NotifyMsgType.Error, message, returnUrl);
                }

                return RedirectToAction("ChooseType");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult UpdateProfile(string usertype)
        {
            if (usertype=="Citizen")
                ViewBag.profileUrl = ConfigurationManager.AppSettings["CitizenOpenIDUrl"]+ usertype+ "/Account/Edit";
            else
                ViewBag.profileUrl = ConfigurationManager.AppSettings["OpenIDUrl"] + "Business/EditJuristicContact";
            

            return View();
        }

    }
}