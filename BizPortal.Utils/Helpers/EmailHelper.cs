using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mail;
using System.ComponentModel.DataAnnotations;
using EGA.EGA_Development.Util.MailV2.Data;
using EGA.EGA_Development.Util.MailV2;
using System.IO;
using BizPortal.Models.Reports;


namespace BizPortal.Utils.Helpers
{
    public static class EmailHelper
    {
        [Obsolete("เปลี่ยนไปใช้ SendEmailV2")]
        public static bool SendEmail(string to, string subject, bool isBodyHtml, string body, bool isNoreply, string sender = null, string senderName = null, MailAttachment[] attachments = null)
        {
            return SendEmail(new string[] { to }, subject, isBodyHtml, body, isNoreply, sender, senderName, attachments);
        }

        [Obsolete("เปลี่ยนไปใช้ SendEmailV2")]
        public static bool SendEmail(string[] to, string subject, bool isBodyHtml, string body, bool isNoreply, string sender = null, string senderName = null, MailAttachment[] attachments = null)
        {
            try
            {
                if (ConfigurationValues.TestMode)
                {
                    to = new string[] { ConfigurationValues.TestEmail };
                }

                MailMessage msg = new MailMessage();
                // More configuration : https://msdn.microsoft.com/en-us/library/ms872853(v=exchg.65).aspx
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", BizPortalSetting.MailSMTP);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 465); //old setting port 587 
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2); // Sending over network 
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1); // Auth Required : See more : https://support.microsoft.com/en-us/kb/555287
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", BizPortalSetting.MailUser);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", BizPortalSetting.MailPwd);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", true); // Using SSL

                if (!string.IsNullOrEmpty(sender))
                {
                    msg.From = sender;
                }
                else
                {
                    msg.From = BizPortalSetting.MailSender;
                }

                if (to.Length > 0)
                {
                    msg.To = string.Join(",", to);
                }

                msg.Subject = subject;
                msg.Body = body;
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.BodyFormat = isBodyHtml ? MailFormat.Html : MailFormat.Text;

