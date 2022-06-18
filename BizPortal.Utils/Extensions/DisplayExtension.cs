using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BizPortal.Utils.Extensions
{
    public static class DisplayExtension
    {
        public static string ToMoneyFormat(this decimal input)
        {
            var s = string.Format("{0:#,##0.00}", input);

            if (s.EndsWith("00"))
            {
                return ((int)input).ToString("#,##0");
            }
            else
            {
                return s;
            }
        }

        public static string ToStringFormat(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm น.");
        }

        public static string ToStringFormatNoTime(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        public static string ShortDetailText(this string data, int maxLength=100)
        {
            if (data.Length > maxLength)
                return string.Format("{0}...", data.Substring(0, maxLength));
            else
                return data;
        }

        public static string ToThaiCurrencyText(this decimal amount)
        {
            string bahtTxt, n, bahtTH = "";
            if (amount < 0) return "";

            bahtTxt = amount.ToString("####.00");
            string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
            string[] temp = bahtTxt.Split('.');
            string intVal = temp[0];
            intVal = intVal.Replace("-", "");
            string decVal = temp[1];
            if (Convert.ToDouble(bahtTxt) == 0)
                bahtTH = "ศูนย์บาทถ้วน";
            else
            {
                for (int i = 0; i < intVal.Length; i++)
                {
                    n = intVal.Substring(i, 1);
                    if (n != "0")
                    {
                        if ((i == (intVal.Length - 1)) && (n == "1"))
                            bahtTH += "เอ็ด";
                        else if ((i == (intVal.Length - 2)) && (n == "2"))
                            bahtTH += "ยี่";
                        else if ((i == (intVal.Length - 2)) && (n == "1"))
                            bahtTH += "";
                        else
                            bahtTH += num[Convert.ToInt32(n)];
                        bahtTH += rank[(intVal.Length - i) - 1];
                    }
                }
                bahtTH += "บาท";
                if (decVal == "00")
                    bahtTH += "ถ้วน";
                else
                {
                    for (int i = 0; i < decVal.Length; i++)
                    {
                        n = decVal.Substring(i, 1);
                        if (n != "0")
                        {
                            if ((i == decVal.Length - 1) && (n == "1"))
                                bahtTH += "เอ็ด";
                            else if ((i == (decVal.Length - 2)) && (n == "2"))
                                bahtTH += "ยี่";
                            else if ((i == (decVal.Length - 2)) && (n == "1"))
                                bahtTH += "";
                            else
                                bahtTH += num[Convert.ToInt32(n)];
                            bahtTH += rank[(decVal.Length - i) - 1];
                        }
                    }
                    bahtTH += "สตางค์";
                }
            }
            return bahtTH;
        }

        public static string ToCitizenIdentityFormat(this string identityID)
        {
            if (string.IsNullOrEmpty(identityID)) return identityID;

            return Regex.Replace(identityID, @"(\d{1})(\d{4})(\d{5})(\d{2})(\d{1})", "$1-$2-$3-$4-$5");
        }

    }
}
