using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shurly.Core.WebApi.Models;

namespace Shurly.Tests.ShurlyController
{
    /// <summary>
    /// Summary description for ShurlyControllerAccountActionShould
    /// </summary>
    [TestClass]
    public class AccountActionShould
    {
        public AccountActionShould()
        {
            _shurlyController = new Web.Controllers.ShurlyController();
        }

        private TestContext _testContextInstance;
        private Web.Controllers.ShurlyController _shurlyController;

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
        public void ReturnBadRequestIfAccountIdIsNullOrEmpty()
        {
            var mockRequestBody = new Mock<IAccountRequestBody>();
            mockRequestBody.Setup(x => x.AccountId).Returns((string)null);

            var httpActionResult = _shurlyController.Account(mockRequestBody.Object);

            Assert.IsInstanceOfType(httpActionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void ReturnOkIfRegistrationIsSuccessful()
        {
            var mockRequestBody = new Mock<IAccountRequestBody>();
            mockRequestBody.Setup(x => x.AccountId).Returns("TestAccountId1");

            var httpActionResult = _shurlyController.Account(mockRequestBody.Object);
            
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<AccountResponseBody>));
        }

        [TestMethod]
        public void ReturnOkIfRegistrationFails()
        {
            var mockRequestBody = new Mock<IAccountRequestBody>();
            mockRequestBody.Setup(x => x.AccountId).Returns("TestAccountId2");

            // first call so we are sure there is duplicate and action will fail
            _shurlyController.Account(mockRequestBody.Object);
            var httpActionResult = _shurlyController.Account(mockRequestBody.Object);

            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<AccountResponseBody>));
        }

        [TestMethod]
        public void ReturnSuccessFalseIfRegistrationFails()
        {
            var mockRequestBody = new Mock<IAccountRequestBody>();
            mockRequestBody.Setup(x => x.AccountId).Returns("TestAccountId3");

            // first call so we are sure there is duplicate and action will fail
            _shurlyController.Account(mockRequestBody.Object);
            var httpActionResult = _shurlyController.Account(mockRequestBody.Object);

            OkNegotiatedContentResult<AccountResponseBody> negResult = httpActionResult as OkNegotiatedContentResult<AccountResponseBody>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(false, negResult.Content.Success);
        }
        
        [TestMethod]
        public void NotReturnPasswordIfRegistrationFails()
        {
            var mockRequestBody = new Mock<IAccountRequestBody>();
            mockRequestBody.Setup(x => x.AccountId).Returns("TestAccountId4");

            // first call so we are sure there is duplicate and action will fail
            _shurlyController.Account(mockRequestBody.Object);
            var httpActionResult = _shurlyController.Account(mockRequestBody.Object);

            OkNegotiatedContentResult<AccountResponseBody> negResult = httpActionResult as OkNegotiatedContentResult<AccountResponseBody>;
            Assert.IsNotNull(negResult);
            Assert.IsTrue(string.IsNullOrEmpty(negResult.Content.Password));
        }

        [TestMethod]
        public void ReturnPasswordIfRegistrationWasSuccessful()
        {
            var mockRequestBody = new Mock<IAccountRequestBody>();
            mockRequestBody.Setup(x => x.AccountId).Returns("TestAccountId5");

            var httpActionResult = _shurlyController.Account(mockRequestBody.Object);

            OkNegotiatedContentResult<AccountResponseBody> negResult = httpActionResult as OkNegotiatedContentResult<AccountResponseBody>;
            Assert.IsNotNull(negResult);
            Assert.IsFalse(string.IsNullOrEmpty(negResult.Content.Password));
        }

        [TestMethod]
        public void ReturnPasswordWith8LengthIfRegistrationWasSuccessful()
        {
            var mockRequestBody = new Mock<IAccountRequestBody>();
            mockRequestBody.Setup(x => x.AccountId).Returns("TestAccountId6");

            var httpActionResult = _shurlyController.Account(mockRequestBody.Object);

            OkNegotiatedContentResult<AccountResponseBody> negResult = httpActionResult as OkNegotiatedContentResult<AccountResponseBody>;
            Assert.IsNotNull(negResult);
            Assert.IsFalse(string.IsNullOrEmpty(negResult.Content.Password));
            Assert.AreEqual(8, negResult.Content.Password.Length);
        }

    }
}
