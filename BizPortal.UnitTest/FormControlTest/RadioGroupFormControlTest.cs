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
    public class RadioGroupFormControlTest
    {
        public RadioGroupFormControlTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private FormControl fcCitizen = new FormControl()
        {
            Control = "SELL_ALCOHOL_PRODUCTION_TYPE_CONTROL",
            Type = ControlType.RadioGroup,
            DataKey = "SELL_ALCOHOL_PRODUCTION_TYPE",
            IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
            RadioGroup = new FormRadioGroup()
            {
                RadioGroupName = "SELL_ALCOHOL_PRODUCTION_TYPE_OPTION",
            },
        };

        private FormControl fcJuristic = new FormControl()
        {
            Control = "SELL_ALCOHOL_PRODUCTION_TYPE_CONTROL",
            Type = ControlType.RadioGroup,
            DataKey = "SELL_ALCOHOL_PRODUCTION_TYPE",
            IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
            RadioGroup = new FormRadioGroup()
            {
                RadioGroupName = "SELL_ALCOHOL_PRODUCTION_TYPE_OPTION",
            },
        };

        private FormControl fcCommon = new FormControl()
        {
            Control = "SELL_ALCOHOL_PRODUCTION_TYPE_CONTROL",
            Type = ControlType.RadioGroup,
            DataKey = "SELL_ALCOHOL_PRODUCTION_TYPE",
            RadioGroup = new FormRadioGroup()
            {
                RadioGroupName = "SELL_ALCOHOL_PRODUCTION_TYPE_OPTION",
            },
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
            Assert.AreEqual(fc.isCompatibleWithDataKey("SELL_ALCOHOL_PRODUCTION_TYPE_CONTROL_OPTION", type), false);
            Assert.AreEqual(fc.isCompatibleWithDataKey("SELL_ALCOHOL_PRODUCTION_TYPE_CONTROL", type), false);
            Assert.AreEqual(fc.isCompatibleWithDataKey("SELL_ALCOHOL_PRODUCTION_TYPE", type), false);
            Assert.AreEqual(fc.isCompatibleWithDataKey("SELL_ALCOHOL_PRODUCTION_TYPE_OPTION", type), true);
        }

        [TestMethod]
        public void TestIsCompatibleWithDataKeyCitizenOnly()
        {
            shouldBeCompatible(fcCitizen, UserTypeEnum.Citizen);
            Assert.AreEqual(fcCitizen.isCompatibleWithDataKey("SELL_ALCOHOL_PRODUCTION_TYPE_OPTION", UserTypeEnum.Juristic), false);
        }

        [TestMethod]
        public void TestIsCompatibleWithDataKeyJuristicOnly()
        {
            shouldBeCompatible(fcJuristic, UserTypeEnum.Juristic);
            Assert.AreEqual(fcJuristic.isCompatibleWithDataKey("SELL_ALCOHOL_PRODUCTION_TYPE_OPTION", UserTypeEnum.Citizen), false);
        }

        [TestMethod]
        public void TestIsCompatibleWithDataKeyCommon()
        {
            shouldBeCompatible(fcCommon, UserTypeEnum.Citizen);
            shouldBeCompatible(fcCommon, UserTypeEnum.Juristic);
        }
    }
}
