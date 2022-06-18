using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public static class ResourceHelper
    {
        public static string GetResourceWord(string name, string resourceName, string culture = "")
        {
            var type = typeof(BizPortal.Resources.Global);
            var assembly = type.Assembly;

            ResourceManager rm = new ResourceManager(string.Format("{0}.{1}", type.Namespace, resourceName), assembly);

            if (string.IsNullOrEmpty(culture))
            {
                return rm.GetString(name);
            }
            else
            {
                return rm.GetString(name, new CultureInfo(culture));
            }
        }

        public static string GetResourceWordWithDefault(string name, string resourceName, string defaultValue = "", string culture = "")
        {
            try
            {
                var type = typeof(BizPortal.Resources.Global);
                var assembly = type.Assembly;
                ResourceManager rm = new ResourceManager(string.Format("{0}.{1}", type.Namespace, resourceName), assembly);
                if (string.IsNullOrEmpty(culture))
                {
                    string word = rm.GetString(name);
                    if (!string.IsNullOrEmpty(word))
                    {
                        return word;
                    }
                    else
                    {
                        return defaultValue;
                    }
                }
                else
                {
                    string word = rm.GetString(name, CultureInfo.CreateSpecificCulture(culture));
                    if (!string.IsNullOrEmpty(word))
                    {
                        return word;
                    }
                    else
                    {
                        return defaultValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return defaultValue;
            }
        }
    }
}
