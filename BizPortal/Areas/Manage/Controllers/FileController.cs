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
using BizPortal.Utils.Security;

namespace BizPortal.Areas.Manage.Controllers
{
    public class FileController : ManageControllerBase
    {
        [HttpPost]
        public JsonResult FormUpload(int id, string type)
        {
            if (string.IsNullOrEmpty(type))
                type = "general";

            string uploadFolder = Vulnerability.GetValidatedDirectoryName(Server.MapPath("~/Uploads") + "\\" + type + "\\" + SecurityHelper.MD5EncryptToHex(id.ToString()));
            string uploadABSPath = "~/Uploads/" + type + "/" + SecurityHelper.MD5EncryptToHex(id.ToString());

            Vulnerability.SecureCreateDirectory(uploadFolder);

            HttpPostedFileBase file = Request.Files[0];
            string fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            string filePath = Vulnerability.GetValidatedFileName(uploadFolder, fileName);
            Vulnerability.SecureDeleteFile(filePath);

            file.SaveAs(filePath);
            var fUpload = DB.CreateFileUploadData(fileName, file.FileName, uploadABSPath + "/" + fileName, file.ContentType, file.ContentLength, false);

            return Json(new
            {
                Status = true,
                FileID = fUpload.FileUploadID,
                FileName = fUpload.FileName
            });
        }

        [HttpPost]
        public JsonResult PLUpload(int id, string type)
        {
            if (string.IsNullOrEmpty(type))
                type = "general";
            string uploadFolder = Vulnerability.GetValidatedDirectoryName(Server.MapPath("~/Uploads") + "\\" + type + "\\" + SecurityHelper.MD5EncryptToHex(id.ToString()));
            string uploadABSPath = "~/Uploads/" + type + "/" + SecurityHelper.MD5EncryptToHex(id.ToString());

            PLUpload plu = new PLUpload(System.Web.HttpContext.Current, uploadFolder, uploadABSPath);
            FileUpload fUpload = plu.ProcessUpload();

            if ((fUpload != null) && (fUpload.TemporaryStatus == false))
            {
                return Json(Task.Run(() =>
                {
                    return new
                    {
                        Status = true,
                        FileID = fUpload.FileUploadID,
                        FileName = fUpload.FileName
                    };
                }));
            }

            return Json(Task.Run(() =>
            {
                return new { Status = false };
            }));
        }
    }
}