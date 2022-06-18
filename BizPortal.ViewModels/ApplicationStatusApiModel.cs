using BizPortal.Utils;
using BizPortal.Utils.Helpers;
using EGA.EGA_Development.Util.MailV2.Data;
using EGA.WS;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class ApplicationStatusApiModel
    {
        /// <summary>
        /// เลขประจำตัวผู้เสียภาษี
        /// </summary>
        [Required]
        public string JuristicID { get; set; }

        /// <summary>
        /// รหัสบริการ เอาจากฐานข้อมูลตาราง Application
        /// </summary>
        [Required]
        public int ServiceID { get; set; }

        #region [Optional]

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// ไฟล์เอกสารแนบ
        /// </summary>
        public FileMetadata[] Files { get; set; }

        /// <summary>
        /// เลขที่/รหัสคำขอ
        /// </summary>
        [StringLength(450)]
        public string TransactionCode { get; set; }

        /// <summary>
        /// วันที่เกิดรายการ เป็น Unix Timestamp
        /// </summary>
        public DateTime? TransactionDate { get; set; }

        /// <summary>
        /// 1: Pending
        /// 2: Rejected
        /// 3: Cancel
        /// 4: Completed
        /// 5: Other
        /// 6: Draft
        /// </summary>
        public int? StatusID { get; set; }

        /// <summary>
        /// ไม่ส่งอีเมล์แจ้งสถานะของระบบ
        /// </summary>
        public bool DisabledSendingSystemEmail { get; set; }

        /// <summary>
        /// ส่งข้อมูลมากรณีต้องการส่งอีเมล์ (ไม่ใช้ Default Email)
        /// </summary>
        public ApplicationStatusEmailMessage EmailMessage { get; set; }

        /// <summary>
        /// ส่งข้อมูลกรณีต้องการส่ง sms
        /// </summary>
        public ApplicationStatusSmsMessage SmsMessage { get; set; }

        #endregion
    }

    public enum SendingStatus
    {
        Success,
        InvalidMobileNumbers,
        Failed
    }

    public class SmsMessageSendingStatus
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public SendingStatus Status { get; set; }

        public string Message { get; set; }

        public MobileNumberInfo[] InvalidMobileNumbers { get; set; }
    }

    public class MobileNumberInfo
    {
        public string MobileNumber { get; set; }

        public string Type { get; set; }

        public string Message { get; set; }
    }

    public class ApplicationStatusSmsMessage
    {
        /// <summary>
        /// ภาษาไทย 200 ตัวอักษร
        /// ภาษาอังกฤษ 400 ตัวอักษร
        /// </summary>
        [StringLength(400)]
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// ตรวจสอบ format จา PhoneNumbers.dll
        /// </summary>
        [Required]
        public string[] MobileNumbers { get; set; }

        public MobileNumberInfo[] GetInvalidNumbers()
        {
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            List<MobileNumberInfo> invalidNumbers = new List<MobileNumberInfo>();

            foreach (var number in MobileNumbers)
            {
                try
                {
                    PhoneNumber numberObj = phoneUtil.Parse(number, "TH");
                    var valid = phoneUtil.IsValidNumber(numberObj);
                    var type = phoneUtil.GetNumberType(numberObj);
                    if (!valid || type != PhoneNumberType.MOBILE)
                    {
                        invalidNumbers.Add(new MobileNumberInfo()
                        {
                            MobileNumber = number,
                            Type = phoneUtil.GetNumberType(numberObj).ToString()
                        });
                    }
                }
                catch (NumberParseException e)
                {
                    invalidNumbers.Add(new MobileNumberInfo()
                    {
                        MobileNumber = number,
                        Type = PhoneNumberType.UNKNOWN.ToString(),
                        Message = e.Message
                    });
                }
            }

            return invalidNumbers.ToArray();
        }

        public SmsMessageSendingStatus SendSms()
        {
            SmsMessageSendingStatus result = new SmsMessageSendingStatus();

            MobileNumberInfo[] invalidNumbers = GetInvalidNumbers();

            //if (invalidNumbers.Length > 0)
            //{
            //    result.Status = SendingStatus.InvalidMobileNumbers;
            //    result.InvalidMobileNumbers = invalidNumbers;
            //    return result;
            //}

            string[] validNumbers = MobileNumbers.Where(o => !invalidNumbers.Select(m => m.MobileNumber).Contains(o)).ToArray();

            if (validNumbers.Length > 0)
            {
                EGAWSAPI api =
                    EGAWSAPI.CreateChatInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("Msnlist", string.Join(";", MobileNumbers));
                args.Add("Msg", Message);
                args.Add("MsgType", "T");
                args.Add("User", ConfigurationManager.AppSettings["SMSUser"]);
                args.Add("Password", ConfigurationManager.AppSettings["SMSPwd"]);
                args.Add("Sender", ConfigurationManager.AppSettings["SMSSender"]);
                var jsonResult =
                    api.Call(ConfigurationManager.AppSettings["SMSApi"], HttpVerb.POST, null,
                    JsonConvert.SerializeObject(args), ContentType.ApplicationJson);

                result.Status = jsonResult["Status"].ToString() == "true" ? SendingStatus.Success : SendingStatus.Failed;
                result.Message = jsonResult["Message"].ToString();
            }
            else
            {
                result.Status = SendingStatus.Success;
            }

            return result;
        }
    }

    public class ApplicationStatusEmailMessage
    {
        //public string SendFromName { get; set; }

        //public string SendFromEmail { get; set; }

        //public string ReplyToName { get; set; }

        //public string ReplyToEmail { get; set; }

        public string SendToEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        public bool IsHtmlBody { get; set; }

        public Base64Attachment[] Attachments { get; set; }
    }
}
