using Microsoft.EntityFrameworkCore;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Repository;

namespace WebApplicationTestMVCTests
{
    [TestClass]
    public class StudySetRepositoryTests
    {
        private ApplicationDbContext _context;
        private StudySetRepository _repository;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new StudySetRepository(_context);
        }

        [TestMethod]
        public void Add_AddsStudySet()
        {
            var studySet = new StudySet("Test Set");

            _repository.Add(studySet);

            var addedStudySet = _context.StudySets.FirstOrDefault(s => s.StudySetName == "Test Set");
            Assert.IsNotNull(addedStudySet);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }
    }
}