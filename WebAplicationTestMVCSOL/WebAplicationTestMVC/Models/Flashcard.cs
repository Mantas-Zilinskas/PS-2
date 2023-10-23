namespace WebAplicationTestMVC.Models
{
    public class Flashcard : IEquatable<Flashcard>
    {
        public Flashcard(string id, string question, string answer)
        {
            this.Id = id;
            this.Question = question;
            this.Answer = answer;
        }

        public string Id { get; set; } // Add the Id property
        public string Question { get; set; }
        public string Answer { get; set; }

        public bool Equals(Flashcard otherCard)
        {
            if (otherCard == null)
                return false;

            if (Id == otherCard.Id && Question == otherCard.Question && Answer == otherCard.Answer)
            {
                return true;
            }
            else
                return false;
        }
    }
}
