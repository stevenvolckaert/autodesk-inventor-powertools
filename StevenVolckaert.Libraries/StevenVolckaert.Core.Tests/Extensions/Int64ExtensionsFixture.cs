using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StevenVolckaert.Extensions.Tests
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
        public void BytesToStringConversion_None()
        {
            Assert.AreEqual("0 B", ((UInt64)0).BytesToString(UnitOfInformationPrefix.None));
            Assert.AreEqual("999 B", ((UInt64)999).BytesToString(UnitOfInformationPrefix.None));
            Assert.AreEqual("1000 B", ((UInt64)1000).BytesToString(UnitOfInformationPrefix.None));
            Assert.AreEqual("123456 B", ((UInt64)123456).BytesToString(UnitOfInformationPrefix.None));
        }

        [TestMethod]
        public void BytesToStringConversion_Decimal()
        {
            Assert.AreEqual("0 B", ((UInt64)0).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("999 B", ((UInt64)999).BytesToString(UnitOfInformationPrefix.Decimal));

            Assert.AreEqual("1 kB", ((UInt64)1000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("999 kB", ((UInt64)999000).BytesToString(UnitOfInformationPrefix.Decimal));

            Assert.AreEqual("1 MB", ((UInt64)1000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("2 MB", ((UInt64)1999000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("2.4 MB", ((UInt64)2440000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("2.5 MB", ((UInt64)2490000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("999 MB", ((UInt64)999000000).BytesToString(UnitOfInformationPrefix.Decimal));

            Assert.AreEqual("1 GB", ((UInt64)1000000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("1.4 GB", ((UInt64)1400000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("2 GB", ((UInt64)1999000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("2.4 GB", ((UInt64)2440000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("2.5 GB", ((UInt64)2490000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("999 GB", ((UInt64)999000000000).BytesToString(UnitOfInformationPrefix.Decimal));

            Assert.AreEqual("1 TB", ((UInt64)1000000000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("1.4 TB", ((UInt64)1400000000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("2 TB", ((UInt64)1999000000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("2.4 TB", ((UInt64)2440000000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("2.5 TB", ((UInt64)2490000000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("999 TB", ((UInt64)999000000000000).BytesToString(UnitOfInformationPrefix.Decimal));

            Assert.AreEqual("1 PB", ((UInt64)1000000000000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("1.4 PB", ((UInt64)1400000000000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("2 PB", ((UInt64)1999000000000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("2.4 PB", ((UInt64)2440000000000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("2.5 PB", ((UInt64)2490000000000000).BytesToString(UnitOfInformationPrefix.Decimal));
            Assert.AreEqual("999 PB", ((UInt64)999000000000000000).BytesToString(UnitOfInformationPrefix.Decimal));
        }

        [TestMethod]
        public void BytesToStringConversion_Binary()
        {
            Assert.AreEqual("0 B", ((UInt64)0).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("1023 B", ((UInt64)1023).BytesToString(UnitOfInformationPrefix.Binary));

            Assert.AreEqual("1 KiB", ((UInt64)1024).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2 KiB", ((UInt64)2048).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2.4 KiB", ((UInt64)2458).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2.5 KiB", ((UInt64)2560).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("999 KiB", ((UInt64)1022976).BytesToString(UnitOfInformationPrefix.Binary));

            Assert.AreEqual("1 MiB", ((UInt64)1048576).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2 MiB", ((UInt64)2097152).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2.4 MiB", ((UInt64)2516582).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2.5 MiB", ((UInt64)2621440).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("999 MiB", ((UInt64)1047527424).BytesToString(UnitOfInformationPrefix.Binary));

            Assert.AreEqual("1 GiB", ((UInt64)1073741824).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2 GiB", ((UInt64)2147483648).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2.4 GiB", ((UInt64)2576980378).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2.5 GiB", ((UInt64)2684354560).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("999 GiB", ((UInt64)1072668082176).BytesToString(UnitOfInformationPrefix.Binary));

            Assert.AreEqual("1 TiB", ((UInt64)1099511627776).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2 TiB", ((UInt64)2199023255552).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2.4 TiB", ((UInt64)2638827906662).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2.5 TiB", ((UInt64)2748779069440).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("999 TiB", ((UInt64)1098412116148224).BytesToString(UnitOfInformationPrefix.Binary));

            Assert.AreEqual("1 PiB", ((UInt64)1125899906842624).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2 PiB", ((UInt64)2251799813685248).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2.4 PiB", ((UInt64)2702159776422298).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("2.5 PiB", ((UInt64)2814749767106560).BytesToString(UnitOfInformationPrefix.Binary));
            Assert.AreEqual("999 PiB", ((UInt64)1124774006935781376).BytesToString(UnitOfInformationPrefix.Binary));
        }
    }
}
