
namespace WebAplicationTestMVC.Models
{
    public class Flashcard : IEquatable<Flashcard>
    {
        public Flashcard(string id, string question, string answer)
        {
            this.id = id;
            this.question = question;
            this.answer = answer;
        }
        public string? answer { get; set; }
        public string? question { get; set; }
        public string? id { get; set; }

        public bool Equals(Flashcard? otherCard)
        {
            if (otherCard == null)
                return false;

            if (answer == otherCard.answer && question == otherCard.question)
            {
                return true;
            }
            else
                return false;
        }
    }
}