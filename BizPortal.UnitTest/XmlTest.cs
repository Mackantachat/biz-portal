using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;
using EGA.Owin.Security.EGAOAuth.Models;
using System.IO;

namespace BizPortal.UnitTest
{
    [TestClass]
    public class XmlTest
    {
        [TestMethod]
        public void Deserialize()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Member), string.Empty);
            Member m = null;
            using (FileStream fs = new FileStream(@"C:\Clouds\Copy\EGA\ระบบสารสนเทศเพื่ออำนวยความสะดวกในการเริ่มต้นธุรกิจ (Starting Business)\oauth data xml.xml", FileMode.Open))
            {
                m = (Member)serializer.Deserialize(fs);
            }
            Assert.IsNotNull(m);
        }
    }
}
