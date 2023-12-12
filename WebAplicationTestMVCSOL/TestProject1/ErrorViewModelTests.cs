using Xunit;
using WebAplicationTestMVC.Models;
using Assert = Xunit.Assert;

public class ErrorViewModelTests
{
    [Fact]
    public void ShowRequestId_RequestIdIsNull_ReturnsFalse()
    {
        // Arrange
        var errorViewModel = new ErrorViewModel { RequestId = null };

        // Act
        bool showRequestId = errorViewModel.ShowRequestId;

        // Assert
        Assert.False(showRequestId);
    }

    [Fact]
    public void ShowRequestId_RequestIdIsNotNull_ReturnsTrue()
    {
        // Arrange
        var errorViewModel = new ErrorViewModel { RequestId = "123456" };

        // Act
        bool showRequestId = errorViewModel.ShowRequestId;

        // Assert
        Assert.True(showRequestId);
    }
}