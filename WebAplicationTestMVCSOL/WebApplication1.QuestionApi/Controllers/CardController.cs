using Microsoft.AspNetCore.Mvc;
using WebApplication1.QuestionApi.Models;
using WebApplication1.QuestionApi.Services;

namespace WebApplication1.QuestionApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ApiController
    {
        [HttpGet("GetCard")]
        public async Task<IActionResult> GetTriviaQuestion()
        {
            ApiHelperService helper = new ApiHelperService();
            helper.InitializeClient();
            var result = await helper.GetTrivia();
            return Ok(result);
        }
    }
}