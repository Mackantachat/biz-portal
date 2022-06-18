using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.JsonConverter
{
    public class MonthDayYearConverter : IsoDateTimeConverter
    {
        public MonthDayYearConverter()
        {
            DateTimeFormat = "MM/dd/yyyy";
        }
    }
}
