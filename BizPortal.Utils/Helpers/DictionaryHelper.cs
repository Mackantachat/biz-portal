using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public static class DictionaryHelper
    {
        public static string GetValue(this Dictionary<string, string> dict, string key)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            else
            {
                return null;
            }
        }
    }
}
