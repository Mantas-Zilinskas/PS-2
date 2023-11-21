using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Interface
{
    public interface IFlashcardRepository
    {
        List<Flashcard> GetAllBySetName(string setName);
        void Add(Flashcard flashcard);
    }
}
