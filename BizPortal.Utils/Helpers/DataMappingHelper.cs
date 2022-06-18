using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public static class DataMappingHelper
    {
        public static T ToObject<T>(this IDictionary<string, object> source) where T : class, new()
        {
            var obj = new T();
            var objType = obj.GetType();

            foreach (var item in source)
            {
                objType.GetProperty(item.Key).SetValue(obj, item.Value, null);
            }

            return obj;
        }

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );
        }
    }
}
