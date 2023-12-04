using Microsoft.EntityFrameworkCore;
using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Repository
{
    public class StudySetRepository : IStudySetRepository
    {
        private readonly ApplicationDbContext _context;

        public StudySetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(StudySet studySet)
        {
           await _context.StudySets.AddAsync(studySet);
            _context.SaveChanges();
        }
        public async Task<List<StudySet>> GetAll()
        {
            return await _context.StudySets.ToListAsync();
        }
        public async Task<StudySet> GetByName(string studySetName)
        {
            return await _context.StudySets.SingleOrDefaultAsync(s => s.StudySetName == studySetName);
        }
    }
}
