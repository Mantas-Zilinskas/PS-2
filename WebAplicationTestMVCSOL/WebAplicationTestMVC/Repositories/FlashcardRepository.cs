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
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllBySetName(string setName)
        {
            await _context.Flashcards.Where(a => a.SetName == setName).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFlashcardById(string Id)
        {
            var flashcard = await _context.Flashcards.FindAsync(Id);
            _context.Flashcards.Remove(flashcard);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Flashcard>> GetAllBySetName(string setName)
        {
            return await _context.Flashcards.Where(f => f.SetName == setName).ToListAsync();
        }

        public async Task<Flashcard> GetFlashcardById(string id)
        {
            return await _context.Flashcards.FindAsync(id);
        }

        public async Task EditFlashcard(string id, string question, string answer) { 
            var flashcard = await _context.Flashcards.FindAsync(id);
            flashcard.Question = question;
            flashcard.Answer = answer;

            await _context.SaveChangesAsync();
        }
    }
}
