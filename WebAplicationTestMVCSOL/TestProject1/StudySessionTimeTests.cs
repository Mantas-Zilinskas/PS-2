using WebAplicationTestMVC.Services;
using Xunit;
using Assert = Xunit.Assert;

public class StudySessionTimeTests
{
    [Fact]
    public void Duration_ReturnsCorrectTimeSpan()
    {
        // Arrange
        var startTime = new DateTime(2023, 1, 1, 10, 0, 0);
        var endTime = new DateTime(2023, 1, 1, 11, 30, 0);
        var studySessionTime = new StudySessionTime(startTime, endTime);

        // Act
        TimeSpan duration = studySessionTime.Duration;

        // Assert
        Assert.Equal(TimeSpan.FromMinutes(90), duration);
    }
}