using WebAplicationTestMVC.Utilities;

namespace WebAplicationTestMVC.Models
{
    public class StudySet
    {
        public int Id { get; set; } 
        public StudySetColor Color { get; set; }
        public string StudySetName { get; set; }
        public DateTime DateCreated { get; set; } 
        public List<Flashcard> Flashcards { get; set; }
        public TimeSpan StudyTime { get; set; }

        public StudySet(string studySetName)
        {
            StudySetName = studySetName;
        }
    }
}
