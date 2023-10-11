namespace WebAplicationTestMVC.Utilities
{
    public record StudySessionTime(DateTime StartTime, DateTime EndTime)
    {
        public TimeSpan Duration => EndTime - StartTime;
    }

}
