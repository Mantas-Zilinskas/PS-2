using WebApplicationTestMVC.LeaderboardAPI.Interfaces;
using WebApplicationTestMVC.LeaderboardAPI.Models;

namespace WebApplicationTestMVC.LeaderboardAPI.Services
{
    public class AttemptService : IAttemptService
    {

        private readonly IAttemptRepository _attemptRepository;
        
        public AttemptService(IAttemptRepository repository) 
        { 
            _attemptRepository = repository;
        }

        public async Task AddAttempt(AttemptDTO attemptDTO)
        {
            int count = await _attemptRepository.GetAttemptCount(attemptDTO.SetName);
            Attempt attempt = new Attempt()
            {
                No = ++count,
                SetName = attemptDTO.SetName,
                Time = attemptDTO.Time,
                WrongAnswers = attemptDTO.WrongAnswers,
                CorrectAnswers = attemptDTO.CorrectAnswers,
            };

            await _attemptRepository.AddAttempt(attempt);
        }

        public async Task DeleteAll()
        {
            await _attemptRepository.DeleteAll();
        }

        public async Task DeleteAttempt(string setName)
        {
            await _attemptRepository.DeleteAttempt(setName);
        }

        public Task<IEnumerable<Attempt>> GetAttempts(string setName)
        {
            return _attemptRepository.GetAttempts(setName);
        }

        public async Task<IEnumerable<Attempt>> GetAttempts()
        {
            return await _attemptRepository.GetAttempts();
        }

        public async Task<Stats> GetStats(string setName)
        {
            int count = await _attemptRepository.GetAttemptCount(setName);
            int totalTime = await _attemptRepository.GetTotalTime(setName);
            int totalWrongAnswers = await _attemptRepository.GetTotalWrongAnswers(setName);
            int totalCorrectAnswers = await _attemptRepository.GetTotalCorrectAnswers(setName);

            Stats stats = new(totalTime, totalWrongAnswers, totalCorrectAnswers, count);

            return stats;
        }
    }
}
