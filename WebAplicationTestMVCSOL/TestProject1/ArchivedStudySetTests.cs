using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Utilities;

namespace WebApplicationTestMVCTests.Models
{
    [TestClass]
    public class ArchivedStudySetTests
    {
        [TestMethod]
        public void Constructor_CreatesArchivedStudySet_FromStudySet()
        {
            // Arrange
            var studySetName = "Calculus";
            var originalStudySet = new StudySet(studySetName)
            {
                Id = 1,
                DateCreated = new DateTime(2023, 1, 1),
                Color = StudySetColor.Blue, 
                StudyTime = new TimeSpan(2, 30, 0) 

            };

            // Act
            var archivedStudySet = new ArchivedStudySet(originalStudySet);

            // Assert
            Assert.AreEqual(studySetName, archivedStudySet.StudySetName);
            Assert.AreEqual(originalStudySet.DateCreated, archivedStudySet.DateCreated);
            Assert.IsTrue((DateTime.Now - archivedStudySet.DateArchived).TotalSeconds < 1); 
            Assert.AreEqual(originalStudySet.Id, archivedStudySet.OriginalStudySetId);
            Assert.AreEqual(originalStudySet.Color, archivedStudySet.Color);
            Assert.AreEqual(originalStudySet.StudyTime, archivedStudySet.StudyTime);
        }
    }
}