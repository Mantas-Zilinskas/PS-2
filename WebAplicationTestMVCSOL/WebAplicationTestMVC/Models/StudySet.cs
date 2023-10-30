using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Models
{
    public class StudySet
    {
        public StudySetColor Color { get; set; }
        public string StudySetName { get; set; }
        public List<Flashcard> Flashcards { get; set; } 
        public StudySet(string studySetName)
        {
            StudySetName = studySetName;
            Flashcards = new List<Flashcard>(); 
        }
    }
}
