using System.Collections;
using WebApplicationTestMVC.LeaderboardAPI.Models;

namespace WebApplicationTestMVC.LeaderboardAPI.Interfaces
{
    public interface IAttemptRepository
    {
        Task<IEnumerable<Attempt>> GetAttempts(string setName);
        Task<IEnumerable<Attempt>> GetAttempts();
        Task DeleteAll();
        Task DeleteAttempt(string setName);
        Task AddAttempt(Attempt attempt);
        Task<int> GetTotalTime(string setName);
        Task<int> GetTotalWrongAnswers(string setName);
        Task<int> GetTotalCorrectAnswers(string setName);
        Task<int> GetAttemptCount(string setName);
    }
}
