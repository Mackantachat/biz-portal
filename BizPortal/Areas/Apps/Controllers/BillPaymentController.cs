using BizPortal.Utils.Annotations;
using BizPortal.Utils.Helpers;
using EGA.WS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Areas.Apps.Controllers
{
    public class BillPaymentController : AppsControllerBase
    {
        // GET: Apps/BillPayment

        private readonly string consumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
        private readonly string consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
        
        [AuthorizeUser(OpenIDUserType = "JuristicPerson,Citizen")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void  Downloadfile()
        {
            //var BizToken = string.Empty;
            //var DocID = string.Empty;
            //// https://api.egov.go.th/ws/auth/validate?ConsumerSecret=6VZxqW0d7sE&AgentID=bizportal



            string content = string.Empty;

            //        {
            //            "LicenseName": "",
            //"LicenseDateTime": "",
            //"OrganizeName": "",
            //"FirstName": "",
            //"LastName": ""


            //Doc data = new Doc();
            //data.LicenseName = "LicenseName1";
            //data.LicenseDateTime = "14-07-2020";
            //data.OrganizeName = "DGa";
            //data.FirstName = "athit";
            //data.LastName = "LastName";



            DocBody data = new DocBody();

            data.OrgNameTH = "สพร.";
            data.OrgNameEN = "DGA";
            //data.HomeCostCenter = "HomeCostCenter";
            data.Name = "BizPortal BillPayment";
            data.BillPaymentCode = "001";
            data.InvoiceStartDate = "14-07-2020";
            data.InvoiceEndDate = "17-07-2020";
            data.CGDRef1 = "Ref1";
            data.CGDRef2 = "Ref2";
            data.Amount = "500";


            data.ItemName1 = "Name1";
            data.ItemAmount1 = "100";


            data.ItemName2 = "Name2";
            data.ItemAmount2 = "100";


            data.ItemName3 = "Name3";
            data.ItemAmount3 = "100";

            data.ItemName4 = "Name4";
            data.ItemAmount4 = "100";

            data.ItemName5 = "Name5";
            data.ItemAmount5 = "100";

            data.AmountThai = "ห้าร้อยบาท";
            data.AmountEng = "Five Hundred";

            data.InvoiceCreateDate = "16-07-2020";
            data.BillerID = "001";

            content = JsonConvert.SerializeObject(data);



            var arg = new Dictionary<string, string> ();


            arg.Add("FileID", "BD7s6U39GR8HL");
            arg.Add("Content", (content != null ? content.ToString() : "{}"));

            var initResult = Api.Call("/report/init", HttpVerb.PUT, arg);
            var printToken = initResult["Result"].ToString();
            var pobjPrint = Api.Get(string.Format("/report/render" + "?key={0}&type=pdf", printToken));


            if (pobjPrint != null && pobjPrint["List"] != null)
            {
              

                using (var stream = new MemoryStream())
                {
                    Response.Clear();



                    string base64URL = QRAndBarCodeHelper.Base64urlStringToBase64String(pobjPrint["List"][0].ToString());
                    byte[] base64Byte = QRAndBarCodeHelper.Base64urlStringToByte(base64URL);


                    stream.Write(base64Byte, 0, base64Byte.Length);
                    stream.Position = 0;

                    //var result = new HttpResponseMessage(HttpStatusCode.OK)
                    //{
                    //    Content = new ByteArrayContent(stream.ToArray())
                    //};
                    //result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    //{
                    //    FileName = "Billpayment.pdf"
                    //};
                    //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    //return result;

                    string contentDisposition;
                    string fileName = "Billpayment";
                    Response.ContentType = "application/pdf";


                  
                   
                    contentDisposition = string.Format("attachment;filename={0}.pdf", fileName);
                    Response.AddHeader("Content-Disposition", contentDisposition);
                    Response.Buffer = true;
                    stream.WriteTo(Response.OutputStream);
                    Response.End();


                }
            }
            else
            {
                throw new Exception("ไม่สามารถสร้างไฟล์ Bill Payment ได้");
            }



          

        }


        public string Base64StringToBase64urlString(string base64string)
        {
            byte[] convertByte = Convert.FromBase64String(base64string);
            return ByteToBase64urlString(convertByte);
        }
        public string Base64urlStringToBase64String(string base64urlstring)
        {
            byte[] convertData = Base64urlStringToByte(base64urlstring);
            return Convert.ToBase64String(convertData);
        }
        public static string ByteToBase64urlString(byte[] input)
        {
            StringBuilder result = new StringBuilder(Convert.ToBase64String(input).TrimEnd('='));
            result.Replace('+', '-');
            result.Replace('/', '_');
            return result.ToString();
        }
        public static byte[] Base64urlStringToByte(string base64ForUrlInput)
        {
            int padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));
            StringBuilder result = new StringBuilder(base64ForUrlInput, base64ForUrlInput.Length + padChars);
            result.Append(String.Empty.PadRight(padChars, '='));
            result.Replace('-', '+');
            result.Replace('_', '/');
            return Convert.FromBase64String(result.ToString());
        }
    }

    class Doc
    {


  
            public string LicenseName { get; set; }
            public string LicenseDateTime { get; set; }
            public string OrganizeName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
         
    }
    class DocBody
    {

        public string OrgNameTH { get; set; }
        public string OrgNameEN { get; set; }
        public string HomeCostCenter { get; set; }
        public string Name { get; set; }
        public string BillPaymentCode { get; set; }
        public string InvoiceStartDate { get; set; }
        public string InvoiceEndDate { get; set; }
        public string CGDRef1 { get; set; }
        public string CGDRef2 { get; set; }
        public string Amount { get; set; }


        public string ItemName1 { get; set; }
        public string ItemAmount1 { get; set; }

        public string ItemName2 { get; set; }
        public string ItemAmount2 { get; set; }

        public string ItemName3 { get; set; }
        public string ItemAmount3 { get; set; }

        public string ItemName4 { get; set; }
        public string ItemAmount4 { get; set; }


        public string ItemName5 { get; set; }
        public string ItemAmount5 { get; set; }


        public string AmountThai { get; set; }
        public string AmountEng { get; set; }

        public string InvoiceCreateDate { get; set; }
        public string BillerID { get; set; }

    }
}