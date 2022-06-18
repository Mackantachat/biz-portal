using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizPortal.Service
{
    public class BusinessService
    {
        public static bool IsCheckBoxCanCheckedOnlyOneForReNew()
        {
            return true;
        }

        public static bool IsCheckBoxCanCheckedOnlyOne()
        {
            return true;
        }

        public static bool IsShowSectionChoice1And2()
        {
            return false;
        }
    }
}