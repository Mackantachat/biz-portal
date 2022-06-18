using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BizPortal.DAL.MongoDB;

namespace BizPortal.UnitTest.FormSectionTest
{
    /// <summary>
    /// Summary description for FormControlTest
    /// </summary>
    [TestClass]
    public class FormSectionTest
    {
        public FormSectionTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private FormSection form = new FormSection()
        {
            Section = "SECTION",
            Type = SectionType.Form,
        };

        private FormSection arrayOfForms = new FormSection()
        {
            Section = "SECTION",
            Type = SectionType.ArrayOfForms,
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
        
        

        [TestMethod]
        public void TestIsCompatibleWithDataKeyForm()
        {
            Assert.AreEqual(form.isCompatibleWithDataKey("SECTION_TOTAL", UserTypeEnum.Citizen), false);
            Assert.AreEqual(form.isCompatibleWithDataKey("SECTION_TOTAL", UserTypeEnum.Juristic), false);

            Assert.AreEqual(form.isCompatibleWithDataKey("ARR_ITEM_ID_0", UserTypeEnum.Citizen), false);
            Assert.AreEqual(form.isCompatibleWithDataKey("ARR_ITEM_ID_0", UserTypeEnum.Juristic), false);
            Assert.AreEqual(form.isCompatibleWithDataKey("ARR_ITEM_ID_99", UserTypeEnum.Citizen), false);
            Assert.AreEqual(form.isCompatibleWithDataKey("ARR_ITEM_ID_99", UserTypeEnum.Juristic), false);
        }

        [TestMethod]
        public void TestIsCompatibleWithDataKeyArrayOfForm()
        {
            Assert.AreEqual(arrayOfForms.isCompatibleWithDataKey("SECTION_TOTAL", UserTypeEnum.Citizen), true);
            Assert.AreEqual(arrayOfForms.isCompatibleWithDataKey("SECTION_TOTAL", UserTypeEnum.Juristic), true);

            Assert.AreEqual(arrayOfForms.isCompatibleWithDataKey("ARR_ITEM_ID_0", UserTypeEnum.Citizen), true);
            Assert.AreEqual(arrayOfForms.isCompatibleWithDataKey("ARR_ITEM_ID_0", UserTypeEnum.Juristic), true);
            Assert.AreEqual(arrayOfForms.isCompatibleWithDataKey("ARR_ITEM_ID_99", UserTypeEnum.Citizen), true);
            Assert.AreEqual(arrayOfForms.isCompatibleWithDataKey("ARR_ITEM_ID_99", UserTypeEnum.Juristic), true);
        }

        [TestMethod]
        public void TestRemoveIndexFromDataKey()
        {
            Assert.AreEqual(form.RemoveIndexFromDataKey("SECTION_AA"), "SECTION_AA");
            Assert.AreEqual(form.RemoveIndexFromDataKey("ARR_ITEM_ID_0"), "ARR_ITEM_ID_0");
            Assert.AreEqual(form.RemoveIndexFromDataKey("ARR_ITEM_ID_0"), "ARR_ITEM_ID_0");
            Assert.AreEqual(form.RemoveIndexFromDataKey("ARR_ITEM_ID_99"), "ARR_ITEM_ID_99");
            Assert.AreEqual(form.RemoveIndexFromDataKey("ARR_ITEM_ID_99"), "ARR_ITEM_ID_99");

            Assert.AreEqual(arrayOfForms.RemoveIndexFromDataKey("SECTION_AA"), "SECTION_AA");
            Assert.AreEqual(arrayOfForms.RemoveIndexFromDataKey("ARR_ITEM_ID_0"), "ARR_ITEM_ID");
            Assert.AreEqual(arrayOfForms.RemoveIndexFromDataKey("ARR_ITEM_ID_0"), "ARR_ITEM_ID");
            Assert.AreEqual(arrayOfForms.RemoveIndexFromDataKey("ARR_ITEM_ID_99"), "ARR_ITEM_ID");
            Assert.AreEqual(arrayOfForms.RemoveIndexFromDataKey("ARR_ITEM_ID_99"), "ARR_ITEM_ID");
        }
    }
}
