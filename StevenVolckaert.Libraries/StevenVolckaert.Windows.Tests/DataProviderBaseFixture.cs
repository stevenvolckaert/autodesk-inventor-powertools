using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StevenVolckaert.Windows.Tests
{
    internal class MockDataProvider : DataProviderBase
    {
        protected async override Task DoLoadData()
        {
            await Task.Delay(100);
        }
    }

    [TestClass]
    public class DataProviderBaseFixture
    {
        private static Stack<String> _eventStack;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // TODO use Moq framework.

            _eventStack = new Stack<String>();
        }

        private static DataProviderBase GetMockedDataProvider()
        {
            // TODO use Moq framework.

            var returnValue = new MockDataProvider();

            returnValue.DataLoading +=
                (sender, e) =>
                {
                    _eventStack.Push("DataLoading");
                };

            returnValue.DataLoaded +=
                (sender, e) =>
                {
                    _eventStack.Push("DataLoaded");
                };

            return returnValue;
        }

        [TestMethod]
        public async Task LoadDataAsync_Succeeds()
        {
            var dataProvider = GetMockedDataProvider();
            _eventStack.Clear();

            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsFalse(dataProvider.IsDataLoaded);

            var task = dataProvider.LoadDataAsync();

            Assert.IsTrue(dataProvider.IsDataLoading);
            Assert.IsFalse(dataProvider.IsDataLoaded);

            await task;

            Assert.AreEqual(_eventStack.Pop(), "DataLoaded");
            Assert.AreEqual(_eventStack.Pop(), "DataLoading");
            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsTrue(dataProvider.IsDataLoaded);

            var task2 = dataProvider.LoadDataAsync();

            Assert.IsTrue(_eventStack.Count == 0);
            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsTrue(dataProvider.IsDataLoaded);

            await task2;

            Assert.IsTrue(_eventStack.Count == 0);
            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsTrue(dataProvider.IsDataLoaded);
        }

        [TestMethod]
        public async Task LoadDataAsync_DoesNotReloadDataIfAlreadyLoaded()
        {
            var dataProvider = GetMockedDataProvider();

            await dataProvider.LoadDataAsync();

            Assert.IsTrue(dataProvider.IsDataLoaded);

            _eventStack.Clear();
            var task = dataProvider.LoadDataAsync();

            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsTrue(dataProvider.IsDataLoaded);

            await task;

            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsTrue(dataProvider.IsDataLoaded);
            Assert.IsTrue(_eventStack.Count == 0);
        }

        [TestMethod]
        public async Task ReloadDataAsync_Succeeds()
        {
            var dataProvider = GetMockedDataProvider();
            _eventStack.Clear();

            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsFalse(dataProvider.IsDataLoaded);

            await dataProvider.LoadDataAsync();

            Assert.IsTrue(dataProvider.IsDataLoaded);
            Assert.AreEqual(_eventStack.Pop(), "DataLoaded");
            Assert.AreEqual(_eventStack.Pop(), "DataLoading");

            var task = dataProvider.ReloadDataAsync();

            Assert.IsTrue(dataProvider.IsDataLoading);
            Assert.IsFalse(dataProvider.IsDataLoaded);

            await task;

            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsTrue(dataProvider.IsDataLoaded);
            Assert.AreEqual(_eventStack.Pop(), "DataLoaded");
            Assert.AreEqual(_eventStack.Pop(), "DataLoading");
        }
    }
}
