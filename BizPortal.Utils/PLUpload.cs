using BizPortal.DAL;
using BizPortal.Models;
using EGA.WS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using System.Configuration;
using BizPortal.Utils.Security;
using BizPortal.Utils.Helpers;

namespace BizPortal.Utils
{
    public class PLUpload : IProgress<int>
    {
        private HttpContext _httpContext;
        private string _tempExtension = "_temp";
        private string _fileName;
        private string _originalFileName;
        private int _totalChunk;
        private bool _lastChunk;
        private bool _firstChunk;
        private string _temp = "~/Uploads/_tmp";
        private string _tmpDir;
        private string _uploadFolder;
        private string _uploadABSFolder;
        private JObject _result = null;

        public PLUpload(HttpContext context)
        {
            this._httpContext = context;
        }

        public PLUpload(HttpContext context, string uploadFolder, string uploadABSFolder)
        {
            this._httpContext = context;
            this._uploadFolder = uploadFolder;
            this._uploadABSFolder = uploadABSFolder;
        }

        public FileUpload ProcessUpload()
        {
            _tmpDir = HttpContext.Current.Server.MapPath(_temp);
            HttpPostedFile file = this._httpContext.Request.Files[0];
            if (file.ContentLength == 0)
                throw new ArgumentException("No file input");

            try
            {
                GetQueryStringParameters();

                string uploadFolder = this._uploadFolder;
                string tempFileName = _fileName + _tempExtension;

                // Create upload folder
                uploadFolder = Vulnerability.SecureCreateDirectory(uploadFolder).FullName;
                // Create temp folder
                _tmpDir = Vulnerability.SecureCreateDirectory(_tmpDir).FullName;

                if (_firstChunk)
                {
                    //Delete temp file
                    Vulnerability.SecureDeleteFile(_tmpDir + "\\" + tempFileName);
                    //Delete target file
                    Vulnerability.SecureDeleteFile(uploadFolder + "\\" + _fileName);
                }


                FileInfo fiSave = new FileInfo(Vulnerability.GetValidatedFileName(_tmpDir, tempFileName));
                FileStream fs = null;
                if (_firstChunk)
                    fs = fiSave.Open(FileMode.Create);
                else
                    fs = fiSave.Open(FileMode.Append, FileAccess.Write);

                using (fs)
                {
                    SaveFile(_httpContext.Request.Files[0].InputStream, fs);
                    fs.Close();
                }

                if (_lastChunk)
                {
                    string ext = Path.GetExtension(_fileName);
                    _fileName = Path.GetRandomFileName() + ext;
                    fiSave.MoveTo(Vulnerability.GetValidatedFileName(uploadFolder, _fileName));
                }

                System.Threading.Thread.Sleep(500);

                FileUpload fUpload = UpdateFileUploadData();
                return fUpload;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public JObject ProcessUploadToEgaWs(string consumerKey, string owner, string type, string name, int requestType, string browseId)
        {
            _tmpDir = HttpContext.Current.Server.MapPath(_temp);
            HttpPostedFile file = this._httpContext.Request.Files[0];
            if (file.ContentLength == 0)
            {
                throw new ArgumentException("No file input");
            }

            try
            {
                GetQueryStringParameters();

                string uploadFolder = this._uploadFolder;
                string tempFileName = _fileName + _tempExtension;


                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Create Temp Dir
                if (!Directory.Exists(_tmpDir))
                {
                    Directory.CreateDirectory(_tmpDir);
                }

                DirectoryInfo diTmp = new DirectoryInfo(_tmpDir);

                if (_firstChunk)
                {
                    //Delete temp file
                    Vulnerability.SecureDeleteFile(_tmpDir + "\\" + tempFileName);

                    //Delete target file
                    Vulnerability.SecureDeleteFile(uploadFolder + "\\" + _fileName);
                }

                FileInfo fiSave = new FileInfo(_tmpDir + "\\" + Path.GetInvalidFileNameChars().Aggregate(tempFileName, (current, c) => current.Replace(c.ToString(), string.Empty)));
                FileStream fs = null;
                if (_firstChunk)
                {
                    fs = fiSave.Open(FileMode.Create);
                }
                else
                {
                    fs = fiSave.Open(FileMode.Append, FileAccess.Write);
                }

                using (fs)
                {
                    SaveFile(_httpContext.Request.Files[0].InputStream, fs);
                    fs.Close();
                }

                if (_lastChunk)
                {
                    DirectoryInfo diUpload = new DirectoryInfo(uploadFolder);

                    // read to byte
                    byte[] data;
                    using (fs = fiSave.Open(FileMode.Open, FileAccess.Read))
                    {
                        int length = System.Convert.ToInt32(fs.Length);
                        data = new byte[length];
                        fs.Read(data, 0, length);
                        fs.Close();
                    }

                    // remove file form temp
                    fiSave.Delete();

                    // send to ega content store
                    _result = EgaContentStore.Add(consumerKey, _originalFileName, "", owner, "", type, name, requestType, browseId, data);
                }

                return _result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<JObject> ProcessUploadToFileService(string fileServiceUrl, string fileUploadAccessTokenUrl, string fileConsumerKey, string fileSecret, string fileInfo)
        {
            _tmpDir = HttpContext.Current.Server.MapPath(_temp);
            HttpPostedFile file = this._httpContext.Request.Files[0];
            if (file.ContentLength == 0)
            {
                throw new ArgumentException("No file input");
            }

            try
            {
                GetQueryStringParameters();
                
                string tempFileName = _fileName + _tempExtension;


                // Create Temp Dir
                if (!Directory.Exists(_tmpDir))
                {
                    Directory.CreateDirectory(_tmpDir);
                }

                DirectoryInfo diTmp = new DirectoryInfo(_tmpDir);

                if (_firstChunk)
                {
                    //Delete temp file
                    Vulnerability.SecureDeleteFile(_tmpDir + "\\" + tempFileName);
                }

                FileInfo fiSave = new FileInfo(_tmpDir + "\\" + Path.GetInvalidFileNameChars().Aggregate(tempFileName, (current, c) => current.Replace(c.ToString(), string.Empty)));
                FileStream fs = null;
                if (_firstChunk)
                {
                    fs = fiSave.Open(FileMode.Create);
                }
                else
                {
                    fs = fiSave.Open(FileMode.Append, FileAccess.Write);
                }

                using (fs)
                {
                    SaveFile(_httpContext.Request.Files[0].InputStream, fs);
                    fs.Close();
                }

                if (_lastChunk)
                {
                    var fileAccessToken = FileHelper.RequestAccessToken(fileUploadAccessTokenUrl, fileConsumerKey, fileSecret, fileInfo);

                    //var filename = data.name;
                    //var uploadedData = {
                    //    UploaderID: $browseHelper.attr('id'),
                    //    UploaderTypeName: extras.uploadertype,
                    //    FileID: data.fileId,
                    //    FileName: data.name,
                    //    ContentType: data.contentType,
                    //    FileSize: data.fileSize,
                    //    IsPublic: data.isPublic,
                    //    FileTypeCode: extras.filetype,
                    //    FileTypeName: extras.filetypename,
                    //    Extras: extras
                    //};


                // send to file service
                using (fs = fiSave.Open(FileMode.Open, FileAccess.Read))
                    {
                        var fileitem = await FileHelper.UploadFile(fileServiceUrl, fileAccessToken, fileConsumerKey, fileSecret, fileInfo, fs, this);

                        if (fileitem != null)
                        {
                            _result = JObject.FromObject(fileitem);
                        }
                        else 
                        {
                            throw new Exception("ไม่สามารถ upload file ได้");
                        }

                        fs.Close();
                    }

                    // remove file form temp
                    fiSave.Delete();
                }

                return _result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private FileUpload UpdateFileUploadData()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            HttpPostedFile file = _httpContext.Request.Files[0];

            string uploadFolder = this._uploadFolder;
            string tempFileName = _fileName + _tempExtension;
            string actualLocation = Vulnerability.GetValidatedFileName(uploadFolder, _fileName);
            string tempLocation = _temp + "/" + tempFileName;
            string actualABSLocation = this._uploadABSFolder + "/" + _fileName;

            FileInfo fi = null;
            if (_lastChunk)
                fi = new FileInfo(actualLocation);
            else
                fi = new FileInfo(Vulnerability.GetValidatedFileName(HttpContext.Current.Server.MapPath(_temp), tempFileName));
            long contentLength = fi.Length;
            string refFileName = Path.GetFileName(_httpContext.Request.Form["name"]);

            if (_firstChunk)
            {
                if (_lastChunk)
                {
                    return db.CreateFileUploadData(_fileName, refFileName, actualABSLocation, file.ContentType, contentLength, false);
                }
                else
                    return db.CreateFileUploadData(_fileName, refFileName, tempLocation, file.ContentType, contentLength, true);
            }
            else
            {
                if (_lastChunk)
                {

                    return db.UpdateFileUploadData(refFileName, actualABSLocation, contentLength, false);
                }
                else
                    return db.UpdateFileUploadData(_fileName, tempLocation, contentLength, true);
            }
        }

        private void GetQueryStringParameters()
        {
            if (_httpContext.Request.Form["chunk"] != null)
            {
                int chunk = int.Parse(_httpContext.Request.Form["chunk"]);
                _fileName = _httpContext.Request.Form["name"];
                _originalFileName = _httpContext.Request.Form["fileName"];
                _totalChunk = int.Parse(_httpContext.Request.Form["chunks"]);
                _lastChunk = _totalChunk - 1 == chunk;
                _firstChunk = chunk == 0;
            }
            else
            {
                _fileName = _httpContext.Request.Form["name"];
                _totalChunk = 1;
                _lastChunk = true;
                _firstChunk = true;
            }
        }

        private void SaveFile(Stream stream, FileStream fs)
        {
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                fs.Write(buffer, 0, bytesRead);
            }
        }

        public void Report(int value)
        {
        }
    }
}
