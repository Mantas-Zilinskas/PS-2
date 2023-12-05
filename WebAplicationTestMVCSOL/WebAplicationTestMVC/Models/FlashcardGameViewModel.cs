namespace WebAplicationTestMVC.Models
{
    public class FlashcardGameViewModel
    {
        public List<FlashcardDTO> Questions { get; set; }
        public List<FlashcardDTO> Answers { get; set; }

        public FlashcardGameViewModel()
        {
            Questions = new List<FlashcardDTO>();
            Answers = new List<FlashcardDTO>();
        }
    }
}
