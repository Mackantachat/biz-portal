using BizPortal.ViewModels.V2;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Extensions
{
    public static class DictionaryViewExtension
    {
        public static ApplicationRequestDataGroupViewModel TryGetData(this Dictionary<string, ApplicationRequestDataGroupViewModel> data, string key)
        {
            if (data != null && data.ContainsKey(key))
            {
                return data[key];
            }
            return null;
        }

        public static string ThenGetStringData(this ApplicationRequestDataGroupViewModel data, string key, string defaultValue = "")
        {
            if (data != null && data.Data != null && data.Data.ContainsKey(key))
            {
                return data.Data[key].ToString();
            }

            return defaultValue;
        }

        public static string ThenGetDateTHStringData(this ApplicationRequestDataGroupViewModel data, string key, string defaultValue = "")
        {
            if (data != null && data.Data != null && data.Data.ContainsKey(key))
            {
                DateTime tmp = Convert.ToDateTime(data.Data[key]);
                return tmp.ToString("yyyyMMdd", new CultureInfo("th-TH"));
            }

            return defaultValue;
        }
        public static string ThenGetDateStringData(this ApplicationRequestDataGroupViewModel data, string key, string defaultValue = "")
        {
            try
            {
                if (data != null && data.Data != null && data.Data.ContainsKey(key))
                {
                    DateTime tmp = Convert.ToDateTime(data.Data[key]);
                    return tmp.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                }
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }
        public static int ThenGetIntData(this ApplicationRequestDataGroupViewModel data, string key, int defaultValue = 0)
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

        public static byte ThenGetByteData(this ApplicationRequestDataGroupViewModel data, string key, byte defaultValue = 0)
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

        public static double ThenGetDoubleData(this ApplicationRequestDataGroupViewModel data, string key, double defaultValue = 0)
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

        public static bool ThenGetBooleanData(this ApplicationRequestDataGroupViewModel data, string key, bool defaultValue = false)
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

        public static string[] ThenGetStringArrayData(this ApplicationRequestDataGroupViewModel data, string key, string[] defaultValue = null)
        {
            if (data != null && data.Data != null && data.Data.ContainsKey(key))
            {
                return JsonConvert.DeserializeObject<string[]>(data.Data[key].ToString());
            }
            return defaultValue;
        }

    }
}
