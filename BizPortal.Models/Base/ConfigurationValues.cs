using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal
{
    public static class ConfigurationValues
    {
        public const string SHOW_INTRO_COOKIE = ".th.go.govchannel.biz.cookie.intro";
        public const string ALERT_COOKIE = "alertcookie";

        public const string ROLES_ADMIN_NAME = "PortalAdmin";
        public const string ROLES_OPDC_ADMIN_NAME = "OPDCAdmin";
        public const string ROLES_ORG_ADMIN_NAME = "OrgAdmin";
        public const string ROLES_ORG_AGENT_NAME = "OrgAgent";
        public const string ROLES_OPDC_AGENT_NAME = "OPDCAgent";
        public const string ROLES_MODULATOR_NAME = "PortalModulator";
        public const string ROLES_DOCUMENT_SIGNER_NAME = "DocumentSigner";

        public const int RESET_PASSWORD_EXPIRED_MINUTE = 1440;

        public static bool TestMode { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["TestMode"]); } }
        public static string TestEmail { get { return ConfigurationManager.AppSettings["TestEmail"]; } }
        public static string TestEmail2 { get { return ConfigurationManager.AppSettings["TestEmail2"]; } }
        public static string BizUrl { get { return ConfigurationManager.AppSettings["BizUrl"]; } }
        public static string ServiceUrl { get { return ConfigurationManager.AppSettings["ServiceUrl"]; } }
        public static string ConsumerKey { get { return ConfigurationManager.AppSettings["ConsumerKey"].ToString(); } }
        public static string ConsumerSecret { get { return ConfigurationManager.AppSettings["ConsumerSecret"].ToString(); } }
        public static string GetHolliday { get { return ConfigurationManager.AppSettings["GetHolliday"].ToString(); } }
        public static string ContactEmail { get { return ConfigurationManager.AppSettings["ContactEmail"].ToString(); } }
        public static string PDF_INVOICE_FILEID { get { return ConfigurationManager.AppSettings["PDF_INVOICE_FILEID"].ToString(); } }
        public static string GoogleMapsKey { get { return ConfigurationManager.AppSettings["googleapi:GoogleMaps"].ToString(); } }
        public static string OmegaAuth { get { return ConfigurationManager.AppSettings["OmegaAuth"].ToString(); } }
    }
}
