using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Extensions
{
    public static class StringLengthExtension
    {
        /// <summary>
        /// ปรับปรุงข้อความให้มีขนาดไม่เกินที่กำหนดไว้ที่ StringLengthAttribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static void AdjustStringLength<T>(this T obj)
            where T : class
        {
            PropertyInfo[] props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(string))
                {
                    StringLengthAttribute attr = prop.GetCustomAttribute(typeof(StringLengthAttribute)) as StringLengthAttribute;
                    if (attr != null)
                    {
                        var valObj = prop.GetValue(obj);
                        if (valObj != null)
                        {
                            string value = valObj.ToString();
                            if (value.Length > attr.MaximumLength)
                            {
                                value = value.Substring(0, attr.MaximumLength);
                                prop.SetValue(obj, value);
                            }
                        }
                    }
                }
            }
        }
    }
}
