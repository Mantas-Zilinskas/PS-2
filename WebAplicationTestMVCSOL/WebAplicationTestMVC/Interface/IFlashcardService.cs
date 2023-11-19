using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Interface
{
    public interface IFlashcardService
    {
        List<Flashcard> GetAllFlashcardsBySetName(string setName);
        void Add(string question, string answer, string setName);
        List<FlashcardDTO> FlashcardsToDTOs(List<Flashcard> flashcards);
    }
}
