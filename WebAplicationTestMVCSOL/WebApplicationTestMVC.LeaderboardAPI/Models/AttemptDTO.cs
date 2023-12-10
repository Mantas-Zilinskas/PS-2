namespace WebApplicationTestMVC.LeaderboardAPI.Models
{
    public class AttemptDTO
    {
        public required string SetName { get; set; }
        public int Time { get; set; }
        public int WrongAnswers { get; set; }
        public int CorrectAnswers { get; set; }
    }
}
