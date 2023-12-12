namespace WebAplicationTestMVC.Models
{
    public class StatsViewModel
    {
        public int TotalAttempts { get; set; } = 0;
        public double AvgTime { get; set; }
        public int TotalTime { get; set; } = 0;
        public double AvgWrongAnswers { get; set; }
        public int TotalWrongAnswers { get; set; } = 0;
        public double AvgCorrectAnswers { get; set; }
        public int TotalCorrectAnswers { get; set; } = 0;
        public double CorrectWrongRatio { get; set; }
    }
}
