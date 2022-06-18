using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BizPortal.DAL.MongoDB;

namespace BizPortal.UnitTest.FormControlTest
{
    /// <summary>
    /// Summary description for FormControlTest
    /// </summary>
    [TestClass]
    public class DropDownFormControlTest
    {
        public DropDownFormControlTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private FormControl fcCitizen = new FormControl()
        {
            Control = "CITIZEN_TITLE_CONTROL",
            Type = ControlType.Dropdown,
            IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
            DataKey = "CITIZEN_TITLE",
        };

        private FormControl fcJuristic = new FormControl()
        {
            Control = "CITIZEN_TITLE_CONTROL",
            Type = ControlType.Dropdown,
            IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
            DataKey = "CITIZEN_TITLE",
        };

        private FormControl fcCommon = new FormControl()
        {
            Control = "CITIZEN_TITLE_CONTROL",
            Type = ControlType.Dropdown,
            DataKey = "CITIZEN_TITLE",
        };

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        
        private void shouldBeCompatible(FormControl fc, UserTypeEnum type)
        {
            Assert.AreEqual(fc.isCompatibleWithDataKey("DROPDOWN_CITIZEN_TITLE_CONTROL", type), false);
            Assert.AreEqual(fc.isCompatibleWithDataKey("DROPDOWN_CITIZEN_TITLE_CONTROL_TEXT", type), false);
            Assert.AreEqual(fc.isCompatibleWithDataKey("CITIZEN_TITLE", type), false);
            Assert.AreEqual(fc.isCompatibleWithDataKey("DROPDOWN_CITIZEN_TITLE", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("DROPDOWN_CITIZEN_TITLE_TEXT", type), true);
        }

        [TestMethod]
        public void TestIsCompatibleWithDataKeyCitizenOnly()
        {
            shouldBeCompatible(fcCitizen, UserTypeEnum.Citizen);
            Assert.AreEqual(fcCitizen.isCompatibleWithDataKey("DROPDOWN_CITIZEN_TITLE", UserTypeEnum.Juristic), false);
        }

        [TestMethod]
        public void TestIsCompatibleWithDataKeyJuristicOnly()
        {
            shouldBeCompatible(fcJuristic, UserTypeEnum.Juristic);
            Assert.AreEqual(fcJuristic.isCompatibleWithDataKey("DROPDOWN_CITIZEN_TITLE", UserTypeEnum.Citizen), false);
        }

        [TestMethod]
        public void TestIsCompatibleWithDataKeyCommon()
        {
            shouldBeCompatible(fcCommon, UserTypeEnum.Citizen);
            shouldBeCompatible(fcCommon, UserTypeEnum.Juristic);
        }
    }
}
