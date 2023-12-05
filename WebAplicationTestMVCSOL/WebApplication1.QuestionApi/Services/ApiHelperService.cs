using System.Net.Http.Headers;
using WebApplication1.QuestionApi.Models;

namespace WebApplication1.QuestionApi.Services
{
    public class ApiHelperService
    {
        public HttpClient ApiClient { get; set; }

        public void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TriviaQuestion> GetTrivia()
        {
            {
                using (HttpResponseMessage response = await ApiClient.GetAsync("https://opentdb.com/api.php?amount=1&category=9&difficulty=easy&type=multiple"))
                {
                    if (response.IsSuccessStatusCode)
                    {   
                        var triviaResult = await response.Content.ReadFromJsonAsync<TriviaResult>();
                        var triviaQuestion = triviaResult.Results.ElementAt(0);
                        triviaQuestion.Question = triviaQuestion.Question.Replace("&qout;", "\"");
                        triviaQuestion.Correct_answer = triviaQuestion.Correct_answer.Replace("&qout;", "\"");
                        return triviaQuestion;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
        }
    }
}
