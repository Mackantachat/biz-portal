using BizPortal.Areas.WebApi.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;
using BizPortal.th.go.moph.fda.privus;
using EGA.Owin.Security.Utils.Extensions;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class FDAController : ApiControllerBase
    {
        private static string getAccessToken(string ConsumerKey, string ConsumerSecret, string AgentID)
        {
            string DGA_WS_URL = ConfigurationManager.AppSettings["DGA_WS_URL"].ToString();
            string token = string.Empty;
            String reqTokenUrl = String.Format("{0}/ws/auth/validate?ConsumerSecret={1}&AgentID={2}", DGA_WS_URL, ConsumerSecret, AgentID);
            HttpWebRequest getTokenReq = WebRequest.Create(reqTokenUrl) as HttpWebRequest;
            getTokenReq.Headers.Add("Consumer-Key", ConsumerKey);
            try
            {
                using (HttpWebResponse oAuthResp = getTokenReq.GetResponse() as HttpWebResponse)
                {
                    StreamReader sReader = new StreamReader(oAuthResp.GetResponseStream());
                    string tokenResponse = sReader.ReadToEnd();
                    token = (string)JObject.Parse(tokenResponse)["Result"];
                }
            }
            catch (Exception e)
            {
                token = string.Empty;
            }
            return token;
        }
        private static string getFdaAuthenURL(FIELDS_AUTHEN_BYPASS FIELDS_AUTHEN)
        {
            System.Threading.Thread.Sleep(1500);
            string AuthUrl = string.Empty;
            //string AuthUrl = serv.BIZPORTAL_URL(new th.go.moph.fda.privus.FIELDS_AUTHEN_BYPASS
            //{
            //    CTZNO = "1100200502716",
            //    CTZNO_NAME = "นาย ทดสอบ สพร",
            //    TOKEN_KEY = "PlAr2dxmz6GpOqsWW8ofIAUU",
            //    CRATE_DATE = DateTime.Now,
            //    EMAIL = "EMAIL",
            //    MOBILE = "089123456789",
            //    TRADER_NO = "0000000000000",
            //    SYSTEM_ID = "5318",
            //    GROUP_ID = "220484",
            //    URL_APP = "http://COSMETICA.FDA.MOPH.GO.TH/FDA_CMT_DEMO_2/Authen_Login/FRM_Authen_Login_Customer.aspx"
            //});
            WS_AUTHEN_BYPASS serv = new WS_AUTHEN_BYPASS();
            AuthUrl = serv.BIZPORTAL_URL(FIELDS_AUTHEN);
            //string url = string.Empty;
            return AuthUrl;
        }
        [Route("Api/v2/FDA/EncryptByCitizenIDFdaFormat")]
        [HttpGet]
        public object EncryptByCitizenIDFdaFormat(string CitizenID)
        {
            int counts = 0;
            //WS_AUTHEN_BYPASS serv = new WS_AUTHEN_BYPASS();
            //string AuthUrl = serv.BIZPORTAL_URL(new th.go.moph.fda.privus.FIELDS_AUTHEN_BYPASS
            //{
            //    CTZNO = "1100200502716",
            //    CTZNO_NAME = "นาย ทดสอบ สพร",
            //    TOKEN_KEY = "PlAr2dxmz6GpOqsWW8ofIAUU",
            //    CRATE_DATE = DateTime.Now,
            //    EMAIL = "EMAIL",
            //    MOBILE = "089123456789",
            //    TRADER_NO = "0000000000000",
            //    SYSTEM_ID = "5318",
            //    GROUP_ID = "220484",
            //    URL_APP = "http://COSMETICA.FDA.MOPH.GO.TH/FDA_CMT_DEMO_2/Authen_Login/FRM_Authen_Login_Customer.aspx"
            //});


            //CitizenID = "1100200502716";
            //CitizenID = "3439900076665";
            string CitizenName = User.Identity.GetUserProfile().FullName;
            string CitizenEmail = User.Identity.GetUserProfile().Email;
            string CitizenMobile = "";
            var identityName = User.Identity.IsAuthenticated ? User.Identity.GetClaimValueOrDefault("UserDisplayName", User.Identity.Name) : "";
            string TOKEN = ConfigurationManager.AppSettings["FDA_TOKEN_KEY"].ToString();
            string GlobalPathCer = ConfigurationManager.AppSettings["PATH_BIZ_CER"].ToString();
            string FDA_URL_GET_PERMISSION = ConfigurationManager.AppSettings["FDA_URL_GET_PERMISSION"].ToString();
            string GlobalStrFormat = "<?xml version='1.0' encoding='utf-8'?><CLS_PERMISSION xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'><CTZNO>{0}</CTZNO><TRADER_NO>{1}</TRADER_NO><PERSON_TYPE>{2}</PERSON_TYPE></CLS_PERMISSION>";
            string text = string.Format(GlobalStrFormat, CitizenID, "", "1");//1 ผู็ประกอบการ
            string encryptetext = string.Empty;
            string decrypttext = string.Empty;
            string result = string.Empty;
            bool IsSuccess = false;
            try
            {
                encryptetext = BizPortal.Utils.FDAUtility.FDA_ENCRYPT_DATA(text, GlobalPathCer);




                string DGA_WS_URL = ConfigurationManager.AppSettings["DGA_WS_URL"].ToString();
                string ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
                string ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
                string egovToken = getAccessToken(ConsumerKey, ConsumerSecret, "BizAPI");
                HttpWebRequest httpWebRequest = WebRequest.Create(DGA_WS_URL + FDA_URL_GET_PERMISSION) as HttpWebRequest;
                httpWebRequest.Headers.Add("TOKENKEY", TOKEN);
                httpWebRequest.Headers.Add("Consumer-Key", ConsumerKey);
                httpWebRequest.Headers.Add("Token", egovToken);
                httpWebRequest.Method = "POST";
                byte[] data = Encoding.ASCII.GetBytes(encryptetext);
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                //httpWebRequest.ContentType = "text/plain";
                httpWebRequest.ContentLength = data.Length;
                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = myHttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);
                result = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                responseStream.Close();
                myHttpWebResponse.Close();

                IsSuccess = true;
            }
            catch (Exception e)
            {
                result = e.Message.ToString();
                IsSuccess = false;
                //throw new CryptographicException("Unable to open key file. or encrypt data");
            }
            string RESULT_CODE = string.Empty;
            string RESULT_DES = string.Empty;
            string RESULT_DATA = string.Empty;
            List<FADResponseDetail> ListResponseDetails = new List<FADResponseDetail>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/CLS_RESULT");
                RESULT_CODE = nodes[0]["RESULT_CODE"].InnerText;
                RESULT_DES = nodes[0]["RESULT_DES"].InnerText;
                if (!String.IsNullOrEmpty(nodes[0]["RESULT_DATA"].InnerText))
                {
                    RESULT_DATA = nodes[0]["RESULT_DATA"].InnerText;
                }

                if (RESULT_CODE == "200" && !String.IsNullOrEmpty(RESULT_DATA))
                {

                    decrypttext = BizPortal.Utils.FDAUtility.FDA_DECRYPT_DATA(nodes[0]["RESULT_DATA"].InnerText);

                    RESULT_DATA = decrypttext;
                    IsSuccess = true;
                    XmlDocument permiss = new XmlDocument();
                    permiss.LoadXml(RESULT_DATA);
                    XmlNodeList nodespermiss = permiss.DocumentElement.SelectNodes("/PERMISSION_DATA");
                    XmlNodeList nodespermisslist = permiss.DocumentElement.SelectNodes("/PERMISSION_DATA/PERMISSION_ITEM/PERMISSION_DATA_LIST");

                    counts = nodespermisslist.Count;
                    if (nodespermisslist.Count > 0)
                    {
                        for (int k = 0; k < nodespermisslist.Count; k++)
                        {
                            //System.Threading.Thread.Sleep(2000);
                            string authurl = string.Empty;
                            FIELDS_AUTHEN_BYPASS fab = new FIELDS_AUTHEN_BYPASS();
                            fab.CRATE_DATE = DateTime.Now;
                            fab.CTZNO = CitizenID;
                            fab.CTZNO_NAME = CitizenName;
                            fab.EMAIL = CitizenEmail;
                            fab.GROUP_ID = nodespermisslist[k]["GROUP_ID"].InnerText.ToString();
                            fab.MOBILE = CitizenMobile;
                            fab.SYSTEM_ID = nodespermisslist[k]["SYSTEM_ID"].InnerText.ToString();
                            fab.TOKEN_KEY = TOKEN;
                            fab.TRADER_NO = nodespermisslist[k]["TRADER_NO"].InnerText.ToString();
                            fab.URL_APP = nodespermisslist[k]["URL"].InnerText.ToString();
                            authurl = nodespermisslist[k]["URL"].InnerText.ToString();
                            try
                            {
                                authurl = getFdaAuthenURL(fab);
                            }
                            catch
                            {

                                authurl = getFdaAuthenURL(fab);

                            }
                            //authurl = nodespermisslist[k]["URL"].InnerText.ToString();

                            //System.Threading.Thread.Sleep(1000);
                            ListResponseDetails.Add(new FADResponseDetail
                            {

                                URL_LIST = "<a target='_blank' href='" + authurl + "'>" + nodespermisslist[k]["SYSTEM_NAME"].InnerText.ToString() + " (" + nodespermisslist[k]["SYSTEM_NAME"].InnerText.ToString() + ") </a>"
                            });
                        }
                    }
                    else
                    {
                        IsSuccess = false;
                    }


                }
                else
                {
                    IsSuccess = false;
                }
            }
            catch (Exception e)
            {
                decrypttext = e.Message;
            }
            // To convert JSON text contained in string json into an XML node
            //XmlDocument docx = JsonConvert.DeserializeXmlNode(json);

            //var Result = new
            //{
            //    data = new { Code = RESULT_CODE, Msg = RESULT_DES, Value = RESULT_DATA },
            //    Msg = result,
            //    Success = IsSuccess

            //};          
            FADResponse FADResponse = new FADResponse()
            {
                Code = RESULT_CODE,
                Msg = RESULT_DES,
                Success = IsSuccess,
                ResponseDetail = ListResponseDetails,
                //Counts= counts
            };

            return FADResponse;
        }
        public class FADResponse
        {
            //public FADResponseDetail ResponseDetail { get; set; }
            public List<FADResponseDetail> ResponseDetail { get; set; }
            public string Code { get; set; }
            public string Msg { get; set; }
            public bool Success { get; set; }
            public int Counts { get; set; }
        }
        public class FADResponseDetail
        {

            //public string TRADER_NAME { get; set; }
            //public string TRADER_NO { get; set; }
            //public string GROUP_ID { get; set; }
            //public string SYSTEM_ID { get; set; }
            //public string SYSTEM_NAME { get; set; }
            //public string URL { get; set; }
            public string URL_LIST { get; set; }




            //public string TOKEN_KEY { get; set; }
            //public DateTime CRATE_DATE { get; set; }
            //public string CTZNO { get; set; }
            //public string CTZNO_NAME { get; set; }
            //public string EMAIL { get; set; }
            //public string MOBILE { get; set; }
            //public string TRADER_NO { get; set; }
            //public string SYSTEM_ID { get; set; }
            //public string GROUP_ID { get; set; }
            //public string URL_APP { get; set; }
        }

        //public class FAD_FIELDS_AUTHEN_BYPASS
        //{
        //    public string TOKEN_KEY { get; set; }
        //    public string CRATE_DATE { get; set; }
        //    public string CTZNO { get; set; }
        //    public string CTZNO_NAME { get; set; }
        //    public string EMAIL { get; set; }
        //    public string MOBILE { get; set; }
        //    public string TRADER_NO { get; set; }
        //    public string SYSTEM_ID { get; set; }
        //    public string GROUP_ID { get; set; }
        //    public string URL_APP { get; set; }
        //}  //public class FAD_FIELDS_AUTHEN_BYPASS
        //{
        //    public string TOKEN_KEY { get; set; }
        //    public string CRATE_DATE { get; set; }
        //    public string CTZNO { get; set; }
        //    public string CTZNO_NAME { get; set; }
        //    public string EMAIL { get; set; }
        //    public string MOBILE { get; set; }
        //    public string TRADER_NO { get; set; }
        //    public string SYSTEM_ID { get; set; }
        //    public string GROUP_ID { get; set; }
        //    public string URL_APP { get; set; }
        //}  //public class FAD_FIELDS_AUTHEN_BYPASS
        //{
        //    public string TOKEN_KEY { get; set; }
        //    public string CRATE_DATE { get; set; }
        //    public string CTZNO { get; set; }
        //    public string CTZNO_NAME { get; set; }
        //    public string EMAIL { get; set; }
        //    public string MOBILE { get; set; }
        //    public string TRADER_NO { get; set; }
        //    public string SYSTEM_ID { get; set; }
        //    public string GROUP_ID { get; set; }
        //    public string URL_APP { get; set; }
        //}
    }
}