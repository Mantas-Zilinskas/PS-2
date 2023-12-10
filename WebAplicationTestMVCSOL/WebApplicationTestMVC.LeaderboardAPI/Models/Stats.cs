namespace WebApplicationTestMVC.LeaderboardAPI.Models
{
    public class Stats
    {
        public int TotalAttempts { get; set; } = 0;
        public double AvgTime { get; set; } = 0;
        public int TotalTime { get; set; } = 0;
        public double AvgWrongAnswers { get; set; } = 0;
        public int TotalWrongAnswers { get; set; } = 0;
        public double AvgCorrectAnswers { get; set; } = 0;
        public int TotalCorrectAnswers { get; set; } = 0;
        public double CorrectWrongRatio { get; set; } = 0;

        public Stats() {}
        public Stats(int totalTime, int totalWrongAnswers, int totalCorrectAnswers, int count)
        {
            if (count != 0)
            {
                TotalAttempts = count;
                TotalTime = totalTime;
                TotalWrongAnswers = totalWrongAnswers;
                TotalCorrectAnswers = totalCorrectAnswers;
                AvgTime = TotalTime / count;
                AvgWrongAnswers = TotalWrongAnswers / count;
                AvgCorrectAnswers = TotalCorrectAnswers / count;
                CorrectWrongRatio = (TotalWrongAnswers == 0) ? 0 : TotalCorrectAnswers / TotalWrongAnswers;
            }
        }
    }
}
