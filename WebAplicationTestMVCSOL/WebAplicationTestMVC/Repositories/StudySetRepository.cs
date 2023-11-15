using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;
using Microsoft.EntityFrameworkCore;
using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Repository
{
    public class StudySetRepository : IStudySetRepository
    {
        private readonly ApplicationDbContext _context;

        public StudySetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(string studySetName)
        {
            var studySet = new StudySet(studySetName)
            {
                DateCreated = DateTime.Now // Set the DateCreated property to the current date and time
            };

            _context.StudySets.Add(studySet);
            _context.SaveChanges();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StudySet>> GetAll()
        {
            return await _context.StudySets.ToListAsync();
        }

        public Task<StudySet> GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<StudySet> GetByName(string studySetName)
        {
            return await _context.StudySets.SingleOrDefaultAsync(s => s.StudySetName == studySetName);
        }

        public async Task<List<StudySet>> GetByDateFilter(StudySetDateFilter filter) {
            var allStudySets = await GetAll();
            return allStudySets.Where(filter.Invoke).ToList();
        }

        public async Task<List<StudySet>> GetAllOrderedBy(StudySetOrderFilter orderFilter) {
            var allStudySets = await GetAll();
            return orderFilter(allStudySets).ToList();
        }
        public void Update(StudySet studySet)
        {
            throw new NotImplementedException();
        }
    }
}
