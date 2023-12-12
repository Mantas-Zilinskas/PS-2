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