namespace WebApplicationTestMVC.LeaderboardAPI.Models
{
    public class Attempt
    {
        public int No { get; set; }
        public string SetName { get; set; }
        public int Time { get; set; }
        public int WrongAnswers { get; set; }
        public int CorrectAnswers { get; set; }
    }
}
