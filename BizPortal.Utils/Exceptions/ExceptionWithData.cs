using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Exceptions
{
    public class ExceptionWithData : Exception
    {
        public object CustomData { get; set; }

        public ExceptionWithData(string message, object customData)
            : base (message)
        {
            CustomData = customData;
        }
    }
}
