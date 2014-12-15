using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StevenVolckaert.Windows.Mvvm;

namespace StevenVolckaert.Windows.Tests.Mvvm
{
    [TestClass]
    public class DataViewModelBaseFixture
    {
        private class MockDataViewModel : DataViewModelBase
        {
            protected override async Task DoLoadData()
            {
                await Task.Delay(10);
                // Data = <SomeData>;
            }

            protected override async Task DoSaveData()
            {
                await Task.Delay(10);
            }
        }

        private static DataViewModelBase GetMockDataViewModel(Stack<String> eventStack)
        {
            // TODO Use Moq framework.

            var returnValue = new MockDataViewModel();

            returnValue.DataLoading += (sender, e) => eventStack.Push("DataLoading");
            returnValue.DataLoaded += (sender, e) => eventStack.Push("DataLoaded");
            returnValue.DataSaving += (sender, e) => eventStack.Push("DataSaving");
            returnValue.DataSaved += (sender, e) => eventStack.Push("DataSaved");

            return returnValue;
        }

        [TestMethod]
        public async Task SaveDataAsync_Succeeds()
        {
            var eventStack = new Stack<String>();
            var viewModel = GetMockDataViewModel(eventStack);

            Assert.IsFalse(viewModel.IsDataSaving);
            Assert.IsFalse(viewModel.IsDataSaved);
            Assert.IsFalse(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Idle);

            var task1 = viewModel.SaveDataAsync();

            Assert.AreEqual(eventStack.Pop(), "DataSaving");
            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsTrue(viewModel.IsDataSaving);
            Assert.IsFalse(viewModel.IsDataSaved);
            Assert.IsTrue(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Saving);

            await task1;

            Assert.AreEqual(eventStack.Pop(), "DataSaved");
            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsFalse(viewModel.IsDataSaving);
            Assert.IsTrue(viewModel.IsDataSaved);
            Assert.IsFalse(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Idle);

            var task2 = viewModel.SaveDataAsync();

            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsFalse(viewModel.IsDataSaving);
            Assert.IsTrue(viewModel.IsDataSaved);
            Assert.IsFalse(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Idle);

            await task2;

            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsFalse(viewModel.IsDataSaving);
            Assert.IsTrue(viewModel.IsDataSaved);
            Assert.IsFalse(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Idle);

            viewModel.IsDataSaved = false;

            var task3 = viewModel.SaveDataAsync();

            Assert.AreEqual(eventStack.Pop(), "DataSaving");
            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsTrue(viewModel.IsDataSaving);
            Assert.IsFalse(viewModel.IsDataSaved);
            Assert.IsTrue(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Saving);

            await task3;

            Assert.AreEqual(eventStack.Pop(), "DataSaved");
            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsFalse(viewModel.IsDataSaving);
            Assert.IsTrue(viewModel.IsDataSaved);
            Assert.IsFalse(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Idle);
        }

        [TestMethod]
        public async Task LoadDataAsync_Succeeds()
        {
            var eventStack = new Stack<String>();
            var viewModel = GetMockDataViewModel(eventStack);

            Assert.IsFalse(viewModel.IsDataLoading);
            Assert.IsFalse(viewModel.IsDataLoaded);
            Assert.IsFalse(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Idle);

            var task1 = viewModel.LoadDataAsync();

            Assert.AreEqual(eventStack.Pop(), "DataLoading");
            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsTrue(viewModel.IsDataLoading);
            Assert.IsFalse(viewModel.IsDataLoaded);
            Assert.IsTrue(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Loading);

            await task1;

            Assert.AreEqual(eventStack.Pop(), "DataLoaded");
            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsFalse(viewModel.IsDataLoading);
            Assert.IsTrue(viewModel.IsDataLoaded);
            Assert.IsFalse(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Idle);

            var task2 = viewModel.LoadDataAsync();

            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsFalse(viewModel.IsDataLoading);
            Assert.IsTrue(viewModel.IsDataLoaded);
            Assert.IsFalse(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Idle);

            await task2;

            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsFalse(viewModel.IsDataLoading);
            Assert.IsTrue(viewModel.IsDataLoaded);
            Assert.IsFalse(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Idle);

            var task3 = viewModel.ReloadDataAsync();

            Assert.AreEqual(eventStack.Pop(), "DataLoading");
            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsTrue(viewModel.IsDataLoading);
            Assert.IsFalse(viewModel.IsDataLoaded);
            Assert.IsTrue(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Loading);

            await task3;

            Assert.AreEqual(eventStack.Pop(), "DataLoaded");
            Assert.IsTrue(eventStack.Count == 0);
            Assert.IsFalse(viewModel.IsDataLoading);
            Assert.IsTrue(viewModel.IsDataLoaded);
            Assert.IsFalse(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Idle);
        }

        [TestMethod]
        public async Task SaveDataCommand_CanExecute()
        {
            var eventStack = new Stack<String>();
            var viewModel = GetMockDataViewModel(eventStack);

            var task1 = viewModel.SaveDataCommand.Execute();

            Assert.AreEqual(eventStack.Pop(), "DataSaving");
            Assert.IsTrue(eventStack.Count == 0);

            await task1;

            Assert.AreEqual(eventStack.Pop(), "DataSaved");
            Assert.IsTrue(eventStack.Count == 0);
        }

        [TestMethod]
        public async Task ResaveDataAsync_Succeeds()
        {
            var eventStack = new Stack<String>();
            var viewModel = GetMockDataViewModel(eventStack);

            await viewModel.SaveDataAsync();
            eventStack.Clear();

            var task = viewModel.SaveDataAsync(ignoreState: true);

            Assert.IsTrue(viewModel.IsDataSaving);
            Assert.IsFalse(viewModel.IsDataSaved);
            Assert.IsTrue(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Saving);

            await task;

            Assert.AreEqual(eventStack.Pop(), "DataSaved");
            Assert.AreEqual(eventStack.Pop(), "DataSaving");
            Assert.IsFalse(viewModel.IsDataSaving);
            Assert.IsTrue(viewModel.IsDataSaved);
            Assert.IsFalse(viewModel.IsBusy);
            Assert.AreEqual(viewModel.Status, Properties.Resources.Idle);
        }

        private class DataViewModelWithoutDataOperations : DataViewModelBase
        {
        }

        [TestMethod, ExpectedException(typeof(NotImplementedException))]
        public async Task DataViewModelWithoutDataOperations_ThrowsOnSaveDataAsync()
        {
            await new DataViewModelWithoutDataOperations().SaveDataAsync();
        }

        [TestMethod, ExpectedException(typeof(NotImplementedException))]
        public async Task DataViewModelWithoutDataOperations_ThrowsOnLoadDataAsync()
        {
            await new DataViewModelWithoutDataOperations().LoadDataAsync();
        }

        [TestMethod, ExpectedException(typeof(NotImplementedException))]
        public async Task DataViewModelWithoutDataOperations_ThrowsOnReloadDataAsync()
        {
            await new DataViewModelWithoutDataOperations().ReloadDataAsync();
        }
    }
}
