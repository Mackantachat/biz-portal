using System;
using BizPortal.Utils.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BizPortal.UnitTest
{
    [TestClass]
    public class ResourceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string val0 = ResourceHelper.GetResourceWordWithDefault("APPLICATION", "Application", "Application");
            string val = ResourceHelper.GetResourceWordWithDefault("APP_HOSPITAL_PLAN", "PermitResource.APP_HOSPITAL_PLAN_RES", "APP_HOSPITAL_PLAN");
            Console.WriteLine(val);
        }
    }
}
