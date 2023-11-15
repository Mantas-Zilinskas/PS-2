using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Interface
{
    public interface IFlashcardRepository
    {
        Task<List<Flashcard>> GetAllBySetName(string setName);
        Task<Flashcard> GetById(string Id);
        void Add(string question, string answer, string setName);
        void Update(Flashcard flashcard);
        Task Delete(string id);
    }
}
