using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Extensions
{
    public class WarningException : BizPortalException
    {
        public WarningException(string message) : base(message)
        {
        }
    }
}
