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
    public class AddressFormControlTest
    {
        public AddressFormControlTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private FormControl fcCitizen = new FormControl()
        {
            Control = "INFORMATION_STORE__ADDRESS__CONTROL",
            Type = ControlType.AddressForm,
            DataKey = "INFORMATION_STORE__ADDRESS",
            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
            IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Citizen },
            PreFill = true,
            DisplayStaticIfHasData = true,
        };

        private FormControl fcJuristic = new FormControl()
        {
            Control = "INFORMATION_STORE__ADDRESS__CONTROL",
            Type = ControlType.AddressForm,
            DataKey = "INFORMATION_STORE__ADDRESS",
            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
            IdentityTypes = new UserTypeEnum[] { UserTypeEnum.Juristic },
            PreFill = true,
            DisplayStaticIfHasData = true,
        };

        private FormControl fcCommon = new FormControl()
        {
            Control = "INFORMATION_STORE__ADDRESS__CONTROL",
            Type = ControlType.AddressForm,
            DataKey = "INFORMATION_STORE__ADDRESS",
            Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "* Required" } },
            PreFill = true,
            DisplayStaticIfHasData = true,
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
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_FORM", type), false);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_FORM_DATAKEY", type), false);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_MOO_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_SOI_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_BUILDING_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_FLOOR_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_ROAD_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_ROOMNO_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_POSTCODE_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_TELEPHONE_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_TELEPHONE_EXT_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_FAX_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_LAT_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_LNG_INFORMATION_STORE__ADDRESS", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("INFORMATION_STORE_EMAIL", type), false);
            Assert.AreEqual(fc.isCompatibleWithDataKey("INFORMATION_STORE_LOCAL_ADMINISTRATIVE", type), false);
            Assert.AreEqual(fc.isCompatibleWithDataKey("INFORMATION_STORE_HEALTH_OTHER", type), false);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_PROVINCE_INFORMATION_STORE__ADDRESS_TEXT", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_AMPHUR_INFORMATION_STORE__ADDRESS_TEXT", type), true);
            Assert.AreEqual(fc.isCompatibleWithDataKey("ADDRESS_TUMBOL_INFORMATION_STORE__ADDRESS_TEXT", type), true);
        }

        [TestMethod]
        public void TestIsCompatibleWithDataKeyCitizenOnly()
        {
            shouldBeCompatible(fcCitizen, UserTypeEnum.Citizen);
            Assert.AreEqual(fcCitizen.isCompatibleWithDataKey("ADDRESS_INFORMATION_STORE__ADDRESS", UserTypeEnum.Juristic), false);
        }

        [TestMethod]
        public void TestIsCompatibleWithDataKeyJuristicOnly()
        {
            shouldBeCompatible(fcJuristic, UserTypeEnum.Juristic);
            Assert.AreEqual(fcJuristic.isCompatibleWithDataKey("ADDRESS_INFORMATION_STORE__ADDRESS", UserTypeEnum.Citizen), false);
        }

        [TestMethod]
        public void TestIsCompatibleWithDataKeyCommon()
        {
            shouldBeCompatible(fcCommon, UserTypeEnum.Citizen);
            shouldBeCompatible(fcCommon, UserTypeEnum.Juristic);
        }
    }
}
