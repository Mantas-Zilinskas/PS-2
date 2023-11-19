using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Interface
{
    public interface IFlashcardRepository
    {
        List<Flashcard> GetAllBySetName(string setName);
        Flashcard GetById(string Id);
        void Add(Flashcard flashcard);
        void Update(Flashcard flashcard);
        void Delete(string id);
    }
}
