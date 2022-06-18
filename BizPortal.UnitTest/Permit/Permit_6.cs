using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BizPortal.DAL.MongoDB.Permit;

namespace BizPortal.UnitTest.Permit
{
    [TestClass]
    public class Permit_6
    {
        [TestMethod]
        public static void Init()
        {
            FormPermit_6.InitFormSectionGroup();
            FormPermit_6.InitFormSection();
            FormPermit_6.InitFormSectionRow();
            FormPermit_6.InitFormAppFile();
            FormPermit_6.InitFormFileList();
        }
    }
}
