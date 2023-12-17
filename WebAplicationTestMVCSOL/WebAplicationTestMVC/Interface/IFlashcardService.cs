using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Interface
{
    public interface IFlashcardService
    {
        Task<List<Flashcard>> GetAllFlashcardsBySetName(string setName);
        Task Add(string question, string answer, string setName);
        List<FlashcardDTO> FlashcardsToDTOs(List<Flashcard> flashcards);
        FlashcardDTO FlashcardsToDTOs(Flashcard flashcards);
        Task DeleteAllFlashcardsBySetName(string studySetName);
        Task DeleteFlashcardById(string id);
        Task<Flashcard> GetFlashcardById(string id);
        Task EditFlashcard(string id, string question, string answer);
    }
}