                // Sending
                SmtpMail.SmtpServer = BizPortalSetting.MailSMTP;
                SmtpMail.Send(msg);
                return true;
            }
            catch (Exception exc)
            {
                //TODO: Keep Log
                return false;
            }
        }

        public static bool SendEmailV2(IEnumerable<MailAddress> toAddr, IEnumerable<MailAddress> ccAddr, string subject, string body, bool isBodyHtml = true,
            IEnumerable<MailAddress> senderAddr = null, IEnumerable<MailAddress> replyToAddr = null,
            IEnumerable<Attachment> attachments = null)
        {
            try
            {
                if (ConfigurationValues.TestMode)
                {
                    //toAddr = new MailAddress[]
                    //{
                    //    toAddr.First() ?? new MailAddress { Name = ConfigurationValues.TestEmail, Address = ConfigurationValues.TestEmail },
                    //    new MailAddress { Name = ConfigurationValues.TestEmail, Address = ConfigurationValues.TestEmail }
                    //};
                    subject = string.Format("[UAT] {0}", subject);
                }

                if (senderAddr == null || !senderAddr.Any())
                {
                    senderAddr = new MailAddress[] { new MailAddress() { Address = BizPortalSetting.MailSender, Name = "Biz Portal" } };
                }

                //if (replyToAddr == null || !replyToAddr.Any())
                //{
                //    replyToAddr = new MailAddress[] { new MailAddress() { Address = "contact@dga.or.th", Name = "DGA Contact" } };
                //}

                SmtpClient client = new SmtpClient(BizPortalSetting.MailUser, BizPortalSetting.MailPwd);
                if (ccAddr != null && ccAddr.Count() > 0)
                {
                    client.Send(toAddr, ccAddr, null, subject, body, isBodyHtml, senderAddr, replyToAddr, attachments);
                }
                else
                {
                    client.Send(toAddr, subject, body, isBodyHtml, senderAddr, replyToAddr, attachments);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool SendEmailV2(IEnumerable<MailAddress> toAddr, string subject, string body, bool isBodyHtml = true,
            IEnumerable<MailAddress> senderAddr = null, IEnumerable<MailAddress> replyToAddr = null,
            IEnumerable<Attachment> attachments = null)
        {
            return SendEmailV2(toAddr, null, subject, body, isBodyHtml, senderAddr, replyToAddr, attachments);
        }

        public static bool SendJuristicRequestApprovedEmail(string to, string body, string juristicName, DateTime approveDate, string remark, string trackURL, Dictionary<string, string> attachFiles)
        {
            if (attachFiles == null)
                attachFiles = new Dictionary<string, string>();
            string header = Resources.ApplicationStatusRequests.MAIL_JURISTIC_REQUEST_APPROVED;
            attachFiles.Add("JURISTIC_NAME", juristicName);
            attachFiles.Add("JURISTIC_REMARK", remark);
            attachFiles.Add("JURISTIC_TRACK_URL", trackURL);
            //attachFiles.Add("APPROVED_DATE", approveDate.ToString("dd/MM/yyyy HH:mm:ss"));

            body = DynamicMergeEmail(body, attachFiles);

            //return SendEmail(new string[] { to }, header, true, body, true);
            return SendEmailV2(new MailAddress[] { new MailAddress { Name = to, Address = to } }, header, body, true);
        }

        public static bool SendJuristicRequestRejectedEmail(string to, string body, string juristicName, DateTime approveDate, string remark, string trackURL, Dictionary<string, string> attachFiles)
        {
            if (attachFiles == null)
                attachFiles = new Dictionary<string, string>();
            string header = Resources.ApplicationStatusRequests.MAIL_JURISTIC_REQUEST_REJECTED;
            attachFiles.Add("JURISTIC_NAME", juristicName);
            attachFiles.Add("JURISTIC_REMARK", remark);
            attachFiles.Add("APPLICATION_URL", trackURL);
            //attachFiles.Add("APPROVED_DATE", approveDate.ToString("dd/MM/yyyy HH:mm:ss"));
            body = DynamicMergeEmail(body, attachFiles);

            //return SendEmail(new string[] { to }, header, true, body, true);
            return SendEmailV2(new MailAddress[] { new MailAddress { Name = to, Address = to } }, header, body, true);
        }

        public static bool SendRequestSubmitEmail(string email, ConfirmationFormJuristicPDFModel model, string url)
        {
            var rand = new Random();
            string randomFolder = rand.Next(10000).ToString("00000");
            if (!Directory.Exists(Path.Combine(System.Web.HttpRuntime.CodegenDir, randomFolder)))
            {
                Directory.CreateDirectory(Path.Combine(System.Web.HttpRuntime.CodegenDir, randomFolder));
            }
            string filename = Path.Combine(System.Web.HttpRuntime.CodegenDir, randomFolder, model.PreferFileName);
          
            File.WriteAllBytes(filename, iTextReportHelper.GetConfirmationFormJuristicReport(model));
            var attachment = new FileAttachment
            {
                ContentType = "application/pdf",
                FileName = model.PreferFileName,
                Path = filename,
            };
            string mailSubject = "คุณยื่นคำขอผ่านระบบ Biz Portal เวลา {0} น. วันที่ {1} เป็นที่เรียบร้อยแล้ว";
            mailSubject = string.Format(mailSubject, model.RequestDateTime.ToString("HH.mm"), model.RequestDateTime.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("th-TH")));
            var appList = "";
            int i = 1;


            foreach (var r in model.Requests)
            {     
               //appList += i + ". " + r.ApplicationName + "<br/>";
                if(r.RequestStatus != ApplicationStatusV2Enum.FAILED)
                {
                    appList += $"เลขที่คำขอ:{r.RequestID}<br/>ใบอนุญาต:{r.ApplicationName}<br/>หน่วยงาน:{r.OrgName}<br/>สถานะคำขอ: ยื่นคำขอ <br/>การดำเนินการ: รอเจ้าหน้าที่รับเรื่อง <br/>วันที่ยื่นคำขอ:{model.RequestDateTime.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("th-TH"))} เวลา {model.RequestDateTime.ToString("HH.mm")} น.<br/><br/>";
                    i++;
                }
                  // appList += $"{i}. {r.ApplicationName} (เลขที่คำร้อง/คำขอ: {r.RequestID} สถานะ {requestStatus})";
                
            }
            //เป็นที่เรียบร้อยแล้ว
            var mailBody = "<p>เรียน {2} </p><p>ขณะนี้คำขอของคุณได้ยื่นเรียบร้อยแล้ว รายละเอียดดังนี้ดัง</p>" +
                "<p>{4}</p><p><a href='{5}'>รายละเอียดเพิ่มเติมโปรด คลิกที่นี่ </a>หรือคัดลอกและวางลิงก์ต่อไปนี้ลงในเบราว์เซอร์ของคุณ {5} </p>" +
                "<p><div style='color: grey; font-size: 0.8em'>" + "<p>หมายเหตุ : ทีมงาน Biz Portal ไม่มีนโยบายที่จะสอบถามรหัสการใช้งานต่างๆของคุณ เพื่อความปลอดภัยในการใช้งาน กรุณาเก็บรักษารหัสต่างๆไว้เป็นความลับ<br/>*** อีเมลนี้เป็นการแจ้งจากระบบอัตโนมัติ กรุณาอย่าตอบกลับ ***</p></div>" +
                "<p>สอบถามข้อมูลเพิ่มเติมได้ที่ email contact@dga.or.th หรือโทร 02-612-6060</p><p>ขอบคุณที่ใช้บริการ<br/>BizPortal.go.th";
            mailBody = string.Format(mailBody,
                model.RequestDateTime.ToString("HH.mm"),
                model.RequestDateTime.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("th-TH")),
                model.RequestorName,
                model.Requests.Count,
                appList,
                url
                );
            return SendEmailV2(new MailAddress[] { new MailAddress { Name = email, Address = email } }, mailSubject, mailBody, true, null, null, new Attachment[] { attachment });
        }


        public class StatusUpdateEmailToAgentModel
        {
            public string RequestNumber { get; set; }
            public string PermitName { get; set; }
            public string OrgName { get; set; }
            public string Status { get; set; }
            public string StatusOther { get; set; }
            public string ViewUrl { get; set; }
        }

        public static bool SendCreateRequestEmailToAgent(IEnumerable<MailAddress> emails, StatusUpdateEmailToAgentModel model)
        {
            string mailSubject = "แจ้งมีคำขอใช้บริการจากระบบ BizPortal.go.th";
            var mailBody = "<p>เรียน เจ้าหน้าที่ </p>" +
                //"<p>ทางเราขอเรียนให้ทราบว่ามีคำขอเลขที่ {5} จากผู้ประกอบการเมื่อ เวลา {0} น. วันที่ {1} โดยมีรายละเอียด ดังนี้</p>" +
                "<p>แจ้งมีคำขอใช้บริการ รายละเอียดดังนี้</p>" +
                "<p><b>เลขที่คำขอ :</b> {7} <br/>" +
                "<b>ใบอนุญาต :</b> {2} <br/> " +
                "<b>หน่วยงาน :</b> {3} <br/>" +
                "<b>สถานะคำขอ :</b> {4} <br/> " +
                "<b>การดำเนินการ :</b> {5} <br/>" +
                "<b>วันที่ยื่นคำขอ :</b> {1} เวลา {0} น. <br/></p>" +
                "<p>รายละเอียดเพิ่มเติมโปรด <a href='{6}'>คลิกที่นี่</a> หรือคัดลอกและวางลิงก์ต่อไปนี้ลงในเบราว์เซอร์ของท่าน {6}</p>" +
                "<p>สอบถามข้อมูลเพิ่มเติมได้ที่ email contact@dga.or.th หรือโทร 02-612-6060</p>" +
                "<p> ขอบคุณที่ใช้บริการ <br/> " +
                "<b>BizPortal.go.th</b><br/></p>" +
                "<img height='30px' src='" + ConfigurationValues.BizUrl + "/Content/SingleForm-frontis/v2/images/logo.png' /><div style='color: grey; font-size: 0.8em'>" +
                "<p>หมายเหตุ : ทีมงาน Biz Portal ไม่มีนโยบายที่จะสอบถามรหัสการใช้งานต่างๆของคุณ เพื่อความปลอดภัยในการใช้งาน กรุณาเก็บรักษารหัสต่างๆไว้เป็นความลับ<br/>*** อีเมลนี้เป็นการแจ้งจากระบบอัตโนมัติ กรุณาอย่าตอบกลับ ***</p></div>";
              
            mailBody = string.Format(mailBody,
                DateTime.Now.ToString("HH.mm"),
                DateTime.Now.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("th-TH")),
                model.PermitName,
                model.OrgName,
                model.Status,
                model.StatusOther,
                model.ViewUrl,
                model.RequestNumber
            );
            return SendEmailV2(emails, mailSubject, mailBody, true, null, null); ;
        }


        public static bool SendStatusUpdateEmailToAgent(List<MailAddress> emails, StatusUpdateEmailToAgentModel model)
        {
            string mailSubject = "แจ้งมีการแก้ไขคำขอจากระบบ BizPortal.go.th";
            var mailBody = "<p>เรียน เจ้าหน้าที่ </p>" +
                 //"<p>ทางเราขอเรียนให้ทราบว่าคำขอเลขที่ {7} ได้รับการแก้ไขจากผู้ประกอบการแล้ว เมื่อ เวลา {0} น. วันที่ {1} โดยมีรายละเอียด ดังนี้</p>" +
                 "<p>แจ้งมีการแก้ไขคำขอ รายละเอียดดังนี้</p>" +
                "<p><b>เลขที่คำขอ :</b> {7} <br/>" +
                "<b>ใบอนุญาต :</b> {2} <br/>" +
                "<b>หน่วยงาน :</b> {3} <br/>" +
                "<b>สถานะคำขอ :</b> {4} <br/>" +
                "<b>การดำเนินการ  :</b> {5} <br/>" +
                "<b>วันที่ปรับปรุงสถานะ :</b> {1} เวลา {0} น. <br/></p>" +
                "<p>รายละเอียดเพิ่มเติมโปรด <a href='{6}'>คลิกที่นี่</a> หรือคัดลอกและวางลิงก์ต่อไปนี้ลงในเบราว์เซอร์ของท่าน {6}</p>" +
                "<p>สอบถามข้อมูลเพิ่มเติมได้ที่ email contact@dga.or.th หรือโทร 02-612-6060</p>" +
                "<p> ขอบคุณที่ใช้บริการ <br/> " +
                "<b>BizPortal.go.th</b><br/></p>" +
                "<img height='30px' src='" + ConfigurationValues.BizUrl + "/Content/SingleForm-frontis/v2/images/logo.png' /><div style='color: grey; font-size: 0.8em'>" +
                "<p>หมายเหตุ : ทีมงาน Biz Portal ไม่มีนโยบายที่จะสอบถามรหัสการใช้งานต่างๆของคุณ เพื่อความปลอดภัยในการใช้งาน กรุณาเก็บรักษารหัสต่างๆไว้เป็นความลับ<br/>*** อีเมลนี้เป็นการแจ้งจากระบบอัตโนมัติ กรุณาอย่าตอบกลับ ***</p></div>";
            mailBody = string.Format(mailBody,
                DateTime.Now.ToString("HH.mm"),
                DateTime.Now.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("th-TH")),
                model.PermitName,
                model.OrgName,
                model.Status,
                model.StatusOther,
                model.ViewUrl,
                model.RequestNumber
            );
            return SendEmailV2(emails, mailSubject, mailBody, true, null, null); ;
        }
        public static bool SendNoticeSigningEmailToSigner(List<MailAddress> emails, StatusUpdateEmailToAgentModel model)
        {
            string mailSubject = "ท่านมีเอกสารพิจารณาลงนามในระบบ BizPortal.go.th";
            var mailBody = "<p>เรียน ผู้ลงนาม</p>" +
                "<p>ขณะนี้มีเอกสารเพื่อรอท่านพิจารณาลงนามอิเล็กทรอนิกส์ รายละเอียดดังนี้</p>" +
                //"<p>ทางเราขอเรียนให้ทราบว่าคำขอเลขที่ {5} รอการลงนาม เมื่อ เวลา {0} น. วันที่ {1} โดยมีรายละเอียด ดังนี้</p>" +
                "<p><b>เลขที่คำขอ :</b> {5} <br/>" +
                "<b>ใบอนุญาต :</b> {2} <br/>" +
                "<b>หน่วยงาน :</b> {3} <br/>" +
                "<b>วันที่เสนอลงนาม :</b> {1} เวลา {0} น.<br/>" +
                //"สถานะ : {4} <br/>" +
                //"สถานะย่อย  : {5} <br/>" +
                "</p><p>กรุณาตรวจสอบรายละเอียดและพิจารณาลงนามอิเล็กทรอนิกส์ โดย <a href='{4}'>คลิกที่นี่</a> หรือคัดลอกและวางลิงก์ต่อไปนี้ลงในเบราว์เซอร์ของท่าน {4}</p>" +
                "<p>สอบถามข้อมูลเพิ่มเติมได้ที่ email contact@dga.or.th หรือโทร 02-612-6060/p>" +
                "<p>ขอบคุณที่ใช้บริการ<br/>" +
                "<p><b>BizPortal.go.th</b><br/></p>"+
                "<img height='30px' src='" + ConfigurationValues.BizUrl + "/Content/SingleForm-frontis/v2/images/logo.png' /><div style='color: grey; font-size: 0.8em'>" +
                "<p>หมายเหตุ : ทีมงาน Biz Portal ไม่มีนโยบายที่จะสอบถามรหัสการใช้งานต่างๆของคุณ เพื่อความปลอดภัยในการใช้งาน กรุณาเก็บรักษารหัสต่างๆไว้เป็นความลับ<br/>*** อีเมลนี้เป็นการแจ้งจากระบบอัตโนมัติ กรุณาอย่าตอบกลับ ***</p></div>";
            mailBody = string.Format(mailBody,
                DateTime.Now.ToString("HH.mm"),
                DateTime.Now.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("th-TH")),
                model.PermitName,
                model.OrgName,
                //model.Status,
                //model.StatusOther,
                model.ViewUrl,
                model.RequestNumber
            );
            return SendEmailV2(emails, mailSubject, mailBody, true, null, null); ;
        }
        public static bool SendNoticeSigningEmailToAgent(List<MailAddress> emails, StatusUpdateEmailToAgentModel model)
        {
            string mailSubject = "แจ้งผลการพิจารณาลงนามอิเล็กทรอนิกส์จากระบบ BizPortal.go.th";
            var mailBody = "<p>เรียน เจ้าหน้าที่ </p>" +
                "<p>แจ้งผลการพิจารณาลงนามอิเล็กทรอนิกส์ รายละเอียดดังนี้</p>" +
                //"<p>ทางเราขอเรียนให้ทราบว่าคำขอเลขที่ {7} ได้รับผลการลงนามจากผู้ลงนามแล้ว เมื่อ เวลา {0} น. วันที่ {1} โดยมีรายละเอียด ดังนี้</p>" +
                "<p><b>เลขที่คำขอ :</b> {7} <br/>" +
                "<b>ใบอนุญาต :</b> {2} <br/>" +
                "<b>หน่วยงาน :</b> {3} <br/>" +
                "<b>ผลการพิจารณาลงนาม :</b> {4} <br/>" +
                "<b>หมายเหตุ   :</b> {5} <br/>" +
                 "<b>วันที่พิจารณา:</b> {1} เวลา {0} น.<br/>" +
                "</p><p>กรุณาตรวจสอบรายละเอียดและดำเนินการออกใบอนุญาต โดย <a href='{6}'>คลิกที่นี่</a> หรือคัดลอกและวางลิงก์ต่อไปนี้ลงในเบราว์เซอร์ของท่าน {6}</p>" +
                "<p>สอบถามข้อมูลเพิ่มเติมได้ที่ email contact@dga.or.th หรือโทร 02-612-6060</p>" +
                 "<p>ขอบคุณที่ใช้บริการ<br/>" +
                "<p><b>BizPortal.go.th</b><br/></p>"+
                "<img height='30px' src='" + ConfigurationValues.BizUrl + "/Content/SingleForm-frontis/v2/images/logo.png' /><div style='color: grey; font-size: 0.8em'>" +
                 "<p>หมายเหตุ : ทีมงาน Biz Portal ไม่มีนโยบายที่จะสอบถามรหัสการใช้งานต่างๆของคุณ เพื่อความปลอดภัยในการใช้งาน กรุณาเก็บรักษารหัสต่างๆไว้เป็นความลับ<br/>*** อีเมลนี้เป็นการแจ้งจากระบบอัตโนมัติ กรุณาอย่าตอบกลับ ***</p></div>";
            mailBody = string.Format(mailBody,
                DateTime.Now.ToString("HH.mm"),
                DateTime.Now.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("th-TH")),
                model.PermitName,
                model.OrgName,
                model.Status,
                model.StatusOther,
                model.ViewUrl,
                model.RequestNumber
            );
            return SendEmailV2(emails, mailSubject, mailBody, true, null, null); ;
        }

        public class StatusUpdateEmailModel
        {
            public string RequestorName { get; set; }
            public string PermitName { get; set; }
            public string OrgName { get; set; }
            public string Status { get; set; }
            public string StatusOther { get; set; }
            public string Remark { get; set; }
            public string ViewUrl { get; set; }
            public string RequestNumber { get; set; }
        }

        public static bool SendStatusUpdateEmailToRequestor(string email, StatusUpdateEmailModel model)
        {
            string mailSubject = "แจ้งการเปลี่ยนสถานะคำขอจากระบบ BizPortal.go.th";
            var mailBody = "<p>เรียน {8} </p>" +
                "<p>ขณะนี้คำขอของคุณได้รับการเปลี่ยนสถานะจากเจ้าหน้าที่ รายละเอียดดังนี้</p>" +
                //"<p>ทางเราขอเรียนให้ทราบว่าคำขอของคุณได้รับการเปลี่ยนสถานะจากเจ้าหน้าที่ เมื่อ เวลา {0} น. วันที่ {1} โดยมีรายละเอียด ดังนี้ </p>" +
                "<p><b>เลขที่คำขอ :</b> {9} <br/>" +
                "<b>ใบอนุญาต :</b> {2} <br/>" +
                "<b>หน่วยงาน :</b> {3} <br/>" +
                "<b>สถานะคำขอ :</b> {4} <br/>" +
                "<b>การดำเนินการ :</b> {5} <br/>" +
                "<b>วันที่ปรับปรุงสถานะ :</b> {1} เวลา {0} น. <br/></p>" +
                "<p>รายละเอียดเพิ่มเติมโปรด <a href='{7}'>คลิกที่นี่</a> หรือคัดลอกและวางลิงก์ต่อไปนี้ลงในเบราว์เซอร์ของท่าน {7}</p>" +
                "<p>สอบถามข้อมูลเพิ่มเติมได้ที่ email contact@dga.or.th หรือโทร 02-612-6060</p>" +
                "<p> ขอบคุณที่ใช้บริการ <br/> " +
                "<b>BizPortal.go.th</b><br/></p>" +
                "<img height='30px' src='" + ConfigurationValues.BizUrl + "/Content/SingleForm-frontis/v2/images/logo.png' /><div style='color: grey; font-size: 0.8em'>" +
                "<p>หมายเหตุ : ทีมงาน Biz Portal ไม่มีนโยบายที่จะสอบถามรหัสการใช้งานต่างๆของคุณ เพื่อความปลอดภัยในการใช้งาน กรุณาเก็บรักษารหัสต่างๆไว้เป็นความลับ<br/>*** อีเมลนี้เป็นการแจ้งจากระบบอัตโนมัติ กรุณาอย่าตอบกลับ ***</p></div>";
            mailBody = string.Format(mailBody,
                DateTime.Now.ToString("HH.mm"),
                DateTime.Now.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("th-TH")),
                model.PermitName,
                model.OrgName,
                model.Status,
                model.StatusOther,
                model.Remark,
                model.ViewUrl,
                model.RequestorName,
                model.RequestNumber
            );
            return SendEmailV2(new MailAddress[] { new MailAddress { Name = email, Address = email } }, mailSubject, mailBody, true, null, null);
        }

        public static bool SendJuristicRequestSubmitEmail(string to, string body, DateTime submit, Dictionary<string, string> attachFiles, string[] cc, UserTypeEnum identityType)
        {
            if (attachFiles == null)
                attachFiles = new Dictionary<string, string>();
            string header = Resources.ApplicationStatusRequests.MAIL_JURISTIC_REQUEST_SUBMITED;
            attachFiles.Add("SUBMIT_DATE", submit.ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("th-TH")));
            body = DynamicMergeEmail(body, attachFiles);

            string mailSubject = "คุณยื่นคำขอผ่านระบบ Doing Business Portal เวลา {0} น. วันที่ {1} เป็นที่เรียบร้อยแล้ว";
            mailSubject = string.Format(mailSubject, "11.00", submit.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("th-TH")));

            string mailBody = "<p>เรียน คุณ สมชาย รักษ์การค้า </p><p>ทางเราขอเรียนให้ทราบว่าคุณได้ยื่นคำขอผ่านระบบ Doing Business Portal เวลา {0} น. วันที่ {1} เป็นที่เรียบร้อยแล้ว</p><p>โดยยื่นคำขอทั้งสิ้น 1 รายการ  ดังนี้<br/>1. ขอหนังสือรับรองการแจ้งจัดตั้งสถานที่จำหน่ายหรือสะสมอาหาร (ไม่เกิน 200 ตร.ม.)</p><p>หากคุณต้องการติดตามสถานะคำขอผ่าน Doing Business Portal โปรด<a href='https://biz.govchannel.go.th/th'>คลิกที่นี่</a>หรือวางลิงก์ต่อไปนี้ลงในเบราว์เซอร์ของคุณ https://biz.govchannel.go.th/th/</p><br/><p>ขอขอบพระคุณที่ใช้บริการผ่านระบบ Doing Business Portal<br/>ทีมงาน Doing Business Portal</p><img height='30px' src='https://biz.govchannel.go.th/img/logos/logo-doing-biz-portal.jpg' /><div style='color: grey; font-size: 0.8em'><p>หมายเหตุ : ทีมงาน Doing Business Portal ไม่มีนโยบายที่จะสอบถามรหัสการใช้งานต่างๆของคุณ เพื่อความปลอดภัยในการใช้งาน กรุณาเก็บรักษารหัสต่างๆไว้เป็นความลับ<br/>*** อีเมลนี้เป็นการแจ้งจากระบบอัตโนมัติ กรุณาอย่าตอบกลับ ***</p></div>";

            var attachment = new FileAttachment
            {
                ContentType = "application/pdf",
                FileName = "ใบรับคำขอ",
                Path = ServerHelper.MapPath("~/Content/SingleForm-frontis/ใบรับคำขอ.pdf"),
            };

            if (identityType == UserTypeEnum.Citizen)
            {
                mailBody = "<p>เรียน คุณ จอน ศรีลาดี </p><p>ทางเราขอเรียนให้ทราบว่าคุณได้ยื่นคำขอผ่านระบบ Doing Business Portal <br/>เวลา {0} น. วันที่ {1} เป็นที่เรียบร้อยแล้ว</p><p>โดยยื่นคำขอทั้งสิ้น 1 รายการ  ดังนี้<br/>1. ขอหนังสือรับรองการแจ้งจัดตั้งสถานที่จำหน่ายหรือสะสมอาหาร (ไม่เกิน 200 ตร.ม.)</p><p>หากคุณต้องการติดตามสถานะคำขอผ่าน Doing Business Portal โปรด<a href='https://biz.govchannel.go.th/th'>คลิกที่นี่</a>หรือวางลิงก์ต่อไปนี้ลงในเบราว์เซอร์ของคุณ https://biz.govchannel.go.th/th/</p><br/><p>ขอขอบพระคุณที่ใช้บริการผ่านระบบ Doing Business Portal<br/><b>ทีมงาน Doing Business Portal</b></p><img height='30px' src='https://biz.govchannel.go.th/img/logos/logo-doing-biz-portal.jpg' /><div style='color: grey; font-size: 0.8em'><p>หมายเหตุ : ทีมงาน Doing Business Portal ไม่มีนโยบายที่จะสอบถามรหัสการใช้งานต่างๆของคุณ เพื่อความปลอดภัยในการใช้งาน กรุณาเก็บรักษารหัสต่างๆไว้เป็นความลับ<br/>*** อีเมลนี้เป็นการแจ้งจากระบบอัตโนมัติ กรุณาอย่าตอบกลับ ***</p></div>";

                attachment = new FileAttachment
                {
                    ContentType = "application/pdf",
                    FileName = "ใบรับคำขอ_บุคคล",
                    Path = ServerHelper.MapPath("~/Content/SingleForm-frontis/ใบรับคำขอ_บุคคล.pdf"),
                };
            }
            mailBody = string.Format(mailBody, "11.00", submit.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("th-TH")));

            //return SendEmail(new string[] { to }, header, true, body, true);
            if (cc != null && cc.Any())
            {
                List<MailAddress> ccTos = new List<MailAddress>();
                foreach (var cmail in cc)
                {
                    ccTos.Add(new MailAddress() { Address = cmail });
                }
                return SendEmailV2(new MailAddress[] { new MailAddress { Name = to, Address = to } }, ccTos.ToArray(), header, body, true);
            }
            else
            {
                return SendEmailV2(new MailAddress[] { new MailAddress { Name = to, Address = to } }, mailSubject, mailBody, true, null, null, new Attachment[] { attachment });
            }
        }

        public static string DynamicMergeEmail(string body, Dictionary<string, string> attachFiles)
        {
            foreach (var item in attachFiles.Select(s => s.Key).ToList())
            {
                body = body.Replace("<::" + item + "::>", attachFiles[item]);
            }

            return body;
        }

        public static bool SendContactEmail(string type, string detail, string name, string citizenId, string email, string mobileno, string lang, bool isReply)
        {
            List<MailAddress> sendToAddr = new List<MailAddress>();
            sendToAddr.Add(new MailAddress { Name = ConfigurationValues.ContactEmail, Address = ConfigurationValues.ContactEmail });
            if (isReply)
                sendToAddr.Add(new MailAddress { Name = email, Address = email });

            string subject = string.Format(Resources.ContactUs.EMAIL_SUBJECT, type);
            string body = Resources.ContactUs.EMAIL_BODY;


            Dictionary<string, string> attachFiles = new Dictionary<string, string>();
            attachFiles.Add("FULL_NAME", name);
            attachFiles.Add("TYPE", type);
            attachFiles.Add("DETAIL", detail);
            attachFiles.Add("CITIZENID", citizenId);
            attachFiles.Add("MOBILENO", string.IsNullOrEmpty(mobileno) ? "-" : mobileno);
            attachFiles.Add("EMAIL", string.IsNullOrEmpty(email) ? "-" : email);

            body = DynamicMergeEmail(body, attachFiles);

            //return SendEmail(to, subject, true, body, true, email, email);
            return SendEmailV2(sendToAddr, subject, body, true, new MailAddress[] { new MailAddress { Name = email, Address = email } });
        }
   
    }
}
