using Microsoft.AspNetCore.Http;
using Moq;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebApplicationTestMVCTests
{
    [TestClass]
    public class ColorManagerTests
    {
        private List<StudySet> studySets;
        private Mock<HttpContext> mockHttpContext;
        private Mock<HttpRequest> mockRequest;
        private Mock<HttpResponse> mockResponse;

        [TestInitialize]
        public void SetUp()
        {
            studySets = new List<StudySet>
            {
                new StudySet("Mathematics"),
                new StudySet("Science")
            };

            mockHttpContext = new Mock<HttpContext>();
            mockRequest = new Mock<HttpRequest>();
            mockResponse = new Mock<HttpResponse>();
            mockHttpContext.Setup(c => c.Request).Returns(mockRequest.Object);
            mockHttpContext.Setup(c => c.Response).Returns(mockResponse.Object);

            var cookieCollection = new Mock<IRequestCookieCollection>();
            mockRequest.Setup(c => c.Cookies).Returns(cookieCollection.Object);
        }

        [TestMethod]
        public void AssignUniqueColor_AssignsColorsAndSetsCookies()
        {
            mockResponse.Setup(r => r.Cookies.Append(It.IsAny<string>(), It.IsAny<string>()));

            ColorManager.AssignUniqueColor(studySets, mockHttpContext.Object);

            foreach (var studySet in studySets)
            {
                Assert.IsTrue(Enum.IsDefined(typeof(StudySetColor), studySet.Color));
            }
            mockResponse.Verify(r => r.Cookies.Append(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce());
        }
    }
}