using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Extensions
{
    public static class DictionaryExtension
    {
        public static Dictionary<string, TValue> ToUpperKeyDictionary<TValue>(this Dictionary<string, TValue> dict)
        {
            if (dict != null)
            {
                Dictionary<string, TValue> newDict = new Dictionary<string, TValue>();
                foreach (var item in dict)
                {
                    newDict.Add(item.Key.ToUpper(), item.Value);
                }
                return newDict;
            }
            return null;
        }

        /// <summary>
        /// Try to add key-value pair to dictionary. If key is already exist, it will replace with new value.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddOrUpdate<TValue>(this Dictionary<string, TValue> dictionary, string key, TValue value)
        {
            if (dictionary.ContainsKey(key)) dictionary.Remove(key);

            dictionary.Add(key, value);
        }

        public static bool IsDefault<T>(this T value) where T : struct
        {
            bool isDefault = value.Equals(default(T));
            return isDefault;
        }
    }
}
