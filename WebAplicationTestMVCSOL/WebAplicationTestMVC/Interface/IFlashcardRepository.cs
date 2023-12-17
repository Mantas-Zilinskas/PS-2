using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Interface
{
    public interface IFlashcardRepository
    {
        Task<List<Flashcard>> GetAllBySetName(string setName);
        Task Add(Flashcard flashcard);
        Task DeleteAllBySetName(string setName);
        Task DeleteFlashcardById(string id);
        Task<Flashcard> GetFlashcardById(string id);
        Task EditFlashcard(string id, string question, string answer);
    }
}
