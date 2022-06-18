using BizPortal.Utils.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BizPortal.UnitTest.ExtensionTest
{
    [TestClass]
    public class RegExExtensionTest
    {
        [TestMethod]
        public void VatRegExTest()
        {
            var pattern = RegExPattern.VAT_REGEX;

            var obj = new
            {
                A = "ทดสอบ",
                B = "<ทดสอบ>",
                C = "[ทดสอบ]",
                D = "!ทดสอบ"
            };

            var isMatch = obj.AdjustRegEx(pattern);

            Assert.IsTrue(isMatch);
        }

        [TestMethod]
        public void RegExReplaceTest()
        {
            var pattern = @"(.{1})(.{4})(.{5})(.{2})";

            var str = "1100501076368";

            var regexStr = Regex.Replace(str, pattern, "$1-$2-$3-$4-");

            Assert.AreEqual("1-1005-01076-36-8", regexStr);
        }
    }
}
