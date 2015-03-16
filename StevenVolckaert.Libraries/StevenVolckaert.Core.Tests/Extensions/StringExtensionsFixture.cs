using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StevenVolckaert.Extensions.Tests
{
    [TestClass]
    public class StringExtensionsFixture
    {
        public enum MockedEnumeration
        {
            Foo,
            Bar
        }

        [TestMethod]
        public void DefaultIfEmptyTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DefaultIfNullOrWhiteSpaceTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void IsDecimalTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void IsInt32Test()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ToListTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ToNullableDecimalTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ToNullableInt32Test()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TryToUpperInvariantTest()
        {
            Assert.AreEqual(" FOO ", " Foo ".TryToUpperInvariant());
            Assert.AreEqual(null, ((String)null).TryToUpperInvariant());
        }

        [TestMethod]
        public void TryTrimTest()
        {
            Assert.AreEqual("Foo", " Foo    ".TryTrim());
            Assert.AreEqual(null, ((String)null).TryTrim());
        }

        [TestMethod]
        public void ParseAs_Succeeds()
        {
            Assert.AreEqual(MockedEnumeration.Foo, "Foo".ParseAs<MockedEnumeration>());
            Assert.AreEqual(MockedEnumeration.Bar, "Bar".ParseAs<MockedEnumeration>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ParseAs_ThrowsArgumentException()
        {
            "Baz".ParseAs<MockedEnumeration>();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ParseAs_ThrowsArgumentNullException()
        {
            string str = null;
            str.ParseAs<MockedEnumeration>();
        }

        [TestMethod]
        public void ParseAsWhileIgnoringCase_Succeeds()
        {
            Assert.AreEqual(MockedEnumeration.Foo, "foo".ParseAs<MockedEnumeration>(ignoreCase: true));
            Assert.AreEqual(MockedEnumeration.Bar, "bar".ParseAs<MockedEnumeration>(ignoreCase: true));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ParseAsWhileIgnoringCase_ThrowsArgumentException()
        {
            "baz".ParseAs<MockedEnumeration>(ignoreCase: true);
        }

        [TestMethod]
        public void TryParseAs_Succeeds()
        {
            Assert.AreEqual(MockedEnumeration.Foo, "Foo".TryParseAs<MockedEnumeration>(MockedEnumeration.Foo));
            Assert.AreEqual(MockedEnumeration.Bar, "Bar".TryParseAs<MockedEnumeration>(MockedEnumeration.Foo));
            Assert.AreEqual(MockedEnumeration.Foo, "Baz".TryParseAs<MockedEnumeration>(MockedEnumeration.Foo));

            string str = null;
            Assert.AreEqual(MockedEnumeration.Bar, str.TryParseAs<MockedEnumeration>(MockedEnumeration.Bar));
        }

        [TestMethod]
        public void TryParseAsWhileIgnoringCase_Succeeds()
        {
            Assert.AreEqual(MockedEnumeration.Foo, "foo".TryParseAs<MockedEnumeration>(MockedEnumeration.Foo, ignoreCase: true));
            Assert.AreEqual(MockedEnumeration.Bar, "bar".TryParseAs<MockedEnumeration>(MockedEnumeration.Foo, ignoreCase: true));
            Assert.AreEqual(MockedEnumeration.Foo, "baz".TryParseAs<MockedEnumeration>(MockedEnumeration.Foo, ignoreCase: true));
        }
    }
}
