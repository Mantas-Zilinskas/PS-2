using WebAplicationTestMVC.Models;

namespace WebApplicationTestMVCTests.Models
{
    [TestClass]
    public class FavoriteStudySetTests
    {
        [TestMethod]
        public void FavoriteStudySet_StoresAndRetrievesValues()
        {
            // Arrange
            const int id = 1;
            const string userIdentifier = "User123";
            const int studySetId = 101;

            // Act
            var favoriteStudySet = new FavoriteStudySet
            {
                Id = id,
                UserIdentifier = userIdentifier,
                StudySetId = studySetId
            };

            // Assert
            Assert.AreEqual(id, favoriteStudySet.Id);
            Assert.AreEqual(userIdentifier, favoriteStudySet.UserIdentifier);
            Assert.AreEqual(studySetId, favoriteStudySet.StudySetId);
        }
    }
}