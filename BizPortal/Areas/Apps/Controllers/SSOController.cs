using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using EGA.Owin.Security.EGAOAuth.Models;
using System.Xml.Serialization;
using EGA.WS;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using EGA.EGA_Development.Util;
using BizPortal.Utils;
using EGA.Owin.Security.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Annotations;

namespace BizPortal.Areas.Apps.Controllers
{
    [AuthorizeUser(OpenIDUserType = "JuristicPerson")]
    public class SSOController : AppsControllerBase
    {
        #region[EmployerRegister]
        public ActionResult EmployerRegis()
        {
            int appID = DB.Applications.Where(e => e.ApplicationSysName == "การขอขึ้นทะเบียนนายจ้างและผู้ประกันตน").Select(e => e.ApplicationID).SingleOrDefault();
            var list = default(EmployerRegisFileType).ToEnumList();

            ApplicationRequestEntity request = ApplicationRequestEntity.Get(appID, IdentityID, IdentityType,
                new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.DRAFT, ApplicationStatusV2Enum.CHECK, ApplicationStatusV2Enum.PENDING, ApplicationStatusV2Enum.WAITING, ApplicationStatusV2Enum.COMPLETED });

            string msg = null;

            if (request == null)
            {
                msg = null;
            }
            else
            {
                switch (request.Status)
                {
                    case ApplicationStatusV2Enum.CHECK:
                    case ApplicationStatusV2Enum.PENDING:
                        msg = Resources.Apps_EmployerRegis.MSG_PENDING_REQ;
                        break;
                    case ApplicationStatusV2Enum.WAITING:
                        msg = Resources.Apps_EmployerRegis.MSG_WAITING_REQ;
                        break;
                    case ApplicationStatusV2Enum.COMPLETED:
                        msg = Resources.Apps_EmployerRegis.MSG_COMPLETED_REQ;
                        break;
                    case ApplicationStatusV2Enum.DRAFT:
                    case ApplicationStatusV2Enum.REJECTED:
                        break;
                    default:
                        msg = Resources.Apps_EmployerRegis.MSG_UNKNOWN_STATUS;
                        break;
                }
            }


            var documents = list.Where(e => e.Group.Split(',').Contains(EmployerRegisUserType.Coperate.GetEnumStringValue())).ToList();

            foreach (var doc in documents)
            {
                doc.Description = ResourceHelper.GetResourceWord(string.Format("APPS_DOC_{0}", doc.Name), "Apps_EmployerRegis");
                doc.Text = ResourceHelper.GetResourceWord(string.Format("APPS_DOC_{0}_DESC", doc.Name), "Apps_EmployerRegis");
            }

            ViewBag.CoperateFileList = documents;
            ViewBag.NotifyMsg = msg;
            ViewBag.AppID = appID;
            ViewBag.JuristicID = IdentityID;

            return View();
        }

