using Moq;
using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Services;
using WebAplicationTestMVC.Utilities;

namespace WebApplicationTestMVCTests
{
    [TestClass]
    public class StudySetServiceTests
    {
        private Mock<IStudySetRepository> mockRepository;
        private StudySetService service;

        [TestInitialize]
        public void SetUp()
        {
            mockRepository = new Mock<IStudySetRepository>();
            service = new StudySetService(mockRepository.Object);
        }

        [TestMethod]
        public void GetStudySetByName_ReturnsStudySet_WhenExists()
        {
            // Arrange
            var studySetName = "Mathematics";
            var expectedStudySet = new StudySet(studySetName) { Id = 1, DateCreated = DateTime.Now };
            mockRepository.Setup(repo => repo.GetByName(studySetName)).Returns(expectedStudySet);

            // Act
            var result = service.GetStudySetByName(studySetName);

            // Assert
            Assert.AreEqual(expectedStudySet, result);
        }

        [TestMethod]
        public void GetByDateFilter_ReturnsFilteredStudySets()
        {
            // Arrange
            var studySets = new List<StudySet>
            {
                new StudySet("Mathematics") { DateCreated = DateTime.Now.AddDays(-10), Id = 1 },
                new StudySet("Science") { DateCreated = DateTime.Now.AddDays(-5), Id = 2 },
                new StudySet("History") { DateCreated = DateTime.Now.AddDays(-1), Id = 3 }
            };
            var filter = new StudySetDateFilter((studySet) => studySet.DateCreated > DateTime.Now.AddDays(-7));
            mockRepository.Setup(repo => repo.GetAll()).Returns(studySets);

            // Act
            var result = service.GetByDateFilter(filter);

            // Assert
            var expected = studySets.Where(s => s.DateCreated > DateTime.Now.AddDays(-7)).ToList();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAllOrderedBy_ReturnsStudySetsInOrder()
        {
            // Arrange
            var studySets = new List<StudySet>
            {
                new StudySet("Science") { DateCreated = DateTime.Now.AddDays(-5), Id = 2 },
                new StudySet("Mathematics") { DateCreated = DateTime.Now.AddDays(-10), Id = 1 },
                new StudySet("History") { DateCreated = DateTime.Now.AddDays(-1), Id = 3 }
            };
            var orderFilter = new StudySetOrderFilter((sets) => sets.OrderBy(s => s.DateCreated));
            mockRepository.Setup(repo => repo.GetAll()).Returns(studySets);

            // Act
            var result = service.GetAllOrderedBy(orderFilter);

            // Assert
            var expected = studySets.OrderBy(s => s.DateCreated).ToList();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetStudySetById_ReturnsStudySet_WhenExists()
        {
            // Arrange
            var studySetId = 1;
            var expectedStudySet = new StudySet("Mathematics") { DateCreated = DateTime.Now.AddDays(-10), Id = studySetId };
            mockRepository.Setup(repo => repo.GetById(studySetId)).Returns(expectedStudySet);

            // Act
            var result = service.GetStudySetById(studySetId);

            // Assert
            Assert.AreEqual(expectedStudySet, result);
        }

        [TestMethod]
        public void UpdateStudySet_CallsUpdateOnRepository()
        {
            // Arrange
            var studySet = new StudySet("Mathematics") { DateCreated = DateTime.Now.AddDays(-10), Id = 1 };

            // Act
            service.UpdateStudySet(studySet);

            // Assert
            mockRepository.Verify(repo => repo.Update(studySet), Times.Once);
        }
    }
}
