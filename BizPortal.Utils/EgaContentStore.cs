using EGA.WS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft;
using System.ComponentModel.DataAnnotations;

namespace BizPortal.Utils
{
    public static class EgaContentStore
    {
        public static JObject Get(string consumerKey, string fileId)
        {
            EGAWSAPI api = EGAWSAPI.CreateInstance(consumerKey);
            string url = ConfigurationManager.AppSettings["ContentStoreUrl"] + "/load";
            Dictionary<string, string> arg = new Dictionary<string, string>();
            arg.Add("FileID", fileId);

            var result = api.Get(url, arg);
            return result;
        }

        public static JObject GetAllOfRequest(string consumerKey, int juristicRequestId, FileStatus? status = null, FileRequestTypeEnum? requestType = null)
        {
            EGAWSAPI api = EGAWSAPI.CreateInstance(consumerKey);
            string url = ConfigurationManager.AppSettings["ContentStoreUrl"] + "/find";
            Dictionary<string, string> arg = new Dictionary<string, string>();

            if (status.HasValue)
            {
                if (requestType.HasValue)
                    arg.Add("Extras", JsonConvert.SerializeObject(new { Status = ((int)status).ToString(), JuristicRequestId = juristicRequestId.ToString(), RequestType = ((int)requestType.Value).ToString() }));
                else
                    arg.Add("Extras", JsonConvert.SerializeObject(new { Status = ((int)status).ToString(), JuristicRequestId = juristicRequestId.ToString() }));
            }
            else
            {
                if (requestType.HasValue)
                    arg.Add("Extras", JsonConvert.SerializeObject(new { JuristicRequestId = juristicRequestId.ToString(), RequestType = ((int)requestType.Value).ToString() }));
                else
                    arg.Add("Extras", JsonConvert.SerializeObject(new { JuristicRequestId = juristicRequestId.ToString() }));
            }

            var result = api.Get(url, arg);
            return result;
        }

        public static JObject GetAllOfOwner(string consumerKey, string juristicId, FileStatus? status = null)
        {
            EGAWSAPI api = EGAWSAPI.CreateInstance(consumerKey);
            string url = ConfigurationManager.AppSettings["ContentStoreUrl"] + "/find";
            Dictionary<string, string> arg = new Dictionary<string, string>();

            arg.Add("Owner", juristicId.ToString());

            if (status.HasValue)
            {
                arg.Add("Extras", JsonConvert.SerializeObject(new { Status = ((int)status).ToString() }));
            }

            var result = api.Get(url, arg);
            return result;
        }

        public static JObject Add(string consumerKey, string filename, string detail, string owner, string tags, string type, string name, int requestType, string browseId, byte[] fileData)
        {
            EGAWSAPI api = EGAWSAPI.CreateInstance(consumerKey);
            string url = ConfigurationManager.AppSettings["ContentStoreUrl"] + "/save";
            Dictionary<string, string> arg = new Dictionary<string, string>();

            arg.Add("FileName", filename);
            arg.Add("FileDescription", detail);
            arg.Add("Owner", owner);
            arg.Add("Tags", tags);
            arg.Add("Extras", JsonConvert.SerializeObject(new
            {
                Status = ((int)FileStatus.Temp).ToString(),
                JuristicRequestId = "0",
                Type = type,
                Name = name,
                RequestType = (int)requestType,
                BrowseID = browseId
            }
            ));
            arg.Add("Data", Base64.ByteToBase64urlString(fileData));

            var result = api.Post(url, arg);
            return result;
        }


        // Remarks * the Extras Fields can't update patial fields
        public static JObject Update(string consumerKey, string fileId, string name, string detail, string owner, string content, string tags, string extras, byte[] fileData)
        {
            EGAWSAPI api = EGAWSAPI.CreateInstance(consumerKey);
            string url = ConfigurationManager.AppSettings["ContentStoreUrl"] + "/edit";
            Dictionary<string, string> arg = new Dictionary<string, string>();

            arg.Add("FileID", fileId);
            arg.Add("FileName", name);
            arg.Add("FileDescription", detail);
            arg.Add("Owner", owner);
            arg.Add("Tags", tags);
            arg.Add("Extras", extras);
            arg.Add("Data", Base64.ByteToBase64urlString(fileData));

            var result = api.Put(url, arg);
            return result;
        }

