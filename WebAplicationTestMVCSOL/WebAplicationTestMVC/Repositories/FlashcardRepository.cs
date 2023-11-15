using Microsoft.EntityFrameworkCore;
using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Repository
{
    public class FlashcardRepository : IFlashcardRepository
    {
        private readonly ApplicationDbContext _context;

        public FlashcardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(string question, string answer, string setName)
        {
            _context.Database.EnsureCreated();

            var studySet = _context.StudySets.SingleOrDefault(s => s.StudySetName == setName);

            if (studySet != null)
            {

                var flashcardId = Guid.NewGuid().ToString();
                var flashcard = new Flashcard(flashcardId, question, answer, setName)
                {
                    StudySetId = studySet.Id
                };

                _context.Flashcards.Add(flashcard);
                _context.SaveChanges();
            }
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Flashcard> GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Flashcard>> GetAllBySetName(string setName)
        {
            return await _context.Flashcards.Where(f => f.SetName == setName).ToListAsync();
        }

        public void Update(Flashcard flashcard)
        {
            throw new NotImplementedException();
        }
    }
}
