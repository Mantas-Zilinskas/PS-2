using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using System.Text;
using WebAplicationTestMVC.Services;
using WebAplicationTestMVC.Utilities;

namespace WebApplicationTestMVCTests.Utilities
{
    [TestClass]
    public class DarkModeManagerTests
    {
        private Mock<HttpContext> mockHttpContext;
        private Mock<ISession> mockSession;
        private const string DarkModeSettingsKey = "DarkModeSettings";

        [TestInitialize]
        public void SetUp()
        {
            mockSession = new Mock<ISession>();
            mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(ctx => ctx.Session).Returns(mockSession.Object);
        }

        [TestMethod]
        public void GetDarkModeSettings_ReturnsDefaultSettings_WhenSessionIsEmpty()
        {
            // Arrange
            byte[] dummySessionValue;
            mockSession.Setup(s => s.TryGetValue(It.IsAny<string>(), out dummySessionValue)).Returns(false);

            // Act
            var result = DarkModeManager.GetDarkModeSettings(mockHttpContext.Object);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsDarkModeEnabled);
        }

        [TestMethod]
        public void GetDarkModeSettings_ReturnsStoredSettings_WhenSessionContainsSettings()
        {
            // Arrange
            var settings = new DarkModeSettings { IsDarkModeEnabled = true };
            var serializedSettings = JsonConvert.SerializeObject(settings);
            var encodedSettings = Encoding.UTF8.GetBytes(serializedSettings);

            mockSession.Setup(s => s.TryGetValue(DarkModeSettingsKey, out encodedSettings)).Returns(true);

            // Act
            var result = DarkModeManager.GetDarkModeSettings(mockHttpContext.Object);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsDarkModeEnabled);
        }

        [TestMethod]
        public void ToggleDarkMode_ChangesDarkModeSetting()
        {
            // Arrange
            var initialSettings = new DarkModeSettings { IsDarkModeEnabled = false };
            var serializedInitialSettings = JsonConvert.SerializeObject(initialSettings);
            var encodedInitialSettings = Encoding.UTF8.GetBytes(serializedInitialSettings);

            mockSession.Setup(s => s.TryGetValue(DarkModeSettingsKey, out encodedInitialSettings)).Returns(true);

            byte[] capturedValue = null;
            mockSession.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Callback<string, byte[]>((key, value) => capturedValue = value);

            // Act
            DarkModeManager.ToggleDarkMode(mockHttpContext.Object);

            // Assert
            var updatedSettings = JsonConvert.DeserializeObject<DarkModeSettings>(Encoding.UTF8.GetString(capturedValue));
            Assert.IsNotNull(updatedSettings);
            Assert.IsTrue(updatedSettings.IsDarkModeEnabled);
        }
    }
}
