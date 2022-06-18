using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils
{
    public static class DBDUtility
    {

           public static Dictionary<int, string> fileTypeRefs =  null;
           public static Dictionary<int, string> ownerType = null;
        public static Dictionary<int, string> investCode = null;

        public static Dictionary<int, string> GetFileTypeRef()
           {
            fileTypeRefs = new Dictionary<int, string>();

            fileTypeRefs.Add(1, "JURISTIC_COMMITTEE_ID_CARD");
            fileTypeRefs.Add(2, "JURISTIC_COMMITTEE_HOUSEHOLD_REGISTRATION_COPY");
            fileTypeRefs.Add(3, "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY");
            fileTypeRefs.Add(4, "ID_CARD_COPY");
            fileTypeRefs.Add(5, "CITIZEN_HOUSEHOLD_REGISTRATION_COPY");
            fileTypeRefs.Add(6, "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC");
            fileTypeRefs.Add(7, "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD");
            fileTypeRefs.Add(8, "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD");
            fileTypeRefs.Add(9, "CITIZEN_AUTHORIZATION_AUTHORIZE_DOC");
            fileTypeRefs.Add(10, "AUTHORIZATION_AUTHORIZEE_ID_CARD");
            fileTypeRefs.Add(11, "CITIZEN_AUTHORIZATION_AUTHORIZOR_ID_CARD");
            fileTypeRefs.Add(12, "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_BUILDING_OWNER_DOC");
            fileTypeRefs.Add(13, "APP_ELECTONIC_COMMERCIAL_INFORMATION_STORE_BUILDING_OWNER_ID_CARD");
            fileTypeRefs.Add(14, "INFORMATION_STORE_HQ_MAP");
            fileTypeRefs.Add(15, "INFORMATION_STORE_MAP");
            fileTypeRefs.Add(16, "INFORMATION_STORE_RENTAL_CONTRACT");
            fileTypeRefs.Add(17, "INFORMATION_STORE_BUILDING_USAGE_AGREEMENT");
            fileTypeRefs.Add(18, "INFORMATION_STORE_BUILDING_OWNER_JURISTIC_REGISTRATION");
            fileTypeRefs.Add(19, "INFORMATION_STORE_HOUSEHOLD_RENT");
            fileTypeRefs.Add(20, "DOC_COMMERCIAL_STATEMENT");
            fileTypeRefs.Add(21, "APP_ELECTONIC_COMMERCIAL_DOCUMENT_TRADE");
            fileTypeRefs.Add(22, "DOC_COMMERCIAL_ALLOW");
            fileTypeRefs.Add(23, "DOC_COMMERCIAL_BUDGET");
            fileTypeRefs.Add(24, "DOC_COMMERCIAL_AREA_PICTURE");
            fileTypeRefs.Add(25, "DOC_COMMERCIAL_CHILD");
            fileTypeRefs.Add(26, "26");//ไม่ใช้แล้ว
            fileTypeRefs.Add(27, "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY");
            fileTypeRefs.Add(28, "APP_ELECTONIC_COMMERCIAL_PICTURE_INDEX_WEBSITE");
            fileTypeRefs.Add(29, "APP_ELECTONIC_COMMERCIAL_PICTURE_PRODUCT");
            fileTypeRefs.Add(30, "APP_ELECTONIC_COMMERCIAL_PICTURE_METHOD_ORDER_AND_PAYMENT");
            fileTypeRefs.Add(31, "APP_ELECTONIC_COMMERCIAL_CANCEL_CURRENT_PERMIT");
            fileTypeRefs.Add(32, "APP_ELECTONIC_COMMERCIAL_EDIT_DOCUMENT_CHANGE");
            fileTypeRefs.Add(33, "APP_ELECTONIC_COMMERCIAL_CANCEL_OWNER_DEATH");
            fileTypeRefs.Add(34, "APP_ELECTONIC_COMMERCIAL_CANCEL_COUNT_ORDERED");
            fileTypeRefs.Add(35, "APP_ELECTONIC_COMMERCIAL_CANCEL_HEIR");
            fileTypeRefs.Add(36, "APP_ELECTONIC_COMMERCIAL_CANCEL_STOP_BUSINESS");
            fileTypeRefs.Add(37, "FREE_DOC");
            return fileTypeRefs;
        }

        public static Dictionary<int, string>  GetOwnerType()
        {
            ownerType =  new Dictionary<int, string>();
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
                    var DateFormat = DateTime.ParseExact(str_Date,"yyyyMMdd", new CultureInfo("th-TH"));
                    return DateFormat.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                }
                catch(Exception )
                {
                    return null;
                }
                

               
            }         
            return "";
        }
        public static string GetJuristicTitle(string juristic_type)
        {
           
            if (!String.IsNullOrEmpty(juristic_type))
            {

                try
                {
                    string titleCode = string.Empty;
                    if (juristic_type.Contains("บริษัท"))
                    {
                        titleCode = "801";
                    }
                    else if (juristic_type.Contains("ห้างหุ้นส่วนสามัญนิติบุคคล"))
                    {
                        titleCode = "802";
                    }
                    else if (juristic_type.Contains("ห้างหุ้นส่วนจำกัด"))
                    {
                        titleCode = "803";
                    }
                    else if (juristic_type.Contains("ธนาคาร"))
                    {
                        titleCode = "804";
                    }
                    else
                    {
                        titleCode = "800"; //ไม่ระบุ
                    }
                    return titleCode;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
        
    }
}
