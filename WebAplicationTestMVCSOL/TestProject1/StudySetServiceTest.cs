using Moq;
using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Services;

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
            service = new StudySetService(null, mockRepository.Object);
        }

        [TestMethod]
        public void GetAllStudySets_ReturnsAllStudySets()
        {

            var expectedStudySets = new List<StudySet>
            {
                new StudySet("Mathematics") { DateCreated = DateTime.Now.AddDays(-10), Id = 1 },
                new StudySet("Science") { DateCreated = DateTime.Now.AddDays(-5), Id = 2 },
                new StudySet("History") { DateCreated = DateTime.Now, Id = 3 }
            };

            mockRepository.Setup(repo => repo.GetAll()).Returns(expectedStudySets);

            var result = service.GetAllStudySets();

            Assert.AreEqual(expectedStudySets, result);
        }

        [TestMethod]
        public void AddNewStudySet_AddsStudySetWhenNotExisting()
        {
            var studySetName = "New Study Set";
            mockRepository.Setup(repo => repo.GetByName(studySetName)).Returns((StudySet)null);

            service.AddNewStudySet(studySetName);

            mockRepository.Verify(repo => repo.Add(It.Is<StudySet>(s => s.StudySetName == studySetName)), Times.Once);
        }
    }
}