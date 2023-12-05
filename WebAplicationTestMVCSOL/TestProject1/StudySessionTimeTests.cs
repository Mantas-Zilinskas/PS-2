using WebAplicationTestMVC.Services;

namespace WebApplicationTestMVCTests
{
    [TestClass]
    public class StudySessionTimeTests
    {
        [TestMethod]
        public void Duration_ShouldCalculateCorrectDuration()
        {
            // Arrange
            DateTime startTime = new DateTime(2023, 1, 1, 10, 0, 0);
            DateTime endTime = new DateTime(2023, 1, 1, 11, 30, 0);
            var studySessionTime = new StudySessionTime(startTime, endTime);

            // Act
            TimeSpan duration = studySessionTime.Duration;

            // Assert
            TimeSpan expectedDuration = TimeSpan.FromMinutes(90);
            Assert.AreEqual(expectedDuration, duration);
        }

        [TestMethod]
        public void Duration_ShouldHandleNegativeDuration()
        {
            // Arrange
            DateTime startTime = new DateTime(2023, 1, 1, 12, 0, 0);
            DateTime endTime = new DateTime(2023, 1, 1, 11, 30, 0); // End time is earlier than start time
            var studySessionTime = new StudySessionTime(startTime, endTime);

            // Act
            TimeSpan duration = studySessionTime.Duration;

            // Assert
            TimeSpan expectedDuration = TimeSpan.FromMinutes(-30); // Negative duration
            Assert.AreEqual(expectedDuration, duration);
        }
    }
}