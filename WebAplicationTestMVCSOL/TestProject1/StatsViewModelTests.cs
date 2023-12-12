using Xunit;
using WebAplicationTestMVC.Models;
using Assert = Xunit.Assert;

public class StatsViewModelTests
{
    [Fact]
    public void DefaultValues_InitializedCorrectly()
    {
        // Arrange
        var viewModel = new StatsViewModel();

        // Assert
        Assert.Equal(0, viewModel.TotalAttempts);
        Assert.Equal(0.0, viewModel.AvgTime);
        Assert.Equal(0, viewModel.TotalTime);
        Assert.Equal(0.0, viewModel.AvgWrongAnswers);
        Assert.Equal(0, viewModel.TotalWrongAnswers);
        Assert.Equal(0.0, viewModel.AvgCorrectAnswers);
        Assert.Equal(0, viewModel.TotalCorrectAnswers);
        Assert.Equal(0.0, viewModel.CorrectWrongRatio);
    }

    [Fact]
    public void SetValues_ModifyProperties()
    {
        // Arrange
        var viewModel = new StatsViewModel();

        // Act
        viewModel.TotalAttempts = 5;
        viewModel.AvgTime = 10.5;
        viewModel.TotalTime = 52;
        viewModel.AvgWrongAnswers = 1.2;
        viewModel.TotalWrongAnswers = 6;
        viewModel.AvgCorrectAnswers = 3.8;
        viewModel.TotalCorrectAnswers = 19;
        viewModel.CorrectWrongRatio = 0.633;

        // Assert
        Assert.Equal(5, viewModel.TotalAttempts);
        Assert.Equal(10.5, viewModel.AvgTime);
        Assert.Equal(52, viewModel.TotalTime);
        Assert.Equal(1.2, viewModel.AvgWrongAnswers);
        Assert.Equal(6, viewModel.TotalWrongAnswers);
        Assert.Equal(3.8, viewModel.AvgCorrectAnswers);
        Assert.Equal(19, viewModel.TotalCorrectAnswers);
        Assert.Equal(0.633, viewModel.CorrectWrongRatio);
    }
}