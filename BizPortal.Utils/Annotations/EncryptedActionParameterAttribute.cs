using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Utils.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptedActionParameterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            NameValueCollection queryStrings = null;

            if (HttpContext.Current.Request.QueryString.Get("q") != null)
            {
                string encryptedQueryString = HttpContext.Current.Request.QueryString.Get("q");
                string decrptedString = Decrypt(encryptedQueryString.ToString());
                queryStrings = HttpUtility.ParseQueryString(decrptedString);
            }
            if (queryStrings != null)
            {
                var parameters = filterContext.ActionDescriptor.GetParameters();

                foreach (var key in queryStrings.AllKeys)
                {
                    var param = parameters.Where(o => o.ParameterName == key).SingleOrDefault();
                    if (param != null)
                    {
                        var type = param.ParameterType;
                        var value = queryStrings.Get(key);

                        if (!string.IsNullOrEmpty(value))
                        {
                            if (type.Equals(typeof(Guid)) || type.Equals(typeof(Guid?)))
                            {
                                filterContext.ActionParameters[key] = Guid.Parse(value);
                            }
                            else
                            {
                                filterContext.ActionParameters[key] = Convert.ChangeType(value, type);
                            }
                        }

                        //if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                        //{
                        //    type = Nullable.GetUnderlyingType(type);
                        //}

                        //if (value != null)
                        //{
                        //    filterContext.ActionParameters[key] = Convert.ChangeType(value, type);
                        //}
                    }
                    else
                    {
                        filterContext.ActionParameters[key] = queryStrings.Get(key);
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }

        private string Decrypt(string encryptedText)
        {
            string key = "ega+bizportal";
            byte[] DecryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            byte[] inputByte = new byte[encryptedText.Length];

            DecryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByte = Convert.FromBase64String(encryptedText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByte, 0, inputByte.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }

    }
}
