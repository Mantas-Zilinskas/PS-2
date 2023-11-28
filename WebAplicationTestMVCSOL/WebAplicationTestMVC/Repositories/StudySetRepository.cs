using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;
using Microsoft.EntityFrameworkCore;


namespace WebAplicationTestMVC.Repository
{
    public class StudySetRepository : IStudySetRepository
    {
        private readonly ApplicationDbContext _context;

        public StudySetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(StudySet studySet)
        {
            _context.StudySets.Add(studySet);
            _context.SaveChanges();
        }
        public List<StudySet> GetAll()
        {
            return _context.StudySets.ToList();
        }
        public StudySet GetByName(string studySetName)
        {
            return _context.StudySets.SingleOrDefault(s => s.StudySetName == studySetName);
        }
        public StudySet GetById(int studySetId)
        {
            return _context.StudySets.Find(studySetId);
        }

        public void Update(StudySet studySet)
        {
            _context.Entry(studySet).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
