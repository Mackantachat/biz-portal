using EGA.EGA_Development.Util;
using EGA.EGA_FileService.Util;
using EGA.EGA_FileService.Util.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public static class FileHelper
    {
        private const int BUFFER_SIZE = 256 * 1024; // 256KB
        public const int DEFAULT_CHUNK_SIZE = 2 * 1024 * 1024; //2MB
        public const int MIN_CHUNK_SIZE = 1024 * 1024; //1MB
        public const int MAX_CHUNK_SIZE = 60 * 1024 * 1024; //60MB
        public const int MAX_CHUNK = 100;
        public const long MAX_FILE_SIZE = 5368709120; //5GB = 5 * 1024 * 1024 * 1024
        
        public static string RequestAccessToken(string servicePath, string consumerKey, string consumerSecret, string fileItemString)
        {
            if (String.IsNullOrEmpty(servicePath))
                throw new ArgumentNullException("servicePath");
            if (String.IsNullOrEmpty(consumerKey))
                throw new ArgumentNullException("consumerKey");
            var fileItem = ModelHelper.Deserialize<FileItem>(fileItemString);
            if (fileItem == null)
                throw new ArgumentNullException("fileItem");
            if (String.IsNullOrEmpty(consumerSecret))
                throw new ArgumentNullException("consumerSecret");

            string signedFileInfo = Signer.SignedFileInfo(fileItemString, consumerKey, consumerSecret);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(servicePath);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.Timeout = 10000;
            request.Headers.Add("Origin", GetCurrentDomain());

            string postData = $"Consumer-Key={consumerKey}&FileInfo={System.Web.HttpUtility.UrlEncode(fileItemString)}&SignedFileInfo={signedFileInfo}";
            byte[] data = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = data.Length;

            string message = String.Empty;

            var AccessToken = new { AccessToken = "" };
            try
            {
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(data, 0, data.Length);
                    postStream.Flush();
                    postStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    message = stream.ReadToEnd();
                }
                if (response.StatusCode == HttpStatusCode.NoContent)
                    throw new FileNotFoundException(message, fileItem.FileId);
                //if (response.StatusCode == HttpStatusCode.Unauthorized)
                //    throw new UnauthorizedAccessException(message);

                var token = JsonConvert.DeserializeAnonymousType(message, AccessToken);
                return token.AccessToken;
            }
            catch (WebException exc)
            {
                var errorMsg = new { Message = "" };
                using (StreamReader stream = new StreamReader(exc.Response.GetResponseStream()))
                {
                    message = stream.ReadToEnd();
                }
                //try
                //{
                //var msg = JsonConvert.DeserializeAnonymousType(message, errorMsg);
                HttpWebResponse response = (HttpWebResponse)exc.Response;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException(message);
                throw new Exception($"{exc.Message} with message \"{message}\"", exc);
                //}
                //catch {
                //    throw exc;
                //}
            }
        }
       
        public static string RequestDeleteToken(string servicePath, string consumerKey, string consumerSecret, string[] deletingFileIds)
        {
            if (String.IsNullOrEmpty(servicePath))
                throw new ArgumentNullException("servicePath");
            if (String.IsNullOrEmpty(consumerKey))
                throw new ArgumentNullException("consumerKey");
            if (deletingFileIds == null || deletingFileIds.Length == 0)
                throw new ArgumentNullException("deletingFileIds");
            if (String.IsNullOrEmpty(consumerSecret))
                throw new ArgumentNullException("consumerSecret");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(servicePath);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
#if DEBUG
            request.Timeout = 1800000;
#else
            request.Timeout = 10000;
#endif
            string del = JsonConvert.SerializeObject(deletingFileIds);
            string postData = $"Consumer-Key={consumerKey}&DeletingFileIds={del}";
            byte[] data = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = data.Length;

            string json = String.Empty;

            var AccessToken = new { AccessToken = "" };
            try
            {
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(data, 0, data.Length);
                    postStream.Flush();
                    postStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.NoContent)
                    throw new FileNotFoundException(json, del);

                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    json = stream.ReadToEnd();
                }
                var token = JsonConvert.DeserializeAnonymousType(json, AccessToken);
                return token.AccessToken;
            }
            catch (WebException exc)
            {
                var errorMsg = new { Message = "" };
                string message = String.Empty;
                using (StreamReader stream = new StreamReader(exc.Response.GetResponseStream()))
                {
                    message = stream.ReadToEnd();
                }
                try
                {
                    var msg = JsonConvert.DeserializeAnonymousType(message, errorMsg);
                    throw new Exception(String.Format("{0} with message \"{1}\"", exc.Message, msg.Message), exc);
                }
                catch
                {
                    throw exc;
                }
            }
        }
        
        public static string RequestSearchToken(string servicePath, string consumerKey, string consumerSecret, string criterionString)
        {
            if (String.IsNullOrEmpty(servicePath))
                throw new ArgumentNullException("servicePath");
            if (String.IsNullOrEmpty(consumerKey))
                throw new ArgumentNullException("consumerKey");
            SearchCriterion criterion = JsonConvert.DeserializeObject<SearchCriterion>(criterionString);
            if (criterion == null)
                throw new ArgumentNullException("criterionString");
            if (String.IsNullOrEmpty(consumerSecret))
                throw new ArgumentNullException("consumerSecret");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(servicePath);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.Timeout = 10000;

            string postData = $"Consumer-Key={consumerKey}&Criterion={criterionString}";
            byte[] data = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = data.Length;

            string json = String.Empty;

            var AccessToken = new { AccessToken = "" };
            try
            {
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(data, 0, data.Length);
                    postStream.Flush();
                    postStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.NoContent)
                    throw new FileNotFoundException(json, criterionString);

                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    json = stream.ReadToEnd();
                }
                var token = JsonConvert.DeserializeAnonymousType(json, AccessToken);
                return token.AccessToken;
            }
            catch (WebException exc)
            {
                var errorMsg = new { Message = "" };
                string message = String.Empty;
                using (StreamReader stream = new StreamReader(exc.Response.GetResponseStream()))
                {
                    message = stream.ReadToEnd();
                }
                try
                {
                    var msg = JsonConvert.DeserializeAnonymousType(message, errorMsg);
                    throw new Exception(String.Format("{0} with message \"{1}\"", exc.Message, msg.Message), exc);
                }
                catch
                {
                    throw exc;
                }
            }
        }
        
        public static byte[] DownloadFile(string filePath)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                var request = new HttpRequestMessage(HttpMethod.Get, filePath);
                request.Headers.Add("Origin", GetCurrentDomain());
                var result = client.SendAsync(request).ContinueWith((t) => { return new HttpResponseMessage() { Content = t.Result.Content }; });
                byte[] bytes = null;
                result.Wait();
                if (result.IsCompleted)
                {
                    bytes = result.Result.Content.ReadAsByteArrayAsync().Result;
                }
                return bytes;
            }
        }
        
        public static async Task<FileItem> UploadFile(string servicePath, string accessToken, string consumerKey, string consumerSecret, string fileItemString, Stream dataStream, IProgress<int> progress)
        {
            long fileSize = dataStream.Length;
            if (fileSize > MAX_FILE_SIZE)
                throw new ArgumentOutOfRangeException("dataStream", $"file size must less than {MAX_FILE_SIZE:n0} bytes or equal.");

            int calculatedChunkSize = DEFAULT_CHUNK_SIZE;
            if (((long)DEFAULT_CHUNK_SIZE * MAX_CHUNK) > fileSize)
            {
                calculatedChunkSize = calculatedChunkSize * 2;

                while (((long)calculatedChunkSize * MAX_CHUNK) < fileSize)
                {
                    calculatedChunkSize = calculatedChunkSize * 2;
                }
            }
            else
            {
                calculatedChunkSize = DEFAULT_CHUNK_SIZE;
            }
            int totalChunk = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(fileSize) / calculatedChunkSize));

            byte[] buffer = new byte[BUFFER_SIZE];
            int bytesRead = 0;
            int chunk = 0;
            int chunkSize = 0;

            FileItem file = null;

            Stream output = new MemoryStream();
            try
            {
                long remain = fileSize;
                int readSize = (int)Math.Min((long)BUFFER_SIZE, remain);
                while ((bytesRead = dataStream.Read(buffer, 0, readSize)) > 0)
                {
                    chunkSize += bytesRead;
                    remain -= bytesRead;
                    output.Write(buffer, 0, bytesRead);
                    if ((readSize < BUFFER_SIZE) || (chunkSize >= calculatedChunkSize))
                    {
                        output.Seek(0, SeekOrigin.Begin);
                        file = await UploadFile(servicePath, accessToken, consumerKey, consumerSecret, fileItemString, output, chunk, totalChunk);
                        output.Close();
                        output.Dispose();

                        output = new MemoryStream();
                        chunkSize = 0;
                        chunk++;
                        progress.Report((int)(1 - (remain / fileSize) * 100));
                    }
                    readSize = (int)Math.Min((long)BUFFER_SIZE, remain);
                }
            }
            finally
            {
                output.Close();
                output.Dispose();

            }

            return file;
        }
        
        public static Task<FileItem> UploadFile(string servicePath, string accessToken, string consumerKey, string consumerSecret, string fileItemString, Stream dataStream, int chunkIndex, int totalChunk)
        {
            if (String.IsNullOrEmpty(servicePath))
                throw new ArgumentNullException("servicePath");
            if (String.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException("accessToken");
            if (String.IsNullOrEmpty(consumerKey))
                throw new ArgumentNullException("consumerKey");
            var fileItem = ModelHelper.Deserialize<FileItem>(fileItemString);
            if (fileItem == null)
                throw new ArgumentNullException("fileItem");
            if (dataStream == null)
                throw new ArgumentNullException("dataStream");
            long datalength = dataStream.Length;
            if (datalength == 0)
                throw new ArgumentOutOfRangeException("dataStream must has length greater than 0.");
            if (String.IsNullOrEmpty(consumerSecret))
                throw new ArgumentNullException("consumerSecret");
            if (chunkIndex < 0)
                throw new ArgumentOutOfRangeException("chunkIndex must be greater than 0 or equal.");
            //if (chunkIndex > MAX_CHUNK - 1)
            //    throw new ArgumentOutOfRangeException($"chunkIndex must be less than {MAX_CHUNK}.");
            if (totalChunk < 1)
                throw new ArgumentOutOfRangeException("totalChunk must be greater than 0.");
            if (totalChunk <= chunkIndex)
                throw new ArgumentOutOfRangeException("totalChunk must be greater than chunkIndex.");
            //if (totalChunk > MAX_CHUNK)
            //    throw new ArgumentOutOfRangeException($"totalChunk must be less than {MAX_CHUNK} or equal.");
            var lastChunk = totalChunk - 1;
            if ((chunkIndex != lastChunk) && (datalength < MIN_CHUNK_SIZE))
                throw new ArgumentOutOfRangeException($"Chunk size of dataStream must be greater than {MIN_CHUNK_SIZE: n0} or equal");

            string signedFileInfo = Signer.SignedFileInfo(fileItemString, consumerKey, consumerSecret);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(servicePath);
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.Timeout = 60000;
            request.KeepAlive = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            request.Headers.Add("Origin", GetCurrentDomain());

            string json = String.Empty;
            FileItem file = null;

            try
            {
                using (Stream reqStream = request.GetRequestStream())
                {
                    string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                    reqStream.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, "AccessToken", accessToken);
                    byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                    reqStream.Write(formitembytes, 0, formitembytes.Length);

                    reqStream.Write(boundarybytes, 0, boundarybytes.Length);
                    formitem = string.Format(formdataTemplate, "Consumer-Key", consumerKey);
                    formitembytes = Encoding.UTF8.GetBytes(formitem);
                    reqStream.Write(formitembytes, 0, formitembytes.Length);

                    reqStream.Write(boundarybytes, 0, boundarybytes.Length);
                    formitem = string.Format(formdataTemplate, "FileInfo", fileItemString);
                    formitembytes = Encoding.UTF8.GetBytes(formitem);
                    reqStream.Write(formitembytes, 0, formitembytes.Length);

                    reqStream.Write(boundarybytes, 0, boundarybytes.Length);
                    formitem = string.Format(formdataTemplate, "SignedFileInfo", signedFileInfo);
                    formitembytes = Encoding.UTF8.GetBytes(formitem);
                    reqStream.Write(formitembytes, 0, formitembytes.Length);

                    reqStream.Write(boundarybytes, 0, boundarybytes.Length);
                    formitem = string.Format(formdataTemplate, "name", fileItem.Name);
                    formitembytes = Encoding.UTF8.GetBytes(formitem);
                    reqStream.Write(formitembytes, 0, formitembytes.Length);

                    reqStream.Write(boundarybytes, 0, boundarybytes.Length);
                    formitem = string.Format(formdataTemplate, "chunk", chunkIndex);
                    formitembytes = Encoding.UTF8.GetBytes(formitem);
                    reqStream.Write(formitembytes, 0, formitembytes.Length);

                    reqStream.Write(boundarybytes, 0, boundarybytes.Length);
                    formitem = string.Format(formdataTemplate, "chunks", totalChunk);
                    formitembytes = Encoding.UTF8.GetBytes(formitem);
                    reqStream.Write(formitembytes, 0, formitembytes.Length);

                    reqStream.Write(boundarybytes, 0, boundarybytes.Length);
                    formitem = $"Content-Disposition: form-data; name=\"file\"; filename=\"blob\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                    formitembytes = Encoding.UTF8.GetBytes(formitem);
                    reqStream.Write(formitembytes, 0, formitembytes.Length);

                    dataStream.CopyTo(reqStream, BUFFER_SIZE);

                    byte[] trailer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                    reqStream.Write(trailer, 0, trailer.Length);
                    reqStream.Close();

                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    json = stream.ReadToEnd();
                }
                file = ModelHelper.Deserialize<FileItem>(json);

                //return file;
            }
            catch (WebException exc)
            {
                var errorMsg = new { Message = "" };
                string message = String.Empty;
                if (exc.Response != null)
                {
                    using (StreamReader stream = new StreamReader(exc.Response.GetResponseStream()))
                    {
                        message = stream.ReadToEnd();
                    }
                }
                var msg = JsonConvert.DeserializeAnonymousType(message, errorMsg);
                throw new Exception(String.Format("{0} with message \"{1}\"", exc.Message, msg.Message), exc);
            }
            return Task.FromResult(file);
        }
        
        public static void DeleteFile(string servicePath, string accessToken, string consumerKey, string consumerSecret, string[] deletingFileIds)
        {
            if (String.IsNullOrEmpty(servicePath))
                throw new ArgumentNullException("servicePath");
            if (String.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException("accessToken");
            if (String.IsNullOrEmpty(consumerKey))
                throw new ArgumentNullException("consumerKey");
            if (String.IsNullOrEmpty(consumerSecret))
                throw new ArgumentNullException("consumerSecret");
            if ((deletingFileIds == null) || (deletingFileIds.Length == 0))
                throw new ArgumentNullException("fileItem");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(servicePath);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "DELETE";
            request.Timeout = 10000;

            string del = JsonConvert.SerializeObject(deletingFileIds);

            string postData = $"AccessToken={accessToken}&Consumer-Key={consumerKey}&DeletingFileIds={del}";
            byte[] data = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = data.Length;

            try
            {
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(data, 0, data.Length);
                    postStream.Flush();
                    postStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException exc)
            {
                var errorMsg = new { Message = "" };
                string message = String.Empty;
                using (StreamReader stream = new StreamReader(exc.Response.GetResponseStream()))
                {
                    message = stream.ReadToEnd();
                }
                var msg = JsonConvert.DeserializeAnonymousType(message, errorMsg);
                throw new Exception(String.Format("{0} with message \"{1}\"", exc.Message, msg.Message), exc);
            }

        }
        
        public static SearchResult SearchFile(string servicePath, string accessToken, string consumerKey, string consumerSecret, SearchCriterion criterion)
        {
            if (String.IsNullOrEmpty(servicePath))
                throw new ArgumentNullException("servicePath");
            if (String.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException("accessToken");
            if (String.IsNullOrEmpty(consumerKey))
                throw new ArgumentNullException("consumerKey");
            if (String.IsNullOrEmpty(consumerSecret))
                throw new ArgumentNullException("consumerSecret");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(servicePath);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.Timeout = 10000;
            request.KeepAlive = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            string del = JsonConvert.SerializeObject(criterion);

            string postData = $"AccessToken={accessToken}&Consumer-Key={consumerKey}&Criterion={del}";
            byte[] data = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = data.Length;

            try
            {
                string json = String.Empty;
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(data, 0, data.Length);
                    postStream.Flush();
                    postStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    json = stream.ReadToEnd();
                }

                return ModelHelper.Deserialize<SearchResult>(json);

            }
            catch (WebException exc)
            {
                var errorMsg = new { Message = "" };
                string message = String.Empty;
                using (StreamReader stream = new StreamReader(exc.Response.GetResponseStream()))
                {
                    message = stream.ReadToEnd();
                }
                var msg = JsonConvert.DeserializeAnonymousType(message, errorMsg);
                throw new Exception(String.Format("{0} with message \"{1}\"", exc.Message, msg.Message), exc);
            }

        }

        /// <summary>
        /// Get current domain by using System.Web.HttpContext.Current.Request.Url
        /// </summary>
        /// <returns></returns>
        private static string GetCurrentDomain()
        {
            var origin  = ConfigurationManager.AppSettings["FileOriginDomain"];

            if (string.IsNullOrEmpty(origin))
            {
                UriBuilder uri = new UriBuilder();
                uri.Scheme = System.Web.HttpContext.Current.Request.Url.Scheme;
                uri.Host = System.Web.HttpContext.Current.Request.Url.Host;
                uri.Port = System.Web.HttpContext.Current.Request.Url.Port;

                return uri.Uri.ToString();
            }
            else 
            {
                return origin;
            }
        }

        /// <summary>
        /// http://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        public static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        public static string SignedFileInfo(string plainText, string consumerKey, string consumerSecret) 
        {
            return Signer.SignedFileInfo(plainText, consumerKey, consumerSecret);
        }
    }

    public static class ModelHelper
    {
        public static T Deserialize<T>(string input) where T : class
        {
            if (input == null)
                return default(T);

            Type type = typeof(T);
            if (type == typeof(FileItem))
            {
                _FileItem file = JsonConvert.DeserializeObject<_FileItem>(input);
                if (file == null)
                    return default(T);
                return new FileItem(file) as T;
            }

            return JsonConvert.DeserializeObject<T>(input);
        }
        public static string Serialize<T>(T obj) where T : class
        {
            return JsonConvert.SerializeObject(obj);
        }
    }

    public class FileItem
    {
        public FileItem() { }

        internal FileItem(_FileItem file)
        {
            try { FileId = file.FileId; } catch { }
            try { Name = file.Name; } catch { }
            try { ContentType = file.ContentType; } catch { }
            try { FileSize = file.FileSize; } catch { }
            try { IsPublic = file.IsPublic; } catch { }
            try { CreatorToken = file.CreatorToken; } catch { }
            try { CreationDate = file.CreationDate; } catch { }
            try { UploadStatus = !string.IsNullOrEmpty(file.Status) ? EnumUtil.GetEnum<UploadStatus>(file.Status) : UploadStatus.Unknown;  } catch { }
        }
        public string FileId { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public Guid[] Consumers { get; set; }
        [DefaultValue(true)]
        public bool IsPublic { get; set; }
        public Guid CreatorToken { get; set; }
        public DateTime CreationDate { get; set; }
        public UploadStatus UploadStatus { get; set; }
        public string Status { get { return this.UploadStatus.GetStringValue(); } }
        public string Source { get; set; }
        public int DownloadCount { get; set; }
        public DateTime? LastAccess { get; set; }
    }

    internal class _FileItem
    {
        public string FileId { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        [DefaultValue(true)]
        public bool IsPublic { get; set; }
        public Guid CreatorToken { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
    }

    public enum UploadStatus
    {
        Unknown = -1,
        Uploading = 0,
        Completed = 1,
        Failed = 2
    }

}

