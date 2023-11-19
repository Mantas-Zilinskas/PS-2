namespace WebAplicationTestMVC.Models
{
    public class FlashcardDTO
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string SetName { get; set; }

        public FlashcardDTO(string id, string question, string answer, string setName)
        {
            Id = id;
            Question = question;
            Answer = answer;
            SetName = setName;
        }
    }
}
