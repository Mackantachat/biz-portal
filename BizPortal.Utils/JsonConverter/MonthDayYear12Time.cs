using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.JsonConverter
{
    public class MonthDayYear12Time : IsoDateTimeConverter
    {
        public MonthDayYear12Time()
        {
            DateTimeFormat = "MM/dd/yyyy HH:mm tt";
        }
    }
}
