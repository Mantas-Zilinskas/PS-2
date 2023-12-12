using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAplicationTestMVC.Controllers;
using WebAplicationTestMVC.Interface;

namespace WebApplicationTestMVCTests
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<IStudySetService> _mockStudySetService;
        private HomeController _controller;
        private Mock<HttpContext> _mockHttpContext;

        [TestInitialize]
        public void SetUp()
        {
            _mockStudySetService = new Mock<IStudySetService>();
            _mockHttpContext = new Mock<HttpContext>();
            var mockRequest = new Mock<HttpRequest>();
            var mockResponse = new Mock<HttpResponse>();
            var mockRequestCookies = new Mock<IRequestCookieCollection>();
            var mockResponseCookies = new Mock<IResponseCookies>();

            _mockHttpContext.Setup(c => c.Request).Returns(mockRequest.Object);
            _mockHttpContext.Setup(c => c.Response).Returns(mockResponse.Object);
            mockRequest.Setup(r => r.Cookies).Returns(mockRequestCookies.Object);
            mockResponse.Setup(r => r.Cookies).Returns(mockResponseCookies.Object);

            var controllerContext = new ControllerContext()
            {
                HttpContext = _mockHttpContext.Object
            };

            _controller = new HomeController(_mockStudySetService.Object)
            {
                ControllerContext = controllerContext
            };
        }
    }
}
