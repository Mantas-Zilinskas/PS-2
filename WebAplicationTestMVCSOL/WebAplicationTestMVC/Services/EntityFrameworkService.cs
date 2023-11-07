using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Services
{
    public class EntityFrameworkService
    {
        private readonly ApplicationDbContext _context;

        public EntityFrameworkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateTable()
        {

            _context.Database.EnsureCreated();
        }

        public void InsertFlashcard(string question, string answer, string setName)
        {

            var studySet = _context.StudySets.SingleOrDefault(s => s.StudySetName == setName);

            if (studySet != null)
            {

                var flashcardId = Guid.NewGuid().ToString();
                var flashcard = new Flashcard(flashcardId, question, answer, setName)
                {
                    StudySetId = studySet.Id
                };

                _context.Flashcards.Add(flashcard);
                _context.SaveChanges();
            }
            else
            {

            }
        }

        public void InsertStudySet(string studySetName)
        {
            var studySet = new StudySet(studySetName)
            {
                DateCreated = DateTime.Now // Set the DateCreated property to the current date and time
            };

            _context.StudySets.Add(studySet);
            _context.SaveChanges();
        }



        public bool FlashcardExists(Flashcard newFlashcard)
        {

            return _context.Flashcards.Any(f => f.Id == newFlashcard.Id);
        }

        public List<Flashcard> GetFlashcardsBySetName(string setName)
        {

            return _context.Flashcards.Where(f => f.SetName == setName).ToList();
        }

        public StudySet GetStudySetByName(string studySetName)
        {
            return _context.StudySets.SingleOrDefault(s => s.StudySetName == studySetName);
        }

        public List<StudySet> GetStudySets()
        {

            return _context.StudySets.ToList();
        }
    }
}