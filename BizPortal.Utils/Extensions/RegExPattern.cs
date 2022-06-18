using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Extensions
{
    public static class RegExPattern
    {
        public const string EMAIL = @"[\w-]+([\.]?[\w-])*\@[\w-]+([\.][\w-]+)+";
        public const string ACCOUNTS_CODE = @"^[0-9]{1,50}$";
        public const string WEBSITE = @"^(http\:\/\/|https\:\/\/)?([a-z0-9][a-z0-9\-]*\.)+[a-z0-9][a-z0-9\-]*$@i";

        //public const string VAT_REGEX = @"[\^\|\&\<\>\[\]\{\}\x00\x1a\x1b\x1c\x1d\x1e\x1f\x7f\xa0\xb0\xc0\xd0\xf0]";
        public const string VAT_REGEX = @"[\^\|]";
    }
}
