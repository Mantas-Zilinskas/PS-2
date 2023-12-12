using Microsoft.EntityFrameworkCore;
using WebApplicationTestMVC.LeaderboardAPI.Interfaces;
using WebApplicationTestMVC.LeaderboardAPI.Models;

namespace WebApplicationTestMVC.LeaderboardAPI.Repositories
{
    public class AttemptRepository : IAttemptRepository
    {
        private readonly DataContext _context;

        public AttemptRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAttempt(Attempt attempt)
        {
            await _context.Attempt.AddAsync(attempt);
            _context.SaveChanges();
        }

        public async Task DeleteAttempt(string setName)
        {
            await _context.Attempt.Where(a => a.SetName == setName).ExecuteDeleteAsync();
            _context.SaveChanges();
        }

        public async Task DeleteAll()
        {
            await _context.Attempt.ExecuteDeleteAsync();
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Attempt>> GetAttempts(string setName)
        {
            return await _context.Attempt.Where(a => a.SetName == setName).ToListAsync();
        }

        public async Task<IEnumerable<Attempt>> GetAttempts()
        {
            return await _context.Attempt.ToListAsync();
        }

        public async Task<int> GetTotalTime(string setName)
        {
            return await _context.Attempt.Where(a => a.SetName == setName).SumAsync(a => a.Time);
        }

        public async Task<int> GetTotalCorrectAnswers(string setName)
        {
            return await _context.Attempt.Where(a => a.SetName == setName).SumAsync(a => a.CorrectAnswers);
        }

        public async Task<int> GetTotalWrongAnswers(string setName)
        {
            return await _context.Attempt.Where(a => a.SetName == setName).SumAsync(a => a.WrongAnswers);
        }

        public async Task<int> GetAttemptCount(string setName) 
        {
            return await _context.Attempt.Where(a => a.SetName == setName).CountAsync();
        }
    }
}
