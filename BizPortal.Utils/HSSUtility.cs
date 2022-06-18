using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils
{
    public static class HSSUtility
    {

          
           public static Dictionary<int, string> ownerType = null;
   
     

        public static Dictionary<int, string>  GetOwnerType()
        {
            ownerType =  new Dictionary<int, string>();
            ownerType.Add(1, "บุคคลธรรมดา");
            ownerType.Add(2, "นิติบุคคล");
            return ownerType;
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
