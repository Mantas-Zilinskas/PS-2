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

        public async Task Add(Flashcard flashcard)
        {
            _context.Database.EnsureCreated();
            await _context.Flashcards.AddAsync(flashcard);
            _context.SaveChanges();
        }
        public async Task<List<Flashcard>> GetAllBySetName(string setName)
        {
            return await _context.Flashcards.Where(f => f.SetName == setName).ToListAsync();
        }
    }
}
