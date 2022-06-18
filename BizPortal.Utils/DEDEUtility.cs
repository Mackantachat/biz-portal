using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils
{
    public static class DEDEUtility
    {

        public static Dictionary<string, string> fileTypeRefs = null;
        public static Dictionary<int, string> ownerType = null;
        public static Dictionary<int, string> investCode = null;
        public static NameValueCollection fileType = null;
        public static NameValueCollection GetFileTypeDEDERef()
        {
            fileType = new System.Collections.Specialized.NameValueCollection();
            fileType.Add("1", "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY");
            fileType.Add("2", "ID_CARD_COPY");
            fileType.Add("2", "JURISTIC_COMMITTEE_ID_CARD");
            fileType.Add("2", "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD");
            fileType.Add("2", "AUTHORIZATION_AUTHORIZEE_ID_CARD");
            fileType.Add("3", "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC");
            fileType.Add("3", "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC");
          
            fileType.Add("4", "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD");
            fileType.Add("4", "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD");
            fileType.Add("5", "ENERGY_PRODUCTION_MAP");
            fileType.Add("6", "ENERGY_PRODUCTION_DOC");
            fileType.Add("7", "ENERGY_PRODUCTION_ENGINEER_LICENSE");
            fileType.Add("8", "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY");
            fileType.Add("8", "CITIZEN_HOUSEHOLD_REGISTRATION_COPY");
            fileType.Add("21", "ENERGY_PRODUCTION_PLAN");
            return fileType;
        }
        public static Dictionary<string, string> GetFileTypeRef()
        {
            fileTypeRefs = new Dictionary<string, string>();
            fileTypeRefs.Add("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY","8");
            fileTypeRefs.Add("ID_CARD_COPY","9");
            fileTypeRefs.Add("JURISTIC_COMMITTEE_ID_CARD", "9");
            fileTypeRefs.Add("JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD", "9");
            fileTypeRefs.Add("AUTHORIZATION_AUTHORIZEE_ID_CARD", "9");
            fileTypeRefs.Add("JURISTIC_AUTHORIZATION_AUTHORIZE_DOC", "10");
            fileTypeRefs.Add("CITIZEN_AUTHORIZATION_AUTHORIZE_DOC", "10");
            fileTypeRefs.Add("CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD","11");
            fileTypeRefs.Add("JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD","11");
            
            fileTypeRefs.Add("ENERGY_PRODUCTION_MAP","12");
            fileTypeRefs.Add("ENERGY_PRODUCTION_DOC","13");
            fileTypeRefs.Add("ENERGY_PRODUCTION_ENGINEER_LICENSE","14");
            fileTypeRefs.Add("JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY", "16");
            fileTypeRefs.Add("CITIZEN_HOUSEHOLD_REGISTRATION_COPY", "16");
            fileTypeRefs.Add("FREE_DOC", "16");
        
            fileTypeRefs.Add("ENERGY_PRODUCTION_PLAN","15");
            //#region JURISTIC
            //"CERTIFICATION_OF_COMPANY_REGISTRATION_COPY",
            //            "JURISTIC_COMMITTEE_ID_CARD",
            //            "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY",
            //            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
            //            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
            //            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
            //            #endregion

            //#region CITIZEN
            //            "ID_CARD_COPY",
            //            "CITIZEN_HOUSEHOLD_REGISTRATION_COPY",
            //            "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC",
            //            "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD",
            //            "AUTHORIZATION_AUTHORIZEE_ID_CARD",
            //            #endregion


            //#region Juristic Authorization
            //            "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC",
            //            "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD",
            //            "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD",
            //            #endregion
                        
            //#region APP_ENERGY_PRODUCTION DOC
            //            "ENERGY_PRODUCTION_MAP",
            //            "ENERGY_PRODUCTION_PLAN",
            //            "ENERGY_PRODUCTION_DOC",
            //            "ENERGY_PRODUCTION_ENGINEER_LICENSE",
            //            #endregion
            return fileTypeRefs;
        }

        public static Dictionary<int, string> GetOwnerType()
        {
            ownerType = new Dictionary<int, string>();
            ownerType.Add(1, "บุคคลธรรมดา");
            ownerType.Add(2, "นิติบุคคล");
            return ownerType;
        }
        public static Dictionary<int, string> GetInvestCode()
        {
            investCode = new Dictionary<int, string>();
            investCode.Add(1, "เงินสด");
            investCode.Add(2, "ทรัพย์สิน");

            return investCode;
        }
        public static string GetDateFormat_Dashes(string str_Date)
        {
            string result_DateFormatEn = string.Empty;
            if (!String.IsNullOrEmpty(str_Date))
            {
                string[] tempsplit = str_Date.Split('-');

                string newdateFormat = tempsplit[2] + "/" + tempsplit[1] + "/" + tempsplit[0];

                var DateFormatEn = DateTime.Parse(newdateFormat, new CultureInfo("en-US"));

                result_DateFormatEn = DateFormatEn.ToString("yyyyMMdd", new CultureInfo("th-TH"));
            }
            else
            {
                result_DateFormatEn = DateTime.Now.ToString("yyyyMMdd", new CultureInfo("th-TH"));
            }
            return result_DateFormatEn;



        }
        public static string GetDateFormat(string str_Date)
        {
            string result_DateFormatEn = string.Empty;
            if (!String.IsNullOrEmpty(str_Date))
            {

                try
                {
                    var DateFormat = DateTime.ParseExact(str_Date, "yyyyMMdd", new CultureInfo("th-TH"));
                    return DateFormat.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                }
                catch (Exception)
                {
                    return null;
                }



            }

            return "";



        }

    }
}
