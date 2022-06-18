using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public class StringHelper
    {
        public static bool IsTrueValue(string data)
        {
            return ("" + data).ToLower() == "true";
        }

        public static bool IsNumeric(string str)
        {
            int i = 0;
            return int.TryParse(str, out i);
        }

        public static decimal ToDecimal(string text, decimal defaultVal)
        {
            decimal val = 0;
            return decimal.TryParse(text, out val) ? val : defaultVal;
        }

        public static string DefaultIfEmpty(string originalText, string defaultText)
        {
            return string.IsNullOrWhiteSpace(originalText) ? defaultText : originalText;
        }
    }
}
