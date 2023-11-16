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

        public void Add(Flashcard flashcard)
        {
            _context.Database.EnsureCreated();
            _context.Flashcards.Add(flashcard);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Flashcard GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Flashcard> GetAllBySetName(string setName)
        {
            return _context.Flashcards.Where(f => f.SetName == setName).ToList();
        }

        public void Update(Flashcard flashcard)
        {
            throw new NotImplementedException();
        }
    }
}
