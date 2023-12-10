namespace WebApplicationTestMVC.LeaderboardAPI.Models
{
    public class Stats
    {
        public double AvgTime { get; set; }
        public int TotalTime { get; set; } = 0;
        public double AvgWrongAnswers { get; set; }
        public int TotalWrongAnswers { get; set; } = 0;
        public double AvgCorrectAnswers { get; set; }
        public int TotalCorrectAnswers { get; set; } = 0;
        public double CorrectWrongRatio { get; set; }

        public Stats(int totalTime, int totalWrongAnswers, int totalCorrectAnswers, int count)
        {
            TotalTime = totalTime;
            TotalWrongAnswers = totalWrongAnswers;
            TotalCorrectAnswers = totalCorrectAnswers;
            AvgTime = TotalTime / count;
            AvgWrongAnswers = TotalWrongAnswers / count;
            AvgCorrectAnswers = TotalCorrectAnswers / count;
            CorrectWrongRatio = TotalCorrectAnswers / TotalWrongAnswers;
        }
    }
}
