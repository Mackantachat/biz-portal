using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.JsonConverter
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format, string culture = "en")
        {
            Culture = new System.Globalization.CultureInfo(culture);
            DateTimeFormat = format;
        }
    }
}