        public static JObject UpdateStatus(string consumerKey, string fileId, Dictionary<string, string> extra, int juristicRequestId, string fileDrescription, FileStatus status)
        {
            EGAWSAPI api = EGAWSAPI.CreateInstance(consumerKey);
            string url = ConfigurationManager.AppSettings["ContentStoreUrl"] + "/edit";
            Dictionary<string, string> arg = new Dictionary<string, string>();


            if (extra.ContainsKey("JuristicRequestId"))
            {
                extra["JuristicRequestId"] = juristicRequestId.ToString();
            }
            else
            {
                extra.Add("JuristicRequestId", juristicRequestId.ToString());
            }

            if (extra.ContainsKey("Status"))
            {
                extra["Status"] = ((int)status).ToString();
            }
            else
            {
                extra.Add("Status", ((int)status).ToString());
            }

            arg.Add("FileID", fileId);
            arg.Add("FileDescription", fileDrescription);
            arg.Add("Extras", JsonConvert.SerializeObject(extra));

            var result = api.Put(url, arg);
            return result;
        }

        public static JObject UpdateStatuses(string consumerKey, int juristicRequestId, List<FileMetadata> fileMetadatas, FileStatus status)
        {
            EGAWSAPI api = EGAWSAPI.CreateInstance(consumerKey);
            string url = ConfigurationManager.AppSettings["ContentStoreUrl"] + "/edits";

            foreach (var fm in fileMetadatas)
            {
                fm.FileDescription = fm.FileDescription ?? string.Empty;

                if (fm.Extras.ContainsKey("JuristicRequestId"))
                {
                    fm.Extras["JuristicRequestId"] = juristicRequestId.ToString();
                }
                else
                {
                    fm.Extras.Add("JuristicRequestId", juristicRequestId.ToString());
                }

                if (fm.Extras.ContainsKey("Status"))
                {
                    fm.Extras["Status"] = ((int)status).ToString();
                }
                else
                {
                    fm.Extras.Add("Status", ((int)status).ToString());
                }
            }

            string custombody = JsonConvert.SerializeObject(new
            {
                List = fileMetadatas
            });

            var result = api.Call(url, HttpVerb.PUT, new Dictionary<string, string>(), custombody, ContentType.ApplicationJson);
            return result;
        }

        [Obsolete("Use UpdateStatuses function instead")]
        public static List<JObject> UpdateStatusList(string consumerKey, string[] fileIds, Dictionary<string, string>[] extras, int juristicRequestId, string[] fileDescriptions, FileStatus status)
        {
            List<JObject> results = new List<JObject>();
            for (int i = 0; i < fileIds.Length; i++)
            {
                results.Add(UpdateStatus(consumerKey, fileIds[i], extras[i], juristicRequestId, fileDescriptions[i], FileStatus.InUse));
            }
            return results;
        }

        public static JObject Delete(string consumerKey, string fileId)
        {
            EGAWSAPI api = EGAWSAPI.CreateInstance(consumerKey);
            string url = ConfigurationManager.AppSettings["ContentStoreUrl"] + "/delete";
            url = url + "?FileID=" + fileId;
            var result = api.Delete(url);

            return result;
        }
    }

    public class FileMetadata
    {
        [JsonProperty("FileID")]
        public string FileID { get; set; }
        [JsonProperty("FileName")]
        public string FileName { get; set; }
        [JsonProperty("FileDescription")]
        public string FileDescription { get; set; }
        [JsonProperty("Extras")]
        public Dictionary<string, string> Extras { get; set; }
        public string FileTypeCode { get; set; }
        public long FileSize { get; set; }
        public bool IsPublic { get; set; }
        public int UploadStatus { get; set; }
    }

}
