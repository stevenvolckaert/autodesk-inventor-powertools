using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StevenVolckaert.Web.Client.Tests
{
    [TestClass]
    public class RestClientFixture
    {
        private class Post
        {
            public string Id { get; set; }
            public string UserId { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
        }

        private static readonly Uri _baseAddress = new Uri("http://jsonplaceholder.typicode.com");
        private static readonly Uri _incorrectBaseAddress = new Uri("http://jsonholder.typicade.com");

        //private static Mock<Post> _mock;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            //var mock = new Mock<IBookService>();
            //mock.Setup(x => x.GetAll()).Returns(new Book[] { new Book(), new Book() }.AsQueryable<Book>());
            // ... .Results(Task.FromResult(new Book())); for async methods.
            //mock.Setup(x => x.Get(id: It.IsAny<int>())).Returns(new Task<Book>(x => new Book()));
            //var mock = new Mock<Post>();

            //mock.Setup(x => x).Returns(new Post { Title = "foo", Body = "bar", UserId = "1" });
            //_mock = mock;

            // Also check out Shim (only for VS Premium & Ultimate).
            // use var result = Task.Result; to block a non-async test method.
        }

        [TestMethod]
        public async Task GetResource_RelativeRequestUriAndMissingBaseAddress_ThrowsImmediately()
        {
            // Arrange
            var client = new RestClient();
            var task = client.GetAsync<Post>(new Uri("post/1", UriKind.Relative));

            // Assert
            await task.AssertThrows<InvalidOperationException>();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public async Task GetResource_NullRequestUri_ThrowsImmediately()
        {
            var client = new RestClient();
            await client.GetAsync<Post>(uri: null);
        }

        [TestMethod]
        public void GetResourceList_Succeeds()
        {
            // Arrange
            var client = new RestClient(_baseAddress);

            // Act
            var response = client.GetAsync<IEnumerable<Post>>(new Uri("posts", UriKind.Relative)).Result;

            // Assert
            Assert.IsTrue(response.Succeeded);
            Assert.IsTrue(response.Data.Any());
        }

        [TestMethod]
        public async Task GetResourceList_Fails()
        {
            // Arrange
            var client = new RestClient(_incorrectBaseAddress);

            // Act
            var response = await client.GetAsync<IEnumerable<Post>>(new Uri("posts", UriKind.Relative));

            // Assert
            Assert.IsFalse(response.Succeeded);
            Assert.IsTrue(response.Data == null);
        }

        [TestMethod]
        public async Task GetResource_Succeeds()
        {
            // Arrange
            var client = new RestClient(_baseAddress);

            // Act
            var result = await client.GetAsync<Post>(new Uri("posts/1", UriKind.Relative));

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateResource_Succeeds()
        {
            // Arrange
            var client = new RestClient(_baseAddress);
            //var data = _mock.Object;
            var data = new Post { Title = "foo", Body = "bar", UserId = "1" };

            // Act
            var response = await client.PostAsync(new Uri("posts", UriKind.Relative), data);

            // Assert
            Assert.IsTrue(response.Succeeded);
            Assert.AreEqual(response.Data.Title, data.Title);
            Assert.AreEqual(response.Data.Body, data.Body);
            Assert.AreEqual(response.Data.UserId, data.UserId);

            //mockService.Verify(x => x.GetAll(), Times.Once());
        }

        //[TestMethod]
        //public async Task UpdateResource_Succeeds()
        //{
        //}

        //[TestMethod]
        //public async Task DeleteResource_Succeeds()
        //{
        //}
    }
}
