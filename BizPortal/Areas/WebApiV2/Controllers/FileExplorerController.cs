using BizPortal.Areas.WebApi.Controllers;
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using BizPortal.ViewModels.V2;
using MongoDB.Driver;
using System.Linq;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    //[Authorize()]
    public class FileExplorerController : ApiControllerBase
    {
        [Route("Api/v2/FileExplorer/List")]
        [HttpPost]
        public DataTablesResult<FileExplorerViewModel> List(FileExplorerDatatables dataTables)
        {
            DataTablesResult<FileExplorerViewModel> result = new DataTablesResult<FileExplorerViewModel>();

            var repo = MongoFactory.GetFileMetadataCollection().AsQueryable();
            var query = repo.Where(o => o.IdentityID == IdentityID).AsQueryable();
            var searchQuery = dataTables.GenerateSearchQuery<FileMetadataEntity>(query, "UpdatedDate", "desc");

            result.RecordsFiltered = result.RecordsTotal = query.Count();
            result.Data = searchQuery.Select(o => new FileExplorerViewModel()
            {
                ApplicationID = o.ApplicationID,
                ContentType = o.ContentType,
                FileID = o.FileID,
                FileSize = o.FileSize,
                IsPublic = o.IsPublic,
                FileName = o.FileName,
                FileTypeCode = o.FileTypeCode,
                UploadStatus = (int)o.UploadStatus,
                //Description = o.Description,
                //Extras = o.Extras,
                CreatedDate = o.CreatedDate,
                UpdatedDate = o.UpdatedDate
            }).ToList();

            foreach (var data in result.Data)
            {
                data.ApplicationName = DB.ApplicationTranslations.Where(o => o.ApplicationID == data.ApplicationID && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.ApplicationName).FirstOrDefault();
                data.FileTypeName = ResourceHelper.GetResourceWordWithDefault(data.FileTypeCode, "FileType", data.FileTypeCode);
            }




            //for(int i = 0;i<3; i++)
            //{
            //    result.Data = result.Data.Concat(result.Data);
            //}


            return result;
        }
    }
}
