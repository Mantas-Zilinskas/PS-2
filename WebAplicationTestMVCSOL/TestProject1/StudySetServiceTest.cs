using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Services;
using Xunit;
using Assert = Xunit.Assert;

public class StudySetServiceTests
{
    [Fact]
    public async Task GetAllStudySets_ReturnsListOfStudySets()
    {
        // Arrange
        var studySets = new List<StudySet>
        {
            new StudySet("Set 1"),
            new StudySet("Set 2"),
            new StudySet("Set 3"),
        };

        var mockRepository = new MockStudySetRepository(studySets);
        var service = new StudySetService(null, mockRepository);

        // Act
        var result = await service.GetAllStudySets();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(studySets.Count, result.Count);
    }

    [Fact]
    public async Task AddNewStudySet_WithUniqueName_AddsStudySet()
    {
        // Arrange
        var studySetName = "New Set";
        var mockRepository = new MockStudySetRepository(new List<StudySet>());
        var service = new StudySetService(null, mockRepository);

        // Act
        var result = await service.AddNewStudySet(studySetName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(studySetName, result.StudySetName);
    }

    [Fact]
    public async Task AddNewStudySet_WithDuplicateName_AppendsCounter()
    {
        // Arrange
        var studySetName = "Duplicate Set";
        var existingSets = new List<StudySet>
        {
            new StudySet("Duplicate Set"),
            new StudySet("Duplicate Set (1)"),
        };

        var mockRepository = new MockStudySetRepository(existingSets);
        var service = new StudySetService(null, mockRepository);

        // Act
        var result = await service.AddNewStudySet(studySetName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Duplicate Set (2)", result.StudySetName);
    }

    [Fact]
    public async Task GetStudySetByName_WithValidName_ReturnsStudySet()
    {
        // Arrange
        var studySetName = "Existing Set";
        var existingSets = new List<StudySet>
        {
            new StudySet("Another Set"),
            new StudySet("Existing Set"),
        };

        var mockRepository = new MockStudySetRepository(existingSets);
        var service = new StudySetService(null, mockRepository);

        // Act
        var result = await service.GetStudySetByName(studySetName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(studySetName, result.StudySetName);
    }
}
