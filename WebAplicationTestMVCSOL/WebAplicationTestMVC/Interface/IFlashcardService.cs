using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Interface
{
    public interface IFlashcardService
    {
        Task<List<Flashcard>> GetAllFlashcardsBySetName(string setName);
        Task Add(string question, string answer, string setName);
        List<FlashcardDTO> FlashcardsToDTOs(List<Flashcard> flashcards);
        Task DeleteAllFlashcardsBySetName(string studySetName);
    }
}
