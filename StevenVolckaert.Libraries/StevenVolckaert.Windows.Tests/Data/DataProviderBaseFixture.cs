using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StevenVolckaert.Windows.Data.Tests
{
    [TestClass]
    public class DataProviderBaseFixture
    {
        private class MockDataProvider : DataProviderBase
        {
            protected async override Task DoLoadData()
            {
                await Task.Delay(10);
            }
        }

        //[ClassInitialize]
        //public static void ClassInitialize(TestContext context)
        //{
        //    // TODO Use Moq framework.
        //}

        private static DataProviderBase GetMockDataProvider(Stack<String> eventStack)
        {
            // TODO Use Moq framework.

            var returnValue = new MockDataProvider();

            returnValue.DataLoading += (sender, e) => eventStack.Push("DataLoading");
            returnValue.DataLoaded += (sender, e) => eventStack.Push("DataLoaded");

            return returnValue;
        }

        [TestMethod]
        public async Task LoadDataAsync_Succeeds()
        {
            var eventStack = new Stack<String>();
            var dataProvider = GetMockDataProvider(eventStack);

            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsFalse(dataProvider.IsDataLoaded);

            var task = dataProvider.LoadDataAsync();

            Assert.IsTrue(dataProvider.IsDataLoading);
            Assert.IsFalse(dataProvider.IsDataLoaded);

            await task;

            Assert.AreEqual("DataLoaded", eventStack.Pop());
            Assert.AreEqual("DataLoading", eventStack.Pop());
            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsTrue(dataProvider.IsDataLoaded);

            var task2 = dataProvider.LoadDataAsync();

            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsTrue(dataProvider.IsDataLoaded);

            await task2;

            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsTrue(dataProvider.IsDataLoaded);
        }

        [TestMethod]
        public async Task LoadDataAsync_DoesNotReloadDataIfAlreadyLoaded()
        {
            var eventStack = new Stack<String>();
            var dataProvider = GetMockDataProvider(eventStack);

            await dataProvider.LoadDataAsync();

            Assert.IsTrue(dataProvider.IsDataLoaded);

            eventStack.Clear();
            var task = dataProvider.LoadDataAsync();

            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsTrue(dataProvider.IsDataLoaded);

            await task;

            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsTrue(dataProvider.IsDataLoaded);
            Assert.IsTrue(eventStack.Count == 0);
        }

        [TestMethod]
        public async Task ReloadDataAsync_Succeeds()
        {
            var eventStack = new Stack<String>();
            var dataProvider = GetMockDataProvider(eventStack);

            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsFalse(dataProvider.IsDataLoaded);

            await dataProvider.LoadDataAsync();

            Assert.IsTrue(dataProvider.IsDataLoaded);
            Assert.AreEqual("DataLoaded", eventStack.Pop());
            Assert.AreEqual("DataLoading", eventStack.Pop());

            var task = dataProvider.ReloadDataAsync();

            Assert.IsTrue(dataProvider.IsDataLoading);
            Assert.IsFalse(dataProvider.IsDataLoaded);

            await task;

            Assert.IsFalse(dataProvider.IsDataLoading);
            Assert.IsTrue(dataProvider.IsDataLoaded);
            Assert.AreEqual("DataLoaded", eventStack.Pop());
            Assert.AreEqual("DataLoading", eventStack.Pop());
        }
    }
}
