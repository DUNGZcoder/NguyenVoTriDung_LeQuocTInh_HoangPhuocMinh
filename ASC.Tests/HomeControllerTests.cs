using ASC.Web.Configuration;
using ASC.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ASC.Tests
{
    public class HomeControllerTests
    {
        private Mock<IOptions<ApplicationSettings>> mockOptions;
        private Mock<ILogger<HomeController>> mockLogger;
        private Mock<IHttpContextAccessor> mockHttpContextAccessor;
        private HomeController controller;
        private FakeSession fakeSession;

        public HomeControllerTests()
        {
            // Setup mock IOptions<ApplicationSettings>
            mockOptions = new Mock<IOptions<ApplicationSettings>>();
            mockOptions.Setup(o => o.Value).Returns(new ApplicationSettings
            {
                ApplicationTitle = "ASC Web Application",
                AdminEmail = "admin@ascweb.com",
                AdminName = "Administrator"
            });

            // Setup mock ILogger
            mockLogger = new Mock<ILogger<HomeController>>();

            // Setup FakeSession
            fakeSession = new FakeSession();

            // Setup mock HttpContextAccessor
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(c => c.Session).Returns(fakeSession);
            mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(mockHttpContext.Object);

            // Create HomeController with mocked dependencies
            controller = new HomeController(mockLogger.Object, mockOptions.Object, mockHttpContextAccessor.Object);
        }

        [Fact]
        public void HomeController_Index_ReturnViewResult_Test()
        {
            // Act
            var result = controller.Index();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void HomeController_Index_ReturnViewResultWithoutNullModel_Test()
        {
            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(controller.ViewBag.Title);
        }

        [Fact]
        public void HomeController_Index_NoModelStateErrors_Test()
        {
            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(controller.ModelState.IsValid);
        }

        [Fact]
        public void HomeController_Index_SetSessionData_Test()
        {
            // Act
            var result = controller.Index() as ViewResult;
            var sessionValue = Microsoft.AspNetCore.Http.SessionExtensions.GetString(fakeSession, "ApplicationTitle");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(controller.ViewBag.Title);
            Assert.Equal("ASC Web Application", sessionValue);
        }
    }
}
