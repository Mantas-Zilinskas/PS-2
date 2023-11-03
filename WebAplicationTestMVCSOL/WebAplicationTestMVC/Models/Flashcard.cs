using System.ComponentModel.DataAnnotations.Schema;

namespace WebAplicationTestMVC.Models
{
    public class Flashcard
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string SetName { get; set; }

        
        public int StudySetId { get; set; }

        
        [ForeignKey("StudySetId")]
        public StudySet StudySet { get; set; }

        public Flashcard(string id, string question, string answer, string setName)
        {
            Id = id;
            Question = question;
            Answer = answer;
            SetName = setName;
        }
    }
}
