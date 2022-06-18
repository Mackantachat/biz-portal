using BizPortal.DAL;
using BizPortal.Models;
using BizPortal.Utils.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BizPortal.Utils
{
    public class EDocument
    {
        private string consumerKey { get; set; }

        private string token { get; set; }

        private Application application { get; set; }

        public EDocument(int applicatonId, string agentId = "BizAgent")
        {
            var db = new ApplicationDbContext();
            var application = db.Applications.Where(e => e.ApplicationID == applicatonId && !e.IsDeleted).FirstOrDefault();

            if (application != null && !string.IsNullOrEmpty(application.ELicenseConsumerKey) && !string.IsNullOrEmpty(application.ELicenseSecret))
            {
                this.application = application;
                this.consumerKey = application.ELicenseConsumerKey;
                this.token = getApiToken(application.ELicenseConsumerKey, application.ELicenseSecret, agentId);
            }
            else
            {
                throw new Exception("ไม่พบ ELicense ConsumerKey");
            }
        }

        public string RenderDocument(string templateId, JObject data, List<SigningPerson> signers, bool isRenderSignature = false)
        {
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["DGA_WS_URL"]);
                var request = new RestRequest(ConfigurationManager.AppSettings["RenderTemplate"] + "?TemplateID=" + templateId, Method.PUT);

                var json = getJSON(data, signers, isRenderSignature);
                var link = getLink(data);
                var xml = getXML(data, signers, isRenderSignature);

                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Consumer-Key", consumerKey);
                request.AddHeader("Token", token);
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AddFileBytes("Content", json, "data.json", "application/json");
                request.AddFileBytes("Attachment", xml, "data.xml", "text/xml");
                request.AddParameter("Link", link);

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content.Contains("DocumentID"))
                {
                    var obj = JObject.Parse(response.Content);
                    return obj["DocumentID"].ToString();
                }
                else
                {
                    throw new Exception("ไม่สามารถสร้างเอกสารได้");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string RegisterDocument(string templateId, string fileName, string contentType, string base64Data, string link ="")
        {
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["DGA_WS_URL"]);
                var request = new RestRequest(ConfigurationManager.AppSettings["RegisterTemplate"] + "?TemplateID=" + templateId, Method.PUT);

                request.AddHeader("Consumer-Key", consumerKey);
                request.AddHeader("Token", token);
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AddFileBytes("Content", Convert.FromBase64String(base64Data), fileName, contentType);
                request.AddParameter("Link", link);

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content.Contains("DocumentID"))
                {
                    var obj = JObject.Parse(response.Content);
                    return obj["DocumentID"].ToString();
                }
                else
                {
                    throw new Exception("ไม่สามารถสร้างเอกสารได้");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string OrganizationSigning(string documentID)
        {
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["DGA_WS_URL"]);
                var request = new RestRequest(ConfigurationManager.AppSettings["SigningOrganization"], Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Consumer-Key", consumerKey);
                request.AddHeader("Token", token);
                request.AddBody(new { DocumentID = documentID });

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content.Contains("SignatureID"))
                {
                    var obj = JObject.Parse(response.Content);
                    return obj["SignatureID"].ToString();
                }
                else
                {
                    throw new Exception("ไม่สามารถลงนามเอกสารแบบหน่วยงานได้");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public byte[] DownloadDocument(string documentID)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["DGA_WS_URL"]);
            var request = new RestRequest(ConfigurationManager.AppSettings["SigningDownload"] + "?DocumentID=" + documentID, Method.GET);

            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Consumer-Key", consumerKey);
            request.AddHeader("Token", token);

            return client.DownloadData(request);
        }

        private string getApiToken(string consumerKey, string secret, string agentId)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["DGA_WS_URL"]);
            var request = new RestRequest("ws/auth/validate?ConsumerSecret=" + secret + "&AgentID=" + agentId, Method.GET);
            request.AddHeader("Consumer-Key", consumerKey);

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content.Contains("Result"))
            {
                var obj = JObject.Parse(response.Content);
                return obj["Result"].ToString();
            }
            else
            {
                throw new Exception("ไม่สามารถขอ Token จาก api.egov.go.th ได้");
            }
        }

        private byte[] getJSON(JObject data, List<SigningPerson> signers, bool isRenderSignature)
        {
            // เพิ่ม signature
            if (signers != null && signers.Count > 0)
            {
                var i = 0;
                var date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th")).ToThaiDigit();

                data["Signatures"] = JArray.FromObject(signers.Select(e => new
                {
                    who = e.FirstName + " " + e.LastName,
                    position = e.Position,
                    date = isRenderSignature ? date : ""
                }).ToArray());




                if (isRenderSignature)
                {
                    if (!data.ContainsKey("Images"))
                    {
                        data["Images"] = JObject.FromObject(new { });
                    }

                    foreach (var signer in signers)
                    {
                        data["Images"]["SignaturePhoto" + i] = Regex.Replace(signer.SignatureBase64, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                        i++;
                    }
                }
            }

            var json = JsonConvert.SerializeObject(data);
            var jsonBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            var jsonByte = Convert.FromBase64String(jsonBase64);

            return jsonByte;
        }

        private byte[] getXML(JObject data, List<SigningPerson> signers, bool isRenderSignature)
        {
            var template = (JObject)null;
            var signer = signers.FirstOrDefault();

            // ลบ image ทิ้งใน xml ไม่ได้ใช้
            if (data.ContainsKey("Images"))
            {
                data.Remove("Images");
            }

            // ลบ link ทิ้งใน xml ไม่ได้ใช้
            if (data.ContainsKey("Links"))
            {
                data.Remove("Links");
            }

            // ลบ checkbox ทิ้งใน xml ไม่ได้ใช้
            if (data.ContainsKey("Checkbox"))
            {
                data.Remove("Checkbox");
            }

            // ลบ signature ทิ้งใน xml ใช้คนละ format
            if (data.ContainsKey("Signatures"))
            {
                data.Remove("Signatures");
            }

            switch (Enum.Parse(typeof(EdocumentXMLStandard), application.ELicenseXMLStandard))
            {
                case EdocumentXMLStandard.hl7:
                    template = JObject.Parse("{\"Bundle\":{\"@xmlns:xhtml\":\"http://www.w3.org/1999/xhtml\",\"@xmlns\":\"http://hl7.org/fhir\",\"@xmlns:xsi\":\"http://www.w3.org/2001/XMLSchema-instance\",\"@xsi:schemaLocation\":\"http://hl7.org/fhir file:../schema/fhir/bundle.xsd\",\"#comment\":[],\"language\":{\"@value\":\"TH\"},\"type\":{\"@value\":\"document\"},\"entry\":{\"resource\":null},\"signature\":null}}");
                    template["Bundle"]["entry"]["resource"] = data;
                    template["Bundle"]["signature"] = JObject.FromObject(new
                    {
                        when = new
                        {
                            attr_value = isRenderSignature ? DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss-zzz") : ""
                        },
                        who = new
                        {
                            display = new
                            {
                                attr_value = signer.FirstName + " " + signer.LastName
                            }
                        },
                        sigFormat = new
                        {
                            attr_value = "application/signature+xml"
                        },
                        data = new
                        {
                            attr_value = ""
                        },
                    }); ;

                    break;

                case EdocumentXMLStandard.UNCEFACT:
                    template = JObject.Parse("{\"rsm:OCPBDirectSellPermit\":{\"@xmlns:rsm\":\"urn:un:unece:uncefact:data:standard:OCPBDirectSellPermit:1\",\"@xmlns:ram\":\"urn:un:unece:uncefact:data:standard:OCPBDirectSellPermit_ReuseableAggregateCoreComponent:1\",\"@xmlns:qdt\":\"urn:un:unece:uncefact:data:Standard:QualifiedDataType:21\",\"@xmlns:udt\":\"urn:un:unece:uncefact:data:standard:UnqualifiedDataType:21\",\"@xmlns:xsi\":\"http://www.w3.org/2001/XMLSchema-instance\",\"@xsi:schemaLocation\":\"urn:un:unece:uncefact:data:standard:OCPBDirectSellPermit:1 file:../schema/etda/standard/OCPBDirectSellPermit_1p0.xsd\"}}");
                    template["rsm:OCPBDirectSellPermit"]["rsm:ExchangedDocumentContext"] = JObject.FromObject(data)["rsm__ExchangedDocumentContext"];
                    template["rsm:OCPBDirectSellPermit"]["rsm:ExchangedDocument"] = JObject.FromObject(data)["rsm__ExchangedDocument"];
                    template["rsm:OCPBDirectSellPermit"]["rsm:ExchangedDocument"]["ram__SignatoryAuthentication"] = JObject.FromObject(new
                    {
                        ram__ActualDateTime = new
                        {
                            udt__DateTime = isRenderSignature ? DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss-zzz") : ""
                        },
                        ram__ProviderParty = new
                        {
                            ram__Name = signer.FirstName + " " + signer.LastName,
                            ram__Description = signer.Position
                        }
                    });

                    break;
            }

            if (template != null)
            {
                var json = JsonConvert.SerializeObject(template).Replace("attr_", "@").Replace("__", ":"); ;
                var xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" + JsonConvert.DeserializeXNode(json).ToString();
                var xmlBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
                var xmlByte = Convert.FromBase64String(xmlBase64);

                return xmlByte;
            }
            else
            {
                throw new Exception("ไม่สามารถสร้างเอกสารได้เนื่องจาก XML ไม่ถูกต้อง");
            }
        }

        private string getLink(JObject data) 
        {
            // org link
            if (data.ContainsKey("Links"))
            {
                return data["Links"].Value<string>("QrCode") ?? "";
            }

            return "";
        }
    }
}
