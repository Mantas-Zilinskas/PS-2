using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Interface
{
    public interface IFlashcardRepository
    {
        Task<List<Flashcard>> GetAll();
        Task<List<Flashcard>> GetAllBySetName(string setName);
        Task Add(Flashcard flashcard);
    }
}
