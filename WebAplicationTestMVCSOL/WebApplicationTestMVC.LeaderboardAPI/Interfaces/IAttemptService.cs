using WebApplicationTestMVC.LeaderboardAPI.Models;

namespace WebApplicationTestMVC.LeaderboardAPI.Interfaces
{
    public interface IAttemptService
    {
        Task<IEnumerable<Attempt>> GetAttempts(string setName);
        Task<IEnumerable<Attempt>> GetAttempts();
        Task DeleteAttempt(string setName);
        Task<Stats> GetStats(string setName);
        Task DeleteAll();
        Task AddAttempt(AttemptDTO attempt);
    }
}
