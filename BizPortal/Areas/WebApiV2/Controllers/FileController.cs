using BizPortal.Areas.WebApi.Controllers;
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class FileController : ApiControllerBase
    {
        [HttpPost]
        [Route("Api/V2/File/SignedFileInfo")]
        public string SignedFileInfo(FileInfo fileInfo)
        {
            return FileHelper.SignedFileInfo(fileInfo.SereializeData, ConfigurationManager.AppSettings["FileConsumerKey"], ConfigurationManager.AppSettings["FileConsumerSecret"]);
        }

        [HttpGet]
        [Route("Api/V2/File/GetToken")]
        public ResponseData GetToken(Guid requestId, string fileId, string fileConsumerKey)
        {
            try
            {
                var isFound = false;
                var request = ApplicationRequestEntity.Get(requestId);

                if (request != null)
                {
                    var isMatchfileConsumerkey = DB.Applications.Any(e => e.ApplicationID == request.ApplicationID && e.FileOwner == fileConsumerKey);

                    if (isMatchfileConsumerkey)
                    {
                        if (!isFound && request.GovFiles != null && request.GovFiles.Count > 0)
                        {
                            isFound = request.GovFiles.Where(o => o.FileID == fileId).Any();
                        }

                        if (!isFound && request.EPermitFiles != null && request.EPermitFiles.Count > 0)
                        {
                            isFound = request.EPermitFiles.Where(o => o.FileID == fileId).Any();
                        }

                        if (!isFound && request.BillPaymentFiles != null && request.BillPaymentFiles.Count > 0)
                        {
                            isFound = request.BillPaymentFiles.Where(o => o.FileID == fileId).Any();
                        }
                        
                        if(!isFound && request.Transactions != null && request.Transactions.Count > 0)
                        {
                            foreach(var trans in request.Transactions)
                            {
                                if(trans.UploadedFiles != null && trans.UploadedFiles.Any(t => t.Files.Any(f => f.FileID == fileId)))
                                {
                                    isFound = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                           throw new Exception("fileConsumerKey key not found");
                    }
                }

                if (isFound)
                {
                    var fileInfo = new BizPortal.ViewModels.V2.FileMetadata() { FileID = fileId };
                    var token = fileInfo.GetDownloadToken();

                    return new ResponseData()
                    {
                        Data = token,
                        Type = ResultDataType.Success,
                        Message = "Successfully"
                    };
                }
                else
                {
                    throw new Exception("File not found");
                }
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Data = null,
                    Type = ResultDataType.Error,
                    Message = "File not found"
                };
            }
        }
    
    }
}