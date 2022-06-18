using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BizPortal.Utils;
using BizPortal.Models;
using BizPortal.Utils.Helpers;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BizPortal.ViewModels;
using BizPortal.Utils.Annotations;

namespace BizPortal.Areas.Apps.Controllers
{
    [AuthorizeUser]
    public class FileController : AppsControllerBase
    {
        [HttpPost]
        public ActionResult PLUpload(int appId, string type, string name, int? requestType = null, string browseId ="")
        {
            if (!requestType.HasValue)
            {
                requestType = (int)FileRequestTypeEnum.Request;
            }

            Guid? consumerKey = DB.Applications.Where(e => e.ApplicationID == appId && e.IsDeleted == false).Select(e => e.ConsumerKey).SingleOrDefault();
            if (!consumerKey.HasValue)
            {
                return HttpNotFound("Application Id not found");
            }

            string uploadFolder = Server.MapPath("~/Uploads") + "\\" + "appFiles" + "\\" + SecurityHelper.MD5EncryptToHex(appId.ToString());
            string uploadABSPath = "~/Uploads/appFiles/" + SecurityHelper.MD5EncryptToHex(appId.ToString());

            PLUpload plu = new PLUpload(System.Web.HttpContext.Current, uploadFolder, uploadABSPath);
            JObject result = plu.ProcessUploadToEgaWs(consumerKey.ToString(), IdentityID, type, name, requestType.Value, browseId);

            if (result != null)
            {
                return Json(result.ToObject<FileMetadata>(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetFile(int appId, string fileId)
        {
            try
            {
                Guid? consumerKey = DB.Applications.Where(e => e.ApplicationID == appId && e.IsDeleted == false).Select(e => e.ConsumerKey).SingleOrDefault();
                if (!consumerKey.HasValue)
                {
                    return HttpNotFound("ApplicationId not found");
                }

                JObject result = EgaContentStore.Get(consumerKey.ToString(), fileId);
                if (result != null)
                {
                    return Json(result.ToObject<FileMetadata>(), JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                // TODO: error handler
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFiles(int appId, int juristicRequestId)
        {
            try
            {
                Guid? consumerKey = DB.Applications.Where(e => e.ApplicationID == appId && e.IsDeleted == false).Select(e => e.ConsumerKey).SingleOrDefault();
                if (!consumerKey.HasValue)
                {
                    return HttpNotFound("ApplicationId not found");
                }

                JObject result = EgaContentStore.GetAllOfRequest(consumerKey.ToString(), juristicRequestId, FileStatus.InUse);

                if (result != null)
                {
                    if (result["List"] != null)
                    {
                        FileMetadata[] files = result["List"].ToObject<FileMetadata[]>();
                        var groupedFile = files.GroupBy(o => o.FileDescription)
                            .Select(o => new
                            {
                                RefName = o.Key,
                                Files = o.ToArray()
                            }).ToArray();
                        return Json(groupedFile, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: error handler
            }

            return Json(Enumerable.Empty<object>(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public ActionResult DeleteFiles(int appId, string fileIds)
        {
            try
            {
                Guid? consumerKey = DB.Applications.Where(e => e.ApplicationID == appId && e.IsDeleted == false).Select(e => e.ConsumerKey).SingleOrDefault();
                if (!consumerKey.HasValue)
                {
                    return HttpNotFound("ApplicationId not found");
                }

                string[] ids = fileIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, bool> results = new Dictionary<string, bool>();

                foreach (string id in ids)
                {
                    JObject result = EgaContentStore.Delete(consumerKey.ToString(), id);
                    bool status = false;
                    bool.TryParse(result["Result"].ToString(), out status);
                    results.Add(id, status);
                }

                return Json(results, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // TODO: error handler
            }

            return Json(Enumerable.Empty<object>(), JsonRequestBehavior.AllowGet);
        }
    }
}