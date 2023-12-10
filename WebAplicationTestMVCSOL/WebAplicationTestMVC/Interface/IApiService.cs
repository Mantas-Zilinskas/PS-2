using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Interface
{
    public interface IApiService
    {
        public Task DeleteAttempts(string setName);
        public Task AddAttempt(int time, int correctsAnswers, int wrongAnswers);
        public Task<StatsViewModel?> GetStats(string setName);
    }
}
