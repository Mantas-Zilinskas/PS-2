using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Services
{
    public class ApiService : IApiService
    {
        HttpClient client = new HttpClient();

        public ApiService()
        {
            //Uri uri = new Uri("http://localhost:44303/");
            Uri uri = new Uri("https://localhost:7106/");

            client.BaseAddress = uri;
        }

        public async Task AddAttempt(string setName, int time, int correctsAnswers, int wrongAnswers)
        {
            var postData = new
            {
                SetName = setName,
                Time = time,
                WrongAnswers = wrongAnswers,
                CorrectAnswers = correctsAnswers
            };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(postData), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(client.BaseAddress + "Attempt/AddAttempt", content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        public async Task DeleteAttempts(string setName)
        {
            HttpResponseMessage response = await client.DeleteAsync(client.BaseAddress + "Attempt/DeleteBySetName/" + setName);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Delete request successful");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        public async Task<StatsViewModel?> GetStats(string setName)
        {

            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "Attempt/GetStats/" + setName);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StatsViewModel>();
            }

            return null;
        }
    }
}
