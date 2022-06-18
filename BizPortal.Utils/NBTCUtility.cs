using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils
{
    public static class NBTCUtility
    {


        public static Dictionary<int, string> carType = null;
        public static Dictionary<string, string> fileTypeRefs = null;
        public static Dictionary<int, string> addressType = null;
        public static Dictionary<int, string> ownerType = null;
        public static Dictionary<int, string> requestType = null;
        public static Dictionary<int, string> transportIn = null;
        public static Dictionary<int, string> transportPlace = null;
        public static Dictionary<int, string> readyType = null;
        public static Dictionary<string, string> GetFileTypeNBTCRef()
        {
            fileTypeRefs = new Dictionary<string, string>();
            fileTypeRefs.Add("JURISTIC_COMMITTEE_ID_CARD", "4");
            fileTypeRefs.Add("JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY", "5");
            fileTypeRefs.Add("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY", "3");
            fileTypeRefs.Add("VAT_REGISTRATION", "6");
            fileTypeRefs.Add("JURISTIC_AUTHORIZATION_AUTHORIZE_DOC", "15");
            fileTypeRefs.Add("JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD", "15");
            fileTypeRefs.Add("AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY", "15");
            fileTypeRefs.Add("JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD", "15");
            fileTypeRefs.Add("ID_CARD_COPY", "1");
            fileTypeRefs.Add("CITIZEN_HOUSEHOLD_REGISTRATION_COPY", "2");
            fileTypeRefs.Add("CITIZEN_AUTHORIZATION_AUTHORIZE_DOC", "15");
            fileTypeRefs.Add("AUTHORIZATION_AUTHORIZEE_ID_CARD", "15");
            fileTypeRefs.Add("CITIZEN_AUTHORIZATION_AUTHORIZEE_HOUSEHOLD_REGISTRATION_COPY", "15");
            fileTypeRefs.Add("CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD", "15");
            fileTypeRefs.Add("INFORMATION_STORE_BUILDING_OWNER_DOC", "7");
            fileTypeRefs.Add("INFORMATION_STORE_BUILDING_OWNER_ID_CARD", "8");
            fileTypeRefs.Add("INFORMATION_STORE_MAP", "9");
            fileTypeRefs.Add("INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY", "10");
            fileTypeRefs.Add("INFORMATION_STORE_RENTAL_CONTRACT", "11");
            fileTypeRefs.Add("INFORMATION_STORE_BUILDING_USAGE_AGREEMENT", "12");
            fileTypeRefs.Add("INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION", "13");
            fileTypeRefs.Add("COMMERCE_REGISTRATION_DOC_CITIZEN", "14");
            fileTypeRefs.Add("FREE_DOC","16");
            return fileTypeRefs;
        }

        public static Dictionary<int, string> GetOwnerType()
        {
            ownerType = new Dictionary<int, string>();
         
            ownerType.Add(1, "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION__JURISTIC");
            ownerType.Add(2, "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION__CITIZEN");
            ownerType.Add(3, "INFORMATION_STORE_BUILDING_TYPE_OPTION__GOVERNMENT");
            ownerType.Add(4, "INFORMATION_STORE_BUILDING_TYPE_OPTION__ROYAL");
            return ownerType;
        }
        public static Dictionary<int, string> GetAddressType()
        {
            addressType = new Dictionary<int, string>();
            addressType.Add(1,"INFORMATION_STORE_BUILDING_TYPE_OPTION__OWNED");
            addressType.Add(2,"INFORMATION_STORE_BUILDING_TYPE_OPTION__RENT");
          
        
            return addressType;
        }
        
        public static Dictionary<int, string> GetRequrestType()
        {
            requestType = new Dictionary<int, string>();
            requestType.Add(1, "REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER");
            requestType.Add(0, "REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE");
            return requestType;
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
