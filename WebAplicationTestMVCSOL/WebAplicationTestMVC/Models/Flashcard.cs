using OfficeOpenXml.Drawing.Chart;

namespace WebAplicationTestMVC.Models
{
    public class Flashcard
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
    }
}
