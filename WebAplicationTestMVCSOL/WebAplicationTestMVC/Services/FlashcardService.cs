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

        public List<Flashcard> GetAllFlashcardsBySetName(string setName)
        {
            StudySet set = _StudySetRepository.GetByName(setName);

            if (set != null)
            {
                List<Flashcard> flashcards = _FlashCardRepository.GetAllBySetName(setName);
                return flashcards;
            }
            else {
                throw new Exception("study set not found");
            }
        }

        public void Add(string question, string answer, string setName) {

            var studySet = _StudySetRepository.GetByName(setName);

            if (studySet != null)
            {
                Flashcard flashcard = new Flashcard(Guid.NewGuid().ToString(), question, answer, setName);
                flashcard.StudySetId = studySet.Id;

                _FlashCardRepository.Add(flashcard);
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
    }
}
