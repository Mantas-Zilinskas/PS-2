using Microsoft.EntityFrameworkCore;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Repository;

[TestClass]
public class FlashcardRepositoryTests
{
    private ApplicationDbContext _context;
    private FlashcardRepository _repository;

    [TestInitialize]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "FlashcardDb")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new FlashcardRepository(_context);
    }

    [TestMethod]
    public void Add_ShouldAddFlashcard()
    {
        var flashcard = new Flashcard("1", "What is 2+2?", "4", "Math");
        flashcard.StudySetId = 1;

        _repository.Add(flashcard);

        var addedFlashcard = _context.Flashcards.FirstOrDefault(f => f.Id == flashcard.Id);
        Assert.IsNotNull(addedFlashcard);
        Assert.AreEqual("What is 2+2?", addedFlashcard.Question);
    }

    [TestMethod]
    public async Task GetAllBySetName_ShouldReturnAllFlashcardsOfSet()
    {
        // Arrange
        var flashcards = new List<Flashcard>
        {
            new Flashcard("1", "Q1", "A1", "Math") { StudySetId = 1 },
            new Flashcard("2", "Q2", "A2", "Math") { StudySetId = 1 },
            new Flashcard("3", "Q3", "A3", "Science") { StudySetId = 2 }
        };

        foreach (var flashcard in flashcards)
        {
            _context.Flashcards.Add(flashcard);
        }
        _context.SaveChanges();

        // Act
        var retrievedFlashcards = await _repository.GetAllBySetName("Math");

        // Assert
        Assert.AreEqual(2, retrievedFlashcards.Count);
        Assert.IsTrue(retrievedFlashcards.Any(f => f.SetName == "Math"));
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
