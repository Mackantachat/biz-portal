using BizPortal.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.UnitTest
{
    [TestClass]
    public class SeedTest
    {
        [TestMethod]
        public void TestSeed()
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                BizPortal.DAL.Migrations.Configuration config = new BizPortal.DAL.Migrations.Configuration();
                config.DebugSeed(context);
            }
            catch (Exception e)
            {
            }
        }
    }
}
