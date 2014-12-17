using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StevenVolckaert.Tests
{
    [TestClass]
    public class IEnumerableExtensionsFixture
    {
        [TestMethod]
        public void ExceptTest()
        {
            var src = new List<String> { "foo", "bar", "baz" };
            var act = src.Except("bar");

            Assert.IsTrue(act.Count() == 2);
            Assert.IsFalse(act.Contains("bar"));

            src = new List<String> { "foo", "bar", "baz" };
            act = src.Except("xxx");
            Assert.IsTrue(act.Count() == 3);
            Assert.IsFalse(act.Contains("xxx"));
        }

        [TestMethod]
        public void IsSubsetOf_ReturnsTrue()
        {
            var src = new List<String> { "foo", "bar", "baz" };
            var oth = new List<String> { "x", "baz", "1", "bar", "foo", "y", "something", "z", "else" };
            var res = src.IsSubsetOf(oth);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void IsSubsetOf_ReturnsFalse()
        {
            var src = new List<String> { "foo", "bar", "baz" };
            var oth = new List<String> { "x", "bax", "1", "bar", "foo", "y", "something", "z", "else" };
            var res = src.IsSubsetOf(oth);
            Assert.IsFalse(res);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void OrderByOrdinal_ThrowsImmediatelyWhenArgumentIsNull()
        {
            String[] src = null;
            src.OrderByOrdinal(x => x);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void OrderByOrdinal_ThrowsImmediatelyWhenKeySelectorIsNull()
        {
            var src = new String[] { "foo 11", "baz 11", "foo", "bar", "baz", "foo 1", "bar 20", "foo 10", "bar 1", "baz 2" };
            src.OrderByOrdinal(null);
        }

        [TestMethod]
        public void OrderByOrdinalTest()
        {
            var src = new String[] { "foo 11", "baz 11", "foo", "bar", "baz", "foo 1", "bar 20", "foo 10", "bar 1", "baz 2" };
            var exp = new String[] { "bar", "bar 1", "bar 20", "baz", "baz 2", "baz 11", "foo", "foo 1", "foo 10", "foo 11" };
            var act = src.OrderByOrdinal(x => x).ToArray();

            CollectionAssert.AreEqual(exp, act);

            src = new String[] { "1", "10", "100", "101", "102", "11", "12", "13", "14", "15", "16", "17", "18", "19", "2", "20", "3", "4", "5", "6", "7", "8", "9" };
            exp = new String[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "100", "101", "102" };
            act = src.OrderByOrdinal(x => x).ToArray();

            CollectionAssert.AreEqual(exp, act);

            src = new String[] { "File 1.txt", "File 10.txt", "File 11.csv", "File 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            exp = new String[] { "File 1.txt", "File 2.jpg", "File 3.doc", "File 10.txt", "File 11.csv", "File 20.xls", "File 21.ppt" };
            act = src.OrderByOrdinal(x => x).ToArray();

            CollectionAssert.AreEqual(exp, act);

            // Verify case-sensitive ordering.
            src = new String[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            exp = new String[] { "file 2.jpg", "file 10.txt", "File 1.txt", "File 3.doc", "File 11.csv", "File 20.xls", "File 21.ppt" };
            act = src.OrderByOrdinal(x => x).ToArray();

            CollectionAssert.AreEqual(exp, act);
        }

        [TestMethod]
        public void OrderByOrdinalCaseInsensitiveTest()
        {
            var src = new String[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "file 20.xls", "File 21.ppt", "File 3.doc" };
            var exp = new String[] { "File 1.txt", "file 2.jpg", "File 3.doc", "file 10.txt", "File 11.csv", "file 20.xls", "File 21.ppt" };
            var act = src.OrderByOrdinal(x => x, ignoreCase: true).ToArray();

            CollectionAssert.AreEqual(exp, act);
        }

        [TestMethod]
        public void OrderByOrdinalDescendingTest()
        {
            var src = new String[] { "foo 11", "baz 11", "foo", "bar", "baz", "foo 1", "bar 20", "foo 10", "bar 1", "baz 2" };
            var exp = new String[] { "foo 11", "foo 10", "foo 1", "foo", "baz 11", "baz 2", "baz", "bar 20", "bar 1", "bar" };
            var act = src.OrderByOrdinalDescending(x => x).ToArray();

            CollectionAssert.AreEqual(exp, act);

            src = new String[] { "1", "10", "100", "101", "102", "11", "12", "13", "14", "15", "16", "17", "18", "19", "2", "20", "3", "4", "5", "6", "7", "8", "9" };
            exp = new String[] { "102", "101", "100", "20", "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "9", "8", "7", "6", "5", "4", "3", "2", "1" };
            act = src.OrderByOrdinalDescending(x => x).ToArray();

            CollectionAssert.AreEqual(exp, act);

            src = new String[] { "File 1.txt", "File 10.txt", "File 11.csv", "File 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            exp = new String[] { "File 21.ppt", "File 20.xls", "File 11.csv", "File 10.txt", "File 3.doc", "File 2.jpg", "File 1.txt" };
            act = src.OrderByOrdinalDescending(x => x).ToArray();

            CollectionAssert.AreEqual(exp, act);

            // Verify case-sensitive ordering.
            src = new String[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            exp = new String[] { "File 21.ppt", "File 20.xls", "File 11.csv", "File 3.doc", "File 1.txt", "file 10.txt", "file 2.jpg" };
            act = src.OrderByOrdinalDescending(x => x).ToArray();

            CollectionAssert.AreEqual(exp, act);
        }

        [TestMethod]
        public void OrderByOrdinalDescendingCaseInsensitiveTest()
        {
            var src = new String[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "file 20.xls", "File 21.ppt", "File 3.doc" };
            var exp = new String[] { "File 21.ppt", "file 20.xls", "File 11.csv", "file 10.txt", "File 3.doc", "file 2.jpg", "File 1.txt" };
            var act = src.OrderByOrdinalDescending(x => x, ignoreCase: true).ToArray();

            CollectionAssert.AreEqual(exp, act);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ToObservableCollectionTest_ThrowsImmediatelyWhenArgumentIsNull()
        {
            String[] src = null;
            var collection = src.ToObservableCollection();
        }

        [TestMethod]
        public void ToStringTest()
        {
            var src = new String[] { "File 1.txt", "file 10.txt", "File 11.csv" };
            var exp = "File 1.txt; file 10.txt; File 11.csv";
            var act = src.ToString(separator: "; ");

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void UnionTest()
        {
            var src = new List<String> { "foo", "bar" };
            var act = src.Union("foo").Union("baz");
            Assert.IsTrue(act.Count() == 3);
            Assert.IsTrue(act.Contains("baz"));
        }
    }
}