        public ActionResult EmployerRegisForm(bool dl = false, bool denied = false)
        {
            string oAuthData = User.Identity.GetClaimValueOrDefault(EGA.Owin.Security.EGAOAuth.EGAOAuthAuthenticationClaimType.OAuthDataXml);
            if (!dl && !denied && string.IsNullOrEmpty(oAuthData))
            {
                var denyCallbackUrl = ServerHelper.ResolveServerUrl("~" + Url.Action("EmployerRegisForm", "SSO", new { area = "Apps", language = CurrentCulture, denied = true }));
                return GetOAuthData("", @Url.Action("EmployerRegisForm", "SSO", new { area = "Apps", language = CurrentCulture }), denyCallbackUrl);
            }

            if (dl)
            {
                Dictionary<string, string> memberDict = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(oAuthData))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Member), string.Empty);
                    Member member = (Member)serializer.Deserialize(new StringReader(oAuthData));
                    memberDict.Add("comname01", member.Name);
                    memberDict.Add("num01", member.Address.HouseNumber);
                    memberDict.Add("moo01", member.Address.Moo);
                    memberDict.Add("soi01", member.Address.Soi);
                    memberDict.Add("road01", member.Address.Road);
                    memberDict.Add("tambon01", member.Address.SubDistrict);
                    memberDict.Add("amphor01", member.Address.District);
                    memberDict.Add("province", member.Address.Province);
                    memberDict.Add("zipcode01", member.Address.PostCode);
                    memberDict.Add("tel01", member.ContactInfo.Telephone);

                    int recordNumber = 1;
                    for (int i = 0; i < member.Committees.Length; i++)
                    {
                        if (i > 2)
                            break; // ใส่ได้มากสุด 4 รายชื่อ

                        Committee c = member.Committees[i];
                        memberDict.Add(string.Format("name{0}", recordNumber.ToString().PadLeft(2, '0')), string.Format("{0}{1} {2}", c.Title, c.FirstName, c.LastName));
                        memberDict.Add(string.Format("id{0}", recordNumber.ToString().PadLeft(2, '0')), c.Identifier);
                        recordNumber++;
                    }

                    memberDict.Add("name04", member.Name);
                    memberDict.Add("number01", member.Address.HouseNumber);
                    memberDict.Add("moo02", member.Address.Moo);
                    memberDict.Add("soi02", member.Address.Soi);
                    memberDict.Add("road02", member.Address.Road);
                    memberDict.Add("tambon02", member.Address.SubDistrict);
                    memberDict.Add("amphor02", member.Address.District);
                    memberDict.Add("province02", member.Address.Province);
                    memberDict.Add("zip02", member.Address.PostCode);
                    memberDict.Add("tel05", member.ContactInfo.Telephone);
                }

                EGAWSAPI api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);

                string json = JsonConvert.SerializeObject(memberDict);
                JObject result = api.Call("/form/render/pdf?FileID=BKylqh9gqe8Hf", HttpVerb.POST, null, json);
                if (result != null)
                {
                    string base64URL = XConvert.FromBase64urlStringToBase64String(result["Result"].ToString());
                    byte[] base64Byte = XConvert.FromBase64urlStringToByte(base64URL);
                    MemoryStream ms = new MemoryStream(base64Byte);
                    return File(ms, "application/pdf", "EmployerRegisForm.pdf");
                }
            }

            return View();
        }
        #endregion

        #region[EmployeeRegister]
        public ActionResult EmployeeRegis(string test)
        {
            int appID = 2;
            var list = default(EmployeeRegisFileType).ToEnumList();
            foreach (var doc in list)
            {
                doc.Description = ResourceHelper.GetResourceWord(string.Format("APPS_DOC_{0}", doc.Name), "Apps_EmployeeRegis");
                doc.Text = ResourceHelper.GetResourceWord(string.Format("APPS_DOC_{0}_DESC", doc.Name), "Apps_EmployeeRegis");
            }

            ApplicationRequestEntity request =
                ApplicationRequestEntity.Get(appID, IdentityID, IdentityType,
                new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.WAITING, ApplicationStatusV2Enum.CHECK, ApplicationStatusV2Enum.PENDING }); //, ApplicationStatusV2Enum.COMPLETED });

            string msg = null;

            if (request == null)
            {
                msg = null;
            }
            else
            {
                switch (request.Status)
                {
                    case ApplicationStatusV2Enum.PENDING:
                    case ApplicationStatusV2Enum.CHECK:
                    case ApplicationStatusV2Enum.WAITING:
                        msg = Resources.Apps_EmployeeRegis.MSG_PENDING_REQ;
                        break;
                    case ApplicationStatusV2Enum.COMPLETED:
                        //msg = Resources.Apps_EmployeeRegis.MSG_COMPLETED_REQ;
                        //break;
                    case ApplicationStatusV2Enum.DRAFT:
                    case ApplicationStatusV2Enum.REJECTED:
                        break;
                    default:
                        msg = Resources.Apps_EmployeeRegis.MSG_UNKNOWN_STATUS;
                        break;
                }
            }

            ViewBag.NotifyMsg = msg;
            ViewBag.FormFileList = list.Where(e => e.Group.Split(',').Contains("Form")).ToList();
            ViewBag.EmployeeThaiFileList = list.Where(e => e.Group.Split(',').Contains("Employee") && e.Group.Split(',').Contains("Thai")).ToList();
            ViewBag.EmployeeForeignerFileList = list.Where(e => e.Group.Split(',').Contains("Employee") && e.Group.Split(',').Contains("Foreigner")).ToList();
            ViewBag.AppID = appID;
            ViewBag.JuristicID = IdentityID;

            return View();
        }

        public ActionResult EmployeeRegisForm(bool dl = false, bool denied = false, int page = 0)
        {
            string oAuthData = User.Identity.GetClaimValueOrDefault(EGA.Owin.Security.EGAOAuth.EGAOAuthAuthenticationClaimType.OAuthDataXml);
            if (!dl && !denied && string.IsNullOrEmpty(oAuthData))
            {
                if (page > 0)
                {
                    var denyCallbackUrl = ServerHelper.ResolveServerUrl("~" + Url.Action("EmployeeRegisForm", "SSO", new { area = "Apps", language = CurrentCulture, denied = true, page = page }));
                    return GetOAuthData("", Url.Action("EmployeeRegisForm", "SSO", new { area = "Apps", language = CurrentCulture, page = page }), denyCallbackUrl);
                }
                else
                {
                    var denyCallbackUrl = ServerHelper.ResolveServerUrl("~" + Url.Action("EmployeeRegisForm", "SSO", new { area = "Apps", language = CurrentCulture, denied = true }));
                    return GetOAuthData("", Url.Action("EmployeeRegisForm", "SSO", new { area = "Apps", language = CurrentCulture }), denyCallbackUrl);
                }
            }

            if (dl)
            {
                Dictionary<string, string> memberDict = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(oAuthData))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Member), string.Empty);
                    Member member = (Member)serializer.Deserialize(new StringReader(oAuthData));
                    memberDict.Add("Text1", member.Name);
                    memberDict.Add("Text17", member.Address.HouseNumber);
                    memberDict.Add("Text18", member.Address.Moo);
                    memberDict.Add("Text19", member.Address.Soi);
                    memberDict.Add("Text20", member.Address.Road);
                    memberDict.Add("Text21", member.Address.SubDistrict);
                    memberDict.Add("Text22", member.Address.District);
                    memberDict.Add("Text23", member.Address.Province);
                    memberDict.Add("Text24", member.Address.PostCode);
                    memberDict.Add("Text25", member.ContactInfo.Telephone);
                    if (page > 0)
                        memberDict.Add("Text26", page.ToString());
                }

                EGAWSAPI api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);

                string json = JsonConvert.SerializeObject(memberDict);
                JObject result = api.Call("/form/render/pdf?FileID=BKyc6k3FozlEh", HttpVerb.POST, null, json);
                if (result != null)
                {
                    string base64URL = XConvert.FromBase64urlStringToBase64String(result["Result"].ToString());
                    byte[] base64Byte = XConvert.FromBase64urlStringToByte(base64URL);
                    MemoryStream ms = new MemoryStream(base64Byte);
                    return File(ms, "application/pdf", "EmployeeRegisForm.pdf");
                }
            }

            ViewBag.Page = page;
            return View();
        }

        public ActionResult EmployeeRegis103Form(bool dl = false, bool denied = false)
        {
            string oAuthData = User.Identity.GetClaimValueOrDefault(EGA.Owin.Security.EGAOAuth.EGAOAuthAuthenticationClaimType.OAuthDataXml);
            if (!dl && !denied && string.IsNullOrEmpty(oAuthData))
            {
                var denyCallbackUrl = ServerHelper.ResolveServerUrl("~" + Url.Action("EmployeeRegis103Form", "SSO", new { area = "Apps", language = CurrentCulture, denied = true }));
                return GetOAuthData("", Url.Action("EmployeeRegis103Form", "SSO", new { area = "Apps", language = CurrentCulture }), denyCallbackUrl);
            }

            if (dl)
            {
                Dictionary<string, string> memberDict = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(oAuthData))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Member), string.Empty);
                    Member member = (Member)serializer.Deserialize(new StringReader(oAuthData));
                    memberDict.Add("CompanyName01", member.Name);
                }

                EGAWSAPI api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);

                string json = JsonConvert.SerializeObject(memberDict);
                JObject result = api.Call("/form/render/pdf?FileID=BKylHU6jGDbQM", HttpVerb.POST, null, json);
                if (result != null)
                {
                    string base64URL = XConvert.FromBase64urlStringToBase64String(result["Result"].ToString());
                    byte[] base64Byte = XConvert.FromBase64urlStringToByte(base64URL);
                    MemoryStream ms = new MemoryStream(base64Byte);
                    return File(ms, "application/pdf", "sso103form.pdf");
                }
            }

            return View();
        }
        #endregion

    }
}