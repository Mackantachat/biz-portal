using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace BizPortal.Utils.Extensions
{
    public static class DefaultValueExtension
    {
        public static string DefaultString(this object obj, string defaultValue = "")
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return defaultValue;

            return obj.ToString();
        }

        public static int DefaultInt(this object obj, int defaultValue = 0)
        {
            return int.Parse(obj.DefaultString(defaultValue.ToString()));
        }

        public static decimal DefaultDecimal(this object obj, decimal defaultValue = 0)
        {
            return decimal.Parse(obj.DefaultString(defaultValue.ToString()));
        }

        public static DateTime? DefaultDateTime(this object obj, string format, IFormatProvider provider, DateTimeStyles style, DateTime? defaultValue = null)
        {
            var str = obj.DefaultString();
            DateTime? result = defaultValue;
            DateTime tmp_result = DateTime.Now;
            if (DateTime.TryParseExact(str, format, provider, style, out tmp_result))
            {
                result = tmp_result;
            }
            return result;
        }

        public static string TryGetString<T>(this Dictionary<string, T> dict, string key, string defaultValue = "")
        {
            if (dict == null || !dict.ContainsKey(key))
                return defaultValue;

            return ("" + dict[key]).ToString();
        }

        public static bool TryGetBool<T>(this Dictionary<string, T> dict, string key, bool defaultValue = false)
        {
            try
            {
                return bool.Parse(dict.TryGetString(key, "false"));
            }
            catch { return defaultValue; }
        }

        public static T TryGetData<T>(this Dictionary<string, T> data, string key) where T : class
        {
            if (data != null && data.ContainsKey(key))
            {
                return data[key];
            }

            return null;
        }

        public static string ThenGetStringData<T>(this T data, string key, string defaultValue = "")
        {
            if (data != null && data.GetType().GetProperty("Data") != null)
            {
                var items = data.GetType().GetProperty("Data").GetValue(data) as Dictionary<string, string>;

                if (items != null && items.ContainsKey(key))
                {
                    return items[key].ToString();
                }
            }

            return defaultValue;
        }

        public static int ThenGetIntData<T>(this T data, string key, int defaultValue = 0)
        {
            try
            {
                return int.Parse(data.ThenGetStringData(key, defaultValue.ToString()));
            }
            catch
            {
                return defaultValue;
            }
        }

        public static byte ThenGetByteData<T>(this T data, string key, byte defaultValue = 0)
        {
            try
            {
                return byte.Parse(data.ThenGetStringData(key, defaultValue.ToString()));
            }
            catch
            {
                return defaultValue;
            }
        }

        public static double ThenGetDoubleData<T>(this T data, string key, double defaultValue = 0)
        {
            try
            {
                return double.Parse(data.ThenGetStringData(key, defaultValue.ToString()));
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool ThenGetBooleanData<T>(this T data, string key, bool defaultValue = false)
        {
            try
            {
                return bool.Parse(data.ThenGetStringData(key, defaultValue.ToString()));
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string[] ThenGetStringRepeaterData<T>(this T data, string key, string[] defaultValue = null)
        {
            if (data != null && data.GetType().GetProperty("Data") != null)
            {
                var items = data.GetType().GetProperty("Data").GetValue(data) as Dictionary<string, string>;

                if (items != null && items.ContainsKey(key))
                {
                    return JsonConvert.DeserializeObject<List<Dictionary<string,string>>>(items[key])
                                      .SelectMany(e => e.Values)
                                      .ToArray();
                }
            }
                
            return defaultValue;
        }

        public static string[] ThenGetStringArrayData<T>(this T data, string key, string[] defaultValue = null)
        {
            if (data != null && data.GetType().GetProperty("Data") != null)
            {
                var groupname = data.GetType().GetProperty("GroupName").GetValue(data);
                var items = data.GetType().GetProperty("Data").GetValue(data) as Dictionary<string, string>;

                if (items != null && items.ContainsKey(groupname + "_TOTAL") && int.TryParse(items[groupname + "_TOTAL"], out int total))
                {
                    var result = new List<string>();

                    for (int i = 0; i < total; i++) 
                    {
                        if (items.ContainsKey(key + "_" + i))
                        {
                            result.Add(items[key + "_" + i]);
                        }
                    }

                    return result.ToArray();
                }
            }

            return defaultValue;
        }

        public static Dictionary<string, string>[] ThenGetArrayData<T>(this T data)
        {
            if (data != null && data.GetType().GetProperty("Data") != null)
            {
                var groupname = data.GetType().GetProperty("GroupName").GetValue(data);
                var items = data.GetType().GetProperty("Data").GetValue(data) as Dictionary<string, string>;

                if (items != null && items.ContainsKey(groupname + "_TOTAL") && int.TryParse(items[groupname + "_TOTAL"], out int total))
                {
                    var result = new List<Dictionary<string, string>>();

                    for (int i = 0; i < total; i++)
                    {
                        var setdata = new Dictionary<string, string>();

                        foreach (var item in items)
                        {
                            int.TryParse(item.Key.Split('_').LastOrDefault() ?? "", out int setindex);

                            if (setindex <= i)
                            {
                                if (item.Key.Contains("_" + i))
                                {
                                    setdata.Add(item.Key.Replace("_" + i,""), item.Value);
                                }
                            }
                            else 
                            {
                                break;
                            }
                        }

                        result.Add(setdata);
                    }
                    
                    return result.ToArray();
                }
            }

            return null;
        }

        public static string NullIfEmpty(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            return str;
        }
    }
}
