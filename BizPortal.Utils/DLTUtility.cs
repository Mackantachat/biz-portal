using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils
{
    public static class DLTUtility
    {


        public static Dictionary<int, string> carType = null;
        public static Dictionary<string, string> fileTypeRefs = null;
        public static Dictionary<int, string> addressType = null;
        public static Dictionary<int, string> ownerType = null;
        public static Dictionary<int, string> requestType = null;
        public static Dictionary<int, string> transportIn = null;
        public static Dictionary<int, string> transportPlace = null;
        public static Dictionary<int, string> readyType = null;
        public static Dictionary<string, string> GetFileTypeDLTRef()
        {
            fileTypeRefs = new Dictionary<string, string>();
            fileTypeRefs.Add("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY", "4");
            fileTypeRefs.Add("JURISTIC_MEMORANDUM", "4.1");
            fileTypeRefs.Add("JURISTIC_SHARE_HOLDER_LIST", "4.3");
            fileTypeRefs.Add("JURISTIC_COMMITTEE_ID_CARD", "4.4");
            fileTypeRefs.Add("JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY", "4.4");
            fileTypeRefs.Add("JURISTIC_AUTHORIZATION_AUTHORIZE_DOC", "12");
            fileTypeRefs.Add("JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD", "12");
            fileTypeRefs.Add("JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD", "12");
            fileTypeRefs.Add("ID_CARD_COPY_NON_PASSPORT", "1");
            fileTypeRefs.Add("CITIZEN_HOUSEHOLD_REGISTRATION_COPY", "2");
            fileTypeRefs.Add("CITIZEN_AUTHORIZATION_AUTHORIZE_DOC", "12");
            fileTypeRefs.Add("CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD_MULTIPLE", "12");
            fileTypeRefs.Add("CITIZEN_AUTHORIZATION_AUTHORIZEE_ID_CARD_MULTIPLE", "12");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_OWNER_DOC", "8.2");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_OWNER_ID_CARD", "8.3");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_HOUSEHOLD_RENT", "8.3");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_RENTAL_CONTRACT", "8.2");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_OWNER_JURISTIC_REGISTRATION", "8.3");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_INFORMATION_USAGE_AGREEMENT", "8.2");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_LABEL", "7.1");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_WITHIN", "7.1");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_MAINTENANCE_ENTRANCE", "7.1");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PHOTO_MAINTENANCE_PARKING", "7.2");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_TRANSPORTATION_VOLUME", "5");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_CERTIFICATION", "10");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_SECURITIES", "11");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_PURCHASE_REQUEST", "6");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_CAR_LICENSE", "6");
            //"APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_NO_1");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_NO_2", "13");
            fileTypeRefs.Add("APP_TRANSPORT_NON_REGULAR_TRUCK_REQUEST_INFORMATION_DOC_NO_3", "13");
            fileTypeRefs.Add("FREE_DOC","13");
            return fileTypeRefs;
        }

        public static Dictionary<int, string> GetOwnerType()
        {
            ownerType = new Dictionary<int, string>();
            ownerType.Add(1, "บุคคลธรรมดา");
            ownerType.Add(2, "นิติบุคคล");
            ownerType.Add(3, "ภาครัฐ");
            ownerType.Add(4, "ทรัพย์สินส่วนพระมหากษัตริย์");
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
        public static Dictionary<int, string> GetTransportIn()
        {
            transportIn = new Dictionary<int, string>();
            transportIn.Add(1, "KINGDOM");
            transportIn.Add(0, "OTHER");
            return transportIn;
        }
        public static Dictionary<int, string> GetTransportPlace()
        {
            transportPlace = new Dictionary<int, string>();
            transportPlace.Add(1, "HAVE_TRANFER");
            transportPlace.Add(0, "NOT_HAVE_TRANFER");
            return transportPlace;
        }
        public static Dictionary<int, string> GetCarType()
        {
            carType = new Dictionary<int, string>();
            carType.Add(1, "PICKUP");
            carType.Add(2, "VAN");
            carType.Add(3, "LIQUID");
            carType.Add(4, "DANGER");
            carType.Add(5, "SPECIAL");
            carType.Add(6, "TRAILER");
            carType.Add(7, "SEMI_TRAILER");
            carType.Add(8, "SEMI_TRAILER_LONG");
            carType.Add(9, "TOWING_TRUCK");
            return carType;
        }
        public static Dictionary<int, string> GetReady()
        {
            readyType = new Dictionary<int, string>();
            readyType.Add(1, "GET_PERMIT");
            readyType.Add(0, "WITHIN");
            return readyType;
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
