using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StevenVolckaert.Tests.Extensions
{
    [TestClass]
    public class Int64ExtensionsFixture
    {
        public Int64ExtensionsFixture()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //private TestContext testContextInstance;

        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

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
        public void UnitOfInformationToStringConversion()
        {
            // assert KilobytesToString(UInt64) extension method
            Assert.AreEqual("0 kB", ((UInt64)0).KilobytesToString());
            Assert.AreEqual("999 kB", ((UInt64)999).KilobytesToString());

            Assert.AreEqual("1 MB", ((UInt64)1000).KilobytesToString());
            Assert.AreEqual("2 MB", ((UInt64)1999).KilobytesToString());
            Assert.AreEqual("2.4 MB", ((UInt64)2440).KilobytesToString());
            Assert.AreEqual("2.5 MB", ((UInt64)2490).KilobytesToString());
            Assert.AreEqual("999 MB", ((UInt64)999000).KilobytesToString());

            Assert.AreEqual("1 GB", ((UInt64)1000000).KilobytesToString());
            Assert.AreEqual("2 GB", ((UInt64)1999000).KilobytesToString());
            Assert.AreEqual("2.4 GB", ((UInt64)2440000).KilobytesToString());
            Assert.AreEqual("2.5 GB", ((UInt64)2490000).KilobytesToString());
            Assert.AreEqual("999 GB", ((UInt64)999000000).KilobytesToString());

            Assert.AreEqual("1 TB", ((UInt64)1000000000).KilobytesToString());
            Assert.AreEqual("1.4 TB", ((UInt64)1400000000).KilobytesToString());
            Assert.AreEqual("2 TB", ((UInt64)1999000000).KilobytesToString());
            Assert.AreEqual("2.4 TB", ((UInt64)2440000000).KilobytesToString());
            Assert.AreEqual("2.5 TB", ((UInt64)2490000000).KilobytesToString());
            Assert.AreEqual("999 TB", ((UInt64)999000000000).KilobytesToString());

            // TODO assert KibibytesToString(UInt64) extension method
            Assert.AreEqual("0 KiB", ((UInt64)0).KibibytesToString());
            Assert.AreEqual("1023 KiB", ((UInt64)1023).KibibytesToString());

            Assert.AreEqual("1 MiB", ((UInt64)1024).KibibytesToString());
            Assert.AreEqual("2 MiB", ((UInt64)2048).KibibytesToString());
            Assert.AreEqual("2.4 MiB", ((UInt64)2458).KibibytesToString());
            Assert.AreEqual("2.5 MiB", ((UInt64)2560).KibibytesToString());
            Assert.AreEqual("999 MiB", ((UInt64)1022976).KibibytesToString());

            Assert.AreEqual("1 GiB", ((UInt64)1048576).KibibytesToString());
            Assert.AreEqual("2 GiB", ((UInt64)2097152).KibibytesToString());
            Assert.AreEqual("2.4 GiB", ((UInt64)2516582).KibibytesToString());
            Assert.AreEqual("2.5 GiB", ((UInt64)2621440).KibibytesToString());
            Assert.AreEqual("999 GiB", ((UInt64)1047527424).KibibytesToString());

            Assert.AreEqual("1 TiB", ((UInt64)1073741824).KibibytesToString());
            Assert.AreEqual("1.4 TiB", ((UInt64)1503238554).KibibytesToString());
            Assert.AreEqual("2 TiB", ((UInt64)2147483648).KibibytesToString());
            Assert.AreEqual("2.4 TiB", ((UInt64)2576980378).KibibytesToString());
            Assert.AreEqual("2.5 TiB", ((UInt64)2684354560).KibibytesToString());
            Assert.AreEqual("999 TiB", ((UInt64)1072668082176).KibibytesToString());
        }
        /*
        public void TestMethod1()
        {
            // arrange
            // act
            // assert
        }
        */
    }
}
