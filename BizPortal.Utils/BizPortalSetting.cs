using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal
{
    public static class BizPortalSetting
    {
        public const string ALERT_COOKIE = "alertcookie";
        public const string MODAL_COOKIE = "customcookie";

        public static string MailSMTP { get { return System.Configuration.ConfigurationManager.AppSettings["MailSMTP"]; } }
        public static string MailUser { get { return System.Configuration.ConfigurationManager.AppSettings["MailUser"]; } }
        public static string MailPwd { get { return System.Configuration.ConfigurationManager.AppSettings["MailPwd"]; } }
        public static string MailSender { get { return System.Configuration.ConfigurationManager.AppSettings["MailSender"]; } }
        public static string MailSenderName { get { return System.Configuration.ConfigurationManager.AppSettings["MailSenderName"]; } }

    }
}
