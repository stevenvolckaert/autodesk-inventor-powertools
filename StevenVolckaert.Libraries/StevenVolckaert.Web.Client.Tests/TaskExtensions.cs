using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StevenVolckaert.Web.Client.Tests
{
    internal static class TaskExtensions
    {
        public static async Task AssertThrows<TException>(this Task task)
            where TException : Exception
        {
            try
            {
                await task;
                Assert.Fail("Expected exception of type '" + typeof(TException) + "'.");
            }
            catch (TException)
            {
                // Expected
            }
        }
    }
}
