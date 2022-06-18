using EGA.WS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public class HolidayHelper
    {
        public static object GetHoliday()
        {
            var result = (object)null;

            try
            {
                var api = EGAWSAPI.CreateInstance(ConfigurationValues.ConsumerKey, ConfigurationValues.ConsumerSecret);
                var param = new Dictionary<string, string> { { "id", DateTime.Now.ToString("yyyy", new System.Globalization.CultureInfo("en-US")) } };
                var res = api.Get(ConfigurationValues.GetHolliday, param);

                if (res != null && res["Result"] != null)
                {
                    return res["Result"];
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
