using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public static class JsonHelper
    {
        public static string GetStringValue(this JObject obj, string key)
        {
            if (obj != null && obj.ContainsKey(key))
            {
                return obj[key].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static JObject GetObjectData(this JObject obj, string key)
        {
            if (obj != null && obj.ContainsKey(key))
            {
                return obj[key].ToObject<JObject>();
            }
            else
            {
                return new JObject();
            }
        }

        public static JArray GetArrayData(this JObject obj, string key)
        {
            if (obj != null && obj.ContainsKey(key))
            {
                return JArray.Parse(obj[key].ToString());
            }
            else
            {
                return new JArray();
            }
        }
    }
}
