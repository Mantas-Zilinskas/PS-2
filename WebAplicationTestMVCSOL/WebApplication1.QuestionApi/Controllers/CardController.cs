using Microsoft.AspNetCore.Mvc;
using WebApplication1.QuestionApi.Models;
using WebApplication1.QuestionApi.Services;

namespace WebApplication1.QuestionApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CardController> _logger;

        public CardController(ILogger<CardController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetCard")]
        public async Task<TriviaQuestion> GetTriviaQuestion()
        {
            ApiHelperService helper = new ApiHelperService();
            helper.InitializeClient();
            var result = await helper.GetTrivia();
            return result;
        }
    }
}