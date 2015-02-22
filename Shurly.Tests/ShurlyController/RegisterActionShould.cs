using System.Security.Principal;
using System.Threading;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shurly.Core.WebApi.Models;

namespace Shurly.Tests.ShurlyController
{
    /// <summary>
    /// Summary description for ShurlyControllerRegisterActionShould
    /// </summary>
    [TestClass]
    public class RegisterActionShould
    {
        public RegisterActionShould()
        {
            var mockUser = new Mock<IPrincipal>();
            var mockIdentity = new Mock<IIdentity>();
            mockUser.Setup(x => x.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns("Shurley");
            Thread.CurrentPrincipal = mockUser.Object;

            _shurlyController = new SelfHost.Controllers.ShurlyController();
        }

        private TestContext _testContextInstance;
        private SelfHost.Controllers.ShurlyController _shurlyController;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
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
        public void ReturnBadRequestIfUrlIsNullOrEmpty()
        {
            var mockRequestBody = new Mock<RegisterRequestBody>();
            mockRequestBody.Setup(x => x.Url).Returns((string)null);

            var httpActionResult = _shurlyController.Register(mockRequestBody.Object);

            Assert.IsInstanceOfType(httpActionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void ReturnOkIfUrlRegistrationIsSuccessful()
        {
            var mockRequestBody = new Mock<RegisterRequestBody>();
            mockRequestBody.Setup(x => x.Url).Returns("http://facebook.com");

            var httpActionResult = _shurlyController.Register(mockRequestBody.Object);

            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<RegisterResponseBody>));
        }

        [TestMethod]
        public void ReturnShortUrlIfRegistrationWasSuccessful()
        {
            var mockRequestBody = new Mock<RegisterRequestBody>();
            mockRequestBody.Setup(x => x.Url).Returns("http://facebook.com");

            var httpActionResult = _shurlyController.Register(mockRequestBody.Object);

            OkNegotiatedContentResult<RegisterResponseBody> negResult = httpActionResult as OkNegotiatedContentResult<RegisterResponseBody>;
            Assert.IsNotNull(negResult);
            Assert.IsFalse(string.IsNullOrEmpty(negResult.Content.ShortUrl));
        }

    }
}
