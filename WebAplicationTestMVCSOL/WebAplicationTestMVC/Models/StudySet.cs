using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Models
{
    public class StudySet
    {
        public int Id { get; set; } // Primary key property
        public StudySetColor Color { get; set; }
        public string StudySetName { get; set; }
        public List<Flashcard> Flashcards { get; set; }

        public StudySet(string studySetName)
        {
            StudySetName = studySetName;
        }
    }

}
