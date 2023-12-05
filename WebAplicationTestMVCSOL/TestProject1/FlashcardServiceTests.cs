using Moq;
using System.Collections;
using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Services;

namespace WebApplicationTestMVCTests
{
    [TestClass]
    public class FlashcardServiceTests
    {
        private Mock<IFlashcardRepository> mockFlashcardRepository;
        private Mock<IStudySetRepository> mockStudySetRepository;
        private FlashcardService service;

        [TestInitialize]
        public void SetUp()
        {
            mockFlashcardRepository = new Mock<IFlashcardRepository>();
            mockStudySetRepository = new Mock<IStudySetRepository>();
            service = new FlashcardService(mockFlashcardRepository.Object, mockStudySetRepository.Object);
        }

  /*      [TestMethod]
        public void GetAllFlashcardsBySetName_ReturnsFlashcards()
        {
            // Arrange
            var setName = "Math";
            var flashcards = new List<Flashcard>
            {
                new Flashcard("1", "What is 2+2?", "4", setName),
                new Flashcard("2", "What is 3+3?", "6", setName),
            };

            mockFlashcardRepository.Setup(repo => repo.GetAllBySetName(setName)).Returns(flashcards);

            // Act
            var result = service.GetAllFlashcardsBySetName(setName);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(flashcards, result, new FlashcardComparer());
        }*/

 /*       [TestMethod]
        public void Add_AddsFlashcard()
        {
            // Arrange
            var flashcard = new Flashcard("1", "What is 2+2?", "4", "Math");
            var flashcardToAdd = new Flashcard(flashcard.Id, flashcard.Question, flashcard.Answer, flashcard.SetName);

            mockFlashcardRepository.Setup(repo => repo.Add(It.IsAny<Flashcard>()));

            // Act
            service.Add(flashcard.Question, flashcard.Answer, flashcard.SetName);

            // Assert
            mockFlashcardRepository.Verify(repo => repo.Add(It.Is<Flashcard>(
                fc => fc.Id == flashcardToAdd.Id &&
                      fc.Question == flashcardToAdd.Question &&
                      fc.Answer == flashcardToAdd.Answer &&
                      fc.SetName == flashcardToAdd.SetName)), Times.Once);
        }*/

        [TestMethod]
        public void FlashcardsToDTOs_ConvertsToDTOs()
        {
            // Arrange
            var flashcards = new List<Flashcard>
            {
                new Flashcard("1", "What is 2+2?", "4", "Math"),
                new Flashcard("2", "What is 3+3?", "6", "Math"),
            };

            var expectedDTOs = new List<FlashcardDTO>
            {
                new FlashcardDTO("1", "What is 2+2?", "4", "Math"),
                new FlashcardDTO("2", "What is 3+3?", "6", "Math"),
            };

            // Act
            var result = service.FlashcardsToDTOs(flashcards);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedDTOs, result, new FlashcardDTOComparer());
        }
    }

    class FlashcardComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var lhs = x as Flashcard;
            var rhs = y as Flashcard;
            if (lhs == null || rhs == null)
            {
                return -1;
            }

            return lhs.Equals(rhs) ? 0 : -1;
        }
    }

    class FlashcardDTOComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var lhs = x as FlashcardDTO;
            var rhs = y as FlashcardDTO;
            if (lhs == null || rhs == null)
            {
                return -1;
            }

            return lhs.Id == rhs.Id && lhs.Question == rhs.Question &&
                   lhs.Answer == rhs.Answer && lhs.SetName == rhs.SetName
                ? 0
                : -1;
        }
    }
}