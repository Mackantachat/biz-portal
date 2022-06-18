using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BizPortal.Utils.Extensions
{
    public static class RegExExtension
    {
        public static bool AdjustRegEx<T>(this T obj, string pattern) where T : class
        {
            bool match = false;
            Regex regex = new Regex(pattern);

            PropertyInfo[] props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(string))
                {
                    var valObj = prop.GetValue(obj);
                    if (valObj != null)
                    {
                        string value = valObj.ToString();
                        match = match || regex.IsMatch(value) ;
                    }
                }
            }

            return match;
        }
    }
}
