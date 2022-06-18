using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BizPortal.DAL.MongoDB.Permit;

namespace BizPortal.UnitTest.Permit
{
    [TestClass]
    public class Permit_5
    {
        [TestMethod]
        public static void Init()
        {
            FormPermit_5.InitFormSectionGroup();
            FormPermit_5.InitFormSection();
            FormPermit_5.InitFormSectionRow();
            FormPermit_5.InitFormAppFile();
            FormPermit_5.InitFormFileList();
        }
    }
}
