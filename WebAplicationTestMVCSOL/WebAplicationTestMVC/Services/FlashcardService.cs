using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Interface;

namespace WebAplicationTestMVC.Services
{
    public class FlashcardService : IFlashcardService
    {
        private readonly IFlashcardRepository _FlashCardRepository;
        private readonly IStudySetRepository _StudySetRepository;

        public FlashcardService(IFlashcardRepository flashcardRepository, IStudySetRepository studySetRepository)
        {
            _FlashCardRepository = flashcardRepository;
            _StudySetRepository = studySetRepository;
        }

        public async Task<List<Flashcard>> GetAllFlashcardsBySetName(string setName)
        {
                List<Flashcard> flashcards = await _FlashCardRepository.GetAllBySetName(setName);
                return flashcards;
        }

        public async Task Add(string question, string answer, string setName) {

            var studySet = await _StudySetRepository.GetByName(setName);

            if (studySet != null)
            {
                Flashcard flashcard = new Flashcard(Guid.NewGuid().ToString(), question, answer, setName);
                flashcard.StudySetId = studySet.Id;

                await _FlashCardRepository.Add(flashcard);
            }
            else
            {
                throw new Exception("study set not found");
            }
        }

        public List<FlashcardDTO> FlashcardsToDTOs(List<Flashcard> flashcards){
            List<FlashcardDTO> flashcardDTOs = flashcards.Select(f => new FlashcardDTO(f.Id, f.Question, f.Answer, f.SetName)).ToList();
            return flashcardDTOs;
        }
        // Add this method to your FlashcardService to convert flashcards to DTOs
        public async Task<List<FlashcardDTO>> GetAllFlashcardsAsDTOs()
        {
            var flashcards = await _FlashCardRepository.GetAll(); // Modify as necessary to get all flashcards
            return flashcards.Select(fc => new FlashcardDTO(fc.Id, fc.Question, fc.Answer, fc.SetName)).ToList();
        }


    }
}
