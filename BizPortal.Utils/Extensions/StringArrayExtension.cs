using System.Collections.Generic;
using System.Linq;

namespace BizPortal.Utils.Extensions
{
    public static class StringArrayExtension
    {
        
        public static string[] ConcatItems(this string[] arr, params string[] arr2)
        {
            var ret = new List<string>(arr);
            ret.AddRange(arr2);
            return ret.Distinct().ToArray();
        }
    }
}
