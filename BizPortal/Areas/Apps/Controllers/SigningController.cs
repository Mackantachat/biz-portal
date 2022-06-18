using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Annotations;
using BizPortal.ViewModels.Apps.EDocument;
using EGA.EGA_Development.Util;
using Microsoft.Ajax.Utilities;
using SharpRaven.Data.Context;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media.Animation;

namespace BizPortal.Areas.Apps.Controllers
{
    public class SigningController : AppsControllerBase
    {
        // GET: Apps/Signing
        [Authorize(Roles = ConfigurationValues.ROLES_DOCUMENT_SIGNER_NAME)]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignedDocument(SignedDocumentResponse model)
        {
            var dtNow = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            // TODO: do smth to model
            var document = EDocumentEntity.Get(model.DocumentID);
            if (document != null)
            {
                var me = document.PersonalSigners.Find(o => o.IdentityID == IdentityID);
                var status = PersonalSigningStatus.NEW;
                switch (model.Status)
                {
                    case "SIGNED":
                        status = PersonalSigningStatus.SIGNED;
                        me.SignatureID = model.SignatureID;
                        break;
                    case "REJECTED":
                        status = PersonalSigningStatus.REJECTED;
                        me.PersonalSigningRemark = model.Message;
                        break;
                    case "FAILED":
                        status = PersonalSigningStatus.FAILED;
                        break;
                    default:
                        break;
                }

                me.PersonalSigningStatus = status;
                me.PersonalSigningRemark = model.Message;
                me.SignedDate = dtNow;

                if (document.PersonalSigners.All(o => o.PersonalSigningStatus == PersonalSigningStatus.NEW))
                {
                    document.SigningStatus = EDocumentStatus.NEW;
                }
                else if (document.PersonalSigners.All(o => o.PersonalSigningStatus == PersonalSigningStatus.SIGNED))
                {
                    document.SigningStatus = EDocumentStatus.COMPLETED;
                }
                else if (document.PersonalSigners.Any(o => o.PersonalSigningStatus == PersonalSigningStatus.REJECTED))
                {
                    document.SigningStatus = EDocumentStatus.REJECTED;
                }
                else if (document.PersonalSigners.Any(o => o.PersonalSigningStatus == PersonalSigningStatus.SIGNED))
                {
                    document.SigningStatus = EDocumentStatus.PENDING;
                }

                document.UpdatedDate = dtNow;
                document.Update();
            }

            return RedirectToAction("Index");
        }
    }
}