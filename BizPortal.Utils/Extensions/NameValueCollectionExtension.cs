﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Extensions
{
    public static class NameValueCollectionExtension
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this NameValueCollection col)
        {
            var dict = new Dictionary<TKey, TValue>();
            var keyConverter = TypeDescriptor.GetConverter(typeof(TKey));
            var valueConverter = TypeDescriptor.GetConverter(typeof(TValue));

            foreach (string name in col)
            {
                TKey key = (TKey)keyConverter.ConvertFromString(name);
                TValue value = (TValue)valueConverter.ConvertFromString(col[name]);
                dict.Add(key, value);
            }

            return dict;
        }
    }
}
